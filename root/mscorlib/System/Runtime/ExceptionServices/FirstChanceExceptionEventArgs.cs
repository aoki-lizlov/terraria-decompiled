using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020007A0 RID: 1952
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		// Token: 0x0600453D RID: 17725 RVA: 0x000E4A73 File Offset: 0x000E2C73
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x000E4A82 File Offset: 0x000E2C82
		public Exception Exception
		{
			[CompilerGenerated]
			get
			{
				return this.<Exception>k__BackingField;
			}
		}

		// Token: 0x04002C94 RID: 11412
		[CompilerGenerated]
		private readonly Exception <Exception>k__BackingField;
	}
}
