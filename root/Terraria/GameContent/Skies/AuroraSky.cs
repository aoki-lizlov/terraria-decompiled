using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using Terraria.GameInput;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
	// Token: 0x02000448 RID: 1096
	public class AuroraSky : CustomSky
	{
		// Token: 0x060031D2 RID: 12754 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OnLoad()
		{
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x005E3258 File Offset: 0x005E1458
		public override void Update(GameTime gameTime)
		{
			if (FocusHelper.PauseSkies)
			{
				return;
			}
			if (this._isLeaving)
			{
				this._opacity -= (float)gameTime.ElapsedGameTime.TotalSeconds * 0.5f;
				if (this._opacity < 0f)
				{
					this._isActive = false;
					this._opacity = 0f;
					return;
				}
			}
			else
			{
				this._opacity += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
				if (this._opacity > 1f)
				{
					this._opacity = 1f;
				}
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x005E32F1 File Offset: 0x005E14F1
		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth != 3.4028235E+38f)
			{
				return;
			}
			AuroraSky.DrawAuroraSky(this.vertexStrip, this._opacity, ref this._lastSkyColor);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x005E3314 File Offset: 0x005E1514
		private static void DrawAuroraSky(VertexStrip vertexStrip, float skyOpacity, ref Color lastSkyColor)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["Aurora"];
			float num = (Main.dayTime ? 54000f : 32400f);
			float num2 = (float)Main.time;
			skyOpacity *= Utils.Remap(num2, 0f, 180f, 0f, 1f, true) * Utils.Remap(num2, num - 180f, num, 1f, 0f, true);
			if (skyOpacity <= 0.01f || Main.dayTime)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			int num3 = 1;
			float num4 = 1f;
			float num5 = 1f;
			bool flag5 = false;
			float num6 = 1f;
			switch (Main.GetMoonPhase())
			{
			case MoonPhase.Full:
				flag = true;
				num3 = 3;
				break;
			case MoonPhase.ThreeQuartersAtLeft:
				num3 = 2;
				flag5 = true;
				break;
			case MoonPhase.HalfAtLeft:
				flag2 = true;
				flag3 = true;
				num3 = 3;
				flag4 = true;
				num5 *= 0.5f;
				break;
			case MoonPhase.QuarterAtLeft:
				return;
			case MoonPhase.Empty:
				flag2 = true;
				num3 = 3;
				break;
			case MoonPhase.QuarterAtRight:
				num3 = 2;
				flag5 = true;
				num6 = 0.5f;
				break;
			case MoonPhase.HalfAtRight:
				return;
			case MoonPhase.ThreeQuartersAtRight:
				flag2 = true;
				flag3 = true;
				num3 = 3;
				flag4 = true;
				num5 *= 0.5f;
				num6 = 0.5f;
				break;
			}
			PlayerInput.SetZoom_Background();
			Main.spriteBatch.End();
			Vector2 vector = new Vector2(1920f, 1080f);
			float num7 = (float)Main.ScreenSize.X / vector.X;
			miscShaderData.UseSpriteTransformMatrix(new Matrix?(Main.LatestSurfaceBackgroundBeginner.transformMatrix * Matrix.CreateScale(num7)));
			Vector2 lastCelestialBodyPosition = Main.LastCelestialBodyPosition;
			lastCelestialBodyPosition.Y *= vector.X / vector.Y / ((float)Main.ScreenSize.X / (float)Main.ScreenSize.Y);
			float num8 = Main.GlobalTimeWrappedHourly / 60f;
			for (int i = 0; i < num3; i++)
			{
				vertexStrip.Reset(0);
				int num9 = 140;
				float num10 = 2.5f;
				float num11 = 0f;
				float num12 = 1f;
				Vector4 vector2 = new Vector4(0f, 0f, 0f, 0f);
				if (i == 0)
				{
					vector2.Y = 0f;
				}
				if (i == 1)
				{
					vector2.Y = 0.7f;
				}
				if (i == 2)
				{
					vector2.Y = 0.8f;
				}
				if (flag4)
				{
					num12 = 1f;
					vector2.X = 0.3f;
				}
				if (flag2)
				{
					num10 = 1f;
					num11 += 0.33f;
					if (i != 0)
					{
						vector2.Y = 0.4f + (float)i * 0.2f;
					}
					if (!flag3)
					{
						vector2.Z = 0.2f;
					}
				}
				if (flag && i != 0)
				{
					vector2.Y = 0.4f;
				}
				if (flag5 && i == 0)
				{
					vector2.Y = 0.3f;
				}
				if (flag5 && i == 1)
				{
					vector2.Y = 0.5f;
				}
				if (flag && i == 0)
				{
					vector2.Y = 0.5f;
				}
				if (flag2 && i == 0)
				{
					vector2.Y = 0.7f;
				}
				miscShaderData.UseShaderSpecificData(vector2);
				for (int j = num9; j >= 0; j--)
				{
					float num13 = (float)j / (float)num9;
					float num14 = num13;
					if (flag5 && i == 1)
					{
						num13 = Utils.Remap(num13, 0f, 1f, 50f / (float)num9, 90f / (float)num9, true);
					}
					float num15 = num13;
					if (!flag)
					{
						num15 = 1f - num13;
					}
					float num16 = MathHelper.Lerp(0.4f, 0.1f, num13);
					float num17 = 0.4f + num8;
					float num18 = 3f;
					float num19 = 0.5f + (float)Math.Cos((double)num13 * 3.141592653589793 * (double)num18 + (double)num17) * 0.4f * MathHelper.Lerp(1f, 0.3f, num15);
					float num20 = Utils.Remap(Math.Abs((float)Math.Sin((double)num13 * 3.141592653589793 * (double)num18 + (double)num17)), 0f, 0.98f, 0f, 1f, true);
					float num21 = MathHelper.Lerp(0.2f, 0.05f, num15) * num4;
					float num22 = 0.5f - 0.5f * (float)Math.Cos((double)(num13 * 6.2831855f));
					float num23 = num8;
					if (flag5)
					{
						float num24 = num8 * 0.16f;
						if (i == 1)
						{
							Utils.Remap(num13, 0f, 1f, 50f / (float)num9, 90f / (float)num9, true);
						}
						num16 += (1f - num13) * 0.05f;
						num21 += 0.05f;
						if (i == 1)
						{
							num16 = 0.5f + (float)Math.Cos((double)(num24 * 6.2831855f * 0.15f + num13 * 60f)) * 0.03f;
							float num25 = num13 + num24;
							num19 = 0.5f + (float)Math.Cos((double)num25 * 3.141592653589793 * 2.0) * 1.4f * MathHelper.Lerp(1f, 0.3f, num13);
							num19 += (float)Math.Sin((double)(num24 * 6.2831855f)) * MathHelper.Lerp(0.4f, 0.13f, num13);
							num16 -= (float)Math.Cos((double)(num24 * 6.2831855f * 3f + num13 * 5f)) * 0.06f;
							num21 += 0.15f;
							num19 = num14 * 1.1f;
							num20 = 1f - (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f)) * 0.35f - 0.35f;
							num16 = (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f)) * 0.0125f + 0.55f;
							num21 = 0.16f * num4 + 0.05f + (float)Math.Sin((double)(num14 * 6.2831855f * 2f)) * 0.025f;
							num21 += 0.2f;
						}
						if (i == 0)
						{
							float num26 = Utils.Remap(num13, 0f, 0.3f, 0f, 1f, true);
							num22 *= num26 * num26 * num26;
							num21 -= 0.1f;
							num21 += 0.8f * num13 * num13;
						}
					}
					if (flag && i == 0)
					{
						float num27 = num8 * 0.16f;
						num16 = 0.5f + (float)Math.Cos((double)(num27 * 6.2831855f * 0.15f + num13 * 60f)) * 0.03f;
						float num28 = num13 + num27;
						num19 = 0.5f + (float)Math.Cos((double)num28 * 3.141592653589793 * 2.0) * 1.4f * MathHelper.Lerp(1f, 0.3f, num13);
						num19 += (float)Math.Sin((double)(num27 * 6.2831855f)) * MathHelper.Lerp(0.4f, 0.13f, num13);
						num21 += (float)(Math.Sin((double)(num27 * 6.2831855f)) + 1.0) * MathHelper.Lerp(0.24f, 0.15f, num13) * num4;
						num16 -= (float)Math.Cos((double)(num27 * 6.2831855f * 3f + num13 * 5f)) * 0.06f;
						num19 = num14 * 1.1f;
						num16 = (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f + num8 * 2f + 3.1415927f)) * 0.025f + 0.55f;
						num21 = 0.16f * num4 + 0.05f + (float)Math.Sin((double)(num14 * 6.2831855f * 2f + num8 * 2f)) * 0.02f;
						num20 = 1f - (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f)) * 0.35f - 0.35f;
					}
					if (flag2)
					{
						float num29 = num8 * 0.16f;
						if (i == 0)
						{
							num16 = 0.5f + (float)Math.Cos((double)(num29 * 6.2831855f * 0.15f + num13 * 60f)) * 0.03f;
							float num30 = num13 + num29;
							num19 = 0.5f + (float)Math.Cos((double)num30 * 3.141592653589793 * 2.0) * 1.4f * MathHelper.Lerp(1f, 0.3f, num13);
							num19 += (float)Math.Sin((double)(num29 * 6.2831855f)) * MathHelper.Lerp(0.4f, 0.13f, num13);
							num16 -= (float)Math.Cos((double)(num29 * 6.2831855f * 3f + num13 * 5f)) * 0.06f;
							num21 += 0.15f;
							num19 = num14 * 1.1f;
							num20 = 1f - (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f)) * 0.35f - 0.35f;
							num16 = (float)Math.Sin((double)(num14 * 6.2831855f * 2f + 1.5707964f)) * 0.025f + 0.55f;
							num21 = 0.16f * num4 + 0.05f + (float)Math.Sin((double)(num14 * 6.2831855f * 2f)) * 0.05f;
						}
						else if (i == 1 || i == 2)
						{
							num16 = MathHelper.Lerp(0.3f, 0.3f, num13);
							Math.Sin((double)(num8 * 6.2831855f));
							float num31 = (float)Math.Cos((double)(num8 * 6.2831855f));
							if (i == 1)
							{
								num21 += 0.5f * num13;
							}
							num16 -= (float)Math.Cos((double)(num13 * 6.2831855f + num8 * 2f)) * 0.07f;
							num20 = Utils.Remap(Math.Abs(num31), 0f, 0.98f, 0f, 1f, true);
							num20 = 1f;
							num19 = num13;
							num23 += 0.35f;
							if (!flag3)
							{
								num23 -= 0.35f;
							}
							num22 *= 0.55f;
							if (i == 2)
							{
								Math.Sin((double)(num8 * 6.2831855f));
								Math.Cos((double)(num8 * 6.2831855f));
								num16 -= (float)Math.Cos((double)(num8 * 6.2831855f * 0.35f + num13 * 13.73f)) * 0.04f * (1f - num13) + 0.04f;
								num16 -= 0.03f;
							}
						}
						else if (i == 1)
						{
							num16 = MathHelper.Lerp(0.4f, 0.1f, num13);
							Math.Sin((double)(num8 * 6.2831855f));
							float num32 = (float)Math.Cos((double)(num8 * 6.2831855f));
							num16 -= (float)Math.Cos((double)(num13 * 6.2831855f + num8 * 2f)) * 0.07f;
							num20 = Utils.Remap(Math.Abs(num32), 0f, 0.98f, 0f, 1f, true);
							num20 = 1f;
							num19 = num13;
							num23 += 0.35f;
							num22 *= 0.55f;
						}
						else if (i == 2)
						{
							num16 = MathHelper.Lerp(0.1f, 0.4f, num13);
							Math.Sin((double)(num8 * 6.2831855f));
							float num33 = (float)Math.Cos((double)(num8 * 6.2831855f));
							num16 -= (float)Math.Cos((double)(num8 * 6.2831855f * 0.35f)) * 0.15f * (1f - num13);
							num23 += 0.35f;
							num20 = Utils.Remap(Math.Abs(num33), 0f, 0.98f, 0f, 1f, true);
							num20 = 1f;
							num19 = num13;
						}
					}
					if (flag3)
					{
						num23 = num8 + (float)i * 0.05f;
						num10 = 0.5f;
						num11 = 0.02f;
					}
					if (flag2 && !flag3)
					{
						num12 = 1f;
						num11 = 0.45f;
					}
					if (flag && i != 0)
					{
						num22 = Math.Max(num22 * 2f, num13);
						if (num22 > 1f)
						{
							num22 = 1f;
						}
						num19 = MathHelper.Lerp(num19, lastCelestialBodyPosition.X, num13);
						num16 += 0.05f;
						num16 = MathHelper.Lerp(num16, lastCelestialBodyPosition.Y + 0.025f, num13);
						num22 *= 0.5f;
					}
					Vector2 vector3 = vector * new Vector2(num19, num16);
					Vector2 vector4 = vector * new Vector2(num19, num16 - num21);
					if (!flag)
					{
						float num34 = Main.GlobalTimeWrappedHourly * 0.1f;
						vector3 += ((num34 + 0.3f) * 6.2831855f).ToRotationVector2() * 2f;
						vector4 += ((num34 * 0.8f + 0.67f) * 6.2831855f).ToRotationVector2() * 2f;
						vector4.Y += (float)Math.Sin((double)((num34 + num13) * 6.2831855f * 3f)) * 15f - 15f;
						vector3.Y += (float)Math.Sin((double)((num34 + num13) * 6.2831855f * 0.5f)) * 1f;
						vector4.Y += (float)Math.Sin((double)((num34 + num13) * 6.2831855f * 0.5f)) * 1f;
						vector3.X += (float)Math.Sin((double)((num34 + num13) * 6.2831855f * 1f)) * 3f;
						vector4.X += (float)Math.Sin((double)((num34 + num13) * 6.2831855f * 0.75f)) * 3f;
					}
					Color color = Main.hslToRgb((float)((double)num23 + Math.Cos((double)(num13 * 6.2831855f * num10)) * 0.1) % 1f, num6, 0.5f, byte.MaxValue);
					Color color2 = Main.hslToRgb((float)((double)num23 + Math.Cos((double)(num13 * 6.2831855f * num10)) * 0.1 + (double)num11) % 1f, num6, num12, byte.MaxValue);
					if (i == 0 && j == 19)
					{
						lastSkyColor = color;
					}
					float num35 = num20 * skyOpacity * num22 * num5;
					if (flag)
					{
						float num36 = (vector * new Vector2(num19, num16 - num21 * 0.25f)).Distance(vector * lastCelestialBodyPosition);
						num35 *= Utils.Remap(num36, 29f, 60f, 0f, 1f, true);
						float num37 = 505f;
						float num38 = 1f - num13;
						num38 *= num38 * num38;
						if (i == 1)
						{
							vector3.X -= num37 * num38;
							vector4.X -= num37 * num38;
							num35 -= num13 * num13 * 0.36f;
						}
						if (i == 2)
						{
							vector3.X += num37 * num38;
							vector4.X += num37 * num38;
							num35 -= num13 * num13 * 0.36f;
						}
					}
					vertexStrip.AddVertexPair(vector3, vector4, num13, color * num35, color2 * num35);
				}
				miscShaderData.Apply(null);
				vertexStrip.PrepareIndices(true);
				vertexStrip.DrawTrail();
			}
			Main.LatestSurfaceBackgroundBeginner.Begin(Main.spriteBatch);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x005E42B4 File Offset: 0x005E24B4
		public static void ModifyTileColor(ref Color tileColor, float intensity)
		{
			AuroraSky auroraSky = SkyManager.Instance["Aurora"] as AuroraSky;
			if (auroraSky == null)
			{
				return;
			}
			float opacity = auroraSky._opacity;
			if (opacity <= 0f)
			{
				return;
			}
			MoonPhase moonPhase = Main.GetMoonPhase();
			if (moonPhase == MoonPhase.QuarterAtLeft)
			{
				return;
			}
			Color lastSkyColor = auroraSky._lastSkyColor;
			lastSkyColor.A = byte.MaxValue;
			tileColor = Color.Lerp(tileColor, lastSkyColor, opacity * intensity);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x005E431D File Offset: 0x005E251D
		public override void Activate(Vector2 position, params object[] args)
		{
			this._isActive = true;
			this._isLeaving = false;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x005E432D File Offset: 0x005E252D
		public override void Deactivate(params object[] args)
		{
			this._isLeaving = true;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x005E4336 File Offset: 0x005E2536
		public override void Reset()
		{
			this._opacity = 0f;
			this._isActive = false;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x005E434A File Offset: 0x005E254A
		public override bool IsActive()
		{
			return this._isActive;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x005E4352 File Offset: 0x005E2552
		public AuroraSky()
		{
		}

		// Token: 0x040057C1 RID: 22465
		private UnifiedRandom _random = new UnifiedRandom();

		// Token: 0x040057C2 RID: 22466
		private bool _isActive;

		// Token: 0x040057C3 RID: 22467
		private bool _isLeaving;

		// Token: 0x040057C4 RID: 22468
		private float _opacity;

		// Token: 0x040057C5 RID: 22469
		private VertexStrip vertexStrip = new VertexStrip();

		// Token: 0x040057C6 RID: 22470
		private Color _lastSkyColor;

		// Token: 0x02000949 RID: 2377
		// (Invoke) Token: 0x0600485E RID: 18526
		private delegate void ScriptMethodSignature(VertexStrip vertexStrip, float skyOpacity, ref Color lastSkyColor);
	}
}
