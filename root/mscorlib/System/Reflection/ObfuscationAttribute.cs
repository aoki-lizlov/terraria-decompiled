using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000884 RID: 2180
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		// Token: 0x06004928 RID: 18728 RVA: 0x000EEBD3 File Offset: 0x000ECDD3
		public ObfuscationAttribute()
		{
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x000EEBFB File Offset: 0x000ECDFB
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x000EEC03 File Offset: 0x000ECE03
		public bool StripAfterObfuscation
		{
			[CompilerGenerated]
			get
			{
				return this.<StripAfterObfuscation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StripAfterObfuscation>k__BackingField = value;
			}
		} = true;

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x000EEC0C File Offset: 0x000ECE0C
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x000EEC14 File Offset: 0x000ECE14
		public bool Exclude
		{
			[CompilerGenerated]
			get
			{
				return this.<Exclude>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Exclude>k__BackingField = value;
			}
		} = true;

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x000EEC1D File Offset: 0x000ECE1D
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x000EEC25 File Offset: 0x000ECE25
		public bool ApplyToMembers
		{
			[CompilerGenerated]
			get
			{
				return this.<ApplyToMembers>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ApplyToMembers>k__BackingField = value;
			}
		} = true;

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x000EEC2E File Offset: 0x000ECE2E
		// (set) Token: 0x06004930 RID: 18736 RVA: 0x000EEC36 File Offset: 0x000ECE36
		public string Feature
		{
			[CompilerGenerated]
			get
			{
				return this.<Feature>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Feature>k__BackingField = value;
			}
		} = "all";

		// Token: 0x04002E74 RID: 11892
		[CompilerGenerated]
		private bool <StripAfterObfuscation>k__BackingField;

		// Token: 0x04002E75 RID: 11893
		[CompilerGenerated]
		private bool <Exclude>k__BackingField;

		// Token: 0x04002E76 RID: 11894
		[CompilerGenerated]
		private bool <ApplyToMembers>k__BackingField;

		// Token: 0x04002E77 RID: 11895
		[CompilerGenerated]
		private string <Feature>k__BackingField;
	}
}
