using System;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x0200000C RID: 12
	internal sealed class BitReserve
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000037D5 File Offset: 0x000019D5
		internal BitReserve()
		{
			this.InitBlock();
			this._Offset = 0;
			this._Totbit = 0;
			this._BufByteIdx = 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037F8 File Offset: 0x000019F8
		private void InitBlock()
		{
			this._Buffer = new int[32768];
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000380A File Offset: 0x00001A0A
		internal int HssTell()
		{
			return this._Totbit;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003814 File Offset: 0x00001A14
		internal int ReadBits(int n)
		{
			this._Totbit += n;
			int num = 0;
			int num2 = this._BufByteIdx;
			if (num2 + n < 32768)
			{
				while (n-- > 0)
				{
					num <<= 1;
					num |= ((this._Buffer[num2++] != 0) ? 1 : 0);
				}
			}
			else
			{
				while (n-- > 0)
				{
					num <<= 1;
					num |= ((this._Buffer[num2] != 0) ? 1 : 0);
					num2 = (num2 + 1) & 32767;
				}
			}
			this._BufByteIdx = num2;
			return num;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003898 File Offset: 0x00001A98
		internal int ReadOneBit()
		{
			this._Totbit++;
			int num = this._Buffer[this._BufByteIdx];
			this._BufByteIdx = (this._BufByteIdx + 1) & 32767;
			return num;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000038CC File Offset: 0x00001ACC
		internal void PutBuffer(int val)
		{
			int offset = this._Offset;
			this._Buffer[offset++] = val & 128;
			this._Buffer[offset++] = val & 64;
			this._Buffer[offset++] = val & 32;
			this._Buffer[offset++] = val & 16;
			this._Buffer[offset++] = val & 8;
			this._Buffer[offset++] = val & 4;
			this._Buffer[offset++] = val & 2;
			this._Buffer[offset++] = val & 1;
			if (offset == 32768)
			{
				this._Offset = 0;
				return;
			}
			this._Offset = offset;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003976 File Offset: 0x00001B76
		internal void RewindStreamBits(int bitCount)
		{
			this._Totbit -= bitCount;
			this._BufByteIdx -= bitCount;
			if (this._BufByteIdx < 0)
			{
				this._BufByteIdx += 32768;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000039B0 File Offset: 0x00001BB0
		internal void RewindStreamBytes(int byteCount)
		{
			int num = byteCount << 3;
			this._Totbit -= num;
			this._BufByteIdx -= num;
			if (this._BufByteIdx < 0)
			{
				this._BufByteIdx += 32768;
			}
		}

		// Token: 0x0400002C RID: 44
		private const int BUFSIZE = 32768;

		// Token: 0x0400002D RID: 45
		private const int BUFSIZE_MASK = 32767;

		// Token: 0x0400002E RID: 46
		private int[] _Buffer;

		// Token: 0x0400002F RID: 47
		private int _Offset;

		// Token: 0x04000030 RID: 48
		private int _Totbit;

		// Token: 0x04000031 RID: 49
		private int _BufByteIdx;
	}
}
