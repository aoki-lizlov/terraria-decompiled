using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x02000058 RID: 88
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	[ComVisible(true)]
	public class CRC32
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001CAF5 File Offset: 0x0001ACF5
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int num = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, num);
			if (output != null)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, num);
				if (output != null)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001CB9E File Offset: 0x0001AD9E
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((UIntPtr)((W ^ (uint)B) & 255U))] ^ (W >> 8));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (this.reverseBits)
				{
					uint num2 = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)num2)];
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)num3)];
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001CC50 File Offset: 0x0001AE50
		public void UpdateCRC(byte b)
		{
			if (this.reverseBits)
			{
				uint num = (this._register >> 24) ^ (uint)b;
				this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)num)];
				return;
			}
			uint num2 = (this._register & 255U) ^ (uint)b;
			this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)num2)];
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (this.reverseBits)
				{
					uint num = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)((num >= 0U) ? num : (num + 256U)))];
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)((num2 >= 0U) ? num2 : (num2 + 256U)))];
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001CD3C File Offset: 0x0001AF3C
		private static uint ReverseBits(uint data)
		{
			uint num = ((data & 1431655765U) << 1) | ((data >> 1) & 1431655765U);
			num = ((num & 858993459U) << 2) | ((num >> 2) & 858993459U);
			num = ((num & 252645135U) << 4) | ((num >> 4) & 252645135U);
			return (num << 24) | ((num & 65280U) << 8) | ((num >> 8) & 65280U) | (num >> 24);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		private static byte ReverseBits(byte data)
		{
			uint num = (uint)data * 131586U;
			uint num2 = 17055760U;
			uint num3 = num & num2;
			uint num4 = (num << 2) & (num2 << 1);
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					if ((num & 1U) == 1U)
					{
						num = (num >> 1) ^ this.dwPolynomial;
					}
					else
					{
						num >>= 1;
					}
				}
				if (this.reverseBits)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b != 0);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001CE50 File Offset: 0x0001B050
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec != 0U)
			{
				if ((vec & 1U) == 1U)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001CE7C File Offset: 0x0001B07C
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001CEA4 File Offset: 0x0001B0A4
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~this._register;
			array2[0] = this.dwPolynomial;
			uint num2 = 1U;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			this.gf2_matrix_square(array, array2);
			this.gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				this.gf2_matrix_square(array, array2);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0U)
				{
					break;
				}
				this.gf2_matrix_square(array2, array);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0U);
			num ^= (uint)crc;
			this._register = ~num;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001CF5B File Offset: 0x0001B15B
		public CRC32()
			: this(false)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001CF64 File Offset: 0x0001B164
		public CRC32(bool reverseBits)
			: this(-306674912, reverseBits)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001CF72 File Offset: 0x0001B172
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001CF95 File Offset: 0x0001B195
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x0400030D RID: 781
		private const int BUFFER_SIZE = 8192;

		// Token: 0x0400030E RID: 782
		private uint dwPolynomial;

		// Token: 0x0400030F RID: 783
		private long _TotalBytesRead;

		// Token: 0x04000310 RID: 784
		private bool reverseBits;

		// Token: 0x04000311 RID: 785
		private uint[] crc32Table;

		// Token: 0x04000312 RID: 786
		private uint _register = uint.MaxValue;
	}
}
