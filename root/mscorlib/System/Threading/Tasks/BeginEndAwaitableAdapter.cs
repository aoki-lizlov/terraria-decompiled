using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002E5 RID: 741
	internal sealed class BeginEndAwaitableAdapter : RendezvousAwaitable<IAsyncResult>
	{
		// Token: 0x06002177 RID: 8567 RVA: 0x00079298 File Offset: 0x00077498
		public BeginEndAwaitableAdapter()
		{
			base.RunContinuationsAsynchronously = false;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000792A7 File Offset: 0x000774A7
		// Note: this type is marked as 'beforefieldinit'.
		static BeginEndAwaitableAdapter()
		{
		}

		// Token: 0x04001AAA RID: 6826
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			((BeginEndAwaitableAdapter)asyncResult.AsyncState).SetResult(asyncResult);
		};

		// Token: 0x020002E6 RID: 742
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002179 RID: 8569 RVA: 0x000792BE File Offset: 0x000774BE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600217A RID: 8570 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600217B RID: 8571 RVA: 0x000792CA File Offset: 0x000774CA
			internal void <.cctor>b__2_0(IAsyncResult asyncResult)
			{
				((BeginEndAwaitableAdapter)asyncResult.AsyncState).SetResult(asyncResult);
			}

			// Token: 0x04001AAB RID: 6827
			public static readonly BeginEndAwaitableAdapter.<>c <>9 = new BeginEndAwaitableAdapter.<>c();
		}
	}
}
