using System;
using System.Runtime.InteropServices;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x0200004A RID: 74
	internal class NativeMethods
	{
		// Token: 0x060001CA RID: 458
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		[return: MarshalAs(3)]
		public static extern bool CorsairSetLedsColors(int size, [In] [Out] CorsairLedColor[] ledsColors);

		// Token: 0x060001CB RID: 459
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		[return: MarshalAs(3)]
		public static extern bool CorsairSetLedsColorsAsync(int size, [In] [Out] CorsairLedColor[] ledsColors, IntPtr callback, IntPtr context);

		// Token: 0x060001CC RID: 460
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern int CorsairGetDeviceCount();

		// Token: 0x060001CD RID: 461
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern IntPtr CorsairGetDeviceInfo(int deviceIndex);

		// Token: 0x060001CE RID: 462
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern IntPtr CorsairGetLedPositions();

		// Token: 0x060001CF RID: 463
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern IntPtr CorsairGetLedPositionsByDeviceIndex(int deviceIndex);

		// Token: 0x060001D0 RID: 464
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern bool CorsairRequestControl(CorsairAccessMode accessMode);

		// Token: 0x060001D1 RID: 465
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern CorsairProtocolDetails CorsairPerformProtocolHandshake();

		// Token: 0x060001D2 RID: 466
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern CorsairError CorsairGetLastError();

		// Token: 0x060001D3 RID: 467
		[DllImport("CUESDK_2015.dll", CallingConvention = 2, CharSet = 3)]
		public static extern bool CorsairReleaseControl(CorsairAccessMode accessMode);

		// Token: 0x060001D4 RID: 468 RVA: 0x0000448A File Offset: 0x0000268A
		public NativeMethods()
		{
		}

		// Token: 0x040001BB RID: 443
		private const string DLL_NAME = "CUESDK_2015.dll";
	}
}
