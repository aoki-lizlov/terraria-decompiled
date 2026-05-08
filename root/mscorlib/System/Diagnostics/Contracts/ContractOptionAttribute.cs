using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A26 RID: 2598
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractOptionAttribute : Attribute
	{
		// Token: 0x06006013 RID: 24595 RVA: 0x0014CD93 File Offset: 0x0014AF93
		public ContractOptionAttribute(string category, string setting, bool enabled)
		{
			this._category = category;
			this._setting = setting;
			this._enabled = enabled;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x0014CDB0 File Offset: 0x0014AFB0
		public ContractOptionAttribute(string category, string setting, string value)
		{
			this._category = category;
			this._setting = setting;
			this._value = value;
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x0014CDCD File Offset: 0x0014AFCD
		public string Category
		{
			get
			{
				return this._category;
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06006016 RID: 24598 RVA: 0x0014CDD5 File Offset: 0x0014AFD5
		public string Setting
		{
			get
			{
				return this._setting;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x0014CDDD File Offset: 0x0014AFDD
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06006018 RID: 24600 RVA: 0x0014CDE5 File Offset: 0x0014AFE5
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040039D4 RID: 14804
		private string _category;

		// Token: 0x040039D5 RID: 14805
		private string _setting;

		// Token: 0x040039D6 RID: 14806
		private bool _enabled;

		// Token: 0x040039D7 RID: 14807
		private string _value;
	}
}
