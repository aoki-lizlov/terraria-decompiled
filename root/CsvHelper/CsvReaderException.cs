using System;
using System.Runtime.Serialization;

namespace CsvHelper
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class CsvReaderException : CsvHelperException
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000021F3 File Offset: 0x000003F3
		public CsvReaderException()
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000021FB File Offset: 0x000003FB
		public CsvReaderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002204 File Offset: 0x00000404
		public CsvReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000220E File Offset: 0x0000040E
		public CsvReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
