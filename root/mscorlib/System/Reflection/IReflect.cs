using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x02000872 RID: 2162
	public interface IReflect
	{
		// Token: 0x06004876 RID: 18550
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004877 RID: 18551
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06004878 RID: 18552
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06004879 RID: 18553
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600487A RID: 18554
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x0600487B RID: 18555
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x0600487C RID: 18556
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600487D RID: 18557
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x0600487E RID: 18558
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x0600487F RID: 18559
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06004880 RID: 18560
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004881 RID: 18561
		Type UnderlyingSystemType { get; }
	}
}
