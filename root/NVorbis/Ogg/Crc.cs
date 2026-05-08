using System;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001B RID: 27
	internal class Crc : ICrc
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000676C File Offset: 0x0000496C
		static Crc()
		{
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num << 24;
				for (int i = 0; i < 8; i++)
				{
					num2 = (num2 << 1) ^ ((num2 >= 2147483648U) ? 79764919U : 0U);
				}
				Crc.s_crcTable[(int)num] = num2;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000067C6 File Offset: 0x000049C6
		public Crc()
		{
			this.Reset();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000067D4 File Offset: 0x000049D4
		public void Reset()
		{
			this._crc = 0U;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000067DD File Offset: 0x000049DD
		public void Update(int nextVal)
		{
			checked
			{
				this._crc = (this._crc << 8) ^ Crc.s_crcTable[(int)((IntPtr)(unchecked((long)nextVal ^ (long)((ulong)(this._crc >> 24)))))];
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006802 File Offset: 0x00004A02
		public bool Test(uint checkCrc)
		{
			return this._crc == checkCrc;
		}

		// Token: 0x0400008D RID: 141
		private const uint CRC32_POLY = 79764919U;

		// Token: 0x0400008E RID: 142
		private static readonly uint[] s_crcTable = new uint[256];

		// Token: 0x0400008F RID: 143
		private uint _crc;
	}
}
