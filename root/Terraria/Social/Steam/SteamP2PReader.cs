using System;
using System.Collections.Generic;
using Steamworks;

namespace Terraria.Social.Steam
{
	// Token: 0x0200014D RID: 333
	public class SteamP2PReader
	{
		// Token: 0x06001D16 RID: 7446 RVA: 0x00500D32 File Offset: 0x004FEF32
		public SteamP2PReader(int channel)
		{
			this._channel = channel;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00500D70 File Offset: 0x004FEF70
		public void ClearUser(CSteamID id)
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00500DB8 File Offset: 0x004FEFB8
		public bool IsDataAvailable(CSteamID id)
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			bool flag2;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(id))
				{
					flag2 = false;
				}
				else
				{
					Queue<SteamP2PReader.ReadResult> queue = this._pendingReadBuffers[id];
					if (queue.Count == 0 || queue.Peek().Size == 0U)
					{
						flag2 = false;
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x00500E30 File Offset: 0x004FF030
		public void SetReadEvent(SteamP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x00500E3C File Offset: 0x004FF03C
		private bool IsPacketAvailable(out uint size)
		{
			object steamLock = this.SteamLock;
			bool flag2;
			lock (steamLock)
			{
				flag2 = SteamNetworking.IsP2PPacketAvailable(ref size, this._channel);
			}
			return flag2;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00500E84 File Offset: 0x004FF084
		public void ReadTick()
		{
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				while (this._deletionQueue.Count > 0)
				{
					this._pendingReadBuffers.Remove(this._deletionQueue.Dequeue());
				}
				uint num;
				while (this.IsPacketAvailable(out num))
				{
					byte[] array;
					if (this._bufferPool.Count == 0)
					{
						array = new byte[Math.Max(num, 4096U)];
					}
					else
					{
						array = this._bufferPool.Dequeue();
					}
					object steamLock = this.SteamLock;
					uint num2;
					CSteamID csteamID;
					bool flag3;
					lock (steamLock)
					{
						flag3 = SteamNetworking.ReadP2PPacket(array, (uint)array.Length, ref num2, ref csteamID, this._channel);
					}
					if (flag3)
					{
						if (this._readEvent == null || this._readEvent(array, (int)num2, csteamID))
						{
							if (!this._pendingReadBuffers.ContainsKey(csteamID))
							{
								this._pendingReadBuffers[csteamID] = new Queue<SteamP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[csteamID].Enqueue(new SteamP2PReader.ReadResult(array, num2));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00500FE8 File Offset: 0x004FF1E8
		public int Receive(CSteamID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					return 0;
				}
				Queue<SteamP2PReader.ReadResult> queue = this._pendingReadBuffers[user];
				while (queue.Count > 0)
				{
					SteamP2PReader.ReadResult readResult = queue.Peek();
					uint num2 = Math.Min((uint)(bufferSize - (int)num), readResult.Size - readResult.Offset);
					if (num2 == 0U)
					{
						return (int)num;
					}
					Array.Copy(readResult.Data, (long)((ulong)readResult.Offset), buffer, (long)bufferOffset + (long)((ulong)num), (long)((ulong)num2));
					if (num2 == readResult.Size - readResult.Offset)
					{
						this._bufferPool.Enqueue(queue.Dequeue().Data);
					}
					else
					{
						readResult.Offset += num2;
					}
					num += num2;
				}
			}
			return (int)num;
		}

		// Token: 0x0400161F RID: 5663
		public object SteamLock = new object();

		// Token: 0x04001620 RID: 5664
		private const int BUFFER_SIZE = 4096;

		// Token: 0x04001621 RID: 5665
		private Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<CSteamID, Queue<SteamP2PReader.ReadResult>>();

		// Token: 0x04001622 RID: 5666
		private Queue<CSteamID> _deletionQueue = new Queue<CSteamID>();

		// Token: 0x04001623 RID: 5667
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x04001624 RID: 5668
		private int _channel;

		// Token: 0x04001625 RID: 5669
		private SteamP2PReader.OnReadEvent _readEvent;

		// Token: 0x02000740 RID: 1856
		public class ReadResult
		{
			// Token: 0x060040BB RID: 16571 RVA: 0x0069ED0F File Offset: 0x0069CF0F
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x040069BD RID: 27069
			public byte[] Data;

			// Token: 0x040069BE RID: 27070
			public uint Size;

			// Token: 0x040069BF RID: 27071
			public uint Offset;
		}

		// Token: 0x02000741 RID: 1857
		// (Invoke) Token: 0x060040BD RID: 16573
		public delegate bool OnReadEvent(byte[] data, int size, CSteamID user);
	}
}
