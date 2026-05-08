using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x0200004A RID: 74
	public class Lighting
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00353E47 File Offset: 0x00352047
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00353E4E File Offset: 0x0035204E
		public static float GlobalBrightness
		{
			[CompilerGenerated]
			get
			{
				return Lighting.<GlobalBrightness>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Lighting.<GlobalBrightness>k__BackingField = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00353E56 File Offset: 0x00352056
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00353E60 File Offset: 0x00352060
		public static LightMode Mode
		{
			get
			{
				return Lighting._mode;
			}
			set
			{
				Lighting._mode = value;
				switch (Lighting._mode)
				{
				case LightMode.White:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 1;
					break;
				case LightMode.Retro:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 2;
					break;
				case LightMode.Trippy:
					Lighting._activeEngine = Lighting.LegacyEngine;
					Lighting.LegacyEngine.Mode = 3;
					break;
				case LightMode.Color:
					Lighting._activeEngine = Lighting.NewEngine;
					Lighting.LegacyEngine.Mode = 0;
					Lighting.OffScreenTiles = 35;
					break;
				}
				Main.renderCount = 0;
				Main.renderNow = false;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00353EFE File Offset: 0x003520FE
		public static bool NotRetro
		{
			get
			{
				return Lighting.Mode != LightMode.Retro && Lighting.Mode != LightMode.Trippy;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00353F15 File Offset: 0x00352115
		public static bool UsingNewLighting
		{
			get
			{
				return Lighting.Mode == LightMode.Color;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00353F1F File Offset: 0x0035211F
		public static bool UpdateEveryFrame
		{
			get
			{
				return !Main.RenderTargetsRequired && !Lighting.NotRetro;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00353F32 File Offset: 0x00352132
		public static void Initialize()
		{
			Lighting.GlobalBrightness = 1.2f;
			Lighting.NewEngine.Rebuild();
			Lighting.LegacyEngine.Rebuild();
			if (Lighting._activeEngine == null)
			{
				Lighting.Mode = LightMode.Color;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00353F60 File Offset: 0x00352160
		public static void LightTiles(Rectangle area)
		{
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			Main.render = true;
			Lighting.UpdateGlobalBrightness();
			Lighting._activeEngine.ProcessArea(area);
			TimeLogger.Lighting.AddTime(startTimestamp);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00353F94 File Offset: 0x00352194
		private static void UpdateGlobalBrightness()
		{
			Lighting.GlobalBrightness = 1.2f;
			if (Main.player[Main.myPlayer].blind)
			{
				Lighting.GlobalBrightness = 1f;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00353FBC File Offset: 0x003521BC
		public static float Brightness(int x, int y)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y);
			return Lighting.GlobalBrightness * (color.X + color.Y + color.Z) / 3f;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00353FF8 File Offset: 0x003521F8
		public static Vector3 GetSubLight(Vector2 position)
		{
			Vector2 vector = position / 16f - new Vector2(0.5f, 0.5f);
			Vector2 vector2 = new Vector2(vector.X % 1f, vector.Y % 1f);
			int num = (int)vector.X;
			int num2 = (int)vector.Y;
			Vector3 color = Lighting._activeEngine.GetColor(num, num2);
			Vector3 color2 = Lighting._activeEngine.GetColor(num + 1, num2);
			Vector3 color3 = Lighting._activeEngine.GetColor(num, num2 + 1);
			Vector3 color4 = Lighting._activeEngine.GetColor(num + 1, num2 + 1);
			Vector3 vector3 = Vector3.Lerp(color, color2, vector2.X);
			Vector3 vector4 = Vector3.Lerp(color3, color4, vector2.X);
			return Vector3.Lerp(vector3, vector4, vector2.Y);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x003540BD File Offset: 0x003522BD
		public static void AddLight(Vector2 position, Vector3 rgb)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), rgb.X, rgb.Y, rgb.Z);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x003540F0 File Offset: 0x003522F0
		public static void AddLight(Vector2 position, float r, float g, float b)
		{
			Lighting.AddLight((int)(position.X / 16f), (int)(position.Y / 16f), r, g, b);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00354114 File Offset: 0x00352314
		public static void AddLight(int i, int j, int torchID, float lightAmount)
		{
			float num;
			float num2;
			float num3;
			TorchID.TorchColor(torchID, out num, out num2, out num3);
			Lighting._activeEngine.AddLight(i, j, new Vector3(num * lightAmount, num2 * lightAmount, num3 * lightAmount));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00354148 File Offset: 0x00352348
		public static void AddLight(Vector2 position, int torchID)
		{
			float num;
			float num2;
			float num3;
			TorchID.TorchColor(torchID, out num, out num2, out num3);
			Lighting.AddLight((int)position.X / 16, (int)position.Y / 16, num, num2, num3);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0035417D File Offset: 0x0035237D
		public static void AddLight(int i, int j, float r, float g, float b)
		{
			if (Main.gamePaused)
			{
				return;
			}
			if (Main.netMode == 2)
			{
				return;
			}
			Lighting._activeEngine.AddLight(i, j, new Vector3(r, g, b));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x003541A5 File Offset: 0x003523A5
		public static void NextLightMode()
		{
			Lighting.Mode++;
			if (!Enum.IsDefined(typeof(LightMode), Lighting.Mode))
			{
				Lighting.Mode = LightMode.White;
			}
			Lighting.Clear();
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x003541D9 File Offset: 0x003523D9
		public static void Clear()
		{
			Lighting._activeEngine.Clear();
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x003541E5 File Offset: 0x003523E5
		public static Color GetColor(Point tileCoords)
		{
			if (Main.gameMenu)
			{
				return Color.White;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * Lighting.GlobalBrightness);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00354219 File Offset: 0x00352419
		public static Color GetColor(Point tileCoords, Color originalColor)
		{
			if (Main.gameMenu)
			{
				return originalColor;
			}
			return new Color(Lighting._activeEngine.GetColor(tileCoords.X, tileCoords.Y) * originalColor.ToVector3());
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0035424B File Offset: 0x0035244B
		public static Color GetColor(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			return new Color(Lighting._activeEngine.GetColor(x, y) * oldColor.ToVector3());
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00354274 File Offset: 0x00352474
		public static Color GetColorClamped(int x, int y, Color oldColor)
		{
			if (Main.gameMenu)
			{
				return oldColor;
			}
			Vector3 vector = Lighting._activeEngine.GetColor(x, y);
			vector = Vector3.Min(Vector3.One, vector);
			return new Color(vector * oldColor.ToVector3());
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x003542B8 File Offset: 0x003524B8
		public static Color GetColor(int x, int y)
		{
			if (Main.gameMenu)
			{
				return Color.White;
			}
			Color color = default(Color);
			Vector3 color2 = Lighting._activeEngine.GetColor(x, y);
			float num = Lighting.GlobalBrightness * 255f;
			int num2 = (int)(color2.X * num);
			int num3 = (int)(color2.Y * num);
			int num4 = (int)(color2.Z * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num4 <<= 16;
			num3 <<= 8;
			color.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			return color;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0035435C File Offset: 0x0035255C
		public static void GetColor9Slice(int centerX, int centerY, ref Color[] slices)
		{
			int num = 0;
			for (int i = centerX - 1; i <= centerX + 1; i++)
			{
				for (int j = centerY - 1; j <= centerY + 1; j++)
				{
					Vector3 color = Lighting._activeEngine.GetColor(i, j);
					int num2 = (int)(255f * color.X * Lighting.GlobalBrightness);
					int num3 = (int)(255f * color.Y * Lighting.GlobalBrightness);
					int num4 = (int)(255f * color.Z * Lighting.GlobalBrightness);
					if (num2 > 255)
					{
						num2 = 255;
					}
					if (num3 > 255)
					{
						num3 = 255;
					}
					if (num4 > 255)
					{
						num4 = 255;
					}
					num4 <<= 16;
					num3 <<= 8;
					slices[num].PackedValue = (uint)(num2 | num3 | num4 | -16777216);
					num += 3;
				}
				num -= 8;
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00354444 File Offset: 0x00352644
		public static void GetColor9Slice(int x, int y, ref Vector3[] slices)
		{
			slices[0] = Lighting._activeEngine.GetColor(x - 1, y - 1) * Lighting.GlobalBrightness;
			slices[3] = Lighting._activeEngine.GetColor(x - 1, y) * Lighting.GlobalBrightness;
			slices[6] = Lighting._activeEngine.GetColor(x - 1, y + 1) * Lighting.GlobalBrightness;
			slices[1] = Lighting._activeEngine.GetColor(x, y - 1) * Lighting.GlobalBrightness;
			slices[4] = Lighting._activeEngine.GetColor(x, y) * Lighting.GlobalBrightness;
			slices[7] = Lighting._activeEngine.GetColor(x, y + 1) * Lighting.GlobalBrightness;
			slices[2] = Lighting._activeEngine.GetColor(x + 1, y - 1) * Lighting.GlobalBrightness;
			slices[5] = Lighting._activeEngine.GetColor(x + 1, y) * Lighting.GlobalBrightness;
			slices[8] = Lighting._activeEngine.GetColor(x + 1, y + 1) * Lighting.GlobalBrightness;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00354578 File Offset: 0x00352778
		public static void GetCornerColors(int centerX, int centerY, out VertexColors vertices, float scale = 1f)
		{
			vertices = default(VertexColors);
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color5 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			Vector3 color6 = Lighting._activeEngine.GetColor(centerX - 1, centerY - 1);
			Vector3 color7 = Lighting._activeEngine.GetColor(centerX + 1, centerY - 1);
			Vector3 color8 = Lighting._activeEngine.GetColor(centerX - 1, centerY + 1);
			Vector3 color9 = Lighting._activeEngine.GetColor(centerX + 1, centerY + 1);
			float num = Lighting.GlobalBrightness * scale * 63.75f;
			int num2 = (int)((color2.X + color6.X + color4.X + color.X) * num);
			int num3 = (int)((color2.Y + color6.Y + color4.Y + color.Y) * num);
			int num4 = (int)((color2.Z + color6.Z + color4.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.TopLeftColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color2.X + color7.X + color5.X + color.X) * num);
			num3 = (int)((color2.Y + color7.Y + color5.Y + color.Y) * num);
			num4 = (int)((color2.Z + color7.Z + color5.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.TopRightColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color3.X + color8.X + color4.X + color.X) * num);
			num3 = (int)((color3.Y + color8.Y + color4.Y + color.Y) * num);
			num4 = (int)((color3.Z + color8.Z + color4.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.BottomLeftColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
			num2 = (int)((color3.X + color9.X + color5.X + color.X) * num);
			num3 = (int)((color3.Y + color9.Y + color5.Y + color.Y) * num);
			num4 = (int)((color3.Z + color9.Z + color5.Z + color.Z) * num);
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			num3 <<= 8;
			num4 <<= 16;
			vertices.BottomRightColor.PackedValue = (uint)(num2 | num3 | num4 | -16777216);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0035492C File Offset: 0x00352B2C
		public static void GetColor4Slice(int centerX, int centerY, ref Color[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(centerX, centerY - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(centerX, centerY + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(centerX - 1, centerY);
			Vector3 color4 = Lighting._activeEngine.GetColor(centerX + 1, centerY);
			float num = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num >= num4)
			{
				int num5 = (int)(255f * color3.X * Lighting.GlobalBrightness);
				int num6 = (int)(255f * color3.Y * Lighting.GlobalBrightness);
				int num7 = (int)(255f * color3.Z * Lighting.GlobalBrightness);
				if (num5 > 255)
				{
					num5 = 255;
				}
				if (num6 > 255)
				{
					num6 = 255;
				}
				if (num7 > 255)
				{
					num7 = 255;
				}
				slices[0] = new Color((int)((byte)num5), (int)((byte)num6), (int)((byte)num7), 255);
			}
			else
			{
				int num8 = (int)(255f * color.X * Lighting.GlobalBrightness);
				int num9 = (int)(255f * color.Y * Lighting.GlobalBrightness);
				int num10 = (int)(255f * color.Z * Lighting.GlobalBrightness);
				if (num8 > 255)
				{
					num8 = 255;
				}
				if (num9 > 255)
				{
					num9 = 255;
				}
				if (num10 > 255)
				{
					num10 = 255;
				}
				slices[0] = new Color((int)((byte)num8), (int)((byte)num9), (int)((byte)num10), 255);
			}
			if (num >= num3)
			{
				int num11 = (int)(255f * color4.X * Lighting.GlobalBrightness);
				int num12 = (int)(255f * color4.Y * Lighting.GlobalBrightness);
				int num13 = (int)(255f * color4.Z * Lighting.GlobalBrightness);
				if (num11 > 255)
				{
					num11 = 255;
				}
				if (num12 > 255)
				{
					num12 = 255;
				}
				if (num13 > 255)
				{
					num13 = 255;
				}
				slices[1] = new Color((int)((byte)num11), (int)((byte)num12), (int)((byte)num13), 255);
			}
			else
			{
				int num14 = (int)(255f * color.X * Lighting.GlobalBrightness);
				int num15 = (int)(255f * color.Y * Lighting.GlobalBrightness);
				int num16 = (int)(255f * color.Z * Lighting.GlobalBrightness);
				if (num14 > 255)
				{
					num14 = 255;
				}
				if (num15 > 255)
				{
					num15 = 255;
				}
				if (num16 > 255)
				{
					num16 = 255;
				}
				slices[1] = new Color((int)((byte)num14), (int)((byte)num15), (int)((byte)num16), 255);
			}
			if (num2 >= num4)
			{
				int num17 = (int)(255f * color3.X * Lighting.GlobalBrightness);
				int num18 = (int)(255f * color3.Y * Lighting.GlobalBrightness);
				int num19 = (int)(255f * color3.Z * Lighting.GlobalBrightness);
				if (num17 > 255)
				{
					num17 = 255;
				}
				if (num18 > 255)
				{
					num18 = 255;
				}
				if (num19 > 255)
				{
					num19 = 255;
				}
				slices[2] = new Color((int)((byte)num17), (int)((byte)num18), (int)((byte)num19), 255);
			}
			else
			{
				int num20 = (int)(255f * color2.X * Lighting.GlobalBrightness);
				int num21 = (int)(255f * color2.Y * Lighting.GlobalBrightness);
				int num22 = (int)(255f * color2.Z * Lighting.GlobalBrightness);
				if (num20 > 255)
				{
					num20 = 255;
				}
				if (num21 > 255)
				{
					num21 = 255;
				}
				if (num22 > 255)
				{
					num22 = 255;
				}
				slices[2] = new Color((int)((byte)num20), (int)((byte)num21), (int)((byte)num22), 255);
			}
			if (num2 >= num3)
			{
				int num23 = (int)(255f * color4.X * Lighting.GlobalBrightness);
				int num24 = (int)(255f * color4.Y * Lighting.GlobalBrightness);
				int num25 = (int)(255f * color4.Z * Lighting.GlobalBrightness);
				if (num23 > 255)
				{
					num23 = 255;
				}
				if (num24 > 255)
				{
					num24 = 255;
				}
				if (num25 > 255)
				{
					num25 = 255;
				}
				slices[3] = new Color((int)((byte)num23), (int)((byte)num24), (int)((byte)num25), 255);
				return;
			}
			int num26 = (int)(255f * color2.X * Lighting.GlobalBrightness);
			int num27 = (int)(255f * color2.Y * Lighting.GlobalBrightness);
			int num28 = (int)(255f * color2.Z * Lighting.GlobalBrightness);
			if (num26 > 255)
			{
				num26 = 255;
			}
			if (num27 > 255)
			{
				num27 = 255;
			}
			if (num28 > 255)
			{
				num28 = 255;
			}
			slices[3] = new Color((int)((byte)num26), (int)((byte)num27), (int)((byte)num28), 255);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00354E64 File Offset: 0x00353064
		public static void GetColor4Slice(int x, int y, ref Vector3[] slices)
		{
			Vector3 color = Lighting._activeEngine.GetColor(x, y - 1);
			Vector3 color2 = Lighting._activeEngine.GetColor(x, y + 1);
			Vector3 color3 = Lighting._activeEngine.GetColor(x - 1, y);
			Vector3 color4 = Lighting._activeEngine.GetColor(x + 1, y);
			float num = color.X + color.Y + color.Z;
			float num2 = color2.X + color2.Y + color2.Z;
			float num3 = color4.X + color4.Y + color4.Z;
			float num4 = color3.X + color3.Y + color3.Z;
			if (num >= num4)
			{
				slices[0] = color3 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[0] = color * Lighting.GlobalBrightness;
			}
			if (num >= num3)
			{
				slices[1] = color4 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[1] = color * Lighting.GlobalBrightness;
			}
			if (num2 >= num4)
			{
				slices[2] = color3 * Lighting.GlobalBrightness;
			}
			else
			{
				slices[2] = color2 * Lighting.GlobalBrightness;
			}
			if (num2 >= num3)
			{
				slices[3] = color4 * Lighting.GlobalBrightness;
				return;
			}
			slices[3] = color2 * Lighting.GlobalBrightness;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0000357B File Offset: 0x0000177B
		public Lighting()
		{
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00354FB7 File Offset: 0x003531B7
		// Note: this type is marked as 'beforefieldinit'.
		static Lighting()
		{
		}

		// Token: 0x040009A9 RID: 2473
		private const float DEFAULT_GLOBAL_BRIGHTNESS = 1.2f;

		// Token: 0x040009AA RID: 2474
		private const float BLIND_GLOBAL_BRIGHTNESS = 1f;

		// Token: 0x040009AB RID: 2475
		[CompilerGenerated]
		private static float <GlobalBrightness>k__BackingField;

		// Token: 0x040009AC RID: 2476
		[Old]
		public static int OffScreenTiles = 45;

		// Token: 0x040009AD RID: 2477
		private static LightMode _mode = LightMode.Color;

		// Token: 0x040009AE RID: 2478
		private static readonly LightingEngine NewEngine = new LightingEngine();

		// Token: 0x040009AF RID: 2479
		private static readonly LegacyLighting LegacyEngine = new LegacyLighting(Main.Camera);

		// Token: 0x040009B0 RID: 2480
		private static ILightingEngine _activeEngine;
	}
}
