using System;
using System.Collections.Generic;
using rail;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200013A RID: 314
	public class WeGameP2PWriter
	{
		// Token: 0x06001C82 RID: 7298 RVA: 0x004FEA60 File Offset: 0x004FCC60
		public WeGameP2PWriter()
		{
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x004FEA94 File Offset: 0x004FCC94
		public void QueueSend(RailID user, byte[] data, int length)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Queue<WeGameP2PWriter.WriteInformation> queue;
				if (this._pendingSendData.ContainsKey(user))
				{
					queue = this._pendingSendData[user];
				}
				else
				{
					queue = (this._pendingSendData[user] = new Queue<WeGameP2PWriter.WriteInformation>());
				}
				int i = length;
				int num = 0;
				while (i > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation;
					if (queue.Count == 0 || 1024 - queue.Peek().Size == 0)
					{
						if (this._bufferPool.Count > 0)
						{
							writeInformation = new WeGameP2PWriter.WriteInformation(this._bufferPool.Dequeue());
						}
						else
						{
							writeInformation = new WeGameP2PWriter.WriteInformation();
						}
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

		// Token: 0x06001C84 RID: 7300 RVA: 0x004FEBB0 File Offset: 0x004FCDB0
		public void ClearUser(RailID user)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._pendingSendData.ContainsKey(user))
				{
					Queue<WeGameP2PWriter.WriteInformation> queue = this._pendingSendData[user];
					while (queue.Count > 0)
					{
						this._bufferPool.Enqueue(queue.Dequeue().Data);
					}
				}
				if (this._pendingSendDataSwap.ContainsKey(user))
				{
					Queue<WeGameP2PWriter.WriteInformation> queue2 = this._pendingSendDataSwap[user];
					while (queue2.Count > 0)
					{
						this._bufferPool.Enqueue(queue2.Dequeue().Data);
					}
				}
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x004FEC64 File Offset: 0x004FCE64
		public void SetLocalPeer(RailID rail_id)
		{
			if (this._local_id == null)
			{
				this._local_id = new RailID();
			}
			this._local_id.id_ = rail_id.id_;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x004FEC90 File Offset: 0x004FCE90
		private RailID GetLocalPeer()
		{
			return this._local_id;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x004FEC98 File Offset: 0x004FCE98
		private bool IsValid()
		{
			return this._local_id != null && this._local_id.IsValid();
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x004FECB8 File Offset: 0x004FCEB8
		public void SendAll()
		{
			if (!this.IsValid())
			{
				return;
			}
			object @lock = this._lock;
			lock (@lock)
			{
				Utils.Swap<Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>>(ref this._pendingSendData, ref this._pendingSendDataSwap);
			}
			foreach (KeyValuePair<RailID, Queue<WeGameP2PWriter.WriteInformation>> keyValuePair in this._pendingSendDataSwap)
			{
				Queue<WeGameP2PWriter.WriteInformation> value = keyValuePair.Value;
				while (value.Count > 0)
				{
					WeGameP2PWriter.WriteInformation writeInformation = value.Dequeue();
					bool flag2 = rail_api.RailFactory().RailNetworkHelper().SendData(this.GetLocalPeer(), keyValuePair.Key, writeInformation.Data, (uint)writeInformation.Size) == 0;
					this._bufferPool.Enqueue(writeInformation.Data);
				}
			}
		}

		// Token: 0x040015D7 RID: 5591
		private const int BUFFER_SIZE = 1024;

		// Token: 0x040015D8 RID: 5592
		private RailID _local_id;

		// Token: 0x040015D9 RID: 5593
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendData = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x040015DA RID: 5594
		private Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>> _pendingSendDataSwap = new Dictionary<RailID, Queue<WeGameP2PWriter.WriteInformation>>();

		// Token: 0x040015DB RID: 5595
		private Queue<byte[]> _bufferPool = new Queue<byte[]>();

		// Token: 0x040015DC RID: 5596
		private object _lock = new object();

		// Token: 0x02000737 RID: 1847
		public class WriteInformation
		{
			// Token: 0x060040A9 RID: 16553 RVA: 0x0069EBCD File Offset: 0x0069CDCD
			public WriteInformation()
			{
				this.Data = new byte[1024];
				this.Size = 0;
			}

			// Token: 0x060040AA RID: 16554 RVA: 0x0069EBEC File Offset: 0x0069CDEC
			public WriteInformation(byte[] data)
			{
				this.Data = data;
				this.Size = 0;
			}

			// Token: 0x040069AD RID: 27053
			public byte[] Data;

			// Token: 0x040069AE RID: 27054
			public int Size;
		}
	}
}
