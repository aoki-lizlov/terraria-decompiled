using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000045 RID: 69
	internal static class CorsairHelper
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00007788 File Offset: 0x00005988
		public static Fragment CreateFragment(CorsairLedPosition[] leds, Vector2 offset)
		{
			Point[] array = new Point[leds.Length];
			Vector2[] array2 = new Vector2[leds.Length];
			double num = 0.0;
			double num2 = 0.0;
			if (leds.Length != 0)
			{
				num = leds[0].Top + (double)offset.Y;
				num2 = leds[0].Top + (double)offset.Y;
			}
			for (int i = 0; i < leds.Length; i++)
			{
				int num3 = i;
				leds[num3].Left = leds[num3].Left + (double)offset.X;
				int num4 = i;
				leds[num4].Top = leds[num4].Top + (double)offset.Y;
				Point point;
				point..ctor((int)Math.Floor((leds[i].Left + leds[i].Width * 0.5) / 20.0), (int)Math.Floor(leds[i].Top / 20.0));
				array[i] = point;
				num = Math.Min(leds[i].Top, num);
				num2 = Math.Max(leds[i].Top, num2);
			}
			double num5 = 1.0;
			if (num != num2)
			{
				num5 = 1.0 / (num2 - num);
			}
			for (int j = 0; j < leds.Length; j++)
			{
				array2[j] = new Vector2((float)((leds[j].Left + leds[j].Width * 0.5) * num5), (float)((leds[j].Top - num) * num5));
			}
			return Fragment.FromCustom(array, array2);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000793C File Offset: 0x00005B3C
		public static CorsairLedPosition[] GetLedPositionsForMouseMatOrKeyboard(int deviceIndex)
		{
			IntPtr intPtr = NativeMethods.CorsairGetLedPositionsByDeviceIndex(deviceIndex);
			if (intPtr == IntPtr.Zero)
			{
				return new CorsairLedPosition[0];
			}
			CorsairLedPositions corsairLedPositions = (CorsairLedPositions)Marshal.PtrToStructure(intPtr, typeof(CorsairLedPositions));
			int numberOfLed = corsairLedPositions.NumberOfLed;
			CorsairLedPosition[] array = new CorsairLedPosition[numberOfLed];
			int num = Marshal.SizeOf(typeof(CorsairLedPosition));
			for (int i = 0; i < numberOfLed; i++)
			{
				array[i] = (CorsairLedPosition)Marshal.PtrToStructure(corsairLedPositions.LedPositionPtr, typeof(CorsairLedPosition));
				corsairLedPositions.LedPositionPtr += num;
			}
			return array;
		}
	}
}
