using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
	// Token: 0x020004FF RID: 1279
	public class ScreenObstruction
	{
		// Token: 0x060035D6 RID: 13782 RVA: 0x0061DA78 File Offset: 0x0061BC78
		public static void Update(SceneState sceneState, SceneMetrics metrics)
		{
			float num = 0f;
			float num2 = 0.1f;
			if (metrics.PerspectivePlayer.insideUnbreakableWalls)
			{
				int progressPlayerCanSafelyMatch = DangerousDungeonCurse.GetProgressPlayerCanSafelyMatch();
				int num3 = DangerousDungeonCurse.GetProgressPlayerNeedsToMatch(metrics.PerspectivePlayer) - progressPlayerCanSafelyMatch;
				if (num3 > 0)
				{
					float num4 = 0.9f;
					num = Utils.Clamp<float>(0.4f * (float)num3, 0f, num4);
					num2 = (ScreenObstruction.lastSpeed = 0.01f);
				}
			}
			if (metrics.PerspectivePlayer.headcovered)
			{
				num = 0.95f;
				num2 = (ScreenObstruction.lastSpeed = 0.3f);
			}
			if (num == 0f && ScreenObstruction.screenObstruction != 0f)
			{
				num2 = ScreenObstruction.lastSpeed;
			}
			else
			{
				ScreenObstruction.lastSpeed = num2;
			}
			sceneState.MoveTowards(ref ScreenObstruction.screenObstruction, num, num2);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x0061DB2C File Offset: 0x0061BD2C
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (ScreenObstruction.screenObstruction == 0f)
			{
				return;
			}
			Color color = Color.Black * ScreenObstruction.screenObstruction;
			int num = TextureAssets.Extra[49].Width();
			int num2 = 10;
			Rectangle rect = Main.SceneMetrics.PerspectivePlayer.getRect();
			rect.Inflate((num - rect.Width) / 2, (num - rect.Height) / 2 + num2 / 2);
			rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num2);
			Rectangle rectangle = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
			Rectangle rectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
			Rectangle rectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
			Rectangle rectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, rectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
			spriteBatch.Draw(TextureAssets.Extra[49].Value, rect, color);
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x0000357B File Offset: 0x0000177B
		public ScreenObstruction()
		{
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x0061DD16 File Offset: 0x0061BF16
		// Note: this type is marked as 'beforefieldinit'.
		static ScreenObstruction()
		{
		}

		// Token: 0x04005AED RID: 23277
		public static float lastSpeed = 0.1f;

		// Token: 0x04005AEE RID: 23278
		public static float screenObstruction;
	}
}
