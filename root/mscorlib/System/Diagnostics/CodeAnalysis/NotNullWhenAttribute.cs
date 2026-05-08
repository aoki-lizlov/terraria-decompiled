using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A60 RID: 2656
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06006168 RID: 24936 RVA: 0x0014D9D6 File Offset: 0x0014BBD6
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06006169 RID: 24937 RVA: 0x0014D9E5 File Offset: 0x0014BBE5
		public bool ReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnValue>k__BackingField;
			}
		}

		// Token: 0x04003A80 RID: 14976
		[CompilerGenerated]
		private readonly bool <ReturnValue>k__BackingField;
	}
}
