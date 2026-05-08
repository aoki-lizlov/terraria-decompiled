using System;
using System.Runtime.Serialization;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200013C RID: 316
	[DataContract]
	public class ReportServerID
	{
		// Token: 0x06001C89 RID: 7305 RVA: 0x0000357B File Offset: 0x0000177B
		public ReportServerID()
		{
		}

		// Token: 0x040015E0 RID: 5600
		[DataMember]
		public string _serverID;
	}
}
