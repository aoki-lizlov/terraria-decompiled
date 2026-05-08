using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000339 RID: 825
	internal sealed class ContinuationTaskFromTask : Task
	{
		// Token: 0x0600242B RID: 9259 RVA: 0x000829B8 File Offset: 0x00080BB8
		public ContinuationTaskFromTask(Task antecedent, Delegate action, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000829EC File Offset: 0x00080BEC
		internal override void InnerInvoke()
		{
			Task antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task> action = this.m_action as Action<Task>;
			if (action != null)
			{
				action(antecedent);
				return;
			}
			Action<Task, object> action2 = this.m_action as Action<Task, object>;
			if (action2 != null)
			{
				action2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001BF9 RID: 7161
		private Task m_antecedent;
	}
}
