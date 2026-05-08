using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x0200000F RID: 15
	public class IRailDlcHelperImpl : RailObject, IRailDlcHelper
	{
		// Token: 0x06001120 RID: 4384 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailDlcHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00002B0C File Offset: 0x00000D0C
		~IRailDlcHelperImpl()
		{
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00002B34 File Offset: 0x00000D34
		public virtual RailResult AsyncQueryIsOwnedDlcsOnServer(List<RailDlcID> dlc_ids, string user_data)
		{
			IntPtr intPtr = ((dlc_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailDlcID__SWIG_0());
			if (dlc_ids != null)
			{
				RailConverter.Csharp2Cpp(dlc_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailDlcHelper_AsyncQueryIsOwnedDlcsOnServer(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailDlcID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00002B84 File Offset: 0x00000D84
		public virtual RailResult AsyncCheckAllDlcsStateReady(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailDlcHelper_AsyncCheckAllDlcsStateReady(this.swigCPtr_, user_data);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00002B94 File Offset: 0x00000D94
		public virtual bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_0(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
				installed_path = RAIL_API_PINVOKE.RailString_c_str(intPtr2);
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
			return flag;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00002C04 File Offset: 0x00000E04
		public virtual bool IsDlcInstalled(RailDlcID dlc_id)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsDlcInstalled__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00002C60 File Offset: 0x00000E60
		public virtual bool IsOwnedDlc(RailDlcID dlc_id)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_IsOwnedDlc(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00002CBC File Offset: 0x00000EBC
		public virtual uint GetDlcCount()
		{
			return RAIL_API_PINVOKE.IRailDlcHelper_GetDlcCount(this.swigCPtr_);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00002CCC File Offset: 0x00000ECC
		public virtual bool GetDlcInfo(uint index, RailDlcInfo dlc_info)
		{
			IntPtr intPtr = ((dlc_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcInfo__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_GetDlcInfo(this.swigCPtr_, index, intPtr);
			}
			finally
			{
				if (dlc_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, dlc_info);
				}
				RAIL_API_PINVOKE.delete_RailDlcInfo(intPtr);
			}
			return flag;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00002D1C File Offset: 0x00000F1C
		public virtual bool AsyncInstallDlc(RailDlcID dlc_id, string user_data)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_AsyncInstallDlc(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00002D78 File Offset: 0x00000F78
		public virtual bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data)
		{
			IntPtr intPtr = ((dlc_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDlcID__SWIG_0());
			if (dlc_id != null)
			{
				RailConverter.Csharp2Cpp(dlc_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailDlcHelper_AsyncRemoveDlc(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
			return flag;
		}
	}
}
