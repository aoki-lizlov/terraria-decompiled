using System;

namespace System
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	internal class ReflectionOnlyType : RuntimeType
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x0005B002 File Offset: 0x00059202
		private ReflectionOnlyType()
		{
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0005B00A File Offset: 0x0005920A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
		}
	}
}
