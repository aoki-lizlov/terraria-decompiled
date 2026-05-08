using System;
using System.Runtime.ExceptionServices;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000B60 RID: 2912
	internal class RuntimeAugments
	{
		// Token: 0x06006AAA RID: 27306 RVA: 0x000E4B23 File Offset: 0x000E2D23
		public static void ReportUnhandledException(Exception exception)
		{
			ExceptionDispatchInfo.Capture(exception).Throw();
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06006AAB RID: 27307 RVA: 0x0016ECC9 File Offset: 0x0016CEC9
		internal static ReflectionExecutionDomainCallbacks Callbacks
		{
			get
			{
				return RuntimeAugments.s_reflectionExecutionDomainCallbacks;
			}
		}

		// Token: 0x06006AAC RID: 27308 RVA: 0x000025BE File Offset: 0x000007BE
		public RuntimeAugments()
		{
		}

		// Token: 0x06006AAD RID: 27309 RVA: 0x0016ECD0 File Offset: 0x0016CED0
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeAugments()
		{
		}

		// Token: 0x04003D70 RID: 15728
		private static ReflectionExecutionDomainCallbacks s_reflectionExecutionDomainCallbacks = new ReflectionExecutionDomainCallbacks();
	}
}
