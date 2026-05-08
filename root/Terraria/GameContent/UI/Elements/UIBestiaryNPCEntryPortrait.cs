using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D7 RID: 983
	public class UIBestiaryNPCEntryPortrait : UIElement
	{
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x005A6E38 File Offset: 0x005A5038
		// (set) Token: 0x06002DDF RID: 11743 RVA: 0x005A6E40 File Offset: 0x005A5040
		public BestiaryEntry Entry
		{
			[CompilerGenerated]
			get
			{
				return this.<Entry>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Entry>k__BackingField = value;
			}
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x005A6E4C File Offset: 0x005A504C
		public UIBestiaryNPCEntryPortrait(BestiaryEntry entry, Asset<Texture2D> portraitBackgroundAsset, Color portraitColor, List<IBestiaryBackgroundOverlayAndColorProvider> overlays)
		{
			this.Entry = entry;
			this.Height.Set(112f, 0f);
			this.Width.Set(193f, 0f);
			base.SetPadding(0f);
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(-4f, 1f),
				Height = new StyleDimension(-4f, 1f),
				IgnoresMouseInteraction = true,
				OverflowHidden = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uielement.SetPadding(0f);
			if (portraitBackgroundAsset != null)
			{
				uielement.Append(new UIImage(portraitBackgroundAsset)
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					ScaleToFit = true,
					Width = new StyleDimension(0f, 1f),
					Height = new StyleDimension(0f, 1f),
					Color = portraitColor
				});
			}
			for (int i = 0; i < overlays.Count; i++)
			{
				Asset<Texture2D> backgroundOverlayImage = overlays[i].GetBackgroundOverlayImage();
				Color? backgroundOverlayColor = overlays[i].GetBackgroundOverlayColor();
				uielement.Append(new UIImage(backgroundOverlayImage)
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					ScaleToFit = true,
					Width = new StyleDimension(0f, 1f),
					Height = new StyleDimension(0f, 1f),
					Color = ((backgroundOverlayColor != null) ? backgroundOverlayColor.Value : Color.Lerp(Color.White, portraitColor, 0.5f))
				});
			}
			UIBestiaryEntryIcon uibestiaryEntryIcon = new UIBestiaryEntryIcon(entry, true);
			uielement.Append(uibestiaryEntryIcon);
			base.Append(uielement);
			base.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Portrait_Front", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			});
		}

		// Token: 0x0400550E RID: 21774
		[CompilerGenerated]
		private BestiaryEntry <Entry>k__BackingField;
	}
}
