using System;
using System.Runtime.Serialization;

namespace CsvHelper
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class CsvMissingFieldException : CsvReaderException
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002280 File Offset: 0x00000480
		public CsvMissingFieldException()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002288 File Offset: 0x00000488
		public CsvMissingFieldException(string message)
			: base(message)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002291 File Offset: 0x00000491
		public CsvMissingFieldException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000229B File Offset: 0x0000049B
		public CsvMissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
