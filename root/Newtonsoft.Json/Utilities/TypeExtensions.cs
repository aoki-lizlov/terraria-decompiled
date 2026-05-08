using System;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000072 RID: 114
	internal static class TypeExtensions
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00017D75 File Offset: 0x00015F75
		public static MethodInfo Method(this Delegate d)
		{
			return d.Method;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00017D7D File Offset: 0x00015F7D
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			return memberInfo.MemberType;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017D85 File Offset: 0x00015F85
		public static bool ContainsGenericParameters(this Type type)
		{
			return type.ContainsGenericParameters;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017D8D File Offset: 0x00015F8D
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017D95 File Offset: 0x00015F95
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017D9D File Offset: 0x00015F9D
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return type.IsGenericTypeDefinition;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017DA5 File Offset: 0x00015FA5
		public static Type BaseType(this Type type)
		{
			return type.BaseType;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017DAD File Offset: 0x00015FAD
		public static Assembly Assembly(this Type type)
		{
			return type.Assembly;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00017DB5 File Offset: 0x00015FB5
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00017DBD File Offset: 0x00015FBD
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00017DC5 File Offset: 0x00015FC5
		public static bool IsSealed(this Type type)
		{
			return type.IsSealed;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00017DCD File Offset: 0x00015FCD
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00017DD5 File Offset: 0x00015FD5
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00017DDD File Offset: 0x00015FDD
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00017DE5 File Offset: 0x00015FE5
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces, out Type match)
		{
			Type type2 = type;
			while (type2 != null)
			{
				if (string.Equals(type2.FullName, fullTypeName, 4))
				{
					match = type2;
					return true;
				}
				type2 = type2.BaseType();
			}
			if (searchInterfaces)
			{
				Type[] interfaces = type.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (string.Equals(interfaces[i].Name, fullTypeName, 4))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00017E58 File Offset: 0x00016058
		public static bool AssignableToTypeName(this Type type, string fullTypeName, bool searchInterfaces)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, searchInterfaces, out type2);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017E70 File Offset: 0x00016070
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			Type type2 = type;
			while (type2 != null)
			{
				foreach (Type type3 in type2.GetInterfaces())
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
				type2 = type2.BaseType();
			}
			return false;
		}
	}
}
