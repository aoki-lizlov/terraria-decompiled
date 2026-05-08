using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008EF RID: 2287
	internal class DynamicMethodTokenGenerator : TokenGenerator
	{
		// Token: 0x06004F06 RID: 20230 RVA: 0x000F9D13 File Offset: 0x000F7F13
		public DynamicMethodTokenGenerator(DynamicMethod m)
		{
			this.m = m;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x000F9D22 File Offset: 0x000F7F22
		public int GetToken(string str)
		{
			return this.m.AddRef(str);
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x00084CDD File Offset: 0x00082EDD
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x000F9D22 File Offset: 0x000F7F22
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.m.AddRef(member);
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x000F9D22 File Offset: 0x000F7F22
		public int GetToken(SignatureHelper helper)
		{
			return this.m.AddRef(helper);
		}

		// Token: 0x040030C9 RID: 12489
		private DynamicMethod m;
	}
}
