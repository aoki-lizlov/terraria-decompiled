using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000249 RID: 585
	internal class WindowsConsoleDriver : IConsoleDriver
	{
		// Token: 0x06001C08 RID: 7176 RVA: 0x0006A04C File Offset: 0x0006824C
		public WindowsConsoleDriver()
		{
			this.outputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_OUTPUT);
			this.inputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_INPUT);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			this.defaultAttribute = consoleScreenBufferInfo.Attribute;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0006A09B File Offset: 0x0006829B
		private static ConsoleColor GetForeground(short attr)
		{
			attr &= 15;
			return (ConsoleColor)attr;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0006A0A5 File Offset: 0x000682A5
		private static ConsoleColor GetBackground(short attr)
		{
			attr &= 240;
			attr = (short)(attr >> 4);
			return (ConsoleColor)attr;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0006A0B8 File Offset: 0x000682B8
		private static short GetAttrForeground(int attr, ConsoleColor color)
		{
			attr &= -16;
			return (short)(attr | (int)color);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0006A0C4 File Offset: 0x000682C4
		private static short GetAttrBackground(int attr, ConsoleColor color)
		{
			attr &= -241;
			int num = (int)((int)color << 4);
			return (short)(attr | num);
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0006A0E4 File Offset: 0x000682E4
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x0006A114 File Offset: 0x00068314
		public ConsoleColor BackgroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetBackground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrBackground = WindowsConsoleDriver.GetAttrBackground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrBackground);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x0006A154 File Offset: 0x00068354
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x0006A182 File Offset: 0x00068382
		public int BufferHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.Y;
			}
			set
			{
				this.SetBufferSize(this.BufferWidth, value);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0006A194 File Offset: 0x00068394
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x0006A1C2 File Offset: 0x000683C2
		public int BufferWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.X;
			}
			set
			{
				this.SetBufferSize(value, this.BufferHeight);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0006A1D1 File Offset: 0x000683D1
		public bool CapsLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(20) & 1) == 1;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0006A1E0 File Offset: 0x000683E0
		// (set) Token: 0x06001C15 RID: 7189 RVA: 0x0006A20E File Offset: 0x0006840E
		public int CursorLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.X;
			}
			set
			{
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0006A220 File Offset: 0x00068420
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x0006A24C File Offset: 0x0006844C
		public int CursorSize
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Size;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				consoleCursorInfo.Size = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x0006A2A8 File Offset: 0x000684A8
		// (set) Token: 0x06001C19 RID: 7193 RVA: 0x0006A2D6 File Offset: 0x000684D6
		public int CursorTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.Y;
			}
			set
			{
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0006A2E8 File Offset: 0x000684E8
		// (set) Token: 0x06001C1B RID: 7195 RVA: 0x0006A314 File Offset: 0x00068514
		public bool CursorVisible
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Visible;
			}
			set
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				if (consoleCursorInfo.Visible == value)
				{
					return;
				}
				consoleCursorInfo.Visible = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0006A364 File Offset: 0x00068564
		// (set) Token: 0x06001C1D RID: 7197 RVA: 0x0006A394 File Offset: 0x00068594
		public ConsoleColor ForegroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetForeground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrForeground = WindowsConsoleDriver.GetAttrForeground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrForeground);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001C1E RID: 7198 RVA: 0x0006A3D4 File Offset: 0x000685D4
		public bool KeyAvailable
		{
			get
			{
				InputRecord inputRecord = default(InputRecord);
				int num;
				while (WindowsConsoleDriver.PeekConsoleInput(this.inputHandle, out inputRecord, 1, out num))
				{
					if (num == 0)
					{
						return false;
					}
					if (inputRecord.EventType == 1 && inputRecord.KeyDown && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
					{
						return true;
					}
					if (!WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
					{
						throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error().ToString());
					}
				}
				throw new InvalidOperationException("Error in PeekConsoleInput " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0000408A File Offset: 0x0000228A
		public bool Initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0006A46C File Offset: 0x0006866C
		public int LargestWindowHeight
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error().ToString());
				}
				return (int)largestConsoleWindowSize.Y;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0006A4B8 File Offset: 0x000686B8
		public int LargestWindowWidth
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error().ToString());
				}
				return (int)largestConsoleWindowSize.X;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0006A504 File Offset: 0x00068704
		public bool NumberLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(144) & 1) == 1;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0006A518 File Offset: 0x00068718
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x0006A578 File Offset: 0x00068778
		public string Title
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 1024) == 0)
				{
					stringBuilder = new StringBuilder(26001);
					if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 26000) == 0)
					{
						throw new Exception("Got " + Marshal.GetLastWin32Error().ToString());
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!WindowsConsoleDriver.SetConsoleTitle(value))
				{
					throw new Exception("Got " + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0006A5B8 File Offset: 0x000687B8
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x0006A5F8 File Offset: 0x000687F8
		public bool TreatControlCAsInput
		{
			get
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
				return (num & 1) == 0;
			}
			set
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
				if ((num & 1) == 0 == value)
				{
					return;
				}
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!WindowsConsoleDriver.SetConsoleMode(this.inputHandle, num))
				{
					throw new Exception("Failed in SetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0006A674 File Offset: 0x00068874
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x0006A6B0 File Offset: 0x000688B0
		public int WindowHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Bottom - consoleScreenBufferInfo.Window.Top + 1);
			}
			set
			{
				this.SetWindowSize(this.WindowWidth, value);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0006A6C0 File Offset: 0x000688C0
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0006A6EE File Offset: 0x000688EE
		public int WindowLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Left;
			}
			set
			{
				this.SetWindowPosition(value, this.WindowTop);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0006A700 File Offset: 0x00068900
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0006A72E File Offset: 0x0006892E
		public int WindowTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Top;
			}
			set
			{
				this.SetWindowPosition(this.WindowLeft, value);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006A740 File Offset: 0x00068940
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0006A77C File Offset: 0x0006897C
		public int WindowWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Right - consoleScreenBufferInfo.Window.Left + 1);
			}
			set
			{
				this.SetWindowSize(value, this.WindowHeight);
			}
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x0006A78B File Offset: 0x0006898B
		public void Beep(int frequency, int duration)
		{
			WindowsConsoleDriver._Beep(frequency, duration);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006A794 File Offset: 0x00068994
		public void Clear()
		{
			Coord coord = default(Coord);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			int num = (int)(consoleScreenBufferInfo.Size.X * consoleScreenBufferInfo.Size.Y);
			int num2;
			WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, ' ', num, coord, out num2);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, consoleScreenBufferInfo.Attribute, num, coord, out num2);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x0006A81C File Offset: 0x00068A1C
		public unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceBackColor");
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			CharInfo[] array = new CharInfo[sourceWidth * sourceHeight];
			Coord coord = new Coord(sourceWidth, sourceHeight);
			Coord coord2 = new Coord(0, 0);
			SmallRect smallRect = new SmallRect(sourceLeft, sourceTop, sourceLeft + sourceWidth - 1, sourceTop + sourceHeight - 1);
			fixed (CharInfo* ptr = &array[0])
			{
				void* ptr2 = (void*)ptr;
				if (!WindowsConsoleDriver.ReadConsoleOutput(this.outputHandle, ptr2, coord, coord2, ref smallRect))
				{
					throw new ArgumentException(string.Empty, "Cannot read from the specified coordinates.");
				}
			}
			short num = WindowsConsoleDriver.GetAttrForeground(0, sourceForeColor);
			num = WindowsConsoleDriver.GetAttrBackground((int)num, sourceBackColor);
			coord2 = new Coord(sourceLeft, sourceTop);
			int i = 0;
			while (i < sourceHeight)
			{
				int num2;
				WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, sourceChar, sourceWidth, coord2, out num2);
				WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, num, sourceWidth, coord2, out num2);
				i++;
				coord2.Y += 1;
			}
			coord2 = new Coord(0, 0);
			smallRect = new SmallRect(targetLeft, targetTop, targetLeft + sourceWidth - 1, targetTop + sourceHeight - 1);
			if (!WindowsConsoleDriver.WriteConsoleOutput(this.outputHandle, array, coord, coord2, ref smallRect))
			{
				throw new ArgumentException(string.Empty, "Cannot write to the specified coordinates.");
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00004088 File Offset: 0x00002288
		public void Init()
		{
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0006A97C File Offset: 0x00068B7C
		public string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag;
			do
			{
				ConsoleKeyInfo consoleKeyInfo = this.ReadKey(false);
				flag = consoleKeyInfo.KeyChar == '\n';
				if (!flag)
				{
					stringBuilder.Append(consoleKeyInfo.KeyChar);
				}
			}
			while (!flag);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x0006A9C0 File Offset: 0x00068BC0
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			InputRecord inputRecord = default(InputRecord);
			int num;
			while (WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
			{
				if (inputRecord.KeyDown && inputRecord.EventType == 1 && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
				{
					bool flag = (inputRecord.ControlKeyState & 3) != 0;
					bool flag2 = (inputRecord.ControlKeyState & 12) != 0;
					bool flag3 = (inputRecord.ControlKeyState & 16) != 0;
					return new ConsoleKeyInfo(inputRecord.Character, (ConsoleKey)inputRecord.VirtualKeyCode, flag3, flag, flag2);
				}
			}
			throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error().ToString());
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0006AA5F File Offset: 0x00068C5F
		public void ResetColor()
		{
			WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, this.defaultAttribute);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0006AA74 File Offset: 0x00068C74
		public void SetBufferSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			if (width - 1 > (int)consoleScreenBufferInfo.Window.Right)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height - 1 > (int)consoleScreenBufferInfo.Window.Bottom)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			Coord coord = new Coord(width, height);
			if (!WindowsConsoleDriver.SetConsoleScreenBufferSize(this.outputHandle, coord))
			{
				throw new ArgumentOutOfRangeException("height/width", "Cannot be smaller than the window size.");
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0006AAF4 File Offset: 0x00068CF4
		public void SetCursorPosition(int left, int top)
		{
			Coord coord = new Coord(left, top);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006AB18 File Offset: 0x00068D18
		public void SetWindowPosition(int left, int top)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Left = (short)left;
			window.Top = (short)top;
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006AB88 File Offset: 0x00068D88
		public void SetWindowSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Right = (short)((int)window.Left + width - 1);
			window.Bottom = (short)((int)window.Top + height - 1);
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006AC08 File Offset: 0x00068E08
		private static bool IsModifierKey(short virtualKeyCode)
		{
			return virtualKeyCode - 16 <= 2 || virtualKeyCode == 20 || virtualKeyCode - 144 <= 1;
		}

		// Token: 0x06001C3B RID: 7227
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetStdHandle(Handles handle);

		// Token: 0x06001C3C RID: 7228
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "Beep", SetLastError = true)]
		private static extern void _Beep(int frequency, int duration);

		// Token: 0x06001C3D RID: 7229
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

		// Token: 0x06001C3E RID: 7230
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputCharacter(IntPtr handle, char c, int size, Coord coord, out int written);

		// Token: 0x06001C3F RID: 7231
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputAttribute(IntPtr handle, short c, int size, Coord coord, out int written);

		// Token: 0x06001C40 RID: 7232
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorPosition(IntPtr handle, Coord coord);

		// Token: 0x06001C41 RID: 7233
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr handle, short attribute);

		// Token: 0x06001C42 RID: 7234
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleScreenBufferSize(IntPtr handle, Coord newSize);

		// Token: 0x06001C43 RID: 7235
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleWindowInfo(IntPtr handle, bool absolute, ref SmallRect rect);

		// Token: 0x06001C44 RID: 7236
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetConsoleTitle(StringBuilder sb, int size);

		// Token: 0x06001C45 RID: 7237
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTitle(string title);

		// Token: 0x06001C46 RID: 7238
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleCursorInfo(IntPtr handle, out ConsoleCursorInfo info);

		// Token: 0x06001C47 RID: 7239
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorInfo(IntPtr handle, ref ConsoleCursorInfo info);

		// Token: 0x06001C48 RID: 7240
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern short GetKeyState(int virtKey);

		// Token: 0x06001C49 RID: 7241
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleMode(IntPtr handle, out int mode);

		// Token: 0x06001C4A RID: 7242
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleMode(IntPtr handle, int mode);

		// Token: 0x06001C4B RID: 7243
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool PeekConsoleInput(IntPtr handle, out InputRecord record, int length, out int eventsRead);

		// Token: 0x06001C4C RID: 7244
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ReadConsoleInput(IntPtr handle, out InputRecord record, int length, out int nread);

		// Token: 0x06001C4D RID: 7245
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern Coord GetLargestConsoleWindowSize(IntPtr handle);

		// Token: 0x06001C4E RID: 7246
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern bool ReadConsoleOutput(IntPtr handle, void* buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x06001C4F RID: 7247
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr handle, CharInfo[] buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x040018C1 RID: 6337
		private IntPtr inputHandle;

		// Token: 0x040018C2 RID: 6338
		private IntPtr outputHandle;

		// Token: 0x040018C3 RID: 6339
		private short defaultAttribute;
	}
}
