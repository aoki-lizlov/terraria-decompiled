using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000139 RID: 313
	public class WeGameP2PReader
	{
		// Token: 0x06001C78 RID: 7288 RVA: 0x004FE632 File Offset: 0x004FC832
		public WeGameP2PReader()
		{
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x004FE668 File Offset: 0x004FC868
		public void ClearUser(RailID id)
		{
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				this._deletionQueue.Enqueue(id);
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x004FE6B0 File Offset: 0x004FC8B0
		public bool IsDataAvailable(RailID id)
		{
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			bool flag2;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(id))
				{
					flag2 = false;
				}
				else
				{
					Queue<WeGameP2PReader.ReadResult> queue = this._pendingReadBuffers[id];
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

		// Token: 0x06001C7B RID: 7291 RVA: 0x004FE728 File Offset: 0x004FC928
		public void SetReadEvent(WeGameP2PReader.OnReadEvent method)
		{
			this._readEvent = method;
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x004FE734 File Offset: 0x004FC934
		private bool IsPacketAvailable(out uint size)
		{
			object railLock = this.RailLock;
			bool flag2;
			lock (railLock)
			{
				flag2 = rail_api.RailFactory().RailNetworkHelper().IsDataReady(new RailID
				{
					id_ = this.GetLocalPeer().id_
				}, ref size);
			}
			return flag2;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x004FE798 File Offset: 0x004FC998
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x004FE7A0 File Offset: 0x004FC9A0
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x004FE7CC File Offset: 0x004FC9CC
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x004FE7EC File Offset: 0x004FC9EC
		public void ReadTick()
		{
			if (!this.IsValid())
			{
				return;
			}
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
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
					RailID railID = new RailID();
					object railLock = this.RailLock;
					bool flag3;
					lock (railLock)
					{
						flag3 = rail_api.RailFactory().RailNetworkHelper().ReadData(this.GetLocalPeer(), railID, array, num) == 0;
					}
					if (flag3)
					{
						if (this._readEvent == null || this._readEvent(array, (int)num, railID))
						{
							if (!this._pendingReadBuffers.ContainsKey(railID))
							{
								this._pendingReadBuffers[railID] = new Queue<WeGameP2PReader.ReadResult>();
							}
							this._pendingReadBuffers[railID].Enqueue(new WeGameP2PReader.ReadResult(array, num));
						}
						else
						{
							this._bufferPool.Enqueue(array);
						}
					}
				}
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x004FE964 File Offset: 0x004FCB64
		public int Receive(RailID user, byte[] buffer, int bufferOffset, int bufferSize)
		{
			uint num = 0U;
			Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> pendingReadBuffers = this._pendingReadBuffers;
			lock (pendingReadBuffers)
			{
				if (!this._pendingReadBuffers.ContainsKey(user))
				{
					return 0;
				}
				Queue<WeGameP2PReader.ReadResult> queue = this._pendingReadBuffers[user];
				while (queue.Count > 0)
				{
					WeGameP2PReader.ReadResult readResult = queue.Peek();
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

		// Token: 0x040015D0 RID: 5584
		public object RailLock = new object();

		// Token: 0x040015D1 RID: 5585
		private const int BUFFER_SIZE = 4096;

		// Token: 0x040015D2 RID: 5586
		private Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>> _pendingReadBuffers = new Dictionary<RailID, Queue<WeGameP2PReader.ReadResult>>();

		// Token: 0x040015D3 RID: 5587
		private Queue<RailID> _deletionQueue = new Queue<RailID>();

		// Token: 0x040015D4 RID: 5588
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x040015D5 RID: 5589
		private WeGameP2PReader.OnReadEvent _readEvent;

		// Token: 0x040015D6 RID: 5590
		private RailID _local_id;

		// Token: 0x02000735 RID: 1845
		public class ReadResult
		{
			// Token: 0x060040A4 RID: 16548 RVA: 0x0069EBB0 File Offset: 0x0069CDB0
			public ReadResult(byte[] data, uint size)
			{
				this.Data = data;
				this.Size = size;
				this.Offset = 0U;
			}

			// Token: 0x040069AA RID: 27050
			public byte[] Data;

			// Token: 0x040069AB RID: 27051
			public uint Size;

			// Token: 0x040069AC RID: 27052
			public uint Offset;
		}

		// Token: 0x02000736 RID: 1846
		// (Invoke) Token: 0x060040A6 RID: 16550
		public delegate bool OnReadEvent(byte[] data, int size, RailID user);
	}
}
