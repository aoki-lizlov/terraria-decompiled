using System;
using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
	// Token: 0x0200076B RID: 1899
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	[ComVisible(true)]
	public interface IExpando : IReflect
	{
		// Token: 0x06004485 RID: 17541
		FieldInfo AddField(string name);

		// Token: 0x06004486 RID: 17542
		PropertyInfo AddProperty(string name);

		// Token: 0x06004487 RID: 17543
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x06004488 RID: 17544
		void RemoveMember(MemberInfo m);
	}
}
