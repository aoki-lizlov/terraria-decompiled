using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000470 RID: 1136
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		// Token: 0x06002F02 RID: 12034 RVA: 0x000025BE File Offset: 0x000007BE
		protected RandomNumberGenerator()
		{
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000A8DAE File Offset: 0x000A6FAE
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000A8DBA File Offset: 0x000A6FBA
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000A8DC7 File Offset: 0x000A6FC7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002F07 RID: 12039
		public abstract void GetBytes(byte[] data);

		// Token: 0x06002F08 RID: 12040 RVA: 0x000A8DD8 File Offset: 0x000A6FD8
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000174FB File Offset: 0x000156FB
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000A8E59 File Offset: 0x000A7059
		public static void Fill(Span<byte> data)
		{
			RandomNumberGenerator.FillSpan(data);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000A8E64 File Offset: 0x000A7064
		internal unsafe static void FillSpan(Span<byte> data)
		{
			if (data.Length > 0)
			{
				fixed (byte* pinnableReference = data.GetPinnableReference())
				{
					Interop.GetRandomBytes(pinnableReference, data.Length);
				}
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000A8E94 File Offset: 0x000A7094
		public virtual void GetBytes(Span<byte> data)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			try
			{
				this.GetBytes(array, 0, data.Length);
				new ReadOnlySpan<byte>(array, 0, data.Length).CopyTo(data);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000A8F08 File Offset: 0x000A7108
		public virtual void GetNonZeroBytes(Span<byte> data)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(data.Length);
			try
			{
				this.GetNonZeroBytes(array);
				new ReadOnlySpan<byte>(array, 0, data.Length).CopyTo(data);
			}
			finally
			{
				Array.Clear(array, 0, data.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000A8F74 File Offset: 0x000A7174
		public unsafe static int GetInt32(int fromInclusive, int toExclusive)
		{
			if (fromInclusive >= toExclusive)
			{
				throw new ArgumentException("Range of random number does not contain at least one possibility.");
			}
			uint num = (uint)(toExclusive - fromInclusive - 1);
			if (num == 0U)
			{
				return fromInclusive;
			}
			uint num2 = num;
			num2 |= num2 >> 1;
			num2 |= num2 >> 2;
			num2 |= num2 >> 4;
			num2 |= num2 >> 8;
			num2 |= num2 >> 16;
			Span<uint> span = new Span<uint>(stackalloc byte[(UIntPtr)4], 1);
			uint num3;
			do
			{
				RandomNumberGenerator.FillSpan(MemoryMarshal.AsBytes<uint>(span));
				num3 = num2 & *span[0];
			}
			while (num3 > num);
			return (int)(num3 + (uint)fromInclusive);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000A8FE5 File Offset: 0x000A71E5
		public static int GetInt32(int toExclusive)
		{
			if (toExclusive <= 0)
			{
				throw new ArgumentOutOfRangeException("toExclusive", "Positive number required.");
			}
			return RandomNumberGenerator.GetInt32(0, toExclusive);
		}
	}
}
