using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Utilities;
using ObjCRuntime;
using SDL2;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001A RID: 26
	internal static class SDL2_FNAPlatform
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x0000AA68 File Offset: 0x00008C68
		public unsafe static string ProgramInit(LaunchParameters args)
		{
			try
			{
				SDL2_FNAPlatform.OSVersion = SDL.SDL_GetPlatform();
			}
			catch (DllNotFoundException)
			{
				FNALoggerEXT.LogError("SDL2 was not found! Do you have fnalibs?");
				throw;
			}
			catch (BadImageFormatException ex)
			{
				string text = string.Format("This process is {0}-bit, the DLL is {1}-bit!", (IntPtr.Size == 4) ? "32" : "64", (IntPtr.Size == 4) ? "64" : "32");
				FNALoggerEXT.LogError(text);
				throw new BadImageFormatException(text, ex);
			}
			SDL.SDL_SetMainReady();
			if (SDL2_FNAPlatform.OSVersion.Equals("Windows") && Debugger.IsAttached)
			{
				SDL.SDL_SetHint("SDL_WINDOWS_DISABLE_THREAD_NAMING", "1");
			}
			string baseDirectory = SDL2_FNAPlatform.GetBaseDirectory();
			string text2 = Path.Combine(baseDirectory, "gamecontrollerdb.txt");
			if (File.Exists(text2))
			{
				SDL.SDL_SetHint("SDL_GAMECONTROLLERCONFIG_FILE", text2);
			}
			string text3 = ((Environment.GetEnvironmentVariable("FNA_GAMEPAD_IGNORE_PHYSICAL_LAYOUT") == "1") ? "1" : "0");
			SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_USE_BUTTON_LABELS", text3, SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			if (Environment.GetEnvironmentVariable("FNA_NUKE_STEAM_INPUT") == "1")
			{
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_IGNORE_DEVICES", "0x28DE/0x11FF", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT", "", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_GAMECONTROLLER_ALLOW_STEAM_VIRTUAL_GAMEPAD", "0", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			string text4;
			if (args.TryGetValue("glprofile", out text4))
			{
				if (text4 == "es3")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_ES3", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
				else if (text4 == "core")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_CORE_PROFILE", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
				else if (text4 == "compatibility")
				{
					SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_COMPATIBILITY_PROFILE", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				}
			}
			if (args.TryGetValue("angle", out text4) && text4 == "1")
			{
				SDL.SDL_SetHintWithPriority("FNA3D_OPENGL_FORCE_ES3", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
				SDL.SDL_SetHintWithPriority("SDL_OPENGL_ES_DRIVER", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (args.TryGetValue("forcemailboxvsync", out text4) && text4 == "1")
			{
				SDL.SDL_SetHintWithPriority("FNA3D_VULKAN_FORCE_MAILBOX_VSYNC", "1", SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (args.TryGetValue("audiodriver", out text4))
			{
				SDL.SDL_SetHintWithPriority("SDL_AUDIODRIVER", text4, SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
			}
			if (SDL.SDL_Init(8224U) != 0)
			{
				throw new Exception("SDL_Init failed: " + SDL.SDL_GetError());
			}
			string text5 = SDL.SDL_GetCurrentVideoDriver();
			SDL2_FNAPlatform.SupportsGlobalMouse = SDL2_FNAPlatform.OSVersion.Equals("Windows") || SDL2_FNAPlatform.OSVersion.Equals("Mac OS X") || text5.Equals("x11");
			if (Environment.GetEnvironmentVariable("FNA_MOUSE_DISABLE_GLOBAL_ACCESS") == "1")
			{
				SDL2_FNAPlatform.SupportsGlobalMouse = false;
			}
			SDL2_FNAPlatform.SupportsOrientations = SDL2_FNAPlatform.OSVersion.Equals("iOS") || SDL2_FNAPlatform.OSVersion.Equals("Android");
			if (!text5.Equals("wayland") && !text5.Equals("cocoa") && !text5.Equals("uikit"))
			{
				SDL.SDL_SetHintWithPriority("SDL_VIDEO_HIGHDPI_DISABLED", "1", SDL.SDL_HintPriority.SDL_HINT_NORMAL);
			}
			if (SDL2_FNAPlatform.OSVersion.Equals("Windows"))
			{
				SDL.SDL_SetHint("SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS", "1");
			}
			if (string.IsNullOrEmpty(SDL.SDL_GetHint("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS")))
			{
				SDL.SDL_SetHint("SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS", "1");
			}
			SDL.SDL_SetHint("SDL_IOS_ORIENTATIONS", "LandscapeLeft LandscapeRight Portrait");
			SDL.SDL_Event[] array = new SDL.SDL_Event[1];
			SDL.SDL_PumpEvents();
			while (SDL.SDL_PeepEvents(array, 1, SDL.SDL_eventaction.SDL_GETEVENT, SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED, SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED) == 1)
			{
				SDL2_FNAPlatform.INTERNAL_AddInstance(array[0].cdevice.which);
			}
			if (SDL2_FNAPlatform.OSVersion.Equals("Windows") && SDL.SDL_GetHint("FNA_WIN32_IGNORE_WM_PAINT") != "1")
			{
				IntPtr intPtr;
				SDL.SDL_GetEventFilter(out SDL2_FNAPlatform.prevEventFilter, out intPtr);
				SDL.SDL_SetEventFilter(SDL2_FNAPlatform.win32OnPaint, intPtr);
			}
			if (SDL.SDL_GetHint("SteamTesla") == "1")
			{
				IntPtr intPtr2 = SDL.SDL_LoadBMP("tesla.bmp");
				if (intPtr2 != IntPtr.Zero)
				{
					SDL.SDL_Surface* ptr = (SDL.SDL_Surface*)(void*)intPtr2;
					int w = ptr->w;
					int h = ptr->h;
					IntPtr intPtr3 = SDL.SDL_CreateWindow(null, 0, 0, w, h, (SDL.SDL_WindowFlags)0U);
					if (intPtr3 != IntPtr.Zero)
					{
						ulong num = SDL.SDL_GetTicks64() + 2000UL;
						do
						{
							IntPtr intPtr4 = SDL.SDL_GetWindowSurface(intPtr3);
							SDL.SDL_BlitSurface(intPtr2, IntPtr.Zero, intPtr4, IntPtr.Zero);
							SDL.SDL_UpdateWindowSurface(intPtr3);
						}
						while (SDL.SDL_GetTicks64() - num <= 0UL);
						SDL.SDL_DestroyWindow(intPtr3);
					}
					SDL.SDL_FreeSurface(intPtr2);
				}
			}
			return baseDirectory;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000AF24 File Offset: 0x00009124
		public static void ProgramExit(object sender, EventArgs e)
		{
			SDL.SDL_QuitSubSystem(8224U);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0000AF30 File Offset: 0x00009130
		public static IntPtr Malloc(int size)
		{
			return SDL.SDL_malloc((IntPtr)size);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0000AF3D File Offset: 0x0000913D
		public static void SetEnv(string name, string value)
		{
			SDL.SDL_SetHintWithPriority(name, value, SDL.SDL_HintPriority.SDL_HINT_OVERRIDE);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000AF48 File Offset: 0x00009148
		public static GameWindow CreateWindow()
		{
			SDL.SDL_WindowFlags sdl_WindowFlags = (SDL.SDL_WindowFlags)(1544U | FNA3D.FNA3D_PrepareWindowAttributes());
			if ((sdl_WindowFlags & SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN) == SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN && SDL.SDL_GetHint("FNA3D_VULKAN_PIPELINE_CACHE_FILE_NAME") == null)
			{
				string text2;
				if (SDL2_FNAPlatform.OSVersion.Equals("Windows") || SDL2_FNAPlatform.OSVersion.Equals("Mac OS X") || SDL2_FNAPlatform.OSVersion.Equals("Linux") || SDL2_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL2_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL2_FNAPlatform.OSVersion.Equals("NetBSD"))
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
				sdl_WindowFlags |= SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI;
			}
			string defaultWindowTitle = AssemblyHelper.GetDefaultWindowTitle();
			IntPtr intPtr = SDL.SDL_CreateWindow(defaultWindowTitle, 805240832, 805240832, GraphicsDeviceManager.DefaultBackBufferWidth, GraphicsDeviceManager.DefaultBackBufferHeight, sdl_WindowFlags);
			if (intPtr == IntPtr.Zero)
			{
				throw new NoSuitableGraphicsDeviceException(SDL.SDL_GetError());
			}
			SDL2_FNAPlatform.INTERNAL_SetIcon(intPtr, defaultWindowTitle);
			SDL.SDL_DisableScreenSaver();
			SDL2_FNAPlatform.OnIsMouseVisibleChanged(false);
			sdl_WindowFlags = (SDL.SDL_WindowFlags)SDL.SDL_GetWindowFlags(intPtr);
			if ((sdl_WindowFlags & SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI) == (SDL.SDL_WindowFlags)0U)
			{
				Environment.SetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI", "0");
			}
			return new FNAWindow(intPtr, "\\\\.\\DISPLAY" + (SDL.SDL_GetWindowDisplayIndex(intPtr) + 1).ToString());
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000B0E4 File Offset: 0x000092E4
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

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000B164 File Offset: 0x00009364
		public static void ApplyWindowChanges(IntPtr window, int clientWidth, int clientHeight, bool wantsFullscreen, string screenDeviceName, ref string resultDeviceName)
		{
			bool flag = false;
			SDL2_FNAPlatform.ScaleForWindow(window, false, ref clientWidth, ref clientHeight);
			if (!wantsFullscreen)
			{
				bool flag2;
				if ((SDL.SDL_GetWindowFlags(window) & 1U) != 0U)
				{
					SDL.SDL_SetWindowFullscreen(window, 0U);
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
				SDL.SDL_SetWindowFullscreen(window, 0U);
				resultDeviceName = screenDeviceName;
				flag = true;
			}
			if (flag)
			{
				int num4 = SDL.SDL_WINDOWPOS_CENTERED_DISPLAY(num3);
				SDL.SDL_SetWindowPosition(window, num4, num4);
			}
			if (wantsFullscreen)
			{
				if ((SDL.SDL_GetWindowFlags(window) & 4U) == 0U)
				{
					SDL.SDL_DisplayMode sdl_DisplayMode;
					SDL.SDL_GetCurrentDisplayMode(num3, out sdl_DisplayMode);
					SDL.SDL_SetWindowSize(window, sdl_DisplayMode.w, sdl_DisplayMode.h);
				}
				SDL.SDL_SetWindowFullscreen(window, 4097U);
			}
			if (Mouse.WindowHandle == window)
			{
				Rectangle windowBounds = SDL2_FNAPlatform.GetWindowBounds(window);
				Mouse.INTERNAL_WindowWidth = windowBounds.Width;
				Mouse.INTERNAL_WindowHeight = windowBounds.Height;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000B290 File Offset: 0x00009490
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

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000B2FC File Offset: 0x000094FC
		public static Rectangle GetWindowBounds(IntPtr window)
		{
			Rectangle rectangle;
			if ((SDL.SDL_GetWindowFlags(window) & 1U) != 0U)
			{
				SDL.SDL_DisplayMode sdl_DisplayMode;
				SDL.SDL_GetCurrentDisplayMode(SDL.SDL_GetWindowDisplayIndex(window), out sdl_DisplayMode);
				rectangle.X = 0;
				rectangle.Y = 0;
				rectangle.Width = sdl_DisplayMode.w;
				rectangle.Height = sdl_DisplayMode.h;
			}
			else
			{
				SDL.SDL_GetWindowPosition(window, out rectangle.X, out rectangle.Y);
				SDL.SDL_GetWindowSize(window, out rectangle.Width, out rectangle.Height);
			}
			return rectangle;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000B376 File Offset: 0x00009576
		public static bool GetWindowResizable(IntPtr window)
		{
			return (SDL.SDL_GetWindowFlags(window) & 32U) > 0U;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000B384 File Offset: 0x00009584
		public static void SetWindowResizable(IntPtr window, bool resizable)
		{
			SDL.SDL_SetWindowResizable(window, resizable ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000B393 File Offset: 0x00009593
		public static bool GetWindowBorderless(IntPtr window)
		{
			return (SDL.SDL_GetWindowFlags(window) & 16U) > 0U;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000B3A1 File Offset: 0x000095A1
		public static void SetWindowBorderless(IntPtr window, bool borderless)
		{
			SDL.SDL_SetWindowBordered(window, borderless ? SDL.SDL_bool.SDL_FALSE : SDL.SDL_bool.SDL_TRUE);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public static void SetWindowTitle(IntPtr window, string title)
		{
			SDL.SDL_SetWindowTitle(window, title);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0000B3B9 File Offset: 0x000095B9
		public static bool IsScreenKeyboardShown(IntPtr window)
		{
			return SDL.SDL_IsScreenKeyboardShown(window) == SDL.SDL_bool.SDL_TRUE;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000B3C4 File Offset: 0x000095C4
		private static void INTERNAL_SetIcon(IntPtr window, string title)
		{
			string text = string.Empty;
			try
			{
				text = SDL2_FNAPlatform.INTERNAL_GetIconName(title + ".png");
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
						intPtr2 = SDL.SDL_CreateRGBSurfaceFrom(intPtr, num, num2, 32, num * 4, 255U, 65280U, 16711680U, 4278190080U);
					}
					SDL.SDL_SetWindowIcon(window, intPtr2);
					SDL.SDL_FreeSurface(intPtr2);
					FNA3D.FNA3D_Image_Free(intPtr);
					return;
				}
			}
			catch (DllNotFoundException)
			{
			}
			text = SDL2_FNAPlatform.INTERNAL_GetIconName(title + ".bmp");
			if (!string.IsNullOrEmpty(text))
			{
				IntPtr intPtr3 = SDL.SDL_LoadBMP(text);
				SDL.SDL_SetWindowIcon(window, intPtr3);
				SDL.SDL_FreeSurface(intPtr3);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000B4A8 File Offset: 0x000096A8
		private static string INTERNAL_GetIconName(string title)
		{
			string text = Path.Combine(TitleLocation.Path, title);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(TitleLocation.Path, SDL2_FNAPlatform.INTERNAL_StripBadChars(title));
			if (File.Exists(text))
			{
				return text;
			}
			return string.Empty;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000B4EC File Offset: 0x000096EC
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

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000B574 File Offset: 0x00009774
		public static void SetTextInputRectangle(IntPtr window, Rectangle rectangle)
		{
			SDL.SDL_Rect sdl_Rect = default(SDL.SDL_Rect);
			sdl_Rect.x = rectangle.X;
			sdl_Rect.y = rectangle.Y;
			sdl_Rect.w = rectangle.Width;
			sdl_Rect.h = rectangle.Height;
			SDL.SDL_SetTextInputRect(ref sdl_Rect);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000B5C4 File Offset: 0x000097C4
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

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000B5F0 File Offset: 0x000097F0
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

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000B67F File Offset: 0x0000987F
		public static bool SupportsOrientationChanges()
		{
			return SDL2_FNAPlatform.SupportsOrientations;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000B688 File Offset: 0x00009888
		public static GraphicsAdapter RegisterGame(Game game)
		{
			SDL.SDL_ShowWindow(game.Window.Handle);
			SDL2_FNAPlatform.activeGames.Add(game);
			int num = SDL.SDL_GetWindowDisplayIndex(game.Window.Handle);
			return GraphicsAdapter.Adapters[num];
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000B6CC File Offset: 0x000098CC
		public static void UnregisterGame(Game game)
		{
			SDL2_FNAPlatform.activeGames.Remove(game);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000B6DC File Offset: 0x000098DC
		public unsafe static void PollEvents(Game game, ref GraphicsAdapter currentAdapter, bool[] textInputControlDown, ref bool textInputSuppress)
		{
			char* ptr = stackalloc char[(UIntPtr)64];
			SDL.SDL_Event sdl_Event;
			while (SDL.SDL_PollEvent(out sdl_Event) == 1)
			{
				if (sdl_Event.type == SDL.SDL_EventType.SDL_KEYDOWN)
				{
					Keys keys = SDL2_FNAPlatform.ToXNAKey(ref sdl_Event.key.keysym);
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
					else if (sdl_Event.key.repeat > 0)
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
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_KEYUP)
				{
					Keys keys2 = SDL2_FNAPlatform.ToXNAKey(ref sdl_Event.key.keysym);
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
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
				{
					Mouse.INTERNAL_onClicked((int)(sdl_Event.button.button - 1));
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_MOUSEWHEEL)
				{
					Mouse.INTERNAL_MouseWheel += sdl_Event.wheel.y * 120;
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_FINGERDOWN)
				{
					TouchPanel.TouchDeviceExists = true;
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerId, TouchLocationState.Pressed, sdl_Event.tfinger.x, sdl_Event.tfinger.y, 0f, 0f);
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_FINGERMOTION)
				{
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerId, TouchLocationState.Moved, sdl_Event.tfinger.x, sdl_Event.tfinger.y, sdl_Event.tfinger.dx, sdl_Event.tfinger.dy);
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_FINGERUP)
				{
					TouchPanel.INTERNAL_onTouchEvent((int)sdl_Event.tfinger.fingerId, TouchLocationState.Released, sdl_Event.tfinger.x, sdl_Event.tfinger.y, 0f, 0f);
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_WINDOWEVENT)
				{
					if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED)
					{
						game.IsActive = true;
						if (SDL.SDL_GetCurrentVideoDriver() == "x11")
						{
							SDL.SDL_SetWindowFullscreen(game.Window.Handle, game.GraphicsDevice.PresentationParameters.IsFullScreen ? 4097U : 0U);
						}
						SDL.SDL_DisableScreenSaver();
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST)
					{
						game.IsActive = false;
						if (SDL.SDL_GetCurrentVideoDriver() == "x11")
						{
							SDL.SDL_SetWindowFullscreen(game.Window.Handle, 0U);
						}
						SDL.SDL_EnableScreenSaver();
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED)
					{
						Mouse.INTERNAL_WindowWidth = sdl_Event.window.data1;
						Mouse.INTERNAL_WindowHeight = sdl_Event.window.data2;
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED)
					{
						uint num4 = SDL.SDL_GetWindowFlags(game.Window.Handle);
						if ((num4 & 32U) != 0U && (num4 & 1536U) != 0U)
						{
							((FNAWindow)game.Window).INTERNAL_ClientSizeChanged();
						}
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED)
					{
						game.RedrawWindow();
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED)
					{
						int num5 = SDL.SDL_GetWindowDisplayIndex(game.Window.Handle);
						if (num5 >= GraphicsAdapter.Adapters.Count)
						{
							GraphicsAdapter.AdaptersChanged();
						}
						if (GraphicsAdapter.Adapters[num5] != currentAdapter)
						{
							currentAdapter = GraphicsAdapter.Adapters[num5];
							game.GraphicsDevice.Reset(game.GraphicsDevice.PresentationParameters, currentAdapter);
						}
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER)
					{
						SDL.SDL_DisableScreenSaver();
					}
					else if (sdl_Event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE)
					{
						SDL.SDL_EnableScreenSaver();
					}
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_DISPLAYEVENT)
				{
					GraphicsAdapter.AdaptersChanged();
					int num6 = SDL.SDL_GetWindowDisplayIndex(game.Window.Handle);
					currentAdapter = GraphicsAdapter.Adapters[num6];
					if (sdl_Event.display.displayEvent == SDL.SDL_DisplayEventID.SDL_DISPLAYEVENT_ORIENTATION)
					{
						if (SDL2_FNAPlatform.SupportsOrientationChanges())
						{
							SDL2_FNAPlatform.INTERNAL_HandleOrientationChange(SDL2_FNAPlatform.INTERNAL_ConvertOrientation((SDL.SDL_DisplayOrientation)sdl_Event.display.data1), game.GraphicsDevice, currentAdapter, (FNAWindow)game.Window);
						}
					}
					else
					{
						game.GraphicsDevice.QuietlyUpdateAdapter(currentAdapter);
					}
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED)
				{
					SDL2_FNAPlatform.INTERNAL_AddInstance(sdl_Event.cdevice.which);
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED)
				{
					SDL2_FNAPlatform.INTERNAL_RemoveInstance(sdl_Event.cdevice.which);
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_TEXTINPUT && !textInputSuppress)
				{
					int num7 = SDL2_FNAPlatform.MeasureStringLength(&sdl_Event.text.text.FixedElementField);
					if (num7 > 0)
					{
						int chars = Encoding.UTF8.GetChars(&sdl_Event.text.text.FixedElementField, num7, ptr, num7);
						for (int i = 0; i < chars; i++)
						{
							TextInputEXT.OnTextInput(ptr[i]);
						}
					}
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_TEXTEDITING)
				{
					int num8 = SDL2_FNAPlatform.MeasureStringLength(&sdl_Event.edit.text.FixedElementField);
					if (num8 > 0)
					{
						int chars2 = Encoding.UTF8.GetChars(&sdl_Event.edit.text.FixedElementField, num8, ptr, num8);
						TextInputEXT.OnTextEditing(new string(ptr, 0, chars2), sdl_Event.edit.start, sdl_Event.edit.length);
					}
					else
					{
						TextInputEXT.OnTextEditing(null, 0, 0);
					}
				}
				else if (sdl_Event.type == SDL.SDL_EventType.SDL_QUIT)
				{
					game.RunApplication = false;
					return;
				}
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000BD90 File Offset: 0x00009F90
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

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000BDAF File Offset: 0x00009FAF
		public static bool NeedsPlatformMainLoop()
		{
			return SDL.SDL_GetPlatform().Equals("Emscripten");
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public static void RunPlatformMainLoop(Game game)
		{
			if (SDL.SDL_GetPlatform().Equals("Emscripten"))
			{
				SDL2_FNAPlatform.emscriptenGame = game;
				SDL2_FNAPlatform.emscripten_set_main_loop(new SDL2_FNAPlatform.em_callback_func(SDL2_FNAPlatform.RunEmscriptenMainLoop), 0, 1);
				return;
			}
			throw new NotSupportedException("Cannot run the main loop of an unknown platform");
		}

		// Token: 0x06000AE8 RID: 2792
		[DllImport("__Native", CallingConvention = CallingConvention.Cdecl)]
		private static extern void emscripten_set_main_loop(SDL2_FNAPlatform.em_callback_func func, int fps, int simulate_infinite_loop);

		// Token: 0x06000AE9 RID: 2793
		[DllImport("__Native", CallingConvention = CallingConvention.Cdecl)]
		private static extern void emscripten_cancel_main_loop();

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		[MonoPInvokeCallback(typeof(SDL2_FNAPlatform.em_callback_func))]
		private static void RunEmscriptenMainLoop()
		{
			SDL2_FNAPlatform.emscriptenGame.RunOneFrame();
			if (!SDL2_FNAPlatform.emscriptenGame.RunApplication)
			{
				SDL2_FNAPlatform.emscriptenGame.Exit();
				SDL2_FNAPlatform.emscripten_cancel_main_loop();
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000BE20 File Offset: 0x0000A020
		public static GraphicsAdapter[] GetGraphicsAdapters()
		{
			SDL.SDL_DisplayMode sdl_DisplayMode = default(SDL.SDL_DisplayMode);
			GraphicsAdapter[] array = new GraphicsAdapter[SDL.SDL_GetNumVideoDisplays()];
			for (int i = 0; i < array.Length; i++)
			{
				List<DisplayMode> list = new List<DisplayMode>();
				for (int j = SDL.SDL_GetNumDisplayModes(i) - 1; j >= 0; j--)
				{
					SDL.SDL_GetDisplayMode(i, j, out sdl_DisplayMode);
					bool flag = false;
					foreach (DisplayMode displayMode in list)
					{
						if (sdl_DisplayMode.w == displayMode.Width && sdl_DisplayMode.h == displayMode.Height)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						list.Add(new DisplayMode(sdl_DisplayMode.w, sdl_DisplayMode.h, SurfaceFormat.Color));
					}
				}
				array[i] = new GraphicsAdapter(new DisplayModeCollection(list), "\\\\.\\DISPLAY" + (i + 1).ToString(), SDL.SDL_GetDisplayName(i));
			}
			return array;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000BF28 File Offset: 0x0000A128
		public static DisplayMode GetCurrentDisplayMode(int adapterIndex)
		{
			SDL.SDL_DisplayMode sdl_DisplayMode = default(SDL.SDL_DisplayMode);
			SDL.SDL_GetCurrentDisplayMode(adapterIndex, out sdl_DisplayMode);
			return new DisplayMode(sdl_DisplayMode.w, sdl_DisplayMode.h, SurfaceFormat.Color);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000BF58 File Offset: 0x0000A158
		public static IntPtr GetMonitorHandle(int adapterIndex)
		{
			return new IntPtr(adapterIndex);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000BF60 File Offset: 0x0000A160
		public static void GetMouseState(IntPtr window, out int x, out int y, out ButtonState left, out ButtonState middle, out ButtonState right, out ButtonState x1, out ButtonState x2)
		{
			uint num;
			if (SDL2_FNAPlatform.GetRelativeMouseMode(window))
			{
				num = SDL.SDL_GetRelativeMouseState(out x, out y);
			}
			else if (SDL2_FNAPlatform.SupportsGlobalMouse)
			{
				num = SDL.SDL_GetGlobalMouseState(out x, out y);
				int num2 = 0;
				int num3 = 0;
				SDL.SDL_GetWindowPosition(window, out num2, out num3);
				x -= num2;
				y -= num3;
			}
			else
			{
				num = SDL.SDL_GetMouseState(out x, out y);
			}
			left = (ButtonState)(num & SDL.SDL_BUTTON_LMASK);
			middle = (ButtonState)((num & SDL.SDL_BUTTON_MMASK) >> 1);
			right = (ButtonState)((num & SDL.SDL_BUTTON_RMASK) >> 2);
			x1 = (ButtonState)((num & SDL.SDL_BUTTON_X1MASK) >> 3);
			x2 = (ButtonState)((num & SDL.SDL_BUTTON_X2MASK) >> 4);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000BFEB File Offset: 0x0000A1EB
		public static void OnIsMouseVisibleChanged(bool visible)
		{
			SDL.SDL_ShowCursor((visible > false) ? 1 : 0);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000BFF7 File Offset: 0x0000A1F7
		public static bool GetRelativeMouseMode(IntPtr window)
		{
			return SDL.SDL_GetRelativeMouseMode() == SDL.SDL_bool.SDL_TRUE;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000C004 File Offset: 0x0000A204
		public static void SetRelativeMouseMode(IntPtr window, bool enable)
		{
			SDL.SDL_SetRelativeMouseMode(enable ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE);
			if (enable)
			{
				int num;
				SDL.SDL_GetRelativeMouseState(out num, out num);
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000C02C File Offset: 0x0000A22C
		private static string GetBaseDirectory()
		{
			if (Environment.GetEnvironmentVariable("FNA_SDL_FORCE_BASE_PATH") != "1" && (SDL2_FNAPlatform.OSVersion.Equals("Windows") || SDL2_FNAPlatform.OSVersion.Equals("Mac OS X") || SDL2_FNAPlatform.OSVersion.Equals("Linux") || SDL2_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL2_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL2_FNAPlatform.OSVersion.Equals("NetBSD")))
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

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public static string GetStorageRoot()
		{
			string text = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName).Replace(".vshost", "");
			if (SDL2_FNAPlatform.OSVersion.Equals("Windows"))
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedGames", text);
			}
			if (SDL2_FNAPlatform.OSVersion.Equals("Mac OS X"))
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
				if (SDL2_FNAPlatform.OSVersion.Equals("Linux") || SDL2_FNAPlatform.OSVersion.Equals("FreeBSD") || SDL2_FNAPlatform.OSVersion.Equals("OpenBSD") || SDL2_FNAPlatform.OSVersion.Equals("NetBSD"))
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

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		public static DriveInfo GetDriveInfo(string storageRoot)
		{
			DriveInfo driveInfo;
			try
			{
				driveInfo = new DriveInfo(SDL2_FNAPlatform.MonoPathRootWorkaround(storageRoot));
			}
			catch (Exception ex)
			{
				FNALoggerEXT.LogError("Failed to get DriveInfo: " + ex.ToString());
				driveInfo = null;
			}
			return driveInfo;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000C248 File Offset: 0x0000A448
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

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000C322 File Offset: 0x0000A522
		public static IntPtr ReadToPointer(string path, out IntPtr size)
		{
			return SDL.SDL_LoadFile(path, out size);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000C32B File Offset: 0x0000A52B
		public static void FreeFilePointer(IntPtr file)
		{
			SDL.SDL_free(file);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000C333 File Offset: 0x0000A533
		public static void ShowRuntimeError(string title, string message)
		{
			SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, title ?? "", message ?? "", IntPtr.Zero);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000C358 File Offset: 0x0000A558
		public static Microphone[] GetMicrophones()
		{
			if (!SDL2_FNAPlatform.micInit)
			{
				SDL.SDL_InitSubSystem(16U);
				SDL2_FNAPlatform.micInit = true;
			}
			int num = SDL.SDL_GetNumAudioDevices(1);
			if (num < 1)
			{
				return new Microphone[0];
			}
			Microphone[] array = new Microphone[num + 1];
			SDL.SDL_AudioSpec sdl_AudioSpec = default(SDL.SDL_AudioSpec);
			sdl_AudioSpec.freq = 44100;
			sdl_AudioSpec.format = 32784;
			sdl_AudioSpec.channels = 1;
			sdl_AudioSpec.samples = 4096;
			SDL.SDL_AudioSpec sdl_AudioSpec2;
			array[0] = new Microphone(SDL.SDL_OpenAudioDevice(null, 1, ref sdl_AudioSpec, out sdl_AudioSpec2, 0), "Default Device");
			for (int i = 0; i < num; i++)
			{
				string text = SDL.SDL_GetAudioDeviceName(i, 1);
				array[i + 1] = new Microphone(SDL.SDL_OpenAudioDevice(text, 1, ref sdl_AudioSpec, out sdl_AudioSpec2, 0), text);
			}
			return array;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000C418 File Offset: 0x0000A618
		public unsafe static int GetMicrophoneSamples(uint handle, byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				return (int)SDL.SDL_DequeueAudio(handle, (IntPtr)((void*)ptr2), (uint)count);
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000C43D File Offset: 0x0000A63D
		public static int GetMicrophoneQueuedBytes(uint handle)
		{
			return (int)SDL.SDL_GetQueuedAudioSize(handle);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000C445 File Offset: 0x0000A645
		public static void StartMicrophone(uint handle)
		{
			SDL.SDL_PauseAudioDevice(handle, 0);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000C44E File Offset: 0x0000A64E
		public static void StopMicrophone(uint handle)
		{
			SDL.SDL_PauseAudioDevice(handle, 1);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000C458 File Offset: 0x0000A658
		public static GamePadCapabilities GetGamePadCapabilities(int index)
		{
			if (SDL2_FNAPlatform.INTERNAL_devices[index] == IntPtr.Zero)
			{
				return default(GamePadCapabilities);
			}
			return SDL2_FNAPlatform.INTERNAL_capabilities[index];
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000C490 File Offset: 0x0000A690
		public static GamePadState GetGamePadState(int index, GamePadDeadZone deadZoneMode)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				return default(GamePadState);
			}
			Vector2 vector = new Vector2((float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX) / 32767f, (float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY) / -32767f);
			Vector2 vector2 = new Vector2((float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX) / 32767f, (float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY) / -32767f);
			float num = (float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT) / 32767f;
			float num2 = (float)SDL.SDL_GameControllerGetAxis(intPtr, SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT) / 32767f;
			Buttons buttons = (Buttons)0;
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A) != 0)
			{
				buttons |= Buttons.A;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B) != 0)
			{
				buttons |= Buttons.B;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X) != 0)
			{
				buttons |= Buttons.X;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y) != 0)
			{
				buttons |= Buttons.Y;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK) != 0)
			{
				buttons |= Buttons.Back;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE) != 0)
			{
				buttons |= Buttons.BigButton;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START) != 0)
			{
				buttons |= Buttons.Start;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK) != 0)
			{
				buttons |= Buttons.LeftStick;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK) != 0)
			{
				buttons |= Buttons.RightStick;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER) != 0)
			{
				buttons |= Buttons.LeftShoulder;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER) != 0)
			{
				buttons |= Buttons.RightShoulder;
			}
			ButtonState buttonState = ButtonState.Released;
			ButtonState buttonState2 = ButtonState.Released;
			ButtonState buttonState3 = ButtonState.Released;
			ButtonState buttonState4 = ButtonState.Released;
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP) != 0)
			{
				buttons |= Buttons.DPadUp;
				buttonState = ButtonState.Pressed;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN) != 0)
			{
				buttons |= Buttons.DPadDown;
				buttonState2 = ButtonState.Pressed;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT) != 0)
			{
				buttons |= Buttons.DPadLeft;
				buttonState3 = ButtonState.Pressed;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT) != 0)
			{
				buttons |= Buttons.DPadRight;
				buttonState4 = ButtonState.Pressed;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MISC1) != 0)
			{
				buttons |= Buttons.Misc1EXT;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE1) != 0)
			{
				buttons |= Buttons.Paddle1EXT;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE2) != 0)
			{
				buttons |= Buttons.Paddle2EXT;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE3) != 0)
			{
				buttons |= Buttons.Paddle3EXT;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE4) != 0)
			{
				buttons |= Buttons.Paddle4EXT;
			}
			if (SDL.SDL_GameControllerGetButton(intPtr, SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_TOUCHPAD) != 0)
			{
				buttons |= Buttons.TouchPadEXT;
			}
			GamePadState gamePadState = new GamePadState(new GamePadThumbSticks(vector, vector2, deadZoneMode), new GamePadTriggers(num, num2, deadZoneMode), new GamePadButtons(buttons), new GamePadDPad(buttonState, buttonState2, buttonState3, buttonState4))
			{
				IsConnected = true,
				PacketNumber = SDL2_FNAPlatform.INTERNAL_states[index].PacketNumber
			};
			if (gamePadState != SDL2_FNAPlatform.INTERNAL_states[index])
			{
				gamePadState.PacketNumber++;
				SDL2_FNAPlatform.INTERNAL_states[index] = gamePadState;
			}
			return gamePadState;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000C73C File Offset: 0x0000A93C
		public static bool SetGamePadVibration(int index, float leftMotor, float rightMotor)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			return !(intPtr == IntPtr.Zero) && SDL.SDL_GameControllerRumble(intPtr, (ushort)(MathHelper.Clamp(leftMotor, 0f, 1f) * 65535f), (ushort)(MathHelper.Clamp(rightMotor, 0f, 1f) * 65535f), 0U) == 0;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000C798 File Offset: 0x0000A998
		public static bool SetGamePadTriggerVibration(int index, float leftTrigger, float rightTrigger)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			return !(intPtr == IntPtr.Zero) && SDL.SDL_GameControllerRumbleTriggers(intPtr, (ushort)(MathHelper.Clamp(leftTrigger, 0f, 1f) * 65535f), (ushort)(MathHelper.Clamp(rightTrigger, 0f, 1f) * 65535f), 0U) == 0;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public static string GetGamePadGUID(int index)
		{
			return SDL2_FNAPlatform.INTERNAL_guids[index];
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0000C800 File Offset: 0x0000AA00
		public static void SetGamePadLightBar(int index, Color color)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			SDL.SDL_GameControllerSetLED(intPtr, color.R, color.G, color.B);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0000C840 File Offset: 0x0000AA40
		public unsafe static bool GetGamePadGyro(int index, out Vector3 gyro)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				gyro = Vector3.Zero;
				return false;
			}
			if (SDL.SDL_GameControllerIsSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO) == SDL.SDL_bool.SDL_FALSE)
			{
				SDL.SDL_GameControllerSetSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO, SDL.SDL_bool.SDL_TRUE);
			}
			float* ptr = stackalloc float[(UIntPtr)12];
			if (SDL.SDL_GameControllerGetSensorData(intPtr, SDL.SDL_SensorType.SDL_SENSOR_GYRO, (IntPtr)((void*)ptr), 3) < 0)
			{
				gyro = Vector3.Zero;
				return false;
			}
			gyro.X = *ptr;
			gyro.Y = ptr[1];
			gyro.Z = ptr[2];
			return true;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public unsafe static bool GetGamePadAccelerometer(int index, out Vector3 accel)
		{
			IntPtr intPtr = SDL2_FNAPlatform.INTERNAL_devices[index];
			if (intPtr == IntPtr.Zero)
			{
				accel = Vector3.Zero;
				return false;
			}
			if (SDL.SDL_GameControllerIsSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL) == SDL.SDL_bool.SDL_FALSE)
			{
				SDL.SDL_GameControllerSetSensorEnabled(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL, SDL.SDL_bool.SDL_TRUE);
			}
			float* ptr = stackalloc float[(UIntPtr)12];
			if (SDL.SDL_GameControllerGetSensorData(intPtr, SDL.SDL_SensorType.SDL_SENSOR_ACCEL, (IntPtr)((void*)ptr), 3) < 0)
			{
				accel = Vector3.Zero;
				return false;
			}
			accel.X = *ptr;
			accel.Y = ptr[1];
			accel.Z = ptr[2];
			return true;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000C950 File Offset: 0x0000AB50
		private static void INTERNAL_AddInstance(int dev)
		{
			int num = -1;
			for (int i = 0; i < SDL2_FNAPlatform.INTERNAL_devices.Length; i++)
			{
				if (SDL2_FNAPlatform.INTERNAL_devices[i] == IntPtr.Zero)
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
			SDL2_FNAPlatform.INTERNAL_devices[num] = SDL.SDL_GameControllerOpen(dev);
			IntPtr intPtr = SDL.SDL_GameControllerGetJoystick(SDL2_FNAPlatform.INTERNAL_devices[num]);
			int num2 = SDL.SDL_JoystickInstanceID(intPtr);
			if (SDL2_FNAPlatform.INTERNAL_instanceList.ContainsKey(num2))
			{
				SDL2_FNAPlatform.INTERNAL_devices[num] = IntPtr.Zero;
				return;
			}
			SDL2_FNAPlatform.INTERNAL_instanceList.Add(num2, num);
			SDL2_FNAPlatform.INTERNAL_states[num] = default(GamePadState);
			SDL2_FNAPlatform.INTERNAL_states[num].IsConnected = true;
			bool flag = SDL.SDL_GameControllerRumble(SDL2_FNAPlatform.INTERNAL_devices[num], 0, 0, 0U) == 0;
			bool flag2 = SDL.SDL_GameControllerRumbleTriggers(SDL2_FNAPlatform.INTERNAL_devices[num], 0, 0, 0U) == 0;
			GamePadCapabilities gamePadCapabilities = default(GamePadCapabilities);
			gamePadCapabilities.IsConnected = true;
			gamePadCapabilities.GamePadType = SDL2_FNAPlatform.INTERNAL_gamepadType[(int)SDL.SDL_JoystickGetType(intPtr)];
			gamePadCapabilities.HasAButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasBButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasXButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasYButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasBackButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasBigButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasStartButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftStickButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasRightStickButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftShoulderButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasRightShoulderButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasDPadUpButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasDPadDownButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasDPadLeftButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasDPadRightButton = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftXThumbStick = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftYThumbStick = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasRightXThumbStick = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasRightYThumbStick = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftTrigger = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasRightTrigger = SDL.SDL_GameControllerGetBindForAxis(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasLeftVibrationMotor = flag;
			gamePadCapabilities.HasRightVibrationMotor = flag;
			gamePadCapabilities.HasVoiceSupport = false;
			gamePadCapabilities.HasLightBarEXT = SDL.SDL_GameControllerHasLED(SDL2_FNAPlatform.INTERNAL_devices[num]) == SDL.SDL_bool.SDL_TRUE;
			gamePadCapabilities.HasTriggerVibrationMotorsEXT = flag2;
			gamePadCapabilities.HasMisc1EXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MISC1).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasPaddle1EXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE1).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasPaddle2EXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE2).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasPaddle3EXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE3).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasPaddle4EXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_PADDLE4).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasTouchPadEXT = SDL.SDL_GameControllerGetBindForButton(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_TOUCHPAD).bindType > SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE;
			gamePadCapabilities.HasGyroEXT = SDL.SDL_GameControllerHasSensor(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_SensorType.SDL_SENSOR_GYRO) == SDL.SDL_bool.SDL_TRUE;
			gamePadCapabilities.HasAccelerometerEXT = SDL.SDL_GameControllerHasSensor(SDL2_FNAPlatform.INTERNAL_devices[num], SDL.SDL_SensorType.SDL_SENSOR_ACCEL) == SDL.SDL_bool.SDL_TRUE;
			SDL2_FNAPlatform.INTERNAL_capabilities[num] = gamePadCapabilities;
			ushort num3 = SDL.SDL_JoystickGetVendor(intPtr);
			ushort num4 = SDL.SDL_JoystickGetProduct(intPtr);
			if (num3 == 0 && num4 == 0)
			{
				SDL2_FNAPlatform.INTERNAL_guids[num] = "xinput";
			}
			else
			{
				SDL2_FNAPlatform.INTERNAL_guids[num] = string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", new object[]
				{
					(int)(num3 & 255),
					num3 >> 8,
					(int)(num4 & 255),
					num4 >> 8
				});
			}
			if (num3 == 10462)
			{
				SDL.SDL_GameControllerType sdl_GameControllerType = SDL.SDL_GameControllerGetType(SDL2_FNAPlatform.INTERNAL_devices[num]);
				if (sdl_GameControllerType == SDL.SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOX360 || sdl_GameControllerType == SDL.SDL_GameControllerType.SDL_CONTROLLER_TYPE_XBOXONE)
				{
					SDL2_FNAPlatform.INTERNAL_guids[num] = "xinput";
				}
				else if (sdl_GameControllerType == SDL.SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS4)
				{
					SDL2_FNAPlatform.INTERNAL_guids[num] = "4c05c405";
				}
				else if (sdl_GameControllerType == SDL.SDL_GameControllerType.SDL_CONTROLLER_TYPE_PS5)
				{
					SDL2_FNAPlatform.INTERNAL_guids[num] = "4c05e60c";
				}
			}
			string text = SDL.SDL_GameControllerMapping(SDL2_FNAPlatform.INTERNAL_devices[num]);
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
				SDL.SDL_GameControllerName(SDL2_FNAPlatform.INTERNAL_devices[num]),
				", GUID: ",
				SDL2_FNAPlatform.INTERNAL_guids[num],
				", ",
				text2
			}));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000CF10 File Offset: 0x0000B110
		private static void INTERNAL_RemoveInstance(int dev)
		{
			int num;
			if (!SDL2_FNAPlatform.INTERNAL_instanceList.TryGetValue(dev, out num))
			{
				return;
			}
			SDL2_FNAPlatform.INTERNAL_instanceList.Remove(dev);
			SDL.SDL_GameControllerClose(SDL2_FNAPlatform.INTERNAL_devices[num]);
			SDL2_FNAPlatform.INTERNAL_devices[num] = IntPtr.Zero;
			SDL2_FNAPlatform.INTERNAL_states[num] = default(GamePadState);
			SDL2_FNAPlatform.INTERNAL_guids[num] = string.Empty;
			SDL.SDL_ClearError();
			FNALoggerEXT.LogInfo("Removed device, player: " + num.ToString());
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000CF90 File Offset: 0x0000B190
		private static string[] GenStringArray()
		{
			string[] array = new string[GamePad.GAMEPAD_COUNT];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Empty;
			}
			return array;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000CFBF File Offset: 0x0000B1BF
		public static TouchPanelCapabilities GetTouchCapabilities()
		{
			bool flag = SDL.SDL_GetNumTouchDevices() > 0;
			return new TouchPanelCapabilities(flag, flag ? 4 : 0);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public unsafe static void UpdateTouchPanelState()
		{
			long num = SDL.SDL_GetTouchDevice(0);
			for (int i = 0; i < 8; i++)
			{
				SDL.SDL_Finger* ptr = (SDL.SDL_Finger*)(void*)SDL.SDL_GetTouchFinger(num, i);
				if (ptr == null)
				{
					TouchPanel.SetFinger(i, -1, Vector2.Zero);
				}
				else
				{
					TouchPanel.SetFinger(i, (int)ptr->id, new Vector2((float)Math.Round((double)(ptr->x * (float)TouchPanel.DisplayWidth)), (float)Math.Round((double)(ptr->y * (float)TouchPanel.DisplayHeight))));
				}
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000D052 File Offset: 0x0000B252
		public static int GetNumTouchFingers()
		{
			return SDL.SDL_GetNumTouchFingers(SDL.SDL_GetTouchDevice(0));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0000D05F File Offset: 0x0000B25F
		public static bool IsTextInputActive(IntPtr window)
		{
			return SDL.SDL_IsTextInputActive() > SDL.SDL_bool.SDL_FALSE;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0000D069 File Offset: 0x0000B269
		public static void StartTextInput(IntPtr window)
		{
			SDL.SDL_StartTextInput();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0000D070 File Offset: 0x0000B270
		public static void StopTextInput(IntPtr window)
		{
			SDL.SDL_StopTextInput();
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0000D078 File Offset: 0x0000B278
		private static Keys ToXNAKey(ref SDL.SDL_Keysym key)
		{
			Keys keys;
			if (SDL2_FNAPlatform.UseScancodes)
			{
				if (SDL2_FNAPlatform.INTERNAL_scanMap.TryGetValue((int)key.scancode, out keys))
				{
					return keys;
				}
			}
			else if (SDL2_FNAPlatform.INTERNAL_keyMap.TryGetValue((int)key.sym, out keys))
			{
				return keys;
			}
			FNALoggerEXT.LogWarn("KEY/SCANCODE MISSING FROM SDL2->XNA DICTIONARY: " + key.sym.ToString() + " " + key.scancode.ToString());
			return Keys.None;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public static Keys GetKeyFromScancode(Keys scancode)
		{
			if (SDL2_FNAPlatform.UseScancodes)
			{
				return scancode;
			}
			SDL.SDL_Scancode sdl_Scancode;
			if (SDL2_FNAPlatform.INTERNAL_xnaMap.TryGetValue((int)scancode, out sdl_Scancode))
			{
				SDL.SDL_Keycode sdl_Keycode = SDL.SDL_GetKeyFromScancode(sdl_Scancode);
				Keys keys;
				if (SDL2_FNAPlatform.INTERNAL_keyMap.TryGetValue((int)sdl_Keycode, out keys))
				{
					return keys;
				}
				FNALoggerEXT.LogWarn("KEYCODE MISSING FROM SDL2->XNA DICTIONARY: " + sdl_Keycode.ToString());
			}
			else
			{
				FNALoggerEXT.LogWarn("SCANCODE MISSING FROM XNA->SDL2 DICTIONARY: " + scancode.ToString());
			}
			return Keys.None;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0000D178 File Offset: 0x0000B378
		private unsafe static int Win32OnPaint(IntPtr userdata, IntPtr evtPtr)
		{
			SDL.SDL_Event* ptr = (SDL.SDL_Event*)(void*)evtPtr;
			if (ptr->type == SDL.SDL_EventType.SDL_WINDOWEVENT && ptr->window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED)
			{
				foreach (Game game in SDL2_FNAPlatform.activeGames)
				{
					if (game.Window != null && ptr->window.windowID == SDL.SDL_GetWindowID(game.Window.Handle))
					{
						game.RedrawWindow();
						return 0;
					}
				}
			}
			if (SDL2_FNAPlatform.prevEventFilter != null)
			{
				return SDL2_FNAPlatform.prevEventFilter(userdata, evtPtr);
			}
			return 1;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0000D22C File Offset: 0x0000B42C
		// Note: this type is marked as 'beforefieldinit'.
		static SDL2_FNAPlatform()
		{
		}

		// Token: 0x04000509 RID: 1289
		private static string OSVersion;

		// Token: 0x0400050A RID: 1290
		private static readonly bool UseScancodes = Environment.GetEnvironmentVariable("FNA_KEYBOARD_USE_SCANCODES") == "1";

		// Token: 0x0400050B RID: 1291
		private static bool SupportsGlobalMouse;

		// Token: 0x0400050C RID: 1292
		private static bool SupportsOrientations;

		// Token: 0x0400050D RID: 1293
		private static List<Game> activeGames = new List<Game>();

		// Token: 0x0400050E RID: 1294
		private static Game emscriptenGame;

		// Token: 0x0400050F RID: 1295
		private static bool micInit = false;

		// Token: 0x04000510 RID: 1296
		private static IntPtr[] INTERNAL_devices = new IntPtr[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000511 RID: 1297
		private static Dictionary<int, int> INTERNAL_instanceList = new Dictionary<int, int>();

		// Token: 0x04000512 RID: 1298
		private static string[] INTERNAL_guids = SDL2_FNAPlatform.GenStringArray();

		// Token: 0x04000513 RID: 1299
		private static GamePadState[] INTERNAL_states = new GamePadState[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000514 RID: 1300
		private static GamePadCapabilities[] INTERNAL_capabilities = new GamePadCapabilities[GamePad.GAMEPAD_COUNT];

		// Token: 0x04000515 RID: 1301
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

		// Token: 0x04000516 RID: 1302
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
				1073742106,
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

		// Token: 0x04000517 RID: 1303
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
				282,
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

		// Token: 0x04000518 RID: 1304
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

		// Token: 0x04000519 RID: 1305
		private static SDL.SDL_EventFilter win32OnPaint = new SDL.SDL_EventFilter(SDL2_FNAPlatform.Win32OnPaint);

		// Token: 0x0400051A RID: 1306
		private static SDL.SDL_EventFilter prevEventFilter;

		// Token: 0x0200039A RID: 922
		// (Invoke) Token: 0x06001AA4 RID: 6820
		private delegate void em_callback_func();
	}
}
