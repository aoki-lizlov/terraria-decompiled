using System;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B9 RID: 1465
	internal class SoapHelper
	{
		// Token: 0x060038FB RID: 14587 RVA: 0x000CB3B7 File Offset: 0x000C95B7
		public static Exception GetException(ISoapXsd type, string msg)
		{
			return new RemotingException("Soap Parse error, xsd:type xsd:" + type.GetXsdType() + " " + msg);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000025CE File Offset: 0x000007CE
		public static string Normalize(string s)
		{
			return s;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapHelper()
		{
		}
	}
}
