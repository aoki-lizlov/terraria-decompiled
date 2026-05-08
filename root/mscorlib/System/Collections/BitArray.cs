using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A8E RID: 2702
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060062E3 RID: 25315 RVA: 0x0015114B File Offset: 0x0014F34B
		public BitArray(int length)
			: this(length, false)
		{
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x00151158 File Offset: 0x0014F358
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Non-negative number required.");
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = (defaultValue ? (-1) : 0);
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x001511CC File Offset: 0x0014F3CC
		public BitArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length > 268435455)
			{
				throw new ArgumentException(SR.Format("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", 8), "bytes");
			}
			this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
			this.m_length = bytes.Length * 8;
			int num = 0;
			int num2 = 0;
			while (bytes.Length - num2 >= 4)
			{
				this.m_array[num++] = (int)(bytes[num2] & byte.MaxValue) | ((int)(bytes[num2 + 1] & byte.MaxValue) << 8) | ((int)(bytes[num2 + 2] & byte.MaxValue) << 16) | ((int)(bytes[num2 + 3] & byte.MaxValue) << 24);
				num2 += 4;
			}
			switch (bytes.Length - num2)
			{
			case 1:
				goto IL_00FA;
			case 2:
				break;
			case 3:
				this.m_array[num] = (int)(bytes[num2 + 2] & byte.MaxValue) << 16;
				break;
			default:
				goto IL_0113;
			}
			this.m_array[num] |= (int)(bytes[num2 + 1] & byte.MaxValue) << 8;
			IL_00FA:
			this.m_array[num] |= (int)(bytes[num2] & byte.MaxValue);
			IL_0113:
			this._version = 0;
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x001512F4 File Offset: 0x0014F4F4
		public BitArray(bool[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
			this.m_length = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i])
				{
					this.m_array[i / 32] |= 1 << i % 32;
				}
			}
			this._version = 0;
		}

		// Token: 0x060062E7 RID: 25319 RVA: 0x0015136C File Offset: 0x0014F56C
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(SR.Format("The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.", 32), "values");
			}
			this.m_array = new int[values.Length];
			Array.Copy(values, 0, this.m_array, 0, values.Length);
			this.m_length = values.Length * 32;
			this._version = 0;
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x001513E8 File Offset: 0x0014F5E8
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			Array.Copy(bits.m_array, 0, this.m_array, 0, arrayLength);
			this.m_length = bits.m_length;
			this._version = bits._version;
		}

		// Token: 0x170010F6 RID: 4342
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x00151462 File Offset: 0x0014F662
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (this.m_array[index / 32] & (1 << index % 32)) != 0;
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x001514A0 File Offset: 0x0014F6A0
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", index, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x0015151C File Offset: 0x0014F71C
		public void SetAll(bool value)
		{
			int num = (value ? (-1) : 0);
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x00151564 File Offset: 0x0014F764
		public BitArray And(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] &= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x001515DC File Offset: 0x0014F7DC
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x00151654 File Offset: 0x0014F854
		public BitArray Xor(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException("Array lengths must be the same.");
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] ^= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060062F1 RID: 25329 RVA: 0x001516CC File Offset: 0x0014F8CC
		public BitArray Not()
		{
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = ~this.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060062F2 RID: 25330 RVA: 0x00151714 File Offset: 0x0014F914
		public BitArray RightShift(int count)
		{
			if (count > 0)
			{
				int num = 0;
				int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
				if (count < this.m_length)
				{
					int i = count / 32;
					int num2 = count - i * 32;
					if (num2 == 0)
					{
						uint num3 = uint.MaxValue >> 32 - this.m_length % 32;
						this.m_array[arrayLength - 1] &= (int)num3;
						Array.Copy(this.m_array, i, this.m_array, 0, arrayLength - i);
						num = arrayLength - i;
					}
					else
					{
						int num4 = arrayLength - 1;
						while (i < num4)
						{
							uint num5 = (uint)this.m_array[i] >> num2;
							int num6 = this.m_array[++i] << ((32 - num2) & 31);
							this.m_array[num++] = num6 | (int)num5;
						}
						uint num7 = uint.MaxValue >> 32 - this.m_length % 32;
						num7 &= (uint)this.m_array[i];
						this.m_array[num++] = (int)(num7 >> num2);
					}
				}
				Array.Clear(this.m_array, num, arrayLength - num);
				this._version++;
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non-negative number required.");
			}
			this._version++;
			return this;
		}

		// Token: 0x060062F3 RID: 25331 RVA: 0x00151850 File Offset: 0x0014FA50
		public BitArray LeftShift(int count)
		{
			if (count > 0)
			{
				int num2;
				if (count < this.m_length)
				{
					int num = (this.m_length - 1) / 32;
					num2 = count / 32;
					int num3 = count - num2 * 32;
					if (num3 == 0)
					{
						Array.Copy(this.m_array, 0, this.m_array, num2, num + 1 - num2);
					}
					else
					{
						int i = num - num2;
						while (i > 0)
						{
							int num4 = this.m_array[i] << num3;
							uint num5 = (uint)this.m_array[--i] >> ((32 - num3) & 31);
							this.m_array[num] = num4 | (int)num5;
							num--;
						}
						this.m_array[num] = this.m_array[i] << num3;
					}
				}
				else
				{
					num2 = BitArray.GetArrayLength(this.m_length, 32);
				}
				Array.Clear(this.m_array, 0, num2);
				this._version++;
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non-negative number required.");
			}
			this._version++;
			return this;
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060062F4 RID: 25332 RVA: 0x0015194D File Offset: 0x0014FB4D
		// (set) Token: 0x060062F5 RID: 25333 RVA: 0x00151958 File Offset: 0x0014FB58
		public int Length
		{
			get
			{
				return this.m_length;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					Array.Resize<int>(ref this.m_array, arrayLength);
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		// Token: 0x060062F6 RID: 25334 RVA: 0x00151A18 File Offset: 0x0014FC18
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			int[] array2 = array as int[];
			if (array2 != null)
			{
				int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
				int num2 = this.m_length % 32;
				if (num2 == 0)
				{
					Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32));
					return;
				}
				Array.Copy(this.m_array, 0, array2, index, BitArray.GetArrayLength(this.m_length, 32) - 1);
				array2[index + num] = this.m_array[num] & ((1 << num2) - 1);
				return;
			}
			else if (array is byte[])
			{
				int num3 = this.m_length % 8;
				int num4 = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < num4)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				if (num3 > 0)
				{
					num4--;
				}
				byte[] array3 = (byte[])array;
				for (int i = 0; i < num4; i++)
				{
					array3[index + i] = (byte)((this.m_array[i / 4] >> i % 4 * 8) & 255);
				}
				if (num3 > 0)
				{
					int num5 = num4;
					array3[index + num5] = (byte)((this.m_array[num5 / 4] >> num5 % 4 * 8) & ((1 << num3) - 1));
					return;
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException("Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].", "array");
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
				}
				bool[] array4 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array4[index + j] = ((this.m_array[j / 32] >> j % 32) & 1) != 0;
				}
				return;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x0015194D File Offset: 0x0014FB4D
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060062F8 RID: 25336 RVA: 0x00151C00 File Offset: 0x0014FE00
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060062FA RID: 25338 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060062FB RID: 25339 RVA: 0x00151C22 File Offset: 0x0014FE22
		public object Clone()
		{
			return new BitArray(this);
		}

		// Token: 0x060062FC RID: 25340 RVA: 0x00151C2A File Offset: 0x0014FE2A
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x060062FD RID: 25341 RVA: 0x00151C32 File Offset: 0x0014FE32
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x04003AEF RID: 15087
		private int[] m_array;

		// Token: 0x04003AF0 RID: 15088
		private int m_length;

		// Token: 0x04003AF1 RID: 15089
		private int _version;

		// Token: 0x04003AF2 RID: 15090
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003AF3 RID: 15091
		private const int _ShrinkThreshold = 256;

		// Token: 0x04003AF4 RID: 15092
		private const int BitsPerInt32 = 32;

		// Token: 0x04003AF5 RID: 15093
		private const int BytesPerInt32 = 4;

		// Token: 0x04003AF6 RID: 15094
		private const int BitsPerByte = 8;

		// Token: 0x02000A8F RID: 2703
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x060062FE RID: 25342 RVA: 0x00151C41 File Offset: 0x0014FE41
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x060062FF RID: 25343 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006300 RID: 25344 RVA: 0x00151C64 File Offset: 0x0014FE64
			public virtual bool MoveNext()
			{
				ICollection collection = this.bitarray;
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.index < collection.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = collection.Count;
				return false;
			}

			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x06006301 RID: 25345 RVA: 0x00151CDA File Offset: 0x0014FEDA
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this.index >= ((ICollection)this.bitarray).Count)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006302 RID: 25346 RVA: 0x00151D19 File Offset: 0x0014FF19
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.index = -1;
			}

			// Token: 0x04003AF7 RID: 15095
			private BitArray bitarray;

			// Token: 0x04003AF8 RID: 15096
			private int index;

			// Token: 0x04003AF9 RID: 15097
			private int version;

			// Token: 0x04003AFA RID: 15098
			private bool currentElement;
		}
	}
}
