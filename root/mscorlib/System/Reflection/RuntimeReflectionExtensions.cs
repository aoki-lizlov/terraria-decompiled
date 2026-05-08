using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020008AC RID: 2220
	public static class RuntimeReflectionExtensions
	{
		// Token: 0x06004AEC RID: 19180 RVA: 0x000F0321 File Offset: 0x000EE521
		public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x000F033F File Offset: 0x000EE53F
		public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x000F035D File Offset: 0x000EE55D
		public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x000F037B File Offset: 0x000EE57B
		public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x000F0399 File Offset: 0x000EE599
		public static FieldInfo GetRuntimeField(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetField(name);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x000F03B6 File Offset: 0x000EE5B6
		public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethod(name, parameters);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x000F03D4 File Offset: 0x000EE5D4
		public static PropertyInfo GetRuntimeProperty(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperty(name);
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x000F03F1 File Offset: 0x000EE5F1
		public static EventInfo GetRuntimeEvent(this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvent(name);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x000F040E File Offset: 0x000EE60E
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return method.GetBaseDefinition();
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x000F042A File Offset: 0x000EE62A
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x000F0447 File Offset: 0x000EE647
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}

		// Token: 0x04002EE5 RID: 12005
		private const BindingFlags Everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
	}
}
