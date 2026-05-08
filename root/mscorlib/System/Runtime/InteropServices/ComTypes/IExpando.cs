using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000778 RID: 1912
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x060044B2 RID: 17586
		FieldInfo AddField(string name);

		// Token: 0x060044B3 RID: 17587
		PropertyInfo AddProperty(string name);

		// Token: 0x060044B4 RID: 17588
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x060044B5 RID: 17589
		void RemoveMember(MemberInfo m);
	}
}
