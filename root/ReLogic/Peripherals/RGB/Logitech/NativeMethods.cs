using System;
using System.Runtime.InteropServices;

namespace ReLogic.Peripherals.RGB.Logitech
{
	// Token: 0x02000042 RID: 66
	internal class NativeMethods
	{
		// Token: 0x0600019D RID: 413
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedInit();

		// Token: 0x0600019E RID: 414
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedGetConfigOptionNumber([MarshalAs(21)] string configPath, ref double defaultNumber);

		// Token: 0x0600019F RID: 415
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedGetConfigOptionBool([MarshalAs(21)] string configPath, ref bool defaultRed);

		// Token: 0x060001A0 RID: 416
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedGetConfigOptionColor([MarshalAs(21)] string configPath, ref int defaultRed, ref int defaultGreen, ref int defaultBlue);

		// Token: 0x060001A1 RID: 417
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetTargetDevice(int targetDevice);

		// Token: 0x060001A2 RID: 418
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedGetSdkVersion(ref int majorNum, ref int minorNum, ref int buildNum);

		// Token: 0x060001A3 RID: 419
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSaveCurrentLighting();

		// Token: 0x060001A4 RID: 420
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLighting(int redPercentage, int greenPercentage, int bluePercentage);

		// Token: 0x060001A5 RID: 421
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedRestoreLighting();

		// Token: 0x060001A6 RID: 422
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedFlashLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

		// Token: 0x060001A7 RID: 423
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedPulseLighting(int redPercentage, int greenPercentage, int bluePercentage, int milliSecondsDuration, int milliSecondsInterval);

		// Token: 0x060001A8 RID: 424
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedStopEffects();

		// Token: 0x060001A9 RID: 425
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedExcludeKeysFromBitmap(KeyName[] keyList, int listCount);

		// Token: 0x060001AA RID: 426
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLightingFromBitmap(byte[] bitmap);

		// Token: 0x060001AB RID: 427
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLightingForKeyWithScanCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

		// Token: 0x060001AC RID: 428
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLightingForKeyWithHidCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

		// Token: 0x060001AD RID: 429
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLightingForKeyWithQuartzCode(int keyCode, int redPercentage, int greenPercentage, int bluePercentage);

		// Token: 0x060001AE RID: 430
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSetLightingForKeyWithKeyName(KeyName keyCode, int redPercentage, int greenPercentage, int bluePercentage);

		// Token: 0x060001AF RID: 431
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedSaveLightingForKey(KeyName keyName);

		// Token: 0x060001B0 RID: 432
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedRestoreLightingForKey(KeyName keyName);

		// Token: 0x060001B1 RID: 433
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedFlashSingleKey(KeyName keyName, int redPercentage, int greenPercentage, int bluePercentage, int msDuration, int msInterval);

		// Token: 0x060001B2 RID: 434
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedPulseSingleKey(KeyName keyName, int startRedPercentage, int startGreenPercentage, int startBluePercentage, int finishRedPercentage, int finishGreenPercentage, int finishBluePercentage, int msDuration, bool isInfinite);

		// Token: 0x060001B3 RID: 435
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern bool LogiLedStopEffectsOnKey(KeyName keyName);

		// Token: 0x060001B4 RID: 436
		[DllImport("LogitechLedEnginesWrapper ", CallingConvention = 2)]
		public static extern void LogiLedShutdown();

		// Token: 0x060001B5 RID: 437 RVA: 0x0000448A File Offset: 0x0000268A
		public NativeMethods()
		{
		}

		// Token: 0x040001A9 RID: 425
		private const int LOGI_DEVICETYPE_MONOCHROME_ORD = 0;

		// Token: 0x040001AA RID: 426
		private const int LOGI_DEVICETYPE_RGB_ORD = 1;

		// Token: 0x040001AB RID: 427
		private const int LOGI_DEVICETYPE_PERKEY_RGB_ORD = 2;

		// Token: 0x040001AC RID: 428
		public const int LOGI_DEVICETYPE_MONOCHROME = 1;

		// Token: 0x040001AD RID: 429
		public const int LOGI_DEVICETYPE_RGB = 2;

		// Token: 0x040001AE RID: 430
		public const int LOGI_DEVICETYPE_PERKEY_RGB = 4;

		// Token: 0x040001AF RID: 431
		public const int LOGI_LED_BITMAP_WIDTH = 21;

		// Token: 0x040001B0 RID: 432
		public const int LOGI_LED_BITMAP_HEIGHT = 6;

		// Token: 0x040001B1 RID: 433
		public const int LOGI_LED_BITMAP_BYTES_PER_KEY = 4;

		// Token: 0x040001B2 RID: 434
		public const int LOGI_LED_BITMAP_SIZE = 504;

		// Token: 0x040001B3 RID: 435
		public const int LOGI_LED_DURATION_INFINITE = 0;
	}
}
