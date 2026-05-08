using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200034E RID: 846
	public class FilterProviderInfoElement : IFilterInfoProvider, IProvideSearchFilterString, IBestiaryInfoElement
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x005736A9 File Offset: 0x005718A9
		// (set) Token: 0x0600287D RID: 10365 RVA: 0x005736B1 File Offset: 0x005718B1
		public int DisplayTextPriority
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayTextPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayTextPriority>k__BackingField = value;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x005736BA File Offset: 0x005718BA
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x005736C2 File Offset: 0x005718C2
		public bool HideInPortraitInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<HideInPortraitInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HideInPortraitInfo>k__BackingField = value;
			}
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x005736CB File Offset: 0x005718CB
		public FilterProviderInfoElement(string nameLanguageKey, int filterIconFrame)
		{
			this._key = nameLanguageKey;
			this._filterIconFrame.X = filterIconFrame % 16;
			this._filterIconFrame.Y = filterIconFrame / 16;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x005736F8 File Offset: 0x005718F8
		public UIElement GetFilterImage()
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
			return new UIImageFramed(asset, asset.Frame(16, 5, this._filterIconFrame.X, this._filterIconFrame.Y, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x00573751 File Offset: 0x00571951
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0057376D File Offset: 0x0057196D
		public string GetDisplayNameKey()
		{
			return this._key;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x00573778 File Offset: 0x00571978
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			if (this.HideInPortraitInfo)
			{
				return null;
			}
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			UIElement uielement = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", 1), null, 12, 7)
			{
				Width = new StyleDimension(-14f, 1f),
				Height = new StyleDimension(34f, 0f),
				BackgroundColor = new Color(43, 56, 101),
				BorderColor = Color.Transparent,
				Left = new StyleDimension(5f, 0f)
			};
			uielement.SetPadding(0f);
			uielement.PaddingRight = 5f;
			UIElement filterImage = this.GetFilterImage();
			filterImage.HAlign = 0f;
			filterImage.Left = new StyleDimension(5f, 0f);
			UIText uitext = new UIText(Language.GetText(this.GetDisplayNameKey()), 0.8f, false)
			{
				HAlign = 0f,
				PaddingLeft = 38f,
				Width = StyleDimension.FromPercent(1f),
				TextOriginX = 0f,
				TextOriginY = 0f,
				VAlign = 0.5f,
				DynamicallyScaleDownToWidth = true
			};
			if (filterImage != null)
			{
				uielement.Append(filterImage);
			}
			uielement.Append(uitext);
			this.AddOnHover(uielement);
			return uielement;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x005738C9 File Offset: 0x00571AC9
		private void AddOnHover(UIElement button)
		{
			button.OnUpdate += delegate(UIElement e)
			{
				this.ShowButtonName(e);
			};
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x005738E0 File Offset: 0x00571AE0
		private void ShowButtonName(UIElement element)
		{
			if (!element.IsMouseHovering)
			{
				return;
			}
			string textValue = Language.GetTextValue(this.GetDisplayNameKey());
			Main.instance.MouseText(textValue, 0, 0, -1, -1, -1, -1, 0);
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x00573914 File Offset: 0x00571B14
		[CompilerGenerated]
		private void <AddOnHover>b__17_0(UIElement e)
		{
			this.ShowButtonName(e);
		}

		// Token: 0x0400514B RID: 20811
		private const int framesPerRow = 16;

		// Token: 0x0400514C RID: 20812
		private const int framesPerColumn = 5;

		// Token: 0x0400514D RID: 20813
		private Point _filterIconFrame;

		// Token: 0x0400514E RID: 20814
		private string _key;

		// Token: 0x0400514F RID: 20815
		[CompilerGenerated]
		private int <DisplayTextPriority>k__BackingField;

		// Token: 0x04005150 RID: 20816
		[CompilerGenerated]
		private bool <HideInPortraitInfo>k__BackingField;
	}
}
