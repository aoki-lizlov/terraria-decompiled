using System;
using System.Runtime.Serialization;

namespace CsvHelper.TypeConversion
{
	// Token: 0x0200001C RID: 28
	public class CsvTypeConverterException : CsvHelperException
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000021F3 File Offset: 0x000003F3
		public CsvTypeConverterException()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000021FB File Offset: 0x000003FB
		public CsvTypeConverterException(string message)
			: base(message)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00002204 File Offset: 0x00000404
		public CsvTypeConverterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000220E File Offset: 0x0000040E
		public CsvTypeConverterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
