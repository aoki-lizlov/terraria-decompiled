using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000280 RID: 640
	public class PortalHelper
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x0054E46C File Offset: 0x0054C66C
		static PortalHelper()
		{
			for (int i = 0; i < PortalHelper.SLOPE_EDGES.Length; i++)
			{
				PortalHelper.SLOPE_EDGES[i].Normalize();
			}
			for (int j = 0; j < PortalHelper.FoundPortals.GetLength(0); j++)
			{
				PortalHelper.FoundPortals[j, 0] = -1;
				PortalHelper.FoundPortals[j, 1] = -1;
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0054E608 File Offset: 0x0054C808
		public static void UpdatePortalPoints()
		{
			PortalHelper.anyPortalAtAll = false;
			for (int i = 0; i < PortalHelper.FoundPortals.GetLength(0); i++)
			{
				PortalHelper.FoundPortals[i, 0] = -1;
				PortalHelper.FoundPortals[i, 1] = -1;
			}
			for (int j = 0; j < PortalHelper.PortalCooldownForPlayers.Length; j++)
			{
				if (PortalHelper.PortalCooldownForPlayers[j] > 0)
				{
					PortalHelper.PortalCooldownForPlayers[j]--;
				}
			}
			for (int k = 0; k < PortalHelper.PortalCooldownForNPCs.Length; k++)
			{
				if (PortalHelper.PortalCooldownForNPCs[k] > 0)
				{
					PortalHelper.PortalCooldownForNPCs[k]--;
				}
			}
			for (int l = 0; l < 1000; l++)
			{
				Projectile projectile = Main.projectile[l];
				if (projectile.active && projectile.type == 602 && projectile.ai[1] >= 0f && projectile.ai[1] <= 1f && projectile.owner >= 0 && projectile.owner <= 255)
				{
					PortalHelper.FoundPortals[projectile.owner, (int)projectile.ai[1]] = l;
					if (PortalHelper.FoundPortals[projectile.owner, 0] != -1 && PortalHelper.FoundPortals[projectile.owner, 1] != -1)
					{
						PortalHelper.anyPortalAtAll = true;
					}
				}
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0054E763 File Offset: 0x0054C963
		public static void ResetNPCSlotData(int npcIndex)
		{
			PortalHelper.PortalCooldownForNPCs[npcIndex] = 0;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0054E770 File Offset: 0x0054C970
		public static void TryGoingThroughPortals(Entity ent)
		{
			if (!PortalHelper.anyPortalAtAll)
			{
				return;
			}
			float num = 0f;
			Vector2 velocity = ent.velocity;
			int width = ent.width;
			int height = ent.height;
			int num2 = 1;
			if (ent is Player)
			{
				num2 = (int)((Player)ent).gravDir;
			}
			for (int i = 0; i < PortalHelper.FoundPortals.GetLength(0); i++)
			{
				if (PortalHelper.FoundPortals[i, 0] != -1 && PortalHelper.FoundPortals[i, 1] != -1 && (!(ent is Player) || (i < PortalHelper.PortalCooldownForPlayers.Length && PortalHelper.PortalCooldownForPlayers[i] <= 0)) && (!(ent is NPC) || (i < PortalHelper.PortalCooldownForNPCs.Length && PortalHelper.PortalCooldownForNPCs[i] <= 0)))
				{
					for (int j = 0; j < 2; j++)
					{
						Projectile projectile = Main.projectile[PortalHelper.FoundPortals[i, j]];
						Vector2 vector;
						Vector2 vector2;
						PortalHelper.GetPortalEdges(projectile.Center, projectile.ai[0], out vector, out vector2);
						if (Collision.CheckAABBvLineCollision(ent.position + ent.velocity, ent.Size, vector, vector2, 2f, ref num))
						{
							Projectile projectile2 = Main.projectile[PortalHelper.FoundPortals[i, 1 - j]];
							float num3 = ent.Hitbox.Distance(projectile.Center);
							int num4;
							int num5;
							Vector2 vector3 = PortalHelper.GetPortalOutingPoint(ent.Size, projectile2.Center, projectile2.ai[0], out num4, out num5) + Vector2.Normalize(new Vector2((float)num4, (float)num5)) * num3;
							Vector2 vector4 = Vector2.UnitX * 16f;
							if (!(Collision.TileCollision(vector3 - vector4, vector4, width, height, true, true, num2, false, false, true) != vector4))
							{
								vector4 = -Vector2.UnitX * 16f;
								if (!(Collision.TileCollision(vector3 - vector4, vector4, width, height, true, true, num2, false, false, true) != vector4))
								{
									vector4 = Vector2.UnitY * 16f;
									if (!(Collision.TileCollision(vector3 - vector4, vector4, width, height, true, true, num2, false, false, true) != vector4))
									{
										vector4 = -Vector2.UnitY * 16f;
										if (!(Collision.TileCollision(vector3 - vector4, vector4, width, height, true, true, num2, false, false, true) != vector4))
										{
											float num6 = 0.1f;
											if (num5 == -num2)
											{
												num6 = 0.1f;
											}
											if (ent.velocity == Vector2.Zero)
											{
												ent.velocity = (projectile.ai[0] - 1.5707964f).ToRotationVector2() * num6;
											}
											if (ent.velocity.Length() < num6)
											{
												ent.velocity.Normalize();
												ent.velocity *= num6;
											}
											Vector2 vector5 = Vector2.Normalize(new Vector2((float)num4, (float)num5));
											if (vector5.HasNaNs() || vector5 == Vector2.Zero)
											{
												vector5 = Vector2.UnitX * (float)ent.direction;
											}
											ent.velocity = vector5 * ent.velocity.Length();
											if ((num5 == -num2 && Math.Sign(ent.velocity.Y) != -num2) || Math.Abs(ent.velocity.Y) < 0.1f)
											{
												ent.velocity.Y = (float)(-(float)num2) * 0.1f;
											}
											int num7 = (int)((float)(projectile2.owner * 2) + projectile2.ai[1]);
											int num8 = num7 + ((num7 % 2 == 0) ? 1 : (-1));
											if (ent is Player)
											{
												Player player = (Player)ent;
												player.lastPortalColorIndex = num8;
												player.Teleport(vector3, 4, num7);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(96, -1, -1, null, player.whoAmI, vector3.X, vector3.Y, (float)num7, 0, 0, 0);
													NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
												}
												PortalHelper.PortalCooldownForPlayers[i] = 10;
												return;
											}
											if (ent is NPC)
											{
												NPC npc = (NPC)ent;
												npc.lastPortalColorIndex = num8;
												npc.Teleport(vector3, 4, num7);
												if (Main.netMode == 2)
												{
													NetMessage.SendData(100, -1, -1, null, npc.whoAmI, vector3.X, vector3.Y, (float)num7, 0, 0, 0);
													NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
												}
												PortalHelper.PortalCooldownForPlayers[i] = 10;
												if (num5 == -1 && ent.velocity.Y > -3f)
												{
													ent.velocity.Y = -3f;
												}
											}
											return;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0054EC68 File Offset: 0x0054CE68
		public static int TryPlacingPortal(Projectile theBolt, Vector2 velocity, Vector2 theCrashVelocity)
		{
			Vector2 vector = velocity / velocity.Length();
			Point point = PortalHelper.FindCollision(theBolt.position, theBolt.position + velocity + vector * 32f).ToTileCoordinates();
			Tile tile = Main.tile[point.X, point.Y];
			Vector2 vector2 = new Vector2((float)(point.X * 16 + 8), (float)(point.Y * 16 + 8));
			if (!WorldGen.SolidOrSlopedTile(tile))
			{
				return -1;
			}
			int num = (int)tile.slope();
			bool flag = tile.halfBrick();
			for (int i = 0; i < (flag ? 2 : PortalHelper.EDGES.Length); i++)
			{
				Point point2;
				if (Vector2.Dot(PortalHelper.EDGES[i], vector) > 0f && PortalHelper.FindValidLine(point, (int)PortalHelper.EDGES[i].Y, (int)(-(int)PortalHelper.EDGES[i].X), out point2))
				{
					vector2 = new Vector2((float)(point2.X * 16 + 8), (float)(point2.Y * 16 + 8));
					return PortalHelper.AddPortal(theBolt, vector2 - PortalHelper.EDGES[i] * (flag ? 0f : 8f), (float)Math.Atan2((double)PortalHelper.EDGES[i].Y, (double)PortalHelper.EDGES[i].X) + 1.5707964f, (int)theBolt.ai[0], theBolt.direction);
				}
			}
			if (num != 0)
			{
				Vector2 vector3 = PortalHelper.SLOPE_EDGES[num - 1];
				Point point3;
				if (Vector2.Dot(vector3, -vector) > 0f && PortalHelper.FindValidLine(point, -PortalHelper.SLOPE_OFFSETS[num - 1].Y, PortalHelper.SLOPE_OFFSETS[num - 1].X, out point3))
				{
					vector2 = new Vector2((float)(point3.X * 16 + 8), (float)(point3.Y * 16 + 8));
					return PortalHelper.AddPortal(theBolt, vector2, (float)Math.Atan2((double)vector3.Y, (double)vector3.X) - 1.5707964f, (int)theBolt.ai[0], theBolt.direction);
				}
			}
			return -1;
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0054EEB4 File Offset: 0x0054D0B4
		private static bool FindValidLine(Point position, int xOffset, int yOffset, out Point bestPosition)
		{
			bestPosition = position;
			if (PortalHelper.IsValidLine(position, xOffset, yOffset))
			{
				return true;
			}
			Point point = new Point(position.X - xOffset, position.Y - yOffset);
			if (PortalHelper.IsValidLine(point, xOffset, yOffset))
			{
				bestPosition = point;
				return true;
			}
			Point point2 = new Point(position.X + xOffset, position.Y + yOffset);
			if (PortalHelper.IsValidLine(point2, xOffset, yOffset))
			{
				bestPosition = point2;
				return true;
			}
			return false;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0054EF2C File Offset: 0x0054D12C
		private static bool IsValidLine(Point position, int xOffset, int yOffset)
		{
			Tile tile = Main.tile[position.X, position.Y];
			Tile tile2 = Main.tile[position.X - xOffset, position.Y - yOffset];
			Tile tile3 = Main.tile[position.X + xOffset, position.Y + yOffset];
			return !PortalHelper.BlockPortals(Main.tile[position.X + yOffset, position.Y - xOffset]) && !PortalHelper.BlockPortals(Main.tile[position.X + yOffset - xOffset, position.Y - xOffset - yOffset]) && !PortalHelper.BlockPortals(Main.tile[position.X + yOffset + xOffset, position.Y - xOffset + yOffset]) && (PortalHelper.CanPlacePortalOn(tile) && PortalHelper.CanPlacePortalOn(tile2) && PortalHelper.CanPlacePortalOn(tile3) && tile2.HasSameSlope(tile) && tile3.HasSameSlope(tile));
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0054F020 File Offset: 0x0054D220
		private static bool CanPlacePortalOn(Tile t)
		{
			return PortalHelper.DoesTileTypeSupportPortals(t.type) && WorldGen.SolidOrSlopedTile(t);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0054F037 File Offset: 0x0054D237
		private static bool DoesTileTypeSupportPortals(ushort tileType)
		{
			return tileType != 496;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0054F044 File Offset: 0x0054D244
		private static bool BlockPortals(Tile t)
		{
			return t.active() && !Main.tileCut[(int)t.type] && !TileID.Sets.BreakableWhenPlacing[(int)t.type] && Main.tileSolid[(int)t.type];
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0054F07C File Offset: 0x0054D27C
		private static Vector2 FindCollision(Vector2 startPosition, Vector2 stopPosition)
		{
			int lastX = 0;
			int lastY = 0;
			Utils.PlotLine(startPosition.ToTileCoordinates(), stopPosition.ToTileCoordinates(), delegate(int x, int y)
			{
				lastX = x;
				lastY = y;
				return !WorldGen.SolidOrSlopedTile(x, y);
			}, false);
			return new Vector2((float)lastX * 16f, (float)lastY * 16f);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0054F0DC File Offset: 0x0054D2DC
		private static int AddPortal(Projectile sourceProjectile, Vector2 position, float angle, int form, int direction)
		{
			if (!PortalHelper.SupportedTilesAreFine(position, angle))
			{
				return -1;
			}
			PortalHelper.RemoveMyOldPortal(form);
			PortalHelper.RemoveIntersectingPortals(position, angle);
			int num = Projectile.NewProjectile(Projectile.InheritSource(sourceProjectile), position.X, position.Y, 0f, 0f, 602, 0, 0f, Main.myPlayer, angle, (float)form, 0f, null);
			Main.projectile[num].direction = direction;
			Main.projectile[num].netUpdate = true;
			return num;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0054F158 File Offset: 0x0054D358
		private static void RemoveMyOldPortal(int form)
		{
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.type == 602 && projectile.owner == Main.myPlayer && projectile.ai[1] == (float)form)
				{
					projectile.Kill();
					return;
				}
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x0054F1B4 File Offset: 0x0054D3B4
		private static void RemoveIntersectingPortals(Vector2 position, float angle)
		{
			Vector2 vector;
			Vector2 vector2;
			PortalHelper.GetPortalEdges(position, angle, out vector, out vector2);
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.type == 602)
				{
					Vector2 vector3;
					Vector2 vector4;
					PortalHelper.GetPortalEdges(projectile.Center, projectile.ai[0], out vector3, out vector4);
					if (Collision.CheckLinevLine(vector, vector2, vector3, vector4).Length != 0)
					{
						if (projectile.owner != Main.myPlayer && Main.netMode != 2)
						{
							NetMessage.SendData(95, -1, -1, null, projectile.owner, (float)((int)projectile.ai[1]), 0f, 0f, 0, 0, 0);
						}
						projectile.Kill();
					}
				}
			}
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x0054F272 File Offset: 0x0054D472
		public static Color GetPortalColor(int colorIndex)
		{
			return PortalHelper.GetPortalColor(colorIndex / 2, colorIndex % 2);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0054F280 File Offset: 0x0054D480
		public static Color GetPortalColor(int player, int portal)
		{
			Color color = Color.White;
			if (Main.netMode == 0)
			{
				if (portal == 0)
				{
					color = Main.hslToRgb(0.12f, 1f, 0.5f, byte.MaxValue);
				}
				else
				{
					color = Main.hslToRgb(0.52f, 1f, 0.6f, byte.MaxValue);
				}
			}
			else
			{
				float num = 0.08f;
				color = Main.hslToRgb((0.5f + (float)player * (num * 2f) + (float)portal * num) % 1f, 1f, 0.5f, byte.MaxValue);
			}
			color.A = 66;
			return color;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0054F318 File Offset: 0x0054D518
		private static void GetPortalEdges(Vector2 position, float angle, out Vector2 start, out Vector2 end)
		{
			Vector2 vector = angle.ToRotationVector2();
			start = position + vector * -22f;
			end = position + vector * 22f;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0054F35C File Offset: 0x0054D55C
		private static Vector2 GetPortalOutingPoint(Vector2 objectSize, Vector2 portalPosition, float portalAngle, out int bonusX, out int bonusY)
		{
			int num = (int)Math.Round((double)(MathHelper.WrapAngle(portalAngle) / 0.7853982f));
			if (num == 2 || num == -2)
			{
				bonusX = ((num == 2) ? (-1) : 1);
				bonusY = 0;
				return portalPosition + new Vector2((num == 2) ? (-objectSize.X) : 0f, -objectSize.Y / 2f);
			}
			if (num == 0 || num == 4)
			{
				bonusX = 0;
				bonusY = ((num == 0) ? 1 : (-1));
				return portalPosition + new Vector2(-objectSize.X / 2f, (num == 0) ? 0f : (-objectSize.Y));
			}
			if (num == -3 || num == 3)
			{
				bonusX = ((num == -3) ? 1 : (-1));
				bonusY = -1;
				return portalPosition + new Vector2((num == -3) ? 0f : (-objectSize.X), -objectSize.Y);
			}
			if (num == 1 || num == -1)
			{
				bonusX = ((num == -1) ? 1 : (-1));
				bonusY = 1;
				return portalPosition + new Vector2((num == -1) ? 0f : (-objectSize.X), 0f);
			}
			bonusX = 0;
			bonusY = 0;
			return portalPosition;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0054F47C File Offset: 0x0054D67C
		public static void SyncPortalsOnPlayerJoin(int plr, int fluff, List<Point> dontInclude, out List<Point> portalSections)
		{
			portalSections = new List<Point>();
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && (projectile.type == 602 || projectile.type == 601))
				{
					Vector2 center = projectile.Center;
					int sectionX = Netplay.GetSectionX((int)(center.X / 16f));
					int sectionY = Netplay.GetSectionY((int)(center.Y / 16f));
					for (int j = sectionX - fluff; j < sectionX + fluff + 1; j++)
					{
						for (int k = sectionY - fluff; k < sectionY + fluff + 1; k++)
						{
							if (j >= 0 && j < Main.maxSectionsX && k >= 0 && k < Main.maxSectionsY && !Netplay.Clients[plr].TileSections[j, k] && !dontInclude.Contains(new Point(j, k)))
							{
								portalSections.Add(new Point(j, k));
							}
						}
					}
				}
			}
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0054F584 File Offset: 0x0054D784
		public static void SyncPortalSections(Vector2 portalPosition, int fluff)
		{
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					RemoteClient.CheckSection(i, portalPosition, fluff);
				}
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0054F5B8 File Offset: 0x0054D7B8
		public static bool SupportedTilesAreFine(Vector2 portalCenter, float portalAngle)
		{
			Point point = portalCenter.ToTileCoordinates();
			int num = (int)Math.Round((double)(MathHelper.WrapAngle(portalAngle) / 0.7853982f));
			int num2;
			int num3;
			if (num == 2 || num == -2)
			{
				num2 = ((num == 2) ? (-1) : 1);
				num3 = 0;
			}
			else if (num == 0 || num == 4)
			{
				num2 = 0;
				num3 = ((num == 0) ? 1 : (-1));
			}
			else if (num == -3 || num == 3)
			{
				num2 = ((num == -3) ? 1 : (-1));
				num3 = -1;
			}
			else
			{
				if (num != 1 && num != -1)
				{
					Main.NewText(string.Concat(new object[] { "Broken portal! (over4s = ", num, " , ", portalAngle, ")" }), byte.MaxValue, byte.MaxValue, byte.MaxValue);
					return false;
				}
				num2 = ((num == -1) ? 1 : (-1));
				num3 = 1;
			}
			if (num2 != 0 && num3 != 0)
			{
				int num4 = 3;
				if (num2 == -1 && num3 == 1)
				{
					num4 = 5;
				}
				if (num2 == 1 && num3 == -1)
				{
					num4 = 2;
				}
				if (num2 == 1 && num3 == 1)
				{
					num4 = 4;
				}
				num4--;
				return PortalHelper.SupportedSlope(point.X, point.Y, num4) && PortalHelper.SupportedSlope(point.X + num2, point.Y - num3, num4) && PortalHelper.SupportedSlope(point.X - num2, point.Y + num3, num4);
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					point.X--;
				}
				return PortalHelper.SupportedNormal(point.X, point.Y) && PortalHelper.SupportedNormal(point.X, point.Y - 1) && PortalHelper.SupportedNormal(point.X, point.Y + 1);
			}
			if (num3 != 0)
			{
				if (num3 == 1)
				{
					point.Y--;
				}
				return (PortalHelper.SupportedNormal(point.X, point.Y) && PortalHelper.SupportedNormal(point.X + 1, point.Y) && PortalHelper.SupportedNormal(point.X - 1, point.Y)) || (PortalHelper.SupportedHalfbrick(point.X, point.Y) && PortalHelper.SupportedHalfbrick(point.X + 1, point.Y) && PortalHelper.SupportedHalfbrick(point.X - 1, point.Y));
			}
			return true;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0054F7E0 File Offset: 0x0054D9E0
		private static bool SupportedSlope(int x, int y, int slope)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)tile.type] && !TileID.Sets.BreakableWhenPlacing[(int)tile.type] && Main.tileSolid[(int)tile.type] && (int)tile.slope() == slope && PortalHelper.DoesTileTypeSupportPortals(tile.type);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0054F848 File Offset: 0x0054DA48
		private static bool SupportedHalfbrick(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)tile.type] && !TileID.Sets.BreakableWhenPlacing[(int)tile.type] && Main.tileSolid[(int)tile.type] && tile.halfBrick() && PortalHelper.DoesTileTypeSupportPortals(tile.type);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0054F8AC File Offset: 0x0054DAAC
		private static bool SupportedNormal(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile != null && tile.nactive() && !Main.tileCut[(int)tile.type] && !TileID.Sets.BreakableWhenPlacing[(int)tile.type] && Main.tileSolid[(int)tile.type] && !TileID.Sets.NotReallySolid[(int)tile.type] && !tile.halfBrick() && tile.slope() == 0 && PortalHelper.DoesTileTypeSupportPortals(tile.type);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0000357B File Offset: 0x0000177B
		public PortalHelper()
		{
		}

		// Token: 0x04004F42 RID: 20290
		public const int PORTALS_PER_PERSON = 2;

		// Token: 0x04004F43 RID: 20291
		private static int[,] FoundPortals = new int[256, 2];

		// Token: 0x04004F44 RID: 20292
		private static int[] PortalCooldownForPlayers = new int[256];

		// Token: 0x04004F45 RID: 20293
		private static int[] PortalCooldownForNPCs = new int[Main.maxNPCs];

		// Token: 0x04004F46 RID: 20294
		private static readonly Vector2[] EDGES = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(0f, -1f),
			new Vector2(1f, 0f),
			new Vector2(-1f, 0f)
		};

		// Token: 0x04004F47 RID: 20295
		private static readonly Vector2[] SLOPE_EDGES = new Vector2[]
		{
			new Vector2(1f, -1f),
			new Vector2(-1f, -1f),
			new Vector2(1f, 1f),
			new Vector2(-1f, 1f)
		};

		// Token: 0x04004F48 RID: 20296
		private static readonly Point[] SLOPE_OFFSETS = new Point[]
		{
			new Point(1, -1),
			new Point(-1, -1),
			new Point(1, 1),
			new Point(-1, 1)
		};

		// Token: 0x04004F49 RID: 20297
		private static bool anyPortalAtAll = false;

		// Token: 0x02000806 RID: 2054
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x060042D3 RID: 17107 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x060042D4 RID: 17108 RVA: 0x006BFF57 File Offset: 0x006BE157
			internal bool <FindCollision>b__0(int x, int y)
			{
				this.lastX = x;
				this.lastY = y;
				return !WorldGen.SolidOrSlopedTile(x, y);
			}

			// Token: 0x040071BF RID: 29119
			public int lastX;

			// Token: 0x040071C0 RID: 29120
			public int lastY;
		}
	}
}
