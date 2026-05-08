using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A3C RID: 2620
	[Serializable]
	public class EventSourceException : Exception
	{
		// Token: 0x06006099 RID: 24729 RVA: 0x0014D2E8 File Offset: 0x0014B4E8
		public EventSourceException()
			: base("An error occurred when writing to a listener.")
		{
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x0002A236 File Offset: 0x00028436
		public EventSourceException(string message)
			: base(message)
		{
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x0002A23F File Offset: 0x0002843F
		public EventSourceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x00018937 File Offset: 0x00016B37
		protected EventSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x0014D2F5 File Offset: 0x0014B4F5
		internal EventSourceException(Exception innerException)
			: base("An error occurred when writing to a listener.", innerException)
		{
		}
	}
}
