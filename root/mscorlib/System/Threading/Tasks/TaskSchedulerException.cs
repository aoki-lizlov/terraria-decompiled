using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public class TaskSchedulerException : Exception
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x0007881D File Offset: 0x00076A1D
		public TaskSchedulerException()
			: base("An exception was thrown by a TaskScheduler.")
		{
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0002A236 File Offset: 0x00028436
		public TaskSchedulerException(string message)
			: base(message)
		{
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0007882A File Offset: 0x00076A2A
		public TaskSchedulerException(Exception innerException)
			: base("An exception was thrown by a TaskScheduler.", innerException)
		{
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0002A23F File Offset: 0x0002843F
		public TaskSchedulerException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00018937 File Offset: 0x00016B37
		protected TaskSchedulerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
