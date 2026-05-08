using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006CE RID: 1742
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x06004020 RID: 16416 RVA: 0x000E0BB2 File Offset: 0x000DEDB2
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x000E0BC9 File Offset: 0x000DEDC9
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x06004022 RID: 16418 RVA: 0x00002050 File Offset: 0x00000250
		public ComImportAttribute()
		{
		}
	}
}
