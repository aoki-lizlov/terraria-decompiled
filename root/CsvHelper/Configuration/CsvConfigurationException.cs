using System;
using System.Runtime.Serialization;

namespace CsvHelper.Configuration
{
	// Token: 0x02000038 RID: 56
	public class CsvConfigurationException : CsvHelperException
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x000021F3 File Offset: 0x000003F3
		public CsvConfigurationException()
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000021FB File Offset: 0x000003FB
		public CsvConfigurationException(string message)
			: base(message)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00002204 File Offset: 0x00000404
		public CsvConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000220E File Offset: 0x0000040E
		public CsvConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
