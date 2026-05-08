using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001CC RID: 460
	public struct FinalFractalHelper
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x0051BC2C File Offset: 0x00519E2C
		public static int GetRandomProfileIndex()
		{
			List<int> list = FinalFractalHelper._fractalProfiles.Keys.ToList<int>();
			int num = Main.rand.Next(list.Count);
			if (list[num] == 4956)
			{
				list.RemoveAt(num);
				num = Main.rand.Next(list.Count);
			}
			return list[num];
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0051BC88 File Offset: 0x00519E88
		public void Draw(Projectile proj)
		{
			FinalFractalHelper.FinalFractalProfile finalFractalProfile = FinalFractalHelper.GetFinalFractalProfile((int)proj.ai[1]);
			MiscShaderData miscShaderData = GameShaders.Misc["FinalFractal"];
			int num = 4;
			int num2 = 0;
			int num3 = 0;
			int num4 = 4;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num, (float)num2, (float)num3, (float)num4));
			miscShaderData.UseImage0("Images/Extra_" + 201);
			miscShaderData.UseImage1("Images/Extra_" + 193);
			miscShaderData.Apply(null);
			FinalFractalHelper._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, finalFractalProfile.colorMethod, finalFractalProfile.widthMethod, -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			FinalFractalHelper._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0051BD8C File Offset: 0x00519F8C
		public static FinalFractalHelper.FinalFractalProfile GetFinalFractalProfile(int usedSwordId)
		{
			FinalFractalHelper.FinalFractalProfile defaultProfile;
			if (!FinalFractalHelper._fractalProfiles.TryGetValue(usedSwordId, out defaultProfile))
			{
				defaultProfile = FinalFractalHelper._defaultProfile;
			}
			return defaultProfile;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0051BDB0 File Offset: 0x00519FB0
		private Color StripColors(float progressOnStrip)
		{
			Color color = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			color.A /= 2;
			return color;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0051BE0B File Offset: 0x0051A00B
		private float StripWidth(float progressOnStrip)
		{
			return 50f;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0051BE14 File Offset: 0x0051A014
		// Note: this type is marked as 'beforefieldinit'.
		static FinalFractalHelper()
		{
		}

		// Token: 0x04004A11 RID: 18961
		public const int TotalIllusions = 4;

		// Token: 0x04004A12 RID: 18962
		public const int FramesPerImportantTrail = 15;

		// Token: 0x04004A13 RID: 18963
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004A14 RID: 18964
		private static Dictionary<int, FinalFractalHelper.FinalFractalProfile> _fractalProfiles = new Dictionary<int, FinalFractalHelper.FinalFractalProfile>
		{
			{
				65,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(236, 62, 192))
			},
			{
				1123,
				new FinalFractalHelper.FinalFractalProfile(48f, Main.OurFavoriteColor)
			},
			{
				46,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(122, 66, 191))
			},
			{
				121,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(254, 158, 35))
			},
			{
				190,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(107, 203, 0))
			},
			{
				368,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(236, 200, 19))
			},
			{
				674,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(236, 200, 19))
			},
			{
				273,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(179, 54, 201))
			},
			{
				675,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(179, 54, 201))
			},
			{
				2880,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(84, 234, 245))
			},
			{
				989,
				new FinalFractalHelper.FinalFractalProfile(48f, new Color(91, 158, 232))
			},
			{
				1826,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(252, 95, 4))
			},
			{
				3063,
				new FinalFractalHelper.FinalFractalProfile(76f, new Color(254, 194, 250))
			},
			{
				3065,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(237, 63, 133))
			},
			{
				757,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(80, 222, 122))
			},
			{
				155,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(56, 78, 210))
			},
			{
				795,
				new FinalFractalHelper.FinalFractalProfile(70f, new Color(237, 28, 36))
			},
			{
				3018,
				new FinalFractalHelper.FinalFractalProfile(80f, new Color(143, 215, 29))
			},
			{
				4144,
				new FinalFractalHelper.FinalFractalProfile(45f, new Color(178, 255, 180))
			},
			{
				3507,
				new FinalFractalHelper.FinalFractalProfile(45f, new Color(235, 166, 135))
			},
			{
				4956,
				new FinalFractalHelper.FinalFractalProfile(86f, new Color(178, 255, 180))
			}
		};

		// Token: 0x04004A15 RID: 18965
		private static FinalFractalHelper.FinalFractalProfile _defaultProfile = new FinalFractalHelper.FinalFractalProfile(50f, Color.White);

		// Token: 0x0200078B RID: 1931
		// (Invoke) Token: 0x06004166 RID: 16742
		public delegate void SpawnDustMethod(Vector2 centerPosition, float rotation, Vector2 velocity);

		// Token: 0x0200078C RID: 1932
		public struct FinalFractalProfile
		{
			// Token: 0x06004169 RID: 16745 RVA: 0x006BA4AC File Offset: 0x006B86AC
			public FinalFractalProfile(float fullBladeLength, Color color)
			{
				this.trailWidth = fullBladeLength / 2f;
				this.trailColor = color;
				this.widthMethod = null;
				this.colorMethod = null;
				this.dustMethod = null;
				this.widthMethod = new VertexStrip.StripHalfWidthFunction(this.StripWidth);
				this.colorMethod = new VertexStrip.StripColorFunction(this.StripColors);
				this.dustMethod = new FinalFractalHelper.SpawnDustMethod(this.StripDust);
			}

			// Token: 0x0600416A RID: 16746 RVA: 0x006BA538 File Offset: 0x006B8738
			private void StripDust(Vector2 centerPosition, float rotation, Vector2 velocity)
			{
				if (Main.rand.Next(9) == 0)
				{
					int num = Main.rand.Next(1, 4);
					for (int i = 0; i < num; i++)
					{
						Dust dust = Dust.NewDustPerfect(centerPosition, 278, null, 100, Color.Lerp(this.trailColor, Color.White, Main.rand.NextFloat() * 0.3f), 1f);
						dust.scale = 0.4f;
						dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
						dust.noGravity = true;
						dust.velocity += rotation.ToRotationVector2() * (3f + Main.rand.NextFloat() * 4f);
					}
				}
			}

			// Token: 0x0600416B RID: 16747 RVA: 0x006BA610 File Offset: 0x006B8810
			private Color StripColors(float progressOnStrip)
			{
				Color color = this.trailColor * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
				color.A /= 2;
				return color;
			}

			// Token: 0x0600416C RID: 16748 RVA: 0x006BA651 File Offset: 0x006B8851
			private float StripWidth(float progressOnStrip)
			{
				return this.trailWidth;
			}

			// Token: 0x0400702A RID: 28714
			public float trailWidth;

			// Token: 0x0400702B RID: 28715
			public Color trailColor;

			// Token: 0x0400702C RID: 28716
			public FinalFractalHelper.SpawnDustMethod dustMethod;

			// Token: 0x0400702D RID: 28717
			public VertexStrip.StripColorFunction colorMethod;

			// Token: 0x0400702E RID: 28718
			public VertexStrip.StripHalfWidthFunction widthMethod;
		}
	}
}
