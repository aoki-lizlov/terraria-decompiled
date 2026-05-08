using System;

namespace rail
{
	// Token: 0x0200018D RID: 397
	public interface IRailUtils
	{
		// Token: 0x060018AC RID: 6316
		uint GetTimeCountSinceGameLaunch();

		// Token: 0x060018AD RID: 6317
		uint GetTimeCountSinceComputerLaunch();

		// Token: 0x060018AE RID: 6318
		uint GetTimeFromServer();

		// Token: 0x060018AF RID: 6319
		RailResult AsyncGetImageData(string image_path, uint scale_to_width, uint scale_to_height, string user_data);

		// Token: 0x060018B0 RID: 6320
		void GetErrorString(RailResult result, out string error_string);

		// Token: 0x060018B1 RID: 6321
		RailResult DirtyWordsFilter(string words, bool replace_sensitive, RailDirtyWordsCheckResult check_result);

		// Token: 0x060018B2 RID: 6322
		EnumRailPlatformType GetRailPlatformType();

		// Token: 0x060018B3 RID: 6323
		RailResult GetLaunchAppParameters(EnumRailLaunchAppType app_type, out string parameter);

		// Token: 0x060018B4 RID: 6324
		RailResult GetPlatformLanguageCode(out string language_code);

		// Token: 0x060018B5 RID: 6325
		RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback);

		// Token: 0x060018B6 RID: 6326
		RailResult GetCountryCodeOfCurrentLoggedInIP(out string country_code);
	}
}
