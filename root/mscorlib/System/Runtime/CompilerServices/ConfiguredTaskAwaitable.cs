using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E4 RID: 2020
	public readonly struct ConfiguredTaskAwaitable
	{
		// Token: 0x060045DD RID: 17885 RVA: 0x000E5825 File Offset: 0x000E3A25
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x000E5834 File Offset: 0x000E3A34
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002CCE RID: 11470
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x020007E5 RID: 2021
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IConfiguredTaskAwaiter
		{
			// Token: 0x060045DF RID: 17887 RVA: 0x000E583C File Offset: 0x000E3A3C
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000ACA RID: 2762
			// (get) Token: 0x060045E0 RID: 17888 RVA: 0x000E584C File Offset: 0x000E3A4C
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x060045E1 RID: 17889 RVA: 0x000E5859 File Offset: 0x000E3A59
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x060045E2 RID: 17890 RVA: 0x000E586E File Offset: 0x000E3A6E
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x060045E3 RID: 17891 RVA: 0x000E5883 File Offset: 0x000E3A83
			[StackTraceHidden]
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x04002CCF RID: 11471
			internal readonly Task m_task;

			// Token: 0x04002CD0 RID: 11472
			internal readonly bool m_continueOnCapturedContext;
		}
	}
}
