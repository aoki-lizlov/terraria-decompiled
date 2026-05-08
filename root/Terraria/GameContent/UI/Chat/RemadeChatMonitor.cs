using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.GameInput;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x02000381 RID: 897
	public class RemadeChatMonitor : IChatMonitor
	{
		// Token: 0x060029AD RID: 10669 RVA: 0x0057E31F File Offset: 0x0057C51F
		public RemadeChatMonitor()
		{
			this._showCount = 10;
			this._startChatLine = 0;
			this._messages = new List<ChatMessageContainer>();
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x0057E341 File Offset: 0x0057C541
		public void NewText(string newText, byte R = 255, byte G = 255, byte B = 255)
		{
			this.AddNewMessage(newText, new Color((int)R, (int)G, (int)B), -1);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x0057E354 File Offset: 0x0057C554
		public void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1)
		{
			this.AddNewMessage(text, c, WidthLimit);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x0057E360 File Offset: 0x0057C560
		public void AddNewMessage(string text, Color color, int widthLimitInPixels = -1)
		{
			Trace.WriteLine("[chat] " + text);
			ChatMessageContainer chatMessageContainer = new ChatMessageContainer();
			chatMessageContainer.SetContents(text, color, widthLimitInPixels);
			this._messages.Insert(0, chatMessageContainer);
			while (this._messages.Count > 500)
			{
				this._messages.RemoveAt(this._messages.Count - 1);
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x0057E3C8 File Offset: 0x0057C5C8
		public void DrawChat(bool drawingPlayerChat)
		{
			int num = this._startChatLine;
			int num2 = 0;
			int num3 = 0;
			while (num > 0 && num2 < this._messages.Count)
			{
				int num4 = Math.Min(num, this._messages[num2].LineCount);
				num -= num4;
				num3 += num4;
				if (num3 == this._messages[num2].LineCount)
				{
					num3 = 0;
					num2++;
				}
			}
			int num5 = 0;
			int? num6 = null;
			int num7 = -1;
			int? num8 = null;
			int num9 = -1;
			while (num5 < this._showCount && num2 < this._messages.Count)
			{
				ChatMessageContainer chatMessageContainer = this._messages[num2];
				if (!chatMessageContainer.Prepared || !(drawingPlayerChat | chatMessageContainer.CanBeShownWhenChatIsClosed))
				{
					break;
				}
				TextSnippet[] snippetWithInversedIndex = chatMessageContainer.GetSnippetWithInversedIndex(num3);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, snippetWithInversedIndex, new Vector2(88f, (float)(Main.screenHeight - 30 - 28 - num5 * 22)), 0f, Vector2.Zero, Vector2.One, out num9, -1f, 2f);
				if (num9 >= 0)
				{
					num8 = new int?(num9);
					num6 = new int?(num2);
					num7 = num3;
				}
				num5++;
				num3++;
				if (num3 >= chatMessageContainer.LineCount)
				{
					num3 = 0;
					num2++;
				}
			}
			if (num6 != null && num8 != null && !PlayerInput.IgnoreMouseInterface)
			{
				TextSnippet[] snippetWithInversedIndex2 = this._messages[num6.Value].GetSnippetWithInversedIndex(num7);
				snippetWithInversedIndex2[num8.Value].OnHover();
				Main.LocalPlayer.mouseInterface = true;
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					snippetWithInversedIndex2[num8.Value].OnClick();
				}
			}
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x0057E582 File Offset: 0x0057C782
		public void Clear()
		{
			this._messages.Clear();
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x0057E590 File Offset: 0x0057C790
		public void Update()
		{
			if (this._lastChatWidthLimit != Main.ChatLineWidthLimit)
			{
				this._lastChatWidthLimit = Main.ChatLineWidthLimit;
				foreach (ChatMessageContainer chatMessageContainer in this._messages)
				{
					chatMessageContainer.OnWidthLimitChanged();
				}
			}
			foreach (ChatMessageContainer chatMessageContainer2 in this._messages)
			{
				chatMessageContainer2.Update();
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x0057E638 File Offset: 0x0057C838
		public void Offset(int linesOffset)
		{
			this._startChatLine += linesOffset;
			this.ClampMessageIndex();
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0057E650 File Offset: 0x0057C850
		private void ClampMessageIndex()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = this._startChatLine + this._showCount;
			while (num < num4 && num2 < this._messages.Count)
			{
				int num5 = Math.Min(num4 - num, this._messages[num2].LineCount);
				num += num5;
				if (num < num4)
				{
					num2++;
					num3 = 0;
				}
				else
				{
					num3 = num5;
				}
			}
			int num6 = this._showCount;
			while (num6 > 0 && num > 0)
			{
				num3--;
				num6--;
				num--;
				if (num3 < 0)
				{
					num2--;
					if (num2 == -1)
					{
						break;
					}
					num3 = this._messages[num2].LineCount - 1;
				}
			}
			this._startChatLine = num;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x0057E6FC File Offset: 0x0057C8FC
		public void ResetOffset()
		{
			this._startChatLine = 0;
		}

		// Token: 0x040052AA RID: 21162
		private const int MaxMessages = 500;

		// Token: 0x040052AB RID: 21163
		private int _showCount;

		// Token: 0x040052AC RID: 21164
		private int _startChatLine;

		// Token: 0x040052AD RID: 21165
		private List<ChatMessageContainer> _messages;

		// Token: 0x040052AE RID: 21166
		private int _lastChatWidthLimit;
	}
}
