using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000409 RID: 1033
	public class UIWorldListItem : AWorldListItem
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x005B2C29 File Offset: 0x005B0E29
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x005B2C38 File Offset: 0x005B0E38
		public UIWorldListItem(WorldFileData data, int orderInList, bool canBePlayed, bool hasBeenPlayedByActivePlayer, bool isNewlyGenerated)
		{
			this._orderInList = orderInList;
			this._data = data;
			this._canBePlayed = canBePlayed;
			this._hasBeenPlayedByActivePlayer = hasBeenPlayedByActivePlayer;
			this._isNewlyGenerated = isNewlyGenerated;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.OnLeftDoubleClick += this.PlayGame;
			base.Append(this._worldIcon);
			if (this._data.DefeatedMoonlord)
			{
				UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconCompletion", 1))
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					Top = new StyleDimension(-10f, 0f),
					Left = new StyleDimension(-3f, 0f),
					IgnoresMouseInteraction = true
				};
				this._worldIcon.Append(uiimage);
			}
			if (base.GetIcons().Count >= 2 && !this._data.ZenithWorld)
			{
				UIImage uiimage2 = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconMixedSeed", 1))
				{
					HAlign = 0f,
					VAlign = 1f,
					Top = StyleDimension.FromPixels(0f),
					Left = StyleDimension.FromPixels(0f)
				};
				this._worldIcon.Append(uiimage2);
			}
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(this._buttonPlayTexture, null);
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.PlayGame;
			base.OnLeftDoubleClick += this.PlayGame;
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
				uiimageButton3.SetSnapPoint("Cloud", orderInList, null, null);
				base.Append(uiimageButton3);
				num += 24f;
			}
			if (this._data.WorldGeneratorVersion != 0UL)
			{
				UIImageButton uiimageButton4 = new UIImageButton(this._buttonSeedTexture, null);
				uiimageButton4.VAlign = 1f;
				uiimageButton4.Left.Set(num, 0f);
				uiimageButton4.OnLeftClick += this.SeedButtonClick;
				uiimageButton4.OnMouseOver += this.SeedMouseOver;
				uiimageButton4.OnMouseOut += this.ButtonMouseOut;
				uiimageButton4.SetSnapPoint("Seed", orderInList, null, null);
				base.Append(uiimageButton4);
				num += 24f;
			}
			UIImageButton uiimageButton5 = new UIImageButton(this._buttonRenameTexture, null);
			uiimageButton5.VAlign = 1f;
			uiimageButton5.Left.Set(num, 0f);
			uiimageButton5.OnLeftClick += this.RenameButtonClick;
			uiimageButton5.OnMouseOver += this.RenameMouseOver;
			uiimageButton5.OnMouseOut += this.ButtonMouseOut;
			uiimageButton5.SetSnapPoint("Rename", orderInList, null, null);
			base.Append(uiimageButton5);
			num += 24f;
			UIImageButton uiimageButton6 = new UIImageButton(this._buttonDeleteTexture, null);
			uiimageButton6.VAlign = 1f;
			uiimageButton6.HAlign = 1f;
			if (!this._data.IsFavorite)
			{
				uiimageButton6.OnLeftClick += this.DeleteButtonClick;
			}
			uiimageButton6.OnMouseOver += this.DeleteMouseOver;
			uiimageButton6.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uiimageButton6;
			base.Append(uiimageButton6);
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
			int num2 = 0;
			if (this._hasBeenPlayedByActivePlayer)
			{
				UIImage uiimage3 = new UIImage(this._hasBeenPlayedByActivePlayerTexture)
				{
					HAlign = 1f,
					Left = new StyleDimension((float)num2, 0f),
					Top = new StyleDimension(-6f, 0f),
					ImageScale = 0.75f,
					UseTextureSizeForOrigin = false
				};
				uiimage3.OnMouseOver += this.HasPlayedMouseOver;
				uiimage3.OnMouseOut += this.DeleteMouseOut;
				base.Append(uiimage3);
				num2 -= 24;
			}
			if (this._isNewlyGenerated)
			{
				UIImage uiimage4 = new UIImage(this._isNewlyGeneratedTexture)
				{
					HAlign = 1f,
					Left = new StyleDimension((float)(num2 - 2), 0f),
					Top = new StyleDimension(-6f, 0f),
					ImageScale = 0.75f,
					UseTextureSizeForOrigin = false
				};
				uiimage4.OnMouseOver += this.NewlyGeneratedMouseOver;
				uiimage4.OnMouseOut += this.DeleteMouseOut;
				base.Append(uiimage4);
				num2 -= 24;
			}
			uiimageButton.SetSnapPoint("Play", orderInList, null, null);
			uiimageButton2.SetSnapPoint("Favorite", orderInList, null, null);
			uiimageButton5.SetSnapPoint("Rename", orderInList, null, null);
			uiimageButton6.SetSnapPoint("Delete", orderInList, null, null);
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x005B33EC File Offset: 0x005B15EC
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive", 1);
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive", 1);
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive", 1);
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", 1);
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1);
			this._buttonSeedTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonSeed", 1);
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", 1);
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1);
			this._hasBeenPlayedByActivePlayerTexture = Main.Assets.Request<Texture2D>("Images/UI/IconPlayedBefore", 1);
			this._isNewlyGeneratedTexture = Main.Assets.Request<Texture2D>("Images/UI/IconNewlyGenerated", 1);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x005B3501 File Offset: 0x005B1701
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x005B3540 File Offset: 0x005B1740
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			if (!this._canBePlayed)
			{
				this.BorderColor = new Color(150, 150, 150) * 1f;
				this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x005B35CC File Offset: 0x005B17CC
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			if (!this._canBePlayed)
			{
				this.BorderColor = new Color(127, 127, 127) * 0.7f;
				this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
			}
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x005B366B File Offset: 0x005B186B
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x005B3682 File Offset: 0x005B1882
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x005B36BC File Offset: 0x005B18BC
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x005B36F6 File Offset: 0x005B18F6
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x005B370D File Offset: 0x005B190D
		private void SeedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.CopySeed", this._data.GetFullSeedText(true)));
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x005B3730 File Offset: 0x005B1930
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x005B376A File Offset: 0x005B196A
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x005B377C File Offset: 0x005B197C
		private void NewlyGeneratedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.WorldNewlyGenerated"));
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x005B3793 File Offset: 0x005B1993
		private void HasPlayedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.WorldHasBeenPlayed"));
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x005B37AA File Offset: 0x005B19AA
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x005B37BC File Offset: 0x005B19BC
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

		// Token: 0x06002F82 RID: 12162 RVA: 0x005B385C File Offset: 0x005B1A5C
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			for (int i = 0; i < Main.WorldList.Count; i++)
			{
				if (Main.WorldList[i] == this._data)
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.selectedWorld = i;
					Main.menuMode = 9;
					return;
				}
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x005B38B4 File Offset: 0x005B1AB4
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (!this._data.IsValid)
			{
				return;
			}
			if (this.TryMovingToRejectionMenuIfNeeded(this._data.GameMode))
			{
				return;
			}
			this._data.SetAsActive();
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			Main.GetInputText("", false);
			if (Main.menuMultiplayer && SocialAPI.Network != null)
			{
				Main.menuMode = 889;
			}
			else if (Main.menuMultiplayer)
			{
				Main.menuMode = 30;
			}
			else
			{
				Main.menuMode = 10;
			}
			if (!Main.menuMultiplayer)
			{
				WorldGen.playWorld();
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x005B395C File Offset: 0x005B1B5C
		private bool TryMovingToRejectionMenuIfNeeded(int worldGameMode)
		{
			if (!GameModeID.IsValid(worldGameMode))
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.WorldCannotBeLoadedBecauseItHasAnInvalidGameMode");
				Main.menuMode = 1000000;
				return true;
			}
			bool flag = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool flag2 = worldGameMode == 3;
			if (flag && !flag2)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.PlayerIsCreativeAndWorldIsNotCreative");
				Main.menuMode = 1000000;
				return true;
			}
			if (!flag && flag2)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.PlayerIsNotCreativeAndWorldIsCreative");
				Main.menuMode = 1000000;
				return true;
			}
			return false;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x005B3A28 File Offset: 0x005B1C28
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, this._data.GetWorldName(false), new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true, 27);
			Main.MenuUI.SetState(uivirtualKeyboard);
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x005B3AB0 File Offset: 0x005B1CB0
		private void OnFinishedSettingName(string name)
		{
			string text = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(text);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x005B3AD7 File Offset: 0x005B1CD7
		private void GoBackHere()
		{
			Main.GoToWorldSelect();
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x005B3AE0 File Offset: 0x005B1CE0
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

		// Token: 0x06002F89 RID: 12169 RVA: 0x005B3BDB File Offset: 0x005B1DDB
		private void SeedButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Platform.Get<IClipboard>().Value = this._data.GetFullSeedText(false);
			this._buttonLabel.SetText(Language.GetTextValue("UI.SeedCopied"));
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x005B3C08 File Offset: 0x005B1E08
		public override int CompareTo(object obj)
		{
			UIWorldListItem uiworldListItem = obj as UIWorldListItem;
			if (uiworldListItem != null)
			{
				return this._orderInList.CompareTo(uiworldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x005B3C38 File Offset: 0x005B1E38
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x005B3C47 File Offset: 0x005B1E47
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x005B3C58 File Offset: 0x005B1E58
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x005B3D48 File Offset: 0x005B1F48
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
			Utils.DrawBorderString(spriteBatch, text, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string text2;
			Color color2;
			base.GetDifficulty(out text2, out color2);
			float x = FontAssets.MouseText.Value.MeasureString(text2).X;
			float num3 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, text2, vector + new Vector2(num3, 3f), color2, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
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

		// Token: 0x04005670 RID: 22128
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04005671 RID: 22129
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005672 RID: 22130
		private UIElement _worldIcon;

		// Token: 0x04005673 RID: 22131
		private UIText _buttonLabel;

		// Token: 0x04005674 RID: 22132
		private UIText _deleteButtonLabel;

		// Token: 0x04005675 RID: 22133
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x04005676 RID: 22134
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x04005677 RID: 22135
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x04005678 RID: 22136
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x04005679 RID: 22137
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x0400567A RID: 22138
		private Asset<Texture2D> _buttonSeedTexture;

		// Token: 0x0400567B RID: 22139
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x0400567C RID: 22140
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x0400567D RID: 22141
		private Asset<Texture2D> _hasBeenPlayedByActivePlayerTexture;

		// Token: 0x0400567E RID: 22142
		private Asset<Texture2D> _isNewlyGeneratedTexture;

		// Token: 0x0400567F RID: 22143
		private UIImageButton _deleteButton;

		// Token: 0x04005680 RID: 22144
		private int _orderInList;

		// Token: 0x04005681 RID: 22145
		private bool _canBePlayed;

		// Token: 0x04005682 RID: 22146
		private bool _hasBeenPlayedByActivePlayer;

		// Token: 0x04005683 RID: 22147
		private bool _isNewlyGenerated;
	}
}
