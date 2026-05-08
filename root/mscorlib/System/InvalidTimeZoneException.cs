using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000115 RID: 277
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class InvalidTimeZoneException : Exception
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000455D File Offset: 0x0000275D
		public InvalidTimeZoneException()
		{
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002A236 File Offset: 0x00028436
		public InvalidTimeZoneException(string message)
			: base(message)
		{
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002A23F File Offset: 0x0002843F
		public InvalidTimeZoneException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00018937 File Offset: 0x00016B37
		protected InvalidTimeZoneException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
