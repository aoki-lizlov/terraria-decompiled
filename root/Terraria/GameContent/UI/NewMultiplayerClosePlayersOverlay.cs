using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000371 RID: 881
	public class NewMultiplayerClosePlayersOverlay : IMultiplayerClosePlayersOverlay
	{
		// Token: 0x06002944 RID: 10564 RVA: 0x00579E70 File Offset: 0x00578070
		public void Draw()
		{
			int teamNamePlateDistance = Main.teamNamePlateDistance;
			if (teamNamePlateDistance <= 0)
			{
				return;
			}
			this._playerOnScreenCache.Clear();
			this._playerOffScreenCache.Clear();
			SpriteBatch spriteBatch = Main.spriteBatch;
			int num = teamNamePlateDistance * 8;
			Player[] player = Main.player;
			int myPlayer = Main.myPlayer;
			byte mouseTextColor = Main.mouseTextColor;
			Color[] teamColor = Main.teamColor;
			Vector2 screenPosition = Main.screenPosition;
			Player player2 = player[myPlayer];
			float num2 = (float)mouseTextColor / 255f;
			if (Main.netMode == 0)
			{
				return;
			}
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			for (int i = 0; i < 255; i++)
			{
				if (i != myPlayer)
				{
					Player player3 = player[i];
					bool flag = player3.spectating == myPlayer;
					if (player3.active && (!player3.dead || flag || player3.ghost) && player3.team == player2.team)
					{
						if (player3.team == 0 && !flag)
						{
							return;
						}
						string name = player3.name;
						Vector2 vector;
						bool flag2;
						Vector2 vector2;
						NewMultiplayerClosePlayersOverlay.GetDistance(value, player3, name, out vector, out flag2, out vector2);
						Color color = new Color((int)((byte)((float)teamColor[player3.team].R * num2)), (int)((byte)((float)teamColor[player3.team].G * num2)), (int)((byte)((float)teamColor[player3.team].B * num2)), (int)mouseTextColor);
						if (flag2)
						{
							float num3 = player3.Distance(player2.Center);
							if (num3 <= (float)num)
							{
								vector.Y += 40f;
								float num4 = 20f;
								float num5 = -27f;
								num5 -= (vector2.X - 85f) / 2f;
								string textValue = Language.GetTextValue("GameUI.PlayerDistance", (int)(num3 / 16f * 2f));
								Vector2 vector3 = value.MeasureString(textValue);
								vector3.X = vector.X - num5;
								vector3.Y = vector.Y + vector2.Y / 2f - vector3.Y / 2f - num4;
								this._playerOffScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache(name, vector, color, vector3, textValue, player3, vector2, flag));
							}
						}
						else
						{
							this._playerOnScreenCache.Add(new NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache(name, vector, color));
						}
					}
				}
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			for (int j = 0; j < this._playerOnScreenCache.Count; j++)
			{
				this._playerOnScreenCache[j].DrawPlayerName_WhenPlayerIsOnScreen(spriteBatch);
			}
			for (int k = 0; k < this._playerOffScreenCache.Count; k++)
			{
				this._playerOffScreenCache[k].DrawPlayerName(spriteBatch);
			}
			for (int l = 0; l < this._playerOffScreenCache.Count; l++)
			{
				this._playerOffScreenCache[l].DrawPlayerDistance(spriteBatch);
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			for (int m = 0; m < this._playerOffScreenCache.Count; m++)
			{
				this._playerOffScreenCache[m].DrawLifeBar();
			}
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
			for (int n = 0; n < this._playerOffScreenCache.Count; n++)
			{
				this._playerOffScreenCache[n].DrawPlayerHead();
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0057A204 File Offset: 0x00578404
		private static void GetDistance(DynamicSpriteFont font, Player player, string nameToShow, out Vector2 namePlatePos, out bool offScreen, out Vector2 measurement)
		{
			measurement = font.MeasureString(nameToShow);
			namePlatePos = Main.GetChatDrawPosition(player);
			namePlatePos.Y -= measurement.Y / 2f;
			if (player.chatOverhead.timeLeft > 0 || player.emoteTime > 0)
			{
				namePlatePos.Y -= measurement.Y;
			}
			Vector2 vector = Main.ScreenSize.ToVector2() / Main.UIScale;
			Vector2 vector2 = vector / 2f;
			Vector2 vector3 = Vector2.Max(new Vector2(100f), vector / 2f - new Vector2(80f, 50f));
			Vector2 vector4 = namePlatePos - vector2;
			float num = (vector4 / vector3).Length();
			if (num > 1f)
			{
				offScreen = true;
				namePlatePos = vector2 + vector4 / num;
			}
			else
			{
				offScreen = false;
			}
			namePlatePos -= measurement / 2f;
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0057A322 File Offset: 0x00578522
		public NewMultiplayerClosePlayersOverlay()
		{
		}

		// Token: 0x040051DF RID: 20959
		private List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache> _playerOnScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOnScreenCache>();

		// Token: 0x040051E0 RID: 20960
		private List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache> _playerOffScreenCache = new List<NewMultiplayerClosePlayersOverlay.PlayerOffScreenCache>();

		// Token: 0x020008D2 RID: 2258
		private struct PlayerOnScreenCache
		{
			// Token: 0x0600466F RID: 18031 RVA: 0x006C6AF3 File Offset: 0x006C4CF3
			public PlayerOnScreenCache(string name, Vector2 pos, Color color)
			{
				this._name = name;
				this._pos = pos;
				this._color = color;
			}

			// Token: 0x06004670 RID: 18032 RVA: 0x006C6B0C File Offset: 0x006C4D0C
			public void DrawPlayerName_WhenPlayerIsOnScreen(SpriteBatch spriteBatch)
			{
				this._pos = this._pos.Floor();
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X - 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X + 2f, this._pos.Y), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y - 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, new Vector2(this._pos.X, this._pos.Y + 2f), Color.Black, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this._name, this._pos, this._color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
			}

			// Token: 0x0400736D RID: 29549
			private string _name;

			// Token: 0x0400736E RID: 29550
			private Vector2 _pos;

			// Token: 0x0400736F RID: 29551
			private Color _color;
		}

		// Token: 0x020008D3 RID: 2259
		private struct PlayerOffScreenCache
		{
			// Token: 0x06004671 RID: 18033 RVA: 0x006C6CC4 File Offset: 0x006C4EC4
			public PlayerOffScreenCache(string name, Vector2 pos, Color color, Vector2 npDistPos, string npDist, Player thePlayer, Vector2 theMeasurement, bool drawScryingOrb)
			{
				this.nameToShow = name;
				this.namePlatePos = pos.Floor();
				this.namePlateColor = color;
				this.distanceDrawPosition = npDistPos.Floor();
				this.distanceString = npDist;
				this.player = thePlayer;
				this.measurement = theMeasurement;
				this.drawScryingOrb = drawScryingOrb;
			}

			// Token: 0x06004672 RID: 18034 RVA: 0x006C6D18 File Offset: 0x006C4F18
			public void DrawPlayerName(SpriteBatch spriteBatch)
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, this.nameToShow, this.namePlatePos + new Vector2(0f, -40f), this.namePlateColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}

			// Token: 0x06004673 RID: 18035 RVA: 0x006C6D74 File Offset: 0x006C4F74
			public void DrawPlayerHead()
			{
				float num = 20f;
				float num2 = -27f;
				num2 -= (this.measurement.X - 85f) / 2f;
				Color playerHeadBordersColor = Main.GetPlayerHeadBordersColor(this.player);
				Vector2 vector = new Vector2(this.namePlatePos.X, this.namePlatePos.Y - num);
				vector.X -= 22f + num2;
				vector.Y += 8f;
				vector = vector.Floor();
				Main.MapPlayerRenderer.DrawPlayerHead(Main.Camera, this.player, vector, 1f, 0.8f, playerHeadBordersColor);
				if (this.drawScryingOrb)
				{
					Texture2D texture2D;
					Rectangle rectangle;
					Main.GetItemDrawFrame(5644, out texture2D, out rectangle);
					Main.spriteBatch.Draw(texture2D, vector + new Vector2(-26f, 4f), new Rectangle?(rectangle), Color.White, 0f, rectangle.Size() / 2f, 1f, SpriteEffects.None, 0f);
				}
			}

			// Token: 0x06004674 RID: 18036 RVA: 0x006C6E80 File Offset: 0x006C5080
			public void DrawLifeBar()
			{
				Vector2 vector = Main.screenPosition + this.distanceDrawPosition + new Vector2(26f, 20f);
				if (this.player.statLife != this.player.statLifeMax2)
				{
					Main.instance.DrawHealthBar(vector.X, vector.Y, this.player.statLife, this.player.statLifeMax2, 1f, 1.25f, true);
				}
			}

			// Token: 0x06004675 RID: 18037 RVA: 0x006C6F04 File Offset: 0x006C5104
			public void DrawPlayerDistance(SpriteBatch spriteBatch)
			{
				float num = 0.85f;
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X - 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X + 2f, this.distanceDrawPosition.Y), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y - 2f), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, new Vector2(this.distanceDrawPosition.X, this.distanceDrawPosition.Y + 2f), Color.Black, 0f, default(Vector2), num, SpriteEffects.None, 0f, null, null);
				DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, this.distanceString, this.distanceDrawPosition, this.namePlateColor, 0f, default(Vector2), num, SpriteEffects.None, 0f, null, null);
			}

			// Token: 0x04007370 RID: 29552
			private Player player;

			// Token: 0x04007371 RID: 29553
			private string nameToShow;

			// Token: 0x04007372 RID: 29554
			private Vector2 namePlatePos;

			// Token: 0x04007373 RID: 29555
			private Color namePlateColor;

			// Token: 0x04007374 RID: 29556
			private Vector2 distanceDrawPosition;

			// Token: 0x04007375 RID: 29557
			private string distanceString;

			// Token: 0x04007376 RID: 29558
			private Vector2 measurement;

			// Token: 0x04007377 RID: 29559
			private bool drawScryingOrb;
		}
	}
}
