using System;

namespace System.Reflection
{
	// Token: 0x02000871 RID: 2161
	public interface ICustomAttributeProvider
	{
		// Token: 0x06004873 RID: 18547
		object[] GetCustomAttributes(bool inherit);

		// Token: 0x06004874 RID: 18548
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x06004875 RID: 18549
		bool IsDefined(Type attributeType, bool inherit);
	}
}
