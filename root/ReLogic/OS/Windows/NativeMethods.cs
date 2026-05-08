using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReLogic.OS.Windows
{
	// Token: 0x0200006E RID: 110
	internal static class NativeMethods
	{
		// Token: 0x06000264 RID: 612
		[DllImport("user32.dll", CharSet = 3)]
		public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000265 RID: 613
		[DllImport("user32.dll", CharSet = 3)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		// Token: 0x06000266 RID: 614
		[DllImport("user32.dll", CharSet = 3)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000267 RID: 615
		[DllImport("user32.dll", CallingConvention = 3, CharSet = 3)]
		public static extern bool TranslateMessage(ref Message message);

		// Token: 0x06000268 RID: 616
		[DllImport("user32.dll", CharSet = 3)]
		public static extern IntPtr GetForegroundWindow();

		// Token: 0x06000269 RID: 617
		[DllImport("kernel32.dll", CharSet = 3)]
		public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out NativeMethods.ConsoleMode lpMode);

		// Token: 0x0600026A RID: 618
		[DllImport("kernel32.dll", CharSet = 3)]
		public static extern bool SetConsoleMode(IntPtr hConsoleHandle, NativeMethods.ConsoleMode dwMode);

		// Token: 0x0600026B RID: 619
		[DllImport("kernel32.dll", CharSet = 3)]
		public static extern IntPtr GetStdHandle(NativeMethods.StdHandleType nStdHandle);

		// Token: 0x0600026C RID: 620
		[DllImport("user32.dll")]
		[return: MarshalAs(2)]
		public static extern bool FlashWindowEx(ref NativeMethods.FlashInfo flashInfo);

		// Token: 0x0600026D RID: 621
		[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, NativeMethods.DeviceCap nIndex);

		// Token: 0x0600026E RID: 622
		[DllImport("shell32.dll", CharSet = 4)]
		private static extern int SHFileOperation(ref NativeMethods.SHFILEOPSTRUCT FileOp);

		// Token: 0x0600026F RID: 623 RVA: 0x0000A39C File Offset: 0x0000859C
		private static bool Send(string path, NativeMethods.FileOperationFlags flags)
		{
			bool flag;
			try
			{
				NativeMethods.SHFILEOPSTRUCT shfileopstruct = new NativeMethods.SHFILEOPSTRUCT
				{
					wFunc = NativeMethods.FileOperationType.FO_DELETE,
					pFrom = path + "\0\0",
					fFlags = (NativeMethods.FileOperationFlags.FOF_ALLOWUNDO | flags)
				};
				NativeMethods.SHFileOperation(ref shfileopstruct);
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A3FC File Offset: 0x000085FC
		private static bool Send(string path)
		{
			return NativeMethods.Send(path, NativeMethods.FileOperationFlags.FOF_NOCONFIRMATION | NativeMethods.FileOperationFlags.FOF_WANTNUKEWARNING);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A409 File Offset: 0x00008609
		public static bool MoveToRecycleBin(string path)
		{
			return NativeMethods.Send(path, NativeMethods.FileOperationFlags.FOF_SILENT | NativeMethods.FileOperationFlags.FOF_NOCONFIRMATION | NativeMethods.FileOperationFlags.FOF_NOERRORUI);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A418 File Offset: 0x00008618
		private static bool DeleteFile(string path, NativeMethods.FileOperationFlags flags)
		{
			bool flag;
			try
			{
				NativeMethods.SHFILEOPSTRUCT shfileopstruct = new NativeMethods.SHFILEOPSTRUCT
				{
					wFunc = NativeMethods.FileOperationType.FO_DELETE,
					pFrom = path + "\0\0",
					fFlags = flags
				};
				NativeMethods.SHFileOperation(ref shfileopstruct);
				flag = true;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A474 File Offset: 0x00008674
		private static bool DeleteCompletelySilent(string path)
		{
			return NativeMethods.DeleteFile(path, NativeMethods.FileOperationFlags.FOF_SILENT | NativeMethods.FileOperationFlags.FOF_NOCONFIRMATION | NativeMethods.FileOperationFlags.FOF_NOERRORUI);
		}

		// Token: 0x06000274 RID: 628
		[DllImport("user32.dll")]
		internal static extern uint GetRawInputDeviceList([Out] NativeMethods.RAWINPUTDEVICELIST[] pRawInputDeviceList, ref uint puiNumDevices, uint cbSize);

		// Token: 0x06000275 RID: 629
		[DllImport("user32.dll", CharSet = 3)]
		public static extern int ShowCursor(bool bShow);

		// Token: 0x0400032A RID: 810
		internal const int RIM_TYPEMOUSE = 0;

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x06000465 RID: 1125
		public delegate IntPtr WndProcCallback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x020000D6 RID: 214
		public enum StdHandleType
		{
			// Token: 0x040005CB RID: 1483
			Input = -10,
			// Token: 0x040005CC RID: 1484
			Output = -11,
			// Token: 0x040005CD RID: 1485
			Error = -12
		}

		// Token: 0x020000D7 RID: 215
		[Flags]
		public enum ConsoleMode
		{
			// Token: 0x040005CF RID: 1487
			ProcessedInput = 1,
			// Token: 0x040005D0 RID: 1488
			LineInput = 2,
			// Token: 0x040005D1 RID: 1489
			EchoInput = 4,
			// Token: 0x040005D2 RID: 1490
			WindowInput = 8,
			// Token: 0x040005D3 RID: 1491
			MouseInput = 16,
			// Token: 0x040005D4 RID: 1492
			InsertMode = 32,
			// Token: 0x040005D5 RID: 1493
			QuickEditMode = 64,
			// Token: 0x040005D6 RID: 1494
			ExtendedFlags = 128,
			// Token: 0x040005D7 RID: 1495
			AutoPosition = 256,
			// Token: 0x040005D8 RID: 1496
			VirtualTerminalInput = 512
		}

		// Token: 0x020000D8 RID: 216
		[Flags]
		public enum FlashFlags : uint
		{
			// Token: 0x040005DA RID: 1498
			Stop = 0U,
			// Token: 0x040005DB RID: 1499
			Caption = 1U,
			// Token: 0x040005DC RID: 1500
			Tray = 2U,
			// Token: 0x040005DD RID: 1501
			CaptionAndTray = 3U,
			// Token: 0x040005DE RID: 1502
			Timer = 4U,
			// Token: 0x040005DF RID: 1503
			UntilFocused = 12U
		}

		// Token: 0x020000D9 RID: 217
		public struct FlashInfo
		{
			// Token: 0x06000468 RID: 1128 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
			public static NativeMethods.FlashInfo CreateStart(IntPtr hWnd)
			{
				return new NativeMethods.FlashInfo
				{
					_cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(NativeMethods.FlashInfo))),
					_hWnd = hWnd,
					_dwFlags = (NativeMethods.FlashFlags)15U,
					_uCount = uint.MaxValue,
					_dwTimeout = 0U
				};
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x0000E544 File Offset: 0x0000C744
			public static NativeMethods.FlashInfo CreateStop(IntPtr hWnd)
			{
				return new NativeMethods.FlashInfo
				{
					_cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(NativeMethods.FlashInfo))),
					_hWnd = hWnd,
					_dwFlags = NativeMethods.FlashFlags.Stop,
					_uCount = uint.MaxValue,
					_dwTimeout = 0U
				};
			}

			// Token: 0x040005E0 RID: 1504
			private uint _cbSize;

			// Token: 0x040005E1 RID: 1505
			private IntPtr _hWnd;

			// Token: 0x040005E2 RID: 1506
			private NativeMethods.FlashFlags _dwFlags;

			// Token: 0x040005E3 RID: 1507
			private uint _uCount;

			// Token: 0x040005E4 RID: 1508
			private uint _dwTimeout;
		}

		// Token: 0x020000DA RID: 218
		public enum DeviceCap
		{
			// Token: 0x040005E6 RID: 1510
			VertRes = 10,
			// Token: 0x040005E7 RID: 1511
			DesktopVertRes = 117
		}

		// Token: 0x020000DB RID: 219
		[Flags]
		private enum FileOperationFlags : ushort
		{
			// Token: 0x040005E9 RID: 1513
			FOF_SILENT = 4,
			// Token: 0x040005EA RID: 1514
			FOF_NOCONFIRMATION = 16,
			// Token: 0x040005EB RID: 1515
			FOF_ALLOWUNDO = 64,
			// Token: 0x040005EC RID: 1516
			FOF_SIMPLEPROGRESS = 256,
			// Token: 0x040005ED RID: 1517
			FOF_NOERRORUI = 1024,
			// Token: 0x040005EE RID: 1518
			FOF_WANTNUKEWARNING = 16384
		}

		// Token: 0x020000DC RID: 220
		private enum FileOperationType : uint
		{
			// Token: 0x040005F0 RID: 1520
			FO_MOVE = 1U,
			// Token: 0x040005F1 RID: 1521
			FO_COPY,
			// Token: 0x040005F2 RID: 1522
			FO_DELETE,
			// Token: 0x040005F3 RID: 1523
			FO_RENAME
		}

		// Token: 0x020000DD RID: 221
		[StructLayout(0, CharSet = 4, Pack = 1)]
		private struct SHFILEOPSTRUCT
		{
			// Token: 0x040005F4 RID: 1524
			public IntPtr hwnd;

			// Token: 0x040005F5 RID: 1525
			[MarshalAs(8)]
			public NativeMethods.FileOperationType wFunc;

			// Token: 0x040005F6 RID: 1526
			public string pFrom;

			// Token: 0x040005F7 RID: 1527
			public string pTo;

			// Token: 0x040005F8 RID: 1528
			public NativeMethods.FileOperationFlags fFlags;

			// Token: 0x040005F9 RID: 1529
			[MarshalAs(2)]
			public bool fAnyOperationsAborted;

			// Token: 0x040005FA RID: 1530
			public IntPtr hNameMappings;

			// Token: 0x040005FB RID: 1531
			public string lpszProgressTitle;
		}

		// Token: 0x020000DE RID: 222
		internal struct RAWINPUTDEVICELIST
		{
			// Token: 0x040005FC RID: 1532
			public IntPtr hDevice;

			// Token: 0x040005FD RID: 1533
			public uint dwType;
		}
	}
}
