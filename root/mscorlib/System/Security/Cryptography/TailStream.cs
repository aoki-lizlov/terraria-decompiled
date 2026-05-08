using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x0200046B RID: 1131
	internal sealed class TailStream : Stream
	{
		// Token: 0x06002ED5 RID: 11989 RVA: 0x000A8547 File Offset: 0x000A6747
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000A484D File Offset: 0x000A2A4D
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000A8564 File Offset: 0x000A6764
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000A85B4 File Offset: 0x000A67B4
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0000408A File Offset: 0x0000228A
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002EDB RID: 11995 RVA: 0x000A85C6 File Offset: 0x000A67C6
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000A85D1 File Offset: 0x000A67D1
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002EDD RID: 11997 RVA: 0x000A85D1 File Offset: 0x000A67D1
		// (set) Token: 0x06002EDE RID: 11998 RVA: 0x000A85D1 File Offset: 0x000A67D1
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00004088 File Offset: 0x00002288
		public override void Flush()
		{
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000A85D1 File Offset: 0x000A67D1
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000A85D1 File Offset: 0x000A67D1
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000A85E2 File Offset: 0x000A67E2
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000A85F4 File Offset: 0x000A67F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					global::System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				global::System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x0400204F RID: 8271
		private byte[] _Buffer;

		// Token: 0x04002050 RID: 8272
		private int _BufferSize;

		// Token: 0x04002051 RID: 8273
		private int _BufferIndex;

		// Token: 0x04002052 RID: 8274
		private bool _BufferFull;
	}
}
