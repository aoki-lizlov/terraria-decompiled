using System;

namespace System.Security
{
	// Token: 0x020003A8 RID: 936
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		// Token: 0x06002822 RID: 10274 RVA: 0x00092BC9 File Offset: 0x00090DC9
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00092BD8 File Offset: 0x00090DD8
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x00092BE0 File Offset: 0x00090DE0
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00092BE9 File Offset: 0x00090DE9
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x04001D71 RID: 7537
		private SecurityRuleSet m_ruleSet;

		// Token: 0x04001D72 RID: 7538
		private bool m_skipVerificationInFullTrust;
	}
}
