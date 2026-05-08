using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;
using Terraria.ID;

namespace Terraria
{
	// Token: 0x0200005A RID: 90
	public class WorldItem : Entity
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x004A7AB6 File Offset: 0x004A5CB6
		static WorldItem()
		{
			RemoteClient.NetSectionActivated += WorldItem.SyncItemsInSection;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x004A7AD3 File Offset: 0x004A5CD3
		public override string ToString()
		{
			return string.Concat(new object[] { "[", this.whoAmI, "]", this.inner });
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x004A7B07 File Offset: 0x004A5D07
		public void ClearOut()
		{
			this.TurnToAir(false);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x004A7B10 File Offset: 0x004A5D10
		public void OverrideWith(Item item)
		{
			this.inner = item;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x004A7B19 File Offset: 0x004A5D19
		public void ResetStats(int Type)
		{
			this.SetDefaultsBringOver();
			this.inner.ResetStats(Type);
			this.wet = false;
			this.wetCount = 0;
			this.lavaWet = false;
			this.timeSinceTheItemHasBeenReservedForSomeone = 0;
			this.instanced = false;
			this.UpdateEntityFields();
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x004A7B56 File Offset: 0x004A5D56
		public void SetDefaultsBringOver()
		{
			if (Main.netMode == 1 || Main.netMode == 2)
			{
				this.playerIndexTheItemIsReservedFor = 255;
				return;
			}
			this.playerIndexTheItemIsReservedFor = Main.myPlayer;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x004A7B7F File Offset: 0x004A5D7F
		public bool active
		{
			get
			{
				return this.inner.active;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x004A7B8C File Offset: 0x004A5D8C
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x004A7B99 File Offset: 0x004A5D99
		public int type
		{
			get
			{
				return this.inner.type;
			}
			set
			{
				this.inner.type = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x004A7BA7 File Offset: 0x004A5DA7
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x004A7BB4 File Offset: 0x004A5DB4
		public int stack
		{
			get
			{
				return this.inner.stack;
			}
			set
			{
				this.inner.stack = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x004A7BC2 File Offset: 0x004A5DC2
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x004A7BCF File Offset: 0x004A5DCF
		public bool newAndShiny
		{
			get
			{
				return this.inner.newAndShiny;
			}
			set
			{
				this.inner.newAndShiny = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x004A7BDD File Offset: 0x004A5DDD
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x004A7BEA File Offset: 0x004A5DEA
		public Color color
		{
			get
			{
				return this.inner.color;
			}
			set
			{
				this.inner.color = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x004A7BF8 File Offset: 0x004A5DF8
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x004A7C05 File Offset: 0x004A5E05
		public bool favorited
		{
			get
			{
				return this.inner.favorited;
			}
			set
			{
				this.inner.favorited = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x004A7C13 File Offset: 0x004A5E13
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x004A7C20 File Offset: 0x004A5E20
		public short makeNPC
		{
			get
			{
				return this.inner.makeNPC;
			}
			set
			{
				this.inner.makeNPC = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x004A7C2E File Offset: 0x004A5E2E
		public int value
		{
			get
			{
				return this.inner.value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x004A7C3B File Offset: 0x004A5E3B
		public int useTime
		{
			get
			{
				return this.inner.useTime;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x004A7C48 File Offset: 0x004A5E48
		public int useAnimation
		{
			get
			{
				return this.inner.useAnimation;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x004A7C55 File Offset: 0x004A5E55
		public int useAmmo
		{
			get
			{
				return this.inner.useAmmo;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x004A7C62 File Offset: 0x004A5E62
		public int maxStack
		{
			get
			{
				return this.inner.maxStack;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x004A7C6F File Offset: 0x004A5E6F
		public int damage
		{
			get
			{
				return this.inner.damage;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x004A7C7C File Offset: 0x004A5E7C
		public float knockBack
		{
			get
			{
				return this.inner.knockBack;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x004A7C89 File Offset: 0x004A5E89
		public float shootSpeed
		{
			get
			{
				return this.inner.shootSpeed;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x004A7C96 File Offset: 0x004A5E96
		public float scale
		{
			get
			{
				return this.inner.scale;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x004A7CA3 File Offset: 0x004A5EA3
		public int ammo
		{
			get
			{
				return this.inner.ammo;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x004A7CB0 File Offset: 0x004A5EB0
		public bool notAmmo
		{
			get
			{
				return this.inner.notAmmo;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x004A7CBD File Offset: 0x004A5EBD
		public int shoot
		{
			get
			{
				return this.inner.shoot;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x004A7CCA File Offset: 0x004A5ECA
		public int rare
		{
			get
			{
				return this.inner.rare;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x004A7CD7 File Offset: 0x004A5ED7
		public int placeStyle
		{
			get
			{
				return this.inner.placeStyle;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x004A7CE4 File Offset: 0x004A5EE4
		public int createTile
		{
			get
			{
				return this.inner.createTile;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x004A7CF1 File Offset: 0x004A5EF1
		public int glowMask
		{
			get
			{
				return (int)this.inner.glowMask;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x004A7CFE File Offset: 0x004A5EFE
		public bool expert
		{
			get
			{
				return this.inner.expert;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x004A7D0B File Offset: 0x004A5F0B
		public string Name
		{
			get
			{
				return this.inner.Name;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x004A7D18 File Offset: 0x004A5F18
		public int alpha
		{
			get
			{
				return this.inner.alpha;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x004A7D25 File Offset: 0x004A5F25
		public int buffType
		{
			get
			{
				return this.inner.buffType;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x004A7D32 File Offset: 0x004A5F32
		public bool IsACoin
		{
			get
			{
				return this.inner.IsACoin;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x004A7D3F File Offset: 0x004A5F3F
		public bool IsAir
		{
			get
			{
				return this.inner.IsAir;
			}
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x004A7D4C File Offset: 0x004A5F4C
		public void SetDefaults(int type)
		{
			this.ResetStats(type);
			this.inner.SetDefaults(type, null);
			this.UpdateEntityFields();
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x004A7D68 File Offset: 0x004A5F68
		public void TurnToAir(bool fullReset = false)
		{
			this.inner.TurnToAir(fullReset);
			this.UpdateEntityFields();
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x004A7D7C File Offset: 0x004A5F7C
		public void Prefix(int prefix)
		{
			this.inner.Prefix(prefix);
			this.UpdateEntityFields();
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x004A7D91 File Offset: 0x004A5F91
		public bool OnlyNeedOneInInventory()
		{
			return this.inner.OnlyNeedOneInInventory();
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x004A7D9E File Offset: 0x004A5F9E
		public Color GetColor(Color newColor)
		{
			return this.inner.GetColor(newColor);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x004A7DAC File Offset: 0x004A5FAC
		public Color GetAlpha(Color newColor)
		{
			return this.inner.GetAlpha(newColor);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x004A7DBA File Offset: 0x004A5FBA
		public string AffixName()
		{
			return this.inner.AffixName();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x004A7DC8 File Offset: 0x004A5FC8
		public void TryCombiningIntoNearbyItems(int myItemIndex)
		{
			if (this.playerIndexTheItemIsReservedFor != Main.myPlayer)
			{
				return;
			}
			if (!this.inner.CanPassivelyStackInWorld())
			{
				return;
			}
			if (this.stack >= this.maxStack)
			{
				return;
			}
			int num = 30;
			for (int i = myItemIndex + 1; i < 400; i++)
			{
				WorldItem worldItem = Main.item[i];
				if (!worldItem.IsAir && Item.CanStack(this.inner, worldItem.inner) && worldItem.shimmered == this.shimmered && worldItem.playerIndexTheItemIsReservedFor == this.playerIndexTheItemIsReservedFor && Math.Abs(this.position.X - worldItem.position.X) + Math.Abs(this.position.Y - worldItem.position.Y) <= (float)num)
				{
					int num2 = Math.Min(worldItem.stack, this.maxStack - this.stack);
					worldItem.stack -= num2;
					this.stack += num2;
					float num3 = (float)num2 / (float)this.stack;
					this.position = Vector2.Lerp(worldItem.position, this.position, num3);
					this.velocity = Vector2.Lerp(worldItem.velocity, this.velocity, num3);
					if (worldItem.stack <= 0)
					{
						worldItem.TurnToAir(false);
					}
					if (Main.netMode != 0)
					{
						NetMessage.SendData(21, -1, -1, null, myItemIndex, 0f, 0f, 0f, 0, 0, 0);
						NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x004A7F6C File Offset: 0x004A616C
		public void FindOwner()
		{
			if (Main.netMode == 1 && this.shimmerTime > 0f)
			{
				this.keepTime = 0;
			}
			if (this.keepTime > 0)
			{
				return;
			}
			int num = this.playerIndexTheItemIsReservedFor;
			int num2 = 255;
			bool flag = true;
			if (this.type == 267 && this.ownIgnore != -1)
			{
				flag = false;
			}
			if (EmergencyStacking.HasPendingTransferInvolving(this))
			{
				num2 = 255;
			}
			else if (this.shimmerTime > 0f)
			{
				num2 = 255;
			}
			else if (flag)
			{
				float num3 = (float)NPC.sWidth;
				for (int i = 0; i < 255; i++)
				{
					if (this.ownIgnore != i)
					{
						Player player = Main.player[i];
						if (player.active && !player.dead)
						{
							Player.ItemSpaceStatus itemSpaceStatus = player.ItemSpace(Main.item[this.whoAmI]);
							if (player.CanPullItem(Main.item[this.whoAmI], itemSpaceStatus))
							{
								float num4 = Math.Abs(player.position.X + (float)(player.width / 2) - this.position.X - (float)(this.width / 2)) + Math.Abs(player.position.Y + (float)(player.height / 2) - this.position.Y - (float)this.height);
								if (player.manaMagnet && (this.type == 184 || this.type == 1735 || this.type == 1868))
								{
									num4 -= (float)Item.manaGrabRange;
								}
								if (player.lifeMagnet && (this.type == 58 || this.type == 1734 || this.type == 1867))
								{
									num4 -= (float)Item.lifeGrabRange;
								}
								if (this.type == 4143)
								{
									num4 -= (float)Item.manaGrabRange;
								}
								if (num3 > num4)
								{
									num3 = num4;
									num2 = i;
								}
							}
						}
					}
				}
				if (Main.netMode != 0 && num2 != 255)
				{
					Player player2 = Main.player[num2];
					int itemGrabRange = player2.GetItemGrabRange(this);
					Rectangle hitbox = player2.Hitbox;
					hitbox.Inflate(itemGrabRange, itemGrabRange);
					if (!hitbox.Intersects(base.Hitbox) && Wiring.IsHopperInRangeOf(this))
					{
						num2 = 255;
					}
				}
			}
			if (num2 == num)
			{
				return;
			}
			if (Main.netMode == 1)
			{
				this.playerIndexTheItemIsReservedFor = 255;
				NetMessage.SendData(39, -1, -1, null, this.whoAmI, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (num != Main.myPlayer && Main.player[num].active)
			{
				this.playerIndexTheItemIsReservedFor = num;
				if (this.timeSinceTheItemHasBeenReservedForSomeone >= 0)
				{
					this.timeSinceTheItemHasBeenReservedForSomeone = -1;
					NetMessage.SendData(39, num, -1, null, this.whoAmI, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
			}
			else
			{
				this.playerIndexTheItemIsReservedFor = num2;
				this.timeSinceTheItemHasBeenReservedForSomeone = 0;
				NetMessage.SendData(22, -1, -1, null, this.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x004A827C File Offset: 0x004A647C
		private void UpdateEntityFields()
		{
			this.width = (this.height = 16);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x004A829C File Offset: 0x004A649C
		public void UpdateItem(int i)
		{
			this.UpdateEntityFields();
			this.whoAmI = i;
			if (Main.timeItemSlotCannotBeReusedFor[i] > 0)
			{
				if (Main.netMode == 2)
				{
					Main.timeItemSlotCannotBeReusedFor[i]--;
					return;
				}
				Main.timeItemSlotCannotBeReusedFor[i] = 0;
			}
			if (!this.active)
			{
				return;
			}
			if (this.instanced)
			{
				if (Main.netMode == 2)
				{
					this.TurnToAir(false);
					return;
				}
				this.keepTime = 6000;
				this.ownTime = 0;
				this.noGrabDelay = 0;
				this.playerIndexTheItemIsReservedFor = Main.myPlayer;
			}
			if (Main.netMode == 0)
			{
				this.playerIndexTheItemIsReservedFor = Main.myPlayer;
			}
			float num = 0.1f;
			float num2 = 7f;
			if (Main.netMode == 1)
			{
				Point point = base.Bottom.ToTileCoordinates();
				if (WorldGen.InWorld(point, 0) && Main.tile[point.X, point.Y] == null)
				{
					num = 0f;
					this.velocity = Vector2.Zero;
					if (this.instanced && Main.GameUpdateCount % 10U == 0U)
					{
						NetMessage.SendData(159, -1, -1, null, point.X / 200, (float)(point.Y / 150), 0f, 0f, 0, 0, 0);
					}
				}
			}
			Vector2 vector = this.velocity * 0.5f;
			if (this.shimmerWet)
			{
				num = 0.065f;
				num2 = 4f;
				vector = this.velocity * 0.375f;
			}
			else if (this.honeyWet)
			{
				num = 0.05f;
				num2 = 3f;
				vector = this.velocity * 0.25f;
			}
			else if (this.wet)
			{
				num = 0.08f;
				num2 = 5f;
			}
			if (this.ownTime > 0)
			{
				this.ownTime--;
			}
			else
			{
				this.ownIgnore = -1;
			}
			if (this.keepTime > 0)
			{
				this.keepTime--;
			}
			if (!this.beingGrabbed)
			{
				if (this.type == 205 && this.playerIndexTheItemIsReservedFor == Main.myPlayer && Main.raining && (Main.isThereAWorldSurface || Main.remixWorld) && WorldGen.IsSurfaceForAtmospherics(this.position.ToTileCoordinates()))
				{
					int num3 = (int)base.Center.X / 16;
					int num4 = (int)base.Center.Y / 16;
					if (WorldGen.InWorld(num3, num4, 0) && WallID.Sets.AllowsWind[(int)Main.tile[num3, num4].wall])
					{
						int num5 = 600;
						if (Main.dayRate > 0 && Main.dayRate < num5)
						{
							num5 /= Main.dayRate;
						}
						if (Main.rand.Next(num5) == 0 && Main.rand.NextFloat() < Main.maxRaining)
						{
							int stack = this.stack;
							this.SetDefaults(206);
							this.playerIndexTheItemIsReservedFor = Main.myPlayer;
							this.stack = stack;
							NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
				if (this.shimmered)
				{
					if (Main.rand.Next(30) == 0)
					{
						int num6 = Dust.NewDust(this.position, this.width, this.height, 309, 0f, 0f, 0, default(Color), 1f);
						Dust dust = Main.dust[num6];
						dust.position.X = dust.position.X + (float)Main.rand.Next(-8, 5);
						Dust dust2 = Main.dust[num6];
						dust2.position.Y = dust2.position.Y + (float)Main.rand.Next(-8, 5);
						Main.dust[num6].scale *= 1.1f;
						Main.dust[num6].velocity *= 0.3f;
						int num7 = Main.rand.Next(6);
						if (num7 == 0)
						{
							Main.dust[num6].color = new Color(255, 255, 210);
						}
						else if (num7 == 1)
						{
							Main.dust[num6].color = new Color(190, 245, 255);
						}
						else if (num7 == 2)
						{
							Main.dust[num6].color = new Color(255, 150, 255);
						}
						else
						{
							Main.dust[num6].color = new Color(190, 175, 255);
						}
					}
					Lighting.AddLight(base.Center, (1f - this.shimmerTime) * 0.8f, (1f - this.shimmerTime) * 0.8f, (1f - this.shimmerTime) * 0.8f);
					num = 0f;
					if (this.shimmerWet)
					{
						if (this.velocity.Y > -4f)
						{
							this.velocity.Y = this.velocity.Y - 0.05f;
						}
					}
					else
					{
						int num8 = 2;
						int num9 = (int)(base.Center.X / 16f);
						int num10 = (int)(base.Center.Y / 16f);
						bool flag = false;
						for (int j = num10; j < num10 + num8; j++)
						{
							if (WorldGen.InWorld(num9, j, 0) && Main.tile[num9, j] != null && Main.tile[num9, j].shimmer() && Main.tile[num9, j].liquid > 0)
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							if (this.velocity.Y > -4f)
							{
								this.velocity.Y = this.velocity.Y - 0.05f;
							}
						}
						else
						{
							this.velocity.Y = this.velocity.Y * 0.9f;
						}
					}
				}
				if (this.shimmerWet && !this.shimmered)
				{
					this.Shimmering();
				}
				else if (this.shimmerTime > 0f)
				{
					this.shimmerTime -= 0.01f;
					if (this.shimmerTime < 0f)
					{
						this.shimmerTime = 0f;
					}
				}
				if (this.shimmerTime == 0f)
				{
					this.TryCombiningIntoNearbyItems(i);
				}
				if (this.timeLeftInWhichTheItemCannotBeTakenByEnemies > 0)
				{
					this.timeLeftInWhichTheItemCannotBeTakenByEnemies--;
				}
				if (this.timeLeftInWhichTheItemCannotBeTakenByEnemies == 0 && this.playerIndexTheItemIsReservedFor == Main.myPlayer)
				{
					this.GetPickedUpByMonsters_Special(i);
					if (Main.expertMode && this.IsACoin)
					{
						this.GetPickedUpByMonsters_Money(i);
					}
				}
				this.MoveInWorld(num, num2, ref vector, i);
				if (this.lavaWet)
				{
					this.CheckLavaDeath(i);
				}
				this.CheckInWorld(i);
				this.DespawnIfMeetingConditions(i);
				if (this.type == 74)
				{
					this.TryGrantingMakeAWishSet();
				}
			}
			else
			{
				this.wet = false;
				this.wetCount = 0;
				this.lavaWet = false;
				this.honeyWet = false;
				this.shimmerWet = false;
				this.beingGrabbed = false;
				this.onConveyor = false;
				this.ApplyMovement(ref vector);
			}
			this.UpdateItem_VisualEffects();
			if (this.timeSinceItemSpawned < 2147483547)
			{
				this.timeSinceItemSpawned++;
			}
			if (this.noGrabDelay > 0)
			{
				this.noGrabDelay--;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x004A89C8 File Offset: 0x004A6BC8
		private void CheckInWorld(int i)
		{
			if (WorldGen.InWorld(this.position.ToTileCoordinates(), 20))
			{
				return;
			}
			if (ItemID.Sets.RecoverableImportantItem[this.type])
			{
				Point point;
				if ((this.instanced || Main.netMode == 0) && Main.LocalPlayer.SpawnX >= 0)
				{
					point = new Point(Main.LocalPlayer.SpawnX, Main.LocalPlayer.SpawnY);
				}
				else
				{
					point = new Point(Main.spawnTileX, Main.spawnTileY);
				}
				base.Center = point.ToWorldCoordinates(8f, 8f);
				this.velocity = Vector2.Zero;
			}
			else
			{
				this.TurnToAir(false);
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x004A8A94 File Offset: 0x004A6C94
		private void TryGrantingMakeAWishSet()
		{
			if (this.playerIndexTheItemIsReservedFor != Main.myPlayer)
			{
				return;
			}
			if (!this.wet || this.stack != 1)
			{
				return;
			}
			if (this.ownIgnore == 1 && this.noGrabDelay <= 0)
			{
				return;
			}
			byte b = Player.FindClosest(this.position, this.width, this.height);
			if (b == 255)
			{
				return;
			}
			if (!Main.player[(int)b].ZoneDesert)
			{
				return;
			}
			this.TurnToAir(false);
			if (Main.netMode != 0)
			{
				NetMessage.SendData(21, -1, -1, null, this.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
			bool flag = true;
			int num = 5;
			this.SpawnShimmeredItem(5655, flag, num);
			this.SpawnShimmeredItem(5656, flag, num);
			this.SpawnShimmeredItem(5657, flag, num);
			this.SpawnShimmeredItem(5658, flag, num);
			this.SpawnShimmeredItem(5661, flag, num);
			ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.HeroicisSetSpawnSound, new ParticleOrchestraSettings
			{
				PositionInWorld = base.Center
			});
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x004A8B9C File Offset: 0x004A6D9C
		private void SpawnShimmeredItem(short idToCheck, bool splitToSides, int numberOfItems)
		{
			int num = Item.NewItem(this.GetItemSource_Misc(ItemSourceID.Shimmer), (int)this.position.X, (int)this.position.Y, this.width, this.height, (int)idToCheck, 1, false, 0, false);
			WorldItem worldItem = Main.item[num];
			worldItem.stack = 1;
			worldItem.shimmerTime = 1f;
			worldItem.shimmered = true;
			worldItem.shimmerWet = true;
			worldItem.wet = true;
			worldItem.velocity *= 0.1f;
			worldItem.playerIndexTheItemIsReservedFor = Main.myPlayer;
			if (splitToSides)
			{
				worldItem.velocity.X = 1f * (float)numberOfItems;
				WorldItem worldItem2 = worldItem;
				worldItem2.velocity.X = worldItem2.velocity.X * (1f + (float)numberOfItems * 0.05f);
				if (numberOfItems % 2 == 0)
				{
					WorldItem worldItem3 = worldItem;
					worldItem3.velocity.X = worldItem3.velocity.X * -1f;
				}
			}
			NetMessage.SendData(145, -1, -1, null, num, 1f, 0f, 0f, 0, 0, 0);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x004A8CA0 File Offset: 0x004A6EA0
		private void DespawnIfMeetingConditions(int i)
		{
			if (this.type == 75 && Main.dayTime && !Main.remixWorld && !this.shimmered && !this.beingGrabbed)
			{
				for (int j = 0; j < 10; j++)
				{
					Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X, this.velocity.Y, 150, default(Color), 1.2f);
				}
				for (int k = 0; k < 3; k++)
				{
					Gore.NewGore(this.position, new Vector2(this.velocity.X, this.velocity.Y), Main.rand.Next(16, 18), 1f);
				}
				this.TurnToAir(false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (this.type == 4143 && this.timeSinceItemSpawned > 300)
			{
				for (int l = 0; l < 20; l++)
				{
					Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X, this.velocity.Y, 150, Color.Lerp(Color.CornflowerBlue, Color.Indigo, Main.rand.NextFloat()), 1.2f);
				}
				this.TurnToAir(false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
			if (this.type == 3822 && !DD2Event.Ongoing)
			{
				int num = Main.rand.Next(18, 24);
				for (int m = 0; m < num; m++)
				{
					int num2 = Dust.NewDust(base.Center, 0, 0, 61, 0f, 0f, 0, default(Color), 1.7f);
					Main.dust[num2].velocity *= 8f;
					Dust dust = Main.dust[num2];
					dust.velocity.Y = dust.velocity.Y - 1f;
					Main.dust[num2].position = Vector2.Lerp(Main.dust[num2].position, base.Center, 0.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].noLight = true;
				}
				this.TurnToAir(false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x004A8F68 File Offset: 0x004A7168
		private void CheckLavaDeath(int i)
		{
			if (this.type == 267)
			{
				if (Main.netMode != 1)
				{
					int num = this.stack;
					this.TurnToAir(false);
					bool flag = false;
					for (int j = 0; j < Main.maxNPCs; j++)
					{
						if (Main.npc[j].active && Main.npc[j].type == 22)
						{
							int num2 = -Main.npc[j].direction;
							if (Main.npc[j].IsNPCValidForBestiaryKillCredit())
							{
								Main.BestiaryTracker.Kills.RegisterKill(Main.npc[j]);
							}
							Main.npc[j].StrikeNPCNoInteraction(9999, 10f, -num2, false, false, false);
							num--;
							flag = true;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(28, -1, -1, null, j, 9999f, 10f, (float)(-(float)num2), 0, 0, 0);
							}
							NPC.SpawnWOF(this.position);
						}
					}
					if (flag)
					{
						List<int> list = new List<int>();
						for (int k = 0; k < Main.maxNPCs; k++)
						{
							if (num <= 0)
							{
								break;
							}
							NPC npc = Main.npc[k];
							if (npc.active && npc.isLikeATownNPC)
							{
								list.Add(k);
							}
						}
						while (num > 0 && list.Count > 0)
						{
							int num3 = Main.rand.Next(list.Count);
							int num4 = list[num3];
							list.RemoveAt(num3);
							int num5 = -Main.npc[num4].direction;
							if (Main.npc[num4].IsNPCValidForBestiaryKillCredit())
							{
								Main.BestiaryTracker.Kills.RegisterKill(Main.npc[num4]);
							}
							Main.npc[num4].StrikeNPCNoInteraction(9999, 10f, -num5, false, false, false);
							num--;
							if (Main.netMode == 2)
							{
								NetMessage.SendData(28, -1, -1, null, num4, 9999f, 10f, (float)(-(float)num5), 0, 0, 0);
							}
						}
					}
					NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
			}
			else if (this.playerIndexTheItemIsReservedFor == Main.myPlayer)
			{
				if (this.type <= 0 && this.type >= (int)ItemID.Count)
				{
					return;
				}
				if ((this.rare != 0 && this.rare != -1) || ItemID.Sets.IsLavaImmuneRegardlessOfRarity[this.type])
				{
					return;
				}
				this.TurnToAir(false);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x004A91F8 File Offset: 0x004A73F8
		private void Shimmering()
		{
			if (this.inner.CanShimmer())
			{
				int num = (int)(base.Center.X / 16f);
				int num2 = (int)(this.position.Y / 16f - 1f);
				Tile tile = Main.tile[num, num2];
				if (WorldGen.InWorld(num, num2, 0) && tile != null && tile.liquid > 0 && tile.shimmer())
				{
					if (this.playerIndexTheItemIsReservedFor == Main.myPlayer && Main.netMode != 1)
					{
						this.shimmerTime += 0.01f;
						if (this.shimmerTime > 0.9f)
						{
							this.shimmerTime = 0.9f;
							this.GetShimmered();
							return;
						}
					}
					else
					{
						this.shimmerTime += 0.01f;
						if (this.shimmerTime > 1f)
						{
							this.shimmerTime = 1f;
						}
					}
					return;
				}
			}
			if (this.shimmerTime > 0f)
			{
				this.shimmerTime -= 0.01f;
				if (this.shimmerTime < 0f)
				{
					this.shimmerTime = 0f;
				}
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x004A931C File Offset: 0x004A751C
		private void MoveInWorld(float gravity, float maxFallSpeed, ref Vector2 wetVelocity, int i)
		{
			if (!this.shimmered && ItemID.Sets.ItemNoGravity[this.type])
			{
				this.velocity.X = this.velocity.X * 0.95f;
				if ((double)this.velocity.X < 0.1 && (double)this.velocity.X > -0.1)
				{
					this.velocity.X = 0f;
				}
				this.velocity.Y = this.velocity.Y * 0.95f;
				if ((double)this.velocity.Y < 0.1 && (double)this.velocity.Y > -0.1)
				{
					this.velocity.Y = 0f;
				}
			}
			else
			{
				bool flag = false;
				if (this.shimmered && this.active)
				{
					int num = 50;
					for (int j = 0; j < 400; j++)
					{
						if (i != j && Main.item[j].active && Main.item[j].shimmered)
						{
							if (num-- <= 0)
							{
								break;
							}
							float num2 = (float)((this.width + Main.item[j].width) / 2);
							if (Math.Abs(base.Center.X - Main.item[j].Center.X) <= num2 && Math.Abs(base.Center.Y - Main.item[j].Center.Y) <= num2)
							{
								flag = true;
								float num3 = Vector2.Distance(base.Center, Main.item[j].Center);
								num2 /= num3;
								if (num2 > 10f)
								{
									num2 = 10f;
								}
								if (base.Center.X < Main.item[j].Center.X)
								{
									if (this.velocity.X > -3f * num2)
									{
										this.velocity.X = this.velocity.X - 0.1f * num2;
									}
									if (Main.item[j].velocity.X < 3f)
									{
										WorldItem worldItem = Main.item[j];
										worldItem.velocity.X = worldItem.velocity.X + 0.1f * num2;
									}
								}
								else if (base.Center.X > Main.item[j].Center.X)
								{
									if (this.velocity.X < 3f * num2)
									{
										this.velocity.X = this.velocity.X + 0.1f * num2;
									}
									if (Main.item[j].velocity.X > -3f)
									{
										WorldItem worldItem2 = Main.item[j];
										worldItem2.velocity.X = worldItem2.velocity.X - 0.1f * num2;
									}
								}
								else if (i < j)
								{
									if (this.velocity.X > -3f * num2)
									{
										this.velocity.X = this.velocity.X - 0.1f * num2;
									}
									if (Main.item[j].velocity.X < 3f * num2)
									{
										WorldItem worldItem3 = Main.item[j];
										worldItem3.velocity.X = worldItem3.velocity.X + 0.1f * num2;
									}
								}
							}
						}
					}
				}
				this.velocity.Y = this.velocity.Y + gravity;
				if (this.velocity.Y > maxFallSpeed)
				{
					this.velocity.Y = maxFallSpeed;
				}
				this.velocity.X = this.velocity.X * 0.95f;
				if ((double)this.velocity.X < 0.1 && (double)this.velocity.X > -0.1)
				{
					this.velocity.X = 0f;
				}
				if (flag)
				{
					this.velocity.X = this.velocity.X * 0.8f;
				}
			}
			this.onConveyor = Collision.ApplyConveyorBeltMovementToVelocity(this, ref this.velocity);
			bool flag2 = Collision.LavaCollision(this.position, this.width, this.height);
			if (flag2)
			{
				this.lavaWet = true;
			}
			bool flag3 = Collision.WetCollision(this.position, this.width, this.height);
			if (Collision.honey)
			{
				this.honeyWet = true;
			}
			if (Collision.shimmer)
			{
				this.shimmerWet = true;
			}
			if (flag3)
			{
				if (!this.wet)
				{
					if (this.wetCount == 0)
					{
						this.wetCount = 20;
						if (!flag2)
						{
							if (this.shimmerWet)
							{
								for (int k = 0; k < 10; k++)
								{
									int num4 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 308, 0f, 0f, 0, default(Color), 1f);
									Dust dust = Main.dust[num4];
									dust.velocity.Y = dust.velocity.Y - 4f;
									Dust dust2 = Main.dust[num4];
									dust2.velocity.X = dust2.velocity.X * 2.5f;
									Main.dust[num4].scale = 0.8f;
									Main.dust[num4].noGravity = true;
									int num5 = Main.rand.Next(6);
									if (num5 == 0)
									{
										Main.dust[num4].color = new Color(255, 255, 210);
									}
									else if (num5 == 1)
									{
										Main.dust[num4].color = new Color(190, 245, 255);
									}
									else if (num5 == 2)
									{
										Main.dust[num4].color = new Color(255, 150, 255);
									}
									else
									{
										Main.dust[num4].color = new Color(190, 175, 255);
									}
								}
								SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 4, 1f, 0f);
							}
							else if (this.honeyWet)
							{
								for (int l = 0; l < 5; l++)
								{
									int num6 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 152, 0f, 0f, 0, default(Color), 1f);
									Dust dust3 = Main.dust[num6];
									dust3.velocity.Y = dust3.velocity.Y - 1f;
									Dust dust4 = Main.dust[num6];
									dust4.velocity.X = dust4.velocity.X * 2.5f;
									Main.dust[num6].scale = 1.3f;
									Main.dust[num6].alpha = 100;
									Main.dust[num6].noGravity = true;
								}
								SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
							}
							else
							{
								for (int m = 0; m < 10; m++)
								{
									int num7 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
									Dust dust5 = Main.dust[num7];
									dust5.velocity.Y = dust5.velocity.Y - 4f;
									Dust dust6 = Main.dust[num7];
									dust6.velocity.X = dust6.velocity.X * 2.5f;
									Main.dust[num7].scale *= 0.8f;
									Main.dust[num7].alpha = 100;
									Main.dust[num7].noGravity = true;
								}
								SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
							}
						}
						else
						{
							for (int n = 0; n < 5; n++)
							{
								int num8 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
								Dust dust7 = Main.dust[num8];
								dust7.velocity.Y = dust7.velocity.Y - 1.5f;
								Dust dust8 = Main.dust[num8];
								dust8.velocity.X = dust8.velocity.X * 2.5f;
								Main.dust[num8].scale = 1.3f;
								Main.dust[num8].alpha = 100;
								Main.dust[num8].noGravity = true;
							}
							SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
						}
					}
					this.wet = true;
				}
			}
			else if (this.wet)
			{
				this.wet = false;
				if (this.wetCount == 0)
				{
					this.wetCount = 20;
					if (!this.lavaWet)
					{
						if (this.shimmerWet)
						{
							for (int num9 = 0; num9 < 10; num9++)
							{
								int num10 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 308, 0f, 0f, 0, default(Color), 1f);
								Dust dust9 = Main.dust[num10];
								dust9.velocity.Y = dust9.velocity.Y - 4f;
								Dust dust10 = Main.dust[num10];
								dust10.velocity.X = dust10.velocity.X * 2.5f;
								Main.dust[num10].scale = 0.8f;
								Main.dust[num10].noGravity = true;
								int num11 = Main.rand.Next(6);
								if (num11 == 0)
								{
									Main.dust[num10].color = new Color(255, 255, 210);
								}
								else if (num11 == 1)
								{
									Main.dust[num10].color = new Color(190, 245, 255);
								}
								else if (num11 == 2)
								{
									Main.dust[num10].color = new Color(255, 150, 255);
								}
								else
								{
									Main.dust[num10].color = new Color(190, 175, 255);
								}
							}
							SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 5, 1f, 0f);
						}
						else if (this.honeyWet)
						{
							for (int num12 = 0; num12 < 5; num12++)
							{
								int num13 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 152, 0f, 0f, 0, default(Color), 1f);
								Dust dust11 = Main.dust[num13];
								dust11.velocity.Y = dust11.velocity.Y - 1f;
								Dust dust12 = Main.dust[num13];
								dust12.velocity.X = dust12.velocity.X * 2.5f;
								Main.dust[num13].scale = 1.3f;
								Main.dust[num13].alpha = 100;
								Main.dust[num13].noGravity = true;
							}
							SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
						}
						else
						{
							for (int num14 = 0; num14 < 10; num14++)
							{
								int num15 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2)), this.width + 12, 24, Dust.dustWater(), 0f, 0f, 0, default(Color), 1f);
								Dust dust13 = Main.dust[num15];
								dust13.velocity.Y = dust13.velocity.Y - 4f;
								Dust dust14 = Main.dust[num15];
								dust14.velocity.X = dust14.velocity.X * 2.5f;
								Main.dust[num15].scale *= 0.8f;
								Main.dust[num15].alpha = 100;
								Main.dust[num15].noGravity = true;
							}
							SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
						}
					}
					else
					{
						for (int num16 = 0; num16 < 5; num16++)
						{
							int num17 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
							Dust dust15 = Main.dust[num17];
							dust15.velocity.Y = dust15.velocity.Y - 1.5f;
							Dust dust16 = Main.dust[num17];
							dust16.velocity.X = dust16.velocity.X * 2.5f;
							Main.dust[num17].scale = 1.3f;
							Main.dust[num17].alpha = 100;
							Main.dust[num17].noGravity = true;
						}
						SoundEngine.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1, 1f, 0f);
					}
				}
			}
			if (!this.wet)
			{
				this.lavaWet = false;
				this.honeyWet = false;
				this.shimmerWet = false;
			}
			if (this.wetCount > 0)
			{
				this.wetCount -= 1;
			}
			if (this.wet)
			{
				if (this.wet)
				{
					Vector2 velocity = this.velocity;
					this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, false, false, 1, false, true, true);
					if (this.velocity.X != velocity.X)
					{
						wetVelocity.X = this.velocity.X;
					}
					if (this.velocity.Y != velocity.Y)
					{
						wetVelocity.Y = this.velocity.Y;
					}
				}
			}
			else
			{
				this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, false, false, 1, false, true, true);
			}
			this.ApplyMovement(ref wetVelocity);
			Vector4 vector = Collision.SlopeCollision(this.position, this.velocity, this.width, this.height, gravity, false, true);
			this.position.X = vector.X;
			this.position.Y = vector.Y;
			this.velocity.X = vector.Z;
			this.velocity.Y = vector.W;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x004AA2E7 File Offset: 0x004A84E7
		private void ApplyMovement(ref Vector2 wetVelocity)
		{
			if (this.wet)
			{
				this.position += wetVelocity;
				return;
			}
			this.position += this.velocity;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x004AA320 File Offset: 0x004A8520
		private void GetPickedUpByMonsters_Special(int i)
		{
			bool flag = false;
			bool flag2 = false;
			int type = this.type;
			if ((type == 89 || type == 3507) && !NPC.unlockedSlimeCopperSpawn)
			{
				flag = true;
				flag2 = true;
			}
			if (!flag2)
			{
				return;
			}
			bool flag3 = false;
			Rectangle hitbox = base.Hitbox;
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.active && flag && npc.type >= 0 && npc.type < (int)NPCID.Count && NPCID.Sets.CanConvertIntoCopperSlimeTownNPC[npc.type] && hitbox.Intersects(npc.Hitbox))
				{
					flag3 = true;
					NPC.TransformCopperSlime(j);
					break;
				}
			}
			if (flag3)
			{
				this.TurnToAir(true);
				NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x004AA3F4 File Offset: 0x004A85F4
		private void GetPickedUpByMonsters_Money(int i)
		{
			Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
			for (int j = 0; j < Main.maxNPCs; j++)
			{
				NPC npc = Main.npc[j];
				if (npc.active && npc.lifeMax > 5 && !npc.friendly && !npc.immortal && !npc.dontTakeDamage && !npc.SpawnedFromStatue && !NPCID.Sets.CantTakeLunchMoney[npc.type])
				{
					float num = (float)this.stack;
					float num2 = 1f;
					if (this.type == 72)
					{
						num2 = 100f;
					}
					if (this.type == 73)
					{
						num2 = 10000f;
					}
					if (this.type == 74)
					{
						num2 = 1000000f;
					}
					num *= num2;
					float num3 = (float)npc.extraValue;
					int num4 = npc.realLife;
					NPC npc2 = npc;
					if (num4 >= 0 && Main.npc[num4].active)
					{
						npc2 = Main.npc[num4];
						num3 = (float)npc2.extraValue;
					}
					else
					{
						num4 = -1;
					}
					if (num3 < num && num3 + num < 999000000f)
					{
						Rectangle rectangle2 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
						if (rectangle.Intersects(rectangle2))
						{
							float num5 = (float)Main.rand.Next(50, 76) * 0.01f;
							if (this.type == 71)
							{
								num5 += (float)Main.rand.Next(51) * 0.01f;
							}
							if (this.type == 72)
							{
								num5 += (float)Main.rand.Next(26) * 0.01f;
							}
							if (num5 > 1f)
							{
								num5 = 1f;
							}
							int num6 = (int)((float)this.stack * num5);
							if (num6 < 1)
							{
								num6 = 1;
							}
							if (num6 > this.stack)
							{
								num6 = this.stack;
							}
							this.stack -= num6;
							int num7 = (int)((float)num6 * num2);
							int num8 = j;
							if (num4 >= 0)
							{
								num8 = num4;
							}
							npc2.extraValue += num7;
							if (Main.netMode == 0)
							{
								npc2.moneyPing(this.position);
							}
							else
							{
								NetMessage.SendData(92, -1, -1, null, num8, (float)num7, this.position.X, this.position.Y, 0, 0, 0);
							}
							if (this.stack <= 0)
							{
								this.TurnToAir(true);
							}
							NetMessage.SendData(21, -1, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
						}
					}
				}
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x004AA6A8 File Offset: 0x004A88A8
		private void UpdateItem_VisualEffects()
		{
			if (this.type == 5043)
			{
				float num = (float)Main.rand.Next(90, 111) * 0.01f;
				num *= (Main.essScale + 0.5f) / 2f;
				Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.25f * num, 0.25f * num, 0.25f * num);
				return;
			}
			if (this.type == 116)
			{
				float num2 = (float)Main.rand.Next(95, 106) * 0.01f;
				Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.56f * num2, 0.43f * num2, 0.15f * num2);
				if (Main.rand.Next(250) == 0)
				{
					int num3 = Dust.NewDust(this.position, this.width, this.height, 6, 0f, 0f, 0, default(Color), (float)Main.rand.Next(3));
					if (Main.dust[num3].scale > 1f)
					{
						Main.dust[num3].noGravity = true;
						return;
					}
				}
			}
			else
			{
				if (this.type == 3191)
				{
					float num4 = (float)Main.rand.Next(90, 111) * 0.01f;
					num4 *= (Main.essScale + 0.5f) / 2f;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.3f * num4, 0.1f * num4, 0.25f * num4);
					return;
				}
				if (this.type == 520 || this.type == 3454)
				{
					float num5 = (float)Main.rand.Next(90, 111) * 0.01f;
					num5 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f * num5, 0.1f * num5, 0.25f * num5);
					return;
				}
				if (this.type == 521 || this.type == 3455)
				{
					float num6 = (float)Main.rand.Next(90, 111) * 0.01f;
					num6 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.25f * num6, 0.1f * num6, 0.5f * num6);
					return;
				}
				if (this.type == 547 || this.type == 3453)
				{
					float num7 = (float)Main.rand.Next(90, 111) * 0.01f;
					num7 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f * num7, 0.3f * num7, 0.05f * num7);
					return;
				}
				if (this.type == 548)
				{
					float num8 = (float)Main.rand.Next(90, 111) * 0.01f;
					num8 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.1f * num8, 0.1f * num8, 0.6f * num8);
					return;
				}
				if (this.type == 575)
				{
					float num9 = (float)Main.rand.Next(90, 111) * 0.01f;
					num9 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.1f * num9, 0.3f * num9, 0.5f * num9);
					return;
				}
				if (this.type == 549)
				{
					float num10 = (float)Main.rand.Next(90, 111) * 0.01f;
					num10 *= Main.essScale;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.1f * num10, 0.5f * num10, 0.2f * num10);
					return;
				}
				if (this.type == 58 || this.type == 1734 || this.type == 1867)
				{
					float num11 = (float)Main.rand.Next(90, 111) * 0.01f;
					num11 *= Main.essScale * 0.5f;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f * num11, 0.1f * num11, 0.1f * num11);
					return;
				}
				if (this.type == 184 || this.type == 1735 || this.type == 1868 || this.type == 4143)
				{
					float num12 = (float)Main.rand.Next(90, 111) * 0.01f;
					num12 *= Main.essScale * 0.5f;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.1f * num12, 0.1f * num12, 0.5f * num12);
					return;
				}
				if (this.type == 522)
				{
					float num13 = (float)Main.rand.Next(90, 111) * 0.01f;
					num13 *= Main.essScale * 0.2f;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f * num13, 1f * num13, 0.1f * num13);
					return;
				}
				if (this.type == 1332)
				{
					float num14 = (float)Main.rand.Next(90, 111) * 0.01f;
					num14 *= Main.essScale * 0.2f;
					Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f * num14, 1f * num14, 0.1f * num14);
					return;
				}
				if (this.type == 3456)
				{
					Lighting.AddLight(base.Center, new Vector3(0.2f, 0.4f, 0.5f) * Main.essScale);
					return;
				}
				if (this.type == 3457)
				{
					Lighting.AddLight(base.Center, new Vector3(0.4f, 0.2f, 0.5f) * Main.essScale);
					return;
				}
				if (this.type == 3458)
				{
					Lighting.AddLight(base.Center, new Vector3(0.5f, 0.4f, 0.2f) * Main.essScale);
					return;
				}
				if (this.type == 3459)
				{
					Lighting.AddLight(base.Center, new Vector3(0.2f, 0.2f, 0.5f) * Main.essScale);
					return;
				}
				if (this.type == 501)
				{
					if (Main.rand.Next(6) == 0)
					{
						int num15 = Dust.NewDust(this.position, this.width, this.height, 55, 0f, 0f, 200, this.color, 1f);
						Main.dust[num15].velocity *= 0.3f;
						Main.dust[num15].scale *= 0.5f;
						return;
					}
				}
				else
				{
					if (this.type == 3822)
					{
						Lighting.AddLight(base.Center, 0.1f, 0.3f, 0.1f);
						return;
					}
					if (this.type == 1970)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0f, 0.75f);
						return;
					}
					if (this.type == 1972)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0f, 0f, 0.75f);
						return;
					}
					if (this.type == 1971)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0.75f, 0f);
						return;
					}
					if (this.type == 1973)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0f, 0.75f, 0f);
						return;
					}
					if (this.type == 1974)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0f, 0f);
						return;
					}
					if (this.type == 1975)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0.75f, 0.75f);
						return;
					}
					if (this.type == 1976)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0.375f, 0f);
						return;
					}
					if (this.type == 2679)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.6f, 0f, 0.6f);
						return;
					}
					if (this.type == 2687)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0f, 0f, 0.6f);
						return;
					}
					if (this.type == 2689)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.6f, 0.6f, 0f);
						return;
					}
					if (this.type == 2683)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0f, 0.6f, 0f);
						return;
					}
					if (this.type == 2685)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.6f, 0f, 0f);
						return;
					}
					if (this.type == 2681)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.6f, 0.6f, 0.6f);
						return;
					}
					if (this.type == 2677)
					{
						Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.6f, 0.375f, 0f);
						return;
					}
					if (this.type == 105)
					{
						if (!this.wet)
						{
							Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f, 0.95f, 0.8f);
							return;
						}
					}
					else
					{
						if (this.type == 2701)
						{
							Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.7f, 0.65f, 0.55f);
							return;
						}
						if (this.createTile == 4)
						{
							int placeStyle = this.placeStyle;
							if ((!this.wet && ItemID.Sets.Torches[this.type]) || ItemID.Sets.WaterTorches[this.type])
							{
								Lighting.AddLight(base.Center, placeStyle);
								return;
							}
						}
						else if (this.type == 3114)
						{
							if (!this.wet)
							{
								Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f, 0f, 1f);
								return;
							}
						}
						else if (this.type == 1245)
						{
							if (!this.wet)
							{
								Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f, 0.5f, 0f);
								return;
							}
						}
						else if (this.type == 433)
						{
							if (!this.wet)
							{
								Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch), 0.3f, 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch));
								return;
							}
						}
						else
						{
							if (this.type == 523)
							{
								Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.85f, 1.2f, 0.7f);
								return;
							}
							if (this.type == 974)
							{
								if (!this.wet)
								{
									Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.75f, 0.85f, 1.4f);
									return;
								}
							}
							else
							{
								if (this.type == 1333)
								{
									Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1.25f, 1.25f, 0.7f);
									return;
								}
								if (this.type == 4383)
								{
									if (!this.wet)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1.4f, 0.85f, 0.55f);
										return;
									}
								}
								else if (this.type == 5293)
								{
									if (!this.wet)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.25f, 0.65f, 1f);
										return;
									}
								}
								else if (this.type == 5353)
								{
									if (!this.wet)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.81f, 0.72f, 1f);
										return;
									}
								}
								else
								{
									if (this.type == 4384)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.25f, 1.3f, 0.8f);
										return;
									}
									if (this.type == 3045)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), (float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f);
										return;
									}
									if (this.type == 3004)
									{
										Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.95f, 0.65f, 1.3f);
										return;
									}
									if (this.type == 2274)
									{
										float num16 = 0.75f;
										float num17 = 1.3499999f;
										float num18 = 1.5f;
										if (!this.wet)
										{
											Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), num16, num17, num18);
											return;
										}
									}
									else if (this.type >= 427 && this.type <= 432)
									{
										if (!this.wet)
										{
											float num19 = 0f;
											float num20 = 0f;
											float num21 = 0f;
											int num22 = this.type - 426;
											if (num22 == 1)
											{
												num19 = 0.1f;
												num20 = 0.2f;
												num21 = 1.1f;
											}
											if (num22 == 2)
											{
												num19 = 1f;
												num20 = 0.1f;
												num21 = 0.1f;
											}
											if (num22 == 3)
											{
												num19 = 0f;
												num20 = 1f;
												num21 = 0.1f;
											}
											if (num22 == 4)
											{
												num19 = 0.9f;
												num20 = 0f;
												num21 = 0.9f;
											}
											if (num22 == 5)
											{
												num19 = 1.3f;
												num20 = 1.3f;
												num21 = 1.3f;
											}
											if (num22 == 6)
											{
												num19 = 0.9f;
												num20 = 0.9f;
												num21 = 0f;
											}
											Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), num19, num20, num21);
											return;
										}
									}
									else
									{
										if (this.type == 2777 || this.type == 2778 || this.type == 2779 || this.type == 2780 || this.type == 2781 || this.type == 2760 || this.type == 2761 || this.type == 2762 || this.type == 3524)
										{
											Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.4f, 0.16f, 0.36f);
											return;
										}
										if (this.type == 2772 || this.type == 2773 || this.type == 2774 || this.type == 2775 || this.type == 2776 || this.type == 2757 || this.type == 2758 || this.type == 2759 || this.type == 3523)
										{
											Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0f, 0.36f, 0.4f);
											return;
										}
										if (this.type == 2782 || this.type == 2783 || this.type == 2784 || this.type == 2785 || this.type == 2786 || this.type == 2763 || this.type == 2764 || this.type == 2765 || this.type == 3522)
										{
											Lighting.AddLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.5f, 0.25f, 0.05f);
											return;
										}
										if (this.type == 3462 || this.type == 3463 || this.type == 3464 || this.type == 3465 || this.type == 3466 || this.type == 3381 || this.type == 3382 || this.type == 3383 || this.type == 3525)
										{
											Lighting.AddLight(base.Center, 0.3f, 0.3f, 0.2f);
											return;
										}
										if (this.type == 41)
										{
											if (!this.wet)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f, 0.75f, 0.55f);
												return;
											}
										}
										else if (this.type == 988)
										{
											if (!this.wet)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.35f, 0.65f, 1f);
												return;
											}
										}
										else
										{
											if (this.type == 1326)
											{
												Lighting.AddLight((int)base.Center.X / 16, (int)base.Center.Y / 16, 1f, 0.1f, 0.8f);
												return;
											}
											if (this.type == 5335)
											{
												Lighting.AddLight((int)base.Center.X / 16, (int)base.Center.Y / 16, 0.85f, 0.1f, 0.8f);
												return;
											}
											if (this.type >= 5140 && this.type <= 5146)
											{
												float num23 = 1f;
												float num24 = 1f;
												float num25 = 1f;
												switch (this.type)
												{
												case 5140:
													num23 *= 0.9f;
													num24 *= 0.8f;
													num25 *= 0.1f;
													break;
												case 5141:
													num23 *= 0.25f;
													num24 *= 0.1f;
													num25 *= 0f;
													break;
												case 5142:
													num23 *= 0f;
													num24 *= 0.25f;
													num25 *= 0f;
													break;
												case 5143:
													num23 *= 0f;
													num24 *= 0.16f;
													num25 *= 0.34f;
													break;
												case 5144:
													num23 *= 0.3f;
													num24 *= 0f;
													num25 *= 0.17f;
													break;
												case 5145:
													num23 *= 0.3f;
													num24 *= 0f;
													num25 *= 0.35f;
													break;
												case 5146:
													num23 *= (float)Main.DiscoR / 255f;
													num24 *= (float)Main.DiscoG / 255f;
													num25 *= (float)Main.DiscoB / 255f;
													break;
												}
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), num23, num24, num25);
												return;
											}
											if (this.type == 282)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.7f, 1f, 0.8f);
												return;
											}
											if (this.type == 286)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.7f, 0.8f, 1f);
												return;
											}
											if (this.type == 3112)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1f, 0.6f, 0.85f);
												return;
											}
											if (this.type == 4776)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.7f, 0f, 1f);
												return;
											}
											if (this.type == 3002)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 1.05f, 0.95f, 0.55f);
												return;
											}
											if (this.type == 5643)
											{
												float num26 = (float)Main.DiscoR / 255f;
												float num27 = (float)Main.DiscoG / 255f;
												float num28 = (float)Main.DiscoB / 255f;
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), num26, num27, num28);
												return;
											}
											if (this.type == 331)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.55f, 0.75f, 0.6f);
												return;
											}
											if (this.type == 183)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.15f, 0.45f, 0.9f);
												return;
											}
											if (this.type == 75)
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.8f, 0.7f, 0.1f);
												if (this.timeSinceItemSpawned % 12 == 0)
												{
													Dust dust = Dust.NewDustPerfect(base.Center + new Vector2(0f, (float)this.height * 0.2f) + Main.rand.NextVector2CircularEdge((float)this.width, (float)this.height * 0.6f) * (0.3f + Main.rand.NextFloat() * 0.5f), 228, new Vector2?(new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f)), 127, default(Color), 1f);
													dust.scale = 0.5f;
													dust.fadeIn = 1.1f;
													dust.noGravity = true;
													dust.noLight = true;
													return;
												}
											}
											else if (ItemID.Sets.BossBag[this.type])
											{
												Lighting.AddLight((int)((this.position.X + (float)this.width) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), 0.4f, 0.4f, 0.4f);
												if (this.timeSinceItemSpawned % 12 == 0)
												{
													Dust dust2 = Dust.NewDustPerfect(base.Center + new Vector2(0f, (float)this.height * -0.1f) + Main.rand.NextVector2CircularEdge((float)this.width * 0.6f, (float)this.height * 0.6f) * (0.3f + Main.rand.NextFloat() * 0.5f), 279, new Vector2?(new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f)), 127, default(Color), 1f);
													dust2.scale = 0.5f;
													dust2.fadeIn = 1.1f;
													dust2.noGravity = true;
													dust2.noLight = true;
													dust2.alpha = 0;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00076336 File Offset: 0x00074536
		public IEntitySource GetNPCSource_FromThis()
		{
			return new EntitySource_Parent(this);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00267779 File Offset: 0x00265979
		public IEntitySource GetItemSource_Misc(int itemSourceId)
		{
			return new EntitySource_ByItemSourceId(this, itemSourceId);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x004AC740 File Offset: 0x004AA940
		public static void ShimmerEffect(Vector2 shimmerPositon)
		{
			SoundEngine.PlaySound(SoundID.Item176, (int)shimmerPositon.X, (int)shimmerPositon.Y, 0f, 1f);
			for (int i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(shimmerPositon, 1, 1, 309, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].scale *= 1.2f;
				int num2 = Main.rand.Next(6);
				if (num2 == 0)
				{
					Main.dust[num].color = new Color(255, 255, 210);
				}
				else if (num2 == 1)
				{
					Main.dust[num].color = new Color(190, 245, 255);
				}
				else if (num2 == 2)
				{
					Main.dust[num].color = new Color(255, 150, 255);
				}
				else
				{
					Main.dust[num].color = new Color(190, 175, 255);
				}
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x004AC860 File Offset: 0x004AAA60
		public void GetShimmered()
		{
			int shimmerEquivalentType = this.inner.GetShimmerEquivalentType(false);
			int decraftingRecipeIndex = ShimmerTransforms.GetDecraftingRecipeIndex(this.inner.GetShimmerEquivalentType(true));
			int transformToItem = ShimmerTransforms.GetTransformToItem(shimmerEquivalentType);
			if (ItemID.Sets.CommonCoin[shimmerEquivalentType])
			{
				if (shimmerEquivalentType == 72)
				{
					this.stack *= 100;
				}
				else if (shimmerEquivalentType == 73)
				{
					this.stack *= 10000;
				}
				else if (shimmerEquivalentType == 74)
				{
					if (this.stack > 1)
					{
						this.stack = 1;
					}
					this.stack *= 1000000;
				}
				Main.player[Main.myPlayer].AddCoinLuck(base.Center, this.stack);
				NetMessage.SendData(146, -1, -1, null, 1, (float)((int)base.Center.X), (float)((int)base.Center.Y), (float)this.stack, 0, 0, 0);
				this.type = 0;
				this.stack = 0;
			}
			else if (transformToItem > 0)
			{
				int stack = this.stack;
				this.SetDefaults(transformToItem);
				this.stack = stack;
				this.shimmered = true;
			}
			else if (this.type == 4986)
			{
				if (NPC.unlockedSlimeRainbowSpawn)
				{
					return;
				}
				NPC.unlockedSlimeRainbowSpawn = true;
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				int num = NPC.NewNPC(this.GetNPCSource_FromThis(), (int)base.Center.X + 4, (int)base.Center.Y, 681, 0, 0f, 0f, 0f, 0f, 255);
				if (num >= 0)
				{
					NPC npc = Main.npc[num];
					npc.velocity = this.velocity;
					npc.shimmerTransparency = 1f;
				}
				WorldGen.CheckAchievement_RealEstateAndTownSlimes();
				int num2 = this.stack;
				this.stack = num2 - 1;
				if (this.stack <= 0)
				{
					this.type = 0;
				}
			}
			else if (this.type == 560)
			{
				if (Main.slimeRain)
				{
					return;
				}
				Main.StartSlimeRain(true);
				int num2 = this.stack;
				this.stack = num2 - 1;
				if (this.stack <= 0)
				{
					this.type = 0;
				}
				else
				{
					this.shimmered = true;
				}
			}
			else if (this.makeNPC > 0)
			{
				int num3 = 50;
				int maxNPCs = Main.maxNPCs;
				int num4 = NPC.GetAvailableAmountOfNPCsToSpawnUpToSlot(this.stack, maxNPCs);
				while (num3 > 0 && num4 > 0 && this.stack > 0)
				{
					num3--;
					num4--;
					int num2 = this.stack;
					this.stack = num2 - 1;
					int num5;
					if (NPCID.Sets.ShimmerTransformToNPC[(int)this.makeNPC] >= 0)
					{
						num5 = NPC.ReleaseNPC((int)base.Center.X, (int)base.Bottom.Y, NPCID.Sets.ShimmerTransformToNPC[(int)this.makeNPC], 0, Main.myPlayer);
					}
					else
					{
						num5 = NPC.ReleaseNPC((int)base.Center.X, (int)base.Bottom.Y, (int)this.makeNPC, this.placeStyle, Main.myPlayer);
					}
					if (num5 >= 0)
					{
						Main.npc[num5].shimmerTransparency = 1f;
					}
				}
				this.shimmered = true;
				if (this.stack <= 0)
				{
					this.type = 0;
				}
			}
			else if (decraftingRecipeIndex >= 0)
			{
				int num6 = this.inner.FindDecraftAmount();
				Recipe recipe = Main.recipe[decraftingRecipeIndex];
				bool flag = recipe.requiredItem[1].stack > 0;
				IEnumerable<Recipe.RequiredItemEntry> enumerable = recipe.requiredItemQuickLookup;
				if (recipe.customShimmerResults != null)
				{
					enumerable = recipe.customShimmerResults.Select((Item item) => new Recipe.RequiredItemEntry
					{
						itemIdOrRecipeGroup = item.type,
						stack = item.stack
					});
				}
				int num7 = 0;
				foreach (Recipe.RequiredItemEntry requiredItemEntry in enumerable)
				{
					if (requiredItemEntry.itemIdOrRecipeGroup <= 0)
					{
						break;
					}
					num7++;
					int i = num6 * requiredItemEntry.stack;
					int num8 = (requiredItemEntry.IsRecipeGroup ? requiredItemEntry.RecipeGroup.DecraftItemId : requiredItemEntry.itemIdOrRecipeGroup);
					if (recipe.alchemy)
					{
						for (int j = i; j > 0; j--)
						{
							if (Main.rand.Next(3) == 0)
							{
								i--;
							}
						}
					}
					while (i > 0)
					{
						int num9 = i;
						if (num9 > 9999)
						{
							num9 = 9999;
						}
						i -= num9;
						int num10 = Item.NewItem(this.GetItemSource_Misc(ItemSourceID.Shimmer), (int)this.position.X, (int)this.position.Y, this.width, this.height, num8, 1, false, 0, false);
						WorldItem worldItem = Main.item[num10];
						worldItem.stack = num9;
						worldItem.shimmerTime = 1f;
						worldItem.shimmered = true;
						worldItem.shimmerWet = true;
						worldItem.wet = true;
						worldItem.velocity *= 0.1f;
						worldItem.playerIndexTheItemIsReservedFor = Main.myPlayer;
						if (flag)
						{
							worldItem.velocity.X = 1f * (float)num7;
							WorldItem worldItem2 = worldItem;
							worldItem2.velocity.X = worldItem2.velocity.X * (1f + (float)num7 * 0.05f);
							if (num7 % 2 == 0)
							{
								WorldItem worldItem3 = worldItem;
								worldItem3.velocity.X = worldItem3.velocity.X * -1f;
							}
						}
						NetMessage.SendData(145, -1, -1, null, num10, 1f, 0f, 0f, 0, 0, 0);
					}
				}
				this.stack -= num6 * recipe.createItem.stack;
				if (this.stack <= 0)
				{
					this.stack = 0;
					this.type = 0;
				}
			}
			if (this.stack > 0)
			{
				this.shimmerTime = 1f;
			}
			else
			{
				this.shimmerTime = 0f;
			}
			this.shimmerWet = true;
			this.wet = true;
			this.velocity *= 0.1f;
			if (Main.netMode == 0)
			{
				WorldItem.ShimmerEffect(base.Center);
			}
			else
			{
				NetMessage.SendData(146, -1, -1, null, 0, (float)((int)base.Center.X), (float)((int)base.Center.Y), 0f, 0, 0, 0);
				NetMessage.SendData(145, -1, -1, null, this.whoAmI, 1f, 0f, 0f, 0, 0, 0);
			}
			AchievementsHelper.NotifyProgressionEvent(27);
			if (this.stack == 0)
			{
				this.makeNPC = -1;
				this.TurnToAir(false);
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x004ACF08 File Offset: 0x004AB108
		private static void SyncItemsInSection(int toClient, Point sectionCoordinates)
		{
			Rectangle rectangle = new Rectangle(sectionCoordinates.X * 200 * 16, sectionCoordinates.Y * 150 * 16, 3200, 2400);
			rectangle.Inflate(16, 16);
			for (int i = 0; i < 400; i++)
			{
				WorldItem worldItem = Main.item[i];
				if (worldItem.active && rectangle.Contains(worldItem.Center.ToPoint()))
				{
					NetMessage.SendData(160, toClient, -1, null, i, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x004ACFA3 File Offset: 0x004AB1A3
		public WorldItem()
		{
		}

		// Token: 0x04000FE7 RID: 4071
		public Item inner = new Item();

		// Token: 0x04000FE8 RID: 4072
		public int ownTime;

		// Token: 0x04000FE9 RID: 4073
		public int playerIndexTheItemIsReservedFor = 255;

		// Token: 0x04000FEA RID: 4074
		public int noGrabDelay;

		// Token: 0x04000FEB RID: 4075
		public bool shimmered;

		// Token: 0x04000FEC RID: 4076
		public float shimmerTime;

		// Token: 0x04000FED RID: 4077
		public bool instanced;

		// Token: 0x04000FEE RID: 4078
		public int ownIgnore = -1;

		// Token: 0x04000FEF RID: 4079
		public int timeSinceTheItemHasBeenReservedForSomeone;

		// Token: 0x04000FF0 RID: 4080
		public int timeLeftInWhichTheItemCannotBeTakenByEnemies;

		// Token: 0x04000FF1 RID: 4081
		public int timeSinceItemSpawned;

		// Token: 0x04000FF2 RID: 4082
		public bool beingGrabbed;

		// Token: 0x04000FF3 RID: 4083
		public bool onConveyor;

		// Token: 0x04000FF4 RID: 4084
		public int keepTime;

		// Token: 0x04000FF5 RID: 4085
		private static SceneMetrics _sceneMetrics = new SceneMetrics();

		// Token: 0x0200065C RID: 1628
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003D93 RID: 15763 RVA: 0x0069308D File Offset: 0x0069128D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003D94 RID: 15764 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003D95 RID: 15765 RVA: 0x0069309C File Offset: 0x0069129C
			internal Recipe.RequiredItemEntry <GetShimmered>b__110_0(Item item)
			{
				return new Recipe.RequiredItemEntry
				{
					itemIdOrRecipeGroup = item.type,
					stack = item.stack
				};
			}

			// Token: 0x04006652 RID: 26194
			public static readonly WorldItem.<>c <>9 = new WorldItem.<>c();

			// Token: 0x04006653 RID: 26195
			public static Func<Item, Recipe.RequiredItemEntry> <>9__110_0;
		}
	}
}
