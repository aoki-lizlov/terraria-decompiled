using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000379 RID: 889
	public class GameTipsDisplay
	{
		// Token: 0x0600296B RID: 10603 RVA: 0x0057B9C8 File Offset: 0x00579BC8
		public GameTipsDisplay(ITipProvider tipProvider)
		{
			this._tipProvider = tipProvider;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0057B9F0 File Offset: 0x00579BF0
		public void Update()
		{
			double time = Main.gameTimeCache.TotalGameTime.TotalSeconds;
			this._currentTips.RemoveAll((GameTipsDisplay.GameTip x) => x.IsExpired(time));
			bool flag = true;
			using (List<GameTipsDisplay.GameTip>.Enumerator enumerator = this._currentTips.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsExpiring(time))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.AddNewTip(time);
			}
			foreach (GameTipsDisplay.GameTip gameTip in this._currentTips)
			{
				gameTip.Update(time);
			}
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x0057BAD8 File Offset: 0x00579CD8
		public void ClearTips()
		{
			this._currentTips.Clear();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x0057BAE8 File Offset: 0x00579CE8
		public void Draw()
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			float num = (float)Main.screenWidth;
			float num2 = (float)Main.screenHeight + this.TipOffsetY;
			float num3 = (float)Main.screenWidth * 0.5f;
			foreach (GameTipsDisplay.GameTip gameTip in this._currentTips)
			{
				if (gameTip.ScreenAnchorX >= -0.5f && gameTip.ScreenAnchorX <= 1.5f)
				{
					DynamicSpriteFont value = FontAssets.MouseText.Value;
					string text = value.CreateWrappedText(gameTip.Text, num3, Language.ActiveCulture.CultureInfo);
					if (text.Split(new char[] { '\n' }).Length > 2)
					{
						text = value.CreateWrappedText(gameTip.Text, num3 * 1.5f - 50f, Language.ActiveCulture.CultureInfo);
					}
					if (Main.vampireSeed)
					{
						text = Language.GetTextValue("Misc.Vampirism");
					}
					else if (WorldGen.getGoodWorldGen)
					{
						string text2 = "";
						for (int i = text.Length - 1; i >= 0; i--)
						{
							text2 += text.Substring(i, 1);
						}
						text = text2;
					}
					else if (WorldGen.drunkWorldGenText)
					{
						text = string.Concat(Main.rand.Next(999999999));
						for (int j = 0; j < 14; j++)
						{
							if (Main.rand.Next(2) == 0)
							{
								text += Main.rand.Next(999999999);
							}
						}
					}
					Vector2 vector = value.MeasureString(text);
					float num4 = 1.1f;
					float num5 = 110f;
					if (vector.Y > num5)
					{
						num4 = num5 / vector.Y;
					}
					Vector2 vector2 = new Vector2(num * gameTip.ScreenAnchorX, num2);
					vector2 -= vector * num4 * 0.5f;
					if (WorldGen.tenthAnniversaryWorldGen && !Main.zenithWorld)
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, vector2, Color.HotPink, 0f, Vector2.Zero, new Vector2(num4, num4), -1f, 2f);
					}
					else
					{
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, value, text, vector2, Color.White, 0f, Vector2.Zero, new Vector2(num4, num4), -1f, 2f);
					}
				}
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x0057BD74 File Offset: 0x00579F74
		private void AddNewTip(double currentTime)
		{
			string text = "UI.Back";
			string key = this._tipProvider.RollAvailableTip().Key;
			if (Language.Exists(key))
			{
				text = key;
			}
			this._currentTips.Add(new GameTipsDisplay.GameTip(text, currentTime));
		}

		// Token: 0x040051F1 RID: 20977
		private readonly List<GameTipsDisplay.GameTip> _currentTips = new List<GameTipsDisplay.GameTip>();

		// Token: 0x040051F2 RID: 20978
		private ITipProvider _tipProvider;

		// Token: 0x040051F3 RID: 20979
		public float TipOffsetY = -150f;

		// Token: 0x020008D4 RID: 2260
		private class GameTip
		{
			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x06004676 RID: 18038 RVA: 0x006C709C File Offset: 0x006C529C
			public string Text
			{
				get
				{
					if (this._textKey == null)
					{
						return "What?!";
					}
					return this._formattedText;
				}
			}

			// Token: 0x06004677 RID: 18039 RVA: 0x006C70B2 File Offset: 0x006C52B2
			public bool IsExpired(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration;
			}

			// Token: 0x06004678 RID: 18040 RVA: 0x006C70C8 File Offset: 0x006C52C8
			public bool IsExpiring(double currentTime)
			{
				return currentTime >= this.SpawnTime + (double)this.Duration - 1.0;
			}

			// Token: 0x06004679 RID: 18041 RVA: 0x006C70E8 File Offset: 0x006C52E8
			public GameTip(string textKey, double spawnTime)
			{
				this._textKey = Language.GetText(textKey);
				this.SpawnTime = spawnTime;
				this.ScreenAnchorX = 2.5f;
				this.Duration = 11.5f;
				this._formattedText = this._textKey.Value;
			}

			// Token: 0x0600467A RID: 18042 RVA: 0x006C7138 File Offset: 0x006C5338
			public void Update(double currentTime)
			{
				double num = currentTime - this.SpawnTime;
				if (num < 0.5)
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(2.5f, 0.5f, (float)Utils.GetLerpValue(0.0, 0.5, num, true));
					return;
				}
				if (num >= (double)(this.Duration - 1f))
				{
					this.ScreenAnchorX = MathHelper.SmoothStep(0.5f, -1.5f, (float)Utils.GetLerpValue((double)(this.Duration - 1f), (double)this.Duration, num, true));
					return;
				}
				this.ScreenAnchorX = 0.5f;
			}

			// Token: 0x04007378 RID: 29560
			private const float APPEAR_FROM = 2.5f;

			// Token: 0x04007379 RID: 29561
			private const float APPEAR_TO = 0.5f;

			// Token: 0x0400737A RID: 29562
			private const float DISAPPEAR_TO = -1.5f;

			// Token: 0x0400737B RID: 29563
			private const float APPEAR_TIME = 0.5f;

			// Token: 0x0400737C RID: 29564
			private const float DISAPPEAR_TIME = 1f;

			// Token: 0x0400737D RID: 29565
			private const float DURATION = 11.5f;

			// Token: 0x0400737E RID: 29566
			private LocalizedText _textKey;

			// Token: 0x0400737F RID: 29567
			private string _formattedText;

			// Token: 0x04007380 RID: 29568
			public float ScreenAnchorX;

			// Token: 0x04007381 RID: 29569
			public readonly float Duration;

			// Token: 0x04007382 RID: 29570
			public readonly double SpawnTime;
		}

		// Token: 0x020008D5 RID: 2261
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600467B RID: 18043 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600467C RID: 18044 RVA: 0x006C71D7 File Offset: 0x006C53D7
			internal bool <Update>b__0(GameTipsDisplay.GameTip x)
			{
				return x.IsExpired(this.time);
			}

			// Token: 0x04007383 RID: 29571
			public double time;
		}
	}
}
