using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003FB RID: 1019
	public class UIAchievementListItem : UIPanel
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x005AE7D8 File Offset: 0x005AC9D8
		public UIAchievementListItem(Achievement achievement, bool largeForOtherLanguages)
		{
			this._large = largeForOtherLanguages;
			this.BackgroundColor = new Color(26, 40, 89) * 0.8f;
			this.BorderColor = new Color(13, 20, 44) * 0.8f;
			float num = (float)(16 + this._large.ToInt() * 20);
			float num2 = (float)(this._large.ToInt() * 6);
			float num3 = (float)(this._large.ToInt() * 12);
			this._achievement = achievement;
			this.Height.Set(66f + num, 0f);
			this.Width.Set(0f, 1f);
			this.PaddingTop = 8f;
			this.PaddingLeft = 9f;
			int iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
			this._iconIndex = iconIndex;
			this._iconFrameUnlocked = new Rectangle(iconIndex % 8 * 66, iconIndex / 8 * 66, 64, 64);
			this._iconFrameLocked = this._iconFrameUnlocked;
			this._iconFrameLocked.X = this._iconFrameLocked.X + 528;
			this._iconFrame = this._iconFrameLocked;
			this.UpdateIconFrame();
			this._achievementIcon = new UIImageFramed(Main.Assets.Request<Texture2D>("Images/UI/Achievements", 1), this._iconFrame);
			this._achievementIcon.Left.Set(num2, 0f);
			this._achievementIcon.Top.Set(num3, 0f);
			base.Append(this._achievementIcon);
			this._achievementIconBorders = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 1));
			this._achievementIconBorders.Left.Set(-4f + num2, 0f);
			this._achievementIconBorders.Top.Set(-4f + num3, 0f);
			base.Append(this._achievementIconBorders);
			this._innerPanelTopTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelTop", 1);
			if (this._large)
			{
				this._innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom_Large", 1);
			}
			else
			{
				this._innerPanelBottomTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelBottom", 1);
			}
			this._categoryTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories", 1);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x005AEA24 File Offset: 0x005ACC24
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			int num = this._large.ToInt() * 6;
			Vector2 vector = new Vector2((float)num, 0f);
			this._locked = !this._achievement.IsCompleted;
			this.UpdateIconFrame();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._achievementIconBorders.GetDimensions();
			float num2 = dimensions.X + dimensions.Width;
			Vector2 vector2 = new Vector2(num2 + 7f, innerDimensions.Y);
			Tuple<decimal, decimal> trackerValues = this.GetTrackerValues();
			bool flag = false;
			if ((!(trackerValues.Item1 == 0m) || !(trackerValues.Item2 == 0m)) && this._locked)
			{
				flag = true;
			}
			float num3 = innerDimensions.Width - dimensions.Width + 1f - (float)(num * 2);
			Vector2 vector3 = new Vector2(0.85f);
			Vector2 vector4 = new Vector2(0.92f);
			string text = FontAssets.ItemStack.Value.CreateWrappedText(this._achievement.Description.Value, (num3 - 20f) * (1f / vector4.X), Language.ActiveCulture.CultureInfo);
			Vector2 vector5 = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, vector4, num3);
			if (!this._large)
			{
				vector5 = ChatManager.GetStringSize(FontAssets.ItemStack.Value, this._achievement.Description.Value, vector4, num3);
			}
			float num4 = 38f + (float)(this._large ? 20 : 0);
			if (vector5.Y > num4)
			{
				vector4.Y *= num4 / vector5.Y;
			}
			Color color = (this._locked ? Color.Silver : Color.Gold);
			color = Color.Lerp(color, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color2 = (this._locked ? Color.DarkGray : Color.Silver);
			color2 = Color.Lerp(color2, Color.White, base.IsMouseHovering ? 1f : 0f);
			Color color3 = (base.IsMouseHovering ? Color.White : Color.Gray);
			Vector2 vector6 = vector2 - Vector2.UnitY * 2f + vector;
			this.DrawPanelTop(spriteBatch, vector6, num3, color3);
			AchievementCategory category = this._achievement.Category;
			vector6.Y += 2f;
			vector6.X += 4f;
			spriteBatch.Draw(this._categoryTexture.Value, vector6, new Rectangle?(this._categoryTexture.Frame(4, 2, (int)category, 0, 0, 0)), base.IsMouseHovering ? Color.White : Color.Silver, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
			vector6.X += 4f;
			vector6.X += 17f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._achievement.FriendlyName.Value, vector6, color, 0f, Vector2.Zero, vector3, num3, 2f);
			vector6.X -= 17f;
			Vector2 vector7 = vector2 + Vector2.UnitY * 27f + vector;
			this.DrawPanelBottom(spriteBatch, vector7, num3, color3);
			vector7.X += 8f;
			vector7.Y += 4f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, vector7, color2, 0f, Vector2.Zero, vector4, -1f, 2f);
			if (flag)
			{
				Vector2 vector8 = vector6 + Vector2.UnitX * num3 + Vector2.UnitY;
				string text2 = (int)trackerValues.Item1 + "/" + (int)trackerValues.Item2;
				Vector2 vector9 = new Vector2(0.75f);
				Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text2, vector9, -1f);
				float num5 = (float)(trackerValues.Item1 / trackerValues.Item2);
				float num6 = 80f;
				Color color4 = new Color(100, 255, 100);
				if (!base.IsMouseHovering)
				{
					color4 = Color.Lerp(color4, Color.Black, 0.25f);
				}
				Color color5 = new Color(255, 255, 255);
				if (!base.IsMouseHovering)
				{
					color5 = Color.Lerp(color5, Color.Black, 0.25f);
				}
				this.DrawProgressBar(spriteBatch, num5, vector8 - Vector2.UnitX * num6 * 0.7f, num6, color5, color4, color4.MultiplyRGBA(new Color(new Vector4(1f, 1f, 1f, 0.5f))));
				vector8.X -= num6 * 1.4f + stringSize.X;
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text2, vector8, color, 0f, new Vector2(0f, 0f), vector9, 90f, 2f);
			}
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x005AEF72 File Offset: 0x005AD172
		private void UpdateIconFrame()
		{
			if (!this._locked)
			{
				this._iconFrame = this._iconFrameUnlocked;
			}
			else
			{
				this._iconFrame = this._iconFrameLocked;
			}
			if (this._achievementIcon != null)
			{
				this._achievementIcon.SetFrame(this._iconFrame);
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x005AEFB0 File Offset: 0x005AD1B0
		private void DrawPanelTop(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(this._innerPanelTopTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 2, this._innerPanelTopTexture.Height())), color);
			spriteBatch.Draw(this._innerPanelTopTexture.Value, new Vector2(position.X + 2f, position.Y), new Rectangle?(new Rectangle(2, 0, 2, this._innerPanelTopTexture.Height())), color, 0f, Vector2.Zero, new Vector2((width - 4f) / 2f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTopTexture.Value, new Vector2(position.X + width - 2f, position.Y), new Rectangle?(new Rectangle(4, 0, 2, this._innerPanelTopTexture.Height())), color);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x005AF098 File Offset: 0x005AD298
		private void DrawPanelBottom(SpriteBatch spriteBatch, Vector2 position, float width, Color color)
		{
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 6, this._innerPanelBottomTexture.Height())), color);
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, new Vector2(position.X + 6f, position.Y), new Rectangle?(new Rectangle(6, 0, 7, this._innerPanelBottomTexture.Height())), color, 0f, Vector2.Zero, new Vector2((width - 12f) / 7f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelBottomTexture.Value, new Vector2(position.X + width - 6f, position.Y), new Rectangle?(new Rectangle(13, 0, 6, this._innerPanelBottomTexture.Height())), color);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x005AF17F File Offset: 0x005AD37F
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(46, 60, 119);
			this.BorderColor = new Color(20, 30, 56);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x005AF1AA File Offset: 0x005AD3AA
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(26, 40, 89) * 0.8f;
			this.BorderColor = new Color(13, 20, 44) * 0.8f;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x005AF1E9 File Offset: 0x005AD3E9
		public Achievement GetAchievement()
		{
			return this._achievement;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x005AF1F4 File Offset: 0x005AD3F4
		private Tuple<decimal, decimal> GetTrackerValues()
		{
			if (!this._achievement.HasTracker)
			{
				return Tuple.Create<decimal, decimal>(0m, 0m);
			}
			IAchievementTracker tracker = this._achievement.GetTracker();
			if (tracker.GetTrackerType() == TrackerType.Int)
			{
				AchievementTracker<int> achievementTracker = (AchievementTracker<int>)tracker;
				return Tuple.Create<decimal, decimal>(achievementTracker.Value, achievementTracker.MaxValue);
			}
			if (tracker.GetTrackerType() == TrackerType.Float)
			{
				AchievementTracker<float> achievementTracker2 = (AchievementTracker<float>)tracker;
				return Tuple.Create<decimal, decimal>((decimal)achievementTracker2.Value, (decimal)achievementTracker2.MaxValue);
			}
			return Tuple.Create<decimal, decimal>(0m, 0m);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x005AF290 File Offset: 0x005AD490
		private void DrawProgressBar(SpriteBatch spriteBatch, float progress, Vector2 spot, float Width = 169f, Color BackColor = default(Color), Color FillingColor = default(Color), Color BlipColor = default(Color))
		{
			if (BlipColor == Color.Transparent)
			{
				BlipColor = new Color(255, 165, 0, 127);
			}
			if (FillingColor == Color.Transparent)
			{
				FillingColor = new Color(255, 241, 51);
			}
			if (BackColor == Color.Transparent)
			{
				FillingColor = new Color(255, 255, 255);
			}
			Texture2D value = TextureAssets.ColorBar.Value;
			Texture2D value2 = TextureAssets.ColorBlip.Value;
			Texture2D value3 = TextureAssets.MagicPixel.Value;
			float num = MathHelper.Clamp(progress, 0f, 1f);
			float num2 = Width * 1f;
			float num3 = 8f;
			float num4 = num2 / 169f;
			Vector2 vector = spot + Vector2.UnitY * num3 + Vector2.UnitX * 1f;
			spriteBatch.Draw(value, spot, new Rectangle?(new Rectangle(5, 0, value.Width - 9, value.Height)), BackColor, 0f, new Vector2(84.5f, 0f), new Vector2(num4, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(value, spot + new Vector2(-num4 * 84.5f - 5f, 0f), new Rectangle?(new Rectangle(0, 0, 5, value.Height)), BackColor, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.Draw(value, spot + new Vector2(num4 * 84.5f, 0f), new Rectangle?(new Rectangle(value.Width - 4, 0, 4, value.Height)), BackColor, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
			vector += Vector2.UnitX * (num - 0.5f) * num2;
			vector.X -= 1f;
			spriteBatch.Draw(value3, vector, new Rectangle?(new Rectangle(0, 0, 1, 1)), FillingColor, 0f, new Vector2(1f, 0.5f), new Vector2(num2 * num, num3), SpriteEffects.None, 0f);
			if (progress != 0f)
			{
				spriteBatch.Draw(value3, vector, new Rectangle?(new Rectangle(0, 0, 1, 1)), BlipColor, 0f, new Vector2(1f, 0.5f), new Vector2(2f, num3), SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(value3, vector, new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black, 0f, new Vector2(0f, 0.5f), new Vector2(num2 * (1f - num), num3), SpriteEffects.None, 0f);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x005AF55C File Offset: 0x005AD75C
		public override int CompareTo(object obj)
		{
			UIAchievementListItem uiachievementListItem = obj as UIAchievementListItem;
			if (uiachievementListItem == null)
			{
				return 0;
			}
			if (this._achievement.IsCompleted && !uiachievementListItem._achievement.IsCompleted)
			{
				return -1;
			}
			if (!this._achievement.IsCompleted && uiachievementListItem._achievement.IsCompleted)
			{
				return 1;
			}
			return this._achievement.Id.CompareTo(uiachievementListItem._achievement.Id);
		}

		// Token: 0x040055FA RID: 22010
		private Achievement _achievement;

		// Token: 0x040055FB RID: 22011
		private UIImageFramed _achievementIcon;

		// Token: 0x040055FC RID: 22012
		private UIImage _achievementIconBorders;

		// Token: 0x040055FD RID: 22013
		private const int _iconSize = 64;

		// Token: 0x040055FE RID: 22014
		private const int _iconSizeWithSpace = 66;

		// Token: 0x040055FF RID: 22015
		private const int _iconsPerRow = 8;

		// Token: 0x04005600 RID: 22016
		private int _iconIndex;

		// Token: 0x04005601 RID: 22017
		private Rectangle _iconFrame;

		// Token: 0x04005602 RID: 22018
		private Rectangle _iconFrameUnlocked;

		// Token: 0x04005603 RID: 22019
		private Rectangle _iconFrameLocked;

		// Token: 0x04005604 RID: 22020
		private Asset<Texture2D> _innerPanelTopTexture;

		// Token: 0x04005605 RID: 22021
		private Asset<Texture2D> _innerPanelBottomTexture;

		// Token: 0x04005606 RID: 22022
		private Asset<Texture2D> _categoryTexture;

		// Token: 0x04005607 RID: 22023
		private bool _locked;

		// Token: 0x04005608 RID: 22024
		private bool _large;
	}
}
