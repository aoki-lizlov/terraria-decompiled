using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A46 RID: 2630
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x060060BE RID: 24766 RVA: 0x0014D46C File Offset: 0x0014B66C
		private EventCommandEventArgs()
		{
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x000174FB File Offset: 0x000156FB
		public IDictionary<string, string> Arguments
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x000174FB File Offset: 0x000156FB
		public EventCommand Command
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool DisableEvent(int eventId)
		{
			return true;
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool EnableEvent(int eventId)
		{
			return true;
		}
	}
}
