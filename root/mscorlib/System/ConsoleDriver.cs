using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000204 RID: 516
	internal static class ConsoleDriver
	{
		// Token: 0x06001933 RID: 6451 RVA: 0x0005F514 File Offset: 0x0005D714
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			if (environmentVariable == "dumb")
			{
				ConsoleDriver.is_console = false;
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0005F579 File Offset: 0x0005D779
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0005F580 File Offset: 0x0005D780
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0005F587 File Offset: 0x0005D787
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0005F58F File Offset: 0x0005D78F
		public static bool Initialized
		{
			get
			{
				return ConsoleDriver.driver.Initialized;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0005F59B File Offset: 0x0005D79B
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0005F5A7 File Offset: 0x0005D7A7
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.driver.BackgroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x0005F5CD File Offset: 0x0005D7CD
		// (set) Token: 0x0600193B RID: 6459 RVA: 0x0005F5D9 File Offset: 0x0005D7D9
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.driver.BufferHeight;
			}
			set
			{
				ConsoleDriver.driver.BufferHeight = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0005F5E6 File Offset: 0x0005D7E6
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0005F5F2 File Offset: 0x0005D7F2
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.driver.BufferWidth;
			}
			set
			{
				ConsoleDriver.driver.BufferWidth = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0005F5FF File Offset: 0x0005D7FF
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.driver.CapsLock;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x0005F60B File Offset: 0x0005D80B
		// (set) Token: 0x06001940 RID: 6464 RVA: 0x0005F617 File Offset: 0x0005D817
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.driver.CursorLeft;
			}
			set
			{
				ConsoleDriver.driver.CursorLeft = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x0005F624 File Offset: 0x0005D824
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x0005F630 File Offset: 0x0005D830
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.driver.CursorSize;
			}
			set
			{
				ConsoleDriver.driver.CursorSize = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0005F63D File Offset: 0x0005D83D
		// (set) Token: 0x06001944 RID: 6468 RVA: 0x0005F649 File Offset: 0x0005D849
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.driver.CursorTop;
			}
			set
			{
				ConsoleDriver.driver.CursorTop = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0005F656 File Offset: 0x0005D856
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0005F662 File Offset: 0x0005D862
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.driver.CursorVisible;
			}
			set
			{
				ConsoleDriver.driver.CursorVisible = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0005F66F File Offset: 0x0005D86F
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.driver.KeyAvailable;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0005F67B File Offset: 0x0005D87B
		// (set) Token: 0x06001949 RID: 6473 RVA: 0x0005F687 File Offset: 0x0005D887
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.driver.ForegroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0005F6AD File Offset: 0x0005D8AD
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowHeight;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0005F6B9 File Offset: 0x0005D8B9
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowWidth;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0005F6C5 File Offset: 0x0005D8C5
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.driver.NumberLock;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0005F6D1 File Offset: 0x0005D8D1
		// (set) Token: 0x0600194E RID: 6478 RVA: 0x0005F6DD File Offset: 0x0005D8DD
		public static string Title
		{
			get
			{
				return ConsoleDriver.driver.Title;
			}
			set
			{
				ConsoleDriver.driver.Title = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0005F6EA File Offset: 0x0005D8EA
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0005F6F6 File Offset: 0x0005D8F6
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.driver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.driver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0005F703 File Offset: 0x0005D903
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0005F70F File Offset: 0x0005D90F
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.driver.WindowHeight;
			}
			set
			{
				ConsoleDriver.driver.WindowHeight = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0005F71C File Offset: 0x0005D91C
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0005F728 File Offset: 0x0005D928
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.driver.WindowLeft;
			}
			set
			{
				ConsoleDriver.driver.WindowLeft = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0005F735 File Offset: 0x0005D935
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0005F741 File Offset: 0x0005D941
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.driver.WindowTop;
			}
			set
			{
				ConsoleDriver.driver.WindowTop = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0005F74E File Offset: 0x0005D94E
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0005F75A File Offset: 0x0005D95A
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.driver.WindowWidth;
			}
			set
			{
				ConsoleDriver.driver.WindowWidth = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0005F767 File Offset: 0x0005D967
		public static bool IsErrorRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleError);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0005F776 File Offset: 0x0005D976
		public static bool IsOutputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleOutput);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0005F785 File Offset: 0x0005D985
		public static bool IsInputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleInput);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0005F794 File Offset: 0x0005D994
		public static void Beep(int frequency, int duration)
		{
			ConsoleDriver.driver.Beep(frequency, duration);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005F7A2 File Offset: 0x0005D9A2
		public static void Clear()
		{
			ConsoleDriver.driver.Clear();
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005F7B0 File Offset: 0x0005D9B0
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, ConsoleColor.Black);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005F7D0 File Offset: 0x0005D9D0
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.driver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005F7F5 File Offset: 0x0005D9F5
		public static void Init()
		{
			ConsoleDriver.driver.Init();
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0005F804 File Offset: 0x0005DA04
		public static int Read()
		{
			return (int)ConsoleDriver.ReadKey(false).KeyChar;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005F81F File Offset: 0x0005DA1F
		public static string ReadLine()
		{
			return ConsoleDriver.driver.ReadLine();
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0005F82B File Offset: 0x0005DA2B
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0005F838 File Offset: 0x0005DA38
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0005F844 File Offset: 0x0005DA44
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.driver.SetBufferSize(width, height);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0005F852 File Offset: 0x0005DA52
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.driver.SetCursorPosition(left, top);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0005F860 File Offset: 0x0005DA60
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.driver.SetWindowPosition(left, top);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0005F86E File Offset: 0x0005DA6E
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.driver.SetWindowSize(width, height);
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0005F87C File Offset: 0x0005DA7C
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput);
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x0600196A RID: 6506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x0600196B RID: 6507
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x0600196C RID: 6508
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x0600196D RID: 6509
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x0600196E RID: 6510
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetBreak(bool wantBreak);

		// Token: 0x040015CD RID: 5581
		internal static IConsoleDriver driver;

		// Token: 0x040015CE RID: 5582
		private static bool is_console;

		// Token: 0x040015CF RID: 5583
		private static bool called_isatty;
	}
}
