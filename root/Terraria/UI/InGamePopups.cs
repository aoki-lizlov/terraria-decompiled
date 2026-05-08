using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Achievements;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Social.Base;

namespace Terraria.UI
{
	// Token: 0x020000F8 RID: 248
	public class InGamePopups
	{
		// Token: 0x06001956 RID: 6486 RVA: 0x0000357B File Offset: 0x0000177B
		public InGamePopups()
		{
		}

		// Token: 0x02000708 RID: 1800
		public class AchievementUnlockedPopup : IInGameNotification
		{
			// Token: 0x17000509 RID: 1289
			// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x0069C9F9 File Offset: 0x0069ABF9
			// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x0069CA01 File Offset: 0x0069AC01
			public bool ShouldBeRemoved
			{
				[CompilerGenerated]
				get
				{
					return this.<ShouldBeRemoved>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<ShouldBeRemoved>k__BackingField = value;
				}
			}

			// Token: 0x1700050A RID: 1290
			// (get) Token: 0x06003FF8 RID: 16376 RVA: 0x0069CA0A File Offset: 0x0069AC0A
			// (set) Token: 0x06003FF9 RID: 16377 RVA: 0x0069CA12 File Offset: 0x0069AC12
			public object CreationObject
			{
				[CompilerGenerated]
				get
				{
					return this.<CreationObject>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CreationObject>k__BackingField = value;
				}
			}

			// Token: 0x06003FFA RID: 16378 RVA: 0x0069CA1C File Offset: 0x0069AC1C
			public AchievementUnlockedPopup(Achievement achievement)
			{
				this.CreationObject = achievement;
				this._ingameDisplayTimeLeft = 300;
				this._theAchievement = achievement;
				this._title = achievement.FriendlyName.Value;
				int iconIndex = Main.Achievements.GetIconIndex(achievement.Name);
				this._iconIndex = iconIndex;
				this._achievementIconFrame = new Rectangle(iconIndex % 8 * 66, iconIndex / 8 * 66, 64, 64);
				this._achievementTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievements", 2);
				this._achievementBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders", 2);
			}

			// Token: 0x06003FFB RID: 16379 RVA: 0x0069CAB8 File Offset: 0x0069ACB8
			public void Update()
			{
				this._ingameDisplayTimeLeft--;
				if (this._ingameDisplayTimeLeft < 0)
				{
					this._ingameDisplayTimeLeft = 0;
				}
			}

			// Token: 0x1700050B RID: 1291
			// (get) Token: 0x06003FFC RID: 16380 RVA: 0x0069CAD8 File Offset: 0x0069ACD8
			private float Scale
			{
				get
				{
					if (this._ingameDisplayTimeLeft < 30)
					{
						return MathHelper.Lerp(0f, 1f, (float)this._ingameDisplayTimeLeft / 30f);
					}
					if (this._ingameDisplayTimeLeft > 285)
					{
						return MathHelper.Lerp(1f, 0f, ((float)this._ingameDisplayTimeLeft - 285f) / 15f);
					}
					return 1f;
				}
			}

			// Token: 0x1700050C RID: 1292
			// (get) Token: 0x06003FFD RID: 16381 RVA: 0x0069CB44 File Offset: 0x0069AD44
			private float Opacity
			{
				get
				{
					float scale = this.Scale;
					if (scale <= 0.5f)
					{
						return 0f;
					}
					return (scale - 0.5f) / 0.5f;
				}
			}

			// Token: 0x06003FFE RID: 16382 RVA: 0x0069CB74 File Offset: 0x0069AD74
			public void PushAnchor(ref Vector2 anchorPosition)
			{
				float num = 50f * this.Opacity;
				anchorPosition.Y -= num;
			}

