using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL2
{
	// Token: 0x02000006 RID: 6
	public static class SDL
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00003C41 File Offset: 0x00001E41
		internal static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00003C58 File Offset: 0x00001E58
		internal static Delegate GetDelegateForFunctionPointer<T>(IntPtr ptr)
		{
			return Marshal.GetDelegateForFunctionPointer(ptr, typeof(T));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00003C6A File Offset: 0x00001E6A
		internal static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00003C7B File Offset: 0x00001E7B
		internal static int Utf8Size(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length * 4 + 1;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00003C8C File Offset: 0x00001E8C
		internal unsafe static byte* Utf8Encode(string str, byte* buffer, int bufferSize)
		{
			if (str == null)
			{
				return null;
			}
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Encoding.UTF8.GetBytes(ptr, str.Length + 1, buffer, bufferSize);
			}
			return buffer;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00003CC8 File Offset: 0x00001EC8
		internal unsafe static byte* Utf8EncodeHeap(string str)
		{
			if (str == null)
			{
				return null;
			}
			int num = SDL.Utf8Size(str);
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

		// Token: 0x060005B7 RID: 1463 RVA: 0x00003D18 File Offset: 0x00001F18
		public unsafe static string UTF8_ToManaged(IntPtr s, bool freePtr = false)
		{
			if (s == IntPtr.Zero)
			{
				return null;
			}
			byte* ptr = (byte*)(void*)s;
			while (*ptr != 0)
			{
				ptr++;
			}
			int num = (int)((long)((byte*)ptr - (byte*)(void*)s));
			if (num == 0)
			{
				return string.Empty;
			}
			checked
			{
				char* ptr2 = stackalloc char[unchecked((UIntPtr)num) * 2];
				int chars = Encoding.UTF8.GetChars((byte*)(void*)s, num, ptr2, num);
				string text = new string(ptr2, 0, chars);
				if (freePtr)
				{
					SDL.SDL_free(s);
				}
				return text;
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00003D87 File Offset: 0x00001F87
		public static uint SDL_FOURCC(byte A, byte B, byte C, byte D)
		{
			return (uint)((int)A | ((int)B << 8) | ((int)C << 16) | ((int)D << 24));
		}

		// Token: 0x060005B9 RID: 1465
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SDL_malloc(IntPtr size);

		// Token: 0x060005BA RID: 1466
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SDL_free(IntPtr memblock);

		// Token: 0x060005BB RID: 1467
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_memcpy(IntPtr dst, IntPtr src, IntPtr len);

		// Token: 0x060005BC RID: 1468
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RWFromFile")]
		private unsafe static extern IntPtr INTERNAL_SDL_RWFromFile(byte* file, byte* mode);

		// Token: 0x060005BD RID: 1469 RVA: 0x00003D98 File Offset: 0x00001F98
		public unsafe static IntPtr SDL_RWFromFile(string file, string mode)
		{
			byte* ptr = SDL.Utf8EncodeHeap(file);
			byte* ptr2 = SDL.Utf8EncodeHeap(mode);
			IntPtr intPtr = SDL.INTERNAL_SDL_RWFromFile(ptr, ptr2);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr2));
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060005BE RID: 1470
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AllocRW();

		// Token: 0x060005BF RID: 1471
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeRW(IntPtr area);

		// Token: 0x060005C0 RID: 1472
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RWFromFP(IntPtr fp, SDL.SDL_bool autoclose);

		// Token: 0x060005C1 RID: 1473
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RWFromMem(IntPtr mem, int size);

		// Token: 0x060005C2 RID: 1474
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RWFromConstMem(IntPtr mem, int size);

		// Token: 0x060005C3 RID: 1475
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWsize(IntPtr context);

		// Token: 0x060005C4 RID: 1476
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWseek(IntPtr context, long offset, int whence);

		// Token: 0x060005C5 RID: 1477
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWtell(IntPtr context);

		// Token: 0x060005C6 RID: 1478
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWread(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

		// Token: 0x060005C7 RID: 1479
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWwrite(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

		// Token: 0x060005C8 RID: 1480
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_ReadU8(IntPtr src);

		// Token: 0x060005C9 RID: 1481
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_ReadLE16(IntPtr src);

		// Token: 0x060005CA RID: 1482
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_ReadBE16(IntPtr src);

		// Token: 0x060005CB RID: 1483
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_ReadLE32(IntPtr src);

		// Token: 0x060005CC RID: 1484
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_ReadBE32(IntPtr src);

		// Token: 0x060005CD RID: 1485
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_ReadLE64(IntPtr src);

		// Token: 0x060005CE RID: 1486
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_ReadBE64(IntPtr src);

		// Token: 0x060005CF RID: 1487
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteU8(IntPtr dst, byte value);

		// Token: 0x060005D0 RID: 1488
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteLE16(IntPtr dst, ushort value);

		// Token: 0x060005D1 RID: 1489
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteBE16(IntPtr dst, ushort value);

		// Token: 0x060005D2 RID: 1490
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteLE32(IntPtr dst, uint value);

		// Token: 0x060005D3 RID: 1491
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteBE32(IntPtr dst, uint value);

		// Token: 0x060005D4 RID: 1492
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteLE64(IntPtr dst, ulong value);

		// Token: 0x060005D5 RID: 1493
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WriteBE64(IntPtr dst, ulong value);

		// Token: 0x060005D6 RID: 1494
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_RWclose(IntPtr context);

		// Token: 0x060005D7 RID: 1495
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadFile")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadFile(byte* file, out IntPtr datasize);

		// Token: 0x060005D8 RID: 1496 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public unsafe static IntPtr SDL_LoadFile(string file, out IntPtr datasize)
		{
			byte* ptr = SDL.Utf8EncodeHeap(file);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadFile(ptr, out datasize);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060005D9 RID: 1497
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetMainReady();

		// Token: 0x060005DA RID: 1498
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WinRTRunApp(SDL.SDL_main_func mainFunction, IntPtr reserved);

		// Token: 0x060005DB RID: 1499
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GDKRunApp(SDL.SDL_main_func mainFunction, IntPtr reserved);

		// Token: 0x060005DC RID: 1500
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UIKitRunApp(int argc, IntPtr argv, SDL.SDL_main_func mainFunction);

		// Token: 0x060005DD RID: 1501
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_Init(uint flags);

		// Token: 0x060005DE RID: 1502
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_InitSubSystem(uint flags);

		// Token: 0x060005DF RID: 1503
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Quit();

		// Token: 0x060005E0 RID: 1504
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_QuitSubSystem(uint flags);

		// Token: 0x060005E1 RID: 1505
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_WasInit(uint flags);

		// Token: 0x060005E2 RID: 1506
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPlatform")]
		private static extern IntPtr INTERNAL_SDL_GetPlatform();

		// Token: 0x060005E3 RID: 1507 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public static string SDL_GetPlatform()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetPlatform(), false);
		}

		// Token: 0x060005E4 RID: 1508
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearHints();

		// Token: 0x060005E5 RID: 1509
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHint")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetHint(byte* name);

		// Token: 0x060005E6 RID: 1510 RVA: 0x00003E04 File Offset: 0x00002004
		public unsafe static string SDL_GetHint(string name)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetHint(SDL.Utf8Encode(name, ptr, num)), false);
		}

		// Token: 0x060005E7 RID: 1511
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHint")]
		private unsafe static extern SDL.SDL_bool INTERNAL_SDL_SetHint(byte* name, byte* value);

		// Token: 0x060005E8 RID: 1512 RVA: 0x00003E30 File Offset: 0x00002030
		public unsafe static SDL.SDL_bool SDL_SetHint(string name, string value)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = SDL.Utf8Size(value);
			byte* ptr2 = stackalloc byte[(UIntPtr)num2];
			return SDL.INTERNAL_SDL_SetHint(SDL.Utf8Encode(name, ptr, num), SDL.Utf8Encode(value, ptr2, num2));
		}

		// Token: 0x060005E9 RID: 1513
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHintWithPriority")]
		private unsafe static extern SDL.SDL_bool INTERNAL_SDL_SetHintWithPriority(byte* name, byte* value, SDL.SDL_HintPriority priority);

		// Token: 0x060005EA RID: 1514 RVA: 0x00003E6C File Offset: 0x0000206C
		public unsafe static SDL.SDL_bool SDL_SetHintWithPriority(string name, string value, SDL.SDL_HintPriority priority)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = SDL.Utf8Size(value);
			byte* ptr2 = stackalloc byte[(UIntPtr)num2];
			return SDL.INTERNAL_SDL_SetHintWithPriority(SDL.Utf8Encode(name, ptr, num), SDL.Utf8Encode(value, ptr2, num2), priority);
		}

		// Token: 0x060005EB RID: 1515
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHintBoolean")]
		private unsafe static extern SDL.SDL_bool INTERNAL_SDL_GetHintBoolean(byte* name, SDL.SDL_bool default_value);

		// Token: 0x060005EC RID: 1516 RVA: 0x00003EA8 File Offset: 0x000020A8
		public unsafe static SDL.SDL_bool SDL_GetHintBoolean(string name, SDL.SDL_bool default_value)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GetHintBoolean(SDL.Utf8Encode(name, ptr, num), default_value);
		}

		// Token: 0x060005ED RID: 1517
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearError();

		// Token: 0x060005EE RID: 1518
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetError")]
		private static extern IntPtr INTERNAL_SDL_GetError();

		// Token: 0x060005EF RID: 1519 RVA: 0x00003ECF File Offset: 0x000020CF
		public static string SDL_GetError()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetError(), false);
		}

		// Token: 0x060005F0 RID: 1520
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetError")]
		private unsafe static extern void INTERNAL_SDL_SetError(byte* fmtAndArglist);

		// Token: 0x060005F1 RID: 1521 RVA: 0x00003EDC File Offset: 0x000020DC
		public unsafe static void SDL_SetError(string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_SetError(SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005F2 RID: 1522
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetErrorMsg(IntPtr errstr, int maxlength);

		// Token: 0x060005F3 RID: 1523
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Log")]
		private unsafe static extern void INTERNAL_SDL_Log(byte* fmtAndArglist);

		// Token: 0x060005F4 RID: 1524 RVA: 0x00003F04 File Offset: 0x00002104
		public unsafe static void SDL_Log(string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_Log(SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005F5 RID: 1525
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogVerbose")]
		private unsafe static extern void INTERNAL_SDL_LogVerbose(int category, byte* fmtAndArglist);

		// Token: 0x060005F6 RID: 1526 RVA: 0x00003F2C File Offset: 0x0000212C
		public unsafe static void SDL_LogVerbose(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogVerbose(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005F7 RID: 1527
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogDebug")]
		private unsafe static extern void INTERNAL_SDL_LogDebug(int category, byte* fmtAndArglist);

		// Token: 0x060005F8 RID: 1528 RVA: 0x00003F54 File Offset: 0x00002154
		public unsafe static void SDL_LogDebug(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogDebug(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005F9 RID: 1529
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogInfo")]
		private unsafe static extern void INTERNAL_SDL_LogInfo(int category, byte* fmtAndArglist);

		// Token: 0x060005FA RID: 1530 RVA: 0x00003F7C File Offset: 0x0000217C
		public unsafe static void SDL_LogInfo(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogInfo(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005FB RID: 1531
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogWarn")]
		private unsafe static extern void INTERNAL_SDL_LogWarn(int category, byte* fmtAndArglist);

		// Token: 0x060005FC RID: 1532 RVA: 0x00003FA4 File Offset: 0x000021A4
		public unsafe static void SDL_LogWarn(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogWarn(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005FD RID: 1533
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogError")]
		private unsafe static extern void INTERNAL_SDL_LogError(int category, byte* fmtAndArglist);

		// Token: 0x060005FE RID: 1534 RVA: 0x00003FCC File Offset: 0x000021CC
		public unsafe static void SDL_LogError(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogError(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x060005FF RID: 1535
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogCritical")]
		private unsafe static extern void INTERNAL_SDL_LogCritical(int category, byte* fmtAndArglist);

		// Token: 0x06000600 RID: 1536 RVA: 0x00003FF4 File Offset: 0x000021F4
		public unsafe static void SDL_LogCritical(int category, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogCritical(category, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x06000601 RID: 1537
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogMessage")]
		private unsafe static extern void INTERNAL_SDL_LogMessage(int category, SDL.SDL_LogPriority priority, byte* fmtAndArglist);

		// Token: 0x06000602 RID: 1538 RVA: 0x0000401C File Offset: 0x0000221C
		public unsafe static void SDL_LogMessage(int category, SDL.SDL_LogPriority priority, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogMessage(category, priority, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x06000603 RID: 1539
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogMessageV")]
		private unsafe static extern void INTERNAL_SDL_LogMessageV(int category, SDL.SDL_LogPriority priority, byte* fmtAndArglist);

		// Token: 0x06000604 RID: 1540 RVA: 0x00004044 File Offset: 0x00002244
		public unsafe static void SDL_LogMessageV(int category, SDL.SDL_LogPriority priority, string fmtAndArglist)
		{
			int num = SDL.Utf8Size(fmtAndArglist);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_LogMessageV(category, priority, SDL.Utf8Encode(fmtAndArglist, ptr, num));
		}

		// Token: 0x06000605 RID: 1541
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_LogPriority SDL_LogGetPriority(int category);

		// Token: 0x06000606 RID: 1542
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetPriority(int category, SDL.SDL_LogPriority priority);

		// Token: 0x06000607 RID: 1543
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetAllPriority(SDL.SDL_LogPriority priority);

		// Token: 0x06000608 RID: 1544
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogResetPriorities();

		// Token: 0x06000609 RID: 1545
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		private static extern void SDL_LogGetOutputFunction(out IntPtr callback, out IntPtr userdata);

		// Token: 0x0600060A RID: 1546 RVA: 0x0000406C File Offset: 0x0000226C
		public static void SDL_LogGetOutputFunction(out SDL.SDL_LogOutputFunction callback, out IntPtr userdata)
		{
			IntPtr zero = IntPtr.Zero;
			SDL.SDL_LogGetOutputFunction(out zero, out userdata);
			if (zero != IntPtr.Zero)
			{
				callback = (SDL.SDL_LogOutputFunction)SDL.GetDelegateForFunctionPointer<SDL.SDL_LogOutputFunction>(zero);
				return;
			}
			callback = null;
		}

		// Token: 0x0600060B RID: 1547
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LogSetOutputFunction(SDL.SDL_LogOutputFunction callback, IntPtr userdata);

		// Token: 0x0600060C RID: 1548
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowMessageBox")]
		private static extern int INTERNAL_SDL_ShowMessageBox([In] ref SDL.INTERNAL_SDL_MessageBoxData messageboxdata, out int buttonid);

		// Token: 0x0600060D RID: 1549 RVA: 0x000040A8 File Offset: 0x000022A8
		private static IntPtr INTERNAL_AllocUTF8(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return IntPtr.Zero;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str + "\0");
			IntPtr intPtr = SDL.SDL_malloc((IntPtr)bytes.Length);
			Marshal.Copy(bytes, 0, intPtr, bytes.Length);
			return intPtr;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000040F4 File Offset: 0x000022F4
		public unsafe static int SDL_ShowMessageBox([In] ref SDL.SDL_MessageBoxData messageboxdata, out int buttonid)
		{
			SDL.INTERNAL_SDL_MessageBoxData internal_SDL_MessageBoxData = new SDL.INTERNAL_SDL_MessageBoxData
			{
				flags = messageboxdata.flags,
				window = messageboxdata.window,
				title = SDL.INTERNAL_AllocUTF8(messageboxdata.title),
				message = SDL.INTERNAL_AllocUTF8(messageboxdata.message),
				numbuttons = messageboxdata.numbuttons
			};
			SDL.INTERNAL_SDL_MessageBoxButtonData[] array = new SDL.INTERNAL_SDL_MessageBoxButtonData[messageboxdata.numbuttons];
			for (int i = 0; i < messageboxdata.numbuttons; i++)
			{
				array[i] = new SDL.INTERNAL_SDL_MessageBoxButtonData
				{
					flags = messageboxdata.buttons[i].flags,
					buttonid = messageboxdata.buttons[i].buttonid,
					text = SDL.INTERNAL_AllocUTF8(messageboxdata.buttons[i].text)
				};
			}
			if (messageboxdata.colorScheme != null)
			{
				internal_SDL_MessageBoxData.colorScheme = Marshal.AllocHGlobal(SDL.SizeOf<SDL.SDL_MessageBoxColorScheme>());
				Marshal.StructureToPtr(messageboxdata.colorScheme.Value, internal_SDL_MessageBoxData.colorScheme, false);
			}
			int num;
			fixed (SDL.INTERNAL_SDL_MessageBoxButtonData* ptr = &array[0])
			{
				SDL.INTERNAL_SDL_MessageBoxButtonData* ptr2 = ptr;
				internal_SDL_MessageBoxData.buttons = (IntPtr)((void*)ptr2);
				num = SDL.INTERNAL_SDL_ShowMessageBox(ref internal_SDL_MessageBoxData, out buttonid);
			}
			Marshal.FreeHGlobal(internal_SDL_MessageBoxData.colorScheme);
			for (int j = 0; j < messageboxdata.numbuttons; j++)
			{
				SDL.SDL_free(array[j].text);
			}
			SDL.SDL_free(internal_SDL_MessageBoxData.message);
			SDL.SDL_free(internal_SDL_MessageBoxData.title);
			return num;
		}

		// Token: 0x0600060F RID: 1551
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowSimpleMessageBox")]
		private unsafe static extern int INTERNAL_SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags flags, byte* title, byte* message, IntPtr window);

		// Token: 0x06000610 RID: 1552 RVA: 0x00004288 File Offset: 0x00002488
		public unsafe static int SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags flags, string title, string message, IntPtr window)
		{
			int num = SDL.Utf8Size(title);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = SDL.Utf8Size(message);
			byte* ptr2 = stackalloc byte[(UIntPtr)num2];
			return SDL.INTERNAL_SDL_ShowSimpleMessageBox(flags, SDL.Utf8Encode(title, ptr, num), SDL.Utf8Encode(message, ptr2, num2), window);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000042C4 File Offset: 0x000024C4
		public static void SDL_VERSION(out SDL.SDL_version x)
		{
			x.major = 2;
			x.minor = 0;
			x.patch = 22;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000042DC File Offset: 0x000024DC
		public static int SDL_VERSIONNUM(int X, int Y, int Z)
		{
			return X * 1000 + Y * 100 + Z;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000042EC File Offset: 0x000024EC
		public static bool SDL_VERSION_ATLEAST(int X, int Y, int Z)
		{
			return SDL.SDL_COMPILEDVERSION >= SDL.SDL_VERSIONNUM(X, Y, Z);
		}

		// Token: 0x06000614 RID: 1556
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetVersion(out SDL.SDL_version ver);

		// Token: 0x06000615 RID: 1557
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetRevision")]
		private static extern IntPtr INTERNAL_SDL_GetRevision();

		// Token: 0x06000616 RID: 1558 RVA: 0x00004300 File Offset: 0x00002500
		public static string SDL_GetRevision()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetRevision(), false);
		}

		// Token: 0x06000617 RID: 1559
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRevisionNumber();

		// Token: 0x06000618 RID: 1560 RVA: 0x0000430D File Offset: 0x0000250D
		public static int SDL_WINDOWPOS_UNDEFINED_DISPLAY(int X)
		{
			return 536805376 | X;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00004316 File Offset: 0x00002516
		public static bool SDL_WINDOWPOS_ISUNDEFINED(int X)
		{
			return ((long)X & (long)((ulong)(-65536))) == 536805376L;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00004329 File Offset: 0x00002529
		public static int SDL_WINDOWPOS_CENTERED_DISPLAY(int X)
		{
			return 805240832 | X;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00004332 File Offset: 0x00002532
		public static bool SDL_WINDOWPOS_ISCENTERED(int X)
		{
			return ((long)X & (long)((ulong)(-65536))) == 805240832L;
		}

		// Token: 0x0600061C RID: 1564
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindow")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateWindow(byte* title, int x, int y, int w, int h, SDL.SDL_WindowFlags flags);

		// Token: 0x0600061D RID: 1565 RVA: 0x00004348 File Offset: 0x00002548
		public unsafe static IntPtr SDL_CreateWindow(string title, int x, int y, int w, int h, SDL.SDL_WindowFlags flags)
		{
			int num = SDL.Utf8Size(title);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_CreateWindow(SDL.Utf8Encode(title, ptr, num), x, y, w, h, flags);
		}

		// Token: 0x0600061E RID: 1566
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_CreateWindowAndRenderer(int width, int height, SDL.SDL_WindowFlags window_flags, out IntPtr window, out IntPtr renderer);

		// Token: 0x0600061F RID: 1567
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateWindowFrom(IntPtr data);

		// Token: 0x06000620 RID: 1568
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyWindow(IntPtr window);

		// Token: 0x06000621 RID: 1569
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DisableScreenSaver();

		// Token: 0x06000622 RID: 1570
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EnableScreenSaver();

		// Token: 0x06000623 RID: 1571
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetClosestDisplayMode(int displayIndex, ref SDL.SDL_DisplayMode mode, out SDL.SDL_DisplayMode closest);

		// Token: 0x06000624 RID: 1572
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out SDL.SDL_DisplayMode mode);

		// Token: 0x06000625 RID: 1573
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentVideoDriver();

		// Token: 0x06000626 RID: 1574 RVA: 0x00004375 File Offset: 0x00002575
		public static string SDL_GetCurrentVideoDriver()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetCurrentVideoDriver(), false);
		}

		// Token: 0x06000627 RID: 1575
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDesktopDisplayMode(int displayIndex, out SDL.SDL_DisplayMode mode);

		// Token: 0x06000628 RID: 1576
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetDisplayName")]
		private static extern IntPtr INTERNAL_SDL_GetDisplayName(int index);

		// Token: 0x06000629 RID: 1577 RVA: 0x00004382 File Offset: 0x00002582
		public static string SDL_GetDisplayName(int index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetDisplayName(index), false);
		}

		// Token: 0x0600062A RID: 1578
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayBounds(int displayIndex, out SDL.SDL_Rect rect);

		// Token: 0x0600062B RID: 1579
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

		// Token: 0x0600062C RID: 1580
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_DisplayOrientation SDL_GetDisplayOrientation(int displayIndex);

		// Token: 0x0600062D RID: 1581
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, out SDL.SDL_DisplayMode mode);

		// Token: 0x0600062E RID: 1582
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDisplayUsableBounds(int displayIndex, out SDL.SDL_Rect rect);

		// Token: 0x0600062F RID: 1583
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumDisplayModes(int displayIndex);

		// Token: 0x06000630 RID: 1584
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumVideoDisplays();

		// Token: 0x06000631 RID: 1585
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumVideoDrivers();

		// Token: 0x06000632 RID: 1586
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetVideoDriver(int index);

		// Token: 0x06000633 RID: 1587 RVA: 0x00004390 File Offset: 0x00002590
		public static string SDL_GetVideoDriver(int index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetVideoDriver(index), false);
		}

		// Token: 0x06000634 RID: 1588
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowBrightness(IntPtr window);

		// Token: 0x06000635 RID: 1589
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowOpacity(IntPtr window, float opacity);

		// Token: 0x06000636 RID: 1590
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowOpacity(IntPtr window, out float out_opacity);

		// Token: 0x06000637 RID: 1591
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowModalFor(IntPtr modal_window, IntPtr parent_window);

		// Token: 0x06000638 RID: 1592
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowInputFocus(IntPtr window);

		// Token: 0x06000639 RID: 1593
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowData")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetWindowData(IntPtr window, byte* name);

		// Token: 0x0600063A RID: 1594 RVA: 0x000043A0 File Offset: 0x000025A0
		public unsafe static IntPtr SDL_GetWindowData(IntPtr window, string name)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GetWindowData(window, SDL.Utf8Encode(name, ptr, num));
		}

		// Token: 0x0600063B RID: 1595
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowDisplayIndex(IntPtr window);

		// Token: 0x0600063C RID: 1596
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowDisplayMode(IntPtr window, out SDL.SDL_DisplayMode mode);

		// Token: 0x0600063D RID: 1597
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowICCProfile(IntPtr window, out IntPtr mode);

		// Token: 0x0600063E RID: 1598
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowFlags(IntPtr window);

		// Token: 0x0600063F RID: 1599
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFromID(uint id);

		// Token: 0x06000640 RID: 1600
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowGammaRamp(IntPtr window, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [Out] ushort[] red, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [Out] ushort[] green, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [Out] ushort[] blue);

		// Token: 0x06000641 RID: 1601
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GetWindowGrab(IntPtr window);

		// Token: 0x06000642 RID: 1602
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GetWindowKeyboardGrab(IntPtr window);

		// Token: 0x06000643 RID: 1603
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GetWindowMouseGrab(IntPtr window);

		// Token: 0x06000644 RID: 1604
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowID(IntPtr window);

		// Token: 0x06000645 RID: 1605
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowPixelFormat(IntPtr window);

		// Token: 0x06000646 RID: 1606
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowMaximumSize(IntPtr window, out int max_w, out int max_h);

		// Token: 0x06000647 RID: 1607
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowMinimumSize(IntPtr window, out int min_w, out int min_h);

		// Token: 0x06000648 RID: 1608
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowPosition(IntPtr window, out int x, out int y);

		// Token: 0x06000649 RID: 1609
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowSize(IntPtr window, out int w, out int h);

		// Token: 0x0600064A RID: 1610
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetWindowSizeInPixels(IntPtr window, out int w, out int h);

		// Token: 0x0600064B RID: 1611
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowSurface(IntPtr window);

		// Token: 0x0600064C RID: 1612
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowTitle")]
		private static extern IntPtr INTERNAL_SDL_GetWindowTitle(IntPtr window);

		// Token: 0x0600064D RID: 1613 RVA: 0x000043C7 File Offset: 0x000025C7
		public static string SDL_GetWindowTitle(IntPtr window)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetWindowTitle(window), false);
		}

		// Token: 0x0600064E RID: 1614
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_BindTexture(IntPtr texture, out float texw, out float texh);

		// Token: 0x0600064F RID: 1615
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_CreateContext(IntPtr window);

		// Token: 0x06000650 RID: 1616
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_DeleteContext(IntPtr context);

		// Token: 0x06000651 RID: 1617
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_LoadLibrary")]
		private unsafe static extern int INTERNAL_SDL_GL_LoadLibrary(byte* path);

		// Token: 0x06000652 RID: 1618 RVA: 0x000043D8 File Offset: 0x000025D8
		public unsafe static int SDL_GL_LoadLibrary(string path)
		{
			byte* ptr = SDL.Utf8EncodeHeap(path);
			int num = SDL.INTERNAL_SDL_GL_LoadLibrary(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000653 RID: 1619
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetProcAddress(IntPtr proc);

		// Token: 0x06000654 RID: 1620 RVA: 0x00004400 File Offset: 0x00002600
		public unsafe static IntPtr SDL_GL_GetProcAddress(string proc)
		{
			int num = SDL.Utf8Size(proc);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.SDL_GL_GetProcAddress((IntPtr)((void*)SDL.Utf8Encode(proc, ptr, num)));
		}

		// Token: 0x06000655 RID: 1621
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_UnloadLibrary();

		// Token: 0x06000656 RID: 1622
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_ExtensionSupported")]
		private unsafe static extern SDL.SDL_bool INTERNAL_SDL_GL_ExtensionSupported(byte* extension);

		// Token: 0x06000657 RID: 1623 RVA: 0x0000442C File Offset: 0x0000262C
		public unsafe static SDL.SDL_bool SDL_GL_ExtensionSupported(string extension)
		{
			int num = SDL.Utf8Size(extension);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GL_ExtensionSupported(SDL.Utf8Encode(extension, ptr, num));
		}

		// Token: 0x06000658 RID: 1624
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_ResetAttributes();

		// Token: 0x06000659 RID: 1625
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_GetAttribute(SDL.SDL_GLattr attr, out int value);

		// Token: 0x0600065A RID: 1626
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_GetSwapInterval();

		// Token: 0x0600065B RID: 1627
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_MakeCurrent(IntPtr window, IntPtr context);

		// Token: 0x0600065C RID: 1628
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetCurrentWindow();

		// Token: 0x0600065D RID: 1629
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetCurrentContext();

		// Token: 0x0600065E RID: 1630
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_GetDrawableSize(IntPtr window, out int w, out int h);

		// Token: 0x0600065F RID: 1631
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_SetAttribute(SDL.SDL_GLattr attr, int value);

		// Token: 0x06000660 RID: 1632 RVA: 0x00004452 File Offset: 0x00002652
		public static int SDL_GL_SetAttribute(SDL.SDL_GLattr attr, SDL.SDL_GLprofile profile)
		{
			return SDL.SDL_GL_SetAttribute(attr, (int)profile);
		}

		// Token: 0x06000661 RID: 1633
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_SetSwapInterval(int interval);

		// Token: 0x06000662 RID: 1634
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_SwapWindow(IntPtr window);

		// Token: 0x06000663 RID: 1635
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GL_UnbindTexture(IntPtr texture);

		// Token: 0x06000664 RID: 1636
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HideWindow(IntPtr window);

		// Token: 0x06000665 RID: 1637
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsScreenSaverEnabled();

		// Token: 0x06000666 RID: 1638
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MaximizeWindow(IntPtr window);

		// Token: 0x06000667 RID: 1639
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MinimizeWindow(IntPtr window);

		// Token: 0x06000668 RID: 1640
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RaiseWindow(IntPtr window);

		// Token: 0x06000669 RID: 1641
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RestoreWindow(IntPtr window);

		// Token: 0x0600066A RID: 1642
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowBrightness(IntPtr window, float brightness);

		// Token: 0x0600066B RID: 1643
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowData")]
		private unsafe static extern IntPtr INTERNAL_SDL_SetWindowData(IntPtr window, byte* name, IntPtr userdata);

		// Token: 0x0600066C RID: 1644 RVA: 0x0000445C File Offset: 0x0000265C
		public unsafe static IntPtr SDL_SetWindowData(IntPtr window, string name, IntPtr userdata)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_SetWindowData(window, SDL.Utf8Encode(name, ptr, num), userdata);
		}

		// Token: 0x0600066D RID: 1645
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowDisplayMode(IntPtr window, ref SDL.SDL_DisplayMode mode);

		// Token: 0x0600066E RID: 1646
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowDisplayMode(IntPtr window, IntPtr mode);

		// Token: 0x0600066F RID: 1647
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowFullscreen(IntPtr window, uint flags);

		// Token: 0x06000670 RID: 1648
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowGammaRamp(IntPtr window, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [In] ushort[] red, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [In] ushort[] green, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [In] ushort[] blue);

		// Token: 0x06000671 RID: 1649
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowGrab(IntPtr window, SDL.SDL_bool grabbed);

		// Token: 0x06000672 RID: 1650
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowKeyboardGrab(IntPtr window, SDL.SDL_bool grabbed);

		// Token: 0x06000673 RID: 1651
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowMouseGrab(IntPtr window, SDL.SDL_bool grabbed);

		// Token: 0x06000674 RID: 1652
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowIcon(IntPtr window, IntPtr icon);

		// Token: 0x06000675 RID: 1653
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowMaximumSize(IntPtr window, int max_w, int max_h);

		// Token: 0x06000676 RID: 1654
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowMinimumSize(IntPtr window, int min_w, int min_h);

		// Token: 0x06000677 RID: 1655
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowPosition(IntPtr window, int x, int y);

		// Token: 0x06000678 RID: 1656
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowSize(IntPtr window, int w, int h);

		// Token: 0x06000679 RID: 1657
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowBordered(IntPtr window, SDL.SDL_bool bordered);

		// Token: 0x0600067A RID: 1658
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right);

		// Token: 0x0600067B RID: 1659
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowResizable(IntPtr window, SDL.SDL_bool resizable);

		// Token: 0x0600067C RID: 1660
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowAlwaysOnTop(IntPtr window, SDL.SDL_bool on_top);

		// Token: 0x0600067D RID: 1661
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowTitle")]
		private unsafe static extern void INTERNAL_SDL_SetWindowTitle(IntPtr window, byte* title);

		// Token: 0x0600067E RID: 1662 RVA: 0x00004484 File Offset: 0x00002684
		public unsafe static void SDL_SetWindowTitle(IntPtr window, string title)
		{
			int num = SDL.Utf8Size(title);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			SDL.INTERNAL_SDL_SetWindowTitle(window, SDL.Utf8Encode(title, ptr, num));
		}

		// Token: 0x0600067F RID: 1663
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ShowWindow(IntPtr window);

		// Token: 0x06000680 RID: 1664
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateWindowSurface(IntPtr window);

		// Token: 0x06000681 RID: 1665
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateWindowSurfaceRects(IntPtr window, [In] SDL.SDL_Rect[] rects, int numrects);

		// Token: 0x06000682 RID: 1666
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_VideoInit")]
		private unsafe static extern int INTERNAL_SDL_VideoInit(byte* driver_name);

		// Token: 0x06000683 RID: 1667 RVA: 0x000044AC File Offset: 0x000026AC
		public unsafe static int SDL_VideoInit(string driver_name)
		{
			int num = SDL.Utf8Size(driver_name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_VideoInit(SDL.Utf8Encode(driver_name, ptr, num));
		}

		// Token: 0x06000684 RID: 1668
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_VideoQuit();

		// Token: 0x06000685 RID: 1669
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowHitTest(IntPtr window, SDL.SDL_HitTest callback, IntPtr callback_data);

		// Token: 0x06000686 RID: 1670
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGrabbedWindow();

		// Token: 0x06000687 RID: 1671
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowMouseRect(IntPtr window, ref SDL.SDL_Rect rect);

		// Token: 0x06000688 RID: 1672
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowMouseRect(IntPtr window, IntPtr rect);

		// Token: 0x06000689 RID: 1673
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowMouseRect(IntPtr window);

		// Token: 0x0600068A RID: 1674
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FlashWindow(IntPtr window, SDL.SDL_FlashOperation operation);

		// Token: 0x0600068B RID: 1675
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_BlendMode SDL_ComposeCustomBlendMode(SDL.SDL_BlendFactor srcColorFactor, SDL.SDL_BlendFactor dstColorFactor, SDL.SDL_BlendOperation colorOperation, SDL.SDL_BlendFactor srcAlphaFactor, SDL.SDL_BlendFactor dstAlphaFactor, SDL.SDL_BlendOperation alphaOperation);

		// Token: 0x0600068C RID: 1676
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Vulkan_LoadLibrary")]
		private unsafe static extern int INTERNAL_SDL_Vulkan_LoadLibrary(byte* path);

		// Token: 0x0600068D RID: 1677 RVA: 0x000044D4 File Offset: 0x000026D4
		public unsafe static int SDL_Vulkan_LoadLibrary(string path)
		{
			byte* ptr = SDL.Utf8EncodeHeap(path);
			int num = SDL.INTERNAL_SDL_Vulkan_LoadLibrary(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x0600068E RID: 1678
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Vulkan_GetVkGetInstanceProcAddr();

		// Token: 0x0600068F RID: 1679
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Vulkan_UnloadLibrary();

		// Token: 0x06000690 RID: 1680
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_Vulkan_GetInstanceExtensions(IntPtr window, out uint pCount, IntPtr pNames);

		// Token: 0x06000691 RID: 1681
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_Vulkan_GetInstanceExtensions(IntPtr window, out uint pCount, IntPtr[] pNames);

		// Token: 0x06000692 RID: 1682
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_Vulkan_CreateSurface(IntPtr window, IntPtr instance, out ulong surface);

		// Token: 0x06000693 RID: 1683
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Vulkan_GetDrawableSize(IntPtr window, out int w, out int h);

		// Token: 0x06000694 RID: 1684
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_CreateView(IntPtr window);

		// Token: 0x06000695 RID: 1685
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Metal_DestroyView(IntPtr view);

		// Token: 0x06000696 RID: 1686
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_GetLayer(IntPtr view);

		// Token: 0x06000697 RID: 1687
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Metal_GetDrawableSize(IntPtr window, out int w, out int h);

		// Token: 0x06000698 RID: 1688
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRenderer(IntPtr window, int index, SDL.SDL_RendererFlags flags);

		// Token: 0x06000699 RID: 1689
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

		// Token: 0x0600069A RID: 1690
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

		// Token: 0x0600069B RID: 1691
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

		// Token: 0x0600069C RID: 1692
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyRenderer(IntPtr renderer);

		// Token: 0x0600069D RID: 1693
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyTexture(IntPtr texture);

		// Token: 0x0600069E RID: 1694
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumRenderDrivers();

		// Token: 0x0600069F RID: 1695
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDrawBlendMode(IntPtr renderer, out SDL.SDL_BlendMode blendMode);

		// Token: 0x060006A0 RID: 1696
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureScaleMode(IntPtr texture, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x060006A1 RID: 1697
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureScaleMode(IntPtr texture, out SDL.SDL_ScaleMode scaleMode);

		// Token: 0x060006A2 RID: 1698
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureUserData(IntPtr texture, IntPtr userdata);

		// Token: 0x060006A3 RID: 1699
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTextureUserData(IntPtr texture);

		// Token: 0x060006A4 RID: 1700
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

		// Token: 0x060006A5 RID: 1701
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRenderDriverInfo(int index, out SDL.SDL_RendererInfo info);

		// Token: 0x060006A6 RID: 1702
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderer(IntPtr window);

		// Token: 0x060006A7 RID: 1703
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRendererInfo(IntPtr renderer, out SDL.SDL_RendererInfo info);

		// Token: 0x060006A8 RID: 1704
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetRendererOutputSize(IntPtr renderer, out int w, out int h);

		// Token: 0x060006A9 RID: 1705
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureAlphaMod(IntPtr texture, out byte alpha);

		// Token: 0x060006AA RID: 1706
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureBlendMode(IntPtr texture, out SDL.SDL_BlendMode blendMode);

		// Token: 0x060006AB RID: 1707
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

		// Token: 0x060006AC RID: 1708
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockTexture(IntPtr texture, ref SDL.SDL_Rect rect, out IntPtr pixels, out int pitch);

		// Token: 0x060006AD RID: 1709
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);

		// Token: 0x060006AE RID: 1710
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockTextureToSurface(IntPtr texture, ref SDL.SDL_Rect rect, out IntPtr surface);

		// Token: 0x060006AF RID: 1711
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockTextureToSurface(IntPtr texture, IntPtr rect, out IntPtr surface);

		// Token: 0x060006B0 RID: 1712
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_QueryTexture(IntPtr texture, out uint format, out int access, out int w, out int h);

		// Token: 0x060006B1 RID: 1713
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderClear(IntPtr renderer);

		// Token: 0x060006B2 RID: 1714
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_Rect dstrect);

		// Token: 0x060006B3 RID: 1715
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_Rect dstrect);

		// Token: 0x060006B4 RID: 1716
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect);

		// Token: 0x060006B5 RID: 1717
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

		// Token: 0x060006B6 RID: 1718
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_Rect dstrect, double angle, ref SDL.SDL_Point center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006B7 RID: 1719
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_Rect dstrect, double angle, ref SDL.SDL_Point center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006B8 RID: 1720
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect, double angle, ref SDL.SDL_Point center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006B9 RID: 1721
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_Rect dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006BA RID: 1722
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, ref SDL.SDL_Point center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006BB RID: 1723
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_Rect dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006BC RID: 1724
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006BD RID: 1725
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006BE RID: 1726
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

		// Token: 0x060006BF RID: 1727
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLines(IntPtr renderer, [In] SDL.SDL_Point[] points, int count);

		// Token: 0x060006C0 RID: 1728
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPoint(IntPtr renderer, int x, int y);

		// Token: 0x060006C1 RID: 1729
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPoints(IntPtr renderer, [In] SDL.SDL_Point[] points, int count);

		// Token: 0x060006C2 RID: 1730
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRect(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x060006C3 RID: 1731
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRect(IntPtr renderer, IntPtr rect);

		// Token: 0x060006C4 RID: 1732
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRects(IntPtr renderer, [In] SDL.SDL_Rect[] rects, int count);

		// Token: 0x060006C5 RID: 1733
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRect(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x060006C6 RID: 1734
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRect(IntPtr renderer, IntPtr rect);

		// Token: 0x060006C7 RID: 1735
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRects(IntPtr renderer, [In] SDL.SDL_Rect[] rects, int count);

		// Token: 0x060006C8 RID: 1736
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_FRect dstrect);

		// Token: 0x060006C9 RID: 1737
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyF(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_FRect dstrect);

		// Token: 0x060006CA RID: 1738
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect);

		// Token: 0x060006CB RID: 1739
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

		// Token: 0x060006CC RID: 1740
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_FRect dstrect, double angle, ref SDL.SDL_FPoint center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006CD RID: 1741
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_FRect dstrect, double angle, ref SDL.SDL_FPoint center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006CE RID: 1742
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect, double angle, ref SDL.SDL_FPoint center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006CF RID: 1743
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, ref SDL.SDL_FRect dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006D0 RID: 1744
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, ref SDL.SDL_FPoint center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006D1 RID: 1745
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref SDL.SDL_FRect dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006D2 RID: 1746
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, ref SDL.SDL_Rect srcrect, IntPtr dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006D3 RID: 1747
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, SDL.SDL_RendererFlip flip);

		// Token: 0x060006D4 RID: 1748
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderGeometry(IntPtr renderer, IntPtr texture, [In] SDL.SDL_Vertex[] vertices, int num_vertices, [In] int[] indices, int num_indices);

		// Token: 0x060006D5 RID: 1749
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderGeometryRaw(IntPtr renderer, IntPtr texture, [In] float[] xy, int xy_stride, [In] int[] color, int color_stride, [In] float[] uv, int uv_stride, int num_vertices, IntPtr indices, int num_indices, int size_indices);

		// Token: 0x060006D6 RID: 1750
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPointF(IntPtr renderer, float x, float y);

		// Token: 0x060006D7 RID: 1751
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawPointsF(IntPtr renderer, [In] SDL.SDL_FPoint[] points, int count);

		// Token: 0x060006D8 RID: 1752
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLineF(IntPtr renderer, float x1, float y1, float x2, float y2);

		// Token: 0x060006D9 RID: 1753
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawLinesF(IntPtr renderer, [In] SDL.SDL_FPoint[] points, int count);

		// Token: 0x060006DA RID: 1754
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRectF(IntPtr renderer, ref SDL.SDL_FRect rect);

		// Token: 0x060006DB RID: 1755
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRectF(IntPtr renderer, IntPtr rect);

		// Token: 0x060006DC RID: 1756
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderDrawRectsF(IntPtr renderer, [In] SDL.SDL_FRect[] rects, int count);

		// Token: 0x060006DD RID: 1757
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRectF(IntPtr renderer, ref SDL.SDL_FRect rect);

		// Token: 0x060006DE RID: 1758
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRectF(IntPtr renderer, IntPtr rect);

		// Token: 0x060006DF RID: 1759
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFillRectsF(IntPtr renderer, [In] SDL.SDL_FRect[] rects, int count);

		// Token: 0x060006E0 RID: 1760
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderGetClipRect(IntPtr renderer, out SDL.SDL_Rect rect);

		// Token: 0x060006E1 RID: 1761
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

		// Token: 0x060006E2 RID: 1762
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderGetScale(IntPtr renderer, out float scaleX, out float scaleY);

		// Token: 0x060006E3 RID: 1763
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderWindowToLogical(IntPtr renderer, int windowX, int windowY, out float logicalX, out float logicalY);

		// Token: 0x060006E4 RID: 1764
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderLogicalToWindow(IntPtr renderer, float logicalX, float logicalY, out int windowX, out int windowY);

		// Token: 0x060006E5 RID: 1765
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderGetViewport(IntPtr renderer, out SDL.SDL_Rect rect);

		// Token: 0x060006E6 RID: 1766
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RenderPresent(IntPtr renderer);

		// Token: 0x060006E7 RID: 1767
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderReadPixels(IntPtr renderer, ref SDL.SDL_Rect rect, uint format, IntPtr pixels, int pitch);

		// Token: 0x060006E8 RID: 1768
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetClipRect(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x060006E9 RID: 1769
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetClipRect(IntPtr renderer, IntPtr rect);

		// Token: 0x060006EA RID: 1770
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetLogicalSize(IntPtr renderer, int w, int h);

		// Token: 0x060006EB RID: 1771
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetScale(IntPtr renderer, float scaleX, float scaleY);

		// Token: 0x060006EC RID: 1772
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetIntegerScale(IntPtr renderer, SDL.SDL_bool enable);

		// Token: 0x060006ED RID: 1773
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetViewport(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x060006EE RID: 1774
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderDrawBlendMode(IntPtr renderer, SDL.SDL_BlendMode blendMode);

		// Token: 0x060006EF RID: 1775
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

		// Token: 0x060006F0 RID: 1776
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

		// Token: 0x060006F1 RID: 1777
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureAlphaMod(IntPtr texture, byte alpha);

		// Token: 0x060006F2 RID: 1778
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureBlendMode(IntPtr texture, SDL.SDL_BlendMode blendMode);

		// Token: 0x060006F3 RID: 1779
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

		// Token: 0x060006F4 RID: 1780
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockTexture(IntPtr texture);

		// Token: 0x060006F5 RID: 1781
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr pixels, int pitch);

		// Token: 0x060006F6 RID: 1782
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

		// Token: 0x060006F7 RID: 1783
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateYUVTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr yPlane, int yPitch, IntPtr uPlane, int uPitch, IntPtr vPlane, int vPitch);

		// Token: 0x060006F8 RID: 1784
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpdateNVTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr yPlane, int yPitch, IntPtr uvPlane, int uvPitch);

		// Token: 0x060006F9 RID: 1785
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_RenderTargetSupported(IntPtr renderer);

		// Token: 0x060006FA RID: 1786
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

		// Token: 0x060006FB RID: 1787
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderGetMetalLayer(IntPtr renderer);

		// Token: 0x060006FC RID: 1788
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderGetMetalCommandEncoder(IntPtr renderer);

		// Token: 0x060006FD RID: 1789
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderSetVSync(IntPtr renderer, int vsync);

		// Token: 0x060006FE RID: 1790
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_RenderIsClipEnabled(IntPtr renderer);

		// Token: 0x060006FF RID: 1791
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RenderFlush(IntPtr renderer);

		// Token: 0x06000700 RID: 1792 RVA: 0x000044F9 File Offset: 0x000026F9
		public static uint SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
		{
			return SDL.SDL_FOURCC(A, B, C, D);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00004504 File Offset: 0x00002704
		public static uint SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType type, uint order, SDL.SDL_PackedLayout layout, byte bits, byte bytes)
		{
			return (uint)(268435456 | ((int)((byte)type) << 24) | ((int)((byte)order) << 20) | ((int)((byte)layout) << 16) | ((int)bits << 8) | (int)bytes);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00004524 File Offset: 0x00002724
		public static byte SDL_PIXELFLAG(uint X)
		{
			return (byte)((X >> 28) & 15U);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000452E File Offset: 0x0000272E
		public static byte SDL_PIXELTYPE(uint X)
		{
			return (byte)((X >> 24) & 15U);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00004538 File Offset: 0x00002738
		public static byte SDL_PIXELORDER(uint X)
		{
			return (byte)((X >> 20) & 15U);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00004542 File Offset: 0x00002742
		public static byte SDL_PIXELLAYOUT(uint X)
		{
			return (byte)((X >> 16) & 15U);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000454C File Offset: 0x0000274C
		public static byte SDL_BITSPERPIXEL(uint X)
		{
			return (byte)((X >> 8) & 255U);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00004558 File Offset: 0x00002758
		public static byte SDL_BYTESPERPIXEL(uint X)
		{
			if (!SDL.SDL_ISPIXELFORMAT_FOURCC(X))
			{
				return (byte)(X & 255U);
			}
			if (X == SDL.SDL_PIXELFORMAT_YUY2 || X == SDL.SDL_PIXELFORMAT_UYVY || X == SDL.SDL_PIXELFORMAT_YVYU)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00004588 File Offset: 0x00002788
		public static bool SDL_ISPIXELFORMAT_INDEXED(uint format)
		{
			if (SDL.SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL.SDL_PixelType sdl_PixelType = (SDL.SDL_PixelType)SDL.SDL_PIXELTYPE(format);
			return sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX1 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX4 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX8;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000045B4 File Offset: 0x000027B4
		public static bool SDL_ISPIXELFORMAT_PACKED(uint format)
		{
			if (SDL.SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL.SDL_PixelType sdl_PixelType = (SDL.SDL_PixelType)SDL.SDL_PIXELTYPE(format);
			return sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED8 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000045E0 File Offset: 0x000027E0
		public static bool SDL_ISPIXELFORMAT_ARRAY(uint format)
		{
			if (SDL.SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL.SDL_PixelType sdl_PixelType = (SDL.SDL_PixelType)SDL.SDL_PIXELTYPE(format);
			return sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYU8 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYU16 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYU32 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYF16 || sdl_PixelType == SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYF32;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00004618 File Offset: 0x00002818
		public static bool SDL_ISPIXELFORMAT_ALPHA(uint format)
		{
			if (SDL.SDL_ISPIXELFORMAT_PACKED(format))
			{
				SDL.SDL_PackedOrder sdl_PackedOrder = (SDL.SDL_PackedOrder)SDL.SDL_PIXELORDER(format);
				return sdl_PackedOrder == SDL.SDL_PackedOrder.SDL_PACKEDORDER_ARGB || sdl_PackedOrder == SDL.SDL_PackedOrder.SDL_PACKEDORDER_RGBA || sdl_PackedOrder == SDL.SDL_PackedOrder.SDL_PACKEDORDER_ABGR || sdl_PackedOrder == SDL.SDL_PackedOrder.SDL_PACKEDORDER_BGRA;
			}
			if (SDL.SDL_ISPIXELFORMAT_ARRAY(format))
			{
				SDL.SDL_ArrayOrder sdl_ArrayOrder = (SDL.SDL_ArrayOrder)SDL.SDL_PIXELORDER(format);
				return sdl_ArrayOrder == SDL.SDL_ArrayOrder.SDL_ARRAYORDER_ARGB || sdl_ArrayOrder == SDL.SDL_ArrayOrder.SDL_ARRAYORDER_RGBA || sdl_ArrayOrder == SDL.SDL_ArrayOrder.SDL_ARRAYORDER_ABGR || sdl_ArrayOrder == SDL.SDL_ArrayOrder.SDL_ARRAYORDER_BGRA;
			}
			return false;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000466A File Offset: 0x0000286A
		public static bool SDL_ISPIXELFORMAT_FOURCC(uint format)
		{
			return format == 0U && SDL.SDL_PIXELFLAG(format) != 1;
		}

		// Token: 0x0600070D RID: 1805
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AllocFormat(uint pixel_format);

		// Token: 0x0600070E RID: 1806
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AllocPalette(int ncolors);

		// Token: 0x0600070F RID: 1807
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CalculateGammaRamp(float gamma, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] [Out] ushort[] ramp);

		// Token: 0x06000710 RID: 1808
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeFormat(IntPtr format);

		// Token: 0x06000711 RID: 1809
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreePalette(IntPtr palette);

		// Token: 0x06000712 RID: 1810
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPixelFormatName")]
		private static extern IntPtr INTERNAL_SDL_GetPixelFormatName(uint format);

		// Token: 0x06000713 RID: 1811 RVA: 0x0000467D File Offset: 0x0000287D
		public static string SDL_GetPixelFormatName(uint format)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetPixelFormatName(format), false);
		}

		// Token: 0x06000714 RID: 1812
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGB(uint pixel, IntPtr format, out byte r, out byte g, out byte b);

		// Token: 0x06000715 RID: 1813
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGBA(uint pixel, IntPtr format, out byte r, out byte g, out byte b, out byte a);

		// Token: 0x06000716 RID: 1814
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGB(IntPtr format, byte r, byte g, byte b);

		// Token: 0x06000717 RID: 1815
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGBA(IntPtr format, byte r, byte g, byte b, byte a);

		// Token: 0x06000718 RID: 1816
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MasksToPixelFormatEnum(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

		// Token: 0x06000719 RID: 1817
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_PixelFormatEnumToMasks(uint format, out int bpp, out uint Rmask, out uint Gmask, out uint Bmask, out uint Amask);

		// Token: 0x0600071A RID: 1818
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetPaletteColors(IntPtr palette, [In] SDL.SDL_Color[] colors, int firstcolor, int ncolors);

		// Token: 0x0600071B RID: 1819
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetPixelFormatPalette(IntPtr format, IntPtr palette);

		// Token: 0x0600071C RID: 1820 RVA: 0x0000468C File Offset: 0x0000288C
		public static SDL.SDL_bool SDL_PointInRect(ref SDL.SDL_Point p, ref SDL.SDL_Rect r)
		{
			if (p.x < r.x || p.x >= r.x + r.w || p.y < r.y || p.y >= r.y + r.h)
			{
				return SDL.SDL_bool.SDL_FALSE;
			}
			return SDL.SDL_bool.SDL_TRUE;
		}

		// Token: 0x0600071D RID: 1821
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_EnclosePoints([In] SDL.SDL_Point[] points, int count, ref SDL.SDL_Rect clip, out SDL.SDL_Rect result);

		// Token: 0x0600071E RID: 1822
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasIntersection(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B);

		// Token: 0x0600071F RID: 1823
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IntersectRect(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B, out SDL.SDL_Rect result);

		// Token: 0x06000720 RID: 1824
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IntersectRectAndLine(ref SDL.SDL_Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);

		// Token: 0x06000721 RID: 1825 RVA: 0x000046E2 File Offset: 0x000028E2
		public static SDL.SDL_bool SDL_RectEmpty(ref SDL.SDL_Rect r)
		{
			if (r.w > 0 && r.h > 0)
			{
				return SDL.SDL_bool.SDL_FALSE;
			}
			return SDL.SDL_bool.SDL_TRUE;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000046F9 File Offset: 0x000028F9
		public static SDL.SDL_bool SDL_RectEquals(ref SDL.SDL_Rect a, ref SDL.SDL_Rect b)
		{
			if (a.x != b.x || a.y != b.y || a.w != b.w || a.h != b.h)
			{
				return SDL.SDL_bool.SDL_FALSE;
			}
			return SDL.SDL_bool.SDL_TRUE;
		}

		// Token: 0x06000723 RID: 1827
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnionRect(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B, out SDL.SDL_Rect result);

		// Token: 0x06000724 RID: 1828
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateShapedWindow")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateShapedWindow(byte* title, uint x, uint y, uint w, uint h, SDL.SDL_WindowFlags flags);

		// Token: 0x06000725 RID: 1829 RVA: 0x00004738 File Offset: 0x00002938
		public unsafe static IntPtr SDL_CreateShapedWindow(string title, uint x, uint y, uint w, uint h, SDL.SDL_WindowFlags flags)
		{
			byte* ptr = SDL.Utf8EncodeHeap(title);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateShapedWindow(ptr, x, y, w, h, flags);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000726 RID: 1830
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsShapedWindow(IntPtr window);

		// Token: 0x06000727 RID: 1831 RVA: 0x00004764 File Offset: 0x00002964
		public static bool SDL_SHAPEMODEALPHA(SDL.WindowShapeMode mode)
		{
			return mode <= SDL.WindowShapeMode.ShapeModeReverseBinarizeAlpha;
		}

		// Token: 0x06000728 RID: 1832
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetWindowShape(IntPtr window, IntPtr shape, ref SDL.SDL_WindowShapeMode shape_mode);

		// Token: 0x06000729 RID: 1833
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetShapedWindowMode(IntPtr window, out SDL.SDL_WindowShapeMode shape_mode);

		// Token: 0x0600072A RID: 1834
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetShapedWindowMode(IntPtr window, IntPtr shape_mode);

		// Token: 0x0600072B RID: 1835 RVA: 0x0000476D File Offset: 0x0000296D
		public static bool SDL_MUSTLOCK(IntPtr surface)
		{
			return (SDL.PtrToStructure<SDL.SDL_Surface>(surface).flags & 2U) > 0U;
		}

		// Token: 0x0600072C RID: 1836
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlit")]
		public static extern int SDL_BlitSurface(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x0600072D RID: 1837
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlit")]
		public static extern int SDL_BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x0600072E RID: 1838
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlit")]
		public static extern int SDL_BlitSurface(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x0600072F RID: 1839
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlit")]
		public static extern int SDL_BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x06000730 RID: 1840
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlitScaled")]
		public static extern int SDL_BlitScaled(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x06000731 RID: 1841
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlitScaled")]
		public static extern int SDL_BlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x06000732 RID: 1842
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlitScaled")]
		public static extern int SDL_BlitScaled(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x06000733 RID: 1843
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_UpperBlitScaled")]
		public static extern int SDL_BlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x06000734 RID: 1844
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_ConvertPixels(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

		// Token: 0x06000735 RID: 1845
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PremultiplyAlpha(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

		// Token: 0x06000736 RID: 1846
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurface(IntPtr src, IntPtr fmt, uint flags);

		// Token: 0x06000737 RID: 1847
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);

		// Token: 0x06000738 RID: 1848
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);

		// Token: 0x06000739 RID: 1849
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

		// Token: 0x0600073A RID: 1850
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

		// Token: 0x0600073B RID: 1851
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);

		// Token: 0x0600073C RID: 1852
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FillRect(IntPtr dst, ref SDL.SDL_Rect rect, uint color);

		// Token: 0x0600073D RID: 1853
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FillRect(IntPtr dst, IntPtr rect, uint color);

		// Token: 0x0600073E RID: 1854
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_FillRects(IntPtr dst, [In] SDL.SDL_Rect[] rects, int count, uint color);

		// Token: 0x0600073F RID: 1855
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeSurface(IntPtr surface);

		// Token: 0x06000740 RID: 1856
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetClipRect(IntPtr surface, out SDL.SDL_Rect rect);

		// Token: 0x06000741 RID: 1857
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasColorKey(IntPtr surface);

		// Token: 0x06000742 RID: 1858
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetColorKey(IntPtr surface, out uint key);

		// Token: 0x06000743 RID: 1859
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

		// Token: 0x06000744 RID: 1860
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceBlendMode(IntPtr surface, out SDL.SDL_BlendMode blendMode);

		// Token: 0x06000745 RID: 1861
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

		// Token: 0x06000746 RID: 1862
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadBMP_RW(IntPtr src, int freesrc);

		// Token: 0x06000747 RID: 1863 RVA: 0x0000477F File Offset: 0x0000297F
		public static IntPtr SDL_LoadBMP(string file)
		{
			return SDL.SDL_LoadBMP_RW(SDL.SDL_RWFromFile(file, "rb"), 1);
		}

		// Token: 0x06000748 RID: 1864
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LockSurface(IntPtr surface);

		// Token: 0x06000749 RID: 1865
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LowerBlit(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x0600074A RID: 1866
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_LowerBlitScaled(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x0600074B RID: 1867
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SaveBMP_RW(IntPtr surface, IntPtr src, int freesrc);

		// Token: 0x0600074C RID: 1868 RVA: 0x00004794 File Offset: 0x00002994
		public static int SDL_SaveBMP(IntPtr surface, string file)
		{
			IntPtr intPtr = SDL.SDL_RWFromFile(file, "wb");
			return SDL.SDL_SaveBMP_RW(surface, intPtr, 1);
		}

		// Token: 0x0600074D RID: 1869
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_SetClipRect(IntPtr surface, ref SDL.SDL_Rect rect);

		// Token: 0x0600074E RID: 1870
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetColorKey(IntPtr surface, int flag, uint key);

		// Token: 0x0600074F RID: 1871
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceAlphaMod(IntPtr surface, byte alpha);

		// Token: 0x06000750 RID: 1872
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceBlendMode(IntPtr surface, SDL.SDL_BlendMode blendMode);

		// Token: 0x06000751 RID: 1873
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

		// Token: 0x06000752 RID: 1874
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfacePalette(IntPtr surface, IntPtr palette);

		// Token: 0x06000753 RID: 1875
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetSurfaceRLE(IntPtr surface, int flag);

		// Token: 0x06000754 RID: 1876
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSurfaceRLE(IntPtr surface);

		// Token: 0x06000755 RID: 1877
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SoftStretch(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x06000756 RID: 1878
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SoftStretchLinear(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x06000757 RID: 1879
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSurface(IntPtr surface);

		// Token: 0x06000758 RID: 1880
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpperBlit(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x06000759 RID: 1881
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_UpperBlitScaled(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect);

		// Token: 0x0600075A RID: 1882
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_DuplicateSurface(IntPtr surface);

		// Token: 0x0600075B RID: 1883
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasClipboardText();

		// Token: 0x0600075C RID: 1884
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetClipboardText")]
		private static extern IntPtr INTERNAL_SDL_GetClipboardText();

		// Token: 0x0600075D RID: 1885 RVA: 0x000047B5 File Offset: 0x000029B5
		public static string SDL_GetClipboardText()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetClipboardText(), true);
		}

		// Token: 0x0600075E RID: 1886
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetClipboardText")]
		private unsafe static extern int INTERNAL_SDL_SetClipboardText(byte* text);

		// Token: 0x0600075F RID: 1887 RVA: 0x000047C4 File Offset: 0x000029C4
		public unsafe static int SDL_SetClipboardText(string text)
		{
			byte* ptr = SDL.Utf8EncodeHeap(text);
			int num = SDL.INTERNAL_SDL_SetClipboardText(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000760 RID: 1888
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PumpEvents();

		// Token: 0x06000761 RID: 1889
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PeepEvents([Out] SDL.SDL_Event[] events, int numevents, SDL.SDL_eventaction action, SDL.SDL_EventType minType, SDL.SDL_EventType maxType);

		// Token: 0x06000762 RID: 1890
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern int SDL_PeepEvents(SDL.SDL_Event* events, int numevents, SDL.SDL_eventaction action, SDL.SDL_EventType minType, SDL.SDL_EventType maxType);

		// Token: 0x06000763 RID: 1891
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasEvent(SDL.SDL_EventType type);

		// Token: 0x06000764 RID: 1892
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasEvents(SDL.SDL_EventType minType, SDL.SDL_EventType maxType);

		// Token: 0x06000765 RID: 1893
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvent(SDL.SDL_EventType type);

		// Token: 0x06000766 RID: 1894
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvents(SDL.SDL_EventType min, SDL.SDL_EventType max);

		// Token: 0x06000767 RID: 1895
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PollEvent(out SDL.SDL_Event _event);

		// Token: 0x06000768 RID: 1896
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WaitEvent(out SDL.SDL_Event _event);

		// Token: 0x06000769 RID: 1897
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WaitEventTimeout(out SDL.SDL_Event _event, int timeout);

		// Token: 0x0600076A RID: 1898
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PushEvent(ref SDL.SDL_Event _event);

		// Token: 0x0600076B RID: 1899
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventFilter(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x0600076C RID: 1900
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		private static extern SDL.SDL_bool SDL_GetEventFilter(out IntPtr filter, out IntPtr userdata);

		// Token: 0x0600076D RID: 1901 RVA: 0x000047EC File Offset: 0x000029EC
		public static SDL.SDL_bool SDL_GetEventFilter(out SDL.SDL_EventFilter filter, out IntPtr userdata)
		{
			IntPtr zero = IntPtr.Zero;
			SDL.SDL_bool sdl_bool = SDL.SDL_GetEventFilter(out zero, out userdata);
			if (zero != IntPtr.Zero)
			{
				filter = (SDL.SDL_EventFilter)SDL.GetDelegateForFunctionPointer<SDL.SDL_EventFilter>(zero);
				return sdl_bool;
			}
			filter = null;
			return sdl_bool;
		}

		// Token: 0x0600076E RID: 1902
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AddEventWatch(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x0600076F RID: 1903
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DelEventWatch(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x06000770 RID: 1904
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FilterEvents(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x06000771 RID: 1905
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_EventState(SDL.SDL_EventType type, int state);

		// Token: 0x06000772 RID: 1906 RVA: 0x00004825 File Offset: 0x00002A25
		public static byte SDL_GetEventState(SDL.SDL_EventType type)
		{
			return SDL.SDL_EventState(type, -1);
		}

		// Token: 0x06000773 RID: 1907
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_RegisterEvents(int numevents);

		// Token: 0x06000774 RID: 1908 RVA: 0x0000482E File Offset: 0x00002A2E
		public static SDL.SDL_Keycode SDL_SCANCODE_TO_KEYCODE(SDL.SDL_Scancode X)
		{
			return (SDL.SDL_Keycode)(X | (SDL.SDL_Scancode)1073741824);
		}

		// Token: 0x06000775 RID: 1909
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardFocus();

		// Token: 0x06000776 RID: 1910
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardState(out int numkeys);

		// Token: 0x06000777 RID: 1911
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Keymod SDL_GetModState();

		// Token: 0x06000778 RID: 1912
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetModState(SDL.SDL_Keymod modstate);

		// Token: 0x06000779 RID: 1913
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Keycode SDL_GetKeyFromScancode(SDL.SDL_Scancode scancode);

		// Token: 0x0600077A RID: 1914
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Scancode SDL_GetScancodeFromKey(SDL.SDL_Keycode key);

		// Token: 0x0600077B RID: 1915
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetScancodeName")]
		private static extern IntPtr INTERNAL_SDL_GetScancodeName(SDL.SDL_Scancode scancode);

		// Token: 0x0600077C RID: 1916 RVA: 0x00004837 File Offset: 0x00002A37
		public static string SDL_GetScancodeName(SDL.SDL_Scancode scancode)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetScancodeName(scancode), false);
		}

		// Token: 0x0600077D RID: 1917
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetScancodeFromName")]
		private unsafe static extern SDL.SDL_Scancode INTERNAL_SDL_GetScancodeFromName(byte* name);

		// Token: 0x0600077E RID: 1918 RVA: 0x00004848 File Offset: 0x00002A48
		public unsafe static SDL.SDL_Scancode SDL_GetScancodeFromName(string name)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GetScancodeFromName(SDL.Utf8Encode(name, ptr, num));
		}

		// Token: 0x0600077F RID: 1919
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetKeyName")]
		private static extern IntPtr INTERNAL_SDL_GetKeyName(SDL.SDL_Keycode key);

		// Token: 0x06000780 RID: 1920 RVA: 0x0000486E File Offset: 0x00002A6E
		public static string SDL_GetKeyName(SDL.SDL_Keycode key)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetKeyName(key), false);
		}

		// Token: 0x06000781 RID: 1921
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetKeyFromName")]
		private unsafe static extern SDL.SDL_Keycode INTERNAL_SDL_GetKeyFromName(byte* name);

		// Token: 0x06000782 RID: 1922 RVA: 0x0000487C File Offset: 0x00002A7C
		public unsafe static SDL.SDL_Keycode SDL_GetKeyFromName(string name)
		{
			int num = SDL.Utf8Size(name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GetKeyFromName(SDL.Utf8Encode(name, ptr, num));
		}

		// Token: 0x06000783 RID: 1923
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_StartTextInput();

		// Token: 0x06000784 RID: 1924
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsTextInputActive();

		// Token: 0x06000785 RID: 1925
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_StopTextInput();

		// Token: 0x06000786 RID: 1926
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearComposition();

		// Token: 0x06000787 RID: 1927
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsTextInputShown();

		// Token: 0x06000788 RID: 1928
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTextInputRect(ref SDL.SDL_Rect rect);

		// Token: 0x06000789 RID: 1929
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasScreenKeyboardSupport();

		// Token: 0x0600078A RID: 1930
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsScreenKeyboardShown(IntPtr window);

		// Token: 0x0600078B RID: 1931
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMouseFocus();

		// Token: 0x0600078C RID: 1932
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetMouseState(out int x, out int y);

		// Token: 0x0600078D RID: 1933
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetMouseState(IntPtr x, out int y);

		// Token: 0x0600078E RID: 1934
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetMouseState(out int x, IntPtr y);

		// Token: 0x0600078F RID: 1935
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetMouseState(IntPtr x, IntPtr y);

		// Token: 0x06000790 RID: 1936
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGlobalMouseState(out int x, out int y);

		// Token: 0x06000791 RID: 1937
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGlobalMouseState(IntPtr x, out int y);

		// Token: 0x06000792 RID: 1938
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGlobalMouseState(out int x, IntPtr y);

		// Token: 0x06000793 RID: 1939
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGlobalMouseState(IntPtr x, IntPtr y);

		// Token: 0x06000794 RID: 1940
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetRelativeMouseState(out int x, out int y);

		// Token: 0x06000795 RID: 1941
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WarpMouseInWindow(IntPtr window, int x, int y);

		// Token: 0x06000796 RID: 1942
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_WarpMouseGlobal(int x, int y);

		// Token: 0x06000797 RID: 1943
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetRelativeMouseMode(SDL.SDL_bool enabled);

		// Token: 0x06000798 RID: 1944
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_CaptureMouse(SDL.SDL_bool enabled);

		// Token: 0x06000799 RID: 1945
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GetRelativeMouseMode();

		// Token: 0x0600079A RID: 1946
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateCursor(IntPtr data, IntPtr mask, int w, int h, int hot_x, int hot_y);

		// Token: 0x0600079B RID: 1947
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

		// Token: 0x0600079C RID: 1948
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSystemCursor(SDL.SDL_SystemCursor id);

		// Token: 0x0600079D RID: 1949
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetCursor(IntPtr cursor);

		// Token: 0x0600079E RID: 1950
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCursor();

		// Token: 0x0600079F RID: 1951
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeCursor(IntPtr cursor);

		// Token: 0x060007A0 RID: 1952
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_ShowCursor(int toggle);

		// Token: 0x060007A1 RID: 1953 RVA: 0x000048A2 File Offset: 0x00002AA2
		public static uint SDL_BUTTON(uint X)
		{
			return 1U << (int)(X - 1U);
		}

		// Token: 0x060007A2 RID: 1954
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumTouchDevices();

		// Token: 0x060007A3 RID: 1955
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_GetTouchDevice(int index);

		// Token: 0x060007A4 RID: 1956
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumTouchFingers(long touchID);

		// Token: 0x060007A5 RID: 1957
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTouchFinger(long touchID, int index);

		// Token: 0x060007A6 RID: 1958
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_TouchDeviceType SDL_GetTouchDeviceType(long touchID);

		// Token: 0x060007A7 RID: 1959
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetTouchName")]
		private static extern IntPtr INTERNAL_SDL_GetTouchName(int index);

		// Token: 0x060007A8 RID: 1960 RVA: 0x000048AC File Offset: 0x00002AAC
		public static string SDL_GetTouchName(int index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetTouchName(index), false);
		}

		// Token: 0x060007A9 RID: 1961
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickRumble(IntPtr joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		// Token: 0x060007AA RID: 1962
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickRumbleTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

		// Token: 0x060007AB RID: 1963
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickClose(IntPtr joystick);

		// Token: 0x060007AC RID: 1964
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickEventState(int state);

		// Token: 0x060007AD RID: 1965
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_JoystickGetAxis(IntPtr joystick, int axis);

		// Token: 0x060007AE RID: 1966
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickGetAxisInitialState(IntPtr joystick, int axis, out short state);

		// Token: 0x060007AF RID: 1967
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickGetBall(IntPtr joystick, int ball, out int dx, out int dy);

		// Token: 0x060007B0 RID: 1968
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickGetButton(IntPtr joystick, int button);

		// Token: 0x060007B1 RID: 1969
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_JoystickGetHat(IntPtr joystick, int hat);

		// Token: 0x060007B2 RID: 1970
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickName")]
		private static extern IntPtr INTERNAL_SDL_JoystickName(IntPtr joystick);

		// Token: 0x060007B3 RID: 1971 RVA: 0x000048BA File Offset: 0x00002ABA
		public static string SDL_JoystickName(IntPtr joystick)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_JoystickName(joystick), false);
		}

		// Token: 0x060007B4 RID: 1972
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickNameForIndex")]
		private static extern IntPtr INTERNAL_SDL_JoystickNameForIndex(int device_index);

		// Token: 0x060007B5 RID: 1973 RVA: 0x000048C8 File Offset: 0x00002AC8
		public static string SDL_JoystickNameForIndex(int device_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_JoystickNameForIndex(device_index), false);
		}

		// Token: 0x060007B6 RID: 1974
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumAxes(IntPtr joystick);

		// Token: 0x060007B7 RID: 1975
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumBalls(IntPtr joystick);

		// Token: 0x060007B8 RID: 1976
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumButtons(IntPtr joystick);

		// Token: 0x060007B9 RID: 1977
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickNumHats(IntPtr joystick);

		// Token: 0x060007BA RID: 1978
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_JoystickOpen(int device_index);

		// Token: 0x060007BB RID: 1979
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickUpdate();

		// Token: 0x060007BC RID: 1980
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_NumJoysticks();

		// Token: 0x060007BD RID: 1981
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern Guid SDL_JoystickGetDeviceGUID(int device_index);

		// Token: 0x060007BE RID: 1982
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern Guid SDL_JoystickGetGUID(IntPtr joystick);

		// Token: 0x060007BF RID: 1983
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickGetGUIDString(Guid guid, byte[] pszGUID, int cbGUID);

		// Token: 0x060007C0 RID: 1984
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetGUIDFromString")]
		private unsafe static extern Guid INTERNAL_SDL_JoystickGetGUIDFromString(byte* pchGUID);

		// Token: 0x060007C1 RID: 1985 RVA: 0x000048D8 File Offset: 0x00002AD8
		public unsafe static Guid SDL_JoystickGetGUIDFromString(string pchGuid)
		{
			int num = SDL.Utf8Size(pchGuid);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_JoystickGetGUIDFromString(SDL.Utf8Encode(pchGuid, ptr, num));
		}

		// Token: 0x060007C2 RID: 1986
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetDeviceVendor(int device_index);

		// Token: 0x060007C3 RID: 1987
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetDeviceProduct(int device_index);

		// Token: 0x060007C4 RID: 1988
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetDeviceProductVersion(int device_index);

		// Token: 0x060007C5 RID: 1989
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickType SDL_JoystickGetDeviceType(int device_index);

		// Token: 0x060007C6 RID: 1990
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickGetDeviceInstanceID(int device_index);

		// Token: 0x060007C7 RID: 1991
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetVendor(IntPtr joystick);

		// Token: 0x060007C8 RID: 1992
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetProduct(IntPtr joystick);

		// Token: 0x060007C9 RID: 1993
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_JoystickGetProductVersion(IntPtr joystick);

		// Token: 0x060007CA RID: 1994
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_JoystickGetSerial")]
		private static extern IntPtr INTERNAL_SDL_JoystickGetSerial(IntPtr joystick);

		// Token: 0x060007CB RID: 1995 RVA: 0x000048FE File Offset: 0x00002AFE
		public static string SDL_JoystickGetSerial(IntPtr joystick)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_JoystickGetSerial(joystick), false);
		}

		// Token: 0x060007CC RID: 1996
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickType SDL_JoystickGetType(IntPtr joystick);

		// Token: 0x060007CD RID: 1997
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickGetAttached(IntPtr joystick);

		// Token: 0x060007CE RID: 1998
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickInstanceID(IntPtr joystick);

		// Token: 0x060007CF RID: 1999
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickPowerLevel SDL_JoystickCurrentPowerLevel(IntPtr joystick);

		// Token: 0x060007D0 RID: 2000
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_JoystickFromInstanceID(int instance_id);

		// Token: 0x060007D1 RID: 2001
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockJoysticks();

		// Token: 0x060007D2 RID: 2002
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockJoysticks();

		// Token: 0x060007D3 RID: 2003
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_JoystickFromPlayerIndex(int player_index);

		// Token: 0x060007D4 RID: 2004
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_JoystickSetPlayerIndex(IntPtr joystick, int player_index);

		// Token: 0x060007D5 RID: 2005
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickAttachVirtual(int type, int naxes, int nbuttons, int nhats);

		// Token: 0x060007D6 RID: 2006
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickDetachVirtual(int device_index);

		// Token: 0x060007D7 RID: 2007
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickIsVirtual(int device_index);

		// Token: 0x060007D8 RID: 2008
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickSetVirtualAxis(IntPtr joystick, int axis, short value);

		// Token: 0x060007D9 RID: 2009
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickSetVirtualButton(IntPtr joystick, int button, byte value);

		// Token: 0x060007DA RID: 2010
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickSetVirtualHat(IntPtr joystick, int hat, byte value);

		// Token: 0x060007DB RID: 2011
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickHasLED(IntPtr joystick);

		// Token: 0x060007DC RID: 2012
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickHasRumble(IntPtr joystick);

		// Token: 0x060007DD RID: 2013
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_JoystickHasRumbleTriggers(IntPtr joystick);

		// Token: 0x060007DE RID: 2014
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickSetLED(IntPtr joystick, byte red, byte green, byte blue);

		// Token: 0x060007DF RID: 2015
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickSendEffect(IntPtr joystick, IntPtr data, int size);

		// Token: 0x060007E0 RID: 2016
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerAddMapping")]
		private unsafe static extern int INTERNAL_SDL_GameControllerAddMapping(byte* mappingString);

		// Token: 0x060007E1 RID: 2017 RVA: 0x0000490C File Offset: 0x00002B0C
		public unsafe static int SDL_GameControllerAddMapping(string mappingString)
		{
			byte* ptr = SDL.Utf8EncodeHeap(mappingString);
			int num = SDL.INTERNAL_SDL_GameControllerAddMapping(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x060007E2 RID: 2018
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerNumMappings();

		// Token: 0x060007E3 RID: 2019
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerMappingForIndex")]
		private static extern IntPtr INTERNAL_SDL_GameControllerMappingForIndex(int mapping_index);

		// Token: 0x060007E4 RID: 2020 RVA: 0x00004931 File Offset: 0x00002B31
		public static string SDL_GameControllerMappingForIndex(int mapping_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerMappingForIndex(mapping_index), true);
		}

		// Token: 0x060007E5 RID: 2021
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
		private static extern int INTERNAL_SDL_GameControllerAddMappingsFromRW(IntPtr rw, int freerw);

		// Token: 0x060007E6 RID: 2022 RVA: 0x0000493F File Offset: 0x00002B3F
		public static int SDL_GameControllerAddMappingsFromFile(string file)
		{
			return SDL.INTERNAL_SDL_GameControllerAddMappingsFromRW(SDL.SDL_RWFromFile(file, "rb"), 1);
		}

		// Token: 0x060007E7 RID: 2023
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerMappingForGUID")]
		private static extern IntPtr INTERNAL_SDL_GameControllerMappingForGUID(Guid guid);

		// Token: 0x060007E8 RID: 2024 RVA: 0x00004952 File Offset: 0x00002B52
		public static string SDL_GameControllerMappingForGUID(Guid guid)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerMappingForGUID(guid), true);
		}

		// Token: 0x060007E9 RID: 2025
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerMapping")]
		private static extern IntPtr INTERNAL_SDL_GameControllerMapping(IntPtr gamecontroller);

		// Token: 0x060007EA RID: 2026 RVA: 0x00004960 File Offset: 0x00002B60
		public static string SDL_GameControllerMapping(IntPtr gamecontroller)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerMapping(gamecontroller), true);
		}

		// Token: 0x060007EB RID: 2027
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsGameController(int joystick_index);

		// Token: 0x060007EC RID: 2028
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerNameForIndex")]
		private static extern IntPtr INTERNAL_SDL_GameControllerNameForIndex(int joystick_index);

		// Token: 0x060007ED RID: 2029 RVA: 0x0000496E File Offset: 0x00002B6E
		public static string SDL_GameControllerNameForIndex(int joystick_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerNameForIndex(joystick_index), false);
		}

		// Token: 0x060007EE RID: 2030
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerMappingForDeviceIndex")]
		private static extern IntPtr INTERNAL_SDL_GameControllerMappingForDeviceIndex(int joystick_index);

		// Token: 0x060007EF RID: 2031 RVA: 0x0000497C File Offset: 0x00002B7C
		public static string SDL_GameControllerMappingForDeviceIndex(int joystick_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerMappingForDeviceIndex(joystick_index), true);
		}

		// Token: 0x060007F0 RID: 2032
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerOpen(int joystick_index);

		// Token: 0x060007F1 RID: 2033
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerName")]
		private static extern IntPtr INTERNAL_SDL_GameControllerName(IntPtr gamecontroller);

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000498A File Offset: 0x00002B8A
		public static string SDL_GameControllerName(IntPtr gamecontroller)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerName(gamecontroller), false);
		}

		// Token: 0x060007F3 RID: 2035
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GameControllerGetVendor(IntPtr gamecontroller);

		// Token: 0x060007F4 RID: 2036
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GameControllerGetProduct(IntPtr gamecontroller);

		// Token: 0x060007F5 RID: 2037
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GameControllerGetProductVersion(IntPtr gamecontroller);

		// Token: 0x060007F6 RID: 2038
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetSerial")]
		private static extern IntPtr INTERNAL_SDL_GameControllerGetSerial(IntPtr gamecontroller);

		// Token: 0x060007F7 RID: 2039 RVA: 0x00004998 File Offset: 0x00002B98
		public static string SDL_GameControllerGetSerial(IntPtr gamecontroller)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerGetSerial(gamecontroller), false);
		}

		// Token: 0x060007F8 RID: 2040
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerGetAttached(IntPtr gamecontroller);

		// Token: 0x060007F9 RID: 2041
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerGetJoystick(IntPtr gamecontroller);

		// Token: 0x060007FA RID: 2042
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerEventState(int state);

		// Token: 0x060007FB RID: 2043
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GameControllerUpdate();

		// Token: 0x060007FC RID: 2044
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetAxisFromString")]
		private unsafe static extern SDL.SDL_GameControllerAxis INTERNAL_SDL_GameControllerGetAxisFromString(byte* pchString);

		// Token: 0x060007FD RID: 2045 RVA: 0x000049A8 File Offset: 0x00002BA8
		public unsafe static SDL.SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(string pchString)
		{
			int num = SDL.Utf8Size(pchString);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GameControllerGetAxisFromString(SDL.Utf8Encode(pchString, ptr, num));
		}

		// Token: 0x060007FE RID: 2046
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetStringForAxis")]
		private static extern IntPtr INTERNAL_SDL_GameControllerGetStringForAxis(SDL.SDL_GameControllerAxis axis);

		// Token: 0x060007FF RID: 2047 RVA: 0x000049CE File Offset: 0x00002BCE
		public static string SDL_GameControllerGetStringForAxis(SDL.SDL_GameControllerAxis axis)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerGetStringForAxis(axis), false);
		}

		// Token: 0x06000800 RID: 2048
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetBindForAxis")]
		private static extern SDL.INTERNAL_SDL_GameControllerButtonBind INTERNAL_SDL_GameControllerGetBindForAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis);

		// Token: 0x06000801 RID: 2049 RVA: 0x000049DC File Offset: 0x00002BDC
		public static SDL.SDL_GameControllerButtonBind SDL_GameControllerGetBindForAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis)
		{
			SDL.INTERNAL_SDL_GameControllerButtonBind internal_SDL_GameControllerButtonBind = SDL.INTERNAL_SDL_GameControllerGetBindForAxis(gamecontroller, axis);
			SDL.SDL_GameControllerButtonBind sdl_GameControllerButtonBind = default(SDL.SDL_GameControllerButtonBind);
			sdl_GameControllerButtonBind.bindType = (SDL.SDL_GameControllerBindType)internal_SDL_GameControllerButtonBind.bindType;
			sdl_GameControllerButtonBind.value.hat.hat = internal_SDL_GameControllerButtonBind.unionVal0;
			sdl_GameControllerButtonBind.value.hat.hat_mask = internal_SDL_GameControllerButtonBind.unionVal1;
			return sdl_GameControllerButtonBind;
		}

		// Token: 0x06000802 RID: 2050
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GameControllerGetAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis);

		// Token: 0x06000803 RID: 2051
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetButtonFromString")]
		private unsafe static extern SDL.SDL_GameControllerButton INTERNAL_SDL_GameControllerGetButtonFromString(byte* pchString);

		// Token: 0x06000804 RID: 2052 RVA: 0x00004A38 File Offset: 0x00002C38
		public unsafe static SDL.SDL_GameControllerButton SDL_GameControllerGetButtonFromString(string pchString)
		{
			int num = SDL.Utf8Size(pchString);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_GameControllerGetButtonFromString(SDL.Utf8Encode(pchString, ptr, num));
		}

		// Token: 0x06000805 RID: 2053
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetStringForButton")]
		private static extern IntPtr INTERNAL_SDL_GameControllerGetStringForButton(SDL.SDL_GameControllerButton button);

		// Token: 0x06000806 RID: 2054 RVA: 0x00004A5E File Offset: 0x00002C5E
		public static string SDL_GameControllerGetStringForButton(SDL.SDL_GameControllerButton button)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerGetStringForButton(button), false);
		}

		// Token: 0x06000807 RID: 2055
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetBindForButton")]
		private static extern SDL.INTERNAL_SDL_GameControllerButtonBind INTERNAL_SDL_GameControllerGetBindForButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button);

		// Token: 0x06000808 RID: 2056 RVA: 0x00004A6C File Offset: 0x00002C6C
		public static SDL.SDL_GameControllerButtonBind SDL_GameControllerGetBindForButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button)
		{
			SDL.INTERNAL_SDL_GameControllerButtonBind internal_SDL_GameControllerButtonBind = SDL.INTERNAL_SDL_GameControllerGetBindForButton(gamecontroller, button);
			SDL.SDL_GameControllerButtonBind sdl_GameControllerButtonBind = default(SDL.SDL_GameControllerButtonBind);
			sdl_GameControllerButtonBind.bindType = (SDL.SDL_GameControllerBindType)internal_SDL_GameControllerButtonBind.bindType;
			sdl_GameControllerButtonBind.value.hat.hat = internal_SDL_GameControllerButtonBind.unionVal0;
			sdl_GameControllerButtonBind.value.hat.hat_mask = internal_SDL_GameControllerButtonBind.unionVal1;
			return sdl_GameControllerButtonBind;
		}

		// Token: 0x06000809 RID: 2057
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GameControllerGetButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button);

		// Token: 0x0600080A RID: 2058
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerRumble(IntPtr gamecontroller, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		// Token: 0x0600080B RID: 2059
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerRumbleTriggers(IntPtr gamecontroller, ushort left_rumble, ushort right_rumble, uint duration_ms);

		// Token: 0x0600080C RID: 2060
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GameControllerClose(IntPtr gamecontroller);

		// Token: 0x0600080D RID: 2061
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetAppleSFSymbolsNameForButton")]
		private static extern IntPtr INTERNAL_SDL_GameControllerGetAppleSFSymbolsNameForButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button);

		// Token: 0x0600080E RID: 2062 RVA: 0x00004AC5 File Offset: 0x00002CC5
		public static string SDL_GameControllerGetAppleSFSymbolsNameForButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerGetAppleSFSymbolsNameForButton(gamecontroller, button), false);
		}

		// Token: 0x0600080F RID: 2063
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GameControllerGetAppleSFSymbolsNameForAxis")]
		private static extern IntPtr INTERNAL_SDL_GameControllerGetAppleSFSymbolsNameForAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis);

		// Token: 0x06000810 RID: 2064 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static string SDL_GameControllerGetAppleSFSymbolsNameForAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GameControllerGetAppleSFSymbolsNameForAxis(gamecontroller, axis), false);
		}

		// Token: 0x06000811 RID: 2065
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerFromInstanceID(int joyid);

		// Token: 0x06000812 RID: 2066
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GameControllerType SDL_GameControllerTypeForIndex(int joystick_index);

		// Token: 0x06000813 RID: 2067
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GameControllerType SDL_GameControllerGetType(IntPtr gamecontroller);

		// Token: 0x06000814 RID: 2068
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GameControllerFromPlayerIndex(int player_index);

		// Token: 0x06000815 RID: 2069
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GameControllerSetPlayerIndex(IntPtr gamecontroller, int player_index);

		// Token: 0x06000816 RID: 2070
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasLED(IntPtr gamecontroller);

		// Token: 0x06000817 RID: 2071
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasRumble(IntPtr gamecontroller);

		// Token: 0x06000818 RID: 2072
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasRumbleTriggers(IntPtr gamecontroller);

		// Token: 0x06000819 RID: 2073
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerSetLED(IntPtr gamecontroller, byte red, byte green, byte blue);

		// Token: 0x0600081A RID: 2074
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasAxis(IntPtr gamecontroller, SDL.SDL_GameControllerAxis axis);

		// Token: 0x0600081B RID: 2075
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasButton(IntPtr gamecontroller, SDL.SDL_GameControllerButton button);

		// Token: 0x0600081C RID: 2076
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerGetNumTouchpads(IntPtr gamecontroller);

		// Token: 0x0600081D RID: 2077
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerGetNumTouchpadFingers(IntPtr gamecontroller, int touchpad);

		// Token: 0x0600081E RID: 2078
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerGetTouchpadFinger(IntPtr gamecontroller, int touchpad, int finger, out byte state, out float x, out float y, out float pressure);

		// Token: 0x0600081F RID: 2079
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerHasSensor(IntPtr gamecontroller, SDL.SDL_SensorType type);

		// Token: 0x06000820 RID: 2080
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerSetSensorEnabled(IntPtr gamecontroller, SDL.SDL_SensorType type, SDL.SDL_bool enabled);

		// Token: 0x06000821 RID: 2081
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GameControllerIsSensorEnabled(IntPtr gamecontroller, SDL.SDL_SensorType type);

		// Token: 0x06000822 RID: 2082
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerGetSensorData(IntPtr gamecontroller, SDL.SDL_SensorType type, IntPtr data, int num_values);

		// Token: 0x06000823 RID: 2083
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerGetSensorData(IntPtr gamecontroller, SDL.SDL_SensorType type, [In] float[] data, int num_values);

		// Token: 0x06000824 RID: 2084
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GameControllerGetSensorDataRate(IntPtr gamecontroller, SDL.SDL_SensorType type);

		// Token: 0x06000825 RID: 2085
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GameControllerSendEffect(IntPtr gamecontroller, IntPtr data, int size);

		// Token: 0x06000826 RID: 2086
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HapticClose(IntPtr haptic);

		// Token: 0x06000827 RID: 2087
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_HapticDestroyEffect(IntPtr haptic, int effect);

		// Token: 0x06000828 RID: 2088
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticEffectSupported(IntPtr haptic, ref SDL.SDL_HapticEffect effect);

		// Token: 0x06000829 RID: 2089
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticGetEffectStatus(IntPtr haptic, int effect);

		// Token: 0x0600082A RID: 2090
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticIndex(IntPtr haptic);

		// Token: 0x0600082B RID: 2091
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HapticName")]
		private static extern IntPtr INTERNAL_SDL_HapticName(int device_index);

		// Token: 0x0600082C RID: 2092 RVA: 0x00004AE3 File Offset: 0x00002CE3
		public static string SDL_HapticName(int device_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_HapticName(device_index), false);
		}

		// Token: 0x0600082D RID: 2093
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNewEffect(IntPtr haptic, ref SDL.SDL_HapticEffect effect);

		// Token: 0x0600082E RID: 2094
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumAxes(IntPtr haptic);

		// Token: 0x0600082F RID: 2095
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumEffects(IntPtr haptic);

		// Token: 0x06000830 RID: 2096
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticNumEffectsPlaying(IntPtr haptic);

		// Token: 0x06000831 RID: 2097
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpen(int device_index);

		// Token: 0x06000832 RID: 2098
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticOpened(int device_index);

		// Token: 0x06000833 RID: 2099
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpenFromJoystick(IntPtr joystick);

		// Token: 0x06000834 RID: 2100
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_HapticOpenFromMouse();

		// Token: 0x06000835 RID: 2101
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticPause(IntPtr haptic);

		// Token: 0x06000836 RID: 2102
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_HapticQuery(IntPtr haptic);

		// Token: 0x06000837 RID: 2103
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleInit(IntPtr haptic);

		// Token: 0x06000838 RID: 2104
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumblePlay(IntPtr haptic, float strength, uint length);

		// Token: 0x06000839 RID: 2105
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleStop(IntPtr haptic);

		// Token: 0x0600083A RID: 2106
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRumbleSupported(IntPtr haptic);

		// Token: 0x0600083B RID: 2107
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticRunEffect(IntPtr haptic, int effect, uint iterations);

		// Token: 0x0600083C RID: 2108
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticSetAutocenter(IntPtr haptic, int autocenter);

		// Token: 0x0600083D RID: 2109
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticSetGain(IntPtr haptic, int gain);

		// Token: 0x0600083E RID: 2110
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticStopAll(IntPtr haptic);

		// Token: 0x0600083F RID: 2111
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticStopEffect(IntPtr haptic, int effect);

		// Token: 0x06000840 RID: 2112
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticUnpause(IntPtr haptic);

		// Token: 0x06000841 RID: 2113
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_HapticUpdateEffect(IntPtr haptic, int effect, ref SDL.SDL_HapticEffect data);

		// Token: 0x06000842 RID: 2114
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_JoystickIsHaptic(IntPtr joystick);

		// Token: 0x06000843 RID: 2115
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_MouseIsHaptic();

		// Token: 0x06000844 RID: 2116
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_NumHaptics();

		// Token: 0x06000845 RID: 2117
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_NumSensors();

		// Token: 0x06000846 RID: 2118
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SensorGetDeviceName")]
		private static extern IntPtr INTERNAL_SDL_SensorGetDeviceName(int device_index);

		// Token: 0x06000847 RID: 2119 RVA: 0x00004AF1 File Offset: 0x00002CF1
		public static string SDL_SensorGetDeviceName(int device_index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_SensorGetDeviceName(device_index), false);
		}

		// Token: 0x06000848 RID: 2120
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_SensorType SDL_SensorGetDeviceType(int device_index);

		// Token: 0x06000849 RID: 2121
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SensorGetDeviceNonPortableType(int device_index);

		// Token: 0x0600084A RID: 2122
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SensorGetDeviceInstanceID(int device_index);

		// Token: 0x0600084B RID: 2123
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SensorOpen(int device_index);

		// Token: 0x0600084C RID: 2124
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SensorFromInstanceID(int instance_id);

		// Token: 0x0600084D RID: 2125
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SensorGetName")]
		private static extern IntPtr INTERNAL_SDL_SensorGetName(IntPtr sensor);

		// Token: 0x0600084E RID: 2126 RVA: 0x00004AFF File Offset: 0x00002CFF
		public static string SDL_SensorGetName(IntPtr sensor)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_SensorGetName(sensor), false);
		}

		// Token: 0x0600084F RID: 2127
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_SensorType SDL_SensorGetType(IntPtr sensor);

		// Token: 0x06000850 RID: 2128
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SensorGetNonPortableType(IntPtr sensor);

		// Token: 0x06000851 RID: 2129
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SensorGetInstanceID(IntPtr sensor);

		// Token: 0x06000852 RID: 2130
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SensorGetData(IntPtr sensor, float[] data, int num_values);

		// Token: 0x06000853 RID: 2131
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SensorClose(IntPtr sensor);

		// Token: 0x06000854 RID: 2132
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SensorUpdate();

		// Token: 0x06000855 RID: 2133
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockSensors();

		// Token: 0x06000856 RID: 2134
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSensors();

		// Token: 0x06000857 RID: 2135 RVA: 0x00004B0D File Offset: 0x00002D0D
		public static ushort SDL_AUDIO_BITSIZE(ushort x)
		{
			return x & 255;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00004B17 File Offset: 0x00002D17
		public static bool SDL_AUDIO_ISFLOAT(ushort x)
		{
			return (x & 256) > 0;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00004B23 File Offset: 0x00002D23
		public static bool SDL_AUDIO_ISBIGENDIAN(ushort x)
		{
			return (x & 4096) > 0;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00004B2F File Offset: 0x00002D2F
		public static bool SDL_AUDIO_ISSIGNED(ushort x)
		{
			return (x & 32768) > 0;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00004B3B File Offset: 0x00002D3B
		public static bool SDL_AUDIO_ISINT(ushort x)
		{
			return (x & 256) == 0;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00004B47 File Offset: 0x00002D47
		public static bool SDL_AUDIO_ISLITTLEENDIAN(ushort x)
		{
			return (x & 4096) == 0;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00004B53 File Offset: 0x00002D53
		public static bool SDL_AUDIO_ISUNSIGNED(ushort x)
		{
			return (x & 32768) == 0;
		}

		// Token: 0x0600085E RID: 2142
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AudioInit")]
		private unsafe static extern int INTERNAL_SDL_AudioInit(byte* driver_name);

		// Token: 0x0600085F RID: 2143 RVA: 0x00004B60 File Offset: 0x00002D60
		public unsafe static int SDL_AudioInit(string driver_name)
		{
			int num = SDL.Utf8Size(driver_name);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_AudioInit(SDL.Utf8Encode(driver_name, ptr, num));
		}

		// Token: 0x06000860 RID: 2144
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AudioQuit();

		// Token: 0x06000861 RID: 2145
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudio();

		// Token: 0x06000862 RID: 2146
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudioDevice(uint dev);

		// Token: 0x06000863 RID: 2147
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeWAV(IntPtr audio_buf);

		// Token: 0x06000864 RID: 2148
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAudioDeviceName")]
		private static extern IntPtr INTERNAL_SDL_GetAudioDeviceName(int index, int iscapture);

		// Token: 0x06000865 RID: 2149 RVA: 0x00004B86 File Offset: 0x00002D86
		public static string SDL_GetAudioDeviceName(int index, int iscapture)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetAudioDeviceName(index, iscapture), false);
		}

		// Token: 0x06000866 RID: 2150
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_AudioStatus SDL_GetAudioDeviceStatus(uint dev);

		// Token: 0x06000867 RID: 2151
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAudioDriver")]
		private static extern IntPtr INTERNAL_SDL_GetAudioDriver(int index);

		// Token: 0x06000868 RID: 2152 RVA: 0x00004B95 File Offset: 0x00002D95
		public static string SDL_GetAudioDriver(int index)
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetAudioDriver(index), false);
		}

		// Token: 0x06000869 RID: 2153
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_AudioStatus SDL_GetAudioStatus();

		// Token: 0x0600086A RID: 2154
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentAudioDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentAudioDriver();

		// Token: 0x0600086B RID: 2155 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public static string SDL_GetCurrentAudioDriver()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetCurrentAudioDriver(), false);
		}

		// Token: 0x0600086C RID: 2156
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumAudioDevices(int iscapture);

		// Token: 0x0600086D RID: 2157
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumAudioDrivers();

		// Token: 0x0600086E RID: 2158
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadWAV_RW(IntPtr src, int freesrc, out SDL.SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len);

		// Token: 0x0600086F RID: 2159 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public static IntPtr SDL_LoadWAV(string file, out SDL.SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len)
		{
			return SDL.SDL_LoadWAV_RW(SDL.SDL_RWFromFile(file, "rb"), 1, out spec, out audio_buf, out audio_len);
		}

		// Token: 0x06000870 RID: 2160
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockAudio();

		// Token: 0x06000871 RID: 2161
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockAudioDevice(uint dev);

		// Token: 0x06000872 RID: 2162
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MixAudio([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] [Out] byte[] dst, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] [In] byte[] src, uint len, int volume);

		// Token: 0x06000873 RID: 2163
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MixAudioFormat(IntPtr dst, IntPtr src, ushort format, uint len, int volume);

		// Token: 0x06000874 RID: 2164
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MixAudioFormat([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)] [Out] byte[] dst, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)] [In] byte[] src, ushort format, uint len, int volume);

		// Token: 0x06000875 RID: 2165
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_OpenAudio(ref SDL.SDL_AudioSpec desired, out SDL.SDL_AudioSpec obtained);

		// Token: 0x06000876 RID: 2166
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_OpenAudio(ref SDL.SDL_AudioSpec desired, IntPtr obtained);

		// Token: 0x06000877 RID: 2167
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_OpenAudioDevice(IntPtr device, int iscapture, ref SDL.SDL_AudioSpec desired, out SDL.SDL_AudioSpec obtained, int allowed_changes);

		// Token: 0x06000878 RID: 2168
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenAudioDevice")]
		private unsafe static extern uint INTERNAL_SDL_OpenAudioDevice(byte* device, int iscapture, ref SDL.SDL_AudioSpec desired, out SDL.SDL_AudioSpec obtained, int allowed_changes);

		// Token: 0x06000879 RID: 2169 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public unsafe static uint SDL_OpenAudioDevice(string device, int iscapture, ref SDL.SDL_AudioSpec desired, out SDL.SDL_AudioSpec obtained, int allowed_changes)
		{
			int num = SDL.Utf8Size(device);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			return SDL.INTERNAL_SDL_OpenAudioDevice(SDL.Utf8Encode(device, ptr, num), iscapture, ref desired, out obtained, allowed_changes);
		}

		// Token: 0x0600087A RID: 2170
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PauseAudio(int pause_on);

		// Token: 0x0600087B RID: 2171
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PauseAudioDevice(uint dev, int pause_on);

		// Token: 0x0600087C RID: 2172
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockAudio();

		// Token: 0x0600087D RID: 2173
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockAudioDevice(uint dev);

		// Token: 0x0600087E RID: 2174
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_QueueAudio(uint dev, IntPtr data, uint len);

		// Token: 0x0600087F RID: 2175
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_DequeueAudio(uint dev, IntPtr data, uint len);

		// Token: 0x06000880 RID: 2176
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetQueuedAudioSize(uint dev);

		// Token: 0x06000881 RID: 2177
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClearQueuedAudio(uint dev);

		// Token: 0x06000882 RID: 2178
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_NewAudioStream(ushort src_format, byte src_channels, int src_rate, ushort dst_format, byte dst_channels, int dst_rate);

		// Token: 0x06000883 RID: 2179
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AudioStreamPut(IntPtr stream, IntPtr buf, int len);

		// Token: 0x06000884 RID: 2180
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AudioStreamGet(IntPtr stream, IntPtr buf, int len);

		// Token: 0x06000885 RID: 2181
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AudioStreamAvailable(IntPtr stream);

		// Token: 0x06000886 RID: 2182
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AudioStreamClear(IntPtr stream);

		// Token: 0x06000887 RID: 2183
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FreeAudioStream(IntPtr stream);

		// Token: 0x06000888 RID: 2184
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioDeviceSpec(int index, int iscapture, out SDL.SDL_AudioSpec spec);

		// Token: 0x06000889 RID: 2185 RVA: 0x00004BF3 File Offset: 0x00002DF3
		public static bool SDL_TICKS_PASSED(uint A, uint B)
		{
			return B - A <= 0U;
		}

		// Token: 0x0600088A RID: 2186
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Delay(uint ms);

		// Token: 0x0600088B RID: 2187
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetTicks();

		// Token: 0x0600088C RID: 2188
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetTicks64();

		// Token: 0x0600088D RID: 2189
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetPerformanceCounter();

		// Token: 0x0600088E RID: 2190
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetPerformanceFrequency();

		// Token: 0x0600088F RID: 2191
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AddTimer(uint interval, SDL.SDL_TimerCallback callback, IntPtr param);

		// Token: 0x06000890 RID: 2192
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_RemoveTimer(int id);

		// Token: 0x06000891 RID: 2193
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetWindowsMessageHook(SDL.SDL_WindowsMessageHook callback, IntPtr userdata);

		// Token: 0x06000892 RID: 2194
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderGetD3D9Device(IntPtr renderer);

		// Token: 0x06000893 RID: 2195
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderGetD3D11Device(IntPtr renderer);

		// Token: 0x06000894 RID: 2196
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_iPhoneSetAnimationCallback(IntPtr window, int interval, SDL.SDL_iPhoneAnimationCallback callback, IntPtr callbackParam);

		// Token: 0x06000895 RID: 2197
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_iPhoneSetEventPump(SDL.SDL_bool enabled);

		// Token: 0x06000896 RID: 2198
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AndroidGetJNIEnv();

		// Token: 0x06000897 RID: 2199
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AndroidGetActivity();

		// Token: 0x06000898 RID: 2200
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsAndroidTV();

		// Token: 0x06000899 RID: 2201
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsChromebook();

		// Token: 0x0600089A RID: 2202
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsDeXMode();

		// Token: 0x0600089B RID: 2203
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_AndroidBackButton();

		// Token: 0x0600089C RID: 2204
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AndroidGetInternalStoragePath")]
		private static extern IntPtr INTERNAL_SDL_AndroidGetInternalStoragePath();

		// Token: 0x0600089D RID: 2205 RVA: 0x00004BFE File Offset: 0x00002DFE
		public static string SDL_AndroidGetInternalStoragePath()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_AndroidGetInternalStoragePath(), false);
		}

		// Token: 0x0600089E RID: 2206
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AndroidGetExternalStorageState();

		// Token: 0x0600089F RID: 2207
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AndroidGetExternalStoragePath")]
		private static extern IntPtr INTERNAL_SDL_AndroidGetExternalStoragePath();

		// Token: 0x060008A0 RID: 2208 RVA: 0x00004C0B File Offset: 0x00002E0B
		public static string SDL_AndroidGetExternalStoragePath()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_AndroidGetExternalStoragePath(), false);
		}

		// Token: 0x060008A1 RID: 2209
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAndroidSDKVersion();

		// Token: 0x060008A2 RID: 2210
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AndroidRequestPermission")]
		private unsafe static extern SDL.SDL_bool INTERNAL_SDL_AndroidRequestPermission(byte* permission);

		// Token: 0x060008A3 RID: 2211 RVA: 0x00004C18 File Offset: 0x00002E18
		public unsafe static SDL.SDL_bool SDL_AndroidRequestPermission(string permission)
		{
			byte* ptr = SDL.Utf8EncodeHeap(permission);
			SDL.SDL_bool sdl_bool = SDL.INTERNAL_SDL_AndroidRequestPermission(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return sdl_bool;
		}

		// Token: 0x060008A4 RID: 2212
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AndroidShowToast")]
		private unsafe static extern int INTERNAL_SDL_AndroidShowToast(byte* message, int duration, int gravity, int xOffset, int yOffset);

		// Token: 0x060008A5 RID: 2213 RVA: 0x00004C40 File Offset: 0x00002E40
		public unsafe static int SDL_AndroidShowToast(string message, int duration, int gravity, int xOffset, int yOffset)
		{
			byte* ptr = SDL.Utf8EncodeHeap(message);
			int num = SDL.INTERNAL_SDL_AndroidShowToast(ptr, duration, gravity, xOffset, yOffset);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x060008A6 RID: 2214
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_WinRT_DeviceFamily SDL_WinRTGetDeviceFamily();

		// Token: 0x060008A7 RID: 2215
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_IsTablet();

		// Token: 0x060008A8 RID: 2216
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_GetWindowWMInfo(IntPtr window, ref SDL.SDL_SysWMinfo info);

		// Token: 0x060008A9 RID: 2217
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetBasePath")]
		private static extern IntPtr INTERNAL_SDL_GetBasePath();

		// Token: 0x060008AA RID: 2218 RVA: 0x00004C6A File Offset: 0x00002E6A
		public static string SDL_GetBasePath()
		{
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetBasePath(), true);
		}

		// Token: 0x060008AB RID: 2219
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPrefPath")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetPrefPath(byte* org, byte* app);

		// Token: 0x060008AC RID: 2220 RVA: 0x00004C78 File Offset: 0x00002E78
		public unsafe static string SDL_GetPrefPath(string org, string app)
		{
			int num = SDL.Utf8Size(org);
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = SDL.Utf8Size(app);
			byte* ptr2 = stackalloc byte[(UIntPtr)num2];
			return SDL.UTF8_ToManaged(SDL.INTERNAL_SDL_GetPrefPath(SDL.Utf8Encode(org, ptr, num), SDL.Utf8Encode(app, ptr2, num2)), true);
		}

		// Token: 0x060008AD RID: 2221
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PowerState SDL_GetPowerInfo(out int secs, out int pct);

		// Token: 0x060008AE RID: 2222
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCPUCount();

		// Token: 0x060008AF RID: 2223
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCPUCacheLineSize();

		// Token: 0x060008B0 RID: 2224
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasRDTSC();

		// Token: 0x060008B1 RID: 2225
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasAltiVec();

		// Token: 0x060008B2 RID: 2226
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasMMX();

		// Token: 0x060008B3 RID: 2227
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_Has3DNow();

		// Token: 0x060008B4 RID: 2228
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSSE();

		// Token: 0x060008B5 RID: 2229
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSSE2();

		// Token: 0x060008B6 RID: 2230
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSSE3();

		// Token: 0x060008B7 RID: 2231
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSSE41();

		// Token: 0x060008B8 RID: 2232
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasSSE42();

		// Token: 0x060008B9 RID: 2233
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasAVX();

		// Token: 0x060008BA RID: 2234
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasAVX2();

		// Token: 0x060008BB RID: 2235
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasAVX512F();

		// Token: 0x060008BC RID: 2236
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasNEON();

		// Token: 0x060008BD RID: 2237
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSystemRAM();

		// Token: 0x060008BE RID: 2238
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_SIMDGetAlignment();

		// Token: 0x060008BF RID: 2239
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SIMDAlloc(uint len);

		// Token: 0x060008C0 RID: 2240
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SIMDRealloc(IntPtr ptr, uint len);

		// Token: 0x060008C1 RID: 2241
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SIMDFree(IntPtr ptr);

		// Token: 0x060008C2 RID: 2242
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_bool SDL_HasARMSIMD();

		// Token: 0x060008C3 RID: 2243
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetPreferredLocales();

		// Token: 0x060008C4 RID: 2244
		[DllImport("SDL2", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenURL")]
		private unsafe static extern int INTERNAL_SDL_OpenURL(byte* url);

		// Token: 0x060008C5 RID: 2245 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public unsafe static int SDL_OpenURL(string url)
		{
			byte* ptr = SDL.Utf8EncodeHeap(url);
			int num = SDL.INTERNAL_SDL_OpenURL(ptr);
			Marshal.FreeHGlobal((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00004CE0 File Offset: 0x00002EE0
		// Note: this type is marked as 'beforefieldinit'.
		static SDL()
		{
		}

		// Token: 0x040002DE RID: 734
		private const string nativeLibName = "SDL2";

		// Token: 0x040002DF RID: 735
		public const int RW_SEEK_SET = 0;

		// Token: 0x040002E0 RID: 736
		public const int RW_SEEK_CUR = 1;

		// Token: 0x040002E1 RID: 737
		public const int RW_SEEK_END = 2;

		// Token: 0x040002E2 RID: 738
		public const uint SDL_RWOPS_UNKNOWN = 0U;

		// Token: 0x040002E3 RID: 739
		public const uint SDL_RWOPS_WINFILE = 1U;

		// Token: 0x040002E4 RID: 740
		public const uint SDL_RWOPS_STDFILE = 2U;

		// Token: 0x040002E5 RID: 741
		public const uint SDL_RWOPS_JNIFILE = 3U;

		// Token: 0x040002E6 RID: 742
		public const uint SDL_RWOPS_MEMORY = 4U;

		// Token: 0x040002E7 RID: 743
		public const uint SDL_RWOPS_MEMORY_RO = 5U;

		// Token: 0x040002E8 RID: 744
		public const uint SDL_INIT_TIMER = 1U;

		// Token: 0x040002E9 RID: 745
		public const uint SDL_INIT_AUDIO = 16U;

		// Token: 0x040002EA RID: 746
		public const uint SDL_INIT_VIDEO = 32U;

		// Token: 0x040002EB RID: 747
		public const uint SDL_INIT_JOYSTICK = 512U;

		// Token: 0x040002EC RID: 748
		public const uint SDL_INIT_HAPTIC = 4096U;

		// Token: 0x040002ED RID: 749
		public const uint SDL_INIT_GAMECONTROLLER = 8192U;

		// Token: 0x040002EE RID: 750
		public const uint SDL_INIT_EVENTS = 16384U;

		// Token: 0x040002EF RID: 751
		public const uint SDL_INIT_SENSOR = 32768U;

		// Token: 0x040002F0 RID: 752
		public const uint SDL_INIT_NOPARACHUTE = 1048576U;

		// Token: 0x040002F1 RID: 753
		public const uint SDL_INIT_EVERYTHING = 62001U;

		// Token: 0x040002F2 RID: 754
		public const string SDL_HINT_FRAMEBUFFER_ACCELERATION = "SDL_FRAMEBUFFER_ACCELERATION";

		// Token: 0x040002F3 RID: 755
		public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";

		// Token: 0x040002F4 RID: 756
		public const string SDL_HINT_RENDER_OPENGL_SHADERS = "SDL_RENDER_OPENGL_SHADERS";

		// Token: 0x040002F5 RID: 757
		public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE = "SDL_RENDER_DIRECT3D_THREADSAFE";

		// Token: 0x040002F6 RID: 758
		public const string SDL_HINT_RENDER_VSYNC = "SDL_RENDER_VSYNC";

		// Token: 0x040002F7 RID: 759
		public const string SDL_HINT_VIDEO_X11_XVIDMODE = "SDL_VIDEO_X11_XVIDMODE";

		// Token: 0x040002F8 RID: 760
		public const string SDL_HINT_VIDEO_X11_XINERAMA = "SDL_VIDEO_X11_XINERAMA";

		// Token: 0x040002F9 RID: 761
		public const string SDL_HINT_VIDEO_X11_XRANDR = "SDL_VIDEO_X11_XRANDR";

		// Token: 0x040002FA RID: 762
		public const string SDL_HINT_GRAB_KEYBOARD = "SDL_GRAB_KEYBOARD";

		// Token: 0x040002FB RID: 763
		public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";

		// Token: 0x040002FC RID: 764
		public const string SDL_HINT_IDLE_TIMER_DISABLED = "SDL_IOS_IDLE_TIMER_DISABLED";

		// Token: 0x040002FD RID: 765
		public const string SDL_HINT_ORIENTATIONS = "SDL_IOS_ORIENTATIONS";

		// Token: 0x040002FE RID: 766
		public const string SDL_HINT_XINPUT_ENABLED = "SDL_XINPUT_ENABLED";

		// Token: 0x040002FF RID: 767
		public const string SDL_HINT_GAMECONTROLLERCONFIG = "SDL_GAMECONTROLLERCONFIG";

		// Token: 0x04000300 RID: 768
		public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";

		// Token: 0x04000301 RID: 769
		public const string SDL_HINT_ALLOW_TOPMOST = "SDL_ALLOW_TOPMOST";

		// Token: 0x04000302 RID: 770
		public const string SDL_HINT_TIMER_RESOLUTION = "SDL_TIMER_RESOLUTION";

		// Token: 0x04000303 RID: 771
		public const string SDL_HINT_RENDER_SCALE_QUALITY = "SDL_RENDER_SCALE_QUALITY";

		// Token: 0x04000304 RID: 772
		public const string SDL_HINT_VIDEO_HIGHDPI_DISABLED = "SDL_VIDEO_HIGHDPI_DISABLED";

		// Token: 0x04000305 RID: 773
		public const string SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";

		// Token: 0x04000306 RID: 774
		public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER = "SDL_VIDEO_WIN_D3DCOMPILER";

		// Token: 0x04000307 RID: 775
		public const string SDL_HINT_MOUSE_RELATIVE_MODE_WARP = "SDL_MOUSE_RELATIVE_MODE_WARP";

		// Token: 0x04000308 RID: 776
		public const string SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT = "SDL_VIDEO_WINDOW_SHARE_PIXEL_FORMAT";

		// Token: 0x04000309 RID: 777
		public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER = "SDL_VIDEO_ALLOW_SCREENSAVER";

		// Token: 0x0400030A RID: 778
		public const string SDL_HINT_ACCELEROMETER_AS_JOYSTICK = "SDL_ACCELEROMETER_AS_JOYSTICK";

		// Token: 0x0400030B RID: 779
		public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";

		// Token: 0x0400030C RID: 780
		public const string SDL_HINT_WINRT_PRIVACY_POLICY_URL = "SDL_WINRT_PRIVACY_POLICY_URL";

		// Token: 0x0400030D RID: 781
		public const string SDL_HINT_WINRT_PRIVACY_POLICY_LABEL = "SDL_WINRT_PRIVACY_POLICY_LABEL";

		// Token: 0x0400030E RID: 782
		public const string SDL_HINT_WINRT_HANDLE_BACK_BUTTON = "SDL_WINRT_HANDLE_BACK_BUTTON";

		// Token: 0x0400030F RID: 783
		public const string SDL_HINT_NO_SIGNAL_HANDLERS = "SDL_NO_SIGNAL_HANDLERS";

		// Token: 0x04000310 RID: 784
		public const string SDL_HINT_IME_INTERNAL_EDITING = "SDL_IME_INTERNAL_EDITING";

		// Token: 0x04000311 RID: 785
		public const string SDL_HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH = "SDL_ANDROID_SEPARATE_MOUSE_AND_TOUCH";

		// Token: 0x04000312 RID: 786
		public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";

		// Token: 0x04000313 RID: 787
		public const string SDL_HINT_THREAD_STACK_SIZE = "SDL_THREAD_STACK_SIZE";

		// Token: 0x04000314 RID: 788
		public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

		// Token: 0x04000315 RID: 789
		public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP = "SDL_WINDOWS_ENABLE_MESSAGELOOP";

		// Token: 0x04000316 RID: 790
		public const string SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4 = "SDL_WINDOWS_NO_CLOSE_ON_ALT_F4";

		// Token: 0x04000317 RID: 791
		public const string SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING = "SDL_XINPUT_USE_OLD_JOYSTICK_MAPPING";

		// Token: 0x04000318 RID: 792
		public const string SDL_HINT_MAC_BACKGROUND_APP = "SDL_MAC_BACKGROUND_APP";

		// Token: 0x04000319 RID: 793
		public const string SDL_HINT_VIDEO_X11_NET_WM_PING = "SDL_VIDEO_X11_NET_WM_PING";

		// Token: 0x0400031A RID: 794
		public const string SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";

		// Token: 0x0400031B RID: 795
		public const string SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION = "SDL_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";

		// Token: 0x0400031C RID: 796
		public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH = "SDL_MOUSE_FOCUS_CLICKTHROUGH";

		// Token: 0x0400031D RID: 797
		public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT = "SDL_BMP_SAVE_LEGACY_FORMAT";

		// Token: 0x0400031E RID: 798
		public const string SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING = "SDL_WINDOWS_DISABLE_THREAD_NAMING";

		// Token: 0x0400031F RID: 799
		public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";

		// Token: 0x04000320 RID: 800
		public const string SDL_HINT_AUDIO_RESAMPLING_MODE = "SDL_AUDIO_RESAMPLING_MODE";

		// Token: 0x04000321 RID: 801
		public const string SDL_HINT_RENDER_LOGICAL_SIZE_MODE = "SDL_RENDER_LOGICAL_SIZE_MODE";

		// Token: 0x04000322 RID: 802
		public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";

		// Token: 0x04000323 RID: 803
		public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";

		// Token: 0x04000324 RID: 804
		public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";

		// Token: 0x04000325 RID: 805
		public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";

		// Token: 0x04000326 RID: 806
		public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";

		// Token: 0x04000327 RID: 807
		public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";

		// Token: 0x04000328 RID: 808
		public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";

		// Token: 0x04000329 RID: 809
		public const string SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

		// Token: 0x0400032A RID: 810
		public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";

		// Token: 0x0400032B RID: 811
		public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";

		// Token: 0x0400032C RID: 812
		public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";

		// Token: 0x0400032D RID: 813
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";

		// Token: 0x0400032E RID: 814
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";

		// Token: 0x0400032F RID: 815
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";

		// Token: 0x04000330 RID: 816
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";

		// Token: 0x04000331 RID: 817
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";

		// Token: 0x04000332 RID: 818
		public const string SDL_HINT_ENABLE_STEAM_CONTROLLERS = "SDL_ENABLE_STEAM_CONTROLLERS";

		// Token: 0x04000333 RID: 819
		public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";

		// Token: 0x04000334 RID: 820
		public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";

		// Token: 0x04000335 RID: 821
		public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";

		// Token: 0x04000336 RID: 822
		public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";

		// Token: 0x04000337 RID: 823
		public const string SDL_HINT_RENDER_BATCHING = "SDL_RENDER_BATCHING";

		// Token: 0x04000338 RID: 824
		public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";

		// Token: 0x04000339 RID: 825
		public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";

		// Token: 0x0400033A RID: 826
		public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";

		// Token: 0x0400033B RID: 827
		public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";

		// Token: 0x0400033C RID: 828
		public const string SDL_HINT_VIDO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";

		// Token: 0x0400033D RID: 829
		public const string SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS = "SDL_GAMECONTROLLER_USE_BUTTON_LABELS";

		// Token: 0x0400033E RID: 830
		public const string SDL_HINT_VIDEO_EXTERNAL_CONTEXT = "SDL_VIDEO_EXTERNAL_CONTEXT";

		// Token: 0x0400033F RID: 831
		public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";

		// Token: 0x04000340 RID: 832
		public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";

		// Token: 0x04000341 RID: 833
		public const string SDL_HINT_VIDEO_X11_FORCE_EGL = "SDL_VIDEO_X11_FORCE_EGL";

		// Token: 0x04000342 RID: 834
		public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";

		// Token: 0x04000343 RID: 835
		public const string SDL_HINT_JOYSTICK_HIDAPI_CORRELATE_XINPUT = "SDL_JOYSTICK_HIDAPI_CORRELATE_XINPUT";

		// Token: 0x04000344 RID: 836
		public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";

		// Token: 0x04000345 RID: 837
		public const string SDL_HINT_AUDIO_DEVICE_APP_NAME = "SDL_AUDIO_DEVICE_APP_NAME";

		// Token: 0x04000346 RID: 838
		public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";

		// Token: 0x04000347 RID: 839
		public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";

		// Token: 0x04000348 RID: 840
		public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";

		// Token: 0x04000349 RID: 841
		public const string SDL_HINT_EMSCRIPTEN_ASYNCIFY = "SDL_EMSCRIPTEN_ASYNCIFY";

		// Token: 0x0400034A RID: 842
		public const string SDL_HINT_LINUX_JOYSTICK_DEADZONES = "SDL_LINUX_JOYSTICK_DEADZONES";

		// Token: 0x0400034B RID: 843
		public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";

		// Token: 0x0400034C RID: 844
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";

		// Token: 0x0400034D RID: 845
		public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";

		// Token: 0x0400034E RID: 846
		public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";

		// Token: 0x0400034F RID: 847
		public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";

		// Token: 0x04000350 RID: 848
		public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";

		// Token: 0x04000351 RID: 849
		public const string SDL_HINT_MOUSE_RELATIVE_SCALING = "SDL_MOUSE_RELATIVE_SCALING";

		// Token: 0x04000352 RID: 850
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";

		// Token: 0x04000353 RID: 851
		public const string SDL_HINT_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";

		// Token: 0x04000354 RID: 852
		public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";

		// Token: 0x04000355 RID: 853
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";

		// Token: 0x04000356 RID: 854
		public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";

		// Token: 0x04000357 RID: 855
		public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";

		// Token: 0x04000358 RID: 856
		public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";

		// Token: 0x04000359 RID: 857
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";

		// Token: 0x0400035A RID: 858
		public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";

		// Token: 0x0400035B RID: 859
		public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";

		// Token: 0x0400035C RID: 860
		public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";

		// Token: 0x0400035D RID: 861
		public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";

		// Token: 0x0400035E RID: 862
		public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";

		// Token: 0x0400035F RID: 863
		public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";

		// Token: 0x04000360 RID: 864
		public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";

		// Token: 0x04000361 RID: 865
		public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";

		// Token: 0x04000362 RID: 866
		public const string SDL_HINT_VIDEO_EGL_ALLOW_TRANSPARENCY = "SDL_VIDEO_EGL_ALLOW_TRANSPARENCY";

		// Token: 0x04000363 RID: 867
		public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";

		// Token: 0x04000364 RID: 868
		public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";

		// Token: 0x04000365 RID: 869
		public const string SDL_HINT_IME_SHOW_UI = "SDL_IME_SHOW_UI";

		// Token: 0x04000366 RID: 870
		public const string SDL_HINT_WINDOW_NO_ACTIVATION_WHEN_SHOWN = "SDL_WINDOW_NO_ACTIVATION_WHEN_SHOWN";

		// Token: 0x04000367 RID: 871
		public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";

		// Token: 0x04000368 RID: 872
		public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";

		// Token: 0x04000369 RID: 873
		public const string SDL_HINT_LINUX_JOYSTICK_CLASSIC = "SDL_LINUX_JOYSTICK_CLASSIC";

		// Token: 0x0400036A RID: 874
		public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";

		// Token: 0x0400036B RID: 875
		public const string SDL_HINT_FORCE_RAISEWINDOW = "SDL_HINT_FORCE_RAISEWINDOW";

		// Token: 0x0400036C RID: 876
		public const string SDL_HINT_IME_SUPPORT_EXTENDED_TEXT = "SDL_IME_SUPPORT_EXTENDED_TEXT";

		// Token: 0x0400036D RID: 877
		public const string SDL_HINT_JOYSTICK_GAMECUBE_RUMBLE_BRAKE = "SDL_JOYSTICK_GAMECUBE_RUMBLE_BRAKE";

		// Token: 0x0400036E RID: 878
		public const string SDL_HINT_JOYSTICK_ROG_CHAKRAM = "SDL_JOYSTICK_ROG_CHAKRAM";

		// Token: 0x0400036F RID: 879
		public const string SDL_HINT_MOUSE_RELATIVE_MODE_CENTER = "SDL_MOUSE_RELATIVE_MODE_CENTER";

		// Token: 0x04000370 RID: 880
		public const string SDL_HINT_MOUSE_AUTO_CAPTURE = "SDL_MOUSE_AUTO_CAPTURE";

		// Token: 0x04000371 RID: 881
		public const string SDL_HINT_VITA_TOUCH_MOUSE_DEVICE = "SDL_HINT_VITA_TOUCH_MOUSE_DEVICE";

		// Token: 0x04000372 RID: 882
		public const string SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";

		// Token: 0x04000373 RID: 883
		public const string SDL_HINT_VIDEO_FOREIGN_WINDOW_OPENGL = "SDL_VIDEO_FOREIGN_WINDOW_OPENGL";

		// Token: 0x04000374 RID: 884
		public const string SDL_HINT_VIDEO_FOREIGN_WINDOW_VULKAN = "SDL_VIDEO_FOREIGN_WINDOW_VULKAN";

		// Token: 0x04000375 RID: 885
		public const string SDL_HINT_X11_WINDOW_TYPE = "SDL_X11_WINDOW_TYPE";

		// Token: 0x04000376 RID: 886
		public const string SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";

		// Token: 0x04000377 RID: 887
		public const int SDL_MAJOR_VERSION = 2;

		// Token: 0x04000378 RID: 888
		public const int SDL_MINOR_VERSION = 0;

		// Token: 0x04000379 RID: 889
		public const int SDL_PATCHLEVEL = 22;

		// Token: 0x0400037A RID: 890
		public static readonly int SDL_COMPILEDVERSION = SDL.SDL_VERSIONNUM(2, 0, 22);

		// Token: 0x0400037B RID: 891
		public const int SDL_WINDOWPOS_UNDEFINED_MASK = 536805376;

		// Token: 0x0400037C RID: 892
		public const int SDL_WINDOWPOS_CENTERED_MASK = 805240832;

		// Token: 0x0400037D RID: 893
		public const int SDL_WINDOWPOS_UNDEFINED = 536805376;

		// Token: 0x0400037E RID: 894
		public const int SDL_WINDOWPOS_CENTERED = 805240832;

		// Token: 0x0400037F RID: 895
		public static readonly uint SDL_PIXELFORMAT_UNKNOWN = 0U;

		// Token: 0x04000380 RID: 896
		public static readonly uint SDL_PIXELFORMAT_INDEX1LSB = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX1, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 1, 0);

		// Token: 0x04000381 RID: 897
		public static readonly uint SDL_PIXELFORMAT_INDEX1MSB = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX1, 2U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 1, 0);

		// Token: 0x04000382 RID: 898
		public static readonly uint SDL_PIXELFORMAT_INDEX4LSB = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX4, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 4, 0);

		// Token: 0x04000383 RID: 899
		public static readonly uint SDL_PIXELFORMAT_INDEX4MSB = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX4, 2U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 4, 0);

		// Token: 0x04000384 RID: 900
		public static readonly uint SDL_PIXELFORMAT_INDEX8 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX8, 0U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 8, 1);

		// Token: 0x04000385 RID: 901
		public static readonly uint SDL_PIXELFORMAT_RGB332 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED8, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_332, 8, 1);

		// Token: 0x04000386 RID: 902
		public static readonly uint SDL_PIXELFORMAT_XRGB444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 12, 2);

		// Token: 0x04000387 RID: 903
		public static readonly uint SDL_PIXELFORMAT_RGB444 = SDL.SDL_PIXELFORMAT_XRGB444;

		// Token: 0x04000388 RID: 904
		public static readonly uint SDL_PIXELFORMAT_XBGR444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 5U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 12, 2);

		// Token: 0x04000389 RID: 905
		public static readonly uint SDL_PIXELFORMAT_BGR444 = SDL.SDL_PIXELFORMAT_XBGR444;

		// Token: 0x0400038A RID: 906
		public static readonly uint SDL_PIXELFORMAT_XRGB1555 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 15, 2);

		// Token: 0x0400038B RID: 907
		public static readonly uint SDL_PIXELFORMAT_RGB555 = SDL.SDL_PIXELFORMAT_XRGB1555;

		// Token: 0x0400038C RID: 908
		public static readonly uint SDL_PIXELFORMAT_XBGR1555 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_INDEX1, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 15, 2);

		// Token: 0x0400038D RID: 909
		public static readonly uint SDL_PIXELFORMAT_BGR555 = SDL.SDL_PIXELFORMAT_XBGR1555;

		// Token: 0x0400038E RID: 910
		public static readonly uint SDL_PIXELFORMAT_ARGB4444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 3U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);

		// Token: 0x0400038F RID: 911
		public static readonly uint SDL_PIXELFORMAT_RGBA4444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 4U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);

		// Token: 0x04000390 RID: 912
		public static readonly uint SDL_PIXELFORMAT_ABGR4444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 7U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);

		// Token: 0x04000391 RID: 913
		public static readonly uint SDL_PIXELFORMAT_BGRA4444 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 8U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_4444, 16, 2);

		// Token: 0x04000392 RID: 914
		public static readonly uint SDL_PIXELFORMAT_ARGB1555 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 3U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 16, 2);

		// Token: 0x04000393 RID: 915
		public static readonly uint SDL_PIXELFORMAT_RGBA5551 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 4U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_5551, 16, 2);

		// Token: 0x04000394 RID: 916
		public static readonly uint SDL_PIXELFORMAT_ABGR1555 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 7U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_1555, 16, 2);

		// Token: 0x04000395 RID: 917
		public static readonly uint SDL_PIXELFORMAT_BGRA5551 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 8U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_5551, 16, 2);

		// Token: 0x04000396 RID: 918
		public static readonly uint SDL_PIXELFORMAT_RGB565 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_565, 16, 2);

		// Token: 0x04000397 RID: 919
		public static readonly uint SDL_PIXELFORMAT_BGR565 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED16, 5U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_565, 16, 2);

		// Token: 0x04000398 RID: 920
		public static readonly uint SDL_PIXELFORMAT_RGB24 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYU8, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 24, 3);

		// Token: 0x04000399 RID: 921
		public static readonly uint SDL_PIXELFORMAT_BGR24 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_ARRAYU8, 4U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_NONE, 24, 3);

		// Token: 0x0400039A RID: 922
		public static readonly uint SDL_PIXELFORMAT_XRGB888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 1U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);

		// Token: 0x0400039B RID: 923
		public static readonly uint SDL_PIXELFORMAT_RGB888 = SDL.SDL_PIXELFORMAT_XRGB888;

		// Token: 0x0400039C RID: 924
		public static readonly uint SDL_PIXELFORMAT_RGBX8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 2U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);

		// Token: 0x0400039D RID: 925
		public static readonly uint SDL_PIXELFORMAT_XBGR888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 5U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);

		// Token: 0x0400039E RID: 926
		public static readonly uint SDL_PIXELFORMAT_BGR888 = SDL.SDL_PIXELFORMAT_XBGR888;

		// Token: 0x0400039F RID: 927
		public static readonly uint SDL_PIXELFORMAT_BGRX8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 6U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 24, 4);

		// Token: 0x040003A0 RID: 928
		public static readonly uint SDL_PIXELFORMAT_ARGB8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 3U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);

		// Token: 0x040003A1 RID: 929
		public static readonly uint SDL_PIXELFORMAT_RGBA8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 4U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);

		// Token: 0x040003A2 RID: 930
		public static readonly uint SDL_PIXELFORMAT_ABGR8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 7U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);

		// Token: 0x040003A3 RID: 931
		public static readonly uint SDL_PIXELFORMAT_BGRA8888 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 8U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_8888, 32, 4);

		// Token: 0x040003A4 RID: 932
		public static readonly uint SDL_PIXELFORMAT_ARGB2101010 = SDL.SDL_DEFINE_PIXELFORMAT(SDL.SDL_PixelType.SDL_PIXELTYPE_PACKED32, 3U, SDL.SDL_PackedLayout.SDL_PACKEDLAYOUT_2101010, 32, 4);

		// Token: 0x040003A5 RID: 933
		public static readonly uint SDL_PIXELFORMAT_YV12 = SDL.SDL_DEFINE_PIXELFOURCC(89, 86, 49, 50);

		// Token: 0x040003A6 RID: 934
		public static readonly uint SDL_PIXELFORMAT_IYUV = SDL.SDL_DEFINE_PIXELFOURCC(73, 89, 85, 86);

		// Token: 0x040003A7 RID: 935
		public static readonly uint SDL_PIXELFORMAT_YUY2 = SDL.SDL_DEFINE_PIXELFOURCC(89, 85, 89, 50);

		// Token: 0x040003A8 RID: 936
		public static readonly uint SDL_PIXELFORMAT_UYVY = SDL.SDL_DEFINE_PIXELFOURCC(85, 89, 86, 89);

		// Token: 0x040003A9 RID: 937
		public static readonly uint SDL_PIXELFORMAT_YVYU = SDL.SDL_DEFINE_PIXELFOURCC(89, 86, 89, 85);

		// Token: 0x040003AA RID: 938
		public const int SDL_NONSHAPEABLE_WINDOW = -1;

		// Token: 0x040003AB RID: 939
		public const int SDL_INVALID_SHAPE_ARGUMENT = -2;

		// Token: 0x040003AC RID: 940
		public const int SDL_WINDOW_LACKS_SHAPE = -3;

		// Token: 0x040003AD RID: 941
		public const uint SDL_SWSURFACE = 0U;

		// Token: 0x040003AE RID: 942
		public const uint SDL_PREALLOC = 1U;

		// Token: 0x040003AF RID: 943
		public const uint SDL_RLEACCEL = 2U;

		// Token: 0x040003B0 RID: 944
		public const uint SDL_DONTFREE = 4U;

		// Token: 0x040003B1 RID: 945
		public const byte SDL_PRESSED = 1;

		// Token: 0x040003B2 RID: 946
		public const byte SDL_RELEASED = 0;

		// Token: 0x040003B3 RID: 947
		public const int SDL_TEXTEDITINGEVENT_TEXT_SIZE = 32;

		// Token: 0x040003B4 RID: 948
		public const int SDL_TEXTINPUTEVENT_TEXT_SIZE = 32;

		// Token: 0x040003B5 RID: 949
		public const int SDL_QUERY = -1;

		// Token: 0x040003B6 RID: 950
		public const int SDL_IGNORE = 0;

		// Token: 0x040003B7 RID: 951
		public const int SDL_DISABLE = 0;

		// Token: 0x040003B8 RID: 952
		public const int SDL_ENABLE = 1;

		// Token: 0x040003B9 RID: 953
		public const int SDLK_SCANCODE_MASK = 1073741824;

		// Token: 0x040003BA RID: 954
		public const uint SDL_BUTTON_LEFT = 1U;

		// Token: 0x040003BB RID: 955
		public const uint SDL_BUTTON_MIDDLE = 2U;

		// Token: 0x040003BC RID: 956
		public const uint SDL_BUTTON_RIGHT = 3U;

		// Token: 0x040003BD RID: 957
		public const uint SDL_BUTTON_X1 = 4U;

		// Token: 0x040003BE RID: 958
		public const uint SDL_BUTTON_X2 = 5U;

		// Token: 0x040003BF RID: 959
		public static readonly uint SDL_BUTTON_LMASK = SDL.SDL_BUTTON(1U);

		// Token: 0x040003C0 RID: 960
		public static readonly uint SDL_BUTTON_MMASK = SDL.SDL_BUTTON(2U);

		// Token: 0x040003C1 RID: 961
		public static readonly uint SDL_BUTTON_RMASK = SDL.SDL_BUTTON(3U);

		// Token: 0x040003C2 RID: 962
		public static readonly uint SDL_BUTTON_X1MASK = SDL.SDL_BUTTON(4U);

		// Token: 0x040003C3 RID: 963
		public static readonly uint SDL_BUTTON_X2MASK = SDL.SDL_BUTTON(5U);

		// Token: 0x040003C4 RID: 964
		public const uint SDL_TOUCH_MOUSEID = 4294967295U;

		// Token: 0x040003C5 RID: 965
		public const byte SDL_HAT_CENTERED = 0;

		// Token: 0x040003C6 RID: 966
		public const byte SDL_HAT_UP = 1;

		// Token: 0x040003C7 RID: 967
		public const byte SDL_HAT_RIGHT = 2;

		// Token: 0x040003C8 RID: 968
		public const byte SDL_HAT_DOWN = 4;

		// Token: 0x040003C9 RID: 969
		public const byte SDL_HAT_LEFT = 8;

		// Token: 0x040003CA RID: 970
		public const byte SDL_HAT_RIGHTUP = 3;

		// Token: 0x040003CB RID: 971
		public const byte SDL_HAT_RIGHTDOWN = 6;

		// Token: 0x040003CC RID: 972
		public const byte SDL_HAT_LEFTUP = 9;

		// Token: 0x040003CD RID: 973
		public const byte SDL_HAT_LEFTDOWN = 12;

		// Token: 0x040003CE RID: 974
		public const float SDL_IPHONE_MAX_GFORCE = 5f;

		// Token: 0x040003CF RID: 975
		public const ushort SDL_HAPTIC_CONSTANT = 1;

		// Token: 0x040003D0 RID: 976
		public const ushort SDL_HAPTIC_SINE = 2;

		// Token: 0x040003D1 RID: 977
		public const ushort SDL_HAPTIC_LEFTRIGHT = 4;

		// Token: 0x040003D2 RID: 978
		public const ushort SDL_HAPTIC_TRIANGLE = 8;

		// Token: 0x040003D3 RID: 979
		public const ushort SDL_HAPTIC_SAWTOOTHUP = 16;

		// Token: 0x040003D4 RID: 980
		public const ushort SDL_HAPTIC_SAWTOOTHDOWN = 32;

		// Token: 0x040003D5 RID: 981
		public const ushort SDL_HAPTIC_SPRING = 128;

		// Token: 0x040003D6 RID: 982
		public const ushort SDL_HAPTIC_DAMPER = 256;

		// Token: 0x040003D7 RID: 983
		public const ushort SDL_HAPTIC_INERTIA = 512;

		// Token: 0x040003D8 RID: 984
		public const ushort SDL_HAPTIC_FRICTION = 1024;

		// Token: 0x040003D9 RID: 985
		public const ushort SDL_HAPTIC_CUSTOM = 2048;

		// Token: 0x040003DA RID: 986
		public const ushort SDL_HAPTIC_GAIN = 4096;

		// Token: 0x040003DB RID: 987
		public const ushort SDL_HAPTIC_AUTOCENTER = 8192;

		// Token: 0x040003DC RID: 988
		public const ushort SDL_HAPTIC_STATUS = 16384;

		// Token: 0x040003DD RID: 989
		public const ushort SDL_HAPTIC_PAUSE = 32768;

		// Token: 0x040003DE RID: 990
		public const byte SDL_HAPTIC_POLAR = 0;

		// Token: 0x040003DF RID: 991
		public const byte SDL_HAPTIC_CARTESIAN = 1;

		// Token: 0x040003E0 RID: 992
		public const byte SDL_HAPTIC_SPHERICAL = 2;

		// Token: 0x040003E1 RID: 993
		public const byte SDL_HAPTIC_STEERING_AXIS = 3;

		// Token: 0x040003E2 RID: 994
		public const uint SDL_HAPTIC_INFINITY = 4294967295U;

		// Token: 0x040003E3 RID: 995
		public const float SDL_STANDARD_GRAVITY = 9.80665f;

		// Token: 0x040003E4 RID: 996
		public const ushort SDL_AUDIO_MASK_BITSIZE = 255;

		// Token: 0x040003E5 RID: 997
		public const ushort SDL_AUDIO_MASK_DATATYPE = 256;

		// Token: 0x040003E6 RID: 998
		public const ushort SDL_AUDIO_MASK_ENDIAN = 4096;

		// Token: 0x040003E7 RID: 999
		public const ushort SDL_AUDIO_MASK_SIGNED = 32768;

		// Token: 0x040003E8 RID: 1000
		public const ushort AUDIO_U8 = 8;

		// Token: 0x040003E9 RID: 1001
		public const ushort AUDIO_S8 = 32776;

		// Token: 0x040003EA RID: 1002
		public const ushort AUDIO_U16LSB = 16;

		// Token: 0x040003EB RID: 1003
		public const ushort AUDIO_S16LSB = 32784;

		// Token: 0x040003EC RID: 1004
		public const ushort AUDIO_U16MSB = 4112;

		// Token: 0x040003ED RID: 1005
		public const ushort AUDIO_S16MSB = 36880;

		// Token: 0x040003EE RID: 1006
		public const ushort AUDIO_U16 = 16;

		// Token: 0x040003EF RID: 1007
		public const ushort AUDIO_S16 = 32784;

		// Token: 0x040003F0 RID: 1008
		public const ushort AUDIO_S32LSB = 32800;

		// Token: 0x040003F1 RID: 1009
		public const ushort AUDIO_S32MSB = 36896;

		// Token: 0x040003F2 RID: 1010
		public const ushort AUDIO_S32 = 32800;

		// Token: 0x040003F3 RID: 1011
		public const ushort AUDIO_F32LSB = 33056;

		// Token: 0x040003F4 RID: 1012
		public const ushort AUDIO_F32MSB = 37152;

		// Token: 0x040003F5 RID: 1013
		public const ushort AUDIO_F32 = 33056;

		// Token: 0x040003F6 RID: 1014
		public static readonly ushort AUDIO_U16SYS = (BitConverter.IsLittleEndian ? 16 : 4112);

		// Token: 0x040003F7 RID: 1015
		public static readonly ushort AUDIO_S16SYS = (BitConverter.IsLittleEndian ? 32784 : 36880);

		// Token: 0x040003F8 RID: 1016
		public static readonly ushort AUDIO_S32SYS = (BitConverter.IsLittleEndian ? 32800 : 36896);

		// Token: 0x040003F9 RID: 1017
		public static readonly ushort AUDIO_F32SYS = (BitConverter.IsLittleEndian ? 33056 : 37152);

		// Token: 0x040003FA RID: 1018
		public const uint SDL_AUDIO_ALLOW_FREQUENCY_CHANGE = 1U;

		// Token: 0x040003FB RID: 1019
		public const uint SDL_AUDIO_ALLOW_FORMAT_CHANGE = 2U;

		// Token: 0x040003FC RID: 1020
		public const uint SDL_AUDIO_ALLOW_CHANNELS_CHANGE = 4U;

		// Token: 0x040003FD RID: 1021
		public const uint SDL_AUDIO_ALLOW_SAMPLES_CHANGE = 8U;

		// Token: 0x040003FE RID: 1022
		public const uint SDL_AUDIO_ALLOW_ANY_CHANGE = 15U;

		// Token: 0x040003FF RID: 1023
		public const int SDL_MIX_MAXVOLUME = 128;

		// Token: 0x04000400 RID: 1024
		public const int SDL_ANDROID_EXTERNAL_STORAGE_READ = 1;

		// Token: 0x04000401 RID: 1025
		public const int SDL_ANDROID_EXTERNAL_STORAGE_WRITE = 2;

		// Token: 0x020002D5 RID: 725
		public enum SDL_bool
		{
			// Token: 0x040016A9 RID: 5801
			SDL_FALSE,
			// Token: 0x040016AA RID: 5802
			SDL_TRUE
		}

		// Token: 0x020002D6 RID: 726
		// (Invoke) Token: 0x06001998 RID: 6552
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long SDLRWopsSizeCallback(IntPtr context);

		// Token: 0x020002D7 RID: 727
		// (Invoke) Token: 0x0600199C RID: 6556
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate long SDLRWopsSeekCallback(IntPtr context, long offset, int whence);

		// Token: 0x020002D8 RID: 728
		// (Invoke) Token: 0x060019A0 RID: 6560
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDLRWopsReadCallback(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

		// Token: 0x020002D9 RID: 729
		// (Invoke) Token: 0x060019A4 RID: 6564
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDLRWopsWriteCallback(IntPtr context, IntPtr ptr, IntPtr size, IntPtr num);

		// Token: 0x020002DA RID: 730
		// (Invoke) Token: 0x060019A8 RID: 6568
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDLRWopsCloseCallback(IntPtr context);

		// Token: 0x020002DB RID: 731
		public struct SDL_RWops
		{
			// Token: 0x040016AB RID: 5803
			public IntPtr size;

			// Token: 0x040016AC RID: 5804
			public IntPtr seek;

			// Token: 0x040016AD RID: 5805
			public IntPtr read;

			// Token: 0x040016AE RID: 5806
			public IntPtr write;

			// Token: 0x040016AF RID: 5807
			public IntPtr close;

			// Token: 0x040016B0 RID: 5808
			public uint type;
		}

		// Token: 0x020002DC RID: 732
		// (Invoke) Token: 0x060019AC RID: 6572
		public delegate int SDL_main_func(int argc, IntPtr argv);

		// Token: 0x020002DD RID: 733
		public enum SDL_HintPriority
		{
			// Token: 0x040016B2 RID: 5810
			SDL_HINT_DEFAULT,
			// Token: 0x040016B3 RID: 5811
			SDL_HINT_NORMAL,
			// Token: 0x040016B4 RID: 5812
			SDL_HINT_OVERRIDE
		}

		// Token: 0x020002DE RID: 734
		public enum SDL_LogCategory
		{
			// Token: 0x040016B6 RID: 5814
			SDL_LOG_CATEGORY_APPLICATION,
			// Token: 0x040016B7 RID: 5815
			SDL_LOG_CATEGORY_ERROR,
			// Token: 0x040016B8 RID: 5816
			SDL_LOG_CATEGORY_ASSERT,
			// Token: 0x040016B9 RID: 5817
			SDL_LOG_CATEGORY_SYSTEM,
			// Token: 0x040016BA RID: 5818
			SDL_LOG_CATEGORY_AUDIO,
			// Token: 0x040016BB RID: 5819
			SDL_LOG_CATEGORY_VIDEO,
			// Token: 0x040016BC RID: 5820
			SDL_LOG_CATEGORY_RENDER,
			// Token: 0x040016BD RID: 5821
			SDL_LOG_CATEGORY_INPUT,
			// Token: 0x040016BE RID: 5822
			SDL_LOG_CATEGORY_TEST,
			// Token: 0x040016BF RID: 5823
			SDL_LOG_CATEGORY_RESERVED1,
			// Token: 0x040016C0 RID: 5824
			SDL_LOG_CATEGORY_RESERVED2,
			// Token: 0x040016C1 RID: 5825
			SDL_LOG_CATEGORY_RESERVED3,
			// Token: 0x040016C2 RID: 5826
			SDL_LOG_CATEGORY_RESERVED4,
			// Token: 0x040016C3 RID: 5827
			SDL_LOG_CATEGORY_RESERVED5,
			// Token: 0x040016C4 RID: 5828
			SDL_LOG_CATEGORY_RESERVED6,
			// Token: 0x040016C5 RID: 5829
			SDL_LOG_CATEGORY_RESERVED7,
			// Token: 0x040016C6 RID: 5830
			SDL_LOG_CATEGORY_RESERVED8,
			// Token: 0x040016C7 RID: 5831
			SDL_LOG_CATEGORY_RESERVED9,
			// Token: 0x040016C8 RID: 5832
			SDL_LOG_CATEGORY_RESERVED10,
			// Token: 0x040016C9 RID: 5833
			SDL_LOG_CATEGORY_CUSTOM
		}

		// Token: 0x020002DF RID: 735
		public enum SDL_LogPriority
		{
			// Token: 0x040016CB RID: 5835
			SDL_LOG_PRIORITY_VERBOSE = 1,
			// Token: 0x040016CC RID: 5836
			SDL_LOG_PRIORITY_DEBUG,
			// Token: 0x040016CD RID: 5837
			SDL_LOG_PRIORITY_INFO,
			// Token: 0x040016CE RID: 5838
			SDL_LOG_PRIORITY_WARN,
			// Token: 0x040016CF RID: 5839
			SDL_LOG_PRIORITY_ERROR,
			// Token: 0x040016D0 RID: 5840
			SDL_LOG_PRIORITY_CRITICAL,
			// Token: 0x040016D1 RID: 5841
			SDL_NUM_LOG_PRIORITIES
		}

		// Token: 0x020002E0 RID: 736
		// (Invoke) Token: 0x060019B0 RID: 6576
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_LogOutputFunction(IntPtr userdata, int category, SDL.SDL_LogPriority priority, IntPtr message);

		// Token: 0x020002E1 RID: 737
		[Flags]
		public enum SDL_MessageBoxFlags : uint
		{
			// Token: 0x040016D3 RID: 5843
			SDL_MESSAGEBOX_ERROR = 16U,
			// Token: 0x040016D4 RID: 5844
			SDL_MESSAGEBOX_WARNING = 32U,
			// Token: 0x040016D5 RID: 5845
			SDL_MESSAGEBOX_INFORMATION = 64U
		}

		// Token: 0x020002E2 RID: 738
		[Flags]
		public enum SDL_MessageBoxButtonFlags : uint
		{
			// Token: 0x040016D7 RID: 5847
			SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 1U,
			// Token: 0x040016D8 RID: 5848
			SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 2U
		}

		// Token: 0x020002E3 RID: 739
		private struct INTERNAL_SDL_MessageBoxButtonData
		{
			// Token: 0x040016D9 RID: 5849
			public SDL.SDL_MessageBoxButtonFlags flags;

			// Token: 0x040016DA RID: 5850
			public int buttonid;

			// Token: 0x040016DB RID: 5851
			public IntPtr text;
		}

		// Token: 0x020002E4 RID: 740
		public struct SDL_MessageBoxButtonData
		{
			// Token: 0x040016DC RID: 5852
			public SDL.SDL_MessageBoxButtonFlags flags;

			// Token: 0x040016DD RID: 5853
			public int buttonid;

			// Token: 0x040016DE RID: 5854
			public string text;
		}

		// Token: 0x020002E5 RID: 741
		public struct SDL_MessageBoxColor
		{
			// Token: 0x040016DF RID: 5855
			public byte r;

			// Token: 0x040016E0 RID: 5856
			public byte g;

			// Token: 0x040016E1 RID: 5857
			public byte b;
		}

		// Token: 0x020002E6 RID: 742
		public enum SDL_MessageBoxColorType
		{
			// Token: 0x040016E3 RID: 5859
			SDL_MESSAGEBOX_COLOR_BACKGROUND,
			// Token: 0x040016E4 RID: 5860
			SDL_MESSAGEBOX_COLOR_TEXT,
			// Token: 0x040016E5 RID: 5861
			SDL_MESSAGEBOX_COLOR_BUTTON_BORDER,
			// Token: 0x040016E6 RID: 5862
			SDL_MESSAGEBOX_COLOR_BUTTON_BACKGROUND,
			// Token: 0x040016E7 RID: 5863
			SDL_MESSAGEBOX_COLOR_BUTTON_SELECTED,
			// Token: 0x040016E8 RID: 5864
			SDL_MESSAGEBOX_COLOR_MAX
		}

		// Token: 0x020002E7 RID: 743
		public struct SDL_MessageBoxColorScheme
		{
			// Token: 0x040016E9 RID: 5865
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.Struct)]
			public SDL.SDL_MessageBoxColor[] colors;
		}

		// Token: 0x020002E8 RID: 744
		private struct INTERNAL_SDL_MessageBoxData
		{
			// Token: 0x040016EA RID: 5866
			public SDL.SDL_MessageBoxFlags flags;

			// Token: 0x040016EB RID: 5867
			public IntPtr window;

			// Token: 0x040016EC RID: 5868
			public IntPtr title;

			// Token: 0x040016ED RID: 5869
			public IntPtr message;

			// Token: 0x040016EE RID: 5870
			public int numbuttons;

			// Token: 0x040016EF RID: 5871
			public IntPtr buttons;

			// Token: 0x040016F0 RID: 5872
			public IntPtr colorScheme;
		}

		// Token: 0x020002E9 RID: 745
		public struct SDL_MessageBoxData
		{
			// Token: 0x040016F1 RID: 5873
			public SDL.SDL_MessageBoxFlags flags;

			// Token: 0x040016F2 RID: 5874
			public IntPtr window;

			// Token: 0x040016F3 RID: 5875
			public string title;

			// Token: 0x040016F4 RID: 5876
			public string message;

			// Token: 0x040016F5 RID: 5877
			public int numbuttons;

			// Token: 0x040016F6 RID: 5878
			public SDL.SDL_MessageBoxButtonData[] buttons;

			// Token: 0x040016F7 RID: 5879
			public SDL.SDL_MessageBoxColorScheme? colorScheme;
		}

		// Token: 0x020002EA RID: 746
		public struct SDL_version
		{
			// Token: 0x040016F8 RID: 5880
			public byte major;

			// Token: 0x040016F9 RID: 5881
			public byte minor;

			// Token: 0x040016FA RID: 5882
			public byte patch;
		}

		// Token: 0x020002EB RID: 747
		public enum SDL_GLattr
		{
			// Token: 0x040016FC RID: 5884
			SDL_GL_RED_SIZE,
			// Token: 0x040016FD RID: 5885
			SDL_GL_GREEN_SIZE,
			// Token: 0x040016FE RID: 5886
			SDL_GL_BLUE_SIZE,
			// Token: 0x040016FF RID: 5887
			SDL_GL_ALPHA_SIZE,
			// Token: 0x04001700 RID: 5888
			SDL_GL_BUFFER_SIZE,
			// Token: 0x04001701 RID: 5889
			SDL_GL_DOUBLEBUFFER,
			// Token: 0x04001702 RID: 5890
			SDL_GL_DEPTH_SIZE,
			// Token: 0x04001703 RID: 5891
			SDL_GL_STENCIL_SIZE,
			// Token: 0x04001704 RID: 5892
			SDL_GL_ACCUM_RED_SIZE,
			// Token: 0x04001705 RID: 5893
			SDL_GL_ACCUM_GREEN_SIZE,
			// Token: 0x04001706 RID: 5894
			SDL_GL_ACCUM_BLUE_SIZE,
			// Token: 0x04001707 RID: 5895
			SDL_GL_ACCUM_ALPHA_SIZE,
			// Token: 0x04001708 RID: 5896
			SDL_GL_STEREO,
			// Token: 0x04001709 RID: 5897
			SDL_GL_MULTISAMPLEBUFFERS,
			// Token: 0x0400170A RID: 5898
			SDL_GL_MULTISAMPLESAMPLES,
			// Token: 0x0400170B RID: 5899
			SDL_GL_ACCELERATED_VISUAL,
			// Token: 0x0400170C RID: 5900
			SDL_GL_RETAINED_BACKING,
			// Token: 0x0400170D RID: 5901
			SDL_GL_CONTEXT_MAJOR_VERSION,
			// Token: 0x0400170E RID: 5902
			SDL_GL_CONTEXT_MINOR_VERSION,
			// Token: 0x0400170F RID: 5903
			SDL_GL_CONTEXT_EGL,
			// Token: 0x04001710 RID: 5904
			SDL_GL_CONTEXT_FLAGS,
			// Token: 0x04001711 RID: 5905
			SDL_GL_CONTEXT_PROFILE_MASK,
			// Token: 0x04001712 RID: 5906
			SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
			// Token: 0x04001713 RID: 5907
			SDL_GL_FRAMEBUFFER_SRGB_CAPABLE,
			// Token: 0x04001714 RID: 5908
			SDL_GL_CONTEXT_RELEASE_BEHAVIOR,
			// Token: 0x04001715 RID: 5909
			SDL_GL_CONTEXT_RESET_NOTIFICATION,
			// Token: 0x04001716 RID: 5910
			SDL_GL_CONTEXT_NO_ERROR
		}

		// Token: 0x020002EC RID: 748
		[Flags]
		public enum SDL_GLprofile
		{
			// Token: 0x04001718 RID: 5912
			SDL_GL_CONTEXT_PROFILE_CORE = 1,
			// Token: 0x04001719 RID: 5913
			SDL_GL_CONTEXT_PROFILE_COMPATIBILITY = 2,
			// Token: 0x0400171A RID: 5914
			SDL_GL_CONTEXT_PROFILE_ES = 4
		}

		// Token: 0x020002ED RID: 749
		[Flags]
		public enum SDL_GLcontext
		{
			// Token: 0x0400171C RID: 5916
			SDL_GL_CONTEXT_DEBUG_FLAG = 1,
			// Token: 0x0400171D RID: 5917
			SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 2,
			// Token: 0x0400171E RID: 5918
			SDL_GL_CONTEXT_ROBUST_ACCESS_FLAG = 4,
			// Token: 0x0400171F RID: 5919
			SDL_GL_CONTEXT_RESET_ISOLATION_FLAG = 8
		}

		// Token: 0x020002EE RID: 750
		public enum SDL_WindowEventID : byte
		{
			// Token: 0x04001721 RID: 5921
			SDL_WINDOWEVENT_NONE,
			// Token: 0x04001722 RID: 5922
			SDL_WINDOWEVENT_SHOWN,
			// Token: 0x04001723 RID: 5923
			SDL_WINDOWEVENT_HIDDEN,
			// Token: 0x04001724 RID: 5924
			SDL_WINDOWEVENT_EXPOSED,
			// Token: 0x04001725 RID: 5925
			SDL_WINDOWEVENT_MOVED,
			// Token: 0x04001726 RID: 5926
			SDL_WINDOWEVENT_RESIZED,
			// Token: 0x04001727 RID: 5927
			SDL_WINDOWEVENT_SIZE_CHANGED,
			// Token: 0x04001728 RID: 5928
			SDL_WINDOWEVENT_MINIMIZED,
			// Token: 0x04001729 RID: 5929
			SDL_WINDOWEVENT_MAXIMIZED,
			// Token: 0x0400172A RID: 5930
			SDL_WINDOWEVENT_RESTORED,
			// Token: 0x0400172B RID: 5931
			SDL_WINDOWEVENT_ENTER,
			// Token: 0x0400172C RID: 5932
			SDL_WINDOWEVENT_LEAVE,
			// Token: 0x0400172D RID: 5933
			SDL_WINDOWEVENT_FOCUS_GAINED,
			// Token: 0x0400172E RID: 5934
			SDL_WINDOWEVENT_FOCUS_LOST,
			// Token: 0x0400172F RID: 5935
			SDL_WINDOWEVENT_CLOSE,
			// Token: 0x04001730 RID: 5936
			SDL_WINDOWEVENT_TAKE_FOCUS,
			// Token: 0x04001731 RID: 5937
			SDL_WINDOWEVENT_HIT_TEST,
			// Token: 0x04001732 RID: 5938
			SDL_WINDOWEVENT_ICCPROF_CHANGED,
			// Token: 0x04001733 RID: 5939
			SDL_WINDOWEVENT_DISPLAY_CHANGED
		}

		// Token: 0x020002EF RID: 751
		public enum SDL_DisplayEventID : byte
		{
			// Token: 0x04001735 RID: 5941
			SDL_DISPLAYEVENT_NONE,
			// Token: 0x04001736 RID: 5942
			SDL_DISPLAYEVENT_ORIENTATION,
			// Token: 0x04001737 RID: 5943
			SDL_DISPLAYEVENT_CONNECTED,
			// Token: 0x04001738 RID: 5944
			SDL_DISPLAYEVENT_DISCONNECTED
		}

		// Token: 0x020002F0 RID: 752
		public enum SDL_DisplayOrientation
		{
			// Token: 0x0400173A RID: 5946
			SDL_ORIENTATION_UNKNOWN,
			// Token: 0x0400173B RID: 5947
			SDL_ORIENTATION_LANDSCAPE,
			// Token: 0x0400173C RID: 5948
			SDL_ORIENTATION_LANDSCAPE_FLIPPED,
			// Token: 0x0400173D RID: 5949
			SDL_ORIENTATION_PORTRAIT,
			// Token: 0x0400173E RID: 5950
			SDL_ORIENTATION_PORTRAIT_FLIPPED
		}

		// Token: 0x020002F1 RID: 753
		public enum SDL_FlashOperation
		{
			// Token: 0x04001740 RID: 5952
			SDL_FLASH_CANCEL,
			// Token: 0x04001741 RID: 5953
			SDL_FLASH_BRIEFLY,
			// Token: 0x04001742 RID: 5954
			SDL_FLASH_UNTIL_FOCUSED
		}

		// Token: 0x020002F2 RID: 754
		[Flags]
		public enum SDL_WindowFlags : uint
		{
			// Token: 0x04001744 RID: 5956
			SDL_WINDOW_FULLSCREEN = 1U,
			// Token: 0x04001745 RID: 5957
			SDL_WINDOW_OPENGL = 2U,
			// Token: 0x04001746 RID: 5958
			SDL_WINDOW_SHOWN = 4U,
			// Token: 0x04001747 RID: 5959
			SDL_WINDOW_HIDDEN = 8U,
			// Token: 0x04001748 RID: 5960
			SDL_WINDOW_BORDERLESS = 16U,
			// Token: 0x04001749 RID: 5961
			SDL_WINDOW_RESIZABLE = 32U,
			// Token: 0x0400174A RID: 5962
			SDL_WINDOW_MINIMIZED = 64U,
			// Token: 0x0400174B RID: 5963
			SDL_WINDOW_MAXIMIZED = 128U,
			// Token: 0x0400174C RID: 5964
			SDL_WINDOW_MOUSE_GRABBED = 256U,
			// Token: 0x0400174D RID: 5965
			SDL_WINDOW_INPUT_FOCUS = 512U,
			// Token: 0x0400174E RID: 5966
			SDL_WINDOW_MOUSE_FOCUS = 1024U,
			// Token: 0x0400174F RID: 5967
			SDL_WINDOW_FULLSCREEN_DESKTOP = 4097U,
			// Token: 0x04001750 RID: 5968
			SDL_WINDOW_FOREIGN = 2048U,
			// Token: 0x04001751 RID: 5969
			SDL_WINDOW_ALLOW_HIGHDPI = 8192U,
			// Token: 0x04001752 RID: 5970
			SDL_WINDOW_MOUSE_CAPTURE = 16384U,
			// Token: 0x04001753 RID: 5971
			SDL_WINDOW_ALWAYS_ON_TOP = 32768U,
			// Token: 0x04001754 RID: 5972
			SDL_WINDOW_SKIP_TASKBAR = 65536U,
			// Token: 0x04001755 RID: 5973
			SDL_WINDOW_UTILITY = 131072U,
			// Token: 0x04001756 RID: 5974
			SDL_WINDOW_TOOLTIP = 262144U,
			// Token: 0x04001757 RID: 5975
			SDL_WINDOW_POPUP_MENU = 524288U,
			// Token: 0x04001758 RID: 5976
			SDL_WINDOW_KEYBOARD_GRABBED = 1048576U,
			// Token: 0x04001759 RID: 5977
			SDL_WINDOW_VULKAN = 268435456U,
			// Token: 0x0400175A RID: 5978
			SDL_WINDOW_METAL = 33554432U,
			// Token: 0x0400175B RID: 5979
			SDL_WINDOW_INPUT_GRABBED = 256U
		}

		// Token: 0x020002F3 RID: 755
		public enum SDL_HitTestResult
		{
			// Token: 0x0400175D RID: 5981
			SDL_HITTEST_NORMAL,
			// Token: 0x0400175E RID: 5982
			SDL_HITTEST_DRAGGABLE,
			// Token: 0x0400175F RID: 5983
			SDL_HITTEST_RESIZE_TOPLEFT,
			// Token: 0x04001760 RID: 5984
			SDL_HITTEST_RESIZE_TOP,
			// Token: 0x04001761 RID: 5985
			SDL_HITTEST_RESIZE_TOPRIGHT,
			// Token: 0x04001762 RID: 5986
			SDL_HITTEST_RESIZE_RIGHT,
			// Token: 0x04001763 RID: 5987
			SDL_HITTEST_RESIZE_BOTTOMRIGHT,
			// Token: 0x04001764 RID: 5988
			SDL_HITTEST_RESIZE_BOTTOM,
			// Token: 0x04001765 RID: 5989
			SDL_HITTEST_RESIZE_BOTTOMLEFT,
			// Token: 0x04001766 RID: 5990
			SDL_HITTEST_RESIZE_LEFT
		}

		// Token: 0x020002F4 RID: 756
		public struct SDL_DisplayMode
		{
			// Token: 0x04001767 RID: 5991
			public uint format;

			// Token: 0x04001768 RID: 5992
			public int w;

			// Token: 0x04001769 RID: 5993
			public int h;

			// Token: 0x0400176A RID: 5994
			public int refresh_rate;

			// Token: 0x0400176B RID: 5995
			public IntPtr driverdata;
		}

		// Token: 0x020002F5 RID: 757
		// (Invoke) Token: 0x060019B4 RID: 6580
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL.SDL_HitTestResult SDL_HitTest(IntPtr win, IntPtr area, IntPtr data);

		// Token: 0x020002F6 RID: 758
		[Flags]
		public enum SDL_BlendMode
		{
			// Token: 0x0400176D RID: 5997
			SDL_BLENDMODE_NONE = 0,
			// Token: 0x0400176E RID: 5998
			SDL_BLENDMODE_BLEND = 1,
			// Token: 0x0400176F RID: 5999
			SDL_BLENDMODE_ADD = 2,
			// Token: 0x04001770 RID: 6000
			SDL_BLENDMODE_MOD = 4,
			// Token: 0x04001771 RID: 6001
			SDL_BLENDMODE_MUL = 8,
			// Token: 0x04001772 RID: 6002
			SDL_BLENDMODE_INVALID = 2147483647
		}

		// Token: 0x020002F7 RID: 759
		public enum SDL_BlendOperation
		{
			// Token: 0x04001774 RID: 6004
			SDL_BLENDOPERATION_ADD = 1,
			// Token: 0x04001775 RID: 6005
			SDL_BLENDOPERATION_SUBTRACT,
			// Token: 0x04001776 RID: 6006
			SDL_BLENDOPERATION_REV_SUBTRACT,
			// Token: 0x04001777 RID: 6007
			SDL_BLENDOPERATION_MINIMUM,
			// Token: 0x04001778 RID: 6008
			SDL_BLENDOPERATION_MAXIMUM
		}

		// Token: 0x020002F8 RID: 760
		public enum SDL_BlendFactor
		{
			// Token: 0x0400177A RID: 6010
			SDL_BLENDFACTOR_ZERO = 1,
			// Token: 0x0400177B RID: 6011
			SDL_BLENDFACTOR_ONE,
			// Token: 0x0400177C RID: 6012
			SDL_BLENDFACTOR_SRC_COLOR,
			// Token: 0x0400177D RID: 6013
			SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
			// Token: 0x0400177E RID: 6014
			SDL_BLENDFACTOR_SRC_ALPHA,
			// Token: 0x0400177F RID: 6015
			SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
			// Token: 0x04001780 RID: 6016
			SDL_BLENDFACTOR_DST_COLOR,
			// Token: 0x04001781 RID: 6017
			SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,
			// Token: 0x04001782 RID: 6018
			SDL_BLENDFACTOR_DST_ALPHA,
			// Token: 0x04001783 RID: 6019
			SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA
		}

		// Token: 0x020002F9 RID: 761
		[Flags]
		public enum SDL_RendererFlags : uint
		{
			// Token: 0x04001785 RID: 6021
			SDL_RENDERER_SOFTWARE = 1U,
			// Token: 0x04001786 RID: 6022
			SDL_RENDERER_ACCELERATED = 2U,
			// Token: 0x04001787 RID: 6023
			SDL_RENDERER_PRESENTVSYNC = 4U,
			// Token: 0x04001788 RID: 6024
			SDL_RENDERER_TARGETTEXTURE = 8U
		}

		// Token: 0x020002FA RID: 762
		[Flags]
		public enum SDL_RendererFlip
		{
			// Token: 0x0400178A RID: 6026
			SDL_FLIP_NONE = 0,
			// Token: 0x0400178B RID: 6027
			SDL_FLIP_HORIZONTAL = 1,
			// Token: 0x0400178C RID: 6028
			SDL_FLIP_VERTICAL = 2
		}

		// Token: 0x020002FB RID: 763
		public enum SDL_TextureAccess
		{
			// Token: 0x0400178E RID: 6030
			SDL_TEXTUREACCESS_STATIC,
			// Token: 0x0400178F RID: 6031
			SDL_TEXTUREACCESS_STREAMING,
			// Token: 0x04001790 RID: 6032
			SDL_TEXTUREACCESS_TARGET
		}

		// Token: 0x020002FC RID: 764
		[Flags]
		public enum SDL_TextureModulate
		{
			// Token: 0x04001792 RID: 6034
			SDL_TEXTUREMODULATE_NONE = 0,
			// Token: 0x04001793 RID: 6035
			SDL_TEXTUREMODULATE_HORIZONTAL = 1,
			// Token: 0x04001794 RID: 6036
			SDL_TEXTUREMODULATE_VERTICAL = 2
		}

		// Token: 0x020002FD RID: 765
		public struct SDL_RendererInfo
		{
			// Token: 0x04001795 RID: 6037
			public IntPtr name;

			// Token: 0x04001796 RID: 6038
			public uint flags;

			// Token: 0x04001797 RID: 6039
			public uint num_texture_formats;

			// Token: 0x04001798 RID: 6040
			[FixedBuffer(typeof(uint), 16)]
			public SDL.SDL_RendererInfo.<texture_formats>e__FixedBuffer texture_formats;

			// Token: 0x04001799 RID: 6041
			public int max_texture_width;

			// Token: 0x0400179A RID: 6042
			public int max_texture_height;

			// Token: 0x02000405 RID: 1029
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 64)]
			public struct <texture_formats>e__FixedBuffer
			{
				// Token: 0x04001E35 RID: 7733
				public uint FixedElementField;
			}
		}

		// Token: 0x020002FE RID: 766
		public enum SDL_ScaleMode
		{
			// Token: 0x0400179C RID: 6044
			SDL_ScaleModeNearest,
			// Token: 0x0400179D RID: 6045
			SDL_ScaleModeLinear,
			// Token: 0x0400179E RID: 6046
			SDL_ScaleModeBest
		}

		// Token: 0x020002FF RID: 767
		public struct SDL_Vertex
		{
			// Token: 0x0400179F RID: 6047
			public SDL.SDL_FPoint position;

			// Token: 0x040017A0 RID: 6048
			public SDL.SDL_Color color;

			// Token: 0x040017A1 RID: 6049
			public SDL.SDL_FPoint tex_coord;
		}

		// Token: 0x02000300 RID: 768
		public enum SDL_PixelType
		{
			// Token: 0x040017A3 RID: 6051
			SDL_PIXELTYPE_UNKNOWN,
			// Token: 0x040017A4 RID: 6052
			SDL_PIXELTYPE_INDEX1,
			// Token: 0x040017A5 RID: 6053
			SDL_PIXELTYPE_INDEX4,
			// Token: 0x040017A6 RID: 6054
			SDL_PIXELTYPE_INDEX8,
			// Token: 0x040017A7 RID: 6055
			SDL_PIXELTYPE_PACKED8,
			// Token: 0x040017A8 RID: 6056
			SDL_PIXELTYPE_PACKED16,
			// Token: 0x040017A9 RID: 6057
			SDL_PIXELTYPE_PACKED32,
			// Token: 0x040017AA RID: 6058
			SDL_PIXELTYPE_ARRAYU8,
			// Token: 0x040017AB RID: 6059
			SDL_PIXELTYPE_ARRAYU16,
			// Token: 0x040017AC RID: 6060
			SDL_PIXELTYPE_ARRAYU32,
			// Token: 0x040017AD RID: 6061
			SDL_PIXELTYPE_ARRAYF16,
			// Token: 0x040017AE RID: 6062
			SDL_PIXELTYPE_ARRAYF32
		}

		// Token: 0x02000301 RID: 769
		public enum SDL_BitmapOrder
		{
			// Token: 0x040017B0 RID: 6064
			SDL_BITMAPORDER_NONE,
			// Token: 0x040017B1 RID: 6065
			SDL_BITMAPORDER_4321,
			// Token: 0x040017B2 RID: 6066
			SDL_BITMAPORDER_1234
		}

		// Token: 0x02000302 RID: 770
		public enum SDL_PackedOrder
		{
			// Token: 0x040017B4 RID: 6068
			SDL_PACKEDORDER_NONE,
			// Token: 0x040017B5 RID: 6069
			SDL_PACKEDORDER_XRGB,
			// Token: 0x040017B6 RID: 6070
			SDL_PACKEDORDER_RGBX,
			// Token: 0x040017B7 RID: 6071
			SDL_PACKEDORDER_ARGB,
			// Token: 0x040017B8 RID: 6072
			SDL_PACKEDORDER_RGBA,
			// Token: 0x040017B9 RID: 6073
			SDL_PACKEDORDER_XBGR,
			// Token: 0x040017BA RID: 6074
			SDL_PACKEDORDER_BGRX,
			// Token: 0x040017BB RID: 6075
			SDL_PACKEDORDER_ABGR,
			// Token: 0x040017BC RID: 6076
			SDL_PACKEDORDER_BGRA
		}

		// Token: 0x02000303 RID: 771
		public enum SDL_ArrayOrder
		{
			// Token: 0x040017BE RID: 6078
			SDL_ARRAYORDER_NONE,
			// Token: 0x040017BF RID: 6079
			SDL_ARRAYORDER_RGB,
			// Token: 0x040017C0 RID: 6080
			SDL_ARRAYORDER_RGBA,
			// Token: 0x040017C1 RID: 6081
			SDL_ARRAYORDER_ARGB,
			// Token: 0x040017C2 RID: 6082
			SDL_ARRAYORDER_BGR,
			// Token: 0x040017C3 RID: 6083
			SDL_ARRAYORDER_BGRA,
			// Token: 0x040017C4 RID: 6084
			SDL_ARRAYORDER_ABGR
		}

		// Token: 0x02000304 RID: 772
		public enum SDL_PackedLayout
		{
			// Token: 0x040017C6 RID: 6086
			SDL_PACKEDLAYOUT_NONE,
			// Token: 0x040017C7 RID: 6087
			SDL_PACKEDLAYOUT_332,
			// Token: 0x040017C8 RID: 6088
			SDL_PACKEDLAYOUT_4444,
			// Token: 0x040017C9 RID: 6089
			SDL_PACKEDLAYOUT_1555,
			// Token: 0x040017CA RID: 6090
			SDL_PACKEDLAYOUT_5551,
			// Token: 0x040017CB RID: 6091
			SDL_PACKEDLAYOUT_565,
			// Token: 0x040017CC RID: 6092
			SDL_PACKEDLAYOUT_8888,
			// Token: 0x040017CD RID: 6093
			SDL_PACKEDLAYOUT_2101010,
			// Token: 0x040017CE RID: 6094
			SDL_PACKEDLAYOUT_1010102
		}

		// Token: 0x02000305 RID: 773
		public struct SDL_Color
		{
			// Token: 0x040017CF RID: 6095
			public byte r;

			// Token: 0x040017D0 RID: 6096
			public byte g;

			// Token: 0x040017D1 RID: 6097
			public byte b;

			// Token: 0x040017D2 RID: 6098
			public byte a;
		}

		// Token: 0x02000306 RID: 774
		public struct SDL_Palette
		{
			// Token: 0x040017D3 RID: 6099
			public int ncolors;

			// Token: 0x040017D4 RID: 6100
			public IntPtr colors;

			// Token: 0x040017D5 RID: 6101
			public int version;

			// Token: 0x040017D6 RID: 6102
			public int refcount;
		}

		// Token: 0x02000307 RID: 775
		public struct SDL_PixelFormat
		{
			// Token: 0x040017D7 RID: 6103
			public uint format;

			// Token: 0x040017D8 RID: 6104
			public IntPtr palette;

			// Token: 0x040017D9 RID: 6105
			public byte BitsPerPixel;

			// Token: 0x040017DA RID: 6106
			public byte BytesPerPixel;

			// Token: 0x040017DB RID: 6107
			public uint Rmask;

			// Token: 0x040017DC RID: 6108
			public uint Gmask;

			// Token: 0x040017DD RID: 6109
			public uint Bmask;

			// Token: 0x040017DE RID: 6110
			public uint Amask;

			// Token: 0x040017DF RID: 6111
			public byte Rloss;

			// Token: 0x040017E0 RID: 6112
			public byte Gloss;

			// Token: 0x040017E1 RID: 6113
			public byte Bloss;

			// Token: 0x040017E2 RID: 6114
			public byte Aloss;

			// Token: 0x040017E3 RID: 6115
			public byte Rshift;

			// Token: 0x040017E4 RID: 6116
			public byte Gshift;

			// Token: 0x040017E5 RID: 6117
			public byte Bshift;

			// Token: 0x040017E6 RID: 6118
			public byte Ashift;

			// Token: 0x040017E7 RID: 6119
			public int refcount;

			// Token: 0x040017E8 RID: 6120
			public IntPtr next;
		}

		// Token: 0x02000308 RID: 776
		public struct SDL_Point
		{
			// Token: 0x040017E9 RID: 6121
			public int x;

			// Token: 0x040017EA RID: 6122
			public int y;
		}

		// Token: 0x02000309 RID: 777
		public struct SDL_Rect
		{
			// Token: 0x040017EB RID: 6123
			public int x;

			// Token: 0x040017EC RID: 6124
			public int y;

			// Token: 0x040017ED RID: 6125
			public int w;

			// Token: 0x040017EE RID: 6126
			public int h;
		}

		// Token: 0x0200030A RID: 778
		public struct SDL_FPoint
		{
			// Token: 0x040017EF RID: 6127
			public float x;

			// Token: 0x040017F0 RID: 6128
			public float y;
		}

		// Token: 0x0200030B RID: 779
		public struct SDL_FRect
		{
			// Token: 0x040017F1 RID: 6129
			public float x;

			// Token: 0x040017F2 RID: 6130
			public float y;

			// Token: 0x040017F3 RID: 6131
			public float w;

			// Token: 0x040017F4 RID: 6132
			public float h;
		}

		// Token: 0x0200030C RID: 780
		public enum WindowShapeMode
		{
			// Token: 0x040017F6 RID: 6134
			ShapeModeDefault,
			// Token: 0x040017F7 RID: 6135
			ShapeModeBinarizeAlpha,
			// Token: 0x040017F8 RID: 6136
			ShapeModeReverseBinarizeAlpha,
			// Token: 0x040017F9 RID: 6137
			ShapeModeColorKey
		}

		// Token: 0x0200030D RID: 781
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_WindowShapeParams
		{
			// Token: 0x040017FA RID: 6138
			[FieldOffset(0)]
			public byte binarizationCutoff;

			// Token: 0x040017FB RID: 6139
			[FieldOffset(0)]
			public SDL.SDL_Color colorKey;
		}

		// Token: 0x0200030E RID: 782
		public struct SDL_WindowShapeMode
		{
			// Token: 0x040017FC RID: 6140
			public SDL.WindowShapeMode mode;

			// Token: 0x040017FD RID: 6141
			public SDL.SDL_WindowShapeParams parameters;
		}

		// Token: 0x0200030F RID: 783
		public struct SDL_Surface
		{
			// Token: 0x040017FE RID: 6142
			public uint flags;

			// Token: 0x040017FF RID: 6143
			public IntPtr format;

			// Token: 0x04001800 RID: 6144
			public int w;

			// Token: 0x04001801 RID: 6145
			public int h;

			// Token: 0x04001802 RID: 6146
			public int pitch;

			// Token: 0x04001803 RID: 6147
			public IntPtr pixels;

			// Token: 0x04001804 RID: 6148
			public IntPtr userdata;

			// Token: 0x04001805 RID: 6149
			public int locked;

			// Token: 0x04001806 RID: 6150
			public IntPtr list_blitmap;

			// Token: 0x04001807 RID: 6151
			public SDL.SDL_Rect clip_rect;

			// Token: 0x04001808 RID: 6152
			public IntPtr map;

			// Token: 0x04001809 RID: 6153
			public int refcount;
		}

		// Token: 0x02000310 RID: 784
		public enum SDL_EventType : uint
		{
			// Token: 0x0400180B RID: 6155
			SDL_FIRSTEVENT,
			// Token: 0x0400180C RID: 6156
			SDL_QUIT = 256U,
			// Token: 0x0400180D RID: 6157
			SDL_APP_TERMINATING,
			// Token: 0x0400180E RID: 6158
			SDL_APP_LOWMEMORY,
			// Token: 0x0400180F RID: 6159
			SDL_APP_WILLENTERBACKGROUND,
			// Token: 0x04001810 RID: 6160
			SDL_APP_DIDENTERBACKGROUND,
			// Token: 0x04001811 RID: 6161
			SDL_APP_WILLENTERFOREGROUND,
			// Token: 0x04001812 RID: 6162
			SDL_APP_DIDENTERFOREGROUND,
			// Token: 0x04001813 RID: 6163
			SDL_LOCALECHANGED,
			// Token: 0x04001814 RID: 6164
			SDL_DISPLAYEVENT = 336U,
			// Token: 0x04001815 RID: 6165
			SDL_WINDOWEVENT = 512U,
			// Token: 0x04001816 RID: 6166
			SDL_SYSWMEVENT,
			// Token: 0x04001817 RID: 6167
			SDL_KEYDOWN = 768U,
			// Token: 0x04001818 RID: 6168
			SDL_KEYUP,
			// Token: 0x04001819 RID: 6169
			SDL_TEXTEDITING,
			// Token: 0x0400181A RID: 6170
			SDL_TEXTINPUT,
			// Token: 0x0400181B RID: 6171
			SDL_KEYMAPCHANGED,
			// Token: 0x0400181C RID: 6172
			SDL_TEXTEDITING_EXT,
			// Token: 0x0400181D RID: 6173
			SDL_MOUSEMOTION = 1024U,
			// Token: 0x0400181E RID: 6174
			SDL_MOUSEBUTTONDOWN,
			// Token: 0x0400181F RID: 6175
			SDL_MOUSEBUTTONUP,
			// Token: 0x04001820 RID: 6176
			SDL_MOUSEWHEEL,
			// Token: 0x04001821 RID: 6177
			SDL_JOYAXISMOTION = 1536U,
			// Token: 0x04001822 RID: 6178
			SDL_JOYBALLMOTION,
			// Token: 0x04001823 RID: 6179
			SDL_JOYHATMOTION,
			// Token: 0x04001824 RID: 6180
			SDL_JOYBUTTONDOWN,
			// Token: 0x04001825 RID: 6181
			SDL_JOYBUTTONUP,
			// Token: 0x04001826 RID: 6182
			SDL_JOYDEVICEADDED,
			// Token: 0x04001827 RID: 6183
			SDL_JOYDEVICEREMOVED,
			// Token: 0x04001828 RID: 6184
			SDL_CONTROLLERAXISMOTION = 1616U,
			// Token: 0x04001829 RID: 6185
			SDL_CONTROLLERBUTTONDOWN,
			// Token: 0x0400182A RID: 6186
			SDL_CONTROLLERBUTTONUP,
			// Token: 0x0400182B RID: 6187
			SDL_CONTROLLERDEVICEADDED,
			// Token: 0x0400182C RID: 6188
			SDL_CONTROLLERDEVICEREMOVED,
			// Token: 0x0400182D RID: 6189
			SDL_CONTROLLERDEVICEREMAPPED,
			// Token: 0x0400182E RID: 6190
			SDL_CONTROLLERTOUCHPADDOWN,
			// Token: 0x0400182F RID: 6191
			SDL_CONTROLLERTOUCHPADMOTION,
			// Token: 0x04001830 RID: 6192
			SDL_CONTROLLERTOUCHPADUP,
			// Token: 0x04001831 RID: 6193
			SDL_CONTROLLERSENSORUPDATE,
			// Token: 0x04001832 RID: 6194
			SDL_FINGERDOWN = 1792U,
			// Token: 0x04001833 RID: 6195
			SDL_FINGERUP,
			// Token: 0x04001834 RID: 6196
			SDL_FINGERMOTION,
			// Token: 0x04001835 RID: 6197
			SDL_DOLLARGESTURE = 2048U,
			// Token: 0x04001836 RID: 6198
			SDL_DOLLARRECORD,
			// Token: 0x04001837 RID: 6199
			SDL_MULTIGESTURE,
			// Token: 0x04001838 RID: 6200
			SDL_CLIPBOARDUPDATE = 2304U,
			// Token: 0x04001839 RID: 6201
			SDL_DROPFILE = 4096U,
			// Token: 0x0400183A RID: 6202
			SDL_DROPTEXT,
			// Token: 0x0400183B RID: 6203
			SDL_DROPBEGIN,
			// Token: 0x0400183C RID: 6204
			SDL_DROPCOMPLETE,
			// Token: 0x0400183D RID: 6205
			SDL_AUDIODEVICEADDED = 4352U,
			// Token: 0x0400183E RID: 6206
			SDL_AUDIODEVICEREMOVED,
			// Token: 0x0400183F RID: 6207
			SDL_SENSORUPDATE = 4608U,
			// Token: 0x04001840 RID: 6208
			SDL_RENDER_TARGETS_RESET = 8192U,
			// Token: 0x04001841 RID: 6209
			SDL_RENDER_DEVICE_RESET,
			// Token: 0x04001842 RID: 6210
			SDL_POLLSENTINEL = 32512U,
			// Token: 0x04001843 RID: 6211
			SDL_USEREVENT = 32768U,
			// Token: 0x04001844 RID: 6212
			SDL_LASTEVENT = 65535U
		}

		// Token: 0x02000311 RID: 785
		public enum SDL_MouseWheelDirection : uint
		{
			// Token: 0x04001846 RID: 6214
			SDL_MOUSEWHEEL_NORMAL,
			// Token: 0x04001847 RID: 6215
			SDL_MOUSEWHEEL_FLIPPED
		}

		// Token: 0x02000312 RID: 786
		public struct SDL_GenericEvent
		{
			// Token: 0x04001848 RID: 6216
			public SDL.SDL_EventType type;

			// Token: 0x04001849 RID: 6217
			public uint timestamp;
		}

		// Token: 0x02000313 RID: 787
		public struct SDL_DisplayEvent
		{
			// Token: 0x0400184A RID: 6218
			public SDL.SDL_EventType type;

			// Token: 0x0400184B RID: 6219
			public uint timestamp;

			// Token: 0x0400184C RID: 6220
			public uint display;

			// Token: 0x0400184D RID: 6221
			public SDL.SDL_DisplayEventID displayEvent;

			// Token: 0x0400184E RID: 6222
			private byte padding1;

			// Token: 0x0400184F RID: 6223
			private byte padding2;

			// Token: 0x04001850 RID: 6224
			private byte padding3;

			// Token: 0x04001851 RID: 6225
			public int data1;
		}

		// Token: 0x02000314 RID: 788
		public struct SDL_WindowEvent
		{
			// Token: 0x04001852 RID: 6226
			public SDL.SDL_EventType type;

			// Token: 0x04001853 RID: 6227
			public uint timestamp;

			// Token: 0x04001854 RID: 6228
			public uint windowID;

			// Token: 0x04001855 RID: 6229
			public SDL.SDL_WindowEventID windowEvent;

			// Token: 0x04001856 RID: 6230
			private byte padding1;

			// Token: 0x04001857 RID: 6231
			private byte padding2;

			// Token: 0x04001858 RID: 6232
			private byte padding3;

			// Token: 0x04001859 RID: 6233
			public int data1;

			// Token: 0x0400185A RID: 6234
			public int data2;
		}

		// Token: 0x02000315 RID: 789
		public struct SDL_KeyboardEvent
		{
			// Token: 0x0400185B RID: 6235
			public SDL.SDL_EventType type;

			// Token: 0x0400185C RID: 6236
			public uint timestamp;

			// Token: 0x0400185D RID: 6237
			public uint windowID;

			// Token: 0x0400185E RID: 6238
			public byte state;

			// Token: 0x0400185F RID: 6239
			public byte repeat;

			// Token: 0x04001860 RID: 6240
			private byte padding2;

			// Token: 0x04001861 RID: 6241
			private byte padding3;

			// Token: 0x04001862 RID: 6242
			public SDL.SDL_Keysym keysym;
		}

		// Token: 0x02000316 RID: 790
		public struct SDL_TextEditingEvent
		{
			// Token: 0x04001863 RID: 6243
			public SDL.SDL_EventType type;

			// Token: 0x04001864 RID: 6244
			public uint timestamp;

			// Token: 0x04001865 RID: 6245
			public uint windowID;

			// Token: 0x04001866 RID: 6246
			[FixedBuffer(typeof(byte), 32)]
			public SDL.SDL_TextEditingEvent.<text>e__FixedBuffer text;

			// Token: 0x04001867 RID: 6247
			public int start;

			// Token: 0x04001868 RID: 6248
			public int length;

			// Token: 0x02000406 RID: 1030
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 32)]
			public struct <text>e__FixedBuffer
			{
				// Token: 0x04001E36 RID: 7734
				public byte FixedElementField;
			}
		}

		// Token: 0x02000317 RID: 791
		public struct SDL_TextEditingExtEvent
		{
			// Token: 0x04001869 RID: 6249
			public SDL.SDL_EventType type;

			// Token: 0x0400186A RID: 6250
			public uint timestamp;

			// Token: 0x0400186B RID: 6251
			public uint windowID;

			// Token: 0x0400186C RID: 6252
			public IntPtr text;

			// Token: 0x0400186D RID: 6253
			public int start;

			// Token: 0x0400186E RID: 6254
			public int length;
		}

		// Token: 0x02000318 RID: 792
		public struct SDL_TextInputEvent
		{
			// Token: 0x0400186F RID: 6255
			public SDL.SDL_EventType type;

			// Token: 0x04001870 RID: 6256
			public uint timestamp;

			// Token: 0x04001871 RID: 6257
			public uint windowID;

			// Token: 0x04001872 RID: 6258
			[FixedBuffer(typeof(byte), 32)]
			public SDL.SDL_TextInputEvent.<text>e__FixedBuffer text;

			// Token: 0x02000407 RID: 1031
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 32)]
			public struct <text>e__FixedBuffer
			{
				// Token: 0x04001E37 RID: 7735
				public byte FixedElementField;
			}
		}

		// Token: 0x02000319 RID: 793
		public struct SDL_MouseMotionEvent
		{
			// Token: 0x04001873 RID: 6259
			public SDL.SDL_EventType type;

			// Token: 0x04001874 RID: 6260
			public uint timestamp;

			// Token: 0x04001875 RID: 6261
			public uint windowID;

			// Token: 0x04001876 RID: 6262
			public uint which;

			// Token: 0x04001877 RID: 6263
			public byte state;

			// Token: 0x04001878 RID: 6264
			private byte padding1;

			// Token: 0x04001879 RID: 6265
			private byte padding2;

			// Token: 0x0400187A RID: 6266
			private byte padding3;

			// Token: 0x0400187B RID: 6267
			public int x;

			// Token: 0x0400187C RID: 6268
			public int y;

			// Token: 0x0400187D RID: 6269
			public int xrel;

			// Token: 0x0400187E RID: 6270
			public int yrel;
		}

		// Token: 0x0200031A RID: 794
		public struct SDL_MouseButtonEvent
		{
			// Token: 0x0400187F RID: 6271
			public SDL.SDL_EventType type;

			// Token: 0x04001880 RID: 6272
			public uint timestamp;

			// Token: 0x04001881 RID: 6273
			public uint windowID;

			// Token: 0x04001882 RID: 6274
			public uint which;

			// Token: 0x04001883 RID: 6275
			public byte button;

			// Token: 0x04001884 RID: 6276
			public byte state;

			// Token: 0x04001885 RID: 6277
			public byte clicks;

			// Token: 0x04001886 RID: 6278
			private byte padding1;

			// Token: 0x04001887 RID: 6279
			public int x;

			// Token: 0x04001888 RID: 6280
			public int y;
		}

		// Token: 0x0200031B RID: 795
		public struct SDL_MouseWheelEvent
		{
			// Token: 0x04001889 RID: 6281
			public SDL.SDL_EventType type;

			// Token: 0x0400188A RID: 6282
			public uint timestamp;

			// Token: 0x0400188B RID: 6283
			public uint windowID;

			// Token: 0x0400188C RID: 6284
			public uint which;

			// Token: 0x0400188D RID: 6285
			public int x;

			// Token: 0x0400188E RID: 6286
			public int y;

			// Token: 0x0400188F RID: 6287
			public uint direction;

			// Token: 0x04001890 RID: 6288
			public float preciseX;

			// Token: 0x04001891 RID: 6289
			public float preciseY;
		}

		// Token: 0x0200031C RID: 796
		public struct SDL_JoyAxisEvent
		{
			// Token: 0x04001892 RID: 6290
			public SDL.SDL_EventType type;

			// Token: 0x04001893 RID: 6291
			public uint timestamp;

			// Token: 0x04001894 RID: 6292
			public int which;

			// Token: 0x04001895 RID: 6293
			public byte axis;

			// Token: 0x04001896 RID: 6294
			private byte padding1;

			// Token: 0x04001897 RID: 6295
			private byte padding2;

			// Token: 0x04001898 RID: 6296
			private byte padding3;

			// Token: 0x04001899 RID: 6297
			public short axisValue;

			// Token: 0x0400189A RID: 6298
			public ushort padding4;
		}

		// Token: 0x0200031D RID: 797
		public struct SDL_JoyBallEvent
		{
			// Token: 0x0400189B RID: 6299
			public SDL.SDL_EventType type;

			// Token: 0x0400189C RID: 6300
			public uint timestamp;

			// Token: 0x0400189D RID: 6301
			public int which;

			// Token: 0x0400189E RID: 6302
			public byte ball;

			// Token: 0x0400189F RID: 6303
			private byte padding1;

			// Token: 0x040018A0 RID: 6304
			private byte padding2;

			// Token: 0x040018A1 RID: 6305
			private byte padding3;

			// Token: 0x040018A2 RID: 6306
			public short xrel;

			// Token: 0x040018A3 RID: 6307
			public short yrel;
		}

		// Token: 0x0200031E RID: 798
		public struct SDL_JoyHatEvent
		{
			// Token: 0x040018A4 RID: 6308
			public SDL.SDL_EventType type;

			// Token: 0x040018A5 RID: 6309
			public uint timestamp;

			// Token: 0x040018A6 RID: 6310
			public int which;

			// Token: 0x040018A7 RID: 6311
			public byte hat;

			// Token: 0x040018A8 RID: 6312
			public byte hatValue;

			// Token: 0x040018A9 RID: 6313
			private byte padding1;

			// Token: 0x040018AA RID: 6314
			private byte padding2;
		}

		// Token: 0x0200031F RID: 799
		public struct SDL_JoyButtonEvent
		{
			// Token: 0x040018AB RID: 6315
			public SDL.SDL_EventType type;

			// Token: 0x040018AC RID: 6316
			public uint timestamp;

			// Token: 0x040018AD RID: 6317
			public int which;

			// Token: 0x040018AE RID: 6318
			public byte button;

			// Token: 0x040018AF RID: 6319
			public byte state;

			// Token: 0x040018B0 RID: 6320
			private byte padding1;

			// Token: 0x040018B1 RID: 6321
			private byte padding2;
		}

		// Token: 0x02000320 RID: 800
		public struct SDL_JoyDeviceEvent
		{
			// Token: 0x040018B2 RID: 6322
			public SDL.SDL_EventType type;

			// Token: 0x040018B3 RID: 6323
			public uint timestamp;

			// Token: 0x040018B4 RID: 6324
			public int which;
		}

		// Token: 0x02000321 RID: 801
		public struct SDL_ControllerAxisEvent
		{
			// Token: 0x040018B5 RID: 6325
			public SDL.SDL_EventType type;

			// Token: 0x040018B6 RID: 6326
			public uint timestamp;

			// Token: 0x040018B7 RID: 6327
			public int which;

			// Token: 0x040018B8 RID: 6328
			public byte axis;

			// Token: 0x040018B9 RID: 6329
			private byte padding1;

			// Token: 0x040018BA RID: 6330
			private byte padding2;

			// Token: 0x040018BB RID: 6331
			private byte padding3;

			// Token: 0x040018BC RID: 6332
			public short axisValue;

			// Token: 0x040018BD RID: 6333
			private ushort padding4;
		}

		// Token: 0x02000322 RID: 802
		public struct SDL_ControllerButtonEvent
		{
			// Token: 0x040018BE RID: 6334
			public SDL.SDL_EventType type;

			// Token: 0x040018BF RID: 6335
			public uint timestamp;

			// Token: 0x040018C0 RID: 6336
			public int which;

			// Token: 0x040018C1 RID: 6337
			public byte button;

			// Token: 0x040018C2 RID: 6338
			public byte state;

			// Token: 0x040018C3 RID: 6339
			private byte padding1;

			// Token: 0x040018C4 RID: 6340
			private byte padding2;
		}

		// Token: 0x02000323 RID: 803
		public struct SDL_ControllerDeviceEvent
		{
			// Token: 0x040018C5 RID: 6341
			public SDL.SDL_EventType type;

			// Token: 0x040018C6 RID: 6342
			public uint timestamp;

			// Token: 0x040018C7 RID: 6343
			public int which;
		}

		// Token: 0x02000324 RID: 804
		public struct SDL_ControllerTouchpadEvent
		{
			// Token: 0x040018C8 RID: 6344
			public SDL.SDL_EventType type;

			// Token: 0x040018C9 RID: 6345
			public uint timestamp;

			// Token: 0x040018CA RID: 6346
			public int which;

			// Token: 0x040018CB RID: 6347
			public int touchpad;

			// Token: 0x040018CC RID: 6348
			public int finger;

			// Token: 0x040018CD RID: 6349
			public float x;

			// Token: 0x040018CE RID: 6350
			public float y;

			// Token: 0x040018CF RID: 6351
			public float pressure;
		}

		// Token: 0x02000325 RID: 805
		public struct SDL_ControllerSensorEvent
		{
			// Token: 0x040018D0 RID: 6352
			public SDL.SDL_EventType type;

			// Token: 0x040018D1 RID: 6353
			public uint timestamp;

			// Token: 0x040018D2 RID: 6354
			public int which;

			// Token: 0x040018D3 RID: 6355
			public int sensor;

			// Token: 0x040018D4 RID: 6356
			public float data1;

			// Token: 0x040018D5 RID: 6357
			public float data2;

			// Token: 0x040018D6 RID: 6358
			public float data3;
		}

		// Token: 0x02000326 RID: 806
		public struct SDL_AudioDeviceEvent
		{
			// Token: 0x040018D7 RID: 6359
			public SDL.SDL_EventType type;

			// Token: 0x040018D8 RID: 6360
			public uint timestamp;

			// Token: 0x040018D9 RID: 6361
			public uint which;

			// Token: 0x040018DA RID: 6362
			public byte iscapture;

			// Token: 0x040018DB RID: 6363
			private byte padding1;

			// Token: 0x040018DC RID: 6364
			private byte padding2;

			// Token: 0x040018DD RID: 6365
			private byte padding3;
		}

		// Token: 0x02000327 RID: 807
		public struct SDL_TouchFingerEvent
		{
			// Token: 0x040018DE RID: 6366
			public SDL.SDL_EventType type;

			// Token: 0x040018DF RID: 6367
			public uint timestamp;

			// Token: 0x040018E0 RID: 6368
			public long touchId;

			// Token: 0x040018E1 RID: 6369
			public long fingerId;

			// Token: 0x040018E2 RID: 6370
			public float x;

			// Token: 0x040018E3 RID: 6371
			public float y;

			// Token: 0x040018E4 RID: 6372
			public float dx;

			// Token: 0x040018E5 RID: 6373
			public float dy;

			// Token: 0x040018E6 RID: 6374
			public float pressure;

			// Token: 0x040018E7 RID: 6375
			public uint windowID;
		}

		// Token: 0x02000328 RID: 808
		public struct SDL_MultiGestureEvent
		{
			// Token: 0x040018E8 RID: 6376
			public SDL.SDL_EventType type;

			// Token: 0x040018E9 RID: 6377
			public uint timestamp;

			// Token: 0x040018EA RID: 6378
			public long touchId;

			// Token: 0x040018EB RID: 6379
			public float dTheta;

			// Token: 0x040018EC RID: 6380
			public float dDist;

			// Token: 0x040018ED RID: 6381
			public float x;

			// Token: 0x040018EE RID: 6382
			public float y;

			// Token: 0x040018EF RID: 6383
			public ushort numFingers;

			// Token: 0x040018F0 RID: 6384
			public ushort padding;
		}

		// Token: 0x02000329 RID: 809
		public struct SDL_DollarGestureEvent
		{
			// Token: 0x040018F1 RID: 6385
			public SDL.SDL_EventType type;

			// Token: 0x040018F2 RID: 6386
			public uint timestamp;

			// Token: 0x040018F3 RID: 6387
			public long touchId;

			// Token: 0x040018F4 RID: 6388
			public long gestureId;

			// Token: 0x040018F5 RID: 6389
			public uint numFingers;

			// Token: 0x040018F6 RID: 6390
			public float error;

			// Token: 0x040018F7 RID: 6391
			public float x;

			// Token: 0x040018F8 RID: 6392
			public float y;
		}

		// Token: 0x0200032A RID: 810
		public struct SDL_DropEvent
		{
			// Token: 0x040018F9 RID: 6393
			public SDL.SDL_EventType type;

			// Token: 0x040018FA RID: 6394
			public uint timestamp;

			// Token: 0x040018FB RID: 6395
			public IntPtr file;

			// Token: 0x040018FC RID: 6396
			public uint windowID;
		}

		// Token: 0x0200032B RID: 811
		public struct SDL_SensorEvent
		{
			// Token: 0x040018FD RID: 6397
			public SDL.SDL_EventType type;

			// Token: 0x040018FE RID: 6398
			public uint timestamp;

			// Token: 0x040018FF RID: 6399
			public int which;

			// Token: 0x04001900 RID: 6400
			[FixedBuffer(typeof(float), 6)]
			public SDL.SDL_SensorEvent.<data>e__FixedBuffer data;

			// Token: 0x02000408 RID: 1032
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 24)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04001E38 RID: 7736
				public float FixedElementField;
			}
		}

		// Token: 0x0200032C RID: 812
		public struct SDL_QuitEvent
		{
			// Token: 0x04001901 RID: 6401
			public SDL.SDL_EventType type;

			// Token: 0x04001902 RID: 6402
			public uint timestamp;
		}

		// Token: 0x0200032D RID: 813
		public struct SDL_UserEvent
		{
			// Token: 0x04001903 RID: 6403
			public SDL.SDL_EventType type;

			// Token: 0x04001904 RID: 6404
			public uint timestamp;

			// Token: 0x04001905 RID: 6405
			public uint windowID;

			// Token: 0x04001906 RID: 6406
			public int code;

			// Token: 0x04001907 RID: 6407
			public IntPtr data1;

			// Token: 0x04001908 RID: 6408
			public IntPtr data2;
		}

		// Token: 0x0200032E RID: 814
		public struct SDL_SysWMEvent
		{
			// Token: 0x04001909 RID: 6409
			public SDL.SDL_EventType type;

			// Token: 0x0400190A RID: 6410
			public uint timestamp;

			// Token: 0x0400190B RID: 6411
			public IntPtr msg;
		}

		// Token: 0x0200032F RID: 815
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_Event
		{
			// Token: 0x0400190C RID: 6412
			[FieldOffset(0)]
			public SDL.SDL_EventType type;

			// Token: 0x0400190D RID: 6413
			[FieldOffset(0)]
			public SDL.SDL_EventType typeFSharp;

			// Token: 0x0400190E RID: 6414
			[FieldOffset(0)]
			public SDL.SDL_DisplayEvent display;

			// Token: 0x0400190F RID: 6415
			[FieldOffset(0)]
			public SDL.SDL_WindowEvent window;

			// Token: 0x04001910 RID: 6416
			[FieldOffset(0)]
			public SDL.SDL_KeyboardEvent key;

			// Token: 0x04001911 RID: 6417
			[FieldOffset(0)]
			public SDL.SDL_TextEditingEvent edit;

			// Token: 0x04001912 RID: 6418
			[FieldOffset(0)]
			public SDL.SDL_TextEditingExtEvent editExt;

			// Token: 0x04001913 RID: 6419
			[FieldOffset(0)]
			public SDL.SDL_TextInputEvent text;

			// Token: 0x04001914 RID: 6420
			[FieldOffset(0)]
			public SDL.SDL_MouseMotionEvent motion;

			// Token: 0x04001915 RID: 6421
			[FieldOffset(0)]
			public SDL.SDL_MouseButtonEvent button;

			// Token: 0x04001916 RID: 6422
			[FieldOffset(0)]
			public SDL.SDL_MouseWheelEvent wheel;

			// Token: 0x04001917 RID: 6423
			[FieldOffset(0)]
			public SDL.SDL_JoyAxisEvent jaxis;

			// Token: 0x04001918 RID: 6424
			[FieldOffset(0)]
			public SDL.SDL_JoyBallEvent jball;

			// Token: 0x04001919 RID: 6425
			[FieldOffset(0)]
			public SDL.SDL_JoyHatEvent jhat;

			// Token: 0x0400191A RID: 6426
			[FieldOffset(0)]
			public SDL.SDL_JoyButtonEvent jbutton;

			// Token: 0x0400191B RID: 6427
			[FieldOffset(0)]
			public SDL.SDL_JoyDeviceEvent jdevice;

			// Token: 0x0400191C RID: 6428
			[FieldOffset(0)]
			public SDL.SDL_ControllerAxisEvent caxis;

			// Token: 0x0400191D RID: 6429
			[FieldOffset(0)]
			public SDL.SDL_ControllerButtonEvent cbutton;

			// Token: 0x0400191E RID: 6430
			[FieldOffset(0)]
			public SDL.SDL_ControllerDeviceEvent cdevice;

			// Token: 0x0400191F RID: 6431
			[FieldOffset(0)]
			public SDL.SDL_ControllerTouchpadEvent ctouchpad;

			// Token: 0x04001920 RID: 6432
			[FieldOffset(0)]
			public SDL.SDL_ControllerSensorEvent csensor;

			// Token: 0x04001921 RID: 6433
			[FieldOffset(0)]
			public SDL.SDL_AudioDeviceEvent adevice;

			// Token: 0x04001922 RID: 6434
			[FieldOffset(0)]
			public SDL.SDL_SensorEvent sensor;

			// Token: 0x04001923 RID: 6435
			[FieldOffset(0)]
			public SDL.SDL_QuitEvent quit;

			// Token: 0x04001924 RID: 6436
			[FieldOffset(0)]
			public SDL.SDL_UserEvent user;

			// Token: 0x04001925 RID: 6437
			[FieldOffset(0)]
			public SDL.SDL_SysWMEvent syswm;

			// Token: 0x04001926 RID: 6438
			[FieldOffset(0)]
			public SDL.SDL_TouchFingerEvent tfinger;

			// Token: 0x04001927 RID: 6439
			[FieldOffset(0)]
			public SDL.SDL_MultiGestureEvent mgesture;

			// Token: 0x04001928 RID: 6440
			[FieldOffset(0)]
			public SDL.SDL_DollarGestureEvent dgesture;

			// Token: 0x04001929 RID: 6441
			[FieldOffset(0)]
			public SDL.SDL_DropEvent drop;

			// Token: 0x0400192A RID: 6442
			[FixedBuffer(typeof(byte), 56)]
			[FieldOffset(0)]
			private SDL.SDL_Event.<padding>e__FixedBuffer padding;

			// Token: 0x02000409 RID: 1033
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 56)]
			public struct <padding>e__FixedBuffer
			{
				// Token: 0x04001E39 RID: 7737
				public byte FixedElementField;
			}
		}

		// Token: 0x02000330 RID: 816
		// (Invoke) Token: 0x060019B8 RID: 6584
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDL_EventFilter(IntPtr userdata, IntPtr sdlevent);

		// Token: 0x02000331 RID: 817
		public enum SDL_eventaction
		{
			// Token: 0x0400192C RID: 6444
			SDL_ADDEVENT,
			// Token: 0x0400192D RID: 6445
			SDL_PEEKEVENT,
			// Token: 0x0400192E RID: 6446
			SDL_GETEVENT
		}

		// Token: 0x02000332 RID: 818
		public enum SDL_Scancode
		{
			// Token: 0x04001930 RID: 6448
			SDL_SCANCODE_UNKNOWN,
			// Token: 0x04001931 RID: 6449
			SDL_SCANCODE_A = 4,
			// Token: 0x04001932 RID: 6450
			SDL_SCANCODE_B,
			// Token: 0x04001933 RID: 6451
			SDL_SCANCODE_C,
			// Token: 0x04001934 RID: 6452
			SDL_SCANCODE_D,
			// Token: 0x04001935 RID: 6453
			SDL_SCANCODE_E,
			// Token: 0x04001936 RID: 6454
			SDL_SCANCODE_F,
			// Token: 0x04001937 RID: 6455
			SDL_SCANCODE_G,
			// Token: 0x04001938 RID: 6456
			SDL_SCANCODE_H,
			// Token: 0x04001939 RID: 6457
			SDL_SCANCODE_I,
			// Token: 0x0400193A RID: 6458
			SDL_SCANCODE_J,
			// Token: 0x0400193B RID: 6459
			SDL_SCANCODE_K,
			// Token: 0x0400193C RID: 6460
			SDL_SCANCODE_L,
			// Token: 0x0400193D RID: 6461
			SDL_SCANCODE_M,
			// Token: 0x0400193E RID: 6462
			SDL_SCANCODE_N,
			// Token: 0x0400193F RID: 6463
			SDL_SCANCODE_O,
			// Token: 0x04001940 RID: 6464
			SDL_SCANCODE_P,
			// Token: 0x04001941 RID: 6465
			SDL_SCANCODE_Q,
			// Token: 0x04001942 RID: 6466
			SDL_SCANCODE_R,
			// Token: 0x04001943 RID: 6467
			SDL_SCANCODE_S,
			// Token: 0x04001944 RID: 6468
			SDL_SCANCODE_T,
			// Token: 0x04001945 RID: 6469
			SDL_SCANCODE_U,
			// Token: 0x04001946 RID: 6470
			SDL_SCANCODE_V,
			// Token: 0x04001947 RID: 6471
			SDL_SCANCODE_W,
			// Token: 0x04001948 RID: 6472
			SDL_SCANCODE_X,
			// Token: 0x04001949 RID: 6473
			SDL_SCANCODE_Y,
			// Token: 0x0400194A RID: 6474
			SDL_SCANCODE_Z,
			// Token: 0x0400194B RID: 6475
			SDL_SCANCODE_1,
			// Token: 0x0400194C RID: 6476
			SDL_SCANCODE_2,
			// Token: 0x0400194D RID: 6477
			SDL_SCANCODE_3,
			// Token: 0x0400194E RID: 6478
			SDL_SCANCODE_4,
			// Token: 0x0400194F RID: 6479
			SDL_SCANCODE_5,
			// Token: 0x04001950 RID: 6480
			SDL_SCANCODE_6,
			// Token: 0x04001951 RID: 6481
			SDL_SCANCODE_7,
			// Token: 0x04001952 RID: 6482
			SDL_SCANCODE_8,
			// Token: 0x04001953 RID: 6483
			SDL_SCANCODE_9,
			// Token: 0x04001954 RID: 6484
			SDL_SCANCODE_0,
			// Token: 0x04001955 RID: 6485
			SDL_SCANCODE_RETURN,
			// Token: 0x04001956 RID: 6486
			SDL_SCANCODE_ESCAPE,
			// Token: 0x04001957 RID: 6487
			SDL_SCANCODE_BACKSPACE,
			// Token: 0x04001958 RID: 6488
			SDL_SCANCODE_TAB,
			// Token: 0x04001959 RID: 6489
			SDL_SCANCODE_SPACE,
			// Token: 0x0400195A RID: 6490
			SDL_SCANCODE_MINUS,
			// Token: 0x0400195B RID: 6491
			SDL_SCANCODE_EQUALS,
			// Token: 0x0400195C RID: 6492
			SDL_SCANCODE_LEFTBRACKET,
			// Token: 0x0400195D RID: 6493
			SDL_SCANCODE_RIGHTBRACKET,
			// Token: 0x0400195E RID: 6494
			SDL_SCANCODE_BACKSLASH,
			// Token: 0x0400195F RID: 6495
			SDL_SCANCODE_NONUSHASH,
			// Token: 0x04001960 RID: 6496
			SDL_SCANCODE_SEMICOLON,
			// Token: 0x04001961 RID: 6497
			SDL_SCANCODE_APOSTROPHE,
			// Token: 0x04001962 RID: 6498
			SDL_SCANCODE_GRAVE,
			// Token: 0x04001963 RID: 6499
			SDL_SCANCODE_COMMA,
			// Token: 0x04001964 RID: 6500
			SDL_SCANCODE_PERIOD,
			// Token: 0x04001965 RID: 6501
			SDL_SCANCODE_SLASH,
			// Token: 0x04001966 RID: 6502
			SDL_SCANCODE_CAPSLOCK,
			// Token: 0x04001967 RID: 6503
			SDL_SCANCODE_F1,
			// Token: 0x04001968 RID: 6504
			SDL_SCANCODE_F2,
			// Token: 0x04001969 RID: 6505
			SDL_SCANCODE_F3,
			// Token: 0x0400196A RID: 6506
			SDL_SCANCODE_F4,
			// Token: 0x0400196B RID: 6507
			SDL_SCANCODE_F5,
			// Token: 0x0400196C RID: 6508
			SDL_SCANCODE_F6,
			// Token: 0x0400196D RID: 6509
			SDL_SCANCODE_F7,
			// Token: 0x0400196E RID: 6510
			SDL_SCANCODE_F8,
			// Token: 0x0400196F RID: 6511
			SDL_SCANCODE_F9,
			// Token: 0x04001970 RID: 6512
			SDL_SCANCODE_F10,
			// Token: 0x04001971 RID: 6513
			SDL_SCANCODE_F11,
			// Token: 0x04001972 RID: 6514
			SDL_SCANCODE_F12,
			// Token: 0x04001973 RID: 6515
			SDL_SCANCODE_PRINTSCREEN,
			// Token: 0x04001974 RID: 6516
			SDL_SCANCODE_SCROLLLOCK,
			// Token: 0x04001975 RID: 6517
			SDL_SCANCODE_PAUSE,
			// Token: 0x04001976 RID: 6518
			SDL_SCANCODE_INSERT,
			// Token: 0x04001977 RID: 6519
			SDL_SCANCODE_HOME,
			// Token: 0x04001978 RID: 6520
			SDL_SCANCODE_PAGEUP,
			// Token: 0x04001979 RID: 6521
			SDL_SCANCODE_DELETE,
			// Token: 0x0400197A RID: 6522
			SDL_SCANCODE_END,
			// Token: 0x0400197B RID: 6523
			SDL_SCANCODE_PAGEDOWN,
			// Token: 0x0400197C RID: 6524
			SDL_SCANCODE_RIGHT,
			// Token: 0x0400197D RID: 6525
			SDL_SCANCODE_LEFT,
			// Token: 0x0400197E RID: 6526
			SDL_SCANCODE_DOWN,
			// Token: 0x0400197F RID: 6527
			SDL_SCANCODE_UP,
			// Token: 0x04001980 RID: 6528
			SDL_SCANCODE_NUMLOCKCLEAR,
			// Token: 0x04001981 RID: 6529
			SDL_SCANCODE_KP_DIVIDE,
			// Token: 0x04001982 RID: 6530
			SDL_SCANCODE_KP_MULTIPLY,
			// Token: 0x04001983 RID: 6531
			SDL_SCANCODE_KP_MINUS,
			// Token: 0x04001984 RID: 6532
			SDL_SCANCODE_KP_PLUS,
			// Token: 0x04001985 RID: 6533
			SDL_SCANCODE_KP_ENTER,
			// Token: 0x04001986 RID: 6534
			SDL_SCANCODE_KP_1,
			// Token: 0x04001987 RID: 6535
			SDL_SCANCODE_KP_2,
			// Token: 0x04001988 RID: 6536
			SDL_SCANCODE_KP_3,
			// Token: 0x04001989 RID: 6537
			SDL_SCANCODE_KP_4,
			// Token: 0x0400198A RID: 6538
			SDL_SCANCODE_KP_5,
			// Token: 0x0400198B RID: 6539
			SDL_SCANCODE_KP_6,
			// Token: 0x0400198C RID: 6540
			SDL_SCANCODE_KP_7,
			// Token: 0x0400198D RID: 6541
			SDL_SCANCODE_KP_8,
			// Token: 0x0400198E RID: 6542
			SDL_SCANCODE_KP_9,
			// Token: 0x0400198F RID: 6543
			SDL_SCANCODE_KP_0,
			// Token: 0x04001990 RID: 6544
			SDL_SCANCODE_KP_PERIOD,
			// Token: 0x04001991 RID: 6545
			SDL_SCANCODE_NONUSBACKSLASH,
			// Token: 0x04001992 RID: 6546
			SDL_SCANCODE_APPLICATION,
			// Token: 0x04001993 RID: 6547
			SDL_SCANCODE_POWER,
			// Token: 0x04001994 RID: 6548
			SDL_SCANCODE_KP_EQUALS,
			// Token: 0x04001995 RID: 6549
			SDL_SCANCODE_F13,
			// Token: 0x04001996 RID: 6550
			SDL_SCANCODE_F14,
			// Token: 0x04001997 RID: 6551
			SDL_SCANCODE_F15,
			// Token: 0x04001998 RID: 6552
			SDL_SCANCODE_F16,
			// Token: 0x04001999 RID: 6553
			SDL_SCANCODE_F17,
			// Token: 0x0400199A RID: 6554
			SDL_SCANCODE_F18,
			// Token: 0x0400199B RID: 6555
			SDL_SCANCODE_F19,
			// Token: 0x0400199C RID: 6556
			SDL_SCANCODE_F20,
			// Token: 0x0400199D RID: 6557
			SDL_SCANCODE_F21,
			// Token: 0x0400199E RID: 6558
			SDL_SCANCODE_F22,
			// Token: 0x0400199F RID: 6559
			SDL_SCANCODE_F23,
			// Token: 0x040019A0 RID: 6560
			SDL_SCANCODE_F24,
			// Token: 0x040019A1 RID: 6561
			SDL_SCANCODE_EXECUTE,
			// Token: 0x040019A2 RID: 6562
			SDL_SCANCODE_HELP,
			// Token: 0x040019A3 RID: 6563
			SDL_SCANCODE_MENU,
			// Token: 0x040019A4 RID: 6564
			SDL_SCANCODE_SELECT,
			// Token: 0x040019A5 RID: 6565
			SDL_SCANCODE_STOP,
			// Token: 0x040019A6 RID: 6566
			SDL_SCANCODE_AGAIN,
			// Token: 0x040019A7 RID: 6567
			SDL_SCANCODE_UNDO,
			// Token: 0x040019A8 RID: 6568
			SDL_SCANCODE_CUT,
			// Token: 0x040019A9 RID: 6569
			SDL_SCANCODE_COPY,
			// Token: 0x040019AA RID: 6570
			SDL_SCANCODE_PASTE,
			// Token: 0x040019AB RID: 6571
			SDL_SCANCODE_FIND,
			// Token: 0x040019AC RID: 6572
			SDL_SCANCODE_MUTE,
			// Token: 0x040019AD RID: 6573
			SDL_SCANCODE_VOLUMEUP,
			// Token: 0x040019AE RID: 6574
			SDL_SCANCODE_VOLUMEDOWN,
			// Token: 0x040019AF RID: 6575
			SDL_SCANCODE_KP_COMMA = 133,
			// Token: 0x040019B0 RID: 6576
			SDL_SCANCODE_KP_EQUALSAS400,
			// Token: 0x040019B1 RID: 6577
			SDL_SCANCODE_INTERNATIONAL1,
			// Token: 0x040019B2 RID: 6578
			SDL_SCANCODE_INTERNATIONAL2,
			// Token: 0x040019B3 RID: 6579
			SDL_SCANCODE_INTERNATIONAL3,
			// Token: 0x040019B4 RID: 6580
			SDL_SCANCODE_INTERNATIONAL4,
			// Token: 0x040019B5 RID: 6581
			SDL_SCANCODE_INTERNATIONAL5,
			// Token: 0x040019B6 RID: 6582
			SDL_SCANCODE_INTERNATIONAL6,
			// Token: 0x040019B7 RID: 6583
			SDL_SCANCODE_INTERNATIONAL7,
			// Token: 0x040019B8 RID: 6584
			SDL_SCANCODE_INTERNATIONAL8,
			// Token: 0x040019B9 RID: 6585
			SDL_SCANCODE_INTERNATIONAL9,
			// Token: 0x040019BA RID: 6586
			SDL_SCANCODE_LANG1,
			// Token: 0x040019BB RID: 6587
			SDL_SCANCODE_LANG2,
			// Token: 0x040019BC RID: 6588
			SDL_SCANCODE_LANG3,
			// Token: 0x040019BD RID: 6589
			SDL_SCANCODE_LANG4,
			// Token: 0x040019BE RID: 6590
			SDL_SCANCODE_LANG5,
			// Token: 0x040019BF RID: 6591
			SDL_SCANCODE_LANG6,
			// Token: 0x040019C0 RID: 6592
			SDL_SCANCODE_LANG7,
			// Token: 0x040019C1 RID: 6593
			SDL_SCANCODE_LANG8,
			// Token: 0x040019C2 RID: 6594
			SDL_SCANCODE_LANG9,
			// Token: 0x040019C3 RID: 6595
			SDL_SCANCODE_ALTERASE,
			// Token: 0x040019C4 RID: 6596
			SDL_SCANCODE_SYSREQ,
			// Token: 0x040019C5 RID: 6597
			SDL_SCANCODE_CANCEL,
			// Token: 0x040019C6 RID: 6598
			SDL_SCANCODE_CLEAR,
			// Token: 0x040019C7 RID: 6599
			SDL_SCANCODE_PRIOR,
			// Token: 0x040019C8 RID: 6600
			SDL_SCANCODE_RETURN2,
			// Token: 0x040019C9 RID: 6601
			SDL_SCANCODE_SEPARATOR,
			// Token: 0x040019CA RID: 6602
			SDL_SCANCODE_OUT,
			// Token: 0x040019CB RID: 6603
			SDL_SCANCODE_OPER,
			// Token: 0x040019CC RID: 6604
			SDL_SCANCODE_CLEARAGAIN,
			// Token: 0x040019CD RID: 6605
			SDL_SCANCODE_CRSEL,
			// Token: 0x040019CE RID: 6606
			SDL_SCANCODE_EXSEL,
			// Token: 0x040019CF RID: 6607
			SDL_SCANCODE_KP_00 = 176,
			// Token: 0x040019D0 RID: 6608
			SDL_SCANCODE_KP_000,
			// Token: 0x040019D1 RID: 6609
			SDL_SCANCODE_THOUSANDSSEPARATOR,
			// Token: 0x040019D2 RID: 6610
			SDL_SCANCODE_DECIMALSEPARATOR,
			// Token: 0x040019D3 RID: 6611
			SDL_SCANCODE_CURRENCYUNIT,
			// Token: 0x040019D4 RID: 6612
			SDL_SCANCODE_CURRENCYSUBUNIT,
			// Token: 0x040019D5 RID: 6613
			SDL_SCANCODE_KP_LEFTPAREN,
			// Token: 0x040019D6 RID: 6614
			SDL_SCANCODE_KP_RIGHTPAREN,
			// Token: 0x040019D7 RID: 6615
			SDL_SCANCODE_KP_LEFTBRACE,
			// Token: 0x040019D8 RID: 6616
			SDL_SCANCODE_KP_RIGHTBRACE,
			// Token: 0x040019D9 RID: 6617
			SDL_SCANCODE_KP_TAB,
			// Token: 0x040019DA RID: 6618
			SDL_SCANCODE_KP_BACKSPACE,
			// Token: 0x040019DB RID: 6619
			SDL_SCANCODE_KP_A,
			// Token: 0x040019DC RID: 6620
			SDL_SCANCODE_KP_B,
			// Token: 0x040019DD RID: 6621
			SDL_SCANCODE_KP_C,
			// Token: 0x040019DE RID: 6622
			SDL_SCANCODE_KP_D,
			// Token: 0x040019DF RID: 6623
			SDL_SCANCODE_KP_E,
			// Token: 0x040019E0 RID: 6624
			SDL_SCANCODE_KP_F,
			// Token: 0x040019E1 RID: 6625
			SDL_SCANCODE_KP_XOR,
			// Token: 0x040019E2 RID: 6626
			SDL_SCANCODE_KP_POWER,
			// Token: 0x040019E3 RID: 6627
			SDL_SCANCODE_KP_PERCENT,
			// Token: 0x040019E4 RID: 6628
			SDL_SCANCODE_KP_LESS,
			// Token: 0x040019E5 RID: 6629
			SDL_SCANCODE_KP_GREATER,
			// Token: 0x040019E6 RID: 6630
			SDL_SCANCODE_KP_AMPERSAND,
			// Token: 0x040019E7 RID: 6631
			SDL_SCANCODE_KP_DBLAMPERSAND,
			// Token: 0x040019E8 RID: 6632
			SDL_SCANCODE_KP_VERTICALBAR,
			// Token: 0x040019E9 RID: 6633
			SDL_SCANCODE_KP_DBLVERTICALBAR,
			// Token: 0x040019EA RID: 6634
			SDL_SCANCODE_KP_COLON,
			// Token: 0x040019EB RID: 6635
			SDL_SCANCODE_KP_HASH,
			// Token: 0x040019EC RID: 6636
			SDL_SCANCODE_KP_SPACE,
			// Token: 0x040019ED RID: 6637
			SDL_SCANCODE_KP_AT,
			// Token: 0x040019EE RID: 6638
			SDL_SCANCODE_KP_EXCLAM,
			// Token: 0x040019EF RID: 6639
			SDL_SCANCODE_KP_MEMSTORE,
			// Token: 0x040019F0 RID: 6640
			SDL_SCANCODE_KP_MEMRECALL,
			// Token: 0x040019F1 RID: 6641
			SDL_SCANCODE_KP_MEMCLEAR,
			// Token: 0x040019F2 RID: 6642
			SDL_SCANCODE_KP_MEMADD,
			// Token: 0x040019F3 RID: 6643
			SDL_SCANCODE_KP_MEMSUBTRACT,
			// Token: 0x040019F4 RID: 6644
			SDL_SCANCODE_KP_MEMMULTIPLY,
			// Token: 0x040019F5 RID: 6645
			SDL_SCANCODE_KP_MEMDIVIDE,
			// Token: 0x040019F6 RID: 6646
			SDL_SCANCODE_KP_PLUSMINUS,
			// Token: 0x040019F7 RID: 6647
			SDL_SCANCODE_KP_CLEAR,
			// Token: 0x040019F8 RID: 6648
			SDL_SCANCODE_KP_CLEARENTRY,
			// Token: 0x040019F9 RID: 6649
			SDL_SCANCODE_KP_BINARY,
			// Token: 0x040019FA RID: 6650
			SDL_SCANCODE_KP_OCTAL,
			// Token: 0x040019FB RID: 6651
			SDL_SCANCODE_KP_DECIMAL,
			// Token: 0x040019FC RID: 6652
			SDL_SCANCODE_KP_HEXADECIMAL,
			// Token: 0x040019FD RID: 6653
			SDL_SCANCODE_LCTRL = 224,
			// Token: 0x040019FE RID: 6654
			SDL_SCANCODE_LSHIFT,
			// Token: 0x040019FF RID: 6655
			SDL_SCANCODE_LALT,
			// Token: 0x04001A00 RID: 6656
			SDL_SCANCODE_LGUI,
			// Token: 0x04001A01 RID: 6657
			SDL_SCANCODE_RCTRL,
			// Token: 0x04001A02 RID: 6658
			SDL_SCANCODE_RSHIFT,
			// Token: 0x04001A03 RID: 6659
			SDL_SCANCODE_RALT,
			// Token: 0x04001A04 RID: 6660
			SDL_SCANCODE_RGUI,
			// Token: 0x04001A05 RID: 6661
			SDL_SCANCODE_MODE = 257,
			// Token: 0x04001A06 RID: 6662
			SDL_SCANCODE_AUDIONEXT,
			// Token: 0x04001A07 RID: 6663
			SDL_SCANCODE_AUDIOPREV,
			// Token: 0x04001A08 RID: 6664
			SDL_SCANCODE_AUDIOSTOP,
			// Token: 0x04001A09 RID: 6665
			SDL_SCANCODE_AUDIOPLAY,
			// Token: 0x04001A0A RID: 6666
			SDL_SCANCODE_AUDIOMUTE,
			// Token: 0x04001A0B RID: 6667
			SDL_SCANCODE_MEDIASELECT,
			// Token: 0x04001A0C RID: 6668
			SDL_SCANCODE_WWW,
			// Token: 0x04001A0D RID: 6669
			SDL_SCANCODE_MAIL,
			// Token: 0x04001A0E RID: 6670
			SDL_SCANCODE_CALCULATOR,
			// Token: 0x04001A0F RID: 6671
			SDL_SCANCODE_COMPUTER,
			// Token: 0x04001A10 RID: 6672
			SDL_SCANCODE_AC_SEARCH,
			// Token: 0x04001A11 RID: 6673
			SDL_SCANCODE_AC_HOME,
			// Token: 0x04001A12 RID: 6674
			SDL_SCANCODE_AC_BACK,
			// Token: 0x04001A13 RID: 6675
			SDL_SCANCODE_AC_FORWARD,
			// Token: 0x04001A14 RID: 6676
			SDL_SCANCODE_AC_STOP,
			// Token: 0x04001A15 RID: 6677
			SDL_SCANCODE_AC_REFRESH,
			// Token: 0x04001A16 RID: 6678
			SDL_SCANCODE_AC_BOOKMARKS,
			// Token: 0x04001A17 RID: 6679
			SDL_SCANCODE_BRIGHTNESSDOWN,
			// Token: 0x04001A18 RID: 6680
			SDL_SCANCODE_BRIGHTNESSUP,
			// Token: 0x04001A19 RID: 6681
			SDL_SCANCODE_DISPLAYSWITCH,
			// Token: 0x04001A1A RID: 6682
			SDL_SCANCODE_KBDILLUMTOGGLE,
			// Token: 0x04001A1B RID: 6683
			SDL_SCANCODE_KBDILLUMDOWN,
			// Token: 0x04001A1C RID: 6684
			SDL_SCANCODE_KBDILLUMUP,
			// Token: 0x04001A1D RID: 6685
			SDL_SCANCODE_EJECT,
			// Token: 0x04001A1E RID: 6686
			SDL_SCANCODE_SLEEP,
			// Token: 0x04001A1F RID: 6687
			SDL_SCANCODE_APP1,
			// Token: 0x04001A20 RID: 6688
			SDL_SCANCODE_APP2,
			// Token: 0x04001A21 RID: 6689
			SDL_SCANCODE_AUDIOREWIND,
			// Token: 0x04001A22 RID: 6690
			SDL_SCANCODE_AUDIOFASTFORWARD,
			// Token: 0x04001A23 RID: 6691
			SDL_NUM_SCANCODES = 512
		}

		// Token: 0x02000333 RID: 819
		public enum SDL_Keycode
		{
			// Token: 0x04001A25 RID: 6693
			SDLK_UNKNOWN,
			// Token: 0x04001A26 RID: 6694
			SDLK_RETURN = 13,
			// Token: 0x04001A27 RID: 6695
			SDLK_ESCAPE = 27,
			// Token: 0x04001A28 RID: 6696
			SDLK_BACKSPACE = 8,
			// Token: 0x04001A29 RID: 6697
			SDLK_TAB,
			// Token: 0x04001A2A RID: 6698
			SDLK_SPACE = 32,
			// Token: 0x04001A2B RID: 6699
			SDLK_EXCLAIM,
			// Token: 0x04001A2C RID: 6700
			SDLK_QUOTEDBL,
			// Token: 0x04001A2D RID: 6701
			SDLK_HASH,
			// Token: 0x04001A2E RID: 6702
			SDLK_PERCENT = 37,
			// Token: 0x04001A2F RID: 6703
			SDLK_DOLLAR = 36,
			// Token: 0x04001A30 RID: 6704
			SDLK_AMPERSAND = 38,
			// Token: 0x04001A31 RID: 6705
			SDLK_QUOTE,
			// Token: 0x04001A32 RID: 6706
			SDLK_LEFTPAREN,
			// Token: 0x04001A33 RID: 6707
			SDLK_RIGHTPAREN,
			// Token: 0x04001A34 RID: 6708
			SDLK_ASTERISK,
			// Token: 0x04001A35 RID: 6709
			SDLK_PLUS,
			// Token: 0x04001A36 RID: 6710
			SDLK_COMMA,
			// Token: 0x04001A37 RID: 6711
			SDLK_MINUS,
			// Token: 0x04001A38 RID: 6712
			SDLK_PERIOD,
			// Token: 0x04001A39 RID: 6713
			SDLK_SLASH,
			// Token: 0x04001A3A RID: 6714
			SDLK_0,
			// Token: 0x04001A3B RID: 6715
			SDLK_1,
			// Token: 0x04001A3C RID: 6716
			SDLK_2,
			// Token: 0x04001A3D RID: 6717
			SDLK_3,
			// Token: 0x04001A3E RID: 6718
			SDLK_4,
			// Token: 0x04001A3F RID: 6719
			SDLK_5,
			// Token: 0x04001A40 RID: 6720
			SDLK_6,
			// Token: 0x04001A41 RID: 6721
			SDLK_7,
			// Token: 0x04001A42 RID: 6722
			SDLK_8,
			// Token: 0x04001A43 RID: 6723
			SDLK_9,
			// Token: 0x04001A44 RID: 6724
			SDLK_COLON,
			// Token: 0x04001A45 RID: 6725
			SDLK_SEMICOLON,
			// Token: 0x04001A46 RID: 6726
			SDLK_LESS,
			// Token: 0x04001A47 RID: 6727
			SDLK_EQUALS,
			// Token: 0x04001A48 RID: 6728
			SDLK_GREATER,
			// Token: 0x04001A49 RID: 6729
			SDLK_QUESTION,
			// Token: 0x04001A4A RID: 6730
			SDLK_AT,
			// Token: 0x04001A4B RID: 6731
			SDLK_LEFTBRACKET = 91,
			// Token: 0x04001A4C RID: 6732
			SDLK_BACKSLASH,
			// Token: 0x04001A4D RID: 6733
			SDLK_RIGHTBRACKET,
			// Token: 0x04001A4E RID: 6734
			SDLK_CARET,
			// Token: 0x04001A4F RID: 6735
			SDLK_UNDERSCORE,
			// Token: 0x04001A50 RID: 6736
			SDLK_BACKQUOTE,
			// Token: 0x04001A51 RID: 6737
			SDLK_a,
			// Token: 0x04001A52 RID: 6738
			SDLK_b,
			// Token: 0x04001A53 RID: 6739
			SDLK_c,
			// Token: 0x04001A54 RID: 6740
			SDLK_d,
			// Token: 0x04001A55 RID: 6741
			SDLK_e,
			// Token: 0x04001A56 RID: 6742
			SDLK_f,
			// Token: 0x04001A57 RID: 6743
			SDLK_g,
			// Token: 0x04001A58 RID: 6744
			SDLK_h,
			// Token: 0x04001A59 RID: 6745
			SDLK_i,
			// Token: 0x04001A5A RID: 6746
			SDLK_j,
			// Token: 0x04001A5B RID: 6747
			SDLK_k,
			// Token: 0x04001A5C RID: 6748
			SDLK_l,
			// Token: 0x04001A5D RID: 6749
			SDLK_m,
			// Token: 0x04001A5E RID: 6750
			SDLK_n,
			// Token: 0x04001A5F RID: 6751
			SDLK_o,
			// Token: 0x04001A60 RID: 6752
			SDLK_p,
			// Token: 0x04001A61 RID: 6753
			SDLK_q,
			// Token: 0x04001A62 RID: 6754
			SDLK_r,
			// Token: 0x04001A63 RID: 6755
			SDLK_s,
			// Token: 0x04001A64 RID: 6756
			SDLK_t,
			// Token: 0x04001A65 RID: 6757
			SDLK_u,
			// Token: 0x04001A66 RID: 6758
			SDLK_v,
			// Token: 0x04001A67 RID: 6759
			SDLK_w,
			// Token: 0x04001A68 RID: 6760
			SDLK_x,
			// Token: 0x04001A69 RID: 6761
			SDLK_y,
			// Token: 0x04001A6A RID: 6762
			SDLK_z,
			// Token: 0x04001A6B RID: 6763
			SDLK_CAPSLOCK = 1073741881,
			// Token: 0x04001A6C RID: 6764
			SDLK_F1,
			// Token: 0x04001A6D RID: 6765
			SDLK_F2,
			// Token: 0x04001A6E RID: 6766
			SDLK_F3,
			// Token: 0x04001A6F RID: 6767
			SDLK_F4,
			// Token: 0x04001A70 RID: 6768
			SDLK_F5,
			// Token: 0x04001A71 RID: 6769
			SDLK_F6,
			// Token: 0x04001A72 RID: 6770
			SDLK_F7,
			// Token: 0x04001A73 RID: 6771
			SDLK_F8,
			// Token: 0x04001A74 RID: 6772
			SDLK_F9,
			// Token: 0x04001A75 RID: 6773
			SDLK_F10,
			// Token: 0x04001A76 RID: 6774
			SDLK_F11,
			// Token: 0x04001A77 RID: 6775
			SDLK_F12,
			// Token: 0x04001A78 RID: 6776
			SDLK_PRINTSCREEN,
			// Token: 0x04001A79 RID: 6777
			SDLK_SCROLLLOCK,
			// Token: 0x04001A7A RID: 6778
			SDLK_PAUSE,
			// Token: 0x04001A7B RID: 6779
			SDLK_INSERT,
			// Token: 0x04001A7C RID: 6780
			SDLK_HOME,
			// Token: 0x04001A7D RID: 6781
			SDLK_PAGEUP,
			// Token: 0x04001A7E RID: 6782
			SDLK_DELETE = 127,
			// Token: 0x04001A7F RID: 6783
			SDLK_END = 1073741901,
			// Token: 0x04001A80 RID: 6784
			SDLK_PAGEDOWN,
			// Token: 0x04001A81 RID: 6785
			SDLK_RIGHT,
			// Token: 0x04001A82 RID: 6786
			SDLK_LEFT,
			// Token: 0x04001A83 RID: 6787
			SDLK_DOWN,
			// Token: 0x04001A84 RID: 6788
			SDLK_UP,
			// Token: 0x04001A85 RID: 6789
			SDLK_NUMLOCKCLEAR,
			// Token: 0x04001A86 RID: 6790
			SDLK_KP_DIVIDE,
			// Token: 0x04001A87 RID: 6791
			SDLK_KP_MULTIPLY,
			// Token: 0x04001A88 RID: 6792
			SDLK_KP_MINUS,
			// Token: 0x04001A89 RID: 6793
			SDLK_KP_PLUS,
			// Token: 0x04001A8A RID: 6794
			SDLK_KP_ENTER,
			// Token: 0x04001A8B RID: 6795
			SDLK_KP_1,
			// Token: 0x04001A8C RID: 6796
			SDLK_KP_2,
			// Token: 0x04001A8D RID: 6797
			SDLK_KP_3,
			// Token: 0x04001A8E RID: 6798
			SDLK_KP_4,
			// Token: 0x04001A8F RID: 6799
			SDLK_KP_5,
			// Token: 0x04001A90 RID: 6800
			SDLK_KP_6,
			// Token: 0x04001A91 RID: 6801
			SDLK_KP_7,
			// Token: 0x04001A92 RID: 6802
			SDLK_KP_8,
			// Token: 0x04001A93 RID: 6803
			SDLK_KP_9,
			// Token: 0x04001A94 RID: 6804
			SDLK_KP_0,
			// Token: 0x04001A95 RID: 6805
			SDLK_KP_PERIOD,
			// Token: 0x04001A96 RID: 6806
			SDLK_APPLICATION = 1073741925,
			// Token: 0x04001A97 RID: 6807
			SDLK_POWER,
			// Token: 0x04001A98 RID: 6808
			SDLK_KP_EQUALS,
			// Token: 0x04001A99 RID: 6809
			SDLK_F13,
			// Token: 0x04001A9A RID: 6810
			SDLK_F14,
			// Token: 0x04001A9B RID: 6811
			SDLK_F15,
			// Token: 0x04001A9C RID: 6812
			SDLK_F16,
			// Token: 0x04001A9D RID: 6813
			SDLK_F17,
			// Token: 0x04001A9E RID: 6814
			SDLK_F18,
			// Token: 0x04001A9F RID: 6815
			SDLK_F19,
			// Token: 0x04001AA0 RID: 6816
			SDLK_F20,
			// Token: 0x04001AA1 RID: 6817
			SDLK_F21,
			// Token: 0x04001AA2 RID: 6818
			SDLK_F22,
			// Token: 0x04001AA3 RID: 6819
			SDLK_F23,
			// Token: 0x04001AA4 RID: 6820
			SDLK_F24,
			// Token: 0x04001AA5 RID: 6821
			SDLK_EXECUTE,
			// Token: 0x04001AA6 RID: 6822
			SDLK_HELP,
			// Token: 0x04001AA7 RID: 6823
			SDLK_MENU,
			// Token: 0x04001AA8 RID: 6824
			SDLK_SELECT,
			// Token: 0x04001AA9 RID: 6825
			SDLK_STOP,
			// Token: 0x04001AAA RID: 6826
			SDLK_AGAIN,
			// Token: 0x04001AAB RID: 6827
			SDLK_UNDO,
			// Token: 0x04001AAC RID: 6828
			SDLK_CUT,
			// Token: 0x04001AAD RID: 6829
			SDLK_COPY,
			// Token: 0x04001AAE RID: 6830
			SDLK_PASTE,
			// Token: 0x04001AAF RID: 6831
			SDLK_FIND,
			// Token: 0x04001AB0 RID: 6832
			SDLK_MUTE,
			// Token: 0x04001AB1 RID: 6833
			SDLK_VOLUMEUP,
			// Token: 0x04001AB2 RID: 6834
			SDLK_VOLUMEDOWN,
			// Token: 0x04001AB3 RID: 6835
			SDLK_KP_COMMA = 1073741957,
			// Token: 0x04001AB4 RID: 6836
			SDLK_KP_EQUALSAS400,
			// Token: 0x04001AB5 RID: 6837
			SDLK_ALTERASE = 1073741977,
			// Token: 0x04001AB6 RID: 6838
			SDLK_SYSREQ,
			// Token: 0x04001AB7 RID: 6839
			SDLK_CANCEL,
			// Token: 0x04001AB8 RID: 6840
			SDLK_CLEAR,
			// Token: 0x04001AB9 RID: 6841
			SDLK_PRIOR,
			// Token: 0x04001ABA RID: 6842
			SDLK_RETURN2,
			// Token: 0x04001ABB RID: 6843
			SDLK_SEPARATOR,
			// Token: 0x04001ABC RID: 6844
			SDLK_OUT,
			// Token: 0x04001ABD RID: 6845
			SDLK_OPER,
			// Token: 0x04001ABE RID: 6846
			SDLK_CLEARAGAIN,
			// Token: 0x04001ABF RID: 6847
			SDLK_CRSEL,
			// Token: 0x04001AC0 RID: 6848
			SDLK_EXSEL,
			// Token: 0x04001AC1 RID: 6849
			SDLK_KP_00 = 1073742000,
			// Token: 0x04001AC2 RID: 6850
			SDLK_KP_000,
			// Token: 0x04001AC3 RID: 6851
			SDLK_THOUSANDSSEPARATOR,
			// Token: 0x04001AC4 RID: 6852
			SDLK_DECIMALSEPARATOR,
			// Token: 0x04001AC5 RID: 6853
			SDLK_CURRENCYUNIT,
			// Token: 0x04001AC6 RID: 6854
			SDLK_CURRENCYSUBUNIT,
			// Token: 0x04001AC7 RID: 6855
			SDLK_KP_LEFTPAREN,
			// Token: 0x04001AC8 RID: 6856
			SDLK_KP_RIGHTPAREN,
			// Token: 0x04001AC9 RID: 6857
			SDLK_KP_LEFTBRACE,
			// Token: 0x04001ACA RID: 6858
			SDLK_KP_RIGHTBRACE,
			// Token: 0x04001ACB RID: 6859
			SDLK_KP_TAB,
			// Token: 0x04001ACC RID: 6860
			SDLK_KP_BACKSPACE,
			// Token: 0x04001ACD RID: 6861
			SDLK_KP_A,
			// Token: 0x04001ACE RID: 6862
			SDLK_KP_B,
			// Token: 0x04001ACF RID: 6863
			SDLK_KP_C,
			// Token: 0x04001AD0 RID: 6864
			SDLK_KP_D,
			// Token: 0x04001AD1 RID: 6865
			SDLK_KP_E,
			// Token: 0x04001AD2 RID: 6866
			SDLK_KP_F,
			// Token: 0x04001AD3 RID: 6867
			SDLK_KP_XOR,
			// Token: 0x04001AD4 RID: 6868
			SDLK_KP_POWER,
			// Token: 0x04001AD5 RID: 6869
			SDLK_KP_PERCENT,
			// Token: 0x04001AD6 RID: 6870
			SDLK_KP_LESS,
			// Token: 0x04001AD7 RID: 6871
			SDLK_KP_GREATER,
			// Token: 0x04001AD8 RID: 6872
			SDLK_KP_AMPERSAND,
			// Token: 0x04001AD9 RID: 6873
			SDLK_KP_DBLAMPERSAND,
			// Token: 0x04001ADA RID: 6874
			SDLK_KP_VERTICALBAR,
			// Token: 0x04001ADB RID: 6875
			SDLK_KP_DBLVERTICALBAR,
			// Token: 0x04001ADC RID: 6876
			SDLK_KP_COLON,
			// Token: 0x04001ADD RID: 6877
			SDLK_KP_HASH,
			// Token: 0x04001ADE RID: 6878
			SDLK_KP_SPACE,
			// Token: 0x04001ADF RID: 6879
			SDLK_KP_AT,
			// Token: 0x04001AE0 RID: 6880
			SDLK_KP_EXCLAM,
			// Token: 0x04001AE1 RID: 6881
			SDLK_KP_MEMSTORE,
			// Token: 0x04001AE2 RID: 6882
			SDLK_KP_MEMRECALL,
			// Token: 0x04001AE3 RID: 6883
			SDLK_KP_MEMCLEAR,
			// Token: 0x04001AE4 RID: 6884
			SDLK_KP_MEMADD,
			// Token: 0x04001AE5 RID: 6885
			SDLK_KP_MEMSUBTRACT,
			// Token: 0x04001AE6 RID: 6886
			SDLK_KP_MEMMULTIPLY,
			// Token: 0x04001AE7 RID: 6887
			SDLK_KP_MEMDIVIDE,
			// Token: 0x04001AE8 RID: 6888
			SDLK_KP_PLUSMINUS,
			// Token: 0x04001AE9 RID: 6889
			SDLK_KP_CLEAR,
			// Token: 0x04001AEA RID: 6890
			SDLK_KP_CLEARENTRY,
			// Token: 0x04001AEB RID: 6891
			SDLK_KP_BINARY,
			// Token: 0x04001AEC RID: 6892
			SDLK_KP_OCTAL,
			// Token: 0x04001AED RID: 6893
			SDLK_KP_DECIMAL,
			// Token: 0x04001AEE RID: 6894
			SDLK_KP_HEXADECIMAL,
			// Token: 0x04001AEF RID: 6895
			SDLK_LCTRL = 1073742048,
			// Token: 0x04001AF0 RID: 6896
			SDLK_LSHIFT,
			// Token: 0x04001AF1 RID: 6897
			SDLK_LALT,
			// Token: 0x04001AF2 RID: 6898
			SDLK_LGUI,
			// Token: 0x04001AF3 RID: 6899
			SDLK_RCTRL,
			// Token: 0x04001AF4 RID: 6900
			SDLK_RSHIFT,
			// Token: 0x04001AF5 RID: 6901
			SDLK_RALT,
			// Token: 0x04001AF6 RID: 6902
			SDLK_RGUI,
			// Token: 0x04001AF7 RID: 6903
			SDLK_MODE = 1073742081,
			// Token: 0x04001AF8 RID: 6904
			SDLK_AUDIONEXT,
			// Token: 0x04001AF9 RID: 6905
			SDLK_AUDIOPREV,
			// Token: 0x04001AFA RID: 6906
			SDLK_AUDIOSTOP,
			// Token: 0x04001AFB RID: 6907
			SDLK_AUDIOPLAY,
			// Token: 0x04001AFC RID: 6908
			SDLK_AUDIOMUTE,
			// Token: 0x04001AFD RID: 6909
			SDLK_MEDIASELECT,
			// Token: 0x04001AFE RID: 6910
			SDLK_WWW,
			// Token: 0x04001AFF RID: 6911
			SDLK_MAIL,
			// Token: 0x04001B00 RID: 6912
			SDLK_CALCULATOR,
			// Token: 0x04001B01 RID: 6913
			SDLK_COMPUTER,
			// Token: 0x04001B02 RID: 6914
			SDLK_AC_SEARCH,
			// Token: 0x04001B03 RID: 6915
			SDLK_AC_HOME,
			// Token: 0x04001B04 RID: 6916
			SDLK_AC_BACK,
			// Token: 0x04001B05 RID: 6917
			SDLK_AC_FORWARD,
			// Token: 0x04001B06 RID: 6918
			SDLK_AC_STOP,
			// Token: 0x04001B07 RID: 6919
			SDLK_AC_REFRESH,
			// Token: 0x04001B08 RID: 6920
			SDLK_AC_BOOKMARKS,
			// Token: 0x04001B09 RID: 6921
			SDLK_BRIGHTNESSDOWN,
			// Token: 0x04001B0A RID: 6922
			SDLK_BRIGHTNESSUP,
			// Token: 0x04001B0B RID: 6923
			SDLK_DISPLAYSWITCH,
			// Token: 0x04001B0C RID: 6924
			SDLK_KBDILLUMTOGGLE,
			// Token: 0x04001B0D RID: 6925
			SDLK_KBDILLUMDOWN,
			// Token: 0x04001B0E RID: 6926
			SDLK_KBDILLUMUP,
			// Token: 0x04001B0F RID: 6927
			SDLK_EJECT,
			// Token: 0x04001B10 RID: 6928
			SDLK_SLEEP,
			// Token: 0x04001B11 RID: 6929
			SDLK_APP1,
			// Token: 0x04001B12 RID: 6930
			SDLK_APP2,
			// Token: 0x04001B13 RID: 6931
			SDLK_AUDIOREWIND,
			// Token: 0x04001B14 RID: 6932
			SDLK_AUDIOFASTFORWARD
		}

		// Token: 0x02000334 RID: 820
		[Flags]
		public enum SDL_Keymod : ushort
		{
			// Token: 0x04001B16 RID: 6934
			KMOD_NONE = 0,
			// Token: 0x04001B17 RID: 6935
			KMOD_LSHIFT = 1,
			// Token: 0x04001B18 RID: 6936
			KMOD_RSHIFT = 2,
			// Token: 0x04001B19 RID: 6937
			KMOD_LCTRL = 64,
			// Token: 0x04001B1A RID: 6938
			KMOD_RCTRL = 128,
			// Token: 0x04001B1B RID: 6939
			KMOD_LALT = 256,
			// Token: 0x04001B1C RID: 6940
			KMOD_RALT = 512,
			// Token: 0x04001B1D RID: 6941
			KMOD_LGUI = 1024,
			// Token: 0x04001B1E RID: 6942
			KMOD_RGUI = 2048,
			// Token: 0x04001B1F RID: 6943
			KMOD_NUM = 4096,
			// Token: 0x04001B20 RID: 6944
			KMOD_CAPS = 8192,
			// Token: 0x04001B21 RID: 6945
			KMOD_MODE = 16384,
			// Token: 0x04001B22 RID: 6946
			KMOD_SCROLL = 32768,
			// Token: 0x04001B23 RID: 6947
			KMOD_CTRL = 192,
			// Token: 0x04001B24 RID: 6948
			KMOD_SHIFT = 3,
			// Token: 0x04001B25 RID: 6949
			KMOD_ALT = 768,
			// Token: 0x04001B26 RID: 6950
			KMOD_GUI = 3072,
			// Token: 0x04001B27 RID: 6951
			KMOD_RESERVED = 32768
		}

		// Token: 0x02000335 RID: 821
		public struct SDL_Keysym
		{
			// Token: 0x04001B28 RID: 6952
			public SDL.SDL_Scancode scancode;

			// Token: 0x04001B29 RID: 6953
			public SDL.SDL_Keycode sym;

			// Token: 0x04001B2A RID: 6954
			public SDL.SDL_Keymod mod;

			// Token: 0x04001B2B RID: 6955
			public uint unicode;
		}

		// Token: 0x02000336 RID: 822
		public enum SDL_SystemCursor
		{
			// Token: 0x04001B2D RID: 6957
			SDL_SYSTEM_CURSOR_ARROW,
			// Token: 0x04001B2E RID: 6958
			SDL_SYSTEM_CURSOR_IBEAM,
			// Token: 0x04001B2F RID: 6959
			SDL_SYSTEM_CURSOR_WAIT,
			// Token: 0x04001B30 RID: 6960
			SDL_SYSTEM_CURSOR_CROSSHAIR,
			// Token: 0x04001B31 RID: 6961
			SDL_SYSTEM_CURSOR_WAITARROW,
			// Token: 0x04001B32 RID: 6962
			SDL_SYSTEM_CURSOR_SIZENWSE,
			// Token: 0x04001B33 RID: 6963
			SDL_SYSTEM_CURSOR_SIZENESW,
			// Token: 0x04001B34 RID: 6964
			SDL_SYSTEM_CURSOR_SIZEWE,
			// Token: 0x04001B35 RID: 6965
			SDL_SYSTEM_CURSOR_SIZENS,
			// Token: 0x04001B36 RID: 6966
			SDL_SYSTEM_CURSOR_SIZEALL,
			// Token: 0x04001B37 RID: 6967
			SDL_SYSTEM_CURSOR_NO,
			// Token: 0x04001B38 RID: 6968
			SDL_SYSTEM_CURSOR_HAND,
			// Token: 0x04001B39 RID: 6969
			SDL_NUM_SYSTEM_CURSORS
		}

		// Token: 0x02000337 RID: 823
		public struct SDL_Finger
		{
			// Token: 0x04001B3A RID: 6970
			public long id;

			// Token: 0x04001B3B RID: 6971
			public float x;

			// Token: 0x04001B3C RID: 6972
			public float y;

			// Token: 0x04001B3D RID: 6973
			public float pressure;
		}

		// Token: 0x02000338 RID: 824
		public enum SDL_TouchDeviceType
		{
			// Token: 0x04001B3F RID: 6975
			SDL_TOUCH_DEVICE_INVALID = -1,
			// Token: 0x04001B40 RID: 6976
			SDL_TOUCH_DEVICE_DIRECT,
			// Token: 0x04001B41 RID: 6977
			SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
			// Token: 0x04001B42 RID: 6978
			SDL_TOUCH_DEVICE_INDIRECT_RELATIVE
		}

		// Token: 0x02000339 RID: 825
		public enum SDL_JoystickPowerLevel
		{
			// Token: 0x04001B44 RID: 6980
			SDL_JOYSTICK_POWER_UNKNOWN = -1,
			// Token: 0x04001B45 RID: 6981
			SDL_JOYSTICK_POWER_EMPTY,
			// Token: 0x04001B46 RID: 6982
			SDL_JOYSTICK_POWER_LOW,
			// Token: 0x04001B47 RID: 6983
			SDL_JOYSTICK_POWER_MEDIUM,
			// Token: 0x04001B48 RID: 6984
			SDL_JOYSTICK_POWER_FULL,
			// Token: 0x04001B49 RID: 6985
			SDL_JOYSTICK_POWER_WIRED,
			// Token: 0x04001B4A RID: 6986
			SDL_JOYSTICK_POWER_MAX
		}

		// Token: 0x0200033A RID: 826
		public enum SDL_JoystickType
		{
			// Token: 0x04001B4C RID: 6988
			SDL_JOYSTICK_TYPE_UNKNOWN,
			// Token: 0x04001B4D RID: 6989
			SDL_JOYSTICK_TYPE_GAMECONTROLLER,
			// Token: 0x04001B4E RID: 6990
			SDL_JOYSTICK_TYPE_WHEEL,
			// Token: 0x04001B4F RID: 6991
			SDL_JOYSTICK_TYPE_ARCADE_STICK,
			// Token: 0x04001B50 RID: 6992
			SDL_JOYSTICK_TYPE_FLIGHT_STICK,
			// Token: 0x04001B51 RID: 6993
			SDL_JOYSTICK_TYPE_DANCE_PAD,
			// Token: 0x04001B52 RID: 6994
			SDL_JOYSTICK_TYPE_GUITAR,
			// Token: 0x04001B53 RID: 6995
			SDL_JOYSTICK_TYPE_DRUM_KIT,
			// Token: 0x04001B54 RID: 6996
			SDL_JOYSTICK_TYPE_ARCADE_PAD
		}

		// Token: 0x0200033B RID: 827
		public enum SDL_GameControllerBindType
		{
			// Token: 0x04001B56 RID: 6998
			SDL_CONTROLLER_BINDTYPE_NONE,
			// Token: 0x04001B57 RID: 6999
			SDL_CONTROLLER_BINDTYPE_BUTTON,
			// Token: 0x04001B58 RID: 7000
			SDL_CONTROLLER_BINDTYPE_AXIS,
			// Token: 0x04001B59 RID: 7001
			SDL_CONTROLLER_BINDTYPE_HAT
		}

		// Token: 0x0200033C RID: 828
		public enum SDL_GameControllerAxis
		{
			// Token: 0x04001B5B RID: 7003
			SDL_CONTROLLER_AXIS_INVALID = -1,
			// Token: 0x04001B5C RID: 7004
			SDL_CONTROLLER_AXIS_LEFTX,
			// Token: 0x04001B5D RID: 7005
			SDL_CONTROLLER_AXIS_LEFTY,
			// Token: 0x04001B5E RID: 7006
			SDL_CONTROLLER_AXIS_RIGHTX,
			// Token: 0x04001B5F RID: 7007
			SDL_CONTROLLER_AXIS_RIGHTY,
			// Token: 0x04001B60 RID: 7008
			SDL_CONTROLLER_AXIS_TRIGGERLEFT,
			// Token: 0x04001B61 RID: 7009
			SDL_CONTROLLER_AXIS_TRIGGERRIGHT,
			// Token: 0x04001B62 RID: 7010
			SDL_CONTROLLER_AXIS_MAX
		}

		// Token: 0x0200033D RID: 829
		public enum SDL_GameControllerButton
		{
			// Token: 0x04001B64 RID: 7012
			SDL_CONTROLLER_BUTTON_INVALID = -1,
			// Token: 0x04001B65 RID: 7013
			SDL_CONTROLLER_BUTTON_A,
			// Token: 0x04001B66 RID: 7014
			SDL_CONTROLLER_BUTTON_B,
			// Token: 0x04001B67 RID: 7015
			SDL_CONTROLLER_BUTTON_X,
			// Token: 0x04001B68 RID: 7016
			SDL_CONTROLLER_BUTTON_Y,
			// Token: 0x04001B69 RID: 7017
			SDL_CONTROLLER_BUTTON_BACK,
			// Token: 0x04001B6A RID: 7018
			SDL_CONTROLLER_BUTTON_GUIDE,
			// Token: 0x04001B6B RID: 7019
			SDL_CONTROLLER_BUTTON_START,
			// Token: 0x04001B6C RID: 7020
			SDL_CONTROLLER_BUTTON_LEFTSTICK,
			// Token: 0x04001B6D RID: 7021
			SDL_CONTROLLER_BUTTON_RIGHTSTICK,
			// Token: 0x04001B6E RID: 7022
			SDL_CONTROLLER_BUTTON_LEFTSHOULDER,
			// Token: 0x04001B6F RID: 7023
			SDL_CONTROLLER_BUTTON_RIGHTSHOULDER,
			// Token: 0x04001B70 RID: 7024
			SDL_CONTROLLER_BUTTON_DPAD_UP,
			// Token: 0x04001B71 RID: 7025
			SDL_CONTROLLER_BUTTON_DPAD_DOWN,
			// Token: 0x04001B72 RID: 7026
			SDL_CONTROLLER_BUTTON_DPAD_LEFT,
			// Token: 0x04001B73 RID: 7027
			SDL_CONTROLLER_BUTTON_DPAD_RIGHT,
			// Token: 0x04001B74 RID: 7028
			SDL_CONTROLLER_BUTTON_MISC1,
			// Token: 0x04001B75 RID: 7029
			SDL_CONTROLLER_BUTTON_PADDLE1,
			// Token: 0x04001B76 RID: 7030
			SDL_CONTROLLER_BUTTON_PADDLE2,
			// Token: 0x04001B77 RID: 7031
			SDL_CONTROLLER_BUTTON_PADDLE3,
			// Token: 0x04001B78 RID: 7032
			SDL_CONTROLLER_BUTTON_PADDLE4,
			// Token: 0x04001B79 RID: 7033
			SDL_CONTROLLER_BUTTON_TOUCHPAD,
			// Token: 0x04001B7A RID: 7034
			SDL_CONTROLLER_BUTTON_MAX
		}

		// Token: 0x0200033E RID: 830
		public enum SDL_GameControllerType
		{
			// Token: 0x04001B7C RID: 7036
			SDL_CONTROLLER_TYPE_UNKNOWN,
			// Token: 0x04001B7D RID: 7037
			SDL_CONTROLLER_TYPE_XBOX360,
			// Token: 0x04001B7E RID: 7038
			SDL_CONTROLLER_TYPE_XBOXONE,
			// Token: 0x04001B7F RID: 7039
			SDL_CONTROLLER_TYPE_PS3,
			// Token: 0x04001B80 RID: 7040
			SDL_CONTROLLER_TYPE_PS4,
			// Token: 0x04001B81 RID: 7041
			SDL_CONTROLLER_TYPE_NINTENDO_SWITCH_PRO,
			// Token: 0x04001B82 RID: 7042
			SDL_CONTROLLER_TYPE_VIRTUAL,
			// Token: 0x04001B83 RID: 7043
			SDL_CONTROLLER_TYPE_PS5,
			// Token: 0x04001B84 RID: 7044
			SDL_CONTROLLER_TYPE_AMAZON_LUNA,
			// Token: 0x04001B85 RID: 7045
			SDL_CONTROLLER_TYPE_GOOGLE_STADIA
		}

		// Token: 0x0200033F RID: 831
		public struct INTERNAL_GameControllerButtonBind_hat
		{
			// Token: 0x04001B86 RID: 7046
			public int hat;

			// Token: 0x04001B87 RID: 7047
			public int hat_mask;
		}

		// Token: 0x02000340 RID: 832
		[StructLayout(LayoutKind.Explicit)]
		public struct INTERNAL_GameControllerButtonBind_union
		{
			// Token: 0x04001B88 RID: 7048
			[FieldOffset(0)]
			public int button;

			// Token: 0x04001B89 RID: 7049
			[FieldOffset(0)]
			public int axis;

			// Token: 0x04001B8A RID: 7050
			[FieldOffset(0)]
			public SDL.INTERNAL_GameControllerButtonBind_hat hat;
		}

		// Token: 0x02000341 RID: 833
		public struct SDL_GameControllerButtonBind
		{
			// Token: 0x04001B8B RID: 7051
			public SDL.SDL_GameControllerBindType bindType;

			// Token: 0x04001B8C RID: 7052
			public SDL.INTERNAL_GameControllerButtonBind_union value;
		}

		// Token: 0x02000342 RID: 834
		private struct INTERNAL_SDL_GameControllerButtonBind
		{
			// Token: 0x04001B8D RID: 7053
			public int bindType;

			// Token: 0x04001B8E RID: 7054
			public int unionVal0;

			// Token: 0x04001B8F RID: 7055
			public int unionVal1;
		}

		// Token: 0x02000343 RID: 835
		public struct SDL_HapticDirection
		{
			// Token: 0x04001B90 RID: 7056
			public byte type;

			// Token: 0x04001B91 RID: 7057
			[FixedBuffer(typeof(int), 3)]
			public SDL.SDL_HapticDirection.<dir>e__FixedBuffer dir;

			// Token: 0x0200040A RID: 1034
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 12)]
			public struct <dir>e__FixedBuffer
			{
				// Token: 0x04001E3A RID: 7738
				public int FixedElementField;
			}
		}

		// Token: 0x02000344 RID: 836
		public struct SDL_HapticConstant
		{
			// Token: 0x04001B92 RID: 7058
			public ushort type;

			// Token: 0x04001B93 RID: 7059
			public SDL.SDL_HapticDirection direction;

			// Token: 0x04001B94 RID: 7060
			public uint length;

			// Token: 0x04001B95 RID: 7061
			public ushort delay;

			// Token: 0x04001B96 RID: 7062
			public ushort button;

			// Token: 0x04001B97 RID: 7063
			public ushort interval;

			// Token: 0x04001B98 RID: 7064
			public short level;

			// Token: 0x04001B99 RID: 7065
			public ushort attack_length;

			// Token: 0x04001B9A RID: 7066
			public ushort attack_level;

			// Token: 0x04001B9B RID: 7067
			public ushort fade_length;

			// Token: 0x04001B9C RID: 7068
			public ushort fade_level;
		}

		// Token: 0x02000345 RID: 837
		public struct SDL_HapticPeriodic
		{
			// Token: 0x04001B9D RID: 7069
			public ushort type;

			// Token: 0x04001B9E RID: 7070
			public SDL.SDL_HapticDirection direction;

			// Token: 0x04001B9F RID: 7071
			public uint length;

			// Token: 0x04001BA0 RID: 7072
			public ushort delay;

			// Token: 0x04001BA1 RID: 7073
			public ushort button;

			// Token: 0x04001BA2 RID: 7074
			public ushort interval;

			// Token: 0x04001BA3 RID: 7075
			public ushort period;

			// Token: 0x04001BA4 RID: 7076
			public short magnitude;

			// Token: 0x04001BA5 RID: 7077
			public short offset;

			// Token: 0x04001BA6 RID: 7078
			public ushort phase;

			// Token: 0x04001BA7 RID: 7079
			public ushort attack_length;

			// Token: 0x04001BA8 RID: 7080
			public ushort attack_level;

			// Token: 0x04001BA9 RID: 7081
			public ushort fade_length;

			// Token: 0x04001BAA RID: 7082
			public ushort fade_level;
		}

		// Token: 0x02000346 RID: 838
		public struct SDL_HapticCondition
		{
			// Token: 0x04001BAB RID: 7083
			public ushort type;

			// Token: 0x04001BAC RID: 7084
			public SDL.SDL_HapticDirection direction;

			// Token: 0x04001BAD RID: 7085
			public uint length;

			// Token: 0x04001BAE RID: 7086
			public ushort delay;

			// Token: 0x04001BAF RID: 7087
			public ushort button;

			// Token: 0x04001BB0 RID: 7088
			public ushort interval;

			// Token: 0x04001BB1 RID: 7089
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<right_sat>e__FixedBuffer right_sat;

			// Token: 0x04001BB2 RID: 7090
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<left_sat>e__FixedBuffer left_sat;

			// Token: 0x04001BB3 RID: 7091
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<right_coeff>e__FixedBuffer right_coeff;

			// Token: 0x04001BB4 RID: 7092
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<left_coeff>e__FixedBuffer left_coeff;

			// Token: 0x04001BB5 RID: 7093
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<deadband>e__FixedBuffer deadband;

			// Token: 0x04001BB6 RID: 7094
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<center>e__FixedBuffer center;

			// Token: 0x0200040B RID: 1035
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <center>e__FixedBuffer
			{
				// Token: 0x04001E3B RID: 7739
				public short FixedElementField;
			}

			// Token: 0x0200040C RID: 1036
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <deadband>e__FixedBuffer
			{
				// Token: 0x04001E3C RID: 7740
				public ushort FixedElementField;
			}

			// Token: 0x0200040D RID: 1037
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <left_coeff>e__FixedBuffer
			{
				// Token: 0x04001E3D RID: 7741
				public short FixedElementField;
			}

			// Token: 0x0200040E RID: 1038
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <left_sat>e__FixedBuffer
			{
				// Token: 0x04001E3E RID: 7742
				public ushort FixedElementField;
			}

			// Token: 0x0200040F RID: 1039
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <right_coeff>e__FixedBuffer
			{
				// Token: 0x04001E3F RID: 7743
				public short FixedElementField;
			}

			// Token: 0x02000410 RID: 1040
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <right_sat>e__FixedBuffer
			{
				// Token: 0x04001E40 RID: 7744
				public ushort FixedElementField;
			}
		}

		// Token: 0x02000347 RID: 839
		public struct SDL_HapticRamp
		{
			// Token: 0x04001BB7 RID: 7095
			public ushort type;

			// Token: 0x04001BB8 RID: 7096
			public SDL.SDL_HapticDirection direction;

			// Token: 0x04001BB9 RID: 7097
			public uint length;

			// Token: 0x04001BBA RID: 7098
			public ushort delay;

			// Token: 0x04001BBB RID: 7099
			public ushort button;

			// Token: 0x04001BBC RID: 7100
			public ushort interval;

			// Token: 0x04001BBD RID: 7101
			public short start;

			// Token: 0x04001BBE RID: 7102
			public short end;

			// Token: 0x04001BBF RID: 7103
			public ushort attack_length;

			// Token: 0x04001BC0 RID: 7104
			public ushort attack_level;

			// Token: 0x04001BC1 RID: 7105
			public ushort fade_length;

			// Token: 0x04001BC2 RID: 7106
			public ushort fade_level;
		}

		// Token: 0x02000348 RID: 840
		public struct SDL_HapticLeftRight
		{
			// Token: 0x04001BC3 RID: 7107
			public ushort type;

			// Token: 0x04001BC4 RID: 7108
			public uint length;

			// Token: 0x04001BC5 RID: 7109
			public ushort large_magnitude;

			// Token: 0x04001BC6 RID: 7110
			public ushort small_magnitude;
		}

		// Token: 0x02000349 RID: 841
		public struct SDL_HapticCustom
		{
			// Token: 0x04001BC7 RID: 7111
			public ushort type;

			// Token: 0x04001BC8 RID: 7112
			public SDL.SDL_HapticDirection direction;

			// Token: 0x04001BC9 RID: 7113
			public uint length;

			// Token: 0x04001BCA RID: 7114
			public ushort delay;

			// Token: 0x04001BCB RID: 7115
			public ushort button;

			// Token: 0x04001BCC RID: 7116
			public ushort interval;

			// Token: 0x04001BCD RID: 7117
			public byte channels;

			// Token: 0x04001BCE RID: 7118
			public ushort period;

			// Token: 0x04001BCF RID: 7119
			public ushort samples;

			// Token: 0x04001BD0 RID: 7120
			public IntPtr data;

			// Token: 0x04001BD1 RID: 7121
			public ushort attack_length;

			// Token: 0x04001BD2 RID: 7122
			public ushort attack_level;

			// Token: 0x04001BD3 RID: 7123
			public ushort fade_length;

			// Token: 0x04001BD4 RID: 7124
			public ushort fade_level;
		}

		// Token: 0x0200034A RID: 842
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_HapticEffect
		{
			// Token: 0x04001BD5 RID: 7125
			[FieldOffset(0)]
			public ushort type;

			// Token: 0x04001BD6 RID: 7126
			[FieldOffset(0)]
			public SDL.SDL_HapticConstant constant;

			// Token: 0x04001BD7 RID: 7127
			[FieldOffset(0)]
			public SDL.SDL_HapticPeriodic periodic;

			// Token: 0x04001BD8 RID: 7128
			[FieldOffset(0)]
			public SDL.SDL_HapticCondition condition;

			// Token: 0x04001BD9 RID: 7129
			[FieldOffset(0)]
			public SDL.SDL_HapticRamp ramp;

			// Token: 0x04001BDA RID: 7130
			[FieldOffset(0)]
			public SDL.SDL_HapticLeftRight leftright;

			// Token: 0x04001BDB RID: 7131
			[FieldOffset(0)]
			public SDL.SDL_HapticCustom custom;
		}

		// Token: 0x0200034B RID: 843
		public enum SDL_SensorType
		{
			// Token: 0x04001BDD RID: 7133
			SDL_SENSOR_INVALID = -1,
			// Token: 0x04001BDE RID: 7134
			SDL_SENSOR_UNKNOWN,
			// Token: 0x04001BDF RID: 7135
			SDL_SENSOR_ACCEL,
			// Token: 0x04001BE0 RID: 7136
			SDL_SENSOR_GYRO
		}

		// Token: 0x0200034C RID: 844
		public enum SDL_AudioStatus
		{
			// Token: 0x04001BE2 RID: 7138
			SDL_AUDIO_STOPPED,
			// Token: 0x04001BE3 RID: 7139
			SDL_AUDIO_PLAYING,
			// Token: 0x04001BE4 RID: 7140
			SDL_AUDIO_PAUSED
		}

		// Token: 0x0200034D RID: 845
		public struct SDL_AudioSpec
		{
			// Token: 0x04001BE5 RID: 7141
			public int freq;

			// Token: 0x04001BE6 RID: 7142
			public ushort format;

			// Token: 0x04001BE7 RID: 7143
			public byte channels;

			// Token: 0x04001BE8 RID: 7144
			public byte silence;

			// Token: 0x04001BE9 RID: 7145
			public ushort samples;

			// Token: 0x04001BEA RID: 7146
			public uint size;

			// Token: 0x04001BEB RID: 7147
			public SDL.SDL_AudioCallback callback;

			// Token: 0x04001BEC RID: 7148
			public IntPtr userdata;
		}

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x060019BC RID: 6588
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioCallback(IntPtr userdata, IntPtr stream, int len);

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x060019C0 RID: 6592
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint SDL_TimerCallback(uint interval, IntPtr param);

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x060019C4 RID: 6596
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDL_WindowsMessageHook(IntPtr userdata, IntPtr hWnd, uint message, ulong wParam, long lParam);

		// Token: 0x02000351 RID: 849
		// (Invoke) Token: 0x060019C8 RID: 6600
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_iPhoneAnimationCallback(IntPtr p);

		// Token: 0x02000352 RID: 850
		public enum SDL_WinRT_DeviceFamily
		{
			// Token: 0x04001BEE RID: 7150
			SDL_WINRT_DEVICEFAMILY_UNKNOWN,
			// Token: 0x04001BEF RID: 7151
			SDL_WINRT_DEVICEFAMILY_DESKTOP,
			// Token: 0x04001BF0 RID: 7152
			SDL_WINRT_DEVICEFAMILY_MOBILE,
			// Token: 0x04001BF1 RID: 7153
			SDL_WINRT_DEVICEFAMILY_XBOX
		}

		// Token: 0x02000353 RID: 851
		public enum SDL_SYSWM_TYPE
		{
			// Token: 0x04001BF3 RID: 7155
			SDL_SYSWM_UNKNOWN,
			// Token: 0x04001BF4 RID: 7156
			SDL_SYSWM_WINDOWS,
			// Token: 0x04001BF5 RID: 7157
			SDL_SYSWM_X11,
			// Token: 0x04001BF6 RID: 7158
			SDL_SYSWM_DIRECTFB,
			// Token: 0x04001BF7 RID: 7159
			SDL_SYSWM_COCOA,
			// Token: 0x04001BF8 RID: 7160
			SDL_SYSWM_UIKIT,
			// Token: 0x04001BF9 RID: 7161
			SDL_SYSWM_WAYLAND,
			// Token: 0x04001BFA RID: 7162
			SDL_SYSWM_MIR,
			// Token: 0x04001BFB RID: 7163
			SDL_SYSWM_WINRT,
			// Token: 0x04001BFC RID: 7164
			SDL_SYSWM_ANDROID,
			// Token: 0x04001BFD RID: 7165
			SDL_SYSWM_VIVANTE,
			// Token: 0x04001BFE RID: 7166
			SDL_SYSWM_OS2,
			// Token: 0x04001BFF RID: 7167
			SDL_SYSWM_HAIKU,
			// Token: 0x04001C00 RID: 7168
			SDL_SYSWM_KMSDRM
		}

		// Token: 0x02000354 RID: 852
		public struct INTERNAL_windows_wminfo
		{
			// Token: 0x04001C01 RID: 7169
			public IntPtr window;

			// Token: 0x04001C02 RID: 7170
			public IntPtr hdc;

			// Token: 0x04001C03 RID: 7171
			public IntPtr hinstance;
		}

		// Token: 0x02000355 RID: 853
		public struct INTERNAL_winrt_wminfo
		{
			// Token: 0x04001C04 RID: 7172
			public IntPtr window;
		}

		// Token: 0x02000356 RID: 854
		public struct INTERNAL_x11_wminfo
		{
			// Token: 0x04001C05 RID: 7173
			public IntPtr display;

			// Token: 0x04001C06 RID: 7174
			public IntPtr window;
		}

		// Token: 0x02000357 RID: 855
		public struct INTERNAL_directfb_wminfo
		{
			// Token: 0x04001C07 RID: 7175
			public IntPtr dfb;

			// Token: 0x04001C08 RID: 7176
			public IntPtr window;

			// Token: 0x04001C09 RID: 7177
			public IntPtr surface;
		}

		// Token: 0x02000358 RID: 856
		public struct INTERNAL_cocoa_wminfo
		{
			// Token: 0x04001C0A RID: 7178
			public IntPtr window;
		}

		// Token: 0x02000359 RID: 857
		public struct INTERNAL_uikit_wminfo
		{
			// Token: 0x04001C0B RID: 7179
			public IntPtr window;

			// Token: 0x04001C0C RID: 7180
			public uint framebuffer;

			// Token: 0x04001C0D RID: 7181
			public uint colorbuffer;

			// Token: 0x04001C0E RID: 7182
			public uint resolveFramebuffer;
		}

		// Token: 0x0200035A RID: 858
		public struct INTERNAL_wayland_wminfo
		{
			// Token: 0x04001C0F RID: 7183
			public IntPtr display;

			// Token: 0x04001C10 RID: 7184
			public IntPtr surface;

			// Token: 0x04001C11 RID: 7185
			public IntPtr shell_surface;

			// Token: 0x04001C12 RID: 7186
			public IntPtr egl_window;

			// Token: 0x04001C13 RID: 7187
			public IntPtr xdg_surface;

			// Token: 0x04001C14 RID: 7188
			public IntPtr xdg_toplevel;

			// Token: 0x04001C15 RID: 7189
			public IntPtr xdg_popup;

			// Token: 0x04001C16 RID: 7190
			public IntPtr xdg_positioner;
		}

		// Token: 0x0200035B RID: 859
		public struct INTERNAL_mir_wminfo
		{
			// Token: 0x04001C17 RID: 7191
			public IntPtr connection;

			// Token: 0x04001C18 RID: 7192
			public IntPtr surface;
		}

		// Token: 0x0200035C RID: 860
		public struct INTERNAL_android_wminfo
		{
			// Token: 0x04001C19 RID: 7193
			public IntPtr window;

			// Token: 0x04001C1A RID: 7194
			public IntPtr surface;
		}

		// Token: 0x0200035D RID: 861
		public struct INTERNAL_vivante_wminfo
		{
			// Token: 0x04001C1B RID: 7195
			public IntPtr display;

			// Token: 0x04001C1C RID: 7196
			public IntPtr window;
		}

		// Token: 0x0200035E RID: 862
		public struct INTERNAL_os2_wminfo
		{
			// Token: 0x04001C1D RID: 7197
			public IntPtr hwnd;

			// Token: 0x04001C1E RID: 7198
			public IntPtr hwndFrame;
		}

		// Token: 0x0200035F RID: 863
		public struct INTERNAL_kmsdrm_wminfo
		{
			// Token: 0x04001C1F RID: 7199
			private int dev_index;

			// Token: 0x04001C20 RID: 7200
			private int drm_fd;

			// Token: 0x04001C21 RID: 7201
			private IntPtr gbm_dev;
		}

		// Token: 0x02000360 RID: 864
		[StructLayout(LayoutKind.Explicit)]
		public struct INTERNAL_SysWMDriverUnion
		{
			// Token: 0x04001C22 RID: 7202
			[FieldOffset(0)]
			public SDL.INTERNAL_windows_wminfo win;

			// Token: 0x04001C23 RID: 7203
			[FieldOffset(0)]
			public SDL.INTERNAL_winrt_wminfo winrt;

			// Token: 0x04001C24 RID: 7204
			[FieldOffset(0)]
			public SDL.INTERNAL_x11_wminfo x11;

			// Token: 0x04001C25 RID: 7205
			[FieldOffset(0)]
			public SDL.INTERNAL_directfb_wminfo dfb;

			// Token: 0x04001C26 RID: 7206
			[FieldOffset(0)]
			public SDL.INTERNAL_cocoa_wminfo cocoa;

			// Token: 0x04001C27 RID: 7207
			[FieldOffset(0)]
			public SDL.INTERNAL_uikit_wminfo uikit;

			// Token: 0x04001C28 RID: 7208
			[FieldOffset(0)]
			public SDL.INTERNAL_wayland_wminfo wl;

			// Token: 0x04001C29 RID: 7209
			[FieldOffset(0)]
			public SDL.INTERNAL_mir_wminfo mir;

			// Token: 0x04001C2A RID: 7210
			[FieldOffset(0)]
			public SDL.INTERNAL_android_wminfo android;

			// Token: 0x04001C2B RID: 7211
			[FieldOffset(0)]
			public SDL.INTERNAL_os2_wminfo os2;

			// Token: 0x04001C2C RID: 7212
			[FieldOffset(0)]
			public SDL.INTERNAL_vivante_wminfo vivante;

			// Token: 0x04001C2D RID: 7213
			[FieldOffset(0)]
			public SDL.INTERNAL_kmsdrm_wminfo ksmdrm;
		}

		// Token: 0x02000361 RID: 865
		public struct SDL_SysWMinfo
		{
			// Token: 0x04001C2E RID: 7214
			public SDL.SDL_version version;

			// Token: 0x04001C2F RID: 7215
			public SDL.SDL_SYSWM_TYPE subsystem;

			// Token: 0x04001C30 RID: 7216
			public SDL.INTERNAL_SysWMDriverUnion info;
		}

		// Token: 0x02000362 RID: 866
		public enum SDL_PowerState
		{
			// Token: 0x04001C32 RID: 7218
			SDL_POWERSTATE_UNKNOWN,
			// Token: 0x04001C33 RID: 7219
			SDL_POWERSTATE_ON_BATTERY,
			// Token: 0x04001C34 RID: 7220
			SDL_POWERSTATE_NO_BATTERY,
			// Token: 0x04001C35 RID: 7221
			SDL_POWERSTATE_CHARGING,
			// Token: 0x04001C36 RID: 7222
			SDL_POWERSTATE_CHARGED
		}

		// Token: 0x02000363 RID: 867
		public struct SDL_Locale
		{
			// Token: 0x04001C37 RID: 7223
			public IntPtr language;

			// Token: 0x04001C38 RID: 7224
			public IntPtr country;
		}
	}
}
