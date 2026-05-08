using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D5 RID: 2005
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public class StateMachineAttribute : Attribute
	{
		// Token: 0x060045B3 RID: 17843 RVA: 0x000E5353 File Offset: 0x000E3553
		public StateMachineAttribute(Type stateMachineType)
		{
			this.StateMachineType = stateMachineType;
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060045B4 RID: 17844 RVA: 0x000E5362 File Offset: 0x000E3562
		public Type StateMachineType
		{
			[CompilerGenerated]
			get
			{
				return this.<StateMachineType>k__BackingField;
			}
		}

		// Token: 0x04002CC2 RID: 11458
		[CompilerGenerated]
		private readonly Type <StateMachineType>k__BackingField;
	}
}
