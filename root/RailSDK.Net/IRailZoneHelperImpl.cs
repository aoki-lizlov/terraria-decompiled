using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000035 RID: 53
	public class IRailZoneHelperImpl : RailObject, IRailZoneHelper
	{
		// Token: 0x0600135A RID: 4954 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailZoneHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00008774 File Offset: 0x00006974
		~IRailZoneHelperImpl()
		{
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0000879C File Offset: 0x0000699C
		public virtual RailResult AsyncGetZoneList(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailZoneHelper_AsyncGetZoneList(this.swigCPtr_, user_data);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000087AC File Offset: 0x000069AC
		public virtual RailResult AsyncGetRoomListInZone(ulong zone_id, uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<RoomInfoListFilter> filter, string user_data)
		{
			IntPtr intPtr = ((sorter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListSorter__SWIG_0());
			if (sorter != null)
			{
				RailConverter.Csharp2Cpp(sorter, intPtr);
			}
			IntPtr intPtr2 = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneHelper_AsyncGetRoomListInZone(this.swigCPtr_, zone_id, start_index, end_index, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRoomInfoListSorter(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRoomInfoListFilter(intPtr2);
			}
			return railResult;
		}
	}
}
