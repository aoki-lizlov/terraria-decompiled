using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.Tile_Entities;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.GameContent
{
	// Token: 0x02000268 RID: 616
	public class TeleportPylonsSystem : IOnPlayerJoining
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x005491B8 File Offset: 0x005473B8
		public List<TeleportPylonInfo> Pylons
		{
			get
			{
				return this._pylons;
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x005491C0 File Offset: 0x005473C0
		public void Update()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			if (this._cooldownForUpdatingPylonsList > 0)
			{
				this._cooldownForUpdatingPylonsList--;
				return;
			}
			this._cooldownForUpdatingPylonsList = int.MaxValue;
			this.UpdatePylonsListAndBroadcastChanges();
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x005491F4 File Offset: 0x005473F4
		public bool HasPylonOfType(TeleportPylonType pylonType)
		{
			return this._pylons.Any((TeleportPylonInfo x) => x.TypeOfPylon == pylonType);
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x00549225 File Offset: 0x00547425
		public bool HasAnyPylon()
		{
			return this._pylons.Count > 0;
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00549235 File Offset: 0x00547435
		public void RequestImmediateUpdate()
		{
			if (Main.netMode == 1)
			{
				return;
			}
			this._cooldownForUpdatingPylonsList = int.MaxValue;
			this.UpdatePylonsListAndBroadcastChanges();
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00549254 File Offset: 0x00547454
		private void UpdatePylonsListAndBroadcastChanges()
		{
			Utils.Swap<List<TeleportPylonInfo>>(ref this._pylons, ref this._pylonsOld);
			this._pylons.Clear();
			foreach (TileEntity tileEntity in TileEntity.ByPosition.Values)
			{
				TETeleportationPylon teteleportationPylon = tileEntity as TETeleportationPylon;
				TeleportPylonType teleportPylonType;
				if (teteleportationPylon != null && teteleportationPylon.TryGetPylonType(out teleportPylonType))
				{
					TeleportPylonInfo teleportPylonInfo = new TeleportPylonInfo
					{
						PositionInTiles = teteleportationPylon.Position,
						TypeOfPylon = teleportPylonType
					};
					this._pylons.Add(teleportPylonInfo);
				}
			}
			IEnumerable<TeleportPylonInfo> enumerable = this._pylonsOld.Except(this._pylons);
			foreach (TeleportPylonInfo teleportPylonInfo2 in this._pylons.Except(this._pylonsOld))
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(teleportPylonInfo2, NetTeleportPylonModule.SubPacketType.PylonWasAdded));
			}
			foreach (TeleportPylonInfo teleportPylonInfo3 in enumerable)
			{
				NetManager.Instance.BroadcastOrLoopback(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(teleportPylonInfo3, NetTeleportPylonModule.SubPacketType.PylonWasRemoved));
			}
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x005493B4 File Offset: 0x005475B4
		public void AddForClient(TeleportPylonInfo info)
		{
			if (this._pylons.Contains(info))
			{
				return;
			}
			this._pylons.Add(info);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x005493D4 File Offset: 0x005475D4
		public void RemoveForClient(TeleportPylonInfo info)
		{
			this._pylons.RemoveAll((TeleportPylonInfo x) => x.Equals(info));
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00549408 File Offset: 0x00547608
		public void HandleTeleportRequest(TeleportPylonInfo info, int playerIndex)
		{
			Player player = Main.player[playerIndex];
			string text = null;
			bool flag = true;
			if (flag)
			{
				flag &= TeleportPylonsSystem.IsPlayerNearAPylon(player);
				if (!flag)
				{
					text = "Net.CannotTeleportToPylonBecausePlayerIsNotNearAPylon";
				}
			}
			if (flag)
			{
				int num = this.HowManyNPCsDoesPylonNeed(info, player);
				flag &= this.DoesPylonHaveEnoughNPCsAroundIt(info, num);
				if (!flag)
				{
					text = "Net.CannotTeleportToPylonBecauseNotEnoughNPCs";
				}
			}
			if (flag)
			{
				if (!NPC.downedPlantBoss && (double)info.PositionInTiles.Y > Main.worldSurface && Framing.GetTileSafely((int)info.PositionInTiles.X, (int)info.PositionInTiles.Y).wall == 87)
				{
					flag = false;
				}
				if (!flag)
				{
					text = "Net.CannotTeleportToPylonBecauseAccessingLihzahrdTempleEarly";
				}
			}
			if (flag)
			{
				this._sceneMetrics.Scan(new SceneMetricsScanSettings
				{
					BiomeScanCenterPositionInWorld = info.PositionInTiles.ToWorldCoordinates(8f, 8f)
				});
				flag = this.DoesPylonAcceptTeleportation(info, player);
				if (!flag)
				{
					text = "Net.CannotTeleportToPylonBecauseNotMeetingBiomeRequirements";
				}
			}
			if (flag)
			{
				bool flag2 = false;
				int num2 = 0;
				for (int i = 0; i < this._pylons.Count; i++)
				{
					TeleportPylonInfo teleportPylonInfo = this._pylons[i];
					if (player.InTileEntityInteractionRange((int)teleportPylonInfo.PositionInTiles.X, (int)teleportPylonInfo.PositionInTiles.Y, 3, 4, TileReachCheckSettings.Pylons))
					{
						if (num2 < 1)
						{
							num2 = 1;
						}
						int num3 = this.HowManyNPCsDoesPylonNeed(teleportPylonInfo, player);
						if (this.DoesPylonHaveEnoughNPCsAroundIt(teleportPylonInfo, num3))
						{
							if (num2 < 2)
							{
								num2 = 2;
							}
							this._sceneMetrics.Scan(new SceneMetricsScanSettings
							{
								BiomeScanCenterPositionInWorld = teleportPylonInfo.PositionInTiles.ToWorldCoordinates(8f, 8f)
							});
							if (this.DoesPylonAcceptTeleportation(teleportPylonInfo, player))
							{
								flag2 = true;
								break;
							}
						}
					}
				}
				if (!flag2)
				{
					flag = false;
					switch (num2)
					{
					default:
						text = "Net.CannotTeleportToPylonBecausePlayerIsNotNearAPylon";
						break;
					case 1:
						text = "Net.CannotTeleportToPylonBecauseNotEnoughNPCsAtCurrentPylon";
						break;
					case 2:
						text = "Net.CannotTeleportToPylonBecauseNotMeetingBiomeRequirements";
						break;
					}
				}
			}
			if (flag)
			{
				Vector2 vector = info.PositionInTiles.ToWorldCoordinates(8f, 8f) - new Vector2(0f, (float)player.HeightOffsetBoost);
				int num4 = 9;
				int typeOfPylon = (int)info.TypeOfPylon;
				int num5 = 0;
				player.Teleport(vector, num4, typeOfPylon);
				player.velocity = Vector2.Zero;
				if (Main.netMode == 2)
				{
					RemoteClient.CheckSection(player.whoAmI, player.position, 1);
					NetMessage.SendData(65, -1, -1, null, 0, (float)player.whoAmI, vector.X, vector.Y, num4, num5, typeOfPylon);
					return;
				}
			}
			else
			{
				ChatHelper.SendChatMessageToClient(NetworkText.FromKey(text, new object[0]), ChatColors.ServerMessage, playerIndex);
			}
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x00549693 File Offset: 0x00547893
		public static bool IsPlayerNearAPylon(Player player)
		{
			return player.IsTileTypeInInteractionRange(597, TileReachCheckSettings.Pylons);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x005496A8 File Offset: 0x005478A8
		private bool DoesPylonHaveEnoughNPCsAroundIt(TeleportPylonInfo info, int necessaryNPCCount)
		{
			if (necessaryNPCCount <= 0)
			{
				return true;
			}
			Point16 positionInTiles = info.PositionInTiles;
			return TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(necessaryNPCCount, positionInTiles);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x005496CC File Offset: 0x005478CC
		public static bool DoesPositionHaveEnoughNPCs(int necessaryNPCCount, Point16 centerPoint)
		{
			Rectangle rectangle = Utils.CenteredRectangle(centerPoint, SceneMetrics.ZoneScanSize);
			int num = necessaryNPCCount;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.isLikeATownNPC && !npc.homeless && rectangle.Contains(npc.homeTileX, npc.homeTileY))
				{
					Vector2 vector = new Vector2((float)npc.homeTileX, (float)npc.homeTileY);
					Vector2 vector2 = new Vector2(npc.Center.X / 16f, npc.Center.Y / 16f);
					if (Vector2.Distance(vector, vector2) < 100f)
					{
						num--;
						if (num == 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x0054978B File Offset: 0x0054798B
		public void RequestTeleportation(TeleportPylonInfo info, Player player)
		{
			NetManager.Instance.SendToServerOrLoopback(NetTeleportPylonModule.SerializeUseRequest(info));
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x005497A0 File Offset: 0x005479A0
		private bool DoesPylonAcceptTeleportation(TeleportPylonInfo info, Player player)
		{
			switch (info.TypeOfPylon)
			{
			case TeleportPylonType.SurfacePurity:
			{
				bool flag = (double)info.PositionInTiles.Y <= Main.worldSurface;
				if (Main.remixWorld)
				{
					flag = (double)info.PositionInTiles.Y > Main.rockLayer && (int)info.PositionInTiles.Y < Main.maxTilesY - 350;
				}
				bool flag2 = (int)info.PositionInTiles.X >= Main.maxTilesX - 380 || info.PositionInTiles.X <= 380;
				return flag && !flag2 && (!this._sceneMetrics.EnoughTilesForJungle && !this._sceneMetrics.EnoughTilesForSnow && !this._sceneMetrics.EnoughTilesForDesert && !this._sceneMetrics.EnoughTilesForGlowingMushroom && !this._sceneMetrics.EnoughTilesForHallow && !this._sceneMetrics.EnoughTilesForCrimson && !this._sceneMetrics.EnoughTilesForCorruption);
			}
			case TeleportPylonType.Jungle:
				return this._sceneMetrics.EnoughTilesForJungle;
			case TeleportPylonType.Hallow:
				return this._sceneMetrics.EnoughTilesForHallow;
			case TeleportPylonType.Underground:
				return (double)info.PositionInTiles.Y >= Main.worldSurface;
			case TeleportPylonType.Beach:
			{
				bool flag3 = (double)info.PositionInTiles.Y <= Main.worldSurface && (double)info.PositionInTiles.Y > Main.worldSurface * 0.3499999940395355;
				bool flag4 = (int)info.PositionInTiles.X >= Main.maxTilesX - 380 || info.PositionInTiles.X <= 380;
				if (Main.remixWorld)
				{
					flag3 |= (double)info.PositionInTiles.Y > Main.rockLayer && (int)info.PositionInTiles.Y < Main.maxTilesY - 350;
					flag4 |= (double)info.PositionInTiles.X < (double)Main.maxTilesX * 0.43 || (double)info.PositionInTiles.X > (double)Main.maxTilesX * 0.57;
				}
				return flag4 && flag3;
			}
			case TeleportPylonType.Desert:
				return this._sceneMetrics.EnoughTilesForDesert;
			case TeleportPylonType.Snow:
				return this._sceneMetrics.EnoughTilesForSnow;
			case TeleportPylonType.GlowingMushroom:
				return (!Main.remixWorld || (int)info.PositionInTiles.Y < Main.maxTilesY - 200) && this._sceneMetrics.EnoughTilesForGlowingMushroom;
			case TeleportPylonType.Victory:
				return true;
			case TeleportPylonType.Underworld:
				return (int)info.PositionInTiles.Y >= Main.UnderworldLayer;
			case TeleportPylonType.Shimmer:
				return this._sceneMetrics.EnoughTilesForShimmer;
			default:
				return true;
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x00549A58 File Offset: 0x00547C58
		private int HowManyNPCsDoesPylonNeed(TeleportPylonInfo info, Player player)
		{
			TeleportPylonType typeOfPylon = info.TypeOfPylon;
			if (typeOfPylon != TeleportPylonType.Victory)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00549A73 File Offset: 0x00547C73
		public void Reset()
		{
			this._pylons.Clear();
			this._cooldownForUpdatingPylonsList = 0;
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x00549A88 File Offset: 0x00547C88
		public void OnPlayerJoining(int playerIndex)
		{
			foreach (TeleportPylonInfo teleportPylonInfo in this._pylons)
			{
				NetManager.Instance.SendToClient(NetTeleportPylonModule.SerializePylonWasAddedOrRemoved(teleportPylonInfo, NetTeleportPylonModule.SubPacketType.PylonWasAdded), playerIndex);
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00549AE8 File Offset: 0x00547CE8
		public static void SpawnInWorldDust(int tileStyle, Rectangle dustBox)
		{
			float num = 1f;
			float num2 = 1f;
			float num3 = 1f;
			switch ((byte)tileStyle)
			{
			case 0:
				num = 0.05f;
				num2 = 0.8f;
				num3 = 0.3f;
				break;
			case 1:
				num = 0.7f;
				num2 = 0.8f;
				num3 = 0.05f;
				break;
			case 2:
				num = 0.5f;
				num2 = 0.3f;
				num3 = 0.7f;
				break;
			case 3:
				num = 0.4f;
				num2 = 0.4f;
				num3 = 0.6f;
				break;
			case 4:
				num = 0.2f;
				num2 = 0.2f;
				num3 = 0.95f;
				break;
			case 5:
				num = 0.85f;
				num2 = 0.45f;
				num3 = 0.1f;
				break;
			case 6:
				num = 1f;
				num2 = 1f;
				num3 = 1.2f;
				break;
			case 7:
				num = 0.4f;
				num2 = 0.7f;
				num3 = 1.2f;
				break;
			case 8:
				num = 0.7f;
				num2 = 0.7f;
				num3 = 0.7f;
				break;
			case 9:
				num = 0.05f;
				num2 = 0.8f;
				num3 = 0.3f;
				break;
			case 10:
				num = 0.05f;
				num2 = 0.8f;
				num3 = 0.3f;
				break;
			}
			int num4 = Dust.NewDust(dustBox.TopLeft(), dustBox.Width, dustBox.Height, 43, 0f, 0f, 254, new Color(num, num2, num3, 1f), 0.5f);
			Main.dust[num4].velocity *= 0.1f;
			Dust dust = Main.dust[num4];
			dust.velocity.Y = dust.velocity.Y - 0.2f;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00549C9A File Offset: 0x00547E9A
		public TeleportPylonsSystem()
		{
		}

		// Token: 0x04004DB6 RID: 19894
		private List<TeleportPylonInfo> _pylons = new List<TeleportPylonInfo>();

		// Token: 0x04004DB7 RID: 19895
		private List<TeleportPylonInfo> _pylonsOld = new List<TeleportPylonInfo>();

		// Token: 0x04004DB8 RID: 19896
		private int _cooldownForUpdatingPylonsList;

		// Token: 0x04004DB9 RID: 19897
		private const int CooldownTimePerPylonsListUpdate = 2147483647;

		// Token: 0x04004DBA RID: 19898
		private SceneMetrics _sceneMetrics = new SceneMetrics();

		// Token: 0x020007EB RID: 2027
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06004277 RID: 17015 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06004278 RID: 17016 RVA: 0x006BED61 File Offset: 0x006BCF61
			internal bool <HasPylonOfType>b__0(TeleportPylonInfo x)
			{
				return x.TypeOfPylon == this.pylonType;
			}

			// Token: 0x04007156 RID: 29014
			public TeleportPylonType pylonType;
		}

		// Token: 0x020007EC RID: 2028
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06004279 RID: 17017 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x0600427A RID: 17018 RVA: 0x006BED71 File Offset: 0x006BCF71
			internal bool <RemoveForClient>b__0(TeleportPylonInfo x)
			{
				return x.Equals(this.info);
			}

			// Token: 0x04007157 RID: 29015
			public TeleportPylonInfo info;
		}
	}
}
