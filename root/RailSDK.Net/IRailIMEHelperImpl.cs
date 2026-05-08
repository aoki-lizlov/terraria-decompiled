using System;

namespace rail
{
	// Token: 0x0200001C RID: 28
	public class IRailIMEHelperImpl : RailObject, IRailIMEHelper
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailIMEHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00004BB8 File Offset: 0x00002DB8
		~IRailIMEHelperImpl()
		{
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public virtual RailResult EnableIMEHelperTextInputWindow(bool enable, RailWindowPosition position)
		{
			IntPtr intPtr = ((position == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailWindowPosition__SWIG_0());
			if (position != null)
			{
				RailConverter.Csharp2Cpp(position, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailIMEHelper_EnableIMEHelperTextInputWindow(this.swigCPtr_, enable, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailWindowPosition(intPtr);
			}
			return railResult;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00004C30 File Offset: 0x00002E30
		public virtual RailResult UpdateIMEHelperTextInputWindowPosition(RailWindowPosition position)
		{
			IntPtr intPtr = ((position == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailWindowPosition__SWIG_0());
			if (position != null)
			{
				RailConverter.Csharp2Cpp(position, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailIMEHelper_UpdateIMEHelperTextInputWindowPosition(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailWindowPosition(intPtr);
			}
			return railResult;
		}
	}
}
