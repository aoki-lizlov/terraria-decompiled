using System;

namespace rail
{
	// Token: 0x02000037 RID: 55
	public class IRailZoneServerHelperImpl : RailObject, IRailZoneServerHelper
	{
		// Token: 0x0600136E RID: 4974 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailZoneServerHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00008AC4 File Offset: 0x00006CC4
		~IRailZoneServerHelperImpl()
		{
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00008AEC File Offset: 0x00006CEC
		public virtual RailZoneID GetPlayerSelectedZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServerHelper_GetPlayerSelectedZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00008B14 File Offset: 0x00006D14
		public virtual RailZoneID GetRootZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServerHelper_GetRootZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00008B3C File Offset: 0x00006D3C
		public virtual IRailZoneServer OpenZoneServer(RailZoneID zone_id, out RailResult result)
		{
			IntPtr intPtr = ((zone_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailZoneID__SWIG_0());
			if (zone_id != null)
			{
				RailConverter.Csharp2Cpp(zone_id, intPtr);
			}
			IRailZoneServer railZoneServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailZoneServerHelper_OpenZoneServer(this.swigCPtr_, intPtr, out result);
				railZoneServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailZoneServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
			return railZoneServer;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public virtual RailResult AsyncSwitchPlayerSelectedZone(RailZoneID zone_id)
		{
			IntPtr intPtr = ((zone_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailZoneID__SWIG_0());
			if (zone_id != null)
			{
				RailConverter.Csharp2Cpp(zone_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServerHelper_AsyncSwitchPlayerSelectedZone(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
			return railResult;
		}
	}
}
