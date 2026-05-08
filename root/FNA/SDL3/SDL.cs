using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SDL3
{
	// Token: 0x02000005 RID: 5
	public static class SDL
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000024CC File Offset: 0x000006CC
		private unsafe static byte* EncodeAsUTF8(string str)
		{
			if (str == null)
			{
				return null;
			}
			int num = str.Length * 4 + 1;
			byte* ptr = (byte*)(void*)SDL.SDL_malloc((UIntPtr)((ulong)((long)num)));
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

		// Token: 0x060000D8 RID: 216 RVA: 0x00002528 File Offset: 0x00000728
		private unsafe static string DecodeFromUTF8(IntPtr ptr, bool shouldFree = false)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			byte* ptr2 = (byte*)(void*)ptr;
			while (*ptr2 != 0)
			{
				ptr2++;
			}
			string text = new string((sbyte*)(void*)ptr, 0, (int)((long)((byte*)ptr2 - (byte*)(void*)ptr)), Encoding.UTF8);
			if (shouldFree)
			{
				SDL.SDL_free(ptr);
			}
			return text;
		}

		// Token: 0x060000D9 RID: 217
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_malloc(UIntPtr size);

		// Token: 0x060000DA RID: 218
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_free(IntPtr mem);

		// Token: 0x060000DB RID: 219
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_memcpy(IntPtr dst, IntPtr src, UIntPtr len);

		// Token: 0x060000DC RID: 220
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ReportAssertion")]
		private unsafe static extern SDL.SDL_AssertState INTERNAL_SDL_ReportAssertion(ref SDL.SDL_AssertData data, byte* func, byte* file, int line);

		// Token: 0x060000DD RID: 221 RVA: 0x0000257C File Offset: 0x0000077C
		public unsafe static SDL.SDL_AssertState SDL_ReportAssertion(ref SDL.SDL_AssertData data, string func, string file, int line)
		{
			byte* ptr = SDL.EncodeAsUTF8(func);
			byte* ptr2 = SDL.EncodeAsUTF8(file);
			SDL.SDL_AssertState sdl_AssertState = SDL.INTERNAL_SDL_ReportAssertion(ref data, ptr, ptr2, line);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdl_AssertState;
		}

		// Token: 0x060000DE RID: 222
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetAssertionHandler(SDL.SDL_AssertionHandler handler, IntPtr userdata);

		// Token: 0x060000DF RID: 223
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDefaultAssertionHandler();

		// Token: 0x060000E0 RID: 224
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAssertionHandler(out IntPtr puserdata);

		// Token: 0x060000E1 RID: 225
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAssertionReport();

		// Token: 0x060000E2 RID: 226
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetAssertionReport();

		// Token: 0x060000E3 RID: 227
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AsyncIOFromFile")]
		private unsafe static extern IntPtr INTERNAL_SDL_AsyncIOFromFile(byte* file, byte* mode);

		// Token: 0x060000E4 RID: 228 RVA: 0x000025B8 File Offset: 0x000007B8
		public unsafe static IntPtr SDL_AsyncIOFromFile(string file, string mode)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			byte* ptr2 = SDL.EncodeAsUTF8(mode);
			IntPtr intPtr = SDL.INTERNAL_SDL_AsyncIOFromFile(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return intPtr;
		}

		// Token: 0x060000E5 RID: 229
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_GetAsyncIOSize(IntPtr asyncio);

		// Token: 0x060000E6 RID: 230
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata);

		// Token: 0x060000E7 RID: 231
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteAsyncIO(IntPtr asyncio, IntPtr ptr, ulong offset, ulong size, IntPtr queue, IntPtr userdata);

		// Token: 0x060000E8 RID: 232
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CloseAsyncIO(IntPtr asyncio, SDL.SDLBool flush, IntPtr queue, IntPtr userdata);

		// Token: 0x060000E9 RID: 233
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateAsyncIOQueue();

		// Token: 0x060000EA RID: 234
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyAsyncIOQueue(IntPtr queue);

		// Token: 0x060000EB RID: 235
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetAsyncIOResult(IntPtr queue, out SDL.SDL_AsyncIOOutcome outcome);

		// Token: 0x060000EC RID: 236
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitAsyncIOResult(IntPtr queue, out SDL.SDL_AsyncIOOutcome outcome, int timeoutMS);

		// Token: 0x060000ED RID: 237
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SignalAsyncIOQueue(IntPtr queue);

		// Token: 0x060000EE RID: 238
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadFileAsync")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_LoadFileAsync(byte* file, IntPtr queue, IntPtr userdata);

		// Token: 0x060000EF RID: 239 RVA: 0x000025F0 File Offset: 0x000007F0
		public unsafe static SDL.SDLBool SDL_LoadFileAsync(string file, IntPtr queue, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_LoadFileAsync(ptr, queue, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060000F0 RID: 240
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TryLockSpinlock(IntPtr @lock);

		// Token: 0x060000F1 RID: 241
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockSpinlock(IntPtr @lock);

		// Token: 0x060000F2 RID: 242
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSpinlock(IntPtr @lock);

		// Token: 0x060000F3 RID: 243
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MemoryBarrierReleaseFunction();

		// Token: 0x060000F4 RID: 244
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_MemoryBarrierAcquireFunction();

		// Token: 0x060000F5 RID: 245
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CompareAndSwapAtomicInt(ref SDL.SDL_AtomicInt a, int oldval, int newval);

		// Token: 0x060000F6 RID: 246
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_SetAtomicInt(ref SDL.SDL_AtomicInt a, int v);

		// Token: 0x060000F7 RID: 247
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAtomicInt(ref SDL.SDL_AtomicInt a);

		// Token: 0x060000F8 RID: 248
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AddAtomicInt(ref SDL.SDL_AtomicInt a, int v);

		// Token: 0x060000F9 RID: 249
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CompareAndSwapAtomicU32(ref SDL.SDL_AtomicU32 a, uint oldval, uint newval);

		// Token: 0x060000FA RID: 250
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_SetAtomicU32(ref SDL.SDL_AtomicU32 a, uint v);

		// Token: 0x060000FB RID: 251
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAtomicU32(ref SDL.SDL_AtomicU32 a);

		// Token: 0x060000FC RID: 252
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_AddAtomicU32(ref SDL.SDL_AtomicU32 a, int v);

		// Token: 0x060000FD RID: 253
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CompareAndSwapAtomicPointer(ref IntPtr a, IntPtr oldval, IntPtr newval);

		// Token: 0x060000FE RID: 254
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SetAtomicPointer(ref IntPtr a, IntPtr v);

		// Token: 0x060000FF RID: 255
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAtomicPointer(ref IntPtr a);

		// Token: 0x06000100 RID: 256
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetError")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetError(byte* fmt);

		// Token: 0x06000101 RID: 257 RVA: 0x00002618 File Offset: 0x00000818
		public unsafe static SDL.SDLBool SDL_SetError(string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetError(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000102 RID: 258
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_OutOfMemory();

		// Token: 0x06000103 RID: 259
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetError")]
		private static extern IntPtr INTERNAL_SDL_GetError();

		// Token: 0x06000104 RID: 260 RVA: 0x0000263D File Offset: 0x0000083D
		public static string SDL_GetError()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetError(), false);
		}

		// Token: 0x06000105 RID: 261
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClearError();

		// Token: 0x06000106 RID: 262
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGlobalProperties();

		// Token: 0x06000107 RID: 263
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_CreateProperties();

		// Token: 0x06000108 RID: 264
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CopyProperties(uint src, uint dst);

		// Token: 0x06000109 RID: 265
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LockProperties(uint props);

		// Token: 0x0600010A RID: 266
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockProperties(uint props);

		// Token: 0x0600010B RID: 267
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetPointerPropertyWithCleanup")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetPointerPropertyWithCleanup(uint props, byte* name, IntPtr value, SDL.SDL_CleanupPropertyCallback cleanup, IntPtr userdata);

		// Token: 0x0600010C RID: 268 RVA: 0x0000264C File Offset: 0x0000084C
		public unsafe static SDL.SDLBool SDL_SetPointerPropertyWithCleanup(uint props, string name, IntPtr value, SDL.SDL_CleanupPropertyCallback cleanup, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetPointerPropertyWithCleanup(props, ptr, value, cleanup, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600010D RID: 269
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetPointerProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetPointerProperty(uint props, byte* name, IntPtr value);

		// Token: 0x0600010E RID: 270 RVA: 0x00002678 File Offset: 0x00000878
		public unsafe static SDL.SDLBool SDL_SetPointerProperty(uint props, string name, IntPtr value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetPointerProperty(props, ptr, value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600010F RID: 271
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetStringProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetStringProperty(uint props, byte* name, byte* value);

		// Token: 0x06000110 RID: 272 RVA: 0x000026A0 File Offset: 0x000008A0
		public unsafe static SDL.SDLBool SDL_SetStringProperty(uint props, string name, string value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			byte* ptr2 = SDL.EncodeAsUTF8(value);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetStringProperty(props, ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x06000111 RID: 273
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetNumberProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetNumberProperty(uint props, byte* name, long value);

		// Token: 0x06000112 RID: 274 RVA: 0x000026DC File Offset: 0x000008DC
		public unsafe static SDL.SDLBool SDL_SetNumberProperty(uint props, string name, long value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetNumberProperty(props, ptr, value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000113 RID: 275
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetFloatProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetFloatProperty(uint props, byte* name, float value);

		// Token: 0x06000114 RID: 276 RVA: 0x00002704 File Offset: 0x00000904
		public unsafe static SDL.SDLBool SDL_SetFloatProperty(uint props, string name, float value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetFloatProperty(props, ptr, value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000115 RID: 277
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetBooleanProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetBooleanProperty(uint props, byte* name, SDL.SDLBool value);

		// Token: 0x06000116 RID: 278 RVA: 0x0000272C File Offset: 0x0000092C
		public unsafe static SDL.SDLBool SDL_SetBooleanProperty(uint props, string name, SDL.SDLBool value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetBooleanProperty(props, ptr, value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000117 RID: 279
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HasProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_HasProperty(uint props, byte* name);

		// Token: 0x06000118 RID: 280 RVA: 0x00002754 File Offset: 0x00000954
		public unsafe static SDL.SDLBool SDL_HasProperty(uint props, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_HasProperty(props, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000119 RID: 281
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPropertyType")]
		private unsafe static extern SDL.SDL_PropertyType INTERNAL_SDL_GetPropertyType(uint props, byte* name);

		// Token: 0x0600011A RID: 282 RVA: 0x0000277C File Offset: 0x0000097C
		public unsafe static SDL.SDL_PropertyType SDL_GetPropertyType(uint props, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDL_PropertyType sdl_PropertyType = SDL.INTERNAL_SDL_GetPropertyType(props, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_PropertyType;
		}

		// Token: 0x0600011B RID: 283
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPointerProperty")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetPointerProperty(uint props, byte* name, IntPtr default_value);

		// Token: 0x0600011C RID: 284 RVA: 0x000027A4 File Offset: 0x000009A4
		public unsafe static IntPtr SDL_GetPointerProperty(uint props, string name, IntPtr default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			IntPtr intPtr = SDL.INTERNAL_SDL_GetPointerProperty(props, ptr, default_value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x0600011D RID: 285
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetStringProperty")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetStringProperty(uint props, byte* name, byte* default_value);

		// Token: 0x0600011E RID: 286 RVA: 0x000027CC File Offset: 0x000009CC
		public unsafe static string SDL_GetStringProperty(uint props, string name, string default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			byte* ptr2 = SDL.EncodeAsUTF8(default_value);
			string text = SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetStringProperty(props, ptr, ptr2), false);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return text;
		}

		// Token: 0x0600011F RID: 287
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetNumberProperty")]
		private unsafe static extern long INTERNAL_SDL_GetNumberProperty(uint props, byte* name, long default_value);

		// Token: 0x06000120 RID: 288 RVA: 0x0000280C File Offset: 0x00000A0C
		public unsafe static long SDL_GetNumberProperty(uint props, string name, long default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			long num = SDL.INTERNAL_SDL_GetNumberProperty(props, ptr, default_value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000121 RID: 289
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetFloatProperty")]
		private unsafe static extern float INTERNAL_SDL_GetFloatProperty(uint props, byte* name, float default_value);

		// Token: 0x06000122 RID: 290 RVA: 0x00002834 File Offset: 0x00000A34
		public unsafe static float SDL_GetFloatProperty(uint props, string name, float default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			float num = SDL.INTERNAL_SDL_GetFloatProperty(props, ptr, default_value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000123 RID: 291
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetBooleanProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GetBooleanProperty(uint props, byte* name, SDL.SDLBool default_value);

		// Token: 0x06000124 RID: 292 RVA: 0x0000285C File Offset: 0x00000A5C
		public unsafe static SDL.SDLBool SDL_GetBooleanProperty(uint props, string name, SDL.SDLBool default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GetBooleanProperty(props, ptr, default_value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000125 RID: 293
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ClearProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_ClearProperty(uint props, byte* name);

		// Token: 0x06000126 RID: 294 RVA: 0x00002884 File Offset: 0x00000A84
		public unsafe static SDL.SDLBool SDL_ClearProperty(uint props, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_ClearProperty(props, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000127 RID: 295
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_EnumerateProperties(uint props, SDL.SDL_EnumeratePropertiesCallback callback, IntPtr userdata);

		// Token: 0x06000128 RID: 296
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyProperties(uint props);

		// Token: 0x06000129 RID: 297
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateThreadRuntime")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateThreadRuntime(SDL.SDL_ThreadFunction fn, byte* name, IntPtr data, IntPtr pfnBeginThread, IntPtr pfnEndThread);

		// Token: 0x0600012A RID: 298 RVA: 0x000028AC File Offset: 0x00000AAC
		public unsafe static IntPtr SDL_CreateThreadRuntime(SDL.SDL_ThreadFunction fn, string name, IntPtr data, IntPtr pfnBeginThread, IntPtr pfnEndThread)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateThreadRuntime(fn, ptr, data, pfnBeginThread, pfnEndThread);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x0600012B RID: 299
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateThreadWithPropertiesRuntime(uint props, IntPtr pfnBeginThread, IntPtr pfnEndThread);

		// Token: 0x0600012C RID: 300
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetThreadName")]
		private static extern IntPtr INTERNAL_SDL_GetThreadName(IntPtr thread);

		// Token: 0x0600012D RID: 301 RVA: 0x000028D6 File Offset: 0x00000AD6
		public static string SDL_GetThreadName(IntPtr thread)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetThreadName(thread), false);
		}

		// Token: 0x0600012E RID: 302
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetCurrentThreadID();

		// Token: 0x0600012F RID: 303
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetThreadID(IntPtr thread);

		// Token: 0x06000130 RID: 304
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetCurrentThreadPriority(SDL.SDL_ThreadPriority priority);

		// Token: 0x06000131 RID: 305
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WaitThread(IntPtr thread, IntPtr status);

		// Token: 0x06000132 RID: 306
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_ThreadState SDL_GetThreadState(IntPtr thread);

		// Token: 0x06000133 RID: 307
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DetachThread(IntPtr thread);

		// Token: 0x06000134 RID: 308
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTLS(IntPtr id);

		// Token: 0x06000135 RID: 309
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTLS(IntPtr id, IntPtr value, SDL.SDL_TLSDestructorCallback destructor);

		// Token: 0x06000136 RID: 310
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CleanupTLS();

		// Token: 0x06000137 RID: 311
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateMutex();

		// Token: 0x06000138 RID: 312
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockMutex(IntPtr mutex);

		// Token: 0x06000139 RID: 313
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TryLockMutex(IntPtr mutex);

		// Token: 0x0600013A RID: 314
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockMutex(IntPtr mutex);

		// Token: 0x0600013B RID: 315
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyMutex(IntPtr mutex);

		// Token: 0x0600013C RID: 316
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRWLock();

		// Token: 0x0600013D RID: 317
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockRWLockForReading(IntPtr rwlock);

		// Token: 0x0600013E RID: 318
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockRWLockForWriting(IntPtr rwlock);

		// Token: 0x0600013F RID: 319
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TryLockRWLockForReading(IntPtr rwlock);

		// Token: 0x06000140 RID: 320
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TryLockRWLockForWriting(IntPtr rwlock);

		// Token: 0x06000141 RID: 321
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockRWLock(IntPtr rwlock);

		// Token: 0x06000142 RID: 322
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyRWLock(IntPtr rwlock);

		// Token: 0x06000143 RID: 323
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSemaphore(uint initial_value);

		// Token: 0x06000144 RID: 324
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroySemaphore(IntPtr sem);

		// Token: 0x06000145 RID: 325
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WaitSemaphore(IntPtr sem);

		// Token: 0x06000146 RID: 326
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TryWaitSemaphore(IntPtr sem);

		// Token: 0x06000147 RID: 327
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitSemaphoreTimeout(IntPtr sem, int timeoutMS);

		// Token: 0x06000148 RID: 328
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SignalSemaphore(IntPtr sem);

		// Token: 0x06000149 RID: 329
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSemaphoreValue(IntPtr sem);

		// Token: 0x0600014A RID: 330
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateCondition();

		// Token: 0x0600014B RID: 331
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyCondition(IntPtr cond);

		// Token: 0x0600014C RID: 332
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SignalCondition(IntPtr cond);

		// Token: 0x0600014D RID: 333
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BroadcastCondition(IntPtr cond);

		// Token: 0x0600014E RID: 334
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WaitCondition(IntPtr cond, IntPtr mutex);

		// Token: 0x0600014F RID: 335
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitConditionTimeout(IntPtr cond, IntPtr mutex, int timeoutMS);

		// Token: 0x06000150 RID: 336
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShouldInit(ref SDL.SDL_InitState state);

		// Token: 0x06000151 RID: 337
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShouldQuit(ref SDL.SDL_InitState state);

		// Token: 0x06000152 RID: 338
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetInitialized(ref SDL.SDL_InitState state, SDL.SDLBool initialized);

		// Token: 0x06000153 RID: 339
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_IOFromFile")]
		private unsafe static extern IntPtr INTERNAL_SDL_IOFromFile(byte* file, byte* mode);

		// Token: 0x06000154 RID: 340 RVA: 0x000028E4 File Offset: 0x00000AE4
		public unsafe static IntPtr SDL_IOFromFile(string file, string mode)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			byte* ptr2 = SDL.EncodeAsUTF8(mode);
			IntPtr intPtr = SDL.INTERNAL_SDL_IOFromFile(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return intPtr;
		}

		// Token: 0x06000155 RID: 341
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromMem(IntPtr mem, UIntPtr size);

		// Token: 0x06000156 RID: 342
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromConstMem(IntPtr mem, UIntPtr size);

		// Token: 0x06000157 RID: 343
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_IOFromDynamicMem();

		// Token: 0x06000158 RID: 344
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenIO(ref SDL.SDL_IOStreamInterface iface, IntPtr userdata);

		// Token: 0x06000159 RID: 345
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CloseIO(IntPtr context);

		// Token: 0x0600015A RID: 346
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetIOProperties(IntPtr context);

		// Token: 0x0600015B RID: 347
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_IOStatus SDL_GetIOStatus(IntPtr context);

		// Token: 0x0600015C RID: 348
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_GetIOSize(IntPtr context);

		// Token: 0x0600015D RID: 349
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_SeekIO(IntPtr context, long offset, SDL.SDL_IOWhence whence);

		// Token: 0x0600015E RID: 350
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_TellIO(IntPtr context);

		// Token: 0x0600015F RID: 351
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern UIntPtr SDL_ReadIO(IntPtr context, IntPtr ptr, UIntPtr size);

		// Token: 0x06000160 RID: 352
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern UIntPtr SDL_WriteIO(IntPtr context, IntPtr ptr, UIntPtr size);

		// Token: 0x06000161 RID: 353
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_IOprintf")]
		private unsafe static extern UIntPtr INTERNAL_SDL_IOprintf(IntPtr context, byte* fmt);

		// Token: 0x06000162 RID: 354 RVA: 0x0000291C File Offset: 0x00000B1C
		public unsafe static UIntPtr SDL_IOprintf(IntPtr context, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			UIntPtr uintPtr = SDL.INTERNAL_SDL_IOprintf(context, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return uintPtr;
		}

		// Token: 0x06000163 RID: 355
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FlushIO(IntPtr context);

		// Token: 0x06000164 RID: 356
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadFile_IO(IntPtr src, out UIntPtr datasize, SDL.SDLBool closeio);

		// Token: 0x06000165 RID: 357
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadFile")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadFile(byte* file, out UIntPtr datasize);

		// Token: 0x06000166 RID: 358 RVA: 0x00002944 File Offset: 0x00000B44
		public unsafe static IntPtr SDL_LoadFile(string file, out UIntPtr datasize)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadFile(ptr, out datasize);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000167 RID: 359
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SaveFile_IO(IntPtr src, IntPtr data, UIntPtr datasize, SDL.SDLBool closeio);

		// Token: 0x06000168 RID: 360
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SaveFile")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SaveFile(byte* file, IntPtr data, UIntPtr datasize);

		// Token: 0x06000169 RID: 361 RVA: 0x0000296C File Offset: 0x00000B6C
		public unsafe static SDL.SDLBool SDL_SaveFile(string file, IntPtr data, UIntPtr datasize)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SaveFile(ptr, data, datasize);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600016A RID: 362
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU8(IntPtr src, out byte value);

		// Token: 0x0600016B RID: 363
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS8(IntPtr src, out sbyte value);

		// Token: 0x0600016C RID: 364
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU16LE(IntPtr src, out ushort value);

		// Token: 0x0600016D RID: 365
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS16LE(IntPtr src, out short value);

		// Token: 0x0600016E RID: 366
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU16BE(IntPtr src, out ushort value);

		// Token: 0x0600016F RID: 367
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS16BE(IntPtr src, out short value);

		// Token: 0x06000170 RID: 368
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU32LE(IntPtr src, out uint value);

		// Token: 0x06000171 RID: 369
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS32LE(IntPtr src, out int value);

		// Token: 0x06000172 RID: 370
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU32BE(IntPtr src, out uint value);

		// Token: 0x06000173 RID: 371
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS32BE(IntPtr src, out int value);

		// Token: 0x06000174 RID: 372
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU64LE(IntPtr src, out ulong value);

		// Token: 0x06000175 RID: 373
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS64LE(IntPtr src, out long value);

		// Token: 0x06000176 RID: 374
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadU64BE(IntPtr src, out ulong value);

		// Token: 0x06000177 RID: 375
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadS64BE(IntPtr src, out long value);

		// Token: 0x06000178 RID: 376
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU8(IntPtr dst, byte value);

		// Token: 0x06000179 RID: 377
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS8(IntPtr dst, sbyte value);

		// Token: 0x0600017A RID: 378
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU16LE(IntPtr dst, ushort value);

		// Token: 0x0600017B RID: 379
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS16LE(IntPtr dst, short value);

		// Token: 0x0600017C RID: 380
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU16BE(IntPtr dst, ushort value);

		// Token: 0x0600017D RID: 381
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS16BE(IntPtr dst, short value);

		// Token: 0x0600017E RID: 382
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU32LE(IntPtr dst, uint value);

		// Token: 0x0600017F RID: 383
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS32LE(IntPtr dst, int value);

		// Token: 0x06000180 RID: 384
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU32BE(IntPtr dst, uint value);

		// Token: 0x06000181 RID: 385
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS32BE(IntPtr dst, int value);

		// Token: 0x06000182 RID: 386
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU64LE(IntPtr dst, ulong value);

		// Token: 0x06000183 RID: 387
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS64LE(IntPtr dst, long value);

		// Token: 0x06000184 RID: 388
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteU64BE(IntPtr dst, ulong value);

		// Token: 0x06000185 RID: 389
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteS64BE(IntPtr dst, long value);

		// Token: 0x06000186 RID: 390
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumAudioDrivers();

		// Token: 0x06000187 RID: 391
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAudioDriver")]
		private static extern IntPtr INTERNAL_SDL_GetAudioDriver(int index);

		// Token: 0x06000188 RID: 392 RVA: 0x00002993 File Offset: 0x00000B93
		public static string SDL_GetAudioDriver(int index)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetAudioDriver(index), false);
		}

		// Token: 0x06000189 RID: 393
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentAudioDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentAudioDriver();

		// Token: 0x0600018A RID: 394 RVA: 0x000029A1 File Offset: 0x00000BA1
		public static string SDL_GetCurrentAudioDriver()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCurrentAudioDriver(), false);
		}

		// Token: 0x0600018B RID: 395
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioPlaybackDevices(out int count);

		// Token: 0x0600018C RID: 396
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioRecordingDevices(out int count);

		// Token: 0x0600018D RID: 397
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAudioDeviceName")]
		private static extern IntPtr INTERNAL_SDL_GetAudioDeviceName(uint devid);

		// Token: 0x0600018E RID: 398 RVA: 0x000029AE File Offset: 0x00000BAE
		public static string SDL_GetAudioDeviceName(uint devid)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetAudioDeviceName(devid), false);
		}

		// Token: 0x0600018F RID: 399
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetAudioDeviceFormat(uint devid, out SDL.SDL_AudioSpec spec, out int sample_frames);

		// Token: 0x06000190 RID: 400
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioDeviceChannelMap(uint devid, out int count);

		// Token: 0x06000191 RID: 401
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_OpenAudioDevice(uint devid, ref SDL.SDL_AudioSpec spec);

		// Token: 0x06000192 RID: 402
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsAudioDevicePhysical(uint devid);

		// Token: 0x06000193 RID: 403
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsAudioDevicePlayback(uint devid);

		// Token: 0x06000194 RID: 404
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PauseAudioDevice(uint devid);

		// Token: 0x06000195 RID: 405
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ResumeAudioDevice(uint devid);

		// Token: 0x06000196 RID: 406
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AudioDevicePaused(uint devid);

		// Token: 0x06000197 RID: 407
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioDeviceGain(uint devid);

		// Token: 0x06000198 RID: 408
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioDeviceGain(uint devid, float gain);

		// Token: 0x06000199 RID: 409
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseAudioDevice(uint devid);

		// Token: 0x0600019A RID: 410
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BindAudioStreams(uint devid, IntPtr[] streams, int num_streams);

		// Token: 0x0600019B RID: 411
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BindAudioStream(uint devid, IntPtr stream);

		// Token: 0x0600019C RID: 412
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnbindAudioStreams(IntPtr[] streams, int num_streams);

		// Token: 0x0600019D RID: 413
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnbindAudioStream(IntPtr stream);

		// Token: 0x0600019E RID: 414
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAudioStreamDevice(IntPtr stream);

		// Token: 0x0600019F RID: 415
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateAudioStream(ref SDL.SDL_AudioSpec src_spec, ref SDL.SDL_AudioSpec dst_spec);

		// Token: 0x060001A0 RID: 416
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetAudioStreamProperties(IntPtr stream);

		// Token: 0x060001A1 RID: 417
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetAudioStreamFormat(IntPtr stream, out SDL.SDL_AudioSpec src_spec, out SDL.SDL_AudioSpec dst_spec);

		// Token: 0x060001A2 RID: 418
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamFormat(IntPtr stream, ref SDL.SDL_AudioSpec src_spec, ref SDL.SDL_AudioSpec dst_spec);

		// Token: 0x060001A3 RID: 419
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioStreamFrequencyRatio(IntPtr stream);

		// Token: 0x060001A4 RID: 420
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamFrequencyRatio(IntPtr stream, float ratio);

		// Token: 0x060001A5 RID: 421
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetAudioStreamGain(IntPtr stream);

		// Token: 0x060001A6 RID: 422
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamGain(IntPtr stream, float gain);

		// Token: 0x060001A7 RID: 423
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioStreamInputChannelMap(IntPtr stream, out int count);

		// Token: 0x060001A8 RID: 424
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetAudioStreamOutputChannelMap(IntPtr stream, out int count);

		// Token: 0x060001A9 RID: 425
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamInputChannelMap(IntPtr stream, int[] chmap, int count);

		// Token: 0x060001AA RID: 426
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamOutputChannelMap(IntPtr stream, int[] chmap, int count);

		// Token: 0x060001AB RID: 427
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PutAudioStreamData(IntPtr stream, IntPtr buf, int len);

		// Token: 0x060001AC RID: 428
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PutAudioStreamDataNoCopy(IntPtr stream, IntPtr buf, int len, SDL.SDL_AudioStreamDataCompleteCallback callback, IntPtr userdata);

		// Token: 0x060001AD RID: 429
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PutAudioStreamPlanarData(IntPtr stream, IntPtr[] channel_buffers, int num_channels, int num_samples);

		// Token: 0x060001AE RID: 430
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamData(IntPtr stream, IntPtr buf, int len);

		// Token: 0x060001AF RID: 431
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamAvailable(IntPtr stream);

		// Token: 0x060001B0 RID: 432
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetAudioStreamQueued(IntPtr stream);

		// Token: 0x060001B1 RID: 433
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FlushAudioStream(IntPtr stream);

		// Token: 0x060001B2 RID: 434
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClearAudioStream(IntPtr stream);

		// Token: 0x060001B3 RID: 435
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PauseAudioStreamDevice(IntPtr stream);

		// Token: 0x060001B4 RID: 436
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ResumeAudioStreamDevice(IntPtr stream);

		// Token: 0x060001B5 RID: 437
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AudioStreamDevicePaused(IntPtr stream);

		// Token: 0x060001B6 RID: 438
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LockAudioStream(IntPtr stream);

		// Token: 0x060001B7 RID: 439
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UnlockAudioStream(IntPtr stream);

		// Token: 0x060001B8 RID: 440
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamGetCallback(IntPtr stream, SDL.SDL_AudioStreamCallback callback, IntPtr userdata);

		// Token: 0x060001B9 RID: 441
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioStreamPutCallback(IntPtr stream, SDL.SDL_AudioStreamCallback callback, IntPtr userdata);

		// Token: 0x060001BA RID: 442
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyAudioStream(IntPtr stream);

		// Token: 0x060001BB RID: 443
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenAudioDeviceStream(uint devid, ref SDL.SDL_AudioSpec spec, SDL.SDL_AudioStreamCallback callback, IntPtr userdata);

		// Token: 0x060001BC RID: 444
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetAudioPostmixCallback(uint devid, SDL.SDL_AudioPostmixCallback callback, IntPtr userdata);

		// Token: 0x060001BD RID: 445
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LoadWAV_IO(IntPtr src, SDL.SDLBool closeio, out SDL.SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len);

		// Token: 0x060001BE RID: 446
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadWAV")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_LoadWAV(byte* path, out SDL.SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len);

		// Token: 0x060001BF RID: 447 RVA: 0x000029BC File Offset: 0x00000BBC
		public unsafe static SDL.SDLBool SDL_LoadWAV(string path, out SDL.SDL_AudioSpec spec, out IntPtr audio_buf, out uint audio_len)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_LoadWAV(ptr, out spec, out audio_buf, out audio_len);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060001C0 RID: 448
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_MixAudio(IntPtr dst, IntPtr src, SDL.SDL_AudioFormat format, uint len, float volume);

		// Token: 0x060001C1 RID: 449
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ConvertAudioSamples(ref SDL.SDL_AudioSpec src_spec, IntPtr src_data, int src_len, ref SDL.SDL_AudioSpec dst_spec, IntPtr dst_data, out int dst_len);

		// Token: 0x060001C2 RID: 450
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAudioFormatName")]
		private static extern IntPtr INTERNAL_SDL_GetAudioFormatName(SDL.SDL_AudioFormat format);

		// Token: 0x060001C3 RID: 451 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static string SDL_GetAudioFormatName(SDL.SDL_AudioFormat format)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetAudioFormatName(format), false);
		}

		// Token: 0x060001C4 RID: 452
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSilenceValueForFormat(SDL.SDL_AudioFormat format);

		// Token: 0x060001C5 RID: 453
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_ComposeCustomBlendMode(SDL.SDL_BlendFactor srcColorFactor, SDL.SDL_BlendFactor dstColorFactor, SDL.SDL_BlendOperation colorOperation, SDL.SDL_BlendFactor srcAlphaFactor, SDL.SDL_BlendFactor dstAlphaFactor, SDL.SDL_BlendOperation alphaOperation);

		// Token: 0x060001C6 RID: 454
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPixelFormatName")]
		private static extern IntPtr INTERNAL_SDL_GetPixelFormatName(SDL.SDL_PixelFormat format);

		// Token: 0x060001C7 RID: 455 RVA: 0x000029F2 File Offset: 0x00000BF2
		public static string SDL_GetPixelFormatName(SDL.SDL_PixelFormat format)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetPixelFormatName(format), false);
		}

		// Token: 0x060001C8 RID: 456
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetMasksForPixelFormat(SDL.SDL_PixelFormat format, out int bpp, out uint Rmask, out uint Gmask, out uint Bmask, out uint Amask);

		// Token: 0x060001C9 RID: 457
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

		// Token: 0x060001CA RID: 458
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetPixelFormatDetails(SDL.SDL_PixelFormat format);

		// Token: 0x060001CB RID: 459
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreatePalette(int ncolors);

		// Token: 0x060001CC RID: 460
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetPaletteColors(IntPtr palette, SDL.SDL_Color[] colors, int firstcolor, int ncolors);

		// Token: 0x060001CD RID: 461
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyPalette(IntPtr palette);

		// Token: 0x060001CE RID: 462
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGB(IntPtr format, IntPtr palette, byte r, byte g, byte b);

		// Token: 0x060001CF RID: 463
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapRGBA(IntPtr format, IntPtr palette, byte r, byte g, byte b, byte a);

		// Token: 0x060001D0 RID: 464
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGB(uint pixelvalue, IntPtr format, IntPtr palette, out byte r, out byte g, out byte b);

		// Token: 0x060001D1 RID: 465
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetRGBA(uint pixelvalue, IntPtr format, IntPtr palette, out byte r, out byte g, out byte b, out byte a);

		// Token: 0x060001D2 RID: 466
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasRectIntersection(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B);

		// Token: 0x060001D3 RID: 467
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectIntersection(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B, out SDL.SDL_Rect result);

		// Token: 0x060001D4 RID: 468
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectUnion(ref SDL.SDL_Rect A, ref SDL.SDL_Rect B, out SDL.SDL_Rect result);

		// Token: 0x060001D5 RID: 469
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectEnclosingPoints(SDL.SDL_Point[] points, int count, ref SDL.SDL_Rect clip, out SDL.SDL_Rect result);

		// Token: 0x060001D6 RID: 470
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectAndLineIntersection(ref SDL.SDL_Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);

		// Token: 0x060001D7 RID: 471
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasRectIntersectionFloat(ref SDL.SDL_FRect A, ref SDL.SDL_FRect B);

		// Token: 0x060001D8 RID: 472
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectIntersectionFloat(ref SDL.SDL_FRect A, ref SDL.SDL_FRect B, out SDL.SDL_FRect result);

		// Token: 0x060001D9 RID: 473
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectUnionFloat(ref SDL.SDL_FRect A, ref SDL.SDL_FRect B, out SDL.SDL_FRect result);

		// Token: 0x060001DA RID: 474
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectEnclosingPointsFloat(SDL.SDL_FPoint[] points, int count, ref SDL.SDL_FRect clip, out SDL.SDL_FRect result);

		// Token: 0x060001DB RID: 475
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRectAndLineIntersectionFloat(ref SDL.SDL_FRect rect, ref float X1, ref float Y1, ref float X2, ref float Y2);

		// Token: 0x060001DC RID: 476
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSurface(int width, int height, SDL.SDL_PixelFormat format);

		// Token: 0x060001DD RID: 477
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSurfaceFrom(int width, int height, SDL.SDL_PixelFormat format, IntPtr pixels, int pitch);

		// Token: 0x060001DE RID: 478
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroySurface(IntPtr surface);

		// Token: 0x060001DF RID: 479
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSurfaceProperties(IntPtr surface);

		// Token: 0x060001E0 RID: 480
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceColorspace(IntPtr surface, SDL.SDL_Colorspace colorspace);

		// Token: 0x060001E1 RID: 481
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Colorspace SDL_GetSurfaceColorspace(IntPtr surface);

		// Token: 0x060001E2 RID: 482
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSurfacePalette(IntPtr surface);

		// Token: 0x060001E3 RID: 483
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfacePalette(IntPtr surface, IntPtr palette);

		// Token: 0x060001E4 RID: 484
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSurfacePalette(IntPtr surface);

		// Token: 0x060001E5 RID: 485
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AddSurfaceAlternateImage(IntPtr surface, IntPtr image);

		// Token: 0x060001E6 RID: 486
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SurfaceHasAlternateImages(IntPtr surface);

		// Token: 0x060001E7 RID: 487
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSurfaceImages(IntPtr surface, out int count);

		// Token: 0x060001E8 RID: 488
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RemoveSurfaceAlternateImages(IntPtr surface);

		// Token: 0x060001E9 RID: 489
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LockSurface(IntPtr surface);

		// Token: 0x060001EA RID: 490
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockSurface(IntPtr surface);

		// Token: 0x060001EB RID: 491
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadSurface_IO(IntPtr src, SDL.SDLBool closeio);

		// Token: 0x060001EC RID: 492
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadSurface")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadSurface(byte* file);

		// Token: 0x060001ED RID: 493 RVA: 0x00002A00 File Offset: 0x00000C00
		public unsafe static IntPtr SDL_LoadSurface(string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadSurface(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060001EE RID: 494
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadBMP_IO(IntPtr src, SDL.SDLBool closeio);

		// Token: 0x060001EF RID: 495
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadBMP")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadBMP(byte* file);

		// Token: 0x060001F0 RID: 496 RVA: 0x00002A28 File Offset: 0x00000C28
		public unsafe static IntPtr SDL_LoadBMP(string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadBMP(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060001F1 RID: 497
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SaveBMP_IO(IntPtr surface, IntPtr dst, SDL.SDLBool closeio);

		// Token: 0x060001F2 RID: 498
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SaveBMP")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SaveBMP(IntPtr surface, byte* file);

		// Token: 0x060001F3 RID: 499 RVA: 0x00002A50 File Offset: 0x00000C50
		public unsafe static SDL.SDLBool SDL_SaveBMP(IntPtr surface, string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SaveBMP(surface, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060001F4 RID: 500
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_LoadPNG_IO(IntPtr src, SDL.SDLBool closeio);

		// Token: 0x060001F5 RID: 501
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadPNG")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadPNG(byte* file);

		// Token: 0x060001F6 RID: 502 RVA: 0x00002A78 File Offset: 0x00000C78
		public unsafe static IntPtr SDL_LoadPNG(string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadPNG(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060001F7 RID: 503
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SavePNG_IO(IntPtr surface, IntPtr dst, SDL.SDLBool closeio);

		// Token: 0x060001F8 RID: 504
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SavePNG")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SavePNG(IntPtr surface, byte* file);

		// Token: 0x060001F9 RID: 505 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public unsafe static SDL.SDLBool SDL_SavePNG(IntPtr surface, string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SavePNG(surface, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060001FA RID: 506
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceRLE(IntPtr surface, SDL.SDLBool enabled);

		// Token: 0x060001FB RID: 507
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SurfaceHasRLE(IntPtr surface);

		// Token: 0x060001FC RID: 508
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceColorKey(IntPtr surface, SDL.SDLBool enabled, uint key);

		// Token: 0x060001FD RID: 509
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SurfaceHasColorKey(IntPtr surface);

		// Token: 0x060001FE RID: 510
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetSurfaceColorKey(IntPtr surface, out uint key);

		// Token: 0x060001FF RID: 511
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

		// Token: 0x06000200 RID: 512
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

		// Token: 0x06000201 RID: 513
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceAlphaMod(IntPtr surface, byte alpha);

		// Token: 0x06000202 RID: 514
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

		// Token: 0x06000203 RID: 515
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceBlendMode(IntPtr surface, uint blendMode);

		// Token: 0x06000204 RID: 516
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetSurfaceBlendMode(IntPtr surface, IntPtr blendMode);

		// Token: 0x06000205 RID: 517
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetSurfaceClipRect(IntPtr surface, ref SDL.SDL_Rect rect);

		// Token: 0x06000206 RID: 518
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetSurfaceClipRect(IntPtr surface, out SDL.SDL_Rect rect);

		// Token: 0x06000207 RID: 519
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FlipSurface(IntPtr surface, SDL.SDL_FlipMode flip);

		// Token: 0x06000208 RID: 520
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RotateSurface(IntPtr surface, float angle);

		// Token: 0x06000209 RID: 521
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_DuplicateSurface(IntPtr surface);

		// Token: 0x0600020A RID: 522
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ScaleSurface(IntPtr surface, int width, int height, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x0600020B RID: 523
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurface(IntPtr surface, SDL.SDL_PixelFormat format);

		// Token: 0x0600020C RID: 524
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ConvertSurfaceAndColorspace(IntPtr surface, SDL.SDL_PixelFormat format, IntPtr palette, SDL.SDL_Colorspace colorspace, uint props);

		// Token: 0x0600020D RID: 525
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ConvertPixels(int width, int height, SDL.SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL.SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch);

		// Token: 0x0600020E RID: 526
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ConvertPixelsAndColorspace(int width, int height, SDL.SDL_PixelFormat src_format, SDL.SDL_Colorspace src_colorspace, uint src_properties, IntPtr src, int src_pitch, SDL.SDL_PixelFormat dst_format, SDL.SDL_Colorspace dst_colorspace, uint dst_properties, IntPtr dst, int dst_pitch);

		// Token: 0x0600020F RID: 527
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PremultiplyAlpha(int width, int height, SDL.SDL_PixelFormat src_format, IntPtr src, int src_pitch, SDL.SDL_PixelFormat dst_format, IntPtr dst, int dst_pitch, SDL.SDLBool linear);

		// Token: 0x06000210 RID: 528
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PremultiplySurfaceAlpha(IntPtr surface, SDL.SDLBool linear);

		// Token: 0x06000211 RID: 529
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClearSurface(IntPtr surface, float r, float g, float b, float a);

		// Token: 0x06000212 RID: 530
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FillSurfaceRect(IntPtr dst, IntPtr rect, uint color);

		// Token: 0x06000213 RID: 531
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FillSurfaceRects(IntPtr dst, SDL.SDL_Rect[] rects, int count, uint color);

		// Token: 0x06000214 RID: 532
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x06000215 RID: 533
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurfaceUnchecked(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x06000216 RID: 534
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurfaceScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x06000217 RID: 535
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurfaceUncheckedScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x06000218 RID: 536
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StretchSurface(IntPtr src, ref SDL.SDL_Rect srcrect, IntPtr dst, ref SDL.SDL_Rect dstrect, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x06000219 RID: 537
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurfaceTiled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

		// Token: 0x0600021A RID: 538
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurfaceTiledWithScale(IntPtr src, IntPtr srcrect, float scale, SDL.SDL_ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);

		// Token: 0x0600021B RID: 539
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_BlitSurface9Grid(IntPtr src, IntPtr srcrect, int left_width, int right_width, int top_height, int bottom_height, float scale, SDL.SDL_ScaleMode scaleMode, IntPtr dst, IntPtr dstrect);

		// Token: 0x0600021C RID: 540
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapSurfaceRGB(IntPtr surface, byte r, byte g, byte b);

		// Token: 0x0600021D RID: 541
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_MapSurfaceRGBA(IntPtr surface, byte r, byte g, byte b, byte a);

		// Token: 0x0600021E RID: 542
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadSurfacePixel(IntPtr surface, int x, int y, out byte r, out byte g, out byte b, out byte a);

		// Token: 0x0600021F RID: 543
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReadSurfacePixelFloat(IntPtr surface, int x, int y, out float r, out float g, out float b, out float a);

		// Token: 0x06000220 RID: 544
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteSurfacePixel(IntPtr surface, int x, int y, byte r, byte g, byte b, byte a);

		// Token: 0x06000221 RID: 545
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WriteSurfacePixelFloat(IntPtr surface, int x, int y, float r, float g, float b, float a);

		// Token: 0x06000222 RID: 546
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumCameraDrivers();

		// Token: 0x06000223 RID: 547
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCameraDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCameraDriver(int index);

		// Token: 0x06000224 RID: 548 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public static string SDL_GetCameraDriver(int index)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCameraDriver(index), false);
		}

		// Token: 0x06000225 RID: 549
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentCameraDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentCameraDriver();

		// Token: 0x06000226 RID: 550 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public static string SDL_GetCurrentCameraDriver()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCurrentCameraDriver(), false);
		}

		// Token: 0x06000227 RID: 551
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCameras(out int count);

		// Token: 0x06000228 RID: 552
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCameraSupportedFormats(uint instance_id, out int count);

		// Token: 0x06000229 RID: 553
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCameraName")]
		private static extern IntPtr INTERNAL_SDL_GetCameraName(uint instance_id);

		// Token: 0x0600022A RID: 554 RVA: 0x00002AE1 File Offset: 0x00000CE1
		public static string SDL_GetCameraName(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCameraName(instance_id), false);
		}

		// Token: 0x0600022B RID: 555
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_CameraPosition SDL_GetCameraPosition(uint instance_id);

		// Token: 0x0600022C RID: 556
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenCamera(uint instance_id, ref SDL.SDL_CameraSpec spec);

		// Token: 0x0600022D RID: 557
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_CameraPermissionState SDL_GetCameraPermissionState(IntPtr camera);

		// Token: 0x0600022E RID: 558
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetCameraID(IntPtr camera);

		// Token: 0x0600022F RID: 559
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetCameraProperties(IntPtr camera);

		// Token: 0x06000230 RID: 560
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetCameraFormat(IntPtr camera, out SDL.SDL_CameraSpec spec);

		// Token: 0x06000231 RID: 561
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AcquireCameraFrame(IntPtr camera, out ulong timestampNS);

		// Token: 0x06000232 RID: 562
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseCameraFrame(IntPtr camera, IntPtr frame);

		// Token: 0x06000233 RID: 563
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseCamera(IntPtr camera);

		// Token: 0x06000234 RID: 564
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetClipboardText")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetClipboardText(byte* text);

		// Token: 0x06000235 RID: 565 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public unsafe static SDL.SDLBool SDL_SetClipboardText(string text)
		{
			byte* ptr = SDL.EncodeAsUTF8(text);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetClipboardText(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000236 RID: 566
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetClipboardText")]
		private static extern IntPtr INTERNAL_SDL_GetClipboardText();

		// Token: 0x06000237 RID: 567 RVA: 0x00002B15 File Offset: 0x00000D15
		public static string SDL_GetClipboardText()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetClipboardText(), true);
		}

		// Token: 0x06000238 RID: 568
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasClipboardText();

		// Token: 0x06000239 RID: 569
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetPrimarySelectionText")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetPrimarySelectionText(byte* text);

		// Token: 0x0600023A RID: 570 RVA: 0x00002B24 File Offset: 0x00000D24
		public unsafe static SDL.SDLBool SDL_SetPrimarySelectionText(string text)
		{
			byte* ptr = SDL.EncodeAsUTF8(text);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetPrimarySelectionText(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600023B RID: 571
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPrimarySelectionText")]
		private static extern IntPtr INTERNAL_SDL_GetPrimarySelectionText();

		// Token: 0x0600023C RID: 572 RVA: 0x00002B49 File Offset: 0x00000D49
		public static string SDL_GetPrimarySelectionText()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetPrimarySelectionText(), true);
		}

		// Token: 0x0600023D RID: 573
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasPrimarySelectionText();

		// Token: 0x0600023E RID: 574
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetClipboardData(SDL.SDL_ClipboardDataCallback callback, SDL.SDL_ClipboardCleanupCallback cleanup, IntPtr userdata, IntPtr mime_types, UIntPtr num_mime_types);

		// Token: 0x0600023F RID: 575
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClearClipboardData();

		// Token: 0x06000240 RID: 576
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetClipboardData")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetClipboardData(byte* mime_type, out UIntPtr size);

		// Token: 0x06000241 RID: 577 RVA: 0x00002B58 File Offset: 0x00000D58
		public unsafe static IntPtr SDL_GetClipboardData(string mime_type, out UIntPtr size)
		{
			byte* ptr = SDL.EncodeAsUTF8(mime_type);
			IntPtr intPtr = SDL.INTERNAL_SDL_GetClipboardData(ptr, out size);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000242 RID: 578
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_HasClipboardData")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_HasClipboardData(byte* mime_type);

		// Token: 0x06000243 RID: 579 RVA: 0x00002B80 File Offset: 0x00000D80
		public unsafe static SDL.SDLBool SDL_HasClipboardData(string mime_type)
		{
			byte* ptr = SDL.EncodeAsUTF8(mime_type);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_HasClipboardData(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000244 RID: 580
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetClipboardMimeTypes(out UIntPtr num_mime_types);

		// Token: 0x06000245 RID: 581
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumLogicalCPUCores();

		// Token: 0x06000246 RID: 582
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetCPUCacheLineSize();

		// Token: 0x06000247 RID: 583
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasAltiVec();

		// Token: 0x06000248 RID: 584
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasMMX();

		// Token: 0x06000249 RID: 585
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasSSE();

		// Token: 0x0600024A RID: 586
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasSSE2();

		// Token: 0x0600024B RID: 587
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasSSE3();

		// Token: 0x0600024C RID: 588
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasSSE41();

		// Token: 0x0600024D RID: 589
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasSSE42();

		// Token: 0x0600024E RID: 590
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasAVX();

		// Token: 0x0600024F RID: 591
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasAVX2();

		// Token: 0x06000250 RID: 592
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasAVX512F();

		// Token: 0x06000251 RID: 593
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasARMSIMD();

		// Token: 0x06000252 RID: 594
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasNEON();

		// Token: 0x06000253 RID: 595
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasLSX();

		// Token: 0x06000254 RID: 596
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasLASX();

		// Token: 0x06000255 RID: 597
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSystemRAM();

		// Token: 0x06000256 RID: 598
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern UIntPtr SDL_GetSIMDAlignment();

		// Token: 0x06000257 RID: 599
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSystemPageSize();

		// Token: 0x06000258 RID: 600
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumVideoDrivers();

		// Token: 0x06000259 RID: 601
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetVideoDriver(int index);

		// Token: 0x0600025A RID: 602 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public static string SDL_GetVideoDriver(int index)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetVideoDriver(index), false);
		}

		// Token: 0x0600025B RID: 603
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentVideoDriver();

		// Token: 0x0600025C RID: 604 RVA: 0x00002BB3 File Offset: 0x00000DB3
		public static string SDL_GetCurrentVideoDriver()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCurrentVideoDriver(), false);
		}

		// Token: 0x0600025D RID: 605
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_SystemTheme SDL_GetSystemTheme();

		// Token: 0x0600025E RID: 606
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDisplays(out int count);

		// Token: 0x0600025F RID: 607
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetPrimaryDisplay();

		// Token: 0x06000260 RID: 608
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayProperties(uint displayID);

		// Token: 0x06000261 RID: 609
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetDisplayName")]
		private static extern IntPtr INTERNAL_SDL_GetDisplayName(uint displayID);

		// Token: 0x06000262 RID: 610 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static string SDL_GetDisplayName(uint displayID)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetDisplayName(displayID), false);
		}

		// Token: 0x06000263 RID: 611
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetDisplayBounds(uint displayID, out SDL.SDL_Rect rect);

		// Token: 0x06000264 RID: 612
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetDisplayUsableBounds(uint displayID, out SDL.SDL_Rect rect);

		// Token: 0x06000265 RID: 613
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_DisplayOrientation SDL_GetNaturalDisplayOrientation(uint displayID);

		// Token: 0x06000266 RID: 614
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_DisplayOrientation SDL_GetCurrentDisplayOrientation(uint displayID);

		// Token: 0x06000267 RID: 615
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetDisplayContentScale(uint displayID);

		// Token: 0x06000268 RID: 616
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetFullscreenDisplayModes(uint displayID, out int count);

		// Token: 0x06000269 RID: 617
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetClosestFullscreenDisplayMode(uint displayID, int w, int h, float refresh_rate, SDL.SDLBool include_high_density_modes, out SDL.SDL_DisplayMode closest);

		// Token: 0x0600026A RID: 618
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDesktopDisplayMode(uint displayID);

		// Token: 0x0600026B RID: 619
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCurrentDisplayMode(uint displayID);

		// Token: 0x0600026C RID: 620
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForPoint(ref SDL.SDL_Point point);

		// Token: 0x0600026D RID: 621
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForRect(ref SDL.SDL_Rect rect);

		// Token: 0x0600026E RID: 622
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetDisplayForWindow(IntPtr window);

		// Token: 0x0600026F RID: 623
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowPixelDensity(IntPtr window);

		// Token: 0x06000270 RID: 624
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowDisplayScale(IntPtr window);

		// Token: 0x06000271 RID: 625
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowFullscreenMode(IntPtr window, ref SDL.SDL_DisplayMode mode);

		// Token: 0x06000272 RID: 626
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFullscreenMode(IntPtr window);

		// Token: 0x06000273 RID: 627
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowICCProfile(IntPtr window, out UIntPtr size);

		// Token: 0x06000274 RID: 628
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PixelFormat SDL_GetWindowPixelFormat(IntPtr window);

		// Token: 0x06000275 RID: 629
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindows(out int count);

		// Token: 0x06000276 RID: 630
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindow")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateWindow(byte* title, int w, int h, SDL.SDL_WindowFlags flags);

		// Token: 0x06000277 RID: 631 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public unsafe static IntPtr SDL_CreateWindow(string title, int w, int h, SDL.SDL_WindowFlags flags)
		{
			byte* ptr = SDL.EncodeAsUTF8(title);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateWindow(ptr, w, h, flags);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000278 RID: 632
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreatePopupWindow(IntPtr parent, int offset_x, int offset_y, int w, int h, SDL.SDL_WindowFlags flags);

		// Token: 0x06000279 RID: 633
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateWindowWithProperties(uint props);

		// Token: 0x0600027A RID: 634
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowID(IntPtr window);

		// Token: 0x0600027B RID: 635
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFromID(uint id);

		// Token: 0x0600027C RID: 636
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowParent(IntPtr window);

		// Token: 0x0600027D RID: 637
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetWindowProperties(IntPtr window);

		// Token: 0x0600027E RID: 638
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_WindowFlags SDL_GetWindowFlags(IntPtr window);

		// Token: 0x0600027F RID: 639
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowTitle")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetWindowTitle(IntPtr window, byte* title);

		// Token: 0x06000280 RID: 640 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public unsafe static SDL.SDLBool SDL_SetWindowTitle(IntPtr window, string title)
		{
			byte* ptr = SDL.EncodeAsUTF8(title);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetWindowTitle(window, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000281 RID: 641
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowTitle")]
		private static extern IntPtr INTERNAL_SDL_GetWindowTitle(IntPtr window);

		// Token: 0x06000282 RID: 642 RVA: 0x00002C1E File Offset: 0x00000E1E
		public static string SDL_GetWindowTitle(IntPtr window)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetWindowTitle(window), false);
		}

		// Token: 0x06000283 RID: 643
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowIcon(IntPtr window, IntPtr icon);

		// Token: 0x06000284 RID: 644
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowPosition(IntPtr window, int x, int y);

		// Token: 0x06000285 RID: 645
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowPosition(IntPtr window, out int x, out int y);

		// Token: 0x06000286 RID: 646
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowSize(IntPtr window, int w, int h);

		// Token: 0x06000287 RID: 647
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowSize(IntPtr window, out int w, out int h);

		// Token: 0x06000288 RID: 648
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowSafeArea(IntPtr window, out SDL.SDL_Rect rect);

		// Token: 0x06000289 RID: 649
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowAspectRatio(IntPtr window, float min_aspect, float max_aspect);

		// Token: 0x0600028A RID: 650
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowAspectRatio(IntPtr window, out float min_aspect, out float max_aspect);

		// Token: 0x0600028B RID: 651
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right);

		// Token: 0x0600028C RID: 652
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowSizeInPixels(IntPtr window, out int w, out int h);

		// Token: 0x0600028D RID: 653
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowMinimumSize(IntPtr window, int min_w, int min_h);

		// Token: 0x0600028E RID: 654
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowMinimumSize(IntPtr window, out int w, out int h);

		// Token: 0x0600028F RID: 655
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowMaximumSize(IntPtr window, int max_w, int max_h);

		// Token: 0x06000290 RID: 656
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowMaximumSize(IntPtr window, out int w, out int h);

		// Token: 0x06000291 RID: 657
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowBordered(IntPtr window, SDL.SDLBool bordered);

		// Token: 0x06000292 RID: 658
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowResizable(IntPtr window, SDL.SDLBool resizable);

		// Token: 0x06000293 RID: 659
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowAlwaysOnTop(IntPtr window, SDL.SDLBool on_top);

		// Token: 0x06000294 RID: 660
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowFillDocument(IntPtr window, SDL.SDLBool fill);

		// Token: 0x06000295 RID: 661
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShowWindow(IntPtr window);

		// Token: 0x06000296 RID: 662
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HideWindow(IntPtr window);

		// Token: 0x06000297 RID: 663
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RaiseWindow(IntPtr window);

		// Token: 0x06000298 RID: 664
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_MaximizeWindow(IntPtr window);

		// Token: 0x06000299 RID: 665
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_MinimizeWindow(IntPtr window);

		// Token: 0x0600029A RID: 666
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RestoreWindow(IntPtr window);

		// Token: 0x0600029B RID: 667
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowFullscreen(IntPtr window, SDL.SDLBool fullscreen);

		// Token: 0x0600029C RID: 668
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SyncWindow(IntPtr window);

		// Token: 0x0600029D RID: 669
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WindowHasSurface(IntPtr window);

		// Token: 0x0600029E RID: 670
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowSurface(IntPtr window);

		// Token: 0x0600029F RID: 671
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowSurfaceVSync(IntPtr window, int vsync);

		// Token: 0x060002A0 RID: 672
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowSurfaceVSync(IntPtr window, out int vsync);

		// Token: 0x060002A1 RID: 673
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateWindowSurface(IntPtr window);

		// Token: 0x060002A2 RID: 674
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateWindowSurfaceRects(IntPtr window, SDL.SDL_Rect[] rects, int numrects);

		// Token: 0x060002A3 RID: 675
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_DestroyWindowSurface(IntPtr window);

		// Token: 0x060002A4 RID: 676
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowKeyboardGrab(IntPtr window, SDL.SDLBool grabbed);

		// Token: 0x060002A5 RID: 677
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowMouseGrab(IntPtr window, SDL.SDLBool grabbed);

		// Token: 0x060002A6 RID: 678
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowKeyboardGrab(IntPtr window);

		// Token: 0x060002A7 RID: 679
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowMouseGrab(IntPtr window);

		// Token: 0x060002A8 RID: 680
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGrabbedWindow();

		// Token: 0x060002A9 RID: 681
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowMouseRect(IntPtr window, ref SDL.SDL_Rect rect);

		// Token: 0x060002AA RID: 682
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowMouseRect(IntPtr window);

		// Token: 0x060002AB RID: 683
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowOpacity(IntPtr window, float opacity);

		// Token: 0x060002AC RID: 684
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowOpacity(IntPtr window);

		// Token: 0x060002AD RID: 685
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowParent(IntPtr window, IntPtr parent);

		// Token: 0x060002AE RID: 686
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowModal(IntPtr window, SDL.SDLBool modal);

		// Token: 0x060002AF RID: 687
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowFocusable(IntPtr window, SDL.SDLBool focusable);

		// Token: 0x060002B0 RID: 688
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShowWindowSystemMenu(IntPtr window, int x, int y);

		// Token: 0x060002B1 RID: 689
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowHitTest(IntPtr window, SDL.SDL_HitTest callback, IntPtr callback_data);

		// Token: 0x060002B2 RID: 690
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowShape(IntPtr window, IntPtr shape);

		// Token: 0x060002B3 RID: 691
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FlashWindow(IntPtr window, SDL.SDL_FlashOperation operation);

		// Token: 0x060002B4 RID: 692
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowProgressState(IntPtr window, SDL.SDL_ProgressState state);

		// Token: 0x060002B5 RID: 693
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_ProgressState SDL_GetWindowProgressState(IntPtr window);

		// Token: 0x060002B6 RID: 694
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowProgressValue(IntPtr window, float value);

		// Token: 0x060002B7 RID: 695
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetWindowProgressValue(IntPtr window);

		// Token: 0x060002B8 RID: 696
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyWindow(IntPtr window);

		// Token: 0x060002B9 RID: 697
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ScreenSaverEnabled();

		// Token: 0x060002BA RID: 698
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_EnableScreenSaver();

		// Token: 0x060002BB RID: 699
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_DisableScreenSaver();

		// Token: 0x060002BC RID: 700
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_LoadLibrary")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GL_LoadLibrary(byte* path);

		// Token: 0x060002BD RID: 701 RVA: 0x00002C2C File Offset: 0x00000E2C
		public unsafe static SDL.SDLBool SDL_GL_LoadLibrary(string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GL_LoadLibrary(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060002BE RID: 702
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_GetProcAddress")]
		private unsafe static extern IntPtr INTERNAL_SDL_GL_GetProcAddress(byte* proc);

		// Token: 0x060002BF RID: 703 RVA: 0x00002C54 File Offset: 0x00000E54
		public unsafe static IntPtr SDL_GL_GetProcAddress(string proc)
		{
			byte* ptr = SDL.EncodeAsUTF8(proc);
			IntPtr intPtr = SDL.INTERNAL_SDL_GL_GetProcAddress(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060002C0 RID: 704
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_EGL_GetProcAddress")]
		private unsafe static extern IntPtr INTERNAL_SDL_EGL_GetProcAddress(byte* proc);

		// Token: 0x060002C1 RID: 705 RVA: 0x00002C7C File Offset: 0x00000E7C
		public unsafe static IntPtr SDL_EGL_GetProcAddress(string proc)
		{
			byte* ptr = SDL.EncodeAsUTF8(proc);
			IntPtr intPtr = SDL.INTERNAL_SDL_EGL_GetProcAddress(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060002C2 RID: 706
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_UnloadLibrary();

		// Token: 0x060002C3 RID: 707
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GL_ExtensionSupported")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GL_ExtensionSupported(byte* extension);

		// Token: 0x060002C4 RID: 708 RVA: 0x00002CA4 File Offset: 0x00000EA4
		public unsafe static SDL.SDLBool SDL_GL_ExtensionSupported(string extension)
		{
			byte* ptr = SDL.EncodeAsUTF8(extension);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GL_ExtensionSupported(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060002C5 RID: 709
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GL_ResetAttributes();

		// Token: 0x060002C6 RID: 710
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_SetAttribute(SDL.SDL_GLAttr attr, int value);

		// Token: 0x060002C7 RID: 711
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_GetAttribute(SDL.SDL_GLAttr attr, out int value);

		// Token: 0x060002C8 RID: 712
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_CreateContext(IntPtr window);

		// Token: 0x060002C9 RID: 713
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_MakeCurrent(IntPtr window, IntPtr context);

		// Token: 0x060002CA RID: 714
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetCurrentWindow();

		// Token: 0x060002CB RID: 715
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GL_GetCurrentContext();

		// Token: 0x060002CC RID: 716
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_EGL_GetCurrentDisplay();

		// Token: 0x060002CD RID: 717
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_EGL_GetCurrentConfig();

		// Token: 0x060002CE RID: 718
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_EGL_GetWindowSurface(IntPtr window);

		// Token: 0x060002CF RID: 719
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EGL_SetAttributeCallbacks(SDL.SDL_EGLAttribArrayCallback platformAttribCallback, SDL.SDL_EGLIntArrayCallback surfaceAttribCallback, SDL.SDL_EGLIntArrayCallback contextAttribCallback, IntPtr userdata);

		// Token: 0x060002D0 RID: 720
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_SetSwapInterval(int interval);

		// Token: 0x060002D1 RID: 721
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_GetSwapInterval(out int interval);

		// Token: 0x060002D2 RID: 722
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_SwapWindow(IntPtr window);

		// Token: 0x060002D3 RID: 723
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GL_DestroyContext(IntPtr context);

		// Token: 0x060002D4 RID: 724
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowOpenFileDialog")]
		private unsafe static extern void INTERNAL_SDL_ShowOpenFileDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL.SDL_DialogFileFilter[] filters, int nfilters, byte* default_location, SDL.SDLBool allow_many);

		// Token: 0x060002D5 RID: 725 RVA: 0x00002CCC File Offset: 0x00000ECC
		public unsafe static void SDL_ShowOpenFileDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL.SDL_DialogFileFilter[] filters, int nfilters, string default_location, SDL.SDLBool allow_many)
		{
			byte* ptr = SDL.EncodeAsUTF8(default_location);
			SDL.INTERNAL_SDL_ShowOpenFileDialog(callback, userdata, window, filters, nfilters, ptr, allow_many);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060002D6 RID: 726
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowSaveFileDialog")]
		private unsafe static extern void INTERNAL_SDL_ShowSaveFileDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL.SDL_DialogFileFilter[] filters, int nfilters, byte* default_location);

		// Token: 0x060002D7 RID: 727 RVA: 0x00002CFC File Offset: 0x00000EFC
		public unsafe static void SDL_ShowSaveFileDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, SDL.SDL_DialogFileFilter[] filters, int nfilters, string default_location)
		{
			byte* ptr = SDL.EncodeAsUTF8(default_location);
			SDL.INTERNAL_SDL_ShowSaveFileDialog(callback, userdata, window, filters, nfilters, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060002D8 RID: 728
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowOpenFolderDialog")]
		private unsafe static extern void INTERNAL_SDL_ShowOpenFolderDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, byte* default_location, SDL.SDLBool allow_many);

		// Token: 0x060002D9 RID: 729 RVA: 0x00002D28 File Offset: 0x00000F28
		public unsafe static void SDL_ShowOpenFolderDialog(SDL.SDL_DialogFileCallback callback, IntPtr userdata, IntPtr window, string default_location, SDL.SDLBool allow_many)
		{
			byte* ptr = SDL.EncodeAsUTF8(default_location);
			SDL.INTERNAL_SDL_ShowOpenFolderDialog(callback, userdata, window, ptr, allow_many);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060002DA RID: 730
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ShowFileDialogWithProperties(SDL.SDL_FileDialogType type, SDL.SDL_DialogFileCallback callback, IntPtr userdata, uint props);

		// Token: 0x060002DB RID: 731
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GUIDToString")]
		private unsafe static extern void INTERNAL_SDL_GUIDToString(SDL.SDL_GUID guid, byte* pszGUID, int cbGUID);

		// Token: 0x060002DC RID: 732 RVA: 0x00002D54 File Offset: 0x00000F54
		public unsafe static void SDL_GUIDToString(SDL.SDL_GUID guid, string pszGUID, int cbGUID)
		{
			byte* ptr = SDL.EncodeAsUTF8(pszGUID);
			SDL.INTERNAL_SDL_GUIDToString(guid, ptr, cbGUID);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060002DD RID: 733
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_StringToGUID")]
		private unsafe static extern SDL.SDL_GUID INTERNAL_SDL_StringToGUID(byte* pchGUID);

		// Token: 0x060002DE RID: 734 RVA: 0x00002D7C File Offset: 0x00000F7C
		public unsafe static SDL.SDL_GUID SDL_StringToGUID(string pchGUID)
		{
			byte* ptr = SDL.EncodeAsUTF8(pchGUID);
			SDL.SDL_GUID sdl_GUID = SDL.INTERNAL_SDL_StringToGUID(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_GUID;
		}

		// Token: 0x060002DF RID: 735
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PowerState SDL_GetPowerInfo(out int seconds, out int percent);

		// Token: 0x060002E0 RID: 736
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSensors(out int count);

		// Token: 0x060002E1 RID: 737
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetSensorNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetSensorNameForID(uint instance_id);

		// Token: 0x060002E2 RID: 738 RVA: 0x00002DA1 File Offset: 0x00000FA1
		public static string SDL_GetSensorNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetSensorNameForID(instance_id), false);
		}

		// Token: 0x060002E3 RID: 739
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_SensorType SDL_GetSensorTypeForID(uint instance_id);

		// Token: 0x060002E4 RID: 740
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSensorNonPortableTypeForID(uint instance_id);

		// Token: 0x060002E5 RID: 741
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenSensor(uint instance_id);

		// Token: 0x060002E6 RID: 742
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetSensorFromID(uint instance_id);

		// Token: 0x060002E7 RID: 743
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSensorProperties(IntPtr sensor);

		// Token: 0x060002E8 RID: 744
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetSensorName")]
		private static extern IntPtr INTERNAL_SDL_GetSensorName(IntPtr sensor);

		// Token: 0x060002E9 RID: 745 RVA: 0x00002DAF File Offset: 0x00000FAF
		public static string SDL_GetSensorName(IntPtr sensor)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetSensorName(sensor), false);
		}

		// Token: 0x060002EA RID: 746
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_SensorType SDL_GetSensorType(IntPtr sensor);

		// Token: 0x060002EB RID: 747
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetSensorNonPortableType(IntPtr sensor);

		// Token: 0x060002EC RID: 748
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetSensorID(IntPtr sensor);

		// Token: 0x060002ED RID: 749
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern SDL.SDLBool SDL_GetSensorData(IntPtr sensor, float* data, int num_values);

		// Token: 0x060002EE RID: 750
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseSensor(IntPtr sensor);

		// Token: 0x060002EF RID: 751
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateSensors();

		// Token: 0x060002F0 RID: 752
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_LockJoysticks();

		// Token: 0x060002F1 RID: 753
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockJoysticks();

		// Token: 0x060002F2 RID: 754
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasJoystick();

		// Token: 0x060002F3 RID: 755
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetJoysticks(out int count);

		// Token: 0x060002F4 RID: 756
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetJoystickNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetJoystickNameForID(uint instance_id);

		// Token: 0x060002F5 RID: 757 RVA: 0x00002DBD File Offset: 0x00000FBD
		public static string SDL_GetJoystickNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetJoystickNameForID(instance_id), false);
		}

		// Token: 0x060002F6 RID: 758
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetJoystickPathForID")]
		private static extern IntPtr INTERNAL_SDL_GetJoystickPathForID(uint instance_id);

		// Token: 0x060002F7 RID: 759 RVA: 0x00002DCB File Offset: 0x00000FCB
		public static string SDL_GetJoystickPathForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetJoystickPathForID(instance_id), false);
		}

		// Token: 0x060002F8 RID: 760
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetJoystickPlayerIndexForID(uint instance_id);

		// Token: 0x060002F9 RID: 761
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GUID SDL_GetJoystickGUIDForID(uint instance_id);

		// Token: 0x060002FA RID: 762
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickVendorForID(uint instance_id);

		// Token: 0x060002FB RID: 763
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickProductForID(uint instance_id);

		// Token: 0x060002FC RID: 764
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickProductVersionForID(uint instance_id);

		// Token: 0x060002FD RID: 765
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickType SDL_GetJoystickTypeForID(uint instance_id);

		// Token: 0x060002FE RID: 766
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenJoystick(uint instance_id);

		// Token: 0x060002FF RID: 767
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetJoystickFromID(uint instance_id);

		// Token: 0x06000300 RID: 768
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetJoystickFromPlayerIndex(int player_index);

		// Token: 0x06000301 RID: 769
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_AttachVirtualJoystick(ref SDL.SDL_VirtualJoystickDesc desc);

		// Token: 0x06000302 RID: 770
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_DetachVirtualJoystick(uint instance_id);

		// Token: 0x06000303 RID: 771
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsJoystickVirtual(uint instance_id);

		// Token: 0x06000304 RID: 772
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);

		// Token: 0x06000305 RID: 773
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);

		// Token: 0x06000306 RID: 774
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickVirtualButton(IntPtr joystick, int button, SDL.SDLBool down);

		// Token: 0x06000307 RID: 775
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, byte value);

		// Token: 0x06000308 RID: 776
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, SDL.SDLBool down, float x, float y, float pressure);

		// Token: 0x06000309 RID: 777
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern SDL.SDLBool SDL_SendJoystickVirtualSensorData(IntPtr joystick, SDL.SDL_SensorType type, ulong sensor_timestamp, float* data, int num_values);

		// Token: 0x0600030A RID: 778
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetJoystickProperties(IntPtr joystick);

		// Token: 0x0600030B RID: 779
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetJoystickName")]
		private static extern IntPtr INTERNAL_SDL_GetJoystickName(IntPtr joystick);

		// Token: 0x0600030C RID: 780 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public static string SDL_GetJoystickName(IntPtr joystick)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetJoystickName(joystick), false);
		}

		// Token: 0x0600030D RID: 781
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetJoystickPath")]
		private static extern IntPtr INTERNAL_SDL_GetJoystickPath(IntPtr joystick);

		// Token: 0x0600030E RID: 782 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public static string SDL_GetJoystickPath(IntPtr joystick)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetJoystickPath(joystick), false);
		}

		// Token: 0x0600030F RID: 783
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetJoystickPlayerIndex(IntPtr joystick);

		// Token: 0x06000310 RID: 784
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickPlayerIndex(IntPtr joystick, int player_index);

		// Token: 0x06000311 RID: 785
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GUID SDL_GetJoystickGUID(IntPtr joystick);

		// Token: 0x06000312 RID: 786
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickVendor(IntPtr joystick);

		// Token: 0x06000313 RID: 787
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickProduct(IntPtr joystick);

		// Token: 0x06000314 RID: 788
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickProductVersion(IntPtr joystick);

		// Token: 0x06000315 RID: 789
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);

		// Token: 0x06000316 RID: 790
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetJoystickSerial")]
		private static extern IntPtr INTERNAL_SDL_GetJoystickSerial(IntPtr joystick);

		// Token: 0x06000317 RID: 791 RVA: 0x00002DF5 File Offset: 0x00000FF5
		public static string SDL_GetJoystickSerial(IntPtr joystick)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetJoystickSerial(joystick), false);
		}

		// Token: 0x06000318 RID: 792
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickType SDL_GetJoystickType(IntPtr joystick);

		// Token: 0x06000319 RID: 793
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetJoystickGUIDInfo(SDL.SDL_GUID guid, out ushort vendor, out ushort product, out ushort version, out ushort crc16);

		// Token: 0x0600031A RID: 794
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_JoystickConnected(IntPtr joystick);

		// Token: 0x0600031B RID: 795
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetJoystickID(IntPtr joystick);

		// Token: 0x0600031C RID: 796
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumJoystickAxes(IntPtr joystick);

		// Token: 0x0600031D RID: 797
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumJoystickBalls(IntPtr joystick);

		// Token: 0x0600031E RID: 798
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumJoystickHats(IntPtr joystick);

		// Token: 0x0600031F RID: 799
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumJoystickButtons(IntPtr joystick);

		// Token: 0x06000320 RID: 800
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetJoystickEventsEnabled(SDL.SDLBool enabled);

		// Token: 0x06000321 RID: 801
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_JoystickEventsEnabled();

		// Token: 0x06000322 RID: 802
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateJoysticks();

		// Token: 0x06000323 RID: 803
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GetJoystickAxis(IntPtr joystick, int axis);

		// Token: 0x06000324 RID: 804
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);

		// Token: 0x06000325 RID: 805
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);

		// Token: 0x06000326 RID: 806
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern byte SDL_GetJoystickHat(IntPtr joystick, int hat);

		// Token: 0x06000327 RID: 807
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetJoystickButton(IntPtr joystick, int button);

		// Token: 0x06000328 RID: 808
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RumbleJoystick(IntPtr joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		// Token: 0x06000329 RID: 809
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RumbleJoystickTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

		// Token: 0x0600032A RID: 810
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);

		// Token: 0x0600032B RID: 811
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);

		// Token: 0x0600032C RID: 812
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseJoystick(IntPtr joystick);

		// Token: 0x0600032D RID: 813
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);

		// Token: 0x0600032E RID: 814
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, out int percent);

		// Token: 0x0600032F RID: 815
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AddGamepadMapping")]
		private unsafe static extern int INTERNAL_SDL_AddGamepadMapping(byte* mapping);

		// Token: 0x06000330 RID: 816 RVA: 0x00002E04 File Offset: 0x00001004
		public unsafe static int SDL_AddGamepadMapping(string mapping)
		{
			byte* ptr = SDL.EncodeAsUTF8(mapping);
			int num = SDL.INTERNAL_SDL_AddGamepadMapping(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000331 RID: 817
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_AddGamepadMappingsFromIO(IntPtr src, SDL.SDLBool closeio);

		// Token: 0x06000332 RID: 818
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AddGamepadMappingsFromFile")]
		private unsafe static extern int INTERNAL_SDL_AddGamepadMappingsFromFile(byte* file);

		// Token: 0x06000333 RID: 819 RVA: 0x00002E2C File Offset: 0x0000102C
		public unsafe static int SDL_AddGamepadMappingsFromFile(string file)
		{
			byte* ptr = SDL.EncodeAsUTF8(file);
			int num = SDL.INTERNAL_SDL_AddGamepadMappingsFromFile(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000334 RID: 820
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ReloadGamepadMappings();

		// Token: 0x06000335 RID: 821
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadMappings(out int count);

		// Token: 0x06000336 RID: 822
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadMappingForGUID")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMappingForGUID(SDL.SDL_GUID guid);

		// Token: 0x06000337 RID: 823 RVA: 0x00002E51 File Offset: 0x00001051
		public static string SDL_GetGamepadMappingForGUID(SDL.SDL_GUID guid)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadMappingForGUID(guid), true);
		}

		// Token: 0x06000338 RID: 824
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadMapping")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMapping(IntPtr gamepad);

		// Token: 0x06000339 RID: 825 RVA: 0x00002E5F File Offset: 0x0000105F
		public static string SDL_GetGamepadMapping(IntPtr gamepad)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadMapping(gamepad), true);
		}

		// Token: 0x0600033A RID: 826
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetGamepadMapping")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetGamepadMapping(uint instance_id, byte* mapping);

		// Token: 0x0600033B RID: 827 RVA: 0x00002E70 File Offset: 0x00001070
		public unsafe static SDL.SDLBool SDL_SetGamepadMapping(uint instance_id, string mapping)
		{
			byte* ptr = SDL.EncodeAsUTF8(mapping);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetGamepadMapping(instance_id, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600033C RID: 828
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasGamepad();

		// Token: 0x0600033D RID: 829
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepads(out int count);

		// Token: 0x0600033E RID: 830
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsGamepad(uint instance_id);

		// Token: 0x0600033F RID: 831
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadNameForID(uint instance_id);

		// Token: 0x06000340 RID: 832 RVA: 0x00002E96 File Offset: 0x00001096
		public static string SDL_GetGamepadNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadNameForID(instance_id), false);
		}

		// Token: 0x06000341 RID: 833
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadPathForID")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadPathForID(uint instance_id);

		// Token: 0x06000342 RID: 834 RVA: 0x00002EA4 File Offset: 0x000010A4
		public static string SDL_GetGamepadPathForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadPathForID(instance_id), false);
		}

		// Token: 0x06000343 RID: 835
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetGamepadPlayerIndexForID(uint instance_id);

		// Token: 0x06000344 RID: 836
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GUID SDL_GetGamepadGUIDForID(uint instance_id);

		// Token: 0x06000345 RID: 837
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadVendorForID(uint instance_id);

		// Token: 0x06000346 RID: 838
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProductForID(uint instance_id);

		// Token: 0x06000347 RID: 839
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProductVersionForID(uint instance_id);

		// Token: 0x06000348 RID: 840
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadType SDL_GetGamepadTypeForID(uint instance_id);

		// Token: 0x06000349 RID: 841
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadType SDL_GetRealGamepadTypeForID(uint instance_id);

		// Token: 0x0600034A RID: 842
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadMappingForID")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadMappingForID(uint instance_id);

		// Token: 0x0600034B RID: 843 RVA: 0x00002EB2 File Offset: 0x000010B2
		public static string SDL_GetGamepadMappingForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadMappingForID(instance_id), true);
		}

		// Token: 0x0600034C RID: 844
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenGamepad(uint instance_id);

		// Token: 0x0600034D RID: 845
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadFromID(uint instance_id);

		// Token: 0x0600034E RID: 846
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadFromPlayerIndex(int player_index);

		// Token: 0x0600034F RID: 847
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGamepadProperties(IntPtr gamepad);

		// Token: 0x06000350 RID: 848
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGamepadID(IntPtr gamepad);

		// Token: 0x06000351 RID: 849
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadName")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadName(IntPtr gamepad);

		// Token: 0x06000352 RID: 850 RVA: 0x00002EC0 File Offset: 0x000010C0
		public static string SDL_GetGamepadName(IntPtr gamepad)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadName(gamepad), false);
		}

		// Token: 0x06000353 RID: 851
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadPath")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadPath(IntPtr gamepad);

		// Token: 0x06000354 RID: 852 RVA: 0x00002ECE File Offset: 0x000010CE
		public static string SDL_GetGamepadPath(IntPtr gamepad)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadPath(gamepad), false);
		}

		// Token: 0x06000355 RID: 853
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadType SDL_GetGamepadType(IntPtr gamepad);

		// Token: 0x06000356 RID: 854
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadType SDL_GetRealGamepadType(IntPtr gamepad);

		// Token: 0x06000357 RID: 855
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetGamepadPlayerIndex(IntPtr gamepad);

		// Token: 0x06000358 RID: 856
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGamepadPlayerIndex(IntPtr gamepad, int player_index);

		// Token: 0x06000359 RID: 857
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadVendor(IntPtr gamepad);

		// Token: 0x0600035A RID: 858
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProduct(IntPtr gamepad);

		// Token: 0x0600035B RID: 859
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadProductVersion(IntPtr gamepad);

		// Token: 0x0600035C RID: 860
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ushort SDL_GetGamepadFirmwareVersion(IntPtr gamepad);

		// Token: 0x0600035D RID: 861
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadSerial")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadSerial(IntPtr gamepad);

		// Token: 0x0600035E RID: 862 RVA: 0x00002EDC File Offset: 0x000010DC
		public static string SDL_GetGamepadSerial(IntPtr gamepad)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadSerial(gamepad), false);
		}

		// Token: 0x0600035F RID: 863
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetGamepadSteamHandle(IntPtr gamepad);

		// Token: 0x06000360 RID: 864
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_JoystickConnectionState SDL_GetGamepadConnectionState(IntPtr gamepad);

		// Token: 0x06000361 RID: 865
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PowerState SDL_GetGamepadPowerInfo(IntPtr gamepad, out int percent);

		// Token: 0x06000362 RID: 866
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadConnected(IntPtr gamepad);

		// Token: 0x06000363 RID: 867
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadJoystick(IntPtr gamepad);

		// Token: 0x06000364 RID: 868
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGamepadEventsEnabled(SDL.SDLBool enabled);

		// Token: 0x06000365 RID: 869
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadEventsEnabled();

		// Token: 0x06000366 RID: 870
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGamepadBindings(IntPtr gamepad, out int count);

		// Token: 0x06000367 RID: 871
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateGamepads();

		// Token: 0x06000368 RID: 872
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadTypeFromString")]
		private unsafe static extern SDL.SDL_GamepadType INTERNAL_SDL_GetGamepadTypeFromString(byte* str);

		// Token: 0x06000369 RID: 873 RVA: 0x00002EEC File Offset: 0x000010EC
		public unsafe static SDL.SDL_GamepadType SDL_GetGamepadTypeFromString(string str)
		{
			byte* ptr = SDL.EncodeAsUTF8(str);
			SDL.SDL_GamepadType sdl_GamepadType = SDL.INTERNAL_SDL_GetGamepadTypeFromString(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_GamepadType;
		}

		// Token: 0x0600036A RID: 874
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadStringForType")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForType(SDL.SDL_GamepadType type);

		// Token: 0x0600036B RID: 875 RVA: 0x00002F11 File Offset: 0x00001111
		public static string SDL_GetGamepadStringForType(SDL.SDL_GamepadType type)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadStringForType(type), false);
		}

		// Token: 0x0600036C RID: 876
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadAxisFromString")]
		private unsafe static extern SDL.SDL_GamepadAxis INTERNAL_SDL_GetGamepadAxisFromString(byte* str);

		// Token: 0x0600036D RID: 877 RVA: 0x00002F20 File Offset: 0x00001120
		public unsafe static SDL.SDL_GamepadAxis SDL_GetGamepadAxisFromString(string str)
		{
			byte* ptr = SDL.EncodeAsUTF8(str);
			SDL.SDL_GamepadAxis sdl_GamepadAxis = SDL.INTERNAL_SDL_GetGamepadAxisFromString(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_GamepadAxis;
		}

		// Token: 0x0600036E RID: 878
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadStringForAxis")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForAxis(SDL.SDL_GamepadAxis axis);

		// Token: 0x0600036F RID: 879 RVA: 0x00002F45 File Offset: 0x00001145
		public static string SDL_GetGamepadStringForAxis(SDL.SDL_GamepadAxis axis)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadStringForAxis(axis), false);
		}

		// Token: 0x06000370 RID: 880
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadHasAxis(IntPtr gamepad, SDL.SDL_GamepadAxis axis);

		// Token: 0x06000371 RID: 881
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern short SDL_GetGamepadAxis(IntPtr gamepad, SDL.SDL_GamepadAxis axis);

		// Token: 0x06000372 RID: 882
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadButtonFromString")]
		private unsafe static extern SDL.SDL_GamepadButton INTERNAL_SDL_GetGamepadButtonFromString(byte* str);

		// Token: 0x06000373 RID: 883 RVA: 0x00002F54 File Offset: 0x00001154
		public unsafe static SDL.SDL_GamepadButton SDL_GetGamepadButtonFromString(string str)
		{
			byte* ptr = SDL.EncodeAsUTF8(str);
			SDL.SDL_GamepadButton sdl_GamepadButton = SDL.INTERNAL_SDL_GetGamepadButtonFromString(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_GamepadButton;
		}

		// Token: 0x06000374 RID: 884
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadStringForButton")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadStringForButton(SDL.SDL_GamepadButton button);

		// Token: 0x06000375 RID: 885 RVA: 0x00002F79 File Offset: 0x00001179
		public static string SDL_GetGamepadStringForButton(SDL.SDL_GamepadButton button)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadStringForButton(button), false);
		}

		// Token: 0x06000376 RID: 886
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadHasButton(IntPtr gamepad, SDL.SDL_GamepadButton button);

		// Token: 0x06000377 RID: 887
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetGamepadButton(IntPtr gamepad, SDL.SDL_GamepadButton button);

		// Token: 0x06000378 RID: 888
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadButtonLabel SDL_GetGamepadButtonLabelForType(SDL.SDL_GamepadType type, SDL.SDL_GamepadButton button);

		// Token: 0x06000379 RID: 889
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GamepadButtonLabel SDL_GetGamepadButtonLabel(IntPtr gamepad, SDL.SDL_GamepadButton button);

		// Token: 0x0600037A RID: 890
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGamepadTouchpads(IntPtr gamepad);

		// Token: 0x0600037B RID: 891
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGamepadTouchpadFingers(IntPtr gamepad, int touchpad);

		// Token: 0x0600037C RID: 892
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetGamepadTouchpadFinger(IntPtr gamepad, int touchpad, int finger, out SDL.SDLBool down, out float x, out float y, out float pressure);

		// Token: 0x0600037D RID: 893
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadHasSensor(IntPtr gamepad, SDL.SDL_SensorType type);

		// Token: 0x0600037E RID: 894
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGamepadSensorEnabled(IntPtr gamepad, SDL.SDL_SensorType type, SDL.SDLBool enabled);

		// Token: 0x0600037F RID: 895
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GamepadSensorEnabled(IntPtr gamepad, SDL.SDL_SensorType type);

		// Token: 0x06000380 RID: 896
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern float SDL_GetGamepadSensorDataRate(IntPtr gamepad, SDL.SDL_SensorType type);

		// Token: 0x06000381 RID: 897
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public unsafe static extern SDL.SDLBool SDL_GetGamepadSensorData(IntPtr gamepad, SDL.SDL_SensorType type, float* data, int num_values);

		// Token: 0x06000382 RID: 898
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RumbleGamepad(IntPtr gamepad, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

		// Token: 0x06000383 RID: 899
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RumbleGamepadTriggers(IntPtr gamepad, ushort left_rumble, ushort right_rumble, uint duration_ms);

		// Token: 0x06000384 RID: 900
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGamepadLED(IntPtr gamepad, byte red, byte green, byte blue);

		// Token: 0x06000385 RID: 901
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SendGamepadEffect(IntPtr gamepad, IntPtr data, int size);

		// Token: 0x06000386 RID: 902
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseGamepad(IntPtr gamepad);

		// Token: 0x06000387 RID: 903
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForButton")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL.SDL_GamepadButton button);

		// Token: 0x06000388 RID: 904 RVA: 0x00002F87 File Offset: 0x00001187
		public static string SDL_GetGamepadAppleSFSymbolsNameForButton(IntPtr gamepad, SDL.SDL_GamepadButton button)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForButton(gamepad, button), false);
		}

		// Token: 0x06000389 RID: 905
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForAxis")]
		private static extern IntPtr INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL.SDL_GamepadAxis axis);

		// Token: 0x0600038A RID: 906 RVA: 0x00002F96 File Offset: 0x00001196
		public static string SDL_GetGamepadAppleSFSymbolsNameForAxis(IntPtr gamepad, SDL.SDL_GamepadAxis axis)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis), false);
		}

		// Token: 0x0600038B RID: 907
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasKeyboard();

		// Token: 0x0600038C RID: 908
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboards(out int count);

		// Token: 0x0600038D RID: 909
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetKeyboardNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetKeyboardNameForID(uint instance_id);

		// Token: 0x0600038E RID: 910 RVA: 0x00002FA5 File Offset: 0x000011A5
		public static string SDL_GetKeyboardNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetKeyboardNameForID(instance_id), false);
		}

		// Token: 0x0600038F RID: 911
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardFocus();

		// Token: 0x06000390 RID: 912
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetKeyboardState(out int numkeys);

		// Token: 0x06000391 RID: 913
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetKeyboard();

		// Token: 0x06000392 RID: 914
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Keymod SDL_GetModState();

		// Token: 0x06000393 RID: 915
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetModState(SDL.SDL_Keymod modstate);

		// Token: 0x06000394 RID: 916
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetKeyFromScancode(SDL.SDL_Scancode scancode, SDL.SDL_Keymod modstate, SDL.SDLBool key_event);

		// Token: 0x06000395 RID: 917
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Scancode SDL_GetScancodeFromKey(uint key, IntPtr modstate);

		// Token: 0x06000396 RID: 918
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetScancodeName")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetScancodeName(SDL.SDL_Scancode scancode, byte* name);

		// Token: 0x06000397 RID: 919 RVA: 0x00002FB4 File Offset: 0x000011B4
		public unsafe static SDL.SDLBool SDL_SetScancodeName(SDL.SDL_Scancode scancode, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetScancodeName(scancode, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000398 RID: 920
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetScancodeName")]
		private static extern IntPtr INTERNAL_SDL_GetScancodeName(SDL.SDL_Scancode scancode);

		// Token: 0x06000399 RID: 921 RVA: 0x00002FDA File Offset: 0x000011DA
		public static string SDL_GetScancodeName(SDL.SDL_Scancode scancode)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetScancodeName(scancode), false);
		}

		// Token: 0x0600039A RID: 922
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetScancodeFromName")]
		private unsafe static extern SDL.SDL_Scancode INTERNAL_SDL_GetScancodeFromName(byte* name);

		// Token: 0x0600039B RID: 923 RVA: 0x00002FE8 File Offset: 0x000011E8
		public unsafe static SDL.SDL_Scancode SDL_GetScancodeFromName(string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDL_Scancode sdl_Scancode = SDL.INTERNAL_SDL_GetScancodeFromName(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdl_Scancode;
		}

		// Token: 0x0600039C RID: 924
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetKeyName")]
		private static extern IntPtr INTERNAL_SDL_GetKeyName(uint key);

		// Token: 0x0600039D RID: 925 RVA: 0x0000300D File Offset: 0x0000120D
		public static string SDL_GetKeyName(uint key)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetKeyName(key), false);
		}

		// Token: 0x0600039E RID: 926
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetKeyFromName")]
		private unsafe static extern uint INTERNAL_SDL_GetKeyFromName(byte* name);

		// Token: 0x0600039F RID: 927 RVA: 0x0000301C File Offset: 0x0000121C
		public unsafe static uint SDL_GetKeyFromName(string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			uint num = SDL.INTERNAL_SDL_GetKeyFromName(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x060003A0 RID: 928
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StartTextInput(IntPtr window);

		// Token: 0x060003A1 RID: 929
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StartTextInputWithProperties(IntPtr window, uint props);

		// Token: 0x060003A2 RID: 930
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TextInputActive(IntPtr window);

		// Token: 0x060003A3 RID: 931
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StopTextInput(IntPtr window);

		// Token: 0x060003A4 RID: 932
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClearComposition(IntPtr window);

		// Token: 0x060003A5 RID: 933
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextInputArea(IntPtr window, ref SDL.SDL_Rect rect, int cursor);

		// Token: 0x060003A6 RID: 934
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextInputArea(IntPtr window, out SDL.SDL_Rect rect, out int cursor);

		// Token: 0x060003A7 RID: 935
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasScreenKeyboardSupport();

		// Token: 0x060003A8 RID: 936
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ScreenKeyboardShown(IntPtr window);

		// Token: 0x060003A9 RID: 937
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasMouse();

		// Token: 0x060003AA RID: 938
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMice(out int count);

		// Token: 0x060003AB RID: 939
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetMouseNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetMouseNameForID(uint instance_id);

		// Token: 0x060003AC RID: 940 RVA: 0x00003041 File Offset: 0x00001241
		public static string SDL_GetMouseNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetMouseNameForID(instance_id), false);
		}

		// Token: 0x060003AD RID: 941
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetMouseFocus();

		// Token: 0x060003AE RID: 942
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_MouseButtonFlags SDL_GetMouseState(out float x, out float y);

		// Token: 0x060003AF RID: 943
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_MouseButtonFlags SDL_GetGlobalMouseState(out float x, out float y);

		// Token: 0x060003B0 RID: 944
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_MouseButtonFlags SDL_GetRelativeMouseState(out float x, out float y);

		// Token: 0x060003B1 RID: 945
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_WarpMouseInWindow(IntPtr window, float x, float y);

		// Token: 0x060003B2 RID: 946
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WarpMouseGlobal(float x, float y);

		// Token: 0x060003B3 RID: 947
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRelativeMouseTransform(SDL.SDL_MouseMotionTransformCallback callback, IntPtr userdata);

		// Token: 0x060003B4 RID: 948
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetWindowRelativeMouseMode(IntPtr window, SDL.SDLBool enabled);

		// Token: 0x060003B5 RID: 949
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetWindowRelativeMouseMode(IntPtr window);

		// Token: 0x060003B6 RID: 950
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CaptureMouse(SDL.SDLBool enabled);

		// Token: 0x060003B7 RID: 951
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateCursor(IntPtr data, IntPtr mask, int w, int h, int hot_x, int hot_y);

		// Token: 0x060003B8 RID: 952
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

		// Token: 0x060003B9 RID: 953
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateAnimatedCursor(SDL.SDL_CursorFrameInfo[] frames, int frame_count, int hot_x, int hot_y);

		// Token: 0x060003BA RID: 954
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSystemCursor(SDL.SDL_SystemCursor id);

		// Token: 0x060003BB RID: 955
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetCursor(IntPtr cursor);

		// Token: 0x060003BC RID: 956
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetCursor();

		// Token: 0x060003BD RID: 957
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDefaultCursor();

		// Token: 0x060003BE RID: 958
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyCursor(IntPtr cursor);

		// Token: 0x060003BF RID: 959
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShowCursor();

		// Token: 0x060003C0 RID: 960
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HideCursor();

		// Token: 0x060003C1 RID: 961
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CursorVisible();

		// Token: 0x060003C2 RID: 962
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTouchDevices(out int count);

		// Token: 0x060003C3 RID: 963
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetTouchDeviceName")]
		private static extern IntPtr INTERNAL_SDL_GetTouchDeviceName(ulong touchID);

		// Token: 0x060003C4 RID: 964 RVA: 0x0000304F File Offset: 0x0000124F
		public static string SDL_GetTouchDeviceName(ulong touchID)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetTouchDeviceName(touchID), false);
		}

		// Token: 0x060003C5 RID: 965
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_TouchDeviceType SDL_GetTouchDeviceType(ulong touchID);

		// Token: 0x060003C6 RID: 966
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTouchFingers(ulong touchID, out int count);

		// Token: 0x060003C7 RID: 967
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PenDeviceType SDL_GetPenDeviceType(uint instance_id);

		// Token: 0x060003C8 RID: 968
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PumpEvents();

		// Token: 0x060003C9 RID: 969
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_PeepEvents([Out] SDL.SDL_Event[] events, int numevents, SDL.SDL_EventAction action, uint minType, uint maxType);

		// Token: 0x060003CA RID: 970
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasEvent(uint type);

		// Token: 0x060003CB RID: 971
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HasEvents(uint minType, uint maxType);

		// Token: 0x060003CC RID: 972
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvent(uint type);

		// Token: 0x060003CD RID: 973
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FlushEvents(uint minType, uint maxType);

		// Token: 0x060003CE RID: 974
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PollEvent(out SDL.SDL_Event @event);

		// Token: 0x060003CF RID: 975
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitEvent(out SDL.SDL_Event @event);

		// Token: 0x060003D0 RID: 976
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitEventTimeout(out SDL.SDL_Event @event, int timeoutMS);

		// Token: 0x060003D1 RID: 977
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PushEvent(ref SDL.SDL_Event @event);

		// Token: 0x060003D2 RID: 978
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventFilter(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x060003D3 RID: 979
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetEventFilter(out SDL.SDL_EventFilter filter, out IntPtr userdata);

		// Token: 0x060003D4 RID: 980
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AddEventWatch(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x060003D5 RID: 981
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RemoveEventWatch(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x060003D6 RID: 982
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_FilterEvents(SDL.SDL_EventFilter filter, IntPtr userdata);

		// Token: 0x060003D7 RID: 983
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetEventEnabled(uint type, SDL.SDLBool enabled);

		// Token: 0x060003D8 RID: 984
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_EventEnabled(uint type);

		// Token: 0x060003D9 RID: 985
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_RegisterEvents(int numevents);

		// Token: 0x060003DA RID: 986
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetWindowFromEvent(ref SDL.SDL_Event @event);

		// Token: 0x060003DB RID: 987
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetEventDescription")]
		private unsafe static extern int INTERNAL_SDL_GetEventDescription(ref SDL.SDL_Event @event, byte* buf, int buflen);

		// Token: 0x060003DC RID: 988 RVA: 0x00003060 File Offset: 0x00001260
		public unsafe static int SDL_GetEventDescription(ref SDL.SDL_Event @event, string buf, int buflen)
		{
			byte* ptr = SDL.EncodeAsUTF8(buf);
			int num = SDL.INTERNAL_SDL_GetEventDescription(ref @event, ptr, buflen);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x060003DD RID: 989
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetBasePath")]
		private static extern IntPtr INTERNAL_SDL_GetBasePath();

		// Token: 0x060003DE RID: 990 RVA: 0x00003087 File Offset: 0x00001287
		public static string SDL_GetBasePath()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetBasePath(), false);
		}

		// Token: 0x060003DF RID: 991
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPrefPath")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetPrefPath(byte* org, byte* app);

		// Token: 0x060003E0 RID: 992 RVA: 0x00003094 File Offset: 0x00001294
		public unsafe static string SDL_GetPrefPath(string org, string app)
		{
			byte* ptr = SDL.EncodeAsUTF8(org);
			byte* ptr2 = SDL.EncodeAsUTF8(app);
			string text = SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetPrefPath(ptr, ptr2), true);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return text;
		}

		// Token: 0x060003E1 RID: 993
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetUserFolder")]
		private static extern IntPtr INTERNAL_SDL_GetUserFolder(SDL.SDL_Folder folder);

		// Token: 0x060003E2 RID: 994 RVA: 0x000030D2 File Offset: 0x000012D2
		public static string SDL_GetUserFolder(SDL.SDL_Folder folder)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetUserFolder(folder), false);
		}

		// Token: 0x060003E3 RID: 995
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateDirectory")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_CreateDirectory(byte* path);

		// Token: 0x060003E4 RID: 996 RVA: 0x000030E0 File Offset: 0x000012E0
		public unsafe static SDL.SDLBool SDL_CreateDirectory(string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_CreateDirectory(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060003E5 RID: 997
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_EnumerateDirectory")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_EnumerateDirectory(byte* path, SDL.SDL_EnumerateDirectoryCallback callback, IntPtr userdata);

		// Token: 0x060003E6 RID: 998 RVA: 0x00003108 File Offset: 0x00001308
		public unsafe static SDL.SDLBool SDL_EnumerateDirectory(string path, SDL.SDL_EnumerateDirectoryCallback callback, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_EnumerateDirectory(ptr, callback, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060003E7 RID: 999
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RemovePath")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RemovePath(byte* path);

		// Token: 0x060003E8 RID: 1000 RVA: 0x00003130 File Offset: 0x00001330
		public unsafe static SDL.SDLBool SDL_RemovePath(string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RemovePath(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060003E9 RID: 1001
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RenamePath")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RenamePath(byte* oldpath, byte* newpath);

		// Token: 0x060003EA RID: 1002 RVA: 0x00003158 File Offset: 0x00001358
		public unsafe static SDL.SDLBool SDL_RenamePath(string oldpath, string newpath)
		{
			byte* ptr = SDL.EncodeAsUTF8(oldpath);
			byte* ptr2 = SDL.EncodeAsUTF8(newpath);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RenamePath(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x060003EB RID: 1003
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CopyFile")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_CopyFile(byte* oldpath, byte* newpath);

		// Token: 0x060003EC RID: 1004 RVA: 0x00003190 File Offset: 0x00001390
		public unsafe static SDL.SDLBool SDL_CopyFile(string oldpath, string newpath)
		{
			byte* ptr = SDL.EncodeAsUTF8(oldpath);
			byte* ptr2 = SDL.EncodeAsUTF8(newpath);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_CopyFile(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x060003ED RID: 1005
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPathInfo")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GetPathInfo(byte* path, out SDL.SDL_PathInfo info);

		// Token: 0x060003EE RID: 1006 RVA: 0x000031C8 File Offset: 0x000013C8
		public unsafe static SDL.SDLBool SDL_GetPathInfo(string path, out SDL.SDL_PathInfo info)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GetPathInfo(ptr, out info);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060003EF RID: 1007
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GlobDirectory")]
		private unsafe static extern IntPtr INTERNAL_SDL_GlobDirectory(byte* path, byte* pattern, SDL.SDL_GlobFlags flags, out int count);

		// Token: 0x060003F0 RID: 1008 RVA: 0x000031F0 File Offset: 0x000013F0
		public unsafe static IntPtr SDL_GlobDirectory(string path, string pattern, SDL.SDL_GlobFlags flags, out int count)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			byte* ptr2 = SDL.EncodeAsUTF8(pattern);
			IntPtr intPtr = SDL.INTERNAL_SDL_GlobDirectory(ptr, ptr2, flags, out count);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return intPtr;
		}

		// Token: 0x060003F1 RID: 1009
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetCurrentDirectory")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentDirectory();

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000322A File Offset: 0x0000142A
		public static string SDL_GetCurrentDirectory()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetCurrentDirectory(), true);
		}

		// Token: 0x060003F3 RID: 1011
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GPUSupportsShaderFormats")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GPUSupportsShaderFormats(SDL.SDL_GPUShaderFormat format_flags, byte* name);

		// Token: 0x060003F4 RID: 1012 RVA: 0x00003238 File Offset: 0x00001438
		public unsafe static SDL.SDLBool SDL_GPUSupportsShaderFormats(SDL.SDL_GPUShaderFormat format_flags, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GPUSupportsShaderFormats(format_flags, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060003F5 RID: 1013
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GPUSupportsProperties(uint props);

		// Token: 0x060003F6 RID: 1014
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateGPUDevice")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateGPUDevice(SDL.SDL_GPUShaderFormat format_flags, SDL.SDLBool debug_mode, byte* name);

		// Token: 0x060003F7 RID: 1015 RVA: 0x00003260 File Offset: 0x00001460
		public unsafe static IntPtr SDL_CreateGPUDevice(SDL.SDL_GPUShaderFormat format_flags, SDL.SDLBool debug_mode, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateGPUDevice(format_flags, debug_mode, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060003F8 RID: 1016
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUDeviceWithProperties(uint props);

		// Token: 0x060003F9 RID: 1017
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyGPUDevice(IntPtr device);

		// Token: 0x060003FA RID: 1018
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumGPUDrivers();

		// Token: 0x060003FB RID: 1019
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGPUDriver")]
		private static extern IntPtr INTERNAL_SDL_GetGPUDriver(int index);

		// Token: 0x060003FC RID: 1020 RVA: 0x00003287 File Offset: 0x00001487
		public static string SDL_GetGPUDriver(int index)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGPUDriver(index), false);
		}

		// Token: 0x060003FD RID: 1021
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetGPUDeviceDriver")]
		private static extern IntPtr INTERNAL_SDL_GetGPUDeviceDriver(IntPtr device);

		// Token: 0x060003FE RID: 1022 RVA: 0x00003295 File Offset: 0x00001495
		public static string SDL_GetGPUDeviceDriver(IntPtr device)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetGPUDeviceDriver(device), false);
		}

		// Token: 0x060003FF RID: 1023
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GPUShaderFormat SDL_GetGPUShaderFormats(IntPtr device);

		// Token: 0x06000400 RID: 1024
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetGPUDeviceProperties(IntPtr device);

		// Token: 0x06000401 RID: 1025
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUComputePipeline(IntPtr device, ref SDL.SDL_GPUComputePipelineCreateInfo createinfo);

		// Token: 0x06000402 RID: 1026
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUGraphicsPipeline(IntPtr device, ref SDL.SDL_GPUGraphicsPipelineCreateInfo createinfo);

		// Token: 0x06000403 RID: 1027
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUSampler(IntPtr device, ref SDL.SDL_GPUSamplerCreateInfo createinfo);

		// Token: 0x06000404 RID: 1028
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUShader(IntPtr device, ref SDL.SDL_GPUShaderCreateInfo createinfo);

		// Token: 0x06000405 RID: 1029
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUTexture(IntPtr device, ref SDL.SDL_GPUTextureCreateInfo createinfo);

		// Token: 0x06000406 RID: 1030
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUBuffer(IntPtr device, ref SDL.SDL_GPUBufferCreateInfo createinfo);

		// Token: 0x06000407 RID: 1031
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPUTransferBuffer(IntPtr device, ref SDL.SDL_GPUTransferBufferCreateInfo createinfo);

		// Token: 0x06000408 RID: 1032
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetGPUBufferName")]
		private unsafe static extern void INTERNAL_SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, byte* text);

		// Token: 0x06000409 RID: 1033 RVA: 0x000032A4 File Offset: 0x000014A4
		public unsafe static void SDL_SetGPUBufferName(IntPtr device, IntPtr buffer, string text)
		{
			byte* ptr = SDL.EncodeAsUTF8(text);
			SDL.INTERNAL_SDL_SetGPUBufferName(device, buffer, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x0600040A RID: 1034
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetGPUTextureName")]
		private unsafe static extern void INTERNAL_SDL_SetGPUTextureName(IntPtr device, IntPtr texture, byte* text);

		// Token: 0x0600040B RID: 1035 RVA: 0x000032CC File Offset: 0x000014CC
		public unsafe static void SDL_SetGPUTextureName(IntPtr device, IntPtr texture, string text)
		{
			byte* ptr = SDL.EncodeAsUTF8(text);
			SDL.INTERNAL_SDL_SetGPUTextureName(device, texture, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x0600040C RID: 1036
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_InsertGPUDebugLabel")]
		private unsafe static extern void INTERNAL_SDL_InsertGPUDebugLabel(IntPtr command_buffer, byte* text);

		// Token: 0x0600040D RID: 1037 RVA: 0x000032F4 File Offset: 0x000014F4
		public unsafe static void SDL_InsertGPUDebugLabel(IntPtr command_buffer, string text)
		{
			byte* ptr = SDL.EncodeAsUTF8(text);
			SDL.INTERNAL_SDL_InsertGPUDebugLabel(command_buffer, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x0600040E RID: 1038
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_PushGPUDebugGroup")]
		private unsafe static extern void INTERNAL_SDL_PushGPUDebugGroup(IntPtr command_buffer, byte* name);

		// Token: 0x0600040F RID: 1039 RVA: 0x0000331C File Offset: 0x0000151C
		public unsafe static void SDL_PushGPUDebugGroup(IntPtr command_buffer, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.INTERNAL_SDL_PushGPUDebugGroup(command_buffer, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x06000410 RID: 1040
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PopGPUDebugGroup(IntPtr command_buffer);

		// Token: 0x06000411 RID: 1041
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUTexture(IntPtr device, IntPtr texture);

		// Token: 0x06000412 RID: 1042
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUSampler(IntPtr device, IntPtr sampler);

		// Token: 0x06000413 RID: 1043
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUBuffer(IntPtr device, IntPtr buffer);

		// Token: 0x06000414 RID: 1044
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer);

		// Token: 0x06000415 RID: 1045
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUComputePipeline(IntPtr device, IntPtr compute_pipeline);

		// Token: 0x06000416 RID: 1046
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUShader(IntPtr device, IntPtr shader);

		// Token: 0x06000417 RID: 1047
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUGraphicsPipeline(IntPtr device, IntPtr graphics_pipeline);

		// Token: 0x06000418 RID: 1048
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_AcquireGPUCommandBuffer(IntPtr device);

		// Token: 0x06000419 RID: 1049
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PushGPUVertexUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

		// Token: 0x0600041A RID: 1050
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PushGPUFragmentUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

		// Token: 0x0600041B RID: 1051
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_PushGPUComputeUniformData(IntPtr command_buffer, uint slot_index, IntPtr data, uint length);

		// Token: 0x0600041C RID: 1052
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_BeginGPURenderPass(IntPtr command_buffer, SDL.SDL_GPUColorTargetInfo[] color_target_infos, uint num_color_targets, ref SDL.SDL_GPUDepthStencilTargetInfo depth_stencil_target_info);

		// Token: 0x0600041D RID: 1053
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUGraphicsPipeline(IntPtr render_pass, IntPtr graphics_pipeline);

		// Token: 0x0600041E RID: 1054
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUViewport(IntPtr render_pass, ref SDL.SDL_GPUViewport viewport);

		// Token: 0x0600041F RID: 1055
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUScissor(IntPtr render_pass, ref SDL.SDL_Rect scissor);

		// Token: 0x06000420 RID: 1056
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUBlendConstants(IntPtr render_pass, SDL.SDL_FColor blend_constants);

		// Token: 0x06000421 RID: 1057
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetGPUStencilReference(IntPtr render_pass, byte reference);

		// Token: 0x06000422 RID: 1058
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexBuffers(IntPtr render_pass, uint first_slot, SDL.SDL_GPUBufferBinding[] bindings, uint num_bindings);

		// Token: 0x06000423 RID: 1059
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUIndexBuffer(IntPtr render_pass, ref SDL.SDL_GPUBufferBinding binding, SDL.SDL_GPUIndexElementSize index_element_size);

		// Token: 0x06000424 RID: 1060
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexSamplers(IntPtr render_pass, uint first_slot, SDL.SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		// Token: 0x06000425 RID: 1061
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexStorageTextures(IntPtr render_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		// Token: 0x06000426 RID: 1062
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUVertexStorageBuffers(IntPtr render_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		// Token: 0x06000427 RID: 1063
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentSamplers(IntPtr render_pass, uint first_slot, SDL.SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		// Token: 0x06000428 RID: 1064
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentStorageTextures(IntPtr render_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		// Token: 0x06000429 RID: 1065
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUFragmentStorageBuffers(IntPtr render_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		// Token: 0x0600042A RID: 1066
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUIndexedPrimitives(IntPtr render_pass, uint num_indices, uint num_instances, uint first_index, int vertex_offset, uint first_instance);

		// Token: 0x0600042B RID: 1067
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUPrimitives(IntPtr render_pass, uint num_vertices, uint num_instances, uint first_vertex, uint first_instance);

		// Token: 0x0600042C RID: 1068
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count);

		// Token: 0x0600042D RID: 1069
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DrawGPUIndexedPrimitivesIndirect(IntPtr render_pass, IntPtr buffer, uint offset, uint draw_count);

		// Token: 0x0600042E RID: 1070
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPURenderPass(IntPtr render_pass);

		// Token: 0x0600042F RID: 1071
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_BeginGPUComputePass(IntPtr command_buffer, SDL.SDL_GPUStorageTextureReadWriteBinding[] storage_texture_bindings, uint num_storage_texture_bindings, SDL.SDL_GPUStorageBufferReadWriteBinding[] storage_buffer_bindings, uint num_storage_buffer_bindings);

		// Token: 0x06000430 RID: 1072
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputePipeline(IntPtr compute_pass, IntPtr compute_pipeline);

		// Token: 0x06000431 RID: 1073
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeSamplers(IntPtr compute_pass, uint first_slot, SDL.SDL_GPUTextureSamplerBinding[] texture_sampler_bindings, uint num_bindings);

		// Token: 0x06000432 RID: 1074
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeStorageTextures(IntPtr compute_pass, uint first_slot, IntPtr[] storage_textures, uint num_bindings);

		// Token: 0x06000433 RID: 1075
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BindGPUComputeStorageBuffers(IntPtr compute_pass, uint first_slot, IntPtr[] storage_buffers, uint num_bindings);

		// Token: 0x06000434 RID: 1076
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DispatchGPUCompute(IntPtr compute_pass, uint groupcount_x, uint groupcount_y, uint groupcount_z);

		// Token: 0x06000435 RID: 1077
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DispatchGPUComputeIndirect(IntPtr compute_pass, IntPtr buffer, uint offset);

		// Token: 0x06000436 RID: 1078
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPUComputePass(IntPtr compute_pass);

		// Token: 0x06000437 RID: 1079
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_MapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer, SDL.SDLBool cycle);

		// Token: 0x06000438 RID: 1080
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnmapGPUTransferBuffer(IntPtr device, IntPtr transfer_buffer);

		// Token: 0x06000439 RID: 1081
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_BeginGPUCopyPass(IntPtr command_buffer);

		// Token: 0x0600043A RID: 1082
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UploadToGPUTexture(IntPtr copy_pass, ref SDL.SDL_GPUTextureTransferInfo source, ref SDL.SDL_GPUTextureRegion destination, SDL.SDLBool cycle);

		// Token: 0x0600043B RID: 1083
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UploadToGPUBuffer(IntPtr copy_pass, ref SDL.SDL_GPUTransferBufferLocation source, ref SDL.SDL_GPUBufferRegion destination, SDL.SDLBool cycle);

		// Token: 0x0600043C RID: 1084
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CopyGPUTextureToTexture(IntPtr copy_pass, ref SDL.SDL_GPUTextureLocation source, ref SDL.SDL_GPUTextureLocation destination, uint w, uint h, uint d, SDL.SDLBool cycle);

		// Token: 0x0600043D RID: 1085
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CopyGPUBufferToBuffer(IntPtr copy_pass, ref SDL.SDL_GPUBufferLocation source, ref SDL.SDL_GPUBufferLocation destination, uint size, SDL.SDLBool cycle);

		// Token: 0x0600043E RID: 1086
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DownloadFromGPUTexture(IntPtr copy_pass, ref SDL.SDL_GPUTextureRegion source, ref SDL.SDL_GPUTextureTransferInfo destination);

		// Token: 0x0600043F RID: 1087
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DownloadFromGPUBuffer(IntPtr copy_pass, ref SDL.SDL_GPUBufferRegion source, ref SDL.SDL_GPUTransferBufferLocation destination);

		// Token: 0x06000440 RID: 1088
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_EndGPUCopyPass(IntPtr copy_pass);

		// Token: 0x06000441 RID: 1089
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GenerateMipmapsForGPUTexture(IntPtr command_buffer, IntPtr texture);

		// Token: 0x06000442 RID: 1090
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_BlitGPUTexture(IntPtr command_buffer, ref SDL.SDL_GPUBlitInfo info);

		// Token: 0x06000443 RID: 1091
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WindowSupportsGPUSwapchainComposition(IntPtr device, IntPtr window, SDL.SDL_GPUSwapchainComposition swapchain_composition);

		// Token: 0x06000444 RID: 1092
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WindowSupportsGPUPresentMode(IntPtr device, IntPtr window, SDL.SDL_GPUPresentMode present_mode);

		// Token: 0x06000445 RID: 1093
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ClaimWindowForGPUDevice(IntPtr device, IntPtr window);

		// Token: 0x06000446 RID: 1094
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseWindowFromGPUDevice(IntPtr device, IntPtr window);

		// Token: 0x06000447 RID: 1095
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGPUSwapchainParameters(IntPtr device, IntPtr window, SDL.SDL_GPUSwapchainComposition swapchain_composition, SDL.SDL_GPUPresentMode present_mode);

		// Token: 0x06000448 RID: 1096
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGPUAllowedFramesInFlight(IntPtr device, uint allowed_frames_in_flight);

		// Token: 0x06000449 RID: 1097
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GPUTextureFormat SDL_GetGPUSwapchainTextureFormat(IntPtr device, IntPtr window);

		// Token: 0x0600044A RID: 1098
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AcquireGPUSwapchainTexture(IntPtr command_buffer, IntPtr window, out IntPtr swapchain_texture, out uint swapchain_texture_width, out uint swapchain_texture_height);

		// Token: 0x0600044B RID: 1099
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitForGPUSwapchain(IntPtr device, IntPtr window);

		// Token: 0x0600044C RID: 1100
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitAndAcquireGPUSwapchainTexture(IntPtr command_buffer, IntPtr window, out IntPtr swapchain_texture, out uint swapchain_texture_width, out uint swapchain_texture_height);

		// Token: 0x0600044D RID: 1101
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SubmitGPUCommandBuffer(IntPtr command_buffer);

		// Token: 0x0600044E RID: 1102
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_SubmitGPUCommandBufferAndAcquireFence(IntPtr command_buffer);

		// Token: 0x0600044F RID: 1103
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CancelGPUCommandBuffer(IntPtr command_buffer);

		// Token: 0x06000450 RID: 1104
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitForGPUIdle(IntPtr device);

		// Token: 0x06000451 RID: 1105
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitForGPUFences(IntPtr device, SDL.SDLBool wait_all, IntPtr[] fences, uint num_fences);

		// Token: 0x06000452 RID: 1106
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_QueryGPUFence(IntPtr device, IntPtr fence);

		// Token: 0x06000453 RID: 1107
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ReleaseGPUFence(IntPtr device, IntPtr fence);

		// Token: 0x06000454 RID: 1108
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GPUTextureFormatTexelBlockSize(SDL.SDL_GPUTextureFormat format);

		// Token: 0x06000455 RID: 1109
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GPUTextureSupportsFormat(IntPtr device, SDL.SDL_GPUTextureFormat format, SDL.SDL_GPUTextureType type, SDL.SDL_GPUTextureUsageFlags usage);

		// Token: 0x06000456 RID: 1110
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GPUTextureSupportsSampleCount(IntPtr device, SDL.SDL_GPUTextureFormat format, SDL.SDL_GPUSampleCount sample_count);

		// Token: 0x06000457 RID: 1111
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_CalculateGPUTextureFormatSize(SDL.SDL_GPUTextureFormat format, uint width, uint height, uint depth_or_layer_count);

		// Token: 0x06000458 RID: 1112
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_PixelFormat SDL_GetPixelFormatFromGPUTextureFormat(SDL.SDL_GPUTextureFormat format);

		// Token: 0x06000459 RID: 1113
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_GPUTextureFormat SDL_GetGPUTextureFormatFromPixelFormat(SDL.SDL_PixelFormat format);

		// Token: 0x0600045A RID: 1114
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetHaptics(out int count);

		// Token: 0x0600045B RID: 1115
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHapticNameForID")]
		private static extern IntPtr INTERNAL_SDL_GetHapticNameForID(uint instance_id);

		// Token: 0x0600045C RID: 1116 RVA: 0x00003342 File Offset: 0x00001542
		public static string SDL_GetHapticNameForID(uint instance_id)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetHapticNameForID(instance_id), false);
		}

		// Token: 0x0600045D RID: 1117
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenHaptic(uint instance_id);

		// Token: 0x0600045E RID: 1118
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetHapticFromID(uint instance_id);

		// Token: 0x0600045F RID: 1119
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetHapticID(IntPtr haptic);

		// Token: 0x06000460 RID: 1120
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHapticName")]
		private static extern IntPtr INTERNAL_SDL_GetHapticName(IntPtr haptic);

		// Token: 0x06000461 RID: 1121 RVA: 0x00003350 File Offset: 0x00001550
		public static string SDL_GetHapticName(IntPtr haptic)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetHapticName(haptic), false);
		}

		// Token: 0x06000462 RID: 1122
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsMouseHaptic();

		// Token: 0x06000463 RID: 1123
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenHapticFromMouse();

		// Token: 0x06000464 RID: 1124
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsJoystickHaptic(IntPtr joystick);

		// Token: 0x06000465 RID: 1125
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenHapticFromJoystick(IntPtr joystick);

		// Token: 0x06000466 RID: 1126
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_CloseHaptic(IntPtr haptic);

		// Token: 0x06000467 RID: 1127
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetMaxHapticEffects(IntPtr haptic);

		// Token: 0x06000468 RID: 1128
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetMaxHapticEffectsPlaying(IntPtr haptic);

		// Token: 0x06000469 RID: 1129
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetHapticFeatures(IntPtr haptic);

		// Token: 0x0600046A RID: 1130
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumHapticAxes(IntPtr haptic);

		// Token: 0x0600046B RID: 1131
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HapticEffectSupported(IntPtr haptic, ref SDL.SDL_HapticEffect effect);

		// Token: 0x0600046C RID: 1132
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_CreateHapticEffect(IntPtr haptic, ref SDL.SDL_HapticEffect effect);

		// Token: 0x0600046D RID: 1133
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateHapticEffect(IntPtr haptic, int effect, ref SDL.SDL_HapticEffect data);

		// Token: 0x0600046E RID: 1134
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RunHapticEffect(IntPtr haptic, int effect, uint iterations);

		// Token: 0x0600046F RID: 1135
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StopHapticEffect(IntPtr haptic, int effect);

		// Token: 0x06000470 RID: 1136
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyHapticEffect(IntPtr haptic, int effect);

		// Token: 0x06000471 RID: 1137
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetHapticEffectStatus(IntPtr haptic, int effect);

		// Token: 0x06000472 RID: 1138
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetHapticGain(IntPtr haptic, int gain);

		// Token: 0x06000473 RID: 1139
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetHapticAutocenter(IntPtr haptic, int autocenter);

		// Token: 0x06000474 RID: 1140
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PauseHaptic(IntPtr haptic);

		// Token: 0x06000475 RID: 1141
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ResumeHaptic(IntPtr haptic);

		// Token: 0x06000476 RID: 1142
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StopHapticEffects(IntPtr haptic);

		// Token: 0x06000477 RID: 1143
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_HapticRumbleSupported(IntPtr haptic);

		// Token: 0x06000478 RID: 1144
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_InitHapticRumble(IntPtr haptic);

		// Token: 0x06000479 RID: 1145
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_PlayHapticRumble(IntPtr haptic, float strength, uint length);

		// Token: 0x0600047A RID: 1146
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StopHapticRumble(IntPtr haptic);

		// Token: 0x0600047B RID: 1147
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_init();

		// Token: 0x0600047C RID: 1148
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_exit();

		// Token: 0x0600047D RID: 1149
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_hid_device_change_count();

		// Token: 0x0600047E RID: 1150
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_hid_enumerate(ushort vendor_id, ushort product_id);

		// Token: 0x0600047F RID: 1151
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_hid_free_enumeration(IntPtr devs);

		// Token: 0x06000480 RID: 1152
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_open")]
		private unsafe static extern IntPtr INTERNAL_SDL_hid_open(ushort vendor_id, ushort product_id, byte* serial_number);

		// Token: 0x06000481 RID: 1153 RVA: 0x00003360 File Offset: 0x00001560
		public unsafe static IntPtr SDL_hid_open(ushort vendor_id, ushort product_id, string serial_number)
		{
			byte* ptr = SDL.EncodeAsUTF8(serial_number);
			IntPtr intPtr = SDL.INTERNAL_SDL_hid_open(vendor_id, product_id, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000482 RID: 1154
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_open_path")]
		private unsafe static extern IntPtr INTERNAL_SDL_hid_open_path(byte* path);

		// Token: 0x06000483 RID: 1155 RVA: 0x00003388 File Offset: 0x00001588
		public unsafe static IntPtr SDL_hid_open_path(string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			IntPtr intPtr = SDL.INTERNAL_SDL_hid_open_path(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000484 RID: 1156
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_hid_get_properties(IntPtr dev);

		// Token: 0x06000485 RID: 1157
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_write(IntPtr dev, IntPtr data, UIntPtr length);

		// Token: 0x06000486 RID: 1158
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_read_timeout(IntPtr dev, IntPtr data, UIntPtr length, int milliseconds);

		// Token: 0x06000487 RID: 1159
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_read(IntPtr dev, IntPtr data, UIntPtr length);

		// Token: 0x06000488 RID: 1160
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_set_nonblocking(IntPtr dev, int nonblock);

		// Token: 0x06000489 RID: 1161
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_send_feature_report(IntPtr dev, IntPtr data, UIntPtr length);

		// Token: 0x0600048A RID: 1162
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_feature_report(IntPtr dev, IntPtr data, UIntPtr length);

		// Token: 0x0600048B RID: 1163
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_input_report(IntPtr dev, IntPtr data, UIntPtr length);

		// Token: 0x0600048C RID: 1164
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_close(IntPtr dev);

		// Token: 0x0600048D RID: 1165
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_get_manufacturer_string")]
		private unsafe static extern int INTERNAL_SDL_hid_get_manufacturer_string(IntPtr dev, byte* @string, UIntPtr maxlen);

		// Token: 0x0600048E RID: 1166 RVA: 0x000033B0 File Offset: 0x000015B0
		public unsafe static int SDL_hid_get_manufacturer_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			byte* ptr = SDL.EncodeAsUTF8(@string);
			int num = SDL.INTERNAL_SDL_hid_get_manufacturer_string(dev, ptr, maxlen);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x0600048F RID: 1167
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_get_product_string")]
		private unsafe static extern int INTERNAL_SDL_hid_get_product_string(IntPtr dev, byte* @string, UIntPtr maxlen);

		// Token: 0x06000490 RID: 1168 RVA: 0x000033D8 File Offset: 0x000015D8
		public unsafe static int SDL_hid_get_product_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			byte* ptr = SDL.EncodeAsUTF8(@string);
			int num = SDL.INTERNAL_SDL_hid_get_product_string(dev, ptr, maxlen);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000491 RID: 1169
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_get_serial_number_string")]
		private unsafe static extern int INTERNAL_SDL_hid_get_serial_number_string(IntPtr dev, byte* @string, UIntPtr maxlen);

		// Token: 0x06000492 RID: 1170 RVA: 0x00003400 File Offset: 0x00001600
		public unsafe static int SDL_hid_get_serial_number_string(IntPtr dev, string @string, UIntPtr maxlen)
		{
			byte* ptr = SDL.EncodeAsUTF8(@string);
			int num = SDL.INTERNAL_SDL_hid_get_serial_number_string(dev, ptr, maxlen);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000493 RID: 1171
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_hid_get_indexed_string")]
		private unsafe static extern int INTERNAL_SDL_hid_get_indexed_string(IntPtr dev, int string_index, byte* @string, UIntPtr maxlen);

		// Token: 0x06000494 RID: 1172 RVA: 0x00003428 File Offset: 0x00001628
		public unsafe static int SDL_hid_get_indexed_string(IntPtr dev, int string_index, string @string, UIntPtr maxlen)
		{
			byte* ptr = SDL.EncodeAsUTF8(@string);
			int num = SDL.INTERNAL_SDL_hid_get_indexed_string(dev, string_index, ptr, maxlen);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return num;
		}

		// Token: 0x06000495 RID: 1173
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_hid_get_device_info(IntPtr dev);

		// Token: 0x06000496 RID: 1174
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_hid_get_report_descriptor(IntPtr dev, IntPtr buf, UIntPtr buf_size);

		// Token: 0x06000497 RID: 1175
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_hid_ble_scan(SDL.SDLBool active);

		// Token: 0x06000498 RID: 1176
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHintWithPriority")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetHintWithPriority(byte* name, byte* value, SDL.SDL_HintPriority priority);

		// Token: 0x06000499 RID: 1177 RVA: 0x00003450 File Offset: 0x00001650
		public unsafe static SDL.SDLBool SDL_SetHintWithPriority(string name, string value, SDL.SDL_HintPriority priority)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			byte* ptr2 = SDL.EncodeAsUTF8(value);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetHintWithPriority(ptr, ptr2, priority);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x0600049A RID: 1178
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHint")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetHint(byte* name, byte* value);

		// Token: 0x0600049B RID: 1179 RVA: 0x0000348C File Offset: 0x0000168C
		public unsafe static SDL.SDLBool SDL_SetHint(string name, string value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			byte* ptr2 = SDL.EncodeAsUTF8(value);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetHint(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x0600049C RID: 1180
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ResetHint")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_ResetHint(byte* name);

		// Token: 0x0600049D RID: 1181 RVA: 0x000034C4 File Offset: 0x000016C4
		public unsafe static SDL.SDLBool SDL_ResetHint(string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_ResetHint(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600049E RID: 1182
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetHints();

		// Token: 0x0600049F RID: 1183
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHint")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetHint(byte* name);

		// Token: 0x060004A0 RID: 1184 RVA: 0x000034EC File Offset: 0x000016EC
		public unsafe static string SDL_GetHint(string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			string text = SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetHint(ptr), false);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return text;
		}

		// Token: 0x060004A1 RID: 1185
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetHintBoolean")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GetHintBoolean(byte* name, SDL.SDLBool default_value);

		// Token: 0x060004A2 RID: 1186 RVA: 0x00003518 File Offset: 0x00001718
		public unsafe static SDL.SDLBool SDL_GetHintBoolean(string name, SDL.SDLBool default_value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GetHintBoolean(ptr, default_value);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060004A3 RID: 1187
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_AddHintCallback")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_AddHintCallback(byte* name, SDL.SDL_HintCallback callback, IntPtr userdata);

		// Token: 0x060004A4 RID: 1188 RVA: 0x00003540 File Offset: 0x00001740
		public unsafe static SDL.SDLBool SDL_AddHintCallback(string name, SDL.SDL_HintCallback callback, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_AddHintCallback(ptr, callback, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060004A5 RID: 1189
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RemoveHintCallback")]
		private unsafe static extern void INTERNAL_SDL_RemoveHintCallback(byte* name, SDL.SDL_HintCallback callback, IntPtr userdata);

		// Token: 0x060004A6 RID: 1190 RVA: 0x00003567 File Offset: 0x00001767
		public unsafe static void SDL_RemoveHintCallback(string name, SDL.SDL_HintCallback callback, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			SDL.INTERNAL_SDL_RemoveHintCallback(ptr, callback, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004A7 RID: 1191
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_Init(SDL.SDL_InitFlags flags);

		// Token: 0x060004A8 RID: 1192
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_InitSubSystem(SDL.SDL_InitFlags flags);

		// Token: 0x060004A9 RID: 1193
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_QuitSubSystem(SDL.SDL_InitFlags flags);

		// Token: 0x060004AA RID: 1194
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_InitFlags SDL_WasInit(SDL.SDL_InitFlags flags);

		// Token: 0x060004AB RID: 1195
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Quit();

		// Token: 0x060004AC RID: 1196
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsMainThread();

		// Token: 0x060004AD RID: 1197
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RunOnMainThread(SDL.SDL_MainThreadCallback callback, IntPtr userdata, SDL.SDLBool wait_complete);

		// Token: 0x060004AE RID: 1198
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetAppMetadata")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetAppMetadata(byte* appname, byte* appversion, byte* appidentifier);

		// Token: 0x060004AF RID: 1199 RVA: 0x00003584 File Offset: 0x00001784
		public unsafe static SDL.SDLBool SDL_SetAppMetadata(string appname, string appversion, string appidentifier)
		{
			byte* ptr = SDL.EncodeAsUTF8(appname);
			byte* ptr2 = SDL.EncodeAsUTF8(appversion);
			byte* ptr3 = SDL.EncodeAsUTF8(appidentifier);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetAppMetadata(ptr, ptr2, ptr3);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			SDL.SDL_free((IntPtr)((void*)ptr3));
			return sdlbool;
		}

		// Token: 0x060004B0 RID: 1200
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetAppMetadataProperty")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetAppMetadataProperty(byte* name, byte* value);

		// Token: 0x060004B1 RID: 1201 RVA: 0x000035D0 File Offset: 0x000017D0
		public unsafe static SDL.SDLBool SDL_SetAppMetadataProperty(string name, string value)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			byte* ptr2 = SDL.EncodeAsUTF8(value);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetAppMetadataProperty(ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x060004B2 RID: 1202
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetAppMetadataProperty")]
		private unsafe static extern IntPtr INTERNAL_SDL_GetAppMetadataProperty(byte* name);

		// Token: 0x060004B3 RID: 1203 RVA: 0x00003608 File Offset: 0x00001808
		public unsafe static string SDL_GetAppMetadataProperty(string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			string text = SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetAppMetadataProperty(ptr), false);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return text;
		}

		// Token: 0x060004B4 RID: 1204
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadObject")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadObject(byte* sofile);

		// Token: 0x060004B5 RID: 1205 RVA: 0x00003634 File Offset: 0x00001834
		public unsafe static IntPtr SDL_LoadObject(string sofile)
		{
			byte* ptr = SDL.EncodeAsUTF8(sofile);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadObject(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060004B6 RID: 1206
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LoadFunction")]
		private unsafe static extern IntPtr INTERNAL_SDL_LoadFunction(IntPtr handle, byte* name);

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000365C File Offset: 0x0000185C
		public unsafe static IntPtr SDL_LoadFunction(IntPtr handle, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			IntPtr intPtr = SDL.INTERNAL_SDL_LoadFunction(handle, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060004B8 RID: 1208
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnloadObject(IntPtr handle);

		// Token: 0x060004B9 RID: 1209
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetPreferredLocales(out int count);

		// Token: 0x060004BA RID: 1210
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetLogPriorities(SDL.SDL_LogPriority priority);

		// Token: 0x060004BB RID: 1211
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetLogPriority(int category, SDL.SDL_LogPriority priority);

		// Token: 0x060004BC RID: 1212
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_LogPriority SDL_GetLogPriority(int category);

		// Token: 0x060004BD RID: 1213
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ResetLogPriorities();

		// Token: 0x060004BE RID: 1214
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetLogPriorityPrefix")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_SetLogPriorityPrefix(SDL.SDL_LogPriority priority, byte* prefix);

		// Token: 0x060004BF RID: 1215 RVA: 0x00003684 File Offset: 0x00001884
		public unsafe static SDL.SDLBool SDL_SetLogPriorityPrefix(SDL.SDL_LogPriority priority, string prefix)
		{
			byte* ptr = SDL.EncodeAsUTF8(prefix);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_SetLogPriorityPrefix(priority, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060004C0 RID: 1216
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Log")]
		private unsafe static extern void INTERNAL_SDL_Log(byte* fmt);

		// Token: 0x060004C1 RID: 1217 RVA: 0x000036AA File Offset: 0x000018AA
		public unsafe static void SDL_Log(string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_Log(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004C2 RID: 1218
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogTrace")]
		private unsafe static extern void INTERNAL_SDL_LogTrace(int category, byte* fmt);

		// Token: 0x060004C3 RID: 1219 RVA: 0x000036C4 File Offset: 0x000018C4
		public unsafe static void SDL_LogTrace(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogTrace(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004C4 RID: 1220
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogVerbose")]
		private unsafe static extern void INTERNAL_SDL_LogVerbose(int category, byte* fmt);

		// Token: 0x060004C5 RID: 1221 RVA: 0x000036EC File Offset: 0x000018EC
		public unsafe static void SDL_LogVerbose(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogVerbose(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004C6 RID: 1222
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogDebug")]
		private unsafe static extern void INTERNAL_SDL_LogDebug(int category, byte* fmt);

		// Token: 0x060004C7 RID: 1223 RVA: 0x00003714 File Offset: 0x00001914
		public unsafe static void SDL_LogDebug(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogDebug(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004C8 RID: 1224
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogInfo")]
		private unsafe static extern void INTERNAL_SDL_LogInfo(int category, byte* fmt);

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000373C File Offset: 0x0000193C
		public unsafe static void SDL_LogInfo(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogInfo(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004CA RID: 1226
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogWarn")]
		private unsafe static extern void INTERNAL_SDL_LogWarn(int category, byte* fmt);

		// Token: 0x060004CB RID: 1227 RVA: 0x00003764 File Offset: 0x00001964
		public unsafe static void SDL_LogWarn(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogWarn(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004CC RID: 1228
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogError")]
		private unsafe static extern void INTERNAL_SDL_LogError(int category, byte* fmt);

		// Token: 0x060004CD RID: 1229 RVA: 0x0000378C File Offset: 0x0000198C
		public unsafe static void SDL_LogError(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogError(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004CE RID: 1230
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogCritical")]
		private unsafe static extern void INTERNAL_SDL_LogCritical(int category, byte* fmt);

		// Token: 0x060004CF RID: 1231 RVA: 0x000037B4 File Offset: 0x000019B4
		public unsafe static void SDL_LogCritical(int category, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogCritical(category, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004D0 RID: 1232
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_LogMessage")]
		private unsafe static extern void INTERNAL_SDL_LogMessage(int category, SDL.SDL_LogPriority priority, byte* fmt);

		// Token: 0x060004D1 RID: 1233 RVA: 0x000037DC File Offset: 0x000019DC
		public unsafe static void SDL_LogMessage(int category, SDL.SDL_LogPriority priority, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.INTERNAL_SDL_LogMessage(category, priority, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x060004D2 RID: 1234
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetDefaultLogOutputFunction();

		// Token: 0x060004D3 RID: 1235
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GetLogOutputFunction(out SDL.SDL_LogOutputFunction callback, out IntPtr userdata);

		// Token: 0x060004D4 RID: 1236
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetLogOutputFunction(SDL.SDL_LogOutputFunction callback, IntPtr userdata);

		// Token: 0x060004D5 RID: 1237
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ShowMessageBox(ref SDL.SDL_MessageBoxData messageboxdata, out int buttonid);

		// Token: 0x060004D6 RID: 1238
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ShowSimpleMessageBox")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags flags, byte* title, byte* message, IntPtr window);

		// Token: 0x060004D7 RID: 1239 RVA: 0x00003804 File Offset: 0x00001A04
		public unsafe static SDL.SDLBool SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags flags, string title, string message, IntPtr window)
		{
			byte* ptr = SDL.EncodeAsUTF8(title);
			byte* ptr2 = SDL.EncodeAsUTF8(message);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_ShowSimpleMessageBox(flags, ptr, ptr2, window);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x060004D8 RID: 1240
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_CreateView(IntPtr window);

		// Token: 0x060004D9 RID: 1241
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Metal_DestroyView(IntPtr view);

		// Token: 0x060004DA RID: 1242
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_Metal_GetLayer(IntPtr view);

		// Token: 0x060004DB RID: 1243
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenURL")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_OpenURL(byte* url);

		// Token: 0x060004DC RID: 1244 RVA: 0x00003840 File Offset: 0x00001A40
		public unsafe static SDL.SDLBool SDL_OpenURL(string url)
		{
			byte* ptr = SDL.EncodeAsUTF8(url);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_OpenURL(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060004DD RID: 1245
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetPlatform")]
		private static extern IntPtr INTERNAL_SDL_GetPlatform();

		// Token: 0x060004DE RID: 1246 RVA: 0x00003865 File Offset: 0x00001A65
		public static string SDL_GetPlatform()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetPlatform(), false);
		}

		// Token: 0x060004DF RID: 1247
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateProcess(IntPtr args, SDL.SDLBool pipe_stdio);

		// Token: 0x060004E0 RID: 1248
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateProcessWithProperties(uint props);

		// Token: 0x060004E1 RID: 1249
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetProcessProperties(IntPtr process);

		// Token: 0x060004E2 RID: 1250
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_ReadProcess(IntPtr process, out UIntPtr datasize, out int exitcode);

		// Token: 0x060004E3 RID: 1251
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetProcessInput(IntPtr process);

		// Token: 0x060004E4 RID: 1252
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetProcessOutput(IntPtr process);

		// Token: 0x060004E5 RID: 1253
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_KillProcess(IntPtr process, SDL.SDLBool force);

		// Token: 0x060004E6 RID: 1254
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_WaitProcess(IntPtr process, SDL.SDLBool block, out int exitcode);

		// Token: 0x060004E7 RID: 1255
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyProcess(IntPtr process);

		// Token: 0x060004E8 RID: 1256
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetNumRenderDrivers();

		// Token: 0x060004E9 RID: 1257
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetRenderDriver")]
		private static extern IntPtr INTERNAL_SDL_GetRenderDriver(int index);

		// Token: 0x060004EA RID: 1258 RVA: 0x00003872 File Offset: 0x00001A72
		public static string SDL_GetRenderDriver(int index)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetRenderDriver(index), false);
		}

		// Token: 0x060004EB RID: 1259
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateWindowAndRenderer")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_CreateWindowAndRenderer(byte* title, int width, int height, SDL.SDL_WindowFlags window_flags, out IntPtr window, out IntPtr renderer);

		// Token: 0x060004EC RID: 1260 RVA: 0x00003880 File Offset: 0x00001A80
		public unsafe static SDL.SDLBool SDL_CreateWindowAndRenderer(string title, int width, int height, SDL.SDL_WindowFlags window_flags, out IntPtr window, out IntPtr renderer)
		{
			byte* ptr = SDL.EncodeAsUTF8(title);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_CreateWindowAndRenderer(ptr, width, height, window_flags, out window, out renderer);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x060004ED RID: 1261
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateRenderer")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateRenderer(IntPtr window, byte* name);

		// Token: 0x060004EE RID: 1262 RVA: 0x000038AC File Offset: 0x00001AAC
		public unsafe static IntPtr SDL_CreateRenderer(IntPtr window, string name)
		{
			byte* ptr = SDL.EncodeAsUTF8(name);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateRenderer(window, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x060004EF RID: 1263
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateRendererWithProperties(uint props);

		// Token: 0x060004F0 RID: 1264
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPURenderer(IntPtr device, IntPtr window);

		// Token: 0x060004F1 RID: 1265
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetGPURendererDevice(IntPtr renderer);

		// Token: 0x060004F2 RID: 1266
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateSoftwareRenderer(IntPtr surface);

		// Token: 0x060004F3 RID: 1267
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderer(IntPtr window);

		// Token: 0x060004F4 RID: 1268
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderWindow(IntPtr renderer);

		// Token: 0x060004F5 RID: 1269
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetRendererName")]
		private static extern IntPtr INTERNAL_SDL_GetRendererName(IntPtr renderer);

		// Token: 0x060004F6 RID: 1270 RVA: 0x000038D2 File Offset: 0x00001AD2
		public static string SDL_GetRendererName(IntPtr renderer)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetRendererName(renderer), false);
		}

		// Token: 0x060004F7 RID: 1271
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetRendererProperties(IntPtr renderer);

		// Token: 0x060004F8 RID: 1272
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderOutputSize(IntPtr renderer, out int w, out int h);

		// Token: 0x060004F9 RID: 1273
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetCurrentRenderOutputSize(IntPtr renderer, out int w, out int h);

		// Token: 0x060004FA RID: 1274
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTexture(IntPtr renderer, SDL.SDL_PixelFormat format, SDL.SDL_TextureAccess access, int w, int h);

		// Token: 0x060004FB RID: 1275
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

		// Token: 0x060004FC RID: 1276
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTextureWithProperties(IntPtr renderer, uint props);

		// Token: 0x060004FD RID: 1277
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_GetTextureProperties(IntPtr texture);

		// Token: 0x060004FE RID: 1278
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRendererFromTexture(IntPtr texture);

		// Token: 0x060004FF RID: 1279
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureSize(IntPtr texture, out float w, out float h);

		// Token: 0x06000500 RID: 1280
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTexturePalette(IntPtr texture, ref SDL.SDL_Palette palette);

		// Token: 0x06000501 RID: 1281
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTexturePalette(IntPtr texture);

		// Token: 0x06000502 RID: 1282
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

		// Token: 0x06000503 RID: 1283
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureColorModFloat(IntPtr texture, float r, float g, float b);

		// Token: 0x06000504 RID: 1284
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

		// Token: 0x06000505 RID: 1285
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureColorModFloat(IntPtr texture, out float r, out float g, out float b);

		// Token: 0x06000506 RID: 1286
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureAlphaMod(IntPtr texture, byte alpha);

		// Token: 0x06000507 RID: 1287
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureAlphaModFloat(IntPtr texture, float alpha);

		// Token: 0x06000508 RID: 1288
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureAlphaMod(IntPtr texture, out byte alpha);

		// Token: 0x06000509 RID: 1289
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureAlphaModFloat(IntPtr texture, out float alpha);

		// Token: 0x0600050A RID: 1290
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureBlendMode(IntPtr texture, uint blendMode);

		// Token: 0x0600050B RID: 1291
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureBlendMode(IntPtr texture, IntPtr blendMode);

		// Token: 0x0600050C RID: 1292
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetTextureScaleMode(IntPtr texture, SDL.SDL_ScaleMode scaleMode);

		// Token: 0x0600050D RID: 1293
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTextureScaleMode(IntPtr texture, out SDL.SDL_ScaleMode scaleMode);

		// Token: 0x0600050E RID: 1294
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr pixels, int pitch);

		// Token: 0x0600050F RID: 1295
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateYUVTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr Yplane, int Ypitch, IntPtr Uplane, int Upitch, IntPtr Vplane, int Vpitch);

		// Token: 0x06000510 RID: 1296
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_UpdateNVTexture(IntPtr texture, ref SDL.SDL_Rect rect, IntPtr Yplane, int Ypitch, IntPtr UVplane, int UVpitch);

		// Token: 0x06000511 RID: 1297
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LockTexture(IntPtr texture, ref SDL.SDL_Rect rect, out IntPtr pixels, out int pitch);

		// Token: 0x06000512 RID: 1298
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_LockTextureToSurface(IntPtr texture, ref SDL.SDL_Rect rect, out IntPtr surface);

		// Token: 0x06000513 RID: 1299
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UnlockTexture(IntPtr texture);

		// Token: 0x06000514 RID: 1300
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

		// Token: 0x06000515 RID: 1301
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

		// Token: 0x06000516 RID: 1302
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderLogicalPresentation(IntPtr renderer, int w, int h, SDL.SDL_RendererLogicalPresentation mode);

		// Token: 0x06000517 RID: 1303
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderLogicalPresentation(IntPtr renderer, out int w, out int h, out SDL.SDL_RendererLogicalPresentation mode);

		// Token: 0x06000518 RID: 1304
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderLogicalPresentationRect(IntPtr renderer, out SDL.SDL_FRect rect);

		// Token: 0x06000519 RID: 1305
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderCoordinatesFromWindow(IntPtr renderer, float window_x, float window_y, out float x, out float y);

		// Token: 0x0600051A RID: 1306
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderCoordinatesToWindow(IntPtr renderer, float x, float y, out float window_x, out float window_y);

		// Token: 0x0600051B RID: 1307
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_ConvertEventToRenderCoordinates(IntPtr renderer, ref SDL.SDL_Event @event);

		// Token: 0x0600051C RID: 1308
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderViewport(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x0600051D RID: 1309
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderViewport(IntPtr renderer, out SDL.SDL_Rect rect);

		// Token: 0x0600051E RID: 1310
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderViewportSet(IntPtr renderer);

		// Token: 0x0600051F RID: 1311
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderSafeArea(IntPtr renderer, out SDL.SDL_Rect rect);

		// Token: 0x06000520 RID: 1312
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderClipRect(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x06000521 RID: 1313
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderClipRect(IntPtr renderer, out SDL.SDL_Rect rect);

		// Token: 0x06000522 RID: 1314
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderClipEnabled(IntPtr renderer);

		// Token: 0x06000523 RID: 1315
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderScale(IntPtr renderer, float scaleX, float scaleY);

		// Token: 0x06000524 RID: 1316
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderScale(IntPtr renderer, out float scaleX, out float scaleY);

		// Token: 0x06000525 RID: 1317
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

		// Token: 0x06000526 RID: 1318
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderDrawColorFloat(IntPtr renderer, float r, float g, float b, float a);

		// Token: 0x06000527 RID: 1319
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

		// Token: 0x06000528 RID: 1320
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderDrawColorFloat(IntPtr renderer, out float r, out float g, out float b, out float a);

		// Token: 0x06000529 RID: 1321
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderColorScale(IntPtr renderer, float scale);

		// Token: 0x0600052A RID: 1322
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderColorScale(IntPtr renderer, out float scale);

		// Token: 0x0600052B RID: 1323
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderDrawBlendMode(IntPtr renderer, uint blendMode);

		// Token: 0x0600052C RID: 1324
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderDrawBlendMode(IntPtr renderer, IntPtr blendMode);

		// Token: 0x0600052D RID: 1325
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderClear(IntPtr renderer);

		// Token: 0x0600052E RID: 1326
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderPoint(IntPtr renderer, float x, float y);

		// Token: 0x0600052F RID: 1327
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderPoints(IntPtr renderer, SDL.SDL_FPoint[] points, int count);

		// Token: 0x06000530 RID: 1328
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderLine(IntPtr renderer, float x1, float y1, float x2, float y2);

		// Token: 0x06000531 RID: 1329
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderLines(IntPtr renderer, SDL.SDL_FPoint[] points, int count);

		// Token: 0x06000532 RID: 1330
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderRect(IntPtr renderer, ref SDL.SDL_FRect rect);

		// Token: 0x06000533 RID: 1331
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderRects(IntPtr renderer, SDL.SDL_FRect[] rects, int count);

		// Token: 0x06000534 RID: 1332
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderFillRect(IntPtr renderer, ref SDL.SDL_FRect rect);

		// Token: 0x06000535 RID: 1333
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderFillRects(IntPtr renderer, SDL.SDL_FRect[] rects, int count);

		// Token: 0x06000536 RID: 1334
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTexture(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, ref SDL.SDL_FRect dstrect);

		// Token: 0x06000537 RID: 1335
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTextureRotated(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, ref SDL.SDL_FRect dstrect, double angle, ref SDL.SDL_FPoint center, SDL.SDL_FlipMode flip);

		// Token: 0x06000538 RID: 1336
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTextureAffine(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, ref SDL.SDL_FPoint origin, ref SDL.SDL_FPoint right, ref SDL.SDL_FPoint down);

		// Token: 0x06000539 RID: 1337
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTextureTiled(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, float scale, ref SDL.SDL_FRect dstrect);

		// Token: 0x0600053A RID: 1338
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTexture9Grid(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, ref SDL.SDL_FRect dstrect);

		// Token: 0x0600053B RID: 1339
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderTexture9GridTiled(IntPtr renderer, IntPtr texture, ref SDL.SDL_FRect srcrect, float left_width, float right_width, float top_height, float bottom_height, float scale, ref SDL.SDL_FRect dstrect, float tileScale);

		// Token: 0x0600053C RID: 1340
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderGeometry(IntPtr renderer, IntPtr texture, SDL.SDL_Vertex[] vertices, int num_vertices, int[] indices, int num_indices);

		// Token: 0x0600053D RID: 1341
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderGeometryRaw(IntPtr renderer, IntPtr texture, IntPtr xy, int xy_stride, IntPtr color, int color_stride, IntPtr uv, int uv_stride, int num_vertices, IntPtr indices, int num_indices, int size_indices);

		// Token: 0x0600053E RID: 1342
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderTextureAddressMode(IntPtr renderer, SDL.SDL_TextureAddressMode u_mode, SDL.SDL_TextureAddressMode v_mode);

		// Token: 0x0600053F RID: 1343
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderTextureAddressMode(IntPtr renderer, out SDL.SDL_TextureAddressMode u_mode, out SDL.SDL_TextureAddressMode v_mode);

		// Token: 0x06000540 RID: 1344
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_RenderReadPixels(IntPtr renderer, ref SDL.SDL_Rect rect);

		// Token: 0x06000541 RID: 1345
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RenderPresent(IntPtr renderer);

		// Token: 0x06000542 RID: 1346
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyTexture(IntPtr texture);

		// Token: 0x06000543 RID: 1347
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyRenderer(IntPtr renderer);

		// Token: 0x06000544 RID: 1348
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_FlushRenderer(IntPtr renderer);

		// Token: 0x06000545 RID: 1349
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderMetalLayer(IntPtr renderer);

		// Token: 0x06000546 RID: 1350
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetRenderMetalCommandEncoder(IntPtr renderer);

		// Token: 0x06000547 RID: 1351
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_AddVulkanRenderSemaphores(IntPtr renderer, uint wait_stage_mask, long wait_semaphore, long signal_semaphore);

		// Token: 0x06000548 RID: 1352
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetRenderVSync(IntPtr renderer, int vsync);

		// Token: 0x06000549 RID: 1353
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetRenderVSync(IntPtr renderer, out int vsync);

		// Token: 0x0600054A RID: 1354
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RenderDebugText")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RenderDebugText(IntPtr renderer, float x, float y, byte* str);

		// Token: 0x0600054B RID: 1355 RVA: 0x000038E0 File Offset: 0x00001AE0
		public unsafe static SDL.SDLBool SDL_RenderDebugText(IntPtr renderer, float x, float y, string str)
		{
			byte* ptr = SDL.EncodeAsUTF8(str);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RenderDebugText(renderer, x, y, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600054C RID: 1356
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RenderDebugTextFormat")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RenderDebugTextFormat(IntPtr renderer, float x, float y, byte* fmt);

		// Token: 0x0600054D RID: 1357 RVA: 0x00003908 File Offset: 0x00001B08
		public unsafe static SDL.SDLBool SDL_RenderDebugTextFormat(IntPtr renderer, float x, float y, string fmt)
		{
			byte* ptr = SDL.EncodeAsUTF8(fmt);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RenderDebugTextFormat(renderer, x, y, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600054E RID: 1358
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetDefaultTextureScaleMode(IntPtr renderer, SDL.SDL_ScaleMode scale_mode);

		// Token: 0x0600054F RID: 1359
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetDefaultTextureScaleMode(IntPtr renderer, out SDL.SDL_ScaleMode scale_mode);

		// Token: 0x06000550 RID: 1360
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateGPURenderState(IntPtr renderer, ref SDL.SDL_GPURenderStateCreateInfo createinfo);

		// Token: 0x06000551 RID: 1361
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGPURenderStateFragmentUniforms(IntPtr state, uint slot_index, IntPtr data, uint length);

		// Token: 0x06000552 RID: 1362
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_SetGPURenderState(IntPtr renderer, IntPtr state);

		// Token: 0x06000553 RID: 1363
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyGPURenderState(IntPtr state);

		// Token: 0x06000554 RID: 1364
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenTitleStorage")]
		private unsafe static extern IntPtr INTERNAL_SDL_OpenTitleStorage(byte* @override, uint props);

		// Token: 0x06000555 RID: 1365 RVA: 0x00003930 File Offset: 0x00001B30
		public unsafe static IntPtr SDL_OpenTitleStorage(string @override, uint props)
		{
			byte* ptr = SDL.EncodeAsUTF8(@override);
			IntPtr intPtr = SDL.INTERNAL_SDL_OpenTitleStorage(ptr, props);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000556 RID: 1366
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenUserStorage")]
		private unsafe static extern IntPtr INTERNAL_SDL_OpenUserStorage(byte* org, byte* app, uint props);

		// Token: 0x06000557 RID: 1367 RVA: 0x00003958 File Offset: 0x00001B58
		public unsafe static IntPtr SDL_OpenUserStorage(string org, string app, uint props)
		{
			byte* ptr = SDL.EncodeAsUTF8(org);
			byte* ptr2 = SDL.EncodeAsUTF8(app);
			IntPtr intPtr = SDL.INTERNAL_SDL_OpenUserStorage(ptr, ptr2, props);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return intPtr;
		}

		// Token: 0x06000558 RID: 1368
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_OpenFileStorage")]
		private unsafe static extern IntPtr INTERNAL_SDL_OpenFileStorage(byte* path);

		// Token: 0x06000559 RID: 1369 RVA: 0x00003994 File Offset: 0x00001B94
		public unsafe static IntPtr SDL_OpenFileStorage(string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			IntPtr intPtr = SDL.INTERNAL_SDL_OpenFileStorage(ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x0600055A RID: 1370
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_OpenStorage(ref SDL.SDL_StorageInterface iface, IntPtr userdata);

		// Token: 0x0600055B RID: 1371
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_CloseStorage(IntPtr storage);

		// Token: 0x0600055C RID: 1372
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_StorageReady(IntPtr storage);

		// Token: 0x0600055D RID: 1373
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetStorageFileSize")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GetStorageFileSize(IntPtr storage, byte* path, out ulong length);

		// Token: 0x0600055E RID: 1374 RVA: 0x000039BC File Offset: 0x00001BBC
		public unsafe static SDL.SDLBool SDL_GetStorageFileSize(IntPtr storage, string path, out ulong length)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GetStorageFileSize(storage, ptr, out length);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600055F RID: 1375
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_ReadStorageFile")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_ReadStorageFile(IntPtr storage, byte* path, IntPtr destination, ulong length);

		// Token: 0x06000560 RID: 1376 RVA: 0x000039E4 File Offset: 0x00001BE4
		public unsafe static SDL.SDLBool SDL_ReadStorageFile(IntPtr storage, string path, IntPtr destination, ulong length)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_ReadStorageFile(storage, ptr, destination, length);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000561 RID: 1377
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_WriteStorageFile")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_WriteStorageFile(IntPtr storage, byte* path, IntPtr source, ulong length);

		// Token: 0x06000562 RID: 1378 RVA: 0x00003A0C File Offset: 0x00001C0C
		public unsafe static SDL.SDLBool SDL_WriteStorageFile(IntPtr storage, string path, IntPtr source, ulong length)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_WriteStorageFile(storage, ptr, source, length);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000563 RID: 1379
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateStorageDirectory")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_CreateStorageDirectory(IntPtr storage, byte* path);

		// Token: 0x06000564 RID: 1380 RVA: 0x00003A34 File Offset: 0x00001C34
		public unsafe static SDL.SDLBool SDL_CreateStorageDirectory(IntPtr storage, string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_CreateStorageDirectory(storage, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000565 RID: 1381
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_EnumerateStorageDirectory")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_EnumerateStorageDirectory(IntPtr storage, byte* path, SDL.SDL_EnumerateDirectoryCallback callback, IntPtr userdata);

		// Token: 0x06000566 RID: 1382 RVA: 0x00003A5C File Offset: 0x00001C5C
		public unsafe static SDL.SDLBool SDL_EnumerateStorageDirectory(IntPtr storage, string path, SDL.SDL_EnumerateDirectoryCallback callback, IntPtr userdata)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_EnumerateStorageDirectory(storage, ptr, callback, userdata);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000567 RID: 1383
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RemoveStoragePath")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RemoveStoragePath(IntPtr storage, byte* path);

		// Token: 0x06000568 RID: 1384 RVA: 0x00003A84 File Offset: 0x00001C84
		public unsafe static SDL.SDLBool SDL_RemoveStoragePath(IntPtr storage, string path)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RemoveStoragePath(storage, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x06000569 RID: 1385
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_RenameStoragePath")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_RenameStoragePath(IntPtr storage, byte* oldpath, byte* newpath);

		// Token: 0x0600056A RID: 1386 RVA: 0x00003AAC File Offset: 0x00001CAC
		public unsafe static SDL.SDLBool SDL_RenameStoragePath(IntPtr storage, string oldpath, string newpath)
		{
			byte* ptr = SDL.EncodeAsUTF8(oldpath);
			byte* ptr2 = SDL.EncodeAsUTF8(newpath);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_RenameStoragePath(storage, ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x0600056B RID: 1387
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CopyStorageFile")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_CopyStorageFile(IntPtr storage, byte* oldpath, byte* newpath);

		// Token: 0x0600056C RID: 1388 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public unsafe static SDL.SDLBool SDL_CopyStorageFile(IntPtr storage, string oldpath, string newpath)
		{
			byte* ptr = SDL.EncodeAsUTF8(oldpath);
			byte* ptr2 = SDL.EncodeAsUTF8(newpath);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_CopyStorageFile(storage, ptr, ptr2);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return sdlbool;
		}

		// Token: 0x0600056D RID: 1389
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetStoragePathInfo")]
		private unsafe static extern SDL.SDLBool INTERNAL_SDL_GetStoragePathInfo(IntPtr storage, byte* path, out SDL.SDL_PathInfo info);

		// Token: 0x0600056E RID: 1390 RVA: 0x00003B24 File Offset: 0x00001D24
		public unsafe static SDL.SDLBool SDL_GetStoragePathInfo(IntPtr storage, string path, out SDL.SDL_PathInfo info)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			SDL.SDLBool sdlbool = SDL.INTERNAL_SDL_GetStoragePathInfo(storage, ptr, out info);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return sdlbool;
		}

		// Token: 0x0600056F RID: 1391
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetStorageSpaceRemaining(IntPtr storage);

		// Token: 0x06000570 RID: 1392
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GlobStorageDirectory")]
		private unsafe static extern IntPtr INTERNAL_SDL_GlobStorageDirectory(IntPtr storage, byte* path, byte* pattern, SDL.SDL_GlobFlags flags, out int count);

		// Token: 0x06000571 RID: 1393 RVA: 0x00003B4C File Offset: 0x00001D4C
		public unsafe static IntPtr SDL_GlobStorageDirectory(IntPtr storage, string path, string pattern, SDL.SDL_GlobFlags flags, out int count)
		{
			byte* ptr = SDL.EncodeAsUTF8(path);
			byte* ptr2 = SDL.EncodeAsUTF8(pattern);
			IntPtr intPtr = SDL.INTERNAL_SDL_GlobStorageDirectory(storage, ptr, ptr2, flags, out count);
			SDL.SDL_free((IntPtr)((void*)ptr));
			SDL.SDL_free((IntPtr)((void*)ptr2));
			return intPtr;
		}

		// Token: 0x06000572 RID: 1394
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsTablet();

		// Token: 0x06000573 RID: 1395
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_IsTV();

		// Token: 0x06000574 RID: 1396
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDL_Sandbox SDL_GetSandbox();

		// Token: 0x06000575 RID: 1397
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationWillTerminate();

		// Token: 0x06000576 RID: 1398
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationDidReceiveMemoryWarning();

		// Token: 0x06000577 RID: 1399
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationWillEnterBackground();

		// Token: 0x06000578 RID: 1400
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationDidEnterBackground();

		// Token: 0x06000579 RID: 1401
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationWillEnterForeground();

		// Token: 0x0600057A RID: 1402
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_OnApplicationDidEnterForeground();

		// Token: 0x0600057B RID: 1403
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetDateTimeLocalePreferences(out SDL.SDL_DateFormat dateFormat, out SDL.SDL_TimeFormat timeFormat);

		// Token: 0x0600057C RID: 1404
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetCurrentTime(IntPtr ticks);

		// Token: 0x0600057D RID: 1405
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_TimeToDateTime(long ticks, out SDL.SDL_DateTime dt, SDL.SDLBool localTime);

		// Token: 0x0600057E RID: 1406
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_DateTimeToTime(ref SDL.SDL_DateTime dt, IntPtr ticks);

		// Token: 0x0600057F RID: 1407
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_TimeToWindows(long ticks, out uint dwLowDateTime, out uint dwHighDateTime);

		// Token: 0x06000580 RID: 1408
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern long SDL_TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

		// Token: 0x06000581 RID: 1409
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDaysInMonth(int year, int month);

		// Token: 0x06000582 RID: 1410
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDayOfYear(int year, int month, int day);

		// Token: 0x06000583 RID: 1411
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetDayOfWeek(int year, int month, int day);

		// Token: 0x06000584 RID: 1412
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetTicks();

		// Token: 0x06000585 RID: 1413
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetTicksNS();

		// Token: 0x06000586 RID: 1414
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetPerformanceCounter();

		// Token: 0x06000587 RID: 1415
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong SDL_GetPerformanceFrequency();

		// Token: 0x06000588 RID: 1416
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_Delay(uint ms);

		// Token: 0x06000589 RID: 1417
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DelayNS(ulong ns);

		// Token: 0x0600058A RID: 1418
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DelayPrecise(ulong ns);

		// Token: 0x0600058B RID: 1419
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_AddTimer(uint interval, SDL.SDL_TimerCallback callback, IntPtr userdata);

		// Token: 0x0600058C RID: 1420
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern uint SDL_AddTimerNS(ulong interval, SDL.SDL_NSTimerCallback callback, IntPtr userdata);

		// Token: 0x0600058D RID: 1421
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_RemoveTimer(uint id);

		// Token: 0x0600058E RID: 1422
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_CreateTray")]
		private unsafe static extern IntPtr INTERNAL_SDL_CreateTray(IntPtr icon, byte* tooltip);

		// Token: 0x0600058F RID: 1423 RVA: 0x00003B88 File Offset: 0x00001D88
		public unsafe static IntPtr SDL_CreateTray(IntPtr icon, string tooltip)
		{
			byte* ptr = SDL.EncodeAsUTF8(tooltip);
			IntPtr intPtr = SDL.INTERNAL_SDL_CreateTray(icon, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x06000590 RID: 1424
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTrayIcon(IntPtr tray, IntPtr icon);

		// Token: 0x06000591 RID: 1425
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetTrayTooltip")]
		private unsafe static extern void INTERNAL_SDL_SetTrayTooltip(IntPtr tray, byte* tooltip);

		// Token: 0x06000592 RID: 1426 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public unsafe static void SDL_SetTrayTooltip(IntPtr tray, string tooltip)
		{
			byte* ptr = SDL.EncodeAsUTF8(tooltip);
			SDL.INTERNAL_SDL_SetTrayTooltip(tray, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x06000593 RID: 1427
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTrayMenu(IntPtr tray);

		// Token: 0x06000594 RID: 1428
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_CreateTraySubmenu(IntPtr entry);

		// Token: 0x06000595 RID: 1429
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTrayMenu(IntPtr tray);

		// Token: 0x06000596 RID: 1430
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTraySubmenu(IntPtr entry);

		// Token: 0x06000597 RID: 1431
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTrayEntries(IntPtr menu, out int count);

		// Token: 0x06000598 RID: 1432
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_RemoveTrayEntry(IntPtr entry);

		// Token: 0x06000599 RID: 1433
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_InsertTrayEntryAt")]
		private unsafe static extern IntPtr INTERNAL_SDL_InsertTrayEntryAt(IntPtr menu, int pos, byte* label, SDL.SDL_TrayEntryFlags flags);

		// Token: 0x0600059A RID: 1434 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public unsafe static IntPtr SDL_InsertTrayEntryAt(IntPtr menu, int pos, string label, SDL.SDL_TrayEntryFlags flags)
		{
			byte* ptr = SDL.EncodeAsUTF8(label);
			IntPtr intPtr = SDL.INTERNAL_SDL_InsertTrayEntryAt(menu, pos, ptr, flags);
			SDL.SDL_free((IntPtr)((void*)ptr));
			return intPtr;
		}

		// Token: 0x0600059B RID: 1435
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetTrayEntryLabel")]
		private unsafe static extern void INTERNAL_SDL_SetTrayEntryLabel(IntPtr entry, byte* label);

		// Token: 0x0600059C RID: 1436 RVA: 0x00003C00 File Offset: 0x00001E00
		public unsafe static void SDL_SetTrayEntryLabel(IntPtr entry, string label)
		{
			byte* ptr = SDL.EncodeAsUTF8(label);
			SDL.INTERNAL_SDL_SetTrayEntryLabel(entry, ptr);
			SDL.SDL_free((IntPtr)((void*)ptr));
		}

		// Token: 0x0600059D RID: 1437
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetTrayEntryLabel")]
		private static extern IntPtr INTERNAL_SDL_GetTrayEntryLabel(IntPtr entry);

		// Token: 0x0600059E RID: 1438 RVA: 0x00003C26 File Offset: 0x00001E26
		public static string SDL_GetTrayEntryLabel(IntPtr entry)
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetTrayEntryLabel(entry), false);
		}

		// Token: 0x0600059F RID: 1439
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTrayEntryChecked(IntPtr entry, SDL.SDLBool check);

		// Token: 0x060005A0 RID: 1440
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTrayEntryChecked(IntPtr entry);

		// Token: 0x060005A1 RID: 1441
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTrayEntryEnabled(IntPtr entry, SDL.SDLBool enabled);

		// Token: 0x060005A2 RID: 1442
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern SDL.SDLBool SDL_GetTrayEntryEnabled(IntPtr entry);

		// Token: 0x060005A3 RID: 1443
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetTrayEntryCallback(IntPtr entry, SDL.SDL_TrayCallback callback, IntPtr userdata);

		// Token: 0x060005A4 RID: 1444
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_ClickTrayEntry(IntPtr entry);

		// Token: 0x060005A5 RID: 1445
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_DestroyTray(IntPtr tray);

		// Token: 0x060005A6 RID: 1446
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTrayEntryParent(IntPtr entry);

		// Token: 0x060005A7 RID: 1447
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTrayMenuParentEntry(IntPtr menu);

		// Token: 0x060005A8 RID: 1448
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr SDL_GetTrayMenuParentTray(IntPtr menu);

		// Token: 0x060005A9 RID: 1449
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_UpdateTrays();

		// Token: 0x060005AA RID: 1450
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_GetVersion();

		// Token: 0x060005AB RID: 1451
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetRevision")]
		private static extern IntPtr INTERNAL_SDL_GetRevision();

		// Token: 0x060005AC RID: 1452 RVA: 0x00003C34 File Offset: 0x00001E34
		public static string SDL_GetRevision()
		{
			return SDL.DecodeFromUTF8(SDL.INTERNAL_SDL_GetRevision(), false);
		}

		// Token: 0x060005AD RID: 1453
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_SetMainReady();

		// Token: 0x060005AE RID: 1454
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_RunApp(int argc, IntPtr argv, SDL.SDL_main_func mainFunction, IntPtr reserved);

		// Token: 0x060005AF RID: 1455
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern int SDL_EnterAppMainCallbacks(int argc, IntPtr argv, SDL.SDL_AppInit_func appinit, SDL.SDL_AppIterate_func appiter, SDL.SDL_AppEvent_func appevent, SDL.SDL_AppQuit_func appquit);

		// Token: 0x060005B0 RID: 1456
		[DllImport("SDL3", CallingConvention = CallingConvention.Cdecl)]
		public static extern void SDL_GDKSuspendComplete();

		// Token: 0x040000C3 RID: 195
		private const string nativeLibName = "SDL3";

		// Token: 0x040000C4 RID: 196
		public const string SDL_PROP_NAME_STRING = "SDL.name";

		// Token: 0x040000C5 RID: 197
		public const string SDL_PROP_THREAD_CREATE_ENTRY_FUNCTION_POINTER = "SDL.thread.create.entry_function";

		// Token: 0x040000C6 RID: 198
		public const string SDL_PROP_THREAD_CREATE_NAME_STRING = "SDL.thread.create.name";

		// Token: 0x040000C7 RID: 199
		public const string SDL_PROP_THREAD_CREATE_USERDATA_POINTER = "SDL.thread.create.userdata";

		// Token: 0x040000C8 RID: 200
		public const string SDL_PROP_THREAD_CREATE_STACKSIZE_NUMBER = "SDL.thread.create.stacksize";

		// Token: 0x040000C9 RID: 201
		public const string SDL_PROP_IOSTREAM_WINDOWS_HANDLE_POINTER = "SDL.iostream.windows.handle";

		// Token: 0x040000CA RID: 202
		public const string SDL_PROP_IOSTREAM_STDIO_FILE_POINTER = "SDL.iostream.stdio.file";

		// Token: 0x040000CB RID: 203
		public const string SDL_PROP_IOSTREAM_FILE_DESCRIPTOR_NUMBER = "SDL.iostream.file_descriptor";

		// Token: 0x040000CC RID: 204
		public const string SDL_PROP_IOSTREAM_ANDROID_AASSET_POINTER = "SDL.iostream.android.aasset";

		// Token: 0x040000CD RID: 205
		public const string SDL_PROP_IOSTREAM_MEMORY_POINTER = "SDL.iostream.memory.base";

		// Token: 0x040000CE RID: 206
		public const string SDL_PROP_IOSTREAM_MEMORY_SIZE_NUMBER = "SDL.iostream.memory.size";

		// Token: 0x040000CF RID: 207
		public const string SDL_PROP_IOSTREAM_MEMORY_FREE_FUNC_POINTER = "SDL.iostream.memory.free";

		// Token: 0x040000D0 RID: 208
		public const string SDL_PROP_IOSTREAM_DYNAMIC_MEMORY_POINTER = "SDL.iostream.dynamic.memory";

		// Token: 0x040000D1 RID: 209
		public const string SDL_PROP_IOSTREAM_DYNAMIC_CHUNKSIZE_NUMBER = "SDL.iostream.dynamic.chunksize";

		// Token: 0x040000D2 RID: 210
		public const string SDL_PROP_AUDIOSTREAM_AUTO_CLEANUP_BOOLEAN = "SDL.audiostream.auto_cleanup";

		// Token: 0x040000D3 RID: 211
		public const string SDL_PROP_SURFACE_SDR_WHITE_POINT_FLOAT = "SDL.surface.SDR_white_point";

		// Token: 0x040000D4 RID: 212
		public const string SDL_PROP_SURFACE_HDR_HEADROOM_FLOAT = "SDL.surface.HDR_headroom";

		// Token: 0x040000D5 RID: 213
		public const string SDL_PROP_SURFACE_TONEMAP_OPERATOR_STRING = "SDL.surface.tonemap";

		// Token: 0x040000D6 RID: 214
		public const string SDL_PROP_SURFACE_HOTSPOT_X_NUMBER = "SDL.surface.hotspot.x";

		// Token: 0x040000D7 RID: 215
		public const string SDL_PROP_SURFACE_HOTSPOT_Y_NUMBER = "SDL.surface.hotspot.y";

		// Token: 0x040000D8 RID: 216
		public const string SDL_PROP_SURFACE_ROTATION_FLOAT = "SDL.surface.rotation";

		// Token: 0x040000D9 RID: 217
		public const string SDL_PROP_GLOBAL_VIDEO_WAYLAND_WL_DISPLAY_POINTER = "SDL.video.wayland.wl_display";

		// Token: 0x040000DA RID: 218
		public const string SDL_PROP_DISPLAY_HDR_ENABLED_BOOLEAN = "SDL.display.HDR_enabled";

		// Token: 0x040000DB RID: 219
		public const string SDL_PROP_DISPLAY_KMSDRM_PANEL_ORIENTATION_NUMBER = "SDL.display.KMSDRM.panel_orientation";

		// Token: 0x040000DC RID: 220
		public const string SDL_PROP_DISPLAY_WAYLAND_WL_OUTPUT_POINTER = "SDL.display.wayland.wl_output";

		// Token: 0x040000DD RID: 221
		public const string SDL_PROP_DISPLAY_WINDOWS_HMONITOR_POINTER = "SDL.display.windows.hmonitor";

		// Token: 0x040000DE RID: 222
		public const string SDL_PROP_WINDOW_CREATE_ALWAYS_ON_TOP_BOOLEAN = "SDL.window.create.always_on_top";

		// Token: 0x040000DF RID: 223
		public const string SDL_PROP_WINDOW_CREATE_BORDERLESS_BOOLEAN = "SDL.window.create.borderless";

		// Token: 0x040000E0 RID: 224
		public const string SDL_PROP_WINDOW_CREATE_CONSTRAIN_POPUP_BOOLEAN = "SDL.window.create.constrain_popup";

		// Token: 0x040000E1 RID: 225
		public const string SDL_PROP_WINDOW_CREATE_FOCUSABLE_BOOLEAN = "SDL.window.create.focusable";

		// Token: 0x040000E2 RID: 226
		public const string SDL_PROP_WINDOW_CREATE_EXTERNAL_GRAPHICS_CONTEXT_BOOLEAN = "SDL.window.create.external_graphics_context";

		// Token: 0x040000E3 RID: 227
		public const string SDL_PROP_WINDOW_CREATE_FLAGS_NUMBER = "SDL.window.create.flags";

		// Token: 0x040000E4 RID: 228
		public const string SDL_PROP_WINDOW_CREATE_FULLSCREEN_BOOLEAN = "SDL.window.create.fullscreen";

		// Token: 0x040000E5 RID: 229
		public const string SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER = "SDL.window.create.height";

		// Token: 0x040000E6 RID: 230
		public const string SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN = "SDL.window.create.hidden";

		// Token: 0x040000E7 RID: 231
		public const string SDL_PROP_WINDOW_CREATE_HIGH_PIXEL_DENSITY_BOOLEAN = "SDL.window.create.high_pixel_density";

		// Token: 0x040000E8 RID: 232
		public const string SDL_PROP_WINDOW_CREATE_MAXIMIZED_BOOLEAN = "SDL.window.create.maximized";

		// Token: 0x040000E9 RID: 233
		public const string SDL_PROP_WINDOW_CREATE_MENU_BOOLEAN = "SDL.window.create.menu";

		// Token: 0x040000EA RID: 234
		public const string SDL_PROP_WINDOW_CREATE_METAL_BOOLEAN = "SDL.window.create.metal";

		// Token: 0x040000EB RID: 235
		public const string SDL_PROP_WINDOW_CREATE_MINIMIZED_BOOLEAN = "SDL.window.create.minimized";

		// Token: 0x040000EC RID: 236
		public const string SDL_PROP_WINDOW_CREATE_MODAL_BOOLEAN = "SDL.window.create.modal";

		// Token: 0x040000ED RID: 237
		public const string SDL_PROP_WINDOW_CREATE_MOUSE_GRABBED_BOOLEAN = "SDL.window.create.mouse_grabbed";

		// Token: 0x040000EE RID: 238
		public const string SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN = "SDL.window.create.opengl";

		// Token: 0x040000EF RID: 239
		public const string SDL_PROP_WINDOW_CREATE_PARENT_POINTER = "SDL.window.create.parent";

		// Token: 0x040000F0 RID: 240
		public const string SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN = "SDL.window.create.resizable";

		// Token: 0x040000F1 RID: 241
		public const string SDL_PROP_WINDOW_CREATE_TITLE_STRING = "SDL.window.create.title";

		// Token: 0x040000F2 RID: 242
		public const string SDL_PROP_WINDOW_CREATE_TRANSPARENT_BOOLEAN = "SDL.window.create.transparent";

		// Token: 0x040000F3 RID: 243
		public const string SDL_PROP_WINDOW_CREATE_TOOLTIP_BOOLEAN = "SDL.window.create.tooltip";

		// Token: 0x040000F4 RID: 244
		public const string SDL_PROP_WINDOW_CREATE_UTILITY_BOOLEAN = "SDL.window.create.utility";

		// Token: 0x040000F5 RID: 245
		public const string SDL_PROP_WINDOW_CREATE_VULKAN_BOOLEAN = "SDL.window.create.vulkan";

		// Token: 0x040000F6 RID: 246
		public const string SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER = "SDL.window.create.width";

		// Token: 0x040000F7 RID: 247
		public const string SDL_PROP_WINDOW_CREATE_X_NUMBER = "SDL.window.create.x";

		// Token: 0x040000F8 RID: 248
		public const string SDL_PROP_WINDOW_CREATE_Y_NUMBER = "SDL.window.create.y";

		// Token: 0x040000F9 RID: 249
		public const string SDL_PROP_WINDOW_CREATE_COCOA_WINDOW_POINTER = "SDL.window.create.cocoa.window";

		// Token: 0x040000FA RID: 250
		public const string SDL_PROP_WINDOW_CREATE_COCOA_VIEW_POINTER = "SDL.window.create.cocoa.view";

		// Token: 0x040000FB RID: 251
		public const string SDL_PROP_WINDOW_CREATE_WINDOWSCENE_POINTER = "SDL.window.create.uikit.windowscene";

		// Token: 0x040000FC RID: 252
		public const string SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN = "SDL.window.create.wayland.surface_role_custom";

		// Token: 0x040000FD RID: 253
		public const string SDL_PROP_WINDOW_CREATE_WAYLAND_CREATE_EGL_WINDOW_BOOLEAN = "SDL.window.create.wayland.create_egl_window";

		// Token: 0x040000FE RID: 254
		public const string SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER = "SDL.window.create.wayland.wl_surface";

		// Token: 0x040000FF RID: 255
		public const string SDL_PROP_WINDOW_CREATE_WIN32_HWND_POINTER = "SDL.window.create.win32.hwnd";

		// Token: 0x04000100 RID: 256
		public const string SDL_PROP_WINDOW_CREATE_WIN32_PIXEL_FORMAT_HWND_POINTER = "SDL.window.create.win32.pixel_format_hwnd";

		// Token: 0x04000101 RID: 257
		public const string SDL_PROP_WINDOW_CREATE_X11_WINDOW_NUMBER = "SDL.window.create.x11.window";

		// Token: 0x04000102 RID: 258
		public const string SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_CANVAS_ID_STRING = "SDL.window.create.emscripten.canvas_id";

		// Token: 0x04000103 RID: 259
		public const string SDL_PROP_WINDOW_CREATE_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING = "SDL.window.create.emscripten.keyboard_element";

		// Token: 0x04000104 RID: 260
		public const string SDL_PROP_WINDOW_SHAPE_POINTER = "SDL.window.shape";

		// Token: 0x04000105 RID: 261
		public const string SDL_PROP_WINDOW_HDR_ENABLED_BOOLEAN = "SDL.window.HDR_enabled";

		// Token: 0x04000106 RID: 262
		public const string SDL_PROP_WINDOW_SDR_WHITE_LEVEL_FLOAT = "SDL.window.SDR_white_level";

		// Token: 0x04000107 RID: 263
		public const string SDL_PROP_WINDOW_HDR_HEADROOM_FLOAT = "SDL.window.HDR_headroom";

		// Token: 0x04000108 RID: 264
		public const string SDL_PROP_WINDOW_ANDROID_WINDOW_POINTER = "SDL.window.android.window";

		// Token: 0x04000109 RID: 265
		public const string SDL_PROP_WINDOW_ANDROID_SURFACE_POINTER = "SDL.window.android.surface";

		// Token: 0x0400010A RID: 266
		public const string SDL_PROP_WINDOW_UIKIT_WINDOW_POINTER = "SDL.window.uikit.window";

		// Token: 0x0400010B RID: 267
		public const string SDL_PROP_WINDOW_UIKIT_METAL_VIEW_TAG_NUMBER = "SDL.window.uikit.metal_view_tag";

		// Token: 0x0400010C RID: 268
		public const string SDL_PROP_WINDOW_UIKIT_OPENGL_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.framebuffer";

		// Token: 0x0400010D RID: 269
		public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RENDERBUFFER_NUMBER = "SDL.window.uikit.opengl.renderbuffer";

		// Token: 0x0400010E RID: 270
		public const string SDL_PROP_WINDOW_UIKIT_OPENGL_RESOLVE_FRAMEBUFFER_NUMBER = "SDL.window.uikit.opengl.resolve_framebuffer";

		// Token: 0x0400010F RID: 271
		public const string SDL_PROP_WINDOW_KMSDRM_DEVICE_INDEX_NUMBER = "SDL.window.kmsdrm.dev_index";

		// Token: 0x04000110 RID: 272
		public const string SDL_PROP_WINDOW_KMSDRM_DRM_FD_NUMBER = "SDL.window.kmsdrm.drm_fd";

		// Token: 0x04000111 RID: 273
		public const string SDL_PROP_WINDOW_KMSDRM_GBM_DEVICE_POINTER = "SDL.window.kmsdrm.gbm_dev";

		// Token: 0x04000112 RID: 274
		public const string SDL_PROP_WINDOW_COCOA_WINDOW_POINTER = "SDL.window.cocoa.window";

		// Token: 0x04000113 RID: 275
		public const string SDL_PROP_WINDOW_COCOA_METAL_VIEW_TAG_NUMBER = "SDL.window.cocoa.metal_view_tag";

		// Token: 0x04000114 RID: 276
		public const string SDL_PROP_WINDOW_OPENVR_OVERLAY_ID_NUMBER = "SDL.window.openvr.overlay_id";

		// Token: 0x04000115 RID: 277
		public const string SDL_PROP_WINDOW_VIVANTE_DISPLAY_POINTER = "SDL.window.vivante.display";

		// Token: 0x04000116 RID: 278
		public const string SDL_PROP_WINDOW_VIVANTE_WINDOW_POINTER = "SDL.window.vivante.window";

		// Token: 0x04000117 RID: 279
		public const string SDL_PROP_WINDOW_VIVANTE_SURFACE_POINTER = "SDL.window.vivante.surface";

		// Token: 0x04000118 RID: 280
		public const string SDL_PROP_WINDOW_WIN32_HWND_POINTER = "SDL.window.win32.hwnd";

		// Token: 0x04000119 RID: 281
		public const string SDL_PROP_WINDOW_WIN32_HDC_POINTER = "SDL.window.win32.hdc";

		// Token: 0x0400011A RID: 282
		public const string SDL_PROP_WINDOW_WIN32_INSTANCE_POINTER = "SDL.window.win32.instance";

		// Token: 0x0400011B RID: 283
		public const string SDL_PROP_WINDOW_WAYLAND_DISPLAY_POINTER = "SDL.window.wayland.display";

		// Token: 0x0400011C RID: 284
		public const string SDL_PROP_WINDOW_WAYLAND_SURFACE_POINTER = "SDL.window.wayland.surface";

		// Token: 0x0400011D RID: 285
		public const string SDL_PROP_WINDOW_WAYLAND_VIEWPORT_POINTER = "SDL.window.wayland.viewport";

		// Token: 0x0400011E RID: 286
		public const string SDL_PROP_WINDOW_WAYLAND_EGL_WINDOW_POINTER = "SDL.window.wayland.egl_window";

		// Token: 0x0400011F RID: 287
		public const string SDL_PROP_WINDOW_WAYLAND_XDG_SURFACE_POINTER = "SDL.window.wayland.xdg_surface";

		// Token: 0x04000120 RID: 288
		public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_POINTER = "SDL.window.wayland.xdg_toplevel";

		// Token: 0x04000121 RID: 289
		public const string SDL_PROP_WINDOW_WAYLAND_XDG_TOPLEVEL_EXPORT_HANDLE_STRING = "SDL.window.wayland.xdg_toplevel_export_handle";

		// Token: 0x04000122 RID: 290
		public const string SDL_PROP_WINDOW_WAYLAND_XDG_POPUP_POINTER = "SDL.window.wayland.xdg_popup";

		// Token: 0x04000123 RID: 291
		public const string SDL_PROP_WINDOW_WAYLAND_XDG_POSITIONER_POINTER = "SDL.window.wayland.xdg_positioner";

		// Token: 0x04000124 RID: 292
		public const string SDL_PROP_WINDOW_X11_DISPLAY_POINTER = "SDL.window.x11.display";

		// Token: 0x04000125 RID: 293
		public const string SDL_PROP_WINDOW_X11_SCREEN_NUMBER = "SDL.window.x11.screen";

		// Token: 0x04000126 RID: 294
		public const string SDL_PROP_WINDOW_X11_WINDOW_NUMBER = "SDL.window.x11.window";

		// Token: 0x04000127 RID: 295
		public const string SDL_PROP_WINDOW_EMSCRIPTEN_CANVAS_ID_STRING = "SDL.window.emscripten.canvas_id";

		// Token: 0x04000128 RID: 296
		public const string SDL_PROP_WINDOW_EMSCRIPTEN_KEYBOARD_ELEMENT_STRING = "SDL.window.emscripten.keyboard_element";

		// Token: 0x04000129 RID: 297
		public const string SDL_PROP_FILE_DIALOG_FILTERS_POINTER = "SDL.filedialog.filters";

		// Token: 0x0400012A RID: 298
		public const string SDL_PROP_FILE_DIALOG_NFILTERS_NUMBER = "SDL.filedialog.nfilters";

		// Token: 0x0400012B RID: 299
		public const string SDL_PROP_FILE_DIALOG_WINDOW_POINTER = "SDL.filedialog.window";

		// Token: 0x0400012C RID: 300
		public const string SDL_PROP_FILE_DIALOG_LOCATION_STRING = "SDL.filedialog.location";

		// Token: 0x0400012D RID: 301
		public const string SDL_PROP_FILE_DIALOG_MANY_BOOLEAN = "SDL.filedialog.many";

		// Token: 0x0400012E RID: 302
		public const string SDL_PROP_FILE_DIALOG_TITLE_STRING = "SDL.filedialog.title";

		// Token: 0x0400012F RID: 303
		public const string SDL_PROP_FILE_DIALOG_ACCEPT_STRING = "SDL.filedialog.accept";

		// Token: 0x04000130 RID: 304
		public const string SDL_PROP_FILE_DIALOG_CANCEL_STRING = "SDL.filedialog.cancel";

		// Token: 0x04000131 RID: 305
		public const string SDL_PROP_JOYSTICK_CAP_MONO_LED_BOOLEAN = "SDL.joystick.cap.mono_led";

		// Token: 0x04000132 RID: 306
		public const string SDL_PROP_JOYSTICK_CAP_RGB_LED_BOOLEAN = "SDL.joystick.cap.rgb_led";

		// Token: 0x04000133 RID: 307
		public const string SDL_PROP_JOYSTICK_CAP_PLAYER_LED_BOOLEAN = "SDL.joystick.cap.player_led";

		// Token: 0x04000134 RID: 308
		public const string SDL_PROP_JOYSTICK_CAP_RUMBLE_BOOLEAN = "SDL.joystick.cap.rumble";

		// Token: 0x04000135 RID: 309
		public const string SDL_PROP_JOYSTICK_CAP_TRIGGER_RUMBLE_BOOLEAN = "SDL.joystick.cap.trigger_rumble";

		// Token: 0x04000136 RID: 310
		public const string SDL_PROP_TEXTINPUT_TYPE_NUMBER = "SDL.textinput.type";

		// Token: 0x04000137 RID: 311
		public const string SDL_PROP_TEXTINPUT_CAPITALIZATION_NUMBER = "SDL.textinput.capitalization";

		// Token: 0x04000138 RID: 312
		public const string SDL_PROP_TEXTINPUT_AUTOCORRECT_BOOLEAN = "SDL.textinput.autocorrect";

		// Token: 0x04000139 RID: 313
		public const string SDL_PROP_TEXTINPUT_MULTILINE_BOOLEAN = "SDL.textinput.multiline";

		// Token: 0x0400013A RID: 314
		public const string SDL_PROP_TEXTINPUT_ANDROID_INPUTTYPE_NUMBER = "SDL.textinput.android.inputtype";

		// Token: 0x0400013B RID: 315
		public const string SDL_PROP_GPU_DEVICE_CREATE_DEBUGMODE_BOOLEAN = "SDL.gpu.device.create.debugmode";

		// Token: 0x0400013C RID: 316
		public const string SDL_PROP_GPU_DEVICE_CREATE_PREFERLOWPOWER_BOOLEAN = "SDL.gpu.device.create.preferlowpower";

		// Token: 0x0400013D RID: 317
		public const string SDL_PROP_GPU_DEVICE_CREATE_VERBOSE_BOOLEAN = "SDL.gpu.device.create.verbose";

		// Token: 0x0400013E RID: 318
		public const string SDL_PROP_GPU_DEVICE_CREATE_NAME_STRING = "SDL.gpu.device.create.name";

		// Token: 0x0400013F RID: 319
		public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_CLIP_DISTANCE_BOOLEAN = "SDL.gpu.device.create.feature.clip_distance";

		// Token: 0x04000140 RID: 320
		public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_DEPTH_CLAMPING_BOOLEAN = "SDL.gpu.device.create.feature.depth_clamping";

		// Token: 0x04000141 RID: 321
		public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_INDIRECT_DRAW_FIRST_INSTANCE_BOOLEAN = "SDL.gpu.device.create.feature.indirect_draw_first_instance";

		// Token: 0x04000142 RID: 322
		public const string SDL_PROP_GPU_DEVICE_CREATE_FEATURE_ANISOTROPY_BOOLEAN = "SDL.gpu.device.create.feature.anisotropy";

		// Token: 0x04000143 RID: 323
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_PRIVATE_BOOLEAN = "SDL.gpu.device.create.shaders.private";

		// Token: 0x04000144 RID: 324
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_SPIRV_BOOLEAN = "SDL.gpu.device.create.shaders.spirv";

		// Token: 0x04000145 RID: 325
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXBC_BOOLEAN = "SDL.gpu.device.create.shaders.dxbc";

		// Token: 0x04000146 RID: 326
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_DXIL_BOOLEAN = "SDL.gpu.device.create.shaders.dxil";

		// Token: 0x04000147 RID: 327
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_MSL_BOOLEAN = "SDL.gpu.device.create.shaders.msl";

		// Token: 0x04000148 RID: 328
		public const string SDL_PROP_GPU_DEVICE_CREATE_SHADERS_METALLIB_BOOLEAN = "SDL.gpu.device.create.shaders.metallib";

		// Token: 0x04000149 RID: 329
		public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_ALLOW_FEWER_RESOURCE_SLOTS_BOOLEAN = "SDL.gpu.device.create.d3d12.allowtier1resourcebinding";

		// Token: 0x0400014A RID: 330
		public const string SDL_PROP_GPU_DEVICE_CREATE_D3D12_SEMANTIC_NAME_STRING = "SDL.gpu.device.create.d3d12.semantic";

		// Token: 0x0400014B RID: 331
		public const string SDL_PROP_GPU_DEVICE_CREATE_VULKAN_REQUIRE_HARDWARE_ACCELERATION_BOOLEAN = "SDL.gpu.device.create.vulkan.requirehardwareacceleration";

		// Token: 0x0400014C RID: 332
		public const string SDL_PROP_GPU_DEVICE_CREATE_VULKAN_OPTIONS_POINTER = "SDL.gpu.device.create.vulkan.options";

		// Token: 0x0400014D RID: 333
		public const string SDL_PROP_GPU_DEVICE_NAME_STRING = "SDL.gpu.device.name";

		// Token: 0x0400014E RID: 334
		public const string SDL_PROP_GPU_DEVICE_DRIVER_NAME_STRING = "SDL.gpu.device.driver_name";

		// Token: 0x0400014F RID: 335
		public const string SDL_PROP_GPU_DEVICE_DRIVER_VERSION_STRING = "SDL.gpu.device.driver_version";

		// Token: 0x04000150 RID: 336
		public const string SDL_PROP_GPU_DEVICE_DRIVER_INFO_STRING = "SDL.gpu.device.driver_info";

		// Token: 0x04000151 RID: 337
		public const string SDL_PROP_GPU_COMPUTEPIPELINE_CREATE_NAME_STRING = "SDL.gpu.computepipeline.create.name";

		// Token: 0x04000152 RID: 338
		public const string SDL_PROP_GPU_GRAPHICSPIPELINE_CREATE_NAME_STRING = "SDL.gpu.graphicspipeline.create.name";

		// Token: 0x04000153 RID: 339
		public const string SDL_PROP_GPU_SAMPLER_CREATE_NAME_STRING = "SDL.gpu.sampler.create.name";

		// Token: 0x04000154 RID: 340
		public const string SDL_PROP_GPU_SHADER_CREATE_NAME_STRING = "SDL.gpu.shader.create.name";

		// Token: 0x04000155 RID: 341
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_R_FLOAT = "SDL.gpu.texture.create.d3d12.clear.r";

		// Token: 0x04000156 RID: 342
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_G_FLOAT = "SDL.gpu.texture.create.d3d12.clear.g";

		// Token: 0x04000157 RID: 343
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_B_FLOAT = "SDL.gpu.texture.create.d3d12.clear.b";

		// Token: 0x04000158 RID: 344
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_A_FLOAT = "SDL.gpu.texture.create.d3d12.clear.a";

		// Token: 0x04000159 RID: 345
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_DEPTH_FLOAT = "SDL.gpu.texture.create.d3d12.clear.depth";

		// Token: 0x0400015A RID: 346
		public const string SDL_PROP_GPU_TEXTURE_CREATE_D3D12_CLEAR_STENCIL_NUMBER = "SDL.gpu.texture.create.d3d12.clear.stencil";

		// Token: 0x0400015B RID: 347
		public const string SDL_PROP_GPU_TEXTURE_CREATE_NAME_STRING = "SDL.gpu.texture.create.name";

		// Token: 0x0400015C RID: 348
		public const string SDL_PROP_GPU_BUFFER_CREATE_NAME_STRING = "SDL.gpu.buffer.create.name";

		// Token: 0x0400015D RID: 349
		public const string SDL_PROP_GPU_TRANSFERBUFFER_CREATE_NAME_STRING = "SDL.gpu.transferbuffer.create.name";

		// Token: 0x0400015E RID: 350
		public const string SDL_PROP_HIDAPI_LIBUSB_DEVICE_HANDLE_POINTER = "SDL.hidapi.libusb.device.handle";

		// Token: 0x0400015F RID: 351
		public const string SDL_HINT_ALLOW_ALT_TAB_WHILE_GRABBED = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";

		// Token: 0x04000160 RID: 352
		public const string SDL_HINT_ANDROID_ALLOW_RECREATE_ACTIVITY = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";

		// Token: 0x04000161 RID: 353
		public const string SDL_HINT_ANDROID_BLOCK_ON_PAUSE = "SDL_ANDROID_BLOCK_ON_PAUSE";

		// Token: 0x04000162 RID: 354
		public const string SDL_HINT_ANDROID_LOW_LATENCY_AUDIO = "SDL_ANDROID_LOW_LATENCY_AUDIO";

		// Token: 0x04000163 RID: 355
		public const string SDL_HINT_ANDROID_TRAP_BACK_BUTTON = "SDL_ANDROID_TRAP_BACK_BUTTON";

		// Token: 0x04000164 RID: 356
		public const string SDL_HINT_APP_ID = "SDL_APP_ID";

		// Token: 0x04000165 RID: 357
		public const string SDL_HINT_APP_NAME = "SDL_APP_NAME";

		// Token: 0x04000166 RID: 358
		public const string SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";

		// Token: 0x04000167 RID: 359
		public const string SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";

		// Token: 0x04000168 RID: 360
		public const string SDL_HINT_AUDIO_ALSA_DEFAULT_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_DEVICE";

		// Token: 0x04000169 RID: 361
		public const string SDL_HINT_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE";

		// Token: 0x0400016A RID: 362
		public const string SDL_HINT_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE = "SDL_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE";

		// Token: 0x0400016B RID: 363
		public const string SDL_HINT_AUDIO_CATEGORY = "SDL_AUDIO_CATEGORY";

		// Token: 0x0400016C RID: 364
		public const string SDL_HINT_AUDIO_CHANNELS = "SDL_AUDIO_CHANNELS";

		// Token: 0x0400016D RID: 365
		public const string SDL_HINT_AUDIO_DEVICE_APP_ICON_NAME = "SDL_AUDIO_DEVICE_APP_ICON_NAME";

		// Token: 0x0400016E RID: 366
		public const string SDL_HINT_AUDIO_DEVICE_SAMPLE_FRAMES = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";

		// Token: 0x0400016F RID: 367
		public const string SDL_HINT_AUDIO_DEVICE_STREAM_NAME = "SDL_AUDIO_DEVICE_STREAM_NAME";

		// Token: 0x04000170 RID: 368
		public const string SDL_HINT_AUDIO_DEVICE_STREAM_ROLE = "SDL_AUDIO_DEVICE_STREAM_ROLE";

		// Token: 0x04000171 RID: 369
		public const string SDL_HINT_AUDIO_DEVICE_RAW_STREAM = "SDL_AUDIO_DEVICE_RAW_STREAM";

		// Token: 0x04000172 RID: 370
		public const string SDL_HINT_AUDIO_DISK_INPUT_FILE = "SDL_AUDIO_DISK_INPUT_FILE";

		// Token: 0x04000173 RID: 371
		public const string SDL_HINT_AUDIO_DISK_OUTPUT_FILE = "SDL_AUDIO_DISK_OUTPUT_FILE";

		// Token: 0x04000174 RID: 372
		public const string SDL_HINT_AUDIO_DISK_TIMESCALE = "SDL_AUDIO_DISK_TIMESCALE";

		// Token: 0x04000175 RID: 373
		public const string SDL_HINT_AUDIO_DRIVER = "SDL_AUDIO_DRIVER";

		// Token: 0x04000176 RID: 374
		public const string SDL_HINT_AUDIO_DUMMY_TIMESCALE = "SDL_AUDIO_DUMMY_TIMESCALE";

		// Token: 0x04000177 RID: 375
		public const string SDL_HINT_AUDIO_FORMAT = "SDL_AUDIO_FORMAT";

		// Token: 0x04000178 RID: 376
		public const string SDL_HINT_AUDIO_FREQUENCY = "SDL_AUDIO_FREQUENCY";

		// Token: 0x04000179 RID: 377
		public const string SDL_HINT_AUDIO_INCLUDE_MONITORS = "SDL_AUDIO_INCLUDE_MONITORS";

		// Token: 0x0400017A RID: 378
		public const string SDL_HINT_AUTO_UPDATE_JOYSTICKS = "SDL_AUTO_UPDATE_JOYSTICKS";

		// Token: 0x0400017B RID: 379
		public const string SDL_HINT_AUTO_UPDATE_SENSORS = "SDL_AUTO_UPDATE_SENSORS";

		// Token: 0x0400017C RID: 380
		public const string SDL_HINT_BMP_SAVE_LEGACY_FORMAT = "SDL_BMP_SAVE_LEGACY_FORMAT";

		// Token: 0x0400017D RID: 381
		public const string SDL_HINT_CAMERA_DRIVER = "SDL_CAMERA_DRIVER";

		// Token: 0x0400017E RID: 382
		public const string SDL_HINT_CPU_FEATURE_MASK = "SDL_CPU_FEATURE_MASK";

		// Token: 0x0400017F RID: 383
		public const string SDL_HINT_JOYSTICK_DIRECTINPUT = "SDL_JOYSTICK_DIRECTINPUT";

		// Token: 0x04000180 RID: 384
		public const string SDL_HINT_FILE_DIALOG_DRIVER = "SDL_FILE_DIALOG_DRIVER";

		// Token: 0x04000181 RID: 385
		public const string SDL_HINT_DISPLAY_USABLE_BOUNDS = "SDL_DISPLAY_USABLE_BOUNDS";

		// Token: 0x04000182 RID: 386
		public const string SDL_HINT_INVALID_PARAM_CHECKS = "SDL_INVALID_PARAM_CHECKS";

		// Token: 0x04000183 RID: 387
		public const string SDL_HINT_EMSCRIPTEN_ASYNCIFY = "SDL_EMSCRIPTEN_ASYNCIFY";

		// Token: 0x04000184 RID: 388
		public const string SDL_HINT_EMSCRIPTEN_CANVAS_SELECTOR = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";

		// Token: 0x04000185 RID: 389
		public const string SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";

		// Token: 0x04000186 RID: 390
		public const string SDL_HINT_ENABLE_SCREEN_KEYBOARD = "SDL_ENABLE_SCREEN_KEYBOARD";

		// Token: 0x04000187 RID: 391
		public const string SDL_HINT_EVDEV_DEVICES = "SDL_EVDEV_DEVICES";

		// Token: 0x04000188 RID: 392
		public const string SDL_HINT_EVENT_LOGGING = "SDL_EVENT_LOGGING";

		// Token: 0x04000189 RID: 393
		public const string SDL_HINT_FORCE_RAISEWINDOW = "SDL_FORCE_RAISEWINDOW";

		// Token: 0x0400018A RID: 394
		public const string SDL_HINT_FRAMEBUFFER_ACCELERATION = "SDL_FRAMEBUFFER_ACCELERATION";

		// Token: 0x0400018B RID: 395
		public const string SDL_HINT_GAMECONTROLLERCONFIG = "SDL_GAMECONTROLLERCONFIG";

		// Token: 0x0400018C RID: 396
		public const string SDL_HINT_GAMECONTROLLERCONFIG_FILE = "SDL_GAMECONTROLLERCONFIG_FILE";

		// Token: 0x0400018D RID: 397
		public const string SDL_HINT_GAMECONTROLLERTYPE = "SDL_GAMECONTROLLERTYPE";

		// Token: 0x0400018E RID: 398
		public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES = "SDL_GAMECONTROLLER_IGNORE_DEVICES";

		// Token: 0x0400018F RID: 399
		public const string SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";

		// Token: 0x04000190 RID: 400
		public const string SDL_HINT_GAMECONTROLLER_SENSOR_FUSION = "SDL_GAMECONTROLLER_SENSOR_FUSION";

		// Token: 0x04000191 RID: 401
		public const string SDL_HINT_GDK_TEXTINPUT_DEFAULT_TEXT = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";

		// Token: 0x04000192 RID: 402
		public const string SDL_HINT_GDK_TEXTINPUT_DESCRIPTION = "SDL_GDK_TEXTINPUT_DESCRIPTION";

		// Token: 0x04000193 RID: 403
		public const string SDL_HINT_GDK_TEXTINPUT_MAX_LENGTH = "SDL_GDK_TEXTINPUT_MAX_LENGTH";

		// Token: 0x04000194 RID: 404
		public const string SDL_HINT_GDK_TEXTINPUT_SCOPE = "SDL_GDK_TEXTINPUT_SCOPE";

		// Token: 0x04000195 RID: 405
		public const string SDL_HINT_GDK_TEXTINPUT_TITLE = "SDL_GDK_TEXTINPUT_TITLE";

		// Token: 0x04000196 RID: 406
		public const string SDL_HINT_HIDAPI_LIBUSB = "SDL_HIDAPI_LIBUSB";

		// Token: 0x04000197 RID: 407
		public const string SDL_HINT_HIDAPI_LIBUSB_GAMECUBE = "SDL_HIDAPI_LIBUSB_GAMECUBE";

		// Token: 0x04000198 RID: 408
		public const string SDL_HINT_HIDAPI_LIBUSB_WHITELIST = "SDL_HIDAPI_LIBUSB_WHITELIST";

		// Token: 0x04000199 RID: 409
		public const string SDL_HINT_HIDAPI_UDEV = "SDL_HIDAPI_UDEV";

		// Token: 0x0400019A RID: 410
		public const string SDL_HINT_GPU_DRIVER = "SDL_GPU_DRIVER";

		// Token: 0x0400019B RID: 411
		public const string SDL_HINT_HIDAPI_ENUMERATE_ONLY_CONTROLLERS = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";

		// Token: 0x0400019C RID: 412
		public const string SDL_HINT_HIDAPI_IGNORE_DEVICES = "SDL_HIDAPI_IGNORE_DEVICES";

		// Token: 0x0400019D RID: 413
		public const string SDL_HINT_IME_IMPLEMENTED_UI = "SDL_IME_IMPLEMENTED_UI";

		// Token: 0x0400019E RID: 414
		public const string SDL_HINT_IOS_HIDE_HOME_INDICATOR = "SDL_IOS_HIDE_HOME_INDICATOR";

		// Token: 0x0400019F RID: 415
		public const string SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";

		// Token: 0x040001A0 RID: 416
		public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES = "SDL_JOYSTICK_ARCADESTICK_DEVICES";

		// Token: 0x040001A1 RID: 417
		public const string SDL_HINT_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";

		// Token: 0x040001A2 RID: 418
		public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES = "SDL_JOYSTICK_BLACKLIST_DEVICES";

		// Token: 0x040001A3 RID: 419
		public const string SDL_HINT_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";

		// Token: 0x040001A4 RID: 420
		public const string SDL_HINT_JOYSTICK_DEVICE = "SDL_JOYSTICK_DEVICE";

		// Token: 0x040001A5 RID: 421
		public const string SDL_HINT_JOYSTICK_ENHANCED_REPORTS = "SDL_JOYSTICK_ENHANCED_REPORTS";

		// Token: 0x040001A6 RID: 422
		public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";

		// Token: 0x040001A7 RID: 423
		public const string SDL_HINT_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";

		// Token: 0x040001A8 RID: 424
		public const string SDL_HINT_JOYSTICK_GAMEINPUT = "SDL_JOYSTICK_GAMEINPUT";

		// Token: 0x040001A9 RID: 425
		public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES = "SDL_JOYSTICK_GAMECUBE_DEVICES";

		// Token: 0x040001AA RID: 426
		public const string SDL_HINT_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";

		// Token: 0x040001AB RID: 427
		public const string SDL_HINT_JOYSTICK_HIDAPI = "SDL_JOYSTICK_HIDAPI";

		// Token: 0x040001AC RID: 428
		public const string SDL_HINT_JOYSTICK_HIDAPI_COMBINE_JOY_CONS = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";

		// Token: 0x040001AD RID: 429
		public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE = "SDL_JOYSTICK_HIDAPI_GAMECUBE";

		// Token: 0x040001AE RID: 430
		public const string SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";

		// Token: 0x040001AF RID: 431
		public const string SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS = "SDL_JOYSTICK_HIDAPI_JOY_CONS";

		// Token: 0x040001B0 RID: 432
		public const string SDL_HINT_JOYSTICK_HIDAPI_JOYCON_HOME_LED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";

		// Token: 0x040001B1 RID: 433
		public const string SDL_HINT_JOYSTICK_HIDAPI_LUNA = "SDL_JOYSTICK_HIDAPI_LUNA";

		// Token: 0x040001B2 RID: 434
		public const string SDL_HINT_JOYSTICK_HIDAPI_NINTENDO_CLASSIC = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";

		// Token: 0x040001B3 RID: 435
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS3 = "SDL_JOYSTICK_HIDAPI_PS3";

		// Token: 0x040001B4 RID: 436
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";

		// Token: 0x040001B5 RID: 437
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS4 = "SDL_JOYSTICK_HIDAPI_PS4";

		// Token: 0x040001B6 RID: 438
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";

		// Token: 0x040001B7 RID: 439
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS5 = "SDL_JOYSTICK_HIDAPI_PS5";

		// Token: 0x040001B8 RID: 440
		public const string SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";

		// Token: 0x040001B9 RID: 441
		public const string SDL_HINT_JOYSTICK_HIDAPI_SHIELD = "SDL_JOYSTICK_HIDAPI_SHIELD";

		// Token: 0x040001BA RID: 442
		public const string SDL_HINT_JOYSTICK_HIDAPI_STADIA = "SDL_JOYSTICK_HIDAPI_STADIA";

		// Token: 0x040001BB RID: 443
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM = "SDL_JOYSTICK_HIDAPI_STEAM";

		// Token: 0x040001BC RID: 444
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM_HOME_LED = "SDL_JOYSTICK_HIDAPI_STEAM_HOME_LED";

		// Token: 0x040001BD RID: 445
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAMDECK = "SDL_JOYSTICK_HIDAPI_STEAMDECK";

		// Token: 0x040001BE RID: 446
		public const string SDL_HINT_JOYSTICK_HIDAPI_STEAM_HORI = "SDL_JOYSTICK_HIDAPI_STEAM_HORI";

		// Token: 0x040001BF RID: 447
		public const string SDL_HINT_JOYSTICK_HIDAPI_LG4FF = "SDL_JOYSTICK_HIDAPI_LG4FF";

		// Token: 0x040001C0 RID: 448
		public const string SDL_HINT_JOYSTICK_HIDAPI_8BITDO = "SDL_JOYSTICK_HIDAPI_8BITDO";

		// Token: 0x040001C1 RID: 449
		public const string SDL_HINT_JOYSTICK_HIDAPI_SINPUT = "SDL_JOYSTICK_HIDAPI_SINPUT";

		// Token: 0x040001C2 RID: 450
		public const string SDL_HINT_JOYSTICK_HIDAPI_ZUIKI = "SDL_JOYSTICK_HIDAPI_ZUIKI";

		// Token: 0x040001C3 RID: 451
		public const string SDL_HINT_JOYSTICK_HIDAPI_FLYDIGI = "SDL_JOYSTICK_HIDAPI_FLYDIGI";

		// Token: 0x040001C4 RID: 452
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH = "SDL_JOYSTICK_HIDAPI_SWITCH";

		// Token: 0x040001C5 RID: 453
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";

		// Token: 0x040001C6 RID: 454
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";

		// Token: 0x040001C7 RID: 455
		public const string SDL_HINT_JOYSTICK_HIDAPI_SWITCH2 = "SDL_JOYSTICK_HIDAPI_SWITCH2";

		// Token: 0x040001C8 RID: 456
		public const string SDL_HINT_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";

		// Token: 0x040001C9 RID: 457
		public const string SDL_HINT_JOYSTICK_HIDAPI_WII = "SDL_JOYSTICK_HIDAPI_WII";

		// Token: 0x040001CA RID: 458
		public const string SDL_HINT_JOYSTICK_HIDAPI_WII_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";

		// Token: 0x040001CB RID: 459
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX = "SDL_JOYSTICK_HIDAPI_XBOX";

		// Token: 0x040001CC RID: 460
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";

		// Token: 0x040001CD RID: 461
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";

		// Token: 0x040001CE RID: 462
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_360_WIRELESS = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";

		// Token: 0x040001CF RID: 463
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";

		// Token: 0x040001D0 RID: 464
		public const string SDL_HINT_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";

		// Token: 0x040001D1 RID: 465
		public const string SDL_HINT_JOYSTICK_HIDAPI_GIP = "SDL_JOYSTICK_HIDAPI_GIP";

		// Token: 0x040001D2 RID: 466
		public const string SDL_HINT_JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA = "SDL_JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA";

		// Token: 0x040001D3 RID: 467
		public const string SDL_HINT_JOYSTICK_IOKIT = "SDL_JOYSTICK_IOKIT";

		// Token: 0x040001D4 RID: 468
		public const string SDL_HINT_JOYSTICK_LINUX_CLASSIC = "SDL_JOYSTICK_LINUX_CLASSIC";

		// Token: 0x040001D5 RID: 469
		public const string SDL_HINT_JOYSTICK_LINUX_DEADZONES = "SDL_JOYSTICK_LINUX_DEADZONES";

		// Token: 0x040001D6 RID: 470
		public const string SDL_HINT_JOYSTICK_LINUX_DIGITAL_HATS = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";

		// Token: 0x040001D7 RID: 471
		public const string SDL_HINT_JOYSTICK_LINUX_HAT_DEADZONES = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";

		// Token: 0x040001D8 RID: 472
		public const string SDL_HINT_JOYSTICK_MFI = "SDL_JOYSTICK_MFI";

		// Token: 0x040001D9 RID: 473
		public const string SDL_HINT_JOYSTICK_RAWINPUT = "SDL_JOYSTICK_RAWINPUT";

		// Token: 0x040001DA RID: 474
		public const string SDL_HINT_JOYSTICK_RAWINPUT_CORRELATE_XINPUT = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";

		// Token: 0x040001DB RID: 475
		public const string SDL_HINT_JOYSTICK_ROG_CHAKRAM = "SDL_JOYSTICK_ROG_CHAKRAM";

		// Token: 0x040001DC RID: 476
		public const string SDL_HINT_JOYSTICK_THREAD = "SDL_JOYSTICK_THREAD";

		// Token: 0x040001DD RID: 477
		public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES = "SDL_JOYSTICK_THROTTLE_DEVICES";

		// Token: 0x040001DE RID: 478
		public const string SDL_HINT_JOYSTICK_THROTTLE_DEVICES_EXCLUDED = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";

		// Token: 0x040001DF RID: 479
		public const string SDL_HINT_JOYSTICK_WGI = "SDL_JOYSTICK_WGI";

		// Token: 0x040001E0 RID: 480
		public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES = "SDL_JOYSTICK_WHEEL_DEVICES";

		// Token: 0x040001E1 RID: 481
		public const string SDL_HINT_JOYSTICK_WHEEL_DEVICES_EXCLUDED = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";

		// Token: 0x040001E2 RID: 482
		public const string SDL_HINT_JOYSTICK_ZERO_CENTERED_DEVICES = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";

		// Token: 0x040001E3 RID: 483
		public const string SDL_HINT_JOYSTICK_HAPTIC_AXES = "SDL_JOYSTICK_HAPTIC_AXES";

		// Token: 0x040001E4 RID: 484
		public const string SDL_HINT_KEYCODE_OPTIONS = "SDL_KEYCODE_OPTIONS";

		// Token: 0x040001E5 RID: 485
		public const string SDL_HINT_KMSDRM_DEVICE_INDEX = "SDL_KMSDRM_DEVICE_INDEX";

		// Token: 0x040001E6 RID: 486
		public const string SDL_HINT_KMSDRM_REQUIRE_DRM_MASTER = "SDL_KMSDRM_REQUIRE_DRM_MASTER";

		// Token: 0x040001E7 RID: 487
		public const string SDL_HINT_KMSDRM_ATOMIC = "SDL_KMSDRM_ATOMIC";

		// Token: 0x040001E8 RID: 488
		public const string SDL_HINT_LOGGING = "SDL_LOGGING";

		// Token: 0x040001E9 RID: 489
		public const string SDL_HINT_MAC_BACKGROUND_APP = "SDL_MAC_BACKGROUND_APP";

		// Token: 0x040001EA RID: 490
		public const string SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";

		// Token: 0x040001EB RID: 491
		public const string SDL_HINT_MAC_OPENGL_ASYNC_DISPATCH = "SDL_MAC_OPENGL_ASYNC_DISPATCH";

		// Token: 0x040001EC RID: 492
		public const string SDL_HINT_MAC_OPTION_AS_ALT = "SDL_MAC_OPTION_AS_ALT";

		// Token: 0x040001ED RID: 493
		public const string SDL_HINT_MAC_SCROLL_MOMENTUM = "SDL_MAC_SCROLL_MOMENTUM";

		// Token: 0x040001EE RID: 494
		public const string SDL_HINT_MAC_PRESS_AND_HOLD = "SDL_MAC_PRESS_AND_HOLD";

		// Token: 0x040001EF RID: 495
		public const string SDL_HINT_MAIN_CALLBACK_RATE = "SDL_MAIN_CALLBACK_RATE";

		// Token: 0x040001F0 RID: 496
		public const string SDL_HINT_MOUSE_AUTO_CAPTURE = "SDL_MOUSE_AUTO_CAPTURE";

		// Token: 0x040001F1 RID: 497
		public const string SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";

		// Token: 0x040001F2 RID: 498
		public const string SDL_HINT_MOUSE_DOUBLE_CLICK_TIME = "SDL_MOUSE_DOUBLE_CLICK_TIME";

		// Token: 0x040001F3 RID: 499
		public const string SDL_HINT_MOUSE_DEFAULT_SYSTEM_CURSOR = "SDL_MOUSE_DEFAULT_SYSTEM_CURSOR";

		// Token: 0x040001F4 RID: 500
		public const string SDL_HINT_MOUSE_DPI_SCALE_CURSORS = "SDL_MOUSE_DPI_SCALE_CURSORS";

		// Token: 0x040001F5 RID: 501
		public const string SDL_HINT_MOUSE_EMULATE_WARP_WITH_RELATIVE = "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE";

		// Token: 0x040001F6 RID: 502
		public const string SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH = "SDL_MOUSE_FOCUS_CLICKTHROUGH";

		// Token: 0x040001F7 RID: 503
		public const string SDL_HINT_MOUSE_NORMAL_SPEED_SCALE = "SDL_MOUSE_NORMAL_SPEED_SCALE";

		// Token: 0x040001F8 RID: 504
		public const string SDL_HINT_MOUSE_RELATIVE_MODE_CENTER = "SDL_MOUSE_RELATIVE_MODE_CENTER";

		// Token: 0x040001F9 RID: 505
		public const string SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE = "SDL_MOUSE_RELATIVE_SPEED_SCALE";

		// Token: 0x040001FA RID: 506
		public const string SDL_HINT_MOUSE_RELATIVE_SYSTEM_SCALE = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";

		// Token: 0x040001FB RID: 507
		public const string SDL_HINT_MOUSE_RELATIVE_WARP_MOTION = "SDL_MOUSE_RELATIVE_WARP_MOTION";

		// Token: 0x040001FC RID: 508
		public const string SDL_HINT_MOUSE_RELATIVE_CURSOR_VISIBLE = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";

		// Token: 0x040001FD RID: 509
		public const string SDL_HINT_MOUSE_TOUCH_EVENTS = "SDL_MOUSE_TOUCH_EVENTS";

		// Token: 0x040001FE RID: 510
		public const string SDL_HINT_MUTE_CONSOLE_KEYBOARD = "SDL_MUTE_CONSOLE_KEYBOARD";

		// Token: 0x040001FF RID: 511
		public const string SDL_HINT_NO_SIGNAL_HANDLERS = "SDL_NO_SIGNAL_HANDLERS";

		// Token: 0x04000200 RID: 512
		public const string SDL_HINT_OPENGL_LIBRARY = "SDL_OPENGL_LIBRARY";

		// Token: 0x04000201 RID: 513
		public const string SDL_HINT_EGL_LIBRARY = "SDL_EGL_LIBRARY";

		// Token: 0x04000202 RID: 514
		public const string SDL_HINT_OPENGL_ES_DRIVER = "SDL_OPENGL_ES_DRIVER";

		// Token: 0x04000203 RID: 515
		public const string SDL_HINT_OPENVR_LIBRARY = "SDL_OPENVR_LIBRARY";

		// Token: 0x04000204 RID: 516
		public const string SDL_HINT_ORIENTATIONS = "SDL_ORIENTATIONS";

		// Token: 0x04000205 RID: 517
		public const string SDL_HINT_POLL_SENTINEL = "SDL_POLL_SENTINEL";

		// Token: 0x04000206 RID: 518
		public const string SDL_HINT_PREFERRED_LOCALES = "SDL_PREFERRED_LOCALES";

		// Token: 0x04000207 RID: 519
		public const string SDL_HINT_QUIT_ON_LAST_WINDOW_CLOSE = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";

		// Token: 0x04000208 RID: 520
		public const string SDL_HINT_RENDER_DIRECT3D_THREADSAFE = "SDL_RENDER_DIRECT3D_THREADSAFE";

		// Token: 0x04000209 RID: 521
		public const string SDL_HINT_RENDER_DIRECT3D11_DEBUG = "SDL_RENDER_DIRECT3D11_DEBUG";

		// Token: 0x0400020A RID: 522
		public const string SDL_HINT_RENDER_DIRECT3D11_WARP = "SDL_RENDER_DIRECT3D11_WARP";

		// Token: 0x0400020B RID: 523
		public const string SDL_HINT_RENDER_VULKAN_DEBUG = "SDL_RENDER_VULKAN_DEBUG";

		// Token: 0x0400020C RID: 524
		public const string SDL_HINT_RENDER_GPU_DEBUG = "SDL_RENDER_GPU_DEBUG";

		// Token: 0x0400020D RID: 525
		public const string SDL_HINT_RENDER_GPU_LOW_POWER = "SDL_RENDER_GPU_LOW_POWER";

		// Token: 0x0400020E RID: 526
		public const string SDL_HINT_RENDER_DRIVER = "SDL_RENDER_DRIVER";

		// Token: 0x0400020F RID: 527
		public const string SDL_HINT_RENDER_LINE_METHOD = "SDL_RENDER_LINE_METHOD";

		// Token: 0x04000210 RID: 528
		public const string SDL_HINT_RENDER_METAL_PREFER_LOW_POWER_DEVICE = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";

		// Token: 0x04000211 RID: 529
		public const string SDL_HINT_RENDER_VSYNC = "SDL_RENDER_VSYNC";

		// Token: 0x04000212 RID: 530
		public const string SDL_HINT_RETURN_KEY_HIDES_IME = "SDL_RETURN_KEY_HIDES_IME";

		// Token: 0x04000213 RID: 531
		public const string SDL_HINT_ROG_GAMEPAD_MICE = "SDL_ROG_GAMEPAD_MICE";

		// Token: 0x04000214 RID: 532
		public const string SDL_HINT_ROG_GAMEPAD_MICE_EXCLUDED = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";

		// Token: 0x04000215 RID: 533
		public const string SDL_HINT_PS2_GS_WIDTH = "SDL_PS2_GS_WIDTH";

		// Token: 0x04000216 RID: 534
		public const string SDL_HINT_PS2_GS_HEIGHT = "SDL_PS2_GS_HEIGHT";

		// Token: 0x04000217 RID: 535
		public const string SDL_HINT_PS2_GS_PROGRESSIVE = "SDL_PS2_GS_PROGRESSIVE";

		// Token: 0x04000218 RID: 536
		public const string SDL_HINT_PS2_GS_MODE = "SDL_PS2_GS_MODE";

		// Token: 0x04000219 RID: 537
		public const string SDL_HINT_RPI_VIDEO_LAYER = "SDL_RPI_VIDEO_LAYER";

		// Token: 0x0400021A RID: 538
		public const string SDL_HINT_SCREENSAVER_INHIBIT_ACTIVITY_NAME = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";

		// Token: 0x0400021B RID: 539
		public const string SDL_HINT_SHUTDOWN_DBUS_ON_QUIT = "SDL_SHUTDOWN_DBUS_ON_QUIT";

		// Token: 0x0400021C RID: 540
		public const string SDL_HINT_STORAGE_TITLE_DRIVER = "SDL_STORAGE_TITLE_DRIVER";

		// Token: 0x0400021D RID: 541
		public const string SDL_HINT_STORAGE_USER_DRIVER = "SDL_STORAGE_USER_DRIVER";

		// Token: 0x0400021E RID: 542
		public const string SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";

		// Token: 0x0400021F RID: 543
		public const string SDL_HINT_THREAD_PRIORITY_POLICY = "SDL_THREAD_PRIORITY_POLICY";

		// Token: 0x04000220 RID: 544
		public const string SDL_HINT_TIMER_RESOLUTION = "SDL_TIMER_RESOLUTION";

		// Token: 0x04000221 RID: 545
		public const string SDL_HINT_TOUCH_MOUSE_EVENTS = "SDL_TOUCH_MOUSE_EVENTS";

		// Token: 0x04000222 RID: 546
		public const string SDL_HINT_TRACKPAD_IS_TOUCH_ONLY = "SDL_TRACKPAD_IS_TOUCH_ONLY";

		// Token: 0x04000223 RID: 547
		public const string SDL_HINT_TV_REMOTE_AS_JOYSTICK = "SDL_TV_REMOTE_AS_JOYSTICK";

		// Token: 0x04000224 RID: 548
		public const string SDL_HINT_VIDEO_ALLOW_SCREENSAVER = "SDL_VIDEO_ALLOW_SCREENSAVER";

		// Token: 0x04000225 RID: 549
		public const string SDL_HINT_VIDEO_DISPLAY_PRIORITY = "SDL_VIDEO_DISPLAY_PRIORITY";

		// Token: 0x04000226 RID: 550
		public const string SDL_HINT_VIDEO_DOUBLE_BUFFER = "SDL_VIDEO_DOUBLE_BUFFER";

		// Token: 0x04000227 RID: 551
		public const string SDL_HINT_VIDEO_DRIVER = "SDL_VIDEO_DRIVER";

		// Token: 0x04000228 RID: 552
		public const string SDL_HINT_VIDEO_DUMMY_SAVE_FRAMES = "SDL_VIDEO_DUMMY_SAVE_FRAMES";

		// Token: 0x04000229 RID: 553
		public const string SDL_HINT_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";

		// Token: 0x0400022A RID: 554
		public const string SDL_HINT_VIDEO_FORCE_EGL = "SDL_VIDEO_FORCE_EGL";

		// Token: 0x0400022B RID: 555
		public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";

		// Token: 0x0400022C RID: 556
		public const string SDL_HINT_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY = "SDL_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY";

		// Token: 0x0400022D RID: 557
		public const string SDL_HINT_VIDEO_METAL_AUTO_RESIZE_DRAWABLE = "SDL_VIDEO_METAL_AUTO_RESIZE_DRAWABLE";

		// Token: 0x0400022E RID: 558
		public const string SDL_HINT_VIDEO_MATCH_EXCLUSIVE_MODE_ON_MOVE = "SDL_VIDEO_MATCH_EXCLUSIVE_MODE_ON_MOVE";

		// Token: 0x0400022F RID: 559
		public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";

		// Token: 0x04000230 RID: 560
		public const string SDL_HINT_VIDEO_OFFSCREEN_SAVE_FRAMES = "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES";

		// Token: 0x04000231 RID: 561
		public const string SDL_HINT_VIDEO_SYNC_WINDOW_OPERATIONS = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";

		// Token: 0x04000232 RID: 562
		public const string SDL_HINT_VIDEO_WAYLAND_ALLOW_LIBDECOR = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";

		// Token: 0x04000233 RID: 563
		public const string SDL_HINT_VIDEO_WAYLAND_MODE_EMULATION = "SDL_VIDEO_WAYLAND_MODE_EMULATION";

		// Token: 0x04000234 RID: 564
		public const string SDL_HINT_VIDEO_WAYLAND_MODE_SCALING = "SDL_VIDEO_WAYLAND_MODE_SCALING";

		// Token: 0x04000235 RID: 565
		public const string SDL_HINT_VIDEO_WAYLAND_PREFER_LIBDECOR = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";

		// Token: 0x04000236 RID: 566
		public const string SDL_HINT_VIDEO_WAYLAND_SCALE_TO_DISPLAY = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";

		// Token: 0x04000237 RID: 567
		public const string SDL_HINT_VIDEO_WIN_D3DCOMPILER = "SDL_VIDEO_WIN_D3DCOMPILER";

		// Token: 0x04000238 RID: 568
		public const string SDL_HINT_VIDEO_X11_EXTERNAL_WINDOW_INPUT = "SDL_VIDEO_X11_EXTERNAL_WINDOW_INPUT";

		// Token: 0x04000239 RID: 569
		public const string SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

		// Token: 0x0400023A RID: 570
		public const string SDL_HINT_VIDEO_X11_NET_WM_PING = "SDL_VIDEO_X11_NET_WM_PING";

		// Token: 0x0400023B RID: 571
		public const string SDL_HINT_VIDEO_X11_NODIRECTCOLOR = "SDL_VIDEO_X11_NODIRECTCOLOR";

		// Token: 0x0400023C RID: 572
		public const string SDL_HINT_VIDEO_X11_SCALING_FACTOR = "SDL_VIDEO_X11_SCALING_FACTOR";

		// Token: 0x0400023D RID: 573
		public const string SDL_HINT_VIDEO_X11_VISUALID = "SDL_VIDEO_X11_VISUALID";

		// Token: 0x0400023E RID: 574
		public const string SDL_HINT_VIDEO_X11_WINDOW_VISUALID = "SDL_VIDEO_X11_WINDOW_VISUALID";

		// Token: 0x0400023F RID: 575
		public const string SDL_HINT_VIDEO_X11_XRANDR = "SDL_VIDEO_X11_XRANDR";

		// Token: 0x04000240 RID: 576
		public const string SDL_HINT_VITA_ENABLE_BACK_TOUCH = "SDL_VITA_ENABLE_BACK_TOUCH";

		// Token: 0x04000241 RID: 577
		public const string SDL_HINT_VITA_ENABLE_FRONT_TOUCH = "SDL_VITA_ENABLE_FRONT_TOUCH";

		// Token: 0x04000242 RID: 578
		public const string SDL_HINT_VITA_MODULE_PATH = "SDL_VITA_MODULE_PATH";

		// Token: 0x04000243 RID: 579
		public const string SDL_HINT_VITA_PVR_INIT = "SDL_VITA_PVR_INIT";

		// Token: 0x04000244 RID: 580
		public const string SDL_HINT_VITA_RESOLUTION = "SDL_VITA_RESOLUTION";

		// Token: 0x04000245 RID: 581
		public const string SDL_HINT_VITA_PVR_OPENGL = "SDL_VITA_PVR_OPENGL";

		// Token: 0x04000246 RID: 582
		public const string SDL_HINT_VITA_TOUCH_MOUSE_DEVICE = "SDL_VITA_TOUCH_MOUSE_DEVICE";

		// Token: 0x04000247 RID: 583
		public const string SDL_HINT_VULKAN_DISPLAY = "SDL_VULKAN_DISPLAY";

		// Token: 0x04000248 RID: 584
		public const string SDL_HINT_VULKAN_LIBRARY = "SDL_VULKAN_LIBRARY";

		// Token: 0x04000249 RID: 585
		public const string SDL_HINT_WAVE_FACT_CHUNK = "SDL_WAVE_FACT_CHUNK";

		// Token: 0x0400024A RID: 586
		public const string SDL_HINT_WAVE_CHUNK_LIMIT = "SDL_WAVE_CHUNK_LIMIT";

		// Token: 0x0400024B RID: 587
		public const string SDL_HINT_WAVE_RIFF_CHUNK_SIZE = "SDL_WAVE_RIFF_CHUNK_SIZE";

		// Token: 0x0400024C RID: 588
		public const string SDL_HINT_WAVE_TRUNCATION = "SDL_WAVE_TRUNCATION";

		// Token: 0x0400024D RID: 589
		public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_RAISED = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";

		// Token: 0x0400024E RID: 590
		public const string SDL_HINT_WINDOW_ACTIVATE_WHEN_SHOWN = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";

		// Token: 0x0400024F RID: 591
		public const string SDL_HINT_WINDOW_ALLOW_TOPMOST = "SDL_WINDOW_ALLOW_TOPMOST";

		// Token: 0x04000250 RID: 592
		public const string SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

		// Token: 0x04000251 RID: 593
		public const string SDL_HINT_WINDOWS_CLOSE_ON_ALT_F4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";

		// Token: 0x04000252 RID: 594
		public const string SDL_HINT_WINDOWS_ENABLE_MENU_MNEMONICS = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";

		// Token: 0x04000253 RID: 595
		public const string SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP = "SDL_WINDOWS_ENABLE_MESSAGELOOP";

		// Token: 0x04000254 RID: 596
		public const string SDL_HINT_WINDOWS_GAMEINPUT = "SDL_WINDOWS_GAMEINPUT";

		// Token: 0x04000255 RID: 597
		public const string SDL_HINT_WINDOWS_RAW_KEYBOARD = "SDL_WINDOWS_RAW_KEYBOARD";

		// Token: 0x04000256 RID: 598
		public const string SDL_HINT_WINDOWS_RAW_KEYBOARD_EXCLUDE_HOTKEYS = "SDL_WINDOWS_RAW_KEYBOARD_EXCLUDE_HOTKEYS";

		// Token: 0x04000257 RID: 599
		public const string SDL_HINT_WINDOWS_FORCE_SEMAPHORE_KERNEL = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";

		// Token: 0x04000258 RID: 600
		public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON = "SDL_WINDOWS_INTRESOURCE_ICON";

		// Token: 0x04000259 RID: 601
		public const string SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";

		// Token: 0x0400025A RID: 602
		public const string SDL_HINT_WINDOWS_USE_D3D9EX = "SDL_WINDOWS_USE_D3D9EX";

		// Token: 0x0400025B RID: 603
		public const string SDL_HINT_WINDOWS_ERASE_BACKGROUND_MODE = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";

		// Token: 0x0400025C RID: 604
		public const string SDL_HINT_X11_FORCE_OVERRIDE_REDIRECT = "SDL_X11_FORCE_OVERRIDE_REDIRECT";

		// Token: 0x0400025D RID: 605
		public const string SDL_HINT_X11_WINDOW_TYPE = "SDL_X11_WINDOW_TYPE";

		// Token: 0x0400025E RID: 606
		public const string SDL_HINT_X11_XCB_LIBRARY = "SDL_X11_XCB_LIBRARY";

		// Token: 0x0400025F RID: 607
		public const string SDL_HINT_XINPUT_ENABLED = "SDL_XINPUT_ENABLED";

		// Token: 0x04000260 RID: 608
		public const string SDL_HINT_ASSERT = "SDL_ASSERT";

		// Token: 0x04000261 RID: 609
		public const string SDL_HINT_PEN_MOUSE_EVENTS = "SDL_PEN_MOUSE_EVENTS";

		// Token: 0x04000262 RID: 610
		public const string SDL_HINT_PEN_TOUCH_EVENTS = "SDL_PEN_TOUCH_EVENTS";

		// Token: 0x04000263 RID: 611
		public const string SDL_PROP_APP_METADATA_NAME_STRING = "SDL.app.metadata.name";

		// Token: 0x04000264 RID: 612
		public const string SDL_PROP_APP_METADATA_VERSION_STRING = "SDL.app.metadata.version";

		// Token: 0x04000265 RID: 613
		public const string SDL_PROP_APP_METADATA_IDENTIFIER_STRING = "SDL.app.metadata.identifier";

		// Token: 0x04000266 RID: 614
		public const string SDL_PROP_APP_METADATA_CREATOR_STRING = "SDL.app.metadata.creator";

		// Token: 0x04000267 RID: 615
		public const string SDL_PROP_APP_METADATA_COPYRIGHT_STRING = "SDL.app.metadata.copyright";

		// Token: 0x04000268 RID: 616
		public const string SDL_PROP_APP_METADATA_URL_STRING = "SDL.app.metadata.url";

		// Token: 0x04000269 RID: 617
		public const string SDL_PROP_APP_METADATA_TYPE_STRING = "SDL.app.metadata.type";

		// Token: 0x0400026A RID: 618
		public const string SDL_PROP_PROCESS_CREATE_ARGS_POINTER = "SDL.process.create.args";

		// Token: 0x0400026B RID: 619
		public const string SDL_PROP_PROCESS_CREATE_ENVIRONMENT_POINTER = "SDL.process.create.environment";

		// Token: 0x0400026C RID: 620
		public const string SDL_PROP_PROCESS_CREATE_WORKING_DIRECTORY_STRING = "SDL.process.create.working_directory";

		// Token: 0x0400026D RID: 621
		public const string SDL_PROP_PROCESS_CREATE_STDIN_NUMBER = "SDL.process.create.stdin_option";

		// Token: 0x0400026E RID: 622
		public const string SDL_PROP_PROCESS_CREATE_STDIN_POINTER = "SDL.process.create.stdin_source";

		// Token: 0x0400026F RID: 623
		public const string SDL_PROP_PROCESS_CREATE_STDOUT_NUMBER = "SDL.process.create.stdout_option";

		// Token: 0x04000270 RID: 624
		public const string SDL_PROP_PROCESS_CREATE_STDOUT_POINTER = "SDL.process.create.stdout_source";

		// Token: 0x04000271 RID: 625
		public const string SDL_PROP_PROCESS_CREATE_STDERR_NUMBER = "SDL.process.create.stderr_option";

		// Token: 0x04000272 RID: 626
		public const string SDL_PROP_PROCESS_CREATE_STDERR_POINTER = "SDL.process.create.stderr_source";

		// Token: 0x04000273 RID: 627
		public const string SDL_PROP_PROCESS_CREATE_STDERR_TO_STDOUT_BOOLEAN = "SDL.process.create.stderr_to_stdout";

		// Token: 0x04000274 RID: 628
		public const string SDL_PROP_PROCESS_CREATE_BACKGROUND_BOOLEAN = "SDL.process.create.background";

		// Token: 0x04000275 RID: 629
		public const string SDL_PROP_PROCESS_CREATE_CMDLINE_STRING = "SDL.process.create.cmdline";

		// Token: 0x04000276 RID: 630
		public const string SDL_PROP_PROCESS_PID_NUMBER = "SDL.process.pid";

		// Token: 0x04000277 RID: 631
		public const string SDL_PROP_PROCESS_STDIN_POINTER = "SDL.process.stdin";

		// Token: 0x04000278 RID: 632
		public const string SDL_PROP_PROCESS_STDOUT_POINTER = "SDL.process.stdout";

		// Token: 0x04000279 RID: 633
		public const string SDL_PROP_PROCESS_STDERR_POINTER = "SDL.process.stderr";

		// Token: 0x0400027A RID: 634
		public const string SDL_PROP_PROCESS_BACKGROUND_BOOLEAN = "SDL.process.background";

		// Token: 0x0400027B RID: 635
		public const string SDL_PROP_RENDERER_CREATE_NAME_STRING = "SDL.renderer.create.name";

		// Token: 0x0400027C RID: 636
		public const string SDL_PROP_RENDERER_CREATE_WINDOW_POINTER = "SDL.renderer.create.window";

		// Token: 0x0400027D RID: 637
		public const string SDL_PROP_RENDERER_CREATE_SURFACE_POINTER = "SDL.renderer.create.surface";

		// Token: 0x0400027E RID: 638
		public const string SDL_PROP_RENDERER_CREATE_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.create.output_colorspace";

		// Token: 0x0400027F RID: 639
		public const string SDL_PROP_RENDERER_CREATE_PRESENT_VSYNC_NUMBER = "SDL.renderer.create.present_vsync";

		// Token: 0x04000280 RID: 640
		public const string SDL_PROP_RENDERER_CREATE_GPU_DEVICE_POINTER = "SDL.renderer.create.gpu.device";

		// Token: 0x04000281 RID: 641
		public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_SPIRV_BOOLEAN = "SDL.renderer.create.gpu.shaders_spirv";

		// Token: 0x04000282 RID: 642
		public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_DXIL_BOOLEAN = "SDL.renderer.create.gpu.shaders_dxil";

		// Token: 0x04000283 RID: 643
		public const string SDL_PROP_RENDERER_CREATE_GPU_SHADERS_MSL_BOOLEAN = "SDL.renderer.create.gpu.shaders_msl";

		// Token: 0x04000284 RID: 644
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_INSTANCE_POINTER = "SDL.renderer.create.vulkan.instance";

		// Token: 0x04000285 RID: 645
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_SURFACE_NUMBER = "SDL.renderer.create.vulkan.surface";

		// Token: 0x04000286 RID: 646
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.create.vulkan.physical_device";

		// Token: 0x04000287 RID: 647
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_DEVICE_POINTER = "SDL.renderer.create.vulkan.device";

		// Token: 0x04000288 RID: 648
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.graphics_queue_family_index";

		// Token: 0x04000289 RID: 649
		public const string SDL_PROP_RENDERER_CREATE_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.create.vulkan.present_queue_family_index";

		// Token: 0x0400028A RID: 650
		public const string SDL_PROP_RENDERER_NAME_STRING = "SDL.renderer.name";

		// Token: 0x0400028B RID: 651
		public const string SDL_PROP_RENDERER_WINDOW_POINTER = "SDL.renderer.window";

		// Token: 0x0400028C RID: 652
		public const string SDL_PROP_RENDERER_SURFACE_POINTER = "SDL.renderer.surface";

		// Token: 0x0400028D RID: 653
		public const string SDL_PROP_RENDERER_VSYNC_NUMBER = "SDL.renderer.vsync";

		// Token: 0x0400028E RID: 654
		public const string SDL_PROP_RENDERER_MAX_TEXTURE_SIZE_NUMBER = "SDL.renderer.max_texture_size";

		// Token: 0x0400028F RID: 655
		public const string SDL_PROP_RENDERER_TEXTURE_FORMATS_POINTER = "SDL.renderer.texture_formats";

		// Token: 0x04000290 RID: 656
		public const string SDL_PROP_RENDERER_TEXTURE_WRAPPING_BOOLEAN = "SDL.renderer.texture_wrapping";

		// Token: 0x04000291 RID: 657
		public const string SDL_PROP_RENDERER_OUTPUT_COLORSPACE_NUMBER = "SDL.renderer.output_colorspace";

		// Token: 0x04000292 RID: 658
		public const string SDL_PROP_RENDERER_HDR_ENABLED_BOOLEAN = "SDL.renderer.HDR_enabled";

		// Token: 0x04000293 RID: 659
		public const string SDL_PROP_RENDERER_SDR_WHITE_POINT_FLOAT = "SDL.renderer.SDR_white_point";

		// Token: 0x04000294 RID: 660
		public const string SDL_PROP_RENDERER_HDR_HEADROOM_FLOAT = "SDL.renderer.HDR_headroom";

		// Token: 0x04000295 RID: 661
		public const string SDL_PROP_RENDERER_D3D9_DEVICE_POINTER = "SDL.renderer.d3d9.device";

		// Token: 0x04000296 RID: 662
		public const string SDL_PROP_RENDERER_D3D11_DEVICE_POINTER = "SDL.renderer.d3d11.device";

		// Token: 0x04000297 RID: 663
		public const string SDL_PROP_RENDERER_D3D11_SWAPCHAIN_POINTER = "SDL.renderer.d3d11.swap_chain";

		// Token: 0x04000298 RID: 664
		public const string SDL_PROP_RENDERER_D3D12_DEVICE_POINTER = "SDL.renderer.d3d12.device";

		// Token: 0x04000299 RID: 665
		public const string SDL_PROP_RENDERER_D3D12_SWAPCHAIN_POINTER = "SDL.renderer.d3d12.swap_chain";

		// Token: 0x0400029A RID: 666
		public const string SDL_PROP_RENDERER_D3D12_COMMAND_QUEUE_POINTER = "SDL.renderer.d3d12.command_queue";

		// Token: 0x0400029B RID: 667
		public const string SDL_PROP_RENDERER_VULKAN_INSTANCE_POINTER = "SDL.renderer.vulkan.instance";

		// Token: 0x0400029C RID: 668
		public const string SDL_PROP_RENDERER_VULKAN_SURFACE_NUMBER = "SDL.renderer.vulkan.surface";

		// Token: 0x0400029D RID: 669
		public const string SDL_PROP_RENDERER_VULKAN_PHYSICAL_DEVICE_POINTER = "SDL.renderer.vulkan.physical_device";

		// Token: 0x0400029E RID: 670
		public const string SDL_PROP_RENDERER_VULKAN_DEVICE_POINTER = "SDL.renderer.vulkan.device";

		// Token: 0x0400029F RID: 671
		public const string SDL_PROP_RENDERER_VULKAN_GRAPHICS_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.graphics_queue_family_index";

		// Token: 0x040002A0 RID: 672
		public const string SDL_PROP_RENDERER_VULKAN_PRESENT_QUEUE_FAMILY_INDEX_NUMBER = "SDL.renderer.vulkan.present_queue_family_index";

		// Token: 0x040002A1 RID: 673
		public const string SDL_PROP_RENDERER_VULKAN_SWAPCHAIN_IMAGE_COUNT_NUMBER = "SDL.renderer.vulkan.swapchain_image_count";

		// Token: 0x040002A2 RID: 674
		public const string SDL_PROP_RENDERER_GPU_DEVICE_POINTER = "SDL.renderer.gpu.device";

		// Token: 0x040002A3 RID: 675
		public const string SDL_PROP_TEXTURE_CREATE_COLORSPACE_NUMBER = "SDL.texture.create.colorspace";

		// Token: 0x040002A4 RID: 676
		public const string SDL_PROP_TEXTURE_CREATE_FORMAT_NUMBER = "SDL.texture.create.format";

		// Token: 0x040002A5 RID: 677
		public const string SDL_PROP_TEXTURE_CREATE_ACCESS_NUMBER = "SDL.texture.create.access";

		// Token: 0x040002A6 RID: 678
		public const string SDL_PROP_TEXTURE_CREATE_WIDTH_NUMBER = "SDL.texture.create.width";

		// Token: 0x040002A7 RID: 679
		public const string SDL_PROP_TEXTURE_CREATE_HEIGHT_NUMBER = "SDL.texture.create.height";

		// Token: 0x040002A8 RID: 680
		public const string SDL_PROP_TEXTURE_CREATE_PALETTE_POINTER = "SDL.texture.create.palette";

		// Token: 0x040002A9 RID: 681
		public const string SDL_PROP_TEXTURE_CREATE_SDR_WHITE_POINT_FLOAT = "SDL.texture.create.SDR_white_point";

		// Token: 0x040002AA RID: 682
		public const string SDL_PROP_TEXTURE_CREATE_HDR_HEADROOM_FLOAT = "SDL.texture.create.HDR_headroom";

		// Token: 0x040002AB RID: 683
		public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_POINTER = "SDL.texture.create.d3d11.texture";

		// Token: 0x040002AC RID: 684
		public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_U_POINTER = "SDL.texture.create.d3d11.texture_u";

		// Token: 0x040002AD RID: 685
		public const string SDL_PROP_TEXTURE_CREATE_D3D11_TEXTURE_V_POINTER = "SDL.texture.create.d3d11.texture_v";

		// Token: 0x040002AE RID: 686
		public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_POINTER = "SDL.texture.create.d3d12.texture";

		// Token: 0x040002AF RID: 687
		public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_U_POINTER = "SDL.texture.create.d3d12.texture_u";

		// Token: 0x040002B0 RID: 688
		public const string SDL_PROP_TEXTURE_CREATE_D3D12_TEXTURE_V_POINTER = "SDL.texture.create.d3d12.texture_v";

		// Token: 0x040002B1 RID: 689
		public const string SDL_PROP_TEXTURE_CREATE_METAL_PIXELBUFFER_POINTER = "SDL.texture.create.metal.pixelbuffer";

		// Token: 0x040002B2 RID: 690
		public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_NUMBER = "SDL.texture.create.opengl.texture";

		// Token: 0x040002B3 RID: 691
		public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.create.opengl.texture_uv";

		// Token: 0x040002B4 RID: 692
		public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.create.opengl.texture_u";

		// Token: 0x040002B5 RID: 693
		public const string SDL_PROP_TEXTURE_CREATE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.create.opengl.texture_v";

		// Token: 0x040002B6 RID: 694
		public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.create.opengles2.texture";

		// Token: 0x040002B7 RID: 695
		public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.create.opengles2.texture_uv";

		// Token: 0x040002B8 RID: 696
		public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.create.opengles2.texture_u";

		// Token: 0x040002B9 RID: 697
		public const string SDL_PROP_TEXTURE_CREATE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.create.opengles2.texture_v";

		// Token: 0x040002BA RID: 698
		public const string SDL_PROP_TEXTURE_CREATE_VULKAN_TEXTURE_NUMBER = "SDL.texture.create.vulkan.texture";

		// Token: 0x040002BB RID: 699
		public const string SDL_PROP_TEXTURE_CREATE_VULKAN_LAYOUT_NUMBER = "SDL.texture.create.vulkan.layout";

		// Token: 0x040002BC RID: 700
		public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_POINTER = "SDL.texture.create.gpu.texture";

		// Token: 0x040002BD RID: 701
		public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_UV_POINTER = "SDL.texture.create.gpu.texture_uv";

		// Token: 0x040002BE RID: 702
		public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_U_POINTER = "SDL.texture.create.gpu.texture_u";

		// Token: 0x040002BF RID: 703
		public const string SDL_PROP_TEXTURE_CREATE_GPU_TEXTURE_V_POINTER = "SDL.texture.create.gpu.texture_v";

		// Token: 0x040002C0 RID: 704
		public const string SDL_PROP_TEXTURE_COLORSPACE_NUMBER = "SDL.texture.colorspace";

		// Token: 0x040002C1 RID: 705
		public const string SDL_PROP_TEXTURE_FORMAT_NUMBER = "SDL.texture.format";

		// Token: 0x040002C2 RID: 706
		public const string SDL_PROP_TEXTURE_ACCESS_NUMBER = "SDL.texture.access";

		// Token: 0x040002C3 RID: 707
		public const string SDL_PROP_TEXTURE_WIDTH_NUMBER = "SDL.texture.width";

		// Token: 0x040002C4 RID: 708
		public const string SDL_PROP_TEXTURE_HEIGHT_NUMBER = "SDL.texture.height";

		// Token: 0x040002C5 RID: 709
		public const string SDL_PROP_TEXTURE_SDR_WHITE_POINT_FLOAT = "SDL.texture.SDR_white_point";

		// Token: 0x040002C6 RID: 710
		public const string SDL_PROP_TEXTURE_HDR_HEADROOM_FLOAT = "SDL.texture.HDR_headroom";

		// Token: 0x040002C7 RID: 711
		public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_POINTER = "SDL.texture.d3d11.texture";

		// Token: 0x040002C8 RID: 712
		public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_U_POINTER = "SDL.texture.d3d11.texture_u";

		// Token: 0x040002C9 RID: 713
		public const string SDL_PROP_TEXTURE_D3D11_TEXTURE_V_POINTER = "SDL.texture.d3d11.texture_v";

		// Token: 0x040002CA RID: 714
		public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_POINTER = "SDL.texture.d3d12.texture";

		// Token: 0x040002CB RID: 715
		public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_U_POINTER = "SDL.texture.d3d12.texture_u";

		// Token: 0x040002CC RID: 716
		public const string SDL_PROP_TEXTURE_D3D12_TEXTURE_V_POINTER = "SDL.texture.d3d12.texture_v";

		// Token: 0x040002CD RID: 717
		public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_NUMBER = "SDL.texture.opengl.texture";

		// Token: 0x040002CE RID: 718
		public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_UV_NUMBER = "SDL.texture.opengl.texture_uv";

		// Token: 0x040002CF RID: 719
		public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_U_NUMBER = "SDL.texture.opengl.texture_u";

		// Token: 0x040002D0 RID: 720
		public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_V_NUMBER = "SDL.texture.opengl.texture_v";

		// Token: 0x040002D1 RID: 721
		public const string SDL_PROP_TEXTURE_OPENGL_TEXTURE_TARGET_NUMBER = "SDL.texture.opengl.target";

		// Token: 0x040002D2 RID: 722
		public const string SDL_PROP_TEXTURE_OPENGL_TEX_W_FLOAT = "SDL.texture.opengl.tex_w";

		// Token: 0x040002D3 RID: 723
		public const string SDL_PROP_TEXTURE_OPENGL_TEX_H_FLOAT = "SDL.texture.opengl.tex_h";

		// Token: 0x040002D4 RID: 724
		public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_NUMBER = "SDL.texture.opengles2.texture";

		// Token: 0x040002D5 RID: 725
		public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_UV_NUMBER = "SDL.texture.opengles2.texture_uv";

		// Token: 0x040002D6 RID: 726
		public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_U_NUMBER = "SDL.texture.opengles2.texture_u";

		// Token: 0x040002D7 RID: 727
		public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_V_NUMBER = "SDL.texture.opengles2.texture_v";

		// Token: 0x040002D8 RID: 728
		public const string SDL_PROP_TEXTURE_OPENGLES2_TEXTURE_TARGET_NUMBER = "SDL.texture.opengles2.target";

		// Token: 0x040002D9 RID: 729
		public const string SDL_PROP_TEXTURE_VULKAN_TEXTURE_NUMBER = "SDL.texture.vulkan.texture";

		// Token: 0x040002DA RID: 730
		public const string SDL_PROP_TEXTURE_GPU_TEXTURE_POINTER = "SDL.texture.gpu.texture";

		// Token: 0x040002DB RID: 731
		public const string SDL_PROP_TEXTURE_GPU_TEXTURE_UV_POINTER = "SDL.texture.gpu.texture_uv";

		// Token: 0x040002DC RID: 732
		public const string SDL_PROP_TEXTURE_GPU_TEXTURE_U_POINTER = "SDL.texture.gpu.texture_u";

		// Token: 0x040002DD RID: 733
		public const string SDL_PROP_TEXTURE_GPU_TEXTURE_V_POINTER = "SDL.texture.gpu.texture_v";

		// Token: 0x020001CE RID: 462
		public struct SDLBool
		{
			// Token: 0x06001921 RID: 6433 RVA: 0x0003F635 File Offset: 0x0003D835
			internal SDLBool(byte value)
			{
				this.value = value;
			}

			// Token: 0x06001922 RID: 6434 RVA: 0x0003F63E File Offset: 0x0003D83E
			public static implicit operator bool(SDL.SDLBool b)
			{
				return b.value > 0;
			}

			// Token: 0x06001923 RID: 6435 RVA: 0x0003F649 File Offset: 0x0003D849
			public static implicit operator SDL.SDLBool(bool b)
			{
				return new SDL.SDLBool((b > false) ? 1 : 0);
			}

			// Token: 0x06001924 RID: 6436 RVA: 0x0003F655 File Offset: 0x0003D855
			public bool Equals(SDL.SDLBool other)
			{
				return other.value == this.value;
			}

			// Token: 0x06001925 RID: 6437 RVA: 0x0003F665 File Offset: 0x0003D865
			public override bool Equals(object rhs)
			{
				if (rhs is bool)
				{
					return this.Equals((bool)rhs);
				}
				return rhs is SDL.SDLBool && this.Equals((SDL.SDLBool)rhs);
			}

			// Token: 0x06001926 RID: 6438 RVA: 0x0003F698 File Offset: 0x0003D898
			public override int GetHashCode()
			{
				return this.value.GetHashCode();
			}

			// Token: 0x04000CF1 RID: 3313
			private readonly byte value;

			// Token: 0x04000CF2 RID: 3314
			internal const byte FALSE_VALUE = 0;

			// Token: 0x04000CF3 RID: 3315
			internal const byte TRUE_VALUE = 1;
		}

		// Token: 0x020001CF RID: 463
		public enum SDL_AssertState
		{
			// Token: 0x04000CF5 RID: 3317
			SDL_ASSERTION_RETRY,
			// Token: 0x04000CF6 RID: 3318
			SDL_ASSERTION_BREAK,
			// Token: 0x04000CF7 RID: 3319
			SDL_ASSERTION_ABORT,
			// Token: 0x04000CF8 RID: 3320
			SDL_ASSERTION_IGNORE,
			// Token: 0x04000CF9 RID: 3321
			SDL_ASSERTION_ALWAYS_IGNORE
		}

		// Token: 0x020001D0 RID: 464
		public struct SDL_AssertData
		{
			// Token: 0x04000CFA RID: 3322
			public SDL.SDLBool always_ignore;

			// Token: 0x04000CFB RID: 3323
			public uint trigger_count;

			// Token: 0x04000CFC RID: 3324
			public unsafe byte* condition;

			// Token: 0x04000CFD RID: 3325
			public unsafe byte* filename;

			// Token: 0x04000CFE RID: 3326
			public int linenum;

			// Token: 0x04000CFF RID: 3327
			public unsafe byte* function;

			// Token: 0x04000D00 RID: 3328
			public unsafe SDL.SDL_AssertData* next;
		}

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x06001928 RID: 6440
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate SDL.SDL_AssertState SDL_AssertionHandler(SDL.SDL_AssertData* data, IntPtr userdata);

		// Token: 0x020001D2 RID: 466
		public enum SDL_AsyncIOTaskType
		{
			// Token: 0x04000D02 RID: 3330
			SDL_ASYNCIO_TASK_READ,
			// Token: 0x04000D03 RID: 3331
			SDL_ASYNCIO_TASK_WRITE,
			// Token: 0x04000D04 RID: 3332
			SDL_ASYNCIO_TASK_CLOSE
		}

		// Token: 0x020001D3 RID: 467
		public enum SDL_AsyncIOResult
		{
			// Token: 0x04000D06 RID: 3334
			SDL_ASYNCIO_COMPLETE,
			// Token: 0x04000D07 RID: 3335
			SDL_ASYNCIO_FAILURE,
			// Token: 0x04000D08 RID: 3336
			SDL_ASYNCIO_CANCELED
		}

		// Token: 0x020001D4 RID: 468
		public struct SDL_AsyncIOOutcome
		{
			// Token: 0x04000D09 RID: 3337
			public IntPtr asyncio;

			// Token: 0x04000D0A RID: 3338
			public SDL.SDL_AsyncIOTaskType type;

			// Token: 0x04000D0B RID: 3339
			public SDL.SDL_AsyncIOResult result;

			// Token: 0x04000D0C RID: 3340
			public IntPtr buffer;

			// Token: 0x04000D0D RID: 3341
			public ulong offset;

			// Token: 0x04000D0E RID: 3342
			public ulong bytes_requested;

			// Token: 0x04000D0F RID: 3343
			public ulong bytes_transferred;

			// Token: 0x04000D10 RID: 3344
			public IntPtr userdata;
		}

		// Token: 0x020001D5 RID: 469
		public struct SDL_AtomicInt
		{
			// Token: 0x04000D11 RID: 3345
			public int value;
		}

		// Token: 0x020001D6 RID: 470
		public struct SDL_AtomicU32
		{
			// Token: 0x04000D12 RID: 3346
			public uint value;
		}

		// Token: 0x020001D7 RID: 471
		public enum SDL_PropertyType
		{
			// Token: 0x04000D14 RID: 3348
			SDL_PROPERTY_TYPE_INVALID,
			// Token: 0x04000D15 RID: 3349
			SDL_PROPERTY_TYPE_POINTER,
			// Token: 0x04000D16 RID: 3350
			SDL_PROPERTY_TYPE_STRING,
			// Token: 0x04000D17 RID: 3351
			SDL_PROPERTY_TYPE_NUMBER,
			// Token: 0x04000D18 RID: 3352
			SDL_PROPERTY_TYPE_FLOAT,
			// Token: 0x04000D19 RID: 3353
			SDL_PROPERTY_TYPE_BOOLEAN
		}

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x0600192C RID: 6444
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_CleanupPropertyCallback(IntPtr userdata, IntPtr value);

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06001930 RID: 6448
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void SDL_EnumeratePropertiesCallback(IntPtr userdata, uint props, byte* name);

		// Token: 0x020001DA RID: 474
		public enum SDL_ThreadPriority
		{
			// Token: 0x04000D1B RID: 3355
			SDL_THREAD_PRIORITY_LOW,
			// Token: 0x04000D1C RID: 3356
			SDL_THREAD_PRIORITY_NORMAL,
			// Token: 0x04000D1D RID: 3357
			SDL_THREAD_PRIORITY_HIGH,
			// Token: 0x04000D1E RID: 3358
			SDL_THREAD_PRIORITY_TIME_CRITICAL
		}

		// Token: 0x020001DB RID: 475
		public enum SDL_ThreadState
		{
			// Token: 0x04000D20 RID: 3360
			SDL_THREAD_UNKNOWN,
			// Token: 0x04000D21 RID: 3361
			SDL_THREAD_ALIVE,
			// Token: 0x04000D22 RID: 3362
			SDL_THREAD_DETACHED,
			// Token: 0x04000D23 RID: 3363
			SDL_THREAD_COMPLETE
		}

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06001934 RID: 6452
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDL_ThreadFunction(IntPtr data);

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06001938 RID: 6456
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_TLSDestructorCallback(IntPtr value);

		// Token: 0x020001DE RID: 478
		public enum SDL_InitStatus
		{
			// Token: 0x04000D25 RID: 3365
			SDL_INIT_STATUS_UNINITIALIZED,
			// Token: 0x04000D26 RID: 3366
			SDL_INIT_STATUS_INITIALIZING,
			// Token: 0x04000D27 RID: 3367
			SDL_INIT_STATUS_INITIALIZED,
			// Token: 0x04000D28 RID: 3368
			SDL_INIT_STATUS_UNINITIALIZING
		}

		// Token: 0x020001DF RID: 479
		public struct SDL_InitState
		{
			// Token: 0x04000D29 RID: 3369
			public SDL.SDL_AtomicInt status;

			// Token: 0x04000D2A RID: 3370
			public ulong thread;

			// Token: 0x04000D2B RID: 3371
			public IntPtr reserved;
		}

		// Token: 0x020001E0 RID: 480
		public enum SDL_IOStatus
		{
			// Token: 0x04000D2D RID: 3373
			SDL_IO_STATUS_READY,
			// Token: 0x04000D2E RID: 3374
			SDL_IO_STATUS_ERROR,
			// Token: 0x04000D2F RID: 3375
			SDL_IO_STATUS_EOF,
			// Token: 0x04000D30 RID: 3376
			SDL_IO_STATUS_NOT_READY,
			// Token: 0x04000D31 RID: 3377
			SDL_IO_STATUS_READONLY,
			// Token: 0x04000D32 RID: 3378
			SDL_IO_STATUS_WRITEONLY
		}

		// Token: 0x020001E1 RID: 481
		public enum SDL_IOWhence
		{
			// Token: 0x04000D34 RID: 3380
			SDL_IO_SEEK_SET,
			// Token: 0x04000D35 RID: 3381
			SDL_IO_SEEK_CUR,
			// Token: 0x04000D36 RID: 3382
			SDL_IO_SEEK_END
		}

		// Token: 0x020001E2 RID: 482
		public struct SDL_IOStreamInterface
		{
			// Token: 0x04000D37 RID: 3383
			public uint version;

			// Token: 0x04000D38 RID: 3384
			public IntPtr size;

			// Token: 0x04000D39 RID: 3385
			public IntPtr seek;

			// Token: 0x04000D3A RID: 3386
			public IntPtr read;

			// Token: 0x04000D3B RID: 3387
			public IntPtr write;

			// Token: 0x04000D3C RID: 3388
			public IntPtr flush;

			// Token: 0x04000D3D RID: 3389
			public IntPtr close;
		}

		// Token: 0x020001E3 RID: 483
		public enum SDL_AudioFormat
		{
			// Token: 0x04000D3F RID: 3391
			SDL_AUDIO_UNKNOWN,
			// Token: 0x04000D40 RID: 3392
			SDL_AUDIO_U8 = 8,
			// Token: 0x04000D41 RID: 3393
			SDL_AUDIO_S8 = 32776,
			// Token: 0x04000D42 RID: 3394
			SDL_AUDIO_S16LE = 32784,
			// Token: 0x04000D43 RID: 3395
			SDL_AUDIO_S16BE = 36880,
			// Token: 0x04000D44 RID: 3396
			SDL_AUDIO_S32LE = 32800,
			// Token: 0x04000D45 RID: 3397
			SDL_AUDIO_S32BE = 36896,
			// Token: 0x04000D46 RID: 3398
			SDL_AUDIO_F32LE = 33056,
			// Token: 0x04000D47 RID: 3399
			SDL_AUDIO_F32BE = 37152,
			// Token: 0x04000D48 RID: 3400
			SDL_AUDIO_S16 = 32784,
			// Token: 0x04000D49 RID: 3401
			SDL_AUDIO_S32 = 32800,
			// Token: 0x04000D4A RID: 3402
			SDL_AUDIO_F32 = 33056
		}

		// Token: 0x020001E4 RID: 484
		public struct SDL_AudioSpec
		{
			// Token: 0x04000D4B RID: 3403
			public SDL.SDL_AudioFormat format;

			// Token: 0x04000D4C RID: 3404
			public int channels;

			// Token: 0x04000D4D RID: 3405
			public int freq;
		}

		// Token: 0x020001E5 RID: 485
		// (Invoke) Token: 0x0600193C RID: 6460
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioStreamDataCompleteCallback(IntPtr userdata, IntPtr buf, int buflen);

		// Token: 0x020001E6 RID: 486
		// (Invoke) Token: 0x06001940 RID: 6464
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AudioStreamCallback(IntPtr userdata, IntPtr stream, int additional_amount, int total_amount);

		// Token: 0x020001E7 RID: 487
		// (Invoke) Token: 0x06001944 RID: 6468
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void SDL_AudioPostmixCallback(IntPtr userdata, SDL.SDL_AudioSpec* spec, float* buffer, int buflen);

		// Token: 0x020001E8 RID: 488
		public enum SDL_BlendOperation
		{
			// Token: 0x04000D4F RID: 3407
			SDL_BLENDOPERATION_ADD = 1,
			// Token: 0x04000D50 RID: 3408
			SDL_BLENDOPERATION_SUBTRACT,
			// Token: 0x04000D51 RID: 3409
			SDL_BLENDOPERATION_REV_SUBTRACT,
			// Token: 0x04000D52 RID: 3410
			SDL_BLENDOPERATION_MINIMUM,
			// Token: 0x04000D53 RID: 3411
			SDL_BLENDOPERATION_MAXIMUM
		}

		// Token: 0x020001E9 RID: 489
		public enum SDL_BlendFactor
		{
			// Token: 0x04000D55 RID: 3413
			SDL_BLENDFACTOR_ZERO = 1,
			// Token: 0x04000D56 RID: 3414
			SDL_BLENDFACTOR_ONE,
			// Token: 0x04000D57 RID: 3415
			SDL_BLENDFACTOR_SRC_COLOR,
			// Token: 0x04000D58 RID: 3416
			SDL_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
			// Token: 0x04000D59 RID: 3417
			SDL_BLENDFACTOR_SRC_ALPHA,
			// Token: 0x04000D5A RID: 3418
			SDL_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
			// Token: 0x04000D5B RID: 3419
			SDL_BLENDFACTOR_DST_COLOR,
			// Token: 0x04000D5C RID: 3420
			SDL_BLENDFACTOR_ONE_MINUS_DST_COLOR,
			// Token: 0x04000D5D RID: 3421
			SDL_BLENDFACTOR_DST_ALPHA,
			// Token: 0x04000D5E RID: 3422
			SDL_BLENDFACTOR_ONE_MINUS_DST_ALPHA
		}

		// Token: 0x020001EA RID: 490
		public enum SDL_PixelType
		{
			// Token: 0x04000D60 RID: 3424
			SDL_PIXELTYPE_UNKNOWN,
			// Token: 0x04000D61 RID: 3425
			SDL_PIXELTYPE_INDEX1,
			// Token: 0x04000D62 RID: 3426
			SDL_PIXELTYPE_INDEX4,
			// Token: 0x04000D63 RID: 3427
			SDL_PIXELTYPE_INDEX8,
			// Token: 0x04000D64 RID: 3428
			SDL_PIXELTYPE_PACKED8,
			// Token: 0x04000D65 RID: 3429
			SDL_PIXELTYPE_PACKED16,
			// Token: 0x04000D66 RID: 3430
			SDL_PIXELTYPE_PACKED32,
			// Token: 0x04000D67 RID: 3431
			SDL_PIXELTYPE_ARRAYU8,
			// Token: 0x04000D68 RID: 3432
			SDL_PIXELTYPE_ARRAYU16,
			// Token: 0x04000D69 RID: 3433
			SDL_PIXELTYPE_ARRAYU32,
			// Token: 0x04000D6A RID: 3434
			SDL_PIXELTYPE_ARRAYF16,
			// Token: 0x04000D6B RID: 3435
			SDL_PIXELTYPE_ARRAYF32,
			// Token: 0x04000D6C RID: 3436
			SDL_PIXELTYPE_INDEX2
		}

		// Token: 0x020001EB RID: 491
		public enum SDL_BitmapOrder
		{
			// Token: 0x04000D6E RID: 3438
			SDL_BITMAPORDER_NONE,
			// Token: 0x04000D6F RID: 3439
			SDL_BITMAPORDER_4321,
			// Token: 0x04000D70 RID: 3440
			SDL_BITMAPORDER_1234
		}

		// Token: 0x020001EC RID: 492
		public enum SDL_PackedOrder
		{
			// Token: 0x04000D72 RID: 3442
			SDL_PACKEDORDER_NONE,
			// Token: 0x04000D73 RID: 3443
			SDL_PACKEDORDER_XRGB,
			// Token: 0x04000D74 RID: 3444
			SDL_PACKEDORDER_RGBX,
			// Token: 0x04000D75 RID: 3445
			SDL_PACKEDORDER_ARGB,
			// Token: 0x04000D76 RID: 3446
			SDL_PACKEDORDER_RGBA,
			// Token: 0x04000D77 RID: 3447
			SDL_PACKEDORDER_XBGR,
			// Token: 0x04000D78 RID: 3448
			SDL_PACKEDORDER_BGRX,
			// Token: 0x04000D79 RID: 3449
			SDL_PACKEDORDER_ABGR,
			// Token: 0x04000D7A RID: 3450
			SDL_PACKEDORDER_BGRA
		}

		// Token: 0x020001ED RID: 493
		public enum SDL_ArrayOrder
		{
			// Token: 0x04000D7C RID: 3452
			SDL_ARRAYORDER_NONE,
			// Token: 0x04000D7D RID: 3453
			SDL_ARRAYORDER_RGB,
			// Token: 0x04000D7E RID: 3454
			SDL_ARRAYORDER_RGBA,
			// Token: 0x04000D7F RID: 3455
			SDL_ARRAYORDER_ARGB,
			// Token: 0x04000D80 RID: 3456
			SDL_ARRAYORDER_BGR,
			// Token: 0x04000D81 RID: 3457
			SDL_ARRAYORDER_BGRA,
			// Token: 0x04000D82 RID: 3458
			SDL_ARRAYORDER_ABGR
		}

		// Token: 0x020001EE RID: 494
		public enum SDL_PackedLayout
		{
			// Token: 0x04000D84 RID: 3460
			SDL_PACKEDLAYOUT_NONE,
			// Token: 0x04000D85 RID: 3461
			SDL_PACKEDLAYOUT_332,
			// Token: 0x04000D86 RID: 3462
			SDL_PACKEDLAYOUT_4444,
			// Token: 0x04000D87 RID: 3463
			SDL_PACKEDLAYOUT_1555,
			// Token: 0x04000D88 RID: 3464
			SDL_PACKEDLAYOUT_5551,
			// Token: 0x04000D89 RID: 3465
			SDL_PACKEDLAYOUT_565,
			// Token: 0x04000D8A RID: 3466
			SDL_PACKEDLAYOUT_8888,
			// Token: 0x04000D8B RID: 3467
			SDL_PACKEDLAYOUT_2101010,
			// Token: 0x04000D8C RID: 3468
			SDL_PACKEDLAYOUT_1010102
		}

		// Token: 0x020001EF RID: 495
		public enum SDL_PixelFormat
		{
			// Token: 0x04000D8E RID: 3470
			SDL_PIXELFORMAT_UNKNOWN,
			// Token: 0x04000D8F RID: 3471
			SDL_PIXELFORMAT_INDEX1LSB = 286261504,
			// Token: 0x04000D90 RID: 3472
			SDL_PIXELFORMAT_INDEX1MSB = 287310080,
			// Token: 0x04000D91 RID: 3473
			SDL_PIXELFORMAT_INDEX2LSB = 470811136,
			// Token: 0x04000D92 RID: 3474
			SDL_PIXELFORMAT_INDEX2MSB = 471859712,
			// Token: 0x04000D93 RID: 3475
			SDL_PIXELFORMAT_INDEX4LSB = 303039488,
			// Token: 0x04000D94 RID: 3476
			SDL_PIXELFORMAT_INDEX4MSB = 304088064,
			// Token: 0x04000D95 RID: 3477
			SDL_PIXELFORMAT_INDEX8 = 318769153,
			// Token: 0x04000D96 RID: 3478
			SDL_PIXELFORMAT_RGB332 = 336660481,
			// Token: 0x04000D97 RID: 3479
			SDL_PIXELFORMAT_XRGB4444 = 353504258,
			// Token: 0x04000D98 RID: 3480
			SDL_PIXELFORMAT_XBGR4444 = 357698562,
			// Token: 0x04000D99 RID: 3481
			SDL_PIXELFORMAT_XRGB1555 = 353570562,
			// Token: 0x04000D9A RID: 3482
			SDL_PIXELFORMAT_XBGR1555 = 357764866,
			// Token: 0x04000D9B RID: 3483
			SDL_PIXELFORMAT_ARGB4444 = 355602434,
			// Token: 0x04000D9C RID: 3484
			SDL_PIXELFORMAT_RGBA4444 = 356651010,
			// Token: 0x04000D9D RID: 3485
			SDL_PIXELFORMAT_ABGR4444 = 359796738,
			// Token: 0x04000D9E RID: 3486
			SDL_PIXELFORMAT_BGRA4444 = 360845314,
			// Token: 0x04000D9F RID: 3487
			SDL_PIXELFORMAT_ARGB1555 = 355667970,
			// Token: 0x04000DA0 RID: 3488
			SDL_PIXELFORMAT_RGBA5551 = 356782082,
			// Token: 0x04000DA1 RID: 3489
			SDL_PIXELFORMAT_ABGR1555 = 359862274,
			// Token: 0x04000DA2 RID: 3490
			SDL_PIXELFORMAT_BGRA5551 = 360976386,
			// Token: 0x04000DA3 RID: 3491
			SDL_PIXELFORMAT_RGB565 = 353701890,
			// Token: 0x04000DA4 RID: 3492
			SDL_PIXELFORMAT_BGR565 = 357896194,
			// Token: 0x04000DA5 RID: 3493
			SDL_PIXELFORMAT_RGB24 = 386930691,
			// Token: 0x04000DA6 RID: 3494
			SDL_PIXELFORMAT_BGR24 = 390076419,
			// Token: 0x04000DA7 RID: 3495
			SDL_PIXELFORMAT_XRGB8888 = 370546692,
			// Token: 0x04000DA8 RID: 3496
			SDL_PIXELFORMAT_RGBX8888 = 371595268,
			// Token: 0x04000DA9 RID: 3497
			SDL_PIXELFORMAT_XBGR8888 = 374740996,
			// Token: 0x04000DAA RID: 3498
			SDL_PIXELFORMAT_BGRX8888 = 375789572,
			// Token: 0x04000DAB RID: 3499
			SDL_PIXELFORMAT_ARGB8888 = 372645892,
			// Token: 0x04000DAC RID: 3500
			SDL_PIXELFORMAT_RGBA8888 = 373694468,
			// Token: 0x04000DAD RID: 3501
			SDL_PIXELFORMAT_ABGR8888 = 376840196,
			// Token: 0x04000DAE RID: 3502
			SDL_PIXELFORMAT_BGRA8888 = 377888772,
			// Token: 0x04000DAF RID: 3503
			SDL_PIXELFORMAT_XRGB2101010 = 370614276,
			// Token: 0x04000DB0 RID: 3504
			SDL_PIXELFORMAT_XBGR2101010 = 374808580,
			// Token: 0x04000DB1 RID: 3505
			SDL_PIXELFORMAT_ARGB2101010 = 372711428,
			// Token: 0x04000DB2 RID: 3506
			SDL_PIXELFORMAT_ABGR2101010 = 376905732,
			// Token: 0x04000DB3 RID: 3507
			SDL_PIXELFORMAT_RGB48 = 403714054,
			// Token: 0x04000DB4 RID: 3508
			SDL_PIXELFORMAT_BGR48 = 406859782,
			// Token: 0x04000DB5 RID: 3509
			SDL_PIXELFORMAT_RGBA64 = 404766728,
			// Token: 0x04000DB6 RID: 3510
			SDL_PIXELFORMAT_ARGB64 = 405815304,
			// Token: 0x04000DB7 RID: 3511
			SDL_PIXELFORMAT_BGRA64 = 407912456,
			// Token: 0x04000DB8 RID: 3512
			SDL_PIXELFORMAT_ABGR64 = 408961032,
			// Token: 0x04000DB9 RID: 3513
			SDL_PIXELFORMAT_RGB48_FLOAT = 437268486,
			// Token: 0x04000DBA RID: 3514
			SDL_PIXELFORMAT_BGR48_FLOAT = 440414214,
			// Token: 0x04000DBB RID: 3515
			SDL_PIXELFORMAT_RGBA64_FLOAT = 438321160,
			// Token: 0x04000DBC RID: 3516
			SDL_PIXELFORMAT_ARGB64_FLOAT = 439369736,
			// Token: 0x04000DBD RID: 3517
			SDL_PIXELFORMAT_BGRA64_FLOAT = 441466888,
			// Token: 0x04000DBE RID: 3518
			SDL_PIXELFORMAT_ABGR64_FLOAT = 442515464,
			// Token: 0x04000DBF RID: 3519
			SDL_PIXELFORMAT_RGB96_FLOAT = 454057996,
			// Token: 0x04000DC0 RID: 3520
			SDL_PIXELFORMAT_BGR96_FLOAT = 457203724,
			// Token: 0x04000DC1 RID: 3521
			SDL_PIXELFORMAT_RGBA128_FLOAT = 455114768,
			// Token: 0x04000DC2 RID: 3522
			SDL_PIXELFORMAT_ARGB128_FLOAT = 456163344,
			// Token: 0x04000DC3 RID: 3523
			SDL_PIXELFORMAT_BGRA128_FLOAT = 458260496,
			// Token: 0x04000DC4 RID: 3524
			SDL_PIXELFORMAT_ABGR128_FLOAT = 459309072,
			// Token: 0x04000DC5 RID: 3525
			SDL_PIXELFORMAT_YV12 = 842094169,
			// Token: 0x04000DC6 RID: 3526
			SDL_PIXELFORMAT_IYUV = 1448433993,
			// Token: 0x04000DC7 RID: 3527
			SDL_PIXELFORMAT_YUY2 = 844715353,
			// Token: 0x04000DC8 RID: 3528
			SDL_PIXELFORMAT_UYVY = 1498831189,
			// Token: 0x04000DC9 RID: 3529
			SDL_PIXELFORMAT_YVYU = 1431918169,
			// Token: 0x04000DCA RID: 3530
			SDL_PIXELFORMAT_NV12 = 842094158,
			// Token: 0x04000DCB RID: 3531
			SDL_PIXELFORMAT_NV21 = 825382478,
			// Token: 0x04000DCC RID: 3532
			SDL_PIXELFORMAT_P010 = 808530000,
			// Token: 0x04000DCD RID: 3533
			SDL_PIXELFORMAT_EXTERNAL_OES = 542328143,
			// Token: 0x04000DCE RID: 3534
			SDL_PIXELFORMAT_MJPG = 1196444237,
			// Token: 0x04000DCF RID: 3535
			SDL_PIXELFORMAT_RGBA32 = 376840196,
			// Token: 0x04000DD0 RID: 3536
			SDL_PIXELFORMAT_ARGB32 = 377888772,
			// Token: 0x04000DD1 RID: 3537
			SDL_PIXELFORMAT_BGRA32 = 372645892,
			// Token: 0x04000DD2 RID: 3538
			SDL_PIXELFORMAT_ABGR32 = 373694468,
			// Token: 0x04000DD3 RID: 3539
			SDL_PIXELFORMAT_RGBX32 = 374740996,
			// Token: 0x04000DD4 RID: 3540
			SDL_PIXELFORMAT_XRGB32 = 375789572,
			// Token: 0x04000DD5 RID: 3541
			SDL_PIXELFORMAT_BGRX32 = 370546692,
			// Token: 0x04000DD6 RID: 3542
			SDL_PIXELFORMAT_XBGR32 = 371595268
		}

		// Token: 0x020001F0 RID: 496
		public enum SDL_ColorType
		{
			// Token: 0x04000DD8 RID: 3544
			SDL_COLOR_TYPE_UNKNOWN,
			// Token: 0x04000DD9 RID: 3545
			SDL_COLOR_TYPE_RGB,
			// Token: 0x04000DDA RID: 3546
			SDL_COLOR_TYPE_YCBCR
		}

		// Token: 0x020001F1 RID: 497
		public enum SDL_ColorRange
		{
			// Token: 0x04000DDC RID: 3548
			SDL_COLOR_RANGE_UNKNOWN,
			// Token: 0x04000DDD RID: 3549
			SDL_COLOR_RANGE_LIMITED,
			// Token: 0x04000DDE RID: 3550
			SDL_COLOR_RANGE_FULL
		}

		// Token: 0x020001F2 RID: 498
		public enum SDL_ColorPrimaries
		{
			// Token: 0x04000DE0 RID: 3552
			SDL_COLOR_PRIMARIES_UNKNOWN,
			// Token: 0x04000DE1 RID: 3553
			SDL_COLOR_PRIMARIES_BT709,
			// Token: 0x04000DE2 RID: 3554
			SDL_COLOR_PRIMARIES_UNSPECIFIED,
			// Token: 0x04000DE3 RID: 3555
			SDL_COLOR_PRIMARIES_BT470M = 4,
			// Token: 0x04000DE4 RID: 3556
			SDL_COLOR_PRIMARIES_BT470BG,
			// Token: 0x04000DE5 RID: 3557
			SDL_COLOR_PRIMARIES_BT601,
			// Token: 0x04000DE6 RID: 3558
			SDL_COLOR_PRIMARIES_SMPTE240,
			// Token: 0x04000DE7 RID: 3559
			SDL_COLOR_PRIMARIES_GENERIC_FILM,
			// Token: 0x04000DE8 RID: 3560
			SDL_COLOR_PRIMARIES_BT2020,
			// Token: 0x04000DE9 RID: 3561
			SDL_COLOR_PRIMARIES_XYZ,
			// Token: 0x04000DEA RID: 3562
			SDL_COLOR_PRIMARIES_SMPTE431,
			// Token: 0x04000DEB RID: 3563
			SDL_COLOR_PRIMARIES_SMPTE432,
			// Token: 0x04000DEC RID: 3564
			SDL_COLOR_PRIMARIES_EBU3213 = 22,
			// Token: 0x04000DED RID: 3565
			SDL_COLOR_PRIMARIES_CUSTOM = 31
		}

		// Token: 0x020001F3 RID: 499
		public enum SDL_TransferCharacteristics
		{
			// Token: 0x04000DEF RID: 3567
			SDL_TRANSFER_CHARACTERISTICS_UNKNOWN,
			// Token: 0x04000DF0 RID: 3568
			SDL_TRANSFER_CHARACTERISTICS_BT709,
			// Token: 0x04000DF1 RID: 3569
			SDL_TRANSFER_CHARACTERISTICS_UNSPECIFIED,
			// Token: 0x04000DF2 RID: 3570
			SDL_TRANSFER_CHARACTERISTICS_GAMMA22 = 4,
			// Token: 0x04000DF3 RID: 3571
			SDL_TRANSFER_CHARACTERISTICS_GAMMA28,
			// Token: 0x04000DF4 RID: 3572
			SDL_TRANSFER_CHARACTERISTICS_BT601,
			// Token: 0x04000DF5 RID: 3573
			SDL_TRANSFER_CHARACTERISTICS_SMPTE240,
			// Token: 0x04000DF6 RID: 3574
			SDL_TRANSFER_CHARACTERISTICS_LINEAR,
			// Token: 0x04000DF7 RID: 3575
			SDL_TRANSFER_CHARACTERISTICS_LOG100,
			// Token: 0x04000DF8 RID: 3576
			SDL_TRANSFER_CHARACTERISTICS_LOG100_SQRT10,
			// Token: 0x04000DF9 RID: 3577
			SDL_TRANSFER_CHARACTERISTICS_IEC61966,
			// Token: 0x04000DFA RID: 3578
			SDL_TRANSFER_CHARACTERISTICS_BT1361,
			// Token: 0x04000DFB RID: 3579
			SDL_TRANSFER_CHARACTERISTICS_SRGB,
			// Token: 0x04000DFC RID: 3580
			SDL_TRANSFER_CHARACTERISTICS_BT2020_10BIT,
			// Token: 0x04000DFD RID: 3581
			SDL_TRANSFER_CHARACTERISTICS_BT2020_12BIT,
			// Token: 0x04000DFE RID: 3582
			SDL_TRANSFER_CHARACTERISTICS_PQ,
			// Token: 0x04000DFF RID: 3583
			SDL_TRANSFER_CHARACTERISTICS_SMPTE428,
			// Token: 0x04000E00 RID: 3584
			SDL_TRANSFER_CHARACTERISTICS_HLG,
			// Token: 0x04000E01 RID: 3585
			SDL_TRANSFER_CHARACTERISTICS_CUSTOM = 31
		}

		// Token: 0x020001F4 RID: 500
		public enum SDL_MatrixCoefficients
		{
			// Token: 0x04000E03 RID: 3587
			SDL_MATRIX_COEFFICIENTS_IDENTITY,
			// Token: 0x04000E04 RID: 3588
			SDL_MATRIX_COEFFICIENTS_BT709,
			// Token: 0x04000E05 RID: 3589
			SDL_MATRIX_COEFFICIENTS_UNSPECIFIED,
			// Token: 0x04000E06 RID: 3590
			SDL_MATRIX_COEFFICIENTS_FCC = 4,
			// Token: 0x04000E07 RID: 3591
			SDL_MATRIX_COEFFICIENTS_BT470BG,
			// Token: 0x04000E08 RID: 3592
			SDL_MATRIX_COEFFICIENTS_BT601,
			// Token: 0x04000E09 RID: 3593
			SDL_MATRIX_COEFFICIENTS_SMPTE240,
			// Token: 0x04000E0A RID: 3594
			SDL_MATRIX_COEFFICIENTS_YCGCO,
			// Token: 0x04000E0B RID: 3595
			SDL_MATRIX_COEFFICIENTS_BT2020_NCL,
			// Token: 0x04000E0C RID: 3596
			SDL_MATRIX_COEFFICIENTS_BT2020_CL,
			// Token: 0x04000E0D RID: 3597
			SDL_MATRIX_COEFFICIENTS_SMPTE2085,
			// Token: 0x04000E0E RID: 3598
			SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_NCL,
			// Token: 0x04000E0F RID: 3599
			SDL_MATRIX_COEFFICIENTS_CHROMA_DERIVED_CL,
			// Token: 0x04000E10 RID: 3600
			SDL_MATRIX_COEFFICIENTS_ICTCP,
			// Token: 0x04000E11 RID: 3601
			SDL_MATRIX_COEFFICIENTS_CUSTOM = 31
		}

		// Token: 0x020001F5 RID: 501
		public enum SDL_ChromaLocation
		{
			// Token: 0x04000E13 RID: 3603
			SDL_CHROMA_LOCATION_NONE,
			// Token: 0x04000E14 RID: 3604
			SDL_CHROMA_LOCATION_LEFT,
			// Token: 0x04000E15 RID: 3605
			SDL_CHROMA_LOCATION_CENTER,
			// Token: 0x04000E16 RID: 3606
			SDL_CHROMA_LOCATION_TOPLEFT
		}

		// Token: 0x020001F6 RID: 502
		public enum SDL_Colorspace
		{
			// Token: 0x04000E18 RID: 3608
			SDL_COLORSPACE_UNKNOWN,
			// Token: 0x04000E19 RID: 3609
			SDL_COLORSPACE_SRGB = 301991328,
			// Token: 0x04000E1A RID: 3610
			SDL_COLORSPACE_SRGB_LINEAR = 301991168,
			// Token: 0x04000E1B RID: 3611
			SDL_COLORSPACE_HDR10 = 301999616,
			// Token: 0x04000E1C RID: 3612
			SDL_COLORSPACE_JPEG = 570426566,
			// Token: 0x04000E1D RID: 3613
			SDL_COLORSPACE_BT601_LIMITED = 554703046,
			// Token: 0x04000E1E RID: 3614
			SDL_COLORSPACE_BT601_FULL = 571480262,
			// Token: 0x04000E1F RID: 3615
			SDL_COLORSPACE_BT709_LIMITED = 554697761,
			// Token: 0x04000E20 RID: 3616
			SDL_COLORSPACE_BT709_FULL = 571474977,
			// Token: 0x04000E21 RID: 3617
			SDL_COLORSPACE_BT2020_LIMITED = 554706441,
			// Token: 0x04000E22 RID: 3618
			SDL_COLORSPACE_BT2020_FULL = 571483657,
			// Token: 0x04000E23 RID: 3619
			SDL_COLORSPACE_RGB_DEFAULT = 301991328,
			// Token: 0x04000E24 RID: 3620
			SDL_COLORSPACE_YUV_DEFAULT = 554703046
		}

		// Token: 0x020001F7 RID: 503
		public struct SDL_Color
		{
			// Token: 0x04000E25 RID: 3621
			public byte r;

			// Token: 0x04000E26 RID: 3622
			public byte g;

			// Token: 0x04000E27 RID: 3623
			public byte b;

			// Token: 0x04000E28 RID: 3624
			public byte a;
		}

		// Token: 0x020001F8 RID: 504
		public struct SDL_FColor
		{
			// Token: 0x04000E29 RID: 3625
			public float r;

			// Token: 0x04000E2A RID: 3626
			public float g;

			// Token: 0x04000E2B RID: 3627
			public float b;

			// Token: 0x04000E2C RID: 3628
			public float a;
		}

		// Token: 0x020001F9 RID: 505
		public struct SDL_Palette
		{
			// Token: 0x04000E2D RID: 3629
			public int ncolors;

			// Token: 0x04000E2E RID: 3630
			public unsafe SDL.SDL_Color* colors;

			// Token: 0x04000E2F RID: 3631
			public uint version;

			// Token: 0x04000E30 RID: 3632
			public int refcount;
		}

		// Token: 0x020001FA RID: 506
		public struct SDL_PixelFormatDetails
		{
			// Token: 0x04000E31 RID: 3633
			public SDL.SDL_PixelFormat format;

			// Token: 0x04000E32 RID: 3634
			public byte bits_per_pixel;

			// Token: 0x04000E33 RID: 3635
			public byte bytes_per_pixel;

			// Token: 0x04000E34 RID: 3636
			[FixedBuffer(typeof(byte), 2)]
			public SDL.SDL_PixelFormatDetails.<padding>e__FixedBuffer padding;

			// Token: 0x04000E35 RID: 3637
			public uint Rmask;

			// Token: 0x04000E36 RID: 3638
			public uint Gmask;

			// Token: 0x04000E37 RID: 3639
			public uint Bmask;

			// Token: 0x04000E38 RID: 3640
			public uint Amask;

			// Token: 0x04000E39 RID: 3641
			public byte Rbits;

			// Token: 0x04000E3A RID: 3642
			public byte Gbits;

			// Token: 0x04000E3B RID: 3643
			public byte Bbits;

			// Token: 0x04000E3C RID: 3644
			public byte Abits;

			// Token: 0x04000E3D RID: 3645
			public byte Rshift;

			// Token: 0x04000E3E RID: 3646
			public byte Gshift;

			// Token: 0x04000E3F RID: 3647
			public byte Bshift;

			// Token: 0x04000E40 RID: 3648
			public byte Ashift;

			// Token: 0x020003F7 RID: 1015
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 2)]
			public struct <padding>e__FixedBuffer
			{
				// Token: 0x04001E27 RID: 7719
				public byte FixedElementField;
			}
		}

		// Token: 0x020001FB RID: 507
		public struct SDL_Point
		{
			// Token: 0x04000E41 RID: 3649
			public int x;

			// Token: 0x04000E42 RID: 3650
			public int y;
		}

		// Token: 0x020001FC RID: 508
		public struct SDL_FPoint
		{
			// Token: 0x04000E43 RID: 3651
			public float x;

			// Token: 0x04000E44 RID: 3652
			public float y;
		}

		// Token: 0x020001FD RID: 509
		public struct SDL_Rect
		{
			// Token: 0x04000E45 RID: 3653
			public int x;

			// Token: 0x04000E46 RID: 3654
			public int y;

			// Token: 0x04000E47 RID: 3655
			public int w;

			// Token: 0x04000E48 RID: 3656
			public int h;
		}

		// Token: 0x020001FE RID: 510
		public struct SDL_FRect
		{
			// Token: 0x04000E49 RID: 3657
			public float x;

			// Token: 0x04000E4A RID: 3658
			public float y;

			// Token: 0x04000E4B RID: 3659
			public float w;

			// Token: 0x04000E4C RID: 3660
			public float h;
		}

		// Token: 0x020001FF RID: 511
		[Flags]
		public enum SDL_SurfaceFlags : uint
		{
			// Token: 0x04000E4E RID: 3662
			SDL_SURFACE_PREALLOCATED = 1U,
			// Token: 0x04000E4F RID: 3663
			SDL_SURFACE_LOCK_NEEDED = 2U,
			// Token: 0x04000E50 RID: 3664
			SDL_SURFACE_LOCKED = 4U,
			// Token: 0x04000E51 RID: 3665
			SDL_SURFACE_SIMD_ALIGNED = 8U
		}

		// Token: 0x02000200 RID: 512
		public enum SDL_ScaleMode
		{
			// Token: 0x04000E53 RID: 3667
			SDL_SCALEMODE_INVALID = -1,
			// Token: 0x04000E54 RID: 3668
			SDL_SCALEMODE_NEAREST,
			// Token: 0x04000E55 RID: 3669
			SDL_SCALEMODE_LINEAR,
			// Token: 0x04000E56 RID: 3670
			SDL_SCALEMODE_PIXELART
		}

		// Token: 0x02000201 RID: 513
		public enum SDL_FlipMode
		{
			// Token: 0x04000E58 RID: 3672
			SDL_FLIP_NONE,
			// Token: 0x04000E59 RID: 3673
			SDL_FLIP_HORIZONTAL,
			// Token: 0x04000E5A RID: 3674
			SDL_FLIP_VERTICAL,
			// Token: 0x04000E5B RID: 3675
			SDL_FLIP_HORIZONTAL_AND_VERTICAL
		}

		// Token: 0x02000202 RID: 514
		public struct SDL_Surface
		{
			// Token: 0x04000E5C RID: 3676
			public SDL.SDL_SurfaceFlags flags;

			// Token: 0x04000E5D RID: 3677
			public SDL.SDL_PixelFormat format;

			// Token: 0x04000E5E RID: 3678
			public int w;

			// Token: 0x04000E5F RID: 3679
			public int h;

			// Token: 0x04000E60 RID: 3680
			public int pitch;

			// Token: 0x04000E61 RID: 3681
			public IntPtr pixels;

			// Token: 0x04000E62 RID: 3682
			public int refcount;

			// Token: 0x04000E63 RID: 3683
			public IntPtr reserved;
		}

		// Token: 0x02000203 RID: 515
		public struct SDL_CameraSpec
		{
			// Token: 0x04000E64 RID: 3684
			public SDL.SDL_PixelFormat format;

			// Token: 0x04000E65 RID: 3685
			public SDL.SDL_Colorspace colorspace;

			// Token: 0x04000E66 RID: 3686
			public int width;

			// Token: 0x04000E67 RID: 3687
			public int height;

			// Token: 0x04000E68 RID: 3688
			public int framerate_numerator;

			// Token: 0x04000E69 RID: 3689
			public int framerate_denominator;
		}

		// Token: 0x02000204 RID: 516
		public enum SDL_CameraPosition
		{
			// Token: 0x04000E6B RID: 3691
			SDL_CAMERA_POSITION_UNKNOWN,
			// Token: 0x04000E6C RID: 3692
			SDL_CAMERA_POSITION_FRONT_FACING,
			// Token: 0x04000E6D RID: 3693
			SDL_CAMERA_POSITION_BACK_FACING
		}

		// Token: 0x02000205 RID: 517
		public enum SDL_CameraPermissionState
		{
			// Token: 0x04000E6F RID: 3695
			SDL_CAMERA_PERMISSION_STATE_DENIED = -1,
			// Token: 0x04000E70 RID: 3696
			SDL_CAMERA_PERMISSION_STATE_PENDING,
			// Token: 0x04000E71 RID: 3697
			SDL_CAMERA_PERMISSION_STATE_APPROVED
		}

		// Token: 0x02000206 RID: 518
		// (Invoke) Token: 0x06001948 RID: 6472
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr SDL_ClipboardDataCallback(IntPtr userdata, byte* mime_type, IntPtr size);

		// Token: 0x02000207 RID: 519
		// (Invoke) Token: 0x0600194C RID: 6476
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_ClipboardCleanupCallback(IntPtr userdata);

		// Token: 0x02000208 RID: 520
		public enum SDL_SystemTheme
		{
			// Token: 0x04000E73 RID: 3699
			SDL_SYSTEM_THEME_UNKNOWN,
			// Token: 0x04000E74 RID: 3700
			SDL_SYSTEM_THEME_LIGHT,
			// Token: 0x04000E75 RID: 3701
			SDL_SYSTEM_THEME_DARK
		}

		// Token: 0x02000209 RID: 521
		public struct SDL_DisplayMode
		{
			// Token: 0x04000E76 RID: 3702
			public uint displayID;

			// Token: 0x04000E77 RID: 3703
			public SDL.SDL_PixelFormat format;

			// Token: 0x04000E78 RID: 3704
			public int w;

			// Token: 0x04000E79 RID: 3705
			public int h;

			// Token: 0x04000E7A RID: 3706
			public float pixel_density;

			// Token: 0x04000E7B RID: 3707
			public float refresh_rate;

			// Token: 0x04000E7C RID: 3708
			public int refresh_rate_numerator;

			// Token: 0x04000E7D RID: 3709
			public int refresh_rate_denominator;

			// Token: 0x04000E7E RID: 3710
			public IntPtr @internal;
		}

		// Token: 0x0200020A RID: 522
		public enum SDL_DisplayOrientation
		{
			// Token: 0x04000E80 RID: 3712
			SDL_ORIENTATION_UNKNOWN,
			// Token: 0x04000E81 RID: 3713
			SDL_ORIENTATION_LANDSCAPE,
			// Token: 0x04000E82 RID: 3714
			SDL_ORIENTATION_LANDSCAPE_FLIPPED,
			// Token: 0x04000E83 RID: 3715
			SDL_ORIENTATION_PORTRAIT,
			// Token: 0x04000E84 RID: 3716
			SDL_ORIENTATION_PORTRAIT_FLIPPED
		}

		// Token: 0x0200020B RID: 523
		[Flags]
		public enum SDL_WindowFlags : ulong
		{
			// Token: 0x04000E86 RID: 3718
			SDL_WINDOW_FULLSCREEN = 1UL,
			// Token: 0x04000E87 RID: 3719
			SDL_WINDOW_OPENGL = 2UL,
			// Token: 0x04000E88 RID: 3720
			SDL_WINDOW_OCCLUDED = 4UL,
			// Token: 0x04000E89 RID: 3721
			SDL_WINDOW_HIDDEN = 8UL,
			// Token: 0x04000E8A RID: 3722
			SDL_WINDOW_BORDERLESS = 16UL,
			// Token: 0x04000E8B RID: 3723
			SDL_WINDOW_RESIZABLE = 32UL,
			// Token: 0x04000E8C RID: 3724
			SDL_WINDOW_MINIMIZED = 64UL,
			// Token: 0x04000E8D RID: 3725
			SDL_WINDOW_MAXIMIZED = 128UL,
			// Token: 0x04000E8E RID: 3726
			SDL_WINDOW_MOUSE_GRABBED = 256UL,
			// Token: 0x04000E8F RID: 3727
			SDL_WINDOW_INPUT_FOCUS = 512UL,
			// Token: 0x04000E90 RID: 3728
			SDL_WINDOW_MOUSE_FOCUS = 1024UL,
			// Token: 0x04000E91 RID: 3729
			SDL_WINDOW_EXTERNAL = 2048UL,
			// Token: 0x04000E92 RID: 3730
			SDL_WINDOW_MODAL = 4096UL,
			// Token: 0x04000E93 RID: 3731
			SDL_WINDOW_HIGH_PIXEL_DENSITY = 8192UL,
			// Token: 0x04000E94 RID: 3732
			SDL_WINDOW_MOUSE_CAPTURE = 16384UL,
			// Token: 0x04000E95 RID: 3733
			SDL_WINDOW_MOUSE_RELATIVE_MODE = 32768UL,
			// Token: 0x04000E96 RID: 3734
			SDL_WINDOW_ALWAYS_ON_TOP = 65536UL,
			// Token: 0x04000E97 RID: 3735
			SDL_WINDOW_UTILITY = 131072UL,
			// Token: 0x04000E98 RID: 3736
			SDL_WINDOW_TOOLTIP = 262144UL,
			// Token: 0x04000E99 RID: 3737
			SDL_WINDOW_POPUP_MENU = 524288UL,
			// Token: 0x04000E9A RID: 3738
			SDL_WINDOW_KEYBOARD_GRABBED = 1048576UL,
			// Token: 0x04000E9B RID: 3739
			SDL_WINDOW_VULKAN = 268435456UL,
			// Token: 0x04000E9C RID: 3740
			SDL_WINDOW_METAL = 536870912UL,
			// Token: 0x04000E9D RID: 3741
			SDL_WINDOW_TRANSPARENT = 1073741824UL,
			// Token: 0x04000E9E RID: 3742
			SDL_WINDOW_NOT_FOCUSABLE = 2147483648UL
		}

		// Token: 0x0200020C RID: 524
		public enum SDL_FlashOperation
		{
			// Token: 0x04000EA0 RID: 3744
			SDL_FLASH_CANCEL,
			// Token: 0x04000EA1 RID: 3745
			SDL_FLASH_BRIEFLY,
			// Token: 0x04000EA2 RID: 3746
			SDL_FLASH_UNTIL_FOCUSED
		}

		// Token: 0x0200020D RID: 525
		public enum SDL_ProgressState
		{
			// Token: 0x04000EA4 RID: 3748
			SDL_PROGRESS_STATE_INVALID = -1,
			// Token: 0x04000EA5 RID: 3749
			SDL_PROGRESS_STATE_NONE,
			// Token: 0x04000EA6 RID: 3750
			SDL_PROGRESS_STATE_INDETERMINATE,
			// Token: 0x04000EA7 RID: 3751
			SDL_PROGRESS_STATE_NORMAL,
			// Token: 0x04000EA8 RID: 3752
			SDL_PROGRESS_STATE_PAUSED,
			// Token: 0x04000EA9 RID: 3753
			SDL_PROGRESS_STATE_ERROR
		}

		// Token: 0x0200020E RID: 526
		// (Invoke) Token: 0x06001950 RID: 6480
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDL_EGLAttribArrayCallback();

		// Token: 0x0200020F RID: 527
		// (Invoke) Token: 0x06001954 RID: 6484
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr SDL_EGLIntArrayCallback();

		// Token: 0x02000210 RID: 528
		public enum SDL_GLAttr
		{
			// Token: 0x04000EAB RID: 3755
			SDL_GL_RED_SIZE,
			// Token: 0x04000EAC RID: 3756
			SDL_GL_GREEN_SIZE,
			// Token: 0x04000EAD RID: 3757
			SDL_GL_BLUE_SIZE,
			// Token: 0x04000EAE RID: 3758
			SDL_GL_ALPHA_SIZE,
			// Token: 0x04000EAF RID: 3759
			SDL_GL_BUFFER_SIZE,
			// Token: 0x04000EB0 RID: 3760
			SDL_GL_DOUBLEBUFFER,
			// Token: 0x04000EB1 RID: 3761
			SDL_GL_DEPTH_SIZE,
			// Token: 0x04000EB2 RID: 3762
			SDL_GL_STENCIL_SIZE,
			// Token: 0x04000EB3 RID: 3763
			SDL_GL_ACCUM_RED_SIZE,
			// Token: 0x04000EB4 RID: 3764
			SDL_GL_ACCUM_GREEN_SIZE,
			// Token: 0x04000EB5 RID: 3765
			SDL_GL_ACCUM_BLUE_SIZE,
			// Token: 0x04000EB6 RID: 3766
			SDL_GL_ACCUM_ALPHA_SIZE,
			// Token: 0x04000EB7 RID: 3767
			SDL_GL_STEREO,
			// Token: 0x04000EB8 RID: 3768
			SDL_GL_MULTISAMPLEBUFFERS,
			// Token: 0x04000EB9 RID: 3769
			SDL_GL_MULTISAMPLESAMPLES,
			// Token: 0x04000EBA RID: 3770
			SDL_GL_ACCELERATED_VISUAL,
			// Token: 0x04000EBB RID: 3771
			SDL_GL_RETAINED_BACKING,
			// Token: 0x04000EBC RID: 3772
			SDL_GL_CONTEXT_MAJOR_VERSION,
			// Token: 0x04000EBD RID: 3773
			SDL_GL_CONTEXT_MINOR_VERSION,
			// Token: 0x04000EBE RID: 3774
			SDL_GL_CONTEXT_FLAGS,
			// Token: 0x04000EBF RID: 3775
			SDL_GL_CONTEXT_PROFILE_MASK,
			// Token: 0x04000EC0 RID: 3776
			SDL_GL_SHARE_WITH_CURRENT_CONTEXT,
			// Token: 0x04000EC1 RID: 3777
			SDL_GL_FRAMEBUFFER_SRGB_CAPABLE,
			// Token: 0x04000EC2 RID: 3778
			SDL_GL_CONTEXT_RELEASE_BEHAVIOR,
			// Token: 0x04000EC3 RID: 3779
			SDL_GL_CONTEXT_RESET_NOTIFICATION,
			// Token: 0x04000EC4 RID: 3780
			SDL_GL_CONTEXT_NO_ERROR,
			// Token: 0x04000EC5 RID: 3781
			SDL_GL_FLOATBUFFERS,
			// Token: 0x04000EC6 RID: 3782
			SDL_GL_EGL_PLATFORM
		}

		// Token: 0x02000211 RID: 529
		public enum SDL_HitTestResult
		{
			// Token: 0x04000EC8 RID: 3784
			SDL_HITTEST_NORMAL,
			// Token: 0x04000EC9 RID: 3785
			SDL_HITTEST_DRAGGABLE,
			// Token: 0x04000ECA RID: 3786
			SDL_HITTEST_RESIZE_TOPLEFT,
			// Token: 0x04000ECB RID: 3787
			SDL_HITTEST_RESIZE_TOP,
			// Token: 0x04000ECC RID: 3788
			SDL_HITTEST_RESIZE_TOPRIGHT,
			// Token: 0x04000ECD RID: 3789
			SDL_HITTEST_RESIZE_RIGHT,
			// Token: 0x04000ECE RID: 3790
			SDL_HITTEST_RESIZE_BOTTOMRIGHT,
			// Token: 0x04000ECF RID: 3791
			SDL_HITTEST_RESIZE_BOTTOM,
			// Token: 0x04000ED0 RID: 3792
			SDL_HITTEST_RESIZE_BOTTOMLEFT,
			// Token: 0x04000ED1 RID: 3793
			SDL_HITTEST_RESIZE_LEFT
		}

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x06001958 RID: 6488
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate SDL.SDL_HitTestResult SDL_HitTest(IntPtr win, SDL.SDL_Point* area, IntPtr data);

		// Token: 0x02000213 RID: 531
		public struct SDL_DialogFileFilter
		{
			// Token: 0x04000ED2 RID: 3794
			public unsafe byte* name;

			// Token: 0x04000ED3 RID: 3795
			public unsafe byte* pattern;
		}

		// Token: 0x02000214 RID: 532
		// (Invoke) Token: 0x0600195C RID: 6492
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_DialogFileCallback(IntPtr userdata, IntPtr filelist, int filter);

		// Token: 0x02000215 RID: 533
		public enum SDL_FileDialogType
		{
			// Token: 0x04000ED5 RID: 3797
			SDL_FILEDIALOG_OPENFILE,
			// Token: 0x04000ED6 RID: 3798
			SDL_FILEDIALOG_SAVEFILE,
			// Token: 0x04000ED7 RID: 3799
			SDL_FILEDIALOG_OPENFOLDER
		}

		// Token: 0x02000216 RID: 534
		public struct SDL_GUID
		{
			// Token: 0x04000ED8 RID: 3800
			[FixedBuffer(typeof(byte), 16)]
			public SDL.SDL_GUID.<data>e__FixedBuffer data;

			// Token: 0x020003F8 RID: 1016
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 16)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04001E28 RID: 7720
				public byte FixedElementField;
			}
		}

		// Token: 0x02000217 RID: 535
		public enum SDL_PowerState
		{
			// Token: 0x04000EDA RID: 3802
			SDL_POWERSTATE_ERROR = -1,
			// Token: 0x04000EDB RID: 3803
			SDL_POWERSTATE_UNKNOWN,
			// Token: 0x04000EDC RID: 3804
			SDL_POWERSTATE_ON_BATTERY,
			// Token: 0x04000EDD RID: 3805
			SDL_POWERSTATE_NO_BATTERY,
			// Token: 0x04000EDE RID: 3806
			SDL_POWERSTATE_CHARGING,
			// Token: 0x04000EDF RID: 3807
			SDL_POWERSTATE_CHARGED
		}

		// Token: 0x02000218 RID: 536
		public enum SDL_SensorType
		{
			// Token: 0x04000EE1 RID: 3809
			SDL_SENSOR_INVALID = -1,
			// Token: 0x04000EE2 RID: 3810
			SDL_SENSOR_UNKNOWN,
			// Token: 0x04000EE3 RID: 3811
			SDL_SENSOR_ACCEL,
			// Token: 0x04000EE4 RID: 3812
			SDL_SENSOR_GYRO,
			// Token: 0x04000EE5 RID: 3813
			SDL_SENSOR_ACCEL_L,
			// Token: 0x04000EE6 RID: 3814
			SDL_SENSOR_GYRO_L,
			// Token: 0x04000EE7 RID: 3815
			SDL_SENSOR_ACCEL_R,
			// Token: 0x04000EE8 RID: 3816
			SDL_SENSOR_GYRO_R,
			// Token: 0x04000EE9 RID: 3817
			SDL_SENSOR_COUNT
		}

		// Token: 0x02000219 RID: 537
		public enum SDL_JoystickType
		{
			// Token: 0x04000EEB RID: 3819
			SDL_JOYSTICK_TYPE_UNKNOWN,
			// Token: 0x04000EEC RID: 3820
			SDL_JOYSTICK_TYPE_GAMEPAD,
			// Token: 0x04000EED RID: 3821
			SDL_JOYSTICK_TYPE_WHEEL,
			// Token: 0x04000EEE RID: 3822
			SDL_JOYSTICK_TYPE_ARCADE_STICK,
			// Token: 0x04000EEF RID: 3823
			SDL_JOYSTICK_TYPE_FLIGHT_STICK,
			// Token: 0x04000EF0 RID: 3824
			SDL_JOYSTICK_TYPE_DANCE_PAD,
			// Token: 0x04000EF1 RID: 3825
			SDL_JOYSTICK_TYPE_GUITAR,
			// Token: 0x04000EF2 RID: 3826
			SDL_JOYSTICK_TYPE_DRUM_KIT,
			// Token: 0x04000EF3 RID: 3827
			SDL_JOYSTICK_TYPE_ARCADE_PAD,
			// Token: 0x04000EF4 RID: 3828
			SDL_JOYSTICK_TYPE_THROTTLE,
			// Token: 0x04000EF5 RID: 3829
			SDL_JOYSTICK_TYPE_COUNT
		}

		// Token: 0x0200021A RID: 538
		public enum SDL_JoystickConnectionState
		{
			// Token: 0x04000EF7 RID: 3831
			SDL_JOYSTICK_CONNECTION_INVALID = -1,
			// Token: 0x04000EF8 RID: 3832
			SDL_JOYSTICK_CONNECTION_UNKNOWN,
			// Token: 0x04000EF9 RID: 3833
			SDL_JOYSTICK_CONNECTION_WIRED,
			// Token: 0x04000EFA RID: 3834
			SDL_JOYSTICK_CONNECTION_WIRELESS
		}

		// Token: 0x0200021B RID: 539
		public struct SDL_VirtualJoystickTouchpadDesc
		{
			// Token: 0x04000EFB RID: 3835
			public ushort nfingers;

			// Token: 0x04000EFC RID: 3836
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_VirtualJoystickTouchpadDesc.<padding>e__FixedBuffer padding;

			// Token: 0x020003F9 RID: 1017
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <padding>e__FixedBuffer
			{
				// Token: 0x04001E29 RID: 7721
				public ushort FixedElementField;
			}
		}

		// Token: 0x0200021C RID: 540
		public struct SDL_VirtualJoystickSensorDesc
		{
			// Token: 0x04000EFD RID: 3837
			public SDL.SDL_SensorType type;

			// Token: 0x04000EFE RID: 3838
			public float rate;
		}

		// Token: 0x0200021D RID: 541
		public struct SDL_VirtualJoystickDesc
		{
			// Token: 0x04000EFF RID: 3839
			public uint version;

			// Token: 0x04000F00 RID: 3840
			public ushort type;

			// Token: 0x04000F01 RID: 3841
			public ushort padding;

			// Token: 0x04000F02 RID: 3842
			public ushort vendor_id;

			// Token: 0x04000F03 RID: 3843
			public ushort product_id;

			// Token: 0x04000F04 RID: 3844
			public ushort naxes;

			// Token: 0x04000F05 RID: 3845
			public ushort nbuttons;

			// Token: 0x04000F06 RID: 3846
			public ushort nballs;

			// Token: 0x04000F07 RID: 3847
			public ushort nhats;

			// Token: 0x04000F08 RID: 3848
			public ushort ntouchpads;

			// Token: 0x04000F09 RID: 3849
			public ushort nsensors;

			// Token: 0x04000F0A RID: 3850
			[FixedBuffer(typeof(ushort), 2)]
			public SDL.SDL_VirtualJoystickDesc.<padding2>e__FixedBuffer padding2;

			// Token: 0x04000F0B RID: 3851
			public uint button_mask;

			// Token: 0x04000F0C RID: 3852
			public uint axis_mask;

			// Token: 0x04000F0D RID: 3853
			public unsafe byte* name;

			// Token: 0x04000F0E RID: 3854
			public unsafe SDL.SDL_VirtualJoystickTouchpadDesc* touchpads;

			// Token: 0x04000F0F RID: 3855
			public unsafe SDL.SDL_VirtualJoystickSensorDesc* sensors;

			// Token: 0x04000F10 RID: 3856
			public IntPtr userdata;

			// Token: 0x04000F11 RID: 3857
			public IntPtr Update;

			// Token: 0x04000F12 RID: 3858
			public IntPtr SetPlayerIndex;

			// Token: 0x04000F13 RID: 3859
			public IntPtr Rumble;

			// Token: 0x04000F14 RID: 3860
			public IntPtr RumbleTriggers;

			// Token: 0x04000F15 RID: 3861
			public IntPtr SetLED;

			// Token: 0x04000F16 RID: 3862
			public IntPtr SendEffect;

			// Token: 0x04000F17 RID: 3863
			public IntPtr SetSensorsEnabled;

			// Token: 0x04000F18 RID: 3864
			public IntPtr Cleanup;

			// Token: 0x020003FA RID: 1018
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 4)]
			public struct <padding2>e__FixedBuffer
			{
				// Token: 0x04001E2A RID: 7722
				public ushort FixedElementField;
			}
		}

		// Token: 0x0200021E RID: 542
		public enum SDL_GamepadType
		{
			// Token: 0x04000F1A RID: 3866
			SDL_GAMEPAD_TYPE_UNKNOWN,
			// Token: 0x04000F1B RID: 3867
			SDL_GAMEPAD_TYPE_STANDARD,
			// Token: 0x04000F1C RID: 3868
			SDL_GAMEPAD_TYPE_XBOX360,
			// Token: 0x04000F1D RID: 3869
			SDL_GAMEPAD_TYPE_XBOXONE,
			// Token: 0x04000F1E RID: 3870
			SDL_GAMEPAD_TYPE_PS3,
			// Token: 0x04000F1F RID: 3871
			SDL_GAMEPAD_TYPE_PS4,
			// Token: 0x04000F20 RID: 3872
			SDL_GAMEPAD_TYPE_PS5,
			// Token: 0x04000F21 RID: 3873
			SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_PRO,
			// Token: 0x04000F22 RID: 3874
			SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_LEFT,
			// Token: 0x04000F23 RID: 3875
			SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_RIGHT,
			// Token: 0x04000F24 RID: 3876
			SDL_GAMEPAD_TYPE_NINTENDO_SWITCH_JOYCON_PAIR,
			// Token: 0x04000F25 RID: 3877
			SDL_GAMEPAD_TYPE_GAMECUBE,
			// Token: 0x04000F26 RID: 3878
			SDL_GAMEPAD_TYPE_COUNT
		}

		// Token: 0x0200021F RID: 543
		public enum SDL_GamepadButton
		{
			// Token: 0x04000F28 RID: 3880
			SDL_GAMEPAD_BUTTON_INVALID = -1,
			// Token: 0x04000F29 RID: 3881
			SDL_GAMEPAD_BUTTON_SOUTH,
			// Token: 0x04000F2A RID: 3882
			SDL_GAMEPAD_BUTTON_EAST,
			// Token: 0x04000F2B RID: 3883
			SDL_GAMEPAD_BUTTON_WEST,
			// Token: 0x04000F2C RID: 3884
			SDL_GAMEPAD_BUTTON_NORTH,
			// Token: 0x04000F2D RID: 3885
			SDL_GAMEPAD_BUTTON_BACK,
			// Token: 0x04000F2E RID: 3886
			SDL_GAMEPAD_BUTTON_GUIDE,
			// Token: 0x04000F2F RID: 3887
			SDL_GAMEPAD_BUTTON_START,
			// Token: 0x04000F30 RID: 3888
			SDL_GAMEPAD_BUTTON_LEFT_STICK,
			// Token: 0x04000F31 RID: 3889
			SDL_GAMEPAD_BUTTON_RIGHT_STICK,
			// Token: 0x04000F32 RID: 3890
			SDL_GAMEPAD_BUTTON_LEFT_SHOULDER,
			// Token: 0x04000F33 RID: 3891
			SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER,
			// Token: 0x04000F34 RID: 3892
			SDL_GAMEPAD_BUTTON_DPAD_UP,
			// Token: 0x04000F35 RID: 3893
			SDL_GAMEPAD_BUTTON_DPAD_DOWN,
			// Token: 0x04000F36 RID: 3894
			SDL_GAMEPAD_BUTTON_DPAD_LEFT,
			// Token: 0x04000F37 RID: 3895
			SDL_GAMEPAD_BUTTON_DPAD_RIGHT,
			// Token: 0x04000F38 RID: 3896
			SDL_GAMEPAD_BUTTON_MISC1,
			// Token: 0x04000F39 RID: 3897
			SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1,
			// Token: 0x04000F3A RID: 3898
			SDL_GAMEPAD_BUTTON_LEFT_PADDLE1,
			// Token: 0x04000F3B RID: 3899
			SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2,
			// Token: 0x04000F3C RID: 3900
			SDL_GAMEPAD_BUTTON_LEFT_PADDLE2,
			// Token: 0x04000F3D RID: 3901
			SDL_GAMEPAD_BUTTON_TOUCHPAD,
			// Token: 0x04000F3E RID: 3902
			SDL_GAMEPAD_BUTTON_MISC2,
			// Token: 0x04000F3F RID: 3903
			SDL_GAMEPAD_BUTTON_MISC3,
			// Token: 0x04000F40 RID: 3904
			SDL_GAMEPAD_BUTTON_MISC4,
			// Token: 0x04000F41 RID: 3905
			SDL_GAMEPAD_BUTTON_MISC5,
			// Token: 0x04000F42 RID: 3906
			SDL_GAMEPAD_BUTTON_MISC6,
			// Token: 0x04000F43 RID: 3907
			SDL_GAMEPAD_BUTTON_COUNT
		}

		// Token: 0x02000220 RID: 544
		public enum SDL_GamepadButtonLabel
		{
			// Token: 0x04000F45 RID: 3909
			SDL_GAMEPAD_BUTTON_LABEL_UNKNOWN,
			// Token: 0x04000F46 RID: 3910
			SDL_GAMEPAD_BUTTON_LABEL_A,
			// Token: 0x04000F47 RID: 3911
			SDL_GAMEPAD_BUTTON_LABEL_B,
			// Token: 0x04000F48 RID: 3912
			SDL_GAMEPAD_BUTTON_LABEL_X,
			// Token: 0x04000F49 RID: 3913
			SDL_GAMEPAD_BUTTON_LABEL_Y,
			// Token: 0x04000F4A RID: 3914
			SDL_GAMEPAD_BUTTON_LABEL_CROSS,
			// Token: 0x04000F4B RID: 3915
			SDL_GAMEPAD_BUTTON_LABEL_CIRCLE,
			// Token: 0x04000F4C RID: 3916
			SDL_GAMEPAD_BUTTON_LABEL_SQUARE,
			// Token: 0x04000F4D RID: 3917
			SDL_GAMEPAD_BUTTON_LABEL_TRIANGLE
		}

		// Token: 0x02000221 RID: 545
		public enum SDL_GamepadAxis
		{
			// Token: 0x04000F4F RID: 3919
			SDL_GAMEPAD_AXIS_INVALID = -1,
			// Token: 0x04000F50 RID: 3920
			SDL_GAMEPAD_AXIS_LEFTX,
			// Token: 0x04000F51 RID: 3921
			SDL_GAMEPAD_AXIS_LEFTY,
			// Token: 0x04000F52 RID: 3922
			SDL_GAMEPAD_AXIS_RIGHTX,
			// Token: 0x04000F53 RID: 3923
			SDL_GAMEPAD_AXIS_RIGHTY,
			// Token: 0x04000F54 RID: 3924
			SDL_GAMEPAD_AXIS_LEFT_TRIGGER,
			// Token: 0x04000F55 RID: 3925
			SDL_GAMEPAD_AXIS_RIGHT_TRIGGER,
			// Token: 0x04000F56 RID: 3926
			SDL_GAMEPAD_AXIS_COUNT
		}

		// Token: 0x02000222 RID: 546
		public enum SDL_GamepadBindingType
		{
			// Token: 0x04000F58 RID: 3928
			SDL_GAMEPAD_BINDTYPE_NONE,
			// Token: 0x04000F59 RID: 3929
			SDL_GAMEPAD_BINDTYPE_BUTTON,
			// Token: 0x04000F5A RID: 3930
			SDL_GAMEPAD_BINDTYPE_AXIS,
			// Token: 0x04000F5B RID: 3931
			SDL_GAMEPAD_BINDTYPE_HAT
		}

		// Token: 0x02000223 RID: 547
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_GamepadBinding
		{
			// Token: 0x04000F5C RID: 3932
			[FieldOffset(0)]
			public SDL.SDL_GamepadBindingType input_type;

			// Token: 0x04000F5D RID: 3933
			[FieldOffset(4)]
			public int input_button;

			// Token: 0x04000F5E RID: 3934
			[FieldOffset(4)]
			public SDL.INTERNAL_SDL_GamepadBinding_input_axis input_axis;

			// Token: 0x04000F5F RID: 3935
			[FieldOffset(4)]
			public SDL.INTERNAL_SDL_GamepadBinding_input_hat input_hat;

			// Token: 0x04000F60 RID: 3936
			[FieldOffset(16)]
			public SDL.SDL_GamepadBindingType output_type;

			// Token: 0x04000F61 RID: 3937
			[FieldOffset(20)]
			public SDL.SDL_GamepadButton output_button;

			// Token: 0x04000F62 RID: 3938
			[FieldOffset(20)]
			public SDL.INTERNAL_SDL_GamepadBinding_output_axis output_axis;
		}

		// Token: 0x02000224 RID: 548
		public struct INTERNAL_SDL_GamepadBinding_input_axis
		{
			// Token: 0x04000F63 RID: 3939
			public int axis;

			// Token: 0x04000F64 RID: 3940
			public int axis_min;

			// Token: 0x04000F65 RID: 3941
			public int axis_max;
		}

		// Token: 0x02000225 RID: 549
		public struct INTERNAL_SDL_GamepadBinding_input_hat
		{
			// Token: 0x04000F66 RID: 3942
			public int hat;

			// Token: 0x04000F67 RID: 3943
			public int hat_mask;
		}

		// Token: 0x02000226 RID: 550
		public struct INTERNAL_SDL_GamepadBinding_output_axis
		{
			// Token: 0x04000F68 RID: 3944
			public SDL.SDL_GamepadAxis axis;

			// Token: 0x04000F69 RID: 3945
			public int axis_min;

			// Token: 0x04000F6A RID: 3946
			public int axis_max;
		}

		// Token: 0x02000227 RID: 551
		public enum SDL_Scancode
		{
			// Token: 0x04000F6C RID: 3948
			SDL_SCANCODE_UNKNOWN,
			// Token: 0x04000F6D RID: 3949
			SDL_SCANCODE_A = 4,
			// Token: 0x04000F6E RID: 3950
			SDL_SCANCODE_B,
			// Token: 0x04000F6F RID: 3951
			SDL_SCANCODE_C,
			// Token: 0x04000F70 RID: 3952
			SDL_SCANCODE_D,
			// Token: 0x04000F71 RID: 3953
			SDL_SCANCODE_E,
			// Token: 0x04000F72 RID: 3954
			SDL_SCANCODE_F,
			// Token: 0x04000F73 RID: 3955
			SDL_SCANCODE_G,
			// Token: 0x04000F74 RID: 3956
			SDL_SCANCODE_H,
			// Token: 0x04000F75 RID: 3957
			SDL_SCANCODE_I,
			// Token: 0x04000F76 RID: 3958
			SDL_SCANCODE_J,
			// Token: 0x04000F77 RID: 3959
			SDL_SCANCODE_K,
			// Token: 0x04000F78 RID: 3960
			SDL_SCANCODE_L,
			// Token: 0x04000F79 RID: 3961
			SDL_SCANCODE_M,
			// Token: 0x04000F7A RID: 3962
			SDL_SCANCODE_N,
			// Token: 0x04000F7B RID: 3963
			SDL_SCANCODE_O,
			// Token: 0x04000F7C RID: 3964
			SDL_SCANCODE_P,
			// Token: 0x04000F7D RID: 3965
			SDL_SCANCODE_Q,
			// Token: 0x04000F7E RID: 3966
			SDL_SCANCODE_R,
			// Token: 0x04000F7F RID: 3967
			SDL_SCANCODE_S,
			// Token: 0x04000F80 RID: 3968
			SDL_SCANCODE_T,
			// Token: 0x04000F81 RID: 3969
			SDL_SCANCODE_U,
			// Token: 0x04000F82 RID: 3970
			SDL_SCANCODE_V,
			// Token: 0x04000F83 RID: 3971
			SDL_SCANCODE_W,
			// Token: 0x04000F84 RID: 3972
			SDL_SCANCODE_X,
			// Token: 0x04000F85 RID: 3973
			SDL_SCANCODE_Y,
			// Token: 0x04000F86 RID: 3974
			SDL_SCANCODE_Z,
			// Token: 0x04000F87 RID: 3975
			SDL_SCANCODE_1,
			// Token: 0x04000F88 RID: 3976
			SDL_SCANCODE_2,
			// Token: 0x04000F89 RID: 3977
			SDL_SCANCODE_3,
			// Token: 0x04000F8A RID: 3978
			SDL_SCANCODE_4,
			// Token: 0x04000F8B RID: 3979
			SDL_SCANCODE_5,
			// Token: 0x04000F8C RID: 3980
			SDL_SCANCODE_6,
			// Token: 0x04000F8D RID: 3981
			SDL_SCANCODE_7,
			// Token: 0x04000F8E RID: 3982
			SDL_SCANCODE_8,
			// Token: 0x04000F8F RID: 3983
			SDL_SCANCODE_9,
			// Token: 0x04000F90 RID: 3984
			SDL_SCANCODE_0,
			// Token: 0x04000F91 RID: 3985
			SDL_SCANCODE_RETURN,
			// Token: 0x04000F92 RID: 3986
			SDL_SCANCODE_ESCAPE,
			// Token: 0x04000F93 RID: 3987
			SDL_SCANCODE_BACKSPACE,
			// Token: 0x04000F94 RID: 3988
			SDL_SCANCODE_TAB,
			// Token: 0x04000F95 RID: 3989
			SDL_SCANCODE_SPACE,
			// Token: 0x04000F96 RID: 3990
			SDL_SCANCODE_MINUS,
			// Token: 0x04000F97 RID: 3991
			SDL_SCANCODE_EQUALS,
			// Token: 0x04000F98 RID: 3992
			SDL_SCANCODE_LEFTBRACKET,
			// Token: 0x04000F99 RID: 3993
			SDL_SCANCODE_RIGHTBRACKET,
			// Token: 0x04000F9A RID: 3994
			SDL_SCANCODE_BACKSLASH,
			// Token: 0x04000F9B RID: 3995
			SDL_SCANCODE_NONUSHASH,
			// Token: 0x04000F9C RID: 3996
			SDL_SCANCODE_SEMICOLON,
			// Token: 0x04000F9D RID: 3997
			SDL_SCANCODE_APOSTROPHE,
			// Token: 0x04000F9E RID: 3998
			SDL_SCANCODE_GRAVE,
			// Token: 0x04000F9F RID: 3999
			SDL_SCANCODE_COMMA,
			// Token: 0x04000FA0 RID: 4000
			SDL_SCANCODE_PERIOD,
			// Token: 0x04000FA1 RID: 4001
			SDL_SCANCODE_SLASH,
			// Token: 0x04000FA2 RID: 4002
			SDL_SCANCODE_CAPSLOCK,
			// Token: 0x04000FA3 RID: 4003
			SDL_SCANCODE_F1,
			// Token: 0x04000FA4 RID: 4004
			SDL_SCANCODE_F2,
			// Token: 0x04000FA5 RID: 4005
			SDL_SCANCODE_F3,
			// Token: 0x04000FA6 RID: 4006
			SDL_SCANCODE_F4,
			// Token: 0x04000FA7 RID: 4007
			SDL_SCANCODE_F5,
			// Token: 0x04000FA8 RID: 4008
			SDL_SCANCODE_F6,
			// Token: 0x04000FA9 RID: 4009
			SDL_SCANCODE_F7,
			// Token: 0x04000FAA RID: 4010
			SDL_SCANCODE_F8,
			// Token: 0x04000FAB RID: 4011
			SDL_SCANCODE_F9,
			// Token: 0x04000FAC RID: 4012
			SDL_SCANCODE_F10,
			// Token: 0x04000FAD RID: 4013
			SDL_SCANCODE_F11,
			// Token: 0x04000FAE RID: 4014
			SDL_SCANCODE_F12,
			// Token: 0x04000FAF RID: 4015
			SDL_SCANCODE_PRINTSCREEN,
			// Token: 0x04000FB0 RID: 4016
			SDL_SCANCODE_SCROLLLOCK,
			// Token: 0x04000FB1 RID: 4017
			SDL_SCANCODE_PAUSE,
			// Token: 0x04000FB2 RID: 4018
			SDL_SCANCODE_INSERT,
			// Token: 0x04000FB3 RID: 4019
			SDL_SCANCODE_HOME,
			// Token: 0x04000FB4 RID: 4020
			SDL_SCANCODE_PAGEUP,
			// Token: 0x04000FB5 RID: 4021
			SDL_SCANCODE_DELETE,
			// Token: 0x04000FB6 RID: 4022
			SDL_SCANCODE_END,
			// Token: 0x04000FB7 RID: 4023
			SDL_SCANCODE_PAGEDOWN,
			// Token: 0x04000FB8 RID: 4024
			SDL_SCANCODE_RIGHT,
			// Token: 0x04000FB9 RID: 4025
			SDL_SCANCODE_LEFT,
			// Token: 0x04000FBA RID: 4026
			SDL_SCANCODE_DOWN,
			// Token: 0x04000FBB RID: 4027
			SDL_SCANCODE_UP,
			// Token: 0x04000FBC RID: 4028
			SDL_SCANCODE_NUMLOCKCLEAR,
			// Token: 0x04000FBD RID: 4029
			SDL_SCANCODE_KP_DIVIDE,
			// Token: 0x04000FBE RID: 4030
			SDL_SCANCODE_KP_MULTIPLY,
			// Token: 0x04000FBF RID: 4031
			SDL_SCANCODE_KP_MINUS,
			// Token: 0x04000FC0 RID: 4032
			SDL_SCANCODE_KP_PLUS,
			// Token: 0x04000FC1 RID: 4033
			SDL_SCANCODE_KP_ENTER,
			// Token: 0x04000FC2 RID: 4034
			SDL_SCANCODE_KP_1,
			// Token: 0x04000FC3 RID: 4035
			SDL_SCANCODE_KP_2,
			// Token: 0x04000FC4 RID: 4036
			SDL_SCANCODE_KP_3,
			// Token: 0x04000FC5 RID: 4037
			SDL_SCANCODE_KP_4,
			// Token: 0x04000FC6 RID: 4038
			SDL_SCANCODE_KP_5,
			// Token: 0x04000FC7 RID: 4039
			SDL_SCANCODE_KP_6,
			// Token: 0x04000FC8 RID: 4040
			SDL_SCANCODE_KP_7,
			// Token: 0x04000FC9 RID: 4041
			SDL_SCANCODE_KP_8,
			// Token: 0x04000FCA RID: 4042
			SDL_SCANCODE_KP_9,
			// Token: 0x04000FCB RID: 4043
			SDL_SCANCODE_KP_0,
			// Token: 0x04000FCC RID: 4044
			SDL_SCANCODE_KP_PERIOD,
			// Token: 0x04000FCD RID: 4045
			SDL_SCANCODE_NONUSBACKSLASH,
			// Token: 0x04000FCE RID: 4046
			SDL_SCANCODE_APPLICATION,
			// Token: 0x04000FCF RID: 4047
			SDL_SCANCODE_POWER,
			// Token: 0x04000FD0 RID: 4048
			SDL_SCANCODE_KP_EQUALS,
			// Token: 0x04000FD1 RID: 4049
			SDL_SCANCODE_F13,
			// Token: 0x04000FD2 RID: 4050
			SDL_SCANCODE_F14,
			// Token: 0x04000FD3 RID: 4051
			SDL_SCANCODE_F15,
			// Token: 0x04000FD4 RID: 4052
			SDL_SCANCODE_F16,
			// Token: 0x04000FD5 RID: 4053
			SDL_SCANCODE_F17,
			// Token: 0x04000FD6 RID: 4054
			SDL_SCANCODE_F18,
			// Token: 0x04000FD7 RID: 4055
			SDL_SCANCODE_F19,
			// Token: 0x04000FD8 RID: 4056
			SDL_SCANCODE_F20,
			// Token: 0x04000FD9 RID: 4057
			SDL_SCANCODE_F21,
			// Token: 0x04000FDA RID: 4058
			SDL_SCANCODE_F22,
			// Token: 0x04000FDB RID: 4059
			SDL_SCANCODE_F23,
			// Token: 0x04000FDC RID: 4060
			SDL_SCANCODE_F24,
			// Token: 0x04000FDD RID: 4061
			SDL_SCANCODE_EXECUTE,
			// Token: 0x04000FDE RID: 4062
			SDL_SCANCODE_HELP,
			// Token: 0x04000FDF RID: 4063
			SDL_SCANCODE_MENU,
			// Token: 0x04000FE0 RID: 4064
			SDL_SCANCODE_SELECT,
			// Token: 0x04000FE1 RID: 4065
			SDL_SCANCODE_STOP,
			// Token: 0x04000FE2 RID: 4066
			SDL_SCANCODE_AGAIN,
			// Token: 0x04000FE3 RID: 4067
			SDL_SCANCODE_UNDO,
			// Token: 0x04000FE4 RID: 4068
			SDL_SCANCODE_CUT,
			// Token: 0x04000FE5 RID: 4069
			SDL_SCANCODE_COPY,
			// Token: 0x04000FE6 RID: 4070
			SDL_SCANCODE_PASTE,
			// Token: 0x04000FE7 RID: 4071
			SDL_SCANCODE_FIND,
			// Token: 0x04000FE8 RID: 4072
			SDL_SCANCODE_MUTE,
			// Token: 0x04000FE9 RID: 4073
			SDL_SCANCODE_VOLUMEUP,
			// Token: 0x04000FEA RID: 4074
			SDL_SCANCODE_VOLUMEDOWN,
			// Token: 0x04000FEB RID: 4075
			SDL_SCANCODE_KP_COMMA = 133,
			// Token: 0x04000FEC RID: 4076
			SDL_SCANCODE_KP_EQUALSAS400,
			// Token: 0x04000FED RID: 4077
			SDL_SCANCODE_INTERNATIONAL1,
			// Token: 0x04000FEE RID: 4078
			SDL_SCANCODE_INTERNATIONAL2,
			// Token: 0x04000FEF RID: 4079
			SDL_SCANCODE_INTERNATIONAL3,
			// Token: 0x04000FF0 RID: 4080
			SDL_SCANCODE_INTERNATIONAL4,
			// Token: 0x04000FF1 RID: 4081
			SDL_SCANCODE_INTERNATIONAL5,
			// Token: 0x04000FF2 RID: 4082
			SDL_SCANCODE_INTERNATIONAL6,
			// Token: 0x04000FF3 RID: 4083
			SDL_SCANCODE_INTERNATIONAL7,
			// Token: 0x04000FF4 RID: 4084
			SDL_SCANCODE_INTERNATIONAL8,
			// Token: 0x04000FF5 RID: 4085
			SDL_SCANCODE_INTERNATIONAL9,
			// Token: 0x04000FF6 RID: 4086
			SDL_SCANCODE_LANG1,
			// Token: 0x04000FF7 RID: 4087
			SDL_SCANCODE_LANG2,
			// Token: 0x04000FF8 RID: 4088
			SDL_SCANCODE_LANG3,
			// Token: 0x04000FF9 RID: 4089
			SDL_SCANCODE_LANG4,
			// Token: 0x04000FFA RID: 4090
			SDL_SCANCODE_LANG5,
			// Token: 0x04000FFB RID: 4091
			SDL_SCANCODE_LANG6,
			// Token: 0x04000FFC RID: 4092
			SDL_SCANCODE_LANG7,
			// Token: 0x04000FFD RID: 4093
			SDL_SCANCODE_LANG8,
			// Token: 0x04000FFE RID: 4094
			SDL_SCANCODE_LANG9,
			// Token: 0x04000FFF RID: 4095
			SDL_SCANCODE_ALTERASE,
			// Token: 0x04001000 RID: 4096
			SDL_SCANCODE_SYSREQ,
			// Token: 0x04001001 RID: 4097
			SDL_SCANCODE_CANCEL,
			// Token: 0x04001002 RID: 4098
			SDL_SCANCODE_CLEAR,
			// Token: 0x04001003 RID: 4099
			SDL_SCANCODE_PRIOR,
			// Token: 0x04001004 RID: 4100
			SDL_SCANCODE_RETURN2,
			// Token: 0x04001005 RID: 4101
			SDL_SCANCODE_SEPARATOR,
			// Token: 0x04001006 RID: 4102
			SDL_SCANCODE_OUT,
			// Token: 0x04001007 RID: 4103
			SDL_SCANCODE_OPER,
			// Token: 0x04001008 RID: 4104
			SDL_SCANCODE_CLEARAGAIN,
			// Token: 0x04001009 RID: 4105
			SDL_SCANCODE_CRSEL,
			// Token: 0x0400100A RID: 4106
			SDL_SCANCODE_EXSEL,
			// Token: 0x0400100B RID: 4107
			SDL_SCANCODE_KP_00 = 176,
			// Token: 0x0400100C RID: 4108
			SDL_SCANCODE_KP_000,
			// Token: 0x0400100D RID: 4109
			SDL_SCANCODE_THOUSANDSSEPARATOR,
			// Token: 0x0400100E RID: 4110
			SDL_SCANCODE_DECIMALSEPARATOR,
			// Token: 0x0400100F RID: 4111
			SDL_SCANCODE_CURRENCYUNIT,
			// Token: 0x04001010 RID: 4112
			SDL_SCANCODE_CURRENCYSUBUNIT,
			// Token: 0x04001011 RID: 4113
			SDL_SCANCODE_KP_LEFTPAREN,
			// Token: 0x04001012 RID: 4114
			SDL_SCANCODE_KP_RIGHTPAREN,
			// Token: 0x04001013 RID: 4115
			SDL_SCANCODE_KP_LEFTBRACE,
			// Token: 0x04001014 RID: 4116
			SDL_SCANCODE_KP_RIGHTBRACE,
			// Token: 0x04001015 RID: 4117
			SDL_SCANCODE_KP_TAB,
			// Token: 0x04001016 RID: 4118
			SDL_SCANCODE_KP_BACKSPACE,
			// Token: 0x04001017 RID: 4119
			SDL_SCANCODE_KP_A,
			// Token: 0x04001018 RID: 4120
			SDL_SCANCODE_KP_B,
			// Token: 0x04001019 RID: 4121
			SDL_SCANCODE_KP_C,
			// Token: 0x0400101A RID: 4122
			SDL_SCANCODE_KP_D,
			// Token: 0x0400101B RID: 4123
			SDL_SCANCODE_KP_E,
			// Token: 0x0400101C RID: 4124
			SDL_SCANCODE_KP_F,
			// Token: 0x0400101D RID: 4125
			SDL_SCANCODE_KP_XOR,
			// Token: 0x0400101E RID: 4126
			SDL_SCANCODE_KP_POWER,
			// Token: 0x0400101F RID: 4127
			SDL_SCANCODE_KP_PERCENT,
			// Token: 0x04001020 RID: 4128
			SDL_SCANCODE_KP_LESS,
			// Token: 0x04001021 RID: 4129
			SDL_SCANCODE_KP_GREATER,
			// Token: 0x04001022 RID: 4130
			SDL_SCANCODE_KP_AMPERSAND,
			// Token: 0x04001023 RID: 4131
			SDL_SCANCODE_KP_DBLAMPERSAND,
			// Token: 0x04001024 RID: 4132
			SDL_SCANCODE_KP_VERTICALBAR,
			// Token: 0x04001025 RID: 4133
			SDL_SCANCODE_KP_DBLVERTICALBAR,
			// Token: 0x04001026 RID: 4134
			SDL_SCANCODE_KP_COLON,
			// Token: 0x04001027 RID: 4135
			SDL_SCANCODE_KP_HASH,
			// Token: 0x04001028 RID: 4136
			SDL_SCANCODE_KP_SPACE,
			// Token: 0x04001029 RID: 4137
			SDL_SCANCODE_KP_AT,
			// Token: 0x0400102A RID: 4138
			SDL_SCANCODE_KP_EXCLAM,
			// Token: 0x0400102B RID: 4139
			SDL_SCANCODE_KP_MEMSTORE,
			// Token: 0x0400102C RID: 4140
			SDL_SCANCODE_KP_MEMRECALL,
			// Token: 0x0400102D RID: 4141
			SDL_SCANCODE_KP_MEMCLEAR,
			// Token: 0x0400102E RID: 4142
			SDL_SCANCODE_KP_MEMADD,
			// Token: 0x0400102F RID: 4143
			SDL_SCANCODE_KP_MEMSUBTRACT,
			// Token: 0x04001030 RID: 4144
			SDL_SCANCODE_KP_MEMMULTIPLY,
			// Token: 0x04001031 RID: 4145
			SDL_SCANCODE_KP_MEMDIVIDE,
			// Token: 0x04001032 RID: 4146
			SDL_SCANCODE_KP_PLUSMINUS,
			// Token: 0x04001033 RID: 4147
			SDL_SCANCODE_KP_CLEAR,
			// Token: 0x04001034 RID: 4148
			SDL_SCANCODE_KP_CLEARENTRY,
			// Token: 0x04001035 RID: 4149
			SDL_SCANCODE_KP_BINARY,
			// Token: 0x04001036 RID: 4150
			SDL_SCANCODE_KP_OCTAL,
			// Token: 0x04001037 RID: 4151
			SDL_SCANCODE_KP_DECIMAL,
			// Token: 0x04001038 RID: 4152
			SDL_SCANCODE_KP_HEXADECIMAL,
			// Token: 0x04001039 RID: 4153
			SDL_SCANCODE_LCTRL = 224,
			// Token: 0x0400103A RID: 4154
			SDL_SCANCODE_LSHIFT,
			// Token: 0x0400103B RID: 4155
			SDL_SCANCODE_LALT,
			// Token: 0x0400103C RID: 4156
			SDL_SCANCODE_LGUI,
			// Token: 0x0400103D RID: 4157
			SDL_SCANCODE_RCTRL,
			// Token: 0x0400103E RID: 4158
			SDL_SCANCODE_RSHIFT,
			// Token: 0x0400103F RID: 4159
			SDL_SCANCODE_RALT,
			// Token: 0x04001040 RID: 4160
			SDL_SCANCODE_RGUI,
			// Token: 0x04001041 RID: 4161
			SDL_SCANCODE_MODE = 257,
			// Token: 0x04001042 RID: 4162
			SDL_SCANCODE_SLEEP,
			// Token: 0x04001043 RID: 4163
			SDL_SCANCODE_WAKE,
			// Token: 0x04001044 RID: 4164
			SDL_SCANCODE_CHANNEL_INCREMENT,
			// Token: 0x04001045 RID: 4165
			SDL_SCANCODE_CHANNEL_DECREMENT,
			// Token: 0x04001046 RID: 4166
			SDL_SCANCODE_MEDIA_PLAY,
			// Token: 0x04001047 RID: 4167
			SDL_SCANCODE_MEDIA_PAUSE,
			// Token: 0x04001048 RID: 4168
			SDL_SCANCODE_MEDIA_RECORD,
			// Token: 0x04001049 RID: 4169
			SDL_SCANCODE_MEDIA_FAST_FORWARD,
			// Token: 0x0400104A RID: 4170
			SDL_SCANCODE_MEDIA_REWIND,
			// Token: 0x0400104B RID: 4171
			SDL_SCANCODE_MEDIA_NEXT_TRACK,
			// Token: 0x0400104C RID: 4172
			SDL_SCANCODE_MEDIA_PREVIOUS_TRACK,
			// Token: 0x0400104D RID: 4173
			SDL_SCANCODE_MEDIA_STOP,
			// Token: 0x0400104E RID: 4174
			SDL_SCANCODE_MEDIA_EJECT,
			// Token: 0x0400104F RID: 4175
			SDL_SCANCODE_MEDIA_PLAY_PAUSE,
			// Token: 0x04001050 RID: 4176
			SDL_SCANCODE_MEDIA_SELECT,
			// Token: 0x04001051 RID: 4177
			SDL_SCANCODE_AC_NEW,
			// Token: 0x04001052 RID: 4178
			SDL_SCANCODE_AC_OPEN,
			// Token: 0x04001053 RID: 4179
			SDL_SCANCODE_AC_CLOSE,
			// Token: 0x04001054 RID: 4180
			SDL_SCANCODE_AC_EXIT,
			// Token: 0x04001055 RID: 4181
			SDL_SCANCODE_AC_SAVE,
			// Token: 0x04001056 RID: 4182
			SDL_SCANCODE_AC_PRINT,
			// Token: 0x04001057 RID: 4183
			SDL_SCANCODE_AC_PROPERTIES,
			// Token: 0x04001058 RID: 4184
			SDL_SCANCODE_AC_SEARCH,
			// Token: 0x04001059 RID: 4185
			SDL_SCANCODE_AC_HOME,
			// Token: 0x0400105A RID: 4186
			SDL_SCANCODE_AC_BACK,
			// Token: 0x0400105B RID: 4187
			SDL_SCANCODE_AC_FORWARD,
			// Token: 0x0400105C RID: 4188
			SDL_SCANCODE_AC_STOP,
			// Token: 0x0400105D RID: 4189
			SDL_SCANCODE_AC_REFRESH,
			// Token: 0x0400105E RID: 4190
			SDL_SCANCODE_AC_BOOKMARKS,
			// Token: 0x0400105F RID: 4191
			SDL_SCANCODE_SOFTLEFT,
			// Token: 0x04001060 RID: 4192
			SDL_SCANCODE_SOFTRIGHT,
			// Token: 0x04001061 RID: 4193
			SDL_SCANCODE_CALL,
			// Token: 0x04001062 RID: 4194
			SDL_SCANCODE_ENDCALL,
			// Token: 0x04001063 RID: 4195
			SDL_SCANCODE_RESERVED = 400,
			// Token: 0x04001064 RID: 4196
			SDL_SCANCODE_COUNT = 512
		}

		// Token: 0x02000228 RID: 552
		public enum SDL_Keycode : uint
		{
			// Token: 0x04001066 RID: 4198
			SDLK_SCANCODE_MASK = 1073741824U,
			// Token: 0x04001067 RID: 4199
			SDLK_UNKNOWN = 0U,
			// Token: 0x04001068 RID: 4200
			SDLK_RETURN = 13U,
			// Token: 0x04001069 RID: 4201
			SDLK_ESCAPE = 27U,
			// Token: 0x0400106A RID: 4202
			SDLK_BACKSPACE = 8U,
			// Token: 0x0400106B RID: 4203
			SDLK_TAB,
			// Token: 0x0400106C RID: 4204
			SDLK_SPACE = 32U,
			// Token: 0x0400106D RID: 4205
			SDLK_EXCLAIM,
			// Token: 0x0400106E RID: 4206
			SDLK_DBLAPOSTROPHE,
			// Token: 0x0400106F RID: 4207
			SDLK_HASH,
			// Token: 0x04001070 RID: 4208
			SDLK_DOLLAR,
			// Token: 0x04001071 RID: 4209
			SDLK_PERCENT,
			// Token: 0x04001072 RID: 4210
			SDLK_AMPERSAND,
			// Token: 0x04001073 RID: 4211
			SDLK_APOSTROPHE,
			// Token: 0x04001074 RID: 4212
			SDLK_LEFTPAREN,
			// Token: 0x04001075 RID: 4213
			SDLK_RIGHTPAREN,
			// Token: 0x04001076 RID: 4214
			SDLK_ASTERISK,
			// Token: 0x04001077 RID: 4215
			SDLK_PLUS,
			// Token: 0x04001078 RID: 4216
			SDLK_COMMA,
			// Token: 0x04001079 RID: 4217
			SDLK_MINUS,
			// Token: 0x0400107A RID: 4218
			SDLK_PERIOD,
			// Token: 0x0400107B RID: 4219
			SDLK_SLASH,
			// Token: 0x0400107C RID: 4220
			SDLK_0,
			// Token: 0x0400107D RID: 4221
			SDLK_1,
			// Token: 0x0400107E RID: 4222
			SDLK_2,
			// Token: 0x0400107F RID: 4223
			SDLK_3,
			// Token: 0x04001080 RID: 4224
			SDLK_4,
			// Token: 0x04001081 RID: 4225
			SDLK_5,
			// Token: 0x04001082 RID: 4226
			SDLK_6,
			// Token: 0x04001083 RID: 4227
			SDLK_7,
			// Token: 0x04001084 RID: 4228
			SDLK_8,
			// Token: 0x04001085 RID: 4229
			SDLK_9,
			// Token: 0x04001086 RID: 4230
			SDLK_COLON,
			// Token: 0x04001087 RID: 4231
			SDLK_SEMICOLON,
			// Token: 0x04001088 RID: 4232
			SDLK_LESS,
			// Token: 0x04001089 RID: 4233
			SDLK_EQUALS,
			// Token: 0x0400108A RID: 4234
			SDLK_GREATER,
			// Token: 0x0400108B RID: 4235
			SDLK_QUESTION,
			// Token: 0x0400108C RID: 4236
			SDLK_AT,
			// Token: 0x0400108D RID: 4237
			SDLK_LEFTBRACKET = 91U,
			// Token: 0x0400108E RID: 4238
			SDLK_BACKSLASH,
			// Token: 0x0400108F RID: 4239
			SDLK_RIGHTBRACKET,
			// Token: 0x04001090 RID: 4240
			SDLK_CARET,
			// Token: 0x04001091 RID: 4241
			SDLK_UNDERSCORE,
			// Token: 0x04001092 RID: 4242
			SDLK_GRAVE,
			// Token: 0x04001093 RID: 4243
			SDLK_A,
			// Token: 0x04001094 RID: 4244
			SDLK_B,
			// Token: 0x04001095 RID: 4245
			SDLK_C,
			// Token: 0x04001096 RID: 4246
			SDLK_D,
			// Token: 0x04001097 RID: 4247
			SDLK_E,
			// Token: 0x04001098 RID: 4248
			SDLK_F,
			// Token: 0x04001099 RID: 4249
			SDLK_G,
			// Token: 0x0400109A RID: 4250
			SDLK_H,
			// Token: 0x0400109B RID: 4251
			SDLK_I,
			// Token: 0x0400109C RID: 4252
			SDLK_J,
			// Token: 0x0400109D RID: 4253
			SDLK_K,
			// Token: 0x0400109E RID: 4254
			SDLK_L,
			// Token: 0x0400109F RID: 4255
			SDLK_M,
			// Token: 0x040010A0 RID: 4256
			SDLK_N,
			// Token: 0x040010A1 RID: 4257
			SDLK_O,
			// Token: 0x040010A2 RID: 4258
			SDLK_P,
			// Token: 0x040010A3 RID: 4259
			SDLK_Q,
			// Token: 0x040010A4 RID: 4260
			SDLK_R,
			// Token: 0x040010A5 RID: 4261
			SDLK_S,
			// Token: 0x040010A6 RID: 4262
			SDLK_T,
			// Token: 0x040010A7 RID: 4263
			SDLK_U,
			// Token: 0x040010A8 RID: 4264
			SDLK_V,
			// Token: 0x040010A9 RID: 4265
			SDLK_W,
			// Token: 0x040010AA RID: 4266
			SDLK_X,
			// Token: 0x040010AB RID: 4267
			SDLK_Y,
			// Token: 0x040010AC RID: 4268
			SDLK_Z,
			// Token: 0x040010AD RID: 4269
			SDLK_LEFTBRACE,
			// Token: 0x040010AE RID: 4270
			SDLK_PIPE,
			// Token: 0x040010AF RID: 4271
			SDLK_RIGHTBRACE,
			// Token: 0x040010B0 RID: 4272
			SDLK_TILDE,
			// Token: 0x040010B1 RID: 4273
			SDLK_DELETE,
			// Token: 0x040010B2 RID: 4274
			SDLK_PLUSMINUS = 177U,
			// Token: 0x040010B3 RID: 4275
			SDLK_CAPSLOCK = 1073741881U,
			// Token: 0x040010B4 RID: 4276
			SDLK_F1,
			// Token: 0x040010B5 RID: 4277
			SDLK_F2,
			// Token: 0x040010B6 RID: 4278
			SDLK_F3,
			// Token: 0x040010B7 RID: 4279
			SDLK_F4,
			// Token: 0x040010B8 RID: 4280
			SDLK_F5,
			// Token: 0x040010B9 RID: 4281
			SDLK_F6,
			// Token: 0x040010BA RID: 4282
			SDLK_F7,
			// Token: 0x040010BB RID: 4283
			SDLK_F8,
			// Token: 0x040010BC RID: 4284
			SDLK_F9,
			// Token: 0x040010BD RID: 4285
			SDLK_F10,
			// Token: 0x040010BE RID: 4286
			SDLK_F11,
			// Token: 0x040010BF RID: 4287
			SDLK_F12,
			// Token: 0x040010C0 RID: 4288
			SDLK_PRINTSCREEN,
			// Token: 0x040010C1 RID: 4289
			SDLK_SCROLLLOCK,
			// Token: 0x040010C2 RID: 4290
			SDLK_PAUSE,
			// Token: 0x040010C3 RID: 4291
			SDLK_INSERT,
			// Token: 0x040010C4 RID: 4292
			SDLK_HOME,
			// Token: 0x040010C5 RID: 4293
			SDLK_PAGEUP,
			// Token: 0x040010C6 RID: 4294
			SDLK_END = 1073741901U,
			// Token: 0x040010C7 RID: 4295
			SDLK_PAGEDOWN,
			// Token: 0x040010C8 RID: 4296
			SDLK_RIGHT,
			// Token: 0x040010C9 RID: 4297
			SDLK_LEFT,
			// Token: 0x040010CA RID: 4298
			SDLK_DOWN,
			// Token: 0x040010CB RID: 4299
			SDLK_UP,
			// Token: 0x040010CC RID: 4300
			SDLK_NUMLOCKCLEAR,
			// Token: 0x040010CD RID: 4301
			SDLK_KP_DIVIDE,
			// Token: 0x040010CE RID: 4302
			SDLK_KP_MULTIPLY,
			// Token: 0x040010CF RID: 4303
			SDLK_KP_MINUS,
			// Token: 0x040010D0 RID: 4304
			SDLK_KP_PLUS,
			// Token: 0x040010D1 RID: 4305
			SDLK_KP_ENTER,
			// Token: 0x040010D2 RID: 4306
			SDLK_KP_1,
			// Token: 0x040010D3 RID: 4307
			SDLK_KP_2,
			// Token: 0x040010D4 RID: 4308
			SDLK_KP_3,
			// Token: 0x040010D5 RID: 4309
			SDLK_KP_4,
			// Token: 0x040010D6 RID: 4310
			SDLK_KP_5,
			// Token: 0x040010D7 RID: 4311
			SDLK_KP_6,
			// Token: 0x040010D8 RID: 4312
			SDLK_KP_7,
			// Token: 0x040010D9 RID: 4313
			SDLK_KP_8,
			// Token: 0x040010DA RID: 4314
			SDLK_KP_9,
			// Token: 0x040010DB RID: 4315
			SDLK_KP_0,
			// Token: 0x040010DC RID: 4316
			SDLK_KP_PERIOD,
			// Token: 0x040010DD RID: 4317
			SDLK_APPLICATION = 1073741925U,
			// Token: 0x040010DE RID: 4318
			SDLK_POWER,
			// Token: 0x040010DF RID: 4319
			SDLK_KP_EQUALS,
			// Token: 0x040010E0 RID: 4320
			SDLK_F13,
			// Token: 0x040010E1 RID: 4321
			SDLK_F14,
			// Token: 0x040010E2 RID: 4322
			SDLK_F15,
			// Token: 0x040010E3 RID: 4323
			SDLK_F16,
			// Token: 0x040010E4 RID: 4324
			SDLK_F17,
			// Token: 0x040010E5 RID: 4325
			SDLK_F18,
			// Token: 0x040010E6 RID: 4326
			SDLK_F19,
			// Token: 0x040010E7 RID: 4327
			SDLK_F20,
			// Token: 0x040010E8 RID: 4328
			SDLK_F21,
			// Token: 0x040010E9 RID: 4329
			SDLK_F22,
			// Token: 0x040010EA RID: 4330
			SDLK_F23,
			// Token: 0x040010EB RID: 4331
			SDLK_F24,
			// Token: 0x040010EC RID: 4332
			SDLK_EXECUTE,
			// Token: 0x040010ED RID: 4333
			SDLK_HELP,
			// Token: 0x040010EE RID: 4334
			SDLK_MENU,
			// Token: 0x040010EF RID: 4335
			SDLK_SELECT,
			// Token: 0x040010F0 RID: 4336
			SDLK_STOP,
			// Token: 0x040010F1 RID: 4337
			SDLK_AGAIN,
			// Token: 0x040010F2 RID: 4338
			SDLK_UNDO,
			// Token: 0x040010F3 RID: 4339
			SDLK_CUT,
			// Token: 0x040010F4 RID: 4340
			SDLK_COPY,
			// Token: 0x040010F5 RID: 4341
			SDLK_PASTE,
			// Token: 0x040010F6 RID: 4342
			SDLK_FIND,
			// Token: 0x040010F7 RID: 4343
			SDLK_MUTE,
			// Token: 0x040010F8 RID: 4344
			SDLK_VOLUMEUP,
			// Token: 0x040010F9 RID: 4345
			SDLK_VOLUMEDOWN,
			// Token: 0x040010FA RID: 4346
			SDLK_KP_COMMA = 1073741957U,
			// Token: 0x040010FB RID: 4347
			SDLK_KP_EQUALSAS400,
			// Token: 0x040010FC RID: 4348
			SDLK_ALTERASE = 1073741977U,
			// Token: 0x040010FD RID: 4349
			SDLK_SYSREQ,
			// Token: 0x040010FE RID: 4350
			SDLK_CANCEL,
			// Token: 0x040010FF RID: 4351
			SDLK_CLEAR,
			// Token: 0x04001100 RID: 4352
			SDLK_PRIOR,
			// Token: 0x04001101 RID: 4353
			SDLK_RETURN2,
			// Token: 0x04001102 RID: 4354
			SDLK_SEPARATOR,
			// Token: 0x04001103 RID: 4355
			SDLK_OUT,
			// Token: 0x04001104 RID: 4356
			SDLK_OPER,
			// Token: 0x04001105 RID: 4357
			SDLK_CLEARAGAIN,
			// Token: 0x04001106 RID: 4358
			SDLK_CRSEL,
			// Token: 0x04001107 RID: 4359
			SDLK_EXSEL,
			// Token: 0x04001108 RID: 4360
			SDLK_KP_00 = 1073742000U,
			// Token: 0x04001109 RID: 4361
			SDLK_KP_000,
			// Token: 0x0400110A RID: 4362
			SDLK_THOUSANDSSEPARATOR,
			// Token: 0x0400110B RID: 4363
			SDLK_DECIMALSEPARATOR,
			// Token: 0x0400110C RID: 4364
			SDLK_CURRENCYUNIT,
			// Token: 0x0400110D RID: 4365
			SDLK_CURRENCYSUBUNIT,
			// Token: 0x0400110E RID: 4366
			SDLK_KP_LEFTPAREN,
			// Token: 0x0400110F RID: 4367
			SDLK_KP_RIGHTPAREN,
			// Token: 0x04001110 RID: 4368
			SDLK_KP_LEFTBRACE,
			// Token: 0x04001111 RID: 4369
			SDLK_KP_RIGHTBRACE,
			// Token: 0x04001112 RID: 4370
			SDLK_KP_TAB,
			// Token: 0x04001113 RID: 4371
			SDLK_KP_BACKSPACE,
			// Token: 0x04001114 RID: 4372
			SDLK_KP_A,
			// Token: 0x04001115 RID: 4373
			SDLK_KP_B,
			// Token: 0x04001116 RID: 4374
			SDLK_KP_C,
			// Token: 0x04001117 RID: 4375
			SDLK_KP_D,
			// Token: 0x04001118 RID: 4376
			SDLK_KP_E,
			// Token: 0x04001119 RID: 4377
			SDLK_KP_F,
			// Token: 0x0400111A RID: 4378
			SDLK_KP_XOR,
			// Token: 0x0400111B RID: 4379
			SDLK_KP_POWER,
			// Token: 0x0400111C RID: 4380
			SDLK_KP_PERCENT,
			// Token: 0x0400111D RID: 4381
			SDLK_KP_LESS,
			// Token: 0x0400111E RID: 4382
			SDLK_KP_GREATER,
			// Token: 0x0400111F RID: 4383
			SDLK_KP_AMPERSAND,
			// Token: 0x04001120 RID: 4384
			SDLK_KP_DBLAMPERSAND,
			// Token: 0x04001121 RID: 4385
			SDLK_KP_VERTICALBAR,
			// Token: 0x04001122 RID: 4386
			SDLK_KP_DBLVERTICALBAR,
			// Token: 0x04001123 RID: 4387
			SDLK_KP_COLON,
			// Token: 0x04001124 RID: 4388
			SDLK_KP_HASH,
			// Token: 0x04001125 RID: 4389
			SDLK_KP_SPACE,
			// Token: 0x04001126 RID: 4390
			SDLK_KP_AT,
			// Token: 0x04001127 RID: 4391
			SDLK_KP_EXCLAM,
			// Token: 0x04001128 RID: 4392
			SDLK_KP_MEMSTORE,
			// Token: 0x04001129 RID: 4393
			SDLK_KP_MEMRECALL,
			// Token: 0x0400112A RID: 4394
			SDLK_KP_MEMCLEAR,
			// Token: 0x0400112B RID: 4395
			SDLK_KP_MEMADD,
			// Token: 0x0400112C RID: 4396
			SDLK_KP_MEMSUBTRACT,
			// Token: 0x0400112D RID: 4397
			SDLK_KP_MEMMULTIPLY,
			// Token: 0x0400112E RID: 4398
			SDLK_KP_MEMDIVIDE,
			// Token: 0x0400112F RID: 4399
			SDLK_KP_PLUSMINUS,
			// Token: 0x04001130 RID: 4400
			SDLK_KP_CLEAR,
			// Token: 0x04001131 RID: 4401
			SDLK_KP_CLEARENTRY,
			// Token: 0x04001132 RID: 4402
			SDLK_KP_BINARY,
			// Token: 0x04001133 RID: 4403
			SDLK_KP_OCTAL,
			// Token: 0x04001134 RID: 4404
			SDLK_KP_DECIMAL,
			// Token: 0x04001135 RID: 4405
			SDLK_KP_HEXADECIMAL,
			// Token: 0x04001136 RID: 4406
			SDLK_LCTRL = 1073742048U,
			// Token: 0x04001137 RID: 4407
			SDLK_LSHIFT,
			// Token: 0x04001138 RID: 4408
			SDLK_LALT,
			// Token: 0x04001139 RID: 4409
			SDLK_LGUI,
			// Token: 0x0400113A RID: 4410
			SDLK_RCTRL,
			// Token: 0x0400113B RID: 4411
			SDLK_RSHIFT,
			// Token: 0x0400113C RID: 4412
			SDLK_RALT,
			// Token: 0x0400113D RID: 4413
			SDLK_RGUI,
			// Token: 0x0400113E RID: 4414
			SDLK_MODE = 1073742081U,
			// Token: 0x0400113F RID: 4415
			SDLK_SLEEP,
			// Token: 0x04001140 RID: 4416
			SDLK_WAKE,
			// Token: 0x04001141 RID: 4417
			SDLK_CHANNEL_INCREMENT,
			// Token: 0x04001142 RID: 4418
			SDLK_CHANNEL_DECREMENT,
			// Token: 0x04001143 RID: 4419
			SDLK_MEDIA_PLAY,
			// Token: 0x04001144 RID: 4420
			SDLK_MEDIA_PAUSE,
			// Token: 0x04001145 RID: 4421
			SDLK_MEDIA_RECORD,
			// Token: 0x04001146 RID: 4422
			SDLK_MEDIA_FAST_FORWARD,
			// Token: 0x04001147 RID: 4423
			SDLK_MEDIA_REWIND,
			// Token: 0x04001148 RID: 4424
			SDLK_MEDIA_NEXT_TRACK,
			// Token: 0x04001149 RID: 4425
			SDLK_MEDIA_PREVIOUS_TRACK,
			// Token: 0x0400114A RID: 4426
			SDLK_MEDIA_STOP,
			// Token: 0x0400114B RID: 4427
			SDLK_MEDIA_EJECT,
			// Token: 0x0400114C RID: 4428
			SDLK_MEDIA_PLAY_PAUSE,
			// Token: 0x0400114D RID: 4429
			SDLK_MEDIA_SELECT,
			// Token: 0x0400114E RID: 4430
			SDLK_AC_NEW,
			// Token: 0x0400114F RID: 4431
			SDLK_AC_OPEN,
			// Token: 0x04001150 RID: 4432
			SDLK_AC_CLOSE,
			// Token: 0x04001151 RID: 4433
			SDLK_AC_EXIT,
			// Token: 0x04001152 RID: 4434
			SDLK_AC_SAVE,
			// Token: 0x04001153 RID: 4435
			SDLK_AC_PRINT,
			// Token: 0x04001154 RID: 4436
			SDLK_AC_PROPERTIES,
			// Token: 0x04001155 RID: 4437
			SDLK_AC_SEARCH,
			// Token: 0x04001156 RID: 4438
			SDLK_AC_HOME,
			// Token: 0x04001157 RID: 4439
			SDLK_AC_BACK,
			// Token: 0x04001158 RID: 4440
			SDLK_AC_FORWARD,
			// Token: 0x04001159 RID: 4441
			SDLK_AC_STOP,
			// Token: 0x0400115A RID: 4442
			SDLK_AC_REFRESH,
			// Token: 0x0400115B RID: 4443
			SDLK_AC_BOOKMARKS,
			// Token: 0x0400115C RID: 4444
			SDLK_SOFTLEFT,
			// Token: 0x0400115D RID: 4445
			SDLK_SOFTRIGHT,
			// Token: 0x0400115E RID: 4446
			SDLK_CALL,
			// Token: 0x0400115F RID: 4447
			SDLK_ENDCALL,
			// Token: 0x04001160 RID: 4448
			SDLK_LEFT_TAB = 536870913U,
			// Token: 0x04001161 RID: 4449
			SDLK_LEVEL5_SHIFT,
			// Token: 0x04001162 RID: 4450
			SDLK_MULTI_KEY_COMPOSE,
			// Token: 0x04001163 RID: 4451
			SDLK_LMETA,
			// Token: 0x04001164 RID: 4452
			SDLK_RMETA,
			// Token: 0x04001165 RID: 4453
			SDLK_LHYPER,
			// Token: 0x04001166 RID: 4454
			SDLK_RHYPER
		}

		// Token: 0x02000229 RID: 553
		[Flags]
		public enum SDL_Keymod : ushort
		{
			// Token: 0x04001168 RID: 4456
			SDL_KMOD_NONE = 0,
			// Token: 0x04001169 RID: 4457
			SDL_KMOD_LSHIFT = 1,
			// Token: 0x0400116A RID: 4458
			SDL_KMOD_RSHIFT = 2,
			// Token: 0x0400116B RID: 4459
			SDL_KMOD_LCTRL = 64,
			// Token: 0x0400116C RID: 4460
			SDL_KMOD_RCTRL = 128,
			// Token: 0x0400116D RID: 4461
			SDL_KMOD_LALT = 256,
			// Token: 0x0400116E RID: 4462
			SDL_KMOD_RALT = 512,
			// Token: 0x0400116F RID: 4463
			SDL_KMOD_LGUI = 1024,
			// Token: 0x04001170 RID: 4464
			SDL_KMOD_RGUI = 2048,
			// Token: 0x04001171 RID: 4465
			SDL_KMOD_NUM = 4096,
			// Token: 0x04001172 RID: 4466
			SDL_KMOD_CAPS = 8192,
			// Token: 0x04001173 RID: 4467
			SDL_KMOD_MODE = 16384,
			// Token: 0x04001174 RID: 4468
			SDL_KMOD_SCROLL = 32768,
			// Token: 0x04001175 RID: 4469
			SDL_KMOD_CTRL = 192,
			// Token: 0x04001176 RID: 4470
			SDL_KMOD_SHIFT = 3,
			// Token: 0x04001177 RID: 4471
			SDL_KMOD_ALT = 768,
			// Token: 0x04001178 RID: 4472
			SDL_KMOD_GUI = 3072
		}

		// Token: 0x0200022A RID: 554
		public enum SDL_TextInputType
		{
			// Token: 0x0400117A RID: 4474
			SDL_TEXTINPUT_TYPE_TEXT,
			// Token: 0x0400117B RID: 4475
			SDL_TEXTINPUT_TYPE_TEXT_NAME,
			// Token: 0x0400117C RID: 4476
			SDL_TEXTINPUT_TYPE_TEXT_EMAIL,
			// Token: 0x0400117D RID: 4477
			SDL_TEXTINPUT_TYPE_TEXT_USERNAME,
			// Token: 0x0400117E RID: 4478
			SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_HIDDEN,
			// Token: 0x0400117F RID: 4479
			SDL_TEXTINPUT_TYPE_TEXT_PASSWORD_VISIBLE,
			// Token: 0x04001180 RID: 4480
			SDL_TEXTINPUT_TYPE_NUMBER,
			// Token: 0x04001181 RID: 4481
			SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_HIDDEN,
			// Token: 0x04001182 RID: 4482
			SDL_TEXTINPUT_TYPE_NUMBER_PASSWORD_VISIBLE
		}

		// Token: 0x0200022B RID: 555
		public enum SDL_Capitalization
		{
			// Token: 0x04001184 RID: 4484
			SDL_CAPITALIZE_NONE,
			// Token: 0x04001185 RID: 4485
			SDL_CAPITALIZE_SENTENCES,
			// Token: 0x04001186 RID: 4486
			SDL_CAPITALIZE_WORDS,
			// Token: 0x04001187 RID: 4487
			SDL_CAPITALIZE_LETTERS
		}

		// Token: 0x0200022C RID: 556
		public enum SDL_SystemCursor
		{
			// Token: 0x04001189 RID: 4489
			SDL_SYSTEM_CURSOR_DEFAULT,
			// Token: 0x0400118A RID: 4490
			SDL_SYSTEM_CURSOR_TEXT,
			// Token: 0x0400118B RID: 4491
			SDL_SYSTEM_CURSOR_WAIT,
			// Token: 0x0400118C RID: 4492
			SDL_SYSTEM_CURSOR_CROSSHAIR,
			// Token: 0x0400118D RID: 4493
			SDL_SYSTEM_CURSOR_PROGRESS,
			// Token: 0x0400118E RID: 4494
			SDL_SYSTEM_CURSOR_NWSE_RESIZE,
			// Token: 0x0400118F RID: 4495
			SDL_SYSTEM_CURSOR_NESW_RESIZE,
			// Token: 0x04001190 RID: 4496
			SDL_SYSTEM_CURSOR_EW_RESIZE,
			// Token: 0x04001191 RID: 4497
			SDL_SYSTEM_CURSOR_NS_RESIZE,
			// Token: 0x04001192 RID: 4498
			SDL_SYSTEM_CURSOR_MOVE,
			// Token: 0x04001193 RID: 4499
			SDL_SYSTEM_CURSOR_NOT_ALLOWED,
			// Token: 0x04001194 RID: 4500
			SDL_SYSTEM_CURSOR_POINTER,
			// Token: 0x04001195 RID: 4501
			SDL_SYSTEM_CURSOR_NW_RESIZE,
			// Token: 0x04001196 RID: 4502
			SDL_SYSTEM_CURSOR_N_RESIZE,
			// Token: 0x04001197 RID: 4503
			SDL_SYSTEM_CURSOR_NE_RESIZE,
			// Token: 0x04001198 RID: 4504
			SDL_SYSTEM_CURSOR_E_RESIZE,
			// Token: 0x04001199 RID: 4505
			SDL_SYSTEM_CURSOR_SE_RESIZE,
			// Token: 0x0400119A RID: 4506
			SDL_SYSTEM_CURSOR_S_RESIZE,
			// Token: 0x0400119B RID: 4507
			SDL_SYSTEM_CURSOR_SW_RESIZE,
			// Token: 0x0400119C RID: 4508
			SDL_SYSTEM_CURSOR_W_RESIZE,
			// Token: 0x0400119D RID: 4509
			SDL_SYSTEM_CURSOR_COUNT
		}

		// Token: 0x0200022D RID: 557
		public enum SDL_MouseWheelDirection
		{
			// Token: 0x0400119F RID: 4511
			SDL_MOUSEWHEEL_NORMAL,
			// Token: 0x040011A0 RID: 4512
			SDL_MOUSEWHEEL_FLIPPED
		}

		// Token: 0x0200022E RID: 558
		public struct SDL_CursorFrameInfo
		{
			// Token: 0x040011A1 RID: 4513
			public unsafe SDL.SDL_Surface* surface;

			// Token: 0x040011A2 RID: 4514
			public uint duration;
		}

		// Token: 0x0200022F RID: 559
		[Flags]
		public enum SDL_MouseButtonFlags : uint
		{
			// Token: 0x040011A4 RID: 4516
			SDL_BUTTON_LMASK = 1U,
			// Token: 0x040011A5 RID: 4517
			SDL_BUTTON_MMASK = 2U,
			// Token: 0x040011A6 RID: 4518
			SDL_BUTTON_RMASK = 4U,
			// Token: 0x040011A7 RID: 4519
			SDL_BUTTON_X1MASK = 8U,
			// Token: 0x040011A8 RID: 4520
			SDL_BUTTON_X2MASK = 16U
		}

		// Token: 0x02000230 RID: 560
		// (Invoke) Token: 0x06001960 RID: 6496
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void SDL_MouseMotionTransformCallback(IntPtr userdata, ulong timestamp, IntPtr window, uint mouseID, float* x, float* y);

		// Token: 0x02000231 RID: 561
		public enum SDL_TouchDeviceType
		{
			// Token: 0x040011AA RID: 4522
			SDL_TOUCH_DEVICE_INVALID = -1,
			// Token: 0x040011AB RID: 4523
			SDL_TOUCH_DEVICE_DIRECT,
			// Token: 0x040011AC RID: 4524
			SDL_TOUCH_DEVICE_INDIRECT_ABSOLUTE,
			// Token: 0x040011AD RID: 4525
			SDL_TOUCH_DEVICE_INDIRECT_RELATIVE
		}

		// Token: 0x02000232 RID: 562
		public struct SDL_Finger
		{
			// Token: 0x040011AE RID: 4526
			public ulong id;

			// Token: 0x040011AF RID: 4527
			public float x;

			// Token: 0x040011B0 RID: 4528
			public float y;

			// Token: 0x040011B1 RID: 4529
			public float pressure;
		}

		// Token: 0x02000233 RID: 563
		[Flags]
		public enum SDL_PenInputFlags : uint
		{
			// Token: 0x040011B3 RID: 4531
			SDL_PEN_INPUT_DOWN = 1U,
			// Token: 0x040011B4 RID: 4532
			SDL_PEN_INPUT_BUTTON_1 = 2U,
			// Token: 0x040011B5 RID: 4533
			SDL_PEN_INPUT_BUTTON_2 = 4U,
			// Token: 0x040011B6 RID: 4534
			SDL_PEN_INPUT_BUTTON_3 = 8U,
			// Token: 0x040011B7 RID: 4535
			SDL_PEN_INPUT_BUTTON_4 = 16U,
			// Token: 0x040011B8 RID: 4536
			SDL_PEN_INPUT_BUTTON_5 = 32U,
			// Token: 0x040011B9 RID: 4537
			SDL_PEN_INPUT_ERASER_TIP = 1073741824U
		}

		// Token: 0x02000234 RID: 564
		public enum SDL_PenAxis
		{
			// Token: 0x040011BB RID: 4539
			SDL_PEN_AXIS_PRESSURE,
			// Token: 0x040011BC RID: 4540
			SDL_PEN_AXIS_XTILT,
			// Token: 0x040011BD RID: 4541
			SDL_PEN_AXIS_YTILT,
			// Token: 0x040011BE RID: 4542
			SDL_PEN_AXIS_DISTANCE,
			// Token: 0x040011BF RID: 4543
			SDL_PEN_AXIS_ROTATION,
			// Token: 0x040011C0 RID: 4544
			SDL_PEN_AXIS_SLIDER,
			// Token: 0x040011C1 RID: 4545
			SDL_PEN_AXIS_TANGENTIAL_PRESSURE,
			// Token: 0x040011C2 RID: 4546
			SDL_PEN_AXIS_COUNT
		}

		// Token: 0x02000235 RID: 565
		public enum SDL_PenDeviceType
		{
			// Token: 0x040011C4 RID: 4548
			SDL_PEN_DEVICE_TYPE_INVALID = -1,
			// Token: 0x040011C5 RID: 4549
			SDL_PEN_DEVICE_TYPE_UNKNOWN,
			// Token: 0x040011C6 RID: 4550
			SDL_PEN_DEVICE_TYPE_DIRECT,
			// Token: 0x040011C7 RID: 4551
			SDL_PEN_DEVICE_TYPE_INDIRECT
		}

		// Token: 0x02000236 RID: 566
		public enum SDL_EventType
		{
			// Token: 0x040011C9 RID: 4553
			SDL_EVENT_FIRST,
			// Token: 0x040011CA RID: 4554
			SDL_EVENT_QUIT = 256,
			// Token: 0x040011CB RID: 4555
			SDL_EVENT_TERMINATING,
			// Token: 0x040011CC RID: 4556
			SDL_EVENT_LOW_MEMORY,
			// Token: 0x040011CD RID: 4557
			SDL_EVENT_WILL_ENTER_BACKGROUND,
			// Token: 0x040011CE RID: 4558
			SDL_EVENT_DID_ENTER_BACKGROUND,
			// Token: 0x040011CF RID: 4559
			SDL_EVENT_WILL_ENTER_FOREGROUND,
			// Token: 0x040011D0 RID: 4560
			SDL_EVENT_DID_ENTER_FOREGROUND,
			// Token: 0x040011D1 RID: 4561
			SDL_EVENT_LOCALE_CHANGED,
			// Token: 0x040011D2 RID: 4562
			SDL_EVENT_SYSTEM_THEME_CHANGED,
			// Token: 0x040011D3 RID: 4563
			SDL_EVENT_DISPLAY_ORIENTATION = 337,
			// Token: 0x040011D4 RID: 4564
			SDL_EVENT_DISPLAY_ADDED,
			// Token: 0x040011D5 RID: 4565
			SDL_EVENT_DISPLAY_REMOVED,
			// Token: 0x040011D6 RID: 4566
			SDL_EVENT_DISPLAY_MOVED,
			// Token: 0x040011D7 RID: 4567
			SDL_EVENT_DISPLAY_DESKTOP_MODE_CHANGED,
			// Token: 0x040011D8 RID: 4568
			SDL_EVENT_DISPLAY_CURRENT_MODE_CHANGED,
			// Token: 0x040011D9 RID: 4569
			SDL_EVENT_DISPLAY_CONTENT_SCALE_CHANGED,
			// Token: 0x040011DA RID: 4570
			SDL_EVENT_DISPLAY_USABLE_BOUNDS_CHANGED,
			// Token: 0x040011DB RID: 4571
			SDL_EVENT_DISPLAY_FIRST = 337,
			// Token: 0x040011DC RID: 4572
			SDL_EVENT_DISPLAY_LAST = 344,
			// Token: 0x040011DD RID: 4573
			SDL_EVENT_WINDOW_SHOWN = 514,
			// Token: 0x040011DE RID: 4574
			SDL_EVENT_WINDOW_HIDDEN,
			// Token: 0x040011DF RID: 4575
			SDL_EVENT_WINDOW_EXPOSED,
			// Token: 0x040011E0 RID: 4576
			SDL_EVENT_WINDOW_MOVED,
			// Token: 0x040011E1 RID: 4577
			SDL_EVENT_WINDOW_RESIZED,
			// Token: 0x040011E2 RID: 4578
			SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED,
			// Token: 0x040011E3 RID: 4579
			SDL_EVENT_WINDOW_METAL_VIEW_RESIZED,
			// Token: 0x040011E4 RID: 4580
			SDL_EVENT_WINDOW_MINIMIZED,
			// Token: 0x040011E5 RID: 4581
			SDL_EVENT_WINDOW_MAXIMIZED,
			// Token: 0x040011E6 RID: 4582
			SDL_EVENT_WINDOW_RESTORED,
			// Token: 0x040011E7 RID: 4583
			SDL_EVENT_WINDOW_MOUSE_ENTER,
			// Token: 0x040011E8 RID: 4584
			SDL_EVENT_WINDOW_MOUSE_LEAVE,
			// Token: 0x040011E9 RID: 4585
			SDL_EVENT_WINDOW_FOCUS_GAINED,
			// Token: 0x040011EA RID: 4586
			SDL_EVENT_WINDOW_FOCUS_LOST,
			// Token: 0x040011EB RID: 4587
			SDL_EVENT_WINDOW_CLOSE_REQUESTED,
			// Token: 0x040011EC RID: 4588
			SDL_EVENT_WINDOW_HIT_TEST,
			// Token: 0x040011ED RID: 4589
			SDL_EVENT_WINDOW_ICCPROF_CHANGED,
			// Token: 0x040011EE RID: 4590
			SDL_EVENT_WINDOW_DISPLAY_CHANGED,
			// Token: 0x040011EF RID: 4591
			SDL_EVENT_WINDOW_DISPLAY_SCALE_CHANGED,
			// Token: 0x040011F0 RID: 4592
			SDL_EVENT_WINDOW_SAFE_AREA_CHANGED,
			// Token: 0x040011F1 RID: 4593
			SDL_EVENT_WINDOW_OCCLUDED,
			// Token: 0x040011F2 RID: 4594
			SDL_EVENT_WINDOW_ENTER_FULLSCREEN,
			// Token: 0x040011F3 RID: 4595
			SDL_EVENT_WINDOW_LEAVE_FULLSCREEN,
			// Token: 0x040011F4 RID: 4596
			SDL_EVENT_WINDOW_DESTROYED,
			// Token: 0x040011F5 RID: 4597
			SDL_EVENT_WINDOW_HDR_STATE_CHANGED,
			// Token: 0x040011F6 RID: 4598
			SDL_EVENT_WINDOW_FIRST = 514,
			// Token: 0x040011F7 RID: 4599
			SDL_EVENT_WINDOW_LAST = 538,
			// Token: 0x040011F8 RID: 4600
			SDL_EVENT_KEY_DOWN = 768,
			// Token: 0x040011F9 RID: 4601
			SDL_EVENT_KEY_UP,
			// Token: 0x040011FA RID: 4602
			SDL_EVENT_TEXT_EDITING,
			// Token: 0x040011FB RID: 4603
			SDL_EVENT_TEXT_INPUT,
			// Token: 0x040011FC RID: 4604
			SDL_EVENT_KEYMAP_CHANGED,
			// Token: 0x040011FD RID: 4605
			SDL_EVENT_KEYBOARD_ADDED,
			// Token: 0x040011FE RID: 4606
			SDL_EVENT_KEYBOARD_REMOVED,
			// Token: 0x040011FF RID: 4607
			SDL_EVENT_TEXT_EDITING_CANDIDATES,
			// Token: 0x04001200 RID: 4608
			SDL_EVENT_SCREEN_KEYBOARD_SHOWN,
			// Token: 0x04001201 RID: 4609
			SDL_EVENT_SCREEN_KEYBOARD_HIDDEN,
			// Token: 0x04001202 RID: 4610
			SDL_EVENT_MOUSE_MOTION = 1024,
			// Token: 0x04001203 RID: 4611
			SDL_EVENT_MOUSE_BUTTON_DOWN,
			// Token: 0x04001204 RID: 4612
			SDL_EVENT_MOUSE_BUTTON_UP,
			// Token: 0x04001205 RID: 4613
			SDL_EVENT_MOUSE_WHEEL,
			// Token: 0x04001206 RID: 4614
			SDL_EVENT_MOUSE_ADDED,
			// Token: 0x04001207 RID: 4615
			SDL_EVENT_MOUSE_REMOVED,
			// Token: 0x04001208 RID: 4616
			SDL_EVENT_JOYSTICK_AXIS_MOTION = 1536,
			// Token: 0x04001209 RID: 4617
			SDL_EVENT_JOYSTICK_BALL_MOTION,
			// Token: 0x0400120A RID: 4618
			SDL_EVENT_JOYSTICK_HAT_MOTION,
			// Token: 0x0400120B RID: 4619
			SDL_EVENT_JOYSTICK_BUTTON_DOWN,
			// Token: 0x0400120C RID: 4620
			SDL_EVENT_JOYSTICK_BUTTON_UP,
			// Token: 0x0400120D RID: 4621
			SDL_EVENT_JOYSTICK_ADDED,
			// Token: 0x0400120E RID: 4622
			SDL_EVENT_JOYSTICK_REMOVED,
			// Token: 0x0400120F RID: 4623
			SDL_EVENT_JOYSTICK_BATTERY_UPDATED,
			// Token: 0x04001210 RID: 4624
			SDL_EVENT_JOYSTICK_UPDATE_COMPLETE,
			// Token: 0x04001211 RID: 4625
			SDL_EVENT_GAMEPAD_AXIS_MOTION = 1616,
			// Token: 0x04001212 RID: 4626
			SDL_EVENT_GAMEPAD_BUTTON_DOWN,
			// Token: 0x04001213 RID: 4627
			SDL_EVENT_GAMEPAD_BUTTON_UP,
			// Token: 0x04001214 RID: 4628
			SDL_EVENT_GAMEPAD_ADDED,
			// Token: 0x04001215 RID: 4629
			SDL_EVENT_GAMEPAD_REMOVED,
			// Token: 0x04001216 RID: 4630
			SDL_EVENT_GAMEPAD_REMAPPED,
			// Token: 0x04001217 RID: 4631
			SDL_EVENT_GAMEPAD_TOUCHPAD_DOWN,
			// Token: 0x04001218 RID: 4632
			SDL_EVENT_GAMEPAD_TOUCHPAD_MOTION,
			// Token: 0x04001219 RID: 4633
			SDL_EVENT_GAMEPAD_TOUCHPAD_UP,
			// Token: 0x0400121A RID: 4634
			SDL_EVENT_GAMEPAD_SENSOR_UPDATE,
			// Token: 0x0400121B RID: 4635
			SDL_EVENT_GAMEPAD_UPDATE_COMPLETE,
			// Token: 0x0400121C RID: 4636
			SDL_EVENT_GAMEPAD_STEAM_HANDLE_UPDATED,
			// Token: 0x0400121D RID: 4637
			SDL_EVENT_FINGER_DOWN = 1792,
			// Token: 0x0400121E RID: 4638
			SDL_EVENT_FINGER_UP,
			// Token: 0x0400121F RID: 4639
			SDL_EVENT_FINGER_MOTION,
			// Token: 0x04001220 RID: 4640
			SDL_EVENT_FINGER_CANCELED,
			// Token: 0x04001221 RID: 4641
			SDL_EVENT_PINCH_BEGIN = 1808,
			// Token: 0x04001222 RID: 4642
			SDL_EVENT_PINCH_UPDATE,
			// Token: 0x04001223 RID: 4643
			SDL_EVENT_PINCH_END,
			// Token: 0x04001224 RID: 4644
			SDL_EVENT_CLIPBOARD_UPDATE = 2304,
			// Token: 0x04001225 RID: 4645
			SDL_EVENT_DROP_FILE = 4096,
			// Token: 0x04001226 RID: 4646
			SDL_EVENT_DROP_TEXT,
			// Token: 0x04001227 RID: 4647
			SDL_EVENT_DROP_BEGIN,
			// Token: 0x04001228 RID: 4648
			SDL_EVENT_DROP_COMPLETE,
			// Token: 0x04001229 RID: 4649
			SDL_EVENT_DROP_POSITION,
			// Token: 0x0400122A RID: 4650
			SDL_EVENT_AUDIO_DEVICE_ADDED = 4352,
			// Token: 0x0400122B RID: 4651
			SDL_EVENT_AUDIO_DEVICE_REMOVED,
			// Token: 0x0400122C RID: 4652
			SDL_EVENT_AUDIO_DEVICE_FORMAT_CHANGED,
			// Token: 0x0400122D RID: 4653
			SDL_EVENT_SENSOR_UPDATE = 4608,
			// Token: 0x0400122E RID: 4654
			SDL_EVENT_PEN_PROXIMITY_IN = 4864,
			// Token: 0x0400122F RID: 4655
			SDL_EVENT_PEN_PROXIMITY_OUT,
			// Token: 0x04001230 RID: 4656
			SDL_EVENT_PEN_DOWN,
			// Token: 0x04001231 RID: 4657
			SDL_EVENT_PEN_UP,
			// Token: 0x04001232 RID: 4658
			SDL_EVENT_PEN_BUTTON_DOWN,
			// Token: 0x04001233 RID: 4659
			SDL_EVENT_PEN_BUTTON_UP,
			// Token: 0x04001234 RID: 4660
			SDL_EVENT_PEN_MOTION,
			// Token: 0x04001235 RID: 4661
			SDL_EVENT_PEN_AXIS,
			// Token: 0x04001236 RID: 4662
			SDL_EVENT_CAMERA_DEVICE_ADDED = 5120,
			// Token: 0x04001237 RID: 4663
			SDL_EVENT_CAMERA_DEVICE_REMOVED,
			// Token: 0x04001238 RID: 4664
			SDL_EVENT_CAMERA_DEVICE_APPROVED,
			// Token: 0x04001239 RID: 4665
			SDL_EVENT_CAMERA_DEVICE_DENIED,
			// Token: 0x0400123A RID: 4666
			SDL_EVENT_RENDER_TARGETS_RESET = 8192,
			// Token: 0x0400123B RID: 4667
			SDL_EVENT_RENDER_DEVICE_RESET,
			// Token: 0x0400123C RID: 4668
			SDL_EVENT_RENDER_DEVICE_LOST,
			// Token: 0x0400123D RID: 4669
			SDL_EVENT_PRIVATE0 = 16384,
			// Token: 0x0400123E RID: 4670
			SDL_EVENT_PRIVATE1,
			// Token: 0x0400123F RID: 4671
			SDL_EVENT_PRIVATE2,
			// Token: 0x04001240 RID: 4672
			SDL_EVENT_PRIVATE3,
			// Token: 0x04001241 RID: 4673
			SDL_EVENT_POLL_SENTINEL = 32512,
			// Token: 0x04001242 RID: 4674
			SDL_EVENT_USER = 32768,
			// Token: 0x04001243 RID: 4675
			SDL_EVENT_LAST = 65535,
			// Token: 0x04001244 RID: 4676
			SDL_EVENT_ENUM_PADDING = 2147483647
		}

		// Token: 0x02000237 RID: 567
		public struct SDL_CommonEvent
		{
			// Token: 0x04001245 RID: 4677
			public uint type;

			// Token: 0x04001246 RID: 4678
			public uint reserved;

			// Token: 0x04001247 RID: 4679
			public ulong timestamp;
		}

		// Token: 0x02000238 RID: 568
		public struct SDL_DisplayEvent
		{
			// Token: 0x04001248 RID: 4680
			public SDL.SDL_EventType type;

			// Token: 0x04001249 RID: 4681
			public uint reserved;

			// Token: 0x0400124A RID: 4682
			public ulong timestamp;

			// Token: 0x0400124B RID: 4683
			public uint displayID;

			// Token: 0x0400124C RID: 4684
			public int data1;

			// Token: 0x0400124D RID: 4685
			public int data2;
		}

		// Token: 0x02000239 RID: 569
		public struct SDL_WindowEvent
		{
			// Token: 0x0400124E RID: 4686
			public SDL.SDL_EventType type;

			// Token: 0x0400124F RID: 4687
			public uint reserved;

			// Token: 0x04001250 RID: 4688
			public ulong timestamp;

			// Token: 0x04001251 RID: 4689
			public uint windowID;

			// Token: 0x04001252 RID: 4690
			public int data1;

			// Token: 0x04001253 RID: 4691
			public int data2;
		}

		// Token: 0x0200023A RID: 570
		public struct SDL_KeyboardDeviceEvent
		{
			// Token: 0x04001254 RID: 4692
			public SDL.SDL_EventType type;

			// Token: 0x04001255 RID: 4693
			public uint reserved;

			// Token: 0x04001256 RID: 4694
			public ulong timestamp;

			// Token: 0x04001257 RID: 4695
			public uint which;
		}

		// Token: 0x0200023B RID: 571
		public struct SDL_KeyboardEvent
		{
			// Token: 0x04001258 RID: 4696
			public SDL.SDL_EventType type;

			// Token: 0x04001259 RID: 4697
			public uint reserved;

			// Token: 0x0400125A RID: 4698
			public ulong timestamp;

			// Token: 0x0400125B RID: 4699
			public uint windowID;

			// Token: 0x0400125C RID: 4700
			public uint which;

			// Token: 0x0400125D RID: 4701
			public SDL.SDL_Scancode scancode;

			// Token: 0x0400125E RID: 4702
			public uint key;

			// Token: 0x0400125F RID: 4703
			public SDL.SDL_Keymod mod;

			// Token: 0x04001260 RID: 4704
			public ushort raw;

			// Token: 0x04001261 RID: 4705
			public SDL.SDLBool down;

			// Token: 0x04001262 RID: 4706
			public SDL.SDLBool repeat;
		}

		// Token: 0x0200023C RID: 572
		public struct SDL_TextEditingEvent
		{
			// Token: 0x04001263 RID: 4707
			public SDL.SDL_EventType type;

			// Token: 0x04001264 RID: 4708
			public uint reserved;

			// Token: 0x04001265 RID: 4709
			public ulong timestamp;

			// Token: 0x04001266 RID: 4710
			public uint windowID;

			// Token: 0x04001267 RID: 4711
			public unsafe byte* text;

			// Token: 0x04001268 RID: 4712
			public int start;

			// Token: 0x04001269 RID: 4713
			public int length;
		}

		// Token: 0x0200023D RID: 573
		public struct SDL_TextEditingCandidatesEvent
		{
			// Token: 0x0400126A RID: 4714
			public SDL.SDL_EventType type;

			// Token: 0x0400126B RID: 4715
			public uint reserved;

			// Token: 0x0400126C RID: 4716
			public ulong timestamp;

			// Token: 0x0400126D RID: 4717
			public uint windowID;

			// Token: 0x0400126E RID: 4718
			public unsafe byte** candidates;

			// Token: 0x0400126F RID: 4719
			public int num_candidates;

			// Token: 0x04001270 RID: 4720
			public int selected_candidate;

			// Token: 0x04001271 RID: 4721
			public SDL.SDLBool horizontal;

			// Token: 0x04001272 RID: 4722
			public byte padding1;

			// Token: 0x04001273 RID: 4723
			public byte padding2;

			// Token: 0x04001274 RID: 4724
			public byte padding3;
		}

		// Token: 0x0200023E RID: 574
		public struct SDL_TextInputEvent
		{
			// Token: 0x04001275 RID: 4725
			public SDL.SDL_EventType type;

			// Token: 0x04001276 RID: 4726
			public uint reserved;

			// Token: 0x04001277 RID: 4727
			public ulong timestamp;

			// Token: 0x04001278 RID: 4728
			public uint windowID;

			// Token: 0x04001279 RID: 4729
			public unsafe byte* text;
		}

		// Token: 0x0200023F RID: 575
		public struct SDL_MouseDeviceEvent
		{
			// Token: 0x0400127A RID: 4730
			public SDL.SDL_EventType type;

			// Token: 0x0400127B RID: 4731
			public uint reserved;

			// Token: 0x0400127C RID: 4732
			public ulong timestamp;

			// Token: 0x0400127D RID: 4733
			public uint which;
		}

		// Token: 0x02000240 RID: 576
		public struct SDL_MouseMotionEvent
		{
			// Token: 0x0400127E RID: 4734
			public SDL.SDL_EventType type;

			// Token: 0x0400127F RID: 4735
			public uint reserved;

			// Token: 0x04001280 RID: 4736
			public ulong timestamp;

			// Token: 0x04001281 RID: 4737
			public uint windowID;

			// Token: 0x04001282 RID: 4738
			public uint which;

			// Token: 0x04001283 RID: 4739
			public SDL.SDL_MouseButtonFlags state;

			// Token: 0x04001284 RID: 4740
			public float x;

			// Token: 0x04001285 RID: 4741
			public float y;

			// Token: 0x04001286 RID: 4742
			public float xrel;

			// Token: 0x04001287 RID: 4743
			public float yrel;
		}

		// Token: 0x02000241 RID: 577
		public struct SDL_MouseButtonEvent
		{
			// Token: 0x04001288 RID: 4744
			public SDL.SDL_EventType type;

			// Token: 0x04001289 RID: 4745
			public uint reserved;

			// Token: 0x0400128A RID: 4746
			public ulong timestamp;

			// Token: 0x0400128B RID: 4747
			public uint windowID;

			// Token: 0x0400128C RID: 4748
			public uint which;

			// Token: 0x0400128D RID: 4749
			public byte button;

			// Token: 0x0400128E RID: 4750
			public SDL.SDLBool down;

			// Token: 0x0400128F RID: 4751
			public byte clicks;

			// Token: 0x04001290 RID: 4752
			public byte padding;

			// Token: 0x04001291 RID: 4753
			public float x;

			// Token: 0x04001292 RID: 4754
			public float y;
		}

		// Token: 0x02000242 RID: 578
		public struct SDL_MouseWheelEvent
		{
			// Token: 0x04001293 RID: 4755
			public SDL.SDL_EventType type;

			// Token: 0x04001294 RID: 4756
			public uint reserved;

			// Token: 0x04001295 RID: 4757
			public ulong timestamp;

			// Token: 0x04001296 RID: 4758
			public uint windowID;

			// Token: 0x04001297 RID: 4759
			public uint which;

			// Token: 0x04001298 RID: 4760
			public float x;

			// Token: 0x04001299 RID: 4761
			public float y;

			// Token: 0x0400129A RID: 4762
			public SDL.SDL_MouseWheelDirection direction;

			// Token: 0x0400129B RID: 4763
			public float mouse_x;

			// Token: 0x0400129C RID: 4764
			public float mouse_y;

			// Token: 0x0400129D RID: 4765
			public int integer_x;

			// Token: 0x0400129E RID: 4766
			public int integer_y;
		}

		// Token: 0x02000243 RID: 579
		public struct SDL_JoyAxisEvent
		{
			// Token: 0x0400129F RID: 4767
			public SDL.SDL_EventType type;

			// Token: 0x040012A0 RID: 4768
			public uint reserved;

			// Token: 0x040012A1 RID: 4769
			public ulong timestamp;

			// Token: 0x040012A2 RID: 4770
			public uint which;

			// Token: 0x040012A3 RID: 4771
			public byte axis;

			// Token: 0x040012A4 RID: 4772
			public byte padding1;

			// Token: 0x040012A5 RID: 4773
			public byte padding2;

			// Token: 0x040012A6 RID: 4774
			public byte padding3;

			// Token: 0x040012A7 RID: 4775
			public short value;

			// Token: 0x040012A8 RID: 4776
			public ushort padding4;
		}

		// Token: 0x02000244 RID: 580
		public struct SDL_JoyBallEvent
		{
			// Token: 0x040012A9 RID: 4777
			public SDL.SDL_EventType type;

			// Token: 0x040012AA RID: 4778
			public uint reserved;

			// Token: 0x040012AB RID: 4779
			public ulong timestamp;

			// Token: 0x040012AC RID: 4780
			public uint which;

			// Token: 0x040012AD RID: 4781
			public byte ball;

			// Token: 0x040012AE RID: 4782
			public byte padding1;

			// Token: 0x040012AF RID: 4783
			public byte padding2;

			// Token: 0x040012B0 RID: 4784
			public byte padding3;

			// Token: 0x040012B1 RID: 4785
			public short xrel;

			// Token: 0x040012B2 RID: 4786
			public short yrel;
		}

		// Token: 0x02000245 RID: 581
		public struct SDL_JoyHatEvent
		{
			// Token: 0x040012B3 RID: 4787
			public SDL.SDL_EventType type;

			// Token: 0x040012B4 RID: 4788
			public uint reserved;

			// Token: 0x040012B5 RID: 4789
			public ulong timestamp;

			// Token: 0x040012B6 RID: 4790
			public uint which;

			// Token: 0x040012B7 RID: 4791
			public byte hat;

			// Token: 0x040012B8 RID: 4792
			public byte value;

			// Token: 0x040012B9 RID: 4793
			public byte padding1;

			// Token: 0x040012BA RID: 4794
			public byte padding2;
		}

		// Token: 0x02000246 RID: 582
		public struct SDL_JoyButtonEvent
		{
			// Token: 0x040012BB RID: 4795
			public SDL.SDL_EventType type;

			// Token: 0x040012BC RID: 4796
			public uint reserved;

			// Token: 0x040012BD RID: 4797
			public ulong timestamp;

			// Token: 0x040012BE RID: 4798
			public uint which;

			// Token: 0x040012BF RID: 4799
			public byte button;

			// Token: 0x040012C0 RID: 4800
			public SDL.SDLBool down;

			// Token: 0x040012C1 RID: 4801
			public byte padding1;

			// Token: 0x040012C2 RID: 4802
			public byte padding2;
		}

		// Token: 0x02000247 RID: 583
		public struct SDL_JoyDeviceEvent
		{
			// Token: 0x040012C3 RID: 4803
			public SDL.SDL_EventType type;

			// Token: 0x040012C4 RID: 4804
			public uint reserved;

			// Token: 0x040012C5 RID: 4805
			public ulong timestamp;

			// Token: 0x040012C6 RID: 4806
			public uint which;
		}

		// Token: 0x02000248 RID: 584
		public struct SDL_JoyBatteryEvent
		{
			// Token: 0x040012C7 RID: 4807
			public SDL.SDL_EventType type;

			// Token: 0x040012C8 RID: 4808
			public uint reserved;

			// Token: 0x040012C9 RID: 4809
			public ulong timestamp;

			// Token: 0x040012CA RID: 4810
			public uint which;

			// Token: 0x040012CB RID: 4811
			public SDL.SDL_PowerState state;

			// Token: 0x040012CC RID: 4812
			public int percent;
		}

		// Token: 0x02000249 RID: 585
		public struct SDL_GamepadAxisEvent
		{
			// Token: 0x040012CD RID: 4813
			public SDL.SDL_EventType type;

			// Token: 0x040012CE RID: 4814
			public uint reserved;

			// Token: 0x040012CF RID: 4815
			public ulong timestamp;

			// Token: 0x040012D0 RID: 4816
			public uint which;

			// Token: 0x040012D1 RID: 4817
			public byte axis;

			// Token: 0x040012D2 RID: 4818
			public byte padding1;

			// Token: 0x040012D3 RID: 4819
			public byte padding2;

			// Token: 0x040012D4 RID: 4820
			public byte padding3;

			// Token: 0x040012D5 RID: 4821
			public short value;

			// Token: 0x040012D6 RID: 4822
			public ushort padding4;
		}

		// Token: 0x0200024A RID: 586
		public struct SDL_GamepadButtonEvent
		{
			// Token: 0x040012D7 RID: 4823
			public SDL.SDL_EventType type;

			// Token: 0x040012D8 RID: 4824
			public uint reserved;

			// Token: 0x040012D9 RID: 4825
			public ulong timestamp;

			// Token: 0x040012DA RID: 4826
			public uint which;

			// Token: 0x040012DB RID: 4827
			public byte button;

			// Token: 0x040012DC RID: 4828
			public SDL.SDLBool down;

			// Token: 0x040012DD RID: 4829
			public byte padding1;

			// Token: 0x040012DE RID: 4830
			public byte padding2;
		}

		// Token: 0x0200024B RID: 587
		public struct SDL_GamepadDeviceEvent
		{
			// Token: 0x040012DF RID: 4831
			public SDL.SDL_EventType type;

			// Token: 0x040012E0 RID: 4832
			public uint reserved;

			// Token: 0x040012E1 RID: 4833
			public ulong timestamp;

			// Token: 0x040012E2 RID: 4834
			public uint which;
		}

		// Token: 0x0200024C RID: 588
		public struct SDL_GamepadTouchpadEvent
		{
			// Token: 0x040012E3 RID: 4835
			public SDL.SDL_EventType type;

			// Token: 0x040012E4 RID: 4836
			public uint reserved;

			// Token: 0x040012E5 RID: 4837
			public ulong timestamp;

			// Token: 0x040012E6 RID: 4838
			public uint which;

			// Token: 0x040012E7 RID: 4839
			public int touchpad;

			// Token: 0x040012E8 RID: 4840
			public int finger;

			// Token: 0x040012E9 RID: 4841
			public float x;

			// Token: 0x040012EA RID: 4842
			public float y;

			// Token: 0x040012EB RID: 4843
			public float pressure;
		}

		// Token: 0x0200024D RID: 589
		public struct SDL_GamepadSensorEvent
		{
			// Token: 0x040012EC RID: 4844
			public SDL.SDL_EventType type;

			// Token: 0x040012ED RID: 4845
			public uint reserved;

			// Token: 0x040012EE RID: 4846
			public ulong timestamp;

			// Token: 0x040012EF RID: 4847
			public uint which;

			// Token: 0x040012F0 RID: 4848
			public int sensor;

			// Token: 0x040012F1 RID: 4849
			[FixedBuffer(typeof(float), 3)]
			public SDL.SDL_GamepadSensorEvent.<data>e__FixedBuffer data;

			// Token: 0x040012F2 RID: 4850
			public ulong sensor_timestamp;

			// Token: 0x020003FB RID: 1019
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 12)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04001E2B RID: 7723
				public float FixedElementField;
			}
		}

		// Token: 0x0200024E RID: 590
		public struct SDL_AudioDeviceEvent
		{
			// Token: 0x040012F3 RID: 4851
			public SDL.SDL_EventType type;

			// Token: 0x040012F4 RID: 4852
			public uint reserved;

			// Token: 0x040012F5 RID: 4853
			public ulong timestamp;

			// Token: 0x040012F6 RID: 4854
			public uint which;

			// Token: 0x040012F7 RID: 4855
			public SDL.SDLBool recording;

			// Token: 0x040012F8 RID: 4856
			public byte padding1;

			// Token: 0x040012F9 RID: 4857
			public byte padding2;

			// Token: 0x040012FA RID: 4858
			public byte padding3;
		}

		// Token: 0x0200024F RID: 591
		public struct SDL_CameraDeviceEvent
		{
			// Token: 0x040012FB RID: 4859
			public SDL.SDL_EventType type;

			// Token: 0x040012FC RID: 4860
			public uint reserved;

			// Token: 0x040012FD RID: 4861
			public ulong timestamp;

			// Token: 0x040012FE RID: 4862
			public uint which;
		}

		// Token: 0x02000250 RID: 592
		public struct SDL_RenderEvent
		{
			// Token: 0x040012FF RID: 4863
			public SDL.SDL_EventType type;

			// Token: 0x04001300 RID: 4864
			public uint reserved;

			// Token: 0x04001301 RID: 4865
			public ulong timestamp;

			// Token: 0x04001302 RID: 4866
			public uint windowID;
		}

		// Token: 0x02000251 RID: 593
		public struct SDL_TouchFingerEvent
		{
			// Token: 0x04001303 RID: 4867
			public SDL.SDL_EventType type;

			// Token: 0x04001304 RID: 4868
			public uint reserved;

			// Token: 0x04001305 RID: 4869
			public ulong timestamp;

			// Token: 0x04001306 RID: 4870
			public ulong touchID;

			// Token: 0x04001307 RID: 4871
			public ulong fingerID;

			// Token: 0x04001308 RID: 4872
			public float x;

			// Token: 0x04001309 RID: 4873
			public float y;

			// Token: 0x0400130A RID: 4874
			public float dx;

			// Token: 0x0400130B RID: 4875
			public float dy;

			// Token: 0x0400130C RID: 4876
			public float pressure;

			// Token: 0x0400130D RID: 4877
			public uint windowID;
		}

		// Token: 0x02000252 RID: 594
		public struct SDL_PinchFingerEvent
		{
			// Token: 0x0400130E RID: 4878
			public SDL.SDL_EventType type;

			// Token: 0x0400130F RID: 4879
			public uint reserved;

			// Token: 0x04001310 RID: 4880
			public ulong timestamp;

			// Token: 0x04001311 RID: 4881
			public float scale;

			// Token: 0x04001312 RID: 4882
			public uint windowID;
		}

		// Token: 0x02000253 RID: 595
		public struct SDL_PenProximityEvent
		{
			// Token: 0x04001313 RID: 4883
			public SDL.SDL_EventType type;

			// Token: 0x04001314 RID: 4884
			public uint reserved;

			// Token: 0x04001315 RID: 4885
			public ulong timestamp;

			// Token: 0x04001316 RID: 4886
			public uint windowID;

			// Token: 0x04001317 RID: 4887
			public uint which;
		}

		// Token: 0x02000254 RID: 596
		public struct SDL_PenMotionEvent
		{
			// Token: 0x04001318 RID: 4888
			public SDL.SDL_EventType type;

			// Token: 0x04001319 RID: 4889
			public uint reserved;

			// Token: 0x0400131A RID: 4890
			public ulong timestamp;

			// Token: 0x0400131B RID: 4891
			public uint windowID;

			// Token: 0x0400131C RID: 4892
			public uint which;

			// Token: 0x0400131D RID: 4893
			public SDL.SDL_PenInputFlags pen_state;

			// Token: 0x0400131E RID: 4894
			public float x;

			// Token: 0x0400131F RID: 4895
			public float y;
		}

		// Token: 0x02000255 RID: 597
		public struct SDL_PenTouchEvent
		{
			// Token: 0x04001320 RID: 4896
			public SDL.SDL_EventType type;

			// Token: 0x04001321 RID: 4897
			public uint reserved;

			// Token: 0x04001322 RID: 4898
			public ulong timestamp;

			// Token: 0x04001323 RID: 4899
			public uint windowID;

			// Token: 0x04001324 RID: 4900
			public uint which;

			// Token: 0x04001325 RID: 4901
			public SDL.SDL_PenInputFlags pen_state;

			// Token: 0x04001326 RID: 4902
			public float x;

			// Token: 0x04001327 RID: 4903
			public float y;

			// Token: 0x04001328 RID: 4904
			public SDL.SDLBool eraser;

			// Token: 0x04001329 RID: 4905
			public SDL.SDLBool down;
		}

		// Token: 0x02000256 RID: 598
		public struct SDL_PenButtonEvent
		{
			// Token: 0x0400132A RID: 4906
			public SDL.SDL_EventType type;

			// Token: 0x0400132B RID: 4907
			public uint reserved;

			// Token: 0x0400132C RID: 4908
			public ulong timestamp;

			// Token: 0x0400132D RID: 4909
			public uint windowID;

			// Token: 0x0400132E RID: 4910
			public uint which;

			// Token: 0x0400132F RID: 4911
			public SDL.SDL_PenInputFlags pen_state;

			// Token: 0x04001330 RID: 4912
			public float x;

			// Token: 0x04001331 RID: 4913
			public float y;

			// Token: 0x04001332 RID: 4914
			public byte button;

			// Token: 0x04001333 RID: 4915
			public SDL.SDLBool down;
		}

		// Token: 0x02000257 RID: 599
		public struct SDL_PenAxisEvent
		{
			// Token: 0x04001334 RID: 4916
			public SDL.SDL_EventType type;

			// Token: 0x04001335 RID: 4917
			public uint reserved;

			// Token: 0x04001336 RID: 4918
			public ulong timestamp;

			// Token: 0x04001337 RID: 4919
			public uint windowID;

			// Token: 0x04001338 RID: 4920
			public uint which;

			// Token: 0x04001339 RID: 4921
			public SDL.SDL_PenInputFlags pen_state;

			// Token: 0x0400133A RID: 4922
			public float x;

			// Token: 0x0400133B RID: 4923
			public float y;

			// Token: 0x0400133C RID: 4924
			public SDL.SDL_PenAxis axis;

			// Token: 0x0400133D RID: 4925
			public float value;
		}

		// Token: 0x02000258 RID: 600
		public struct SDL_DropEvent
		{
			// Token: 0x0400133E RID: 4926
			public SDL.SDL_EventType type;

			// Token: 0x0400133F RID: 4927
			public uint reserved;

			// Token: 0x04001340 RID: 4928
			public ulong timestamp;

			// Token: 0x04001341 RID: 4929
			public uint windowID;

			// Token: 0x04001342 RID: 4930
			public float x;

			// Token: 0x04001343 RID: 4931
			public float y;

			// Token: 0x04001344 RID: 4932
			public unsafe byte* source;

			// Token: 0x04001345 RID: 4933
			public unsafe byte* data;
		}

		// Token: 0x02000259 RID: 601
		public struct SDL_ClipboardEvent
		{
			// Token: 0x04001346 RID: 4934
			public SDL.SDL_EventType type;

			// Token: 0x04001347 RID: 4935
			public uint reserved;

			// Token: 0x04001348 RID: 4936
			public ulong timestamp;

			// Token: 0x04001349 RID: 4937
			public SDL.SDLBool owner;

			// Token: 0x0400134A RID: 4938
			public int num_mime_types;

			// Token: 0x0400134B RID: 4939
			public unsafe byte** mime_types;
		}

		// Token: 0x0200025A RID: 602
		public struct SDL_SensorEvent
		{
			// Token: 0x0400134C RID: 4940
			public SDL.SDL_EventType type;

			// Token: 0x0400134D RID: 4941
			public uint reserved;

			// Token: 0x0400134E RID: 4942
			public ulong timestamp;

			// Token: 0x0400134F RID: 4943
			public uint which;

			// Token: 0x04001350 RID: 4944
			[FixedBuffer(typeof(float), 6)]
			public SDL.SDL_SensorEvent.<data>e__FixedBuffer data;

			// Token: 0x04001351 RID: 4945
			public ulong sensor_timestamp;

			// Token: 0x020003FC RID: 1020
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 24)]
			public struct <data>e__FixedBuffer
			{
				// Token: 0x04001E2C RID: 7724
				public float FixedElementField;
			}
		}

		// Token: 0x0200025B RID: 603
		public struct SDL_QuitEvent
		{
			// Token: 0x04001352 RID: 4946
			public SDL.SDL_EventType type;

			// Token: 0x04001353 RID: 4947
			public uint reserved;

			// Token: 0x04001354 RID: 4948
			public ulong timestamp;
		}

		// Token: 0x0200025C RID: 604
		public struct SDL_UserEvent
		{
			// Token: 0x04001355 RID: 4949
			public uint type;

			// Token: 0x04001356 RID: 4950
			public uint reserved;

			// Token: 0x04001357 RID: 4951
			public ulong timestamp;

			// Token: 0x04001358 RID: 4952
			public uint windowID;

			// Token: 0x04001359 RID: 4953
			public int code;

			// Token: 0x0400135A RID: 4954
			public IntPtr data1;

			// Token: 0x0400135B RID: 4955
			public IntPtr data2;
		}

		// Token: 0x0200025D RID: 605
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_Event
		{
			// Token: 0x0400135C RID: 4956
			[FieldOffset(0)]
			public uint type;

			// Token: 0x0400135D RID: 4957
			[FieldOffset(0)]
			public SDL.SDL_CommonEvent common;

			// Token: 0x0400135E RID: 4958
			[FieldOffset(0)]
			public SDL.SDL_DisplayEvent display;

			// Token: 0x0400135F RID: 4959
			[FieldOffset(0)]
			public SDL.SDL_WindowEvent window;

			// Token: 0x04001360 RID: 4960
			[FieldOffset(0)]
			public SDL.SDL_KeyboardDeviceEvent kdevice;

			// Token: 0x04001361 RID: 4961
			[FieldOffset(0)]
			public SDL.SDL_KeyboardEvent key;

			// Token: 0x04001362 RID: 4962
			[FieldOffset(0)]
			public SDL.SDL_TextEditingEvent edit;

			// Token: 0x04001363 RID: 4963
			[FieldOffset(0)]
			public SDL.SDL_TextEditingCandidatesEvent edit_candidates;

			// Token: 0x04001364 RID: 4964
			[FieldOffset(0)]
			public SDL.SDL_TextInputEvent text;

			// Token: 0x04001365 RID: 4965
			[FieldOffset(0)]
			public SDL.SDL_MouseDeviceEvent mdevice;

			// Token: 0x04001366 RID: 4966
			[FieldOffset(0)]
			public SDL.SDL_MouseMotionEvent motion;

			// Token: 0x04001367 RID: 4967
			[FieldOffset(0)]
			public SDL.SDL_MouseButtonEvent button;

			// Token: 0x04001368 RID: 4968
			[FieldOffset(0)]
			public SDL.SDL_MouseWheelEvent wheel;

			// Token: 0x04001369 RID: 4969
			[FieldOffset(0)]
			public SDL.SDL_JoyDeviceEvent jdevice;

			// Token: 0x0400136A RID: 4970
			[FieldOffset(0)]
			public SDL.SDL_JoyAxisEvent jaxis;

			// Token: 0x0400136B RID: 4971
			[FieldOffset(0)]
			public SDL.SDL_JoyBallEvent jball;

			// Token: 0x0400136C RID: 4972
			[FieldOffset(0)]
			public SDL.SDL_JoyHatEvent jhat;

			// Token: 0x0400136D RID: 4973
			[FieldOffset(0)]
			public SDL.SDL_JoyButtonEvent jbutton;

			// Token: 0x0400136E RID: 4974
			[FieldOffset(0)]
			public SDL.SDL_JoyBatteryEvent jbattery;

			// Token: 0x0400136F RID: 4975
			[FieldOffset(0)]
			public SDL.SDL_GamepadDeviceEvent gdevice;

			// Token: 0x04001370 RID: 4976
			[FieldOffset(0)]
			public SDL.SDL_GamepadAxisEvent gaxis;

			// Token: 0x04001371 RID: 4977
			[FieldOffset(0)]
			public SDL.SDL_GamepadButtonEvent gbutton;

			// Token: 0x04001372 RID: 4978
			[FieldOffset(0)]
			public SDL.SDL_GamepadTouchpadEvent gtouchpad;

			// Token: 0x04001373 RID: 4979
			[FieldOffset(0)]
			public SDL.SDL_GamepadSensorEvent gsensor;

			// Token: 0x04001374 RID: 4980
			[FieldOffset(0)]
			public SDL.SDL_AudioDeviceEvent adevice;

			// Token: 0x04001375 RID: 4981
			[FieldOffset(0)]
			public SDL.SDL_CameraDeviceEvent cdevice;

			// Token: 0x04001376 RID: 4982
			[FieldOffset(0)]
			public SDL.SDL_SensorEvent sensor;

			// Token: 0x04001377 RID: 4983
			[FieldOffset(0)]
			public SDL.SDL_QuitEvent quit;

			// Token: 0x04001378 RID: 4984
			[FieldOffset(0)]
			public SDL.SDL_UserEvent user;

			// Token: 0x04001379 RID: 4985
			[FieldOffset(0)]
			public SDL.SDL_TouchFingerEvent tfinger;

			// Token: 0x0400137A RID: 4986
			[FieldOffset(0)]
			public SDL.SDL_PinchFingerEvent pinch;

			// Token: 0x0400137B RID: 4987
			[FieldOffset(0)]
			public SDL.SDL_PenProximityEvent pproximity;

			// Token: 0x0400137C RID: 4988
			[FieldOffset(0)]
			public SDL.SDL_PenTouchEvent ptouch;

			// Token: 0x0400137D RID: 4989
			[FieldOffset(0)]
			public SDL.SDL_PenMotionEvent pmotion;

			// Token: 0x0400137E RID: 4990
			[FieldOffset(0)]
			public SDL.SDL_PenButtonEvent pbutton;

			// Token: 0x0400137F RID: 4991
			[FieldOffset(0)]
			public SDL.SDL_PenAxisEvent paxis;

			// Token: 0x04001380 RID: 4992
			[FieldOffset(0)]
			public SDL.SDL_RenderEvent render;

			// Token: 0x04001381 RID: 4993
			[FieldOffset(0)]
			public SDL.SDL_DropEvent drop;

			// Token: 0x04001382 RID: 4994
			[FieldOffset(0)]
			public SDL.SDL_ClipboardEvent clipboard;

			// Token: 0x04001383 RID: 4995
			[FixedBuffer(typeof(byte), 128)]
			[FieldOffset(0)]
			public SDL.SDL_Event.<padding>e__FixedBuffer padding;

			// Token: 0x020003FD RID: 1021
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 128)]
			public struct <padding>e__FixedBuffer
			{
				// Token: 0x04001E2D RID: 7725
				public byte FixedElementField;
			}
		}

		// Token: 0x0200025E RID: 606
		public enum SDL_EventAction
		{
			// Token: 0x04001385 RID: 4997
			SDL_ADDEVENT,
			// Token: 0x04001386 RID: 4998
			SDL_PEEKEVENT,
			// Token: 0x04001387 RID: 4999
			SDL_GETEVENT
		}

		// Token: 0x0200025F RID: 607
		// (Invoke) Token: 0x06001964 RID: 6500
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate bool SDL_EventFilter(IntPtr userdata, SDL.SDL_Event* evt);

		// Token: 0x02000260 RID: 608
		public enum SDL_Folder
		{
			// Token: 0x04001389 RID: 5001
			SDL_FOLDER_HOME,
			// Token: 0x0400138A RID: 5002
			SDL_FOLDER_DESKTOP,
			// Token: 0x0400138B RID: 5003
			SDL_FOLDER_DOCUMENTS,
			// Token: 0x0400138C RID: 5004
			SDL_FOLDER_DOWNLOADS,
			// Token: 0x0400138D RID: 5005
			SDL_FOLDER_MUSIC,
			// Token: 0x0400138E RID: 5006
			SDL_FOLDER_PICTURES,
			// Token: 0x0400138F RID: 5007
			SDL_FOLDER_PUBLICSHARE,
			// Token: 0x04001390 RID: 5008
			SDL_FOLDER_SAVEDGAMES,
			// Token: 0x04001391 RID: 5009
			SDL_FOLDER_SCREENSHOTS,
			// Token: 0x04001392 RID: 5010
			SDL_FOLDER_TEMPLATES,
			// Token: 0x04001393 RID: 5011
			SDL_FOLDER_VIDEOS,
			// Token: 0x04001394 RID: 5012
			SDL_FOLDER_COUNT
		}

		// Token: 0x02000261 RID: 609
		public enum SDL_PathType
		{
			// Token: 0x04001396 RID: 5014
			SDL_PATHTYPE_NONE,
			// Token: 0x04001397 RID: 5015
			SDL_PATHTYPE_FILE,
			// Token: 0x04001398 RID: 5016
			SDL_PATHTYPE_DIRECTORY,
			// Token: 0x04001399 RID: 5017
			SDL_PATHTYPE_OTHER
		}

		// Token: 0x02000262 RID: 610
		public struct SDL_PathInfo
		{
			// Token: 0x0400139A RID: 5018
			public SDL.SDL_PathType type;

			// Token: 0x0400139B RID: 5019
			public ulong size;

			// Token: 0x0400139C RID: 5020
			public long create_time;

			// Token: 0x0400139D RID: 5021
			public long modify_time;

			// Token: 0x0400139E RID: 5022
			public long access_time;
		}

		// Token: 0x02000263 RID: 611
		[Flags]
		public enum SDL_GlobFlags : uint
		{
			// Token: 0x040013A0 RID: 5024
			SDL_GLOB_CASEINSENSITIVE = 1U
		}

		// Token: 0x02000264 RID: 612
		public enum SDL_EnumerationResult
		{
			// Token: 0x040013A2 RID: 5026
			SDL_ENUM_CONTINUE,
			// Token: 0x040013A3 RID: 5027
			SDL_ENUM_SUCCESS,
			// Token: 0x040013A4 RID: 5028
			SDL_ENUM_FAILURE
		}

		// Token: 0x02000265 RID: 613
		// (Invoke) Token: 0x06001968 RID: 6504
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate SDL.SDL_EnumerationResult SDL_EnumerateDirectoryCallback(IntPtr userdata, byte* dirname, byte* fname);

		// Token: 0x02000266 RID: 614
		public enum SDL_GPUPrimitiveType
		{
			// Token: 0x040013A6 RID: 5030
			SDL_GPU_PRIMITIVETYPE_TRIANGLELIST,
			// Token: 0x040013A7 RID: 5031
			SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP,
			// Token: 0x040013A8 RID: 5032
			SDL_GPU_PRIMITIVETYPE_LINELIST,
			// Token: 0x040013A9 RID: 5033
			SDL_GPU_PRIMITIVETYPE_LINESTRIP,
			// Token: 0x040013AA RID: 5034
			SDL_GPU_PRIMITIVETYPE_POINTLIST
		}

		// Token: 0x02000267 RID: 615
		public enum SDL_GPULoadOp
		{
			// Token: 0x040013AC RID: 5036
			SDL_GPU_LOADOP_LOAD,
			// Token: 0x040013AD RID: 5037
			SDL_GPU_LOADOP_CLEAR,
			// Token: 0x040013AE RID: 5038
			SDL_GPU_LOADOP_DONT_CARE
		}

		// Token: 0x02000268 RID: 616
		public enum SDL_GPUStoreOp
		{
			// Token: 0x040013B0 RID: 5040
			SDL_GPU_STOREOP_STORE,
			// Token: 0x040013B1 RID: 5041
			SDL_GPU_STOREOP_DONT_CARE,
			// Token: 0x040013B2 RID: 5042
			SDL_GPU_STOREOP_RESOLVE,
			// Token: 0x040013B3 RID: 5043
			SDL_GPU_STOREOP_RESOLVE_AND_STORE
		}

		// Token: 0x02000269 RID: 617
		public enum SDL_GPUIndexElementSize
		{
			// Token: 0x040013B5 RID: 5045
			SDL_GPU_INDEXELEMENTSIZE_16BIT,
			// Token: 0x040013B6 RID: 5046
			SDL_GPU_INDEXELEMENTSIZE_32BIT
		}

		// Token: 0x0200026A RID: 618
		public enum SDL_GPUTextureFormat
		{
			// Token: 0x040013B8 RID: 5048
			SDL_GPU_TEXTUREFORMAT_INVALID,
			// Token: 0x040013B9 RID: 5049
			SDL_GPU_TEXTUREFORMAT_A8_UNORM,
			// Token: 0x040013BA RID: 5050
			SDL_GPU_TEXTUREFORMAT_R8_UNORM,
			// Token: 0x040013BB RID: 5051
			SDL_GPU_TEXTUREFORMAT_R8G8_UNORM,
			// Token: 0x040013BC RID: 5052
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM,
			// Token: 0x040013BD RID: 5053
			SDL_GPU_TEXTUREFORMAT_R16_UNORM,
			// Token: 0x040013BE RID: 5054
			SDL_GPU_TEXTUREFORMAT_R16G16_UNORM,
			// Token: 0x040013BF RID: 5055
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UNORM,
			// Token: 0x040013C0 RID: 5056
			SDL_GPU_TEXTUREFORMAT_R10G10B10A2_UNORM,
			// Token: 0x040013C1 RID: 5057
			SDL_GPU_TEXTUREFORMAT_B5G6R5_UNORM,
			// Token: 0x040013C2 RID: 5058
			SDL_GPU_TEXTUREFORMAT_B5G5R5A1_UNORM,
			// Token: 0x040013C3 RID: 5059
			SDL_GPU_TEXTUREFORMAT_B4G4R4A4_UNORM,
			// Token: 0x040013C4 RID: 5060
			SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM,
			// Token: 0x040013C5 RID: 5061
			SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM,
			// Token: 0x040013C6 RID: 5062
			SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM,
			// Token: 0x040013C7 RID: 5063
			SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM,
			// Token: 0x040013C8 RID: 5064
			SDL_GPU_TEXTUREFORMAT_BC4_R_UNORM,
			// Token: 0x040013C9 RID: 5065
			SDL_GPU_TEXTUREFORMAT_BC5_RG_UNORM,
			// Token: 0x040013CA RID: 5066
			SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM,
			// Token: 0x040013CB RID: 5067
			SDL_GPU_TEXTUREFORMAT_BC6H_RGB_FLOAT,
			// Token: 0x040013CC RID: 5068
			SDL_GPU_TEXTUREFORMAT_BC6H_RGB_UFLOAT,
			// Token: 0x040013CD RID: 5069
			SDL_GPU_TEXTUREFORMAT_R8_SNORM,
			// Token: 0x040013CE RID: 5070
			SDL_GPU_TEXTUREFORMAT_R8G8_SNORM,
			// Token: 0x040013CF RID: 5071
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_SNORM,
			// Token: 0x040013D0 RID: 5072
			SDL_GPU_TEXTUREFORMAT_R16_SNORM,
			// Token: 0x040013D1 RID: 5073
			SDL_GPU_TEXTUREFORMAT_R16G16_SNORM,
			// Token: 0x040013D2 RID: 5074
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_SNORM,
			// Token: 0x040013D3 RID: 5075
			SDL_GPU_TEXTUREFORMAT_R16_FLOAT,
			// Token: 0x040013D4 RID: 5076
			SDL_GPU_TEXTUREFORMAT_R16G16_FLOAT,
			// Token: 0x040013D5 RID: 5077
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_FLOAT,
			// Token: 0x040013D6 RID: 5078
			SDL_GPU_TEXTUREFORMAT_R32_FLOAT,
			// Token: 0x040013D7 RID: 5079
			SDL_GPU_TEXTUREFORMAT_R32G32_FLOAT,
			// Token: 0x040013D8 RID: 5080
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_FLOAT,
			// Token: 0x040013D9 RID: 5081
			SDL_GPU_TEXTUREFORMAT_R11G11B10_UFLOAT,
			// Token: 0x040013DA RID: 5082
			SDL_GPU_TEXTUREFORMAT_R8_UINT,
			// Token: 0x040013DB RID: 5083
			SDL_GPU_TEXTUREFORMAT_R8G8_UINT,
			// Token: 0x040013DC RID: 5084
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UINT,
			// Token: 0x040013DD RID: 5085
			SDL_GPU_TEXTUREFORMAT_R16_UINT,
			// Token: 0x040013DE RID: 5086
			SDL_GPU_TEXTUREFORMAT_R16G16_UINT,
			// Token: 0x040013DF RID: 5087
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_UINT,
			// Token: 0x040013E0 RID: 5088
			SDL_GPU_TEXTUREFORMAT_R32_UINT,
			// Token: 0x040013E1 RID: 5089
			SDL_GPU_TEXTUREFORMAT_R32G32_UINT,
			// Token: 0x040013E2 RID: 5090
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_UINT,
			// Token: 0x040013E3 RID: 5091
			SDL_GPU_TEXTUREFORMAT_R8_INT,
			// Token: 0x040013E4 RID: 5092
			SDL_GPU_TEXTUREFORMAT_R8G8_INT,
			// Token: 0x040013E5 RID: 5093
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_INT,
			// Token: 0x040013E6 RID: 5094
			SDL_GPU_TEXTUREFORMAT_R16_INT,
			// Token: 0x040013E7 RID: 5095
			SDL_GPU_TEXTUREFORMAT_R16G16_INT,
			// Token: 0x040013E8 RID: 5096
			SDL_GPU_TEXTUREFORMAT_R16G16B16A16_INT,
			// Token: 0x040013E9 RID: 5097
			SDL_GPU_TEXTUREFORMAT_R32_INT,
			// Token: 0x040013EA RID: 5098
			SDL_GPU_TEXTUREFORMAT_R32G32_INT,
			// Token: 0x040013EB RID: 5099
			SDL_GPU_TEXTUREFORMAT_R32G32B32A32_INT,
			// Token: 0x040013EC RID: 5100
			SDL_GPU_TEXTUREFORMAT_R8G8B8A8_UNORM_SRGB,
			// Token: 0x040013ED RID: 5101
			SDL_GPU_TEXTUREFORMAT_B8G8R8A8_UNORM_SRGB,
			// Token: 0x040013EE RID: 5102
			SDL_GPU_TEXTUREFORMAT_BC1_RGBA_UNORM_SRGB,
			// Token: 0x040013EF RID: 5103
			SDL_GPU_TEXTUREFORMAT_BC2_RGBA_UNORM_SRGB,
			// Token: 0x040013F0 RID: 5104
			SDL_GPU_TEXTUREFORMAT_BC3_RGBA_UNORM_SRGB,
			// Token: 0x040013F1 RID: 5105
			SDL_GPU_TEXTUREFORMAT_BC7_RGBA_UNORM_SRGB,
			// Token: 0x040013F2 RID: 5106
			SDL_GPU_TEXTUREFORMAT_D16_UNORM,
			// Token: 0x040013F3 RID: 5107
			SDL_GPU_TEXTUREFORMAT_D24_UNORM,
			// Token: 0x040013F4 RID: 5108
			SDL_GPU_TEXTUREFORMAT_D32_FLOAT,
			// Token: 0x040013F5 RID: 5109
			SDL_GPU_TEXTUREFORMAT_D24_UNORM_S8_UINT,
			// Token: 0x040013F6 RID: 5110
			SDL_GPU_TEXTUREFORMAT_D32_FLOAT_S8_UINT,
			// Token: 0x040013F7 RID: 5111
			SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM,
			// Token: 0x040013F8 RID: 5112
			SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM,
			// Token: 0x040013F9 RID: 5113
			SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM,
			// Token: 0x040013FA RID: 5114
			SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM,
			// Token: 0x040013FB RID: 5115
			SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM,
			// Token: 0x040013FC RID: 5116
			SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM,
			// Token: 0x040013FD RID: 5117
			SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM,
			// Token: 0x040013FE RID: 5118
			SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM,
			// Token: 0x040013FF RID: 5119
			SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM,
			// Token: 0x04001400 RID: 5120
			SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM,
			// Token: 0x04001401 RID: 5121
			SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM,
			// Token: 0x04001402 RID: 5122
			SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM,
			// Token: 0x04001403 RID: 5123
			SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM,
			// Token: 0x04001404 RID: 5124
			SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM,
			// Token: 0x04001405 RID: 5125
			SDL_GPU_TEXTUREFORMAT_ASTC_4x4_UNORM_SRGB,
			// Token: 0x04001406 RID: 5126
			SDL_GPU_TEXTUREFORMAT_ASTC_5x4_UNORM_SRGB,
			// Token: 0x04001407 RID: 5127
			SDL_GPU_TEXTUREFORMAT_ASTC_5x5_UNORM_SRGB,
			// Token: 0x04001408 RID: 5128
			SDL_GPU_TEXTUREFORMAT_ASTC_6x5_UNORM_SRGB,
			// Token: 0x04001409 RID: 5129
			SDL_GPU_TEXTUREFORMAT_ASTC_6x6_UNORM_SRGB,
			// Token: 0x0400140A RID: 5130
			SDL_GPU_TEXTUREFORMAT_ASTC_8x5_UNORM_SRGB,
			// Token: 0x0400140B RID: 5131
			SDL_GPU_TEXTUREFORMAT_ASTC_8x6_UNORM_SRGB,
			// Token: 0x0400140C RID: 5132
			SDL_GPU_TEXTUREFORMAT_ASTC_8x8_UNORM_SRGB,
			// Token: 0x0400140D RID: 5133
			SDL_GPU_TEXTUREFORMAT_ASTC_10x5_UNORM_SRGB,
			// Token: 0x0400140E RID: 5134
			SDL_GPU_TEXTUREFORMAT_ASTC_10x6_UNORM_SRGB,
			// Token: 0x0400140F RID: 5135
			SDL_GPU_TEXTUREFORMAT_ASTC_10x8_UNORM_SRGB,
			// Token: 0x04001410 RID: 5136
			SDL_GPU_TEXTUREFORMAT_ASTC_10x10_UNORM_SRGB,
			// Token: 0x04001411 RID: 5137
			SDL_GPU_TEXTUREFORMAT_ASTC_12x10_UNORM_SRGB,
			// Token: 0x04001412 RID: 5138
			SDL_GPU_TEXTUREFORMAT_ASTC_12x12_UNORM_SRGB,
			// Token: 0x04001413 RID: 5139
			SDL_GPU_TEXTUREFORMAT_ASTC_4x4_FLOAT,
			// Token: 0x04001414 RID: 5140
			SDL_GPU_TEXTUREFORMAT_ASTC_5x4_FLOAT,
			// Token: 0x04001415 RID: 5141
			SDL_GPU_TEXTUREFORMAT_ASTC_5x5_FLOAT,
			// Token: 0x04001416 RID: 5142
			SDL_GPU_TEXTUREFORMAT_ASTC_6x5_FLOAT,
			// Token: 0x04001417 RID: 5143
			SDL_GPU_TEXTUREFORMAT_ASTC_6x6_FLOAT,
			// Token: 0x04001418 RID: 5144
			SDL_GPU_TEXTUREFORMAT_ASTC_8x5_FLOAT,
			// Token: 0x04001419 RID: 5145
			SDL_GPU_TEXTUREFORMAT_ASTC_8x6_FLOAT,
			// Token: 0x0400141A RID: 5146
			SDL_GPU_TEXTUREFORMAT_ASTC_8x8_FLOAT,
			// Token: 0x0400141B RID: 5147
			SDL_GPU_TEXTUREFORMAT_ASTC_10x5_FLOAT,
			// Token: 0x0400141C RID: 5148
			SDL_GPU_TEXTUREFORMAT_ASTC_10x6_FLOAT,
			// Token: 0x0400141D RID: 5149
			SDL_GPU_TEXTUREFORMAT_ASTC_10x8_FLOAT,
			// Token: 0x0400141E RID: 5150
			SDL_GPU_TEXTUREFORMAT_ASTC_10x10_FLOAT,
			// Token: 0x0400141F RID: 5151
			SDL_GPU_TEXTUREFORMAT_ASTC_12x10_FLOAT,
			// Token: 0x04001420 RID: 5152
			SDL_GPU_TEXTUREFORMAT_ASTC_12x12_FLOAT
		}

		// Token: 0x0200026B RID: 619
		[Flags]
		public enum SDL_GPUTextureUsageFlags : uint
		{
			// Token: 0x04001422 RID: 5154
			SDL_GPU_TEXTUREUSAGE_SAMPLER = 1U,
			// Token: 0x04001423 RID: 5155
			SDL_GPU_TEXTUREUSAGE_COLOR_TARGET = 2U,
			// Token: 0x04001424 RID: 5156
			SDL_GPU_TEXTUREUSAGE_DEPTH_STENCIL_TARGET = 4U,
			// Token: 0x04001425 RID: 5157
			SDL_GPU_TEXTUREUSAGE_GRAPHICS_STORAGE_READ = 8U,
			// Token: 0x04001426 RID: 5158
			SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_READ = 16U,
			// Token: 0x04001427 RID: 5159
			SDL_GPU_TEXTUREUSAGE_COMPUTE_STORAGE_WRITE = 32U
		}

		// Token: 0x0200026C RID: 620
		public enum SDL_GPUTextureType
		{
			// Token: 0x04001429 RID: 5161
			SDL_GPU_TEXTURETYPE_2D,
			// Token: 0x0400142A RID: 5162
			SDL_GPU_TEXTURETYPE_2D_ARRAY,
			// Token: 0x0400142B RID: 5163
			SDL_GPU_TEXTURETYPE_3D,
			// Token: 0x0400142C RID: 5164
			SDL_GPU_TEXTURETYPE_CUBE,
			// Token: 0x0400142D RID: 5165
			SDL_GPU_TEXTURETYPE_CUBE_ARRAY
		}

		// Token: 0x0200026D RID: 621
		public enum SDL_GPUSampleCount
		{
			// Token: 0x0400142F RID: 5167
			SDL_GPU_SAMPLECOUNT_1,
			// Token: 0x04001430 RID: 5168
			SDL_GPU_SAMPLECOUNT_2,
			// Token: 0x04001431 RID: 5169
			SDL_GPU_SAMPLECOUNT_4,
			// Token: 0x04001432 RID: 5170
			SDL_GPU_SAMPLECOUNT_8
		}

		// Token: 0x0200026E RID: 622
		public enum SDL_GPUCubeMapFace
		{
			// Token: 0x04001434 RID: 5172
			SDL_GPU_CUBEMAPFACE_POSITIVEX,
			// Token: 0x04001435 RID: 5173
			SDL_GPU_CUBEMAPFACE_NEGATIVEX,
			// Token: 0x04001436 RID: 5174
			SDL_GPU_CUBEMAPFACE_POSITIVEY,
			// Token: 0x04001437 RID: 5175
			SDL_GPU_CUBEMAPFACE_NEGATIVEY,
			// Token: 0x04001438 RID: 5176
			SDL_GPU_CUBEMAPFACE_POSITIVEZ,
			// Token: 0x04001439 RID: 5177
			SDL_GPU_CUBEMAPFACE_NEGATIVEZ
		}

		// Token: 0x0200026F RID: 623
		[Flags]
		public enum SDL_GPUBufferUsageFlags : uint
		{
			// Token: 0x0400143B RID: 5179
			SDL_GPU_BUFFERUSAGE_VERTEX = 1U,
			// Token: 0x0400143C RID: 5180
			SDL_GPU_BUFFERUSAGE_INDEX = 2U,
			// Token: 0x0400143D RID: 5181
			SDL_GPU_BUFFERUSAGE_INDIRECT = 4U,
			// Token: 0x0400143E RID: 5182
			SDL_GPU_BUFFERUSAGE_GRAPHICS_STORAGE_READ = 8U,
			// Token: 0x0400143F RID: 5183
			SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_READ = 16U,
			// Token: 0x04001440 RID: 5184
			SDL_GPU_BUFFERUSAGE_COMPUTE_STORAGE_WRITE = 32U
		}

		// Token: 0x02000270 RID: 624
		public enum SDL_GPUTransferBufferUsage
		{
			// Token: 0x04001442 RID: 5186
			SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD,
			// Token: 0x04001443 RID: 5187
			SDL_GPU_TRANSFERBUFFERUSAGE_DOWNLOAD
		}

		// Token: 0x02000271 RID: 625
		public enum SDL_GPUShaderStage
		{
			// Token: 0x04001445 RID: 5189
			SDL_GPU_SHADERSTAGE_VERTEX,
			// Token: 0x04001446 RID: 5190
			SDL_GPU_SHADERSTAGE_FRAGMENT
		}

		// Token: 0x02000272 RID: 626
		[Flags]
		public enum SDL_GPUShaderFormat : uint
		{
			// Token: 0x04001448 RID: 5192
			SDL_GPU_SHADERFORMAT_PRIVATE = 1U,
			// Token: 0x04001449 RID: 5193
			SDL_GPU_SHADERFORMAT_SPIRV = 2U,
			// Token: 0x0400144A RID: 5194
			SDL_GPU_SHADERFORMAT_DXBC = 4U,
			// Token: 0x0400144B RID: 5195
			SDL_GPU_SHADERFORMAT_DXIL = 8U,
			// Token: 0x0400144C RID: 5196
			SDL_GPU_SHADERFORMAT_MSL = 16U,
			// Token: 0x0400144D RID: 5197
			SDL_GPU_SHADERFORMAT_METALLIB = 32U
		}

		// Token: 0x02000273 RID: 627
		public enum SDL_GPUVertexElementFormat
		{
			// Token: 0x0400144F RID: 5199
			SDL_GPU_VERTEXELEMENTFORMAT_INVALID,
			// Token: 0x04001450 RID: 5200
			SDL_GPU_VERTEXELEMENTFORMAT_INT,
			// Token: 0x04001451 RID: 5201
			SDL_GPU_VERTEXELEMENTFORMAT_INT2,
			// Token: 0x04001452 RID: 5202
			SDL_GPU_VERTEXELEMENTFORMAT_INT3,
			// Token: 0x04001453 RID: 5203
			SDL_GPU_VERTEXELEMENTFORMAT_INT4,
			// Token: 0x04001454 RID: 5204
			SDL_GPU_VERTEXELEMENTFORMAT_UINT,
			// Token: 0x04001455 RID: 5205
			SDL_GPU_VERTEXELEMENTFORMAT_UINT2,
			// Token: 0x04001456 RID: 5206
			SDL_GPU_VERTEXELEMENTFORMAT_UINT3,
			// Token: 0x04001457 RID: 5207
			SDL_GPU_VERTEXELEMENTFORMAT_UINT4,
			// Token: 0x04001458 RID: 5208
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT,
			// Token: 0x04001459 RID: 5209
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
			// Token: 0x0400145A RID: 5210
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
			// Token: 0x0400145B RID: 5211
			SDL_GPU_VERTEXELEMENTFORMAT_FLOAT4,
			// Token: 0x0400145C RID: 5212
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE2,
			// Token: 0x0400145D RID: 5213
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE4,
			// Token: 0x0400145E RID: 5214
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2,
			// Token: 0x0400145F RID: 5215
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4,
			// Token: 0x04001460 RID: 5216
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE2_NORM,
			// Token: 0x04001461 RID: 5217
			SDL_GPU_VERTEXELEMENTFORMAT_BYTE4_NORM,
			// Token: 0x04001462 RID: 5218
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE2_NORM,
			// Token: 0x04001463 RID: 5219
			SDL_GPU_VERTEXELEMENTFORMAT_UBYTE4_NORM,
			// Token: 0x04001464 RID: 5220
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT2,
			// Token: 0x04001465 RID: 5221
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT4,
			// Token: 0x04001466 RID: 5222
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT2,
			// Token: 0x04001467 RID: 5223
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT4,
			// Token: 0x04001468 RID: 5224
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT2_NORM,
			// Token: 0x04001469 RID: 5225
			SDL_GPU_VERTEXELEMENTFORMAT_SHORT4_NORM,
			// Token: 0x0400146A RID: 5226
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT2_NORM,
			// Token: 0x0400146B RID: 5227
			SDL_GPU_VERTEXELEMENTFORMAT_USHORT4_NORM,
			// Token: 0x0400146C RID: 5228
			SDL_GPU_VERTEXELEMENTFORMAT_HALF2,
			// Token: 0x0400146D RID: 5229
			SDL_GPU_VERTEXELEMENTFORMAT_HALF4
		}

		// Token: 0x02000274 RID: 628
		public enum SDL_GPUVertexInputRate
		{
			// Token: 0x0400146F RID: 5231
			SDL_GPU_VERTEXINPUTRATE_VERTEX,
			// Token: 0x04001470 RID: 5232
			SDL_GPU_VERTEXINPUTRATE_INSTANCE
		}

		// Token: 0x02000275 RID: 629
		public enum SDL_GPUFillMode
		{
			// Token: 0x04001472 RID: 5234
			SDL_GPU_FILLMODE_FILL,
			// Token: 0x04001473 RID: 5235
			SDL_GPU_FILLMODE_LINE
		}

		// Token: 0x02000276 RID: 630
		public enum SDL_GPUCullMode
		{
			// Token: 0x04001475 RID: 5237
			SDL_GPU_CULLMODE_NONE,
			// Token: 0x04001476 RID: 5238
			SDL_GPU_CULLMODE_FRONT,
			// Token: 0x04001477 RID: 5239
			SDL_GPU_CULLMODE_BACK
		}

		// Token: 0x02000277 RID: 631
		public enum SDL_GPUFrontFace
		{
			// Token: 0x04001479 RID: 5241
			SDL_GPU_FRONTFACE_COUNTER_CLOCKWISE,
			// Token: 0x0400147A RID: 5242
			SDL_GPU_FRONTFACE_CLOCKWISE
		}

		// Token: 0x02000278 RID: 632
		public enum SDL_GPUCompareOp
		{
			// Token: 0x0400147C RID: 5244
			SDL_GPU_COMPAREOP_INVALID,
			// Token: 0x0400147D RID: 5245
			SDL_GPU_COMPAREOP_NEVER,
			// Token: 0x0400147E RID: 5246
			SDL_GPU_COMPAREOP_LESS,
			// Token: 0x0400147F RID: 5247
			SDL_GPU_COMPAREOP_EQUAL,
			// Token: 0x04001480 RID: 5248
			SDL_GPU_COMPAREOP_LESS_OR_EQUAL,
			// Token: 0x04001481 RID: 5249
			SDL_GPU_COMPAREOP_GREATER,
			// Token: 0x04001482 RID: 5250
			SDL_GPU_COMPAREOP_NOT_EQUAL,
			// Token: 0x04001483 RID: 5251
			SDL_GPU_COMPAREOP_GREATER_OR_EQUAL,
			// Token: 0x04001484 RID: 5252
			SDL_GPU_COMPAREOP_ALWAYS
		}

		// Token: 0x02000279 RID: 633
		public enum SDL_GPUStencilOp
		{
			// Token: 0x04001486 RID: 5254
			SDL_GPU_STENCILOP_INVALID,
			// Token: 0x04001487 RID: 5255
			SDL_GPU_STENCILOP_KEEP,
			// Token: 0x04001488 RID: 5256
			SDL_GPU_STENCILOP_ZERO,
			// Token: 0x04001489 RID: 5257
			SDL_GPU_STENCILOP_REPLACE,
			// Token: 0x0400148A RID: 5258
			SDL_GPU_STENCILOP_INCREMENT_AND_CLAMP,
			// Token: 0x0400148B RID: 5259
			SDL_GPU_STENCILOP_DECREMENT_AND_CLAMP,
			// Token: 0x0400148C RID: 5260
			SDL_GPU_STENCILOP_INVERT,
			// Token: 0x0400148D RID: 5261
			SDL_GPU_STENCILOP_INCREMENT_AND_WRAP,
			// Token: 0x0400148E RID: 5262
			SDL_GPU_STENCILOP_DECREMENT_AND_WRAP
		}

		// Token: 0x0200027A RID: 634
		public enum SDL_GPUBlendOp
		{
			// Token: 0x04001490 RID: 5264
			SDL_GPU_BLENDOP_INVALID,
			// Token: 0x04001491 RID: 5265
			SDL_GPU_BLENDOP_ADD,
			// Token: 0x04001492 RID: 5266
			SDL_GPU_BLENDOP_SUBTRACT,
			// Token: 0x04001493 RID: 5267
			SDL_GPU_BLENDOP_REVERSE_SUBTRACT,
			// Token: 0x04001494 RID: 5268
			SDL_GPU_BLENDOP_MIN,
			// Token: 0x04001495 RID: 5269
			SDL_GPU_BLENDOP_MAX
		}

		// Token: 0x0200027B RID: 635
		public enum SDL_GPUBlendFactor
		{
			// Token: 0x04001497 RID: 5271
			SDL_GPU_BLENDFACTOR_INVALID,
			// Token: 0x04001498 RID: 5272
			SDL_GPU_BLENDFACTOR_ZERO,
			// Token: 0x04001499 RID: 5273
			SDL_GPU_BLENDFACTOR_ONE,
			// Token: 0x0400149A RID: 5274
			SDL_GPU_BLENDFACTOR_SRC_COLOR,
			// Token: 0x0400149B RID: 5275
			SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_COLOR,
			// Token: 0x0400149C RID: 5276
			SDL_GPU_BLENDFACTOR_DST_COLOR,
			// Token: 0x0400149D RID: 5277
			SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_COLOR,
			// Token: 0x0400149E RID: 5278
			SDL_GPU_BLENDFACTOR_SRC_ALPHA,
			// Token: 0x0400149F RID: 5279
			SDL_GPU_BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
			// Token: 0x040014A0 RID: 5280
			SDL_GPU_BLENDFACTOR_DST_ALPHA,
			// Token: 0x040014A1 RID: 5281
			SDL_GPU_BLENDFACTOR_ONE_MINUS_DST_ALPHA,
			// Token: 0x040014A2 RID: 5282
			SDL_GPU_BLENDFACTOR_CONSTANT_COLOR,
			// Token: 0x040014A3 RID: 5283
			SDL_GPU_BLENDFACTOR_ONE_MINUS_CONSTANT_COLOR,
			// Token: 0x040014A4 RID: 5284
			SDL_GPU_BLENDFACTOR_SRC_ALPHA_SATURATE
		}

		// Token: 0x0200027C RID: 636
		[Flags]
		public enum SDL_GPUColorComponentFlags : byte
		{
			// Token: 0x040014A6 RID: 5286
			SDL_GPU_COLORCOMPONENT_R = 1,
			// Token: 0x040014A7 RID: 5287
			SDL_GPU_COLORCOMPONENT_G = 2,
			// Token: 0x040014A8 RID: 5288
			SDL_GPU_COLORCOMPONENT_B = 4,
			// Token: 0x040014A9 RID: 5289
			SDL_GPU_COLORCOMPONENT_A = 8
		}

		// Token: 0x0200027D RID: 637
		public enum SDL_GPUFilter
		{
			// Token: 0x040014AB RID: 5291
			SDL_GPU_FILTER_NEAREST,
			// Token: 0x040014AC RID: 5292
			SDL_GPU_FILTER_LINEAR
		}

		// Token: 0x0200027E RID: 638
		public enum SDL_GPUSamplerMipmapMode
		{
			// Token: 0x040014AE RID: 5294
			SDL_GPU_SAMPLERMIPMAPMODE_NEAREST,
			// Token: 0x040014AF RID: 5295
			SDL_GPU_SAMPLERMIPMAPMODE_LINEAR
		}

		// Token: 0x0200027F RID: 639
		public enum SDL_GPUSamplerAddressMode
		{
			// Token: 0x040014B1 RID: 5297
			SDL_GPU_SAMPLERADDRESSMODE_REPEAT,
			// Token: 0x040014B2 RID: 5298
			SDL_GPU_SAMPLERADDRESSMODE_MIRRORED_REPEAT,
			// Token: 0x040014B3 RID: 5299
			SDL_GPU_SAMPLERADDRESSMODE_CLAMP_TO_EDGE
		}

		// Token: 0x02000280 RID: 640
		public enum SDL_GPUPresentMode
		{
			// Token: 0x040014B5 RID: 5301
			SDL_GPU_PRESENTMODE_VSYNC,
			// Token: 0x040014B6 RID: 5302
			SDL_GPU_PRESENTMODE_IMMEDIATE,
			// Token: 0x040014B7 RID: 5303
			SDL_GPU_PRESENTMODE_MAILBOX
		}

		// Token: 0x02000281 RID: 641
		public enum SDL_GPUSwapchainComposition
		{
			// Token: 0x040014B9 RID: 5305
			SDL_GPU_SWAPCHAINCOMPOSITION_SDR,
			// Token: 0x040014BA RID: 5306
			SDL_GPU_SWAPCHAINCOMPOSITION_SDR_LINEAR,
			// Token: 0x040014BB RID: 5307
			SDL_GPU_SWAPCHAINCOMPOSITION_HDR_EXTENDED_LINEAR,
			// Token: 0x040014BC RID: 5308
			SDL_GPU_SWAPCHAINCOMPOSITION_HDR10_ST2084
		}

		// Token: 0x02000282 RID: 642
		public struct SDL_GPUViewport
		{
			// Token: 0x040014BD RID: 5309
			public float x;

			// Token: 0x040014BE RID: 5310
			public float y;

			// Token: 0x040014BF RID: 5311
			public float w;

			// Token: 0x040014C0 RID: 5312
			public float h;

			// Token: 0x040014C1 RID: 5313
			public float min_depth;

			// Token: 0x040014C2 RID: 5314
			public float max_depth;
		}

		// Token: 0x02000283 RID: 643
		public struct SDL_GPUTextureTransferInfo
		{
			// Token: 0x040014C3 RID: 5315
			public IntPtr transfer_buffer;

			// Token: 0x040014C4 RID: 5316
			public uint offset;

			// Token: 0x040014C5 RID: 5317
			public uint pixels_per_row;

			// Token: 0x040014C6 RID: 5318
			public uint rows_per_layer;
		}

		// Token: 0x02000284 RID: 644
		public struct SDL_GPUTransferBufferLocation
		{
			// Token: 0x040014C7 RID: 5319
			public IntPtr transfer_buffer;

			// Token: 0x040014C8 RID: 5320
			public uint offset;
		}

		// Token: 0x02000285 RID: 645
		public struct SDL_GPUTextureLocation
		{
			// Token: 0x040014C9 RID: 5321
			public IntPtr texture;

			// Token: 0x040014CA RID: 5322
			public uint mip_level;

			// Token: 0x040014CB RID: 5323
			public uint layer;

			// Token: 0x040014CC RID: 5324
			public uint x;

			// Token: 0x040014CD RID: 5325
			public uint y;

			// Token: 0x040014CE RID: 5326
			public uint z;
		}

		// Token: 0x02000286 RID: 646
		public struct SDL_GPUTextureRegion
		{
			// Token: 0x040014CF RID: 5327
			public IntPtr texture;

			// Token: 0x040014D0 RID: 5328
			public uint mip_level;

			// Token: 0x040014D1 RID: 5329
			public uint layer;

			// Token: 0x040014D2 RID: 5330
			public uint x;

			// Token: 0x040014D3 RID: 5331
			public uint y;

			// Token: 0x040014D4 RID: 5332
			public uint z;

			// Token: 0x040014D5 RID: 5333
			public uint w;

			// Token: 0x040014D6 RID: 5334
			public uint h;

			// Token: 0x040014D7 RID: 5335
			public uint d;
		}

		// Token: 0x02000287 RID: 647
		public struct SDL_GPUBlitRegion
		{
			// Token: 0x040014D8 RID: 5336
			public IntPtr texture;

			// Token: 0x040014D9 RID: 5337
			public uint mip_level;

			// Token: 0x040014DA RID: 5338
			public uint layer_or_depth_plane;

			// Token: 0x040014DB RID: 5339
			public uint x;

			// Token: 0x040014DC RID: 5340
			public uint y;

			// Token: 0x040014DD RID: 5341
			public uint w;

			// Token: 0x040014DE RID: 5342
			public uint h;
		}

		// Token: 0x02000288 RID: 648
		public struct SDL_GPUBufferLocation
		{
			// Token: 0x040014DF RID: 5343
			public IntPtr buffer;

			// Token: 0x040014E0 RID: 5344
			public uint offset;
		}

		// Token: 0x02000289 RID: 649
		public struct SDL_GPUBufferRegion
		{
			// Token: 0x040014E1 RID: 5345
			public IntPtr buffer;

			// Token: 0x040014E2 RID: 5346
			public uint offset;

			// Token: 0x040014E3 RID: 5347
			public uint size;
		}

		// Token: 0x0200028A RID: 650
		public struct SDL_GPUIndirectDrawCommand
		{
			// Token: 0x040014E4 RID: 5348
			public uint num_vertices;

			// Token: 0x040014E5 RID: 5349
			public uint num_instances;

			// Token: 0x040014E6 RID: 5350
			public uint first_vertex;

			// Token: 0x040014E7 RID: 5351
			public uint first_instance;
		}

		// Token: 0x0200028B RID: 651
		public struct SDL_GPUIndexedIndirectDrawCommand
		{
			// Token: 0x040014E8 RID: 5352
			public uint num_indices;

			// Token: 0x040014E9 RID: 5353
			public uint num_instances;

			// Token: 0x040014EA RID: 5354
			public uint first_index;

			// Token: 0x040014EB RID: 5355
			public int vertex_offset;

			// Token: 0x040014EC RID: 5356
			public uint first_instance;
		}

		// Token: 0x0200028C RID: 652
		public struct SDL_GPUIndirectDispatchCommand
		{
			// Token: 0x040014ED RID: 5357
			public uint groupcount_x;

			// Token: 0x040014EE RID: 5358
			public uint groupcount_y;

			// Token: 0x040014EF RID: 5359
			public uint groupcount_z;
		}

		// Token: 0x0200028D RID: 653
		public struct SDL_GPUSamplerCreateInfo
		{
			// Token: 0x040014F0 RID: 5360
			public SDL.SDL_GPUFilter min_filter;

			// Token: 0x040014F1 RID: 5361
			public SDL.SDL_GPUFilter mag_filter;

			// Token: 0x040014F2 RID: 5362
			public SDL.SDL_GPUSamplerMipmapMode mipmap_mode;

			// Token: 0x040014F3 RID: 5363
			public SDL.SDL_GPUSamplerAddressMode address_mode_u;

			// Token: 0x040014F4 RID: 5364
			public SDL.SDL_GPUSamplerAddressMode address_mode_v;

			// Token: 0x040014F5 RID: 5365
			public SDL.SDL_GPUSamplerAddressMode address_mode_w;

			// Token: 0x040014F6 RID: 5366
			public float mip_lod_bias;

			// Token: 0x040014F7 RID: 5367
			public float max_anisotropy;

			// Token: 0x040014F8 RID: 5368
			public SDL.SDL_GPUCompareOp compare_op;

			// Token: 0x040014F9 RID: 5369
			public float min_lod;

			// Token: 0x040014FA RID: 5370
			public float max_lod;

			// Token: 0x040014FB RID: 5371
			public SDL.SDLBool enable_anisotropy;

			// Token: 0x040014FC RID: 5372
			public SDL.SDLBool enable_compare;

			// Token: 0x040014FD RID: 5373
			public byte padding1;

			// Token: 0x040014FE RID: 5374
			public byte padding2;

			// Token: 0x040014FF RID: 5375
			public uint props;
		}

		// Token: 0x0200028E RID: 654
		public struct SDL_GPUVertexBufferDescription
		{
			// Token: 0x04001500 RID: 5376
			public uint slot;

			// Token: 0x04001501 RID: 5377
			public uint pitch;

			// Token: 0x04001502 RID: 5378
			public SDL.SDL_GPUVertexInputRate input_rate;

			// Token: 0x04001503 RID: 5379
			public uint instance_step_rate;
		}

		// Token: 0x0200028F RID: 655
		public struct SDL_GPUVertexAttribute
		{
			// Token: 0x04001504 RID: 5380
			public uint location;

			// Token: 0x04001505 RID: 5381
			public uint buffer_slot;

			// Token: 0x04001506 RID: 5382
			public SDL.SDL_GPUVertexElementFormat format;

			// Token: 0x04001507 RID: 5383
			public uint offset;
		}

		// Token: 0x02000290 RID: 656
		public struct SDL_GPUVertexInputState
		{
			// Token: 0x04001508 RID: 5384
			public unsafe SDL.SDL_GPUVertexBufferDescription* vertex_buffer_descriptions;

			// Token: 0x04001509 RID: 5385
			public uint num_vertex_buffers;

			// Token: 0x0400150A RID: 5386
			public unsafe SDL.SDL_GPUVertexAttribute* vertex_attributes;

			// Token: 0x0400150B RID: 5387
			public uint num_vertex_attributes;
		}

		// Token: 0x02000291 RID: 657
		public struct SDL_GPUStencilOpState
		{
			// Token: 0x0400150C RID: 5388
			public SDL.SDL_GPUStencilOp fail_op;

			// Token: 0x0400150D RID: 5389
			public SDL.SDL_GPUStencilOp pass_op;

			// Token: 0x0400150E RID: 5390
			public SDL.SDL_GPUStencilOp depth_fail_op;

			// Token: 0x0400150F RID: 5391
			public SDL.SDL_GPUCompareOp compare_op;
		}

		// Token: 0x02000292 RID: 658
		public struct SDL_GPUColorTargetBlendState
		{
			// Token: 0x04001510 RID: 5392
			public SDL.SDL_GPUBlendFactor src_color_blendfactor;

			// Token: 0x04001511 RID: 5393
			public SDL.SDL_GPUBlendFactor dst_color_blendfactor;

			// Token: 0x04001512 RID: 5394
			public SDL.SDL_GPUBlendOp color_blend_op;

			// Token: 0x04001513 RID: 5395
			public SDL.SDL_GPUBlendFactor src_alpha_blendfactor;

			// Token: 0x04001514 RID: 5396
			public SDL.SDL_GPUBlendFactor dst_alpha_blendfactor;

			// Token: 0x04001515 RID: 5397
			public SDL.SDL_GPUBlendOp alpha_blend_op;

			// Token: 0x04001516 RID: 5398
			public SDL.SDL_GPUColorComponentFlags color_write_mask;

			// Token: 0x04001517 RID: 5399
			public SDL.SDLBool enable_blend;

			// Token: 0x04001518 RID: 5400
			public SDL.SDLBool enable_color_write_mask;

			// Token: 0x04001519 RID: 5401
			public byte padding1;

			// Token: 0x0400151A RID: 5402
			public byte padding2;
		}

		// Token: 0x02000293 RID: 659
		public struct SDL_GPUShaderCreateInfo
		{
			// Token: 0x0400151B RID: 5403
			public UIntPtr code_size;

			// Token: 0x0400151C RID: 5404
			public unsafe byte* code;

			// Token: 0x0400151D RID: 5405
			public unsafe byte* entrypoint;

			// Token: 0x0400151E RID: 5406
			public SDL.SDL_GPUShaderFormat format;

			// Token: 0x0400151F RID: 5407
			public SDL.SDL_GPUShaderStage stage;

			// Token: 0x04001520 RID: 5408
			public uint num_samplers;

			// Token: 0x04001521 RID: 5409
			public uint num_storage_textures;

			// Token: 0x04001522 RID: 5410
			public uint num_storage_buffers;

			// Token: 0x04001523 RID: 5411
			public uint num_uniform_buffers;

			// Token: 0x04001524 RID: 5412
			public uint props;
		}

		// Token: 0x02000294 RID: 660
		public struct SDL_GPUTextureCreateInfo
		{
			// Token: 0x04001525 RID: 5413
			public SDL.SDL_GPUTextureType type;

			// Token: 0x04001526 RID: 5414
			public SDL.SDL_GPUTextureFormat format;

			// Token: 0x04001527 RID: 5415
			public SDL.SDL_GPUTextureUsageFlags usage;

			// Token: 0x04001528 RID: 5416
			public uint width;

			// Token: 0x04001529 RID: 5417
			public uint height;

			// Token: 0x0400152A RID: 5418
			public uint layer_count_or_depth;

			// Token: 0x0400152B RID: 5419
			public uint num_levels;

			// Token: 0x0400152C RID: 5420
			public SDL.SDL_GPUSampleCount sample_count;

			// Token: 0x0400152D RID: 5421
			public uint props;
		}

		// Token: 0x02000295 RID: 661
		public struct SDL_GPUBufferCreateInfo
		{
			// Token: 0x0400152E RID: 5422
			public SDL.SDL_GPUBufferUsageFlags usage;

			// Token: 0x0400152F RID: 5423
			public uint size;

			// Token: 0x04001530 RID: 5424
			public uint props;
		}

		// Token: 0x02000296 RID: 662
		public struct SDL_GPUTransferBufferCreateInfo
		{
			// Token: 0x04001531 RID: 5425
			public SDL.SDL_GPUTransferBufferUsage usage;

			// Token: 0x04001532 RID: 5426
			public uint size;

			// Token: 0x04001533 RID: 5427
			public uint props;
		}

		// Token: 0x02000297 RID: 663
		public struct SDL_GPURasterizerState
		{
			// Token: 0x04001534 RID: 5428
			public SDL.SDL_GPUFillMode fill_mode;

			// Token: 0x04001535 RID: 5429
			public SDL.SDL_GPUCullMode cull_mode;

			// Token: 0x04001536 RID: 5430
			public SDL.SDL_GPUFrontFace front_face;

			// Token: 0x04001537 RID: 5431
			public float depth_bias_constant_factor;

			// Token: 0x04001538 RID: 5432
			public float depth_bias_clamp;

			// Token: 0x04001539 RID: 5433
			public float depth_bias_slope_factor;

			// Token: 0x0400153A RID: 5434
			public SDL.SDLBool enable_depth_bias;

			// Token: 0x0400153B RID: 5435
			public SDL.SDLBool enable_depth_clip;

			// Token: 0x0400153C RID: 5436
			public byte padding1;

			// Token: 0x0400153D RID: 5437
			public byte padding2;
		}

		// Token: 0x02000298 RID: 664
		public struct SDL_GPUMultisampleState
		{
			// Token: 0x0400153E RID: 5438
			public SDL.SDL_GPUSampleCount sample_count;

			// Token: 0x0400153F RID: 5439
			public uint sample_mask;

			// Token: 0x04001540 RID: 5440
			public SDL.SDLBool enable_mask;

			// Token: 0x04001541 RID: 5441
			public SDL.SDLBool enable_alpha_to_coverage;

			// Token: 0x04001542 RID: 5442
			public byte padding2;

			// Token: 0x04001543 RID: 5443
			public byte padding3;
		}

		// Token: 0x02000299 RID: 665
		public struct SDL_GPUDepthStencilState
		{
			// Token: 0x04001544 RID: 5444
			public SDL.SDL_GPUCompareOp compare_op;

			// Token: 0x04001545 RID: 5445
			public SDL.SDL_GPUStencilOpState back_stencil_state;

			// Token: 0x04001546 RID: 5446
			public SDL.SDL_GPUStencilOpState front_stencil_state;

			// Token: 0x04001547 RID: 5447
			public byte compare_mask;

			// Token: 0x04001548 RID: 5448
			public byte write_mask;

			// Token: 0x04001549 RID: 5449
			public SDL.SDLBool enable_depth_test;

			// Token: 0x0400154A RID: 5450
			public SDL.SDLBool enable_depth_write;

			// Token: 0x0400154B RID: 5451
			public SDL.SDLBool enable_stencil_test;

			// Token: 0x0400154C RID: 5452
			public byte padding1;

			// Token: 0x0400154D RID: 5453
			public byte padding2;

			// Token: 0x0400154E RID: 5454
			public byte padding3;
		}

		// Token: 0x0200029A RID: 666
		public struct SDL_GPUColorTargetDescription
		{
			// Token: 0x0400154F RID: 5455
			public SDL.SDL_GPUTextureFormat format;

			// Token: 0x04001550 RID: 5456
			public SDL.SDL_GPUColorTargetBlendState blend_state;
		}

		// Token: 0x0200029B RID: 667
		public struct SDL_GPUGraphicsPipelineTargetInfo
		{
			// Token: 0x04001551 RID: 5457
			public unsafe SDL.SDL_GPUColorTargetDescription* color_target_descriptions;

			// Token: 0x04001552 RID: 5458
			public uint num_color_targets;

			// Token: 0x04001553 RID: 5459
			public SDL.SDL_GPUTextureFormat depth_stencil_format;

			// Token: 0x04001554 RID: 5460
			public SDL.SDLBool has_depth_stencil_target;

			// Token: 0x04001555 RID: 5461
			public byte padding1;

			// Token: 0x04001556 RID: 5462
			public byte padding2;

			// Token: 0x04001557 RID: 5463
			public byte padding3;
		}

		// Token: 0x0200029C RID: 668
		public struct SDL_GPUGraphicsPipelineCreateInfo
		{
			// Token: 0x04001558 RID: 5464
			public IntPtr vertex_shader;

			// Token: 0x04001559 RID: 5465
			public IntPtr fragment_shader;

			// Token: 0x0400155A RID: 5466
			public SDL.SDL_GPUVertexInputState vertex_input_state;

			// Token: 0x0400155B RID: 5467
			public SDL.SDL_GPUPrimitiveType primitive_type;

			// Token: 0x0400155C RID: 5468
			public SDL.SDL_GPURasterizerState rasterizer_state;

			// Token: 0x0400155D RID: 5469
			public SDL.SDL_GPUMultisampleState multisample_state;

			// Token: 0x0400155E RID: 5470
			public SDL.SDL_GPUDepthStencilState depth_stencil_state;

			// Token: 0x0400155F RID: 5471
			public SDL.SDL_GPUGraphicsPipelineTargetInfo target_info;

			// Token: 0x04001560 RID: 5472
			public uint props;
		}

		// Token: 0x0200029D RID: 669
		public struct SDL_GPUComputePipelineCreateInfo
		{
			// Token: 0x04001561 RID: 5473
			public UIntPtr code_size;

			// Token: 0x04001562 RID: 5474
			public unsafe byte* code;

			// Token: 0x04001563 RID: 5475
			public unsafe byte* entrypoint;

			// Token: 0x04001564 RID: 5476
			public SDL.SDL_GPUShaderFormat format;

			// Token: 0x04001565 RID: 5477
			public uint num_samplers;

			// Token: 0x04001566 RID: 5478
			public uint num_readonly_storage_textures;

			// Token: 0x04001567 RID: 5479
			public uint num_readonly_storage_buffers;

			// Token: 0x04001568 RID: 5480
			public uint num_readwrite_storage_textures;

			// Token: 0x04001569 RID: 5481
			public uint num_readwrite_storage_buffers;

			// Token: 0x0400156A RID: 5482
			public uint num_uniform_buffers;

			// Token: 0x0400156B RID: 5483
			public uint threadcount_x;

			// Token: 0x0400156C RID: 5484
			public uint threadcount_y;

			// Token: 0x0400156D RID: 5485
			public uint threadcount_z;

			// Token: 0x0400156E RID: 5486
			public uint props;
		}

		// Token: 0x0200029E RID: 670
		public struct SDL_GPUColorTargetInfo
		{
			// Token: 0x0400156F RID: 5487
			public IntPtr texture;

			// Token: 0x04001570 RID: 5488
			public uint mip_level;

			// Token: 0x04001571 RID: 5489
			public uint layer_or_depth_plane;

			// Token: 0x04001572 RID: 5490
			public SDL.SDL_FColor clear_color;

			// Token: 0x04001573 RID: 5491
			public SDL.SDL_GPULoadOp load_op;

			// Token: 0x04001574 RID: 5492
			public SDL.SDL_GPUStoreOp store_op;

			// Token: 0x04001575 RID: 5493
			public IntPtr resolve_texture;

			// Token: 0x04001576 RID: 5494
			public uint resolve_mip_level;

			// Token: 0x04001577 RID: 5495
			public uint resolve_layer;

			// Token: 0x04001578 RID: 5496
			public SDL.SDLBool cycle;

			// Token: 0x04001579 RID: 5497
			public SDL.SDLBool cycle_resolve_texture;

			// Token: 0x0400157A RID: 5498
			public byte padding1;

			// Token: 0x0400157B RID: 5499
			public byte padding2;
		}

		// Token: 0x0200029F RID: 671
		public struct SDL_GPUDepthStencilTargetInfo
		{
			// Token: 0x0400157C RID: 5500
			public IntPtr texture;

			// Token: 0x0400157D RID: 5501
			public float clear_depth;

			// Token: 0x0400157E RID: 5502
			public SDL.SDL_GPULoadOp load_op;

			// Token: 0x0400157F RID: 5503
			public SDL.SDL_GPUStoreOp store_op;

			// Token: 0x04001580 RID: 5504
			public SDL.SDL_GPULoadOp stencil_load_op;

			// Token: 0x04001581 RID: 5505
			public SDL.SDL_GPUStoreOp stencil_store_op;

			// Token: 0x04001582 RID: 5506
			public SDL.SDLBool cycle;

			// Token: 0x04001583 RID: 5507
			public byte clear_stencil;

			// Token: 0x04001584 RID: 5508
			public byte mip_level;

			// Token: 0x04001585 RID: 5509
			public byte layer;
		}

		// Token: 0x020002A0 RID: 672
		public struct SDL_GPUBlitInfo
		{
			// Token: 0x04001586 RID: 5510
			public SDL.SDL_GPUBlitRegion source;

			// Token: 0x04001587 RID: 5511
			public SDL.SDL_GPUBlitRegion destination;

			// Token: 0x04001588 RID: 5512
			public SDL.SDL_GPULoadOp load_op;

			// Token: 0x04001589 RID: 5513
			public SDL.SDL_FColor clear_color;

			// Token: 0x0400158A RID: 5514
			public SDL.SDL_FlipMode flip_mode;

			// Token: 0x0400158B RID: 5515
			public SDL.SDL_GPUFilter filter;

			// Token: 0x0400158C RID: 5516
			public SDL.SDLBool cycle;

			// Token: 0x0400158D RID: 5517
			public byte padding1;

			// Token: 0x0400158E RID: 5518
			public byte padding2;

			// Token: 0x0400158F RID: 5519
			public byte padding3;
		}

		// Token: 0x020002A1 RID: 673
		public struct SDL_GPUBufferBinding
		{
			// Token: 0x04001590 RID: 5520
			public IntPtr buffer;

			// Token: 0x04001591 RID: 5521
			public uint offset;
		}

		// Token: 0x020002A2 RID: 674
		public struct SDL_GPUTextureSamplerBinding
		{
			// Token: 0x04001592 RID: 5522
			public IntPtr texture;

			// Token: 0x04001593 RID: 5523
			public IntPtr sampler;
		}

		// Token: 0x020002A3 RID: 675
		public struct SDL_GPUStorageBufferReadWriteBinding
		{
			// Token: 0x04001594 RID: 5524
			public IntPtr buffer;

			// Token: 0x04001595 RID: 5525
			public SDL.SDLBool cycle;

			// Token: 0x04001596 RID: 5526
			public byte padding1;

			// Token: 0x04001597 RID: 5527
			public byte padding2;

			// Token: 0x04001598 RID: 5528
			public byte padding3;
		}

		// Token: 0x020002A4 RID: 676
		public struct SDL_GPUStorageTextureReadWriteBinding
		{
			// Token: 0x04001599 RID: 5529
			public IntPtr texture;

			// Token: 0x0400159A RID: 5530
			public uint mip_level;

			// Token: 0x0400159B RID: 5531
			public uint layer;

			// Token: 0x0400159C RID: 5532
			public SDL.SDLBool cycle;

			// Token: 0x0400159D RID: 5533
			public byte padding1;

			// Token: 0x0400159E RID: 5534
			public byte padding2;

			// Token: 0x0400159F RID: 5535
			public byte padding3;
		}

		// Token: 0x020002A5 RID: 677
		public struct SDL_GPUVulkanOptions
		{
			// Token: 0x040015A0 RID: 5536
			public uint vulkan_api_version;

			// Token: 0x040015A1 RID: 5537
			public IntPtr feature_list;

			// Token: 0x040015A2 RID: 5538
			public IntPtr vulkan_10_physical_device_features;

			// Token: 0x040015A3 RID: 5539
			public uint device_extension_count;

			// Token: 0x040015A4 RID: 5540
			public unsafe byte** device_extension_names;

			// Token: 0x040015A5 RID: 5541
			public uint instance_extension_count;

			// Token: 0x040015A6 RID: 5542
			public unsafe byte** instance_extension_names;
		}

		// Token: 0x020002A6 RID: 678
		public struct SDL_HapticDirection
		{
			// Token: 0x040015A7 RID: 5543
			public byte type;

			// Token: 0x040015A8 RID: 5544
			[FixedBuffer(typeof(int), 3)]
			public SDL.SDL_HapticDirection.<dir>e__FixedBuffer dir;

			// Token: 0x020003FE RID: 1022
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 12)]
			public struct <dir>e__FixedBuffer
			{
				// Token: 0x04001E2E RID: 7726
				public int FixedElementField;
			}
		}

		// Token: 0x020002A7 RID: 679
		public struct SDL_HapticConstant
		{
			// Token: 0x040015A9 RID: 5545
			public ushort type;

			// Token: 0x040015AA RID: 5546
			public SDL.SDL_HapticDirection direction;

			// Token: 0x040015AB RID: 5547
			public uint length;

			// Token: 0x040015AC RID: 5548
			public ushort delay;

			// Token: 0x040015AD RID: 5549
			public ushort button;

			// Token: 0x040015AE RID: 5550
			public ushort interval;

			// Token: 0x040015AF RID: 5551
			public short level;

			// Token: 0x040015B0 RID: 5552
			public ushort attack_length;

			// Token: 0x040015B1 RID: 5553
			public ushort attack_level;

			// Token: 0x040015B2 RID: 5554
			public ushort fade_length;

			// Token: 0x040015B3 RID: 5555
			public ushort fade_level;
		}

		// Token: 0x020002A8 RID: 680
		public struct SDL_HapticPeriodic
		{
			// Token: 0x040015B4 RID: 5556
			public ushort type;

			// Token: 0x040015B5 RID: 5557
			public SDL.SDL_HapticDirection direction;

			// Token: 0x040015B6 RID: 5558
			public uint length;

			// Token: 0x040015B7 RID: 5559
			public ushort delay;

			// Token: 0x040015B8 RID: 5560
			public ushort button;

			// Token: 0x040015B9 RID: 5561
			public ushort interval;

			// Token: 0x040015BA RID: 5562
			public ushort period;

			// Token: 0x040015BB RID: 5563
			public short magnitude;

			// Token: 0x040015BC RID: 5564
			public short offset;

			// Token: 0x040015BD RID: 5565
			public ushort phase;

			// Token: 0x040015BE RID: 5566
			public ushort attack_length;

			// Token: 0x040015BF RID: 5567
			public ushort attack_level;

			// Token: 0x040015C0 RID: 5568
			public ushort fade_length;

			// Token: 0x040015C1 RID: 5569
			public ushort fade_level;
		}

		// Token: 0x020002A9 RID: 681
		public struct SDL_HapticCondition
		{
			// Token: 0x040015C2 RID: 5570
			public ushort type;

			// Token: 0x040015C3 RID: 5571
			public SDL.SDL_HapticDirection direction;

			// Token: 0x040015C4 RID: 5572
			public uint length;

			// Token: 0x040015C5 RID: 5573
			public ushort delay;

			// Token: 0x040015C6 RID: 5574
			public ushort button;

			// Token: 0x040015C7 RID: 5575
			public ushort interval;

			// Token: 0x040015C8 RID: 5576
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<right_sat>e__FixedBuffer right_sat;

			// Token: 0x040015C9 RID: 5577
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<left_sat>e__FixedBuffer left_sat;

			// Token: 0x040015CA RID: 5578
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<right_coeff>e__FixedBuffer right_coeff;

			// Token: 0x040015CB RID: 5579
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<left_coeff>e__FixedBuffer left_coeff;

			// Token: 0x040015CC RID: 5580
			[FixedBuffer(typeof(ushort), 3)]
			public SDL.SDL_HapticCondition.<deadband>e__FixedBuffer deadband;

			// Token: 0x040015CD RID: 5581
			[FixedBuffer(typeof(short), 3)]
			public SDL.SDL_HapticCondition.<center>e__FixedBuffer center;

			// Token: 0x020003FF RID: 1023
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <center>e__FixedBuffer
			{
				// Token: 0x04001E2F RID: 7727
				public short FixedElementField;
			}

			// Token: 0x02000400 RID: 1024
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <deadband>e__FixedBuffer
			{
				// Token: 0x04001E30 RID: 7728
				public ushort FixedElementField;
			}

			// Token: 0x02000401 RID: 1025
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <left_coeff>e__FixedBuffer
			{
				// Token: 0x04001E31 RID: 7729
				public short FixedElementField;
			}

			// Token: 0x02000402 RID: 1026
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <left_sat>e__FixedBuffer
			{
				// Token: 0x04001E32 RID: 7730
				public ushort FixedElementField;
			}

			// Token: 0x02000403 RID: 1027
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <right_coeff>e__FixedBuffer
			{
				// Token: 0x04001E33 RID: 7731
				public short FixedElementField;
			}

			// Token: 0x02000404 RID: 1028
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 6)]
			public struct <right_sat>e__FixedBuffer
			{
				// Token: 0x04001E34 RID: 7732
				public ushort FixedElementField;
			}
		}

		// Token: 0x020002AA RID: 682
		public struct SDL_HapticRamp
		{
			// Token: 0x040015CE RID: 5582
			public ushort type;

			// Token: 0x040015CF RID: 5583
			public SDL.SDL_HapticDirection direction;

			// Token: 0x040015D0 RID: 5584
			public uint length;

			// Token: 0x040015D1 RID: 5585
			public ushort delay;

			// Token: 0x040015D2 RID: 5586
			public ushort button;

			// Token: 0x040015D3 RID: 5587
			public ushort interval;

			// Token: 0x040015D4 RID: 5588
			public short start;

			// Token: 0x040015D5 RID: 5589
			public short end;

			// Token: 0x040015D6 RID: 5590
			public ushort attack_length;

			// Token: 0x040015D7 RID: 5591
			public ushort attack_level;

			// Token: 0x040015D8 RID: 5592
			public ushort fade_length;

			// Token: 0x040015D9 RID: 5593
			public ushort fade_level;
		}

		// Token: 0x020002AB RID: 683
		public struct SDL_HapticLeftRight
		{
			// Token: 0x040015DA RID: 5594
			public ushort type;

			// Token: 0x040015DB RID: 5595
			public uint length;

			// Token: 0x040015DC RID: 5596
			public ushort large_magnitude;

			// Token: 0x040015DD RID: 5597
			public ushort small_magnitude;
		}

		// Token: 0x020002AC RID: 684
		public struct SDL_HapticCustom
		{
			// Token: 0x040015DE RID: 5598
			public ushort type;

			// Token: 0x040015DF RID: 5599
			public SDL.SDL_HapticDirection direction;

			// Token: 0x040015E0 RID: 5600
			public uint length;

			// Token: 0x040015E1 RID: 5601
			public ushort delay;

			// Token: 0x040015E2 RID: 5602
			public ushort button;

			// Token: 0x040015E3 RID: 5603
			public ushort interval;

			// Token: 0x040015E4 RID: 5604
			public byte channels;

			// Token: 0x040015E5 RID: 5605
			public ushort period;

			// Token: 0x040015E6 RID: 5606
			public ushort samples;

			// Token: 0x040015E7 RID: 5607
			public unsafe ushort* data;

			// Token: 0x040015E8 RID: 5608
			public ushort attack_length;

			// Token: 0x040015E9 RID: 5609
			public ushort attack_level;

			// Token: 0x040015EA RID: 5610
			public ushort fade_length;

			// Token: 0x040015EB RID: 5611
			public ushort fade_level;
		}

		// Token: 0x020002AD RID: 685
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_HapticEffect
		{
			// Token: 0x040015EC RID: 5612
			[FieldOffset(0)]
			public ushort type;

			// Token: 0x040015ED RID: 5613
			[FieldOffset(0)]
			public SDL.SDL_HapticConstant constant;

			// Token: 0x040015EE RID: 5614
			[FieldOffset(0)]
			public SDL.SDL_HapticPeriodic periodic;

			// Token: 0x040015EF RID: 5615
			[FieldOffset(0)]
			public SDL.SDL_HapticCondition condition;

			// Token: 0x040015F0 RID: 5616
			[FieldOffset(0)]
			public SDL.SDL_HapticRamp ramp;

			// Token: 0x040015F1 RID: 5617
			[FieldOffset(0)]
			public SDL.SDL_HapticLeftRight leftright;

			// Token: 0x040015F2 RID: 5618
			[FieldOffset(0)]
			public SDL.SDL_HapticCustom custom;
		}

		// Token: 0x020002AE RID: 686
		public enum SDL_hid_bus_type
		{
			// Token: 0x040015F4 RID: 5620
			SDL_HID_API_BUS_UNKNOWN,
			// Token: 0x040015F5 RID: 5621
			SDL_HID_API_BUS_USB,
			// Token: 0x040015F6 RID: 5622
			SDL_HID_API_BUS_BLUETOOTH,
			// Token: 0x040015F7 RID: 5623
			SDL_HID_API_BUS_I2C,
			// Token: 0x040015F8 RID: 5624
			SDL_HID_API_BUS_SPI
		}

		// Token: 0x020002AF RID: 687
		public struct SDL_hid_device_info
		{
			// Token: 0x040015F9 RID: 5625
			public unsafe byte* path;

			// Token: 0x040015FA RID: 5626
			public ushort vendor_id;

			// Token: 0x040015FB RID: 5627
			public ushort product_id;

			// Token: 0x040015FC RID: 5628
			public unsafe byte* serial_number;

			// Token: 0x040015FD RID: 5629
			public ushort release_number;

			// Token: 0x040015FE RID: 5630
			public unsafe byte* manufacturer_string;

			// Token: 0x040015FF RID: 5631
			public unsafe byte* product_string;

			// Token: 0x04001600 RID: 5632
			public ushort usage_page;

			// Token: 0x04001601 RID: 5633
			public ushort usage;

			// Token: 0x04001602 RID: 5634
			public int interface_number;

			// Token: 0x04001603 RID: 5635
			public int interface_class;

			// Token: 0x04001604 RID: 5636
			public int interface_subclass;

			// Token: 0x04001605 RID: 5637
			public int interface_protocol;

			// Token: 0x04001606 RID: 5638
			public SDL.SDL_hid_bus_type bus_type;

			// Token: 0x04001607 RID: 5639
			public unsafe SDL.SDL_hid_device_info* next;
		}

		// Token: 0x020002B0 RID: 688
		public enum SDL_HintPriority
		{
			// Token: 0x04001609 RID: 5641
			SDL_HINT_DEFAULT,
			// Token: 0x0400160A RID: 5642
			SDL_HINT_NORMAL,
			// Token: 0x0400160B RID: 5643
			SDL_HINT_OVERRIDE
		}

		// Token: 0x020002B1 RID: 689
		// (Invoke) Token: 0x0600196C RID: 6508
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void SDL_HintCallback(IntPtr userdata, byte* name, byte* oldValue, byte* newValue);

		// Token: 0x020002B2 RID: 690
		[Flags]
		public enum SDL_InitFlags : uint
		{
			// Token: 0x0400160D RID: 5645
			SDL_INIT_TIMER = 1U,
			// Token: 0x0400160E RID: 5646
			SDL_INIT_AUDIO = 16U,
			// Token: 0x0400160F RID: 5647
			SDL_INIT_VIDEO = 32U,
			// Token: 0x04001610 RID: 5648
			SDL_INIT_JOYSTICK = 512U,
			// Token: 0x04001611 RID: 5649
			SDL_INIT_HAPTIC = 4096U,
			// Token: 0x04001612 RID: 5650
			SDL_INIT_GAMEPAD = 8192U,
			// Token: 0x04001613 RID: 5651
			SDL_INIT_EVENTS = 16384U,
			// Token: 0x04001614 RID: 5652
			SDL_INIT_SENSOR = 32768U,
			// Token: 0x04001615 RID: 5653
			SDL_INIT_CAMERA = 65536U
		}

		// Token: 0x020002B3 RID: 691
		public enum SDL_AppResult
		{
			// Token: 0x04001617 RID: 5655
			SDL_APP_CONTINUE,
			// Token: 0x04001618 RID: 5656
			SDL_APP_SUCCESS,
			// Token: 0x04001619 RID: 5657
			SDL_APP_FAILURE
		}

		// Token: 0x020002B4 RID: 692
		// (Invoke) Token: 0x06001970 RID: 6512
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL.SDL_AppResult SDL_AppInit_func(IntPtr appstate, int argc, IntPtr argv);

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06001974 RID: 6516
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SDL.SDL_AppResult SDL_AppIterate_func(IntPtr appstate);

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06001978 RID: 6520
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate SDL.SDL_AppResult SDL_AppEvent_func(IntPtr appstate, SDL.SDL_Event* evt);

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x0600197C RID: 6524
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_AppQuit_func(IntPtr appstate, SDL.SDL_AppResult result);

		// Token: 0x020002B8 RID: 696
		// (Invoke) Token: 0x06001980 RID: 6528
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_MainThreadCallback(IntPtr userdata);

		// Token: 0x020002B9 RID: 697
		public struct SDL_Locale
		{
			// Token: 0x0400161A RID: 5658
			public unsafe byte* language;

			// Token: 0x0400161B RID: 5659
			public unsafe byte* country;
		}

		// Token: 0x020002BA RID: 698
		public enum SDL_LogCategory
		{
			// Token: 0x0400161D RID: 5661
			SDL_LOG_CATEGORY_APPLICATION,
			// Token: 0x0400161E RID: 5662
			SDL_LOG_CATEGORY_ERROR,
			// Token: 0x0400161F RID: 5663
			SDL_LOG_CATEGORY_ASSERT,
			// Token: 0x04001620 RID: 5664
			SDL_LOG_CATEGORY_SYSTEM,
			// Token: 0x04001621 RID: 5665
			SDL_LOG_CATEGORY_AUDIO,
			// Token: 0x04001622 RID: 5666
			SDL_LOG_CATEGORY_VIDEO,
			// Token: 0x04001623 RID: 5667
			SDL_LOG_CATEGORY_RENDER,
			// Token: 0x04001624 RID: 5668
			SDL_LOG_CATEGORY_INPUT,
			// Token: 0x04001625 RID: 5669
			SDL_LOG_CATEGORY_TEST,
			// Token: 0x04001626 RID: 5670
			SDL_LOG_CATEGORY_GPU,
			// Token: 0x04001627 RID: 5671
			SDL_LOG_CATEGORY_RESERVED2,
			// Token: 0x04001628 RID: 5672
			SDL_LOG_CATEGORY_RESERVED3,
			// Token: 0x04001629 RID: 5673
			SDL_LOG_CATEGORY_RESERVED4,
			// Token: 0x0400162A RID: 5674
			SDL_LOG_CATEGORY_RESERVED5,
			// Token: 0x0400162B RID: 5675
			SDL_LOG_CATEGORY_RESERVED6,
			// Token: 0x0400162C RID: 5676
			SDL_LOG_CATEGORY_RESERVED7,
			// Token: 0x0400162D RID: 5677
			SDL_LOG_CATEGORY_RESERVED8,
			// Token: 0x0400162E RID: 5678
			SDL_LOG_CATEGORY_RESERVED9,
			// Token: 0x0400162F RID: 5679
			SDL_LOG_CATEGORY_RESERVED10,
			// Token: 0x04001630 RID: 5680
			SDL_LOG_CATEGORY_CUSTOM
		}

		// Token: 0x020002BB RID: 699
		public enum SDL_LogPriority
		{
			// Token: 0x04001632 RID: 5682
			SDL_LOG_PRIORITY_INVALID,
			// Token: 0x04001633 RID: 5683
			SDL_LOG_PRIORITY_TRACE,
			// Token: 0x04001634 RID: 5684
			SDL_LOG_PRIORITY_VERBOSE,
			// Token: 0x04001635 RID: 5685
			SDL_LOG_PRIORITY_DEBUG,
			// Token: 0x04001636 RID: 5686
			SDL_LOG_PRIORITY_INFO,
			// Token: 0x04001637 RID: 5687
			SDL_LOG_PRIORITY_WARN,
			// Token: 0x04001638 RID: 5688
			SDL_LOG_PRIORITY_ERROR,
			// Token: 0x04001639 RID: 5689
			SDL_LOG_PRIORITY_CRITICAL,
			// Token: 0x0400163A RID: 5690
			SDL_LOG_PRIORITY_COUNT
		}

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x06001984 RID: 6532
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void SDL_LogOutputFunction(IntPtr userdata, int category, SDL.SDL_LogPriority priority, byte* message);

		// Token: 0x020002BD RID: 701
		[Flags]
		public enum SDL_MessageBoxFlags : uint
		{
			// Token: 0x0400163C RID: 5692
			SDL_MESSAGEBOX_ERROR = 16U,
			// Token: 0x0400163D RID: 5693
			SDL_MESSAGEBOX_WARNING = 32U,
			// Token: 0x0400163E RID: 5694
			SDL_MESSAGEBOX_INFORMATION = 64U,
			// Token: 0x0400163F RID: 5695
			SDL_MESSAGEBOX_BUTTONS_LEFT_TO_RIGHT = 128U,
			// Token: 0x04001640 RID: 5696
			SDL_MESSAGEBOX_BUTTONS_RIGHT_TO_LEFT = 256U
		}

		// Token: 0x020002BE RID: 702
		[Flags]
		public enum SDL_MessageBoxButtonFlags : uint
		{
			// Token: 0x04001642 RID: 5698
			SDL_MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 1U,
			// Token: 0x04001643 RID: 5699
			SDL_MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 2U
		}

		// Token: 0x020002BF RID: 703
		public struct SDL_MessageBoxButtonData
		{
			// Token: 0x04001644 RID: 5700
			public SDL.SDL_MessageBoxButtonFlags flags;

			// Token: 0x04001645 RID: 5701
			public int buttonID;

			// Token: 0x04001646 RID: 5702
			public unsafe byte* text;
		}

		// Token: 0x020002C0 RID: 704
		public struct SDL_MessageBoxColor
		{
			// Token: 0x04001647 RID: 5703
			public byte r;

			// Token: 0x04001648 RID: 5704
			public byte g;

			// Token: 0x04001649 RID: 5705
			public byte b;
		}

		// Token: 0x020002C1 RID: 705
		public enum SDL_MessageBoxColorType
		{
			// Token: 0x0400164B RID: 5707
			SDL_MESSAGEBOX_COLOR_BACKGROUND,
			// Token: 0x0400164C RID: 5708
			SDL_MESSAGEBOX_COLOR_TEXT,
			// Token: 0x0400164D RID: 5709
			SDL_MESSAGEBOX_COLOR_BUTTON_BORDER,
			// Token: 0x0400164E RID: 5710
			SDL_MESSAGEBOX_COLOR_BUTTON_BACKGROUND,
			// Token: 0x0400164F RID: 5711
			SDL_MESSAGEBOX_COLOR_BUTTON_SELECTED,
			// Token: 0x04001650 RID: 5712
			SDL_MESSAGEBOX_COLOR_COUNT
		}

		// Token: 0x020002C2 RID: 706
		public struct SDL_MessageBoxColorScheme
		{
			// Token: 0x04001651 RID: 5713
			public SDL.SDL_MessageBoxColor colors0;

			// Token: 0x04001652 RID: 5714
			public SDL.SDL_MessageBoxColor colors1;

			// Token: 0x04001653 RID: 5715
			public SDL.SDL_MessageBoxColor colors2;

			// Token: 0x04001654 RID: 5716
			public SDL.SDL_MessageBoxColor colors3;

			// Token: 0x04001655 RID: 5717
			public SDL.SDL_MessageBoxColor colors4;
		}

		// Token: 0x020002C3 RID: 707
		public struct SDL_MessageBoxData
		{
			// Token: 0x04001656 RID: 5718
			public SDL.SDL_MessageBoxFlags flags;

			// Token: 0x04001657 RID: 5719
			public IntPtr window;

			// Token: 0x04001658 RID: 5720
			public unsafe byte* title;

			// Token: 0x04001659 RID: 5721
			public unsafe byte* message;

			// Token: 0x0400165A RID: 5722
			public int numbuttons;

			// Token: 0x0400165B RID: 5723
			public unsafe SDL.SDL_MessageBoxButtonData* buttons;

			// Token: 0x0400165C RID: 5724
			public unsafe SDL.SDL_MessageBoxColorScheme* colorScheme;
		}

		// Token: 0x020002C4 RID: 708
		public enum SDL_ProcessIO
		{
			// Token: 0x0400165E RID: 5726
			SDL_PROCESS_STDIO_INHERITED,
			// Token: 0x0400165F RID: 5727
			SDL_PROCESS_STDIO_NULL,
			// Token: 0x04001660 RID: 5728
			SDL_PROCESS_STDIO_APP,
			// Token: 0x04001661 RID: 5729
			SDL_PROCESS_STDIO_REDIRECT
		}

		// Token: 0x020002C5 RID: 709
		public struct SDL_Vertex
		{
			// Token: 0x04001662 RID: 5730
			public SDL.SDL_FPoint position;

			// Token: 0x04001663 RID: 5731
			public SDL.SDL_FColor color;

			// Token: 0x04001664 RID: 5732
			public SDL.SDL_FPoint tex_coord;
		}

		// Token: 0x020002C6 RID: 710
		public enum SDL_TextureAccess
		{
			// Token: 0x04001666 RID: 5734
			SDL_TEXTUREACCESS_STATIC,
			// Token: 0x04001667 RID: 5735
			SDL_TEXTUREACCESS_STREAMING,
			// Token: 0x04001668 RID: 5736
			SDL_TEXTUREACCESS_TARGET
		}

		// Token: 0x020002C7 RID: 711
		public enum SDL_TextureAddressMode
		{
			// Token: 0x0400166A RID: 5738
			SDL_TEXTURE_ADDRESS_INVALID = -1,
			// Token: 0x0400166B RID: 5739
			SDL_TEXTURE_ADDRESS_AUTO,
			// Token: 0x0400166C RID: 5740
			SDL_TEXTURE_ADDRESS_CLAMP,
			// Token: 0x0400166D RID: 5741
			SDL_TEXTURE_ADDRESS_WRAP
		}

		// Token: 0x020002C8 RID: 712
		public enum SDL_RendererLogicalPresentation
		{
			// Token: 0x0400166F RID: 5743
			SDL_LOGICAL_PRESENTATION_DISABLED,
			// Token: 0x04001670 RID: 5744
			SDL_LOGICAL_PRESENTATION_STRETCH,
			// Token: 0x04001671 RID: 5745
			SDL_LOGICAL_PRESENTATION_LETTERBOX,
			// Token: 0x04001672 RID: 5746
			SDL_LOGICAL_PRESENTATION_OVERSCAN,
			// Token: 0x04001673 RID: 5747
			SDL_LOGICAL_PRESENTATION_INTEGER_SCALE
		}

		// Token: 0x020002C9 RID: 713
		public struct SDL_Texture
		{
			// Token: 0x04001674 RID: 5748
			public SDL.SDL_PixelFormat format;

			// Token: 0x04001675 RID: 5749
			public int w;

			// Token: 0x04001676 RID: 5750
			public int h;

			// Token: 0x04001677 RID: 5751
			public int refcount;
		}

		// Token: 0x020002CA RID: 714
		public struct SDL_GPURenderStateCreateInfo
		{
			// Token: 0x04001678 RID: 5752
			public IntPtr fragment_shader;

			// Token: 0x04001679 RID: 5753
			public int num_sampler_bindings;

			// Token: 0x0400167A RID: 5754
			public unsafe SDL.SDL_GPUTextureSamplerBinding* sampler_bindings;

			// Token: 0x0400167B RID: 5755
			public int num_storage_textures;

			// Token: 0x0400167C RID: 5756
			public unsafe IntPtr* storage_textures;

			// Token: 0x0400167D RID: 5757
			public int num_storage_buffers;

			// Token: 0x0400167E RID: 5758
			public unsafe IntPtr* storage_buffers;

			// Token: 0x0400167F RID: 5759
			public uint props;
		}

		// Token: 0x020002CB RID: 715
		public struct SDL_StorageInterface
		{
			// Token: 0x04001680 RID: 5760
			public uint version;

			// Token: 0x04001681 RID: 5761
			public IntPtr close;

			// Token: 0x04001682 RID: 5762
			public IntPtr ready;

			// Token: 0x04001683 RID: 5763
			public IntPtr enumerate;

			// Token: 0x04001684 RID: 5764
			public IntPtr info;

			// Token: 0x04001685 RID: 5765
			public IntPtr read_file;

			// Token: 0x04001686 RID: 5766
			public IntPtr write_file;

			// Token: 0x04001687 RID: 5767
			public IntPtr mkdir;

			// Token: 0x04001688 RID: 5768
			public IntPtr remove;

			// Token: 0x04001689 RID: 5769
			public IntPtr rename;

			// Token: 0x0400168A RID: 5770
			public IntPtr copy;

			// Token: 0x0400168B RID: 5771
			public IntPtr space_remaining;
		}

		// Token: 0x020002CC RID: 716
		public enum SDL_Sandbox
		{
			// Token: 0x0400168D RID: 5773
			SDL_SANDBOX_NONE,
			// Token: 0x0400168E RID: 5774
			SDL_SANDBOX_UNKNOWN_CONTAINER,
			// Token: 0x0400168F RID: 5775
			SDL_SANDBOX_FLATPAK,
			// Token: 0x04001690 RID: 5776
			SDL_SANDBOX_SNAP,
			// Token: 0x04001691 RID: 5777
			SDL_SANDBOX_MACOS
		}

		// Token: 0x020002CD RID: 717
		public struct SDL_DateTime
		{
			// Token: 0x04001692 RID: 5778
			public int year;

			// Token: 0x04001693 RID: 5779
			public int month;

			// Token: 0x04001694 RID: 5780
			public int day;

			// Token: 0x04001695 RID: 5781
			public int hour;

			// Token: 0x04001696 RID: 5782
			public int minute;

			// Token: 0x04001697 RID: 5783
			public int second;

			// Token: 0x04001698 RID: 5784
			public int nanosecond;

			// Token: 0x04001699 RID: 5785
			public int day_of_week;

			// Token: 0x0400169A RID: 5786
			public int utc_offset;
		}

		// Token: 0x020002CE RID: 718
		public enum SDL_DateFormat
		{
			// Token: 0x0400169C RID: 5788
			SDL_DATE_FORMAT_YYYYMMDD,
			// Token: 0x0400169D RID: 5789
			SDL_DATE_FORMAT_DDMMYYYY,
			// Token: 0x0400169E RID: 5790
			SDL_DATE_FORMAT_MMDDYYYY
		}

		// Token: 0x020002CF RID: 719
		public enum SDL_TimeFormat
		{
			// Token: 0x040016A0 RID: 5792
			SDL_TIME_FORMAT_24HR,
			// Token: 0x040016A1 RID: 5793
			SDL_TIME_FORMAT_12HR
		}

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x06001988 RID: 6536
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint SDL_TimerCallback(IntPtr userdata, uint timerID, uint interval);

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x0600198C RID: 6540
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate ulong SDL_NSTimerCallback(IntPtr userdata, uint timerID, ulong interval);

		// Token: 0x020002D2 RID: 722
		[Flags]
		public enum SDL_TrayEntryFlags : uint
		{
			// Token: 0x040016A3 RID: 5795
			SDL_TRAYENTRY_BUTTON = 1U,
			// Token: 0x040016A4 RID: 5796
			SDL_TRAYENTRY_CHECKBOX = 2U,
			// Token: 0x040016A5 RID: 5797
			SDL_TRAYENTRY_SUBMENU = 4U,
			// Token: 0x040016A6 RID: 5798
			SDL_TRAYENTRY_DISABLED = 2147483648U,
			// Token: 0x040016A7 RID: 5799
			SDL_TRAYENTRY_CHECKED = 1073741824U
		}

		// Token: 0x020002D3 RID: 723
		// (Invoke) Token: 0x06001990 RID: 6544
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SDL_TrayCallback(IntPtr userdata, IntPtr entry);

		// Token: 0x020002D4 RID: 724
		// (Invoke) Token: 0x06001994 RID: 6548
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int SDL_main_func(int argc, IntPtr argv);
	}
}
