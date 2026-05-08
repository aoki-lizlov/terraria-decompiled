using System;

namespace System.Reflection
{
	// Token: 0x02000875 RID: 2165
	public struct InterfaceMapping
	{
		// Token: 0x04002E33 RID: 11827
		public Type TargetType;

		// Token: 0x04002E34 RID: 11828
		public Type InterfaceType;

		// Token: 0x04002E35 RID: 11829
		public MethodInfo[] TargetMethods;

		// Token: 0x04002E36 RID: 11830
		public MethodInfo[] InterfaceMethods;
	}
}
