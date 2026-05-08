using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x02000021 RID: 33
	internal abstract class PageReaderBase : IPageReader, IDisposable
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007A0C File Offset: 0x00005C0C
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00007A13 File Offset: 0x00005C13
		internal static Func<ICrc> CreateCrc
		{
			[CompilerGenerated]
			get
			{
				return PageReaderBase.<CreateCrc>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				PageReaderBase.<CreateCrc>k__BackingField = value;
			}
		} = () => new Crc();

		// Token: 0x06000173 RID: 371 RVA: 0x00007A1C File Offset: 0x00005C1C
		protected PageReaderBase(Stream stream, bool closeOnDispose)
		{
			this._stream = stream;
			this._closeOnDispose = closeOnDispose;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007A68 File Offset: 0x00005C68
		protected long StreamPosition
		{
			get
			{
				Stream stream = this._stream;
				if (stream == null)
				{
					throw new ObjectDisposedException("PageReaderBase");
				}
				return stream.Position;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007A84 File Offset: 0x00005C84
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00007A8C File Offset: 0x00005C8C
		public long ContainerBits
		{
			[CompilerGenerated]
			get
			{
				return this.<ContainerBits>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContainerBits>k__BackingField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00007A95 File Offset: 0x00005C95
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00007A9D File Offset: 0x00005C9D
		public long WasteBits
		{
			[CompilerGenerated]
			get
			{
				return this.<WasteBits>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WasteBits>k__BackingField = value;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007AA8 File Offset: 0x00005CA8
		private bool VerifyPage(byte[] headerBuf, int index, int cnt, out byte[] pageBuf, out int bytesRead)
		{
			byte b = headerBuf[index + 26];
			if (cnt - index < index + 27 + (int)b)
			{
				pageBuf = null;
				bytesRead = 0;
				return false;
			}
			int num = 0;
			int i;
			for (i = 0; i < (int)b; i++)
			{
				num += (int)headerBuf[index + i + 27];
			}
			pageBuf = new byte[num + (int)b + 27];
			Buffer.BlockCopy(headerBuf, index, pageBuf, 0, (int)(b + 27));
			bytesRead = this.EnsureRead(pageBuf, (int)(b + 27), num, 10);
			if (bytesRead != num)
			{
				return false;
			}
			num = pageBuf.Length;
			this._crc.Reset();
			for (i = 0; i < 22; i++)
			{
				this._crc.Update((int)pageBuf[i]);
			}
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			this._crc.Update(0);
			for (i += 4; i < num; i++)
			{
				this._crc.Update((int)pageBuf[i]);
			}
			return this._crc.Test(BitConverter.ToUInt32(pageBuf, 22));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007BB4 File Offset: 0x00005DB4
		private bool AddPage(byte[] pageBuf, bool isResync)
		{
			int num = BitConverter.ToInt32(pageBuf, 14);
			if (!this._ignoredSerials.Contains(num))
			{
				if (this.AddPage(num, pageBuf, isResync))
				{
					this.ContainerBits += (long)(8 * (27 + pageBuf[26]));
					return true;
				}
				this._ignoredSerials.Add(num);
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007C0C File Offset: 0x00005E0C
		private void EnqueueData(byte[] buf, int count)
		{
			if (this._overflowBuf != null)
			{
				byte[] array = new byte[this._overflowBuf.Length - this._overflowBufIndex + count];
				Buffer.BlockCopy(this._overflowBuf, this._overflowBufIndex, array, 0, array.Length - count);
				int num = buf.Length - count;
				Buffer.BlockCopy(buf, num, array, array.Length - count, count);
				this._overflowBufIndex = 0;
				return;
			}
			this._overflowBuf = buf;
			this._overflowBufIndex = buf.Length - count;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007C80 File Offset: 0x00005E80
		private void ClearEnqueuedData(int count)
		{
			if (this._overflowBuf != null && (this._overflowBufIndex += count) >= this._overflowBuf.Length)
			{
				this._overflowBuf = null;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007CB8 File Offset: 0x00005EB8
		private int FillHeader(byte[] buf, int index, int count, int maxTries = 10)
		{
			int num = 0;
			if (this._overflowBuf != null)
			{
				num = Math.Min(this._overflowBuf.Length - this._overflowBufIndex, count);
				Buffer.BlockCopy(this._overflowBuf, this._overflowBufIndex, buf, index, num);
				index += num;
				count -= num;
				if ((this._overflowBufIndex += num) == this._overflowBuf.Length)
				{
					this._overflowBuf = null;
				}
			}
			if (count > 0)
			{
				num += this.EnsureRead(buf, index, count, maxTries);
			}
			return num;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007D38 File Offset: 0x00005F38
		private bool VerifyHeader(byte[] buffer, int index, ref int cnt, bool isFromReadNextPage)
		{
			if (buffer[index] == 79 && buffer[index + 1] == 103 && buffer[index + 2] == 103 && buffer[index + 3] == 83)
			{
				if (cnt < 27)
				{
					if (isFromReadNextPage)
					{
						cnt += this.FillHeader(buffer, 27 - cnt + index, 27 - cnt, 10);
					}
					else
					{
						cnt += this.EnsureRead(buffer, 27 - cnt + index, 27 - cnt, 10);
					}
				}
				if (cnt >= 27)
				{
					byte b = buffer[index + 26];
					if (isFromReadNextPage)
					{
						cnt += this.FillHeader(buffer, index + 27, (int)b, 10);
					}
					else
					{
						cnt += this.EnsureRead(buffer, index + 27, (int)b, 10);
					}
					if (cnt == index + 27 + (int)b)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007DFC File Offset: 0x00005FFC
		protected int EnsureRead(byte[] buf, int index, int count, int maxTries = 10)
		{
			int num = 0;
			int num2 = 0;
			do
			{
				int num3 = this._stream.Read(buf, index + num, count - num);
				if (num3 == 0 && ++num2 == maxTries)
				{
					break;
				}
				num += num3;
			}
			while (num < count);
			return num;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007E35 File Offset: 0x00006035
		protected bool VerifyHeader(byte[] buffer, int index, ref int cnt)
		{
			return this.VerifyHeader(buffer, index, ref cnt, false);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007E41 File Offset: 0x00006041
		protected long SeekStream(long offset)
		{
			if (!this.CheckLock())
			{
				throw new InvalidOperationException("Must be locked prior to reading!");
			}
			return this._stream.Seek(offset, 0);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007E63 File Offset: 0x00006063
		protected virtual void PrepareStreamForNextPage()
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007E65 File Offset: 0x00006065
		protected virtual void SaveNextPageSearch()
		{
		}

		// Token: 0x06000184 RID: 388
		protected abstract bool AddPage(int streamSerial, byte[] pageBuf, bool isResync);

		// Token: 0x06000185 RID: 389
		protected abstract void SetEndOfStreams();

		// Token: 0x06000186 RID: 390 RVA: 0x00007E67 File Offset: 0x00006067
		public virtual void Lock()
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007E69 File Offset: 0x00006069
		protected virtual bool CheckLock()
		{
			return true;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007E6C File Offset: 0x0000606C
		public virtual bool Release()
		{
			return false;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007E70 File Offset: 0x00006070
		public bool ReadNextPage()
		{
			if (!this.CheckLock())
			{
				throw new InvalidOperationException("Must be locked prior to reading!");
			}
			bool flag = false;
			int num = 0;
			this.PrepareStreamForNextPage();
			int num2;
			while ((num2 = this.FillHeader(this._headerBuf, num, 27 - num, 10)) > 0)
			{
				num2 += num;
				for (int i = 0; i < num2 - 4; i++)
				{
					if (this.VerifyHeader(this._headerBuf, i, ref num2, true))
					{
						byte[] array;
						int num3;
						if (this.VerifyPage(this._headerBuf, i, num2, out array, out num3))
						{
							this.ClearEnqueuedData(num3);
							this.SaveNextPageSearch();
							if (this.AddPage(array, flag))
							{
								return true;
							}
							this.WasteBits += (long)(array.Length * 8);
							num = 0;
							num2 = 0;
							break;
						}
						else if (array != null)
						{
							this.EnqueueData(array, num3);
						}
					}
					this.WasteBits += 8L;
					flag = true;
				}
				if (num2 >= 3)
				{
					this._headerBuf[0] = this._headerBuf[num2 - 3];
					this._headerBuf[1] = this._headerBuf[num2 - 2];
					this._headerBuf[2] = this._headerBuf[num2 - 1];
					num = 3;
				}
			}
			if (num2 == 0)
			{
				this.SetEndOfStreams();
			}
			return false;
		}

		// Token: 0x0600018A RID: 394
		public abstract bool ReadPageAt(long offset);

		// Token: 0x0600018B RID: 395 RVA: 0x00007F8F File Offset: 0x0000618F
		public void Dispose()
		{
			this.SetEndOfStreams();
			if (this._closeOnDispose)
			{
				Stream stream = this._stream;
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			this._stream = null;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007FB7 File Offset: 0x000061B7
		// Note: this type is marked as 'beforefieldinit'.
		static PageReaderBase()
		{
		}

		// Token: 0x040000BD RID: 189
		[CompilerGenerated]
		private static Func<ICrc> <CreateCrc>k__BackingField;

		// Token: 0x040000BE RID: 190
		private readonly ICrc _crc = PageReaderBase.CreateCrc.Invoke();

		// Token: 0x040000BF RID: 191
		private readonly HashSet<int> _ignoredSerials = new HashSet<int>();

		// Token: 0x040000C0 RID: 192
		private readonly byte[] _headerBuf = new byte[305];

		// Token: 0x040000C1 RID: 193
		private byte[] _overflowBuf;

		// Token: 0x040000C2 RID: 194
		private int _overflowBufIndex;

		// Token: 0x040000C3 RID: 195
		private Stream _stream;

		// Token: 0x040000C4 RID: 196
		private bool _closeOnDispose;

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private long <ContainerBits>k__BackingField;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		private long <WasteBits>k__BackingField;

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000291 RID: 657 RVA: 0x00009C73 File Offset: 0x00007E73
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000292 RID: 658 RVA: 0x00009C7F File Offset: 0x00007E7F
			public <>c()
			{
			}

			// Token: 0x06000293 RID: 659 RVA: 0x00009C87 File Offset: 0x00007E87
			internal ICrc <.cctor>b__41_0()
			{
				return new Crc();
			}

			// Token: 0x0400010E RID: 270
			public static readonly PageReaderBase.<>c <>9 = new PageReaderBase.<>c();
		}
	}
}
