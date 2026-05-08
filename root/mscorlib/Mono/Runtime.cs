using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000027 RID: 39
	internal static class Runtime
	{
		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void mono_runtime_install_handlers();

		// Token: 0x060000A2 RID: 162 RVA: 0x00003FB0 File Offset: 0x000021B0
		internal static void InstallSignalHandlers()
		{
			Runtime.mono_runtime_install_handlers();
		}

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDisplayName();

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNativeStackTrace(Exception exception);

		// Token: 0x060000A5 RID: 165 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static bool SetGCAllowSynchronousMajor(bool flag)
		{
			return true;
		}

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ExceptionToState_internal(Exception exc, out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000A7 RID: 167 RVA: 0x00003FBC File Offset: 0x000021BC
		private static Tuple<string, ulong, ulong> ExceptionToState(Exception exc)
		{
			ulong num;
			ulong num2;
			return new Tuple<string, ulong, ulong>(Runtime.ExceptionToState_internal(exc, out num, out num2), num, num2);
		}

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableMicrosoftTelemetry();

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FDA File Offset: 0x000021DA
		private static void WriteStateToFile(Exception exc)
		{
			throw new PlatformNotSupportedException("Merp is no longer supported.");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FDA File Offset: 0x000021DA
		private static void SendMicrosoftTelemetry(string payload_str, ulong portable_hash, ulong unportable_hash)
		{
			throw new PlatformNotSupportedException("Merp is no longer supported.");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003FDA File Offset: 0x000021DA
		private static void SendExceptionToTelemetry(Exception exc)
		{
			throw new PlatformNotSupportedException("Merp is no longer supported.");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FDA File Offset: 0x000021DA
		private static void EnableMicrosoftTelemetry(string appBundleID_str, string appSignature_str, string appVersion_str, string merpGUIPath_str, string unused, string appPath_str, string configDir_str)
		{
			throw new PlatformNotSupportedException("Merp is no longer supported.");
		}

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string DumpStateSingle_internal(out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string DumpStateTotal_internal(out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000AF RID: 175 RVA: 0x00003FE8 File Offset: 0x000021E8
		private static Tuple<string, ulong, ulong> DumpStateSingle()
		{
			object obj = Runtime.dump;
			ulong num;
			ulong num2;
			string text;
			lock (obj)
			{
				text = Runtime.DumpStateSingle_internal(out num, out num2);
			}
			return new Tuple<string, ulong, ulong>(text, num, num2);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004038 File Offset: 0x00002238
		private static Tuple<string, ulong, ulong> DumpStateTotal()
		{
			object obj = Runtime.dump;
			ulong num;
			ulong num2;
			string text;
			lock (obj)
			{
				text = Runtime.DumpStateTotal_internal(out num, out num2);
			}
			return new Tuple<string, ulong, ulong>(text, num, num2);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004088 File Offset: 0x00002288
		private static void RegisterReportingForAllNativeLibs()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004088 File Offset: 0x00002288
		private static void RegisterReportingForNativeLib(string modulePathSuffix_str, string moduleName_str)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004088 File Offset: 0x00002288
		private static void EnableCrashReportLog(string directory_str)
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000408A File Offset: 0x0000228A
		private static Runtime.CrashReportLogLevel CheckCrashReportLog(string directory_str, bool clear)
		{
			return Runtime.CrashReportLogLevel.MonoSummaryNone;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000408D File Offset: 0x0000228D
		private static long CheckCrashReportHash(string directory_str, bool clear)
		{
			return 0L;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004091 File Offset: 0x00002291
		private static string CheckCrashReportReason(string directory_str, bool clear)
		{
			return string.Empty;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003FDA File Offset: 0x000021DA
		private static void AnnotateMicrosoftTelemetry(string key, string val)
		{
			throw new PlatformNotSupportedException("Merp is no longer supported.");
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004098 File Offset: 0x00002298
		// Note: this type is marked as 'beforefieldinit'.
		static Runtime()
		{
		}

		// Token: 0x04000CD0 RID: 3280
		private static object dump = new object();

		// Token: 0x02000028 RID: 40
		private enum CrashReportLogLevel
		{
			// Token: 0x04000CD2 RID: 3282
			MonoSummaryNone,
			// Token: 0x04000CD3 RID: 3283
			MonoSummarySetup,
			// Token: 0x04000CD4 RID: 3284
			MonoSummarySuspendHandshake,
			// Token: 0x04000CD5 RID: 3285
			MonoSummaryUnmanagedStacks,
			// Token: 0x04000CD6 RID: 3286
			MonoSummaryManagedStacks,
			// Token: 0x04000CD7 RID: 3287
			MonoSummaryStateWriter,
			// Token: 0x04000CD8 RID: 3288
			MonoSummaryStateWriterDone,
			// Token: 0x04000CD9 RID: 3289
			MonoSummaryMerpWriter,
			// Token: 0x04000CDA RID: 3290
			MonoSummaryMerpInvoke,
			// Token: 0x04000CDB RID: 3291
			MonoSummaryCleanup,
			// Token: 0x04000CDC RID: 3292
			MonoSummaryDone,
			// Token: 0x04000CDD RID: 3293
			MonoSummaryDoubleFault
		}
	}
}
