using System;
using System.Runtime.Serialization;

namespace CsvHelper
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class CsvParserException : CsvHelperException
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000021F3 File Offset: 0x000003F3
		public CsvParserException()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000021FB File Offset: 0x000003FB
		public CsvParserException(string message)
			: base(message)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002204 File Offset: 0x00000404
		public CsvParserException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000220E File Offset: 0x0000040E
		public CsvParserException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
