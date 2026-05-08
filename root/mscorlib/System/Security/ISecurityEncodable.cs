using System;

namespace System.Security
{
	// Token: 0x02000396 RID: 918
	public interface ISecurityEncodable
	{
		// Token: 0x06002807 RID: 10247
		void FromXml(SecurityElement e);

		// Token: 0x06002808 RID: 10248
		SecurityElement ToXml();
	}
}
