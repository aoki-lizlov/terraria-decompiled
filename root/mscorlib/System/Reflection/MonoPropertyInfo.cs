using System;

namespace System.Reflection
{
	// Token: 0x020008CD RID: 2253
	internal struct MonoPropertyInfo
	{
		// Token: 0x04002FEB RID: 12267
		public Type parent;

		// Token: 0x04002FEC RID: 12268
		public Type declaring_type;

		// Token: 0x04002FED RID: 12269
		public string name;

		// Token: 0x04002FEE RID: 12270
		public MethodInfo get_method;

		// Token: 0x04002FEF RID: 12271
		public MethodInfo set_method;

		// Token: 0x04002FF0 RID: 12272
		public PropertyAttributes attrs;
	}
}
