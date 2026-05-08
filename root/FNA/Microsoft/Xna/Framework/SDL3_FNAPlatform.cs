using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Utilities;
using ObjCRuntime;
using SDL3;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001B RID: 27
	internal static class SDL3_FNAPlatform
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		public static string ProgramInit(LaunchParameters args)
		{
			try
			{
				SDL3_FNAPlatform.OSVersion = SDL.SDL_GetPlatform();
			}
			catch (DllNotFoundException)
			{
				FNALoggerEXT.LogError("SDL3 was not found! Do you have fnalibs?");
				throw;
			}
			catch (BadImageFormatException ex)
			{
				string text = string.Format("This process is {0}-bit, the DLL is {1}-bit!", (IntPtr.Size == 4) ? "32" : "64", (IntPtr.Size == 4) ? "64" : "32");
				FNALoggerEXT.LogError(text);
				throw new BadImageFormatException(text, ex);
			}
			SDL.SDL_SetMainReady();
			string baseDirectory = SDL3_FNAPlatform.GetBaseDirectory();
			string text2 = Path.Combine(baseDirectory, "gamecontrollerdb.txt");
			if (File.Exists(text2))
			{
				SDL.SDL_SetHint("SDL_GAMECONTROLLERCONFIG_FILE", text2);
			}
			if (Environment.GetEnvironmentVariable("FNA_NUKE_STEAM_INPUT") == "1")
			{
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_IGNORE_DEVICES", "0x28DE/0x11FF", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT", "", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_ALLOW_STEAM_VIRTUAL_GAMEPAD", "0", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			string text3;
			if (args.TryGetValue("glprofile", out text3))
			{
				if (text3 == "es3")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_ES3", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
				else if (text3 == "core")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_CORE_PROFILE", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
				else if (text3 == "compatibility")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_COMPATIBILITY_PROFILE", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
			}
			if (args.TryGetValue("angle", out text3) && text3 == "1")
			{
				SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_ES3", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_OPENGL_ES_DRIVER", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (args.TryGetValue("forcemailboxvsync", out text3) && text3 == "1")
			{
				SDL.SDL_SetHintWithPriority("FNA3D_VULKAN_FORCE_MAILBOX_VSYNC", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (args.TryGetValue("audiodriver", out text3))
			{
				SDL.SDL_SetHintWithPriority("SDL_AUDIO_DRIVER", text3, SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (!SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO | SDL.SDL_InitFlags.SDL_INIT_GAMEPAD))
			{
				throw new Exception("SDL_Init failed: " + SDL.SDL_GetError());
			}
			string text4 = SDL.SDL_GetCurrentVideoDriver();
			SDL3_FNAPlatform.SupportsGlobalMouse = SDL3_FNAPlatform.OSVersion.Equals("Windows") || SDL3_FNAPlatform.OSVersion.Equals("macOS") || text4.Equals("x11");
			if (Environment.GetEnvironmentVariable("FNA_MOUSE_DISABLE_GLOBAL_ACCESS") == "1")
			{
				SDL3_FNAPlatform.SupportsGlobalMouse = false;
			}
			if (SDL3_FNAPlatform.SupportsGlobalMouse)
			{
				SDL.SDL_SetHint("SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE", "0");
			}
			SDL3_FNAPlatform.SupportsOrientations = SDL3_FNAPlatform.OSVersion.Equals("iOS") || SDL3_FNAPlatform.OSVersion.Equals("Android");
			if (SDL3_FNAPlatform.OSVersion.Equals("Windows"))
			{
				SDL.SDL_SetHint("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS", "1");
			}
			if (string.IsNullOrEmpty(SDL.SDL_GetHint("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS")))
			{
				SDL.SDL_SetHint("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS", "1");
			}
			SDL.SDL_SetHint("SDL_ORIENTATIONS", "LandscapeLeft LandscapeRight Portrait");
			SDL.SDL_Event[] array = new SDL.SDL_Event[1];
			SDL.SDL_PumpEvents();
			while (SDL.SDL_PeepEvents(array, 1, SDL.SDL_EventAction.SDL_GETEVENT, 1619U, 1619U) == 1)
			{
				SDL3_FNAPlatform.INTERNAL_AddInstance(array[0].gdevice.which);
			}
			if (SDL3_FNAPlatform.OSVersion.Equals("Windows") && SDL.SDL_GetHint("FNA_WIN32_IGNORE_WM_PAINT") != "1")
			{
				IntPtr intPtr;
				SDL.SDL_GetEventFilter(out SDL3_FNAPlatform.prevEventFilter, out intPtr);
				SDL.SDL_SetEventFilter(SDL3_FNAPlatform.win32OnPaint, intPtr);
			}
			return baseDirectory;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000E758 File Offset: 0x0000C958
		public static void ProgramExit(object sender, EventArgs e)
		{
			SDL.SDL_QuitSubSystem(SDL.SDL_InitFlags.SDL_INIT_VIDEO | SDL.SDL_InitFlags.SDL_INIT_GAMEPAD);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0000E764 File Offset: 0x0000C964
		public static IntPtr Malloc(int size)
		{
			return SDL.SDL_malloc((UIntPtr)((ulong)((long)size)));
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000E772 File Offset: 0x0000C972
		public static void SetEnv(string name, string value)
		{
			SDL.SDL_SetHintWithPriority(name, value, SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0000E780 File Offset: 0x0000C980
		public static GameWindow CreateWindow()
		{
			SDL.SDL_WindowFlags sdl_WindowFlags = SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN | SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS | SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS | (SDL.SDL_WindowFlags)FNA3D.FNA3D_PrepareWindowAttributes();
			if ((sdl_WindowFlags & SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN) == SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN && SDL.SDL_GetHint("FNA3D_VULKAN_PIPELINE_CACHE_FILE_NAME") == null)
			{
				string text2;
				if (SDL3_FNAPlatform.OSVersion.Equals("Windows") || SDL3_FNAPlatform.OSVersion.Equals("macOS") || SDL3_FNAPlatform.OSVersion.Equals("Linux") || SDL3_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL3_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL3_FNAPlatform.OSVersion.Equals("NetBSD"))
				{
					string text = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName).Replace(".vshost", "");
					text2 = Path.Combine(SDL.SDL_GetPrefPath(null, "FNA3D"), text + "_Vulkan_PipelineCache.blob");
				}
				else
				{
					text2 = string.Empty;
				}
				SDL.SDL_SetHint("FNA3D_VULKAN_PIPELINE_CACHE_FILE_NAME", text2);
			}
			if (Environment.GetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI") == "1")
			{
				sdl_WindowFlags |= SDL.SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY;
			}
			string defaultWindowTitle = AssemblyHelper.GetDefaultWindowTitle();
			IntPtr intPtr = SDL.SDL_CreateWindow(defaultWindowTitle, GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight, sdl_WindowFlags);
			if (intPtr == IntPtr.Zero)
			{
				throw new NoSuitableGraphicsDeviceException(SDL.SDL_GetError());
			}
			SDL3_FNAPlatform.INTERNAL_SetIcon(intPtr, defaultWindowTitle);
			SDL.SDL_DisableScreenSaver();
			SDL3_FNAPlatform.OnIsMouseVisibleChanged(false);
			sdl_WindowFlags = SDL.SDL_GetWindowFlags(intPtr);
			if ((sdl_WindowFlags & SDL.SDL_WindowFlags.SDL_WINDOW_HIGH_PIXEL_DENSITY) == (SDL.SDL_WindowFlags)0UL)
			{
				Environment.SetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI", "0");
			}
			return new FNAWindow(intPtr, "\\\\.\\DISPLAY" + SDL.SDL_GetDisplayForWindow(intPtr).ToString());
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0000E918 File Offset: 0x0000CB18
		public static void DisposeWindow(GameWindow window)
		{
			SDL.SDL_SetHintWithPriority("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS", "0", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			if (Mouse.WindowHandle == window.Handle)
			{
				Mouse.WindowHandle = IntPtr.Zero;
			}
			if (TouchPanel.WindowHandle == window.Handle)
			{
				TouchPanel.WindowHandle = IntPtr.Zero;
			}
			if (TextInputEXT.WindowHandle == window.Handle)
			{
				TextInputEXT.WindowHandle = IntPtr.Zero;
			}
			SDL.SDL_DestroyWindow(window.Handle);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0000E998 File Offset: 0x0000CB98
		public unsafe static void ApplyWindowChanges(IntPtr window, int clientWidth, int clientHeight, bool wantsFullscreen, string screenDeviceName, ref string resultDeviceName)
		{
			bool flag = false;
			SDL3_FNAPlatform.ScaleForWindow(window, false, ref clientWidth, ref clientHeight);
			if (!wantsFullscreen)
			{
				bool flag2;
				if ((SDL.SDL_GetWindowFlags(window) & SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) != (SDL.SDL_WindowFlags)0UL)
				{
					SDL.SDL_SetWindowFullscreen(window, false);
					flag2 = true;
				}
				else
				{
					int num;
					int num2;
					SDL.SDL_GetWindowSize(window, out num, out num2);
					flag2 = clientWidth != num || clientHeight != num2;
				}
				if (flag2)
				{
					SDL.SDL_RestoreWindow(window);
					SDL.SDL_SetWindowSize(window, clientWidth, clientHeight);
					flag = true;
				}
			}
			int num3 = 0;
			for (int i = 0; i < GraphicsAdapter.Adapters.Count; i++)
			{
				if (screenDeviceName == GraphicsAdapter.Adapters[i].DeviceName)
				{
					num3 = i;
					break;
				}
			}
			if (resultDeviceName != screenDeviceName)
			{
				SDL.SDL_SetWindowFullscreen(window, false);
				resultDeviceName = screenDeviceName;
				flag = true;
			}
			if (flag)
			{
				int num4 = (int)(805240832U | SDL3_FNAPlatform.displayIds[num3]);
				SDL.SDL_SetWindowPosition(window, num4, num4);
			}
			if (wantsFullscreen)
			{
				if ((SDL.SDL_GetWindowFlags(window) & SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN) != (SDL.SDL_WindowFlags)0UL)
				{
					SDL.SDL_DisplayMode* ptr = (SDL.SDL_DisplayMode*)(void*)SDL.SDL_GetCurrentDisplayMode(SDL.SDL_GetDisplayForWindow(window));
					SDL.SDL_SetWindowSize(window, ptr->w, ptr->h);
				}
				SDL.SDL_SetWindowFullscreen(window, true);
			}
			if (Mouse.WindowHandle == window)
			{
				Rectangle windowBounds = SDL3_FNAPlatform.GetWindowBounds(window);
				Mouse.INTERNAL_WindowWidth = windowBounds.Width;
				Mouse.INTERNAL_WindowHeight = windowBounds.Height;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		public static void ScaleForWindow(IntPtr window, bool invert, ref int w, ref int h)
		{
			int num;
			int num2;
			SDL.SDL_GetWindowSize(window, out num, out num2);
			int num3;
			int num4;
			FNA3D.FNA3D_GetDrawableSize(window, out num3, out num4);
			if (num != 0 && num2 != 0 && num3 != 0 && num4 != 0 && (num != num3 || num2 != num4))
			{
				if (invert)
				{
					w = (int)((float)w * ((float)num3 / (float)num));
					h = (int)((float)h * ((float)num4 / (float)num2));
					return;
				}
				w = (int)((float)w / ((float)num3 / (float)num));
				h = (int)((float)h / ((float)num4 / (float)num2));
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0000EB50 File Offset: 0x0000CD50
		public unsafe static Rectangle GetWindowBounds(IntPtr window)
		{
			Rectangle rectangle;
			if ((SDL.SDL_GetWindowFlags(window) & SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN) != (SDL.SDL_WindowFlags)0UL)
			{
				SDL.SDL_DisplayMode* ptr = (SDL.SDL_DisplayMode*)(void*)SDL.SDL_GetCurrentDisplayMode(SDL.SDL_GetDisplayForWindow(window));
				rectangle.X = 0;
				rectangle.Y = 0;
				rectangle.Width = ptr->w;
				rectangle.Height = ptr->h;
			}
			else
			{
				SDL.SDL_GetWindowPosition(window, out rectangle.X, out rectangle.Y);
				SDL.SDL_GetWindowSize(window, out rectangle.Width, out rectangle.Height);
			}
			return rectangle;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
		public static bool GetWindowResizable(IntPtr window)
		{
			return (SDL.SDL_GetWindowFlags(window) & SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE) > (SDL.SDL_WindowFlags)0UL;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public static void SetWindowResizable(IntPtr window, bool resizable)
		{
			SDL.SDL_SetWindowResizable(window, resizable);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0000EBEF File Offset: 0x0000CDEF
		public static bool GetWindowBorderless(IntPtr window)
		{
			return (SDL.SDL_GetWindowFlags(window) & SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS) > (SDL.SDL_WindowFlags)0UL;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		public static void SetWindowBorderless(IntPtr window, bool borderless)
		{
			SDL.SDL_SetWindowBordered(window, !borderless);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0000EC11 File Offset: 0x0000CE11
		public static void SetWindowTitle(IntPtr window, string title)
		{
			SDL.SDL_SetWindowTitle(window, title);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0000EC1B File Offset: 0x0000CE1B
		public static bool IsScreenKeyboardShown(IntPtr window)
		{
			return SDL.SDL_ScreenKeyboardShown(window);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0000EC28 File Offset: 0x0000CE28
		private static void INTERNAL_SetIcon(IntPtr window, string title)
		{
			string text = string.Empty;
			try
			{
				text = SDL3_FNAPlatform.INTERNAL_GetIconName(title + ".png");
				if (!string.IsNullOrEmpty(text))
				{
					IntPtr intPtr;
					IntPtr intPtr2;
					using (Stream stream = TitleContainer.OpenStream(text))
					{
						int num;
						int num2;
						int num3;
						intPtr = FNA3D.ReadImageStream(stream, out num, out num2, out num3, -1, -1, false);
						intPtr2 = SDL.SDL_CreateSurfaceFrom(num, num2, SDL.SDL_PixelFormat.SDL_PIXELFORMAT_ABGR8888, intPtr, num * 4);
					}
					SDL.SDL_SetWindowIcon(window, intPtr2);
					SDL.SDL_DestroySurface(intPtr2);
					FNA3D.FNA3D_Image_Free(intPtr);
					return;
				}
			}
			catch (DllNotFoundException)
			{
			}
			text = SDL3_FNAPlatform.INTERNAL_GetIconName(title + ".bmp");
			if (!string.IsNullOrEmpty(text))
			{
				IntPtr intPtr3 = SDL.SDL_LoadBMP(text);
				SDL.SDL_SetWindowIcon(window, intPtr3);
				SDL.SDL_DestroySurface(intPtr3);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		private static string INTERNAL_GetIconName(string title)
		{
			string text = Path.Combine(TitleLocation.Path, title);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(TitleLocation.Path, SDL3_FNAPlatform.INTERNAL_StripBadChars(title));
			if (File.Exists(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0000ED40 File Offset: 0x0000CF40
		private static string INTERNAL_StripBadChars(string path)
		{
			char[] array = new char[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
			List<char> list = new List<char>();
			list.AddRange(Path.GetInvalidFileNameChars());
			list.AddRange(array);
			string text = path;
			foreach (char c in list)
			{
				text = text.Replace(c.ToString(), "");
			}
			return text;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public static void SetTextInputRectangle(IntPtr window, Rectangle rectangle)
		{
			SDL.SDL_Rect sdl_Rect = default(SDL.SDL_Rect);
			sdl_Rect.x = rectangle.X;
			sdl_Rect.y = rectangle.Y;
			sdl_Rect.w = rectangle.Width;
			sdl_Rect.h = rectangle.Height;
			SDL.SDL_SetTextInputArea(window, ref sdl_Rect, 0);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000B5C4 File Offset: 0x000097C4
		private static DisplayOrientation INTERNAL_ConvertOrientation(SDL.SDL_DisplayOrientation orientation)
		{
			switch (orientation)
			{
			case SDL.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE:
				return DisplayOrientation.LandscapeLeft;
			case SDL.SDL_DisplayOrientation.SDL_ORIENTATION_LANDSCAPE_FLIPPED:
				return DisplayOrientation.LandscapeRight;
			case SDL.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT:
			case SDL.SDL_DisplayOrientation.SDL_ORIENTATION_PORTRAIT_FLIPPED:
				return DisplayOrientation.Portrait;
			default:
				throw new NotSupportedException("FNA does not support this device orientation.");
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0000EE1C File Offset: 0x0000D01C
		private static void INTERNAL_HandleOrientationChange(DisplayOrientation orientation, GraphicsDevice graphicsDevice, GraphicsAdapter graphicsAdapter, FNAWindow window)
		{
			int backBufferWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
			int backBufferHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
			int num = Math.Min(backBufferWidth, backBufferHeight);
			int num2 = Math.Max(backBufferWidth, backBufferHeight);
			if (orientation == DisplayOrientation.Portrait)
			{
				graphicsDevice.PresentationParameters.BackBufferWidth = num;
				graphicsDevice.PresentationParameters.BackBufferHeight = num2;
			}
			else
			{
				graphicsDevice.PresentationParameters.BackBufferWidth = num2;
				graphicsDevice.PresentationParameters.BackBufferHeight = num;
			}
			graphicsDevice.PresentationParameters.DisplayOrientation = orientation;
			window.CurrentOrientation = orientation;
			graphicsDevice.Reset(graphicsDevice.PresentationParameters, graphicsAdapter);
			window.INTERNAL_OnOrientationChanged();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0000EEAB File Offset: 0x0000D0AB
		public static bool SupportsOrientationChanges()
		{
			return SDL3_FNAPlatform.SupportsOrientations;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0000EEB2 File Offset: 0x0000D0B2
		public static GraphicsAdapter RegisterGame(Game game)
		{
			SDL.SDL_ShowWindow(game.Window.Handle);
			SDL3_FNAPlatform.activeGames.Add(game);
			return SDL3_FNAPlatform.FetchDisplayAdapter(game.Window.Handle, true);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0000EEE1 File Offset: 0x0000D0E1
		public static void UnregisterGame(Game game)
		{
			SDL3_FNAPlatform.activeGames.Remove(game);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public unsafe static void PollEvents(Game game, ref GraphicsAdapter currentAdapter, bool[] textInputControlDown, ref bool textInputSuppress)
		{
			char* ptr = stackalloc char[(UIntPtr)64];
			SDL.SDL_Event sdl_Event;
			while (SDL.SDL_PollEvent(out sdl_Event))
			{
				if (sdl_Event.type == 768U)
				{
					Keys keys = SDL3_FNAPlatform.ToXNAKey(ref sdl_Event.key.key, ref sdl_Event.key.scancode);
					if (!Keyboard.keys.Contains(keys))
					{
						Keyboard.keys.Add(keys);
						int num;
						if (FNAPlatform.TextInputBindings.TryGetValue(keys, out num))
						{
							textInputControlDown[num] = true;
							TextInputEXT.OnTextInput(FNAPlatform.TextInputCharacters[num]);
						}
						else if ((Keyboard.keys.Contains(Keys.LeftControl) || Keyboard.keys.Contains(Keys.RightControl)) && keys == Keys.V)
						{
							textInputControlDown[6] = true;
							TextInputEXT.OnTextInput(FNAPlatform.TextInputCharacters[6]);
							textInputSuppress = true;
						}
					}
					else if (sdl_Event.key.repeat)
					{
						int num2;
						if (FNAPlatform.TextInputBindings.TryGetValue(keys, out num2))
						{
							TextInputEXT.OnTextInput(FNAPlatform.TextInputCharacters[num2]);
						}
						else if ((Keyboard.keys.Contains(Keys.LeftControl) || Keyboard.keys.Contains(Keys.RightControl)) && keys == Keys.V)
						{
							TextInputEXT.OnTextInput(FNAPlatform.TextInputCharacters[6]);
						}
					}
				}
				else if (sdl_Event.type == 769U)
				{
					Keys keys2 = SDL3_FNAPlatform.ToXNAKey(ref sdl_Event.key.key, ref sdl_Event.key.scancode);
					if (Keyboard.keys.Remove(keys2))
					{
						int num3;
						if (FNAPlatform.TextInputBindings.TryGetValue(keys2, out num3))
						{
							textInputControlDown[num3] = false;
						}
						else if ((!Keyboard.keys.Contains(Keys.LeftControl) && !Keyboard.keys.Contains(Keys.RightControl) && textInputControlDown[6]) || keys2 == Keys.V)
						{
							textInputControlDown[6] = false;
							textInputSuppress = false;
						}
					}
				}
				else if (sdl_Event.type == 1025U)
				{
					Mouse.INTERNAL_onClicked((int)(sdl_Event.button.button - 1));
				}
				else if (sdl_Event.type == 1027U)
				{
					Mouse.INTERNAL_MouseWheel += (int)sdl_Event.wheel.y * 120;
				}
				else if (sdl_Event.type == 1792U)
				{
					TouchPanel.TouchDeviceExists = true;
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerID, TouchLocationState.Pressed, sdl_Event.tfinger.x, sdl_Event.tfinger.y, 0f, 0f);
				}
				else if (sdl_Event.type == 1794U)
				{
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerID, TouchLocationState.Moved, sdl_Event.tfinger.x, sdl_Event.tfinger.y, sdl_Event.tfinger.dx, sdl_Event.tfinger.dy);
				}
				else if (sdl_Event.type == 1793U || sdl_Event.type == 1795U)
				{
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerID, TouchLocationState.Released, sdl_Event.tfinger.x, sdl_Event.tfinger.y, 0f, 0f);
				}
				else if (sdl_Event.type >= 514U && sdl_Event.type <= 538U)
				{
					if (sdl_Event.type == 526U)
					{
						game.IsActive = true;
						if (SDL.SDL_GetCurrentVideoDriver() == "x11")
						{
							SDL.SDL_SetWindowFullscreen(game.Window.Handle, game.GraphicsDevice.PresentationParameters.IsFullScreen);
						}
						SDL.SDL_DisableScreenSaver();
					}
					else if (sdl_Event.type == 527U)
					{
						game.IsActive = false;
						if (SDL.SDL_GetCurrentVideoDriver() == "x11")
						{
							SDL.SDL_SetWindowFullscreen(game.Window.Handle, false);
						}
						SDL.SDL_EnableScreenSaver();
					}
					else if (sdl_Event.type == 519U)
					{
						Rectangle windowBounds = SDL3_FNAPlatform.GetWindowBounds(Mouse.WindowHandle);
						Mouse.INTERNAL_WindowWidth = windowBounds.Width;
						Mouse.INTERNAL_WindowHeight = windowBounds.Height;
					}
					else if (sdl_Event.type == 518U)
					{
						SDL.SDL_WindowFlags sdl_WindowFlags = SDL.SDL_GetWindowFlags(game.Window.Handle);
						if ((sdl_WindowFlags & SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE) != (SDL.SDL_WindowFlags)0UL && (sdl_WindowFlags & (SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS | SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS)) != (SDL.SDL_WindowFlags)0UL)
						{
							((FNAWindow)game.Window).INTERNAL_ClientSizeChanged();
						}
					}
					else if (sdl_Event.type == 516U)
					{
						game.RedrawWindow();
					}
					else if (sdl_Event.type == 517U)
					{
						GraphicsAdapter graphicsAdapter = SDL3_FNAPlatform.FetchDisplayAdapter(game.Window.Handle, true);
						if (graphicsAdapter != currentAdapter)
						{
							currentAdapter = graphicsAdapter;
							game.GraphicsDevice.Reset(game.GraphicsDevice.PresentationParameters, currentAdapter);
						}
					}
					else if (sdl_Event.type == 524U)
					{
						SDL.SDL_DisableScreenSaver();
					}
					else if (sdl_Event.type == 525U)
					{
						SDL.SDL_EnableScreenSaver();
					}
					else if (sdl_Event.type == 535U)
					{
						object service = game.Services.GetService(typeof(IGraphicsDeviceManager));
						if (service != null && service is GraphicsDeviceManager)
						{
							((GraphicsDeviceManager)service).IsFullScreen = true;
						}
					}
					else if (sdl_Event.type == 536U)
					{
						object service2 = game.Services.GetService(typeof(IGraphicsDeviceManager));
						if (service2 != null && service2 is GraphicsDeviceManager)
						{
							((GraphicsDeviceManager)service2).IsFullScreen = false;
						}
					}
				}
				else if (sdl_Event.type >= 337U && sdl_Event.type <= 344U)
				{
					GraphicsAdapter.AdaptersChanged();
					currentAdapter = SDL3_FNAPlatform.FetchDisplayAdapter(game.Window.Handle, true);
					if (sdl_Event.type == 337U)
					{
						if (SDL3_FNAPlatform.SupportsOrientationChanges())
						{
							SDL3_FNAPlatform.INTERNAL_HandleOrientationChange(SDL3_FNAPlatform.INTERNAL_ConvertOrientation((SDL.SDL_DisplayOrientation)sdl_Event.display.data1), game.GraphicsDevice, currentAdapter, (FNAWindow)game.Window);
						}
					}
					else
					{
						game.GraphicsDevice.QuietlyUpdateAdapter(currentAdapter);
					}
				}
				else if (sdl_Event.type == 1619U)
				{
					SDL3_FNAPlatform.INTERNAL_AddInstance(sdl_Event.gdevice.which);
				}
				else if (sdl_Event.type == 1620U)
				{
					SDL3_FNAPlatform.INTERNAL_RemoveInstance(sdl_Event.gdevice.which);
				}
				else if (sdl_Event.type == 771U && !textInputSuppress)
				{
					int num4 = SDL3_FNAPlatform.MeasureStringLength(sdl_Event.text.text);
					if (num4 > 0)
					{
						int chars = Encoding.UTF8.GetChars(sdl_Event.text.text, num4, ptr, num4);
						for (int i = 0; i < chars; i++)
						{
							TextInputEXT.OnTextInput(ptr[i]);
						}
					}
				}
				else if (sdl_Event.type == 770U)
				{
					int num5 = SDL3_FNAPlatform.MeasureStringLength(sdl_Event.edit.text);
					if (num5 > 0)
					{
						int chars2 = Encoding.UTF8.GetChars(sdl_Event.edit.text, num5, ptr, num5);
						TextInputEXT.OnTextEditing(new string(ptr, 0, chars2), sdl_Event.edit.start, sdl_Event.edit.length);
					}
					else
					{
						TextInputEXT.OnTextEditing(null, 0, 0);
					}
				}
				else if (sdl_Event.type == 256U)
				{
					game.RunApplication = false;
					return;
				}
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0000F62C File Offset: 0x0000D82C
		private unsafe static int MeasureStringLength(byte* ptr)
		{
			int num = 0;
			while (*ptr != 0)
			{
				ptr++;
				num++;
			}
			return num;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0000F64B File Offset: 0x0000D84B
		public static bool NeedsPlatformMainLoop()
		{
			return SDL.SDL_GetPlatform().Equals("Emscripten");
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0000F65C File Offset: 0x0000D85C
		public static void RunPlatformMainLoop(Game game)
		{
			if (SDL.SDL_GetPlatform().Equals("Emscripten"))
			{
				SDL3_FNAPlatform.emscriptenGame = game;
				SDL3_FNAPlatform.emscripten_set_main_loop(new SDL3_FNAPlatform.em_callback_func(SDL3_FNAPlatform.RunEmscriptenMainLoop), 0, 1);
				return;
			}
			throw new NotSupportedException("Cannot run the main loop of an unknown platform");
		}

		// Token: 0x06000B2F RID: 2863
		[DllImport("__Native", CallingConvention = CallingConvention.Cdecl)]
		private static extern void emscripten_set_main_loop(SDL3_FNAPlatform.em_callback_func func, int fps, int simulate_infinite_loop);

		// Token: 0x06000B30 RID: 2864
		[DllImport("__Native", CallingConvention = CallingConvention.Cdecl)]
		private static extern void emscripten_cancel_main_loop();

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000F693 File Offset: 0x0000D893
		[MonoPInvokeCallback(typeof(SDL3_FNAPlatform.em_callback_func))]
		private static void RunEmscriptenMainLoop()
		{
			SDL3_FNAPlatform.emscriptenGame.RunOneFrame();
			if (!SDL3_FNAPlatform.emscriptenGame.RunApplication)
			{
				SDL3_FNAPlatform.emscriptenGame.Exit();
				SDL3_FNAPlatform.emscripten_cancel_main_loop();
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0000F6BC File Offset: 0x0000D8BC
		private static GraphicsAdapter FetchDisplayAdapter(IntPtr window, bool retry = true)
		{
			uint num = SDL.SDL_GetDisplayForWindow(window);
			int num2 = -1;
			for (int i = 0; i < SDL3_FNAPlatform.displayIds.Length; i++)
			{
				if (num == SDL3_FNAPlatform.displayIds[i])
				{
					num2 = i;
					break;
				}
			}
			if (num2 >= 0 && num2 <= GraphicsAdapter.Adapters.Count)
			{
				return GraphicsAdapter.Adapters[num2];
			}
			FNALoggerEXT.LogWarn("SDL3 Window ID and Display ID desync'd");
			if (retry)
			{
				GraphicsAdapter.AdaptersChanged();
				return SDL3_FNAPlatform.FetchDisplayAdapter(window, false);
			}
			FNALoggerEXT.LogWarn("SDL3 Window ID and Display ID desync'd really badly");
			return GraphicsAdapter.DefaultAdapter;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0000F744 File Offset: 0x0000D944
		public unsafe static GraphicsAdapter[] GetGraphicsAdapters()
		{
			int num;
			uint* ptr = (uint*)(void*)SDL.SDL_GetDisplays(out num);
			GraphicsAdapter[] array = new GraphicsAdapter[num];
			SDL3_FNAPlatform.displayIds = new uint[num];
			for (int i = 0; i < array.Length; i++)
			{
				List<DisplayMode> list = new List<DisplayMode>();
				int num2;
				SDL.SDL_DisplayMode** ptr2 = (SDL.SDL_DisplayMode**)(void*)SDL.SDL_GetFullscreenDisplayModes(ptr[i], out num2);
				for (int j = num2 - 1; j >= 0; j--)
				{
					bool flag = false;
					foreach (DisplayMode displayMode in list)
					{
						if (((IntPtr*)(ptr2 + (IntPtr)j * (IntPtr)sizeof(SDL.SDL_DisplayMode*) / (IntPtr)sizeof(SDL.SDL_DisplayMode*)))->w == displayMode.Width && ((IntPtr*)(ptr2 + (IntPtr)j * (IntPtr)sizeof(SDL.SDL_DisplayMode*) / (IntPtr)sizeof(SDL.SDL_DisplayMode*)))->h == displayMode.Height)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						list.Add(new DisplayMode(((IntPtr*)(ptr2 + (IntPtr)j * (IntPtr)sizeof(SDL.SDL_DisplayMode*) / (IntPtr)sizeof(SDL.SDL_DisplayMode*)))->w, ((IntPtr*)(ptr2 + (IntPtr)j * (IntPtr)sizeof(SDL.SDL_DisplayMode*) / (IntPtr)sizeof(SDL.SDL_DisplayMode*)))->h, SurfaceFormat.Color));
					}
				}
				SDL.SDL_free((IntPtr)((void*)ptr2));
				array[i] = new GraphicsAdapter(new DisplayModeCollection(list), "\\\\.\\DISPLAY" + (i + 1).ToString(), SDL.SDL_GetDisplayName(ptr[i]));
				SDL3_FNAPlatform.displayIds[i] = ptr[i];
			}
			SDL.SDL_free((IntPtr)((void*)ptr));
			return array;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public unsafe static DisplayMode GetCurrentDisplayMode(int adapterIndex)
		{
			SDL.SDL_DisplayMode* ptr = (SDL.SDL_DisplayMode*)(void*)SDL.SDL_GetCurrentDisplayMode(SDL3_FNAPlatform.displayIds[adapterIndex]);
			return new DisplayMode(ptr->w, ptr->h, SurfaceFormat.Color);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000F8F5 File Offset: 0x0000DAF5
		public static IntPtr GetMonitorHandle(int adapterIndex)
		{
			return new IntPtr((int)SDL3_FNAPlatform.displayIds[adapterIndex]);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0000F904 File Offset: 0x0000DB04
		public static void GetMouseState(IntPtr window, out int x, out int y, out ButtonState left, out ButtonState middle, out ButtonState right, out ButtonState x1, out ButtonState x2)
		{
			float num;
			float num2;
			SDL.SDL_MouseButtonFlags sdl_MouseButtonFlags;
			if (SDL3_FNAPlatform.GetRelativeMouseMode(window))
			{
				sdl_MouseButtonFlags = SDL.SDL_GetRelativeMouseState(out num, out num2);
			}
			else if (SDL3_FNAPlatform.SupportsGlobalMouse)
			{
				sdl_MouseButtonFlags = SDL.SDL_GetGlobalMouseState(out num, out num2);
				int num3 = 0;
				int num4 = 0;
				SDL.SDL_GetWindowPosition(window, out num3, out num4);
				num -= (float)num3;
				num2 -= (float)num4;
			}
			else
			{
				sdl_MouseButtonFlags = SDL.SDL_GetMouseState(out num, out num2);
			}
			x = (int)num;
			y = (int)num2;
			left = (ButtonState)(sdl_MouseButtonFlags & SDL.SDL_MouseButtonFlags.SDL_BUTTON_LMASK);
			middle = (ButtonState)((sdl_MouseButtonFlags & SDL.SDL_MouseButtonFlags.SDL_BUTTON_MMASK) >> 1);
			right = (ButtonState)((sdl_MouseButtonFlags & SDL.SDL_MouseButtonFlags.SDL_BUTTON_RMASK) >> 2);
			x1 = (ButtonState)((sdl_MouseButtonFlags & SDL.SDL_MouseButtonFlags.SDL_BUTTON_X1MASK) >> 3);
			x2 = (ButtonState)((sdl_MouseButtonFlags & SDL.SDL_MouseButtonFlags.SDL_BUTTON_X2MASK) >> 4);
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0000F98B File Offset: 0x0000DB8B
		public static void WarpMouseInWindow(IntPtr window, int x, int y)
		{
			SDL.SDL_WarpMouseInWindow(window, (float)x, (float)y);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0000F997 File Offset: 0x0000DB97
		public static void OnIsMouseVisibleChanged(bool visible)
		{
			if (visible)
			{
				SDL.SDL_ShowCursor();
				return;
			}
			SDL.SDL_HideCursor();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000F9A9 File Offset: 0x0000DBA9
		public static bool GetRelativeMouseMode(IntPtr window)
		{
			return SDL.SDL_GetWindowRelativeMouseMode(window);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		public static void SetRelativeMouseMode(IntPtr window, bool enable)
		{
			SDL.SDL_SetWindowRelativeMouseMode(window, enable);
			if (enable)
			{
				float num;
				SDL.SDL_GetRelativeMouseState(out num, out num);
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0000F9E0 File Offset: 0x0000DBE0
		private static string GetBaseDirectory()
		{
			if (Environment.GetEnvironmentVariable("FNA_SDL_FORCE_BASE_PATH") != "1" && (SDL3_FNAPlatform.OSVersion.Equals("Windows") || SDL3_FNAPlatform.OSVersion.Equals("macOS") || SDL3_FNAPlatform.OSVersion.Equals("Linux") || SDL3_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL3_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL3_FNAPlatform.OSVersion.Equals("NetBSD")))
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
			string text = SDL.SDL_GetBasePath();
			if (string.IsNullOrEmpty(text))
			{
				text = AppDomain.CurrentDomain.BaseDirectory;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = Environment.CurrentDirectory;
			}
			return text;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public static string GetStorageRoot()
		{
			string text = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName).Replace(".vshost", "");
			if (SDL3_FNAPlatform.OSVersion.Equals("Windows"))
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedGames", text);
			}
			if (SDL3_FNAPlatform.OSVersion.Equals("macOS"))
			{
				string environmentVariable = Environment.GetEnvironmentVariable("HOME");
				if (string.IsNullOrEmpty(environmentVariable))
				{
					return ".";
				}
				return Path.Combine(environmentVariable, "Library/Application Support", text);
			}
			else
			{
				if (SDL3_FNAPlatform.OSVersion.Equals("Linux") || SDL3_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL3_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL3_FNAPlatform.OSVersion.Equals("NetBSD"))
				{
					string text2 = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
					if (string.IsNullOrEmpty(text2))
					{
						text2 = Environment.GetEnvironmentVariable("HOME");
						if (string.IsNullOrEmpty(text2))
						{
							return ".";
						}
						text2 += "/.local/share";
					}
					return Path.Combine(text2, text);
				}
				return SDL.SDL_GetPrefPath(null, text);
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public static DriveInfo GetDriveInfo(string storageRoot)
		{
			DriveInfo driveInfo;
			try
			{
				driveInfo = new DriveInfo(SDL3_FNAPlatform.MonoPathRootWorkaround(storageRoot));
			}
			catch (Exception ex)
			{
				FNALoggerEXT.LogError("Failed to get DriveInfo: " + ex.ToString());
				driveInfo = null;
			}
			return driveInfo;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		private static string MonoPathRootWorkaround(string storageRoot)
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				return Path.GetPathRoot(storageRoot);
			}
			if (storageRoot == null)
			{
				return null;
			}
			if (storageRoot.Trim().Length == 0)
			{
				throw new ArgumentException("The specified path is not of a legal form.");
			}
			if (!Path.IsPathRooted(storageRoot) && !storageRoot.Contains(":"))
			{
				return string.Empty;
			}
			int num = -1;
			int num2 = 0;
			string[] logicalDrives = Environment.GetLogicalDrives();
			for (int i = 0; i < logicalDrives.Length; i++)
			{
				if (!string.IsNullOrEmpty(logicalDrives[i]))
				{
					string text = logicalDrives[i];
					if (text[text.Length - 1] != Path.DirectorySeparatorChar)
					{
						text += Path.DirectorySeparatorChar.ToString();
					}
					if (storageRoot.StartsWith(text) && text.Length > num2)
					{
						num = i;
						num2 = text.Length;
					}
				}
			}
			if (num >= 0)
			{
				return logicalDrives[num];
			}
			return Path.GetPathRoot(storageRoot);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public static IntPtr ReadToPointer(string path, out IntPtr size)
		{
			UIntPtr uintPtr;
			IntPtr intPtr = SDL.SDL_LoadFile(path, out uintPtr);
			size = (IntPtr)uintPtr.ToPointer();
			return intPtr;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		public static void FreeFilePointer(IntPtr file)
		{
			SDL.SDL_free(file);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0000FD03 File Offset: 0x0000DF03
		public static void ShowRuntimeError(string title, string message)
		{
			SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, title ?? "", message ?? "", IntPtr.Zero);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0000FD28 File Offset: 0x0000DF28
		public unsafe static Microphone[] GetMicrophones()
		{
			if (!SDL3_FNAPlatform.micInit)
			{
				SDL.SDL_InitSubSystem(SDL.SDL_InitFlags.SDL_INIT_AUDIO);
				SDL3_FNAPlatform.micStreams = new Dictionary<uint, IntPtr>();
				SDL3_FNAPlatform.micInit = true;
			}
			int num;
			uint* ptr = (uint*)(void*)SDL.SDL_GetAudioRecordingDevices(out num);
			if (num < 1)
			{
				return new Microphone[0];
			}
			Microphone[] array = new Microphone[num + 1];
			SDL.SDL_AudioSpec sdl_AudioSpec = default(SDL.SDL_AudioSpec);
			sdl_AudioSpec.freq = 44100;
			sdl_AudioSpec.format = SDL.SDL_AudioFormat.SDL_AUDIO_S16LE;
			sdl_AudioSpec.channels = 1;
			array[0] = new Microphone(SDL.SDL_OpenAudioDevice(4294967294U, ref sdl_AudioSpec), "Default Device");
			for (int i = 0; i < num; i++)
			{
				string text = SDL.SDL_GetAudioDeviceName(ptr[i]);
				array[i + 1] = new Microphone(SDL.SDL_OpenAudioDevice(ptr[i], ref sdl_AudioSpec), text);
				SDL.SDL_AudioSpec sdl_AudioSpec2;
				int num2;
				SDL.SDL_GetAudioDeviceFormat(ptr[i], out sdl_AudioSpec2, out num2);
				IntPtr intPtr = SDL.SDL_CreateAudioStream(ref sdl_AudioSpec, ref sdl_AudioSpec2);
				SDL.SDL_BindAudioStream(ptr[i], intPtr);
				SDL3_FNAPlatform.micStreams.Add(ptr[i], intPtr);
			}
			SDL.SDL_free((IntPtr)((void*)ptr));
			return array;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0000FE3C File Offset: 0x0000E03C
		public unsafe static int GetMicrophoneSamples(uint handle, byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				return SDL.SDL_GetAudioStreamData(SDL3_FNAPlatform.micStreams[handle], (IntPtr)((void*)ptr2), count);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0000FE6B File Offset: 0x0000E06B
		public static int GetMicrophoneQueuedBytes(uint handle)
		{
			return SDL.SDL_GetAudioStreamQueued(SDL3_FNAPlatform.micStreams[handle]);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0000FE7D File Offset: 0x0000E07D
		public static void StartMicrophone(uint handle)
		{
			SDL.SDL_ResumeAudioDevice(handle);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0000FE86 File Offset: 0x0000E086
		public static void StopMicrophone(uint handle)
		{
			SDL.SDL_PauseAudioDevice(handle);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000FE90 File Offset: 0x0000E090
		public static GamePadCapabilities GetGamePadCapabilities(int index)
		{
			if (SDL3_FNAPlatform.INTERNAL_devices[index] == IntPtr.Zero)
			{
				return default(GamePadCapabilities);
			}
			return SDL3_FNAPlatform.INTERNAL_capabilities[index];
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public static GamePadState GetGamePadState(int index, GamePadDeadZone deadZoneMode)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				return default(GamePadState);
			}
			Vector2 vector = new Vector2((float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTX) / 32767f, (float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTY) / -32767f);
			Vector2 vector2 = new Vector2((float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTX) / 32767f, (float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTY) / -32767f);
			float num = (float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFT_TRIGGER) / 32767f;
			float num2 = (float)SDL.SDL_GetGamepadAxis(intPtr, SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHT_TRIGGER) / 32767f;
			Buttons buttons = (Buttons)0;
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_SOUTH))
			{
				buttons |= Buttons.A;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_EAST))
			{
				buttons |= Buttons.B;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_WEST))
			{
				buttons |= Buttons.X;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_NORTH))
			{
				buttons |= Buttons.Y;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_BACK))
			{
				buttons |= Buttons.Back;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_GUIDE))
			{
				buttons |= Buttons.BigButton;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_START))
			{
				buttons |= Buttons.Start;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_STICK))
			{
				buttons |= Buttons.LeftStick;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_STICK))
			{
				buttons |= Buttons.RightStick;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_SHOULDER))
			{
				buttons |= Buttons.LeftShoulder;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER))
			{
				buttons |= Buttons.RightShoulder;
			}
			ButtonState buttonState = ButtonState.Released;
			ButtonState buttonState2 = ButtonState.Released;
			ButtonState buttonState3 = ButtonState.Released;
			ButtonState buttonState4 = ButtonState.Released;
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_UP))
			{
				buttons |= Buttons.DPadUp;
				buttonState = ButtonState.Pressed;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_DOWN))
			{
				buttons |= Buttons.DPadDown;
				buttonState2 = ButtonState.Pressed;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_LEFT))
			{
				buttons |= Buttons.DPadLeft;
				buttonState3 = ButtonState.Pressed;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_RIGHT))
			{
				buttons |= Buttons.DPadRight;
				buttonState4 = ButtonState.Pressed;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC1))
			{
				buttons |= Buttons.Misc1EXT;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1))
			{
				buttons |= Buttons.Paddle1EXT;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE1))
			{
				buttons |= Buttons.Paddle2EXT;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2))
			{
				buttons |= Buttons.Paddle3EXT;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE2))
			{
				buttons |= Buttons.Paddle4EXT;
			}
			if (SDL.SDL_GetGamepadButton(intPtr, SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_TOUCHPAD))
			{
				buttons |= Buttons.TouchPadEXT;
			}
			GamePadState gamePadState = new GamePadState(new GamePadThumbSticks(vector, vector2, deadZoneMode), new GamePadTriggers(num, num2, deadZoneMode), new GamePadButtons(buttons), new GamePadDPad(buttonState, buttonState2, buttonState3, buttonState4))
			{
				IsConnected = true,
				PacketNumber = SDL3_FNAPlatform.INTERNAL_states[index].PacketNumber
			};
			if (gamePadState != SDL3_FNAPlatform.INTERNAL_states[index])
			{
				gamePadState.PacketNumber++;
				SDL3_FNAPlatform.INTERNAL_states[index] = gamePadState;
			}
			return gamePadState;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000101E0 File Offset: 0x0000E3E0
		public static bool SetGamePadVibration(int index, float leftMotor, float rightMotor)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			return !(intPtr == IntPtr.Zero) && SDL.SDL_RumbleGamepad(intPtr, (ushort)(MathHelper.Clamp(leftMotor, 0f, 1f) * 65535f), (ushort)(MathHelper.Clamp(rightMotor, 0f, 1f) * 65535f), 0U);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00010240 File Offset: 0x0000E440
		public static bool SetGamePadTriggerVibration(int index, float leftTrigger, float rightTrigger)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			return !(intPtr == IntPtr.Zero) && SDL.SDL_RumbleGamepadTriggers(intPtr, (ushort)(MathHelper.Clamp(leftTrigger, 0f, 1f) * 65535f), (ushort)(MathHelper.Clamp(rightTrigger, 0f, 1f) * 65535f), 0U);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001029E File Offset: 0x0000E49E
		public static string GetGamePadGUID(int index)
		{
			return SDL3_FNAPlatform.INTERNAL_guids[index];
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public static void SetGamePadLightBar(int index, Color color)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			SDL.SDL_SetGamepadLED(intPtr, color.R, color.G, color.B);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public unsafe static bool GetGamePadGyro(int index, out Vector3 gyro)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				gyro = Vector3.Zero;
				return false;
			}
			if (!SDL.SDL_GamepadSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO))
			{
				SDL.SDL_SetGamepadSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO, true);
			}
			float* ptr = stackalloc float[(UIntPtr)12];
			if (!SDL.SDL_GetGamepadSensorData(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO, ptr, 3))
			{
				gyro = Vector3.Zero;
				return false;
			}
			gyro.X = *ptr;
			gyro.Y = ptr[1];
			gyro.Z = ptr[2];
			return true;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00010378 File Offset: 0x0000E578
		public unsafe static bool GetGamePadAccelerometer(int index, out Vector3 accel)
		{
			IntPtr intPtr = SDL3_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				accel = Vector3.Zero;
				return false;
			}
			if (!SDL.SDL_GamepadSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL))
			{
				SDL.SDL_SetGamepadSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL, true);
			}
			float* ptr = stackalloc float[(UIntPtr)12];
			if (!SDL.SDL_GetGamepadSensorData(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL, ptr, 3))
			{
				accel = Vector3.Zero;
				return false;
			}
			accel.X = *ptr;
			accel.Y = ptr[1];
			accel.Z = ptr[2];
			return true;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00010408 File Offset: 0x0000E608
		private static void INTERNAL_AddInstance(uint dev)
		{
			int num = -1;
			for (int i = 0; i < SDL3_FNAPlatform.INTERNAL_devices.Length; i++)
			{
				if (SDL3_FNAPlatform.INTERNAL_devices[i] == IntPtr.Zero)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return;
			}
			SDL.SDL_ClearError();
			SDL3_FNAPlatform.INTERNAL_devices[num] = SDL.SDL_OpenGamepad(dev);
			IntPtr intPtr = SDL.SDL_GetGamepadJoystick(SDL3_FNAPlatform.INTERNAL_devices[num]);
			uint num2 = SDL.SDL_GetJoystickID(intPtr);
			if (SDL3_FNAPlatform.INTERNAL_instanceList.ContainsKey(num2))
			{
				SDL3_FNAPlatform.INTERNAL_devices[num] = IntPtr.Zero;
				return;
			}
			SDL3_FNAPlatform.INTERNAL_instanceList.Add(num2, num);
			SDL3_FNAPlatform.INTERNAL_states[num] = default(GamePadState);
			SDL3_FNAPlatform.INTERNAL_states[num].IsConnected = true;
			bool flag = SDL.SDL_RumbleGamepad(SDL3_FNAPlatform.INTERNAL_devices[num], 0, 0, 0U);
			bool flag2 = SDL.SDL_RumbleGamepadTriggers(SDL3_FNAPlatform.INTERNAL_devices[num], 0, 0, 0U);
			uint num3 = SDL.SDL_GetGamepadProperties(SDL3_FNAPlatform.INTERNAL_devices[num]);
			GamePadCapabilities gamePadCapabilities = default(GamePadCapabilities);
			gamePadCapabilities.IsConnected = true;
			gamePadCapabilities.GamePadType = SDL3_FNAPlatform.INTERNAL_gamepadType[(int)SDL.SDL_GetJoystickType(intPtr)];
			gamePadCapabilities.HasAButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_SOUTH);
			gamePadCapabilities.HasBButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_EAST);
			gamePadCapabilities.HasXButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_WEST);
			gamePadCapabilities.HasYButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_NORTH);
			gamePadCapabilities.HasBackButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_BACK);
			gamePadCapabilities.HasBigButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_GUIDE);
			gamePadCapabilities.HasStartButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_START);
			gamePadCapabilities.HasLeftStickButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_STICK);
			gamePadCapabilities.HasRightStickButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_STICK);
			gamePadCapabilities.HasLeftShoulderButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_SHOULDER);
			gamePadCapabilities.HasRightShoulderButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_SHOULDER);
			gamePadCapabilities.HasDPadUpButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_UP);
			gamePadCapabilities.HasDPadDownButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_DOWN);
			gamePadCapabilities.HasDPadLeftButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_LEFT);
			gamePadCapabilities.HasDPadRightButton = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_DPAD_RIGHT);
			gamePadCapabilities.HasLeftXThumbStick = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTX);
			gamePadCapabilities.HasLeftYThumbStick = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFTY);
			gamePadCapabilities.HasRightXThumbStick = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTX);
			gamePadCapabilities.HasRightYThumbStick = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHTY);
			gamePadCapabilities.HasLeftTrigger = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_LEFT_TRIGGER);
			gamePadCapabilities.HasRightTrigger = SDL.SDL_GamepadHasAxis(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadAxis.SDL_GAMEPAD_AXIS_RIGHT_TRIGGER);
			gamePadCapabilities.HasLeftVibrationMotor = flag;
			gamePadCapabilities.HasRightVibrationMotor = flag;
			gamePadCapabilities.HasVoiceSupport = false;
			gamePadCapabilities.HasLightBarEXT = SDL.SDL_GetBooleanProperty(num3, "SDL.joystick.cap.rgb_led", false);
			gamePadCapabilities.HasTriggerVibrationMotorsEXT = flag2;
			gamePadCapabilities.HasMisc1EXT = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_MISC1);
			gamePadCapabilities.HasPaddle1EXT = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE1);
			gamePadCapabilities.HasPaddle2EXT = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE1);
			gamePadCapabilities.HasPaddle3EXT = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_RIGHT_PADDLE2);
			gamePadCapabilities.HasPaddle4EXT = SDL.SDL_GamepadHasButton(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GamepadButton.SDL_GAMEPAD_BUTTON_LEFT_PADDLE2);
			gamePadCapabilities.HasTouchPadEXT = SDL.SDL_GetNumGamepadTouchpads(SDL3_FNAPlatform.INTERNAL_devices[num]) > 0;
			gamePadCapabilities.HasGyroEXT = SDL.SDL_GamepadHasSensor(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_SensorType.SDL_SENSOR_GYRO);
			gamePadCapabilities.HasAccelerometerEXT = SDL.SDL_GamepadHasSensor(SDL3_FNAPlatform.INTERNAL_devices[num], SDL.SDL_SensorType.SDL_SENSOR_ACCEL);
			SDL3_FNAPlatform.INTERNAL_capabilities[num] = gamePadCapabilities;
			ushort num4 = SDL.SDL_GetJoystickVendor(intPtr);
			ushort num5 = SDL.SDL_GetJoystickProduct(intPtr);
			if (num4 == 0 && num5 == 0)
			{
				SDL3_FNAPlatform.INTERNAL_guids[num] = "xinput";
			}
			else
			{
				SDL3_FNAPlatform.INTERNAL_guids[num] = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
				{
					(int)(num4 & 255),
					num4 >> 8,
					(int)(num5 & 255),
					num5 >> 8
				});
			}
			if (num4 == 10462)
			{
				SDL.SDL_GamepadType sdl_GamepadType = SDL.SDL_GetGamepadType(SDL3_FNAPlatform.INTERNAL_devices[num]);
				if (sdl_GamepadType == SDL.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOX360 || sdl_GamepadType == SDL.SDL_GamepadType.SDL_GAMEPAD_TYPE_XBOXONE)
				{
					SDL3_FNAPlatform.INTERNAL_guids[num] = "xinput";
				}
				else if (sdl_GamepadType == SDL.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS4)
				{
					SDL3_FNAPlatform.INTERNAL_guids[num] = "4c05c405";
				}
				else if (sdl_GamepadType == SDL.SDL_GamepadType.SDL_GAMEPAD_TYPE_PS5)
				{
					SDL3_FNAPlatform.INTERNAL_guids[num] = "4c05e60c";
				}
			}
			string text = SDL.SDL_GetGamepadMapping(SDL3_FNAPlatform.INTERNAL_devices[num]);
			string text2;
			if (string.IsNullOrEmpty(text))
			{
				text2 = "Mapping not found";
			}
			else
			{
				text2 = "Mapping: " + text;
			}
			FNALoggerEXT.LogInfo(string.Concat(new string[]
			{
				"Controller ",
				num.ToString(),
				": ",
				SDL.SDL_GetGamepadName(SDL3_FNAPlatform.INTERNAL_devices[num]),
				", GUID: ",
				SDL3_FNAPlatform.INTERNAL_guids[num],
				", ",
				text2
			}));
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00010994 File Offset: 0x0000EB94
		private static void INTERNAL_RemoveInstance(uint dev)
		{
			int num;
			if (!SDL3_FNAPlatform.INTERNAL_instanceList.TryGetValue(dev, out num))
			{
				return;
			}
			SDL3_FNAPlatform.INTERNAL_instanceList.Remove(dev);
			SDL.SDL_CloseGamepad(SDL3_FNAPlatform.INTERNAL_devices[num]);
			SDL3_FNAPlatform.INTERNAL_devices[num] = IntPtr.Zero;
			SDL3_FNAPlatform.INTERNAL_states[num] = default(GamePadState);
			SDL3_FNAPlatform.INTERNAL_guids[num] = string.Empty;
			SDL.SDL_ClearError();
			FNALoggerEXT.LogInfo("Removed device, player: " + num.ToString());
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00010A14 File Offset: 0x0000EC14
		private static string[] GenStringArray()
		{
			string[] array = new string[GamePad.GAMEPAD_COUNT];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Empty;
			}
			return array;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00010A44 File Offset: 0x0000EC44
		public static TouchPanelCapabilities GetTouchCapabilities()
		{
			int num;
			SDL.SDL_free(SDL.SDL_GetTouchDevices(out num));
			bool flag = num > 0;
			return new TouchPanelCapabilities(flag, flag ? 4 : 0);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00010A70 File Offset: 0x0000EC70
		public unsafe static void UpdateTouchPanelState()
		{
			int num;
			IntPtr intPtr = SDL.SDL_GetTouchFingers(SDL3_FNAPlatform.GetTouchDeviceId(0), out num);
			for (int i = 0; i < 8; i++)
			{
				if (i >= num)
				{
					TouchPanel.SetFinger(i, -1, Vector2.Zero);
				}
				else
				{
					SDL.SDL_Finger* ptr = *(IntPtr*)((byte*)(void*)intPtr + (IntPtr)i * (IntPtr)sizeof(SDL.SDL_Finger*));
					TouchPanel.SetFinger(i, (int)ptr->id, new Vector2((float)Math.Round((double)(ptr->x * (float)TouchPanel.DisplayWidth)), (float)Math.Round((double)(ptr->y * (float)TouchPanel.DisplayHeight))));
				}
			}
			SDL.SDL_free(intPtr);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public static int GetNumTouchFingers()
		{
			int num;
			SDL.SDL_free(SDL.SDL_GetTouchFingers(SDL3_FNAPlatform.GetTouchDeviceId(0), out num));
			return num;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00010B1C File Offset: 0x0000ED1C
		private unsafe static ulong GetTouchDeviceId(int index)
		{
			int num;
			IntPtr intPtr = SDL.SDL_GetTouchDevices(out num);
			ulong num2 = (ulong)((index >= 0 && index < num) ? (*(long*)((byte*)(void*)intPtr + (IntPtr)index * 8)) : 0L);
			SDL.SDL_free(intPtr);
			return num2;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00010B4F File Offset: 0x0000ED4F
		public static bool IsTextInputActive(IntPtr window)
		{
			return SDL.SDL_TextInputActive(window);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00010B5C File Offset: 0x0000ED5C
		public static void StartTextInput(IntPtr window)
		{
			SDL.SDL_StartTextInput(window);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00010B65 File Offset: 0x0000ED65
		public static void StopTextInput(IntPtr window)
		{
			SDL.SDL_StopTextInput(window);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00010B70 File Offset: 0x0000ED70
		private static Keys ToXNAKey(ref uint sym, ref SDL.SDL_Scancode scancode)
		{
			Keys keys;
			if (SDL3_FNAPlatform.UseScancodes)
			{
				if (SDL3_FNAPlatform.INTERNAL_scanMap.TryGetValue((int)scancode, out keys))
				{
					return keys;
				}
			}
			else if (SDL3_FNAPlatform.INTERNAL_keyMap.TryGetValue((int)sym, out keys))
			{
				return keys;
			}
			FNALoggerEXT.LogWarn("KEY/SCANCODE MISSING FROM SDL3->XNA DICTIONARY: " + sym.ToString() + " " + scancode.ToString());
			return Keys.None;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00010BD4 File Offset: 0x0000EDD4
		public static Keys GetKeyFromScancode(Keys scancode)
		{
			if (SDL3_FNAPlatform.UseScancodes)
			{
				return scancode;
			}
			SDL.SDL_Scancode sdl_Scancode;
			if (SDL3_FNAPlatform.INTERNAL_xnaMap.TryGetValue((int)scancode, out sdl_Scancode))
			{
				uint num = SDL.SDL_GetKeyFromScancode(sdl_Scancode, SDL.SDL_Keymod.SDL_KMOD_NONE, true);
				Keys keys;
				if (SDL3_FNAPlatform.INTERNAL_keyMap.TryGetValue((int)num, out keys))
				{
					return keys;
				}
				FNALoggerEXT.LogWarn("KEYCODE MISSING FROM SDL3->XNA DICTIONARY: " + num.ToString());
			}
			else
			{
				FNALoggerEXT.LogWarn("SCANCODE MISSING FROM XNA->SDL3 DICTIONARY: " + scancode.ToString());
			}
			return Keys.None;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00010C58 File Offset: 0x0000EE58
		private unsafe static bool Win32OnPaint(IntPtr userdata, SDL.SDL_Event* evt)
		{
			if (evt->type == 516U)
			{
				foreach (Game game in SDL3_FNAPlatform.activeGames)
				{
					if (game.Window != null && evt->window.windowID == SDL.SDL_GetWindowID(game.Window.Handle))
					{
						game.RedrawWindow();
						return false;
					}
				}
			}
			return SDL3_FNAPlatform.prevEventFilter == null || SDL3_FNAPlatform.prevEventFilter(userdata, evt);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00010CF8 File Offset: 0x0000EEF8
		// Note: this type is marked as 'beforefieldinit'.
		static SDL3_FNAPlatform()
		{
		}

		// Token: 0x0400051B RID: 1307
		private static string OSVersion;

		// Token: 0x0400051C RID: 1308
		private static readonly bool UseScancodes = Environment.GetEnvironmentVariable("FNA_KEYBOARD_USE_SCANCODES") == "1";

		// Token: 0x0400051D RID: 1309
		private static bool SupportsGlobalMouse;

		// Token: 0x0400051E RID: 1310
		private static bool SupportsOrientations;

		// Token: 0x0400051F RID: 1311
		private static List<Game> activeGames = new List<Game>();

		// Token: 0x04000520 RID: 1312
		private static Game emscriptenGame;

		// Token: 0x04000521 RID: 1313
		private static uint[] displayIds;

		// Token: 0x04000522 RID: 1314
		private static bool micInit = false;

		// Token: 0x04000523 RID: 1315
		private static Dictionary<uint, IntPtr> micStreams;

		// Token: 0x04000524 RID: 1316
		private static IntPtr[] INTERNAL_devices = new IntPtr[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000525 RID: 1317
		private static Dictionary<uint, int> INTERNAL_instanceList = new Dictionary<uint, int>();

		// Token: 0x04000526 RID: 1318
		private static string[] INTERNAL_guids = SDL3_FNAPlatform.GenStringArray();

		// Token: 0x04000527 RID: 1319
		private static GamePadState[] INTERNAL_states = new GamePadState[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000528 RID: 1320
		private static GamePadCapabilities[] INTERNAL_capabilities = new GamePadCapabilities[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000529 RID: 1321
		private static readonly GamePadType[] INTERNAL_gamepadType = new GamePadType[]
		{
			GamePadType.Unknown,
			GamePadType.GamePad,
			GamePadType.Wheel,
			GamePadType.ArcadeStick,
			GamePadType.FlightStick,
			GamePadType.DancePad,
			GamePadType.Guitar,
			GamePadType.DrumKit,
			GamePadType.BigButtonPad
		};

		// Token: 0x0400052A RID: 1322
		private static Dictionary<int, Keys> INTERNAL_keyMap = new Dictionary<int, Keys>
		{
			{
				97,
				Keys.A
			},
			{
				98,
				Keys.B
			},
			{
				99,
				Keys.C
			},
			{
				100,
				Keys.D
			},
			{
				101,
				Keys.E
			},
			{
				102,
				Keys.F
			},
			{
				103,
				Keys.G
			},
			{
				104,
				Keys.H
			},
			{
				105,
				Keys.I
			},
			{
				106,
				Keys.J
			},
			{
				107,
				Keys.K
			},
			{
				108,
				Keys.L
			},
			{
				109,
				Keys.M
			},
			{
				110,
				Keys.N
			},
			{
				111,
				Keys.O
			},
			{
				112,
				Keys.P
			},
			{
				113,
				Keys.Q
			},
			{
				114,
				Keys.R
			},
			{
				115,
				Keys.S
			},
			{
				116,
				Keys.T
			},
			{
				117,
				Keys.U
			},
			{
				118,
				Keys.V
			},
			{
				119,
				Keys.W
			},
			{
				120,
				Keys.X
			},
			{
				121,
				Keys.Y
			},
			{
				122,
				Keys.Z
			},
			{
				48,
				Keys.D0
			},
			{
				49,
				Keys.D1
			},
			{
				50,
				Keys.D2
			},
			{
				51,
				Keys.D3
			},
			{
				52,
				Keys.D4
			},
			{
				53,
				Keys.D5
			},
			{
				54,
				Keys.D6
			},
			{
				55,
				Keys.D7
			},
			{
				56,
				Keys.D8
			},
			{
				57,
				Keys.D9
			},
			{
				1073741922,
				Keys.NumPad0
			},
			{
				1073741913,
				Keys.NumPad1
			},
			{
				1073741914,
				Keys.NumPad2
			},
			{
				1073741915,
				Keys.NumPad3
			},
			{
				1073741916,
				Keys.NumPad4
			},
			{
				1073741917,
				Keys.NumPad5
			},
			{
				1073741918,
				Keys.NumPad6
			},
			{
				1073741919,
				Keys.NumPad7
			},
			{
				1073741920,
				Keys.NumPad8
			},
			{
				1073741921,
				Keys.NumPad9
			},
			{
				1073742040,
				Keys.OemClear
			},
			{
				1073742044,
				Keys.Decimal
			},
			{
				1073741908,
				Keys.Divide
			},
			{
				1073741912,
				Keys.Enter
			},
			{
				1073741910,
				Keys.Subtract
			},
			{
				1073741909,
				Keys.Multiply
			},
			{
				1073741923,
				Keys.OemPeriod
			},
			{
				1073741911,
				Keys.Add
			},
			{
				1073741882,
				Keys.F1
			},
			{
				1073741883,
				Keys.F2
			},
			{
				1073741884,
				Keys.F3
			},
			{
				1073741885,
				Keys.F4
			},
			{
				1073741886,
				Keys.F5
			},
			{
				1073741887,
				Keys.F6
			},
			{
				1073741888,
				Keys.F7
			},
			{
				1073741889,
				Keys.F8
			},
			{
				1073741890,
				Keys.F9
			},
			{
				1073741891,
				Keys.F10
			},
			{
				1073741892,
				Keys.F11
			},
			{
				1073741893,
				Keys.F12
			},
			{
				1073741928,
				Keys.F13
			},
			{
				1073741929,
				Keys.F14
			},
			{
				1073741930,
				Keys.F15
			},
			{
				1073741931,
				Keys.F16
			},
			{
				1073741932,
				Keys.F17
			},
			{
				1073741933,
				Keys.F18
			},
			{
				1073741934,
				Keys.F19
			},
			{
				1073741935,
				Keys.F20
			},
			{
				1073741936,
				Keys.F21
			},
			{
				1073741937,
				Keys.F22
			},
			{
				1073741938,
				Keys.F23
			},
			{
				1073741939,
				Keys.F24
			},
			{
				32,
				Keys.Space
			},
			{
				1073741906,
				Keys.Up
			},
			{
				1073741905,
				Keys.Down
			},
			{
				1073741904,
				Keys.Left
			},
			{
				1073741903,
				Keys.Right
			},
			{
				1073742050,
				Keys.LeftAlt
			},
			{
				1073742054,
				Keys.RightAlt
			},
			{
				1073742048,
				Keys.LeftControl
			},
			{
				1073742052,
				Keys.RightControl
			},
			{
				1073742051,
				Keys.LeftWindows
			},
			{
				1073742055,
				Keys.RightWindows
			},
			{
				1073742049,
				Keys.LeftShift
			},
			{
				1073742053,
				Keys.RightShift
			},
			{
				1073741925,
				Keys.Apps
			},
			{
				1073741942,
				Keys.Apps
			},
			{
				47,
				Keys.OemQuestion
			},
			{
				92,
				Keys.OemPipe
			},
			{
				91,
				Keys.OemOpenBrackets
			},
			{
				93,
				Keys.OemCloseBrackets
			},
			{
				1073741881,
				Keys.CapsLock
			},
			{
				44,
				Keys.OemComma
			},
			{
				127,
				Keys.Delete
			},
			{
				1073741901,
				Keys.End
			},
			{
				8,
				Keys.Back
			},
			{
				13,
				Keys.Enter
			},
			{
				27,
				Keys.Escape
			},
			{
				1073741898,
				Keys.Home
			},
			{
				1073741897,
				Keys.Insert
			},
			{
				45,
				Keys.OemMinus
			},
			{
				1073741907,
				Keys.NumLock
			},
			{
				1073741899,
				Keys.PageUp
			},
			{
				1073741902,
				Keys.PageDown
			},
			{
				1073741896,
				Keys.Pause
			},
			{
				46,
				Keys.OemPeriod
			},
			{
				61,
				Keys.OemPlus
			},
			{
				1073741894,
				Keys.PrintScreen
			},
			{
				39,
				Keys.OemQuotes
			},
			{
				1073741895,
				Keys.Scroll
			},
			{
				59,
				Keys.OemSemicolon
			},
			{
				1073742082,
				Keys.Sleep
			},
			{
				9,
				Keys.Tab
			},
			{
				96,
				Keys.OemTilde
			},
			{
				1073741952,
				Keys.VolumeUp
			},
			{
				1073741953,
				Keys.VolumeDown
			},
			{
				178,
				Keys.OemTilde
			},
			{
				233,
				Keys.None
			},
			{
				124,
				Keys.OemPipe
			},
			{
				43,
				Keys.OemPlus
			},
			{
				248,
				Keys.OemSemicolon
			},
			{
				230,
				Keys.OemQuotes
			},
			{
				0,
				Keys.None
			}
		};

		// Token: 0x0400052B RID: 1323
		private static Dictionary<int, Keys> INTERNAL_scanMap = new Dictionary<int, Keys>
		{
			{
				4,
				Keys.A
			},
			{
				5,
				Keys.B
			},
			{
				6,
				Keys.C
			},
			{
				7,
				Keys.D
			},
			{
				8,
				Keys.E
			},
			{
				9,
				Keys.F
			},
			{
				10,
				Keys.G
			},
			{
				11,
				Keys.H
			},
			{
				12,
				Keys.I
			},
			{
				13,
				Keys.J
			},
			{
				14,
				Keys.K
			},
			{
				15,
				Keys.L
			},
			{
				16,
				Keys.M
			},
			{
				17,
				Keys.N
			},
			{
				18,
				Keys.O
			},
			{
				19,
				Keys.P
			},
			{
				20,
				Keys.Q
			},
			{
				21,
				Keys.R
			},
			{
				22,
				Keys.S
			},
			{
				23,
				Keys.T
			},
			{
				24,
				Keys.U
			},
			{
				25,
				Keys.V
			},
			{
				26,
				Keys.W
			},
			{
				27,
				Keys.X
			},
			{
				28,
				Keys.Y
			},
			{
				29,
				Keys.Z
			},
			{
				39,
				Keys.D0
			},
			{
				30,
				Keys.D1
			},
			{
				31,
				Keys.D2
			},
			{
				32,
				Keys.D3
			},
			{
				33,
				Keys.D4
			},
			{
				34,
				Keys.D5
			},
			{
				35,
				Keys.D6
			},
			{
				36,
				Keys.D7
			},
			{
				37,
				Keys.D8
			},
			{
				38,
				Keys.D9
			},
			{
				98,
				Keys.NumPad0
			},
			{
				89,
				Keys.NumPad1
			},
			{
				90,
				Keys.NumPad2
			},
			{
				91,
				Keys.NumPad3
			},
			{
				92,
				Keys.NumPad4
			},
			{
				93,
				Keys.NumPad5
			},
			{
				94,
				Keys.NumPad6
			},
			{
				95,
				Keys.NumPad7
			},
			{
				96,
				Keys.NumPad8
			},
			{
				97,
				Keys.NumPad9
			},
			{
				216,
				Keys.OemClear
			},
			{
				220,
				Keys.Decimal
			},
			{
				84,
				Keys.Divide
			},
			{
				88,
				Keys.Enter
			},
			{
				86,
				Keys.Subtract
			},
			{
				85,
				Keys.Multiply
			},
			{
				99,
				Keys.OemPeriod
			},
			{
				87,
				Keys.Add
			},
			{
				58,
				Keys.F1
			},
			{
				59,
				Keys.F2
			},
			{
				60,
				Keys.F3
			},
			{
				61,
				Keys.F4
			},
			{
				62,
				Keys.F5
			},
			{
				63,
				Keys.F6
			},
			{
				64,
				Keys.F7
			},
			{
				65,
				Keys.F8
			},
			{
				66,
				Keys.F9
			},
			{
				67,
				Keys.F10
			},
			{
				68,
				Keys.F11
			},
			{
				69,
				Keys.F12
			},
			{
				104,
				Keys.F13
			},
			{
				105,
				Keys.F14
			},
			{
				106,
				Keys.F15
			},
			{
				107,
				Keys.F16
			},
			{
				108,
				Keys.F17
			},
			{
				109,
				Keys.F18
			},
			{
				110,
				Keys.F19
			},
			{
				111,
				Keys.F20
			},
			{
				112,
				Keys.F21
			},
			{
				113,
				Keys.F22
			},
			{
				114,
				Keys.F23
			},
			{
				115,
				Keys.F24
			},
			{
				44,
				Keys.Space
			},
			{
				82,
				Keys.Up
			},
			{
				81,
				Keys.Down
			},
			{
				80,
				Keys.Left
			},
			{
				79,
				Keys.Right
			},
			{
				226,
				Keys.LeftAlt
			},
			{
				230,
				Keys.RightAlt
			},
			{
				224,
				Keys.LeftControl
			},
			{
				228,
				Keys.RightControl
			},
			{
				227,
				Keys.LeftWindows
			},
			{
				231,
				Keys.RightWindows
			},
			{
				225,
				Keys.LeftShift
			},
			{
				229,
				Keys.RightShift
			},
			{
				101,
				Keys.Apps
			},
			{
				118,
				Keys.Apps
			},
			{
				56,
				Keys.OemQuestion
			},
			{
				49,
				Keys.OemPipe
			},
			{
				47,
				Keys.OemOpenBrackets
			},
			{
				48,
				Keys.OemCloseBrackets
			},
			{
				57,
				Keys.CapsLock
			},
			{
				54,
				Keys.OemComma
			},
			{
				76,
				Keys.Delete
			},
			{
				77,
				Keys.End
			},
			{
				42,
				Keys.Back
			},
			{
				40,
				Keys.Enter
			},
			{
				41,
				Keys.Escape
			},
			{
				74,
				Keys.Home
			},
			{
				73,
				Keys.Insert
			},
			{
				45,
				Keys.OemMinus
			},
			{
				83,
				Keys.NumLock
			},
			{
				75,
				Keys.PageUp
			},
			{
				78,
				Keys.PageDown
			},
			{
				72,
				Keys.Pause
			},
			{
				55,
				Keys.OemPeriod
			},
			{
				46,
				Keys.OemPlus
			},
			{
				70,
				Keys.PrintScreen
			},
			{
				52,
				Keys.OemQuotes
			},
			{
				71,
				Keys.Scroll
			},
			{
				51,
				Keys.OemSemicolon
			},
			{
				258,
				Keys.Sleep
			},
			{
				43,
				Keys.Tab
			},
			{
				53,
				Keys.OemTilde
			},
			{
				128,
				Keys.VolumeUp
			},
			{
				129,
				Keys.VolumeDown
			},
			{
				0,
				Keys.None
			},
			{
				50,
				Keys.None
			},
			{
				100,
				Keys.None
			}
		};

		// Token: 0x0400052C RID: 1324
		private static Dictionary<int, SDL.SDL_Scancode> INTERNAL_xnaMap = new Dictionary<int, SDL.SDL_Scancode>
		{
			{
				65,
				SDL.SDL_Scancode.SDL_SCANCODE_A
			},
			{
				66,
				SDL.SDL_Scancode.SDL_SCANCODE_B
			},
			{
				67,
				SDL.SDL_Scancode.SDL_SCANCODE_C
			},
			{
				68,
				SDL.SDL_Scancode.SDL_SCANCODE_D
			},
			{
				69,
				SDL.SDL_Scancode.SDL_SCANCODE_E
			},
			{
				70,
				SDL.SDL_Scancode.SDL_SCANCODE_F
			},
			{
				71,
				SDL.SDL_Scancode.SDL_SCANCODE_G
			},
			{
				72,
				SDL.SDL_Scancode.SDL_SCANCODE_H
			},
			{
				73,
				SDL.SDL_Scancode.SDL_SCANCODE_I
			},
			{
				74,
				SDL.SDL_Scancode.SDL_SCANCODE_J
			},
			{
				75,
				SDL.SDL_Scancode.SDL_SCANCODE_K
			},
			{
				76,
				SDL.SDL_Scancode.SDL_SCANCODE_L
			},
			{
				77,
				SDL.SDL_Scancode.SDL_SCANCODE_M
			},
			{
				78,
				SDL.SDL_Scancode.SDL_SCANCODE_N
			},
			{
				79,
				SDL.SDL_Scancode.SDL_SCANCODE_O
			},
			{
				80,
				SDL.SDL_Scancode.SDL_SCANCODE_P
			},
			{
				81,
				SDL.SDL_Scancode.SDL_SCANCODE_Q
			},
			{
				82,
				SDL.SDL_Scancode.SDL_SCANCODE_R
			},
			{
				83,
				SDL.SDL_Scancode.SDL_SCANCODE_S
			},
			{
				84,
				SDL.SDL_Scancode.SDL_SCANCODE_T
			},
			{
				85,
				SDL.SDL_Scancode.SDL_SCANCODE_U
			},
			{
				86,
				SDL.SDL_Scancode.SDL_SCANCODE_V
			},
			{
				87,
				SDL.SDL_Scancode.SDL_SCANCODE_W
			},
			{
				88,
				SDL.SDL_Scancode.SDL_SCANCODE_X
			},
			{
				89,
				SDL.SDL_Scancode.SDL_SCANCODE_Y
			},
			{
				90,
				SDL.SDL_Scancode.SDL_SCANCODE_Z
			},
			{
				48,
				SDL.SDL_Scancode.SDL_SCANCODE_0
			},
			{
				49,
				SDL.SDL_Scancode.SDL_SCANCODE_1
			},
			{
				50,
				SDL.SDL_Scancode.SDL_SCANCODE_2
			},
			{
				51,
				SDL.SDL_Scancode.SDL_SCANCODE_3
			},
			{
				52,
				SDL.SDL_Scancode.SDL_SCANCODE_4
			},
			{
				53,
				SDL.SDL_Scancode.SDL_SCANCODE_5
			},
			{
				54,
				SDL.SDL_Scancode.SDL_SCANCODE_6
			},
			{
				55,
				SDL.SDL_Scancode.SDL_SCANCODE_7
			},
			{
				56,
				SDL.SDL_Scancode.SDL_SCANCODE_8
			},
			{
				57,
				SDL.SDL_Scancode.SDL_SCANCODE_9
			},
			{
				96,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_0
			},
			{
				97,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_1
			},
			{
				98,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_2
			},
			{
				99,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_3
			},
			{
				100,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_4
			},
			{
				101,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_5
			},
			{
				102,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_6
			},
			{
				103,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_7
			},
			{
				104,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_8
			},
			{
				105,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_9
			},
			{
				254,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_CLEAR
			},
			{
				110,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_DECIMAL
			},
			{
				111,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_DIVIDE
			},
			{
				106,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_MULTIPLY
			},
			{
				109,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_MINUS
			},
			{
				107,
				SDL.SDL_Scancode.SDL_SCANCODE_KP_PLUS
			},
			{
				112,
				SDL.SDL_Scancode.SDL_SCANCODE_F1
			},
			{
				113,
				SDL.SDL_Scancode.SDL_SCANCODE_F2
			},
			{
				114,
				SDL.SDL_Scancode.SDL_SCANCODE_F3
			},
			{
				115,
				SDL.SDL_Scancode.SDL_SCANCODE_F4
			},
			{
				116,
				SDL.SDL_Scancode.SDL_SCANCODE_F5
			},
			{
				117,
				SDL.SDL_Scancode.SDL_SCANCODE_F6
			},
			{
				118,
				SDL.SDL_Scancode.SDL_SCANCODE_F7
			},
			{
				119,
				SDL.SDL_Scancode.SDL_SCANCODE_F8
			},
			{
				120,
				SDL.SDL_Scancode.SDL_SCANCODE_F9
			},
			{
				121,
				SDL.SDL_Scancode.SDL_SCANCODE_F10
			},
			{
				122,
				SDL.SDL_Scancode.SDL_SCANCODE_F11
			},
			{
				123,
				SDL.SDL_Scancode.SDL_SCANCODE_F12
			},
			{
				124,
				SDL.SDL_Scancode.SDL_SCANCODE_F13
			},
			{
				125,
				SDL.SDL_Scancode.SDL_SCANCODE_F14
			},
			{
				126,
				SDL.SDL_Scancode.SDL_SCANCODE_F15
			},
			{
				127,
				SDL.SDL_Scancode.SDL_SCANCODE_F16
			},
			{
				128,
				SDL.SDL_Scancode.SDL_SCANCODE_F17
			},
			{
				129,
				SDL.SDL_Scancode.SDL_SCANCODE_F18
			},
			{
				130,
				SDL.SDL_Scancode.SDL_SCANCODE_F19
			},
			{
				131,
				SDL.SDL_Scancode.SDL_SCANCODE_F20
			},
			{
				132,
				SDL.SDL_Scancode.SDL_SCANCODE_F21
			},
			{
				133,
				SDL.SDL_Scancode.SDL_SCANCODE_F22
			},
			{
				134,
				SDL.SDL_Scancode.SDL_SCANCODE_F23
			},
			{
				135,
				SDL.SDL_Scancode.SDL_SCANCODE_F24
			},
			{
				32,
				SDL.SDL_Scancode.SDL_SCANCODE_SPACE
			},
			{
				38,
				SDL.SDL_Scancode.SDL_SCANCODE_UP
			},
			{
				40,
				SDL.SDL_Scancode.SDL_SCANCODE_DOWN
			},
			{
				37,
				SDL.SDL_Scancode.SDL_SCANCODE_LEFT
			},
			{
				39,
				SDL.SDL_Scancode.SDL_SCANCODE_RIGHT
			},
			{
				164,
				SDL.SDL_Scancode.SDL_SCANCODE_LALT
			},
			{
				165,
				SDL.SDL_Scancode.SDL_SCANCODE_RALT
			},
			{
				162,
				SDL.SDL_Scancode.SDL_SCANCODE_LCTRL
			},
			{
				163,
				SDL.SDL_Scancode.SDL_SCANCODE_RCTRL
			},
			{
				91,
				SDL.SDL_Scancode.SDL_SCANCODE_LGUI
			},
			{
				92,
				SDL.SDL_Scancode.SDL_SCANCODE_RGUI
			},
			{
				160,
				SDL.SDL_Scancode.SDL_SCANCODE_LSHIFT
			},
			{
				161,
				SDL.SDL_Scancode.SDL_SCANCODE_RSHIFT
			},
			{
				93,
				SDL.SDL_Scancode.SDL_SCANCODE_APPLICATION
			},
			{
				191,
				SDL.SDL_Scancode.SDL_SCANCODE_SLASH
			},
			{
				220,
				SDL.SDL_Scancode.SDL_SCANCODE_BACKSLASH
			},
			{
				219,
				SDL.SDL_Scancode.SDL_SCANCODE_LEFTBRACKET
			},
			{
				221,
				SDL.SDL_Scancode.SDL_SCANCODE_RIGHTBRACKET
			},
			{
				20,
				SDL.SDL_Scancode.SDL_SCANCODE_CAPSLOCK
			},
			{
				188,
				SDL.SDL_Scancode.SDL_SCANCODE_COMMA
			},
			{
				46,
				SDL.SDL_Scancode.SDL_SCANCODE_DELETE
			},
			{
				35,
				SDL.SDL_Scancode.SDL_SCANCODE_END
			},
			{
				8,
				SDL.SDL_Scancode.SDL_SCANCODE_BACKSPACE
			},
			{
				13,
				SDL.SDL_Scancode.SDL_SCANCODE_RETURN
			},
			{
				27,
				SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE
			},
			{
				36,
				SDL.SDL_Scancode.SDL_SCANCODE_HOME
			},
			{
				45,
				SDL.SDL_Scancode.SDL_SCANCODE_INSERT
			},
			{
				189,
				SDL.SDL_Scancode.SDL_SCANCODE_MINUS
			},
			{
				144,
				SDL.SDL_Scancode.SDL_SCANCODE_NUMLOCKCLEAR
			},
			{
				33,
				SDL.SDL_Scancode.SDL_SCANCODE_PAGEUP
			},
			{
				34,
				SDL.SDL_Scancode.SDL_SCANCODE_PAGEDOWN
			},
			{
				19,
				SDL.SDL_Scancode.SDL_SCANCODE_PAUSE
			},
			{
				190,
				SDL.SDL_Scancode.SDL_SCANCODE_PERIOD
			},
			{
				187,
				SDL.SDL_Scancode.SDL_SCANCODE_EQUALS
			},
			{
				44,
				SDL.SDL_Scancode.SDL_SCANCODE_PRINTSCREEN
			},
			{
				222,
				SDL.SDL_Scancode.SDL_SCANCODE_APOSTROPHE
			},
			{
				145,
				SDL.SDL_Scancode.SDL_SCANCODE_SCROLLLOCK
			},
			{
				186,
				SDL.SDL_Scancode.SDL_SCANCODE_SEMICOLON
			},
			{
				95,
				SDL.SDL_Scancode.SDL_SCANCODE_SLEEP
			},
			{
				9,
				SDL.SDL_Scancode.SDL_SCANCODE_TAB
			},
			{
				192,
				SDL.SDL_Scancode.SDL_SCANCODE_GRAVE
			},
			{
				175,
				SDL.SDL_Scancode.SDL_SCANCODE_VOLUMEUP
			},
			{
				174,
				SDL.SDL_Scancode.SDL_SCANCODE_VOLUMEDOWN
			},
			{
				0,
				SDL.SDL_Scancode.SDL_SCANCODE_UNKNOWN
			}
		};

		// Token: 0x0400052D RID: 1325
		private static SDL.SDL_EventFilter win32OnPaint = new SDL.SDL_EventFilter(SDL3_FNAPlatform.Win32OnPaint);

		// Token: 0x0400052E RID: 1326
		private static SDL.SDL_EventFilter prevEventFilter;

		// Token: 0x0200039B RID: 923
		// (Invoke) Token: 0x06001AA8 RID: 6824
		private delegate void em_callback_func();
	}
}
