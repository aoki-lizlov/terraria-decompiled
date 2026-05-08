using System;

namespace System.Reflection
{
	// Token: 0x020008AA RID: 2218
	public static class PropertyInfoExtensions
	{
		// Token: 0x06004AE4 RID: 19172 RVA: 0x000F02A4 File Offset: 0x000EE4A4
		public static MethodInfo[] GetAccessors(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetAccessors();
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x000F02B7 File Offset: 0x000EE4B7
		public static MethodInfo[] GetAccessors(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetAccessors(nonPublic);
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x000F02CB File Offset: 0x000EE4CB
		public static MethodInfo GetGetMethod(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetGetMethod();
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x000F02DE File Offset: 0x000EE4DE
		public static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetGetMethod(nonPublic);
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x000F02F2 File Offset: 0x000EE4F2
		public static MethodInfo GetSetMethod(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetSetMethod();
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x000F0305 File Offset: 0x000EE505
		public static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetSetMethod(nonPublic);
		}
	}
}
