using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000382 RID: 898
	public class GlyphTagHandler : ITagHandler
	{
		// Token: 0x060029B7 RID: 10679 RVA: 0x0057E705 File Offset: 0x0057C905
		public static TextSnippet GetGlyph(string keyName)
		{
			return new GlyphTagHandler.GlyphSnippet(keyName);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x0057E710 File Offset: 0x0057C910
		TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
		{
			int num;
			if (!int.TryParse(text, out num) || num >= 26)
			{
				return new TextSnippet(text);
			}
			return new GlyphTagHandler.GlyphSnippet(num)
			{
				DeleteWhole = true,
				Text = "[g:" + num + "]"
			};
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x0057E75C File Offset: 0x0057C95C
		public static string GenerateTag(int index)
		{
			string text = "[g";
			return string.Concat(new object[] { text, ":", index, "]" });
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x0057E79C File Offset: 0x0057C99C
		public static string GenerateTag(string keyname)
		{
			int num;
			if (GlyphTagHandler.GlyphIndexes.TryGetValue(keyname, out num))
			{
				return GlyphTagHandler.GenerateTag(num);
			}
			return keyname;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x0000357B File Offset: 0x0000177B
		public GlyphTagHandler()
		{
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x0057E7C0 File Offset: 0x0057C9C0
		// Note: this type is marked as 'beforefieldinit'.
		static GlyphTagHandler()
		{
		}

		// Token: 0x040052AF RID: 21167
		private const int GlyphsPerLine = 25;

		// Token: 0x040052B0 RID: 21168
		private const int MaxGlyphs = 26;

		// Token: 0x040052B1 RID: 21169
		public static float GlyphsScale = 1f;

		// Token: 0x040052B2 RID: 21170
		public const int DefaultGlyphStyle = 0;

		// Token: 0x040052B3 RID: 21171
		public static int GlyphStyle = 0;

		// Token: 0x040052B4 RID: 21172
		private static Dictionary<string, int> GlyphIndexes = new Dictionary<string, int>
		{
			{
				Buttons.A.ToString(),
				0
			},
			{
				Buttons.B.ToString(),
				1
			},
			{
				Buttons.Back.ToString(),
				4
			},
			{
				Buttons.DPadDown.ToString(),
				15
			},
			{
				Buttons.DPadLeft.ToString(),
				14
			},
			{
				Buttons.DPadRight.ToString(),
				13
			},
			{
				Buttons.DPadUp.ToString(),
				16
			},
			{
				Buttons.LeftShoulder.ToString(),
				6
			},
			{
				Buttons.LeftStick.ToString(),
				10
			},
			{
				Buttons.LeftThumbstickDown.ToString(),
				20
			},
			{
				Buttons.LeftThumbstickLeft.ToString(),
				17
			},
			{
				Buttons.LeftThumbstickRight.ToString(),
				18
			},
			{
				Buttons.LeftThumbstickUp.ToString(),
				19
			},
			{
				Buttons.LeftTrigger.ToString(),
				8
			},
			{
				Buttons.RightShoulder.ToString(),
				7
			},
			{
				Buttons.RightStick.ToString(),
				11
			},
			{
				Buttons.RightThumbstickDown.ToString(),
				24
			},
			{
				Buttons.RightThumbstickLeft.ToString(),
				21
			},
			{
				Buttons.RightThumbstickRight.ToString(),
				22
			},
			{
				Buttons.RightThumbstickUp.ToString(),
				23
			},
			{
				Buttons.RightTrigger.ToString(),
				9
			},
			{
				Buttons.Start.ToString(),
				5
			},
			{
				Buttons.X.ToString(),
				2
			},
			{
				Buttons.Y.ToString(),
				3
			},
			{ "RightStickAxis", 12 },
			{ "LR", 25 }
		};

		// Token: 0x020008D9 RID: 2265
		public class GlyphXboxTagHandler : ITagHandler
		{
			// Token: 0x0600468A RID: 18058 RVA: 0x006C84A8 File Offset: 0x006C66A8
			TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
			{
				int num;
				if (!int.TryParse(text, out num) || num >= 26)
				{
					return new TextSnippet(text);
				}
				return new GlyphTagHandler.GlyphSnippet(num)
				{
					ForcedStyle = 0,
					DeleteWhole = true,
					Text = "[gx:" + num + "]"
				};
			}

			// Token: 0x0600468B RID: 18059 RVA: 0x0000357B File Offset: 0x0000177B
			public GlyphXboxTagHandler()
			{
			}
		}

		// Token: 0x020008DA RID: 2266
		public class GlyphPSTagHandler : ITagHandler
		{
			// Token: 0x0600468C RID: 18060 RVA: 0x006C84FC File Offset: 0x006C66FC
			TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
			{
				int num;
				if (!int.TryParse(text, out num) || num >= 26)
				{
					return new TextSnippet(text);
				}
				return new GlyphTagHandler.GlyphSnippet(num)
				{
					ForcedStyle = 1,
					DeleteWhole = true,
					Text = "[gp:" + num + "]"
				};
			}

			// Token: 0x0600468D RID: 18061 RVA: 0x0000357B File Offset: 0x0000177B
			public GlyphPSTagHandler()
			{
			}
		}

		// Token: 0x020008DB RID: 2267
		public class GlyphSwitchTagHandler : ITagHandler
		{
			// Token: 0x0600468E RID: 18062 RVA: 0x006C8550 File Offset: 0x006C6750
			TextSnippet ITagHandler.Parse(string text, Color baseColor, string options)
			{
				int num;
				if (!int.TryParse(text, out num) || num >= 26)
				{
					return new TextSnippet(text);
				}
				return new GlyphTagHandler.GlyphSnippet(num)
				{
					ForcedStyle = 2,
					DeleteWhole = true,
					Text = "[gn:" + num + "]"
				};
			}

			// Token: 0x0600468F RID: 18063 RVA: 0x0000357B File Offset: 0x0000177B
			public GlyphSwitchTagHandler()
			{
			}
		}

		// Token: 0x020008DC RID: 2268
		public class GlyphSnippet : TextSnippet
		{
			// Token: 0x06004690 RID: 18064 RVA: 0x006C85A2 File Offset: 0x006C67A2
			public GlyphSnippet(int index)
				: base("")
			{
				this._glyphIndex = index;
				this.Color = Color.White;
			}

			// Token: 0x06004691 RID: 18065 RVA: 0x006C85C8 File Offset: 0x006C67C8
			public GlyphSnippet(string keyName)
				: base("")
			{
				GlyphTagHandler.GlyphIndexes.TryGetValue(keyName, out this._glyphIndex);
				this.Color = Color.White;
			}

			// Token: 0x06004692 RID: 18066 RVA: 0x006C85FC File Offset: 0x006C67FC
			public override bool UniqueDraw(bool justCheckingString, out Vector2 size, SpriteBatch spriteBatch, Vector2 position = default(Vector2), Color color = default(Color), float scale = 1f)
			{
				scale *= GlyphTagHandler.GlyphsScale;
				if (!justCheckingString && color != Color.Black)
				{
					int num = this.ForcedStyle;
					int num2;
					if (num == -1)
					{
						num2 = GlyphTagHandler.GlyphStyle;
						if (num2 == -1)
						{
							num = 0;
							GlyphTagHandler.GlyphStyle = 0;
						}
						else
						{
							num = GlyphTagHandler.GlyphStyle;
						}
					}
					int num3 = this._glyphIndex;
					num2 = this._glyphIndex;
					if (num2 == 25)
					{
						num3 = ((Main.GlobalTimeWrappedHourly % 0.6f < 0.3f) ? 17 : 18);
					}
					Texture2D value = TextureAssets.TextGlyph[0].Value;
					spriteBatch.Draw(value, position, new Rectangle?(value.Frame(25, 3, num3, num, 0, 0)), color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
				}
				size = new Vector2(26f) * scale;
				return true;
			}

			// Token: 0x0400738F RID: 29583
			public int ForcedStyle = -1;

			// Token: 0x04007390 RID: 29584
			private int _glyphIndex;
		}
	}
}
