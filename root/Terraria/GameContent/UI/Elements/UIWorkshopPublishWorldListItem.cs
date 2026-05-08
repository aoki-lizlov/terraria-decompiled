using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E3 RID: 995
	public class UIWorkshopPublishWorldListItem : AWorldListItem
	{
		// Token: 0x06002E41 RID: 11841 RVA: 0x005A9924 File Offset: 0x005A7B24
		public UIWorkshopPublishWorldListItem(UIState ownerState, WorldFileData data, int orderInList)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.Left.Set(4f, 0f);
			this._worldIcon.VAlign = 0.5f;
			this._worldIcon.OnLeftDoubleClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.Append(this._worldIcon);
			this._publishButton = new UIIconTextButton(Language.GetText("Workshop.Publish"), Color.White, "Images/UI/Workshop/Publish", 1f, 0.5f, 10f);
			this._publishButton.HAlign = 1f;
			this._publishButton.VAlign = 1f;
			this._publishButton.OnLeftClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.OnLeftDoubleClick += this.PublishButtonClick_ImportWorldToLocalFiles;
			base.Append(this._publishButton);
			this._publishButton.SetSnapPoint("Publish", orderInList, null, null);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x005A9A5D File Offset: 0x005A7C5D
		private void LoadTextures()
		{
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x005A9A86 File Offset: 0x005A7C86
		private void InitializeAppearance()
		{
			this.Height.Set(82f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x005A8B0D File Offset: 0x005A6D0D
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x005A8B37 File Offset: 0x005A6D37
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002E46 RID: 11846 RVA: 0x005A9AC3 File Offset: 0x005A7CC3
		private void PublishButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			Main.MenuUI.SetState(new WorkshopPublishInfoStateForWorld(this._ownerState, this._data));
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x005A9AEC File Offset: 0x005A7CEC
		public override int CompareTo(object obj)
		{
			UIWorkshopPublishWorldListItem uiworkshopPublishWorldListItem = obj as UIWorkshopPublishWorldListItem;
			if (uiworkshopPublishWorldListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopPublishWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x005A9B1C File Offset: 0x005A7D1C
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x005A9B2B File Offset: 0x005A7D2B
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x005A9B3C File Offset: 0x005A7D3C
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width, float height)
		{
			Utils.DrawSplicedPanel(spriteBatch, this._innerPanelTexture.Value, (int)position.X, (int)position.Y, (int)width, (int)height, 10, 10, 10, 10, Color.White);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x005A9B7C File Offset: 0x005A7D7C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = (this._data.IsValid ? Color.White : Color.Gray);
			string worldName = this._data.GetWorldName(true);
			Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, innerDimensions.Y + 3f), color, 1f, 0f, 0f, -1);
			float num2 = (innerDimensions.Width - 22f - dimensions.Width - this._publishButton.GetDimensions().Width) / 2f;
			float height = this._publishButton.GetDimensions().Height;
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + innerDimensions.Height - height);
			float num3 = num2;
			this.DrawPanel(spriteBatch, vector, num3, height);
			string text = "";
			Color white = Color.White;
			base.GetDifficulty(out text, out white);
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text);
			float x = vector2.X;
			float y = vector2.Y;
			float num4 = num3 * 0.5f - x * 0.5f;
			float num5 = height * 0.5f - y * 0.5f;
			Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(num4, num5 + 3f), white, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			float num6 = num2;
			if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
			{
				num6 += 40f;
			}
			this.DrawPanel(spriteBatch, vector, num6, height);
			string textValue = Language.GetTextValue("UI.WorldSizeFormat", this._data.WorldSizeName);
			Vector2 vector3 = FontAssets.MouseText.Value.MeasureString(textValue);
			float x2 = vector3.X;
			float y2 = vector3.Y;
			float num7 = num6 * 0.5f - x2 * 0.5f;
			float num8 = height * 0.5f - y2 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(num7, num8 + 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num6 + 5f;
		}

		// Token: 0x04005549 RID: 21833
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x0400554A RID: 21834
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x0400554B RID: 21835
		private UIElement _worldIcon;

		// Token: 0x0400554C RID: 21836
		private UIElement _publishButton;

		// Token: 0x0400554D RID: 21837
		private int _orderInList;

		// Token: 0x0400554E RID: 21838
		private UIState _ownerState;
	}
}
