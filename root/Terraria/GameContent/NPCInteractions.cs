using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x02000240 RID: 576
	public class NPCInteractions
	{
		// Token: 0x060022A5 RID: 8869 RVA: 0x00539730 File Offset: 0x00537930
		public static void Initialize()
		{
			NPCInteractions.Shop(17, 1, null);
			NPCInteractions.Shop(19, 2, null);
			NPCInteractions.Shop(20, 3, null);
			NPCInteractions.Shop(38, 4, null);
			NPCInteractions.Shop(54, 5, null);
			NPCInteractions.Shop(107, 6, null);
			NPCInteractions.Shop(108, 7, null);
			NPCInteractions.Shop(124, 8, null);
			NPCInteractions.Shop(142, 9, null);
			NPCInteractions.Shop(160, 10, null);
			NPCInteractions.Shop(178, 11, null);
			NPCInteractions.Shop(207, 12, null);
			NPCInteractions.Shop(208, 13, null);
			NPCInteractions.Shop(209, 14, null);
			NPCInteractions.Shop(227, 15, null);
			NPCInteractions.Shop(228, 16, null);
			NPCInteractions.Shop(229, 17, null);
			NPCInteractions.Shop(353, 18, null);
			NPCInteractions.Shop(368, 19, null);
			NPCInteractions.Shop(453, 20, null);
			NPCInteractions.Shop(550, 21, null);
			NPCInteractions.Shop(588, 22, null);
			NPCInteractions.Shop(633, 23, null);
			NPCInteractions.Shop(663, 24, null);
			NPCInteractions.Shop(227, 25, "GameUI.PainterDecor");
			NPCInteractions.Register(new NPCInteractions.Actions.TaxCollectorCollectTaxes());
			NPCInteractions.Register(new NPCInteractions.Actions.NurseHeal());
			NPCInteractions.Register(new NPCInteractions.Actions.CloseChat());
			NPCInteractions.Register(new NPCInteractions.Actions.OpenSign());
			NPCInteractions.Register(new NPCInteractions.Actions.StardewValleyBit());
			NPCInteractions.Register(new NPCInteractions.Actions.DryadPurification());
			NPCInteractions.Register(new NPCInteractions.Actions.AnglerQuest());
			NPCInteractions.Register(new NPCInteractions.Actions.PetAnimal());
			NPCInteractions.Register(new NPCInteractions.Actions.OldManCurse());
			NPCInteractions.Register(new NPCInteractions.Actions.GuideTip());
			NPCInteractions.Register(new NPCInteractions.Actions.PartyGirlMusicSwap());
			NPCInteractions.Register(new NPCInteractions.Actions.GuideReverseCrafting());
			NPCInteractions.Register(new NPCInteractions.Actions.TinkererReforge());
			NPCInteractions.Register(new NPCInteractions.Actions.StylistHairWindow());
			NPCInteractions.Register(new NPCInteractions.Actions.DyeTraderRarePlant());
			NPCInteractions.Register(new NPCInteractions.Actions.TavernkeepAdvice());
			NPCInteractions.Register(new NPCInteractions.Actions.ReportHappiness());
			NPCInteractions.Register(new NPCInteractions.Actions.RequestHome());
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0053991A File Offset: 0x00537B1A
		private static void Shop(int npcType, int shopIndex, string customTextKey = null)
		{
			NPCInteractions.Register(new NPCInteractions.Actions.OpenShop(npcType, shopIndex, customTextKey));
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x00539929 File Offset: 0x00537B29
		private static void Register(NPCInteraction interaction)
		{
			NPCInteractions.All.Add(interaction);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x0000357B File Offset: 0x0000177B
		public NPCInteractions()
		{
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00539936 File Offset: 0x00537B36
		// Note: this type is marked as 'beforefieldinit'.
		static NPCInteractions()
		{
		}

		// Token: 0x04004D08 RID: 19720
		public static List<NPCInteraction> All = new List<NPCInteraction>();

		// Token: 0x020007CA RID: 1994
		public static class Actions
		{
			// Token: 0x02000AB9 RID: 2745
			public class OpenSign : NPCInteraction
			{
				// Token: 0x06004C27 RID: 19495 RVA: 0x006DA83A File Offset: 0x006D8A3A
				public override bool Condition()
				{
					return base.LocalPlayer.sign > -1;
				}

				// Token: 0x06004C28 RID: 19496 RVA: 0x006DA84A File Offset: 0x006D8A4A
				public override string GetText()
				{
					if (Main.editSign)
					{
						return Lang.inter[47].Value;
					}
					return Lang.inter[48].Value;
				}

				// Token: 0x06004C29 RID: 19497 RVA: 0x006DA86E File Offset: 0x006D8A6E
				public override void Interact()
				{
					if (Main.editSign)
					{
						Main.SubmitSignText();
						return;
					}
					IngameFancyUI.OpenVirtualKeyboard(1);
				}

				// Token: 0x06004C2A RID: 19498 RVA: 0x006DA883 File Offset: 0x006D8A83
				public OpenSign()
				{
				}
			}

			// Token: 0x02000ABA RID: 2746
			public class OpenShop : NPCInteraction
			{
				// Token: 0x06004C2B RID: 19499 RVA: 0x006DA88B File Offset: 0x006D8A8B
				public OpenShop(int npcType, int shopIndex, string customTextKey = null)
				{
					this._npcType = npcType;
					this._shopIndex = shopIndex;
					this._customTextKey = customTextKey;
				}

				// Token: 0x06004C2C RID: 19500 RVA: 0x006DA8A8 File Offset: 0x006D8AA8
				public override bool Condition()
				{
					return base.TalkNPCType == this._npcType;
				}

				// Token: 0x06004C2D RID: 19501 RVA: 0x006DA8B8 File Offset: 0x006D8AB8
				public override string GetText()
				{
					if (this._customTextKey != null)
					{
						return Language.GetTextValue(this._customTextKey);
					}
					return Lang.inter[28].Value;
				}

				// Token: 0x06004C2E RID: 19502 RVA: 0x006DA8DB File Offset: 0x006D8ADB
				public override void Interact()
				{
					Main.instance.OpenShop(this._shopIndex);
				}

				// Token: 0x04007879 RID: 30841
				private int _shopIndex;

				// Token: 0x0400787A RID: 30842
				private int _npcType;

				// Token: 0x0400787B RID: 30843
				private string _customTextKey;
			}

			// Token: 0x02000ABB RID: 2747
			public class StardewValleyBit : NPCInteraction
			{
				// Token: 0x170005BF RID: 1471
				// (get) Token: 0x06004C2F RID: 19503 RVA: 0x000379E9 File Offset: 0x00035BE9
				public override bool ShowExcalmation
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06004C30 RID: 19504 RVA: 0x006DA8ED File Offset: 0x006D8AED
				public override bool Condition()
				{
					return base.TalkNPCType == 20 && Main.CanDryadPlayStardewAnimation(base.LocalPlayer, base.TalkNPC);
				}

				// Token: 0x06004C31 RID: 19505 RVA: 0x006DA90C File Offset: 0x006D8B0C
				public override string GetText()
				{
					return Language.GetTextValue("StardewTalk.GiveColaButtonText");
				}

				// Token: 0x06004C32 RID: 19506 RVA: 0x006DA918 File Offset: 0x006D8B18
				public override void Interact()
				{
					Main.DoNPCPortraitHop();
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.DryadText_Do_StardewValleyBit();
				}

				// Token: 0x06004C33 RID: 19507 RVA: 0x006DA883 File Offset: 0x006D8A83
				public StardewValleyBit()
				{
				}
			}

			// Token: 0x02000ABC RID: 2748
			public class DryadPurification : NPCInteraction
			{
				// Token: 0x06004C34 RID: 19508 RVA: 0x006DA939 File Offset: 0x006D8B39
				public override bool Condition()
				{
					return base.TalkNPCType == 20 && !Main.CanDryadPlayStardewAnimation(base.LocalPlayer, base.TalkNPC);
				}

				// Token: 0x06004C35 RID: 19509 RVA: 0x006DA95B File Offset: 0x006D8B5B
				public override string GetText()
				{
					return Lang.inter[49].Value;
				}

				// Token: 0x06004C36 RID: 19510 RVA: 0x006DA96C File Offset: 0x006D8B6C
				public override void Interact()
				{
					Main.DoNPCPortraitHop();
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					bool flag;
					Main.npcChatText = Lang.GetDryadWorldStatusDialog(out flag);
					if (flag)
					{
						AchievementsHelper.HandleSpecialEvent(base.LocalPlayer, 27);
					}
				}

				// Token: 0x06004C37 RID: 19511 RVA: 0x006DA883 File Offset: 0x006D8A83
				public DryadPurification()
				{
				}
			}

			// Token: 0x02000ABD RID: 2749
			public class AnglerQuest : NPCInteraction
			{
				// Token: 0x170005C0 RID: 1472
				// (get) Token: 0x06004C38 RID: 19512 RVA: 0x006DA9AF File Offset: 0x006D8BAF
				public override bool ShowExcalmation
				{
					get
					{
						return !Main.anglerQuestFinished;
					}
				}

				// Token: 0x06004C39 RID: 19513 RVA: 0x006DA9B9 File Offset: 0x006D8BB9
				public override bool Condition()
				{
					return base.TalkNPCType == 369;
				}

				// Token: 0x06004C3A RID: 19514 RVA: 0x006DA9C8 File Offset: 0x006D8BC8
				public override string GetText()
				{
					return Lang.inter[64].Value;
				}

				// Token: 0x06004C3B RID: 19515 RVA: 0x006DA9D7 File Offset: 0x006D8BD7
				public override void Interact()
				{
					Main.NPCChatText_DoAnglerQuest();
				}

				// Token: 0x06004C3C RID: 19516 RVA: 0x006DA883 File Offset: 0x006D8A83
				public AnglerQuest()
				{
				}
			}

			// Token: 0x02000ABE RID: 2750
			public class PetAnimal : NPCInteraction
			{
				// Token: 0x06004C3D RID: 19517 RVA: 0x006DA9DE File Offset: 0x006D8BDE
				public override bool Condition()
				{
					return NPCID.Sets.IsTownPet[base.TalkNPCType];
				}

				// Token: 0x06004C3E RID: 19518 RVA: 0x006DA9EC File Offset: 0x006D8BEC
				public override string GetText()
				{
					return Language.GetTextValue("UI.PetTheAnimal");
				}

				// Token: 0x06004C3F RID: 19519 RVA: 0x006DA9F8 File Offset: 0x006D8BF8
				public override void Interact()
				{
					base.LocalPlayer.PetAnimal(Main.npc[base.LocalPlayer.talkNPC].GetPettingInfo(base.LocalPlayer));
				}

				// Token: 0x06004C40 RID: 19520 RVA: 0x006DA883 File Offset: 0x006D8A83
				public PetAnimal()
				{
				}
			}

			// Token: 0x02000ABF RID: 2751
			public class OldManCurse : NPCInteraction
			{
				// Token: 0x06004C41 RID: 19521 RVA: 0x006DAA21 File Offset: 0x006D8C21
				public override bool Condition()
				{
					return base.TalkNPCType == 37 && !Main.IsItDay();
				}

				// Token: 0x06004C42 RID: 19522 RVA: 0x006DAA37 File Offset: 0x006D8C37
				public override string GetText()
				{
					return Lang.inter[50].Value;
				}

				// Token: 0x06004C43 RID: 19523 RVA: 0x006DAA48 File Offset: 0x006D8C48
				public override void Interact()
				{
					if (Main.netMode == 0)
					{
						NPC.SpawnSkeletron(Main.myPlayer, false);
					}
					else
					{
						NetMessage.SendData(51, -1, -1, null, Main.myPlayer, 1f, 0f, 0f, 0, 0, 0);
					}
					Main.npcChatText = "";
				}

				// Token: 0x06004C44 RID: 19524 RVA: 0x006DA883 File Offset: 0x006D8A83
				public OldManCurse()
				{
				}
			}

			// Token: 0x02000AC0 RID: 2752
			public class GuideTip : NPCInteraction
			{
				// Token: 0x06004C45 RID: 19525 RVA: 0x006DAA94 File Offset: 0x006D8C94
				public override bool Condition()
				{
					return base.TalkNPCType == 22;
				}

				// Token: 0x06004C46 RID: 19526 RVA: 0x006DAAA0 File Offset: 0x006D8CA0
				public override string GetText()
				{
					return Lang.inter[51].Value;
				}

				// Token: 0x06004C47 RID: 19527 RVA: 0x006DAAAF File Offset: 0x006D8CAF
				public override void Interact()
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.HelpText();
					Main.DoNPCPortraitHop();
				}

				// Token: 0x06004C48 RID: 19528 RVA: 0x006DA883 File Offset: 0x006D8A83
				public GuideTip()
				{
				}
			}

			// Token: 0x02000AC1 RID: 2753
			public class TaxCollectorCollectTaxes : NPCInteraction
			{
				// Token: 0x06004C49 RID: 19529 RVA: 0x006DAAD0 File Offset: 0x006D8CD0
				public override bool Condition()
				{
					return base.TalkNPCType == 441;
				}

				// Token: 0x06004C4A RID: 19530 RVA: 0x006DAADF File Offset: 0x006D8CDF
				public override string GetText()
				{
					return Lang.inter[89].Value;
				}

				// Token: 0x06004C4B RID: 19531 RVA: 0x006DAAEE File Offset: 0x006D8CEE
				public override void Interact()
				{
					Main.NPCChatText_DoTaxCollector();
				}

				// Token: 0x06004C4C RID: 19532 RVA: 0x006DAAF5 File Offset: 0x006D8CF5
				public override bool TryAddCoins(ref Color chatColor, out int coinValue)
				{
					coinValue = 0;
					Main.GetCoinValueText_TaxCollector(ref chatColor, ref coinValue);
					return coinValue > 0;
				}

				// Token: 0x06004C4D RID: 19533 RVA: 0x006DA883 File Offset: 0x006D8A83
				public TaxCollectorCollectTaxes()
				{
				}
			}

			// Token: 0x02000AC2 RID: 2754
			public class NurseHeal : NPCInteraction
			{
				// Token: 0x06004C4E RID: 19534 RVA: 0x006DAB07 File Offset: 0x006D8D07
				public override bool Condition()
				{
					return base.TalkNPCType == 18;
				}

				// Token: 0x06004C4F RID: 19535 RVA: 0x006DAB13 File Offset: 0x006D8D13
				public override string GetText()
				{
					return Lang.inter[54].Value;
				}

				// Token: 0x06004C50 RID: 19536 RVA: 0x006DAB22 File Offset: 0x006D8D22
				public override void Interact()
				{
					Main.NPCChatText_DoNurseHeal(Main.GetNurseHealCost());
				}

				// Token: 0x06004C51 RID: 19537 RVA: 0x006DAB2E File Offset: 0x006D8D2E
				public override bool TryAddCoins(ref Color chatColor, out int coinValue)
				{
					coinValue = Main.GetNurseHealCost();
					Main.GetCoinValueText_Nurse(ref chatColor, ref coinValue);
					return coinValue > 0;
				}

				// Token: 0x06004C52 RID: 19538 RVA: 0x006DA883 File Offset: 0x006D8A83
				public NurseHeal()
				{
				}
			}

			// Token: 0x02000AC3 RID: 2755
			public class CloseChat : NPCInteraction
			{
				// Token: 0x06004C53 RID: 19539 RVA: 0x000379E9 File Offset: 0x00035BE9
				public override bool Condition()
				{
					return true;
				}

				// Token: 0x06004C54 RID: 19540 RVA: 0x006DAB44 File Offset: 0x006D8D44
				public override string GetText()
				{
					return Lang.inter[52].Value;
				}

				// Token: 0x06004C55 RID: 19541 RVA: 0x006DAB53 File Offset: 0x006D8D53
				public override void Interact()
				{
					Main.CloseNPCChatOrSign(false);
				}

				// Token: 0x06004C56 RID: 19542 RVA: 0x006DA883 File Offset: 0x006D8A83
				public CloseChat()
				{
				}
			}

			// Token: 0x02000AC4 RID: 2756
			public class ReportHappiness : NPCInteraction
			{
				// Token: 0x06004C57 RID: 19543 RVA: 0x006DAB5B File Offset: 0x006D8D5B
				public override bool Condition()
				{
					return !NPC.CanShowHomelessText(Main.LocalPlayer.talkNPC) && base.LocalPlayer.currentShoppingSettings.HappinessReport != "";
				}

				// Token: 0x06004C58 RID: 19544 RVA: 0x006DAB8A File Offset: 0x006D8D8A
				public override string GetText()
				{
					return Language.GetTextValue("UI.NPCCheckHappiness");
				}

				// Token: 0x06004C59 RID: 19545 RVA: 0x006DAB96 File Offset: 0x006D8D96
				public override void Interact()
				{
					Main.npcChatCornerItem = 0;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.npcChatText = base.LocalPlayer.currentShoppingSettings.HappinessReport;
					Main.DoNPCPortraitHop();
				}

				// Token: 0x06004C5A RID: 19546 RVA: 0x006DA883 File Offset: 0x006D8A83
				public ReportHappiness()
				{
				}
			}

			// Token: 0x02000AC5 RID: 2757
			public class RequestHome : NPCInteraction
			{
				// Token: 0x170005C1 RID: 1473
				// (get) Token: 0x06004C5B RID: 19547 RVA: 0x000379E9 File Offset: 0x00035BE9
				public override bool ShowExcalmation
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06004C5C RID: 19548 RVA: 0x006DABCD File Offset: 0x006D8DCD
				public override bool Condition()
				{
					return NPC.CanShowHomelessText(Main.LocalPlayer.talkNPC);
				}

				// Token: 0x06004C5D RID: 19549 RVA: 0x006DABDE File Offset: 0x006D8DDE
				public override string GetText()
				{
					return Language.GetTextValue("UI.NPCHousing");
				}

				// Token: 0x06004C5E RID: 19550 RVA: 0x006DABEC File Offset: 0x006D8DEC
				public override void Interact()
				{
					Main.npcChatCornerItem = -1;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					Main.DoNPCPortraitHop();
					NPC talkNPC = base.TalkNPC;
					string text = "TownNPCMood_" + NPCID.Search.GetName(talkNPC.netID);
					if (talkNPC.type == 633 && talkNPC.altTexture == 2)
					{
						text += "Transformed";
					}
					if (talkNPC.type == 638)
					{
						text = "DogChatter";
					}
					else if (talkNPC.type == 637)
					{
						text = "CatChatter";
					}
					else if (talkNPC.type == 656)
					{
						text = "BunnyChatter";
					}
					else if (NPCID.Sets.IsTownSlime[talkNPC.type])
					{
						string slimeType = Lang.GetSlimeType(talkNPC);
						text = "Slime" + slimeType + "Chatter";
					}
					Main.npcChatText = Language.GetTextValue(text + ".NoHome");
					Main.npcChatText += "\n\n";
					if (talkNPC.type == 160)
					{
						Main.npcChatText += Language.GetTextValueWith("HousingText.HousingRequirements_Truffle", new
						{
							NPCName = talkNPC.FullName
						});
						return;
					}
					Main.npcChatText += Language.GetTextValue("HousingText.HousingRequirements");
				}

				// Token: 0x06004C5F RID: 19551 RVA: 0x006DA883 File Offset: 0x006D8A83
				public RequestHome()
				{
				}
			}

			// Token: 0x02000AC6 RID: 2758
			public class PartyGirlMusicSwap : NPCInteraction
			{
				// Token: 0x06004C60 RID: 19552 RVA: 0x006DAD36 File Offset: 0x006D8F36
				public override bool Condition()
				{
					return base.TalkNPCType == 208;
				}

				// Token: 0x06004C61 RID: 19553 RVA: 0x006DAD45 File Offset: 0x006D8F45
				public override string GetText()
				{
					return Language.GetTextValue("GameUI.Music");
				}

				// Token: 0x06004C62 RID: 19554 RVA: 0x006DAD51 File Offset: 0x006D8F51
				public override void Interact()
				{
					Main.NPCChatText_PartyGirlSwapMusic();
				}

				// Token: 0x06004C63 RID: 19555 RVA: 0x006DA883 File Offset: 0x006D8A83
				public PartyGirlMusicSwap()
				{
				}
			}

			// Token: 0x02000AC7 RID: 2759
			public class GuideReverseCrafting : NPCInteraction
			{
				// Token: 0x06004C64 RID: 19556 RVA: 0x006DAA94 File Offset: 0x006D8C94
				public override bool Condition()
				{
					return base.TalkNPCType == 22;
				}

				// Token: 0x06004C65 RID: 19557 RVA: 0x006DAD58 File Offset: 0x006D8F58
				public override string GetText()
				{
					return Lang.inter[25].Value;
				}

				// Token: 0x06004C66 RID: 19558 RVA: 0x006DAD67 File Offset: 0x006D8F67
				public override void Interact()
				{
					Main.NPCChatText_GuideReverseCrafting();
				}

				// Token: 0x06004C67 RID: 19559 RVA: 0x006DA883 File Offset: 0x006D8A83
				public GuideReverseCrafting()
				{
				}
			}

			// Token: 0x02000AC8 RID: 2760
			public class TinkererReforge : NPCInteraction
			{
				// Token: 0x06004C68 RID: 19560 RVA: 0x006DAD6E File Offset: 0x006D8F6E
				public override bool Condition()
				{
					return base.TalkNPCType == 107;
				}

				// Token: 0x06004C69 RID: 19561 RVA: 0x006DAD7A File Offset: 0x006D8F7A
				public override string GetText()
				{
					return Lang.inter[19].Value;
				}

				// Token: 0x06004C6A RID: 19562 RVA: 0x006DAD89 File Offset: 0x006D8F89
				public override void Interact()
				{
					Main.NPCChatText_TinkererReforge();
				}

				// Token: 0x06004C6B RID: 19563 RVA: 0x006DA883 File Offset: 0x006D8A83
				public TinkererReforge()
				{
				}
			}

			// Token: 0x02000AC9 RID: 2761
			public class StylistHairWindow : NPCInteraction
			{
				// Token: 0x06004C6C RID: 19564 RVA: 0x006DAD90 File Offset: 0x006D8F90
				public override bool Condition()
				{
					return base.TalkNPCType == 353;
				}

				// Token: 0x06004C6D RID: 19565 RVA: 0x006DAD9F File Offset: 0x006D8F9F
				public override string GetText()
				{
					return Language.GetTextValue("GameUI.HairStyle");
				}

				// Token: 0x06004C6E RID: 19566 RVA: 0x006DADAB File Offset: 0x006D8FAB
				public override void Interact()
				{
					Main.OpenHairWindow();
				}

				// Token: 0x06004C6F RID: 19567 RVA: 0x006DA883 File Offset: 0x006D8A83
				public StylistHairWindow()
				{
				}
			}

			// Token: 0x02000ACA RID: 2762
			public class DyeTraderRarePlant : NPCInteraction
			{
				// Token: 0x06004C70 RID: 19568 RVA: 0x006DADB2 File Offset: 0x006D8FB2
				public override bool Condition()
				{
					return base.TalkNPCType == 207 && Main.hardMode;
				}

				// Token: 0x06004C71 RID: 19569 RVA: 0x006DADC8 File Offset: 0x006D8FC8
				public override string GetText()
				{
					return Lang.inter[107].Value;
				}

				// Token: 0x06004C72 RID: 19570 RVA: 0x006DADD7 File Offset: 0x006D8FD7
				public override void Interact()
				{
					Main.NPCChatText_DyeTraderRarePlant();
				}

				// Token: 0x06004C73 RID: 19571 RVA: 0x006DA883 File Offset: 0x006D8A83
				public DyeTraderRarePlant()
				{
				}
			}

			// Token: 0x02000ACB RID: 2763
			public class TavernkeepAdvice : NPCInteraction
			{
				// Token: 0x06004C74 RID: 19572 RVA: 0x006DADDE File Offset: 0x006D8FDE
				public override bool Condition()
				{
					return base.TalkNPCType == 550;
				}

				// Token: 0x06004C75 RID: 19573 RVA: 0x006DADED File Offset: 0x006D8FED
				public override string GetText()
				{
					return Language.GetTextValue("UI.BartenderHelp");
				}

				// Token: 0x06004C76 RID: 19574 RVA: 0x006DADF9 File Offset: 0x006D8FF9
				public override void Interact()
				{
					Main.NPCChatText_TavernkeepAdvice();
				}

				// Token: 0x06004C77 RID: 19575 RVA: 0x006DA883 File Offset: 0x006D8A83
				public TavernkeepAdvice()
				{
				}
			}
		}
	}
}
