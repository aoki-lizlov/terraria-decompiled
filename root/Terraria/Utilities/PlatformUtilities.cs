using System;
using System.IO;
using SDL3;

namespace Terraria.Utilities
{
	// Token: 0x020000D1 RID: 209
	public static class PlatformUtilities
	{
		// Token: 0x06001834 RID: 6196 RVA: 0x004E184C File Offset: 0x004DFA4C
		public unsafe static void SavePng(string path, int width, int height, byte[] data)
		{
			if (width * height * 4 > data.Length)
			{
				throw new IndexOutOfRangeException();
			}
			fixed (byte[] array = data)
			{
				byte* ptr;
				if (data == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				IntPtr intPtr = SDL.SDL_CreateSurfaceFrom(width, height, SDL.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR8888, (IntPtr)((void*)ptr), width * 4);
				if (intPtr == IntPtr.Zero)
				{
					throw new IOException("Failed to create SDL surface: " + SDL.SDL_GetError());
				}
				try
				{
					IntPtr intPtr2 = SDL.SDL_IOFromFile(path, "wb");
					if (intPtr2 == IntPtr.Zero)
					{
						throw new IOException("Failed to open file for writing: " + SDL.SDL_GetError());
					}
					if (!SDL.SDL_SavePNG_IO(intPtr, intPtr2, true))
					{
						throw new IOException("Failed to save PNG: " + SDL.SDL_GetError());
					}
				}
				finally
				{
					SDL.SDL_DestroySurface(intPtr);
				}
			}
		}
	}
}
