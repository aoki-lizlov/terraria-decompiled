using System;

namespace rail
{
	// Token: 0x02000026 RID: 38
	public class IRailRoomHelperImpl : RailObject, IRailRoomHelper
	{
		// Token: 0x06001280 RID: 4736 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailRoomHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00006404 File Offset: 0x00004604
		~IRailRoomHelperImpl()
		{
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0000642C File Offset: 0x0000462C
		public virtual void set_current_zone_id(ulong zone_id)
		{
			RAIL_API_PINVOKE.IRailRoomHelper_set_current_zone_id(this.swigCPtr_, zone_id);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0000643A File Offset: 0x0000463A
		public virtual ulong get_current_zone_id()
		{
			return RAIL_API_PINVOKE.IRailRoomHelper_get_current_zone_id(this.swigCPtr_);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00006448 File Offset: 0x00004648
		public virtual IRailRoom CreateRoom(RoomOptions options, string room_name, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0(0UL));
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailRoom railRoom;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailRoomHelper_CreateRoom(this.swigCPtr_, intPtr, room_name, out result);
				railRoom = ((intPtr2 == IntPtr.Zero) ? null : new IRailRoomImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RoomOptions(intPtr);
			}
			return railRoom;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000064B4 File Offset: 0x000046B4
		public virtual IRailRoom AsyncCreateRoom(RoomOptions options, string room_name, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0(0UL));
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailRoom railRoom;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailRoomHelper_AsyncCreateRoom(this.swigCPtr_, intPtr, room_name, user_data);
				railRoom = ((intPtr2 == IntPtr.Zero) ? null : new IRailRoomImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RoomOptions(intPtr);
			}
			return railRoom;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00006520 File Offset: 0x00004720
		public virtual IRailRoom OpenRoom(ulong zone_id, ulong room_id, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoomHelper_OpenRoom(this.swigCPtr_, zone_id, room_id, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailRoomImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00006551 File Offset: 0x00004751
		public virtual RailResult AsyncGetUserRoomList(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoomHelper_AsyncGetUserRoomList(this.swigCPtr_, user_data);
		}
	}
}
