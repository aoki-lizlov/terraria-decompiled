using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E6 RID: 2022
	public readonly struct ConfiguredTaskAwaitable<TResult>
	{
		// Token: 0x060045E4 RID: 17892 RVA: 0x000E5890 File Offset: 0x000E3A90
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x000E589F File Offset: 0x000E3A9F
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x04002CD1 RID: 11473
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		// Token: 0x020007E7 RID: 2023
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion, IConfiguredTaskAwaiter
		{
			// Token: 0x060045E6 RID: 17894 RVA: 0x000E58A7 File Offset: 0x000E3AA7
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000ACB RID: 2763
			// (get) Token: 0x060045E7 RID: 17895 RVA: 0x000E58B7 File Offset: 0x000E3AB7
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			// Token: 0x060045E8 RID: 17896 RVA: 0x000E58C4 File Offset: 0x000E3AC4
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			// Token: 0x060045E9 RID: 17897 RVA: 0x000E58D9 File Offset: 0x000E3AD9
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			// Token: 0x060045EA RID: 17898 RVA: 0x000E58EE File Offset: 0x000E3AEE
			[StackTraceHidden]
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x04002CD2 RID: 11474
			private readonly Task<TResult> m_task;

			// Token: 0x04002CD3 RID: 11475
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
