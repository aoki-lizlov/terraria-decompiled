using System;

namespace System.Reflection
{
	// Token: 0x020008C3 RID: 2243
	internal struct MonoEventInfo
	{
		// Token: 0x04002FC5 RID: 12229
		public Type declaring_type;

		// Token: 0x04002FC6 RID: 12230
		public Type reflected_type;

		// Token: 0x04002FC7 RID: 12231
		public string name;

		// Token: 0x04002FC8 RID: 12232
		public MethodInfo add_method;

		// Token: 0x04002FC9 RID: 12233
		public MethodInfo remove_method;

		// Token: 0x04002FCA RID: 12234
		public MethodInfo raise_method;

		// Token: 0x04002FCB RID: 12235
		public EventAttributes attrs;

		// Token: 0x04002FCC RID: 12236
		public MethodInfo[] other_methods;
	}
}
