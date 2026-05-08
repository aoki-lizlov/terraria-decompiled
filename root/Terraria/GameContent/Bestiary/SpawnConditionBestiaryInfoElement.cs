using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000353 RID: 851
	public class SpawnConditionBestiaryInfoElement : FilterProviderInfoElement, IBestiaryBackgroundImagePathAndColorProvider, IBestiaryPrioritizedElement
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x005739B3 File Offset: 0x00571BB3
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x005739BB File Offset: 0x00571BBB
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

		// Token: 0x06002898 RID: 10392 RVA: 0x005739C4 File Offset: 0x00571BC4
		public SpawnConditionBestiaryInfoElement(string nameLanguageKey, int filterIconFrame, string backgroundImagePath = null, Color? backgroundColor = null)
			: base(nameLanguageKey, filterIconFrame)
		{
			this._backgroundImagePath = backgroundImagePath;
			this._backgroundColor = backgroundColor;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x005739DD File Offset: 0x00571BDD
		public Asset<Texture2D> GetBackgroundImage()
		{
			if (this._backgroundImagePath == null)
			{
				return null;
			}
			return Main.Assets.Request<Texture2D>(this._backgroundImagePath, 1);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x005739FA File Offset: 0x00571BFA
		public Color? GetBackgroundColor()
		{
			return this._backgroundColor;
		}

		// Token: 0x04005154 RID: 20820
		private string _backgroundImagePath;

		// Token: 0x04005155 RID: 20821
		private Color? _backgroundColor;

		// Token: 0x04005156 RID: 20822
		[CompilerGenerated]
		private float <OrderPriority>k__BackingField;
	}
}
