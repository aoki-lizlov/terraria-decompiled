using System;

namespace System.Reflection
{
	// Token: 0x02000004 RID: 4
	internal class TypeInfo
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		public Type BaseType
		{
			get
			{
				return this.type.BaseType;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219D File Offset: 0x0000039D
		public bool IsClass
		{
			get
			{
				return this.type.IsClass;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AA File Offset: 0x000003AA
		public bool IsGenericType
		{
			get
			{
				return this.type.IsGenericType;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B7 File Offset: 0x000003B7
		public bool IsPrimitive
		{
			get
			{
				return this.type.IsPrimitive;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C4 File Offset: 0x000003C4
		public bool IsValueType
		{
			get
			{
				return this.type.IsValueType;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021D1 File Offset: 0x000003D1
		public TypeInfo(Type type)
		{
			this.type = type;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021E0 File Offset: 0x000003E0
		public bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return this.type.IsAssignableFrom(typeInfo.type);
		}

		// Token: 0x04000003 RID: 3
		private readonly Type type;
	}
}
