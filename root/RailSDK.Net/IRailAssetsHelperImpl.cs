using System;

namespace rail
{
	// Token: 0x0200000B RID: 11
	public class IRailAssetsHelperImpl : RailObject, IRailAssetsHelper
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailAssetsHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00002618 File Offset: 0x00000818
		~IRailAssetsHelperImpl()
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00002640 File Offset: 0x00000840
		public virtual IRailAssets OpenAssets()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailAssetsHelper_OpenAssets(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAssetsImpl(intPtr);
			}
			return null;
		}
	}
}
