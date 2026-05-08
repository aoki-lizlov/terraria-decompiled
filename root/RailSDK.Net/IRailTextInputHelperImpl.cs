using System;

namespace rail
{
	// Token: 0x0200002F RID: 47
	public class IRailTextInputHelperImpl : RailObject, IRailTextInputHelper
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailTextInputHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00007680 File Offset: 0x00005880
		~IRailTextInputHelperImpl()
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000076A8 File Offset: 0x000058A8
		public virtual RailResult ShowTextInputWindow(RailTextInputWindowOption options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailTextInputWindowOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailTextInputHelper_ShowTextInputWindow(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailTextInputWindowOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000076F8 File Offset: 0x000058F8
		public virtual void GetTextInputContent(out string content)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.IRailTextInputHelper_GetTextInputContent(this.swigCPtr_, intPtr);
			}
			finally
			{
				content = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00007738 File Offset: 0x00005938
		public virtual RailResult HideTextInputWindow()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailTextInputHelper_HideTextInputWindow(this.swigCPtr_);
		}
	}
}
