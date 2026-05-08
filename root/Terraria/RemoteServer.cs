using System;
using System.IO;
using Terraria.Net.Sockets;

namespace Terraria
{
	// Token: 0x02000032 RID: 50
	public class RemoteServer
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0004256F File Offset: 0x0004076F
		public bool HideStatusTextPercent
		{
			get
			{
				return this.ServerSpecialFlags[0];
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0004257D File Offset: 0x0004077D
		public bool StatusTextHasShadows
		{
			get
			{
				return this.ServerSpecialFlags[1];
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0004258B File Offset: 0x0004078B
		public bool ServerWantsToRunCheckBytesInClientLoopThread
		{
			get
			{
				return this.ServerSpecialFlags[2];
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00042599 File Offset: 0x00040799
		public void ResetSpecialFlags()
		{
			this.ServerSpecialFlags = 0;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000425A7 File Offset: 0x000407A7
		public bool ReadBufferFull
		{
			get
			{
				return NetMessage.buffer[256].RemainingReadBufferLength < this.ReadBuffer.Length;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000425C3 File Offset: 0x000407C3
		public bool IsConnected()
		{
			return !this.PendingTermination && this.Socket.IsConnected();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000425DA File Offset: 0x000407DA
		public void ClientWriteCallBack(object state)
		{
			NetMessage.buffer[256].spamCount--;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000425F4 File Offset: 0x000407F4
		public void ClientReadCallBack(object state, int streamLength)
		{
			try
			{
				if (!Netplay.Disconnect)
				{
					if (streamLength == 0)
					{
						this.PendingTermination = true;
					}
					else
					{
						if (Main.ignoreErrors)
						{
							try
							{
								NetMessage.ReceiveBytes(this.ReadBuffer, streamLength, 256);
								goto IL_0041;
							}
							catch
							{
								goto IL_0041;
							}
						}
						NetMessage.ReceiveBytes(this.ReadBuffer, streamLength, 256);
					}
				}
				IL_0041:
				this.IsReading = false;
			}
			catch (Exception ex)
			{
				try
				{
					using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
					{
						streamWriter.WriteLine(DateTime.Now);
						streamWriter.WriteLine(ex);
						streamWriter.WriteLine("");
					}
				}
				catch
				{
				}
				Netplay.Disconnect = true;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000426C8 File Offset: 0x000408C8
		public RemoteServer()
		{
		}

		// Token: 0x04000216 RID: 534
		public ISocket Socket = new TcpSocket();

		// Token: 0x04000217 RID: 535
		public bool IsActive;

		// Token: 0x04000218 RID: 536
		public int State;

		// Token: 0x04000219 RID: 537
		public int TimeOutTimer;

		// Token: 0x0400021A RID: 538
		public bool PendingTermination;

		// Token: 0x0400021B RID: 539
		public bool IsReading;

		// Token: 0x0400021C RID: 540
		public byte[] ReadBuffer;

		// Token: 0x0400021D RID: 541
		public string StatusText;

		// Token: 0x0400021E RID: 542
		public int StatusCount;

		// Token: 0x0400021F RID: 543
		public int StatusMax;

		// Token: 0x04000220 RID: 544
		public BitsByte ServerSpecialFlags;
	}
}
