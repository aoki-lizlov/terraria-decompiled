using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200057D RID: 1405
	[Serializable]
	internal class CrossAppDomainData
	{
		// Token: 0x060037E8 RID: 14312 RVA: 0x000C9CDA File Offset: 0x000C7EDA
		internal CrossAppDomainData(int domainId)
		{
			this._ContextID = 0;
			this._DomainID = domainId;
			this._processGuid = RemotingConfiguration.ProcessId;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x000C9D00 File Offset: 0x000C7F00
		internal int DomainID
		{
			get
			{
				return this._DomainID;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000C9D08 File Offset: 0x000C7F08
		internal string ProcessID
		{
			get
			{
				return this._processGuid;
			}
		}

		// Token: 0x04002561 RID: 9569
		private object _ContextID;

		// Token: 0x04002562 RID: 9570
		private int _DomainID;

		// Token: 0x04002563 RID: 9571
		private string _processGuid;
	}
}
