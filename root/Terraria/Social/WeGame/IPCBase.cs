using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000132 RID: 306
	public abstract class IPCBase
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x004FD8F7 File Offset: 0x004FBAF7
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x004FD8EE File Offset: 0x004FBAEE
		public int BufferSize
		{
			[CompilerGenerated]
			get
			{
				return this.<BufferSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BufferSize>k__BackingField = value;
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001C2F RID: 7215 RVA: 0x004FD8FF File Offset: 0x004FBAFF
		// (remove) Token: 0x06001C30 RID: 7216 RVA: 0x004FD918 File Offset: 0x004FBB18
		public virtual event Action<byte[]> OnDataArrive
		{
			add
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Combine(this._onDataArrive, value);
			}
			remove
			{
				this._onDataArrive = (Action<byte[]>)Delegate.Remove(this._onDataArrive, value);
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x004FD931 File Offset: 0x004FBB31
		public IPCBase()
		{
			this.BufferSize = 256;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x004FD970 File Offset: 0x004FBB70
		protected void AddPackToList(List<byte> pack)
		{
			object listLock = this._listLock;
			lock (listLock)
			{
				this._producer.Add(pack);
				this._haveDataToReadFlag = true;
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x004FD9C0 File Offset: 0x004FBBC0
		protected List<List<byte>> GetPackList()
		{
			List<List<byte>> list = null;
			object listLock = this._listLock;
			lock (listLock)
			{
				List<List<byte>> producer = this._producer;
				this._producer = this._consumer;
				this._consumer = producer;
				this._producer.Clear();
				list = this._consumer;
				this._haveDataToReadFlag = false;
			}
			return list;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x004FDA34 File Offset: 0x004FBC34
		protected bool HaveDataToRead()
		{
			return this._haveDataToReadFlag;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x004FDA3E File Offset: 0x004FBC3E
		public virtual void Reset()
		{
			this._cancelTokenSrc.Cancel();
			this._pipeStream.Dispose();
			this._pipeStream = null;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x004FDA60 File Offset: 0x004FBC60
		public virtual void ProcessDataArriveEvent()
		{
			if (this.HaveDataToRead())
			{
				List<List<byte>> packList = this.GetPackList();
				if (packList != null && this._onDataArrive != null)
				{
					foreach (List<byte> list in packList)
					{
						this._onDataArrive(list.ToArray());
					}
				}
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x004FDAD4 File Offset: 0x004FBCD4
		protected virtual bool BeginReadData()
		{
			bool flag = false;
			IPCContent ipccontent = new IPCContent
			{
				data = new byte[this.BufferSize],
				CancelToken = this._cancelTokenSrc.Token
			};
			WeGameHelper.WriteDebugString("BeginReadData", new object[0]);
			try
			{
				if (this._pipeStream != null)
				{
					this._pipeStream.BeginRead(ipccontent.data, 0, this.BufferSize, new AsyncCallback(this.ReadCallback), ipccontent);
					flag = true;
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("BeginReadData Exception, {0}", new object[] { ex.Message });
			}
			return flag;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x004FDB84 File Offset: 0x004FBD84
		public virtual void ReadCallback(IAsyncResult result)
		{
			WeGameHelper.WriteDebugString("ReadCallback: " + Thread.CurrentThread.ManagedThreadId.ToString(), new object[0]);
			IPCContent ipccontent = (IPCContent)result.AsyncState;
			try
			{
				int num = this._pipeStream.EndRead(result);
				if (!ipccontent.CancelToken.IsCancellationRequested)
				{
					if (num > 0)
					{
						this._totalData.AddRange(ipccontent.data.Take(num));
						if (this._pipeStream.IsMessageComplete)
						{
							this.AddPackToList(this._totalData);
							this._totalData = new List<byte>();
						}
					}
				}
				else
				{
					WeGameHelper.WriteDebugString("IPCBase.ReadCallback.cancel", new object[0]);
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ReadCallback Exception, {0}", new object[] { ex.Message });
			}
			catch (InvalidOperationException ex2)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("ReadCallback Exception, {0}", new object[] { ex2.Message });
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x004FDCA0 File Offset: 0x004FBEA0
		public virtual bool Send(string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return this.Send(bytes);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x004FDCC0 File Offset: 0x004FBEC0
		public virtual bool Send(byte[] data)
		{
			bool flag = false;
			if (this._pipeStream != null && this._pipeStream.IsConnected)
			{
				try
				{
					this._pipeStream.BeginWrite(data, 0, data.Length, new AsyncCallback(this.SendCallback), null);
					flag = true;
				}
				catch (IOException ex)
				{
					this._pipeBrokenFlag = true;
					WeGameHelper.WriteDebugString("Send Exception, {0}", new object[] { ex.Message });
				}
			}
			return flag;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x004FDD40 File Offset: 0x004FBF40
		protected virtual void SendCallback(IAsyncResult result)
		{
			try
			{
				if (this._pipeStream != null)
				{
					this._pipeStream.EndWrite(result);
				}
			}
			catch (IOException ex)
			{
				this._pipeBrokenFlag = true;
				WeGameHelper.WriteDebugString("SendCallback Exception, {0}", new object[] { ex.Message });
			}
		}

		// Token: 0x040015B7 RID: 5559
		private List<List<byte>> _producer = new List<List<byte>>();

		// Token: 0x040015B8 RID: 5560
		private List<List<byte>> _consumer = new List<List<byte>>();

		// Token: 0x040015B9 RID: 5561
		private List<byte> _totalData = new List<byte>();

		// Token: 0x040015BA RID: 5562
		private object _listLock = new object();

		// Token: 0x040015BB RID: 5563
		private volatile bool _haveDataToReadFlag;

		// Token: 0x040015BC RID: 5564
		protected volatile bool _pipeBrokenFlag;

		// Token: 0x040015BD RID: 5565
		protected PipeStream _pipeStream;

		// Token: 0x040015BE RID: 5566
		protected CancellationTokenSource _cancelTokenSrc;

		// Token: 0x040015BF RID: 5567
		protected Action<byte[]> _onDataArrive;

		// Token: 0x040015C0 RID: 5568
		[CompilerGenerated]
		private int <BufferSize>k__BackingField;
	}
}
