using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000354 RID: 852
	public class SpawnConditionBestiaryOverlayInfoElement : FilterProviderInfoElement, IBestiaryBackgroundOverlayAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x00573A02 File Offset: 0x00571C02
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x00573A0A File Offset: 0x00571C0A
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x00573A13 File Offset: 0x00571C13
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x00573A1B File Offset: 0x00571C1B
		public float OrderPriority
		{
			[CompilerGenerated]
			get
			{
				return this.<OrderPriority>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OrderPriority>k__BackingField = value;
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00573A24 File Offset: 0x00571C24
		public SpawnConditionBestiaryOverlayInfoElement(string nameLanguageKey, int filterIconFrame, string overlayImagePath = null, Color? overlayColor = null)
			: base(nameLanguageKey, filterIconFrame)
		{
			this._overlayImagePath = overlayImagePath;
			this._overlayColor = overlayColor;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00573A3D File Offset: 0x00571C3D
		public Asset<Texture2D> GetBackgroundOverlayImage()
		{
			if (this._overlayImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._overlayImagePath, 1);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x00573A5A File Offset: 0x00571C5A
		public Color? GetBackgroundOverlayColor()
		{
			return this._overlayColor;
		}

		// Token: 0x04005157 RID: 20823
		private string _overlayImagePath;

		// Token: 0x04005158 RID: 20824
		private Color? _overlayColor;

		// Token: 0x04005159 RID: 20825
		[CompilerGenerated]
		private float <DisplayPriority>k__BackingField;

		// Token: 0x0400515A RID: 20826
		[CompilerGenerated]
		private float <OrderPriority>k__BackingField;
	}
}
