using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000B21 RID: 2849
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x060068AD RID: 26797 RVA: 0x0016336E File Offset: 0x0016156E
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x00163374 File Offset: 0x00161574
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x00163380 File Offset: 0x00161580
		[SecuritySafeCritical]
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Count must be positive and count must refer to a location within the string/array/collection."));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (count == 0)
			{
				return -1;
			}
			byte* ptr;
			if (array == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Buffer.IndexOfByte(ptr, value, startIndex, count);
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x00163410 File Offset: 0x00161610
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x00163439 File Offset: 0x00161639
		public override bool Equals(object obj)
		{
			return obj is ByteEqualityComparer;
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x00163444 File Offset: 0x00161644
		public ByteEqualityComparer()
		{
		}
	}
}
