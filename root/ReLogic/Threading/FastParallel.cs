using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReLogic.Threading
{
	// Token: 0x0200000D RID: 13
	public static class FastParallel
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003A24 File Offset: 0x00001C24
		static FastParallel()
		{
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003A33 File Offset: 0x00001C33
		public static bool ForceTasksOnCallingThread
		{
			[CompilerGenerated]
			get
			{
				return FastParallel.<ForceTasksOnCallingThread>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				FastParallel.<ForceTasksOnCallingThread>k__BackingField = value;
			}
		} = false;

		// Token: 0x06000096 RID: 150 RVA: 0x00003A3C File Offset: 0x00001C3C
		public static void For(int fromInclusive, int toExclusive, ParallelForAction callback, object context = null)
		{
			int num = toExclusive - fromInclusive;
			if (num == 0)
			{
				return;
			}
			int num2 = Math.Min(Math.Max(1, Environment.ProcessorCount + 1 - 1 - 1), num);
			if (FastParallel.ForceTasksOnCallingThread)
			{
				num2 = 1;
			}
			ThreadPriority priority = Thread.CurrentThread.Priority;
			Thread.CurrentThread.Priority = 4;
			int num3 = num / num2;
			int num4 = num % num2;
			CountdownEvent countdownEvent = new CountdownEvent(num2);
			int num5 = toExclusive;
			for (int i = num2 - 1; i >= 0; i--)
			{
				int num6 = num3;
				if (i < num4)
				{
					num6++;
				}
				num5 -= num6;
				int num7 = num5;
				int num8 = num7 + num6;
				FastParallel.RangeTask rangeTask = new FastParallel.RangeTask(callback, num7, num8, context, countdownEvent);
				if (i < 1)
				{
					FastParallel.InvokeTask(rangeTask);
				}
				else
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(FastParallel.InvokeTask), rangeTask);
				}
			}
			while (countdownEvent.CurrentCount != 0)
			{
			}
			Thread.CurrentThread.Priority = priority;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B14 File Offset: 0x00001D14
		private static void InvokeTask(object context)
		{
			((FastParallel.RangeTask)context).Invoke();
		}

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		private static bool <ForceTasksOnCallingThread>k__BackingField;

		// Token: 0x020000B0 RID: 176
		private class RangeTask
		{
			// Token: 0x06000410 RID: 1040 RVA: 0x0000DEFE File Offset: 0x0000C0FE
			public RangeTask(ParallelForAction action, int fromInclusive, int toExclusive, object context, CountdownEvent countdown)
			{
				this._action = action;
				this._fromInclusive = fromInclusive;
				this._toExclusive = toExclusive;
				this._context = context;
				this._countdown = countdown;
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000DF2B File Offset: 0x0000C12B
			public void Invoke()
			{
				if (this._fromInclusive != this._toExclusive)
				{
					this._action(this._fromInclusive, this._toExclusive, this._context);
				}
				this._countdown.Signal();
			}

			// Token: 0x04000549 RID: 1353
			private readonly ParallelForAction _action;

			// Token: 0x0400054A RID: 1354
			private readonly int _fromInclusive;

			// Token: 0x0400054B RID: 1355
			private readonly int _toExclusive;

			// Token: 0x0400054C RID: 1356
			private readonly object _context;

			// Token: 0x0400054D RID: 1357
			private readonly CountdownEvent _countdown;
		}
	}
}
