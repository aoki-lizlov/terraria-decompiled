using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077C RID: 1916
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x060044D0 RID: 17616
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060044D1 RID: 17617
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x060044D2 RID: 17618
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x060044D3 RID: 17619
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x060044D4 RID: 17620
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x060044D5 RID: 17621
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x060044D6 RID: 17622
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060044D7 RID: 17623
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x060044D8 RID: 17624
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x060044D9 RID: 17625
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x060044DA RID: 17626
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060044DB RID: 17627
		Type UnderlyingSystemType { get; }
	}
}
