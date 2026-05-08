using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000130 RID: 304
	public class AsyncTaskHelper
	{
		// Token: 0x06001C27 RID: 7207 RVA: 0x004FD87C File Offset: 0x004FBA7C
		private AsyncTaskHelper()
		{
			this._currentThreadRunner = new CurrentThreadRunner();
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x004FD890 File Offset: 0x004FBA90
		public void RunAsyncTaskAndReply(Action task, Action replay)
		{
			Task.Factory.StartNew(delegate
			{
				task();
				this._currentThreadRunner.Run(replay);
			});
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x004FD8CF File Offset: 0x004FBACF
		public void RunAsyncTask(Action task)
		{
			Task.Factory.StartNew(task);
		}

		// Token: 0x040015B4 RID: 5556
		private CurrentThreadRunner _currentThreadRunner;

		// Token: 0x02000734 RID: 1844
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x060040A2 RID: 16546 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x060040A3 RID: 16547 RVA: 0x0069EB8D File Offset: 0x0069CD8D
			internal void <RunAsyncTaskAndReply>b__0()
			{
				this.task();
				this.<>4__this._currentThreadRunner.Run(this.replay);
			}

			// Token: 0x040069A7 RID: 27047
			public Action task;

			// Token: 0x040069A8 RID: 27048
			public AsyncTaskHelper <>4__this;

			// Token: 0x040069A9 RID: 27049
			public Action replay;
		}
	}
}
