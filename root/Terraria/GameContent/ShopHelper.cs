using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x02000264 RID: 612
	public class ShopHelper
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x00540144 File Offset: 0x0053E344
		public ShopHelper()
		{
			this._database = new PersonalityDatabase();
			new PersonalityDatabasePopulator().Populate(this._database);
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00540198 File Offset: 0x0053E398
		public ShoppingSettings GetShoppingSettings(Player player, NPC npc)
		{
			ShoppingSettings shoppingSettings = new ShoppingSettings
			{
				PriceAdjustment = 1f,
				HappinessReport = ""
			};
			this._currentNPCBeingTalkedTo = npc;
			this._currentPlayerTalking = player;
			this.ProcessMood(player, npc);
			shoppingSettings.PriceAdjustment = this._currentPriceAdjustment;
			shoppingSettings.HappinessReport = this._currentHappiness;
			return shoppingSettings;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x005401F8 File Offset: 0x0053E3F8
		private float GetSkeletonMerchantPrices(NPC npc)
		{
			float num = 1f;
			if (Main.moonPhase == 1 || Main.moonPhase == 7)
			{
				num = 1.1f;
			}
			if (Main.moonPhase == 2 || Main.moonPhase == 6)
			{
				num = 1.2f;
			}
			if (Main.moonPhase == 3 || Main.moonPhase == 5)
			{
				num = 1.3f;
			}
			if (Main.moonPhase == 4)
			{
				num = 1.4f;
			}
			if (Main.dayTime)
			{
				num += 0.1f;
			}
			return num;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0054026C File Offset: 0x0053E46C
		private float GetTravelingMerchantPrices(NPC npc)
		{
			Vector2 vector = npc.Center / 16f;
			Vector2 vector2 = new Vector2((float)Main.spawnTileX, (float)Main.spawnTileY);
			float num = Vector2.Distance(vector, vector2) / (float)(Main.maxTilesX / 2);
			num = 1.5f - num;
			return (2f + num) / 3f;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x005402C4 File Offset: 0x0053E4C4
		private void ProcessMood(Player player, NPC npc)
		{
			this._currentHappiness = "";
			this._currentPriceAdjustment = 1f;
			if (npc.loveStruck)
			{
				this._currentPriceAdjustment *= 0.9f;
			}
			if (Main.remixWorld)
			{
				return;
			}
			if (npc.type == 368)
			{
				return;
			}
			if (npc.type == 453)
			{
				return;
			}
			if (NPCID.Sets.IsTownPet[npc.type])
			{
				return;
			}
			if (this.IsNotReallyTownNPC(npc))
			{
				return;
			}
			if (this.RuinMoodIfHomeless(npc))
			{
				this._currentPriceAdjustment = 1000f;
			}
			else if (this.IsFarFromHome(npc))
			{
				this._currentPriceAdjustment = 1000f;
			}
			if (this.IsPlayerInEvilBiomes(player))
			{
				this._currentPriceAdjustment = 1000f;
			}
			int num;
			int num2;
			List<NPC> nearbyResidentNPCs = this.GetNearbyResidentNPCs(npc, out num, out num2);
			bool flag = true;
			bool flag2 = true;
			float num3 = 1.05f;
			if (npc.type == 663)
			{
				flag = false;
				num3 = 1f;
				if (num < 2 && num2 < 2)
				{
					this.AddHappinessReportText("HateLonely", null);
					this._currentPriceAdjustment = 1000f;
				}
			}
			if (flag2 && num > 3)
			{
				for (int i = 3; i < num; i++)
				{
					this._currentPriceAdjustment *= num3;
				}
				if (num > 6)
				{
					this.AddHappinessReportText("HateCrowded", null);
				}
				else
				{
					this.AddHappinessReportText("DislikeCrowded", null);
				}
			}
			if (flag && num <= 2 && num2 < 4)
			{
				this.AddHappinessReportText("LoveSpace", null);
				this._currentPriceAdjustment *= 0.95f;
			}
			bool[] array = new bool[(int)NPCID.Count];
			foreach (NPC npc2 in nearbyResidentNPCs)
			{
				array[npc2.type] = true;
			}
			HelperInfo helperInfo = new HelperInfo
			{
				player = player,
				npc = npc,
				NearbyNPCs = nearbyResidentNPCs,
				nearbyNPCsByType = array
			};
			foreach (IShopPersonalityTrait shopPersonalityTrait in this._database.GetByNPCID(npc.type).ShopModifiers)
			{
				shopPersonalityTrait.ModifyShopPrice(helperInfo, this);
			}
			new AllPersonalitiesModifier().ModifyShopPrice(helperInfo, this);
			if (this._currentHappiness == "")
			{
				this.AddHappinessReportText("Content", null);
			}
			this._currentPriceAdjustment = this.LimitAndRoundMultiplier(this._currentPriceAdjustment);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00540544 File Offset: 0x0053E744
		private float LimitAndRoundMultiplier(float priceAdjustment)
		{
			priceAdjustment = MathHelper.Clamp(priceAdjustment, 0.75f, 1.5f);
			priceAdjustment = (float)Math.Round((double)(priceAdjustment * 100f)) / 100f;
			return priceAdjustment;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x0054056F File Offset: 0x0053E76F
		private static string BiomeNameByKey(string biomeNameKey)
		{
			return Language.GetTextValue("TownNPCMoodBiomes." + biomeNameKey);
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00540584 File Offset: 0x0053E784
		private void AddHappinessReportText(string textKeyInCategory, object substitutes = null)
		{
			string text = "TownNPCMood_" + NPCID.Search.GetName(this._currentNPCBeingTalkedTo.netID);
			if (this._currentNPCBeingTalkedTo.type == 633 && this._currentNPCBeingTalkedTo.altTexture == 2)
			{
				text += "Transformed";
			}
			string textValueWith = Language.GetTextValueWith(text + "." + textKeyInCategory, substitutes);
			this._currentHappiness = this._currentHappiness + textValueWith + " ";
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00540607 File Offset: 0x0053E807
		public void LikeBiome(string nameKey)
		{
			this.AddHappinessReportText("LikeBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00540631 File Offset: 0x0053E831
		public void LoveBiome(string nameKey)
		{
			this.AddHappinessReportText("LoveBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x0054065B File Offset: 0x0053E85B
		public void DislikeBiome(string nameKey)
		{
			this.AddHappinessReportText("DislikeBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 1.06f;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00540685 File Offset: 0x0053E885
		public void HateBiome(string nameKey)
		{
			this.AddHappinessReportText("HateBiome", new
			{
				BiomeName = ShopHelper.BiomeNameByKey(nameKey)
			});
			this._currentPriceAdjustment *= 1.12f;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x005406AF File Offset: 0x0053E8AF
		public void LikeNPC(int npcType)
		{
			this.AddHappinessReportText("LikeNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x005406D9 File Offset: 0x0053E8D9
		public void LoveNPCByTypeName(int npcType)
		{
			this.AddHappinessReportText("LoveNPC_" + NPCID.Search.GetName(npcType), new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00540713 File Offset: 0x0053E913
		public void LikePrincess()
		{
			this.AddHappinessReportText("LikeNPC_Princess", new
			{
				NPCName = NPC.GetFullnameByID(663)
			});
			this._currentPriceAdjustment *= 0.94f;
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00540741 File Offset: 0x0053E941
		public void LoveNPC(int npcType)
		{
			this.AddHappinessReportText("LoveNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 0.88f;
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x0054076B File Offset: 0x0053E96B
		public void DislikeNPC(int npcType)
		{
			this.AddHappinessReportText("DislikeNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 1.06f;
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00540795 File Offset: 0x0053E995
		public void HateNPC(int npcType)
		{
			this.AddHappinessReportText("HateNPC", new
			{
				NPCName = NPC.GetFullnameByID(npcType)
			});
			this._currentPriceAdjustment *= 1.12f;
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x005407C0 File Offset: 0x0053E9C0
		private List<NPC> GetNearbyResidentNPCs(NPC npc, out int npcsWithinHouse, out int npcsWithinVillage)
		{
			List<NPC> list = new List<NPC>();
			npcsWithinHouse = 0;
			npcsWithinVillage = 0;
			Vector2 vector = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
			if (npc.homeless)
			{
				vector = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
			}
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (i != npc.whoAmI)
				{
					NPC npc2 = Main.npc[i];
					if (npc2.active && npc2.townNPC && !this.IsNotReallyTownNPC(npc2) && !WorldGen.TownManager.CanNPCsLiveWithEachOther_ShopHelper(npc, npc2))
					{
						Vector2 vector2 = new Vector2((float)npc2.homeTileX, (float)npc2.homeTileY);
						if (npc2.homeless)
						{
							vector2 = npc2.Center / 16f;
						}
						float num = Vector2.Distance(vector, vector2);
						if (num < 25f)
						{
							list.Add(npc2);
							npcsWithinHouse++;
						}
						else if (num < 120f)
						{
							npcsWithinVillage++;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x005408D1 File Offset: 0x0053EAD1
		private bool RuinMoodIfHomeless(NPC npc)
		{
			if (npc.homeless)
			{
				this.AddHappinessReportText("NoHome", null);
			}
			return npc.homeless;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x005408F0 File Offset: 0x0053EAF0
		private bool IsFarFromHome(NPC npc)
		{
			Vector2 vector = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
			Vector2 vector2 = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
			if (Vector2.Distance(vector, vector2) > 120f)
			{
				this.AddHappinessReportText("FarFromHome", null);
				return true;
			}
			return false;
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00540958 File Offset: 0x0053EB58
		private bool IsPlayerInEvilBiomes(Player player)
		{
			for (int i = 0; i < this._dangerousBiomes.Length; i++)
			{
				AShoppingBiome ashoppingBiome = this._dangerousBiomes[i];
				if (ashoppingBiome.IsInBiome(player))
				{
					this.AddHappinessReportText("HateBiome", new
					{
						BiomeName = ShopHelper.BiomeNameByKey(ashoppingBiome.NameKey)
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x005409A8 File Offset: 0x0053EBA8
		private bool IsNotReallyTownNPC(NPC npc)
		{
			int type = npc.type;
			return type == 37 || type == 368 || type == 453;
		}

		// Token: 0x04004D9D RID: 19869
		public const float LowestPossiblePriceMultiplier = 0.75f;

		// Token: 0x04004D9E RID: 19870
		public const float MaxHappinessAchievementPriceMultiplier = 0.82f;

		// Token: 0x04004D9F RID: 19871
		public const float HighestPossiblePriceMultiplier = 1.5f;

		// Token: 0x04004DA0 RID: 19872
		private string _currentHappiness;

		// Token: 0x04004DA1 RID: 19873
		private float _currentPriceAdjustment;

		// Token: 0x04004DA2 RID: 19874
		private NPC _currentNPCBeingTalkedTo;

		// Token: 0x04004DA3 RID: 19875
		private Player _currentPlayerTalking;

		// Token: 0x04004DA4 RID: 19876
		private PersonalityDatabase _database;

		// Token: 0x04004DA5 RID: 19877
		private AShoppingBiome[] _dangerousBiomes = new AShoppingBiome[]
		{
			new CorruptionBiome(),
			new CrimsonBiome(),
			new DungeonBiome()
		};

		// Token: 0x04004DA6 RID: 19878
		private const float likeValue = 0.94f;

		// Token: 0x04004DA7 RID: 19879
		private const float dislikeValue = 1.06f;

		// Token: 0x04004DA8 RID: 19880
		private const float loveValue = 0.88f;

		// Token: 0x04004DA9 RID: 19881
		private const float hateValue = 1.12f;
	}
}
