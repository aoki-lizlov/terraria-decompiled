using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200039E RID: 926
	public class UITextWrappingTest : UIState
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x005839D0 File Offset: 0x00581BD0
		public UITextWrappingTest()
		{
			UIPanel uipanel = new UIPanel
			{
				Top = StyleDimension.FromPixels(100f),
				Left = StyleDimension.FromPixelsAndPercent(-400f, 0.5f),
				Width = StyleDimension.FromPixels(300f),
				Height = StyleDimension.FromPixels(40f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent
			};
			this.modeText = new UIText(this.mode.ToString(), 0.8f, false)
			{
				TextOriginX = 0f,
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPercent(1f)
			};
			uipanel.Append(this.modeText);
			uipanel.OnLeftClick += delegate(UIMouseEvent e, UIElement sender)
			{
				this.CycleMode(1);
			};
			uipanel.OnRightClick += delegate(UIMouseEvent e, UIElement sender)
			{
				this.CycleMode(-1);
			};
			base.Append(uipanel);
			this.scaleText = new UIText(this.ScaleText, 0.8f, false)
			{
				TextOriginX = 0f,
				Top = StyleDimension.FromPixels(150f),
				Left = StyleDimension.FromPixelsAndPercent(-400f, 0.5f),
				Width = StyleDimension.FromPixels(300f),
				Height = StyleDimension.FromPixels(40f)
			};
			base.Append(this.scaleText);
			this.langText = new UIText(this.LangText, 0.8f, false)
			{
				TextOriginX = 1f,
				HAlign = 1f,
				Top = StyleDimension.FromPixels(150f),
				Left = StyleDimension.FromPixelsAndPercent(400f, -0.5f),
				Width = StyleDimension.FromPixels(300f),
				Height = StyleDimension.FromPixels(40f)
			};
			base.Append(this.langText);
			UIList uilist = new UIList();
			uilist.Top = StyleDimension.FromPixels(200f);
			uilist.Left = StyleDimension.FromPixelsAndPercent(-400f, 0.5f);
			uilist.Width = StyleDimension.FromPixels(300f);
			uilist.Height = StyleDimension.FromPixelsAndPercent(-200f, 1f);
			uilist.ListPadding = 5f;
			uilist.ManualSortMethod = delegate(List<UIElement> _)
			{
			};
			this.list = uilist;
			this.list.SetPadding(0f);
			base.Append(this.list);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(-20f, 1f);
			uiscrollbar.HAlign = 1f;
			uiscrollbar.VAlign = 0.5f;
			uiscrollbar.Left.Set(6f, 0f);
			this.list.SetScrollbar(uiscrollbar);
			this.ResetList();
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x00583CD1 File Offset: 0x00581ED1
		private string ScaleText
		{
			get
			{
				return "Up/Down to change scale. Current: " + this.scale + "%";
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x00583CED File Offset: 0x00581EED
		private string LangText
		{
			get
			{
				return "Current Language: " + Language.ActiveCulture.CultureInfo.DisplayName;
			}
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00583D08 File Offset: 0x00581F08
		private void CycleMode(int offset)
		{
			int length = Enum.GetValues(typeof(UITextWrappingTest.Mode)).Length;
			this.mode = (this.mode + offset + length) % (UITextWrappingTest.Mode)length;
			this.ResetList();
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x00583D44 File Offset: 0x00581F44
		private void ResetList()
		{
			this.modeText.SetText(this.mode.ToString());
			this.list.Clear();
			this.list.Add(this.MakeElement("A test string in english.\nSecond line.\n\n^ Double line break\nLooooooooooooooonglinewithnospaces"));
			this.list.Add(this.MakeElement("Ends with newline\n"));
			this.list.Add(this.MakeElement("Non-breaking space: с\u00a0микротранзакциями\n"));
			this.list.Add(this.MakeElement("Thin\u2009Space\nHair\u200aSpace\nZero\u200bWidth\u200bSpace"));
			this.list.Add(this.NewSeparator());
			this.list.Add(this.MakeElement("せいなる スライムが がったいして できた 生き物。ごうまんで 力づよく きらめく けっしょうに おおわれている。つばさが 生える という うわさも ある。"));
			this.list.Add(this.MakeElement("정화된 슬라임들이 모두 통합되어, 눈부신 수정으로 장식된 거만하고 압도적인 힘이 되었습니다. 날개가 돋아난다는 소문도 있습니다. "));
			this.list.Add(this.MakeElement("Святые слизни объединяются в величественную всесокрушающую массу, украшенную превосходными кристаллами. Говорят, она даже может отрастить крылья."));
			this.list.Add(this.MakeElement("神圣史莱姆合并成了一种高傲的粉碎性力量，这种力量佩戴着闪耀的水晶。传说她会长出翅膀。"));
			this.list.Add(this.MakeElement("神聖史萊姆融合後，會點綴著閃耀的水晶，擁有傲視一切的粉碎性力量。傳說她會長出翅膀。"));
			this.list.Add(this.NewSeparator());
			this.list.Add(this.MakeElement("fullwidth terminators。bang！comma，fullstop。rcomma、colon：question？"));
			this.list.Add(this.MakeElement("Chinese separation〈聖聖聖聖〉《聖聖》「聖聖」『聖聖』【聖聖〔聖聖】〖聖聖〗!%),.:;?]}$100,25.24%"));
			this.list.Add(this.NewSeparator());
			this.list.Add(this.MakeElement(new LocalizedText("", "Keybind glyph support {InputTrigger_UseOrAttack} and {InputTrigger_InteractWithTile}").Value));
			this.list.Add(this.MakeElement("[c/FF0000:SomeRedText] [c/00FF00:SomeGreenText] [c/0000FF:SomeBlueText]"));
			this.list.Add(this.MakeElement("[c/FF0000:SomeRedText][c/00FF00:SomeGreenText][c/0000FF:SomeBlueText]"));
			this.list.Add(this.MakeElement("[c/0000FF:Long colored text, with escaped square brackets [\\] inside]"));
			this.list.Add(this.MakeElement("Items[i:1][i:2][i:3][i:4][i:5][i:6][i:7][i:100][i:1000]"));
			this.list.Add(this.MakeElement("ItemsOnSeparateLines\n[i:1]\n[i:2]\n[i:3]"));
			this.list.Add(this.MakeElement("Items and text [i:1] then stuff [i:2] and some more [i:3] etc"));
			this.list.Add(this.MakeElement("nospacebetweenitems[i:6]andtext[i:7]nospacebetweenitems[i:8]andtext[i:9]"));
			this.list.Add(this.MakeElement("[g:0][g:1][g:2][g:3][g:4][g:5][g:6][g:7][g:8][g:9][g:10][g:11][g:12][g:13][g:14][g:15][g:16][g:17][g:18][g:19][g:20][g:21][g:22][g:23][g:24][g:25]"));
			this.list.Add(this.MakeElement(Language.GetTextValue("Achievements.Completed", "[a:TRANSMUTE_ITEM]")));
			this.list.Add(this.MakeElement("[a:TO_INFINITY_AND_BEYOND][a:PURIFY_ENTIRE_WORLD][a:TO_INFINITY_AND_BEYOND][a:TRANSMUTE_ITEM][a:OBTAIN_HAMMER][a:BENCHED][a:HEAVY_METAL][a:GET_GOLDEN_DELIGHT][a:MINER_FOR_FIRE][a:HEAD_IN_THE_CLOUDS][a:GET_TERRASPARK_BOOTS]"));
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x00583FA8 File Offset: 0x005821A8
		private UIElement NewSeparator()
		{
			return new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Color = new Color(89, 116, 213, 255) * 0.9f
			};
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00583FF4 File Offset: 0x005821F4
		private UIElement MakeElement(string value)
		{
			UIElement container = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixels((float)(50 * this.scale)),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent
			};
			container.SetPadding(UITextWrappingTest.TextPadding);
			if (this.mode == UITextWrappingTest.Mode.UIText)
			{
				UIText text3 = new UIText(value, (float)this.scale / 100f, false)
				{
					TextOriginX = 0f,
					HAlign = 0f,
					VAlign = 0f,
					Width = StyleDimension.FromPercent(1f),
					Height = StyleDimension.FromPercent(1f),
					IsWrapped = true
				};
				text3.OnInternalTextChange += delegate
				{
					container.Height = new StyleDimension(text3.MinHeight.Pixels, 0f);
				};
				container.Append(text3);
			}
			else
			{
				UITextWrappingTest.TestElement text = new UITextWrappingTest.TestElement(value, (float)this.scale / 100f, this.mode)
				{
					Width = StyleDimension.FromPercent(1f)
				};
				UITextWrappingTest.TestElement text2 = text;
				text2.OnHeightUpdate = (Action)Delegate.Combine(text2.OnHeightUpdate, new Action(delegate
				{
					container.Height = new StyleDimension(text.MinHeight.Pixels + container.PaddingTop + container.PaddingBottom, 0f);
				}));
				container.Append(text);
			}
			return container;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0058418C File Offset: 0x0058238C
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			CalculatedStyle dimensions = this.list.GetDimensions();
			int num = (int)(dimensions.X + UITextWrappingTest.TextPadding);
			int num2 = (int)(dimensions.X + dimensions.Width - UITextWrappingTest.TextPadding);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(num, (int)dimensions.Y, 1, (int)dimensions.Height), Color.Green);
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(num2, (int)dimensions.Y, 1, (int)dimensions.Height), Color.Green);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00584224 File Offset: 0x00582424
		public override void Update(GameTime gameTime)
		{
			if (Main.keyState.IsKeyDown(Keys.Escape))
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 0;
			}
			int num = 0;
			if (Main.keyState.IsKeyDown(Keys.Down) && Main.oldKeyState.IsKeyUp(Keys.Down))
			{
				num = -10;
			}
			if (Main.keyState.IsKeyDown(Keys.Up) && Main.oldKeyState.IsKeyUp(Keys.Up))
			{
				num = 10;
			}
			if (num != 0)
			{
				this.scale = Utils.Clamp<int>(this.scale + num, 50, 150);
				this.ResetList();
				this.scaleText.SetText(this.ScaleText);
			}
			this.langText.SetText(this.LangText);
			if (Main.mouseLeft)
			{
				Point point = Main.MouseScreen.ToPoint();
				CalculatedStyle dimensions = this.list.GetDimensions();
				if ((float)point.X > dimensions.X && (float)point.Y > dimensions.Y)
				{
					this.list.Width = StyleDimension.FromPixels((float)point.X - dimensions.X);
				}
			}
			base.Update(gameTime);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00584340 File Offset: 0x00582540
		// Note: this type is marked as 'beforefieldinit'.
		static UITextWrappingTest()
		{
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x0058434C File Offset: 0x0058254C
		[CompilerGenerated]
		private void <.ctor>b__8_0(UIMouseEvent e, UIElement sender)
		{
			this.CycleMode(1);
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x00584355 File Offset: 0x00582555
		[CompilerGenerated]
		private void <.ctor>b__8_1(UIMouseEvent e, UIElement sender)
		{
			this.CycleMode(-1);
		}

		// Token: 0x0400530D RID: 21261
		private static readonly float TextPadding = 12f;

		// Token: 0x0400530E RID: 21262
		private UIList list;

		// Token: 0x0400530F RID: 21263
		private UIText modeText;

		// Token: 0x04005310 RID: 21264
		private UIText scaleText;

		// Token: 0x04005311 RID: 21265
		private UIText langText;

		// Token: 0x04005312 RID: 21266
		private UITextWrappingTest.Mode mode;

		// Token: 0x04005313 RID: 21267
		private int scale = 100;

		// Token: 0x020008EF RID: 2287
		private enum Mode
		{
			// Token: 0x040073EA RID: 29674
			UIText,
			// Token: 0x040073EB RID: 29675
			SignsAndNPCChat,
			// Token: 0x040073EC RID: 29676
			WordwrapStringLegacy,
			// Token: 0x040073ED RID: 29677
			DrawColorCodedStringWithShadow,
			// Token: 0x040073EE RID: 29678
			DrawColorCodedStringLegacy,
			// Token: 0x040073EF RID: 29679
			MultilineChat
		}

		// Token: 0x020008F0 RID: 2288
		private class TestElement : UIElement
		{
			// Token: 0x0600471C RID: 18204 RVA: 0x006CAEF0 File Offset: 0x006C90F0
			public TestElement(string text, float scale, UITextWrappingTest.Mode mode)
			{
				this.text = text;
				this.scale = scale;
				this.mode = mode;
			}

			// Token: 0x0600471D RID: 18205 RVA: 0x006CAF10 File Offset: 0x006C9110
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				Vector2 vector = base.GetDimensions().Position();
				float num = base.GetInnerDimensions().Width;
				if (num <= 0f)
				{
					num = 1000f;
				}
				switch (this.mode)
				{
				case UITextWrappingTest.Mode.SignsAndNPCChat:
				{
					int num2;
					string[] array = Utils.WordwrapString(this.text, FontAssets.MouseText.Value, (int)(num / this.scale), 10, out num2);
					float num3 = 30f * this.scale;
					this.MinHeight.Set((float)num2 * num3, 0f);
					this.OnHeightUpdate();
					for (int i = 0; i < num2; i++)
					{
						Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, array[i], vector.X, vector.Y + (float)i * num3, Color.White, Color.Black, Vector2.Zero, this.scale);
					}
					return;
				}
				case UITextWrappingTest.Mode.WordwrapStringLegacy:
				{
					int num4;
					string[] array2 = Utils.WordwrapStringLegacy(this.text, FontAssets.MouseText.Value, (int)(num / this.scale), 10, out num4);
					float num5 = 30f * this.scale;
					this.MinHeight.Set((float)num4 * num5, 0f);
					this.OnHeightUpdate();
					for (int j = 0; j < num4; j++)
					{
						Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, array2[j], vector.X, vector.Y + (float)j * num5, Color.White, Color.Black, Vector2.Zero, this.scale);
					}
					return;
				}
				case UITextWrappingTest.Mode.DrawColorCodedStringWithShadow:
				{
					ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, this.text, vector, Color.White, 0f, Vector2.Zero, new Vector2(this.scale), num, 2f);
					Vector2 stringSize = ChatManager.GetStringSize(FontAssets.MouseText.Value, this.text, new Vector2(this.scale), num);
					this.MinHeight.Set(stringSize.Y, 0f);
					this.OnHeightUpdate();
					return;
				}
				case UITextWrappingTest.Mode.DrawColorCodedStringLegacy:
				{
					ChatManager.DrawColorCodedStringShadow(spriteBatch, FontAssets.MouseText.Value, this.text, vector, Color.Black, 0f, Vector2.Zero, new Vector2(this.scale), num, 2f);
					ChatManager.DrawColorCodedString(spriteBatch, FontAssets.MouseText.Value, this.text, vector, Color.White, 0f, Vector2.Zero, new Vector2(this.scale), num, false);
					Vector2 stringSize2 = ChatManager.GetStringSize(FontAssets.MouseText.Value, this.text, new Vector2(this.scale), num);
					this.MinHeight.Set(stringSize2.Y, 0f);
					this.OnHeightUpdate();
					return;
				}
				case UITextWrappingTest.Mode.MultilineChat:
				{
					List<List<TextSnippet>> list = Utils.WordwrapStringSmart(this.text, Color.White, FontAssets.MouseText.Value, (float)((int)(num / this.scale)), 10);
					float num6 = 30f * this.scale;
					this.MinHeight.Set((float)list.Count * num6, 0f);
					this.OnHeightUpdate();
					for (int k = 0; k < list.Count; k++)
					{
						int num7;
						ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, list[k].ToArray(), vector + new Vector2(0f, (float)k * num6), 0f, Vector2.Zero, new Vector2(this.scale), out num7, -1f, 2f);
					}
					return;
				}
				default:
					return;
				}
			}

			// Token: 0x040073F0 RID: 29680
			private readonly string text;

			// Token: 0x040073F1 RID: 29681
			private readonly float scale;

			// Token: 0x040073F2 RID: 29682
			private readonly UITextWrappingTest.Mode mode;

			// Token: 0x040073F3 RID: 29683
			public Action OnHeightUpdate;
		}

		// Token: 0x020008F1 RID: 2289
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600471E RID: 18206 RVA: 0x006CB2A3 File Offset: 0x006C94A3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600471F RID: 18207 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004720 RID: 18208 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__8_2(List<UIElement> _)
			{
			}

			// Token: 0x040073F4 RID: 29684
			public static readonly UITextWrappingTest.<>c <>9 = new UITextWrappingTest.<>c();

			// Token: 0x040073F5 RID: 29685
			public static Action<List<UIElement>> <>9__8_2;
		}

		// Token: 0x020008F2 RID: 2290
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06004721 RID: 18209 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x040073F6 RID: 29686
			public UIElement container;
		}

		// Token: 0x020008F3 RID: 2291
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_1
		{
			// Token: 0x06004722 RID: 18210 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_1()
			{
			}

			// Token: 0x06004723 RID: 18211 RVA: 0x006CB2AF File Offset: 0x006C94AF
			internal void <MakeElement>b__0()
			{
				this.CS$<>8__locals1.container.Height = new StyleDimension(this.text.MinHeight.Pixels, 0f);
			}

			// Token: 0x040073F7 RID: 29687
			public UIText text;

			// Token: 0x040073F8 RID: 29688
			public UITextWrappingTest.<>c__DisplayClass16_0 CS$<>8__locals1;
		}

		// Token: 0x020008F4 RID: 2292
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_2
		{
			// Token: 0x06004724 RID: 18212 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass16_2()
			{
			}

			// Token: 0x06004725 RID: 18213 RVA: 0x006CB2DC File Offset: 0x006C94DC
			internal void <MakeElement>b__1()
			{
				this.CS$<>8__locals2.container.Height = new StyleDimension(this.text.MinHeight.Pixels + this.CS$<>8__locals2.container.PaddingTop + this.CS$<>8__locals2.container.PaddingBottom, 0f);
			}

			// Token: 0x040073F9 RID: 29689
			public UITextWrappingTest.TestElement text;

			// Token: 0x040073FA RID: 29690
			public UITextWrappingTest.<>c__DisplayClass16_0 CS$<>8__locals2;
		}
	}
}
