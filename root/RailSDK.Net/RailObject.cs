using System;

namespace rail
{
	// Token: 0x0200003A RID: 58
	public class RailObject
	{
		// Token: 0x06001380 RID: 4992 RVA: 0x00008C89 File Offset: 0x00006E89
		internal RailObject()
		{
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00008C9C File Offset: 0x00006E9C
		internal static IntPtr getCPtr(RailObject obj)
		{
			if (obj != null)
			{
				return obj.swigCPtr_;
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00008CB0 File Offset: 0x00006EB0
		~RailObject()
		{
		}

		// Token: 0x04000005 RID: 5
		protected IntPtr swigCPtr_ = IntPtr.Zero;
	}
}
