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
	// Token: 0x020003FD RID: 1021
	public class UICharacterListItem : UIPanel
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x005AFAD1 File Offset: 0x005ADCD1
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x005AFAE0 File Offset: 0x005ADCE0
		public UICharacterListItem(PlayerFileData data, int orderInList)
		{
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive", 1);
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive", 1);
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive", 1);
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", 1);
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1);
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", 1);
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1);
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this._data = data;
			this._orderInList = orderInList;
			this._playerPanel = new UICharacter(data.Player, false, true, 1f, true);
			this._playerPanel.Left.Set(4f, 0f);
			this._playerPanel.OnLeftDoubleClick += this.PlayGame;
			base.OnLeftDoubleClick += this.PlayGame;
			base.Append(this._playerPanel);
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(this._buttonPlayTexture, null);
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.PlayGame;
			uiimageButton.OnMouseOver += this.PlayMouseOver;
			uiimageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton);
			num += 24f;
			UIImageButton uiimageButton2 = new UIImageButton(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture, null);
			uiimageButton2.VAlign = 1f;
			uiimageButton2.Left.Set(num, 0f);
			uiimageButton2.OnLeftClick += this.FavoriteButtonClick;
			uiimageButton2.OnMouseOver += this.FavoriteMouseOver;
			uiimageButton2.OnMouseOut += this.ButtonMouseOut;
			uiimageButton2.SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
			base.Append(uiimageButton2);
			num += 24f;
			if (SocialAPI.Cloud != null)
			{
				UIImageButton uiimageButton3 = new UIImageButton(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture, null);
				uiimageButton3.VAlign = 1f;
				uiimageButton3.Left.Set(num, 0f);
				uiimageButton3.OnLeftClick += this.CloudButtonClick;
				uiimageButton3.OnMouseOver += this.CloudMouseOver;
				uiimageButton3.OnMouseOut += this.ButtonMouseOut;
				base.Append(uiimageButton3);
				uiimageButton3.SetSnapPoint("Cloud", orderInList, null, null);
				num += 24f;
			}
			UIImageButton uiimageButton4 = new UIImageButton(this._buttonRenameTexture, null);
			uiimageButton4.VAlign = 1f;
			uiimageButton4.Left.Set(num, 0f);
			uiimageButton4.OnLeftClick += this.RenameButtonClick;
			uiimageButton4.OnMouseOver += this.RenameMouseOver;
			uiimageButton4.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton4);
			num += 24f;
			UIImageButton uiimageButton5 = new UIImageButton(this._buttonDeleteTexture, null);
			uiimageButton5.VAlign = 1f;
			uiimageButton5.HAlign = 1f;
			if (!this._data.IsFavorite)
			{
				uiimageButton5.OnLeftClick += this.DeleteButtonClick;
			}
			uiimageButton5.OnMouseOver += this.DeleteMouseOver;
			uiimageButton5.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uiimageButton5;
			base.Append(uiimageButton5);
			num += 4f;
			this._buttonLabel = new UIText("", 1f, false);
			this._buttonLabel.VAlign = 1f;
			this._buttonLabel.Left.Set(num, 0f);
			this._buttonLabel.Top.Set(-3f, 0f);
			base.Append(this._buttonLabel);
			this._deleteButtonLabel = new UIText("", 1f, false);
			this._deleteButtonLabel.VAlign = 1f;
			this._deleteButtonLabel.HAlign = 1f;
			this._deleteButtonLabel.Left.Set(-30f, 0f);
			this._deleteButtonLabel.Top.Set(-3f, 0f);
			base.Append(this._deleteButtonLabel);
			uiimageButton.SetSnapPoint("Play", orderInList, null, null);
			uiimageButton2.SetSnapPoint("Favorite", orderInList, null, null);
			uiimageButton4.SetSnapPoint("Rename", orderInList, null, null);
			uiimageButton5.SetSnapPoint("Delete", orderInList, null, null);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x005B00CA File Offset: 0x005AE2CA
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x005B00E1 File Offset: 0x005AE2E1
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x005B011B File Offset: 0x005AE31B
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x005B0155 File Offset: 0x005AE355
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x005B016C File Offset: 0x005AE36C
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x005B01A6 File Offset: 0x005AE3A6
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x005B01B8 File Offset: 0x005AE3B8
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x005B01CC File Offset: 0x005AE3CC
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, this._data.Name, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true, 20);
			Main.MenuUI.SetState(uivirtualKeyboard);
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x005B0254 File Offset: 0x005AE454
		private void OnFinishedSettingName(string name)
		{
			string text = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(text);
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x005B0280 File Offset: 0x005AE480
		private void GoBackHere()
		{
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x005B0288 File Offset: 0x005AE488
		private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._data.MoveToLocal();
			}
			else
			{
				this._data.MoveToCloud();
			}
			((UIImageButton)evt.Target).SetImage(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture, null);
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x005B0328 File Offset: 0x005AE528
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			for (int i = 0; i < Main.PlayerList.Count; i++)
			{
				if (Main.PlayerList[i] == this._data)
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.selectedPlayer = i;
					Main.menuMode = 5;
					return;
				}
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x005B037F File Offset: 0x005AE57F
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (this._data.Player.loadStatus == StatusID.Ok)
			{
				Main.SelectPlayer(this._data);
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x005B03B0 File Offset: 0x005AE5B0
		private void FavoriteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._data.ToggleFavorite();
			((UIImageButton)evt.Target).SetImage(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture, null);
			((UIImageButton)evt.Target).SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				this._deleteButton.OnLeftClick -= this.DeleteButtonClick;
			}
			else
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
				this._deleteButton.OnLeftClick += this.DeleteButtonClick;
			}
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x005B04AC File Offset: 0x005AE6AC
		public override int CompareTo(object obj)
		{
			UICharacterListItem uicharacterListItem = obj as UICharacterListItem;
			if (uicharacterListItem != null)
			{
				return this._orderInList.CompareTo(uicharacterListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x005B04DC File Offset: 0x005AE6DC
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			this._playerPanel.SetAnimated(true);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x005B051C File Offset: 0x005AE71C
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._playerPanel.SetAnimated(false);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x005B0578 File Offset: 0x005AE778
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x005B0668 File Offset: 0x005AE868
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._playerPanel.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = Color.White;
			string text = this._data.Name;
			if (this._data.Player.loadStatus != StatusID.Ok)
			{
				color = Color.Gray;
				string name = StatusID.Search.GetName(this._data.Player.loadStatus);
				text = "(" + name + ") " + text;
			}
			Utils.DrawBorderString(spriteBatch, text, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 200f;
			Vector2 vector2 = vector;
			this.DrawPanel(spriteBatch, vector2, num2);
			spriteBatch.Draw(TextureAssets.Heart.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Heart.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statLifeMax + Language.GetTextValue("GameUI.PlayerLifeMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
			vector2.X += 65f;
			spriteBatch.Draw(TextureAssets.Mana.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Mana.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statManaMax + Language.GetTextValue("GameUI.PlayerManaMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			Vector2 vector3 = vector;
			float num3 = 140f;
			if (GameCulture.FromCultureName(GameCulture.CultureName.Russian).IsActive)
			{
				num3 = 180f;
			}
			this.DrawPanel(spriteBatch, vector3, num3);
			string text2 = "";
			Color color2 = Color.White;
			switch (this._data.Player.difficulty)
			{
			case 0:
				text2 = Language.GetTextValue("UI.Softcore");
				break;
			case 1:
				text2 = Language.GetTextValue("UI.Mediumcore");
				color2 = Main.mcColor;
				break;
			case 2:
				text2 = Language.GetTextValue("UI.Hardcore");
				color2 = Main.hcColor;
				break;
			case 3:
				text2 = Language.GetTextValue("UI.Creative");
				color2 = Main.creativeModeColor;
				break;
			}
			vector3 += new Vector2(num3 * 0.5f - FontAssets.MouseText.Value.MeasureString(text2).X * 0.5f, 3f);
			Utils.DrawBorderString(spriteBatch, text2, vector3, color2, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			Vector2 vector4 = vector;
			float num4 = innerDimensions.X + innerDimensions.Width - vector4.X;
			this.DrawPanel(spriteBatch, vector4, num4);
			TimeSpan playTime = this._data.GetPlayTime();
			int num5 = playTime.Days * 24 + playTime.Hours;
			string text3 = ((num5 < 10) ? "0" : "") + num5 + playTime.ToString("\\:mm\\:ss");
			vector4 += new Vector2(num4 * 0.5f - FontAssets.MouseText.Value.MeasureString(text3).X * 0.5f, 3f);
			Utils.DrawBorderString(spriteBatch, text3, vector4, Color.White, 1f, 0f, 0f, -1);
		}

		// Token: 0x04005613 RID: 22035
		private PlayerFileData _data;

		// Token: 0x04005614 RID: 22036
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04005615 RID: 22037
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005616 RID: 22038
		private UICharacter _playerPanel;

		// Token: 0x04005617 RID: 22039
		private UIText _buttonLabel;

		// Token: 0x04005618 RID: 22040
		private UIText _deleteButtonLabel;

		// Token: 0x04005619 RID: 22041
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x0400561A RID: 22042
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x0400561B RID: 22043
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x0400561C RID: 22044
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x0400561D RID: 22045
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x0400561E RID: 22046
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x0400561F RID: 22047
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x04005620 RID: 22048
		private UIImageButton _deleteButton;

		// Token: 0x04005621 RID: 22049
		private int _orderInList;
	}
}
