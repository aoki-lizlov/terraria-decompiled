using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008FC RID: 2300
	internal interface TokenGenerator
	{
		// Token: 0x06004FFE RID: 20478
		int GetToken(string str);

		// Token: 0x06004FFF RID: 20479
		int GetToken(MemberInfo member, bool create_open_instance);

		// Token: 0x06005000 RID: 20480
		int GetToken(MethodBase method, Type[] opt_param_types);

		// Token: 0x06005001 RID: 20481
		int GetToken(SignatureHelper helper);
	}
}
