using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000359 RID: 857
	public class FlavorTextBestiaryInfoElement : IBestiaryInfoElement
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x00573BF9 File Offset: 0x00571DF9
		public FlavorTextBestiaryInfoElement(string languageKey)
		{
			this._key = languageKey;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00573C08 File Offset: 0x00571E08
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2)
			{
				return null;
			}
			UIPanel uipanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7);
			uipanel.Width = new StyleDimension(-11f, 1f);
			uipanel.Height = new StyleDimension(109f, 0f);
			uipanel.BackgroundColor = new Color(43, 56, 101);
			uipanel.BorderColor = Color.Transparent;
			uipanel.Left = new StyleDimension(3f, 0f);
			uipanel.PaddingLeft = 4f;
			uipanel.PaddingRight = 4f;
			UIText uitext = new UIText(Language.GetText(this._key), 0.8f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				IsWrapped = true
			};
			FlavorTextBestiaryInfoElement.AddDynamicResize(uipanel, uitext);
			uipanel.Append(uitext);
			return uipanel;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x00573D18 File Offset: 0x00571F18
		private static void AddDynamicResize(UIElement container, UIText text)
		{
			text.OnInternalTextChange += delegate
			{
				container.Height = new StyleDimension(text.MinHeight.Pixels, 0f);
			};
		}

		// Token: 0x04005160 RID: 20832
		private string _key;

		// Token: 0x020008BF RID: 2239
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06004620 RID: 17952 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06004621 RID: 17953 RVA: 0x006C65C9 File Offset: 0x006C47C9
			internal void <AddDynamicResize>b__0()
			{
				this.container.Height = new StyleDimension(this.text.MinHeight.Pixels, 0f);
			}

			// Token: 0x04007332 RID: 29490
			public UIElement container;

			// Token: 0x04007333 RID: 29491
			public UIText text;
		}
	}
}
