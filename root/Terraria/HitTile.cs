using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000027 RID: 39
	public class HitTile
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00015AA8 File Offset: 0x00013CA8
		public static void ClearAllTilesAtThisLocation(int x, int y)
		{
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					Main.player[i].hitTile.ClearThisTile(x, y);
				}
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00015AE8 File Offset: 0x00013CE8
		public void ClearThisTile(int x, int y)
		{
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				HitTile.HitTileObject hitTileObject = this.data[num];
				if (hitTileObject.X == x && hitTileObject.Y == y)
				{
					this.Clear(i);
					this.Prune();
				}
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00015B38 File Offset: 0x00013D38
		public HitTile()
		{
			HitTile.rand = new UnifiedRandom();
			this.data = new HitTile.HitTileObject[501];
			this.order = new int[501];
			for (int i = 0; i <= 500; i++)
			{
				this.data[i] = new HitTile.HitTileObject();
				this.order[i] = i;
			}
			this.bufferLocation = 0;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00015BA4 File Offset: 0x00013DA4
		public int TryFinding(int x, int y, int hitType)
		{
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				HitTile.HitTileObject hitTileObject = this.data[num];
				if (hitTileObject.type == hitType)
				{
					if (hitTileObject.X == x && hitTileObject.Y == y)
					{
						return num;
					}
				}
				else if (i != 0 && hitTileObject.type == 0)
				{
					break;
				}
			}
			return -1;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00015BFC File Offset: 0x00013DFC
		public void TryClearingAndPruning(int x, int y, int hitType)
		{
			int num = this.TryFinding(x, y, hitType);
			if (num != -1)
			{
				this.Clear(num);
				this.Prune();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00015C24 File Offset: 0x00013E24
		public int HitObject(int x, int y, int hitType)
		{
			HitTile.HitTileObject hitTileObject;
			for (int i = 0; i <= 500; i++)
			{
				int num = this.order[i];
				hitTileObject = this.data[num];
				if (hitTileObject.type == hitType)
				{
					if (hitTileObject.X == x && hitTileObject.Y == y)
					{
						return num;
					}
				}
				else if (i != 0 && hitTileObject.type == 0)
				{
					break;
				}
			}
			hitTileObject = this.data[this.bufferLocation];
			hitTileObject.X = x;
			hitTileObject.Y = y;
			hitTileObject.type = hitType;
			return this.bufferLocation;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00015CA4 File Offset: 0x00013EA4
		public void UpdatePosition(int tileId, int x, int y)
		{
			if (tileId < 0 || tileId > 500)
			{
				return;
			}
			HitTile.HitTileObject hitTileObject = this.data[tileId];
			hitTileObject.X = x;
			hitTileObject.Y = y;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public int AddDamage(int tileId, int damageAmount, bool updateAmount = true)
		{
			if (tileId < 0 || tileId > 500)
			{
				return 0;
			}
			if (tileId == this.bufferLocation && damageAmount == 0)
			{
				return 0;
			}
			HitTile.HitTileObject hitTileObject = this.data[tileId];
			if (!updateAmount)
			{
				return hitTileObject.damage + damageAmount;
			}
			hitTileObject.damage += damageAmount;
			hitTileObject.timeToLive = 60;
			hitTileObject.animationTimeElapsed = 0;
			hitTileObject.animationDirection = (Main.rand.NextFloat() * 6.2831855f).ToRotationVector2() * 2f;
			this.SortSlots(tileId);
			return hitTileObject.damage;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00015D58 File Offset: 0x00013F58
		private void SortSlots(int tileId)
		{
			if (tileId == this.bufferLocation)
			{
				this.bufferLocation = this.order[500];
				if (tileId != this.bufferLocation)
				{
					this.data[this.bufferLocation].Clear();
				}
				for (int i = 500; i > 0; i--)
				{
					this.order[i] = this.order[i - 1];
				}
				this.order[0] = this.bufferLocation;
				return;
			}
			for (int i = 0; i <= 500; i++)
			{
				if (this.order[i] == tileId)
				{
					IL_00AE:
					while (i > 1)
					{
						int num = this.order[i - 1];
						this.order[i - 1] = this.order[i];
						this.order[i] = num;
						i--;
					}
					this.order[1] = tileId;
					return;
				}
			}
			goto IL_00AE;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00015E20 File Offset: 0x00014020
		public void Clear(int tileId)
		{
			if (tileId < 0 || tileId > 500)
			{
				return;
			}
			this.data[tileId].Clear();
			for (int i = 0; i < 500; i++)
			{
				if (this.order[i] == tileId)
				{
					IL_004D:
					while (i < 500)
					{
						this.order[i] = this.order[i + 1];
						i++;
					}
					this.order[500] = tileId;
					return;
				}
			}
			goto IL_004D;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00015E90 File Offset: 0x00014090
		public void Prune()
		{
			bool flag = false;
			for (int i = 0; i <= 500; i++)
			{
				HitTile.HitTileObject hitTileObject = this.data[i];
				if (hitTileObject.type != 0)
				{
					Tile tile = Main.tile[hitTileObject.X, hitTileObject.Y];
					if (hitTileObject.timeToLive <= 1)
					{
						hitTileObject.Clear();
						flag = true;
					}
					else
					{
						hitTileObject.timeToLive--;
						if ((double)hitTileObject.timeToLive < 12.0)
						{
							hitTileObject.damage -= 10;
						}
						else if ((double)hitTileObject.timeToLive < 24.0)
						{
							hitTileObject.damage -= 7;
						}
						else if ((double)hitTileObject.timeToLive < 36.0)
						{
							hitTileObject.damage -= 5;
						}
						else if ((double)hitTileObject.timeToLive < 48.0)
						{
							hitTileObject.damage -= 2;
						}
						if (hitTileObject.damage < 0)
						{
							hitTileObject.Clear();
							flag = true;
						}
						else if (hitTileObject.type == 1)
						{
							if (!tile.active())
							{
								hitTileObject.Clear();
								flag = true;
							}
						}
						else if (tile.wall == 0)
						{
							hitTileObject.Clear();
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				return;
			}
			int num = 1;
			while (flag)
			{
				flag = false;
				for (int j = num; j < 500; j++)
				{
					if (this.data[this.order[j]].type == 0 && this.data[this.order[j + 1]].type != 0)
					{
						int num2 = this.order[j];
						this.order[j] = this.order[j + 1];
						this.order[j + 1] = num2;
						flag = true;
					}
				}
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001604C File Offset: 0x0001424C
		public void DrawFreshAnimations(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				this.data[i].animationTimeElapsed++;
			}
			if (!Main.SettingsEnabled_MinersWobble)
			{
				return;
			}
			int num = 1;
			Vector2 vector = new Vector2((float)Main.offScreenRange);
			if (Main.drawToScreen)
			{
				vector = Vector2.Zero;
			}
			vector = Vector2.Zero;
			bool flag = Main.ShouldShowInvisibleBlocksAndWalls();
			for (int j = 0; j < this.data.Length; j++)
			{
				if (this.data[j].type == num)
				{
					int damage = this.data[j].damage;
					if (damage >= 20)
					{
						int x = this.data[j].X;
						int y = this.data[j].Y;
						if (WorldGen.InWorld(x, y, 0))
						{
							Tile tile = Main.tile[x, y];
							bool flag2 = tile != null;
							if (flag2 && num == 1)
							{
								flag2 = flag2 && tile.active() && Main.tileSolid[(int)Main.tile[x, y].type] && (!tile.invisibleBlock() || flag);
							}
							if (flag2 && num == 2)
							{
								flag2 = flag2 && tile.wall != 0 && (!tile.invisibleWall() || flag);
							}
							if (flag2)
							{
								bool flag3 = false;
								bool flag4 = false;
								if (tile.type == 10)
								{
									flag3 = false;
								}
								else if (Main.tileSolid[(int)tile.type] && !Main.tileSolidTop[(int)tile.type])
								{
									flag3 = true;
								}
								else if (WorldGen.IsTreeType((int)tile.type))
								{
									flag4 = true;
									int num2 = (int)(tile.frameX / 22);
									int num3 = (int)(tile.frameY / 22);
									if (num3 < 9)
									{
										flag3 = ((num2 != 1 && num2 != 2) || num3 < 6 || num3 > 8) && (num2 != 3 || num3 > 2) && (num2 != 4 || num3 < 3 || num3 > 5) && (num2 != 5 || num3 < 6 || num3 > 8);
									}
								}
								else if (tile.type == 72)
								{
									flag4 = true;
									if (tile.frameX <= 34)
									{
										flag3 = true;
									}
								}
								if (flag3 && tile.slope() == 0 && !tile.halfBrick())
								{
									int num4 = 0;
									if (damage >= 80)
									{
										num4 = 3;
									}
									else if (damage >= 60)
									{
										num4 = 2;
									}
									else if (damage >= 40)
									{
										num4 = 1;
									}
									else if (damage >= 20)
									{
										num4 = 0;
									}
									Rectangle rectangle = new Rectangle(this.data[j].crackStyle * 18, num4 * 18, 16, 16);
									rectangle.Inflate(-2, -2);
									if (flag4)
									{
										rectangle.X = (4 + this.data[j].crackStyle / 2) * 18;
									}
									int animationTimeElapsed = this.data[j].animationTimeElapsed;
									if ((float)animationTimeElapsed < 10f)
									{
										float num5 = (float)animationTimeElapsed / 10f;
										Color color = Lighting.GetColor(x, y);
										float num6 = 0f;
										Vector2 zero = Vector2.Zero;
										float num7 = 0.5f;
										float num8 = num5 % num7;
										num8 *= 1f / num7;
										if ((int)(num5 / num7) % 2 == 1)
										{
											num8 = 1f - num8;
										}
										Tile tileSafely = Framing.GetTileSafely(x, y);
										Tile tile2 = tileSafely;
										Texture2D texture2D = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady((int)tileSafely.type, 0, (int)tileSafely.color());
										if (texture2D != null)
										{
											Vector2 vector2 = new Vector2(8f);
											Vector2 vector3 = new Vector2(1f);
											float num9 = num8 * 0.2f + 1f;
											float num10 = 1f - num8;
											num10 = 1f;
											color *= num10 * num10 * 0.8f;
											Vector2 vector4 = num9 * vector3;
											Vector2 vector5 = (new Vector2((float)(x * 16 - (int)Main.screenPosition.X), (float)(y * 16 - (int)Main.screenPosition.Y)) + vector + vector2 + zero).Floor();
											spriteBatch.Draw(texture2D, vector5, new Rectangle?(new Rectangle((int)tile2.frameX, (int)tile2.frameY, 16, 16)), color, num6, vector2, vector4, SpriteEffects.None, 0f);
											color.A = 180;
											spriteBatch.Draw(TextureAssets.TileCrack.Value, vector5, new Rectangle?(rectangle), color, num6, vector2, vector4, SpriteEffects.None, 0f);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000164C7 File Offset: 0x000146C7
		// Note: this type is marked as 'beforefieldinit'.
		static HitTile()
		{
		}

		// Token: 0x0400013D RID: 317
		internal const int UNUSED = 0;

		// Token: 0x0400013E RID: 318
		internal const int TILE = 1;

		// Token: 0x0400013F RID: 319
		internal const int WALL = 2;

		// Token: 0x04000140 RID: 320
		internal const int MAX_HITTILES = 500;

		// Token: 0x04000141 RID: 321
		internal const int TIMETOLIVE = 60;

		// Token: 0x04000142 RID: 322
		private static UnifiedRandom rand;

		// Token: 0x04000143 RID: 323
		private static int lastCrack = -1;

		// Token: 0x04000144 RID: 324
		public HitTile.HitTileObject[] data;

		// Token: 0x04000145 RID: 325
		private int[] order;

		// Token: 0x04000146 RID: 326
		private int bufferLocation;

		// Token: 0x020005F2 RID: 1522
		public class HitTileObject
		{
			// Token: 0x06003B60 RID: 15200 RVA: 0x0065ADC4 File Offset: 0x00658FC4
			public HitTileObject()
			{
				this.Clear();
			}

			// Token: 0x06003B61 RID: 15201 RVA: 0x0065ADD4 File Offset: 0x00658FD4
			public void Clear()
			{
				this.X = 0;
				this.Y = 0;
				this.damage = 0;
				this.type = 0;
				this.timeToLive = 0;
				if (HitTile.rand == null)
				{
					HitTile.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
				}
				this.crackStyle = HitTile.rand.Next(4);
				while (this.crackStyle == HitTile.lastCrack)
				{
					this.crackStyle = HitTile.rand.Next(4);
				}
				HitTile.lastCrack = this.crackStyle;
			}

			// Token: 0x04006376 RID: 25462
			public int X;

			// Token: 0x04006377 RID: 25463
			public int Y;

			// Token: 0x04006378 RID: 25464
			public int damage;

			// Token: 0x04006379 RID: 25465
			public int type;

			// Token: 0x0400637A RID: 25466
			public int timeToLive;

			// Token: 0x0400637B RID: 25467
			public int crackStyle;

			// Token: 0x0400637C RID: 25468
			public int animationTimeElapsed;

			// Token: 0x0400637D RID: 25469
			public Vector2 animationDirection;
		}
	}
}
