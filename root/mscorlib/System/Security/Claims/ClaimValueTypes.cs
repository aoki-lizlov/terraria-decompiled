using System;
using System.Runtime.InteropServices;

namespace System.Security.Claims
{
	// Token: 0x020004C3 RID: 1219
	[ComVisible(false)]
	public static class ClaimValueTypes
	{
		// Token: 0x04002319 RID: 8985
		private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x0400231A RID: 8986
		public const string Base64Binary = "http://www.w3.org/2001/XMLSchema#base64Binary";

		// Token: 0x0400231B RID: 8987
		public const string Base64Octet = "http://www.w3.org/2001/XMLSchema#base64Octet";

		// Token: 0x0400231C RID: 8988
		public const string Boolean = "http://www.w3.org/2001/XMLSchema#boolean";

		// Token: 0x0400231D RID: 8989
		public const string Date = "http://www.w3.org/2001/XMLSchema#date";

		// Token: 0x0400231E RID: 8990
		public const string DateTime = "http://www.w3.org/2001/XMLSchema#dateTime";

		// Token: 0x0400231F RID: 8991
		public const string Double = "http://www.w3.org/2001/XMLSchema#double";

		// Token: 0x04002320 RID: 8992
		public const string Fqbn = "http://www.w3.org/2001/XMLSchema#fqbn";

		// Token: 0x04002321 RID: 8993
		public const string HexBinary = "http://www.w3.org/2001/XMLSchema#hexBinary";

		// Token: 0x04002322 RID: 8994
		public const string Integer = "http://www.w3.org/2001/XMLSchema#integer";

		// Token: 0x04002323 RID: 8995
		public const string Integer32 = "http://www.w3.org/2001/XMLSchema#integer32";

		// Token: 0x04002324 RID: 8996
		public const string Integer64 = "http://www.w3.org/2001/XMLSchema#integer64";

		// Token: 0x04002325 RID: 8997
		public const string Sid = "http://www.w3.org/2001/XMLSchema#sid";

		// Token: 0x04002326 RID: 8998
		public const string String = "http://www.w3.org/2001/XMLSchema#string";

		// Token: 0x04002327 RID: 8999
		public const string Time = "http://www.w3.org/2001/XMLSchema#time";

		// Token: 0x04002328 RID: 9000
		public const string UInteger32 = "http://www.w3.org/2001/XMLSchema#uinteger32";

		// Token: 0x04002329 RID: 9001
		public const string UInteger64 = "http://www.w3.org/2001/XMLSchema#uinteger64";

		// Token: 0x0400232A RID: 9002
		private const string SoapSchemaNamespace = "http://schemas.xmlsoap.org/";

		// Token: 0x0400232B RID: 9003
		public const string DnsName = "http://schemas.xmlsoap.org/claims/dns";

		// Token: 0x0400232C RID: 9004
		public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

		// Token: 0x0400232D RID: 9005
		public const string Rsa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa";

		// Token: 0x0400232E RID: 9006
		public const string UpnName = "http://schemas.xmlsoap.org/claims/UPN";

		// Token: 0x0400232F RID: 9007
		private const string XmlSignatureConstantsNamespace = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04002330 RID: 9008
		public const string DsaKeyValue = "http://www.w3.org/2000/09/xmldsig#DSAKeyValue";

		// Token: 0x04002331 RID: 9009
		public const string KeyInfo = "http://www.w3.org/2000/09/xmldsig#KeyInfo";

		// Token: 0x04002332 RID: 9010
		public const string RsaKeyValue = "http://www.w3.org/2000/09/xmldsig#RSAKeyValue";

		// Token: 0x04002333 RID: 9011
		private const string XQueryOperatorsNameSpace = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816";

		// Token: 0x04002334 RID: 9012
		public const string DaytimeDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#dayTimeDuration";

		// Token: 0x04002335 RID: 9013
		public const string YearMonthDuration = "http://www.w3.org/TR/2002/WD-xquery-operators-20020816#yearMonthDuration";

		// Token: 0x04002336 RID: 9014
		private const string Xacml10Namespace = "urn:oasis:names:tc:xacml:1.0";

		// Token: 0x04002337 RID: 9015
		public const string Rfc822Name = "urn:oasis:names:tc:xacml:1.0:data-type:rfc822Name";

		// Token: 0x04002338 RID: 9016
		public const string X500Name = "urn:oasis:names:tc:xacml:1.0:data-type:x500Name";
	}
}
