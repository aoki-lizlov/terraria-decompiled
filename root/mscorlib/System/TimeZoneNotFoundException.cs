using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200016B RID: 363
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class TimeZoneNotFoundException : Exception
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0000455D File Offset: 0x0000275D
		public TimeZoneNotFoundException()
		{
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002A236 File Offset: 0x00028436
		public TimeZoneNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002A23F File Offset: 0x0002843F
		public TimeZoneNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00018937 File Offset: 0x00016B37
		protected TimeZoneNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
