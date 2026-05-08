using System;
using XPT.Core.Audio.MP3Sharp.Support;

namespace XPT.Core.Audio.MP3Sharp.Decoding
{
	// Token: 0x02000011 RID: 17
	public sealed class Crc16
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004524 File Offset: 0x00002724
		static Crc16()
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004537 File Offset: 0x00002737
		internal Crc16()
		{
			this._CRC = (short)SupportClass.Identity(65535L);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004554 File Offset: 0x00002754
		internal void AddBits(int bitstring, int length)
		{
			int num = 1 << length - 1;
			do
			{
				if ((((int)this._CRC & 32768) == 0) ^ ((bitstring & num) == 0))
				{
					this._CRC = (short)(this._CRC << 1);
					this._CRC ^= Crc16.Polynomial;
				}
				else
				{
					this._CRC = (short)(this._CRC << 1);
				}
			}
			while ((num = SupportClass.URShift(num, 1)) != 0);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000045C0 File Offset: 0x000027C0
		internal short Checksum()
		{
			short crc = this._CRC;
			this._CRC = (short)SupportClass.Identity(65535L);
			return crc;
		}

		// Token: 0x0400004E RID: 78
		private static readonly short Polynomial = (short)SupportClass.Identity(32773L);

		// Token: 0x0400004F RID: 79
		private short _CRC;
	}
}
