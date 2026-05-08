using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000016 RID: 22
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002596 File Offset: 0x00000796
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000025B1 File Offset: 0x000007B1
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000025C1 File Offset: 0x000007C1
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000025C8 File Offset: 0x000007C8
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025D5 File Offset: 0x000007D5
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000025E2 File Offset: 0x000007E2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000025E5 File Offset: 0x000007E5
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000025F2 File Offset: 0x000007F2
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000025FF File Offset: 0x000007FF
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002613 File Offset: 0x00000813
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002628 File Offset: 0x00000828
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002645 File Offset: 0x00000845
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000264C File Offset: 0x0000084C
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002654 File Offset: 0x00000854
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0400002E RID: 46
		private long _originalPosition;

		// Token: 0x0400002F RID: 47
		private Stream _innerStream;
	}
}
