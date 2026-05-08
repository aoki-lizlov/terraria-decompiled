using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FE RID: 1278
	public class ScreenDarkness
	{
		// Token: 0x060035D1 RID: 13777 RVA: 0x0061D84C File Offset: 0x0061BA4C
		public static void Update(SceneState sceneState, SceneMetrics metrics)
		{
			float num = 0f;
			float num2 = 0.016666668f;
			Vector2 center = metrics.Center;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 370 && Main.npc[i].Distance(center) < 3000f && (Main.npc[i].ai[0] >= 10f || (Main.npc[i].ai[0] == 9f && Main.npc[i].ai[2] > 120f)))
				{
					num = 0.95f;
					ScreenDarkness.frontColor = new Color(0, 0, 120) * 0.3f;
					num2 = 0.03f;
				}
				if (Main.npc[i].active && Main.npc[i].type == 113 && Main.npc[i].Distance(center) < 3000f)
				{
					float num3 = Utils.Remap(Main.npc[i].Distance(center), 2000f, 3000f, 1f, 0f, true);
					num = Main.npc[i].localAI[1] * num3;
					num2 = 1f;
					ScreenDarkness.frontColor = Color.Black;
				}
			}
			sceneState.MoveTowards(ref ScreenDarkness.screenObstruction, num, num2);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x0061D9A8 File Offset: 0x0061BBA8
		public static void DrawBack(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction == 0f)
			{
				return;
			}
			Color color = Color.Black * ScreenDarkness.screenObstruction;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x0061DA08 File Offset: 0x0061BC08
		public static void DrawFront(SpriteBatch spriteBatch)
		{
			if (ScreenDarkness.screenObstruction == 0f)
			{
				return;
			}
			Color color = ScreenDarkness.frontColor * ScreenDarkness.screenObstruction;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x0000357B File Offset: 0x0000177B
		public ScreenDarkness()
		{
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x0061DA68 File Offset: 0x0061BC68
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenDarkness()
		{
		}

		// Token: 0x04005AEB RID: 23275
		public static float screenObstruction;

		// Token: 0x04005AEC RID: 23276
		public static Color frontColor = new Color(0, 0, 120);
	}
}
