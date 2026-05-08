using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F8 RID: 1016
	public class UIResourcePack : UIPanel
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x005AE05C File Offset: 0x005AC25C
		// (set) Token: 0x06002EBE RID: 11966 RVA: 0x005AE064 File Offset: 0x005AC264
		public int Order
		{
			[CompilerGenerated]
			get
			{
				return this.<Order>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Order>k__BackingField = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x005AE06D File Offset: 0x005AC26D
		// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x005AE075 File Offset: 0x005AC275
		public UIElement ContentPanel
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentPanel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContentPanel>k__BackingField = value;
			}
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x005AE080 File Offset: 0x005AC280
		public UIResourcePack(ResourcePack pack, int order)
		{
			this.ResourcePack = pack;
			this.Order = order;
			this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
			this.BorderColor = UIResourcePack.DefaultBorderColor;
			this.Height = StyleDimension.FromPixels(102f);
			this.MinHeight = this.Height;
			this.MaxHeight = this.Height;
			this.MinWidth = StyleDimension.FromPixels(102f);
			this.Width = StyleDimension.FromPercent(1f);
			base.SetPadding(5f);
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1);
			this.OverflowHidden = true;
			this.BuildChildren();
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x005AE130 File Offset: 0x005AC330
		private void BuildChildren()
		{
			StyleDimension styleDimension = StyleDimension.FromPixels(77f);
			StyleDimension styleDimension2 = StyleDimension.FromPixels(4f);
			UIText uitext = new UIText(this.ResourcePack.Name, 1f, false)
			{
				Left = styleDimension,
				Top = styleDimension2
			};
			base.Append(uitext);
			styleDimension2.Pixels += uitext.GetOuterDimensions().Height + 6f;
			UIText uitext2 = new UIText(Language.GetTextValue("UI.Author", this.ResourcePack.Author), 0.7f, false)
			{
				Left = styleDimension,
				Top = styleDimension2
			};
			base.Append(uitext2);
			styleDimension2.Pixels += uitext2.GetOuterDimensions().Height + 10f;
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			UIImage uiimage = new UIImage(asset)
			{
				Left = StyleDimension.FromPixels(72f),
				Top = styleDimension2,
				Height = StyleDimension.FromPixels((float)asset.Height()),
				Width = StyleDimension.FromPixelsAndPercent(-80f, 1f),
				ScaleToFit = true
			};
			this.Recalculate();
			base.Append(uiimage);
			styleDimension2.Pixels += uiimage.GetOuterDimensions().Height + 5f;
			UIElement uielement = new UIElement
			{
				Left = styleDimension,
				Top = styleDimension2,
				Height = StyleDimension.FromPixels(92f - styleDimension2.Pixels),
				Width = StyleDimension.FromPixelsAndPercent(-styleDimension.Pixels, 1f)
			};
			base.Append(uielement);
			this.ContentPanel = uielement;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x005AE2CC File Offset: 0x005AC4CC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			this.DrawIcon(spriteBatch);
			if (this.ResourcePack.Branding == ResourcePack.BrandingType.SteamWorkshop)
			{
				Asset<Texture2D> asset = TextureAssets.Extra[243];
				spriteBatch.Draw(asset.Value, new Vector2(base.GetDimensions().X + base.GetDimensions().Width - (float)asset.Width() - 3f, base.GetDimensions().Y + 2f), new Rectangle?(asset.Frame(1, 1, 0, 0, 0, 0)), Color.White);
			}
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x005AE360 File Offset: 0x005AC560
		private void DrawIcon(SpriteBatch spriteBatch)
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			spriteBatch.Draw(this.ResourcePack.Icon, new Rectangle((int)innerDimensions.X + 4, (int)innerDimensions.Y + 4 + 10, 64, 64), Color.White);
			spriteBatch.Draw(this._iconBorderTexture.Value, new Rectangle((int)innerDimensions.X, (int)innerDimensions.Y + 10, 72, 72), Color.White);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x005AE3D8 File Offset: 0x005AC5D8
		public override int CompareTo(object obj)
		{
			return this.Order.CompareTo(((UIResourcePack)obj).Order);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x005AE3FE File Offset: 0x005AC5FE
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = UIResourcePack.HoverBackgroundColor;
			this.BorderColor = UIResourcePack.HoverBorderColor;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x005AE41D File Offset: 0x005AC61D
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = UIResourcePack.DefaultBackgroundColor;
			this.BorderColor = UIResourcePack.DefaultBorderColor;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x005AE43C File Offset: 0x005AC63C
		// Note: this type is marked as 'beforefieldinit'.
		static UIResourcePack()
		{
		}

		// Token: 0x040055E4 RID: 21988
		private const int PANEL_PADDING = 5;

		// Token: 0x040055E5 RID: 21989
		private const int ICON_SIZE = 64;

		// Token: 0x040055E6 RID: 21990
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x040055E7 RID: 21991
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x040055E8 RID: 21992
		private const float HEIGHT = 102f;

		// Token: 0x040055E9 RID: 21993
		private const float MIN_WIDTH = 102f;

		// Token: 0x040055EA RID: 21994
		private static readonly Color DefaultBackgroundColor = new Color(26, 40, 89) * 0.8f;

		// Token: 0x040055EB RID: 21995
		private static readonly Color DefaultBorderColor = new Color(13, 20, 44) * 0.8f;

		// Token: 0x040055EC RID: 21996
		private static readonly Color HoverBackgroundColor = new Color(46, 60, 119);

		// Token: 0x040055ED RID: 21997
		private static readonly Color HoverBorderColor = new Color(20, 30, 56);

		// Token: 0x040055EE RID: 21998
		public readonly ResourcePack ResourcePack;

		// Token: 0x040055EF RID: 21999
		[CompilerGenerated]
		private int <Order>k__BackingField;

		// Token: 0x040055F0 RID: 22000
		private readonly Asset<Texture2D> _iconBorderTexture;

		// Token: 0x040055F1 RID: 22001
		[CompilerGenerated]
		private UIElement <ContentPanel>k__BackingField;
	}
}
