using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SDL2;
using SDL3;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000018 RID: 24
	internal static class FNAPlatform
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00009FD8 File Offset: 0x000081D8
		static FNAPlatform()
		{
			bool flag = Environment.GetEnvironmentVariable("FNA_PLATFORM_BACKEND") == "SDL2";
			if (!flag)
			{
				FNAPlatform.SetEnv = new FNAPlatform.SetEnvFunc(SDL3_FNAPlatform.SetEnv);
			}
			else
			{
				FNAPlatform.SetEnv = new FNAPlatform.SetEnvFunc(SDL2_FNAPlatform.SetEnv);
			}
			LaunchParameters launchParameters = new LaunchParameters();
			string text;
			if (launchParameters.TryGetValue("enablehighdpi", out text) && text == "1")
			{
				Environment.SetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI", "1");
			}
			if (launchParameters.TryGetValue("gldevice", out text))
			{
				FNAPlatform.SetEnv("FNA3D_FORCE_DRIVER", text);
			}
			if (launchParameters.TryGetValue("enablelateswaptear", out text) && text == "1")
			{
				FNAPlatform.SetEnv("FNA3D_ENABLE_LATESWAPTEAR", "1");
			}
			if (launchParameters.TryGetValue("mojoshaderprofile", out text))
			{
				FNAPlatform.SetEnv("FNA3D_MOJOSHADER_PROFILE", text);
			}
			if (launchParameters.TryGetValue("backbufferscalenearest", out text) && text == "1")
			{
				FNAPlatform.SetEnv("FNA3D_BACKBUFFER_SCALE_NEAREST", "1");
			}
			if (launchParameters.TryGetValue("usescancodes", out text) && text == "1")
			{
				Environment.SetEnvironmentVariable("FNA_KEYBOARD_USE_SCANCODES", "1");
			}
			if (launchParameters.TryGetValue("disableglobalmouse", out text) && text == "1")
			{
				Environment.SetEnvironmentVariable("FNA_MOUSE_DISABLE_GLOBAL_ACCESS", "1");
			}
			if (launchParameters.TryGetValue("nukesteaminput", out text) && text == "1")
			{
				Environment.SetEnvironmentVariable("FNA_NUKE_STEAM_INPUT", "1");
			}
			Environment.SetEnvironmentVariable("FNA_SDL_FORCE_BASE_PATH", Environment.GetEnvironmentVariable("FNA_SDL2_FORCE_BASE_PATH"));
			if (!flag)
			{
				FNAPlatform.Malloc = new FNAPlatform.MallocFunc(SDL3_FNAPlatform.Malloc);
				FNAPlatform.Free = new FNAPlatform.FreeFunc(global::SDL3.SDL.SDL_free);
				FNAPlatform.CreateWindow = new FNAPlatform.CreateWindowFunc(SDL3_FNAPlatform.CreateWindow);
				FNAPlatform.DisposeWindow = new FNAPlatform.DisposeWindowFunc(SDL3_FNAPlatform.DisposeWindow);
				FNAPlatform.ApplyWindowChanges = new FNAPlatform.ApplyWindowChangesFunc(SDL3_FNAPlatform.ApplyWindowChanges);
				FNAPlatform.ScaleForWindow = new FNAPlatform.ScaleForWindowFunc(SDL3_FNAPlatform.ScaleForWindow);
				FNAPlatform.GetWindowBounds = new FNAPlatform.GetWindowBoundsFunc(SDL3_FNAPlatform.GetWindowBounds);
				FNAPlatform.GetWindowResizable = new FNAPlatform.GetWindowResizableFunc(SDL3_FNAPlatform.GetWindowResizable);
				FNAPlatform.SetWindowResizable = new FNAPlatform.SetWindowResizableFunc(SDL3_FNAPlatform.SetWindowResizable);
				FNAPlatform.GetWindowBorderless = new FNAPlatform.GetWindowBorderlessFunc(SDL3_FNAPlatform.GetWindowBorderless);
				FNAPlatform.SetWindowBorderless = new FNAPlatform.SetWindowBorderlessFunc(SDL3_FNAPlatform.SetWindowBorderless);
				FNAPlatform.SetWindowTitle = new FNAPlatform.SetWindowTitleFunc(SDL3_FNAPlatform.SetWindowTitle);
				FNAPlatform.IsScreenKeyboardShown = new FNAPlatform.IsScreenKeyboardShownFunc(SDL3_FNAPlatform.IsScreenKeyboardShown);
				FNAPlatform.RegisterGame = new FNAPlatform.RegisterGameFunc(SDL3_FNAPlatform.RegisterGame);
				FNAPlatform.UnregisterGame = new FNAPlatform.UnregisterGameFunc(SDL3_FNAPlatform.UnregisterGame);
				FNAPlatform.PollEvents = new FNAPlatform.PollEventsFunc(SDL3_FNAPlatform.PollEvents);
				FNAPlatform.GetGraphicsAdapters = new FNAPlatform.GetGraphicsAdaptersFunc(SDL3_FNAPlatform.GetGraphicsAdapters);
				FNAPlatform.GetCurrentDisplayMode = new FNAPlatform.GetCurrentDisplayModeFunc(SDL3_FNAPlatform.GetCurrentDisplayMode);
				FNAPlatform.GetMonitorHandle = new FNAPlatform.GetMonitorHandleFunc(SDL3_FNAPlatform.GetMonitorHandle);
				FNAPlatform.GetKeyFromScancode = new FNAPlatform.GetKeyFromScancodeFunc(SDL3_FNAPlatform.GetKeyFromScancode);
				FNAPlatform.IsTextInputActive = new FNAPlatform.IsTextInputActiveFunc(SDL3_FNAPlatform.IsTextInputActive);
				FNAPlatform.StartTextInput = new FNAPlatform.StartTextInputFunc(SDL3_FNAPlatform.StartTextInput);
				FNAPlatform.StopTextInput = new FNAPlatform.StopTextInputFunc(SDL3_FNAPlatform.StopTextInput);
				FNAPlatform.SetTextInputRectangle = new FNAPlatform.SetTextInputRectangleFunc(SDL3_FNAPlatform.SetTextInputRectangle);
				FNAPlatform.GetMouseState = new FNAPlatform.GetMouseStateFunc(SDL3_FNAPlatform.GetMouseState);
				FNAPlatform.SetMousePosition = new FNAPlatform.SetMousePositionFunc(SDL3_FNAPlatform.WarpMouseInWindow);
				FNAPlatform.OnIsMouseVisibleChanged = new FNAPlatform.OnIsMouseVisibleChangedFunc(SDL3_FNAPlatform.OnIsMouseVisibleChanged);
				FNAPlatform.GetRelativeMouseMode = new FNAPlatform.GetRelativeMouseModeFunc(SDL3_FNAPlatform.GetRelativeMouseMode);
				FNAPlatform.SetRelativeMouseMode = new FNAPlatform.SetRelativeMouseModeFunc(SDL3_FNAPlatform.SetRelativeMouseMode);
				FNAPlatform.GetGamePadCapabilities = new FNAPlatform.GetGamePadCapabilitiesFunc(SDL3_FNAPlatform.GetGamePadCapabilities);
				FNAPlatform.GetGamePadState = new FNAPlatform.GetGamePadStateFunc(SDL3_FNAPlatform.GetGamePadState);
				FNAPlatform.SetGamePadVibration = new FNAPlatform.SetGamePadVibrationFunc(SDL3_FNAPlatform.SetGamePadVibration);
				FNAPlatform.SetGamePadTriggerVibration = new FNAPlatform.SetGamePadTriggerVibrationFunc(SDL3_FNAPlatform.SetGamePadTriggerVibration);
				FNAPlatform.GetGamePadGUID = new FNAPlatform.GetGamePadGUIDFunc(SDL3_FNAPlatform.GetGamePadGUID);
				FNAPlatform.SetGamePadLightBar = new FNAPlatform.SetGamePadLightBarFunc(SDL3_FNAPlatform.SetGamePadLightBar);
				FNAPlatform.GetGamePadGyro = new FNAPlatform.GetGamePadGyroFunc(SDL3_FNAPlatform.GetGamePadGyro);
				FNAPlatform.GetGamePadAccelerometer = new FNAPlatform.GetGamePadAccelerometerFunc(SDL3_FNAPlatform.GetGamePadAccelerometer);
				FNAPlatform.GetStorageRoot = new FNAPlatform.GetStorageRootFunc(SDL3_FNAPlatform.GetStorageRoot);
				FNAPlatform.GetDriveInfo = new FNAPlatform.GetDriveInfoFunc(SDL3_FNAPlatform.GetDriveInfo);
				FNAPlatform.ReadFileToPointer = new FNAPlatform.ReadFileToPointerFunc(SDL3_FNAPlatform.ReadToPointer);
				FNAPlatform.FreeFilePointer = new FNAPlatform.FreeFilePointerFunc(SDL3_FNAPlatform.FreeFilePointer);
				FNAPlatform.ShowRuntimeError = new FNAPlatform.ShowRuntimeErrorFunc(SDL3_FNAPlatform.ShowRuntimeError);
				FNAPlatform.GetMicrophones = new FNAPlatform.GetMicrophonesFunc(SDL3_FNAPlatform.GetMicrophones);
				FNAPlatform.GetMicrophoneSamples = new FNAPlatform.GetMicrophoneSamplesFunc(SDL3_FNAPlatform.GetMicrophoneSamples);
				FNAPlatform.GetMicrophoneQueuedBytes = new FNAPlatform.GetMicrophoneQueuedBytesFunc(SDL3_FNAPlatform.GetMicrophoneQueuedBytes);
				FNAPlatform.StartMicrophone = new FNAPlatform.StartMicrophoneFunc(SDL3_FNAPlatform.StartMicrophone);
				FNAPlatform.StopMicrophone = new FNAPlatform.StopMicrophoneFunc(SDL3_FNAPlatform.StopMicrophone);
				FNAPlatform.GetTouchCapabilities = new FNAPlatform.GetTouchCapabilitiesFunc(SDL3_FNAPlatform.GetTouchCapabilities);
				FNAPlatform.UpdateTouchPanelState = new FNAPlatform.UpdateTouchPanelStateFunc(SDL3_FNAPlatform.UpdateTouchPanelState);
				FNAPlatform.GetNumTouchFingers = new FNAPlatform.GetNumTouchFingersFunc(SDL3_FNAPlatform.GetNumTouchFingers);
				FNAPlatform.SupportsOrientationChanges = new FNAPlatform.SupportsOrientationChangesFunc(SDL3_FNAPlatform.SupportsOrientationChanges);
				FNAPlatform.NeedsPlatformMainLoop = new FNAPlatform.NeedsPlatformMainLoopFunc(SDL3_FNAPlatform.NeedsPlatformMainLoop);
				FNAPlatform.RunPlatformMainLoop = new FNAPlatform.RunPlatformMainLoopFunc(SDL3_FNAPlatform.RunPlatformMainLoop);
			}
			else
			{
				FNAPlatform.Malloc = new FNAPlatform.MallocFunc(SDL2_FNAPlatform.Malloc);
				FNAPlatform.Free = new FNAPlatform.FreeFunc(global::SDL2.SDL.SDL_free);
				FNAPlatform.CreateWindow = new FNAPlatform.CreateWindowFunc(SDL2_FNAPlatform.CreateWindow);
				FNAPlatform.DisposeWindow = new FNAPlatform.DisposeWindowFunc(SDL2_FNAPlatform.DisposeWindow);
				FNAPlatform.ApplyWindowChanges = new FNAPlatform.ApplyWindowChangesFunc(SDL2_FNAPlatform.ApplyWindowChanges);
				FNAPlatform.ScaleForWindow = new FNAPlatform.ScaleForWindowFunc(SDL2_FNAPlatform.ScaleForWindow);
				FNAPlatform.GetWindowBounds = new FNAPlatform.GetWindowBoundsFunc(SDL2_FNAPlatform.GetWindowBounds);
				FNAPlatform.GetWindowResizable = new FNAPlatform.GetWindowResizableFunc(SDL2_FNAPlatform.GetWindowResizable);
				FNAPlatform.SetWindowResizable = new FNAPlatform.SetWindowResizableFunc(SDL2_FNAPlatform.SetWindowResizable);
				FNAPlatform.GetWindowBorderless = new FNAPlatform.GetWindowBorderlessFunc(SDL2_FNAPlatform.GetWindowBorderless);
				FNAPlatform.SetWindowBorderless = new FNAPlatform.SetWindowBorderlessFunc(SDL2_FNAPlatform.SetWindowBorderless);
				FNAPlatform.SetWindowTitle = new FNAPlatform.SetWindowTitleFunc(SDL2_FNAPlatform.SetWindowTitle);
				FNAPlatform.IsScreenKeyboardShown = new FNAPlatform.IsScreenKeyboardShownFunc(SDL2_FNAPlatform.IsScreenKeyboardShown);
				FNAPlatform.RegisterGame = new FNAPlatform.RegisterGameFunc(SDL2_FNAPlatform.RegisterGame);
				FNAPlatform.UnregisterGame = new FNAPlatform.UnregisterGameFunc(SDL2_FNAPlatform.UnregisterGame);
				FNAPlatform.PollEvents = new FNAPlatform.PollEventsFunc(SDL2_FNAPlatform.PollEvents);
				FNAPlatform.GetGraphicsAdapters = new FNAPlatform.GetGraphicsAdaptersFunc(SDL2_FNAPlatform.GetGraphicsAdapters);
				FNAPlatform.GetCurrentDisplayMode = new FNAPlatform.GetCurrentDisplayModeFunc(SDL2_FNAPlatform.GetCurrentDisplayMode);
				FNAPlatform.GetMonitorHandle = new FNAPlatform.GetMonitorHandleFunc(SDL2_FNAPlatform.GetMonitorHandle);
				FNAPlatform.GetKeyFromScancode = new FNAPlatform.GetKeyFromScancodeFunc(SDL2_FNAPlatform.GetKeyFromScancode);
				FNAPlatform.IsTextInputActive = new FNAPlatform.IsTextInputActiveFunc(SDL2_FNAPlatform.IsTextInputActive);
				FNAPlatform.StartTextInput = new FNAPlatform.StartTextInputFunc(SDL2_FNAPlatform.StartTextInput);
				FNAPlatform.StopTextInput = new FNAPlatform.StopTextInputFunc(SDL2_FNAPlatform.StopTextInput);
				FNAPlatform.SetTextInputRectangle = new FNAPlatform.SetTextInputRectangleFunc(SDL2_FNAPlatform.SetTextInputRectangle);
				FNAPlatform.GetMouseState = new FNAPlatform.GetMouseStateFunc(SDL2_FNAPlatform.GetMouseState);
				FNAPlatform.SetMousePosition = new FNAPlatform.SetMousePositionFunc(global::SDL2.SDL.SDL_WarpMouseInWindow);
				FNAPlatform.OnIsMouseVisibleChanged = new FNAPlatform.OnIsMouseVisibleChangedFunc(SDL2_FNAPlatform.OnIsMouseVisibleChanged);
				FNAPlatform.GetRelativeMouseMode = new FNAPlatform.GetRelativeMouseModeFunc(SDL2_FNAPlatform.GetRelativeMouseMode);
				FNAPlatform.SetRelativeMouseMode = new FNAPlatform.SetRelativeMouseModeFunc(SDL2_FNAPlatform.SetRelativeMouseMode);
				FNAPlatform.GetGamePadCapabilities = new FNAPlatform.GetGamePadCapabilitiesFunc(SDL2_FNAPlatform.GetGamePadCapabilities);
				FNAPlatform.GetGamePadState = new FNAPlatform.GetGamePadStateFunc(SDL2_FNAPlatform.GetGamePadState);
				FNAPlatform.SetGamePadVibration = new FNAPlatform.SetGamePadVibrationFunc(SDL2_FNAPlatform.SetGamePadVibration);
				FNAPlatform.SetGamePadTriggerVibration = new FNAPlatform.SetGamePadTriggerVibrationFunc(SDL2_FNAPlatform.SetGamePadTriggerVibration);
				FNAPlatform.GetGamePadGUID = new FNAPlatform.GetGamePadGUIDFunc(SDL2_FNAPlatform.GetGamePadGUID);
				FNAPlatform.SetGamePadLightBar = new FNAPlatform.SetGamePadLightBarFunc(SDL2_FNAPlatform.SetGamePadLightBar);
				FNAPlatform.GetGamePadGyro = new FNAPlatform.GetGamePadGyroFunc(SDL2_FNAPlatform.GetGamePadGyro);
				FNAPlatform.GetGamePadAccelerometer = new FNAPlatform.GetGamePadAccelerometerFunc(SDL2_FNAPlatform.GetGamePadAccelerometer);
				FNAPlatform.GetStorageRoot = new FNAPlatform.GetStorageRootFunc(SDL2_FNAPlatform.GetStorageRoot);
				FNAPlatform.GetDriveInfo = new FNAPlatform.GetDriveInfoFunc(SDL2_FNAPlatform.GetDriveInfo);
				FNAPlatform.ReadFileToPointer = new FNAPlatform.ReadFileToPointerFunc(SDL2_FNAPlatform.ReadToPointer);
				FNAPlatform.FreeFilePointer = new FNAPlatform.FreeFilePointerFunc(SDL2_FNAPlatform.FreeFilePointer);
				FNAPlatform.ShowRuntimeError = new FNAPlatform.ShowRuntimeErrorFunc(SDL2_FNAPlatform.ShowRuntimeError);
				FNAPlatform.GetMicrophones = new FNAPlatform.GetMicrophonesFunc(SDL2_FNAPlatform.GetMicrophones);
				FNAPlatform.GetMicrophoneSamples = new FNAPlatform.GetMicrophoneSamplesFunc(SDL2_FNAPlatform.GetMicrophoneSamples);
				FNAPlatform.GetMicrophoneQueuedBytes = new FNAPlatform.GetMicrophoneQueuedBytesFunc(SDL2_FNAPlatform.GetMicrophoneQueuedBytes);
				FNAPlatform.StartMicrophone = new FNAPlatform.StartMicrophoneFunc(SDL2_FNAPlatform.StartMicrophone);
				FNAPlatform.StopMicrophone = new FNAPlatform.StopMicrophoneFunc(SDL2_FNAPlatform.StopMicrophone);
				FNAPlatform.GetTouchCapabilities = new FNAPlatform.GetTouchCapabilitiesFunc(SDL2_FNAPlatform.GetTouchCapabilities);
				FNAPlatform.UpdateTouchPanelState = new FNAPlatform.UpdateTouchPanelStateFunc(SDL2_FNAPlatform.UpdateTouchPanelState);
				FNAPlatform.GetNumTouchFingers = new FNAPlatform.GetNumTouchFingersFunc(SDL2_FNAPlatform.GetNumTouchFingers);
				FNAPlatform.SupportsOrientationChanges = new FNAPlatform.SupportsOrientationChangesFunc(SDL2_FNAPlatform.SupportsOrientationChanges);
				FNAPlatform.NeedsPlatformMainLoop = new FNAPlatform.NeedsPlatformMainLoopFunc(SDL2_FNAPlatform.NeedsPlatformMainLoop);
				FNAPlatform.RunPlatformMainLoop = new FNAPlatform.RunPlatformMainLoopFunc(SDL2_FNAPlatform.RunPlatformMainLoop);
			}
			FNALoggerEXT.Initialize();
			if (!flag)
			{
				AppDomain.CurrentDomain.ProcessExit += SDL3_FNAPlatform.ProgramExit;
				FNAPlatform.TitleLocation = SDL3_FNAPlatform.ProgramInit(launchParameters);
			}
			else
			{
				AppDomain.CurrentDomain.ProcessExit += SDL2_FNAPlatform.ProgramExit;
				FNAPlatform.TitleLocation = SDL2_FNAPlatform.ProgramInit(launchParameters);
			}
			FNALoggerEXT.HookFNA3D();
		}

		// Token: 0x040004CC RID: 1228
		public static readonly string TitleLocation;

		// Token: 0x040004CD RID: 1229
		public static readonly char[] TextInputCharacters = new char[] { '\u0002', '\u0003', '\b', '\t', '\r', '\u007f', '\u0016' };

		// Token: 0x040004CE RID: 1230
		public static readonly Dictionary<Keys, int> TextInputBindings = new Dictionary<Keys, int>
		{
			{
				Keys.Home,
				0
			},
			{
				Keys.End,
				1
			},
			{
				Keys.Back,
				2
			},
			{
				Keys.Tab,
				3
			},
			{
				Keys.Enter,
				4
			},
			{
				Keys.Delete,
				5
			}
		};

		// Token: 0x040004CF RID: 1231
		public static readonly FNAPlatform.MallocFunc Malloc;

		// Token: 0x040004D0 RID: 1232
		public static readonly FNAPlatform.FreeFunc Free;

		// Token: 0x040004D1 RID: 1233
		public static readonly FNAPlatform.SetEnvFunc SetEnv;

		// Token: 0x040004D2 RID: 1234
		public static readonly FNAPlatform.CreateWindowFunc CreateWindow;

		// Token: 0x040004D3 RID: 1235
		public static readonly FNAPlatform.DisposeWindowFunc DisposeWindow;

		// Token: 0x040004D4 RID: 1236
		public static readonly FNAPlatform.ApplyWindowChangesFunc ApplyWindowChanges;

		// Token: 0x040004D5 RID: 1237
		public static readonly FNAPlatform.ScaleForWindowFunc ScaleForWindow;

		// Token: 0x040004D6 RID: 1238
		public static readonly FNAPlatform.GetWindowBoundsFunc GetWindowBounds;

		// Token: 0x040004D7 RID: 1239
		public static readonly FNAPlatform.GetWindowResizableFunc GetWindowResizable;

		// Token: 0x040004D8 RID: 1240
		public static readonly FNAPlatform.SetWindowResizableFunc SetWindowResizable;

		// Token: 0x040004D9 RID: 1241
		public static readonly FNAPlatform.GetWindowBorderlessFunc GetWindowBorderless;

		// Token: 0x040004DA RID: 1242
		public static readonly FNAPlatform.SetWindowBorderlessFunc SetWindowBorderless;

		// Token: 0x040004DB RID: 1243
		public static readonly FNAPlatform.SetWindowTitleFunc SetWindowTitle;

		// Token: 0x040004DC RID: 1244
		public static readonly FNAPlatform.IsScreenKeyboardShownFunc IsScreenKeyboardShown;

		// Token: 0x040004DD RID: 1245
		public static readonly FNAPlatform.RegisterGameFunc RegisterGame;

		// Token: 0x040004DE RID: 1246
		public static readonly FNAPlatform.UnregisterGameFunc UnregisterGame;

		// Token: 0x040004DF RID: 1247
		public static readonly FNAPlatform.PollEventsFunc PollEvents;

		// Token: 0x040004E0 RID: 1248
		public static readonly FNAPlatform.GetGraphicsAdaptersFunc GetGraphicsAdapters;

		// Token: 0x040004E1 RID: 1249
		public static readonly FNAPlatform.GetCurrentDisplayModeFunc GetCurrentDisplayMode;

		// Token: 0x040004E2 RID: 1250
		public static readonly FNAPlatform.GetMonitorHandleFunc GetMonitorHandle;

		// Token: 0x040004E3 RID: 1251
		public static readonly FNAPlatform.GetKeyFromScancodeFunc GetKeyFromScancode;

		// Token: 0x040004E4 RID: 1252
		public static readonly FNAPlatform.IsTextInputActiveFunc IsTextInputActive;

		// Token: 0x040004E5 RID: 1253
		public static readonly FNAPlatform.StartTextInputFunc StartTextInput;

		// Token: 0x040004E6 RID: 1254
		public static readonly FNAPlatform.StopTextInputFunc StopTextInput;

		// Token: 0x040004E7 RID: 1255
		public static readonly FNAPlatform.SetTextInputRectangleFunc SetTextInputRectangle;

		// Token: 0x040004E8 RID: 1256
		public static readonly FNAPlatform.GetMouseStateFunc GetMouseState;

		// Token: 0x040004E9 RID: 1257
		public static readonly FNAPlatform.SetMousePositionFunc SetMousePosition;

		// Token: 0x040004EA RID: 1258
		public static readonly FNAPlatform.OnIsMouseVisibleChangedFunc OnIsMouseVisibleChanged;

		// Token: 0x040004EB RID: 1259
		public static readonly FNAPlatform.GetRelativeMouseModeFunc GetRelativeMouseMode;

		// Token: 0x040004EC RID: 1260
		public static readonly FNAPlatform.SetRelativeMouseModeFunc SetRelativeMouseMode;

		// Token: 0x040004ED RID: 1261
		public static readonly FNAPlatform.GetGamePadCapabilitiesFunc GetGamePadCapabilities;

		// Token: 0x040004EE RID: 1262
		public static readonly FNAPlatform.GetGamePadStateFunc GetGamePadState;

		// Token: 0x040004EF RID: 1263
		public static readonly FNAPlatform.SetGamePadVibrationFunc SetGamePadVibration;

		// Token: 0x040004F0 RID: 1264
		public static readonly FNAPlatform.SetGamePadTriggerVibrationFunc SetGamePadTriggerVibration;

		// Token: 0x040004F1 RID: 1265
		public static readonly FNAPlatform.GetGamePadGUIDFunc GetGamePadGUID;

		// Token: 0x040004F2 RID: 1266
		public static readonly FNAPlatform.SetGamePadLightBarFunc SetGamePadLightBar;

		// Token: 0x040004F3 RID: 1267
		public static readonly FNAPlatform.GetGamePadGyroFunc GetGamePadGyro;

		// Token: 0x040004F4 RID: 1268
		public static readonly FNAPlatform.GetGamePadAccelerometerFunc GetGamePadAccelerometer;

		// Token: 0x040004F5 RID: 1269
		public static readonly FNAPlatform.GetStorageRootFunc GetStorageRoot;

		// Token: 0x040004F6 RID: 1270
		public static readonly FNAPlatform.GetDriveInfoFunc GetDriveInfo;

		// Token: 0x040004F7 RID: 1271
		public static readonly FNAPlatform.ReadFileToPointerFunc ReadFileToPointer;

		// Token: 0x040004F8 RID: 1272
		public static readonly FNAPlatform.FreeFilePointerFunc FreeFilePointer;

		// Token: 0x040004F9 RID: 1273
		public static readonly FNAPlatform.ShowRuntimeErrorFunc ShowRuntimeError;

		// Token: 0x040004FA RID: 1274
		public static readonly FNAPlatform.GetMicrophonesFunc GetMicrophones;

		// Token: 0x040004FB RID: 1275
		public static readonly FNAPlatform.GetMicrophoneSamplesFunc GetMicrophoneSamples;

		// Token: 0x040004FC RID: 1276
		public static readonly FNAPlatform.GetMicrophoneQueuedBytesFunc GetMicrophoneQueuedBytes;

		// Token: 0x040004FD RID: 1277
		public static readonly FNAPlatform.StartMicrophoneFunc StartMicrophone;

		// Token: 0x040004FE RID: 1278
		public static readonly FNAPlatform.StopMicrophoneFunc StopMicrophone;

		// Token: 0x040004FF RID: 1279
		public static readonly FNAPlatform.GetTouchCapabilitiesFunc GetTouchCapabilities;

		// Token: 0x04000500 RID: 1280
		public static readonly FNAPlatform.UpdateTouchPanelStateFunc UpdateTouchPanelState;

		// Token: 0x04000501 RID: 1281
		public static readonly FNAPlatform.GetNumTouchFingersFunc GetNumTouchFingers;

		// Token: 0x04000502 RID: 1282
		public static readonly FNAPlatform.SupportsOrientationChangesFunc SupportsOrientationChanges;

		// Token: 0x04000503 RID: 1283
		public static readonly FNAPlatform.NeedsPlatformMainLoopFunc NeedsPlatformMainLoop;

		// Token: 0x04000504 RID: 1284
		public static readonly FNAPlatform.RunPlatformMainLoopFunc RunPlatformMainLoop;

		// Token: 0x02000364 RID: 868
		// (Invoke) Token: 0x060019CC RID: 6604
		public delegate IntPtr MallocFunc(int size);

		// Token: 0x02000365 RID: 869
		// (Invoke) Token: 0x060019D0 RID: 6608
		public delegate void FreeFunc(IntPtr ptr);

		// Token: 0x02000366 RID: 870
		// (Invoke) Token: 0x060019D4 RID: 6612
		public delegate void SetEnvFunc(string name, string value);

		// Token: 0x02000367 RID: 871
		// (Invoke) Token: 0x060019D8 RID: 6616
		public delegate GameWindow CreateWindowFunc();

		// Token: 0x02000368 RID: 872
		// (Invoke) Token: 0x060019DC RID: 6620
		public delegate void DisposeWindowFunc(GameWindow window);

		// Token: 0x02000369 RID: 873
		// (Invoke) Token: 0x060019E0 RID: 6624
		public delegate void ApplyWindowChangesFunc(IntPtr window, int clientWidth, int clientHeight, bool wantsFullscreen, string screenDeviceName, ref string resultDeviceName);

		// Token: 0x0200036A RID: 874
		// (Invoke) Token: 0x060019E4 RID: 6628
		public delegate void ScaleForWindowFunc(IntPtr window, bool invert, ref int w, ref int h);

		// Token: 0x0200036B RID: 875
		// (Invoke) Token: 0x060019E8 RID: 6632
		public delegate Rectangle GetWindowBoundsFunc(IntPtr window);

		// Token: 0x0200036C RID: 876
		// (Invoke) Token: 0x060019EC RID: 6636
		public delegate bool GetWindowResizableFunc(IntPtr window);

		// Token: 0x0200036D RID: 877
		// (Invoke) Token: 0x060019F0 RID: 6640
		public delegate void SetWindowResizableFunc(IntPtr window, bool resizable);

		// Token: 0x0200036E RID: 878
		// (Invoke) Token: 0x060019F4 RID: 6644
		public delegate bool GetWindowBorderlessFunc(IntPtr window);

		// Token: 0x0200036F RID: 879
		// (Invoke) Token: 0x060019F8 RID: 6648
		public delegate void SetWindowBorderlessFunc(IntPtr window, bool borderless);

		// Token: 0x02000370 RID: 880
		// (Invoke) Token: 0x060019FC RID: 6652
		public delegate void SetWindowTitleFunc(IntPtr window, string title);

		// Token: 0x02000371 RID: 881
		// (Invoke) Token: 0x06001A00 RID: 6656
		public delegate bool IsScreenKeyboardShownFunc(IntPtr window);

		// Token: 0x02000372 RID: 882
		// (Invoke) Token: 0x06001A04 RID: 6660
		public delegate GraphicsAdapter RegisterGameFunc(Game game);

		// Token: 0x02000373 RID: 883
		// (Invoke) Token: 0x06001A08 RID: 6664
		public delegate void UnregisterGameFunc(Game game);

		// Token: 0x02000374 RID: 884
		// (Invoke) Token: 0x06001A0C RID: 6668
		public delegate void PollEventsFunc(Game game, ref GraphicsAdapter currentAdapter, bool[] textInputControlDown, ref bool textInputSuppress);

		// Token: 0x02000375 RID: 885
		// (Invoke) Token: 0x06001A10 RID: 6672
		public delegate GraphicsAdapter[] GetGraphicsAdaptersFunc();

		// Token: 0x02000376 RID: 886
		// (Invoke) Token: 0x06001A14 RID: 6676
		public delegate DisplayMode GetCurrentDisplayModeFunc(int adapterIndex);

		// Token: 0x02000377 RID: 887
		// (Invoke) Token: 0x06001A18 RID: 6680
		public delegate IntPtr GetMonitorHandleFunc(int adapterIndex);

		// Token: 0x02000378 RID: 888
		// (Invoke) Token: 0x06001A1C RID: 6684
		public delegate Keys GetKeyFromScancodeFunc(Keys scancode);

		// Token: 0x02000379 RID: 889
		// (Invoke) Token: 0x06001A20 RID: 6688
		public delegate bool IsTextInputActiveFunc(IntPtr window);

		// Token: 0x0200037A RID: 890
		// (Invoke) Token: 0x06001A24 RID: 6692
		public delegate void StartTextInputFunc(IntPtr window);

		// Token: 0x0200037B RID: 891
		// (Invoke) Token: 0x06001A28 RID: 6696
		public delegate void StopTextInputFunc(IntPtr window);

		// Token: 0x0200037C RID: 892
		// (Invoke) Token: 0x06001A2C RID: 6700
		public delegate void SetTextInputRectangleFunc(IntPtr window, Rectangle rectangle);

		// Token: 0x0200037D RID: 893
		// (Invoke) Token: 0x06001A30 RID: 6704
		public delegate void GetMouseStateFunc(IntPtr window, out int x, out int y, out ButtonState left, out ButtonState middle, out ButtonState right, out ButtonState x1, out ButtonState x2);

		// Token: 0x0200037E RID: 894
		// (Invoke) Token: 0x06001A34 RID: 6708
		public delegate void SetMousePositionFunc(IntPtr window, int x, int y);

		// Token: 0x0200037F RID: 895
		// (Invoke) Token: 0x06001A38 RID: 6712
		public delegate void OnIsMouseVisibleChangedFunc(bool visible);

		// Token: 0x02000380 RID: 896
		// (Invoke) Token: 0x06001A3C RID: 6716
		public delegate bool GetRelativeMouseModeFunc(IntPtr window);

		// Token: 0x02000381 RID: 897
		// (Invoke) Token: 0x06001A40 RID: 6720
		public delegate void SetRelativeMouseModeFunc(IntPtr window, bool enable);

		// Token: 0x02000382 RID: 898
		// (Invoke) Token: 0x06001A44 RID: 6724
		public delegate GamePadCapabilities GetGamePadCapabilitiesFunc(int index);

		// Token: 0x02000383 RID: 899
		// (Invoke) Token: 0x06001A48 RID: 6728
		public delegate GamePadState GetGamePadStateFunc(int index, GamePadDeadZone deadZoneMode);

		// Token: 0x02000384 RID: 900
		// (Invoke) Token: 0x06001A4C RID: 6732
		public delegate bool SetGamePadVibrationFunc(int index, float leftMotor, float rightMotor);

		// Token: 0x02000385 RID: 901
		// (Invoke) Token: 0x06001A50 RID: 6736
		public delegate bool SetGamePadTriggerVibrationFunc(int index, float leftTrigger, float rightTrigger);

		// Token: 0x02000386 RID: 902
		// (Invoke) Token: 0x06001A54 RID: 6740
		public delegate string GetGamePadGUIDFunc(int index);

		// Token: 0x02000387 RID: 903
		// (Invoke) Token: 0x06001A58 RID: 6744
		public delegate void SetGamePadLightBarFunc(int index, Color color);

		// Token: 0x02000388 RID: 904
		// (Invoke) Token: 0x06001A5C RID: 6748
		public delegate bool GetGamePadGyroFunc(int index, out Vector3 gyro);

		// Token: 0x02000389 RID: 905
		// (Invoke) Token: 0x06001A60 RID: 6752
		public delegate bool GetGamePadAccelerometerFunc(int index, out Vector3 accel);

		// Token: 0x0200038A RID: 906
		// (Invoke) Token: 0x06001A64 RID: 6756
		public delegate string GetStorageRootFunc();

		// Token: 0x0200038B RID: 907
		// (Invoke) Token: 0x06001A68 RID: 6760
		public delegate DriveInfo GetDriveInfoFunc(string storageRoot);

		// Token: 0x0200038C RID: 908
		// (Invoke) Token: 0x06001A6C RID: 6764
		public delegate IntPtr ReadFileToPointerFunc(string path, out IntPtr size);

		// Token: 0x0200038D RID: 909
		// (Invoke) Token: 0x06001A70 RID: 6768
		public delegate void FreeFilePointerFunc(IntPtr file);

		// Token: 0x0200038E RID: 910
		// (Invoke) Token: 0x06001A74 RID: 6772
		public delegate void ShowRuntimeErrorFunc(string title, string message);

		// Token: 0x0200038F RID: 911
		// (Invoke) Token: 0x06001A78 RID: 6776
		public delegate Microphone[] GetMicrophonesFunc();

		// Token: 0x02000390 RID: 912
		// (Invoke) Token: 0x06001A7C RID: 6780
		public delegate int GetMicrophoneSamplesFunc(uint handle, byte[] buffer, int offset, int count);

		// Token: 0x02000391 RID: 913
		// (Invoke) Token: 0x06001A80 RID: 6784
		public delegate int GetMicrophoneQueuedBytesFunc(uint handle);

		// Token: 0x02000392 RID: 914
		// (Invoke) Token: 0x06001A84 RID: 6788
		public delegate void StartMicrophoneFunc(uint handle);

		// Token: 0x02000393 RID: 915
		// (Invoke) Token: 0x06001A88 RID: 6792
		public delegate void StopMicrophoneFunc(uint handle);

		// Token: 0x02000394 RID: 916
		// (Invoke) Token: 0x06001A8C RID: 6796
		public delegate TouchPanelCapabilities GetTouchCapabilitiesFunc();

		// Token: 0x02000395 RID: 917
		// (Invoke) Token: 0x06001A90 RID: 6800
		public delegate void UpdateTouchPanelStateFunc();

		// Token: 0x02000396 RID: 918
		// (Invoke) Token: 0x06001A94 RID: 6804
		public delegate int GetNumTouchFingersFunc();

		// Token: 0x02000397 RID: 919
		// (Invoke) Token: 0x06001A98 RID: 6808
		public delegate bool SupportsOrientationChangesFunc();

		// Token: 0x02000398 RID: 920
		// (Invoke) Token: 0x06001A9C RID: 6812
		public delegate bool NeedsPlatformMainLoopFunc();

		// Token: 0x02000399 RID: 921
		// (Invoke) Token: 0x06001AA0 RID: 6816
		public delegate void RunPlatformMainLoopFunc(Game game);
	}
}
