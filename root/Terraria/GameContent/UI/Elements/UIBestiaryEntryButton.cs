using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D2 RID: 978
	public class UIBestiaryEntryButton : UIElement
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x005A542A File Offset: 0x005A362A
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x005A5432 File Offset: 0x005A3632
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

		// Token: 0x06002DB1 RID: 11697 RVA: 0x005A543C File Offset: 0x005A363C
		public UIBestiaryEntryButton(BestiaryEntry entry, bool isAPrettyPortrait)
		{
			this.Entry = entry;
			this.Height.Set(72f, 0f);
			this.Width.Set(72f, 0f);
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
			uielement.Append(new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Back", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f
			});
			if (isAPrettyPortrait)
			{
				Asset<Texture2D> asset = this.TryGettingBackgroundImageProvider(entry);
				if (asset != null)
				{
					uielement.Append(new UIImage(asset)
					{
						HAlign = 0.5f,
						VAlign = 0.5f
					});
				}
			}
			UIBestiaryEntryIcon uibestiaryEntryIcon = new UIBestiaryEntryIcon(entry, isAPrettyPortrait);
			uielement.Append(uibestiaryEntryIcon);
			base.Append(uielement);
			this._icon = uibestiaryEntryIcon;
			int? num = this.TryGettingDisplayIndex(entry);
			if (num != null)
			{
				UIText uitext = new UIText(num.Value.ToString(), 0.9f, false)
				{
					Top = new StyleDimension(10f, 0f),
					Left = new StyleDimension(10f, 0f),
					IgnoresMouseInteraction = true
				};
				base.Append(uitext);
			}
			this._bordersGlow = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Selection", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			this._bordersOverlay = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Overlay", 1))
			{
				VAlign = 0.5f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true,
				Color = Color.White * 0.6f
			};
			base.Append(this._bordersOverlay);
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Slot_Front", 1));
			uiimage.VAlign = 0.5f;
			uiimage.HAlign = 0.5f;
			uiimage.IgnoresMouseInteraction = true;
			base.Append(uiimage);
			this._borders = uiimage;
			if (isAPrettyPortrait)
			{
				base.RemoveChild(this._bordersOverlay);
			}
			if (!isAPrettyPortrait)
			{
				base.OnMouseOver += this.MouseOver;
				base.OnMouseOut += this.MouseOut;
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x005A56F4 File Offset: 0x005A38F4
		private Asset<Texture2D> TryGettingBackgroundImageProvider(BestiaryEntry entry)
		{
			IEnumerable<IBestiaryBackgroundImagePathAndColorProvider> enumerable = from x in entry.Info
				where x is IBestiaryBackgroundImagePathAndColorProvider
				select x as IBestiaryBackgroundImagePathAndColorProvider;
			IEnumerable<IPreferenceProviderElement> preferences = entry.Info.OfType<IPreferenceProviderElement>();
			foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider in enumerable.Where((IBestiaryBackgroundImagePathAndColorProvider provider) => preferences.Any((IPreferenceProviderElement preference) => preference.Matches(provider))))
			{
				Asset<Texture2D> asset = bestiaryBackgroundImagePathAndColorProvider.GetBackgroundImage();
				if (asset != null)
				{
					return asset;
				}
			}
			foreach (IBestiaryBackgroundImagePathAndColorProvider bestiaryBackgroundImagePathAndColorProvider2 in enumerable)
			{
				Asset<Texture2D> asset = bestiaryBackgroundImagePathAndColorProvider2.GetBackgroundImage();
				if (asset != null)
				{
					return asset;
				}
			}
			return null;
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x005A57FC File Offset: 0x005A39FC
		private int? TryGettingDisplayIndex(BestiaryEntry entry)
		{
			int? num = null;
			IBestiaryInfoElement bestiaryInfoElement = entry.Info.FirstOrDefault((IBestiaryInfoElement x) => x is IBestiaryEntryDisplayIndex);
			if (bestiaryInfoElement != null)
			{
				num = new int?((bestiaryInfoElement as IBestiaryEntryDisplayIndex).BestiaryDisplayIndex);
			}
			return num;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x005A5854 File Offset: 0x005A3A54
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (base.IsMouseHovering)
			{
				Main.instance.MouseText(this._icon.GetHoverText(), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x005A5888 File Offset: 0x005A3A88
		private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.RemoveChild(this._borders);
			base.RemoveChild(this._bordersGlow);
			base.RemoveChild(this._bordersOverlay);
			base.Append(this._borders);
			base.Append(this._bordersGlow);
			this._icon.ForceHover = true;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x005A58F4 File Offset: 0x005A3AF4
		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			base.RemoveChild(this._borders);
			base.RemoveChild(this._bordersGlow);
			base.RemoveChild(this._bordersOverlay);
			base.Append(this._bordersOverlay);
			base.Append(this._borders);
			this._icon.ForceHover = false;
		}

		// Token: 0x040054F2 RID: 21746
		[CompilerGenerated]
		private BestiaryEntry <Entry>k__BackingField;

		// Token: 0x040054F3 RID: 21747
		private UIImage _bordersGlow;

		// Token: 0x040054F4 RID: 21748
		private UIImage _bordersOverlay;

		// Token: 0x040054F5 RID: 21749
		private UIImage _borders;

		// Token: 0x040054F6 RID: 21750
		private UIBestiaryEntryIcon _icon;

		// Token: 0x02000927 RID: 2343
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x060047F4 RID: 18420 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060047F5 RID: 18421 RVA: 0x006CC7B8 File Offset: 0x006CA9B8
			internal bool <TryGettingBackgroundImageProvider>b__2(IBestiaryBackgroundImagePathAndColorProvider provider)
			{
				UIBestiaryEntryButton.<>c__DisplayClass9_1 CS$<>8__locals1 = new UIBestiaryEntryButton.<>c__DisplayClass9_1();
				CS$<>8__locals1.provider = provider;
				return this.preferences.Any((IPreferenceProviderElement preference) => preference.Matches(CS$<>8__locals1.provider));
			}

			// Token: 0x040074F7 RID: 29943
			public IEnumerable<IPreferenceProviderElement> preferences;
		}

		// Token: 0x02000928 RID: 2344
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_1
		{
			// Token: 0x060047F6 RID: 18422 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass9_1()
			{
			}

			// Token: 0x060047F7 RID: 18423 RVA: 0x006CC7E9 File Offset: 0x006CA9E9
			internal bool <TryGettingBackgroundImageProvider>b__3(IPreferenceProviderElement preference)
			{
				return preference.Matches(this.provider);
			}

			// Token: 0x040074F8 RID: 29944
			public IBestiaryBackgroundImagePathAndColorProvider provider;
		}

		// Token: 0x02000929 RID: 2345
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060047F8 RID: 18424 RVA: 0x006CC7F7 File Offset: 0x006CA9F7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060047F9 RID: 18425 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060047FA RID: 18426 RVA: 0x006CC803 File Offset: 0x006CAA03
			internal bool <TryGettingBackgroundImageProvider>b__9_0(IBestiaryInfoElement x)
			{
				return x is IBestiaryBackgroundImagePathAndColorProvider;
			}

			// Token: 0x060047FB RID: 18427 RVA: 0x006CC80E File Offset: 0x006CAA0E
			internal IBestiaryBackgroundImagePathAndColorProvider <TryGettingBackgroundImageProvider>b__9_1(IBestiaryInfoElement x)
			{
				return x as IBestiaryBackgroundImagePathAndColorProvider;
			}

			// Token: 0x060047FC RID: 18428 RVA: 0x006CC816 File Offset: 0x006CAA16
			internal bool <TryGettingDisplayIndex>b__10_0(IBestiaryInfoElement x)
			{
				return x is IBestiaryEntryDisplayIndex;
			}

			// Token: 0x040074F9 RID: 29945
			public static readonly UIBestiaryEntryButton.<>c <>9 = new UIBestiaryEntryButton.<>c();

			// Token: 0x040074FA RID: 29946
			public static Func<IBestiaryInfoElement, bool> <>9__9_0;

			// Token: 0x040074FB RID: 29947
			public static Func<IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider> <>9__9_1;

			// Token: 0x040074FC RID: 29948
			public static Func<IBestiaryInfoElement, bool> <>9__10_0;
		}
	}
}
