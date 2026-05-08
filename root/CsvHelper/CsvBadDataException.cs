using System;
using System.Runtime.Serialization;

namespace CsvHelper
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class CsvBadDataException : CsvHelperException
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021F3 File Offset: 0x000003F3
		public CsvBadDataException()
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FB File Offset: 0x000003FB
		public CsvBadDataException(string message)
			: base(message)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002204 File Offset: 0x00000404
		public CsvBadDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000220E File Offset: 0x0000040E
		public CsvBadDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
