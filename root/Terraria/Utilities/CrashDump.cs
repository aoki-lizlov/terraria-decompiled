using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using ReLogic.OS;

namespace Terraria.Utilities
{
	// Token: 0x020000D7 RID: 215
	public static class CrashDump
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x004E2745 File Offset: 0x004E0945
		public static bool WriteException(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.Present, outputDirectory);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x004E274F File Offset: 0x004E094F
		public static bool Write(CrashDump.Options options, string outputDirectory = ".")
		{
			return CrashDump.Write(options, CrashDump.ExceptionInfo.None, outputDirectory);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x004E275C File Offset: 0x004E095C
		private static string CreateDumpName()
		{
			DateTime dateTime = DateTime.Now.ToLocalTime();
			return string.Format("{0}_{1}_{2}_{3}.dmp", new object[]
			{
				Main.dedServ ? "TerrariaServer" : "Terraria",
				Main.versionNumber,
				dateTime.ToString("MM-dd-yy_HH-mm-ss-ffff", CultureInfo.InvariantCulture),
				Thread.CurrentThread.ManagedThreadId
			});
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x004E27CC File Offset: 0x004E09CC
		private static bool Write(CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo, string outputDirectory)
		{
			if (!Platform.IsWindows)
			{
				return false;
			}
			string text = Path.Combine(outputDirectory, CrashDump.CreateDumpName());
			if (!Utils.TryCreatingDirectory(outputDirectory))
			{
				return false;
			}
			bool flag;
			using (FileStream fileStream = File.Create(text))
			{
				flag = CrashDump.Write(fileStream.SafeFileHandle, options, exceptionInfo);
			}
			return flag;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x004E282C File Offset: 0x004E0A2C
		private static bool Write(SafeHandle fileHandle, CrashDump.Options options, CrashDump.ExceptionInfo exceptionInfo)
		{
			if (!Platform.IsWindows)
			{
				return false;
			}
			Process currentProcess = Process.GetCurrentProcess();
			IntPtr handle = currentProcess.Handle;
			uint id = (uint)currentProcess.Id;
			CrashDump.MiniDumpExceptionInformation miniDumpExceptionInformation;
			miniDumpExceptionInformation.ThreadId = CrashDump.GetCurrentThreadId();
			miniDumpExceptionInformation.ClientPointers = false;
			miniDumpExceptionInformation.ExceptionPointers = IntPtr.Zero;
			if (exceptionInfo == CrashDump.ExceptionInfo.Present)
			{
				miniDumpExceptionInformation.ExceptionPointers = Marshal.GetExceptionPointers();
			}
			bool flag;
			if (miniDumpExceptionInformation.ExceptionPointers == IntPtr.Zero)
			{
				flag = CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			}
			else
			{
				flag = CrashDump.MiniDumpWriteDump(handle, id, fileHandle, (uint)options, ref miniDumpExceptionInformation, IntPtr.Zero, IntPtr.Zero);
			}
			return flag;
		}

		// Token: 0x0600186F RID: 6255
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, ref CrashDump.MiniDumpExceptionInformation expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x06001870 RID: 6256
		[DllImport("dbghelp.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, uint processId, SafeHandle hFile, uint dumpType, IntPtr expParam, IntPtr userStreamParam, IntPtr callbackParam);

		// Token: 0x06001871 RID: 6257
		[DllImport("kernel32.dll", ExactSpelling = true)]
		private static extern uint GetCurrentThreadId();

		// Token: 0x020006FA RID: 1786
		[Flags]
		public enum Options : uint
		{
			// Token: 0x04006832 RID: 26674
			Normal = 0U,
			// Token: 0x04006833 RID: 26675
			WithDataSegs = 1U,
			// Token: 0x04006834 RID: 26676
			WithFullMemory = 2U,
			// Token: 0x04006835 RID: 26677
			WithHandleData = 4U,
			// Token: 0x04006836 RID: 26678
			FilterMemory = 8U,
			// Token: 0x04006837 RID: 26679
			ScanMemory = 16U,
			// Token: 0x04006838 RID: 26680
			WithUnloadedModules = 32U,
			// Token: 0x04006839 RID: 26681
			WithIndirectlyReferencedMemory = 64U,
			// Token: 0x0400683A RID: 26682
			FilterModulePaths = 128U,
			// Token: 0x0400683B RID: 26683
			WithProcessThreadData = 256U,
			// Token: 0x0400683C RID: 26684
			WithPrivateReadWriteMemory = 512U,
			// Token: 0x0400683D RID: 26685
			WithoutOptionalData = 1024U,
			// Token: 0x0400683E RID: 26686
			WithFullMemoryInfo = 2048U,
			// Token: 0x0400683F RID: 26687
			WithThreadInfo = 4096U,
			// Token: 0x04006840 RID: 26688
			WithCodeSegs = 8192U,
			// Token: 0x04006841 RID: 26689
			WithoutAuxiliaryState = 16384U,
			// Token: 0x04006842 RID: 26690
			WithFullAuxiliaryState = 32768U,
			// Token: 0x04006843 RID: 26691
			WithPrivateWriteCopyMemory = 65536U,
			// Token: 0x04006844 RID: 26692
			IgnoreInaccessibleMemory = 131072U,
			// Token: 0x04006845 RID: 26693
			ValidTypeFlags = 262143U
		}

		// Token: 0x020006FB RID: 1787
		private enum ExceptionInfo
		{
			// Token: 0x04006847 RID: 26695
			None,
			// Token: 0x04006848 RID: 26696
			Present
		}

		// Token: 0x020006FC RID: 1788
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		private struct MiniDumpExceptionInformation
		{
			// Token: 0x04006849 RID: 26697
			public uint ThreadId;

			// Token: 0x0400684A RID: 26698
			public IntPtr ExceptionPointers;

			// Token: 0x0400684B RID: 26699
			[MarshalAs(UnmanagedType.Bool)]
			public bool ClientPointers;
		}
	}
}
