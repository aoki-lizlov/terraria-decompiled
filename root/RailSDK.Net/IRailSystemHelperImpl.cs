using System;

namespace rail
{
	// Token: 0x0200002E RID: 46
	public class IRailSystemHelperImpl : RailObject, IRailSystemHelper
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailSystemHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0000763C File Offset: 0x0000583C
		~IRailSystemHelperImpl()
		{
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00007664 File Offset: 0x00005864
		public virtual RailResult SetTerminationTimeoutOwnershipExpired(int timeout_seconds)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSystemHelper_SetTerminationTimeoutOwnershipExpired(this.swigCPtr_, timeout_seconds);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00007672 File Offset: 0x00005872
		public virtual RailSystemState GetPlatformSystemState()
		{
			return (RailSystemState)RAIL_API_PINVOKE.IRailSystemHelper_GetPlatformSystemState(this.swigCPtr_);
		}
	}
}
