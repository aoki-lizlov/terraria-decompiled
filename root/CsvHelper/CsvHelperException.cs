using System;
using System.Runtime.Serialization;

namespace CsvHelper
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class CsvHelperException : Exception
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000225B File Offset: 0x0000045B
		public CsvHelperException()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002263 File Offset: 0x00000463
		public CsvHelperException(string message)
			: base(message)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000226C File Offset: 0x0000046C
		public CsvHelperException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002276 File Offset: 0x00000476
		public CsvHelperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
