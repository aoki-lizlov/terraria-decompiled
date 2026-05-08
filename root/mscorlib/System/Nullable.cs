using System;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200012A RID: 298
	[NonVersionable]
	[Serializable]
	public struct Nullable<T> where T : struct
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x0002D9A9 File Offset: 0x0002BBA9
		[NonVersionable]
		public Nullable(T value)
		{
			this.value = value;
			this.hasValue = true;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002D9B9 File Offset: 0x0002BBB9
		public bool HasValue
		{
			[NonVersionable]
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002D9C1 File Offset: 0x0002BBC1
		public T Value
		{
			get
			{
				if (!this.hasValue)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidOperation_NoValue();
				}
				return this.value;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002D9D6 File Offset: 0x0002BBD6
		[NonVersionable]
		public T GetValueOrDefault()
		{
			return this.value;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002D9DE File Offset: 0x0002BBDE
		[NonVersionable]
		public T GetValueOrDefault(T defaultValue)
		{
			if (!this.hasValue)
			{
				return defaultValue;
			}
			return this.value;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		public override bool Equals(object other)
		{
			if (!this.hasValue)
			{
				return other == null;
			}
			return other != null && this.value.Equals(other);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002DA16 File Offset: 0x0002BC16
		public override int GetHashCode()
		{
			if (!this.hasValue)
			{
				return 0;
			}
			return this.value.GetHashCode();
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0002DA33 File Offset: 0x0002BC33
		public override string ToString()
		{
			if (!this.hasValue)
			{
				return "";
			}
			return this.value.ToString();
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x0002DA54 File Offset: 0x0002BC54
		[NonVersionable]
		public static implicit operator T?(T value)
		{
			return new T?(value);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		[NonVersionable]
		public static explicit operator T(T? value)
		{
			return value.Value;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002DA65 File Offset: 0x0002BC65
		private static object Box(T? o)
		{
			if (!o.hasValue)
			{
				return null;
			}
			return o.value;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002DA7C File Offset: 0x0002BC7C
		private static T? Unbox(object o)
		{
			if (o == null)
			{
				return null;
			}
			return new T?((T)((object)o));
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002DAA4 File Offset: 0x0002BCA4
		private static T? UnboxExact(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o.GetType() != typeof(T))
			{
				throw new InvalidCastException();
			}
			return new T?((T)((object)o));
		}

		// Token: 0x04001108 RID: 4360
		private readonly bool hasValue;

		// Token: 0x04001109 RID: 4361
		internal T value;
	}
}
