using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000355 RID: 853
	public class SpawnConditionDecorativeOverlayInfoElement : IBestiaryInfoElement, IBestiaryBackgroundOverlayAndColorProvider
	{
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x00573A62 File Offset: 0x00571C62
		// (set) Token: 0x060028A3 RID: 10403 RVA: 0x00573A6A File Offset: 0x00571C6A
		public float DisplayPriority
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayPriority>k__BackingField = value;
			}
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x00573A73 File Offset: 0x00571C73
		public SpawnConditionDecorativeOverlayInfoElement(string overlayImagePath = null, Color? overlayColor = null)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x00573A89 File Offset: 0x00571C89
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath, 1);
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00573AA6 File Offset: 0x00571CA6
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x00076333 File Offset: 0x00074533
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			return null;
		}

		// Token: 0x0400515B RID: 20827
		private string _overlayImagePath;

		// Token: 0x0400515C RID: 20828
		private Color? _overlayColor;

		// Token: 0x0400515D RID: 20829
		[CompilerGenerated]
		private float <DisplayPriority>k__BackingField;
	}
}
