using System;

namespace rail
{
	// Token: 0x02000032 RID: 50
	public class IRailUtilsImpl : RailObject, IRailUtils
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x00002137 File Offset: 0x00000337
		internal IRailUtilsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00008010 File Offset: 0x00006210
		~IRailUtilsImpl()
		{
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00008038 File Offset: 0x00006238
		public virtual uint GetTimeCountSinceGameLaunch()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceGameLaunch(this.swigCPtr_);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00008045 File Offset: 0x00006245
		public virtual uint GetTimeCountSinceComputerLaunch()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeCountSinceComputerLaunch(this.swigCPtr_);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00008052 File Offset: 0x00006252
		public virtual uint GetTimeFromServer()
		{
			return RAIL_API_PINVOKE.IRailUtils_GetTimeFromServer(this.swigCPtr_);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0000805F File Offset: 0x0000625F
		public virtual RailResult AsyncGetImageData(string image_path, uint scale_to_width, uint scale_to_height, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUtils_AsyncGetImageData(this.swigCPtr_, image_path, scale_to_width, scale_to_height, user_data);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00008074 File Offset: 0x00006274
		public virtual void GetErrorString(RailResult result, out string error_string)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.IRailUtils_GetErrorString(this.swigCPtr_, (int)result, intPtr);
			}
			finally
			{
				error_string = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000080B8 File Offset: 0x000062B8
		public virtual RailResult DirtyWordsFilter(string words, bool replace_sensitive, RailDirtyWordsCheckResult check_result)
		{
			IntPtr intPtr = ((check_result == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailDirtyWordsCheckResult__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_DirtyWordsFilter(this.swigCPtr_, words, replace_sensitive, intPtr);
			}
			finally
			{
				if (check_result != null)
				{
					RailConverter.Cpp2Csharp(intPtr, check_result);
				}
				RAIL_API_PINVOKE.delete_RailDirtyWordsCheckResult(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00008108 File Offset: 0x00006308
		public virtual EnumRailPlatformType GetRailPlatformType()
		{
			return (EnumRailPlatformType)RAIL_API_PINVOKE.IRailUtils_GetRailPlatformType(this.swigCPtr_);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00008118 File Offset: 0x00006318
		public virtual RailResult GetLaunchAppParameters(EnumRailLaunchAppType app_type, out string parameter)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetLaunchAppParameters(this.swigCPtr_, (int)app_type, intPtr);
			}
			finally
			{
				parameter = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0000815C File Offset: 0x0000635C
		public virtual RailResult GetPlatformLanguageCode(out string language_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetPlatformLanguageCode(this.swigCPtr_, intPtr);
			}
			finally
			{
				language_code = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000081A0 File Offset: 0x000063A0
		public virtual RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailUtils_SetWarningMessageCallback(this.swigCPtr_, callback);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000081B0 File Offset: 0x000063B0
		public virtual RailResult GetCountryCodeOfCurrentLoggedInIP(out string country_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailUtils_GetCountryCodeOfCurrentLoggedInIP(this.swigCPtr_, intPtr);
			}
			finally
			{
				country_code = RAIL_API_PINVOKE.RailString_c_str(intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}
	}
}
