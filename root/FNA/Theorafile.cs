using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000003 RID: 3
public static class Theorafile
{
	// Token: 0x060000B7 RID: 183 RVA: 0x00002314 File Offset: 0x00000514
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

	// Token: 0x060000B8 RID: 184
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tf_open_callbacks")]
	private static extern int INTERNAL_tf_open_callbacks(IntPtr datasource, IntPtr file, Theorafile.tf_callbacks io);

	// Token: 0x060000B9 RID: 185 RVA: 0x00002361 File Offset: 0x00000561
	public static int tf_open_callbacks(IntPtr datasource, out IntPtr file, Theorafile.tf_callbacks io)
	{
		file = Theorafile.AllocTheoraFile();
		return Theorafile.INTERNAL_tf_open_callbacks(datasource, file, io);
	}

	// Token: 0x060000BA RID: 186
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tf_fopen")]
	private unsafe static extern int INTERNAL_tf_fopen(byte* fname, IntPtr file);

	// Token: 0x060000BB RID: 187
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tf_fopen")]
	private static extern int INTERNAL_tf_fopen([MarshalAs(UnmanagedType.LPStr)] string fname, IntPtr file);

	// Token: 0x060000BC RID: 188 RVA: 0x00002374 File Offset: 0x00000574
	public unsafe static int tf_fopen(string fname, out IntPtr file)
	{
		file = Theorafile.AllocTheoraFile();
		int num;
		if (Environment.OSVersion.Platform == PlatformID.Win32NT)
		{
			num = Theorafile.INTERNAL_tf_fopen(fname, file);
		}
		else
		{
			byte* ptr = Theorafile.Utf8Encode(fname);
			num = Theorafile.INTERNAL_tf_fopen(ptr, file);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
		}
		return num;
	}

	// Token: 0x060000BD RID: 189
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl, EntryPoint = "tf_close")]
	private static extern int INTERNAL_tf_close(IntPtr file);

	// Token: 0x060000BE RID: 190 RVA: 0x000023BA File Offset: 0x000005BA
	public static int tf_close(ref IntPtr file)
	{
		int num = Theorafile.INTERNAL_tf_close(file);
		Marshal.FreeHGlobal(file);
		file = IntPtr.Zero;
		return num;
	}

	// Token: 0x060000BF RID: 191
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_hasaudio(IntPtr file);

	// Token: 0x060000C0 RID: 192
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_hasvideo(IntPtr file);

	// Token: 0x060000C1 RID: 193
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern void tf_videoinfo(IntPtr file, out int width, out int height, out double fps, out Theorafile.th_pixel_fmt fmt);

	// Token: 0x060000C2 RID: 194
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern void tf_audioinfo(IntPtr file, out int channels, out int samplerate);

	// Token: 0x060000C3 RID: 195
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_setaudiotrack(IntPtr file, int track);

	// Token: 0x060000C4 RID: 196
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_setvideotrack(IntPtr file, int track);

	// Token: 0x060000C5 RID: 197
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_eos(IntPtr file);

	// Token: 0x060000C6 RID: 198
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern void tf_reset(IntPtr file);

	// Token: 0x060000C7 RID: 199
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_readvideo(IntPtr file, IntPtr buffer, int numframes);

	// Token: 0x060000C8 RID: 200
	[DllImport("libtheorafile", CallingConvention = CallingConvention.Cdecl)]
	public static extern int tf_readaudio(IntPtr file, IntPtr buffer, int length);

	// Token: 0x060000C9 RID: 201 RVA: 0x000023D4 File Offset: 0x000005D4
	private static IntPtr AllocTheoraFile()
	{
		PlatformID platform = Environment.OSVersion.Platform;
		if (IntPtr.Size == 4)
		{
			return Marshal.AllocHGlobal(1160);
		}
		if (IntPtr.Size != 8)
		{
			throw new NotSupportedException("Unhandled architecture!");
		}
		if (platform == PlatformID.Unix)
		{
			return Marshal.AllocHGlobal(1472);
		}
		if (platform == PlatformID.Win32NT)
		{
			return Marshal.AllocHGlobal(1328);
		}
		throw new NotSupportedException("Unhandled platform!");
	}

	// Token: 0x040000BD RID: 189
	private const string nativeLibName = "libtheorafile";

	// Token: 0x020001C7 RID: 455
	public enum SeekWhence
	{
		// Token: 0x04000CE0 RID: 3296
		TF_SEEK_SET,
		// Token: 0x04000CE1 RID: 3297
		TF_SEEK_CUR,
		// Token: 0x04000CE2 RID: 3298
		TF_SEEK_END
	}

	// Token: 0x020001C8 RID: 456
	// (Invoke) Token: 0x06001916 RID: 6422
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr read_func(IntPtr ptr, IntPtr size, IntPtr nmemb, IntPtr datasource);

	// Token: 0x020001C9 RID: 457
	// (Invoke) Token: 0x0600191A RID: 6426
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int seek_func(IntPtr datasource, long offset, Theorafile.SeekWhence whence);

	// Token: 0x020001CA RID: 458
	// (Invoke) Token: 0x0600191E RID: 6430
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int close_func(IntPtr datasource);

	// Token: 0x020001CB RID: 459
	public enum th_pixel_fmt
	{
		// Token: 0x04000CE4 RID: 3300
		TH_PF_420,
		// Token: 0x04000CE5 RID: 3301
		TH_PF_RSVD,
		// Token: 0x04000CE6 RID: 3302
		TH_PF_422,
		// Token: 0x04000CE7 RID: 3303
		TH_PF_444,
		// Token: 0x04000CE8 RID: 3304
		TH_PF_NFORMATS
	}

	// Token: 0x020001CC RID: 460
	public struct tf_callbacks
	{
		// Token: 0x04000CE9 RID: 3305
		public Theorafile.read_func read_func;

		// Token: 0x04000CEA RID: 3306
		public Theorafile.seek_func seek_func;

		// Token: 0x04000CEB RID: 3307
		public Theorafile.close_func close_func;
	}
}
