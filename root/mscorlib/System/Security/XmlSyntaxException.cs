using System;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x0200039C RID: 924
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		// Token: 0x06002810 RID: 10256 RVA: 0x00092B99 File Offset: 0x00090D99
		public XmlSyntaxException()
		{
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x00092B99 File Offset: 0x00090D99
		public XmlSyntaxException(int lineNumber)
		{
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00092B99 File Offset: 0x00090D99
		public XmlSyntaxException(int lineNumber, string message)
		{
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00092B99 File Offset: 0x00090D99
		public XmlSyntaxException(string message)
		{
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00092B99 File Offset: 0x00090D99
		public XmlSyntaxException(string message, Exception inner)
		{
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000183F5 File Offset: 0x000165F5
		private XmlSyntaxException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
