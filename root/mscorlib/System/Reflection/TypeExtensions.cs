using System;

namespace System.Reflection
{
	// Token: 0x020008A4 RID: 2212
	public static class TypeExtensions
	{
		// Token: 0x06004AB4 RID: 19124 RVA: 0x000EFEAB File Offset: 0x000EE0AB
		public static ConstructorInfo GetConstructor(Type type, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructor(types);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x000EFEBF File Offset: 0x000EE0BF
		public static ConstructorInfo[] GetConstructors(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors();
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x000EFED2 File Offset: 0x000EE0D2
		public static ConstructorInfo[] GetConstructors(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors(bindingAttr);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x000EFEE6 File Offset: 0x000EE0E6
		public static MemberInfo[] GetDefaultMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetDefaultMembers();
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x000EFEF9 File Offset: 0x000EE0F9
		public static EventInfo GetEvent(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name);
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x000EFF0D File Offset: 0x000EE10D
		public static EventInfo GetEvent(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x000EFF22 File Offset: 0x000EE122
		public static EventInfo[] GetEvents(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents();
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x000EFF35 File Offset: 0x000EE135
		public static EventInfo[] GetEvents(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents(bindingAttr);
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x000EFF49 File Offset: 0x000EE149
		public static FieldInfo GetField(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x000EFF5D File Offset: 0x000EE15D
		public static FieldInfo GetField(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name, bindingAttr);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x000EFF72 File Offset: 0x000EE172
		public static FieldInfo[] GetFields(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetFields();
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x000EFF85 File Offset: 0x000EE185
		public static FieldInfo[] GetFields(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetFields(bindingAttr);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x000EFF99 File Offset: 0x000EE199
		public static Type[] GetGenericArguments(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetGenericArguments();
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x000EFFAC File Offset: 0x000EE1AC
		public static Type[] GetInterfaces(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetInterfaces();
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x000EFFBF File Offset: 0x000EE1BF
		public static MemberInfo[] GetMember(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x000EFFD3 File Offset: 0x000EE1D3
		public static MemberInfo[] GetMember(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name, bindingAttr);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x000EFFE8 File Offset: 0x000EE1E8
		public static MemberInfo[] GetMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers();
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x000EFFFB File Offset: 0x000EE1FB
		public static MemberInfo[] GetMembers(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers(bindingAttr);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x000F000F File Offset: 0x000EE20F
		public static MethodInfo GetMethod(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x000F0023 File Offset: 0x000EE223
		public static MethodInfo GetMethod(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, bindingAttr);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x000F0038 File Offset: 0x000EE238
		public static MethodInfo GetMethod(Type type, string name, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, types);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x000F004D File Offset: 0x000EE24D
		public static MethodInfo[] GetMethods(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods();
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x000F0060 File Offset: 0x000EE260
		public static MethodInfo[] GetMethods(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods(bindingAttr);
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x000F0074 File Offset: 0x000EE274
		public static Type GetNestedType(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x000F0089 File Offset: 0x000EE289
		public static Type[] GetNestedTypes(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x000F009D File Offset: 0x000EE29D
		public static PropertyInfo[] GetProperties(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties();
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x000F00B0 File Offset: 0x000EE2B0
		public static PropertyInfo[] GetProperties(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties(bindingAttr);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x000F00C4 File Offset: 0x000EE2C4
		public static PropertyInfo GetProperty(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x000F00D8 File Offset: 0x000EE2D8
		public static PropertyInfo GetProperty(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, bindingAttr);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x000F00ED File Offset: 0x000EE2ED
		public static PropertyInfo GetProperty(Type type, string name, Type returnType)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x000F0102 File Offset: 0x000EE302
		public static PropertyInfo GetProperty(Type type, string name, Type returnType, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType, types);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x000F0118 File Offset: 0x000EE318
		public static bool IsAssignableFrom(Type type, Type c)
		{
			Requires.NotNull(type, "type");
			return type.IsAssignableFrom(c);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x000F012C File Offset: 0x000EE32C
		public static bool IsInstanceOfType(Type type, object o)
		{
			Requires.NotNull(type, "type");
			return type.IsInstanceOfType(o);
		}
	}
}
