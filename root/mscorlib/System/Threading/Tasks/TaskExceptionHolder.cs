using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000344 RID: 836
	internal class TaskExceptionHolder
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x0008315D File Offset: 0x0008135D
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0000408A File Offset: 0x0000228A
		private static bool ShouldFailFastOnUnobservedException()
		{
			return false;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0008316C File Offset: 0x0008136C
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted)
				{
					AggregateException ex = new AggregateException("A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.", this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000831E4 File Offset: 0x000813E4
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000831F1 File Offset: 0x000813F1
		internal void Add(object exceptionObject)
		{
			this.Add(exceptionObject, false);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000831FB File Offset: 0x000813FB
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00083210 File Offset: 0x00081410
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = exceptionDispatchInfo;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0008324C File Offset: 0x0008144C
		private void AddFaultException(object exceptionObject)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> lowLevelListWithIList = this.m_faultExceptions;
			if (lowLevelListWithIList == null)
			{
				lowLevelListWithIList = (this.m_faultExceptions = new LowLevelListWithIList<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					lowLevelListWithIList.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception ex2 = enumerator.Current;
								lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(ex2));
							}
							goto IL_00AE;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException("(Internal)Expected an Exception or an IEnumerable<Exception>", "exceptionObject");
					}
					lowLevelListWithIList.AddRange(enumerable2);
				}
			}
			IL_00AE:
			if (lowLevelListWithIList.Count > 0)
			{
				this.MarkAsUnhandled();
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00083328 File Offset: 0x00081528
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00083343 File Offset: 0x00081543
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00083364 File Offset: 0x00081564
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000833C6 File Offset: 0x000815C6
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			IList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000833DC File Offset: 0x000815DC
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000833E4 File Offset: 0x000815E4
		// Note: this type is marked as 'beforefieldinit'.
		static TaskExceptionHolder()
		{
		}

		// Token: 0x04001C0A RID: 7178
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04001C0B RID: 7179
		private readonly Task m_task;

		// Token: 0x04001C0C RID: 7180
		private volatile LowLevelListWithIList<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04001C0D RID: 7181
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04001C0E RID: 7182
		private volatile bool m_isHandled;
	}
}
