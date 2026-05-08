using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000224 RID: 548
	internal class TermInfoDriver : IConsoleDriver
	{
		// Token: 0x06001B15 RID: 6933 RVA: 0x00065710 File Offset: 0x00063910
		private static string TryTermInfoDir(string dir, string term)
		{
			string text = string.Format("{0}/{1:x}/{2}", dir, (int)term[0], term);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(dir, term.Substring(0, 1), term);
			if (File.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0006575C File Offset: 0x0006395C
		private static string SearchTerminfo(string term)
		{
			if (term == null || term == string.Empty)
			{
				return null;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERMINFO");
			if (environmentVariable != null && Directory.Exists(environmentVariable))
			{
				string text = TermInfoDriver.TryTermInfoDir(environmentVariable, term);
				if (text != null)
				{
					return text;
				}
			}
			foreach (string text2 in TermInfoDriver.locations)
			{
				if (Directory.Exists(text2))
				{
					string text = TermInfoDriver.TryTermInfoDir(text2, term);
					if (text != null)
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000657CF File Offset: 0x000639CF
		private void WriteConsole(string str)
		{
			if (str == null)
			{
				return;
			}
			this.stdout.InternalWriteString(str);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000657E1 File Offset: 0x000639E1
		public TermInfoDriver()
			: this(Environment.GetEnvironmentVariable("TERM"))
		{
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000657F4 File Offset: 0x000639F4
		public TermInfoDriver(string term)
		{
			this.term = term;
			string text = TermInfoDriver.SearchTerminfo(term);
			if (text != null)
			{
				this.reader = new TermInfoReader(term, text);
			}
			else if (term == "xterm")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.xterm);
			}
			else if (term == "linux")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.linux);
			}
			if (this.reader == null)
			{
				this.reader = new TermInfoReader(term, KnownTerminals.ansi);
			}
			if (!(Console.stdout is CStreamWriter))
			{
				this.stdout = new CStreamWriter(Console.OpenStandardOutput(0), Console.OutputEncoding, false);
				this.stdout.AutoFlush = true;
				return;
			}
			this.stdout = (CStreamWriter)Console.stdout;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x000658FC File Offset: 0x00063AFC
		public bool Initialized
		{
			get
			{
				return this.inited;
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00065904 File Offset: 0x00063B04
		public void Init()
		{
			if (this.inited)
			{
				return;
			}
			object obj = this.initLock;
			lock (obj)
			{
				if (!this.inited)
				{
					try
					{
						if (!ConsoleDriver.IsConsole)
						{
							throw new IOException("Not a tty.");
						}
						ConsoleDriver.SetEcho(false);
						string text = null;
						this.keypadXmit = this.reader.Get(TermInfoStrings.KeypadXmit);
						this.keypadLocal = this.reader.Get(TermInfoStrings.KeypadLocal);
						if (this.keypadXmit != null)
						{
							this.WriteConsole(this.keypadXmit);
							if (this.keypadLocal != null)
							{
								text += this.keypadLocal;
							}
						}
						this.origPair = this.reader.Get(TermInfoStrings.OrigPair);
						this.origColors = this.reader.Get(TermInfoStrings.OrigColors);
						this.setfgcolor = this.reader.Get(TermInfoStrings.SetAForeground);
						this.setbgcolor = this.reader.Get(TermInfoStrings.SetABackground);
						this.maxColors = this.reader.Get(TermInfoNumbers.MaxColors);
						this.maxColors = Math.Max(Math.Min(this.maxColors, 16), 1);
						string text2 = ((this.origColors == null) ? this.origPair : this.origColors);
						if (text2 != null)
						{
							text += text2;
						}
						if (!ConsoleDriver.TtySetup(this.keypadXmit, text, out this.control_characters, out TermInfoDriver.native_terminal_size))
						{
							this.control_characters = new byte[17];
							TermInfoDriver.native_terminal_size = null;
						}
						this.stdin = new StreamReader(Console.OpenStandardInput(0), Console.InputEncoding);
						this.clear = this.reader.Get(TermInfoStrings.ClearScreen);
						this.bell = this.reader.Get(TermInfoStrings.Bell);
						if (this.clear == null)
						{
							this.clear = this.reader.Get(TermInfoStrings.CursorHome);
							this.clear += this.reader.Get(TermInfoStrings.ClrEos);
						}
						this.csrVisible = this.reader.Get(TermInfoStrings.CursorNormal);
						if (this.csrVisible == null)
						{
							this.csrVisible = this.reader.Get(TermInfoStrings.CursorVisible);
						}
						this.csrInvisible = this.reader.Get(TermInfoStrings.CursorInvisible);
						if (this.term == "cygwin" || this.term == "linux" || (this.term != null && this.term.StartsWith("xterm")) || this.term == "rxvt" || this.term == "dtterm")
						{
							this.titleFormat = "\u001b]0;{0}\a";
						}
						else if (this.term == "iris-ansi")
						{
							this.titleFormat = "\u001bP1.y{0}\u001b\\";
						}
						else if (this.term == "sun-cmd")
						{
							this.titleFormat = "\u001b]l{0}\u001b\\";
						}
						this.cursorAddress = this.reader.Get(TermInfoStrings.CursorAddress);
						this.GetCursorPosition();
						if (this.noGetPosition)
						{
							this.WriteConsole(this.clear);
							this.cursorLeft = 0;
							this.cursorTop = 0;
						}
					}
					finally
					{
						this.inited = true;
					}
				}
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00065C58 File Offset: 0x00063E58
		private void IncrementX()
		{
			this.cursorLeft++;
			if (this.cursorLeft >= this.WindowWidth)
			{
				this.cursorTop++;
				this.cursorLeft = 0;
				if (this.cursorTop >= this.WindowHeight)
				{
					if (this.rl_starty != -1)
					{
						this.rl_starty--;
					}
					this.cursorTop--;
				}
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00065CCC File Offset: 0x00063ECC
		public void WriteSpecialKey(ConsoleKeyInfo key)
		{
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				if (this.cursorLeft > 0 && (this.cursorLeft > this.rl_startx || this.cursorTop != this.rl_starty))
				{
					this.cursorLeft--;
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					this.WriteConsole(" ");
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					return;
				}
				break;
			case ConsoleKey.Tab:
			{
				int num = 8 - this.cursorLeft % 8;
				for (int i = 0; i < num; i++)
				{
					this.IncrementX();
				}
				this.WriteConsole("\t");
				return;
			}
			case (ConsoleKey)10:
			case (ConsoleKey)11:
			case ConsoleKey.Enter:
				break;
			case ConsoleKey.Clear:
				this.WriteConsole(this.clear);
				this.cursorLeft = 0;
				this.cursorTop = 0;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00065DAE File Offset: 0x00063FAE
		public void WriteSpecialKey(char c)
		{
			this.WriteSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00065DC0 File Offset: 0x00063FC0
		public bool IsSpecialKey(ConsoleKeyInfo key)
		{
			if (!this.inited)
			{
				return false;
			}
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				return true;
			case ConsoleKey.Tab:
				return true;
			case ConsoleKey.Clear:
				return true;
			case ConsoleKey.Enter:
				this.cursorLeft = 0;
				this.cursorTop++;
				if (this.cursorTop >= this.WindowHeight)
				{
					this.cursorTop--;
				}
				return false;
			}
			this.IncrementX();
			return false;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00065E41 File Offset: 0x00064041
		public bool IsSpecialKey(char c)
		{
			return this.IsSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00065E54 File Offset: 0x00064054
		private void ChangeColor(string format, ConsoleColor color)
		{
			if (string.IsNullOrEmpty(format))
			{
				return;
			}
			if ((color & (ConsoleColor)(-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException("Invalid Console Color");
			}
			int num = TermInfoDriver._consoleColorToAnsiCode[(int)color] % this.maxColors;
			this.WriteConsole(ParameterizedStrings.Evaluate(format, new ParameterizedStrings.FormatParam[] { num }));
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x00065EAB File Offset: 0x000640AB
		// (set) Token: 0x06001B23 RID: 6947 RVA: 0x00065EC1 File Offset: 0x000640C1
		public ConsoleColor BackgroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setbgcolor, value);
				this.bgcolor = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x00065EE5 File Offset: 0x000640E5
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x00065EFB File Offset: 0x000640FB
		public ConsoleColor ForegroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.fgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setfgcolor, value);
				this.fgcolor = value;
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00065F20 File Offset: 0x00064120
		private void GetCursorPosition()
		{
			int num = 0;
			int num2 = 0;
			int num3 = ConsoleDriver.InternalKeyAvailable(0);
			int num4;
			while (num3-- > 0)
			{
				num4 = this.stdin.Read();
				this.AddToBuffer(num4);
			}
			this.WriteConsole("\u001b[6n");
			if (ConsoleDriver.InternalKeyAvailable(1000) <= 0)
			{
				this.noGetPosition = true;
				return;
			}
			for (num4 = this.stdin.Read(); num4 != 27; num4 = this.stdin.Read())
			{
				this.AddToBuffer(num4);
				if (ConsoleDriver.InternalKeyAvailable(100) <= 0)
				{
					return;
				}
			}
			num4 = this.stdin.Read();
			if (num4 != 91)
			{
				this.AddToBuffer(27);
				this.AddToBuffer(num4);
				return;
			}
			num4 = this.stdin.Read();
			if (num4 != 59)
			{
				num = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num = num * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num--;
			}
			num4 = this.stdin.Read();
			if (num4 != 82)
			{
				num2 = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num2 = num2 * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num2--;
			}
			this.cursorLeft = num2;
			this.cursorTop = num;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00066065 File Offset: 0x00064265
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00066081 File Offset: 0x00064281
		public int BufferHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetBufferSize(this.BufferWidth, value);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0006609E File Offset: 0x0006429E
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x000660BA File Offset: 0x000642BA
		public int BufferWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetBufferSize(value, this.BufferHeight);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000660D7 File Offset: 0x000642D7
		public bool CapsLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000660E8 File Offset: 0x000642E8
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x000660FE File Offset: 0x000642FE
		public int CursorLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorLeft;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0006611B File Offset: 0x0006431B
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x00066131 File Offset: 0x00064331
		public int CursorTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorTop;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0006614E File Offset: 0x0006434E
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x00066164 File Offset: 0x00064364
		public bool CursorVisible
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorVisible;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.cursorVisible = value;
				this.WriteConsole(value ? this.csrVisible : this.csrInvisible);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x00066192 File Offset: 0x00064392
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x000661A3 File Offset: 0x000643A3
		[MonoTODO]
		public int CursorSize
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 1;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x000661B3 File Offset: 0x000643B3
		public bool KeyAvailable
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.writepos > this.readpos || ConsoleDriver.InternalKeyAvailable(0) > 0;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000661DC File Offset: 0x000643DC
		public int LargestWindowHeight
		{
			get
			{
				return this.WindowHeight;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x000661E4 File Offset: 0x000643E4
		public int LargestWindowWidth
		{
			get
			{
				return this.WindowWidth;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000660D7 File Offset: 0x000642D7
		public bool NumberLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x000661EC File Offset: 0x000643EC
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00066202 File Offset: 0x00064402
		public string Title
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.title;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.title = value;
				this.WriteConsole(string.Format(this.titleFormat, value));
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0006622B File Offset: 0x0006442B
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x00066241 File Offset: 0x00064441
		public bool TreatControlCAsInput
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.controlCAsInput;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (this.controlCAsInput == value)
				{
					return;
				}
				ConsoleDriver.SetBreak(value);
				this.controlCAsInput = value;
			}
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0006626C File Offset: 0x0006446C
		private unsafe void CheckWindowDimensions()
		{
			if (TermInfoDriver.native_terminal_size == null || TermInfoDriver.terminal_size == *TermInfoDriver.native_terminal_size)
			{
				return;
			}
			if (*TermInfoDriver.native_terminal_size == -1)
			{
				int num = this.reader.Get(TermInfoNumbers.Columns);
				if (num != 0)
				{
					this.windowWidth = num;
				}
				num = this.reader.Get(TermInfoNumbers.Lines);
				if (num != 0)
				{
					this.windowHeight = num;
				}
			}
			else
			{
				TermInfoDriver.terminal_size = *TermInfoDriver.native_terminal_size;
				this.windowWidth = TermInfoDriver.terminal_size >> 16;
				this.windowHeight = TermInfoDriver.terminal_size & 65535;
			}
			this.bufferHeight = this.windowHeight;
			this.bufferWidth = this.windowWidth;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0006630C File Offset: 0x0006450C
		private void TrySetWindowDimensions(int width, int height)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width", "Value must be higher than 0");
			}
			if (height <= 0)
			{
				throw new ArgumentOutOfRangeException("height", "Value must be highet than 0");
			}
			if (height == this.WindowHeight && width == this.WindowWidth)
			{
				return;
			}
			if (this.term.StartsWith("xterm"))
			{
				this.WriteConsole(string.Concat(new string[]
				{
					"\u001b[8;",
					height.ToString(),
					";",
					width.ToString(),
					"t"
				}));
				Thread.Sleep(50);
				return;
			}
			throw new PlatformNotSupportedException("Resizing can only work in xterm-based terminals");
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x000663B5 File Offset: 0x000645B5
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x000663D1 File Offset: 0x000645D1
		public int WindowHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetWindowSize(this.WindowWidth, value);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x000660D7 File Offset: 0x000642D7
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x000663EE File Offset: 0x000645EE
		public int WindowLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (value == 0)
				{
					return;
				}
				throw new ArgumentOutOfRangeException("Unix terminals only support window position (0; 0)");
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x000660D7 File Offset: 0x000642D7
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x000663EE File Offset: 0x000645EE
		public int WindowTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (value == 0)
				{
					return;
				}
				throw new ArgumentOutOfRangeException("Unix terminals only support window position (0; 0)");
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0006640C File Offset: 0x0006460C
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x00066428 File Offset: 0x00064628
		public int WindowWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetWindowSize(value, this.WindowHeight);
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00066445 File Offset: 0x00064645
		public void Clear()
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.clear);
			this.cursorLeft = 0;
			this.cursorTop = 0;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0006646F File Offset: 0x0006466F
		public void Beep(int frequency, int duration)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.bell);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0006648B File Offset: 0x0006468B
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new PlatformNotSupportedException("Implemented only on Windows");
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000664A8 File Offset: 0x000646A8
		private void AddToBuffer(int b)
		{
			if (this.buffer == null)
			{
				this.buffer = new char[1024];
			}
			else if (this.writepos >= this.buffer.Length)
			{
				char[] array = new char[this.buffer.Length * 2];
				Buffer.BlockCopy(this.buffer, 0, array, 0, this.buffer.Length);
				this.buffer = array;
			}
			char[] array2 = this.buffer;
			int num = this.writepos;
			this.writepos = num + 1;
			array2[num] = (ushort)b;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00066528 File Offset: 0x00064728
		private void AdjustBuffer()
		{
			if (this.readpos >= this.writepos)
			{
				this.readpos = (this.writepos = 0);
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00066554 File Offset: 0x00064754
		private ConsoleKeyInfo CreateKeyInfoFromInt(int n, bool alt)
		{
			char c = (char)n;
			ConsoleKey consoleKey = (ConsoleKey)n;
			bool flag = false;
			bool flag2 = false;
			if (n <= 19)
			{
				switch (n)
				{
				case 8:
				case 9:
				case 12:
				case 13:
					goto IL_00C7;
				case 10:
					consoleKey = ConsoleKey.Enter;
					goto IL_00C7;
				case 11:
					break;
				default:
					if (n == 19)
					{
						goto IL_00C7;
					}
					break;
				}
			}
			else
			{
				if (n == 27)
				{
					consoleKey = ConsoleKey.Escape;
					goto IL_00C7;
				}
				if (n == 32)
				{
					consoleKey = ConsoleKey.Spacebar;
					goto IL_00C7;
				}
				switch (n)
				{
				case 42:
					consoleKey = ConsoleKey.Multiply;
					goto IL_00C7;
				case 43:
					consoleKey = ConsoleKey.Add;
					goto IL_00C7;
				case 45:
					consoleKey = ConsoleKey.Subtract;
					goto IL_00C7;
				case 47:
					consoleKey = ConsoleKey.Divide;
					goto IL_00C7;
				}
			}
			if (n >= 1 && n <= 26)
			{
				flag2 = true;
				consoleKey = ConsoleKey.A + n - 1;
			}
			else if (n >= 97 && n <= 122)
			{
				consoleKey = (ConsoleKey)(-32) + n;
			}
			else if (n >= 65 && n <= 90)
			{
				flag = true;
			}
			else if (n < 48 || n > 57)
			{
				consoleKey = (ConsoleKey)0;
			}
			IL_00C7:
			return new ConsoleKeyInfo(c, consoleKey, flag, alt, flag2);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00066634 File Offset: 0x00064834
		private object GetKeyFromBuffer(bool cooked)
		{
			if (this.readpos >= this.writepos)
			{
				return null;
			}
			int num = (int)this.buffer[this.readpos];
			if (!cooked || !this.rootmap.StartsWith(num))
			{
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
			int num2;
			TermInfoStrings termInfoStrings = this.rootmap.Match(this.buffer, this.readpos, this.writepos - this.readpos, out num2);
			if (termInfoStrings == (TermInfoStrings)(-1))
			{
				if (this.buffer[this.readpos] != '\u001b' || this.writepos - this.readpos < 2)
				{
					return null;
				}
				this.readpos += 2;
				this.AdjustBuffer();
				if (this.buffer[this.readpos + 1] == '\u007f')
				{
					return new ConsoleKeyInfo('\b', ConsoleKey.Backspace, false, true, false);
				}
				return this.CreateKeyInfoFromInt((int)this.buffer[this.readpos + 1], true);
			}
			else
			{
				if (this.keymap[termInfoStrings] != null)
				{
					ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)this.keymap[termInfoStrings];
					this.readpos += num2;
					this.AdjustBuffer();
					return consoleKeyInfo;
				}
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006679C File Offset: 0x0006499C
		private ConsoleKeyInfo ReadKeyInternal(out bool fresh)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.InitKeys();
			object obj;
			if ((obj = this.GetKeyFromBuffer(true)) == null)
			{
				do
				{
					if (ConsoleDriver.InternalKeyAvailable(150) > 0)
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (ConsoleDriver.InternalKeyAvailable(0) > 0);
					}
					else if (this.stdin.DataAvailable())
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (this.stdin.DataAvailable());
					}
					else
					{
						if ((obj = this.GetKeyFromBuffer(false)) != null)
						{
							break;
						}
						this.AddToBuffer(this.stdin.Read());
					}
					obj = this.GetKeyFromBuffer(true);
				}
				while (obj == null);
				fresh = true;
			}
			else
			{
				fresh = false;
			}
			return (ConsoleKeyInfo)obj;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00066856 File Offset: 0x00064A56
		private bool InputPending()
		{
			return this.readpos < this.writepos || this.stdin.DataAvailable();
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00066874 File Offset: 0x00064A74
		private void QueueEcho(char c)
		{
			if (this.echobuf == null)
			{
				this.echobuf = new char[1024];
			}
			char[] array = this.echobuf;
			int num = this.echon;
			this.echon = num + 1;
			array[num] = c;
			if (this.echon == this.echobuf.Length || !this.InputPending())
			{
				this.stdout.InternalWriteChars(this.echobuf, this.echon);
				this.echon = 0;
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000668E8 File Offset: 0x00064AE8
		private void Echo(ConsoleKeyInfo key)
		{
			if (!this.IsSpecialKey(key))
			{
				this.QueueEcho(key.KeyChar);
				return;
			}
			this.EchoFlush();
			this.WriteSpecialKey(key);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0006690E File Offset: 0x00064B0E
		private void EchoFlush()
		{
			if (this.echon == 0)
			{
				return;
			}
			this.stdout.InternalWriteChars(this.echobuf, this.echon);
			this.echon = 0;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00066938 File Offset: 0x00064B38
		public int Read([In] [Out] char[] dest, int index, int count)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			object keyFromBuffer;
			while ((keyFromBuffer = this.GetKeyFromBuffer(true)) != null)
			{
				ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)keyFromBuffer;
				char c = consoleKeyInfo.KeyChar;
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					if (consoleKeyInfo.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(c);
				}
				else if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int num2 = stringBuilder2.Length;
					stringBuilder2.Length = num2 - 1;
				}
			}
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag2);
				flag = flag || flag2;
				char c = consoleKeyInfo.KeyChar;
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					if (consoleKeyInfo.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(c);
					goto IL_00E0;
				}
				if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					int num2 = stringBuilder3.Length;
					stringBuilder3.Length = num2 - 1;
					goto IL_00E0;
				}
				IL_00EA:
				if (consoleKeyInfo.Key == ConsoleKey.Enter)
				{
					break;
				}
				continue;
				IL_00E0:
				if (flag)
				{
					this.Echo(consoleKeyInfo);
					goto IL_00EA;
				}
				goto IL_00EA;
			}
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			int num3 = 0;
			while (count > 0 && num3 < stringBuilder.Length)
			{
				dest[index + num3] = stringBuilder[num3];
				num3++;
				count--;
			}
			for (int i = num3; i < stringBuilder.Length; i++)
			{
				this.AddToBuffer((int)stringBuilder[i]);
			}
			return num3;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00066AA0 File Offset: 0x00064CA0
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			bool flag;
			ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag);
			if (!intercept && flag)
			{
				this.Echo(consoleKeyInfo);
				this.EchoFlush();
			}
			return consoleKeyInfo;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00066ACC File Offset: 0x00064CCC
		public string ReadLine()
		{
			return this.ReadUntilConditionInternal(true);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x00066AD5 File Offset: 0x00064CD5
		public string ReadToEnd()
		{
			return this.ReadUntilConditionInternal(false);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00066AE0 File Offset: 0x00064CE0
		private string ReadUntilConditionInternal(bool haltOnNewLine)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.GetCursorPosition();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			char c = (char)this.control_characters[4];
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag2);
				flag = flag || flag2;
				char keyChar = consoleKeyInfo.KeyChar;
				if (keyChar == c && keyChar != '\0' && stringBuilder.Length == 0)
				{
					break;
				}
				bool flag3 = haltOnNewLine && consoleKeyInfo.Key == ConsoleKey.Enter;
				if (flag3)
				{
					goto IL_00AC;
				}
				if (consoleKeyInfo.Key != ConsoleKey.Backspace)
				{
					stringBuilder.Append(keyChar);
					goto IL_00AC;
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
					goto IL_00AC;
				}
				IL_00B6:
				if (flag3)
				{
					goto Block_10;
				}
				continue;
				IL_00AC:
				if (flag)
				{
					this.Echo(consoleKeyInfo);
					goto IL_00B6;
				}
				goto IL_00B6;
			}
			return null;
			Block_10:
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			return stringBuilder.ToString();
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x00066BC0 File Offset: 0x00064DC0
		public void ResetColor()
		{
			if (!this.inited)
			{
				this.Init();
			}
			string text = ((this.origPair != null) ? this.origPair : this.origColors);
			this.WriteConsole(text);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00066BF9 File Offset: 0x00064DF9
		public void SetBufferSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.TrySetWindowDimensions(width, height);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00066C14 File Offset: 0x00064E14
		public void SetCursorPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.CheckWindowDimensions();
			if (left < 0 || left >= this.bufferWidth)
			{
				throw new ArgumentOutOfRangeException("left", left.ToString(), "Value must be positive and below the buffer width.");
			}
			if (top < 0 || top >= this.bufferHeight)
			{
				throw new ArgumentOutOfRangeException("top", top.ToString(), "Value must be positive and below the buffer height.");
			}
			if (this.cursorAddress == null)
			{
				throw new IOException("This terminal does not suport setting the cursor position.");
			}
			this.WriteConsole(ParameterizedStrings.Evaluate(this.cursorAddress, new ParameterizedStrings.FormatParam[] { top, left }));
			this.cursorLeft = left;
			this.cursorTop = top;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00066CCF File Offset: 0x00064ECF
		public void SetWindowPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
			if (left != 0 || top != 0)
			{
				throw new ArgumentOutOfRangeException("Unix terminals only support window position (0; 0)");
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00066BF9 File Offset: 0x00064DF9
		public void SetWindowSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.TrySetWindowDimensions(width, height);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00066CF0 File Offset: 0x00064EF0
		private void CreateKeyMap()
		{
			this.keymap = new Hashtable();
			this.keymap[TermInfoStrings.KeyBackspace] = new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false);
			this.keymap[TermInfoStrings.KeyClear] = new ConsoleKeyInfo('\0', ConsoleKey.Clear, false, false, false);
			this.keymap[TermInfoStrings.KeyDown] = new ConsoleKeyInfo('\0', ConsoleKey.DownArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyF1] = new ConsoleKeyInfo('\0', ConsoleKey.F1, false, false, false);
			this.keymap[TermInfoStrings.KeyF10] = new ConsoleKeyInfo('\0', ConsoleKey.F10, false, false, false);
			this.keymap[TermInfoStrings.KeyF2] = new ConsoleKeyInfo('\0', ConsoleKey.F2, false, false, false);
			this.keymap[TermInfoStrings.KeyF3] = new ConsoleKeyInfo('\0', ConsoleKey.F3, false, false, false);
			this.keymap[TermInfoStrings.KeyF4] = new ConsoleKeyInfo('\0', ConsoleKey.F4, false, false, false);
			this.keymap[TermInfoStrings.KeyF5] = new ConsoleKeyInfo('\0', ConsoleKey.F5, false, false, false);
			this.keymap[TermInfoStrings.KeyF6] = new ConsoleKeyInfo('\0', ConsoleKey.F6, false, false, false);
			this.keymap[TermInfoStrings.KeyF7] = new ConsoleKeyInfo('\0', ConsoleKey.F7, false, false, false);
			this.keymap[TermInfoStrings.KeyF8] = new ConsoleKeyInfo('\0', ConsoleKey.F8, false, false, false);
			this.keymap[TermInfoStrings.KeyF9] = new ConsoleKeyInfo('\0', ConsoleKey.F9, false, false, false);
			this.keymap[TermInfoStrings.KeyHome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyLeft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyLl] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyNpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeyPpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyRight] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false);
			this.keymap[TermInfoStrings.KeySf] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeySr] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyUp] = new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyA1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad7, false, false, false);
			this.keymap[TermInfoStrings.KeyA3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad9, false, false, false);
			this.keymap[TermInfoStrings.KeyB2] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad5, false, false, false);
			this.keymap[TermInfoStrings.KeyC1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyC3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad3, false, false, false);
			this.keymap[TermInfoStrings.KeyBtab] = new ConsoleKeyInfo('\0', ConsoleKey.Tab, true, false, false);
			this.keymap[TermInfoStrings.KeyBeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyCopy] = new ConsoleKeyInfo('C', ConsoleKey.C, false, true, false);
			this.keymap[TermInfoStrings.KeyEnd] = new ConsoleKeyInfo('\0', ConsoleKey.End, false, false, false);
			this.keymap[TermInfoStrings.KeyEnter] = new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
			this.keymap[TermInfoStrings.KeyHelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, false, false, false);
			this.keymap[TermInfoStrings.KeyPrint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, false, false, false);
			this.keymap[TermInfoStrings.KeyUndo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, false, true, false);
			this.keymap[TermInfoStrings.KeySbeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeyScopy] = new ConsoleKeyInfo('C', ConsoleKey.C, true, true, false);
			this.keymap[TermInfoStrings.KeySdc] = new ConsoleKeyInfo('\t', ConsoleKey.Delete, true, false, false);
			this.keymap[TermInfoStrings.KeyShelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, true, false, false);
			this.keymap[TermInfoStrings.KeyShome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeySleft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySprint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, true, false, false);
			this.keymap[TermInfoStrings.KeySright] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySundo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, true, false, false);
			this.keymap[TermInfoStrings.KeyF11] = new ConsoleKeyInfo('\0', ConsoleKey.F11, false, false, false);
			this.keymap[TermInfoStrings.KeyF12] = new ConsoleKeyInfo('\0', ConsoleKey.F12, false, false, false);
			this.keymap[TermInfoStrings.KeyF13] = new ConsoleKeyInfo('\0', ConsoleKey.F13, false, false, false);
			this.keymap[TermInfoStrings.KeyF14] = new ConsoleKeyInfo('\0', ConsoleKey.F14, false, false, false);
			this.keymap[TermInfoStrings.KeyF15] = new ConsoleKeyInfo('\0', ConsoleKey.F15, false, false, false);
			this.keymap[TermInfoStrings.KeyF16] = new ConsoleKeyInfo('\0', ConsoleKey.F16, false, false, false);
			this.keymap[TermInfoStrings.KeyF17] = new ConsoleKeyInfo('\0', ConsoleKey.F17, false, false, false);
			this.keymap[TermInfoStrings.KeyF18] = new ConsoleKeyInfo('\0', ConsoleKey.F18, false, false, false);
			this.keymap[TermInfoStrings.KeyF19] = new ConsoleKeyInfo('\0', ConsoleKey.F19, false, false, false);
			this.keymap[TermInfoStrings.KeyF20] = new ConsoleKeyInfo('\0', ConsoleKey.F20, false, false, false);
			this.keymap[TermInfoStrings.KeyF21] = new ConsoleKeyInfo('\0', ConsoleKey.F21, false, false, false);
			this.keymap[TermInfoStrings.KeyF22] = new ConsoleKeyInfo('\0', ConsoleKey.F22, false, false, false);
			this.keymap[TermInfoStrings.KeyF23] = new ConsoleKeyInfo('\0', ConsoleKey.F23, false, false, false);
			this.keymap[TermInfoStrings.KeyF24] = new ConsoleKeyInfo('\0', ConsoleKey.F24, false, false, false);
			this.keymap[TermInfoStrings.KeyDc] = new ConsoleKeyInfo('\0', ConsoleKey.Delete, false, false, false);
			this.keymap[TermInfoStrings.KeyIc] = new ConsoleKeyInfo('\0', ConsoleKey.Insert, false, false, false);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0006758C File Offset: 0x0006578C
		private void InitKeys()
		{
			if (this.initKeys)
			{
				return;
			}
			this.CreateKeyMap();
			this.rootmap = new ByteMatcher();
			foreach (TermInfoStrings termInfoStrings in new TermInfoStrings[]
			{
				TermInfoStrings.KeyBackspace,
				TermInfoStrings.KeyClear,
				TermInfoStrings.KeyDown,
				TermInfoStrings.KeyF1,
				TermInfoStrings.KeyF10,
				TermInfoStrings.KeyF2,
				TermInfoStrings.KeyF3,
				TermInfoStrings.KeyF4,
				TermInfoStrings.KeyF5,
				TermInfoStrings.KeyF6,
				TermInfoStrings.KeyF7,
				TermInfoStrings.KeyF8,
				TermInfoStrings.KeyF9,
				TermInfoStrings.KeyHome,
				TermInfoStrings.KeyLeft,
				TermInfoStrings.KeyLl,
				TermInfoStrings.KeyNpage,
				TermInfoStrings.KeyPpage,
				TermInfoStrings.KeyRight,
				TermInfoStrings.KeySf,
				TermInfoStrings.KeySr,
				TermInfoStrings.KeyUp,
				TermInfoStrings.KeyA1,
				TermInfoStrings.KeyA3,
				TermInfoStrings.KeyB2,
				TermInfoStrings.KeyC1,
				TermInfoStrings.KeyC3,
				TermInfoStrings.KeyBtab,
				TermInfoStrings.KeyBeg,
				TermInfoStrings.KeyCopy,
				TermInfoStrings.KeyEnd,
				TermInfoStrings.KeyEnter,
				TermInfoStrings.KeyHelp,
				TermInfoStrings.KeyPrint,
				TermInfoStrings.KeyUndo,
				TermInfoStrings.KeySbeg,
				TermInfoStrings.KeyScopy,
				TermInfoStrings.KeySdc,
				TermInfoStrings.KeyShelp,
				TermInfoStrings.KeyShome,
				TermInfoStrings.KeySleft,
				TermInfoStrings.KeySprint,
				TermInfoStrings.KeySright,
				TermInfoStrings.KeySundo,
				TermInfoStrings.KeyF11,
				TermInfoStrings.KeyF12,
				TermInfoStrings.KeyF13,
				TermInfoStrings.KeyF14,
				TermInfoStrings.KeyF15,
				TermInfoStrings.KeyF16,
				TermInfoStrings.KeyF17,
				TermInfoStrings.KeyF18,
				TermInfoStrings.KeyF19,
				TermInfoStrings.KeyF20,
				TermInfoStrings.KeyF21,
				TermInfoStrings.KeyF22,
				TermInfoStrings.KeyF23,
				TermInfoStrings.KeyF24,
				TermInfoStrings.KeyDc,
				TermInfoStrings.KeyIc
			})
			{
				this.AddStringMapping(termInfoStrings);
			}
			this.rootmap.AddMapping(TermInfoStrings.KeyBackspace, new byte[] { this.control_characters[2] });
			this.rootmap.Sort();
			this.initKeys = true;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00067610 File Offset: 0x00065810
		private void AddStringMapping(TermInfoStrings s)
		{
			byte[] stringBytes = this.reader.GetStringBytes(s);
			if (stringBytes == null)
			{
				return;
			}
			this.rootmap.AddMapping(s, stringBytes);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0006763C File Offset: 0x0006583C
		// Note: this type is marked as 'beforefieldinit'.
		static TermInfoDriver()
		{
		}

		// Token: 0x04001687 RID: 5767
		private unsafe static int* native_terminal_size;

		// Token: 0x04001688 RID: 5768
		private static int terminal_size;

		// Token: 0x04001689 RID: 5769
		private static readonly string[] locations = new string[] { "/usr/share/terminfo", "/etc/terminfo", "/usr/lib/terminfo", "/lib/terminfo" };

		// Token: 0x0400168A RID: 5770
		private TermInfoReader reader;

		// Token: 0x0400168B RID: 5771
		private int cursorLeft;

		// Token: 0x0400168C RID: 5772
		private int cursorTop;

		// Token: 0x0400168D RID: 5773
		private string title = string.Empty;

		// Token: 0x0400168E RID: 5774
		private string titleFormat = string.Empty;

		// Token: 0x0400168F RID: 5775
		private bool cursorVisible = true;

		// Token: 0x04001690 RID: 5776
		private string csrVisible;

		// Token: 0x04001691 RID: 5777
		private string csrInvisible;

		// Token: 0x04001692 RID: 5778
		private string clear;

		// Token: 0x04001693 RID: 5779
		private string bell;

		// Token: 0x04001694 RID: 5780
		private string term;

		// Token: 0x04001695 RID: 5781
		private StreamReader stdin;

		// Token: 0x04001696 RID: 5782
		private CStreamWriter stdout;

		// Token: 0x04001697 RID: 5783
		private int windowWidth;

		// Token: 0x04001698 RID: 5784
		private int windowHeight;

		// Token: 0x04001699 RID: 5785
		private int bufferHeight;

		// Token: 0x0400169A RID: 5786
		private int bufferWidth;

		// Token: 0x0400169B RID: 5787
		private char[] buffer;

		// Token: 0x0400169C RID: 5788
		private int readpos;

		// Token: 0x0400169D RID: 5789
		private int writepos;

		// Token: 0x0400169E RID: 5790
		private string keypadXmit;

		// Token: 0x0400169F RID: 5791
		private string keypadLocal;

		// Token: 0x040016A0 RID: 5792
		private bool controlCAsInput;

		// Token: 0x040016A1 RID: 5793
		private bool inited;

		// Token: 0x040016A2 RID: 5794
		private object initLock = new object();

		// Token: 0x040016A3 RID: 5795
		private bool initKeys;

		// Token: 0x040016A4 RID: 5796
		private string origPair;

		// Token: 0x040016A5 RID: 5797
		private string origColors;

		// Token: 0x040016A6 RID: 5798
		private string cursorAddress;

		// Token: 0x040016A7 RID: 5799
		private ConsoleColor fgcolor = ConsoleColor.White;

		// Token: 0x040016A8 RID: 5800
		private ConsoleColor bgcolor;

		// Token: 0x040016A9 RID: 5801
		private string setfgcolor;

		// Token: 0x040016AA RID: 5802
		private string setbgcolor;

		// Token: 0x040016AB RID: 5803
		private int maxColors;

		// Token: 0x040016AC RID: 5804
		private bool noGetPosition;

		// Token: 0x040016AD RID: 5805
		private Hashtable keymap;

		// Token: 0x040016AE RID: 5806
		private ByteMatcher rootmap;

		// Token: 0x040016AF RID: 5807
		private int rl_startx = -1;

		// Token: 0x040016B0 RID: 5808
		private int rl_starty = -1;

		// Token: 0x040016B1 RID: 5809
		private byte[] control_characters;

		// Token: 0x040016B2 RID: 5810
		private static readonly int[] _consoleColorToAnsiCode = new int[]
		{
			0, 4, 2, 6, 1, 5, 3, 7, 8, 12,
			10, 14, 9, 13, 11, 15
		};

		// Token: 0x040016B3 RID: 5811
		private char[] echobuf;

		// Token: 0x040016B4 RID: 5812
		private int echon;
	}
}
