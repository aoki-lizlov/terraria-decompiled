using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent
{
	// Token: 0x02000277 RID: 631
	public class CoinLossRevengeSystem
	{
		// Token: 0x06002442 RID: 9282 RVA: 0x0054C074 File Offset: 0x0054A274
		public void AddMarkerFromReader(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			Vector2 vector = reader.ReadVector2();
			int num2 = reader.ReadInt32();
			float num3 = reader.ReadSingle();
			int num4 = reader.ReadInt32();
			int num5 = reader.ReadInt32();
			int num6 = reader.ReadInt32();
			float num7 = reader.ReadSingle();
			bool flag = reader.ReadBoolean();
			CoinLossRevengeSystem.RevengeMarker revengeMarker = new CoinLossRevengeSystem.RevengeMarker(vector, num2, num3, num4, num5, num6, num7, flag, this._gameTime, num);
			this.AddMarker(revengeMarker);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x0054C0E4 File Offset: 0x0054A2E4
		private void AddMarker(CoinLossRevengeSystem.RevengeMarker marker)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Add(marker);
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0054C12C File Offset: 0x0054A32C
		public void DestroyMarker(int markerUniqueID)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.UniqueID == markerUniqueID);
			}
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x0054C18C File Offset: 0x0054A38C
		public CoinLossRevengeSystem()
		{
			this._markers = new List<CoinLossRevengeSystem.RevengeMarker>();
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x0054C1AC File Offset: 0x0054A3AC
		public void CacheEnemy(NPC npc)
		{
			if (npc.boss || (npc.realLife != -1 && npc.realLife != npc.whoAmI) || npc.rarity > 0 || npc.extraValue < CoinLossRevengeSystem.MinimumCoinsForCaching)
			{
				return;
			}
			if (npc.position.X < Main.leftWorld + 640f + 16f || npc.position.X + (float)npc.width > Main.rightWorld - 640f - 32f || npc.position.Y < Main.topWorld + 640f + 16f || npc.position.Y > Main.bottomWorld - 640f - 32f - (float)npc.height)
			{
				return;
			}
			int num = npc.netID;
			int num2;
			if (NPCID.Sets.RespawnEnemyID.TryGetValue(num, out num2))
			{
				num = num2;
			}
			if (num == 0)
			{
				return;
			}
			CoinLossRevengeSystem.RevengeMarker revengeMarker = new CoinLossRevengeSystem.RevengeMarker(npc.Center, num, npc.GetLifePercent(), npc.type, npc.aiStyle, npc.extraValue, npc.value, npc.SpawnedFromStatue, this._gameTime, -1);
			this.AddMarker(revengeMarker);
			if (Main.netMode == 2)
			{
				NetMessage.SendCoinLossRevengeMarker(revengeMarker, -1, -1);
			}
			if (CoinLossRevengeSystem.DisplayCaching)
			{
				Main.NewText("Cached " + npc.GivenOrTypeName, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0054C310 File Offset: 0x0054A510
		public void Reset()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				this._markers.Clear();
			}
			this._gameTime = 0;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0054C35C File Offset: 0x0054A55C
		public void Update()
		{
			this._gameTime++;
			if (Main.netMode == 1 && this._gameTime % 60 == 0)
			{
				this.RemoveExpiredOrInvalidMarkers();
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0054C388 File Offset: 0x0054A588
		public void CheckRespawns()
		{
			object obj = this._markersLock;
			lock (obj)
			{
				if (this._markers.Count == 0)
				{
					return;
				}
			}
			List<Tuple<int, Rectangle, Rectangle>> list = new List<Tuple<int, Rectangle, Rectangle>>();
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active && !player.dead)
				{
					list.Add(Tuple.Create<int, Rectangle, Rectangle>(i, Utils.CenteredRectangle(player.Center, CoinLossRevengeSystem._playerBoxSizeInner), Utils.CenteredRectangle(player.Center, CoinLossRevengeSystem._playerBoxSizeOuter)));
				}
			}
			if (list.Count == 0)
			{
				return;
			}
			this.RemoveExpiredOrInvalidMarkers();
			obj = this._markersLock;
			lock (obj)
			{
				List<CoinLossRevengeSystem.RevengeMarker> list2 = new List<CoinLossRevengeSystem.RevengeMarker>();
				for (int j = 0; j < this._markers.Count; j++)
				{
					CoinLossRevengeSystem.RevengeMarker revengeMarker = this._markers[j];
					bool flag2 = false;
					Tuple<int, Rectangle, Rectangle> tuple = null;
					foreach (Tuple<int, Rectangle, Rectangle> tuple2 in list)
					{
						if (revengeMarker.Intersects(tuple2.Item2, tuple2.Item3))
						{
							tuple = tuple2;
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						revengeMarker.SetRespawnAttemptLock(false);
					}
					else if (!revengeMarker.RespawnAttemptLocked)
					{
						revengeMarker.SetRespawnAttemptLock(true);
						if (revengeMarker.WouldNPCBeDiscouraged(Main.player[tuple.Item1]))
						{
							revengeMarker.SetToExpire();
						}
						else
						{
							revengeMarker.SpawnEnemy();
							list2.Add(revengeMarker);
							if (Main.dedServ)
							{
								NetMessage.SendData(127, -1, -1, null, revengeMarker.UniqueID, 0f, 0f, 0f, 0, 0, 0);
							}
						}
					}
				}
				this._markers = this._markers.Except(list2).ToList<CoinLossRevengeSystem.RevengeMarker>();
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0054C5B4 File Offset: 0x0054A7B4
		private void RemoveExpiredOrInvalidMarkers()
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				IEnumerable<CoinLossRevengeSystem.RevengeMarker> enumerable = this._markers.Where((CoinLossRevengeSystem.RevengeMarker x) => x.IsExpired(this._gameTime));
				IEnumerable<CoinLossRevengeSystem.RevengeMarker> enumerable2 = this._markers.Where((CoinLossRevengeSystem.RevengeMarker x) => x.IsInvalid());
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.IsInvalid());
				this._markers.RemoveAll((CoinLossRevengeSystem.RevengeMarker x) => x.IsExpired(this._gameTime));
				if (Main.dedServ)
				{
					foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker in enumerable)
					{
						NetMessage.SendData(127, -1, -1, null, revengeMarker.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
					foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker2 in enumerable2)
					{
						NetMessage.SendData(127, -1, -1, null, revengeMarker2.UniqueID, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0054C754 File Offset: 0x0054A954
		public CoinLossRevengeSystem.RevengeMarker DrawMapIcons(SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, ref string unused)
		{
			CoinLossRevengeSystem.RevengeMarker revengeMarker = null;
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker2 in this._markers)
				{
					if (revengeMarker2.DrawMapIcon(spriteBatch, mapTopLeft, mapX2Y2AndOff, mapRect, mapScale, drawScale, this._gameTime))
					{
						revengeMarker = revengeMarker2;
					}
				}
			}
			return revengeMarker;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0054C7E8 File Offset: 0x0054A9E8
		public void SendAllMarkersToPlayer(int plr)
		{
			object markersLock = this._markersLock;
			lock (markersLock)
			{
				foreach (CoinLossRevengeSystem.RevengeMarker revengeMarker in this._markers)
				{
					NetMessage.SendCoinLossRevengeMarker(revengeMarker, plr, -1);
				}
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0054C864 File Offset: 0x0054AA64
		// Note: this type is marked as 'beforefieldinit'.
		static CoinLossRevengeSystem()
		{
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0054C8A3 File Offset: 0x0054AAA3
		[CompilerGenerated]
		private bool <RemoveExpiredOrInvalidMarkers>b__20_0(CoinLossRevengeSystem.RevengeMarker x)
		{
			return x.IsExpired(this._gameTime);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0054C8A3 File Offset: 0x0054AAA3
		[CompilerGenerated]
		private bool <RemoveExpiredOrInvalidMarkers>b__20_3(CoinLossRevengeSystem.RevengeMarker x)
		{
			return x.IsExpired(this._gameTime);
		}

		// Token: 0x04004DED RID: 19949
		public static bool DisplayCaching = false;

		// Token: 0x04004DEE RID: 19950
		public static int MinimumCoinsForCaching = Item.buyPrice(0, 0, 10, 0);

		// Token: 0x04004DEF RID: 19951
		private const int PLAYER_BOX_WIDTH_INNER = 1968;

		// Token: 0x04004DF0 RID: 19952
		private const int PLAYER_BOX_HEIGHT_INNER = 1200;

		// Token: 0x04004DF1 RID: 19953
		private const int PLAYER_BOX_WIDTH_OUTER = 2608;

		// Token: 0x04004DF2 RID: 19954
		private const int PLAYER_BOX_HEIGHT_OUTER = 1840;

		// Token: 0x04004DF3 RID: 19955
		private static readonly Vector2 _playerBoxSizeInner = new Vector2(1968f, 1200f);

		// Token: 0x04004DF4 RID: 19956
		private static readonly Vector2 _playerBoxSizeOuter = new Vector2(2608f, 1840f);

		// Token: 0x04004DF5 RID: 19957
		private List<CoinLossRevengeSystem.RevengeMarker> _markers;

		// Token: 0x04004DF6 RID: 19958
		private readonly object _markersLock = new object();

		// Token: 0x04004DF7 RID: 19959
		private int _gameTime;

		// Token: 0x020007F6 RID: 2038
		public class RevengeMarker
		{
			// Token: 0x0600429A RID: 17050 RVA: 0x006BF203 File Offset: 0x006BD403
			public void SetToExpire()
			{
				this._forceExpire = true;
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600429B RID: 17051 RVA: 0x006BF20C File Offset: 0x006BD40C
			public bool RespawnAttemptLocked
			{
				get
				{
					return this._attemptedRespawn;
				}
			}

			// Token: 0x0600429C RID: 17052 RVA: 0x006BF214 File Offset: 0x006BD414
			public void SetRespawnAttemptLock(bool state)
			{
				this._attemptedRespawn = state;
			}

			// Token: 0x0600429D RID: 17053 RVA: 0x006BF220 File Offset: 0x006BD420
			public RevengeMarker(Vector2 coords, int npcNetId, float npcHPPercent, int npcType, int npcAiStyle, int coinValue, float baseValue, bool spawnedFromStatue, int gameTime, int uniqueID = -1)
			{
				this._location = coords;
				this._npcNetID = npcNetId;
				this._npcHPPercent = npcHPPercent;
				this._npcTypeAgainstDiscouragement = npcType;
				this._npcAIStyleAgainstDiscouragement = npcAiStyle;
				this._coinsValue = coinValue;
				this._baseValue = baseValue;
				this._spawnedFromStatue = spawnedFromStatue;
				this._hitbox = Utils.CenteredRectangle(this._location, CoinLossRevengeSystem.RevengeMarker.EnemyBoxSize);
				this._expirationTime = this.CalculateExpirationTime(gameTime, coinValue);
				if (uniqueID == -1)
				{
					this._uniqueID = CoinLossRevengeSystem.RevengeMarker._uniqueIDCounter++;
					return;
				}
				this._uniqueID = uniqueID;
			}

			// Token: 0x0600429E RID: 17054 RVA: 0x006BF2B8 File Offset: 0x006BD4B8
			public bool IsInvalid()
			{
				int npcinvasionGroup = NPC.GetNPCInvasionGroup(this._npcTypeAgainstDiscouragement);
				switch (npcinvasionGroup)
				{
				case -3:
					return !DD2Event.Ongoing;
				case -2:
					return !Main.pumpkinMoon || Main.dayTime;
				case -1:
					return !Main.snowMoon || Main.dayTime;
				case 1:
				case 2:
				case 3:
				case 4:
					return npcinvasionGroup != Main.invasionType;
				}
				int npcTypeAgainstDiscouragement = this._npcTypeAgainstDiscouragement;
				if (npcTypeAgainstDiscouragement <= 166)
				{
					if (npcTypeAgainstDiscouragement - 158 > 1 && npcTypeAgainstDiscouragement != 162 && npcTypeAgainstDiscouragement != 166)
					{
						return false;
					}
				}
				else if (npcTypeAgainstDiscouragement != 251 && npcTypeAgainstDiscouragement != 253)
				{
					switch (npcTypeAgainstDiscouragement)
					{
					case 460:
					case 461:
					case 462:
					case 463:
					case 466:
					case 467:
					case 468:
					case 469:
					case 477:
					case 478:
					case 479:
						break;
					case 464:
					case 465:
					case 470:
					case 471:
					case 472:
					case 473:
					case 474:
					case 475:
					case 476:
						return false;
					default:
						return false;
					}
				}
				if (!Main.eclipse || !Main.dayTime)
				{
					return true;
				}
				return false;
			}

			// Token: 0x0600429F RID: 17055 RVA: 0x006BF3DC File Offset: 0x006BD5DC
			public bool IsExpired(int gameTime)
			{
				return this._forceExpire || this._expirationTime <= gameTime;
			}

			// Token: 0x060042A0 RID: 17056 RVA: 0x006BF3F4 File Offset: 0x006BD5F4
			private int CalculateExpirationTime(int gameCacheTime, int coinValue)
			{
				int num;
				if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompSilver)
				{
					num = (int)MathHelper.Lerp(0f, 3600f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompCopper, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)coinValue, false));
				}
				else if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompGold)
				{
					num = (int)MathHelper.Lerp(36000f, 108000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false));
				}
				else if (coinValue < CoinLossRevengeSystem.RevengeMarker._expirationCompPlat)
				{
					num = (int)MathHelper.Lerp(108000f, 216000f, Utils.GetLerpValue((float)CoinLossRevengeSystem.RevengeMarker._expirationCompSilver, (float)CoinLossRevengeSystem.RevengeMarker._expirationCompGold, (float)coinValue, false));
				}
				else
				{
					num = 432000;
				}
				num += 18000;
				return gameCacheTime + num;
			}

			// Token: 0x060042A1 RID: 17057 RVA: 0x006BF4A1 File Offset: 0x006BD6A1
			public bool Intersects(Rectangle rectInner, Rectangle rectOuter)
			{
				return rectOuter.Intersects(this._hitbox);
			}

			// Token: 0x060042A2 RID: 17058 RVA: 0x006BF4B0 File Offset: 0x006BD6B0
			public void SpawnEnemy()
			{
				int num = NPC.NewNPC(new EntitySource_RevengeSystem(), (int)this._location.X, (int)this._location.Y, this._npcNetID, 0, 0f, 0f, 0f, 0f, 255);
				NPC npc = Main.npc[num];
				npc.Center = this._location;
				if (this._npcNetID < 0)
				{
					npc.SetDefaults(this._npcNetID, default(NPCSpawnParams));
				}
				int num2;
				if (NPCID.Sets.SpecialSpawningRules.TryGetValue(this._npcNetID, out num2) && num2 == 0)
				{
					Point point = npc.position.ToTileCoordinates();
					npc.ai[0] = (float)point.X;
					npc.ai[1] = (float)point.Y;
					npc.netUpdate = true;
				}
				npc.timeLeft += 3600;
				npc.extraValue = this._coinsValue;
				npc.value = this._baseValue;
				npc.SpawnedFromStatue = this._spawnedFromStatue;
				float num3 = Math.Max(0.5f, this._npcHPPercent);
				npc.life = (int)((float)npc.lifeMax * num3);
				if (num < Main.maxNPCs)
				{
					if (Main.netMode == 0)
					{
						npc.moneyPing(this._location);
					}
					else
					{
						NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.SendData(92, -1, -1, null, num, (float)this._coinsValue, this._location.X, this._location.Y, 0, 0, 0);
					}
				}
				if (CoinLossRevengeSystem.DisplayCaching)
				{
					Main.NewText("Spawned " + npc.GivenOrTypeName, byte.MaxValue, byte.MaxValue, byte.MaxValue);
				}
			}

			// Token: 0x060042A3 RID: 17059 RVA: 0x006BF668 File Offset: 0x006BD868
			public bool WouldNPCBeDiscouraged(Player playerTarget)
			{
				int num;
				switch (this._npcAIStyleAgainstDiscouragement)
				{
				case 2:
					return NPC.DespawnEncouragement_AIStyle2_FloatingEye_IsDiscouraged(this._npcTypeAgainstDiscouragement, playerTarget.position, 255);
				case 3:
					return !NPC.DespawnEncouragement_AIStyle3_Fighters_NotDiscouraged(this._npcTypeAgainstDiscouragement, playerTarget.position, null);
				case 6:
				{
					bool flag = false;
					num = this._npcTypeAgainstDiscouragement;
					if (num <= 95)
					{
						if (num != 10 && num != 39 && num != 95)
						{
							goto IL_0097;
						}
					}
					else if (num != 117 && num != 510)
					{
						if (num == 513)
						{
							flag = !playerTarget.ZoneUndergroundDesert;
							goto IL_0097;
						}
						goto IL_0097;
					}
					flag = true;
					IL_0097:
					return flag && (double)playerTarget.position.Y < Main.worldSurface * 16.0;
				}
				}
				num = this._npcNetID;
				if (num != 253)
				{
					return num == 490 && Main.dayTime;
				}
				return !Main.eclipse;
			}

			// Token: 0x060042A4 RID: 17060 RVA: 0x006BF758 File Offset: 0x006BD958
			public bool DrawMapIcon(SpriteBatch spriteBatch, Vector2 mapTopLeft, Vector2 mapX2Y2AndOff, Rectangle? mapRect, float mapScale, float drawScale, int gameTime)
			{
				Vector2 vector = this._location / 16f - mapTopLeft;
				vector *= mapScale;
				vector += mapX2Y2AndOff;
				if (mapRect != null && !mapRect.Value.Contains(vector.ToPoint()))
				{
					return false;
				}
				Texture2D texture2D = TextureAssets.MapDeath.Value;
				if (this._coinsValue < 100)
				{
					texture2D = TextureAssets.Coin[0].Value;
				}
				else if (this._coinsValue < 10000)
				{
					texture2D = TextureAssets.Coin[1].Value;
				}
				else if (this._coinsValue < 1000000)
				{
					texture2D = TextureAssets.Coin[2].Value;
				}
				else
				{
					texture2D = TextureAssets.Coin[3].Value;
				}
				Rectangle rectangle = texture2D.Frame(1, 8, 0, 0, 0, 0);
				spriteBatch.Draw(texture2D, vector, new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() / 2f, drawScale, SpriteEffects.None, 0f);
				return Utils.CenteredRectangle(vector, rectangle.Size() * drawScale).Contains(Main.MouseScreen.ToPoint());
			}

			// Token: 0x060042A5 RID: 17061 RVA: 0x006BF87C File Offset: 0x006BDA7C
			public void UseMouseOver(SpriteBatch spriteBatch, ref string mouseTextString, float drawScale = 1f)
			{
				mouseTextString = "";
				Vector2 vector = Main.MouseScreen / drawScale + new Vector2(-28f) + new Vector2(4f, 0f);
				ItemSlot.DrawMoney(spriteBatch, "", vector.X, vector.Y, Utils.CoinsSplit((long)this._coinsValue), true, false);
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x060042A6 RID: 17062 RVA: 0x006BF8E4 File Offset: 0x006BDAE4
			public int UniqueID
			{
				get
				{
					return this._uniqueID;
				}
			}

			// Token: 0x060042A7 RID: 17063 RVA: 0x006BF8EC File Offset: 0x006BDAEC
			public void WriteSelfTo(BinaryWriter writer)
			{
				writer.Write(this._uniqueID);
				writer.WriteVector2(this._location);
				writer.Write(this._npcNetID);
				writer.Write(this._npcHPPercent);
				writer.Write(this._npcTypeAgainstDiscouragement);
				writer.Write(this._npcAIStyleAgainstDiscouragement);
				writer.Write(this._coinsValue);
				writer.Write(this._baseValue);
				writer.Write(this._spawnedFromStatue);
			}

			// Token: 0x060042A8 RID: 17064 RVA: 0x006BF968 File Offset: 0x006BDB68
			// Note: this type is marked as 'beforefieldinit'.
			static RevengeMarker()
			{
			}

			// Token: 0x0400717F RID: 29055
			private static int _uniqueIDCounter = 0;

			// Token: 0x04007180 RID: 29056
			private static readonly int _expirationCompCopper = Item.buyPrice(0, 0, 0, 1);

			// Token: 0x04007181 RID: 29057
			private static readonly int _expirationCompSilver = Item.buyPrice(0, 0, 1, 0);

			// Token: 0x04007182 RID: 29058
			private static readonly int _expirationCompGold = Item.buyPrice(0, 1, 0, 0);

			// Token: 0x04007183 RID: 29059
			private static readonly int _expirationCompPlat = Item.buyPrice(1, 0, 0, 0);

			// Token: 0x04007184 RID: 29060
			private const int ONE_MINUTE = 3600;

			// Token: 0x04007185 RID: 29061
			private const int ENEMY_BOX_WIDTH = 2160;

			// Token: 0x04007186 RID: 29062
			private const int ENEMY_BOX_HEIGHT = 1440;

			// Token: 0x04007187 RID: 29063
			public static readonly Vector2 EnemyBoxSize = new Vector2(2160f, 1440f);

			// Token: 0x04007188 RID: 29064
			private readonly Vector2 _location;

			// Token: 0x04007189 RID: 29065
			private readonly Rectangle _hitbox;

			// Token: 0x0400718A RID: 29066
			private readonly int _npcNetID;

			// Token: 0x0400718B RID: 29067
			private readonly float _npcHPPercent;

			// Token: 0x0400718C RID: 29068
			private readonly float _baseValue;

			// Token: 0x0400718D RID: 29069
			private readonly int _coinsValue;

			// Token: 0x0400718E RID: 29070
			private readonly int _npcTypeAgainstDiscouragement;

			// Token: 0x0400718F RID: 29071
			private readonly int _npcAIStyleAgainstDiscouragement;

			// Token: 0x04007190 RID: 29072
			private readonly int _expirationTime;

			// Token: 0x04007191 RID: 29073
			private readonly bool _spawnedFromStatue;

			// Token: 0x04007192 RID: 29074
			private readonly int _uniqueID;

			// Token: 0x04007193 RID: 29075
			private bool _forceExpire;

			// Token: 0x04007194 RID: 29076
			private bool _attemptedRespawn;
		}

		// Token: 0x020007F7 RID: 2039
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060042A9 RID: 17065 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060042AA RID: 17066 RVA: 0x006BF9C7 File Offset: 0x006BDBC7
			internal bool <DestroyMarker>b__0(CoinLossRevengeSystem.RevengeMarker x)
			{
				return x.UniqueID == this.markerUniqueID;
			}

			// Token: 0x04007195 RID: 29077
			public int markerUniqueID;
		}

		// Token: 0x020007F8 RID: 2040
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060042AB RID: 17067 RVA: 0x006BF9D7 File Offset: 0x006BDBD7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060042AC RID: 17068 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060042AD RID: 17069 RVA: 0x006BF9E3 File Offset: 0x006BDBE3
			internal bool <RemoveExpiredOrInvalidMarkers>b__20_1(CoinLossRevengeSystem.RevengeMarker x)
			{
				return x.IsInvalid();
			}

			// Token: 0x060042AE RID: 17070 RVA: 0x006BF9E3 File Offset: 0x006BDBE3
			internal bool <RemoveExpiredOrInvalidMarkers>b__20_2(CoinLossRevengeSystem.RevengeMarker x)
			{
				return x.IsInvalid();
			}

			// Token: 0x04007196 RID: 29078
			public static readonly CoinLossRevengeSystem.<>c <>9 = new CoinLossRevengeSystem.<>c();

			// Token: 0x04007197 RID: 29079
			public static Func<CoinLossRevengeSystem.RevengeMarker, bool> <>9__20_1;

			// Token: 0x04007198 RID: 29080
			public static Predicate<CoinLossRevengeSystem.RevengeMarker> <>9__20_2;
		}
	}
}
