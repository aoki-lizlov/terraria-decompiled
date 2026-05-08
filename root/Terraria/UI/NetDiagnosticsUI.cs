using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x020000F2 RID: 242
	public class NetDiagnosticsUI : INetDiagnosticsUI
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x004E7880 File Offset: 0x004E5A80
		public void Reset()
		{
			this.bytesRecv = 0;
			this.bytesRecvLast = 0;
			this.bytesSent = 0;
			this.bytesSentLast = 0;
			for (int i = 0; i < this._counterByMessageId.Length; i++)
			{
				this._counterByMessageId[i].Reset();
			}
			this._counterByModuleId.Clear();
			this._counterByMessageId[10].exemptFromBadScoreTest = true;
			this._counterByMessageId[82].exemptFromBadScoreTest = true;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x004E7906 File Offset: 0x004E5B06
		public void CountReadMessage(int messageId, int messageLength)
		{
			Interlocked.Add(ref this.bytesRecv, messageLength);
			this._counterByMessageId[messageId].CountReadMessage(messageLength);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x004E7927 File Offset: 0x004E5B27
		public void CountSentMessage(int messageId, int messageLength)
		{
			Interlocked.Add(ref this.bytesSent, messageLength);
			this._counterByMessageId[messageId].CountSentMessage(messageLength);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x004E7948 File Offset: 0x004E5B48
		public void CountReadModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage counterForMessage;
			this._counterByModuleId.TryGetValue(moduleMessageId, out counterForMessage);
			counterForMessage.CountReadMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = counterForMessage;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x004E797C File Offset: 0x004E5B7C
		public void CountSentModuleMessage(int moduleMessageId, int messageLength)
		{
			NetDiagnosticsUI.CounterForMessage counterForMessage;
			this._counterByModuleId.TryGetValue(moduleMessageId, out counterForMessage);
			counterForMessage.CountSentMessage(messageLength);
			this._counterByModuleId[moduleMessageId] = counterForMessage;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x004E79AD File Offset: 0x004E5BAD
		public void RotateSendRecvCounters()
		{
			this.bytesRecvLast = Interlocked.Exchange(ref this.bytesRecv, 0);
			this.bytesSentLast = Interlocked.Exchange(ref this.bytesSent, 0);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x004E79D7 File Offset: 0x004E5BD7
		public void GetLastSentRecvBytes(out int sent, out int recv)
		{
			sent = this.bytesSentLast;
			recv = this.bytesRecvLast;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x004E79F0 File Offset: 0x004E5BF0
		public void Draw(SpriteBatch spriteBatch)
		{
			Utils.DrawBorderString(Main.spriteBatch, "Packet Stats (bytes) F8 to hide", new Vector2(800f, 80f), Color.White, 1f, 0f, 0f, -1);
			int num = this._counterByMessageId.Length + this._counterByModuleId.Count;
			for (int i = 0; i <= num / 51; i++)
			{
				Utils.DrawInvBG(spriteBatch, 190 + 400 * i, 110, 390, 683, default(Color));
			}
			Vector2 vector;
			for (int j = 0; j < this._counterByMessageId.Length; j++)
			{
				int num2 = j / 51;
				int num3 = j - num2 * 51;
				vector.X = (float)(200 + num2 * 400);
				vector.Y = (float)(120 + num3 * 13);
				this.DrawCounter(spriteBatch, ref this._counterByMessageId[j], j.ToString(), vector);
			}
			int num4 = this._counterByMessageId.Length + 1;
			foreach (KeyValuePair<int, NetDiagnosticsUI.CounterForMessage> keyValuePair in this._counterByModuleId)
			{
				int num5 = num4 / 51;
				int num6 = num4 - num5 * 51;
				vector.X = (float)(200 + num5 * 400);
				vector.Y = (float)(120 + num6 * 13);
				NetDiagnosticsUI.CounterForMessage value = keyValuePair.Value;
				this.DrawCounter(spriteBatch, ref value, ".." + keyValuePair.Key.ToString(), vector);
				num4++;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x004E7B9C File Offset: 0x004E5D9C
		private void DrawCounter(SpriteBatch spriteBatch, ref NetDiagnosticsUI.CounterForMessage counter, string title, Vector2 position)
		{
			if (!counter.exemptFromBadScoreTest)
			{
				if (this._highestFoundReadCount < counter.timesReceived)
				{
					this._highestFoundReadCount = counter.timesReceived;
				}
				if (this._highestFoundReadBytes < counter.bytesReceived)
				{
					this._highestFoundReadBytes = counter.bytesReceived;
				}
			}
			Vector2 vector = position;
			string text = title + ": ";
			float num = Utils.Remap((float)counter.bytesReceived, 0f, (float)this._highestFoundReadBytes, 0f, 1f, true);
			Color color = Main.hslToRgb(0.3f * (1f - num), 1f, 0.5f, byte.MaxValue);
			if (counter.exemptFromBadScoreTest)
			{
				color = Color.White;
			}
			string text2 = text;
			this.DrawText(spriteBatch, text2, vector, color);
			vector.X += 30f;
			text2 = "rx:" + string.Format("{0,0}", counter.timesReceived);
			this.DrawText(spriteBatch, text2, vector, color);
			vector.X += 70f;
			text2 = string.Format("{0,0}", counter.bytesReceived);
			this.DrawText(spriteBatch, text2, vector, color);
			vector.X += 70f;
			text2 = text;
			this.DrawText(spriteBatch, text2, vector, color);
			vector.X += 30f;
			text2 = "tx:" + string.Format("{0,0}", counter.timesSent);
			this.DrawText(spriteBatch, text2, vector, color);
			vector.X += 70f;
			text2 = string.Format("{0,0}", counter.bytesSent);
			this.DrawText(spriteBatch, text2, vector, color);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x004E7D48 File Offset: 0x004E5F48
		private void DrawText(SpriteBatch spriteBatch, string text, Vector2 pos, Color color)
		{
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, pos, color, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f, null, null);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x004E7D80 File Offset: 0x004E5F80
		public NetDiagnosticsUI()
		{
		}

		// Token: 0x04001333 RID: 4915
		private NetDiagnosticsUI.CounterForMessage[] _counterByMessageId = new NetDiagnosticsUI.CounterForMessage[(int)(MessageID.Count + 1)];

		// Token: 0x04001334 RID: 4916
		private Dictionary<int, NetDiagnosticsUI.CounterForMessage> _counterByModuleId = new Dictionary<int, NetDiagnosticsUI.CounterForMessage>();

		// Token: 0x04001335 RID: 4917
		private volatile int bytesRecv;

		// Token: 0x04001336 RID: 4918
		private volatile int bytesRecvLast;

		// Token: 0x04001337 RID: 4919
		private volatile int bytesSent;

		// Token: 0x04001338 RID: 4920
		private volatile int bytesSentLast;

		// Token: 0x04001339 RID: 4921
		private int _highestFoundReadBytes = 1;

		// Token: 0x0400133A RID: 4922
		private int _highestFoundReadCount = 1;

		// Token: 0x02000707 RID: 1799
		private struct CounterForMessage
		{
			// Token: 0x06003FF3 RID: 16371 RVA: 0x0069C99F File Offset: 0x0069AB9F
			public void Reset()
			{
				this.timesReceived = 0;
				this.timesSent = 0;
				this.bytesReceived = 0;
				this.bytesSent = 0;
			}

			// Token: 0x06003FF4 RID: 16372 RVA: 0x0069C9BD File Offset: 0x0069ABBD
			public void CountReadMessage(int messageLength)
			{
				this.timesReceived++;
				this.bytesReceived += messageLength;
			}

			// Token: 0x06003FF5 RID: 16373 RVA: 0x0069C9DB File Offset: 0x0069ABDB
			public void CountSentMessage(int messageLength)
			{
				this.timesSent++;
				this.bytesSent += messageLength;
			}

			// Token: 0x0400689E RID: 26782
			public int timesReceived;

			// Token: 0x0400689F RID: 26783
			public int timesSent;

			// Token: 0x040068A0 RID: 26784
			public int bytesReceived;

			// Token: 0x040068A1 RID: 26785
			public int bytesSent;

			// Token: 0x040068A2 RID: 26786
			public bool exemptFromBadScoreTest;
		}
	}
}
