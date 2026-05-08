using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x02000862 RID: 2146
	public abstract class Binder
	{
		// Token: 0x0600480D RID: 18445 RVA: 0x000025BE File Offset: 0x000007BE
		protected Binder()
		{
		}

		// Token: 0x0600480E RID: 18446
		public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

		// Token: 0x0600480F RID: 18447
		public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

		// Token: 0x06004810 RID: 18448
		public abstract object ChangeType(object value, Type type, CultureInfo culture);

		// Token: 0x06004811 RID: 18449
		public abstract void ReorderArgumentArray(ref object[] args, object state);

		// Token: 0x06004812 RID: 18450
		public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004813 RID: 18451
		public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

		// Token: 0x06004814 RID: 18452 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool CanChangeType(object value, Type type, CultureInfo culture)
		{
			return false;
		}
	}
}
