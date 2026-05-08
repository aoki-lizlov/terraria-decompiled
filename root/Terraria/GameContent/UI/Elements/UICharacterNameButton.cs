using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E9 RID: 1001
	public class UICharacterNameButton : UIElement
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x005AACB0 File Offset: 0x005A8EB0
		public UICharacterNameButton(LocalizedText titleText, LocalizedText emptyContentText, LocalizedText description = null)
		{
			this.Width = StyleDimension.FromPixels(400f);
			this.Height = StyleDimension.FromPixels(40f);
			this.Description = description;
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._textToShowWhenEmpty = emptyContentText;
			float num = 1f;
			UIText uitext = new UIText(titleText, num, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(10f)
			};
			base.Append(uitext);
			this._title = uitext;
			UIText uitext2 = new UIText(Language.GetText("UI.PlayerNameSlot"), num, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Left = StyleDimension.FromPixels(150f)
			};
			base.Append(uitext2);
			this._text = uitext2;
			this.SetContents(null);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x005AADD0 File Offset: 0x005A8FD0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._hovered)
			{
				if (!this._soundedHover)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._soundedHover = true;
			}
			else
			{
				this._soundedHover = false;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White * 0.5f);
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White);
			}
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x005AAEA0 File Offset: 0x005A90A0
		public void SetContents(string name)
		{
			this.actualContents = name;
			if (string.IsNullOrEmpty(this.actualContents))
			{
				this._text.TextColor = Color.Gray;
				this._text.SetText(this._textToShowWhenEmpty);
			}
			else
			{
				this._text.TextColor = Color.White;
				this._text.SetText(this.actualContents);
			}
			this._text.Left = StyleDimension.FromPixels(this._title.GetInnerDimensions().Width + this.DistanceFromTitleToOption);
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x005AAF2C File Offset: 0x005A912C
		public CalculatedStyle GetTextDimensions()
		{
			return this._text.GetDimensions();
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x005AAF3C File Offset: 0x005A913C
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
			Rectangle rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			CalculatedStyle calculatedStyle = this._text.GetDimensions();
			Point point3 = new Point((int)calculatedStyle.X, (int)calculatedStyle.Y);
			Point point4 = new Point(point3.X + (int)calculatedStyle.Width, point3.Y + (int)calculatedStyle.Height);
			Rectangle rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			bool flag = false;
			while (rectangle2.Right > rectangle.Right - padding)
			{
				this._text.SetText(Utils.TrimLastCharacter(this._text.Text));
				flag = true;
				this.RecalculateChildren();
				calculatedStyle = this._text.GetDimensions();
				point3 = new Point((int)calculatedStyle.X, (int)calculatedStyle.Y);
				point4 = new Point(point3.X + (int)calculatedStyle.Width, point3.Y + (int)calculatedStyle.Height);
				rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			}
			if (flag)
			{
				this._text.SetText(Utils.TrimLastCharacter(this._text.Text) + "…");
			}
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x005AB11B File Offset: 0x005A931B
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x005AB124 File Offset: 0x005A9324
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x005AB134 File Offset: 0x005A9334
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x04005585 RID: 21893
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04005586 RID: 21894
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04005587 RID: 21895
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04005588 RID: 21896
		private bool _hovered;

		// Token: 0x04005589 RID: 21897
		private bool _soundedHover;

		// Token: 0x0400558A RID: 21898
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x0400558B RID: 21899
		private string actualContents;

		// Token: 0x0400558C RID: 21900
		private UIText _text;

		// Token: 0x0400558D RID: 21901
		private UIText _title;

		// Token: 0x0400558E RID: 21902
		public readonly LocalizedText Description;

		// Token: 0x0400558F RID: 21903
		public float DistanceFromTitleToOption = 20f;
	}
}