			// Token: 0x06003FFF RID: 16383 RVA: 0x0069CB9C File Offset: 0x0069AD9C
			public void DrawInGame(SpriteBatch sb, Vector2 bottomAnchorPosition)
			{
				float opacity = this.Opacity;
				if (opacity > 0f)
				{
					float num = this.Scale * 1.1f;
					Vector2 vector = (FontAssets.ItemStack.Value.MeasureString(this._title) + new Vector2(58f, 10f)) * num;
					Rectangle rectangle = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, -vector.Y * 0.5f), vector);
					Vector2 mouseScreen = Main.MouseScreen;
					bool flag = rectangle.Contains(mouseScreen.ToPoint());
					Color color = (flag ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f));
					Utils.DrawInvBG(sb, rectangle, color);
					float num2 = num * 0.3f;
					Vector2 vector2 = rectangle.Right() - Vector2.UnitX * num * (12f + num2 * (float)this._achievementIconFrame.Width);
					sb.Draw(this._achievementTexture.Value, vector2, new Rectangle?(this._achievementIconFrame), Color.White * opacity, 0f, new Vector2(0f, (float)(this._achievementIconFrame.Height / 2)), num2, SpriteEffects.None, 0f);
					sb.Draw(this._achievementBorderTexture.Value, vector2, null, Color.White * opacity, 0f, new Vector2(4f, (float)(this._achievementBorderTexture.Height() / 2)), num2, SpriteEffects.None, 0f);
					Color color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
					Utils.DrawBorderString(sb, this._title, vector2 - Vector2.UnitX * 10f, color2 * opacity, num * 0.9f, 1f, 0.4f, -1);
					if (flag)
					{
						this.OnMouseOver();
					}
				}
			}

			// Token: 0x06004000 RID: 16384 RVA: 0x0069CDAC File Offset: 0x0069AFAC
			private void OnMouseOver()
			{
				if (PlayerInput.IgnoreMouseInterface)
				{
					return;
				}
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Main.mouseLeftRelease = false;
					if (Main.gameMenu)
					{
						if (Main.menuMode == 0)
						{
							IngameFancyUI.OpenAchievementsAndGoto(this._theAchievement);
						}
					}
					else
					{
						IngameFancyUI.OpenAchievementsAndGoto(this._theAchievement);
					}
					this._ingameDisplayTimeLeft = 0;
					this.ShouldBeRemoved = true;
				}
			}

			// Token: 0x06004001 RID: 16385 RVA: 0x0069CE1A File Offset: 0x0069B01A
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				Utils.DrawInvBG(spriteBatch, area, Color.Red);
			}

			// Token: 0x040068A3 RID: 26787
			private Achievement _theAchievement;

			// Token: 0x040068A4 RID: 26788
			private Asset<Texture2D> _achievementTexture;

			// Token: 0x040068A5 RID: 26789
			private Asset<Texture2D> _achievementBorderTexture;

			// Token: 0x040068A6 RID: 26790
			private const int _iconSize = 64;

			// Token: 0x040068A7 RID: 26791
			private const int _iconSizeWithSpace = 66;

			// Token: 0x040068A8 RID: 26792
			private const int _iconsPerRow = 8;

			// Token: 0x040068A9 RID: 26793
			private int _iconIndex;

			// Token: 0x040068AA RID: 26794
			private Rectangle _achievementIconFrame;

			// Token: 0x040068AB RID: 26795
			private string _title;

			// Token: 0x040068AC RID: 26796
			private int _ingameDisplayTimeLeft;

			// Token: 0x040068AD RID: 26797
			[CompilerGenerated]
			private bool <ShouldBeRemoved>k__BackingField;

			// Token: 0x040068AE RID: 26798
			[CompilerGenerated]
			private object <CreationObject>k__BackingField;
		}

		// Token: 0x02000709 RID: 1801
		public class PlayerWantsToJoinGamePopup : IInGameNotification
		{
			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06004002 RID: 16386 RVA: 0x0069CE28 File Offset: 0x0069B028
			private float Scale
			{
				get
				{
					if (this._timeLeft < 30)
					{
						return MathHelper.Lerp(0f, 1f, (float)this._timeLeft / 30f);
					}
					if (this._timeLeft > 1785)
					{
						return MathHelper.Lerp(1f, 0f, ((float)this._timeLeft - 1785f) / 15f);
					}
					return 1f;
				}
			}

			// Token: 0x1700050E RID: 1294
			// (get) Token: 0x06004003 RID: 16387 RVA: 0x0069CE94 File Offset: 0x0069B094
			private float Opacity
			{
				get
				{
					float scale = this.Scale;
					if (scale <= 0.5f)
					{
						return 0f;
					}
					return (scale - 0.5f) / 0.5f;
				}
			}

			// Token: 0x1700050F RID: 1295
			// (get) Token: 0x06004004 RID: 16388 RVA: 0x0069CEC3 File Offset: 0x0069B0C3
			// (set) Token: 0x06004005 RID: 16389 RVA: 0x0069CECB File Offset: 0x0069B0CB
			public object CreationObject
			{
				[CompilerGenerated]
				get
				{
					return this.<CreationObject>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<CreationObject>k__BackingField = value;
				}
			}

			// Token: 0x06004006 RID: 16390 RVA: 0x0069CED4 File Offset: 0x0069B0D4
			public PlayerWantsToJoinGamePopup(UserJoinToServerRequest request)
			{
				this._request = request;
				this.CreationObject = request;
				this._timeLeft = 1800;
			}

			// Token: 0x17000510 RID: 1296
			// (get) Token: 0x06004007 RID: 16391 RVA: 0x0069CEF5 File Offset: 0x0069B0F5
			public bool ShouldBeRemoved
			{
				get
				{
					return this._timeLeft <= 0;
				}
			}

			// Token: 0x06004008 RID: 16392 RVA: 0x0069CF03 File Offset: 0x0069B103
			public void Update()
			{
				this._timeLeft--;
			}

			// Token: 0x06004009 RID: 16393 RVA: 0x0069CF14 File Offset: 0x0069B114
			public void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition)
			{
				float opacity = this.Opacity;
				if (opacity > 0f)
				{
					string text = Utils.FormatWith(this._request.GetUserWrapperText(), new
					{
						DisplayName = this._request.UserDisplayName,
						FullId = this._request.UserFullIdentifier
					});
					float num = this.Scale * 1.1f;
					Vector2 vector = (FontAssets.ItemStack.Value.MeasureString(text) + new Vector2(58f, 10f)) * num;
					Rectangle rectangle = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, -vector.Y * 0.5f), vector);
					Vector2 mouseScreen = Main.MouseScreen;
					Color color = (rectangle.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f));
					Utils.DrawInvBG(spriteBatch, rectangle, color);
					Vector2 vector2 = new Vector2((float)rectangle.Left, (float)rectangle.Center.Y);
					vector2.X += 32f;
					Texture2D texture2D = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1).Value;
					Vector2 vector3 = new Vector2((float)(rectangle.Left + 7), MathHelper.Lerp((float)rectangle.Top, (float)rectangle.Bottom, 0.5f) - (float)(texture2D.Height / 2) - 1f);
					bool flag = Utils.CenteredRectangle(vector3 + new Vector2((float)(texture2D.Width / 2), 0f), texture2D.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(texture2D, vector3, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * texture2D.Size(), 1f, SpriteEffects.None, 0f);
					if (flag)
					{
						this.OnMouseOver(false);
					}
					texture2D = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1).Value;
					vector3 = new Vector2((float)(rectangle.Left + 7), MathHelper.Lerp((float)rectangle.Top, (float)rectangle.Bottom, 0.5f) + (float)(texture2D.Height / 2) + 1f);
					flag = Utils.CenteredRectangle(vector3 + new Vector2((float)(texture2D.Width / 2), 0f), texture2D.Size()).Contains(mouseScreen.ToPoint());
					spriteBatch.Draw(texture2D, vector3, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * texture2D.Size(), 1f, SpriteEffects.None, 0f);
					if (flag)
					{
						this.OnMouseOver(true);
					}
					Color color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
					Utils.DrawBorderString(spriteBatch, text, rectangle.Center.ToVector2() + new Vector2(10f, 0f), color2 * opacity, num * 0.9f, 0.5f, 0.4f, -1);
				}
			}

			// Token: 0x0600400A RID: 16394 RVA: 0x0069D280 File Offset: 0x0069B480
			private void OnMouseOver(bool reject = false)
			{
				if (PlayerInput.IgnoreMouseInterface)
				{
					return;
				}
				Main.player[Main.myPlayer].mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					Main.mouseLeftRelease = false;
					this._timeLeft = 0;
					if (reject)
					{
						this._request.Reject();
						return;
					}
					this._request.Accept();
				}
			}

			// Token: 0x0600400B RID: 16395 RVA: 0x0069D2DC File Offset: 0x0069B4DC
			public void PushAnchor(ref Vector2 positionAnchorBottom)
			{
				float num = 70f * this.Opacity;
				positionAnchorBottom.Y -= num;
			}

			// Token: 0x0600400C RID: 16396 RVA: 0x0069D304 File Offset: 0x0069B504
			public void DrawInNotificationsArea(SpriteBatch spriteBatch, Rectangle area, ref int gamepadPointLocalIndexTouse)
			{
				string userWrapperText = this._request.GetUserWrapperText();
				string userDisplayName = this._request.UserDisplayName;
				Utils.TrimTextIfNeeded(ref userDisplayName, FontAssets.MouseText.Value, 0.9f, (float)(area.Width / 4));
				string text = Utils.FormatWith(userWrapperText, new
				{
					DisplayName = userDisplayName,
					FullId = this._request.UserFullIdentifier
				});
				Vector2 mouseScreen = Main.MouseScreen;
				Color color = (area.Contains(mouseScreen.ToPoint()) ? (new Color(64, 109, 164) * 0.75f) : (new Color(64, 109, 164) * 0.5f));
				Utils.DrawInvBG(spriteBatch, area, color);
				Vector2 vector = new Vector2((float)area.Left, (float)area.Center.Y);
				vector.X += 32f;
				Texture2D texture2D = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1).Value;
				Vector2 vector2 = new Vector2((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) - (float)(texture2D.Height / 2) - 1f);
				bool flag = Utils.CenteredRectangle(vector2 + new Vector2((float)(texture2D.Width / 2), 0f), texture2D.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(texture2D, vector2, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * texture2D.Size(), 1f, SpriteEffects.None, 0f);
				if (flag)
				{
					this.OnMouseOver(false);
				}
				texture2D = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1).Value;
				vector2 = new Vector2((float)(area.Left + 7), MathHelper.Lerp((float)area.Top, (float)area.Bottom, 0.5f) + (float)(texture2D.Height / 2) + 1f);
				flag = Utils.CenteredRectangle(vector2 + new Vector2((float)(texture2D.Width / 2), 0f), texture2D.Size()).Contains(mouseScreen.ToPoint());
				spriteBatch.Draw(texture2D, vector2, null, Color.White * (flag ? 1f : 0.5f), 0f, new Vector2(0f, 0.5f) * texture2D.Size(), 1f, SpriteEffects.None, 0f);
				if (flag)
				{
					this.OnMouseOver(true);
				}
				vector.X += 6f;
				Color color2 = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)(Main.mouseTextColor / 5), (int)Main.mouseTextColor);
				Utils.DrawBorderString(spriteBatch, text, vector, color2, 0.9f, 0f, 0.4f, -1);
			}

			// Token: 0x040068AF RID: 26799
			private int _timeLeft;

			// Token: 0x040068B0 RID: 26800
			private const int _timeLeftMax = 1800;

			// Token: 0x040068B1 RID: 26801
			private UserJoinToServerRequest _request;

			// Token: 0x040068B2 RID: 26802
			[CompilerGenerated]
			private object <CreationObject>k__BackingField;
		}
	}
}
