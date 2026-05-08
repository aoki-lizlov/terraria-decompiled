using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Net.Sockets
{
	// Token: 0x02000171 RID: 369
	public class DebugNetworkStream
	{
		// Token: 0x06001DEF RID: 7663 RVA: 0x00502A19 File Offset: 0x00500C19
		public DebugNetworkStream(NetworkStream stream)
		{
			this._stream = stream;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00502A4C File Offset: 0x00500C4C
		public bool DataAvailable
		{
			get
			{
				Queue<DebugNetworkStream.Packet> incomingQueue = this._incomingQueue;
				bool flag2;
				lock (incomingQueue)
				{
					if (this._incomingQueue.Count > 0)
					{
						flag2 = this._incomingQueue.Peek().IsReady();
					}
					else if (this._readMode == DebugNetworkStream.ReadMode.None)
					{
						flag2 = this._stream.DataAvailable;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00502AC0 File Offset: 0x00500CC0
		public void BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this._closed)
			{
				throw new ObjectDisposedException("NetworkStream");
			}
			if (this._writeException != null)
			{
				throw this._writeException;
			}
			Queue<DebugNetworkStream.Packet> outgoingQueue = this._outgoingQueue;
			lock (outgoingQueue)
			{
				if (DebugNetworkStream.Latency == 0U && this._outgoingQueue.Count == 0)
				{
					this._stream.BeginWrite(buffer, offset, count, callback, state);
				}
				else
				{
					this._outgoingQueue.Enqueue(DebugNetworkStream.Packet.CopyOfSlice(buffer, offset, count));
					callback(new DebugNetworkStream.CompletedAsyncResult
					{
						AsyncState = state
					});
				}
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00502B6C File Offset: 0x00500D6C
		public void EndWrite(IAsyncResult result)
		{
			if (result is DebugNetworkStream.CompletedAsyncResult)
			{
				return;
			}
			this._stream.EndWrite(result);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00502B84 File Offset: 0x00500D84
		public void BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this._closed)
			{
				throw new ObjectDisposedException("NetworkStream");
			}
			if (this._readException != null)
			{
				throw this._readException;
			}
			this._beginReadBuf = new ArraySegment<byte>(buffer, offset, count);
			Queue<DebugNetworkStream.Packet> incomingQueue = this._incomingQueue;
			lock (incomingQueue)
			{
				while (this._readMode == DebugNetworkStream.ReadMode.Buffered && this._incomingQueue.Count == 0)
				{
					Monitor.Exit(this._incomingQueue);
					Thread.Sleep(1);
					Monitor.Enter(this._incomingQueue);
					if (this._readException != null)
					{
						throw this._readException;
					}
				}
				if (this._readMode == DebugNetworkStream.ReadMode.None && this._incomingQueue.Count == 0)
				{
					this._readMode = DebugNetworkStream.ReadMode.Direct;
					this._stream.BeginRead(buffer, offset, count, callback, state);
				}
				else
				{
					int num = 0;
					while (count > 0 && this._incomingQueue.Count > 0 && this._incomingQueue.Peek().IsReady())
					{
						DebugNetworkStream.Packet packet = this._incomingQueue.Peek();
						if (packet.Data.Count == 0)
						{
							break;
						}
						int num2 = Math.Min(packet.Data.Count, count);
						Array.Copy(packet.Data.Array, packet.Data.Offset, buffer, offset, num2);
						offset += num2;
						count -= num2;
						num += num2;
						if (num2 == packet.Data.Count)
						{
							this._incomingQueue.Dequeue();
						}
						else
						{
							packet.Data = new ArraySegment<byte>(packet.Data.Array, packet.Data.Offset + num2, packet.Data.Count - num2);
						}
					}
					callback(new DebugNetworkStream.CompletedAsyncResult
					{
						AsyncState = state,
						Read = num
					});
				}
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00502D68 File Offset: 0x00500F68
		public int EndRead(IAsyncResult result)
		{
			if (result is DebugNetworkStream.CompletedAsyncResult)
			{
				return ((DebugNetworkStream.CompletedAsyncResult)result).Read;
			}
			this._readMode = DebugNetworkStream.ReadMode.None;
			return this._stream.EndRead(result);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00502D94 File Offset: 0x00500F94
		public void Close()
		{
			this._closed = true;
			try
			{
				if (this._logWriter != null)
				{
					BinaryWriter logWriter = this._logWriter;
					lock (logWriter)
					{
						this._logWriter.Close();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00502DFC File Offset: 0x00500FFC
		private void Run()
		{
			byte[] array = null;
			while (!this._closed)
			{
				Queue<DebugNetworkStream.Packet> queue = this._outgoingQueue;
				lock (queue)
				{
					while (this._writeException == null && this._outgoingQueue.Count > 0 && this._outgoingQueue.Peek().IsReady())
					{
						this.BeginBufferedWrite(this._outgoingQueue.Dequeue());
					}
				}
				queue = this._incomingQueue;
				lock (queue)
				{
					if (this._readMode == DebugNetworkStream.ReadMode.None && DebugNetworkStream.Latency > 0U)
					{
						this._readMode = DebugNetworkStream.ReadMode.Buffered;
						if (array == null)
						{
							array = new byte[65536];
						}
						this.BeginBufferedRead(array);
					}
				}
				Thread.Sleep(1);
			}
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00502EE0 File Offset: 0x005010E0
		private void BeginBufferedWrite(DebugNetworkStream.Packet packet)
		{
			try
			{
				this._stream.BeginWrite(packet.Data.Array, packet.Data.Offset, packet.Data.Count, new AsyncCallback(this.BufferedWriteCallback), null);
			}
			catch (Exception ex)
			{
				this._writeException = ex;
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00502F44 File Offset: 0x00501144
		private void BufferedWriteCallback(IAsyncResult result)
		{
			try
			{
				this._stream.EndWrite(result);
			}
			catch (Exception ex)
			{
				this._writeException = ex;
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00502F7C File Offset: 0x0050117C
		private void BeginBufferedRead(byte[] buffer)
		{
			try
			{
				this._stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(this.BufferedReadCallback), buffer);
			}
			catch (Exception ex)
			{
				this._readException = ex;
			}
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00502FC4 File Offset: 0x005011C4
		private void BufferedReadCallback(IAsyncResult result)
		{
			int num;
			try
			{
				num = this._stream.EndRead(result);
			}
			catch (Exception ex)
			{
				this._readException = ex;
				return;
			}
			Queue<DebugNetworkStream.Packet> incomingQueue = this._incomingQueue;
			lock (incomingQueue)
			{
				byte[] array = (byte[])result.AsyncState;
				this._incomingQueue.Enqueue(DebugNetworkStream.Packet.CopyOfSlice(array, 0, num));
				if (num != 0)
				{
					if (DebugNetworkStream.Latency == 0U)
					{
						this._readMode = DebugNetworkStream.ReadMode.None;
					}
					else
					{
						this.BeginBufferedRead(array);
					}
				}
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static DebugNetworkStream()
		{
		}

		// Token: 0x04001682 RID: 5762
		public static uint Latency;

		// Token: 0x04001683 RID: 5763
		private readonly NetworkStream _stream;

		// Token: 0x04001684 RID: 5764
		private Queue<DebugNetworkStream.Packet> _outgoingQueue = new Queue<DebugNetworkStream.Packet>();

		// Token: 0x04001685 RID: 5765
		private Queue<DebugNetworkStream.Packet> _incomingQueue = new Queue<DebugNetworkStream.Packet>();

		// Token: 0x04001686 RID: 5766
		private Exception _writeException;

		// Token: 0x04001687 RID: 5767
		private Exception _readException;

		// Token: 0x04001688 RID: 5768
		private DebugNetworkStream.ReadMode _readMode;

		// Token: 0x04001689 RID: 5769
		private bool _closed;

		// Token: 0x0400168A RID: 5770
		private long _startTicks = Stopwatch.GetTimestamp();

		// Token: 0x0400168B RID: 5771
		private BinaryWriter _logWriter;

		// Token: 0x0400168C RID: 5772
		private ArraySegment<byte> _beginReadBuf;

		// Token: 0x0200074C RID: 1868
		private class Packet
		{
			// Token: 0x060040D7 RID: 16599 RVA: 0x0069ED80 File Offset: 0x0069CF80
			public bool IsReady()
			{
				return this.BaseTimestamp + TimeSpan.FromMilliseconds(DebugNetworkStream.Latency) <= DateTime.Now;
			}

			// Token: 0x060040D8 RID: 16600 RVA: 0x0069EDA4 File Offset: 0x0069CFA4
			public static DebugNetworkStream.Packet CopyOfSlice(byte[] buffer, int offset, int count)
			{
				byte[] array = new byte[count];
				Array.Copy(buffer, offset, array, 0, count);
				return new DebugNetworkStream.Packet
				{
					BaseTimestamp = DateTime.Now,
					Data = new ArraySegment<byte>(array)
				};
			}

			// Token: 0x060040D9 RID: 16601 RVA: 0x0000357B File Offset: 0x0000177B
			public Packet()
			{
			}

			// Token: 0x040069D1 RID: 27089
			public DateTime BaseTimestamp;

			// Token: 0x040069D2 RID: 27090
			public ArraySegment<byte> Data;
		}

		// Token: 0x0200074D RID: 1869
		private class CompletedAsyncResult : IAsyncResult
		{
			// Token: 0x17000522 RID: 1314
			// (get) Token: 0x060040DA RID: 16602 RVA: 0x0069EDDE File Offset: 0x0069CFDE
			// (set) Token: 0x060040DB RID: 16603 RVA: 0x0069EDE6 File Offset: 0x0069CFE6
			public object AsyncState
			{
				[CompilerGenerated]
				get
				{
					return this.<AsyncState>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<AsyncState>k__BackingField = value;
				}
			}

			// Token: 0x17000523 RID: 1315
			// (get) Token: 0x060040DC RID: 16604 RVA: 0x0069EDEF File Offset: 0x0069CFEF
			// (set) Token: 0x060040DD RID: 16605 RVA: 0x0069EDF7 File Offset: 0x0069CFF7
			public int Read
			{
				[CompilerGenerated]
				get
				{
					return this.<Read>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Read>k__BackingField = value;
				}
			}

			// Token: 0x17000524 RID: 1316
			// (get) Token: 0x060040DE RID: 16606 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000525 RID: 1317
			// (get) Token: 0x060040DF RID: 16607 RVA: 0x000379E9 File Offset: 0x00035BE9
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x060040E0 RID: 16608 RVA: 0x00356859 File Offset: 0x00354A59
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060040E1 RID: 16609 RVA: 0x0000357B File Offset: 0x0000177B
			public CompletedAsyncResult()
			{
			}

			// Token: 0x040069D3 RID: 27091
			[CompilerGenerated]
			private object <AsyncState>k__BackingField;

			// Token: 0x040069D4 RID: 27092
			[CompilerGenerated]
			private int <Read>k__BackingField;
		}

		// Token: 0x0200074E RID: 1870
		private enum ReadMode
		{
			// Token: 0x040069D6 RID: 27094
			None,
			// Token: 0x040069D7 RID: 27095
			Direct,
			// Token: 0x040069D8 RID: 27096
			Buffered
		}
	}
}
