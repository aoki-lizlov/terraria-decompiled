using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E1 RID: 993
	public class UIWorkshopImportWorldListItem : AWorldListItem
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x005A88FC File Offset: 0x005A6AFC
		public UIWorkshopImportWorldListItem(UIState ownerState, WorldFileData data, int orderInList)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.Left.Set(4f, 0f);
			this._worldIcon.OnLeftDoubleClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			base.Append(this._worldIcon);
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1), null);
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			base.OnLeftDoubleClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			uiimageButton.OnMouseOver += this.PlayMouseOver;
			uiimageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton);
			num += 24f;
			this._buttonLabel = new UIText("", 1f, false);
			this._buttonLabel.VAlign = 1f;
			this._buttonLabel.Left.Set(num, 0f);
			this._buttonLabel.Top.Set(-3f, 0f);
			base.Append(this._buttonLabel);
			uiimageButton.SetSnapPoint("Import", orderInList, null, null);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x005A8A91 File Offset: 0x005A6C91
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x005A8AD0 File Offset: 0x005A6CD0
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x005A8B0D File Offset: 0x005A6D0D
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x005A8B37 File Offset: 0x005A6D37
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x005A8B75 File Offset: 0x005A6D75
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Import"));
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x005A8B8C File Offset: 0x005A6D8C
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x005A8BA0 File Offset: 0x005A6DA0
		private void ImportButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (!this._data.IsValid)
			{
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("Workshop.EnterNewNameForImportedWorld"), this._data.Name, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true, 27);
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x005A8C24 File Offset: 0x005A6E24
		private void OnFinishedSettingName(string name)
		{
			string text = name.Trim();
			if (SocialAPI.Workshop != null)
			{
				SocialAPI.Workshop.ImportDownloadedWorldToLocalSaves(this._data, text, new Action(this.GoBackHere));
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x005A8C5C File Offset: 0x005A6E5C
		private void GoBackHere()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._ownerState);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x005A8C90 File Offset: 0x005A6E90
		public override int CompareTo(object obj)
		{
			UIWorkshopImportWorldListItem uiworkshopImportWorldListItem = obj as UIWorkshopImportWorldListItem;
			if (uiworkshopImportWorldListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopImportWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x005A8CC0 File Offset: 0x005A6EC0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x005A8CCF File Offset: 0x005A6ECF
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x005A8CE0 File Offset: 0x005A6EE0
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x005A8DD0 File Offset: 0x005A6FD0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = Color.White;
			string text = this._data.GetWorldName(true);
			if (!this._data.IsValid)
			{
				color = Color.Gray;
				string name = StatusID.Search.GetName(this._data.LoadStatus);
				text = "(" + name + ") " + text;
			}
			if (text != null)
			{
				Utils.DrawBorderString(spriteBatch, text, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			}
			spriteBatch.Draw(this._workshopIconTexture.Value, new Vector2(base.GetDimensions().X + base.GetDimensions().Width - (float)this._workshopIconTexture.Width() - 3f, base.GetDimensions().Y + 2f), new Rectangle?(this._workshopIconTexture.Frame(1, 1, 0, 0, 0, 0)), Color.White);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string text2 = "";
			Color white = Color.White;
			base.GetDifficulty(out text2, out white);
			float x = FontAssets.MouseText.Value.MeasureString(text2).X;
			float num3 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, text2, vector + new Vector2(num3, 3f), white, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			if (this._data._worldSizeName != null)
			{
				float num4 = 150f;
				if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
				{
					num4 += 40f;
				}
				this.DrawPanel(spriteBatch, vector, num4);
				string textValue = Language.GetTextValue("UI.WorldSizeFormat", this._data.WorldSizeName);
				float x2 = FontAssets.MouseText.Value.MeasureString(textValue).X;
				float num5 = num4 * 0.5f - x2 * 0.5f;
				Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(num5, 3f), Color.White, 1f, 0f, 0f, -1);
				vector.X += num4 + 5f;
			}
			float num6 = innerDimensions.X + innerDimensions.Width - vector.X;
			this.DrawPanel(spriteBatch, vector, num6);
			string text3;
			if (GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
			{
				text3 = this._data.CreationTime.ToString("d MMMM yyyy");
			}
			else
			{
				text3 = this._data.CreationTime.ToShortDateString();
			}
			string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", text3);
			float x3 = FontAssets.MouseText.Value.MeasureString(textValue2).X;
			float num7 = num6 * 0.5f - x3 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(num7, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num6 + 5f;
		}

		// Token: 0x04005535 RID: 21813
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04005536 RID: 21814
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x04005537 RID: 21815
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005538 RID: 21816
		private UIElement _worldIcon;

		// Token: 0x04005539 RID: 21817
		private UIText _buttonLabel;

		// Token: 0x0400553A RID: 21818
		private int _orderInList;

		// Token: 0x0400553B RID: 21819
		public UIState _ownerState;
	}
}
