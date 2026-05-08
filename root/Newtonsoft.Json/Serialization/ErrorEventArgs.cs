using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000089 RID: 137
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00019560 File Offset: 0x00017760
		public object CurrentObject
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentObject>k__BackingField;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00019568 File Offset: 0x00017768
		public ErrorContext ErrorContext
		{
			[CompilerGenerated]
			get
			{
				return this.<ErrorContext>k__BackingField;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00019570 File Offset: 0x00017770
		public ErrorEventArgs(object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}

		// Token: 0x0400028D RID: 653
		[CompilerGenerated]
		private readonly object <CurrentObject>k__BackingField;

		// Token: 0x0400028E RID: 654
		[CompilerGenerated]
		private readonly ErrorContext <ErrorContext>k__BackingField;
	}
}
