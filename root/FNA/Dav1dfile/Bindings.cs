using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Dav1dfile
{
	// Token: 0x02000004 RID: 4
	public static class Bindings
	{
		// Token: 0x060000CA RID: 202
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint df_linked_version();

		// Token: 0x060000CB RID: 203
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int df_open_from_memory(IntPtr bytes, uint size, out IntPtr context);

		// Token: 0x060000CC RID: 204
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "df_fopen")]
		private static extern int INTERNAL_df_fopen([MarshalAs(UnmanagedType.LPStr)] string fname, out IntPtr context);

		// Token: 0x060000CD RID: 205
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "df_fopen")]
		private unsafe static extern int INTERNAL_df_fopen(byte* fname, out IntPtr context);

		// Token: 0x060000CE RID: 206
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern void df_close(IntPtr context);

		// Token: 0x060000CF RID: 207
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern void df_videoinfo(IntPtr context, out int width, out int height, out Bindings.PixelLayout pixelLayout);

		// Token: 0x060000D0 RID: 208
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern void df_videoinfo2(IntPtr context, out int width, out int height, out Bindings.PixelLayout pixelLayout, out byte hbd);

		// Token: 0x060000D1 RID: 209
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int df_guessframerate(IntPtr context, out double fps);

		// Token: 0x060000D2 RID: 210
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int df_eos(IntPtr context);

		// Token: 0x060000D3 RID: 211
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern void df_reset(IntPtr context);

		// Token: 0x060000D4 RID: 212
		[DllImport("dav1dfile", CallingConvention = CallingConvention.Cdecl)]
		public static extern int df_readvideo(IntPtr context, int numFrames, out IntPtr yDataPtr, out IntPtr uDataPtr, out IntPtr vDataPtr, out uint yDataLength, out uint uvDataLength, out uint yStride, out uint uvStride);

		// Token: 0x060000D5 RID: 213 RVA: 0x0000243C File Offset: 0x0000063C
		private unsafe static byte* Utf8Encode(string str)
		{
			int num = str.Length * 4 + 1;
			byte* ptr = (byte*)(void*)Marshal.AllocHGlobal(num);
			fixed (string text = str)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				Encoding.UTF8.GetBytes(ptr2, str.Length + 1, ptr, num);
			}
			return ptr;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000248C File Offset: 0x0000068C
		public unsafe static int df_fopen(string fname, out IntPtr context)
		{
			int num;
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				num = Bindings.INTERNAL_df_fopen(fname, out context);
			}
			else
			{
				byte* ptr = Bindings.Utf8Encode(fname);
				num = Bindings.INTERNAL_df_fopen(ptr, out context);
				Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			}
			return num;
		}

		// Token: 0x040000BE RID: 190
		private const string nativeLibName = "dav1dfile";

		// Token: 0x040000BF RID: 191
		public const uint DAV1DFILE_MAJOR_VERSION = 1U;

		// Token: 0x040000C0 RID: 192
		public const uint DAV1DFILE_MINOR_VERSION = 0U;

		// Token: 0x040000C1 RID: 193
		public const uint DAV1DFILE_PATCH_VERSION = 0U;

		// Token: 0x040000C2 RID: 194
		public const uint DAV1DFILE_COMPILED_VERSION = 10000U;

		// Token: 0x020001CD RID: 461
		public enum PixelLayout
		{
			// Token: 0x04000CED RID: 3309
			I400,
			// Token: 0x04000CEE RID: 3310
			I420,
			// Token: 0x04000CEF RID: 3311
			I422,
			// Token: 0x04000CF0 RID: 3312
			I444
		}
	}
}
