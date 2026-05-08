using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.Localization.IME;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000366 RID: 870
	public class NPCChatPanel
	{
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x005396F7 File Offset: 0x005378F7
		private Player LocalPlayer
		{
			get
			{
				return Main.LocalPlayer;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x00577408 File Offset: 0x00575608
		private byte mouseTextColor
		{
			get
			{
				return Main.mouseTextColor;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x0057740F File Offset: 0x0057560F
		public bool allowRichText
		{
			get
			{
				return this.LocalPlayer.talkNPC != -1;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x00577422 File Offset: 0x00575622
		public bool InVirtualKeyboard
		{
			get
			{
				return Main.InGameUI.CurrentState is UIVirtualKeyboard && PlayerInput.UsingGamepad;
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x0057743C File Offset: 0x0057563C
		public void Draw()
		{
			if (!this.CanHoldConversation())
			{
				this.Close();
				return;
			}
			this.PrepareText();
			this.PrepareInteractions();
			this.PrepareVirtualKeyboard();
			Color color = new Color(200, 200, 200, 200);
			int num = (int)((this.mouseTextColor * 2 + byte.MaxValue) / 3);
			Color color2 = new Color(num, num, num, num);
			Point point = new Point(500, 500);
			Rectangle rectangle = new Rectangle(Main.screenWidth / 2 - point.X / 2, 100, point.X, 30);
			rectangle.Height += 30 * this._textDisplayCache.AmountOfLines;
			rectangle.Height += 30 * this._neededInteractionLines + Math.Max(0, 2 * (this._neededInteractionLines - 1));
			Utils.DrawInvBG(Main.spriteBatch, rectangle, default(Color));
			this.DrawText(color2, rectangle);
			Main.DrawNPCPortrait(color, rectangle.TopLeft());
			Main.DrawNPCChatBottomRightItem(rectangle.BottomRight());
			if (!PlayerInput.IgnoreMouseInterface && rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
			{
				this.LocalPlayer.mouseInterface = true;
			}
			this.DrawButtons(rectangle, color2);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00577578 File Offset: 0x00575778
		private void DrawButtons(Rectangle panelArea, Color chatColor)
		{
			UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsNew = true;
			UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsCount = this._interactions.Count;
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			Vector2 vector = panelArea.BottomLeft() + new Vector2(30f, (float)(-22 * this._neededInteractionLines + Math.Max(0, 2 * (this._neededInteractionLines - 1)) - 4));
			int num = -1;
			int num2 = -1;
			float num3 = 0.9f;
			Rectangle rectangle = new Rectangle((int)vector.X, (int)vector.Y, 100, 22);
			foreach (NPCInteraction npcinteraction in this._interactions)
			{
				num++;
				byte mouseTextColor = this.mouseTextColor;
				chatColor = new Color((int)mouseTextColor, (int)((double)mouseTextColor / 1.1), (int)(mouseTextColor / 2), (int)mouseTextColor);
				if (num % 4 == 0)
				{
					rectangle.X = (int)vector.X;
					rectangle.Y = num / 4 * 22 + (int)vector.Y;
				}
				string text = npcinteraction.GetText();
				int num4 = 0;
				bool flag = npcinteraction.TryAddCoins(ref chatColor, out num4);
				float num5 = 1f;
				Vector2 vector2 = ChatManager.GetStringSize(value, text, new Vector2(num3), -1f);
				if (vector2.X > 260f)
				{
					num5 *= 260f / vector2.X;
				}
				rectangle.Width = (int)(vector2.X * num5);
				bool flag2 = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
				Vector2 vector3 = new Vector2(flag2 ? 1.2f : num3);
				Vector2 vector4 = new Vector2(0f, vector2.Y * 0.5f);
				Color color = (flag2 ? Color.Brown : Color.Black);
				Vector2 vector5 = new Vector2((float)rectangle.Left, (float)rectangle.Center.Y);
				if (flag2)
				{
					vector5.X -= (float)((int)((1.2f - num3) * (float)rectangle.Width * 0.5f));
					vector2 *= 1.2f / num3;
				}
				if (flag2)
				{
					num2 = num;
				}
				ChatManager.DrawColorCodedStringShadow(Main.spriteBatch, value, text, vector5, color, 0f, vector4, vector3 * num5, -1f, 2f);
				ChatManager.DrawColorCodedString(Main.spriteBatch, value, text, vector5, chatColor, 0f, vector4, vector3 * num5, -1f, false);
				UILinkPointNavigator.SetPosition(2500 + num, rectangle.Center.ToVector2());
				rectangle.X += rectangle.Width + 30;
				if (npcinteraction.ShowExcalmation)
				{
					Utils.DrawNotificationIcon(Main.spriteBatch, vector5 + new Vector2(vector2.X * num5, 0f) + new Vector2(8f, 0f), 0f, false);
				}
				if (flag)
				{
					ItemSlot.DrawMoney(Main.spriteBatch, "", (float)(rectangle.X - 45), (float)(rectangle.Y - 44), Utils.CoinsSplit((long)num4), true, false);
					rectangle.X += 106;
				}
				if (!PlayerInput.IgnoreMouseInterface && flag2)
				{
					this.LocalPlayer.mouseInterface = true;
					this.LocalPlayer.releaseUseItem = false;
					num2 = num;
					if (Main.mouseLeft && Main.mouseLeftRelease)
					{
						Main.mouseLeftRelease = false;
						npcinteraction.Interact();
					}
				}
			}
			if (this._lastHovered != num2 && (!PlayerInput.UsingGamepad || num2 != -1))
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			this._lastHovered = num2;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00577930 File Offset: 0x00575B30
		private void PrepareInteractions()
		{
			this._interactions.Clear();
			foreach (NPCInteraction npcinteraction in NPCInteractions.All)
			{
				if (npcinteraction.Condition())
				{
					this._interactions.Add(npcinteraction);
				}
			}
			int count = this._interactions.Count;
			this._neededInteractionLines = (int)Math.Ceiling((double)((float)count / 4f));
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x005779BC File Offset: 0x00575BBC
		private void PrepareVirtualKeyboard()
		{
			int num = 120 + this._textDisplayCache.AmountOfLines * 30 + 30;
			num -= 235;
			UIVirtualKeyboard.ShouldHideText = !PlayerInput.SettingsForUI.ShowGamepadHints;
			if (!PlayerInput.UsingGamepad)
			{
				num = 9999;
			}
			UIVirtualKeyboard.OffsetDown = num;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00577A08 File Offset: 0x00575C08
		private void PrepareText()
		{
			string npcChatText = Main.npcChatText;
			this.OverrideChatTextWithShenanigans(ref npcChatText);
			this._textDisplayCache.PrepareCache(npcChatText);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00577A30 File Offset: 0x00575C30
		private void OverrideChatTextWithShenanigans(ref string chatTextToShow)
		{
			object obj = this.LocalPlayer.talkNPC != -1 && Main.CanDryadPlayStardewAnimation(this.LocalPlayer, Main.npc[this.LocalPlayer.talkNPC]);
			int num = 24;
			if (this.LocalPlayer.talkNPC != -1 && Main.npc[this.LocalPlayer.talkNPC].ai[0] == (float)num && NPC.RerollDryadText == 2)
			{
				NPC.RerollDryadText = 1;
			}
			object obj2 = obj;
			if (obj2 != null && NPC.RerollDryadText == 1 && Main.npc[this.LocalPlayer.talkNPC].ai[0] != (float)num && this.LocalPlayer.talkNPC != -1 && Main.npc[this.LocalPlayer.talkNPC].active && Main.npc[this.LocalPlayer.talkNPC].type == 20)
			{
				NPC.RerollDryadText = 0;
				chatTextToShow = (Main.npcChatText = Main.npc[this.LocalPlayer.talkNPC].GetChat());
				NPC.PreventJojaColaDialog = true;
			}
			if (obj2 != null && !NPC.PreventJojaColaDialog)
			{
				chatTextToShow = Language.GetTextValue("StardewTalk.PlayerHasColaAndIsHoldingIt");
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00577B54 File Offset: 0x00575D54
		private void DrawText(Color textColor, Rectangle textArea)
		{
			Vector2 vector = textArea.TopLeft() + new Vector2(20f, 20f);
			DynamicSpriteFont value = FontAssets.MouseText.Value;
			string[] textLines = this._textDisplayCache.TextLines;
			int amountOfLines = this._textDisplayCache.AmountOfLines;
			for (int i = 0; i < amountOfLines; i++)
			{
				string text = textLines[i];
				if (text != null)
				{
					if (this.allowRichText)
					{
						ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, value, text, vector + new Vector2(0f, (float)(i * 30)), textColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
					}
					else
					{
						Utils.DrawBorderStringFourWay(Main.spriteBatch, value, text, vector.X, vector.Y + (float)(i * 30), textColor, Color.Black, Vector2.Zero, 1f);
					}
				}
			}
			if (!Main.editSign || textLines[amountOfLines - 1] == null)
			{
				return;
			}
			Vector2 vector2 = vector + new Vector2(0f, (float)((amountOfLines - 1) * 30));
			vector2.X += value.MeasureString(textLines[amountOfLines - 1]).X;
			string compositionString = Platform.Get<IImeService>().CompositionString;
			if (compositionString != null && compositionString.Length > 0)
			{
				float x = value.MeasureString(compositionString).X;
				if (x + vector2.X - vector.X > 460f)
				{
					vector2 = vector + new Vector2(0f, (float)(amountOfLines * 30));
				}
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, value, compositionString, vector2, Main.imeCompositionStringColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				Main.instance.SetIMEPanelAnchor(vector2 + new Vector2(0f, 54f), 0f);
				vector2.X += x;
			}
			int num = this.textBlinkerCount + 1;
			this.textBlinkerCount = num;
			if (num >= 20)
			{
				this.textBlinkerState = ((this.textBlinkerState == 0) ? 1 : 0);
				this.textBlinkerCount = 0;
			}
			if (this.textBlinkerState == 1)
			{
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, value, "|", vector2, textColor, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00577DA2 File Offset: 0x00575FA2
		public void Close()
		{
			this._lastHovered = -1;
			this.ClearNPCChatText();
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00577DB1 File Offset: 0x00575FB1
		private void ClearNPCChatText()
		{
			Main.npcChatText = "";
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00577DBD File Offset: 0x00575FBD
		public bool CanHoldConversation()
		{
			return this.LocalPlayer.talkNPC >= 0 || this.LocalPlayer.sign != -1;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x00577DE0 File Offset: 0x00575FE0
		public NPCChatPanel()
		{
		}

		// Token: 0x040051A8 RID: 20904
		private int textBlinkerCount;

		// Token: 0x040051A9 RID: 20905
		private int textBlinkerState;

		// Token: 0x040051AA RID: 20906
		private List<NPCInteraction> _interactions = new List<NPCInteraction>();

		// Token: 0x040051AB RID: 20907
		private TextDisplayCache _textDisplayCache = new TextDisplayCache();

		// Token: 0x040051AC RID: 20908
		private int _neededInteractionLines;

		// Token: 0x040051AD RID: 20909
		public const int AllowedInteractionsPerLine = 4;

		// Token: 0x040051AE RID: 20910
		private int _lastHovered = -1;
	}
}
