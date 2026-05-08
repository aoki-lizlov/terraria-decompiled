using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000018 RID: 24
	public class CountingStream : Stream
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002E80 File Offset: 0x00001080
		public CountingStream(Stream stream)
		{
			this._s = stream;
			try
			{
				this._initialOffset = this._s.Position;
			}
			catch
			{
				this._initialOffset = 0L;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002EC8 File Offset: 0x000010C8
		public Stream WrappedStream
		{
			get
			{
				return this._s;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002ED0 File Offset: 0x000010D0
		public long BytesWritten
		{
			get
			{
				return this._bytesWritten;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002ED8 File Offset: 0x000010D8
		public long BytesRead
		{
			get
			{
				return this._bytesRead;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void Adjust(long delta)
		{
			this._bytesWritten -= delta;
			if (this._bytesWritten < 0L)
			{
				throw new InvalidOperationException();
			}
			if (this._s is CountingStream)
			{
				((CountingStream)this._s).Adjust(delta);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F20 File Offset: 0x00001120
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._s.Read(buffer, offset, count);
			this._bytesRead += (long)num;
			return num;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F4C File Offset: 0x0000114C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			this._s.Write(buffer, offset, count);
			this._bytesWritten += (long)count;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002F6F File Offset: 0x0000116F
		public override bool CanRead
		{
			get
			{
				return this._s.CanRead;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002F7C File Offset: 0x0000117C
		public override bool CanSeek
		{
			get
			{
				return this._s.CanSeek;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002F89 File Offset: 0x00001189
		public override bool CanWrite
		{
			get
			{
				return this._s.CanWrite;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F96 File Offset: 0x00001196
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002FA3 File Offset: 0x000011A3
		public override long Length
		{
			get
			{
				return this._s.Length;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002FB0 File Offset: 0x000011B0
		public long ComputedPosition
		{
			get
			{
				return this._initialOffset + this._bytesWritten;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002FBF File Offset: 0x000011BF
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002FCC File Offset: 0x000011CC
		public override long Position
		{
			get
			{
				return this._s.Position;
			}
			set
			{
				this._s.Seek(value, 0);
				SharedUtilities.Workaround_Ladybug318918(this._s);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002FE7 File Offset: 0x000011E7
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._s.Seek(offset, origin);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002FF6 File Offset: 0x000011F6
		public override void SetLength(long value)
		{
			this._s.SetLength(value);
		}

		// Token: 0x04000033 RID: 51
		private Stream _s;

		// Token: 0x04000034 RID: 52
		private long _bytesWritten;

		// Token: 0x04000035 RID: 53
		private long _bytesRead;

		// Token: 0x04000036 RID: 54
		private long _initialOffset;
	}
}
