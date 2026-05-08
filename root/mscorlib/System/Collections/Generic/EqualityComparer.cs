using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000B1D RID: 2845
	[TypeDependency("System.Collections.Generic.ObjectEqualityComparer`1")]
	[Serializable]
	public abstract class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
	{
		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x0600688F RID: 26767 RVA: 0x00162D74 File Offset: 0x00160F74
		public static EqualityComparer<T> Default
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				EqualityComparer<T> equalityComparer = EqualityComparer<T>.defaultComparer;
				if (equalityComparer == null)
				{
					equalityComparer = EqualityComparer<T>.CreateComparer();
					EqualityComparer<T>.defaultComparer = equalityComparer;
				}
				return equalityComparer;
			}
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x00162D9C File Offset: 0x00160F9C
		[SecuritySafeCritical]
		private static EqualityComparer<T> CreateComparer()
		{
			RuntimeType runtimeType = (RuntimeType)typeof(T);
			if (runtimeType == typeof(byte))
			{
				return (EqualityComparer<T>)new ByteEqualityComparer();
			}
			if (typeof(IEquatable<T>).IsAssignableFrom(runtimeType))
			{
				return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(GenericEqualityComparer<>), runtimeType);
			}
			if (runtimeType.IsGenericType && runtimeType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				RuntimeType runtimeType2 = (RuntimeType)runtimeType.GetGenericArguments()[0];
				if (typeof(IEquatable<>).MakeGenericType(new Type[] { runtimeType2 }).IsAssignableFrom(runtimeType2))
				{
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(NullableEqualityComparer<>), runtimeType2);
				}
			}
			if (runtimeType.IsEnum)
			{
				switch (Type.GetTypeCode(Enum.GetUnderlyingType(runtimeType)))
				{
				case TypeCode.SByte:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(SByteEnumEqualityComparer<>), runtimeType);
				case TypeCode.Byte:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(EnumEqualityComparer<>), runtimeType);
				case TypeCode.Int16:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(ShortEnumEqualityComparer<>), runtimeType);
				case TypeCode.Int64:
				case TypeCode.UInt64:
					return (EqualityComparer<T>)RuntimeType.CreateInstanceForAnotherGenericParameter(typeof(LongEnumEqualityComparer<>), runtimeType);
				}
			}
			return new ObjectEqualityComparer<T>();
		}

		// Token: 0x06006891 RID: 26769
		public abstract bool Equals(T x, T y);

		// Token: 0x06006892 RID: 26770
		public abstract int GetHashCode(T obj);

		// Token: 0x06006893 RID: 26771 RVA: 0x00162F00 File Offset: 0x00161100
		internal virtual int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x00162F34 File Offset: 0x00161134
		internal virtual int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (this.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06006895 RID: 26773 RVA: 0x00162F67 File Offset: 0x00161167
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj is T)
			{
				return this.GetHashCode((T)((object)obj));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return 0;
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x00162F8A File Offset: 0x0016118A
		bool IEqualityComparer.Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is T && y is T)
			{
				return this.Equals((T)((object)x), (T)((object)y));
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x000025BE File Offset: 0x000007BE
		protected EqualityComparer()
		{
		}

		// Token: 0x04003C63 RID: 15459
		private static volatile EqualityComparer<T> defaultComparer;
	}
}
