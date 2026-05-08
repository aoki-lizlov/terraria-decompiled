using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000909 RID: 2313
	internal class ModuleBuilderTokenGenerator : TokenGenerator
	{
		// Token: 0x06005144 RID: 20804 RVA: 0x000FFED7 File Offset: 0x000FE0D7
		public ModuleBuilderTokenGenerator(ModuleBuilder mb)
		{
			this.mb = mb;
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x000FFEE6 File Offset: 0x000FE0E6
		public int GetToken(string str)
		{
			return this.mb.GetToken(str);
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x000FFEF4 File Offset: 0x000FE0F4
		public int GetToken(MemberInfo member, bool create_open_instance)
		{
			return this.mb.GetToken(member, create_open_instance);
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x000FFF03 File Offset: 0x000FE103
		public int GetToken(MethodBase method, Type[] opt_param_types)
		{
			return this.mb.GetToken(method, opt_param_types);
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x000FFF12 File Offset: 0x000FE112
		public int GetToken(SignatureHelper helper)
		{
			return this.mb.GetToken(helper);
		}

		// Token: 0x0400317D RID: 12669
		private ModuleBuilder mb;
	}
}
