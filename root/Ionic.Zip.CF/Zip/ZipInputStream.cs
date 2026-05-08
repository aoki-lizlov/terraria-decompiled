using System;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zip
{
	// Token: 0x0200002B RID: 43
	public class ZipInputStream : Stream
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public ZipInputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C800 File Offset: 0x0000AA00
		public ZipInputStream(string fileName)
		{
			Stream stream = File.Open(fileName, 3, 1, 1);
			this._Init(stream, false, fileName);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C826 File Offset: 0x0000AA26
		public ZipInputStream(Stream stream, bool leaveOpen)
		{
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C838 File Offset: 0x0000AA38
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._inputStream = stream;
			if (!this._inputStream.CanRead)
			{
				throw new ZipException("The stream must be readable.");
			}
			this._container = new ZipContainer(this);
			this._provisionalAlternateEncoding = Encoding.GetEncoding("IBM437");
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this._findRequired = true;
			this._name = name ?? "(stream)";
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C89E File Offset: 0x0000AA9E
		public override string ToString()
		{
			return string.Format("ZipInputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000C8BB File Offset: 0x0000AABB
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000C8C3 File Offset: 0x0000AAC3
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this._provisionalAlternateEncoding;
			}
			set
			{
				this._provisionalAlternateEncoding = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000C8CC File Offset: 0x0000AACC
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public int CodecBufferSize
		{
			get
			{
				return this.<CodecBufferSize>k__BackingField;
			}
			set
			{
				this.<CodecBufferSize>k__BackingField = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000C8DD File Offset: 0x0000AADD
		public string Password
		{
			set
			{
				if (this._closed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._Password = value;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C900 File Offset: 0x0000AB00
		private void SetupStream()
		{
			this._crcStream = this._currentEntry.InternalOpenReader(this._Password);
			this._LeftToRead = this._crcStream.Length;
			this._needSetup = false;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C931 File Offset: 0x0000AB31
		internal Stream ReadStream
		{
			get
			{
				return this._inputStream;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._closed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (this._needSetup)
			{
				this.SetupStream();
			}
			if (this._LeftToRead == 0L)
			{
				return 0;
			}
			int num = ((this._LeftToRead > (long)count) ? count : ((int)this._LeftToRead));
			int num2 = this._crcStream.Read(buffer, offset, num);
			this._LeftToRead -= (long)num2;
			if (this._LeftToRead == 0L)
			{
				int crc = this._crcStream.Crc;
				this._currentEntry.VerifyCrcAfterExtract(crc);
				this._inputStream.Seek(this._endOfEntry, 0);
				SharedUtilities.Workaround_Ladybug318918(this._inputStream);
			}
			return num2;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public ZipEntry GetNextEntry()
		{
			if (this._findRequired)
			{
				long num = SharedUtilities.FindSignature(this._inputStream, 67324752);
				if (num == -1L)
				{
					return null;
				}
				this._inputStream.Seek(-4L, 1);
				SharedUtilities.Workaround_Ladybug318918(this._inputStream);
			}
			else if (this._firstEntry)
			{
				this._inputStream.Seek(this._endOfEntry, 0);
				SharedUtilities.Workaround_Ladybug318918(this._inputStream);
			}
			this._currentEntry = ZipEntry.ReadEntry(this._container, !this._firstEntry);
			this._endOfEntry = this._inputStream.Position;
			this._firstEntry = true;
			this._needSetup = true;
			this._findRequired = false;
			return this._currentEntry;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CAAA File Offset: 0x0000ACAA
		protected override void Dispose(bool disposing)
		{
			if (this._closed)
			{
				return;
			}
			if (disposing)
			{
				if (this._exceptionPending)
				{
					return;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					this._inputStream.Close();
				}
			}
			this._closed = true;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000CADB File Offset: 0x0000ACDB
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000CADE File Offset: 0x0000ACDE
		public override bool CanSeek
		{
			get
			{
				return this._inputStream.CanSeek;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000CAEB File Offset: 0x0000ACEB
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000CAEE File Offset: 0x0000ACEE
		public override long Length
		{
			get
			{
				return this._inputStream.Length;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000CAFB File Offset: 0x0000ACFB
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public override long Position
		{
			get
			{
				return this._inputStream.Position;
			}
			set
			{
				this.Seek(value, 0);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000CB13 File Offset: 0x0000AD13
		public override void Flush()
		{
			throw new NotSupportedException("Flush");
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000CB1F File Offset: 0x0000AD1F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Write");
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this._findRequired = true;
			long num = this._inputStream.Seek(offset, origin);
			SharedUtilities.Workaround_Ladybug318918(this._inputStream);
			return num;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000100 RID: 256
		private Stream _inputStream;

		// Token: 0x04000101 RID: 257
		private Encoding _provisionalAlternateEncoding;

		// Token: 0x04000102 RID: 258
		private ZipEntry _currentEntry;

		// Token: 0x04000103 RID: 259
		private bool _firstEntry;

		// Token: 0x04000104 RID: 260
		private bool _needSetup;

		// Token: 0x04000105 RID: 261
		private ZipContainer _container;

		// Token: 0x04000106 RID: 262
		private CrcCalculatorStream _crcStream;

		// Token: 0x04000107 RID: 263
		private long _LeftToRead;

		// Token: 0x04000108 RID: 264
		internal string _Password;

		// Token: 0x04000109 RID: 265
		private long _endOfEntry;

		// Token: 0x0400010A RID: 266
		private string _name;

		// Token: 0x0400010B RID: 267
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x0400010C RID: 268
		private bool _closed;

		// Token: 0x0400010D RID: 269
		private bool _findRequired;

		// Token: 0x0400010E RID: 270
		private bool _exceptionPending;

		// Token: 0x0400010F RID: 271
		private int <CodecBufferSize>k__BackingField;
	}
}
