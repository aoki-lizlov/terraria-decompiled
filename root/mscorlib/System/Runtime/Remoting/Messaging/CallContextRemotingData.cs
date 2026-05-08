using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005D8 RID: 1496
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000CC8E7 File Offset: 0x000CAAE7
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x000CC8EF File Offset: 0x000CAAEF
		internal string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000CC8F8 File Offset: 0x000CAAF8
		internal bool HasInfo
		{
			get
			{
				return this._logicalCallID != null;
			}
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000CC903 File Offset: 0x000CAB03
		public object Clone()
		{
			return new CallContextRemotingData
			{
				LogicalCallID = this.LogicalCallID
			};
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000025BE File Offset: 0x000007BE
		public CallContextRemotingData()
		{
		}

		// Token: 0x040025DC RID: 9692
		private string _logicalCallID;
	}
}
