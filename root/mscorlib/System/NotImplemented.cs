using System;

namespace System
{
	// Token: 0x02000190 RID: 400
	internal static class NotImplemented
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004E2CF File Offset: 0x0004C4CF
		internal static Exception ByDesign
		{
			get
			{
				return new NotImplementedException();
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004E2D6 File Offset: 0x0004C4D6
		internal static Exception ByDesignWithMessage(string message)
		{
			return new NotImplementedException(message);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004E2CF File Offset: 0x0004C4CF
		internal static Exception ActiveIssue(string issue)
		{
			return new NotImplementedException();
		}
	}
}
