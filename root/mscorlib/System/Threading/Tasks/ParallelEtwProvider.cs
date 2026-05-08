using System;
using System.Diagnostics.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x020002F5 RID: 757
	[EventSource(Name = "System.Threading.Tasks.Parallel.EventSource")]
	internal sealed class ParallelEtwProvider : EventSource
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x0007B6EC File Offset: 0x000798EC
		private ParallelEtwProvider()
		{
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0007B6F4 File Offset: 0x000798F4
		[Event(1, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Start)]
		public unsafe void ParallelLoopBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, ParallelEtwProvider.ForkJoinOperationType OperationType, long InclusiveFrom, long ExclusiveTo)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OperationType))
				};
				ptr[4] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&InclusiveFrom))
				};
				ptr[5] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&ExclusiveTo))
				};
				base.WriteEventCore(1, 6, ptr);
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0007B838 File Offset: 0x00079A38
		[Event(2, Level = EventLevel.Informational, Task = (EventTask)1, Opcode = EventOpcode.Stop)]
		public unsafe void ParallelLoopEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, long TotalIterations)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 8,
					DataPointer = (IntPtr)((void*)(&TotalIterations))
				};
				base.WriteEventCore(2, 4, ptr);
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0007B91C File Offset: 0x00079B1C
		[Event(3, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Start)]
		public unsafe void ParallelInvokeBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID, ParallelEtwProvider.ForkJoinOperationType OperationType, int ActionCount)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				*ptr = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID))
				};
				ptr[1] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OriginatingTaskID))
				};
				ptr[2] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ForkJoinContextID))
				};
				ptr[3] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&OperationType))
				};
				ptr[4] = new EventSource.EventData
				{
					Size = 4,
					DataPointer = (IntPtr)((void*)(&ActionCount))
				};
				base.WriteEventCore(3, 5, ptr);
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0007BA2F File Offset: 0x00079C2F
		[Event(4, Level = EventLevel.Informational, Task = (EventTask)2, Opcode = EventOpcode.Stop)]
		public void ParallelInvokeEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(4, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0007BA46 File Offset: 0x00079C46
		[Event(5, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Start)]
		public void ParallelFork(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0007BA5D File Offset: 0x00079C5D
		[Event(6, Level = EventLevel.Verbose, Task = (EventTask)5, Opcode = EventOpcode.Stop)]
		public void ParallelJoin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ForkJoinContextID)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(6, OriginatingTaskSchedulerID, OriginatingTaskID, ForkJoinContextID);
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0007BA74 File Offset: 0x00079C74
		// Note: this type is marked as 'beforefieldinit'.
		static ParallelEtwProvider()
		{
		}

		// Token: 0x04001AEF RID: 6895
		public static readonly ParallelEtwProvider Log = new ParallelEtwProvider();

		// Token: 0x04001AF0 RID: 6896
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04001AF1 RID: 6897
		private const int PARALLELLOOPBEGIN_ID = 1;

		// Token: 0x04001AF2 RID: 6898
		private const int PARALLELLOOPEND_ID = 2;

		// Token: 0x04001AF3 RID: 6899
		private const int PARALLELINVOKEBEGIN_ID = 3;

		// Token: 0x04001AF4 RID: 6900
		private const int PARALLELINVOKEEND_ID = 4;

		// Token: 0x04001AF5 RID: 6901
		private const int PARALLELFORK_ID = 5;

		// Token: 0x04001AF6 RID: 6902
		private const int PARALLELJOIN_ID = 6;

		// Token: 0x020002F6 RID: 758
		public enum ForkJoinOperationType
		{
			// Token: 0x04001AF8 RID: 6904
			ParallelInvoke = 1,
			// Token: 0x04001AF9 RID: 6905
			ParallelFor,
			// Token: 0x04001AFA RID: 6906
			ParallelForEach
		}

		// Token: 0x020002F7 RID: 759
		public class Tasks
		{
			// Token: 0x060021EE RID: 8686 RVA: 0x000025BE File Offset: 0x000007BE
			public Tasks()
			{
			}

			// Token: 0x04001AFB RID: 6907
			public const EventTask Loop = (EventTask)1;

			// Token: 0x04001AFC RID: 6908
			public const EventTask Invoke = (EventTask)2;

			// Token: 0x04001AFD RID: 6909
			public const EventTask ForkJoin = (EventTask)5;
		}
	}
}
