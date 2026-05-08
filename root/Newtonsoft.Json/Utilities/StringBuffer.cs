using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200006A RID: 106
	internal struct StringBuffer
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00015EC8 File Offset: 0x000140C8
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00015ED0 File Offset: 0x000140D0
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00015ED9 File Offset: 0x000140D9
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00015EE4 File Offset: 0x000140E4
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00015EF3 File Offset: 0x000140F3
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015F04 File Offset: 0x00014104
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015F44 File Offset: 0x00014144
		public void Append(IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00015F91 File Offset: 0x00014191
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015FB8 File Offset: 0x000141B8
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00016003 File Offset: 0x00014203
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00016012 File Offset: 0x00014212
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00016021 File Offset: 0x00014221
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x0400025F RID: 607
		private char[] _buffer;

		// Token: 0x04000260 RID: 608
		private int _position;
	}
}
