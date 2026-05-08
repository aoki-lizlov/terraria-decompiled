using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200036C RID: 876
	public class UIPopupTextManager
	{
		// Token: 0x0600292C RID: 10540 RVA: 0x005788C0 File Offset: 0x00576AC0
		public void ResetText(UIPopupText text)
		{
			text.scale = 0f;
			text.rotation = 0f;
			text.alpha = 1f;
			text.alphaDir = -1;
			text.framesSinceSpawn = 0;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x005788F4 File Offset: 0x00576AF4
		public int NewText(UIAdvancedPopupRequest request)
		{
			if (!Main.showItemText)
			{
				return -1;
			}
			if (Main.netMode == 2)
			{
				return -1;
			}
			int num = this.FindNextItemTextSlot();
			if (num >= 0)
			{
				string text = request.Text;
				Vector2 vector = FontAssets.MouseText.Value.MeasureString(text);
				UIPopupText uipopupText = this.popupText[num];
				this.ResetText(uipopupText);
				uipopupText.active = true;
				uipopupText.position = request.Position;
				if (request.Alignment >= UIPopupTextAlignment.BottomLeft)
				{
					UIPopupText uipopupText2 = uipopupText;
					uipopupText2.position.Y = uipopupText2.position.Y - vector.Y;
				}
				else if (request.Alignment >= UIPopupTextAlignment.MidLeft)
				{
					UIPopupText uipopupText3 = uipopupText;
					uipopupText3.position.Y = uipopupText3.position.Y - vector.Y / 2f;
				}
				int num2 = (int)(request.Alignment % UIPopupTextAlignment.MidLeft);
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						UIPopupText uipopupText4 = uipopupText;
						uipopupText4.position.X = uipopupText4.position.X - vector.X;
					}
				}
				else
				{
					UIPopupText uipopupText5 = uipopupText;
					uipopupText5.position.X = uipopupText5.position.X - vector.X / 2f;
				}
				uipopupText.name = text;
				uipopupText.velocity = request.Velocity;
				uipopupText.lifeTime = request.DurationInFrames;
				uipopupText.context = request.Context;
				uipopupText.color = request.Color;
				uipopupText.PrepareDisplayText();
			}
			return num;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00578A28 File Offset: 0x00576C28
		private int FindNextItemTextSlot()
		{
			int num = -1;
			for (int i = 0; i < 20; i++)
			{
				if (!this.popupText[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				double num2 = (double)Main.bottomWorld;
				for (int j = 0; j < 20; j++)
				{
					if (num2 > (double)this.popupText[j].position.Y)
					{
						num = j;
						num2 = (double)this.popupText[j].position.Y;
					}
				}
			}
			return num;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00578A9C File Offset: 0x00576C9C
		public void UpdateItemText()
		{
			int num = 0;
			for (int i = 0; i < 20; i++)
			{
				if (this.popupText[i].active)
				{
					num++;
					this.popupText[i].Update(i, this);
				}
			}
			this.numActive = num;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00578AE4 File Offset: 0x00576CE4
		public void ClearAll()
		{
			for (int i = 0; i < 20; i++)
			{
				this.popupText[i] = new UIPopupText();
			}
			this.numActive = 0;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x00578B14 File Offset: 0x00576D14
		public void DrawItemTextPopups(float scaleTarget)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < 20; i++)
			{
				UIPopupText uipopupText = this.popupText[i];
				if (uipopupText.active)
				{
					string displayText = uipopupText.displayText;
					Vector2 vector = FontAssets.MouseText.Value.MeasureString(displayText);
					Vector2 vector2 = new Vector2(vector.X * 0.5f, vector.Y * 0.5f);
					float num = scaleTarget;
					float num2 = uipopupText.scale / num;
					int num3 = (int)(255f - 255f * num2);
					float num4 = (float)uipopupText.color.R;
					float num5 = (float)uipopupText.color.G;
					float num6 = (float)uipopupText.color.B;
					float num7 = (float)uipopupText.color.A;
					num4 *= num2 * uipopupText.alpha * 0.3f;
					float alpha = uipopupText.alpha;
					float alpha2 = uipopupText.alpha;
					num7 *= num2 * uipopupText.alpha;
					Color color = Color.Black;
					float num8 = 1f;
					Texture2D texture2D = null;
					if (uipopupText.context == UIPopupTextContext.SpecialSeed)
					{
						color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.6f, byte.MaxValue) * 0.6f;
						num *= 0.5f;
						num8 = 0.8f;
					}
					int num9 = 40;
					Utils.EaseOutCirc((double)Utils.Remap((float)uipopupText.framesSinceSpawn, 0f, (float)num9, 0f, 1f, true));
					float num10 = (float)num3 / 255f;
					for (int j = 0; j < 5; j++)
					{
						Color color2 = color;
						float num11 = 0f;
						float num12 = 0f;
						if (j == 0)
						{
							num11 -= num * 2f;
						}
						else if (j == 1)
						{
							num11 += num * 2f;
						}
						else if (j == 2)
						{
							num12 -= num * 2f;
						}
						else if (j == 3)
						{
							num12 += num * 2f;
						}
						else
						{
							color2 = uipopupText.color * num2 * uipopupText.alpha * num8;
						}
						if (j < 4)
						{
							num7 = (float)uipopupText.color.A * num2 * uipopupText.alpha;
							color2 = new Color(0, 0, 0, (int)num7);
						}
						if (color != Color.Black && j < 4)
						{
							num11 *= 1.3f + 1.3f * num10;
							num12 *= 1.3f + 1.3f * num10;
						}
						float num13 = uipopupText.position.X + num11;
						float num14 = uipopupText.position.Y + num12;
						if (color != Color.Black && j < 4)
						{
							Color color3 = color;
							color3.A = (byte)MathHelper.Lerp(60f, 127f, Utils.GetLerpValue(0f, 255f, num7, true));
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, displayText, new Vector2(num13 + vector2.X, num14 + vector2.Y), Color.Lerp(color2, color3, 0.5f), uipopupText.rotation, vector2, uipopupText.scale, SpriteEffects.None, 0f, null, null);
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, displayText, new Vector2(num13 + vector2.X, num14 + vector2.Y), color3, uipopupText.rotation, vector2, uipopupText.scale, SpriteEffects.None, 0f, null, null);
						}
						else
						{
							DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, displayText, new Vector2(num13 + vector2.X, num14 + vector2.Y), color2, uipopupText.rotation, vector2, uipopupText.scale, SpriteEffects.None, 0f, null, null);
						}
						if (texture2D != null)
						{
							float num15 = (1.3f - num10) * uipopupText.scale * 0.7f;
							Vector2 vector3 = new Vector2(num13 + vector2.X, num14 + vector2.Y);
							Color color4 = color * 0.6f;
							if (j == 4)
							{
								color4 = Color.White * 0.6f;
							}
							color4.A = (byte)((float)color4.A * 0.5f);
							int num16 = 25;
							spriteBatch.Draw(texture2D, vector3 + new Vector2(vector2.X * -0.5f - (float)num16 - texture2D.Size().X / 2f, 0f), null, color4 * uipopupText.scale, 0f, texture2D.Size() / 2f, num15, SpriteEffects.None, 0f);
							spriteBatch.Draw(texture2D, vector3 + new Vector2(vector2.X * 0.5f + (float)num16 + texture2D.Size().X / 2f, 0f), null, color4 * uipopupText.scale, 0f, texture2D.Size() / 2f, num15, SpriteEffects.None, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00579031 File Offset: 0x00577231
		public UIPopupTextManager()
		{
		}

		// Token: 0x040051CA RID: 20938
		public const int maxItemText = 20;

		// Token: 0x040051CB RID: 20939
		public UIPopupText[] popupText = new UIPopupText[20];

		// Token: 0x040051CC RID: 20940
		public int numActive;
	}
}
