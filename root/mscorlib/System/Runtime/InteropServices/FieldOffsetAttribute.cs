using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006D8 RID: 1752
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x0600403E RID: 16446 RVA: 0x000E0EFC File Offset: 0x000DF0FC
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			int fieldOffset;
			if (field.DeclaringType != null && (fieldOffset = field.GetFieldOffset()) >= 0)
			{
				return new FieldOffsetAttribute(fieldOffset);
			}
			return null;
		}

		// Token: 0x0600403F RID: 16447 RVA: 0x000E0F2A File Offset: 0x000DF12A
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return FieldOffsetAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06004040 RID: 16448 RVA: 0x000E0F35 File Offset: 0x000DF135
		public FieldOffsetAttribute(int offset)
		{
			this._val = offset;
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x000E0F44 File Offset: 0x000DF144
		public int Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A61 RID: 10849
		internal int _val;
	}
}
