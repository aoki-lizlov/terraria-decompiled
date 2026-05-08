using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D7 RID: 727
	public class TileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x0055CA78 File Offset: 0x0055AC78
		public void ClearSelfAndPrepareForCheck()
		{
			Main.SmartInteractX = -1;
			Main.SmartInteractY = -1;
			Main.TileInteractionLX = -1;
			Main.TileInteractionHX = -1;
			Main.TileInteractionLY = -1;
			Main.TileInteractionHY = -1;
			Main.SmartInteractTileCoords.Clear();
			Main.SmartInteractTileCoordsSelected.Clear();
			this.targets.Clear();
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0055CAC8 File Offset: 0x0055ACC8
		public bool ProvideCandidate(SmartInteractScanSettings settings, out ISmartInteractCandidate candidate)
		{
			candidate = null;
			Point point = settings.mousevec.ToTileCoordinates();
			this.FillPotentialTargetTiles(settings);
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			if (this.targets.Count > 0)
			{
				float num5 = -1f;
				Tuple<int, int> tuple = this.targets[0];
				for (int i = 0; i < this.targets.Count; i++)
				{
					float num6 = Vector2.Distance(new Vector2((float)this.targets[i].Item1, (float)this.targets[i].Item2) * 16f + Vector2.One * 8f, settings.mousevec);
					if (num5 == -1f || num6 <= num5)
					{
						num5 = num6;
						tuple = this.targets[i];
					}
				}
				if (Collision.InTileBounds(tuple.Item1, tuple.Item2, settings.LX, settings.LY, settings.HX, settings.HY))
				{
					num = tuple.Item1;
					num2 = tuple.Item2;
				}
			}
			bool flag = false;
			int j = 0;
			while (j < this.targets.Count)
			{
				int item = this.targets[j].Item1;
				int item2 = this.targets[j].Item2;
				Tile tile = Main.tile[item, item2];
				int num7 = 0;
				int num8 = 0;
				int num9 = 18;
				int num10 = 18;
				int num11 = 2;
				ushort type = tile.type;
				if (type <= 338)
				{
					if (type <= 172)
					{
						if (type <= 97)
						{
							if (type <= 55)
							{
								switch (type)
								{
								case 10:
									num7 = 1;
									num8 = 3;
									num11 = 0;
									break;
								case 11:
									goto IL_0634;
								case 12:
								case 13:
								case 19:
								case 20:
								case 22:
								case 23:
								case 24:
								case 25:
									break;
								case 14:
								case 17:
								case 26:
									goto IL_065D;
								case 15:
									goto IL_05EC;
								case 16:
								case 18:
									goto IL_0613;
								case 21:
									goto IL_061E;
								default:
									if (type == 29)
									{
										goto IL_0613;
									}
									if (type == 55)
									{
										goto IL_061E;
									}
									break;
								}
							}
							else
							{
								if (type == 77)
								{
									goto IL_065D;
								}
								if (type == 79)
								{
									goto IL_0629;
								}
								switch (type)
								{
								case 85:
								case 94:
								case 96:
								case 97:
									goto IL_061E;
								case 86:
								case 89:
									goto IL_065D;
								case 88:
									num7 = 3;
									num8 = 1;
									num11 = 0;
									break;
								}
							}
						}
						else if (type <= 125)
						{
							switch (type)
							{
							case 101:
							case 102:
								goto IL_0696;
							case 103:
							case 105:
								break;
							case 104:
								num7 = 2;
								num8 = 5;
								break;
							case 106:
								goto IL_0665;
							default:
								if (type == 114)
								{
									goto IL_065D;
								}
								if (type == 125)
								{
									goto IL_061E;
								}
								break;
							}
						}
						else
						{
							switch (type)
							{
							case 132:
								goto IL_061E;
							case 133:
								goto IL_065D;
							case 134:
								goto IL_0613;
							case 135:
							case 137:
							case 138:
								break;
							case 136:
								goto IL_05D3;
							case 139:
								goto IL_0629;
							default:
								if (type == 144)
								{
									goto IL_05D3;
								}
								if (type == 172)
								{
									goto IL_061E;
								}
								break;
							}
						}
					}
					else
					{
						if (type <= 243)
						{
							if (type <= 220)
							{
								if (type == 207)
								{
									num7 = 2;
									num8 = 4;
									num11 = 0;
									goto IL_06AC;
								}
								if (type == 209)
								{
									num7 = 4;
									num8 = 3;
									num11 = 0;
									goto IL_06AC;
								}
								switch (type)
								{
								case 212:
									num7 = 4;
									num8 = 3;
									goto IL_06AC;
								case 213:
								case 214:
								case 215:
								case 219:
									goto IL_06AC;
								case 216:
									break;
								case 217:
								case 218:
									goto IL_065D;
								case 220:
									goto IL_0665;
								default:
									goto IL_06AC;
								}
							}
							else
							{
								if (type == 228)
								{
									goto IL_0665;
								}
								if (type == 237)
								{
									goto IL_065D;
								}
								if (type != 243)
								{
									goto IL_06AC;
								}
								goto IL_0665;
							}
						}
						else if (type <= 287)
						{
							if (type == 247 || type == 283)
							{
								goto IL_0665;
							}
							if (type != 287)
							{
								goto IL_06AC;
							}
							goto IL_061E;
						}
						else
						{
							if (type - 300 <= 8)
							{
								goto IL_0665;
							}
							if (type == 335)
							{
								goto IL_061E;
							}
							if (type != 338)
							{
								goto IL_06AC;
							}
						}
						num7 = 1;
						num8 = 2;
					}
				}
				else
				{
					if (type <= 494)
					{
						if (type <= 441)
						{
							if (type <= 389)
							{
								switch (type)
								{
								case 354:
									break;
								case 355:
									goto IL_0665;
								case 356:
									goto IL_0634;
								default:
									if (type == 377)
									{
										goto IL_065D;
									}
									switch (type)
									{
									case 386:
										goto IL_061E;
									case 387:
										goto IL_0613;
									case 388:
									case 389:
										num7 = 1;
										num8 = 5;
										goto IL_06AC;
									default:
										goto IL_06AC;
									}
									break;
								}
							}
							else
							{
								switch (type)
								{
								case 410:
									goto IL_0634;
								case 411:
									goto IL_061E;
								case 412:
									goto IL_0665;
								default:
									if (type != 425 && type != 441)
									{
										goto IL_06AC;
									}
									goto IL_061E;
								}
							}
						}
						else if (type <= 480)
						{
							if (type != 455)
							{
								switch (type)
								{
								case 463:
								case 475:
									goto IL_0696;
								case 464:
									num7 = 5;
									num8 = 4;
									goto IL_06AC;
								case 465:
								case 466:
								case 471:
								case 472:
								case 473:
								case 474:
									goto IL_06AC;
								case 467:
								case 468:
									goto IL_061E;
								case 469:
									goto IL_065D;
								case 470:
									goto IL_0634;
								default:
									if (type != 480)
									{
										goto IL_06AC;
									}
									goto IL_0634;
								}
							}
						}
						else
						{
							if (type == 487)
							{
								num7 = 4;
								num8 = 2;
								num11 = 0;
								goto IL_06AC;
							}
							if (type != 491)
							{
								if (type != 494)
								{
									goto IL_06AC;
								}
								goto IL_05D3;
							}
						}
					}
					else if (type <= 597)
					{
						if (type <= 509)
						{
							if (type == 497)
							{
								goto IL_05EC;
							}
							if (type == 499)
							{
								goto IL_0665;
							}
							if (type != 509)
							{
								goto IL_06AC;
							}
							goto IL_0634;
						}
						else
						{
							if (type - 510 <= 1)
							{
								goto IL_0629;
							}
							if (type == 573)
							{
								goto IL_061E;
							}
							if (type != 597)
							{
								goto IL_06AC;
							}
							goto IL_0696;
						}
					}
					else if (type <= 663)
					{
						if (type - 621 <= 1)
						{
							goto IL_061E;
						}
						if (type - 657 > 1 && type != 663)
						{
							goto IL_06AC;
						}
						goto IL_0634;
					}
					else if (type <= 721)
					{
						if (type == 699)
						{
							num7 = 4;
							num8 = 4;
							goto IL_06AC;
						}
						if (type - 720 > 1)
						{
							goto IL_06AC;
						}
						goto IL_0634;
					}
					else
					{
						if (type == 725)
						{
							goto IL_0634;
						}
						if (type != 733)
						{
							goto IL_06AC;
						}
					}
					num7 = 3;
					num8 = 3;
					num11 = 0;
				}
				IL_06AC:
				if (TileID.Sets.Campfires[(int)tile.type])
				{
					num7 = 3;
					num8 = 2;
				}
				if (num7 != 0 && num8 != 0)
				{
					int num12 = item - (int)tile.frameX % (num9 * num7) / num9;
					int num13 = item2 - (int)tile.frameY % (num10 * num8 + num11) / num10;
					bool flag2 = Collision.InTileBounds(num, num2, num12, num13, num12 + num7 - 1, num13 + num8 - 1);
					bool flag3 = Collision.InTileBounds(point.X, point.Y, num12, num13, num12 + num7 - 1, num13 + num8 - 1);
					if (flag3)
					{
						num3 = point.X;
						num4 = point.Y;
					}
					if (!settings.FullInteraction)
					{
						flag2 = flag2 && flag3;
					}
					if (flag)
					{
						flag2 = false;
					}
					for (int k = num12; k < num12 + num7; k++)
					{
						for (int l = num13; l < num13 + num8; l++)
						{
							Point point2 = new Point(k, l);
							if (!Main.SmartInteractTileCoords.Contains(point2))
							{
								if (flag2)
								{
									Main.SmartInteractTileCoordsSelected.Add(point2);
								}
								if (flag2 || settings.FullInteraction)
								{
									Main.SmartInteractTileCoords.Add(point2);
								}
							}
						}
					}
					if (!flag && flag2)
					{
						flag = true;
					}
				}
				j++;
				continue;
				IL_05D3:
				num7 = 1;
				num8 = 1;
				num11 = 0;
				goto IL_06AC;
				IL_05EC:
				num7 = 1;
				num8 = 2;
				num11 = 4;
				goto IL_06AC;
				IL_0613:
				num7 = 2;
				num8 = 1;
				goto IL_06AC;
				IL_061E:
				num7 = 2;
				num8 = 2;
				goto IL_06AC;
				IL_0629:
				num7 = 2;
				num8 = 2;
				num11 = 0;
				goto IL_06AC;
				IL_0634:
				num7 = 2;
				num8 = 3;
				num11 = 0;
				goto IL_06AC;
				IL_065D:
				num7 = 3;
				num8 = 2;
				goto IL_06AC;
				IL_0665:
				num7 = 3;
				num8 = 3;
				goto IL_06AC;
				IL_0696:
				num7 = 3;
				num8 = 4;
				goto IL_06AC;
			}
			if (settings.DemandOnlyZeroDistanceTargets)
			{
				if (num3 != -1 && num4 != -1)
				{
					this._candidate.Reuse(true, 0f, num3, num4, settings.LX - 10, settings.LY - 10, settings.HX + 10, settings.HY + 10);
					candidate = this._candidate;
					return true;
				}
				return false;
			}
			else
			{
				if (num != -1 && num2 != -1)
				{
					float num14 = new Rectangle(num * 16, num2 * 16, 16, 16).ClosestPointInRect(settings.mousevec).Distance(settings.mousevec);
					this._candidate.Reuse(false, num14, num, num2, settings.LX - 10, settings.LY - 10, settings.HX + 10, settings.HY + 10);
					candidate = this._candidate;
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0055D390 File Offset: 0x0055B590
		private void FillPotentialTargetTiles(SmartInteractScanSettings settings)
		{
			for (int i = settings.LX; i <= settings.HX; i++)
			{
				for (int j = settings.LY; j <= settings.HY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active())
					{
						if (TileID.Sets.Campfires[(int)tile.type])
						{
							this.targets.Add(new Tuple<int, int>(i, j));
						}
						else if (Recipe.TileUsedInRecipes[(int)tile.type])
						{
							this.targets.Add(new Tuple<int, int>(i, j));
						}
						else if (TileID.Sets.CountsAsWaterForCrafting[(int)tile.type])
						{
							this.targets.Add(new Tuple<int, int>(i, j));
						}
						else
						{
							ushort type = tile.type;
							if (type <= 338)
							{
								if (type <= 125)
								{
									if (type <= 79)
									{
										if (type <= 21)
										{
											if (type - 10 > 1)
											{
												if (type == 15)
												{
													goto IL_039E;
												}
												if (type != 21)
												{
													goto IL_046A;
												}
											}
										}
										else if (type != 29 && type != 55 && type != 79)
										{
											goto IL_046A;
										}
									}
									else if (type <= 97)
									{
										if (type != 85 && type - 88 > 1 && type != 97)
										{
											goto IL_046A;
										}
									}
									else if (type != 102 && type != 104 && type != 125)
									{
										goto IL_046A;
									}
								}
								else if (type <= 209)
								{
									if (type <= 139)
									{
										if (type != 132 && type != 136 && type != 139)
										{
											goto IL_046A;
										}
									}
									else if (type != 144 && type != 207 && type != 209)
									{
										goto IL_046A;
									}
								}
								else if (type <= 237)
								{
									if (type != 212)
									{
										if (type != 216)
										{
											if (type != 237)
											{
												goto IL_046A;
											}
											if (settings.player.HasItem(1293))
											{
												this.targets.Add(new Tuple<int, int>(i, j));
												goto IL_046A;
											}
											goto IL_046A;
										}
									}
									else
									{
										if (settings.player.HasItem(949))
										{
											this.targets.Add(new Tuple<int, int>(i, j));
											goto IL_046A;
										}
										goto IL_046A;
									}
								}
								else if (type != 287 && type != 335 && type != 338)
								{
									goto IL_046A;
								}
							}
							else if (type <= 487)
							{
								if (type <= 425)
								{
									if (type <= 377)
									{
										if (type != 354)
										{
											if (type != 356)
											{
												if (type != 377)
												{
													goto IL_046A;
												}
											}
											else
											{
												if (!Main.fastForwardTimeToDawn && (Main.netMode == 1 || Main.sundialCooldown == 0))
												{
													this.targets.Add(new Tuple<int, int>(i, j));
													goto IL_046A;
												}
												goto IL_046A;
											}
										}
									}
									else if (type - 386 > 3 && type - 410 > 1 && type != 425)
									{
										goto IL_046A;
									}
								}
								else if (type <= 470)
								{
									if (type != 441 && type != 455)
									{
										switch (type)
										{
										case 463:
										case 464:
										case 467:
										case 468:
										case 470:
											break;
										case 465:
										case 466:
										case 469:
											goto IL_046A;
										default:
											goto IL_046A;
										}
									}
								}
								else if (type != 475 && type != 480 && type != 487)
								{
									goto IL_046A;
								}
							}
							else if (type <= 597)
							{
								if (type <= 497)
								{
									if (type != 491 && type != 494)
									{
										if (type != 497)
										{
											goto IL_046A;
										}
										goto IL_039E;
									}
								}
								else if (type - 509 > 2 && type != 573 && type != 597)
								{
									goto IL_046A;
								}
							}
							else if (type <= 663)
							{
								if (type != 621 && type - 657 > 1)
								{
									if (type != 663)
									{
										goto IL_046A;
									}
									if (!Main.fastForwardTimeToDusk && (Main.netMode == 1 || Main.moondialCooldown == 0))
									{
										this.targets.Add(new Tuple<int, int>(i, j));
										goto IL_046A;
									}
									goto IL_046A;
								}
							}
							else if (type <= 721)
							{
								if (type != 699 && type - 720 > 1)
								{
									goto IL_046A;
								}
							}
							else if (type != 725 && type != 733)
							{
								goto IL_046A;
							}
							this.targets.Add(new Tuple<int, int>(i, j));
							goto IL_046A;
							IL_039E:
							if (settings.player.IsWithinSnappngRangeToTile(i, j, 40))
							{
								this.targets.Add(new Tuple<int, int>(i, j));
							}
						}
					}
					IL_046A:;
				}
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0055D827 File Offset: 0x0055BA27
		public TileSmartInteractCandidateProvider()
		{
		}

		// Token: 0x04005062 RID: 20578
		private List<Tuple<int, int>> targets = new List<Tuple<int, int>>();

		// Token: 0x04005063 RID: 20579
		private TileSmartInteractCandidateProvider.ReusableCandidate _candidate = new TileSmartInteractCandidateProvider.ReusableCandidate();

		// Token: 0x02000821 RID: 2081
		private class ReusableCandidate : ISmartInteractCandidate
		{
			// Token: 0x06004303 RID: 17155 RVA: 0x006C099E File Offset: 0x006BEB9E
			public void Reuse(bool strictSettings, float distanceFromCursor, int AimedX, int AimedY, int LX, int LY, int HX, int HY)
			{
				this.DistanceFromCursor = distanceFromCursor;
				this._strictSettings = strictSettings;
				this._aimedX = AimedX;
				this._aimedY = AimedY;
				this._lx = LX;
				this._ly = LY;
				this._hx = HX;
				this._hy = HY;
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06004304 RID: 17156 RVA: 0x006C09DD File Offset: 0x006BEBDD
			// (set) Token: 0x06004305 RID: 17157 RVA: 0x006C09E5 File Offset: 0x006BEBE5
			public float DistanceFromCursor
			{
				[CompilerGenerated]
				get
				{
					return this.<DistanceFromCursor>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DistanceFromCursor>k__BackingField = value;
				}
			}

			// Token: 0x06004306 RID: 17158 RVA: 0x006C09F0 File Offset: 0x006BEBF0
			public void WinCandidacy()
			{
				Main.SmartInteractX = this._aimedX;
				Main.SmartInteractY = this._aimedY;
				if (this._strictSettings)
				{
					Main.SmartInteractShowingFake = Main.SmartInteractTileCoords.Count > 0;
				}
				else
				{
					Main.SmartInteractShowingGenuine = true;
				}
				Main.TileInteractionLX = this._lx - 10;
				Main.TileInteractionLY = this._ly - 10;
				Main.TileInteractionHX = this._hx + 10;
				Main.TileInteractionHY = this._hy + 10;
			}

			// Token: 0x06004307 RID: 17159 RVA: 0x0000357B File Offset: 0x0000177B
			public ReusableCandidate()
			{
			}

			// Token: 0x04007249 RID: 29257
			private bool _strictSettings;

			// Token: 0x0400724A RID: 29258
			private int _aimedX;

			// Token: 0x0400724B RID: 29259
			private int _aimedY;

			// Token: 0x0400724C RID: 29260
			private int _hx;

			// Token: 0x0400724D RID: 29261
			private int _hy;

			// Token: 0x0400724E RID: 29262
			private int _lx;

			// Token: 0x0400724F RID: 29263
			private int _ly;

			// Token: 0x04007250 RID: 29264
			[CompilerGenerated]
			private float <DistanceFromCursor>k__BackingField;
		}
	}
}
