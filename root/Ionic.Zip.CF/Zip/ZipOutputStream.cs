using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200002C RID: 44
	public class ZipOutputStream : Stream
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000CB61 File Offset: 0x0000AD61
		public ZipOutputStream(Stream stream)
			: this(stream, false)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public ZipOutputStream(string fileName)
		{
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			base..ctor();
			Stream stream = File.Open(fileName, 2, 3, 0);
			this._Init(stream, false, fileName);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CBA2 File Offset: 0x0000ADA2
		public ZipOutputStream(Stream stream, bool leaveOpen)
		{
			this._alternateEncoding = Encoding.GetEncoding("IBM437");
			base..ctor();
			this._Init(stream, leaveOpen, null);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		private void _Init(Stream stream, bool leaveOpen, string name)
		{
			this._outputStream = (stream.CanRead ? stream : new CountingStream(stream));
			this.CompressionLevel = CompressionLevel.Default;
			this.CompressionMethod = CompressionMethod.Deflate;
			this._encryption = EncryptionAlgorithm.None;
			this._entriesWritten = new Dictionary<string, ZipEntry>(StringComparer.Ordinal);
			this._zip64 = Zip64Option.Default;
			this._leaveUnderlyingStreamOpen = leaveOpen;
			this.Strategy = CompressionStrategy.Default;
			this._name = name ?? "(stream)";
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CC32 File Offset: 0x0000AE32
		public override string ToString()
		{
			return string.Format("ZipOutputStream::{0}(leaveOpen({1})))", this._name, this._leaveUnderlyingStreamOpen);
		}

		// Token: 0x17000089 RID: 137
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000CC50 File Offset: 0x0000AE50
		public string Password
		{
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._password = value;
				if (this._password == null)
				{
					this._encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._encryption == EncryptionAlgorithm.None)
				{
					this._encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000CC9D File Offset: 0x0000AE9D
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._encryption;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._encryption = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000CCDE File Offset: 0x0000AEDE
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000CCEF File Offset: 0x0000AEEF
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000CCF7 File Offset: 0x0000AEF7
		public CompressionStrategy Strategy
		{
			get
			{
				return this.<Strategy>k__BackingField;
			}
			set
			{
				this.<Strategy>k__BackingField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000CD00 File Offset: 0x0000AF00
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000CD08 File Offset: 0x0000AF08
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._timestamp = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000CD2B File Offset: 0x0000AF2B
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000CD33 File Offset: 0x0000AF33
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this.<CompressionLevel>k__BackingField;
			}
			set
			{
				this.<CompressionLevel>k__BackingField = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public CompressionMethod CompressionMethod
		{
			get
			{
				return this.<CompressionMethod>k__BackingField;
			}
			set
			{
				this.<CompressionMethod>k__BackingField = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000CD55 File Offset: 0x0000AF55
		public string Comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._comment = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000CD78 File Offset: 0x0000AF78
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000CD80 File Offset: 0x0000AF80
		public Zip64Option EnableZip64
		{
			get
			{
				return this._zip64;
			}
			set
			{
				if (this._disposed)
				{
					this._exceptionPending = true;
					throw new InvalidOperationException("The stream has been closed.");
				}
				this._zip64 = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		public bool OutputUsedZip64
		{
			get
			{
				return this._anyEntriesUsedZip64 || this._directoryNeededZip64;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000CDB5 File Offset: 0x0000AFB5
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000CDC0 File Offset: 0x0000AFC0
		public bool IgnoreCase
		{
			get
			{
				return !this._DontIgnoreCase;
			}
			set
			{
				this._DontIgnoreCase = !value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete. It will be removed in a future version of the library. Use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this._alternateEncoding == Encoding.UTF8 && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this._alternateEncoding = Encoding.UTF8;
					this._alternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this._alternateEncoding = ZipOutputStream.DefaultEncoding;
				this._alternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000CE10 File Offset: 0x0000B010
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000CE23 File Offset: 0x0000B023
		[Obsolete("use AlternateEncoding and AlternateEncodingUsage instead.")]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				if (this._alternateEncodingUsage == ZipOption.AsNecessary)
				{
					return this._alternateEncoding;
				}
				return null;
			}
			set
			{
				this._alternateEncoding = value;
				this._alternateEncodingUsage = ZipOption.AsNecessary;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000CE33 File Offset: 0x0000B033
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000CE3B File Offset: 0x0000B03B
		public Encoding AlternateEncoding
		{
			get
			{
				return this._alternateEncoding;
			}
			set
			{
				this._alternateEncoding = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000CE44 File Offset: 0x0000B044
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this._alternateEncodingUsage;
			}
			set
			{
				this._alternateEncodingUsage = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000CE55 File Offset: 0x0000B055
		public static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.GetEncoding("IBM437");
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000CE61 File Offset: 0x0000B061
		private void InsureUniqueEntry(ZipEntry ze1)
		{
			if (this._entriesWritten.ContainsKey(ze1.FileName))
			{
				this._exceptionPending = true;
				throw new ArgumentException(string.Format("The entry '{0}' already exists in the zip archive.", ze1.FileName));
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000CE93 File Offset: 0x0000B093
		internal Stream OutputStream
		{
			get
			{
				return this._outputStream;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000CE9B File Offset: 0x0000B09B
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CEA3 File Offset: 0x0000B0A3
		public bool ContainsEntry(string name)
		{
			return this._entriesWritten.ContainsKey(SharedUtilities.NormalizePathForUseInZipFile(name));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			if (buffer == null)
			{
				this._exceptionPending = true;
				throw new ArgumentNullException("buffer");
			}
			if (this._currentEntry == null)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You must call PutNextEntry() before calling Write().");
			}
			if (this._currentEntry.IsDirectory)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("You cannot Write() data for an entry that is a directory.");
			}
			if (this._needToWriteEntryHeader)
			{
				this._InitiateCurrentEntry(false);
			}
			if (count != 0)
			{
				this._entryOutputStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000CF50 File Offset: 0x0000B150
		public ZipEntry PutNextEntry(string entryName)
		{
			if (string.IsNullOrEmpty(entryName))
			{
				throw new ArgumentNullException("entryName");
			}
			if (this._disposed)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("The stream has been closed.");
			}
			this._FinishCurrentEntry();
			this._currentEntry = ZipEntry.CreateForZipOutputStream(entryName);
			this._currentEntry._container = new ZipContainer(this);
			ZipEntry currentEntry = this._currentEntry;
			currentEntry._BitField |= 8;
			this._currentEntry.SetEntryTimes(DateTime.Now, DateTime.Now, DateTime.Now);
			this._currentEntry.CompressionLevel = this.CompressionLevel;
			this._currentEntry.CompressionMethod = this.CompressionMethod;
			this._currentEntry.Password = this._password;
			this._currentEntry.Encryption = this.Encryption;
			this._currentEntry.AlternateEncoding = this.AlternateEncoding;
			this._currentEntry.AlternateEncodingUsage = this.AlternateEncodingUsage;
			if (entryName.EndsWith("/"))
			{
				this._currentEntry.MarkAsDirectory();
			}
			this._currentEntry.EmitTimesInWindowsFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Windows) != ZipEntryTimestamp.None;
			this._currentEntry.EmitTimesInUnixFormatWhenSaving = (this._timestamp & ZipEntryTimestamp.Unix) != ZipEntryTimestamp.None;
			this.InsureUniqueEntry(this._currentEntry);
			this._needToWriteEntryHeader = true;
			return this._currentEntry;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		private void _InitiateCurrentEntry(bool finishing)
		{
			this._entriesWritten.Add(this._currentEntry.FileName, this._currentEntry);
			this._entryCount++;
			if (this._entryCount > 65534 && this._zip64 == Zip64Option.Default)
			{
				this._exceptionPending = true;
				throw new InvalidOperationException("Too many entries. Consider setting ZipOutputStream.EnableZip64.");
			}
			this._currentEntry.WriteHeader(this._outputStream, finishing ? 99 : 0);
			this._currentEntry.StoreRelativeOffset();
			if (!this._currentEntry.IsDirectory)
			{
				this._currentEntry.WriteSecurityMetadata(this._outputStream);
				this._currentEntry.PrepOutputStream(this._outputStream, finishing ? 0L : (-1L), out this._outputCounter, out this._encryptor, out this._deflater, out this._entryOutputStream);
			}
			this._needToWriteEntryHeader = false;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D17C File Offset: 0x0000B37C
		private void _FinishCurrentEntry()
		{
			if (this._currentEntry != null)
			{
				if (this._needToWriteEntryHeader)
				{
					this._InitiateCurrentEntry(true);
				}
				this._currentEntry.FinishOutputStream(this._outputStream, this._outputCounter, this._encryptor, this._deflater, this._entryOutputStream);
				this._currentEntry.PostProcessOutput(this._outputStream);
				if (this._currentEntry.OutputUsedZip64 != null)
				{
					this._anyEntriesUsedZip64 |= this._currentEntry.OutputUsedZip64.Value;
				}
				this._outputCounter = null;
				this._encryptor = (this._deflater = null);
				this._entryOutputStream = null;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D230 File Offset: 0x0000B430
		protected override void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && !this._exceptionPending)
			{
				this._FinishCurrentEntry();
				this._directoryNeededZip64 = ZipOutput.WriteCentralDirectoryStructure(this._outputStream, this._entriesWritten.Values, 1U, this._zip64, this.Comment, new ZipContainer(this));
				CountingStream countingStream = this._outputStream as CountingStream;
				Stream stream;
				if (countingStream != null)
				{
					stream = countingStream.WrappedStream;
					countingStream.Close();
				}
				else
				{
					stream = this._outputStream;
				}
				if (!this._leaveUnderlyingStreamOpen)
				{
					stream.Close();
				}
				this._outputStream = null;
			}
			this._disposed = true;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000D2C9 File Offset: 0x0000B4C9
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000D2CC File Offset: 0x0000B4CC
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000D2CF File Offset: 0x0000B4CF
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000D2D9 File Offset: 0x0000B4D9
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000D2E6 File Offset: 0x0000B4E6
		public override long Position
		{
			get
			{
				return this._outputStream.Position;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D2ED File Offset: 0x0000B4ED
		public override void Flush()
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D2EF File Offset: 0x0000B4EF
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Read");
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D2FB File Offset: 0x0000B4FB
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Seek");
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D307 File Offset: 0x0000B507
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000110 RID: 272
		private EncryptionAlgorithm _encryption;

		// Token: 0x04000111 RID: 273
		private ZipEntryTimestamp _timestamp;

		// Token: 0x04000112 RID: 274
		internal string _password;

		// Token: 0x04000113 RID: 275
		private string _comment;

		// Token: 0x04000114 RID: 276
		private Stream _outputStream;

		// Token: 0x04000115 RID: 277
		private ZipEntry _currentEntry;

		// Token: 0x04000116 RID: 278
		internal Zip64Option _zip64;

		// Token: 0x04000117 RID: 279
		private Dictionary<string, ZipEntry> _entriesWritten;

		// Token: 0x04000118 RID: 280
		private int _entryCount;

		// Token: 0x04000119 RID: 281
		private ZipOption _alternateEncodingUsage;

		// Token: 0x0400011A RID: 282
		private Encoding _alternateEncoding;

		// Token: 0x0400011B RID: 283
		private bool _leaveUnderlyingStreamOpen;

		// Token: 0x0400011C RID: 284
		private bool _disposed;

		// Token: 0x0400011D RID: 285
		private bool _exceptionPending;

		// Token: 0x0400011E RID: 286
		private bool _anyEntriesUsedZip64;

		// Token: 0x0400011F RID: 287
		private bool _directoryNeededZip64;

		// Token: 0x04000120 RID: 288
		private CountingStream _outputCounter;

		// Token: 0x04000121 RID: 289
		private Stream _encryptor;

		// Token: 0x04000122 RID: 290
		private Stream _deflater;

		// Token: 0x04000123 RID: 291
		private CrcCalculatorStream _entryOutputStream;

		// Token: 0x04000124 RID: 292
		private bool _needToWriteEntryHeader;

		// Token: 0x04000125 RID: 293
		private string _name;

		// Token: 0x04000126 RID: 294
		private bool _DontIgnoreCase;

		// Token: 0x04000127 RID: 295
		private int <CodecBufferSize>k__BackingField;

		// Token: 0x04000128 RID: 296
		private CompressionStrategy <Strategy>k__BackingField;

		// Token: 0x04000129 RID: 297
		private CompressionLevel <CompressionLevel>k__BackingField;

		// Token: 0x0400012A RID: 298
		private CompressionMethod <CompressionMethod>k__BackingField;
	}
}
