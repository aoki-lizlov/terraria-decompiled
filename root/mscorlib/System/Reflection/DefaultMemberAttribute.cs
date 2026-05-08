using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000867 RID: 2151
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class DefaultMemberAttribute : Attribute
	{
		// Token: 0x06004822 RID: 18466 RVA: 0x000EDC6F File Offset: 0x000EBE6F
		public DefaultMemberAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x000EDC7E File Offset: 0x000EBE7E
		public string MemberName
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberName>k__BackingField;
			}
		}

		// Token: 0x04002E05 RID: 11781
		[CompilerGenerated]
		private readonly string <MemberName>k__BackingField;
	}
}
