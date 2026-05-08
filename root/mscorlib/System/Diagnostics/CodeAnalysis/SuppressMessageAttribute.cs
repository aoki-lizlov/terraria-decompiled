using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A5A RID: 2650
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		// Token: 0x06006157 RID: 24919 RVA: 0x0014D955 File Offset: 0x0014BB55
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06006158 RID: 24920 RVA: 0x0014D96B File Offset: 0x0014BB6B
		public string Category
		{
			[CompilerGenerated]
			get
			{
				return this.<Category>k__BackingField;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06006159 RID: 24921 RVA: 0x0014D973 File Offset: 0x0014BB73
		public string CheckId
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckId>k__BackingField;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600615A RID: 24922 RVA: 0x0014D97B File Offset: 0x0014BB7B
		// (set) Token: 0x0600615B RID: 24923 RVA: 0x0014D983 File Offset: 0x0014BB83
		public string Scope
		{
			[CompilerGenerated]
			get
			{
				return this.<Scope>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Scope>k__BackingField = value;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x0014D98C File Offset: 0x0014BB8C
		// (set) Token: 0x0600615D RID: 24925 RVA: 0x0014D994 File Offset: 0x0014BB94
		public string Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Target>k__BackingField = value;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600615E RID: 24926 RVA: 0x0014D99D File Offset: 0x0014BB9D
		// (set) Token: 0x0600615F RID: 24927 RVA: 0x0014D9A5 File Offset: 0x0014BBA5
		public string MessageId
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MessageId>k__BackingField = value;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06006160 RID: 24928 RVA: 0x0014D9AE File Offset: 0x0014BBAE
		// (set) Token: 0x06006161 RID: 24929 RVA: 0x0014D9B6 File Offset: 0x0014BBB6
		public string Justification
		{
			[CompilerGenerated]
			get
			{
				return this.<Justification>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Justification>k__BackingField = value;
			}
		}

		// Token: 0x04003A79 RID: 14969
		[CompilerGenerated]
		private readonly string <Category>k__BackingField;

		// Token: 0x04003A7A RID: 14970
		[CompilerGenerated]
		private readonly string <CheckId>k__BackingField;

		// Token: 0x04003A7B RID: 14971
		[CompilerGenerated]
		private string <Scope>k__BackingField;

		// Token: 0x04003A7C RID: 14972
		[CompilerGenerated]
		private string <Target>k__BackingField;

		// Token: 0x04003A7D RID: 14973
		[CompilerGenerated]
		private string <MessageId>k__BackingField;

		// Token: 0x04003A7E RID: 14974
		[CompilerGenerated]
		private string <Justification>k__BackingField;
	}
}
