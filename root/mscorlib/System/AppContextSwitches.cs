using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001B8 RID: 440
	internal static class AppContextSwitches
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0005245E File Offset: 0x0005065E
		public static bool NoAsyncCurrentCulture
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Globalization.NoAsyncCurrentCulture", ref AppContextSwitches._noAsyncCurrentCulture);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0005246F File Offset: 0x0005066F
		public static bool EnforceJapaneseEraYearRanges
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceJapaneseEraYearRanges, ref AppContextSwitches._enforceJapaneseEraYearRanges);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00052480 File Offset: 0x00050680
		public static bool FormatJapaneseFirstYearAsANumber
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchFormatJapaneseFirstYearAsANumber, ref AppContextSwitches._formatJapaneseFirstYearAsANumber);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00052491 File Offset: 0x00050691
		public static bool EnforceLegacyJapaneseDateParsing
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue(AppContextDefaultValues.SwitchEnforceLegacyJapaneseDateParsing, ref AppContextSwitches._enforceLegacyJapaneseDateParsing);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000524A2 File Offset: 0x000506A2
		public static bool ThrowExceptionIfDisposedCancellationTokenSource
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource", ref AppContextSwitches._throwExceptionIfDisposedCancellationTokenSource);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000524B3 File Offset: 0x000506B3
		public static bool PreserveEventListnerObjectIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity", ref AppContextSwitches._preserveEventListnerObjectIdentity);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x000524C4 File Offset: 0x000506C4
		public static bool UseLegacyPathHandling
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.UseLegacyPathHandling", ref AppContextSwitches._useLegacyPathHandling);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000524D5 File Offset: 0x000506D5
		public static bool BlockLongPaths
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.IO.BlockLongPaths", ref AppContextSwitches._blockLongPaths);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x000524E6 File Offset: 0x000506E6
		public static bool SetActorAsReferenceWhenCopyingClaimsIdentity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.ClaimsIdentity.SetActorAsReferenceWhenCopyingClaimsIdentity", ref AppContextSwitches._cloneActor);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x000524F7 File Offset: 0x000506F7
		public static bool DoNotAddrOfCspParentWindowHandle
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return AppContextSwitches.GetCachedSwitchValue("Switch.System.Security.Cryptography.DoNotAddrOfCspParentWindowHandle", ref AppContextSwitches._doNotAddrOfCspParentWindowHandle);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00052508 File Offset: 0x00050708
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0005250F File Offset: 0x0005070F
		private static bool DisableCaching
		{
			[CompilerGenerated]
			get
			{
				return AppContextSwitches.<DisableCaching>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				AppContextSwitches.<DisableCaching>k__BackingField = value;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00052518 File Offset: 0x00050718
		static AppContextSwitches()
		{
			bool flag;
			if (AppContext.TryGetSwitch("TestSwitch.LocalAppContext.DisableCaching", out flag))
			{
				AppContextSwitches.DisableCaching = flag;
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00052539 File Offset: 0x00050739
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
		{
			return switchValue >= 0 && (switchValue > 0 || AppContextSwitches.GetCachedSwitchValueInternal(switchName, ref switchValue));
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00052550 File Offset: 0x00050750
		private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
		{
			bool flag;
			AppContext.TryGetSwitch(switchName, out flag);
			if (AppContextSwitches.DisableCaching)
			{
				return flag;
			}
			switchValue = (flag ? 1 : (-1));
			return flag;
		}

		// Token: 0x0400135F RID: 4959
		private static int _noAsyncCurrentCulture;

		// Token: 0x04001360 RID: 4960
		private static int _enforceJapaneseEraYearRanges;

		// Token: 0x04001361 RID: 4961
		private static int _formatJapaneseFirstYearAsANumber;

		// Token: 0x04001362 RID: 4962
		private static int _enforceLegacyJapaneseDateParsing;

		// Token: 0x04001363 RID: 4963
		private static int _throwExceptionIfDisposedCancellationTokenSource;

		// Token: 0x04001364 RID: 4964
		private static int _preserveEventListnerObjectIdentity;

		// Token: 0x04001365 RID: 4965
		private static int _useLegacyPathHandling;

		// Token: 0x04001366 RID: 4966
		private static int _blockLongPaths;

		// Token: 0x04001367 RID: 4967
		private static int _cloneActor;

		// Token: 0x04001368 RID: 4968
		private static int _doNotAddrOfCspParentWindowHandle;

		// Token: 0x04001369 RID: 4969
		[CompilerGenerated]
		private static bool <DisableCaching>k__BackingField;
	}
}
