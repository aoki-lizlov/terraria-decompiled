using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x0200014E RID: 334
	public class SteamP2PWriter
	{
		// Token: 0x06001D1D RID: 7453 RVA: 0x005010E4 File Offset: 0x004FF2E4
		public SteamP2PWriter(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00501120 File Offset: 0x004FF320
		public void QueueSend(CSteamID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<SteamP2PWriter.WriteInformation> queue;
				if (this._pendingSendData.ContainsKey(user))
				{
					queue = this._pendingSendData[user];
				}
				else
				{
					queue = (this._pendingSendData[user] = new Queue<SteamP2PWriter.WriteInformation>());
				}
				int i = length;
				int num = 0;
				while (i > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation;
					if (queue.Count == 0 || 1024 - queue.Peek().Size == 0)
					{
						byte[] array;
						if (!this._bufferPool.TryTake(out array))
						{
							array = new byte[1024];
						}
						writeInformation = new SteamP2PWriter.WriteInformation(array);
						queue.Enqueue(writeInformation);
					}
					else
					{
						writeInformation = queue.Peek();
					}
					int num2 = Math.Min(i, 1024 - writeInformation.Size);
					Array.Copy(data, num, writeInformation.Data, writeInformation.Size, num2);
					writeInformation.Size += num2;
					i -= num2;
					num += num2;
				}
			}
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00501238 File Offset: 0x004FF438
		public void SendAll()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Utils.Swap<Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>>(ref this._pendingSendData, ref this._pendingSendDataSwap);
			}
			foreach (KeyValuePair<CSteamID, Queue<SteamP2PWriter.WriteInformation>> keyValuePair in this._pendingSendDataSwap)
			{
				Queue<SteamP2PWriter.WriteInformation> value = keyValuePair.Value;
				while (value.Count > 0)
				{
					SteamP2PWriter.WriteInformation writeInformation = value.Dequeue();
					SteamNetworking.SendP2PPacket(keyValuePair.Key, writeInformation.Data, (uint)writeInformation.Size, 2, this._channel);
					this._bufferPool.Add(writeInformation.Data);
				}
			}
		}

		// Token: 0x04001626 RID: 5670
		private const int BUFFER_SIZE = 1024;

		// Token: 0x04001627 RID: 5671
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x04001628 RID: 5672
		private Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<CSteamID, Queue<SteamP2PWriter.WriteInformation>>();

		// Token: 0x04001629 RID: 5673
		private ConcurrentBag<byte[]> _bufferPool = new ConcurrentBag<byte[]>();

		// Token: 0x0400162A RID: 5674
		private int _channel;

		// Token: 0x0400162B RID: 5675
		private object _lock = new object();

		// Token: 0x02000742 RID: 1858
		public class WriteInformation
		{
			// Token: 0x060040C0 RID: 16576 RVA: 0x0069ED2C File Offset: 0x0069CF2C
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x040069C0 RID: 27072
			public byte[] Data;

			// Token: 0x040069C1 RID: 27073
			public int Size;
		}
	}
}
