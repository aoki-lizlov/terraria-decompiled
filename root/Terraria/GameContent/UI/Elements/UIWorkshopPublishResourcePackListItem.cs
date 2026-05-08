using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E2 RID: 994
	public class UIWorkshopPublishResourcePackListItem : UIPanel
	{
		// Token: 0x06002E34 RID: 11828 RVA: 0x005A91AC File Offset: 0x005A73AC
		public UIWorkshopPublishResourcePackListItem(UIState ownerState, ResourcePack data, int orderInList, bool canBePublished)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this._canPublish = canBePublished;
			this.LoadTextures();
			this.InitializeAppearance();
			UIElement uielement = new UIElement
			{
				Width = new StyleDimension(72f, 0f),
				Height = new StyleDimension(72f, 0f),
				Left = new StyleDimension(4f, 0f),
				VAlign = 0.5f
			};
			uielement.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			UIImage uiimage = new UIImage(data.Icon)
			{
				Width = new StyleDimension(-6f, 1f),
				Height = new StyleDimension(-6f, 1f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				ScaleToFit = true,
				AllowResizingDimensions = false,
				IgnoresMouseInteraction = true
			};
			UIImage uiimage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uielement.Append(uiimage);
			uielement.Append(uiimage2);
			base.Append(uielement);
			this._iconArea = uielement;
			this._publishButton = new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish", 1f, 0.5f, 10f);
			this._publishButton.HAlign = 1f;
			this._publishButton.VAlign = 1f;
			this._publishButton.OnLeftClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.OnLeftDoubleClick += this.PublishButtonClick_ImportResourcePackToLocalFiles;
			base.Append(this._publishButton);
			this._publishButton.SetSnapPoint("Publish", orderInList, null, null);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x005A93A8 File Offset: 0x005A75A8
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._iconBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x005A9408 File Offset: 0x005A7608
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x005A9448 File Offset: 0x005A7648
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			if (!this._canPublish)
			{
				this.BorderColor = new Color(150, 150, 150) * 1f;
				this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x005A94D4 File Offset: 0x005A76D4
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			if (!this._canPublish)
			{
				this.BorderColor = new Color(127, 127, 127) * 0.7f;
				this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x005A9573 File Offset: 0x005A7773
		private void PublishButtonClick_ImportResourcePackToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (this.TryMovingToRejectionMenuIfNeeded())
			{
				return;
			}
			Main.MenuUI.SetState(new WorkshopPublishInfoStateForResourcePack(this._ownerState, this._data));
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x005A95A4 File Offset: 0x005A77A4
		private bool TryMovingToRejectionMenuIfNeeded()
		{
			if (!this._canPublish)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.instance.RejectionMenuInfo = new RejectionMenuInfo
				{
					TextToShow = Language.GetTextValue("Workshop.ReportIssue_CannotPublishZips"),
					ExitAction = new ReturnFromRejectionMenuAction(this.RejectionMenuExitAction)
				};
				Main.menuMode = 1000001;
				return true;
			}
			return false;
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x005A960C File Offset: 0x005A780C
		private void RejectionMenuExitAction()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			if (this._ownerState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._ownerState);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x005A964C File Offset: 0x005A784C
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishResourcePackListItem uiworkshopPublishResourcePackListItem = obj as UIWorkshopPublishResourcePackListItem;
			if (uiworkshopPublishResourcePackListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopPublishResourcePackListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x005A967C File Offset: 0x005A787C
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x005A968B File Offset: 0x005A788B
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x005A969C File Offset: 0x005A789C
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x005A96DC File Offset: 0x005A78DC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._iconArea.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color white = Color.White;
			Utils.DrawBorderString(spriteBatch, this._data.Name, new Vector2(num + 8f, innerDimensions.Y + 3f), white, 1f, 0f, 0f, -1);
			float num2 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector = new Vector2(num + 8f, innerDimensions.Y + innerDimensions.Height - height);
			float num3 = num2;
			this.DrawPanel(spriteBatch, vector, num3, height);
			string textValue = Language.GetTextValue("UI.Author", this._data.Author);
			Color white2 = Color.White;
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x = vector2.X;
			float y = vector2.Y;
			float num4 = num3 * 0.5f - x * 0.5f;
			float num5 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(num4, num5 + 3f), white2, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			float num6 = num2;
			this.DrawPanel(spriteBatch, vector, num6, height);
			string textValue2 = Language.GetTextValue("UI.Version", this._data.Version.GetFormattedVersion());
			Color white3 = Color.White;
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue2);
			float x2 = vector3.X;
			float y2 = vector3.Y;
			float num7 = num6 * 0.5f - x2 * 0.5f;
			float num8 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(num7, num8 + 3f), white3, 1f, 0f, 0f, -1);
			vector.X += num6 + 5f;
		}

		// Token: 0x0400553C RID: 21820
		private ResourcePack _data;

		// Token: 0x0400553D RID: 21821
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x0400553E RID: 21822
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x0400553F RID: 21823
		private Asset<Texture2D> _iconBorderTexture;

		// Token: 0x04005540 RID: 21824
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005541 RID: 21825
		private UIElement _iconArea;

		// Token: 0x04005542 RID: 21826
		private UIElement _publishButton;

		// Token: 0x04005543 RID: 21827
		private int _orderInList;

		// Token: 0x04005544 RID: 21828
		private UIState _ownerState;

		// Token: 0x04005545 RID: 21829
		private const int ICON_SIZE = 64;

		// Token: 0x04005546 RID: 21830
		private const int ICON_BORDER_PADDING = 4;

		// Token: 0x04005547 RID: 21831
		private const int HEIGHT_FLUFF = 10;

		// Token: 0x04005548 RID: 21832
		private bool _canPublish;
	}
}
