using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200002E RID: 46
	internal class ZipSegmentedStream : Stream
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000D50F File Offset: 0x0000B70F
		private ZipSegmentedStream()
		{
			this._exceptionPending = false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D520 File Offset: 0x0000B720
		public static ZipSegmentedStream ForReading(string name, uint initialDiskNumber, uint maxDiskNumber)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.ReadOnly,
				CurrentSegment = initialDiskNumber,
				_maxDiskNumber = maxDiskNumber,
				_baseName = name
			};
			zipSegmentedStream._SetReadStream();
			return zipSegmentedStream;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D558 File Offset: 0x0000B758
		public static ZipSegmentedStream ForWriting(string name, int maxSegmentSize)
		{
			ZipSegmentedStream zipSegmentedStream = new ZipSegmentedStream
			{
				rwMode = ZipSegmentedStream.RwMode.Write,
				CurrentSegment = 0U,
				_baseName = name,
				_maxSegmentSize = maxSegmentSize,
				_baseDir = Path.GetDirectoryName(name)
			};
			if (zipSegmentedStream._baseDir == "")
			{
				zipSegmentedStream._baseDir = ".";
			}
			zipSegmentedStream._SetWriteStream(0U);
			return zipSegmentedStream;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		public static Stream ForUpdate(string name, uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			string text = string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(name), Path.GetFileNameWithoutExtension(name)), diskNumber + 1U);
			return File.Open(text, 3, 3, 0);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000D606 File Offset: 0x0000B806
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000D60E File Offset: 0x0000B80E
		public bool ContiguousWrite
		{
			get
			{
				return this.<ContiguousWrite>k__BackingField;
			}
			set
			{
				this.<ContiguousWrite>k__BackingField = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000D617 File Offset: 0x0000B817
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000D61F File Offset: 0x0000B81F
		public uint CurrentSegment
		{
			get
			{
				return this._currentDiskNumber;
			}
			private set
			{
				this._currentDiskNumber = value;
				this._currentName = null;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000D62F File Offset: 0x0000B82F
		public string CurrentName
		{
			get
			{
				if (this._currentName == null)
				{
					this._currentName = this._NameForSegment(this.CurrentSegment);
				}
				return this._currentName;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000D651 File Offset: 0x0000B851
		public string CurrentTempName
		{
			get
			{
				return this._currentTempName;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D65C File Offset: 0x0000B85C
		private string _NameForSegment(uint diskNumber)
		{
			if (diskNumber >= 99U)
			{
				this._exceptionPending = true;
				throw new OverflowException("The number of zip segments would exceed 99.");
			}
			return string.Format("{0}.z{1:D2}", Path.Combine(Path.GetDirectoryName(this._baseName), Path.GetFileNameWithoutExtension(this._baseName)), diskNumber + 1U);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D6AD File Offset: 0x0000B8AD
		public uint ComputeSegment(int length)
		{
			if (this._innerStream.Position + (long)length > (long)this._maxSegmentSize)
			{
				return this.CurrentSegment + 1U;
			}
			return this.CurrentSegment;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public override string ToString()
		{
			return string.Format("{0}[{1}][{2}], pos=0x{3:X})", new object[]
			{
				"ZipSegmentedStream",
				this.CurrentName,
				this.rwMode.ToString(),
				this.Position
			});
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D72C File Offset: 0x0000B92C
		private void _SetReadStream()
		{
			if (this._innerStream != null)
			{
				this._innerStream.Close();
			}
			if (this.CurrentSegment + 1U == this._maxDiskNumber)
			{
				this._currentName = this._baseName;
			}
			this._innerStream = File.OpenRead(this.CurrentName);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000D77C File Offset: 0x0000B97C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.ReadOnly)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Read.");
			}
			int num = this._innerStream.Read(buffer, offset, count);
			int num2 = num;
			while (num2 != count)
			{
				if (this._innerStream.Position != this._innerStream.Length)
				{
					this._exceptionPending = true;
					throw new ZipException(string.Format("Read error in file {0}", this.CurrentName));
				}
				if (this.CurrentSegment + 1U == this._maxDiskNumber)
				{
					return num;
				}
				this.CurrentSegment += 1U;
				this._SetReadStream();
				offset += num2;
				count -= num2;
				num2 = this._innerStream.Read(buffer, offset, count);
				num += num2;
			}
			return num;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000D834 File Offset: 0x0000BA34
		private void _SetWriteStream(uint increment)
		{
			if (this._innerStream != null)
			{
				this._innerStream.Close();
				if (File.Exists(this.CurrentName))
				{
					File.Delete(this.CurrentName);
				}
				File.Move(this._currentTempName, this.CurrentName);
			}
			if (increment > 0U)
			{
				this.CurrentSegment += increment;
			}
			SharedUtilities.CreateAndOpenUniqueTempFile(this._baseDir, out this._innerStream, out this._currentTempName);
			if (this.CurrentSegment == 0U)
			{
				this._innerStream.Write(BitConverter.GetBytes(134695760), 0, 4);
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Stream Error: Cannot Write.");
			}
			if (this.ContiguousWrite)
			{
				if (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					this._SetWriteStream(1U);
				}
			}
			else
			{
				while (this._innerStream.Position + (long)count > (long)this._maxSegmentSize)
				{
					int num = this._maxSegmentSize - (int)this._innerStream.Position;
					this._innerStream.Write(buffer, offset, num);
					this._SetWriteStream(1U);
					count -= num;
					offset += num;
				}
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000D970 File Offset: 0x0000BB70
		public long TruncateBackward(uint diskNumber, long offset)
		{
			if (diskNumber >= 99U)
			{
				throw new ArgumentOutOfRangeException("diskNumber");
			}
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new ZipException("bad state.");
			}
			if (diskNumber == this.CurrentSegment)
			{
				long num = this._innerStream.Seek(offset, 0);
				SharedUtilities.Workaround_Ladybug318918(this._innerStream);
				return num;
			}
			if (this._innerStream != null)
			{
				this._innerStream.Close();
				if (File.Exists(this._currentTempName))
				{
					File.Delete(this._currentTempName);
				}
			}
			for (uint num2 = this.CurrentSegment - 1U; num2 > diskNumber; num2 -= 1U)
			{
				string text = this._NameForSegment(num2);
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			this.CurrentSegment = diskNumber;
			for (int i = 0; i < 3; i++)
			{
				try
				{
					this._currentTempName = SharedUtilities.InternalGetTempFileName();
					File.Move(this.CurrentName, this._currentTempName);
					break;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			this._innerStream = new FileStream(this._currentTempName, 3);
			long num3 = this._innerStream.Seek(offset, 0);
			SharedUtilities.Workaround_Ladybug318918(this._innerStream);
			return num3;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public override bool CanRead
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.ReadOnly && this._innerStream != null && this._innerStream.CanRead;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		public override bool CanSeek
		{
			get
			{
				return this._innerStream != null && this._innerStream.CanSeek;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000DACF File Offset: 0x0000BCCF
		public override bool CanWrite
		{
			get
			{
				return this.rwMode == ZipSegmentedStream.RwMode.Write && this._innerStream != null && this._innerStream.CanWrite;
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DAEF File Offset: 0x0000BCEF
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000DAFC File Offset: 0x0000BCFC
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000DB09 File Offset: 0x0000BD09
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000DB16 File Offset: 0x0000BD16
		public override long Position
		{
			get
			{
				return this._innerStream.Position;
			}
			set
			{
				this._innerStream.Position = value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = this._innerStream.Seek(offset, origin);
			SharedUtilities.Workaround_Ladybug318918(this._innerStream);
			return num;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000DB4B File Offset: 0x0000BD4B
		public override void SetLength(long value)
		{
			if (this.rwMode != ZipSegmentedStream.RwMode.Write)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException();
			}
			this._innerStream.SetLength(value);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DB70 File Offset: 0x0000BD70
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this._innerStream != null)
				{
					this._innerStream.Close();
					if (this.rwMode == ZipSegmentedStream.RwMode.Write)
					{
						bool exceptionPending = this._exceptionPending;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0400012E RID: 302
		private ZipSegmentedStream.RwMode rwMode;

		// Token: 0x0400012F RID: 303
		private bool _exceptionPending;

		// Token: 0x04000130 RID: 304
		private string _baseName;

		// Token: 0x04000131 RID: 305
		private string _baseDir;

		// Token: 0x04000132 RID: 306
		private string _currentName;

		// Token: 0x04000133 RID: 307
		private string _currentTempName;

		// Token: 0x04000134 RID: 308
		private uint _currentDiskNumber;

		// Token: 0x04000135 RID: 309
		private uint _maxDiskNumber;

		// Token: 0x04000136 RID: 310
		private int _maxSegmentSize;

		// Token: 0x04000137 RID: 311
		private Stream _innerStream;

		// Token: 0x04000138 RID: 312
		private bool <ContiguousWrite>k__BackingField;

		// Token: 0x0200002F RID: 47
		private enum RwMode
		{
			// Token: 0x0400013A RID: 314
			None,
			// Token: 0x0400013B RID: 315
			ReadOnly,
			// Token: 0x0400013C RID: 316
			Write
		}
	}
}
