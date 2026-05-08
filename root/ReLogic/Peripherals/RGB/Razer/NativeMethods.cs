using System;
using System.Runtime.InteropServices;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000033 RID: 51
	internal class NativeMethods
	{
		// Token: 0x06000173 RID: 371
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult Init();

		// Token: 0x06000174 RID: 372
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult UnInit();

		// Token: 0x06000175 RID: 373
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateKeyboardEffect(NativeMethods.KeyboardEffectType effect, ref NativeMethods.CustomKeyboardEffect effectData, ref Guid effectId);

		// Token: 0x06000176 RID: 374
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateMouseEffect(NativeMethods.MouseEffectType effect, ref NativeMethods.CustomMouseEffect effectData, ref Guid effectId);

		// Token: 0x06000177 RID: 375
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateHeadsetEffect(NativeMethods.HeadsetEffectType effect, ref NativeMethods.CustomHeadsetEffect effectData, ref Guid effectId);

		// Token: 0x06000178 RID: 376
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateMousepadEffect(NativeMethods.MousepadEffectType effect, ref NativeMethods.CustomMousepadEffect effectData, ref Guid effectId);

		// Token: 0x06000179 RID: 377
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateKeypadEffect(NativeMethods.KeypadEffectType effect, ref NativeMethods.CustomKeypadEffect effectData, ref Guid effectId);

		// Token: 0x0600017A RID: 378
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult CreateChromaLinkEffect(NativeMethods.ChromaLinkEffectType effect, ref NativeMethods.CustomChromaLinkEffect effectData, ref Guid effectId);

		// Token: 0x0600017B RID: 379
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult DeleteEffect(Guid effectId);

		// Token: 0x0600017C RID: 380
		[DllImport("RzChromaSDK.dll", CallingConvention = 2, CharSet = 2)]
		public static extern RzResult SetEffect(Guid effectId);

		// Token: 0x0600017D RID: 381 RVA: 0x0000448A File Offset: 0x0000268A
		public NativeMethods()
		{
		}

		// Token: 0x04000099 RID: 153
		public const string DLL_NAME = "RzChromaSDK.dll";

		// Token: 0x020000BF RID: 191
		public enum KeyboardEffectType
		{
			// Token: 0x04000572 RID: 1394
			None,
			// Token: 0x04000573 RID: 1395
			Breathing,
			// Token: 0x04000574 RID: 1396
			Custom,
			// Token: 0x04000575 RID: 1397
			Reactive,
			// Token: 0x04000576 RID: 1398
			Static,
			// Token: 0x04000577 RID: 1399
			Spectrumcycling,
			// Token: 0x04000578 RID: 1400
			Wave,
			// Token: 0x04000579 RID: 1401
			Reserved,
			// Token: 0x0400057A RID: 1402
			CustomKey,
			// Token: 0x0400057B RID: 1403
			Invalid
		}

		// Token: 0x020000C0 RID: 192
		public struct CustomKeyboardEffect
		{
			// Token: 0x0600043F RID: 1087 RVA: 0x0000E3D7 File Offset: 0x0000C5D7
			public static NativeMethods.CustomKeyboardEffect Create()
			{
				return new NativeMethods.CustomKeyboardEffect(132);
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x0000E3E3 File Offset: 0x0000C5E3
			private CustomKeyboardEffect(int size)
			{
				this.Color = new uint[size];
				this.Key = new uint[size];
			}

			// Token: 0x0400057C RID: 1404
			public const int Rows = 6;

			// Token: 0x0400057D RID: 1405
			public const int Columns = 22;

			// Token: 0x0400057E RID: 1406
			public const int MaxKeys = 132;

			// Token: 0x0400057F RID: 1407
			public const uint KeyFlag = 16777216U;

			// Token: 0x04000580 RID: 1408
			public const uint ColorMask = 16777215U;

			// Token: 0x04000581 RID: 1409
			[MarshalAs(30, SizeConst = 132)]
			public readonly uint[] Color;

			// Token: 0x04000582 RID: 1410
			[MarshalAs(30, SizeConst = 132)]
			public readonly uint[] Key;
		}

		// Token: 0x020000C1 RID: 193
		public enum MouseEffectType
		{
			// Token: 0x04000584 RID: 1412
			None,
			// Token: 0x04000585 RID: 1413
			Blinking,
			// Token: 0x04000586 RID: 1414
			Breathing,
			// Token: 0x04000587 RID: 1415
			Custom,
			// Token: 0x04000588 RID: 1416
			Reactive,
			// Token: 0x04000589 RID: 1417
			Spectrumcycling,
			// Token: 0x0400058A RID: 1418
			Static,
			// Token: 0x0400058B RID: 1419
			Wave,
			// Token: 0x0400058C RID: 1420
			Custom2,
			// Token: 0x0400058D RID: 1421
			Invalid
		}

		// Token: 0x020000C2 RID: 194
		public struct CustomMouseEffect
		{
			// Token: 0x06000441 RID: 1089 RVA: 0x0000E3FD File Offset: 0x0000C5FD
			public static NativeMethods.CustomMouseEffect Create()
			{
				return new NativeMethods.CustomMouseEffect(63);
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x0000E406 File Offset: 0x0000C606
			private CustomMouseEffect(int size)
			{
				this.Color = new uint[size];
			}

			// Token: 0x0400058E RID: 1422
			public const int Rows = 9;

			// Token: 0x0400058F RID: 1423
			public const int Columns = 7;

			// Token: 0x04000590 RID: 1424
			[MarshalAs(30, SizeConst = 63)]
			public readonly uint[] Color;
		}

		// Token: 0x020000C3 RID: 195
		public enum HeadsetEffectType
		{
			// Token: 0x04000592 RID: 1426
			None,
			// Token: 0x04000593 RID: 1427
			Static,
			// Token: 0x04000594 RID: 1428
			Breathing,
			// Token: 0x04000595 RID: 1429
			Spectrumcycling,
			// Token: 0x04000596 RID: 1430
			Custom,
			// Token: 0x04000597 RID: 1431
			Invalid
		}

		// Token: 0x020000C4 RID: 196
		public struct CustomHeadsetEffect
		{
			// Token: 0x06000443 RID: 1091 RVA: 0x0000E414 File Offset: 0x0000C614
			public static NativeMethods.CustomHeadsetEffect Create()
			{
				return new NativeMethods.CustomHeadsetEffect(5);
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x0000E41C File Offset: 0x0000C61C
			private CustomHeadsetEffect(int size)
			{
				this.Color = new uint[size];
			}

			// Token: 0x04000598 RID: 1432
			public const int Leds = 5;

			// Token: 0x04000599 RID: 1433
			[MarshalAs(30, SizeConst = 5)]
			public readonly uint[] Color;
		}

		// Token: 0x020000C5 RID: 197
		public enum MousepadEffectType
		{
			// Token: 0x0400059B RID: 1435
			None,
			// Token: 0x0400059C RID: 1436
			Breathing,
			// Token: 0x0400059D RID: 1437
			Custom,
			// Token: 0x0400059E RID: 1438
			Spectrumcycling,
			// Token: 0x0400059F RID: 1439
			Static,
			// Token: 0x040005A0 RID: 1440
			Wave,
			// Token: 0x040005A1 RID: 1441
			Invalid
		}

		// Token: 0x020000C6 RID: 198
		public struct CustomMousepadEffect
		{
			// Token: 0x06000445 RID: 1093 RVA: 0x0000E42A File Offset: 0x0000C62A
			public static NativeMethods.CustomMousepadEffect Create()
			{
				return new NativeMethods.CustomMousepadEffect(15);
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x0000E433 File Offset: 0x0000C633
			private CustomMousepadEffect(int size)
			{
				this.Color = new uint[size];
			}

			// Token: 0x040005A2 RID: 1442
			public const int Leds = 15;

			// Token: 0x040005A3 RID: 1443
			[MarshalAs(30, SizeConst = 15)]
			public readonly uint[] Color;
		}

		// Token: 0x020000C7 RID: 199
		public enum KeypadEffectType
		{
			// Token: 0x040005A5 RID: 1445
			None,
			// Token: 0x040005A6 RID: 1446
			Breathing,
			// Token: 0x040005A7 RID: 1447
			Custom,
			// Token: 0x040005A8 RID: 1448
			Reactive,
			// Token: 0x040005A9 RID: 1449
			Spectrumcycling,
			// Token: 0x040005AA RID: 1450
			Static,
			// Token: 0x040005AB RID: 1451
			Wave,
			// Token: 0x040005AC RID: 1452
			Invalid
		}

		// Token: 0x020000C8 RID: 200
		public struct CustomKeypadEffect
		{
			// Token: 0x06000447 RID: 1095 RVA: 0x0000E441 File Offset: 0x0000C641
			public static NativeMethods.CustomKeypadEffect Create()
			{
				return new NativeMethods.CustomKeypadEffect(20);
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x0000E44A File Offset: 0x0000C64A
			private CustomKeypadEffect(int size)
			{
				this.Color = new uint[size];
			}

			// Token: 0x040005AD RID: 1453
			public const int Rows = 4;

			// Token: 0x040005AE RID: 1454
			public const int Columns = 5;

			// Token: 0x040005AF RID: 1455
			[MarshalAs(30, SizeConst = 20)]
			public readonly uint[] Color;
		}

		// Token: 0x020000C9 RID: 201
		public enum ChromaLinkEffectType
		{
			// Token: 0x040005B1 RID: 1457
			None,
			// Token: 0x040005B2 RID: 1458
			Custom,
			// Token: 0x040005B3 RID: 1459
			Static,
			// Token: 0x040005B4 RID: 1460
			Invalid
		}

		// Token: 0x020000CA RID: 202
		public struct CustomChromaLinkEffect
		{
			// Token: 0x06000449 RID: 1097 RVA: 0x0000E458 File Offset: 0x0000C658
			public static NativeMethods.CustomChromaLinkEffect Create()
			{
				return new NativeMethods.CustomChromaLinkEffect(5);
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x0000E460 File Offset: 0x0000C660
			private CustomChromaLinkEffect(int size)
			{
				this.Color = new uint[size];
			}

			// Token: 0x040005B5 RID: 1461
			public const int Leds = 5;

			// Token: 0x040005B6 RID: 1462
			[MarshalAs(30, SizeConst = 5)]
			public readonly uint[] Color;
		}
	}
}
