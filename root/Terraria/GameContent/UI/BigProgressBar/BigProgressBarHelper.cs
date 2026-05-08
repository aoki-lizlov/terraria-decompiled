using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
	// Token: 0x0200038B RID: 907
	public class BigProgressBarHelper
	{
		// Token: 0x060029D6 RID: 10710 RVA: 0x0057F108 File Offset: 0x0057D308
		public static void DrawBareBonesBar(SpriteBatch spriteBatch, float lifePercent)
		{
			Rectangle rectangle = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), new Vector2(400f, 20f));
			Rectangle rectangle2 = rectangle;
			rectangle2.Inflate(2, 2);
			Texture2D value = TextureAssets.MagicPixel.Value;
			Rectangle rectangle3 = new Rectangle(0, 0, 1, 1);
			Rectangle rectangle4 = rectangle;
			rectangle4.Width = (int)((float)rectangle4.Width * lifePercent);
			spriteBatch.Draw(value, rectangle2, new Rectangle?(rectangle3), Color.White * 0.6f);
			spriteBatch.Draw(value, rectangle, new Rectangle?(rectangle3), Color.Black * 0.6f);
			spriteBatch.Draw(value, rectangle4, new Rectangle?(rectangle3), Color.LimeGreen * 0.5f);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x0057F1EC File Offset: 0x0057D3EC
		public static void DrawFancyBar(SpriteBatch spriteBatch, float lifeAmount, float lifeMax, Texture2D barIconTexture, Rectangle barIconFrame)
		{
			Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar", 1).Value;
			Point point = new Point(456, 22);
			Point point2 = new Point(32, 24);
			int num = 6;
			Rectangle rectangle = value.Frame(1, num, 0, 3, 0, 0);
			Color color = Color.White * 0.2f;
			float num2 = lifeAmount / lifeMax;
			int num3 = (int)((float)point.X * num2);
			num3 -= num3 % 2;
			Rectangle rectangle2 = value.Frame(1, num, 0, 2, 0, 0);
			rectangle2.X += point2.X;
			rectangle2.Y += point2.Y;
			rectangle2.Width = 2;
			rectangle2.Height = point.Y;
			Rectangle rectangle3 = value.Frame(1, num, 0, 1, 0, 0);
			rectangle3.X += point2.X;
			rectangle3.Y += point2.Y;
			rectangle3.Width = 2;
			rectangle3.Height = point.Y;
			Rectangle rectangle4 = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), point.ToVector2());
			Vector2 vector = rectangle4.TopLeft() - point2.ToVector2();
			spriteBatch.Draw(value, vector, new Rectangle?(rectangle), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle4.TopLeft(), new Rectangle?(rectangle2), Color.White, 0f, Vector2.Zero, new Vector2((float)(num3 / rectangle2.Width), 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle4.TopLeft() + new Vector2((float)(num3 - 2), 0f), new Rectangle?(rectangle3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Rectangle rectangle5 = value.Frame(1, num, 0, 0, 0, 0);
			spriteBatch.Draw(value, vector, new Rectangle?(rectangle5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Vector2 vector2 = new Vector2(4f, 20f) + new Vector2(26f, 28f) / 2f;
			spriteBatch.Draw(barIconTexture, vector + vector2, new Rectangle?(barIconFrame), Color.White, 0f, barIconFrame.Size() / 2f, 1f, SpriteEffects.None, 0f);
			if (BigProgressBarSystem.ShowText)
			{
				BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle4, lifeAmount, lifeMax);
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0057F4A0 File Offset: 0x0057D6A0
		private static void DrawHealthText(SpriteBatch spriteBatch, Rectangle area, float current, float max)
		{
			DynamicSpriteFont value = FontAssets.ItemStack.Value;
			Vector2 vector = area.Center.ToVector2();
			vector.Y += 1f;
			string text = "/";
			Vector2 vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X, vector.Y, Color.White, Color.Black, vector2 * 0.5f, 1f);
			text = ((int)current).ToString();
			vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X - 5f, vector.Y, Color.White, Color.Black, vector2 * new Vector2(1f, 0.5f), 1f);
			text = ((int)max).ToString();
			vector2 = value.MeasureString(text);
			Utils.DrawBorderStringFourWay(spriteBatch, value, text, vector.X + 5f, vector.Y, Color.White, Color.Black, vector2 * new Vector2(0f, 0.5f), 1f);
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0057F5B8 File Offset: 0x0057D7B8
		public static void DrawFancyBar(SpriteBatch spriteBatch, float lifeAmount, float lifeMax, Texture2D barIconTexture, Rectangle barIconFrame, float shieldCurrent, float shieldMax)
		{
			Texture2D value = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar", 1).Value;
			Point point = new Point(456, 22);
			Point point2 = new Point(32, 24);
			int num = 6;
			Rectangle rectangle = value.Frame(1, num, 0, 3, 0, 0);
			Color color = Color.White * 0.2f;
			float num2 = lifeAmount / lifeMax;
			int num3 = (int)((float)point.X * num2);
			num3 -= num3 % 2;
			Rectangle rectangle2 = value.Frame(1, num, 0, 2, 0, 0);
			rectangle2.X += point2.X;
			rectangle2.Y += point2.Y;
			rectangle2.Width = 2;
			rectangle2.Height = point.Y;
			Rectangle rectangle3 = value.Frame(1, num, 0, 1, 0, 0);
			rectangle3.X += point2.X;
			rectangle3.Y += point2.Y;
			rectangle3.Width = 2;
			rectangle3.Height = point.Y;
			float num4 = shieldCurrent / shieldMax;
			int num5 = (int)((float)point.X * num4);
			num5 -= num5 % 2;
			Rectangle rectangle4 = value.Frame(1, num, 0, 5, 0, 0);
			rectangle4.X += point2.X;
			rectangle4.Y += point2.Y;
			rectangle4.Width = 2;
			rectangle4.Height = point.Y;
			Rectangle rectangle5 = value.Frame(1, num, 0, 4, 0, 0);
			rectangle5.X += point2.X;
			rectangle5.Y += point2.Y;
			rectangle5.Width = 2;
			rectangle5.Height = point.Y;
			Rectangle rectangle6 = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0f, -50f), point.ToVector2());
			Vector2 vector = rectangle6.TopLeft() - point2.ToVector2();
			spriteBatch.Draw(value, vector, new Rectangle?(rectangle), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle6.TopLeft(), new Rectangle?(rectangle2), Color.White, 0f, Vector2.Zero, new Vector2((float)(num3 / rectangle2.Width), 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle6.TopLeft() + new Vector2((float)(num3 - 2), 0f), new Rectangle?(rectangle3), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle6.TopLeft(), new Rectangle?(rectangle4), Color.White, 0f, Vector2.Zero, new Vector2((float)(num5 / rectangle4.Width), 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(value, rectangle6.TopLeft() + new Vector2((float)(num5 - 2), 0f), new Rectangle?(rectangle5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Rectangle rectangle7 = value.Frame(1, num, 0, 0, 0, 0);
			spriteBatch.Draw(value, vector, new Rectangle?(rectangle7), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Vector2 vector2 = new Vector2(4f, 20f) + barIconFrame.Size() / 2f;
			spriteBatch.Draw(barIconTexture, vector + vector2, new Rectangle?(barIconFrame), Color.White, 0f, barIconFrame.Size() / 2f, 1f, SpriteEffects.None, 0f);
			if (BigProgressBarSystem.ShowText)
			{
				if (shieldCurrent > 0f)
				{
					BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle6, shieldCurrent, shieldMax);
					return;
				}
				BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle6, lifeAmount, lifeMax);
			}
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0000357B File Offset: 0x0000177B
		public BigProgressBarHelper()
		{
		}

		// Token: 0x040052CB RID: 21195
		private const string _bossBarTexturePath = "Images/UI/UI_BossBar";
	}
}
