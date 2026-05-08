using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001A7 RID: 423
	[Serializable]
	public abstract class Array : ICollection, IEnumerable, IList, IStructuralComparable, IStructuralEquatable, ICloneable
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x0004F70C File Offset: 0x0004D90C
		public static Array CreateInstance(Type elementType, params long[] lengths)
		{
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length == 0)
			{
				throw new ArgumentException("Must provide at least one rank.");
			}
			int[] array = new int[lengths.Length];
			for (int i = 0; i < lengths.Length; i++)
			{
				long num = lengths[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("len", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004F77F File Offset: 0x0004D97F
		public static ReadOnlyCollection<T> AsReadOnly<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return new ReadOnlyCollection<T>(array);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004F798 File Offset: 0x0004D998
		public static void Resize<T>(ref T[] array, int newSize)
		{
			if (newSize < 0)
			{
				throw new ArgumentOutOfRangeException("newSize", "Non-negative number required.");
			}
			T[] array2 = array;
			if (array2 == null)
			{
				array = new T[newSize];
				return;
			}
			if (array2.Length != newSize)
			{
				T[] array3 = new T[newSize];
				Array.Copy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
				array = array3;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0004F7ED File Offset: 0x0004D9ED
		int ICollection.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0000408A File Offset: 0x0000228A
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E3 RID: 483
		object IList.this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
			set
			{
				this.SetValue(value, index);
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004F808 File Offset: 0x0004DA08
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004F814 File Offset: 0x0004DA14
		bool IList.Contains(object value)
		{
			return Array.IndexOf(this, value) >= 0;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004F823 File Offset: 0x0004DA23
		void IList.Clear()
		{
			Array.Clear(this, this.GetLowerBound(0), this.Length);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004F838 File Offset: 0x0004DA38
		int IList.IndexOf(object value)
		{
			return Array.IndexOf(this, value);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004F808 File Offset: 0x0004DA08
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0004F808 File Offset: 0x0004DA08
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004F808 File Offset: 0x0004DA08
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0004F841 File Offset: 0x0004DA41
		public void CopyTo(Array array, int index)
		{
			if (array != null && array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			Array.Copy(this, this.GetLowerBound(0), array, index, this.Length);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0001AB5D File Offset: 0x00018D5D
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0004F870 File Offset: 0x0004DA70
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Array array = other as Array;
			if (array == null || this.Length != array.Length)
			{
				throw new ArgumentException("Object is not a array with the same number of elements as the array to compare it to.", "other");
			}
			int num = 0;
			int num2 = 0;
			while (num < array.Length && num2 == 0)
			{
				object value = this.GetValue(num);
				object value2 = array.GetValue(num);
				num2 = comparer.Compare(value, value2);
				num++;
			}
			return num2;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0004F8DC File Offset: 0x0004DADC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Array array = other as Array;
			if (array == null || array.Length != this.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				object value = this.GetValue(i);
				object value2 = array.GetValue(i);
				if (!comparer.Equals(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0002B7DF File Offset: 0x000299DF
		internal static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0004F93C File Offset: 0x0004DB3C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			int num = 0;
			for (int i = ((this.Length >= 8) ? (this.Length - 8) : 0); i < this.Length; i++)
			{
				num = Array.CombineHashCodes(num, comparer.GetHashCode(this.GetValue(i)));
			}
			return num;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004F992 File Offset: 0x0004DB92
		public static int BinarySearch(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, null);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		public static TOutput[] ConvertAll<TInput, TOutput>(TInput[] array, Converter<TInput, TOutput> converter)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			TOutput[] array2 = new TOutput[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004FA0D File Offset: 0x0004DC0D
		public static void Copy(Array sourceArray, Array destinationArray, long length)
		{
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", "Arrays larger than 2GB are not supported.");
			}
			Array.Copy(sourceArray, destinationArray, (int)length);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004FA3C File Offset: 0x0004DC3C
		public static void Copy(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
		{
			if (sourceIndex > 2147483647L || sourceIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Arrays larger than 2GB are not supported.");
			}
			if (destinationIndex > 2147483647L || destinationIndex < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Arrays larger than 2GB are not supported.");
			}
			if (length > 2147483647L || length < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("length", "Arrays larger than 2GB are not supported.");
			}
			Array.Copy(sourceArray, (int)sourceIndex, destinationArray, (int)destinationIndex, (int)length);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004FABF File Offset: 0x0004DCBF
		public void CopyTo(Array array, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			this.CopyTo(array, (int)index);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004FAEC File Offset: 0x0004DCEC
		public static void ForEach<T>(T[] array, Action<T> action)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (int i = 0; i < array.Length; i++)
			{
				action(array[i]);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0004FB30 File Offset: 0x0004DD30
		public long LongLength
		{
			get
			{
				long num = (long)this.GetLength(0);
				for (int i = 1; i < this.Rank; i++)
				{
					num *= (long)this.GetLength(i);
				}
				return num;
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004FB63 File Offset: 0x0004DD63
		public long GetLongLength(int dimension)
		{
			return (long)this.GetLength(dimension);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004FB6D File Offset: 0x0004DD6D
		public object GetValue(long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004FB9C File Offset: 0x0004DD9C
		public object GetValue(long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index1, (int)index2);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004FBF8 File Offset: 0x0004DDF8
		public object GetValue(long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", "Arrays larger than 2GB are not supported.");
			}
			return this.GetValue((int)index1, (int)index2, (int)index3);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004FC78 File Offset: 0x0004DE78
		public object GetValue(params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException("Indices length does not match the array rank.");
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			return this.GetValue(array);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004FCF2 File Offset: 0x0004DEF2
		public static int BinarySearch(Array array, int index, int length, object value)
		{
			return Array.BinarySearch(array, index, length, value, null);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004FCFE File Offset: 0x0004DEFE
		public static int BinarySearch(Array array, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch(array, array.GetLowerBound(0), array.Length, value, comparer);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004FD24 File Offset: 0x0004DF24
		public static int BinarySearch(Array array, int index, int length, object value, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			if (comparer == null)
			{
				comparer = Comparer.Default;
			}
			int i = index;
			int num = index + length - 1;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i <= num)
				{
					int median = Array.GetMedian(i, num);
					int num2;
					try
					{
						num2 = comparer.Compare(array2[median], value);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
					}
					if (num2 == 0)
					{
						return median;
					}
					if (num2 < 0)
					{
						i = median + 1;
					}
					else
					{
						num = median - 1;
					}
				}
			}
			else
			{
				while (i <= num)
				{
					int median2 = Array.GetMedian(i, num);
					int num3;
					try
					{
						num3 = comparer.Compare(array.GetValue(median2), value);
					}
					catch (Exception ex2)
					{
						throw new InvalidOperationException("Failed to compare two elements in the array.", ex2);
					}
					if (num3 == 0)
					{
						return median2;
					}
					if (num3 < 0)
					{
						i = median2 + 1;
					}
					else
					{
						num = median2 - 1;
					}
				}
			}
			return ~i;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0004FE58 File Offset: 0x0004E058
		private static int GetMedian(int low, int hi)
		{
			return low + (hi - low >> 1);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0004FE61 File Offset: 0x0004E061
		public static int BinarySearch<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, null);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0004FE7D File Offset: 0x0004E07D
		public static int BinarySearch<T>(T[] array, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.BinarySearch<T>(array, 0, array.Length, value, comparer);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004FE99 File Offset: 0x0004E099
		public static int BinarySearch<T>(T[] array, int index, int length, T value)
		{
			return Array.BinarySearch<T>(array, index, length, value, null);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004FEA8 File Offset: 0x0004E0A8
		public static int BinarySearch<T>(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return ArraySortHelper<T>.Default.BinarySearch(array, index, length, value, comparer);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004FF09 File Offset: 0x0004E109
		public static int IndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf(array, value, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004FF30 File Offset: 0x0004E130
		public static int IndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			return Array.IndexOf(array, value, startIndex, array.Length - startIndex + lowerBound);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004FF68 File Offset: 0x0004E168
		public static int IndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			int lowerBound = array.GetLowerBound(0);
			if (startIndex < lowerBound || startIndex > array.Length + lowerBound)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count > array.Length - startIndex + lowerBound)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			object[] array2 = array as object[];
			int num = startIndex + count;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i < num; i++)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j < num; j++)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k < num; k++)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return lowerBound - 1;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00050062 File Offset: 0x0004E262
		public static int IndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOfImpl<T>(array, value, 0, array.Length);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0005007D File Offset: 0x0004E27D
		public static int IndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.IndexOf<T>(array, value, startIndex, array.Length - startIndex);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005009C File Offset: 0x0004E29C
		public static int IndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || count > array.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			return Array.IndexOfImpl<T>(array, value, startIndex, count);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000500F6 File Offset: 0x0004E2F6
		public static int LastIndexOf(Array array, object value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0005011B File Offset: 0x0004E31B
		public static int LastIndexOf(Array array, object value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf(array, value, startIndex, startIndex + 1);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00050138 File Offset: 0x0004E338
		public static int LastIndexOf(Array array, object value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return -1;
			}
			if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (count > startIndex + 1)
			{
				throw new ArgumentOutOfRangeException("endIndex", "endIndex cannot be greater than startIndex.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			object[] array2 = array as object[];
			int num = startIndex - count + 1;
			if (array2 != null)
			{
				if (value == null)
				{
					for (int i = startIndex; i >= num; i--)
					{
						if (array2[i] == null)
						{
							return i;
						}
					}
				}
				else
				{
					for (int j = startIndex; j >= num; j--)
					{
						object obj = array2[j];
						if (obj != null && obj.Equals(value))
						{
							return j;
						}
					}
				}
			}
			else
			{
				for (int k = startIndex; k >= num; k--)
				{
					object value2 = array.GetValue(k);
					if (value2 == null)
					{
						if (value == null)
						{
							return k;
						}
					}
					else if (value2.Equals(value))
					{
						return k;
					}
				}
			}
			return -1;
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00050235 File Offset: 0x0004E435
		public static int LastIndexOf<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, array.Length - 1, array.Length);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x00050254 File Offset: 0x0004E454
		public static int LastIndexOf<T>(T[] array, T value, int startIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.LastIndexOf<T>(array, value, startIndex, (array.Length == 0) ? 0 : (startIndex + 1));
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00050278 File Offset: 0x0004E478
		public static int LastIndexOf<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1 && startIndex != 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count != 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				return -1;
			}
			else
			{
				if (startIndex < 0 || startIndex >= array.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				return Array.LastIndexOfImpl<T>(array, value, startIndex, count);
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00050302 File Offset: 0x0004E502
		public static void Reverse(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse(array, array.GetLowerBound(0), array.Length);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00050328 File Offset: 0x0004E528
		public static void Reverse(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < lowerBound) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - (index - lowerBound) < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (array.Rank != 1)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			object[] array2 = array as object[];
			if (array2 != null)
			{
				Array.Reverse<object>(array2, index, length);
				return;
			}
			int i = index;
			int num = index + length - 1;
			while (i < num)
			{
				object value = array.GetValue(i);
				array.SetValue(array.GetValue(num), i);
				array.SetValue(value, num);
				i++;
				num--;
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x000503E3 File Offset: 0x0004E5E3
		public static void Reverse<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Reverse<T>(array, 0, array.Length);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00050400 File Offset: 0x0004E600
		public static void Reverse<T>(T[] array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "length", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length <= 1)
			{
				return;
			}
			ref T ptr = ref Unsafe.Add<T>(Unsafe.As<byte, T>(array.GetRawSzArrayData()), index);
			ref T ptr2 = ref Unsafe.Add<T>(Unsafe.Add<T>(ref ptr, length), -1);
			do
			{
				T t = ptr;
				ptr = ptr2;
				ptr2 = t;
				ptr = Unsafe.Add<T>(ref ptr, 1);
				ptr2 = Unsafe.Add<T>(ref ptr2, -1);
			}
			while (Unsafe.IsAddressLessThan<T>(ref ptr, ref ptr2));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x000504A9 File Offset: 0x0004E6A9
		public void SetValue(object value, long index)
		{
			if (index > 2147483647L || index < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000504D8 File Offset: 0x0004E6D8
		public void SetValue(object value, long index1, long index2)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index1, (int)index2);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00050534 File Offset: 0x0004E734
		public void SetValue(object value, long index1, long index2, long index3)
		{
			if (index1 > 2147483647L || index1 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index1", "Arrays larger than 2GB are not supported.");
			}
			if (index2 > 2147483647L || index2 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index2", "Arrays larger than 2GB are not supported.");
			}
			if (index3 > 2147483647L || index3 < -2147483648L)
			{
				throw new ArgumentOutOfRangeException("index3", "Arrays larger than 2GB are not supported.");
			}
			this.SetValue(value, (int)index1, (int)index2, (int)index3);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x000505B8 File Offset: 0x0004E7B8
		public void SetValue(object value, params long[] indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			if (this.Rank != indices.Length)
			{
				throw new ArgumentException("Indices length does not match the array rank.");
			}
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				long num = indices[i];
				if (num > 2147483647L || num < -2147483648L)
				{
					throw new ArgumentOutOfRangeException("index", "Arrays larger than 2GB are not supported.");
				}
				array[i] = (int)num;
			}
			this.SetValue(value, array);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00050633 File Offset: 0x0004E833
		public static void Sort(Array array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, null);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00050658 File Offset: 0x0004E858
		public static void Sort(Array array, int index, int length)
		{
			Array.Sort(array, null, index, length, null);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00050664 File Offset: 0x0004E864
		public static void Sort(Array array, IComparer comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort(array, null, array.GetLowerBound(0), array.Length, comparer);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00050689 File Offset: 0x0004E889
		public static void Sort(Array array, int index, int length, IComparer comparer)
		{
			Array.Sort(array, null, index, length, comparer);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x00050695 File Offset: 0x0004E895
		public static void Sort(Array keys, Array items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, null);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000506BA File Offset: 0x0004E8BA
		public static void Sort(Array keys, Array items, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort(keys, items, keys.GetLowerBound(0), keys.Length, comparer);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x000506DF File Offset: 0x0004E8DF
		public static void Sort(Array keys, Array items, int index, int length)
		{
			Array.Sort(keys, items, index, length, null);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x000506EC File Offset: 0x0004E8EC
		public static void Sort(Array keys, Array items, int index, int length, IComparer comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (keys.Rank != 1 || (items != null && items.Rank != 1))
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			int lowerBound = keys.GetLowerBound(0);
			if (items != null && lowerBound != items.GetLowerBound(0))
			{
				throw new ArgumentException("The arrays' lower bounds must be identical.");
			}
			if (index < lowerBound || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (keys.Length - (index - lowerBound) < length || (items != null && index - lowerBound > items.Length - length))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				Array.SortImpl(keys, items, index, length, comparer);
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000507A1 File Offset: 0x0004E9A1
		public static void Sort<T>(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, null);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000507BC File Offset: 0x0004E9BC
		public static void Sort<T>(T[] array, int index, int length)
		{
			Array.Sort<T>(array, index, length, null);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x000507C7 File Offset: 0x0004E9C7
		public static void Sort<T>(T[] array, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			Array.Sort<T>(array, 0, array.Length, comparer);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x000507E4 File Offset: 0x0004E9E4
		public static void Sort<T>(T[] array, int index, int length, IComparer<T> comparer)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (array.Length - index < length)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				ArraySortHelper<T>.Default.Sort(array, index, length, comparer);
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x00050847 File Offset: 0x0004EA47
		public static void Sort<T>(T[] array, Comparison<T> comparison)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			ArraySortHelper<T>.Sort(array, 0, array.Length, comparison);
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00050870 File Offset: 0x0004EA70
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, null);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005088C File Offset: 0x0004EA8C
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length)
		{
			Array.Sort<TKey, TValue>(keys, items, index, length, null);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00050898 File Offset: 0x0004EA98
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			Array.Sort<TKey, TValue>(keys, items, 0, keys.Length, comparer);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x000508B4 File Offset: 0x0004EAB4
		public static void Sort<TKey, TValue>(TKey[] keys, TValue[] items, int index, int length, IComparer<TKey> comparer)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (index < 0 || length < 0)
			{
				throw new ArgumentOutOfRangeException((length < 0) ? "length" : "index", "Non-negative number required.");
			}
			if (keys.Length - index < length || (items != null && index > items.Length - length))
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (length > 1)
			{
				if (items == null)
				{
					Array.Sort<TKey>(keys, index, length, comparer);
					return;
				}
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, index, length, comparer);
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00050932 File Offset: 0x0004EB32
		public static bool Exists<T>(T[] array, Predicate<T> match)
		{
			return Array.FindIndex<T>(array, match) != -1;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00050944 File Offset: 0x0004EB44
		public static void Fill<T>(T[] array, T value)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00050978 File Offset: 0x0004EB78
		public static void Fill<T>(T[] array, T value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				array[i] = value;
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x000509E0 File Offset: 0x0004EBE0
		public static T Find<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00050A38 File Offset: 0x0004EC38
		public static T[] FindAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			T[] array2 = Array.Empty<T>();
			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					if (num == array2.Length)
					{
						Array.Resize<T>(ref array2, Math.Min((num == 0) ? 4 : (num * 2), array.Length));
					}
					array2[num++] = array[i];
				}
			}
			if (num != array2.Length)
			{
				Array.Resize<T>(ref array2, num);
			}
			return array2;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00050AC5 File Offset: 0x0004ECC5
		public static int FindIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, 0, array.Length, match);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00050AE0 File Offset: 0x0004ECE0
		public static int FindIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindIndex<T>(array, startIndex, array.Length - startIndex, match);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00050B00 File Offset: 0x0004ED00
		public static int FindIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0 || startIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > array.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00050B84 File Offset: 0x0004ED84
		public static T FindLast<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (match(array[i]))
				{
					return array[i];
				}
			}
			return default(T);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00050BDD File Offset: 0x0004EDDD
		public static int FindLastIndex<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, array.Length - 1, array.Length, match);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00050BFC File Offset: 0x0004EDFC
		public static int FindLastIndex<T>(T[] array, int startIndex, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			return Array.FindLastIndex<T>(array, startIndex, startIndex + 1, match);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00050C18 File Offset: 0x0004EE18
		public static int FindLastIndex<T>(T[] array, int startIndex, int count, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			if (array.Length == 0)
			{
				if (startIndex != -1)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
			}
			else if (startIndex < 0 || startIndex >= array.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			int num = startIndex - count;
			for (int i = startIndex; i > num; i--)
			{
				if (match(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00050CB4 File Offset: 0x0004EEB4
		public static bool TrueForAll<T>(T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!match(array[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00050CFD File Offset: 0x0004EEFD
		public IEnumerator GetEnumerator()
		{
			return new Array.ArrayEnumerator(this);
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000025BE File Offset: 0x000007BE
		private Array()
		{
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0004F7ED File Offset: 0x0004D9ED
		internal int InternalArray__ICollection_get_Count()
		{
			return this.Length;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal bool InternalArray__ICollection_get_IsReadOnly()
		{
			return true;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00050D05 File Offset: 0x0004EF05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref byte GetRawSzArrayData()
		{
			return ref Unsafe.As<Array.RawData>(this).Data;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00050D12 File Offset: 0x0004EF12
		internal IEnumerator<T> InternalArray__IEnumerable_GetEnumerator<T>()
		{
			if (this.Length == 0)
			{
				return Array.EmptyInternalEnumerator<T>.Value;
			}
			return new Array.InternalEnumerator<T>(this);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00050D2D File Offset: 0x0004EF2D
		internal void InternalArray__ICollection_Clear()
		{
			throw new NotSupportedException("Collection is read-only");
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00050D39 File Offset: 0x0004EF39
		internal void InternalArray__ICollection_Add<T>(T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00050D39 File Offset: 0x0004EF39
		internal bool InternalArray__ICollection_Remove<T>(T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00050D48 File Offset: 0x0004EF48
		internal bool InternalArray__ICollection_Contains<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException("Only single dimension arrays are supported.");
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					if (t == null)
					{
						return true;
					}
				}
				else if (item.Equals(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00050DAF File Offset: 0x0004EFAF
		internal void InternalArray__ICollection_CopyTo<T>(T[] array, int arrayIndex)
		{
			Array.Copy(this, this.GetLowerBound(0), array, arrayIndex, this.Length);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00050DC8 File Offset: 0x0004EFC8
		internal T InternalArray__IReadOnlyList_get_Item<T>(int index)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			T t;
			this.GetGenericValueImpl<T>(index, out t);
			return t;
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004F7ED File Offset: 0x0004D9ED
		internal int InternalArray__IReadOnlyCollection_get_Count()
		{
			return this.Length;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00050D39 File Offset: 0x0004EF39
		internal void InternalArray__Insert<T>(int index, T item)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00050D39 File Offset: 0x0004EF39
		internal void InternalArray__RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is of a fixed size");
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00050DF4 File Offset: 0x0004EFF4
		internal int InternalArray__IndexOf<T>(T item)
		{
			if (this.Rank > 1)
			{
				throw new RankException("Only single dimension arrays are supported.");
			}
			int length = this.Length;
			for (int i = 0; i < length; i++)
			{
				T t;
				this.GetGenericValueImpl<T>(i, out t);
				if (item == null)
				{
					if (t == null)
					{
						return i + this.GetLowerBound(0);
					}
				}
				else if (t.Equals(item))
				{
					return i + this.GetLowerBound(0);
				}
			}
			return this.GetLowerBound(0) - 1;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00050E74 File Offset: 0x0004F074
		internal T InternalArray__get_Item<T>(int index)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			T t;
			this.GetGenericValueImpl<T>(index, out t);
			return t;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00050EA0 File Offset: 0x0004F0A0
		internal void InternalArray__set_Item<T>(int index, T item)
		{
			if (index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object[] array = this as object[];
			if (array != null)
			{
				array[index] = item;
				return;
			}
			this.SetGenericValueImpl<T>(index, ref item);
		}

		// Token: 0x06001434 RID: 5172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGenericValue_icall<T>(ref Array self, int pos, out T value);

		// Token: 0x06001435 RID: 5173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGenericValue_icall<T>(ref Array self, int pos, ref T value);

		// Token: 0x06001436 RID: 5174 RVA: 0x00050EE0 File Offset: 0x0004F0E0
		internal void GetGenericValueImpl<T>(int pos, out T value)
		{
			Array array = this;
			Array.GetGenericValue_icall<T>(ref array, pos, out value);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		internal void SetGenericValueImpl<T>(int pos, ref T value)
		{
			Array array = this;
			Array.SetGenericValue_icall<T>(ref array, pos, ref value);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00050F10 File Offset: 0x0004F110
		public int Length
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				int num = this.GetLength(0);
				for (int i = 1; i < this.Rank; i++)
				{
					num *= this.GetLength(i);
				}
				return num;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00050F41 File Offset: 0x0004F141
		public int Rank
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.GetRank();
			}
		}

		// Token: 0x0600143A RID: 5178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetRank();

		// Token: 0x0600143B RID: 5179
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLength(int dimension);

		// Token: 0x0600143C RID: 5180
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLowerBound(int dimension);

		// Token: 0x0600143D RID: 5181
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern object GetValue(params int[] indices);

		// Token: 0x0600143E RID: 5182
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetValue(object value, params int[] indices);

		// Token: 0x0600143F RID: 5183
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetValueImpl(int pos);

		// Token: 0x06001440 RID: 5184
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetValueImpl(object value, int pos);

		// Token: 0x06001441 RID: 5185
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool FastCopy(Array source, int source_idx, Array dest, int dest_idx, int length);

		// Token: 0x06001442 RID: 5186
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Array CreateInstanceImpl(Type elementType, int[] lengths, int[] bounds);

		// Token: 0x06001443 RID: 5187 RVA: 0x00050F49 File Offset: 0x0004F149
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int GetUpperBound(int dimension)
		{
			return this.GetLowerBound(dimension) + this.GetLength(dimension) - 1;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00050F5C File Offset: 0x0004F15C
		public object GetValue(int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			int lowerBound = this.GetLowerBound(0);
			if (index < lowerBound || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException("Index has to be between upper and lower bound of the array.");
			}
			if (base.GetType().GetElementType().IsPointer)
			{
				throw new NotSupportedException("Type is not supported.");
			}
			return this.GetValueImpl(index - lowerBound);
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		public object GetValue(int index1, int index2)
		{
			int[] array = new int[] { index1, index2 };
			return this.GetValue(array);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00050FE8 File Offset: 0x0004F1E8
		public object GetValue(int index1, int index2, int index3)
		{
			int[] array = new int[] { index1, index2, index3 };
			return this.GetValue(array);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00051010 File Offset: 0x0004F210
		public void SetValue(object value, int index)
		{
			if (this.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			int lowerBound = this.GetLowerBound(0);
			if (index < lowerBound || index > this.GetUpperBound(0))
			{
				throw new IndexOutOfRangeException("Index has to be >= lower bound and <= upper bound of the array.");
			}
			if (base.GetType().GetElementType().IsPointer)
			{
				throw new NotSupportedException("Type is not supported.");
			}
			this.SetValueImpl(value, index - lowerBound);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005107C File Offset: 0x0004F27C
		public void SetValue(object value, int index1, int index2)
		{
			int[] array = new int[] { index1, index2 };
			this.SetValue(value, array);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000510A0 File Offset: 0x0004F2A0
		public void SetValue(object value, int index1, int index2, int index3)
		{
			int[] array = new int[] { index1, index2, index3 };
			this.SetValue(value, array);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x000510C9 File Offset: 0x0004F2C9
		internal static Array UnsafeCreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			return Array.CreateInstance(elementType, lengths, lowerBounds);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x000510D3 File Offset: 0x0004F2D3
		internal static Array UnsafeCreateInstance(Type elementType, int length1, int length2)
		{
			return Array.CreateInstance(elementType, length1, length2);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000510DD File Offset: 0x0004F2DD
		internal static Array UnsafeCreateInstance(Type elementType, params int[] lengths)
		{
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000510E8 File Offset: 0x0004F2E8
		public static Array CreateInstance(Type elementType, int length)
		{
			int[] array = new int[] { length };
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00051108 File Offset: 0x0004F308
		public static Array CreateInstance(Type elementType, int length1, int length2)
		{
			int[] array = new int[] { length1, length2 };
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005112C File Offset: 0x0004F32C
		public static Array CreateInstance(Type elementType, int length1, int length2, int length3)
		{
			int[] array = new int[] { length1, length2, length3 };
			return Array.CreateInstance(elementType, array);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00051154 File Offset: 0x0004F354
		public static Array CreateInstance(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			int[] array = null;
			elementType = elementType.UnderlyingSystemType as RuntimeType;
			if (elementType == null)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			return Array.CreateInstanceImpl(elementType, lengths, array);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000511F4 File Offset: 0x0004F3F4
		public static Array CreateInstance(Type elementType, int[] lengths, int[] lowerBounds)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null)
			{
				throw new ArgumentNullException("lengths");
			}
			if (lowerBounds == null)
			{
				throw new ArgumentNullException("lowerBounds");
			}
			elementType = elementType.UnderlyingSystemType as RuntimeType;
			if (elementType == null)
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "elementType");
			}
			if (elementType.Equals(typeof(void)))
			{
				throw new NotSupportedException("Array type can not be void");
			}
			if (elementType.ContainsGenericParameters)
			{
				throw new NotSupportedException("Array type can not be an open generic type");
			}
			if (lengths.Length < 1)
			{
				throw new ArgumentException("Arrays must contain >= 1 elements.");
			}
			if (lengths.Length != lowerBounds.Length)
			{
				throw new ArgumentException("Arrays must be of same size.");
			}
			for (int i = 0; i < lowerBounds.Length; i++)
			{
				if (lengths[i] < 0)
				{
					throw new ArgumentOutOfRangeException("lengths", "Each value has to be >= 0.");
				}
				if ((long)lowerBounds[i] + (long)lengths[i] > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("lengths", "Length + bound must not exceed Int32.MaxValue.");
				}
			}
			if (lengths.Length > 255)
			{
				throw new TypeLoadException();
			}
			return Array.CreateInstanceImpl(elementType, lengths, lowerBounds);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00051308 File Offset: 0x0004F508
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Clear(Array array, int index, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (length < 0)
			{
				throw new IndexOutOfRangeException("length < 0");
			}
			int lowerBound = array.GetLowerBound(0);
			if (index < lowerBound)
			{
				throw new IndexOutOfRangeException("index < lower bound");
			}
			index -= lowerBound;
			if (index > array.Length - length)
			{
				throw new IndexOutOfRangeException("index + length > size");
			}
			Array.ClearInternal(array, index, length);
		}

		// Token: 0x06001453 RID: 5203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearInternal(Array a, int index, int count);

		// Token: 0x06001454 RID: 5204 RVA: 0x0005136C File Offset: 0x0004F56C
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, Array destinationArray, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			Array.Copy(sourceArray, sourceArray.GetLowerBound(0), destinationArray, destinationArray.GetLowerBound(0), length);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000513A0 File Offset: 0x0004F5A0
		[ReliabilityContract(Consistency.MayCorruptInstance, Cer.MayFail)]
		public static void Copy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Value has to be >= 0.");
			}
			if (sourceArray.Rank != destinationArray.Rank)
			{
				throw new RankException("Only single dimension arrays are supported here.");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", "Value has to be >= 0.");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Value has to be >= 0.");
			}
			if (Array.FastCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length))
			{
				return;
			}
			int num = sourceIndex - sourceArray.GetLowerBound(0);
			int num2 = destinationIndex - destinationArray.GetLowerBound(0);
			if (num2 < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", "Index was less than the array's lower bound in the first dimension.");
			}
			if (num > sourceArray.Length - length)
			{
				throw new ArgumentException("length");
			}
			if (num2 > destinationArray.Length - length)
			{
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds", "destinationArray");
			}
			Type elementType = sourceArray.GetType().GetElementType();
			Type elementType2 = destinationArray.GetType().GetElementType();
			bool isValueType = elementType2.IsValueType;
			if (sourceArray != destinationArray || num > num2)
			{
				for (int i = 0; i < length; i++)
				{
					object valueImpl = sourceArray.GetValueImpl(num + i);
					if (valueImpl == null && isValueType)
					{
						throw new InvalidCastException();
					}
					try
					{
						destinationArray.SetValueImpl(valueImpl, num2 + i);
					}
					catch (ArgumentException)
					{
						throw Array.CreateArrayTypeMismatchException();
					}
					catch (InvalidCastException)
					{
						if (Array.CanAssignArrayElement(elementType, elementType2))
						{
							throw;
						}
						throw Array.CreateArrayTypeMismatchException();
					}
				}
				return;
			}
			for (int j = length - 1; j >= 0; j--)
			{
				object valueImpl2 = sourceArray.GetValueImpl(num + j);
				try
				{
					destinationArray.SetValueImpl(valueImpl2, num2 + j);
				}
				catch (ArgumentException)
				{
					throw Array.CreateArrayTypeMismatchException();
				}
				catch
				{
					if (Array.CanAssignArrayElement(elementType, elementType2))
					{
						throw;
					}
					throw Array.CreateArrayTypeMismatchException();
				}
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004E657 File Offset: 0x0004C857
		private static ArrayTypeMismatchException CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00051580 File Offset: 0x0004F780
		private static bool CanAssignArrayElement(Type source, Type target)
		{
			if (source.IsValueType)
			{
				return source.IsAssignableFrom(target);
			}
			if (source.IsInterface)
			{
				return !target.IsValueType;
			}
			if (target.IsInterface)
			{
				return !source.IsValueType;
			}
			return source.IsAssignableFrom(target) || target.IsAssignableFrom(source);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000515D3 File Offset: 0x0004F7D3
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
		{
			Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000515E0 File Offset: 0x0004F7E0
		public static T[] Empty<T>()
		{
			return EmptyArray<T>.Value;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00004088 File Offset: 0x00002288
		public void Initialize()
		{
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000515E7 File Offset: 0x0004F7E7
		private static int IndexOfImpl<T>(T[] array, T value, int startIndex, int count)
		{
			return EqualityComparer<T>.Default.IndexOf(array, value, startIndex, count);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x000515F7 File Offset: 0x0004F7F7
		private static int LastIndexOfImpl<T>(T[] array, T value, int startIndex, int count)
		{
			return EqualityComparer<T>.Default.LastIndexOf(array, value, startIndex, count);
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00051608 File Offset: 0x0004F808
		private static void SortImpl(Array keys, Array items, int index, int length, IComparer comparer)
		{
			object[] array = keys as object[];
			object[] array2 = null;
			if (array != null)
			{
				array2 = items as object[];
			}
			if (array != null && (items == null || array2 != null))
			{
				Array.SorterObjectArray sorterObjectArray = new Array.SorterObjectArray(array, array2, comparer);
				sorterObjectArray.Sort(index, length);
				return;
			}
			Array.SorterGenericArray sorterGenericArray = new Array.SorterGenericArray(keys, items, comparer);
			sorterGenericArray.Sort(index, length);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005165A File Offset: 0x0004F85A
		internal static T UnsafeLoad<T>(T[] array, int index)
		{
			return array[index];
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00051663 File Offset: 0x0004F863
		internal static void UnsafeStore<T>(T[] array, int index, T value)
		{
			array[index] = value;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005166D File Offset: 0x0004F86D
		internal static R UnsafeMov<S, R>(S instance)
		{
			return (R)((object)instance);
		}

		// Token: 0x020001A8 RID: 424
		private sealed class ArrayEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06001461 RID: 5217 RVA: 0x0005167A File Offset: 0x0004F87A
			internal ArrayEnumerator(Array array)
			{
				this._array = array;
				this._index = -1;
				this._endIndex = array.Length;
			}

			// Token: 0x06001462 RID: 5218 RVA: 0x0005169C File Offset: 0x0004F89C
			public bool MoveNext()
			{
				if (this._index < this._endIndex)
				{
					this._index++;
					return this._index < this._endIndex;
				}
				return false;
			}

			// Token: 0x06001463 RID: 5219 RVA: 0x000516CA File Offset: 0x0004F8CA
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x06001464 RID: 5220 RVA: 0x0001AB5D File Offset: 0x00018D5D
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06001465 RID: 5221 RVA: 0x000516D4 File Offset: 0x0004F8D4
			public object Current
			{
				get
				{
					if (this._index < 0)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index >= this._endIndex)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					if (this._index == 0 && this._array.GetType().GetElementType().IsPointer)
					{
						throw new NotSupportedException("Type is not supported.");
					}
					return this._array.GetValueImpl(this._index);
				}
			}

			// Token: 0x04001344 RID: 4932
			private Array _array;

			// Token: 0x04001345 RID: 4933
			private int _index;

			// Token: 0x04001346 RID: 4934
			private int _endIndex;
		}

		// Token: 0x020001A9 RID: 425
		[StructLayout(LayoutKind.Sequential)]
		private class RawData
		{
			// Token: 0x06001466 RID: 5222 RVA: 0x000025BE File Offset: 0x000007BE
			public RawData()
			{
			}

			// Token: 0x04001347 RID: 4935
			public IntPtr Bounds;

			// Token: 0x04001348 RID: 4936
			public IntPtr Count;

			// Token: 0x04001349 RID: 4937
			public byte Data;
		}

		// Token: 0x020001AA RID: 426
		internal struct InternalEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06001467 RID: 5223 RVA: 0x00051749 File Offset: 0x0004F949
			internal InternalEnumerator(Array array)
			{
				this.array = array;
				this.idx = -2;
			}

			// Token: 0x06001468 RID: 5224 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x06001469 RID: 5225 RVA: 0x0005175C File Offset: 0x0004F95C
			public bool MoveNext()
			{
				if (this.idx == -2)
				{
					this.idx = this.array.Length;
				}
				if (this.idx != -1)
				{
					int num = this.idx - 1;
					this.idx = num;
					return num != -1;
				}
				return false;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x0600146A RID: 5226 RVA: 0x000517A8 File Offset: 0x0004F9A8
			public T Current
			{
				get
				{
					if (this.idx == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext");
					}
					if (this.idx == -1)
					{
						throw new InvalidOperationException("Enumeration already finished");
					}
					return this.array.InternalArray__get_Item<T>(this.array.Length - 1 - this.idx);
				}
			}

			// Token: 0x0600146B RID: 5227 RVA: 0x000517FD File Offset: 0x0004F9FD
			void IEnumerator.Reset()
			{
				this.idx = -2;
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x0600146C RID: 5228 RVA: 0x00051807 File Offset: 0x0004FA07
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400134A RID: 4938
			private const int NOT_STARTED = -2;

			// Token: 0x0400134B RID: 4939
			private const int FINISHED = -1;

			// Token: 0x0400134C RID: 4940
			private readonly Array array;

			// Token: 0x0400134D RID: 4941
			private int idx;
		}

		// Token: 0x020001AB RID: 427
		internal class EmptyInternalEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600146D RID: 5229 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x0600146E RID: 5230 RVA: 0x0000408A File Offset: 0x0000228A
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600146F RID: 5231 RVA: 0x00051814 File Offset: 0x0004FA14
			public T Current
			{
				get
				{
					throw new InvalidOperationException("Enumeration has not started. Call MoveNext");
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06001470 RID: 5232 RVA: 0x00051820 File Offset: 0x0004FA20
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001471 RID: 5233 RVA: 0x00004088 File Offset: 0x00002288
			void IEnumerator.Reset()
			{
			}

			// Token: 0x06001472 RID: 5234 RVA: 0x000025BE File Offset: 0x000007BE
			public EmptyInternalEnumerator()
			{
			}

			// Token: 0x06001473 RID: 5235 RVA: 0x0005182D File Offset: 0x0004FA2D
			// Note: this type is marked as 'beforefieldinit'.
			static EmptyInternalEnumerator()
			{
			}

			// Token: 0x0400134E RID: 4942
			public static readonly Array.EmptyInternalEnumerator<T> Value = new Array.EmptyInternalEnumerator<T>();
		}

		// Token: 0x020001AC RID: 428
		internal sealed class FunctorComparer<T> : IComparer<T>
		{
			// Token: 0x06001474 RID: 5236 RVA: 0x00051839 File Offset: 0x0004FA39
			public FunctorComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x06001475 RID: 5237 RVA: 0x00051848 File Offset: 0x0004FA48
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x0400134F RID: 4943
			private Comparison<T> comparison;
		}

		// Token: 0x020001AD RID: 429
		private struct SorterObjectArray
		{
			// Token: 0x06001476 RID: 5238 RVA: 0x00051857 File Offset: 0x0004FA57
			internal SorterObjectArray(object[] keys, object[] items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06001477 RID: 5239 RVA: 0x00051878 File Offset: 0x0004FA78
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys[a], this.keys[b]) > 0)
				{
					object obj = this.keys[a];
					this.keys[a] = this.keys[b];
					this.keys[b] = obj;
					if (this.items != null)
					{
						object obj2 = this.items[a];
						this.items[a] = this.items[b];
						this.items[b] = obj2;
					}
				}
			}

			// Token: 0x06001478 RID: 5240 RVA: 0x000518F4 File Offset: 0x0004FAF4
			private void Swap(int i, int j)
			{
				object obj = this.keys[i];
				this.keys[i] = this.keys[j];
				this.keys[j] = obj;
				if (this.items != null)
				{
					object obj2 = this.items[i];
					this.items[i] = this.items[j];
					this.items[j] = obj2;
				}
			}

			// Token: 0x06001479 RID: 5241 RVA: 0x0005194D File Offset: 0x0004FB4D
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x0600147A RID: 5242 RVA: 0x00051958 File Offset: 0x0004FB58
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2PlusOne(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
				}
			}

			// Token: 0x0600147B RID: 5243 RVA: 0x000519C0 File Offset: 0x0004FBC0
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x0600147C RID: 5244 RVA: 0x00051A44 File Offset: 0x0004FC44
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object obj = this.keys[num];
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys[++i], obj) < 0)
					{
					}
					while (this.comparer.Compare(obj, this.keys[--num2]) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x0600147D RID: 5245 RVA: 0x00051AE0 File Offset: 0x0004FCE0
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x0600147E RID: 5246 RVA: 0x00051B30 File Offset: 0x0004FD30
			private void DownHeap(int i, int n, int lo)
			{
				object obj = this.keys[lo + i - 1];
				object obj2 = ((this.items != null) ? this.items[lo + i - 1] : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys[lo + num - 1], this.keys[lo + num]) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(obj, this.keys[lo + num - 1]) >= 0)
					{
						break;
					}
					this.keys[lo + i - 1] = this.keys[lo + num - 1];
					if (this.items != null)
					{
						this.items[lo + i - 1] = this.items[lo + num - 1];
					}
					i = num;
				}
				this.keys[lo + i - 1] = obj;
				if (this.items != null)
				{
					this.items[lo + i - 1] = obj2;
				}
			}

			// Token: 0x0600147F RID: 5247 RVA: 0x00051C18 File Offset: 0x0004FE18
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object obj = this.keys[i + 1];
					object obj2 = ((this.items != null) ? this.items[i + 1] : null);
					while (num >= lo && this.comparer.Compare(obj, this.keys[num]) < 0)
					{
						this.keys[num + 1] = this.keys[num];
						if (this.items != null)
						{
							this.items[num + 1] = this.items[num];
						}
						num--;
					}
					this.keys[num + 1] = obj;
					if (this.items != null)
					{
						this.items[num + 1] = obj2;
					}
				}
			}

			// Token: 0x04001350 RID: 4944
			private object[] keys;

			// Token: 0x04001351 RID: 4945
			private object[] items;

			// Token: 0x04001352 RID: 4946
			private IComparer comparer;
		}

		// Token: 0x020001AE RID: 430
		private struct SorterGenericArray
		{
			// Token: 0x06001480 RID: 5248 RVA: 0x00051CC5 File Offset: 0x0004FEC5
			internal SorterGenericArray(Array keys, Array items, IComparer comparer)
			{
				if (comparer == null)
				{
					comparer = Comparer.Default;
				}
				this.keys = keys;
				this.items = items;
				this.comparer = comparer;
			}

			// Token: 0x06001481 RID: 5249 RVA: 0x00051CE8 File Offset: 0x0004FEE8
			internal void SwapIfGreaterWithItems(int a, int b)
			{
				if (a != b && this.comparer.Compare(this.keys.GetValue(a), this.keys.GetValue(b)) > 0)
				{
					object value = this.keys.GetValue(a);
					this.keys.SetValue(this.keys.GetValue(b), a);
					this.keys.SetValue(value, b);
					if (this.items != null)
					{
						object value2 = this.items.GetValue(a);
						this.items.SetValue(this.items.GetValue(b), a);
						this.items.SetValue(value2, b);
					}
				}
			}

			// Token: 0x06001482 RID: 5250 RVA: 0x00051D90 File Offset: 0x0004FF90
			private void Swap(int i, int j)
			{
				object value = this.keys.GetValue(i);
				this.keys.SetValue(this.keys.GetValue(j), i);
				this.keys.SetValue(value, j);
				if (this.items != null)
				{
					object value2 = this.items.GetValue(i);
					this.items.SetValue(this.items.GetValue(j), i);
					this.items.SetValue(value2, j);
				}
			}

			// Token: 0x06001483 RID: 5251 RVA: 0x00051E09 File Offset: 0x00050009
			internal void Sort(int left, int length)
			{
				this.IntrospectiveSort(left, length);
			}

			// Token: 0x06001484 RID: 5252 RVA: 0x00051E14 File Offset: 0x00050014
			private void IntrospectiveSort(int left, int length)
			{
				if (length < 2)
				{
					return;
				}
				try
				{
					this.IntroSort(left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2PlusOne(this.keys.Length));
				}
				catch (IndexOutOfRangeException)
				{
					IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(this.comparer);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException("Failed to compare two elements in the array.", ex);
				}
			}

			// Token: 0x06001485 RID: 5253 RVA: 0x00051E80 File Offset: 0x00050080
			private void IntroSort(int lo, int hi, int depthLimit)
			{
				while (hi > lo)
				{
					int num = hi - lo + 1;
					if (num <= 16)
					{
						if (num == 1)
						{
							return;
						}
						if (num == 2)
						{
							this.SwapIfGreaterWithItems(lo, hi);
							return;
						}
						if (num == 3)
						{
							this.SwapIfGreaterWithItems(lo, hi - 1);
							this.SwapIfGreaterWithItems(lo, hi);
							this.SwapIfGreaterWithItems(hi - 1, hi);
							return;
						}
						this.InsertionSort(lo, hi);
						return;
					}
					else
					{
						if (depthLimit == 0)
						{
							this.Heapsort(lo, hi);
							return;
						}
						depthLimit--;
						int num2 = this.PickPivotAndPartition(lo, hi);
						this.IntroSort(num2 + 1, hi, depthLimit);
						hi = num2 - 1;
					}
				}
			}

			// Token: 0x06001486 RID: 5254 RVA: 0x00051F04 File Offset: 0x00050104
			private int PickPivotAndPartition(int lo, int hi)
			{
				int num = lo + (hi - lo) / 2;
				this.SwapIfGreaterWithItems(lo, num);
				this.SwapIfGreaterWithItems(lo, hi);
				this.SwapIfGreaterWithItems(num, hi);
				object value = this.keys.GetValue(num);
				this.Swap(num, hi - 1);
				int i = lo;
				int num2 = hi - 1;
				while (i < num2)
				{
					while (this.comparer.Compare(this.keys.GetValue(++i), value) < 0)
					{
					}
					while (this.comparer.Compare(value, this.keys.GetValue(--num2)) < 0)
					{
					}
					if (i >= num2)
					{
						break;
					}
					this.Swap(i, num2);
				}
				this.Swap(i, hi - 1);
				return i;
			}

			// Token: 0x06001487 RID: 5255 RVA: 0x00051FAC File Offset: 0x000501AC
			private void Heapsort(int lo, int hi)
			{
				int num = hi - lo + 1;
				for (int i = num / 2; i >= 1; i--)
				{
					this.DownHeap(i, num, lo);
				}
				for (int j = num; j > 1; j--)
				{
					this.Swap(lo, lo + j - 1);
					this.DownHeap(1, j - 1, lo);
				}
			}

			// Token: 0x06001488 RID: 5256 RVA: 0x00051FFC File Offset: 0x000501FC
			private void DownHeap(int i, int n, int lo)
			{
				object value = this.keys.GetValue(lo + i - 1);
				object obj = ((this.items != null) ? this.items.GetValue(lo + i - 1) : null);
				while (i <= n / 2)
				{
					int num = 2 * i;
					if (num < n && this.comparer.Compare(this.keys.GetValue(lo + num - 1), this.keys.GetValue(lo + num)) < 0)
					{
						num++;
					}
					if (this.comparer.Compare(value, this.keys.GetValue(lo + num - 1)) >= 0)
					{
						break;
					}
					this.keys.SetValue(this.keys.GetValue(lo + num - 1), lo + i - 1);
					if (this.items != null)
					{
						this.items.SetValue(this.items.GetValue(lo + num - 1), lo + i - 1);
					}
					i = num;
				}
				this.keys.SetValue(value, lo + i - 1);
				if (this.items != null)
				{
					this.items.SetValue(obj, lo + i - 1);
				}
			}

			// Token: 0x06001489 RID: 5257 RVA: 0x00052110 File Offset: 0x00050310
			private void InsertionSort(int lo, int hi)
			{
				for (int i = lo; i < hi; i++)
				{
					int num = i;
					object value = this.keys.GetValue(i + 1);
					object obj = ((this.items != null) ? this.items.GetValue(i + 1) : null);
					while (num >= lo && this.comparer.Compare(value, this.keys.GetValue(num)) < 0)
					{
						this.keys.SetValue(this.keys.GetValue(num), num + 1);
						if (this.items != null)
						{
							this.items.SetValue(this.items.GetValue(num), num + 1);
						}
						num--;
					}
					this.keys.SetValue(value, num + 1);
					if (this.items != null)
					{
						this.items.SetValue(obj, num + 1);
					}
				}
			}

			// Token: 0x04001353 RID: 4947
			private Array keys;

			// Token: 0x04001354 RID: 4948
			private Array items;

			// Token: 0x04001355 RID: 4949
			private IComparer comparer;
		}
	}
}
