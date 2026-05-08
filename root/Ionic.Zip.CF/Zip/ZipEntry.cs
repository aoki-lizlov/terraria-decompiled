using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ionic.BZip2;
using Ionic.Crc;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200001D RID: 29
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00004")]
	[ComVisible(true)]
	public class ZipEntry
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000033DF File Offset: 0x000015DF
		internal bool AttributesIndicateDirectory
		{
			get
			{
				return this._InternalFileAttrs == 0 && (this._ExternalFileAttrs & 16) == 16;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000033F8 File Offset: 0x000015F8
		internal void ResetDirEntry()
		{
			this.__FileDataPosition = -1L;
			this._LengthOfHeader = 0;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000340C File Offset: 0x0000160C
		public string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(string.Format("          ZipEntry: {0}\n", this.FileName)).Append(string.Format("   Version Made By: {0}\n", this._VersionMadeBy)).Append(string.Format(" Needed to extract: {0}\n", this.VersionNeeded));
				if (this._IsDirectory)
				{
					stringBuilder.Append("        Entry type: directory\n");
				}
				else
				{
					stringBuilder.Append(string.Format("         File type: {0}\n", this._IsText ? "text" : "binary")).Append(string.Format("       Compression: {0}\n", this.CompressionMethod)).Append(string.Format("        Compressed: 0x{0:X}\n", this.CompressedSize))
						.Append(string.Format("      Uncompressed: 0x{0:X}\n", this.UncompressedSize))
						.Append(string.Format("             CRC32: 0x{0:X8}\n", this._Crc32));
				}
				stringBuilder.Append(string.Format("       Disk Number: {0}\n", this._diskNumber));
				if (this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1)))
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X16}\n", this._RelativeOffsetOfLocalHeader));
				}
				else
				{
					stringBuilder.Append(string.Format("   Relative Offset: 0x{0:X8}\n", this._RelativeOffsetOfLocalHeader));
				}
				stringBuilder.Append(string.Format("         Bit Field: 0x{0:X4}\n", this._BitField)).Append(string.Format("        Encrypted?: {0}\n", this._sourceIsEncrypted)).Append(string.Format("          Timeblob: 0x{0:X8}\n", this._TimeBlob))
					.Append(string.Format("              Time: {0}\n", SharedUtilities.PackedToDateTime(this._TimeBlob)));
				stringBuilder.Append(string.Format("         Is Zip64?: {0}\n", this._InputUsesZip64));
				if (!string.IsNullOrEmpty(this._Comment))
				{
					stringBuilder.Append(string.Format("           Comment: {0}\n", this._Comment));
				}
				stringBuilder.Append("\n");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003634 File Offset: 0x00001834
		internal static ZipEntry ReadDirEntry(ZipFile zf, Dictionary<string, object> previouslySeen)
		{
			Stream readStream = zf.ReadStream;
			Encoding encoding = ((zf.AlternateEncodingUsage == ZipOption.Always) ? zf.AlternateEncoding : ZipFile.DefaultEncoding);
			int num = SharedUtilities.ReadSignature(readStream);
			if (ZipEntry.IsNotValidZipDirEntrySig(num))
			{
				readStream.Seek(-4L, 1);
				SharedUtilities.Workaround_Ladybug318918(readStream);
				if ((long)num != 101010256L && (long)num != 101075792L && num != 67324752)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position 0x{1:X8}", num, readStream.Position));
				}
				return null;
			}
			else
			{
				int num2 = 46;
				byte[] array = new byte[42];
				int num3 = readStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return null;
				}
				int num4 = 0;
				ZipEntry zipEntry = new ZipEntry();
				zipEntry.AlternateEncoding = encoding;
				zipEntry._Source = ZipEntrySource.ZipFile;
				zipEntry._container = new ZipContainer(zf);
				zipEntry._VersionMadeBy = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._LastModified = SharedUtilities.PackedToDateTime(zipEntry._TimeBlob);
				zipEntry._timestamp |= ZipEntryTimestamp.DOS;
				zipEntry._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry._CompressionMethod_FromZipFile = zipEntry._CompressionMethod;
				zipEntry._filenameLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._extraFieldLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._commentLength = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._diskNumber = (uint)array[num4++] + (uint)array[num4++] * 256U;
				zipEntry._InternalFileAttrs = (short)((int)array[num4++] + (int)array[num4++] * 256);
				zipEntry._ExternalFileAttrs = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				zipEntry._RelativeOffsetOfLocalHeader = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				zipEntry.IsText = (zipEntry._InternalFileAttrs & 1) == 1;
				array = new byte[(int)zipEntry._filenameLength];
				num3 = readStream.Read(array, 0, array.Length);
				num2 += num3;
				if ((zipEntry._BitField & 2048) == 2048)
				{
					zipEntry._FileNameInArchive = SharedUtilities.Utf8StringFromBuffer(array);
				}
				else
				{
					zipEntry._FileNameInArchive = SharedUtilities.StringFromBuffer(array, encoding);
				}
				while (previouslySeen.ContainsKey(zipEntry._FileNameInArchive))
				{
					zipEntry._FileNameInArchive = ZipEntry.CopyHelper.AppendCopyToFileName(zipEntry._FileNameInArchive);
					zipEntry._metadataChanged = true;
				}
				if (zipEntry.AttributesIndicateDirectory)
				{
					zipEntry.MarkAsDirectory();
				}
				else if (zipEntry._FileNameInArchive.EndsWith("/"))
				{
					zipEntry.MarkAsDirectory();
				}
				zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				if ((zipEntry._BitField & 1) == 1)
				{
					zipEntry._Encryption_FromZipFile = (zipEntry._Encryption = EncryptionAlgorithm.PkzipWeak);
					zipEntry._sourceIsEncrypted = true;
				}
				if (zipEntry._extraFieldLength > 0)
				{
					zipEntry._InputUsesZip64 = zipEntry._CompressedSize == (long)((ulong)(-1)) || zipEntry._UncompressedSize == (long)((ulong)(-1)) || zipEntry._RelativeOffsetOfLocalHeader == (long)((ulong)(-1));
					num2 += zipEntry.ProcessExtraField(readStream, zipEntry._extraFieldLength);
					zipEntry._CompressedFileDataSize = zipEntry._CompressedSize;
				}
				if (zipEntry._Encryption == EncryptionAlgorithm.PkzipWeak)
				{
					zipEntry._CompressedFileDataSize -= 12L;
				}
				if ((zipEntry._BitField & 8) == 8)
				{
					if (zipEntry._InputUsesZip64)
					{
						zipEntry._LengthOfTrailer += 24;
					}
					else
					{
						zipEntry._LengthOfTrailer += 16;
					}
				}
				zipEntry.AlternateEncoding = (((zipEntry._BitField & 2048) == 2048) ? Encoding.UTF8 : encoding);
				zipEntry.AlternateEncodingUsage = ZipOption.Always;
				if (zipEntry._commentLength > 0)
				{
					array = new byte[(int)zipEntry._commentLength];
					num3 = readStream.Read(array, 0, array.Length);
					num2 += num3;
					if ((zipEntry._BitField & 2048) == 2048)
					{
						zipEntry._Comment = SharedUtilities.Utf8StringFromBuffer(array);
					}
					else
					{
						zipEntry._Comment = SharedUtilities.StringFromBuffer(array, encoding);
					}
				}
				return zipEntry;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003CA4 File Offset: 0x00001EA4
		internal static bool IsNotValidZipDirEntrySig(int signature)
		{
			return signature != 33639248;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public ZipEntry()
		{
			this._CompressionMethod = 8;
			this._CompressionLevel = CompressionLevel.Default;
			this._Encryption = EncryptionAlgorithm.None;
			this._Source = ZipEntrySource.None;
			this.AlternateEncoding = Encoding.GetEncoding("IBM437");
			this.AlternateEncodingUsage = ZipOption.Default;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003D1B File Offset: 0x00001F1B
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003D28 File Offset: 0x00001F28
		public DateTime LastModified
		{
			get
			{
				return this._LastModified.ToLocalTime();
			}
			set
			{
				this._LastModified = ((value.Kind == null) ? DateTime.SpecifyKind(value, 2) : value.ToLocalTime());
				this._Mtime = SharedUtilities.AdjustTime_Reverse(this._LastModified).ToUniversalTime();
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003D74 File Offset: 0x00001F74
		private int BufferSize
		{
			get
			{
				return this._container.BufferSize;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003D81 File Offset: 0x00001F81
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003D89 File Offset: 0x00001F89
		public DateTime ModifiedTime
		{
			get
			{
				return this._Mtime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, this._Atime, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003D9E File Offset: 0x00001F9E
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003DA6 File Offset: 0x00001FA6
		public DateTime AccessedTime
		{
			get
			{
				return this._Atime;
			}
			set
			{
				this.SetEntryTimes(this._Ctime, value, this._Mtime);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003DBB File Offset: 0x00001FBB
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public DateTime CreationTime
		{
			get
			{
				return this._Ctime;
			}
			set
			{
				this.SetEntryTimes(value, this._Atime, this._Mtime);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public void SetEntryTimes(DateTime created, DateTime accessed, DateTime modified)
		{
			this._ntfsTimesAreSet = true;
			if (created == ZipEntry._zeroHour && created.Kind == ZipEntry._zeroHour.Kind)
			{
				created = ZipEntry._win32Epoch;
			}
			if (accessed == ZipEntry._zeroHour && accessed.Kind == ZipEntry._zeroHour.Kind)
			{
				accessed = ZipEntry._win32Epoch;
			}
			if (modified == ZipEntry._zeroHour && modified.Kind == ZipEntry._zeroHour.Kind)
			{
				modified = ZipEntry._win32Epoch;
			}
			this._Ctime = created.ToUniversalTime();
			this._Atime = accessed.ToUniversalTime();
			this._Mtime = modified.ToUniversalTime();
			this._LastModified = this._Mtime;
			if (!this._emitUnixTimes && !this._emitNtfsTimes)
			{
				this._emitNtfsTimes = true;
			}
			this._metadataChanged = true;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003EB2 File Offset: 0x000020B2
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003EBA File Offset: 0x000020BA
		public bool EmitTimesInWindowsFormatWhenSaving
		{
			get
			{
				return this._emitNtfsTimes;
			}
			set
			{
				this._emitNtfsTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003ECA File Offset: 0x000020CA
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003ED2 File Offset: 0x000020D2
		public bool EmitTimesInUnixFormatWhenSaving
		{
			get
			{
				return this._emitUnixTimes;
			}
			set
			{
				this._emitUnixTimes = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003EE2 File Offset: 0x000020E2
		public ZipEntryTimestamp Timestamp
		{
			get
			{
				return this._timestamp;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003EEA File Offset: 0x000020EA
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003EF2 File Offset: 0x000020F2
		public FileAttributes Attributes
		{
			get
			{
				return this._ExternalFileAttrs;
			}
			set
			{
				this._ExternalFileAttrs = value;
				this._VersionMadeBy = 45;
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003F0A File Offset: 0x0000210A
		internal string LocalFileName
		{
			get
			{
				return this._LocalFileName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003F12 File Offset: 0x00002112
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003F1C File Offset: 0x0000211C
		public string FileName
		{
			get
			{
				return this._FileNameInArchive;
			}
			set
			{
				if (this._container.ZipFile == null)
				{
					throw new ZipException("Cannot rename; this is not supported in ZipOutputStream/ZipInputStream.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ZipException("The FileName must be non empty and non-null.");
				}
				string text = ZipEntry.NameInArchive(value, null);
				if (this._FileNameInArchive == text)
				{
					return;
				}
				this._container.ZipFile.RemoveEntry(this);
				this._container.ZipFile.InternalAddEntry(text, this);
				this._FileNameInArchive = text;
				this._container.ZipFile.NotifyEntryChanged();
				this._metadataChanged = true;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003FAC File Offset: 0x000021AC
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003FB4 File Offset: 0x000021B4
		public Stream InputStream
		{
			get
			{
				return this._sourceStream;
			}
			set
			{
				if (this._Source != ZipEntrySource.Stream)
				{
					throw new ZipException("You must not set the input stream for this entry.");
				}
				this._sourceWasJitProvided = true;
				this._sourceStream = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003FD8 File Offset: 0x000021D8
		public bool InputStreamWasJitProvided
		{
			get
			{
				return this._sourceWasJitProvided;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003FE0 File Offset: 0x000021E0
		public ZipEntrySource Source
		{
			get
			{
				return this._Source;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003FE8 File Offset: 0x000021E8
		public short VersionNeeded
		{
			get
			{
				return this._VersionNeeded;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003FF0 File Offset: 0x000021F0
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003FF8 File Offset: 0x000021F8
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				this._Comment = value;
				this._metadataChanged = true;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004008 File Offset: 0x00002208
		public bool? RequiresZip64
		{
			get
			{
				return this._entryRequiresZip64;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004010 File Offset: 0x00002210
		public bool? OutputUsedZip64
		{
			get
			{
				return this._OutputUsesZip64;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004018 File Offset: 0x00002218
		public short BitField
		{
			get
			{
				return this._BitField;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004020 File Offset: 0x00002220
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004028 File Offset: 0x00002228
		public CompressionMethod CompressionMethod
		{
			get
			{
				return (CompressionMethod)this._CompressionMethod;
			}
			set
			{
				if (value == (CompressionMethod)this._CompressionMethod)
				{
					return;
				}
				if (value != CompressionMethod.None && value != CompressionMethod.Deflate && value != CompressionMethod.BZip2)
				{
					throw new InvalidOperationException("Unsupported compression method.");
				}
				this._CompressionMethod = (short)value;
				if (this._CompressionMethod == 0)
				{
					this._CompressionLevel = CompressionLevel.None;
				}
				else if (this.CompressionLevel == CompressionLevel.None)
				{
					this._CompressionLevel = CompressionLevel.Default;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000040A2 File Offset: 0x000022A2
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000040AC File Offset: 0x000022AC
		public CompressionLevel CompressionLevel
		{
			get
			{
				return this._CompressionLevel;
			}
			set
			{
				if (this._CompressionMethod != 8 && this._CompressionMethod != 0)
				{
					return;
				}
				if (value == CompressionLevel.Default && this._CompressionMethod == 8)
				{
					return;
				}
				this._CompressionLevel = value;
				if (value == CompressionLevel.None && this._CompressionMethod == 0)
				{
					return;
				}
				if (this._CompressionLevel == CompressionLevel.None)
				{
					this._CompressionMethod = 0;
				}
				else
				{
					this._CompressionMethod = 8;
				}
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
				this._restreamRequiredOnSave = true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004128 File Offset: 0x00002328
		public long CompressedSize
		{
			get
			{
				return this._CompressedSize;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004130 File Offset: 0x00002330
		public long UncompressedSize
		{
			get
			{
				return this._UncompressedSize;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004138 File Offset: 0x00002338
		public double CompressionRatio
		{
			get
			{
				if (this.UncompressedSize == 0L)
				{
					return 0.0;
				}
				return 100.0 * (1.0 - 1.0 * (double)this.CompressedSize / (1.0 * (double)this.UncompressedSize));
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004190 File Offset: 0x00002390
		public int Crc
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004198 File Offset: 0x00002398
		public bool IsDirectory
		{
			get
			{
				return this._IsDirectory;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000041A0 File Offset: 0x000023A0
		public bool UsesEncryption
		{
			get
			{
				return this._Encryption_FromZipFile != EncryptionAlgorithm.None;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000041AE File Offset: 0x000023AE
		// (set) Token: 0x060000CF RID: 207 RVA: 0x000041B8 File Offset: 0x000023B8
		public EncryptionAlgorithm Encryption
		{
			get
			{
				return this._Encryption;
			}
			set
			{
				if (value == this._Encryption)
				{
					return;
				}
				if (value == EncryptionAlgorithm.Unsupported)
				{
					throw new InvalidOperationException("You may not set Encryption to that value.");
				}
				this._Encryption = value;
				this._restreamRequiredOnSave = true;
				if (this._container.ZipFile != null)
				{
					this._container.ZipFile.NotifyEntryChanged();
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004249 File Offset: 0x00002449
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004209 File Offset: 0x00002409
		public string Password
		{
			private get
			{
				return this._Password;
			}
			set
			{
				this._Password = value;
				if (this._Password == null)
				{
					this._Encryption = EncryptionAlgorithm.None;
					return;
				}
				if (this._Source == ZipEntrySource.ZipFile && !this._sourceIsEncrypted)
				{
					this._restreamRequiredOnSave = true;
				}
				if (this.Encryption == EncryptionAlgorithm.None)
				{
					this._Encryption = EncryptionAlgorithm.PkzipWeak;
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004251 File Offset: 0x00002451
		internal bool IsChanged
		{
			get
			{
				return this._restreamRequiredOnSave | this._metadataChanged;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004260 File Offset: 0x00002460
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004268 File Offset: 0x00002468
		public ExtractExistingFileAction ExtractExistingFile
		{
			get
			{
				return this.<ExtractExistingFile>k__BackingField;
			}
			set
			{
				this.<ExtractExistingFile>k__BackingField = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004271 File Offset: 0x00002471
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004279 File Offset: 0x00002479
		public ZipErrorAction ZipErrorAction
		{
			get
			{
				return this.<ZipErrorAction>k__BackingField;
			}
			set
			{
				this.<ZipErrorAction>k__BackingField = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004282 File Offset: 0x00002482
		public bool IncludedInMostRecentSave
		{
			get
			{
				return !this._skippedDuringSave;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000428D File Offset: 0x0000248D
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004295 File Offset: 0x00002495
		public SetCompressionCallback SetCompression
		{
			get
			{
				return this.<SetCompression>k__BackingField;
			}
			set
			{
				this.<SetCompression>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000429E File Offset: 0x0000249E
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000042BD File Offset: 0x000024BD
		[Obsolete("Beginning with v1.9.1.6 of DotNetZip, this property is obsolete.  It will be removed in a future version of the library. Your applications should  use AlternateEncoding and AlternateEncodingUsage instead.")]
		public bool UseUnicodeAsNecessary
		{
			get
			{
				return this.AlternateEncoding == Encoding.GetEncoding("UTF-8") && this.AlternateEncodingUsage == ZipOption.AsNecessary;
			}
			set
			{
				if (value)
				{
					this.AlternateEncoding = Encoding.GetEncoding("UTF-8");
					this.AlternateEncodingUsage = ZipOption.AsNecessary;
					return;
				}
				this.AlternateEncoding = ZipFile.DefaultEncoding;
				this.AlternateEncodingUsage = ZipOption.Default;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000042EC File Offset: 0x000024EC
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000042F4 File Offset: 0x000024F4
		[Obsolete("This property is obsolete since v1.9.1.6. Use AlternateEncoding and AlternateEncodingUsage instead.", true)]
		public Encoding ProvisionalAlternateEncoding
		{
			get
			{
				return this.<ProvisionalAlternateEncoding>k__BackingField;
			}
			set
			{
				this.<ProvisionalAlternateEncoding>k__BackingField = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000042FD File Offset: 0x000024FD
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004305 File Offset: 0x00002505
		public Encoding AlternateEncoding
		{
			get
			{
				return this.<AlternateEncoding>k__BackingField;
			}
			set
			{
				this.<AlternateEncoding>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000430E File Offset: 0x0000250E
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004316 File Offset: 0x00002516
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				return this.<AlternateEncodingUsage>k__BackingField;
			}
			set
			{
				this.<AlternateEncodingUsage>k__BackingField = value;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004320 File Offset: 0x00002520
		internal static string NameInArchive(string filename, string directoryPathInArchive)
		{
			string text;
			if (directoryPathInArchive == null)
			{
				text = filename;
			}
			else if (string.IsNullOrEmpty(directoryPathInArchive))
			{
				text = Path.GetFileName(filename);
			}
			else
			{
				text = Path.Combine(directoryPathInArchive, Path.GetFileName(filename));
			}
			return SharedUtilities.NormalizePathForUseInZipFile(text);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000435C File Offset: 0x0000255C
		internal static ZipEntry CreateFromNothing(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.None, null, null);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004367 File Offset: 0x00002567
		internal static ZipEntry CreateFromFile(string filename, string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.FileSystem, filename, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004372 File Offset: 0x00002572
		internal static ZipEntry CreateForStream(string entryName, Stream s)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.Stream, s, null);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000437D File Offset: 0x0000257D
		internal static ZipEntry CreateForWriter(string entryName, WriteDelegate d)
		{
			return ZipEntry.Create(entryName, ZipEntrySource.WriteDelegate, d, null);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004388 File Offset: 0x00002588
		internal static ZipEntry CreateForJitStreamProvider(string nameInArchive, OpenDelegate opener, CloseDelegate closer)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.JitStream, opener, closer);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004393 File Offset: 0x00002593
		internal static ZipEntry CreateForZipOutputStream(string nameInArchive)
		{
			return ZipEntry.Create(nameInArchive, ZipEntrySource.ZipOutputStream, null, null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000043A0 File Offset: 0x000025A0
		private static ZipEntry Create(string nameInArchive, ZipEntrySource source, object arg1, object arg2)
		{
			if (string.IsNullOrEmpty(nameInArchive))
			{
				throw new ZipException("The entry name must be non-null and non-empty.");
			}
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._VersionMadeBy = 45;
			zipEntry._Source = source;
			zipEntry._Mtime = (zipEntry._Atime = (zipEntry._Ctime = DateTime.UtcNow));
			if (source == ZipEntrySource.Stream)
			{
				zipEntry._sourceStream = arg1 as Stream;
			}
			else if (source == ZipEntrySource.WriteDelegate)
			{
				zipEntry._WriteDelegate = arg1 as WriteDelegate;
			}
			else if (source == ZipEntrySource.JitStream)
			{
				zipEntry._OpenDelegate = arg1 as OpenDelegate;
				zipEntry._CloseDelegate = arg2 as CloseDelegate;
			}
			else if (source != ZipEntrySource.ZipOutputStream)
			{
				if (source == ZipEntrySource.None)
				{
					zipEntry._Source = ZipEntrySource.FileSystem;
				}
				else
				{
					string text = arg1 as string;
					if (string.IsNullOrEmpty(text))
					{
						throw new ZipException("The filename must be non-null and non-empty.");
					}
					try
					{
						zipEntry._Mtime = File.GetLastWriteTime(text).ToUniversalTime();
						zipEntry._Ctime = File.GetCreationTime(text).ToUniversalTime();
						zipEntry._Atime = File.GetLastAccessTime(text).ToUniversalTime();
						if (File.Exists(text) || Directory.Exists(text))
						{
							zipEntry._ExternalFileAttrs = (int)NetCfFile.GetAttributes(text);
						}
						zipEntry._ntfsTimesAreSet = true;
						zipEntry._LocalFileName = Path.GetFullPath(text);
					}
					catch (PathTooLongException ex)
					{
						string text2 = string.Format("The path is too long, filename={0}", text);
						throw new ZipException(text2, ex);
					}
				}
			}
			zipEntry._LastModified = zipEntry._Mtime;
			zipEntry._FileNameInArchive = SharedUtilities.NormalizePathForUseInZipFile(nameInArchive);
			return zipEntry;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004520 File Offset: 0x00002720
		internal void MarkAsDirectory()
		{
			this._IsDirectory = true;
			if (!this._FileNameInArchive.EndsWith("/"))
			{
				this._FileNameInArchive += "/";
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004551 File Offset: 0x00002751
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004559 File Offset: 0x00002759
		public bool IsText
		{
			get
			{
				return this._IsText;
			}
			set
			{
				this._IsText = value;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004562 File Offset: 0x00002762
		public override string ToString()
		{
			return string.Format("ZipEntry::{0}", this.FileName);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004574 File Offset: 0x00002774
		internal Stream ArchiveStream
		{
			get
			{
				if (this._archiveStream == null)
				{
					if (this._container.ZipFile != null)
					{
						ZipFile zipFile = this._container.ZipFile;
						zipFile.Reset(false);
						this._archiveStream = zipFile.StreamForDiskNumber(this._diskNumber);
					}
					else
					{
						this._archiveStream = this._container.ZipOutputStream.OutputStream;
					}
				}
				return this._archiveStream;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000045DC File Offset: 0x000027DC
		private void SetFdpLoh()
		{
			long position = this.ArchiveStream.Position;
			try
			{
				this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
				SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			}
			catch (IOException ex)
			{
				string text = string.Format("Exception seeking  entry({0}) offset(0x{1:X8}) len(0x{2:X8})", this.FileName, this._RelativeOffsetOfLocalHeader, this.ArchiveStream.Length);
				throw new BadStateException(text, ex);
			}
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			short num = (short)((int)array[26] + (int)array[27] * 256);
			short num2 = (short)((int)array[28] + (int)array[29] * 256);
			this.ArchiveStream.Seek((long)(num + num2), 1);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			this._LengthOfHeader = (int)(30 + num2 + num) + ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
			this.__FileDataPosition = this._RelativeOffsetOfLocalHeader + (long)this._LengthOfHeader;
			this.ArchiveStream.Seek(position, 0);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000046FC File Offset: 0x000028FC
		internal static int GetLengthOfCryptoHeaderBytes(EncryptionAlgorithm a)
		{
			if (a == EncryptionAlgorithm.None)
			{
				return 0;
			}
			if (a == EncryptionAlgorithm.PkzipWeak)
			{
				return 12;
			}
			throw new ZipException("internal error");
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004714 File Offset: 0x00002914
		internal long FileDataPosition
		{
			get
			{
				if (this.__FileDataPosition == -1L)
				{
					this.SetFdpLoh();
				}
				return this.__FileDataPosition;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000472C File Offset: 0x0000292C
		private int LengthOfHeader
		{
			get
			{
				if (this._LengthOfHeader == 0)
				{
					this.SetFdpLoh();
				}
				return this._LengthOfHeader;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004742 File Offset: 0x00002942
		public void Extract()
		{
			this.InternalExtract(".", null, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004751 File Offset: 0x00002951
		public void Extract(ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, null);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004767 File Offset: 0x00002967
		public void Extract(Stream stream)
		{
			this.InternalExtract(null, stream, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004772 File Offset: 0x00002972
		public void Extract(string baseDirectory)
		{
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000477D File Offset: 0x0000297D
		public void Extract(string baseDirectory, ExtractExistingFileAction extractExistingFile)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000478F File Offset: 0x0000298F
		public void ExtractWithPassword(string password)
		{
			this.InternalExtract(".", null, password);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000479E File Offset: 0x0000299E
		public void ExtractWithPassword(string baseDirectory, string password)
		{
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000047A9 File Offset: 0x000029A9
		public void ExtractWithPassword(ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(".", null, password);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000047BF File Offset: 0x000029BF
		public void ExtractWithPassword(string baseDirectory, ExtractExistingFileAction extractExistingFile, string password)
		{
			this.ExtractExistingFile = extractExistingFile;
			this.InternalExtract(baseDirectory, null, password);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000047D1 File Offset: 0x000029D1
		public void ExtractWithPassword(Stream stream, string password)
		{
			this.InternalExtract(null, stream, password);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000047DC File Offset: 0x000029DC
		public CrcCalculatorStream OpenReader()
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(this._Password ?? this._container.Password);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004811 File Offset: 0x00002A11
		public CrcCalculatorStream OpenReader(string password)
		{
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use OpenReader() only with ZipFile.");
			}
			return this.InternalOpenReader(password);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004834 File Offset: 0x00002A34
		internal CrcCalculatorStream InternalOpenReader(string password)
		{
			this.ValidateCompression();
			this.ValidateEncryption();
			this.SetupCryptoForExtract(password);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling OpenReader");
			}
			long num = ((this._CompressionMethod_FromZipFile == 0) ? this._CompressedFileDataSize : this.UncompressedSize);
			Stream archiveStream = this.ArchiveStream;
			this.ArchiveStream.Seek(this.FileDataPosition, 0);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
			Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
			return new CrcCalculatorStream(extractDecompressor, num);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000048C5 File Offset: 0x00002AC5
		private void OnExtractProgress(long bytesWritten, long totalBytesToWrite)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractBlock(this, bytesWritten, totalBytesToWrite);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000048ED File Offset: 0x00002AED
		private void OnBeforeExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSingleEntryExtract(this, path, true);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004927 File Offset: 0x00002B27
		private void OnAfterExtract(string path)
		{
			if (this._container.ZipFile != null && !this._container.ZipFile._inExtractAll)
			{
				this._container.ZipFile.OnSingleEntryExtract(this, path, false);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000495C File Offset: 0x00002B5C
		private void OnExtractExisting(string path)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnExtractExisting(this, path);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004983 File Offset: 0x00002B83
		private static void ReallyDelete(string fileName)
		{
			if ((NetCfFile.GetAttributes(fileName) & 1U) == 1U)
			{
				NetCfFile.SetAttributes(fileName, 128U);
			}
			File.Delete(fileName);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000049A2 File Offset: 0x00002BA2
		private void WriteStatus(string format, params object[] args)
		{
			if (this._container.ZipFile != null && this._container.ZipFile.Verbose)
			{
				this._container.ZipFile.StatusMessageTextWriter.WriteLine(format, args);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000049DC File Offset: 0x00002BDC
		private void InternalExtract(string baseDir, Stream outstream, string password)
		{
			if (this._container == null)
			{
				throw new BadStateException("This entry is an orphan");
			}
			if (this._container.ZipFile == null)
			{
				throw new InvalidOperationException("Use Extract() only with ZipFile.");
			}
			this._container.ZipFile.Reset(false);
			if (this._Source != ZipEntrySource.ZipFile)
			{
				throw new BadStateException("You must call ZipFile.Save before calling any Extract method");
			}
			this.OnBeforeExtract(baseDir);
			this._ioOperationCanceled = false;
			string text = null;
			Stream stream = null;
			bool flag = false;
			bool flag2 = false;
			try
			{
				this.ValidateCompression();
				this.ValidateEncryption();
				if (this.ValidateOutput(baseDir, outstream, out text))
				{
					this.WriteStatus("extract dir {0}...", new object[] { text });
					this.OnAfterExtract(baseDir);
				}
				else
				{
					if (text != null && File.Exists(text))
					{
						flag = true;
						int num = this.CheckExtractExistingFile(baseDir, text);
						if (num == 2)
						{
							goto IL_028F;
						}
						if (num == 1)
						{
							return;
						}
					}
					string text2 = password ?? this._Password ?? this._container.Password;
					if (this._Encryption_FromZipFile != EncryptionAlgorithm.None)
					{
						if (text2 == null)
						{
							throw new BadPasswordException();
						}
						this.SetupCryptoForExtract(text2);
					}
					if (text != null)
					{
						this.WriteStatus("extract file {0}...", new object[] { text });
						text += ".tmp";
						string directoryName = Path.GetDirectoryName(text);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						else if (this._container.ZipFile != null)
						{
							flag2 = this._container.ZipFile._inExtractAll;
						}
						stream = new FileStream(text, 1);
					}
					else
					{
						this.WriteStatus("extract entry {0} to stream...", new object[] { this.FileName });
						stream = outstream;
					}
					if (!this._ioOperationCanceled)
					{
						int num2 = this.ExtractOne(stream);
						if (!this._ioOperationCanceled)
						{
							this.VerifyCrcAfterExtract(num2);
							if (text != null)
							{
								stream.Close();
								stream = null;
								string text3 = text;
								string text4 = null;
								text = text3.Substring(0, text3.Length - 4);
								if (flag)
								{
									text4 = text + ".PendingOverwrite";
									File.Move(text, text4);
								}
								File.Move(text3, text);
								this._SetTimes(text, true);
								if (text4 != null && File.Exists(text4))
								{
									ZipEntry.ReallyDelete(text4);
								}
								if (flag2 && this.FileName.IndexOf('/') != -1)
								{
									string directoryName2 = Path.GetDirectoryName(this.FileName);
									if (this._container.ZipFile[directoryName2] == null)
									{
										this._SetTimes(Path.GetDirectoryName(text), false);
									}
								}
								if (((int)this._VersionMadeBy & 65280) == 2560 || ((int)this._VersionMadeBy & 65280) == 0)
								{
									NetCfFile.SetAttributes(text, (uint)this._ExternalFileAttrs);
								}
							}
							this.OnAfterExtract(baseDir);
						}
					}
					IL_028F:;
				}
			}
			catch (Exception)
			{
				this._ioOperationCanceled = true;
				throw;
			}
			finally
			{
				if (this._ioOperationCanceled && text != null)
				{
					if (stream != null)
					{
						stream.Close();
					}
					if (File.Exists(text) && !flag)
					{
						File.Delete(text);
					}
				}
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004CE0 File Offset: 0x00002EE0
		internal void VerifyCrcAfterExtract(int actualCrc32)
		{
			if (actualCrc32 != this._Crc32)
			{
				throw new BadCrcException("CRC error: the file being extracted appears to be corrupted. " + string.Format("Expected 0x{0:X8}, Actual 0x{1:X8}", this._Crc32, actualCrc32));
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D18 File Offset: 0x00002F18
		private int CheckExtractExistingFile(string baseDir, string targetFileName)
		{
			int num = 0;
			for (;;)
			{
				switch (this.ExtractExistingFile)
				{
				case ExtractExistingFileAction.OverwriteSilently:
					goto IL_0021;
				case ExtractExistingFileAction.DoNotOverwrite:
					goto IL_003A;
				case ExtractExistingFileAction.InvokeExtractProgressEvent:
					if (num > 0)
					{
						goto Block_2;
					}
					this.OnExtractExisting(baseDir);
					if (this._ioOperationCanceled)
					{
						return 2;
					}
					num++;
					continue;
				}
				break;
			}
			goto IL_0085;
			IL_0021:
			this.WriteStatus("the file {0} exists; will overwrite it...", new object[] { targetFileName });
			return 0;
			IL_003A:
			this.WriteStatus("the file {0} exists; not extracting entry...", new object[] { this.FileName });
			this.OnAfterExtract(baseDir);
			return 1;
			Block_2:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
			IL_0085:
			throw new ZipException(string.Format("The file {0} already exists.", targetFileName));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004DC3 File Offset: 0x00002FC3
		private void _CheckRead(int nbytes)
		{
			if (nbytes == 0)
			{
				throw new BadReadException(string.Format("bad read of entry {0} from compressed archive.", this.FileName));
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private int ExtractOne(Stream output)
		{
			int num = 0;
			Stream archiveStream = this.ArchiveStream;
			try
			{
				archiveStream.Seek(this.FileDataPosition, 0);
				SharedUtilities.Workaround_Ladybug318918(archiveStream);
				byte[] array = new byte[this.BufferSize];
				long num2 = ((this._CompressionMethod_FromZipFile != 0) ? this.UncompressedSize : this._CompressedFileDataSize);
				this._inputDecryptorStream = this.GetExtractDecryptor(archiveStream);
				Stream extractDecompressor = this.GetExtractDecompressor(this._inputDecryptorStream);
				long num3 = 0L;
				using (CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(extractDecompressor))
				{
					while (num2 > 0L)
					{
						int num4 = ((num2 > (long)array.Length) ? array.Length : ((int)num2));
						int num5 = crcCalculatorStream.Read(array, 0, num4);
						this._CheckRead(num5);
						output.Write(array, 0, num5);
						num2 -= (long)num5;
						num3 += (long)num5;
						this.OnExtractProgress(num3, this.UncompressedSize);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
					num = crcCalculatorStream.Crc;
				}
			}
			finally
			{
				ZipSegmentedStream zipSegmentedStream = archiveStream as ZipSegmentedStream;
				if (zipSegmentedStream != null)
				{
					zipSegmentedStream.Close();
					this._archiveStream = null;
				}
			}
			return num;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004F00 File Offset: 0x00003100
		internal Stream GetExtractDecompressor(Stream input2)
		{
			short compressionMethod_FromZipFile = this._CompressionMethod_FromZipFile;
			if (compressionMethod_FromZipFile == 0)
			{
				return input2;
			}
			if (compressionMethod_FromZipFile == 8)
			{
				return new DeflateStream(input2, CompressionMode.Decompress, true);
			}
			if (compressionMethod_FromZipFile != 12)
			{
				return null;
			}
			return new BZip2InputStream(input2, true);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004F38 File Offset: 0x00003138
		internal Stream GetExtractDecryptor(Stream input)
		{
			Stream stream;
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				stream = new ZipCipherStream(input, this._zipCrypto_forExtract, CryptoMode.Decrypt);
			}
			else
			{
				stream = input;
			}
			return stream;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004F64 File Offset: 0x00003164
		internal void _SetTimes(string fileOrDirectory, bool isFile)
		{
			try
			{
				if (this._ntfsTimesAreSet)
				{
					int num = NetCfFile.SetTimes(fileOrDirectory, this._Ctime, this._Atime, this._Mtime);
					if (num != 0)
					{
						this.WriteStatus("Warning: SetTimes failed.  entry({0})  file({1})  rc({2})", new object[] { this.FileName, fileOrDirectory, num });
					}
				}
				else
				{
					DateTime dateTime = SharedUtilities.AdjustTime_Reverse(this.LastModified);
					int num2 = NetCfFile.SetLastWriteTime(fileOrDirectory, dateTime);
					if (num2 != 0)
					{
						this.WriteStatus("Warning: SetLastWriteTime failed.  entry({0})  file({1})  rc({2})", new object[] { this.FileName, fileOrDirectory, num2 });
					}
				}
			}
			catch (IOException ex)
			{
				this.WriteStatus("failed to set time on {0}: {1}", new object[] { fileOrDirectory, ex.Message });
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005044 File Offset: 0x00003244
		private string UnsupportedAlgorithm
		{
			get
			{
				string empty = string.Empty;
				uint unsupportedAlgorithmId = this._UnsupportedAlgorithmId;
				if (unsupportedAlgorithmId <= 26128U)
				{
					if (unsupportedAlgorithmId <= 26115U)
					{
						if (unsupportedAlgorithmId == 0U)
						{
							return "--";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26113U:
							return "DES";
						case 26114U:
							return "RC2";
						case 26115U:
							return "3DES-168";
						}
					}
					else
					{
						if (unsupportedAlgorithmId == 26121U)
						{
							return "3DES-112";
						}
						switch (unsupportedAlgorithmId)
						{
						case 26126U:
							return "PKWare AES128";
						case 26127U:
							return "PKWare AES192";
						case 26128U:
							return "PKWare AES256";
						}
					}
				}
				else if (unsupportedAlgorithmId <= 26401U)
				{
					if (unsupportedAlgorithmId == 26370U)
					{
						return "RC2";
					}
					switch (unsupportedAlgorithmId)
					{
					case 26400U:
						return "Blowfish";
					case 26401U:
						return "Twofish";
					}
				}
				else
				{
					if (unsupportedAlgorithmId == 26625U)
					{
						return "RC4";
					}
					if (unsupportedAlgorithmId != 65535U)
					{
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._UnsupportedAlgorithmId);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005164 File Offset: 0x00003364
		private string UnsupportedCompressionMethod
		{
			get
			{
				string empty = string.Empty;
				int compressionMethod = (int)this._CompressionMethod;
				if (compressionMethod <= 14)
				{
					switch (compressionMethod)
					{
					case 0:
						return "Store";
					case 1:
						return "Shrink";
					default:
						switch (compressionMethod)
						{
						case 8:
							return "DEFLATE";
						case 9:
							return "Deflate64";
						case 12:
							return "BZIP2";
						case 14:
							return "LZMA";
						}
						break;
					}
				}
				else
				{
					if (compressionMethod == 19)
					{
						return "LZ77";
					}
					if (compressionMethod == 98)
					{
						return "PPMd";
					}
				}
				return string.Format("Unknown (0x{0:X4})", this._CompressionMethod);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000521C File Offset: 0x0000341C
		internal void ValidateEncryption()
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak || this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._UnsupportedAlgorithmId != 0U)
			{
				throw new ZipException(string.Format("Cannot extract: Entry {0} is encrypted with an algorithm not supported by DotNetZip: {1}", this.FileName, this.UnsupportedAlgorithm));
			}
			throw new ZipException(string.Format("Cannot extract: Entry {0} uses an unsupported encryption algorithm ({1:X2})", this.FileName, (int)this.Encryption));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005280 File Offset: 0x00003480
		private void ValidateCompression()
		{
			if (this._CompressionMethod_FromZipFile != 0 && this._CompressionMethod_FromZipFile != 8 && this._CompressionMethod_FromZipFile != 12)
			{
				throw new ZipException(string.Format("Entry {0} uses an unsupported compression method (0x{1:X2}, {2})", this.FileName, this._CompressionMethod_FromZipFile, this.UnsupportedCompressionMethod));
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000052D0 File Offset: 0x000034D0
		private void SetupCryptoForExtract(string password)
		{
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.None)
			{
				return;
			}
			if (this._Encryption_FromZipFile == EncryptionAlgorithm.PkzipWeak)
			{
				if (password == null)
				{
					throw new ZipException("Missing password.");
				}
				this.ArchiveStream.Seek(this.FileDataPosition - 12L, 0);
				SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
				this._zipCrypto_forExtract = ZipCrypto.ForRead(password, this);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000532C File Offset: 0x0000352C
		private bool ValidateOutput(string basedir, Stream outstream, out string outFileName)
		{
			if (basedir != null)
			{
				string text = this.FileName.Replace("\\", "/");
				if (text.IndexOf(':') == 1)
				{
					text = text.Substring(2);
				}
				if (text.StartsWith("/"))
				{
					text = text.Substring(1);
				}
				if (this._container.ZipFile.FlattenFoldersOnExtract)
				{
					outFileName = Path.Combine(basedir, (text.IndexOf('/') != -1) ? Path.GetFileName(text) : text);
				}
				else
				{
					outFileName = Path.Combine(basedir, text);
				}
				outFileName = outFileName.Replace("/", "\\");
				if (this.IsDirectory || this.FileName.EndsWith("/"))
				{
					if (!Directory.Exists(outFileName))
					{
						Directory.CreateDirectory(outFileName);
						this._SetTimes(outFileName, false);
					}
					else if (this.ExtractExistingFile == ExtractExistingFileAction.OverwriteSilently)
					{
						this._SetTimes(outFileName, false);
					}
					return true;
				}
				return false;
			}
			else
			{
				if (outstream != null)
				{
					outFileName = null;
					return this.IsDirectory || this.FileName.EndsWith("/");
				}
				throw new ArgumentNullException("outstream");
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005444 File Offset: 0x00003644
		private void ReadExtraField()
		{
			this._readExtraDepth++;
			long position = this.ArchiveStream.Position;
			this.ArchiveStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			byte[] array = new byte[30];
			this.ArchiveStream.Read(array, 0, array.Length);
			int num = 26;
			short num2 = (short)((int)array[num++] + (int)array[num++] * 256);
			short num3 = (short)((int)array[num++] + (int)array[num++] * 256);
			this.ArchiveStream.Seek((long)num2, 1);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			this.ProcessExtraField(this.ArchiveStream, num3);
			this.ArchiveStream.Seek(position, 0);
			SharedUtilities.Workaround_Ladybug318918(this.ArchiveStream);
			this._readExtraDepth--;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005524 File Offset: 0x00003724
		private static bool ReadHeader(ZipEntry ze, Encoding defaultEncoding)
		{
			int num = 0;
			ze._RelativeOffsetOfLocalHeader = ze.ArchiveStream.Position;
			int num2 = SharedUtilities.ReadEntrySignature(ze.ArchiveStream);
			num += 4;
			if (ZipEntry.IsNotValidSig(num2))
			{
				ze.ArchiveStream.Seek(-4L, 1);
				SharedUtilities.Workaround_Ladybug318918(ze.ArchiveStream);
				if (ZipEntry.IsNotValidZipDirEntrySig(num2) && (long)num2 != 101010256L)
				{
					throw new BadReadException(string.Format("  Bad signature (0x{0:X8}) at position  0x{1:X8}", num2, ze.ArchiveStream.Position));
				}
				return false;
			}
			else
			{
				byte[] array = new byte[26];
				int num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				if (num3 != array.Length)
				{
					return false;
				}
				num += num3;
				int num4 = 0;
				ze._VersionNeeded = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._BitField = (short)((int)array[num4++] + (int)array[num4++] * 256);
				ze._CompressionMethod_FromZipFile = (ze._CompressionMethod = (short)((int)array[num4++] + (int)array[num4++] * 256));
				ze._TimeBlob = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._LastModified = SharedUtilities.PackedToDateTime(ze._TimeBlob);
				ze._timestamp |= ZipEntryTimestamp.DOS;
				if ((ze._BitField & 1) == 1)
				{
					ze._Encryption_FromZipFile = (ze._Encryption = EncryptionAlgorithm.PkzipWeak);
					ze._sourceIsEncrypted = true;
				}
				ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
				ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
				if ((uint)ze._CompressedSize == 4294967295U || (uint)ze._UncompressedSize == 4294967295U)
				{
					ze._InputUsesZip64 = true;
				}
				short num5 = (short)((int)array[num4++] + (int)array[num4++] * 256);
				short num6 = (short)((int)array[num4++] + (int)array[num4++] * 256);
				array = new byte[(int)num5];
				num3 = ze.ArchiveStream.Read(array, 0, array.Length);
				num += num3;
				if ((ze._BitField & 2048) == 2048)
				{
					ze.AlternateEncoding = Encoding.UTF8;
					ze.AlternateEncodingUsage = ZipOption.Always;
				}
				ze._FileNameInArchive = ze.AlternateEncoding.GetString(array, 0, array.Length);
				if (ze._FileNameInArchive.EndsWith("/"))
				{
					ze.MarkAsDirectory();
				}
				num += ze.ProcessExtraField(ze.ArchiveStream, num6);
				ze._LengthOfTrailer = 0;
				if (!ze._FileNameInArchive.EndsWith("/") && (ze._BitField & 8) == 8)
				{
					long position = ze.ArchiveStream.Position;
					bool flag = true;
					long num7 = 0L;
					int num8 = 0;
					while (flag)
					{
						num8++;
						if (ze._container.ZipFile != null)
						{
							ze._container.ZipFile.OnReadBytes(ze);
						}
						long num9 = SharedUtilities.FindSignature(ze.ArchiveStream, 134695760);
						if (num9 == -1L)
						{
							return false;
						}
						num7 += num9;
						if (ze._InputUsesZip64)
						{
							array = new byte[20];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 20)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._UncompressedSize = BitConverter.ToInt64(array, num4);
							num4 += 8;
							ze._LengthOfTrailer += 24;
						}
						else
						{
							array = new byte[12];
							num3 = ze.ArchiveStream.Read(array, 0, array.Length);
							if (num3 != 12)
							{
								return false;
							}
							num4 = 0;
							ze._Crc32 = (int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256;
							ze._CompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._UncompressedSize = (long)((ulong)((int)array[num4++] + (int)array[num4++] * 256 + (int)array[num4++] * 256 * 256 + (int)array[num4++] * 256 * 256 * 256));
							ze._LengthOfTrailer += 16;
						}
						flag = num7 != ze._CompressedSize;
						if (flag)
						{
							ze.ArchiveStream.Seek(-12L, 1);
							SharedUtilities.Workaround_Ladybug318918(ze.ArchiveStream);
							num7 += 4L;
						}
					}
					ze.ArchiveStream.Seek(position, 0);
					SharedUtilities.Workaround_Ladybug318918(ze.ArchiveStream);
				}
				ze._CompressedFileDataSize = ze._CompressedSize;
				if ((ze._BitField & 1) == 1)
				{
					ze._WeakEncryptionHeader = new byte[12];
					num += ZipEntry.ReadWeakEncryptionHeader(ze._archiveStream, ze._WeakEncryptionHeader);
					ze._CompressedFileDataSize -= 12L;
				}
				ze._LengthOfHeader = num;
				ze._TotalEntrySize = (long)ze._LengthOfHeader + ze._CompressedFileDataSize + (long)ze._LengthOfTrailer;
				return true;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005BD0 File Offset: 0x00003DD0
		internal static int ReadWeakEncryptionHeader(Stream s, byte[] buffer)
		{
			int num = s.Read(buffer, 0, 12);
			if (num != 12)
			{
				throw new ZipException(string.Format("Unexpected end of data at position 0x{0:X8}", s.Position));
			}
			return num;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005C09 File Offset: 0x00003E09
		private static bool IsNotValidSig(int signature)
		{
			return signature != 67324752;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C18 File Offset: 0x00003E18
		internal static ZipEntry ReadEntry(ZipContainer zc, bool first)
		{
			ZipFile zipFile = zc.ZipFile;
			Stream readStream = zc.ReadStream;
			Encoding alternateEncoding = zc.AlternateEncoding;
			ZipEntry zipEntry = new ZipEntry();
			zipEntry._Source = ZipEntrySource.ZipFile;
			zipEntry._container = zc;
			zipEntry._archiveStream = readStream;
			if (zipFile != null)
			{
				zipFile.OnReadEntry(true, null);
			}
			if (first)
			{
				ZipEntry.HandlePK00Prefix(readStream);
			}
			if (!ZipEntry.ReadHeader(zipEntry, alternateEncoding))
			{
				return null;
			}
			zipEntry.__FileDataPosition = zipEntry.ArchiveStream.Position;
			readStream.Seek(zipEntry._CompressedFileDataSize + (long)zipEntry._LengthOfTrailer, 1);
			SharedUtilities.Workaround_Ladybug318918(readStream);
			ZipEntry.HandleUnexpectedDataDescriptor(zipEntry);
			if (zipFile != null)
			{
				zipFile.OnReadBytes(zipEntry);
				zipFile.OnReadEntry(false, zipEntry);
			}
			return zipEntry;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005CBC File Offset: 0x00003EBC
		internal static void HandlePK00Prefix(Stream s)
		{
			uint num = (uint)SharedUtilities.ReadInt(s);
			if (num != 808471376U)
			{
				s.Seek(-4L, 1);
				SharedUtilities.Workaround_Ladybug318918(s);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005CEC File Offset: 0x00003EEC
		private static void HandleUnexpectedDataDescriptor(ZipEntry entry)
		{
			Stream archiveStream = entry.ArchiveStream;
			uint num = (uint)SharedUtilities.ReadInt(archiveStream);
			if ((ulong)num != (ulong)((long)entry._Crc32))
			{
				archiveStream.Seek(-4L, 1);
				SharedUtilities.Workaround_Ladybug318918(archiveStream);
				return;
			}
			int num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 != entry._CompressedSize)
			{
				archiveStream.Seek(-8L, 1);
				SharedUtilities.Workaround_Ladybug318918(archiveStream);
				return;
			}
			num2 = SharedUtilities.ReadInt(archiveStream);
			if ((long)num2 == entry._UncompressedSize)
			{
				return;
			}
			archiveStream.Seek(-12L, 1);
			SharedUtilities.Workaround_Ladybug318918(archiveStream);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005D6C File Offset: 0x00003F6C
		internal static int FindExtraFieldSegment(byte[] extra, int offx, ushort targetHeaderId)
		{
			int num = offx;
			while (num + 3 < extra.Length)
			{
				ushort num2 = (ushort)((int)extra[num++] + (int)extra[num++] * 256);
				if (num2 == targetHeaderId)
				{
					return num - 2;
				}
				short num3 = (short)((int)extra[num++] + (int)extra[num++] * 256);
				num += (int)num3;
			}
			return -1;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005DC0 File Offset: 0x00003FC0
		internal int ProcessExtraField(Stream s, short extraFieldLength)
		{
			int num = 0;
			if (extraFieldLength > 0)
			{
				byte[] array = (this._Extra = new byte[(int)extraFieldLength]);
				num = s.Read(array, 0, array.Length);
				long num2 = s.Position - (long)num;
				int num3 = 0;
				while (num3 + 3 < array.Length)
				{
					int num4 = num3;
					ushort num5 = (ushort)((int)array[num3++] + (int)array[num3++] * 256);
					short num6 = (short)((int)array[num3++] + (int)array[num3++] * 256);
					ushort num7 = num5;
					if (num7 <= 23)
					{
						if (num7 != 1)
						{
							if (num7 != 10)
							{
								if (num7 == 23)
								{
									num3 = this.ProcessExtraFieldPkwareStrongEncryption(array, num3);
								}
							}
							else
							{
								num3 = this.ProcessExtraFieldWindowsTimes(array, num3, num6, num2);
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldZip64(array, num3, num6, num2);
						}
					}
					else if (num7 <= 22613)
					{
						if (num7 != 21589)
						{
							if (num7 == 22613)
							{
								num3 = this.ProcessExtraFieldInfoZipTimes(array, num3, num6, num2);
							}
						}
						else
						{
							num3 = this.ProcessExtraFieldUnixTimes(array, num3, num6, num2);
						}
					}
					else if (num7 != 30805 && num7 != 30837)
					{
					}
					num3 = num4 + (int)num6 + 4;
				}
			}
			return num;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005EDC File Offset: 0x000040DC
		private int ProcessExtraFieldPkwareStrongEncryption(byte[] Buffer, int j)
		{
			j += 2;
			this._UnsupportedAlgorithmId = (uint)((ushort)((int)Buffer[j++] + (int)Buffer[j++] * 256));
			this._Encryption_FromZipFile = (this._Encryption = EncryptionAlgorithm.Unsupported);
			return j;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F88 File Offset: 0x00004188
		private int ProcessExtraFieldZip64(byte[] buffer, int j, short dataSize, long posn)
		{
			ZipEntry.<>c__DisplayClass1 <>c__DisplayClass = new ZipEntry.<>c__DisplayClass1();
			<>c__DisplayClass.buffer = buffer;
			<>c__DisplayClass.j = j;
			<>c__DisplayClass.posn = posn;
			this._InputUsesZip64 = true;
			if (dataSize > 28)
			{
				throw new BadReadException(string.Format("  Inconsistent size (0x{0:X4}) for ZIP64 extra field at position 0x{1:X16}", dataSize, <>c__DisplayClass.posn));
			}
			<>c__DisplayClass.remainingData = (int)dataSize;
			ZipEntry.Func<long> func = new ZipEntry.Func<long>(<>c__DisplayClass.<ProcessExtraFieldZip64>b__0);
			if (this._UncompressedSize == (long)((ulong)(-1)))
			{
				this._UncompressedSize = func();
			}
			if (this._CompressedSize == (long)((ulong)(-1)))
			{
				this._CompressedSize = func();
			}
			if (this._RelativeOffsetOfLocalHeader == (long)((ulong)(-1)))
			{
				this._RelativeOffsetOfLocalHeader = func();
			}
			return <>c__DisplayClass.j;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000603C File Offset: 0x0000423C
		private int ProcessExtraFieldInfoZipTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 12 && dataSize != 8)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for InfoZip v1 extra field at position 0x{1:X16}", dataSize, posn));
			}
			int num = BitConverter.ToInt32(buffer, j);
			this._Mtime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			num = BitConverter.ToInt32(buffer, j);
			this._Atime = ZipEntry._unixEpoch.AddSeconds((double)num);
			j += 4;
			this._Ctime = DateTime.UtcNow;
			this._ntfsTimesAreSet = true;
			this._timestamp |= ZipEntryTimestamp.InfoZip1;
			return j;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006120 File Offset: 0x00004320
		private int ProcessExtraFieldUnixTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			ZipEntry.<>c__DisplayClass4 <>c__DisplayClass = new ZipEntry.<>c__DisplayClass4();
			<>c__DisplayClass.buffer = buffer;
			<>c__DisplayClass.j = j;
			<>c__DisplayClass.<>4__this = this;
			if (dataSize != 13 && dataSize != 9 && dataSize != 5)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for Extended Timestamp extra field at position 0x{1:X16}", dataSize, posn));
			}
			<>c__DisplayClass.remainingData = (int)dataSize;
			ZipEntry.Func<DateTime> func = new ZipEntry.Func<DateTime>(<>c__DisplayClass.<ProcessExtraFieldUnixTimes>b__3);
			if (dataSize == 13 || this._readExtraDepth > 0)
			{
				byte b = <>c__DisplayClass.buffer[<>c__DisplayClass.j++];
				<>c__DisplayClass.remainingData--;
				if ((b & 1) != 0 && <>c__DisplayClass.remainingData >= 4)
				{
					this._Mtime = func();
				}
				this._Atime = (((b & 2) != 0 && <>c__DisplayClass.remainingData >= 4) ? func() : DateTime.UtcNow);
				this._Ctime = (((b & 4) != 0 && <>c__DisplayClass.remainingData >= 4) ? func() : DateTime.UtcNow);
				this._timestamp |= ZipEntryTimestamp.Unix;
				this._ntfsTimesAreSet = true;
				this._emitUnixTimes = true;
			}
			else
			{
				this.ReadExtraField();
			}
			return <>c__DisplayClass.j;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006248 File Offset: 0x00004448
		private int ProcessExtraFieldWindowsTimes(byte[] buffer, int j, short dataSize, long posn)
		{
			if (dataSize != 32)
			{
				throw new BadReadException(string.Format("  Unexpected size (0x{0:X4}) for NTFS times extra field at position 0x{1:X16}", dataSize, posn));
			}
			j += 4;
			short num = (short)((int)buffer[j] + (int)buffer[j + 1] * 256);
			short num2 = (short)((int)buffer[j + 2] + (int)buffer[j + 3] * 256);
			j += 4;
			if (num == 1 && num2 == 24)
			{
				long num3 = BitConverter.ToInt64(buffer, j);
				this._Mtime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Atime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				num3 = BitConverter.ToInt64(buffer, j);
				this._Ctime = DateTime.FromFileTimeUtc(num3);
				j += 8;
				this._ntfsTimesAreSet = true;
				this._timestamp |= ZipEntryTimestamp.Windows;
				this._emitNtfsTimes = true;
			}
			return j;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006318 File Offset: 0x00004518
		internal void WriteCentralDirectoryEntry(Stream s)
		{
			byte[] array = new byte[4096];
			int num = 0;
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 1;
			array[num++] = 2;
			array[num++] = (byte)(this._VersionMadeBy & 255);
			array[num++] = (byte)(((int)this._VersionMadeBy & 65280) >> 8);
			short num2 = ((this.VersionNeeded != 0) ? this.VersionNeeded : 20);
			if (this._OutputUsesZip64 == null)
			{
				this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always);
			}
			short num3 = (this._OutputUsesZip64.Value ? 45 : num2);
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num3 = 46;
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			if (this._OutputUsesZip64.Value)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num4 = (short)encodedFileNameBytes.Length;
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			this._presumeZip64 = this._OutputUsesZip64.Value;
			this._Extra = this.ConstructExtraField(true);
			short num5 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num5 & 255);
			array[num++] = (byte)(((int)num5 & 65280) >> 8);
			int num6 = ((this._CommentBytes == null) ? 0 : this._CommentBytes.Length);
			if (num6 + num > array.Length)
			{
				num6 = array.Length - num;
			}
			array[num++] = (byte)(num6 & 255);
			array[num++] = (byte)((num6 & 65280) >> 8);
			bool flag = this._container.ZipFile != null && this._container.ZipFile.MaxOutputSegmentSize != 0;
			if (flag)
			{
				array[num++] = (byte)(this._diskNumber & 255U);
				array[num++] = (byte)((this._diskNumber & 65280U) >> 8);
			}
			else
			{
				array[num++] = 0;
				array[num++] = 0;
			}
			array[num++] = (this._IsText ? 1 : 0);
			array[num++] = 0;
			array[num++] = (byte)(this._ExternalFileAttrs & 255);
			array[num++] = (byte)((this._ExternalFileAttrs & 65280) >> 8);
			array[num++] = (byte)((this._ExternalFileAttrs & 16711680) >> 16);
			array[num++] = (byte)(((long)this._ExternalFileAttrs & (long)((ulong)(-16777216))) >> 24);
			if (this._RelativeOffsetOfLocalHeader > (long)((ulong)(-1)))
			{
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
				array[num++] = byte.MaxValue;
			}
			else
			{
				array[num++] = (byte)(this._RelativeOffsetOfLocalHeader & 255L);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 65280L) >> 8);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & 16711680L) >> 16);
				array[num++] = (byte)((this._RelativeOffsetOfLocalHeader & (long)((ulong)(-16777216))) >> 24);
			}
			Buffer.BlockCopy(encodedFileNameBytes, 0, array, num, (int)num4);
			num += (int)num4;
			if (this._Extra != null)
			{
				byte[] extra = this._Extra;
				int num7 = 0;
				Buffer.BlockCopy(extra, num7, array, num, (int)num5);
				num += (int)num5;
			}
			if (num6 != 0)
			{
				Buffer.BlockCopy(this._CommentBytes, 0, array, num, num6);
				num += num6;
			}
			s.Write(array, 0, num);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006888 File Offset: 0x00004A88
		private byte[] ConstructExtraField(bool forCentralDirectory)
		{
			List<byte[]> list = new List<byte[]>();
			if (this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && (!forCentralDirectory || this._entryRequiresZip64.Value)))
			{
				int num = 4 + (forCentralDirectory ? 28 : 16);
				byte[] array = new byte[num];
				int num2 = 0;
				if (this._presumeZip64 || forCentralDirectory)
				{
					array[num2++] = 1;
					array[num2++] = 0;
				}
				else
				{
					array[num2++] = 153;
					array[num2++] = 153;
				}
				array[num2++] = (byte)(num - 4);
				array[num2++] = 0;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num2, 8);
				num2 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num2, 8);
				num2 += 8;
				if (forCentralDirectory)
				{
					Array.Copy(BitConverter.GetBytes(this._RelativeOffsetOfLocalHeader), 0, array, num2, 8);
					num2 += 8;
					Array.Copy(BitConverter.GetBytes(0), 0, array, num2, 4);
				}
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitNtfsTimes)
			{
				byte[] array = new byte[36];
				int num3 = 0;
				array[num3++] = 10;
				array[num3++] = 0;
				array[num3++] = 32;
				array[num3++] = 0;
				num3 += 4;
				array[num3++] = 1;
				array[num3++] = 0;
				array[num3++] = 24;
				array[num3++] = 0;
				long num4 = this._Mtime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				num4 = this._Atime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				num4 = this._Ctime.ToFileTime();
				Array.Copy(BitConverter.GetBytes(num4), 0, array, num3, 8);
				num3 += 8;
				list.Add(array);
			}
			if (this._ntfsTimesAreSet && this._emitUnixTimes)
			{
				int num5 = 9;
				if (!forCentralDirectory)
				{
					num5 += 8;
				}
				byte[] array = new byte[num5];
				int num6 = 0;
				array[num6++] = 85;
				array[num6++] = 84;
				array[num6++] = (byte)(num5 - 4);
				array[num6++] = 0;
				array[num6++] = 7;
				int num7 = (int)(this._Mtime - ZipEntry._unixEpoch).TotalSeconds;
				Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
				num6 += 4;
				if (!forCentralDirectory)
				{
					num7 = (int)(this._Atime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
					num6 += 4;
					num7 = (int)(this._Ctime - ZipEntry._unixEpoch).TotalSeconds;
					Array.Copy(BitConverter.GetBytes(num7), 0, array, num6, 4);
					num6 += 4;
				}
				list.Add(array);
			}
			byte[] array2 = null;
			if (list.Count > 0)
			{
				int num8 = 0;
				int num9 = 0;
				for (int i = 0; i < list.Count; i++)
				{
					num8 += list[i].Length;
				}
				array2 = new byte[num8];
				for (int i = 0; i < list.Count; i++)
				{
					Array.Copy(list[i], 0, array2, num9, list[i].Length);
					num9 += list[i].Length;
				}
			}
			return array2;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006C04 File Offset: 0x00004E04
		private string NormalizeFileName()
		{
			string text = this.FileName.Replace("\\", "/");
			string text2;
			if (this._TrimVolumeFromFullyQualifiedPaths && this.FileName.Length >= 3 && this.FileName.get_Chars(1) == ':' && text.get_Chars(2) == '/')
			{
				text2 = text.Substring(3);
			}
			else if (this.FileName.Length >= 4 && text.get_Chars(0) == '/' && text.get_Chars(1) == '/')
			{
				int num = text.IndexOf('/', 2);
				if (num == -1)
				{
					throw new ArgumentException("The path for that entry appears to be badly formatted");
				}
				text2 = text.Substring(num + 1);
			}
			else if (this.FileName.Length >= 3 && text.get_Chars(0) == '.' && text.get_Chars(1) == '/')
			{
				text2 = text.Substring(2);
			}
			else
			{
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006CE0 File Offset: 0x00004EE0
		private byte[] GetEncodedFileNameBytes()
		{
			string text = this.NormalizeFileName();
			switch (this.AlternateEncodingUsage)
			{
			case ZipOption.Default:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = ZipEntry.ibm437.GetBytes(this._Comment);
				}
				this._actualEncoding = ZipEntry.ibm437;
				return ZipEntry.ibm437.GetBytes(text);
			case ZipOption.Always:
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return this.AlternateEncoding.GetBytes(text);
			}
			byte[] array = ZipEntry.ibm437.GetBytes(text);
			string @string = ZipEntry.ibm437.GetString(array, 0, array.Length);
			this._CommentBytes = null;
			if (@string != text)
			{
				array = this.AlternateEncoding.GetBytes(text);
				if (this._Comment != null && this._Comment.Length != 0)
				{
					this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				}
				this._actualEncoding = this.AlternateEncoding;
				return array;
			}
			this._actualEncoding = ZipEntry.ibm437;
			if (this._Comment == null || this._Comment.Length == 0)
			{
				return array;
			}
			byte[] bytes = ZipEntry.ibm437.GetBytes(this._Comment);
			string string2 = ZipEntry.ibm437.GetString(bytes, 0, bytes.Length);
			if (string2 != this.Comment)
			{
				array = this.AlternateEncoding.GetBytes(text);
				this._CommentBytes = this.AlternateEncoding.GetBytes(this._Comment);
				this._actualEncoding = this.AlternateEncoding;
				return array;
			}
			this._CommentBytes = bytes;
			return array;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006E9C File Offset: 0x0000509C
		private bool WantReadAgain()
		{
			return this._UncompressedSize >= 16L && this._CompressionMethod != 0 && this.CompressionLevel != CompressionLevel.None && this._CompressedSize >= this._UncompressedSize && (this._Source != ZipEntrySource.Stream || this._sourceStream.CanSeek) && (this._zipCrypto_forWrite == null || this.CompressedSize - 12L > this.UncompressedSize);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006F10 File Offset: 0x00005110
		private void MaybeUnsetCompressionMethodForWriting(int cycle)
		{
			if (cycle > 1)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.IsDirectory)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				return;
			}
			if (this._Source == ZipEntrySource.Stream)
			{
				if (this._sourceStream != null && this._sourceStream.CanSeek)
				{
					long length = this._sourceStream.Length;
					if (length == 0L)
					{
						this._CompressionMethod = 0;
						return;
					}
				}
			}
			else if (this._Source == ZipEntrySource.FileSystem && SharedUtilities.GetFileLength(this.LocalFileName) == 0L)
			{
				this._CompressionMethod = 0;
				return;
			}
			if (this.SetCompression != null)
			{
				this.CompressionLevel = this.SetCompression(this.LocalFileName, this._FileNameInArchive);
			}
			if (this.CompressionLevel == CompressionLevel.None && this.CompressionMethod == CompressionMethod.Deflate)
			{
				this._CompressionMethod = 0;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006FD8 File Offset: 0x000051D8
		internal void WriteHeader(Stream s, int cycle)
		{
			CountingStream countingStream = s as CountingStream;
			this._future_ROLH = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
			int num = 0;
			byte[] array = new byte[30];
			array[num++] = 80;
			array[num++] = 75;
			array[num++] = 3;
			array[num++] = 4;
			this._presumeZip64 = this._container.Zip64 == Zip64Option.Always || (this._container.Zip64 == Zip64Option.AsNecessary && !s.CanSeek);
			short num2 = (this._presumeZip64 ? 45 : 20);
			if (this.CompressionMethod == CompressionMethod.BZip2)
			{
				num2 = 46;
			}
			array[num++] = (byte)(num2 & 255);
			array[num++] = (byte)(((int)num2 & 65280) >> 8);
			byte[] encodedFileNameBytes = this.GetEncodedFileNameBytes();
			short num3 = (short)encodedFileNameBytes.Length;
			if (this._Encryption == EncryptionAlgorithm.None)
			{
				this._BitField &= -2;
			}
			else
			{
				this._BitField |= 1;
			}
			if (this._actualEncoding.CodePage == Encoding.UTF8.CodePage)
			{
				this._BitField |= 2048;
			}
			if (this.IsDirectory || cycle == 99)
			{
				this._BitField &= -9;
				this._BitField &= -2;
				this.Encryption = EncryptionAlgorithm.None;
				this.Password = null;
			}
			else if (!s.CanSeek)
			{
				this._BitField |= 8;
			}
			array[num++] = (byte)(this._BitField & 255);
			array[num++] = (byte)(((int)this._BitField & 65280) >> 8);
			if (this.__FileDataPosition == -1L)
			{
				this._CompressedSize = 0L;
				this._crcCalculated = false;
			}
			this.MaybeUnsetCompressionMethodForWriting(cycle);
			array[num++] = (byte)(this._CompressionMethod & 255);
			array[num++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			if (cycle == 99)
			{
				this.SetZip64Flags();
			}
			this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
			array[num++] = (byte)(this._TimeBlob & 255);
			array[num++] = (byte)((this._TimeBlob & 65280) >> 8);
			array[num++] = (byte)((this._TimeBlob & 16711680) >> 16);
			array[num++] = (byte)(((long)this._TimeBlob & (long)((ulong)(-16777216))) >> 24);
			array[num++] = (byte)(this._Crc32 & 255);
			array[num++] = (byte)((this._Crc32 & 65280) >> 8);
			array[num++] = (byte)((this._Crc32 & 16711680) >> 16);
			array[num++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			if (this._presumeZip64)
			{
				for (int i = 0; i < 8; i++)
				{
					array[num++] = byte.MaxValue;
				}
			}
			else
			{
				array[num++] = (byte)(this._CompressedSize & 255L);
				array[num++] = (byte)((this._CompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				array[num++] = (byte)(this._UncompressedSize & 255L);
				array[num++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				array[num++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				array[num++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
			}
			array[num++] = (byte)(num3 & 255);
			array[num++] = (byte)(((int)num3 & 65280) >> 8);
			this._Extra = this.ConstructExtraField(false);
			short num4 = (short)((this._Extra == null) ? 0 : this._Extra.Length);
			array[num++] = (byte)(num4 & 255);
			array[num++] = (byte)(((int)num4 & 65280) >> 8);
			byte[] array2 = new byte[num + (int)num3 + (int)num4];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			Buffer.BlockCopy(encodedFileNameBytes, 0, array2, num, encodedFileNameBytes.Length);
			num += encodedFileNameBytes.Length;
			if (this._Extra != null)
			{
				Buffer.BlockCopy(this._Extra, 0, array2, num, this._Extra.Length);
				num += this._Extra.Length;
			}
			this._LengthOfHeader = num;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = true;
				uint num5 = zipSegmentedStream.ComputeSegment(num);
				if (num5 != zipSegmentedStream.CurrentSegment)
				{
					this._future_ROLH = 0L;
				}
				else
				{
					this._future_ROLH = zipSegmentedStream.Position;
				}
				this._diskNumber = num5;
			}
			if (this._container.Zip64 == Zip64Option.Default && (uint)this._RelativeOffsetOfLocalHeader >= 4294967295U)
			{
				throw new ZipException("Offset within the zip archive exceeds 0xFFFFFFFF. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			s.Write(array2, 0, num);
			if (zipSegmentedStream != null)
			{
				zipSegmentedStream.ContiguousWrite = false;
			}
			this._EntryHeader = array2;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000074CC File Offset: 0x000056CC
		private int FigureCrc32()
		{
			if (!this._crcCalculated)
			{
				Stream stream = null;
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(Stream.Null);
					this._WriteDelegate(this.FileName, crcCalculatorStream);
					this._Crc32 = crcCalculatorStream.Crc;
				}
				else if (this._Source != ZipEntrySource.ZipFile)
				{
					if (this._Source == ZipEntrySource.Stream)
					{
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source == ZipEntrySource.JitStream)
					{
						if (this._sourceStream == null)
						{
							this._sourceStream = this._OpenDelegate(this.FileName);
						}
						this.PrepSourceStream();
						stream = this._sourceStream;
					}
					else if (this._Source != ZipEntrySource.ZipOutputStream)
					{
						stream = File.Open(this.LocalFileName, 3, 1, 3);
					}
					CRC32 crc = new CRC32();
					this._Crc32 = crc.GetCrc32(stream);
					if (this._sourceStream == null)
					{
						stream.Close();
					}
				}
				this._crcCalculated = true;
			}
			return this._Crc32;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000075C0 File Offset: 0x000057C0
		private void PrepSourceStream()
		{
			if (this._sourceStream == null)
			{
				throw new ZipException(string.Format("The input stream is null for entry '{0}'.", this.FileName));
			}
			if (this._sourceStreamOriginalPosition != null)
			{
				this._sourceStream.Position = this._sourceStreamOriginalPosition.Value;
				return;
			}
			if (this._sourceStream.CanSeek)
			{
				this._sourceStreamOriginalPosition = new long?(this._sourceStream.Position);
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak && this._Source != ZipEntrySource.ZipFile && (this._BitField & 8) != 8)
			{
				throw new ZipException("It is not possible to use PKZIP encryption on a non-seekable input stream");
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000765C File Offset: 0x0000585C
		internal void CopyMetaData(ZipEntry source)
		{
			this.__FileDataPosition = source.__FileDataPosition;
			this.CompressionMethod = source.CompressionMethod;
			this._CompressionMethod_FromZipFile = source._CompressionMethod_FromZipFile;
			this._CompressedFileDataSize = source._CompressedFileDataSize;
			this._UncompressedSize = source._UncompressedSize;
			this._BitField = source._BitField;
			this._Source = source._Source;
			this._LastModified = source._LastModified;
			this._Mtime = source._Mtime;
			this._Atime = source._Atime;
			this._Ctime = source._Ctime;
			this._ntfsTimesAreSet = source._ntfsTimesAreSet;
			this._emitUnixTimes = source._emitUnixTimes;
			this._emitNtfsTimes = source._emitNtfsTimes;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007711 File Offset: 0x00005911
		private void OnWriteBlock(long bytesXferred, long totalBytesToXfer)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnSaveBlock(this, bytesXferred, totalBytesToXfer);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000773C File Offset: 0x0000593C
		private void _WriteEntryData(Stream s)
		{
			Stream stream = null;
			long num = -1L;
			try
			{
				num = s.Position;
			}
			catch (Exception)
			{
			}
			try
			{
				long num2 = this.SetInputAndFigureFileLength(ref stream);
				CountingStream countingStream = new CountingStream(s);
				Stream stream2;
				Stream stream3;
				if (num2 != 0L)
				{
					stream2 = this.MaybeApplyEncryption(countingStream);
					stream3 = this.MaybeApplyCompression(stream2, num2);
				}
				else
				{
					stream3 = (stream2 = countingStream);
				}
				CrcCalculatorStream crcCalculatorStream = new CrcCalculatorStream(stream3, true);
				if (this._Source == ZipEntrySource.WriteDelegate)
				{
					this._WriteDelegate(this.FileName, crcCalculatorStream);
				}
				else
				{
					byte[] array = new byte[this.BufferSize];
					int num3;
					while ((num3 = SharedUtilities.ReadWithRetry(stream, array, 0, array.Length, this.FileName)) != 0)
					{
						crcCalculatorStream.Write(array, 0, num3);
						this.OnWriteBlock(crcCalculatorStream.TotalBytesSlurped, num2);
						if (this._ioOperationCanceled)
						{
							break;
						}
					}
				}
				this.FinishOutputStream(s, countingStream, stream2, stream3, crcCalculatorStream);
			}
			finally
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._CloseDelegate != null)
					{
						this._CloseDelegate(this.FileName, stream);
					}
				}
				else if (stream is FileStream)
				{
					stream.Close();
				}
			}
			if (this._ioOperationCanceled)
			{
				return;
			}
			this.__FileDataPosition = num;
			this.PostProcessOutput(s);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007874 File Offset: 0x00005A74
		private long SetInputAndFigureFileLength(ref Stream input)
		{
			long num = -1L;
			if (this._Source == ZipEntrySource.Stream)
			{
				this.PrepSourceStream();
				input = this._sourceStream;
				try
				{
					return this._sourceStream.Length;
				}
				catch (NotSupportedException)
				{
					return num;
				}
			}
			if (this._Source == ZipEntrySource.ZipFile)
			{
				string text = ((this._Encryption_FromZipFile == EncryptionAlgorithm.None) ? null : (this._Password ?? this._container.Password));
				this._sourceStream = this.InternalOpenReader(text);
				this.PrepSourceStream();
				input = this._sourceStream;
				num = this._sourceStream.Length;
			}
			else
			{
				if (this._Source == ZipEntrySource.JitStream)
				{
					if (this._sourceStream == null)
					{
						this._sourceStream = this._OpenDelegate(this.FileName);
					}
					this.PrepSourceStream();
					input = this._sourceStream;
					try
					{
						return this._sourceStream.Length;
					}
					catch (NotSupportedException)
					{
						return num;
					}
				}
				if (this._Source == ZipEntrySource.FileSystem)
				{
					FileShare fileShare = 3;
					input = File.Open(this.LocalFileName, 3, 1, fileShare);
					num = input.Length;
				}
			}
			return num;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000798C File Offset: 0x00005B8C
		internal void FinishOutputStream(Stream s, CountingStream entryCounter, Stream encryptor, Stream compressor, CrcCalculatorStream output)
		{
			if (output == null)
			{
				return;
			}
			output.Close();
			if (compressor is DeflateStream)
			{
				compressor.Close();
			}
			else if (compressor is BZip2OutputStream)
			{
				compressor.Close();
			}
			encryptor.Flush();
			encryptor.Close();
			this._LengthOfTrailer = 0;
			this._UncompressedSize = output.TotalBytesSlurped;
			this._CompressedFileDataSize = entryCounter.BytesWritten;
			this._CompressedSize = this._CompressedFileDataSize;
			this._Crc32 = output.Crc;
			this.StoreRelativeOffset();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007A14 File Offset: 0x00005C14
		internal void PostProcessOutput(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			if (this._UncompressedSize == 0L && this._CompressedSize == 0L)
			{
				if (this._Source == ZipEntrySource.ZipOutputStream)
				{
					return;
				}
				if (this._Password != null)
				{
					int num = 0;
					if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
					{
						num = 12;
					}
					if (this._Source == ZipEntrySource.ZipOutputStream && !s.CanSeek)
					{
						throw new ZipException("Zero bytes written, encryption in use, and non-seekable output.");
					}
					if (this.Encryption != EncryptionAlgorithm.None)
					{
						s.Seek((long)(-1 * num), 1);
						s.SetLength(s.Position);
						SharedUtilities.Workaround_Ladybug318918(s);
						if (countingStream != null)
						{
							countingStream.Adjust((long)num);
						}
						this._LengthOfHeader -= num;
						this.__FileDataPosition -= (long)num;
					}
					this._Password = null;
					this._BitField &= -2;
					int num2 = 6;
					this._EntryHeader[num2++] = (byte)(this._BitField & 255);
					this._EntryHeader[num2++] = (byte)(((int)this._BitField & 65280) >> 8);
				}
				this.CompressionMethod = CompressionMethod.None;
				this.Encryption = EncryptionAlgorithm.None;
			}
			else if (this._zipCrypto_forWrite != null && this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				this._CompressedSize += 12L;
			}
			int num3 = 8;
			this._EntryHeader[num3++] = (byte)(this._CompressionMethod & 255);
			this._EntryHeader[num3++] = (byte)(((int)this._CompressionMethod & 65280) >> 8);
			num3 = 14;
			this._EntryHeader[num3++] = (byte)(this._Crc32 & 255);
			this._EntryHeader[num3++] = (byte)((this._Crc32 & 65280) >> 8);
			this._EntryHeader[num3++] = (byte)((this._Crc32 & 16711680) >> 16);
			this._EntryHeader[num3++] = (byte)(((long)this._Crc32 & (long)((ulong)(-16777216))) >> 24);
			this.SetZip64Flags();
			short num4 = (short)((int)this._EntryHeader[26] + (int)this._EntryHeader[27] * 256);
			short num5 = (short)((int)this._EntryHeader[28] + (int)this._EntryHeader[29] * 256);
			if (this._OutputUsesZip64.Value)
			{
				this._EntryHeader[4] = 45;
				this._EntryHeader[5] = 0;
				for (int i = 0; i < 8; i++)
				{
					this._EntryHeader[num3++] = byte.MaxValue;
				}
				num3 = (int)(30 + num4);
				this._EntryHeader[num3++] = 1;
				this._EntryHeader[num3++] = 0;
				num3 += 2;
				Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, this._EntryHeader, num3, 8);
				num3 += 8;
				Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, this._EntryHeader, num3, 8);
			}
			else
			{
				this._EntryHeader[4] = 20;
				this._EntryHeader[5] = 0;
				num3 = 18;
				this._EntryHeader[num3++] = (byte)(this._CompressedSize & 255L);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & 65280L) >> 8);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & 16711680L) >> 16);
				this._EntryHeader[num3++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
				this._EntryHeader[num3++] = (byte)(this._UncompressedSize & 255L);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & 65280L) >> 8);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
				this._EntryHeader[num3++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				if (num5 != 0)
				{
					num3 = (int)(30 + num4);
					short num6 = (short)((int)this._EntryHeader[num3 + 2] + (int)this._EntryHeader[num3 + 3] * 256);
					if (num6 == 16)
					{
						this._EntryHeader[num3++] = 153;
						this._EntryHeader[num3++] = 153;
					}
				}
			}
			if ((this._BitField & 8) != 8 || (this._Source == ZipEntrySource.ZipOutputStream && s.CanSeek))
			{
				ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
				if (zipSegmentedStream != null && this._diskNumber != zipSegmentedStream.CurrentSegment)
				{
					using (Stream stream = ZipSegmentedStream.ForUpdate(this._container.ZipFile.Name, this._diskNumber))
					{
						stream.Seek(this._RelativeOffsetOfLocalHeader, 0);
						stream.Write(this._EntryHeader, 0, this._EntryHeader.Length);
						goto IL_04CA;
					}
				}
				s.Seek(this._RelativeOffsetOfLocalHeader, 0);
				s.Write(this._EntryHeader, 0, this._EntryHeader.Length);
				if (countingStream != null)
				{
					countingStream.Adjust((long)this._EntryHeader.Length);
				}
				s.Seek(this._CompressedSize, 1);
			}
			IL_04CA:
			if ((this._BitField & 8) == 8 && !this.IsDirectory)
			{
				byte[] array = new byte[16 + (this._OutputUsesZip64.Value ? 8 : 0)];
				num3 = 0;
				Array.Copy(BitConverter.GetBytes(134695760), 0, array, num3, 4);
				num3 += 4;
				Array.Copy(BitConverter.GetBytes(this._Crc32), 0, array, num3, 4);
				num3 += 4;
				if (this._OutputUsesZip64.Value)
				{
					Array.Copy(BitConverter.GetBytes(this._CompressedSize), 0, array, num3, 8);
					num3 += 8;
					Array.Copy(BitConverter.GetBytes(this._UncompressedSize), 0, array, num3, 8);
					num3 += 8;
				}
				else
				{
					array[num3++] = (byte)(this._CompressedSize & 255L);
					array[num3++] = (byte)((this._CompressedSize & 65280L) >> 8);
					array[num3++] = (byte)((this._CompressedSize & 16711680L) >> 16);
					array[num3++] = (byte)((this._CompressedSize & (long)((ulong)(-16777216))) >> 24);
					array[num3++] = (byte)(this._UncompressedSize & 255L);
					array[num3++] = (byte)((this._UncompressedSize & 65280L) >> 8);
					array[num3++] = (byte)((this._UncompressedSize & 16711680L) >> 16);
					array[num3++] = (byte)((this._UncompressedSize & (long)((ulong)(-16777216))) >> 24);
				}
				s.Write(array, 0, array.Length);
				this._LengthOfTrailer += array.Length;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008084 File Offset: 0x00006284
		private void SetZip64Flags()
		{
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			if (this._container.Zip64 == Zip64Option.Default && this._entryRequiresZip64.Value)
			{
				throw new ZipException("Compressed or Uncompressed size, or offset exceeds the maximum value. Consider setting the UseZip64WhenSaving property on the ZipFile instance.");
			}
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000810C File Offset: 0x0000630C
		internal void PrepOutputStream(Stream s, long streamLength, out CountingStream outputCounter, out Stream encryptor, out Stream compressor, out CrcCalculatorStream output)
		{
			outputCounter = new CountingStream(s);
			if (streamLength != 0L)
			{
				encryptor = this.MaybeApplyEncryption(outputCounter);
				compressor = this.MaybeApplyCompression(encryptor, streamLength);
			}
			else
			{
				Stream stream;
				compressor = (stream = outputCounter);
				encryptor = stream;
			}
			output = new CrcCalculatorStream(compressor, true);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008158 File Offset: 0x00006358
		private Stream MaybeApplyCompression(Stream s, long streamLength)
		{
			if (this._CompressionMethod == 8 && this.CompressionLevel != CompressionLevel.None)
			{
				DeflateStream deflateStream = new DeflateStream(s, CompressionMode.Compress, this.CompressionLevel, true);
				if (this._container.CodecBufferSize > 0)
				{
					deflateStream.BufferSize = this._container.CodecBufferSize;
				}
				deflateStream.Strategy = this._container.Strategy;
				return deflateStream;
			}
			if (this._CompressionMethod == 12)
			{
				return new BZip2OutputStream(s, true);
			}
			return s;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000081CC File Offset: 0x000063CC
		private Stream MaybeApplyEncryption(Stream s)
		{
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				return new ZipCipherStream(s, this._zipCrypto_forWrite, CryptoMode.Encrypt);
			}
			return s;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000081E6 File Offset: 0x000063E6
		private void OnZipErrorWhileSaving(Exception e)
		{
			if (this._container.ZipFile != null)
			{
				this._ioOperationCanceled = this._container.ZipFile.OnZipErrorSaving(this, e);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008210 File Offset: 0x00006410
		internal void Write(Stream s)
		{
			CountingStream countingStream = s as CountingStream;
			ZipSegmentedStream zipSegmentedStream = s as ZipSegmentedStream;
			bool flag = false;
			do
			{
				try
				{
					if (this._Source == ZipEntrySource.ZipFile && !this._restreamRequiredOnSave)
					{
						this.CopyThroughOneEntry(s);
						break;
					}
					if (this.IsDirectory)
					{
						this.WriteHeader(s, 1);
						this.StoreRelativeOffset();
						this._entryRequiresZip64 = new bool?(this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
						this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
						if (zipSegmentedStream != null)
						{
							this._diskNumber = zipSegmentedStream.CurrentSegment;
						}
						break;
					}
					int num = 0;
					bool flag2;
					do
					{
						num++;
						this.WriteHeader(s, num);
						this.WriteSecurityMetadata(s);
						this._WriteEntryData(s);
						this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
						flag2 = num <= 1 && s.CanSeek && this.WantReadAgain();
						if (flag2)
						{
							if (zipSegmentedStream != null)
							{
								zipSegmentedStream.TruncateBackward(this._diskNumber, this._RelativeOffsetOfLocalHeader);
							}
							else
							{
								s.Seek(this._RelativeOffsetOfLocalHeader, 0);
							}
							s.SetLength(s.Position);
							if (countingStream != null)
							{
								countingStream.Adjust(this._TotalEntrySize);
							}
						}
					}
					while (flag2);
					this._skippedDuringSave = false;
					flag = true;
				}
				catch (Exception ex)
				{
					ZipErrorAction zipErrorAction = this.ZipErrorAction;
					int num2 = 0;
					while (this.ZipErrorAction != ZipErrorAction.Throw)
					{
						if (this.ZipErrorAction == ZipErrorAction.Skip || this.ZipErrorAction == ZipErrorAction.Retry)
						{
							long num3 = ((countingStream != null) ? countingStream.ComputedPosition : s.Position);
							long num4 = num3 - this._future_ROLH;
							if (num4 > 0L)
							{
								s.Seek(num4, 1);
								long position = s.Position;
								s.SetLength(s.Position);
								if (countingStream != null)
								{
									countingStream.Adjust(num3 - position);
								}
							}
							if (this.ZipErrorAction == ZipErrorAction.Skip)
							{
								this.WriteStatus("Skipping file {0} (exception: {1})", new object[]
								{
									this.LocalFileName,
									ex.ToString()
								});
								this._skippedDuringSave = true;
								flag = true;
							}
							else
							{
								this.ZipErrorAction = zipErrorAction;
							}
						}
						else
						{
							if (num2 > 0)
							{
								throw;
							}
							if (this.ZipErrorAction == ZipErrorAction.InvokeErrorEvent)
							{
								this.OnZipErrorWhileSaving(ex);
								if (this._ioOperationCanceled)
								{
									flag = true;
									goto IL_023B;
								}
							}
							num2++;
							continue;
						}
						IL_023B:
						goto IL_023D;
					}
					throw;
				}
				IL_023D:;
			}
			while (!flag);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000847C File Offset: 0x0000667C
		internal void StoreRelativeOffset()
		{
			this._RelativeOffsetOfLocalHeader = this._future_ROLH;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000848A File Offset: 0x0000668A
		internal void NotifySaveComplete()
		{
			this._Encryption_FromZipFile = this._Encryption;
			this._CompressionMethod_FromZipFile = this._CompressionMethod;
			this._restreamRequiredOnSave = false;
			this._metadataChanged = false;
			this._Source = ZipEntrySource.ZipFile;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000084BC File Offset: 0x000066BC
		internal void WriteSecurityMetadata(Stream outstream)
		{
			if (this.Encryption == EncryptionAlgorithm.None)
			{
				return;
			}
			string text = this._Password;
			if (this._Source == ZipEntrySource.ZipFile && text == null)
			{
				text = this._container.Password;
			}
			if (text == null)
			{
				this._zipCrypto_forWrite = null;
				return;
			}
			if (this.Encryption == EncryptionAlgorithm.PkzipWeak)
			{
				this._zipCrypto_forWrite = ZipCrypto.ForWrite(text);
				Random random = new Random();
				byte[] array = new byte[12];
				random.NextBytes(array);
				if ((this._BitField & 8) == 8)
				{
					this._TimeBlob = SharedUtilities.DateTimeToPacked(this.LastModified);
					array[11] = (byte)((this._TimeBlob >> 8) & 255);
				}
				else
				{
					this.FigureCrc32();
					array[11] = (byte)((this._Crc32 >> 24) & 255);
				}
				byte[] array2 = this._zipCrypto_forWrite.EncryptMessage(array, array.Length);
				outstream.Write(array2, 0, array2.Length);
				this._LengthOfHeader += array2.Length;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000085A0 File Offset: 0x000067A0
		private void CopyThroughOneEntry(Stream outStream)
		{
			if (this.LengthOfHeader == 0)
			{
				throw new BadStateException("Bad header length.");
			}
			bool flag = this._metadataChanged || this.ArchiveStream is ZipSegmentedStream || outStream is ZipSegmentedStream || (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default) || (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always);
			if (flag)
			{
				this.CopyThroughWithRecompute(outStream);
			}
			else
			{
				this.CopyThroughWithNoChange(outStream);
			}
			this._entryRequiresZip64 = new bool?(this._CompressedSize >= (long)((ulong)(-1)) || this._UncompressedSize >= (long)((ulong)(-1)) || this._RelativeOffsetOfLocalHeader >= (long)((ulong)(-1)));
			this._OutputUsesZip64 = new bool?(this._container.Zip64 == Zip64Option.Always || this._entryRequiresZip64.Value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008678 File Offset: 0x00006878
		private void CopyThroughWithRecompute(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			long relativeOffsetOfLocalHeader = this._RelativeOffsetOfLocalHeader;
			int lengthOfHeader = this.LengthOfHeader;
			this.WriteHeader(outstream, 0);
			this.StoreRelativeOffset();
			if (!this.FileName.EndsWith("/"))
			{
				long num = relativeOffsetOfLocalHeader + (long)lengthOfHeader;
				int num2 = ZipEntry.GetLengthOfCryptoHeaderBytes(this._Encryption_FromZipFile);
				num -= (long)num2;
				this._LengthOfHeader += num2;
				countingStream.Seek(num, 0);
				long num3 = this._CompressedSize;
				while (num3 > 0L)
				{
					num2 = ((num3 > (long)array.Length) ? array.Length : ((int)num3));
					int num4 = countingStream.Read(array, 0, num2);
					outstream.Write(array, 0, num4);
					num3 -= (long)num4;
					this.OnWriteBlock(countingStream.BytesRead, this._CompressedSize);
					if (this._ioOperationCanceled)
					{
						break;
					}
				}
				if ((this._BitField & 8) == 8)
				{
					int num5 = 16;
					if (this._InputUsesZip64)
					{
						num5 += 8;
					}
					byte[] array2 = new byte[num5];
					countingStream.Read(array2, 0, num5);
					if (this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Default)
					{
						outstream.Write(array2, 0, 8);
						if (this._CompressedSize > (long)((ulong)(-1)))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 8, 4);
						if (this._UncompressedSize > (long)((ulong)(-1)))
						{
							throw new InvalidOperationException("ZIP64 is required");
						}
						outstream.Write(array2, 16, 4);
						this._LengthOfTrailer -= 8;
					}
					else if (!this._InputUsesZip64 && this._container.UseZip64WhenSaving == Zip64Option.Always)
					{
						byte[] array3 = new byte[4];
						outstream.Write(array2, 0, 8);
						outstream.Write(array2, 8, 4);
						outstream.Write(array3, 0, 4);
						outstream.Write(array2, 12, 4);
						outstream.Write(array3, 0, 4);
						this._LengthOfTrailer += 8;
					}
					else
					{
						outstream.Write(array2, 0, num5);
					}
				}
			}
			this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008888 File Offset: 0x00006A88
		private void CopyThroughWithNoChange(Stream outstream)
		{
			byte[] array = new byte[this.BufferSize];
			CountingStream countingStream = new CountingStream(this.ArchiveStream);
			countingStream.Seek(this._RelativeOffsetOfLocalHeader, 0);
			if (this._TotalEntrySize == 0L)
			{
				this._TotalEntrySize = (long)this._LengthOfHeader + this._CompressedFileDataSize + (long)this._LengthOfTrailer;
			}
			CountingStream countingStream2 = outstream as CountingStream;
			this._RelativeOffsetOfLocalHeader = ((countingStream2 != null) ? countingStream2.ComputedPosition : outstream.Position);
			long num = this._TotalEntrySize;
			while (num > 0L)
			{
				int num2 = ((num > (long)array.Length) ? array.Length : ((int)num));
				int num3 = countingStream.Read(array, 0, num2);
				outstream.Write(array, 0, num3);
				num -= (long)num3;
				this.OnWriteBlock(countingStream.BytesRead, this._TotalEntrySize);
				if (this._ioOperationCanceled)
				{
					return;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00008958 File Offset: 0x00006B58
		[Conditional("Trace")]
		private void TraceWriteLine(string format, params object[] varParams)
		{
			lock (this._outputLock)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.Write("{0:000} ZipEntry.Write ", hashCode);
				Console.WriteLine(format, varParams);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000089B0 File Offset: 0x00006BB0
		// Note: this type is marked as 'beforefieldinit'.
		static ZipEntry()
		{
		}

		// Token: 0x0400004C RID: 76
		private short _VersionMadeBy;

		// Token: 0x0400004D RID: 77
		private short _InternalFileAttrs;

		// Token: 0x0400004E RID: 78
		private int _ExternalFileAttrs;

		// Token: 0x0400004F RID: 79
		private short _filenameLength;

		// Token: 0x04000050 RID: 80
		private short _extraFieldLength;

		// Token: 0x04000051 RID: 81
		private short _commentLength;

		// Token: 0x04000052 RID: 82
		private ZipCrypto _zipCrypto_forExtract;

		// Token: 0x04000053 RID: 83
		private ZipCrypto _zipCrypto_forWrite;

		// Token: 0x04000054 RID: 84
		internal DateTime _LastModified;

		// Token: 0x04000055 RID: 85
		private DateTime _Mtime;

		// Token: 0x04000056 RID: 86
		private DateTime _Atime;

		// Token: 0x04000057 RID: 87
		private DateTime _Ctime;

		// Token: 0x04000058 RID: 88
		private bool _ntfsTimesAreSet;

		// Token: 0x04000059 RID: 89
		private bool _emitNtfsTimes = true;

		// Token: 0x0400005A RID: 90
		private bool _emitUnixTimes;

		// Token: 0x0400005B RID: 91
		private bool _TrimVolumeFromFullyQualifiedPaths = true;

		// Token: 0x0400005C RID: 92
		internal string _LocalFileName;

		// Token: 0x0400005D RID: 93
		private string _FileNameInArchive;

		// Token: 0x0400005E RID: 94
		internal short _VersionNeeded;

		// Token: 0x0400005F RID: 95
		internal short _BitField;

		// Token: 0x04000060 RID: 96
		internal short _CompressionMethod;

		// Token: 0x04000061 RID: 97
		private short _CompressionMethod_FromZipFile;

		// Token: 0x04000062 RID: 98
		private CompressionLevel _CompressionLevel;

		// Token: 0x04000063 RID: 99
		internal string _Comment;

		// Token: 0x04000064 RID: 100
		private bool _IsDirectory;

		// Token: 0x04000065 RID: 101
		private byte[] _CommentBytes;

		// Token: 0x04000066 RID: 102
		internal long _CompressedSize;

		// Token: 0x04000067 RID: 103
		internal long _CompressedFileDataSize;

		// Token: 0x04000068 RID: 104
		internal long _UncompressedSize;

		// Token: 0x04000069 RID: 105
		internal int _TimeBlob;

		// Token: 0x0400006A RID: 106
		private bool _crcCalculated;

		// Token: 0x0400006B RID: 107
		internal int _Crc32;

		// Token: 0x0400006C RID: 108
		internal byte[] _Extra;

		// Token: 0x0400006D RID: 109
		private bool _metadataChanged;

		// Token: 0x0400006E RID: 110
		private bool _restreamRequiredOnSave;

		// Token: 0x0400006F RID: 111
		private bool _sourceIsEncrypted;

		// Token: 0x04000070 RID: 112
		private bool _skippedDuringSave;

		// Token: 0x04000071 RID: 113
		private uint _diskNumber;

		// Token: 0x04000072 RID: 114
		private static Encoding ibm437 = Encoding.GetEncoding("IBM437");

		// Token: 0x04000073 RID: 115
		private Encoding _actualEncoding;

		// Token: 0x04000074 RID: 116
		internal ZipContainer _container;

		// Token: 0x04000075 RID: 117
		private long __FileDataPosition = -1L;

		// Token: 0x04000076 RID: 118
		private byte[] _EntryHeader;

		// Token: 0x04000077 RID: 119
		internal long _RelativeOffsetOfLocalHeader;

		// Token: 0x04000078 RID: 120
		private long _future_ROLH;

		// Token: 0x04000079 RID: 121
		private long _TotalEntrySize;

		// Token: 0x0400007A RID: 122
		private int _LengthOfHeader;

		// Token: 0x0400007B RID: 123
		private int _LengthOfTrailer;

		// Token: 0x0400007C RID: 124
		internal bool _InputUsesZip64;

		// Token: 0x0400007D RID: 125
		private uint _UnsupportedAlgorithmId;

		// Token: 0x0400007E RID: 126
		internal string _Password;

		// Token: 0x0400007F RID: 127
		internal ZipEntrySource _Source;

		// Token: 0x04000080 RID: 128
		internal EncryptionAlgorithm _Encryption;

		// Token: 0x04000081 RID: 129
		internal EncryptionAlgorithm _Encryption_FromZipFile;

		// Token: 0x04000082 RID: 130
		internal byte[] _WeakEncryptionHeader;

		// Token: 0x04000083 RID: 131
		internal Stream _archiveStream;

		// Token: 0x04000084 RID: 132
		private Stream _sourceStream;

		// Token: 0x04000085 RID: 133
		private long? _sourceStreamOriginalPosition;

		// Token: 0x04000086 RID: 134
		private bool _sourceWasJitProvided;

		// Token: 0x04000087 RID: 135
		private bool _ioOperationCanceled;

		// Token: 0x04000088 RID: 136
		private bool _presumeZip64;

		// Token: 0x04000089 RID: 137
		private bool? _entryRequiresZip64;

		// Token: 0x0400008A RID: 138
		private bool? _OutputUsesZip64;

		// Token: 0x0400008B RID: 139
		private bool _IsText;

		// Token: 0x0400008C RID: 140
		private ZipEntryTimestamp _timestamp;

		// Token: 0x0400008D RID: 141
		private static DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);

		// Token: 0x0400008E RID: 142
		private static DateTime _win32Epoch = DateTime.FromFileTimeUtc(0L);

		// Token: 0x0400008F RID: 143
		private static DateTime _zeroHour = new DateTime(1, 1, 1, 0, 0, 0, 1);

		// Token: 0x04000090 RID: 144
		private WriteDelegate _WriteDelegate;

		// Token: 0x04000091 RID: 145
		private OpenDelegate _OpenDelegate;

		// Token: 0x04000092 RID: 146
		private CloseDelegate _CloseDelegate;

		// Token: 0x04000093 RID: 147
		private Stream _inputDecryptorStream;

		// Token: 0x04000094 RID: 148
		private int _readExtraDepth;

		// Token: 0x04000095 RID: 149
		private object _outputLock = new object();

		// Token: 0x04000096 RID: 150
		private ExtractExistingFileAction <ExtractExistingFile>k__BackingField;

		// Token: 0x04000097 RID: 151
		private ZipErrorAction <ZipErrorAction>k__BackingField;

		// Token: 0x04000098 RID: 152
		private SetCompressionCallback <SetCompression>k__BackingField;

		// Token: 0x04000099 RID: 153
		private Encoding <ProvisionalAlternateEncoding>k__BackingField;

		// Token: 0x0400009A RID: 154
		private Encoding <AlternateEncoding>k__BackingField;

		// Token: 0x0400009B RID: 155
		private ZipOption <AlternateEncodingUsage>k__BackingField;

		// Token: 0x0200001E RID: 30
		private class CopyHelper
		{
			// Token: 0x0600013F RID: 319 RVA: 0x00008A00 File Offset: 0x00006C00
			internal static string AppendCopyToFileName(string f)
			{
				ZipEntry.CopyHelper.callCount++;
				if (ZipEntry.CopyHelper.callCount > 25)
				{
					throw new OverflowException("overflow while creating filename");
				}
				int num = 1;
				int num2 = f.LastIndexOf(".");
				if (num2 == -1)
				{
					Match match = ZipEntry.CopyHelper.re.Match(f);
					if (match.Success)
					{
						num = int.Parse(match.Groups[1].Value) + 1;
						string text = string.Format(" (copy {0})", num);
						f = f.Substring(0, match.Index) + text;
					}
					else
					{
						string text2 = string.Format(" (copy {0})", num);
						f += text2;
					}
				}
				else
				{
					Match match2 = ZipEntry.CopyHelper.re.Match(f.Substring(0, num2));
					if (match2.Success)
					{
						num = int.Parse(match2.Groups[1].Value) + 1;
						string text3 = string.Format(" (copy {0})", num);
						f = f.Substring(0, match2.Index) + text3 + f.Substring(num2);
					}
					else
					{
						string text4 = string.Format(" (copy {0})", num);
						f = f.Substring(0, num2) + text4 + f.Substring(num2);
					}
				}
				return f;
			}

			// Token: 0x06000140 RID: 320 RVA: 0x00008B63 File Offset: 0x00006D63
			public CopyHelper()
			{
			}

			// Token: 0x06000141 RID: 321 RVA: 0x00008B4C File Offset: 0x00006D4C
			// Note: this type is marked as 'beforefieldinit'.
			static CopyHelper()
			{
			}

			// Token: 0x0400009C RID: 156
			private static Regex re = new Regex(" \\(copy (\\d+)\\)$");

			// Token: 0x0400009D RID: 157
			private static int callCount = 0;
		}

		// Token: 0x0200001F RID: 31
		// (Invoke) Token: 0x06000143 RID: 323
		private delegate T Func<T>();

		// Token: 0x0200005C RID: 92
		private sealed class <>c__DisplayClass1
		{
			// Token: 0x06000416 RID: 1046 RVA: 0x00005F1D File Offset: 0x0000411D
			public <>c__DisplayClass1()
			{
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x00005F28 File Offset: 0x00004128
			public long <ProcessExtraFieldZip64>b__0()
			{
				if (this.remainingData < 8)
				{
					throw new BadReadException(string.Format("  Missing data for ZIP64 extra field, position 0x{0:X16}", this.posn));
				}
				long num = BitConverter.ToInt64(this.buffer, this.j);
				this.j += 8;
				this.remainingData -= 8;
				return num;
			}

			// Token: 0x0400032E RID: 814
			public int remainingData;

			// Token: 0x0400032F RID: 815
			public byte[] buffer;

			// Token: 0x04000330 RID: 816
			public int j;

			// Token: 0x04000331 RID: 817
			public long posn;
		}

		// Token: 0x0200005D RID: 93
		private sealed class <>c__DisplayClass4
		{
			// Token: 0x06000418 RID: 1048 RVA: 0x000060CE File Offset: 0x000042CE
			public <>c__DisplayClass4()
			{
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x000060D8 File Offset: 0x000042D8
			public DateTime <ProcessExtraFieldUnixTimes>b__3()
			{
				int num = BitConverter.ToInt32(this.buffer, this.j);
				this.j += 4;
				this.remainingData -= 4;
				return ZipEntry._unixEpoch.AddSeconds((double)num);
			}

			// Token: 0x04000332 RID: 818
			public int remainingData;

			// Token: 0x04000333 RID: 819
			public ZipEntry <>4__this;

			// Token: 0x04000334 RID: 820
			public byte[] buffer;

			// Token: 0x04000335 RID: 821
			public int j;
		}
	}
}
