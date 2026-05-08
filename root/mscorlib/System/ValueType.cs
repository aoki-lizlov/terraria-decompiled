using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x0200023C RID: 572
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x06001BE9 RID: 7145 RVA: 0x000025BE File Offset: 0x000007BE
		protected ValueType()
		{
		}

		// Token: 0x06001BEA RID: 7146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06001BEB RID: 7147 RVA: 0x000695F0 File Offset: 0x000677F0
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o1 == null && o2 == null)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)o1.GetType();
			RuntimeType runtimeType2 = (RuntimeType)o2.GetType();
			if (runtimeType != runtimeType2)
			{
				return false;
			}
			object[] array;
			bool flag = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return flag;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00056327 File Offset: 0x00054527
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001BED RID: 7149
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		// Token: 0x06001BEE RID: 7150 RVA: 0x00069670 File Offset: 0x00067870
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x000696A8 File Offset: 0x000678A8
		internal static int GetHashCodeOfPtr(IntPtr ptr)
		{
			int num = (int)ptr;
			int num2 = ValueType.Internal.hash_code_of_ptr_seed;
			if (num2 == 0)
			{
				num2 = num;
				Interlocked.CompareExchange(ref ValueType.Internal.hash_code_of_ptr_seed, num2, 0);
				num2 = ValueType.Internal.hash_code_of_ptr_seed;
			}
			return num - num2;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000696DD File Offset: 0x000678DD
		public override string ToString()
		{
			return base.GetType().FullName;
		}

		// Token: 0x0200023D RID: 573
		private static class Internal
		{
			// Token: 0x0400188A RID: 6282
			public static int hash_code_of_ptr_seed;
		}
	}
}
