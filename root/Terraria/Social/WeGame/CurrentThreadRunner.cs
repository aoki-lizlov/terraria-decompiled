using System;
using System.Windows.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200012F RID: 303
	public class CurrentThreadRunner
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x004FD854 File Offset: 0x004FBA54
		public CurrentThreadRunner()
		{
			this._dsipatcher = Dispatcher.CurrentDispatcher;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x004FD867 File Offset: 0x004FBA67
		public void Run(Action f)
		{
			this._dsipatcher.BeginInvoke(f, new object[0]);
		}

		// Token: 0x040015B3 RID: 5555
		private Dispatcher _dsipatcher;
	}
}
