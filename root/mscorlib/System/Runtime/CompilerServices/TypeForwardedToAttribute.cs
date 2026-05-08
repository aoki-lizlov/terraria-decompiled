using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007DA RID: 2010
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		// Token: 0x060045BB RID: 17851 RVA: 0x000E53B9 File Offset: 0x000E35B9
		public TypeForwardedToAttribute(Type destination)
		{
			this.Destination = destination;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060045BC RID: 17852 RVA: 0x000E53C8 File Offset: 0x000E35C8
		public Type Destination
		{
			[CompilerGenerated]
			get
			{
				return this.<Destination>k__BackingField;
			}
		}

		// Token: 0x04002CC5 RID: 11461
		[CompilerGenerated]
		private readonly Type <Destination>k__BackingField;
	}
}
