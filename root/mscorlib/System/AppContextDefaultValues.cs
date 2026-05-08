using System;

namespace System
{
	// Token: 0x020001E9 RID: 489
	internal static class AppContextDefaultValues
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x00004088 File Offset: 0x00002288
		public static void PopulateDefaultValues()
		{
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0005BC88 File Offset: 0x00059E88
		public static bool TryGetSwitchOverride(string switchName, out bool overrideValue)
		{
			overrideValue = false;
			return false;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0005BC8E File Offset: 0x00059E8E
		// Note: this type is marked as 'beforefieldinit'.
		static AppContextDefaultValues()
		{
		}

		// Token: 0x04001532 RID: 5426
		internal const string SwitchNoAsyncCurrentCulture = "Switch.System.Globalization.NoAsyncCurrentCulture";

		// Token: 0x04001533 RID: 5427
		internal static readonly string SwitchEnforceJapaneseEraYearRanges = "Switch.System.Globalization.EnforceJapaneseEraYearRanges";

		// Token: 0x04001534 RID: 5428
		internal static readonly string SwitchFormatJapaneseFirstYearAsANumber = "Switch.System.Globalization.FormatJapaneseFirstYearAsANumber";

		// Token: 0x04001535 RID: 5429
		internal static readonly string SwitchEnforceLegacyJapaneseDateParsing = "Switch.System.Globalization.EnforceLegacyJapaneseDateParsing";

		// Token: 0x04001536 RID: 5430
		internal const string SwitchThrowExceptionIfDisposedCancellationTokenSource = "Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource";

		// Token: 0x04001537 RID: 5431
		internal const string SwitchPreserveEventListnerObjectIdentity = "Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity";

		// Token: 0x04001538 RID: 5432
		internal const string SwitchUseLegacyPathHandling = "Switch.System.IO.UseLegacyPathHandling";

		// Token: 0x04001539 RID: 5433
		internal const string SwitchBlockLongPaths = "Switch.System.IO.BlockLongPaths";

		// Token: 0x0400153A RID: 5434
		internal const string SwitchDoNotAddrOfCspParentWindowHandle = "Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle";

		// Token: 0x0400153B RID: 5435
		internal const string SwitchSetActorAsReferenceWhenCopyingClaimsIdentity = "Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity";
	}
}
