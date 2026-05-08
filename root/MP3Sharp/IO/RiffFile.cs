using System;
using System.IO;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.IO
{
	// Token: 0x02000008 RID: 8
	public class RiffFile
	{
		// Token: 0x06000038 RID: 56 RVA: 0x0000275C File Offset: 0x0000095C
		internal RiffFile()
		{
			this._File = null;
			this.Fmode = 0;
			this._RiffHeader = new RiffFile.RiffChunkHeader(this);
			this._RiffHeader.CkId = RiffFile.FourCC("RIFF");
			this._RiffHeader.CkSize = 0;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027AA File Offset: 0x000009AA
		internal int CurrentFileMode()
		{
			return this.Fmode;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027B4 File Offset: 0x000009B4
		internal virtual int Open(string filename, int newMode)
		{
			int num = 0;
			if (this.Fmode != 0)
			{
				num = this.Close();
			}
			if (num == 0)
			{
				if (newMode != 1)
				{
					if (newMode != 2)
					{
						goto IL_0241;
					}
				}
				else
				{
					try
					{
						this._File = RandomAccessFileStream.CreateRandomAccessFile(filename, "rw");
						try
						{
							sbyte[] array = new sbyte[8];
							array[0] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 24) & 255);
							array[1] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 16) & 255);
							array[2] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 8) & 255);
							array[3] = (sbyte)(this._RiffHeader.CkId & 255);
							sbyte b = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 24) & 255);
							sbyte b2 = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 16) & 255);
							sbyte b3 = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 8) & 255);
							sbyte b4 = (sbyte)(this._RiffHeader.CkSize & 255);
							array[4] = b4;
							array[5] = b3;
							array[6] = b2;
							array[7] = b;
							this._File.Write(SupportClass.ToByteArray(array), 0, 8);
							this.Fmode = 1;
						}
						catch
						{
							this._File.Close();
							this.Fmode = 0;
						}
						return num;
					}
					catch
					{
						this.Fmode = 0;
						return 3;
					}
				}
				try
				{
					this._File = RandomAccessFileStream.CreateRandomAccessFile(filename, "r");
					try
					{
						sbyte[] array2 = new sbyte[8];
						SupportClass.ReadInput(this._File, ref array2, 0, 8);
						this.Fmode = 2;
						this._RiffHeader.CkId = (((int)array2[0] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)array2[1] << 16) & 16711680) | (((int)array2[2] << 8) & 65280) | ((int)array2[3] & 255);
						this._RiffHeader.CkSize = (((int)array2[4] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)array2[5] << 16) & 16711680) | (((int)array2[6] << 8) & 65280) | ((int)array2[7] & 255);
					}
					catch
					{
						this._File.Close();
						this.Fmode = 0;
					}
					return num;
				}
				catch
				{
					this.Fmode = 0;
					return 3;
				}
				IL_0241:
				num = 4;
			}
			return num;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A6C File Offset: 0x00000C6C
		internal virtual int Open(Stream stream, int newMode)
		{
			int num = 0;
			if (this.Fmode != 0)
			{
				num = this.Close();
			}
			if (num == 0)
			{
				if (newMode != 1)
				{
					if (newMode != 2)
					{
						goto IL_022D;
					}
				}
				else
				{
					try
					{
						this._File = stream;
						try
						{
							sbyte[] array = new sbyte[8];
							array[0] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 24) & 255);
							array[1] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 16) & 255);
							array[2] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 8) & 255);
							array[3] = (sbyte)(this._RiffHeader.CkId & 255);
							sbyte b = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 24) & 255);
							sbyte b2 = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 16) & 255);
							sbyte b3 = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 8) & 255);
							sbyte b4 = (sbyte)(this._RiffHeader.CkSize & 255);
							array[4] = b4;
							array[5] = b3;
							array[6] = b2;
							array[7] = b;
							this._File.Write(SupportClass.ToByteArray(array), 0, 8);
							this.Fmode = 1;
						}
						catch
						{
							this._File.Close();
							this.Fmode = 0;
						}
						return num;
					}
					catch
					{
						this.Fmode = 0;
						return 3;
					}
				}
				try
				{
					this._File = stream;
					try
					{
						sbyte[] array2 = new sbyte[8];
						SupportClass.ReadInput(this._File, ref array2, 0, 8);
						this.Fmode = 2;
						this._RiffHeader.CkId = (((int)array2[0] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)array2[1] << 16) & 16711680) | (((int)array2[2] << 8) & 65280) | ((int)array2[3] & 255);
						this._RiffHeader.CkSize = (((int)array2[4] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)array2[5] << 16) & 16711680) | (((int)array2[6] << 8) & 65280) | ((int)array2[7] & 255);
					}
					catch
					{
						this._File.Close();
						this.Fmode = 0;
					}
					return num;
				}
				catch
				{
					this.Fmode = 0;
					return 3;
				}
				IL_022D:
				num = 4;
			}
			return num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D10 File Offset: 0x00000F10
		internal virtual int Write(sbyte[] data, int numBytes)
		{
			if (this.Fmode != 1)
			{
				return 4;
			}
			try
			{
				this._File.Write(SupportClass.ToByteArray(data), 0, numBytes);
				this.Fmode = 1;
			}
			catch
			{
				return 3;
			}
			this._RiffHeader.CkSize += numBytes;
			return 0;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual int Write(short[] data, int numBytes)
		{
			sbyte[] array = new sbyte[numBytes];
			int num = 0;
			for (int i = 0; i < numBytes; i += 2)
			{
				array[i] = (sbyte)(data[num] & 255);
				array[i + 1] = (sbyte)(SupportClass.URShift((int)data[num++], 8) & 255);
			}
			if (this.Fmode != 1)
			{
				return 4;
			}
			try
			{
				this._File.Write(SupportClass.ToByteArray(array), 0, numBytes);
				this.Fmode = 1;
			}
			catch
			{
				return 3;
			}
			this._RiffHeader.CkSize += numBytes;
			return 0;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E0C File Offset: 0x0000100C
		internal virtual int Write(RiffFile.RiffChunkHeader riffHeader, int numBytes)
		{
			sbyte[] array = new sbyte[8];
			array[0] = (sbyte)(SupportClass.URShift(riffHeader.CkId, 24) & 255);
			array[1] = (sbyte)(SupportClass.URShift(riffHeader.CkId, 16) & 255);
			array[2] = (sbyte)(SupportClass.URShift(riffHeader.CkId, 8) & 255);
			array[3] = (sbyte)(riffHeader.CkId & 255);
			sbyte b = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 24) & 255);
			sbyte b2 = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 16) & 255);
			sbyte b3 = (sbyte)(SupportClass.URShift(riffHeader.CkSize, 8) & 255);
			sbyte b4 = (sbyte)(riffHeader.CkSize & 255);
			array[4] = b4;
			array[5] = b3;
			array[6] = b2;
			array[7] = b;
			if (this.Fmode != 1)
			{
				return 4;
			}
			try
			{
				this._File.Write(SupportClass.ToByteArray(array), 0, numBytes);
				this.Fmode = 1;
			}
			catch
			{
				return 3;
			}
			this._RiffHeader.CkSize += numBytes;
			return 0;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002F28 File Offset: 0x00001128
		internal virtual int Write(short data, int numBytes)
		{
			if (this.Fmode != 1)
			{
				return 4;
			}
			try
			{
				new BinaryWriter(this._File).Write(data);
				this.Fmode = 1;
			}
			catch
			{
				return 3;
			}
			this._RiffHeader.CkSize += numBytes;
			return 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F88 File Offset: 0x00001188
		internal virtual int Write(int data, int numBytes)
		{
			if (this.Fmode != 1)
			{
				return 4;
			}
			try
			{
				new BinaryWriter(this._File).Write(data);
				this.Fmode = 1;
			}
			catch
			{
				return 3;
			}
			this._RiffHeader.CkSize += numBytes;
			return 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FE8 File Offset: 0x000011E8
		internal virtual int Read(sbyte[] data, int numBytes)
		{
			int num = 0;
			try
			{
				SupportClass.ReadInput(this._File, ref data, 0, numBytes);
			}
			catch
			{
				num = 3;
			}
			return num;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003020 File Offset: 0x00001220
		internal virtual int Expect(string data, int numBytes)
		{
			int num = 0;
			try
			{
				while (numBytes-- != 0)
				{
					if ((char)((sbyte)this._File.ReadByte()) != data.get_Chars(num++))
					{
						return 3;
					}
				}
			}
			catch
			{
				return 3;
			}
			return 0;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003070 File Offset: 0x00001270
		internal virtual int Close()
		{
			int num = 0;
			int fmode = this.Fmode;
			if (fmode != 1)
			{
				if (fmode != 2)
				{
					goto IL_013F;
				}
			}
			else
			{
				try
				{
					this._File.Seek(0L, 0);
					try
					{
						sbyte[] array = new sbyte[]
						{
							(sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 24) & 255),
							(sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 16) & 255),
							(sbyte)(SupportClass.URShift(this._RiffHeader.CkId, 8) & 255),
							(sbyte)(this._RiffHeader.CkId & 255),
							default(sbyte),
							default(sbyte),
							default(sbyte),
							(sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 24) & 255)
						};
						array[6] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 16) & 255);
						array[5] = (sbyte)(SupportClass.URShift(this._RiffHeader.CkSize, 8) & 255);
						array[4] = (sbyte)(this._RiffHeader.CkSize & 255);
						this._File.Write(SupportClass.ToByteArray(array), 0, 8);
						this._File.Close();
					}
					catch
					{
						num = 3;
					}
					goto IL_013F;
				}
				catch
				{
					num = 3;
					goto IL_013F;
				}
			}
			try
			{
				this._File.Close();
			}
			catch
			{
				num = 3;
			}
			IL_013F:
			this._File = null;
			this.Fmode = 0;
			return num;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003218 File Offset: 0x00001418
		internal virtual long CurrentFilePosition()
		{
			long num;
			try
			{
				num = this._File.Position;
			}
			catch
			{
				num = -1L;
			}
			return num;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000324C File Offset: 0x0000144C
		internal virtual int Backpatch(long fileOffset, RiffFile.RiffChunkHeader data, int numBytes)
		{
			if (this._File == null)
			{
				return 4;
			}
			try
			{
				this._File.Seek(fileOffset, 0);
			}
			catch
			{
				return 3;
			}
			return this.Write(data, numBytes);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003294 File Offset: 0x00001494
		internal virtual int Backpatch(long fileOffset, sbyte[] data, int numBytes)
		{
			if (this._File == null)
			{
				return 4;
			}
			try
			{
				this._File.Seek(fileOffset, 0);
			}
			catch
			{
				return 3;
			}
			return this.Write(data, numBytes);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000032DC File Offset: 0x000014DC
		protected virtual int Seek(long offset)
		{
			int num;
			try
			{
				this._File.Seek(offset, 0);
				num = 0;
			}
			catch
			{
				num = 3;
			}
			return num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003314 File Offset: 0x00001514
		internal static int FourCC(string chunkName)
		{
			sbyte[] array = new sbyte[] { 32, 32, 32, 32 };
			SupportClass.GetSBytesFromString(chunkName, 0, 4, ref array, 0);
			return (((int)array[0] << 24) & (int)SupportClass.Identity((long)((ulong)(-16777216)))) | (((int)array[1] << 16) & 16711680) | (((int)array[2] << 8) & 65280) | ((int)array[3] & 255);
		}

		// Token: 0x04000013 RID: 19
		protected const int DDC_SUCCESS = 0;

		// Token: 0x04000014 RID: 20
		protected const int DDC_FAILURE = 1;

		// Token: 0x04000015 RID: 21
		protected const int DDC_OUT_OF_MEMORY = 2;

		// Token: 0x04000016 RID: 22
		protected const int DDC_FILE_ERROR = 3;

		// Token: 0x04000017 RID: 23
		protected const int DDC_INVALID_CALL = 4;

		// Token: 0x04000018 RID: 24
		protected const int DDC_USER_ABORT = 5;

		// Token: 0x04000019 RID: 25
		protected const int DDC_INVALID_FILE = 6;

		// Token: 0x0400001A RID: 26
		protected const int RF_UNKNOWN = 0;

		// Token: 0x0400001B RID: 27
		protected const int RF_WRITE = 1;

		// Token: 0x0400001C RID: 28
		protected const int RF_READ = 2;

		// Token: 0x0400001D RID: 29
		private readonly RiffFile.RiffChunkHeader _RiffHeader;

		// Token: 0x0400001E RID: 30
		protected int Fmode;

		// Token: 0x0400001F RID: 31
		private Stream _File;

		// Token: 0x0200002F RID: 47
		public class RiffChunkHeader
		{
			// Token: 0x06000168 RID: 360 RVA: 0x0001B56C File Offset: 0x0001976C
			internal RiffChunkHeader(RiffFile enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
			}

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000169 RID: 361 RVA: 0x0001B57B File Offset: 0x0001977B
			internal RiffFile EnclosingInstance
			{
				get
				{
					return this._EnclosingInstance;
				}
			}

			// Token: 0x0600016A RID: 362 RVA: 0x0001B583 File Offset: 0x00019783
			private void InitBlock(RiffFile enclosingInstance)
			{
				this._EnclosingInstance = enclosingInstance;
			}

			// Token: 0x040001D7 RID: 471
			internal int CkId;

			// Token: 0x040001D8 RID: 472
			internal int CkSize;

			// Token: 0x040001D9 RID: 473
			private RiffFile _EnclosingInstance;
		}
	}
}
