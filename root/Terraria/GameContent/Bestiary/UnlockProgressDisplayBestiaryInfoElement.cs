using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200035A RID: 858
	public class UnlockProgressDisplayBestiaryInfoElement : IBestiaryInfoElement
	{
		// Token: 0x060028B4 RID: 10420 RVA: 0x00573D50 File Offset: 0x00571F50
		public UnlockProgressDisplayBestiaryInfoElement(BestiaryUnlockProgressReport progressReport)
		{
			this._progressReport = progressReport;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x00573D60 File Offset: 0x00571F60
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uielement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7)
			{
				Width = new StyleDimension(-11f, 1f),
				Height = new StyleDimension(109f, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Left = new StyleDimension(3f, 0f)
			};
			uielement.PaddingLeft = 4f;
			uielement.PaddingRight = 4f;
			string text = Utils.PrettifyPercentDisplay((float)info.UnlockState / 4f, "P2");
			string text2 = string.Format("{0} Entry Collected", text);
			string text3 = Utils.PrettifyPercentDisplay(this._progressReport.CompletionPercent, "P2");
			string text4 = string.Format("{0} Bestiary Collected", text3);
			int num = 8;
			UIText uitext = new UIText(text2, 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true,
				PaddingTop = (float)(-(float)num),
				PaddingBottom = (float)(-(float)num)
			};
			UIText uitext2 = new UIText(text4, 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true,
				PaddingTop = (float)(-(float)num),
				PaddingBottom = (float)(-(float)num)
			};
			this._text1 = uitext;
			this._text2 = uitext2;
			this.AddDynamicResize(uielement, uitext);
			uielement.Append(uitext);
			uielement.Append(uitext2);
			return uielement;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x00573F3C File Offset: 0x0057213C
		private void AddDynamicResize(UIElement container, UIText text)
		{
			text.OnInternalTextChange += delegate
			{
				container.Height = new StyleDimension(this._text1.MinHeight.Pixels + 4f + this._text2.MinHeight.Pixels, 0f);
				this._text2.Top = new StyleDimension(this._text1.MinHeight.Pixels + 4f, 0f);
			};
		}

		// Token: 0x04005161 RID: 20833
		private BestiaryUnlockProgressReport _progressReport;

		// Token: 0x04005162 RID: 20834
		private UIElement _text1;

		// Token: 0x04005163 RID: 20835
		private UIElement _text2;

		// Token: 0x020008C0 RID: 2240
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06004622 RID: 17954 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06004623 RID: 17955 RVA: 0x006C65F0 File Offset: 0x006C47F0
			internal void <AddDynamicResize>b__0()
			{
				this.container.Height = new StyleDimension(this.<>4__this._text1.MinHeight.Pixels + 4f + this.<>4__this._text2.MinHeight.Pixels, 0f);
				this.<>4__this._text2.Top = new StyleDimension(this.<>4__this._text1.MinHeight.Pixels + 4f, 0f);
			}

			// Token: 0x04007334 RID: 29492
			public UIElement container;

			// Token: 0x04007335 RID: 29493
			public UnlockProgressDisplayBestiaryInfoElement <>4__this;
		}
	}
}
