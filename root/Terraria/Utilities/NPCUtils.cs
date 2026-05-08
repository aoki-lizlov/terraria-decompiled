using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Terraria.Utilities
{
	// Token: 0x020000D0 RID: 208
	public static class NPCUtils
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x004E14B2 File Offset: 0x004DF6B2
		public static NPCUtils.TargetSearchResults SearchForTarget(Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(null, position, flags, playerFilter, npcFilter);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x004E14BE File Offset: 0x004DF6BE
		public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			return NPCUtils.SearchForTarget(searcher, searcher.Center, flags, playerFilter, npcFilter);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x004E14D0 File Offset: 0x004DF6D0
		public static NPCUtils.TargetSearchResults SearchForTarget(NPC searcher, Vector2 position, NPCUtils.TargetSearchFlag flags = NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilter<Player> playerFilter = null, NPCUtils.SearchFilter<NPC> npcFilter = null)
		{
			float num = float.MaxValue;
			int num2 = -1;
			float num3 = float.MaxValue;
			float num4 = float.MaxValue;
			int num5 = -1;
			NPCUtils.TargetType targetType = NPCUtils.TargetType.Player;
			if ((flags & NPCUtils.TargetSearchFlag.NPCs) != NPCUtils.TargetSearchFlag.None)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.active && npc.whoAmI != searcher.whoAmI && (npcFilter == null || npcFilter(npc)))
					{
						float num6 = Vector2.DistanceSquared(position, npc.Center);
						if (num6 < num)
						{
							num2 = i;
							num = num6;
						}
					}
				}
			}
			if ((flags & NPCUtils.TargetSearchFlag.Players) != NPCUtils.TargetSearchFlag.None)
			{
				for (int j = 0; j < 255; j++)
				{
					Player player = Main.player[j];
					if (player.active && !player.dead && !player.ghost && (playerFilter == null || playerFilter(player)))
					{
						float num7 = Vector2.Distance(position, player.Center);
						float num8 = num7 - (float)player.aggro;
						bool flag = searcher != null && player.npcTypeNoAggro[searcher.type];
						if (searcher != null && flag && searcher.direction == 0)
						{
							num8 += 1000f;
						}
						if (num8 < num3)
						{
							num5 = j;
							num3 = num8;
							num4 = num7;
							targetType = NPCUtils.TargetType.Player;
						}
						if (player.tankPet >= 0 && !flag)
						{
							Vector2 center = Main.projectile[player.tankPet].Center;
							num7 = Vector2.Distance(position, center);
							num8 = num7 - 200f;
							if (num8 < num3 && num8 < 200f && Collision.CanHit(position, 0, 0, center, 0, 0))
							{
								num5 = j;
								num3 = num8;
								num4 = num7;
								targetType = NPCUtils.TargetType.TankPet;
							}
						}
					}
				}
			}
			return new NPCUtils.TargetSearchResults(searcher, num2, (float)Math.Sqrt((double)num), num5, num4, num3, targetType);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x004E1690 File Offset: 0x004DF890
		public static void TargetClosestOldOnesInvasion(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, NPCUtils.SearchFilters.OnlyPlayersInCertainDistance(searcher.Center, 200f), new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x004E1700 File Offset: 0x004DF900
		public static void TargetClosestNonBees(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, null, new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.NonBeeNPCs));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x004E1764 File Offset: 0x004DF964
		public static void TargetClosestDownwindFromNPC(NPC searcher, float distanceMaxX, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.Players, NPCUtils.SearchFilters.DownwindFromNPC(searcher, distanceMaxX), null);
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, null) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x004E17C0 File Offset: 0x004DF9C0
		public static void TargetClosestCommon(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			searcher.TargetClosest(faceTarget);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x004E17CC File Offset: 0x004DF9CC
		public static void TargetClosestBetsy(NPC searcher, bool faceTarget = true, Vector2? checkPosition = null)
		{
			NPCUtils.TargetSearchResults targetSearchResults = NPCUtils.SearchForTarget(searcher, NPCUtils.TargetSearchFlag.All, null, new NPCUtils.SearchFilter<NPC>(NPCUtils.SearchFilters.OnlyCrystal));
			if (!targetSearchResults.FoundTarget)
			{
				return;
			}
			NPCUtils.TargetType targetType = targetSearchResults.NearestTargetType;
			if (targetSearchResults.FoundTank && !targetSearchResults.NearestTankOwner.dead)
			{
				targetType = NPCUtils.TargetType.Player;
			}
			searcher.target = targetSearchResults.NearestTargetIndex;
			searcher.targetRect = targetSearchResults.NearestTargetHitbox;
			if (searcher.ShouldFaceTarget(ref targetSearchResults, new NPCUtils.TargetType?(targetType)) && faceTarget)
			{
				searcher.FaceTarget();
			}
		}

		// Token: 0x020006F1 RID: 1777
		// (Invoke) Token: 0x06003FA9 RID: 16297
		public delegate bool SearchFilter<T>(T entity) where T : Entity;

		// Token: 0x020006F2 RID: 1778
		// (Invoke) Token: 0x06003FAD RID: 16301
		public delegate void NPCTargetingMethod(NPC searcher, bool faceTarget, Vector2? checkPosition);

		// Token: 0x020006F3 RID: 1779
		public static class SearchFilters
		{
			// Token: 0x06003FB0 RID: 16304 RVA: 0x0069B248 File Offset: 0x00699448
			public static bool OnlyCrystal(NPC npc)
			{
				return npc.type == 548 && !npc.dontTakeDamageFromHostiles;
			}

			// Token: 0x06003FB1 RID: 16305 RVA: 0x0069B262 File Offset: 0x00699462
			public static NPCUtils.SearchFilter<Player> OnlyPlayersInCertainDistance(Vector2 position, float maxDistance)
			{
				return (Player player) => player.Distance(position) <= maxDistance;
			}

			// Token: 0x06003FB2 RID: 16306 RVA: 0x0069B284 File Offset: 0x00699484
			public static bool NonBeeNPCs(NPC npc)
			{
				return (npc.type != 1 || (npc.ai[1] != 1124f && npc.ai[1] != 1125f)) && npc.type != 211 && npc.type != 210 && npc.type != 222 && npc.CanBeChasedBy(null, false);
			}

			// Token: 0x06003FB3 RID: 16307 RVA: 0x0069B2E9 File Offset: 0x006994E9
			public static NPCUtils.SearchFilter<Player> DownwindFromNPC(NPC npc, float maxDistanceX)
			{
				return delegate(Player player)
				{
					float windSpeedCurrent = Main.windSpeedCurrent;
					float num = player.Center.X - npc.Center.X;
					float num2 = Math.Abs(num);
					float num3 = Math.Abs(player.Center.Y - npc.Center.Y);
					return player.active && !player.dead && num3 < 100f && num2 < maxDistanceX && ((num > 0f && windSpeedCurrent > 0f) || (num < 0f && windSpeedCurrent < 0f));
				};
			}

			// Token: 0x02000A4F RID: 2639
			[CompilerGenerated]
			private sealed class <>c__DisplayClass1_0
			{
				// Token: 0x06004ACE RID: 19150 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass1_0()
				{
				}

				// Token: 0x06004ACF RID: 19151 RVA: 0x006D4CD6 File Offset: 0x006D2ED6
				internal bool <OnlyPlayersInCertainDistance>b__0(Player player)
				{
					return player.Distance(this.position) <= this.maxDistance;
				}

				// Token: 0x0400772C RID: 30508
				public Vector2 position;

				// Token: 0x0400772D RID: 30509
				public float maxDistance;
			}

			// Token: 0x02000A50 RID: 2640
			[CompilerGenerated]
			private sealed class <>c__DisplayClass3_0
			{
				// Token: 0x06004AD0 RID: 19152 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass3_0()
				{
				}

				// Token: 0x06004AD1 RID: 19153 RVA: 0x006D4CF0 File Offset: 0x006D2EF0
				internal bool <DownwindFromNPC>b__0(Player player)
				{
					float windSpeedCurrent = Main.windSpeedCurrent;
					float num = player.Center.X - this.npc.Center.X;
					float num2 = Math.Abs(num);
					float num3 = Math.Abs(player.Center.Y - this.npc.Center.Y);
					return player.active && !player.dead && num3 < 100f && num2 < this.maxDistanceX && ((num > 0f && windSpeedCurrent > 0f) || (num < 0f && windSpeedCurrent < 0f));
				}

				// Token: 0x0400772E RID: 30510
				public NPC npc;

				// Token: 0x0400772F RID: 30511
				public float maxDistanceX;
			}
		}

		// Token: 0x020006F4 RID: 1780
		public enum TargetType
		{
			// Token: 0x0400680C RID: 26636
			None,
			// Token: 0x0400680D RID: 26637
			NPC,
			// Token: 0x0400680E RID: 26638
			Player,
			// Token: 0x0400680F RID: 26639
			TankPet
		}

		// Token: 0x020006F5 RID: 1781
		public struct TargetSearchResults
		{
			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x06003FB4 RID: 16308 RVA: 0x0069B30C File Offset: 0x0069950C
			public int NearestTargetIndex
			{
				get
				{
					NPCUtils.TargetType nearestTargetType = this._nearestTargetType;
					if (nearestTargetType == NPCUtils.TargetType.NPC)
					{
						return this.NearestNPC.WhoAmIToTargetingIndex;
					}
					if (nearestTargetType - NPCUtils.TargetType.Player <= 1)
					{
						return this._nearestTankIndex;
					}
					return -1;
				}
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x06003FB5 RID: 16309 RVA: 0x0069B340 File Offset: 0x00699540
			public Rectangle NearestTargetHitbox
			{
				get
				{
					switch (this._nearestTargetType)
					{
					case NPCUtils.TargetType.NPC:
						return this.NearestNPC.Hitbox;
					case NPCUtils.TargetType.Player:
						return this.NearestTankOwner.Hitbox;
					case NPCUtils.TargetType.TankPet:
						return Main.projectile[this.NearestTankOwner.tankPet].Hitbox;
					default:
						return Rectangle.Empty;
					}
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x06003FB6 RID: 16310 RVA: 0x0069B39E File Offset: 0x0069959E
			public NPCUtils.TargetType NearestTargetType
			{
				get
				{
					return this._nearestTargetType;
				}
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06003FB7 RID: 16311 RVA: 0x0069B3A6 File Offset: 0x006995A6
			public bool FoundTarget
			{
				get
				{
					return this._nearestTargetType > NPCUtils.TargetType.None;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06003FB8 RID: 16312 RVA: 0x0069B3B1 File Offset: 0x006995B1
			public NPC NearestNPC
			{
				get
				{
					if (this._nearestNPCIndex != -1)
					{
						return Main.npc[this._nearestNPCIndex];
					}
					return null;
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06003FB9 RID: 16313 RVA: 0x0069B3CA File Offset: 0x006995CA
			public bool FoundNPC
			{
				get
				{
					return this._nearestNPCIndex != -1;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06003FBA RID: 16314 RVA: 0x0069B3D8 File Offset: 0x006995D8
			public int NearestNPCIndex
			{
				get
				{
					return this._nearestNPCIndex;
				}
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x06003FBB RID: 16315 RVA: 0x0069B3E0 File Offset: 0x006995E0
			public float NearestNPCDistance
			{
				get
				{
					return this._nearestNPCDistance;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0069B3E8 File Offset: 0x006995E8
			public Player NearestTankOwner
			{
				get
				{
					if (this._nearestTankIndex != -1)
					{
						return Main.player[this._nearestTankIndex];
					}
					return null;
				}
			}

			// Token: 0x17000502 RID: 1282
			// (get) Token: 0x06003FBD RID: 16317 RVA: 0x0069B401 File Offset: 0x00699601
			public bool FoundTank
			{
				get
				{
					return this._nearestTankIndex != -1;
				}
			}

			// Token: 0x17000503 RID: 1283
			// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0069B40F File Offset: 0x0069960F
			public int NearestTankOwnerIndex
			{
				get
				{
					return this._nearestTankIndex;
				}
			}

			// Token: 0x17000504 RID: 1284
			// (get) Token: 0x06003FBF RID: 16319 RVA: 0x0069B417 File Offset: 0x00699617
			public float NearestTankDistance
			{
				get
				{
					return this._nearestTankDistance;
				}
			}

			// Token: 0x17000505 RID: 1285
			// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0069B41F File Offset: 0x0069961F
			public float AdjustedTankDistance
			{
				get
				{
					return this._adjustedTankDistance;
				}
			}

			// Token: 0x17000506 RID: 1286
			// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x0069B427 File Offset: 0x00699627
			public NPCUtils.TargetType NearestTankType
			{
				get
				{
					return this._nearestTankType;
				}
			}

			// Token: 0x06003FC2 RID: 16322 RVA: 0x0069B430 File Offset: 0x00699630
			public TargetSearchResults(NPC searcher, int nearestNPCIndex, float nearestNPCDistance, int nearestTankIndex, float nearestTankDistance, float adjustedTankDistance, NPCUtils.TargetType tankType)
			{
				this._nearestNPCIndex = nearestNPCIndex;
				this._nearestNPCDistance = nearestNPCDistance;
				this._nearestTankIndex = nearestTankIndex;
				this._adjustedTankDistance = adjustedTankDistance;
				this._nearestTankDistance = nearestTankDistance;
				this._nearestTankType = tankType;
				if (this._nearestNPCIndex != -1 && this._nearestTankIndex != -1)
				{
					if (this._nearestNPCDistance < this._adjustedTankDistance)
					{
						this._nearestTargetType = NPCUtils.TargetType.NPC;
						return;
					}
					this._nearestTargetType = tankType;
					return;
				}
				else
				{
					if (this._nearestNPCIndex != -1)
					{
						this._nearestTargetType = NPCUtils.TargetType.NPC;
						return;
					}
					if (this._nearestTankIndex != -1)
					{
						this._nearestTargetType = tankType;
						return;
					}
					this._nearestTargetType = NPCUtils.TargetType.None;
					return;
				}
			}

			// Token: 0x04006810 RID: 26640
			private NPCUtils.TargetType _nearestTargetType;

			// Token: 0x04006811 RID: 26641
			private int _nearestNPCIndex;

			// Token: 0x04006812 RID: 26642
			private float _nearestNPCDistance;

			// Token: 0x04006813 RID: 26643
			private int _nearestTankIndex;

			// Token: 0x04006814 RID: 26644
			private float _nearestTankDistance;

			// Token: 0x04006815 RID: 26645
			private float _adjustedTankDistance;

			// Token: 0x04006816 RID: 26646
			private NPCUtils.TargetType _nearestTankType;
		}

		// Token: 0x020006F6 RID: 1782
		[Flags]
		public enum TargetSearchFlag
		{
			// Token: 0x04006818 RID: 26648
			None = 0,
			// Token: 0x04006819 RID: 26649
			NPCs = 1,
			// Token: 0x0400681A RID: 26650
			Players = 2,
			// Token: 0x0400681B RID: 26651
			All = 3
		}
	}
}
