using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using ReLogic.OS.Base;

namespace ReLogic.OS.OSX
{
	// Token: 0x02000070 RID: 112
	internal class PathService : PathService
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000A59C File Offset: 0x0000879C
		public override string GetStoragePath()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("HOME");
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return ".";
			}
			return environmentVariable + "/Library/Application Support";
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A5CF File Offset: 0x000087CF
		public override void OpenURL(string url)
		{
			Process.Start("open", "\"" + url + "\"");
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A5EC File Offset: 0x000087EC
		private static IntPtr MarshalNSString(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str + "\0");
			IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length);
			IntPtr intPtr4;
			try
			{
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				IntPtr intPtr2 = PathService.NativeMethods.objc_getClass("NSString");
				IntPtr intPtr3 = PathService.NativeMethods.sel_registerName("stringWithUTF8String:");
				intPtr4 = PathService.NativeMethods.objc_msgSend(intPtr2, intPtr3, intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return intPtr4;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A65C File Offset: 0x0000885C
		public override void MoveToRecycleBin(string path)
		{
			IntPtr intPtr = PathService.NativeMethods.objc_getClass("NSFileManager");
			IntPtr intPtr2 = PathService.NativeMethods.sel_registerName("defaultManager");
			IntPtr intPtr3 = PathService.NativeMethods.objc_msgSend(intPtr, intPtr2);
			IntPtr intPtr4 = PathService.NativeMethods.objc_getClass("NSURL");
			IntPtr intPtr5 = PathService.NativeMethods.sel_registerName("fileURLWithPath:");
			IntPtr intPtr6 = PathService.MarshalNSString(path);
			IntPtr intPtr7 = PathService.NativeMethods.objc_msgSend(intPtr4, intPtr5, intPtr6);
			IntPtr intPtr8 = PathService.NativeMethods.sel_registerName("trashItemAtURL:resultingItemURL:error:");
			PathService.NativeMethods.objc_msgSend(intPtr3, intPtr8, intPtr7, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009B8C File Offset: 0x00007D8C
		public PathService()
		{
		}

		// Token: 0x020000DF RID: 223
		private class NativeMethods
		{
			// Token: 0x0600046A RID: 1130
			[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation", BestFitMapping = false, CharSet = 2, ThrowOnUnmappableChar = true)]
			public static extern IntPtr objc_getClass([MarshalAs(20)] string name);

			// Token: 0x0600046B RID: 1131
			[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation", BestFitMapping = false, CharSet = 2, ThrowOnUnmappableChar = true)]
			public static extern IntPtr sel_registerName([MarshalAs(20)] string name);

			// Token: 0x0600046C RID: 1132
			[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
			public static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector);

			// Token: 0x0600046D RID: 1133
			[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
			public static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1);

			// Token: 0x0600046E RID: 1134
			[DllImport("/System/Library/Frameworks/Foundation.framework/Foundation")]
			public static extern IntPtr objc_msgSend(IntPtr receiver, IntPtr selector, IntPtr arg1, IntPtr arg2, IntPtr arg3);

			// Token: 0x0600046F RID: 1135 RVA: 0x0000448A File Offset: 0x0000268A
			public NativeMethods()
			{
			}

			// Token: 0x040005FE RID: 1534
			private const string Foundation = "/System/Library/Frameworks/Foundation.framework/Foundation";
		}
	}
}
