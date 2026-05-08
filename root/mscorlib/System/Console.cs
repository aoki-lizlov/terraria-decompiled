using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000200 RID: 512
	public static class Console
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x0005EC18 File Offset: 0x0005CE18
		static Console()
		{
			if (Environment.IsRunningOnWindows)
			{
				try
				{
					Console.inputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetInputCodePage());
					Console.outputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetOutputCodePage());
					goto IL_007D;
				}
				catch
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
					goto IL_007D;
				}
			}
			int num = 0;
			EncodingHelper.InternalCodePage(ref num);
			if (num != -1 && ((num & 268435455) == 3 || (num & 268435456) != 0))
			{
				Console.inputEncoding = (Console.outputEncoding = EncodingHelper.UTF8Unmarked);
			}
			else
			{
				Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
			}
			IL_007D:
			Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005ECC4 File Offset: 0x0005CEC4
		private static void SetupStreams(Encoding inputEncoding, Encoding outputEncoding)
		{
			if (!Environment.IsRunningOnWindows && ConsoleDriver.IsConsole)
			{
				Console.stdin = new CStreamReader(Console.OpenStandardInput(0), inputEncoding);
				Console.stdout = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardOutput(0), outputEncoding, true)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardError(0), outputEncoding, true)
				{
					AutoFlush = true
				});
			}
			else
			{
				Console.stdin = TextReader.Synchronized(new UnexceptionalStreamReader(Console.OpenStandardInput(0), inputEncoding));
				Console.stdout = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardOutput(0), outputEncoding)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardError(0), outputEncoding)
				{
					AutoFlush = true
				});
			}
			GC.SuppressFinalize(Console.stdout);
			GC.SuppressFinalize(Console.stderr);
			GC.SuppressFinalize(Console.stdin);
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005ED9C File Offset: 0x0005CF9C
		public static TextWriter Error
		{
			get
			{
				return Console.stderr;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005EDA3 File Offset: 0x0005CFA3
		public static TextWriter Out
		{
			get
			{
				return Console.stdout;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005EDAA File Offset: 0x0005CFAA
		public static TextReader In
		{
			get
			{
				return Console.stdin;
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0005EDB4 File Offset: 0x0005CFB4
		private static Stream Open(IntPtr handle, FileAccess access, int bufferSize)
		{
			Stream stream;
			try
			{
				FileStream fileStream = new FileStream(handle, access, false, bufferSize, false, true);
				GC.SuppressFinalize(fileStream);
				stream = fileStream;
			}
			catch (IOException)
			{
				stream = Stream.Null;
			}
			return stream;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError(0);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005EDF8 File Offset: 0x0005CFF8
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleError, FileAccess.Write, bufferSize);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0005EE06 File Offset: 0x0005D006
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput(0);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005EE0E File Offset: 0x0005D00E
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleInput, FileAccess.Read, bufferSize);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005EE1C File Offset: 0x0005D01C
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput(0);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0005EE24 File Offset: 0x0005D024
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleOutput, FileAccess.Write, bufferSize);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0005EE32 File Offset: 0x0005D032
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			Console.stderr = TextWriter.Synchronized(newError);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0005EE4D File Offset: 0x0005D04D
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetIn(TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException("newIn");
			}
			Console.stdin = TextReader.Synchronized(newIn);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0005EE68 File Offset: 0x0005D068
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			Console.stdout = TextWriter.Synchronized(newOut);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0005EE83 File Offset: 0x0005D083
		public static void Write(bool value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0005EE90 File Offset: 0x0005D090
		public static void Write(char value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0005EE9D File Offset: 0x0005D09D
		public static void Write(char[] buffer)
		{
			Console.stdout.Write(buffer);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0005EEAA File Offset: 0x0005D0AA
		public static void Write(decimal value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0005EEB7 File Offset: 0x0005D0B7
		public static void Write(double value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0005EEC4 File Offset: 0x0005D0C4
		public static void Write(int value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0005EED1 File Offset: 0x0005D0D1
		public static void Write(long value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0005EEDE File Offset: 0x0005D0DE
		public static void Write(object value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0005EEEB File Offset: 0x0005D0EB
		public static void Write(float value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		public static void Write(string value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0005EF05 File Offset: 0x0005D105
		[CLSCompliant(false)]
		public static void Write(uint value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0005EF12 File Offset: 0x0005D112
		[CLSCompliant(false)]
		public static void Write(ulong value)
		{
			Console.stdout.Write(value);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0005EF1F File Offset: 0x0005D11F
		public static void Write(string format, object arg0)
		{
			Console.stdout.Write(format, arg0);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0005EF2D File Offset: 0x0005D12D
		public static void Write(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.stdout.Write(format);
				return;
			}
			Console.stdout.Write(format, arg);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0005EF4A File Offset: 0x0005D14A
		public static void Write(char[] buffer, int index, int count)
		{
			Console.stdout.Write(buffer, index, count);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0005EF59 File Offset: 0x0005D159
		public static void Write(string format, object arg0, object arg1)
		{
			Console.stdout.Write(format, arg0, arg1);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0005EF68 File Offset: 0x0005D168
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.Write(format, arg0, arg1, arg2);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0005EF78 File Offset: 0x0005D178
		[CLSCompliant(false)]
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.Write(string.Format(format, array));
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0005EFE2 File Offset: 0x0005D1E2
		public static void WriteLine()
		{
			Console.stdout.WriteLine();
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0005EFEE File Offset: 0x0005D1EE
		public static void WriteLine(bool value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0005EFFB File Offset: 0x0005D1FB
		public static void WriteLine(char value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005F008 File Offset: 0x0005D208
		public static void WriteLine(char[] buffer)
		{
			Console.stdout.WriteLine(buffer);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005F015 File Offset: 0x0005D215
		public static void WriteLine(decimal value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005F022 File Offset: 0x0005D222
		public static void WriteLine(double value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005F02F File Offset: 0x0005D22F
		public static void WriteLine(int value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0005F03C File Offset: 0x0005D23C
		public static void WriteLine(long value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005F049 File Offset: 0x0005D249
		public static void WriteLine(object value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005F056 File Offset: 0x0005D256
		public static void WriteLine(float value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005F063 File Offset: 0x0005D263
		public static void WriteLine(string value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0005F070 File Offset: 0x0005D270
		[CLSCompliant(false)]
		public static void WriteLine(uint value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x0005F07D File Offset: 0x0005D27D
		[CLSCompliant(false)]
		public static void WriteLine(ulong value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0005F08A File Offset: 0x0005D28A
		public static void WriteLine(string format, object arg0)
		{
			Console.stdout.WriteLine(format, arg0);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0005F098 File Offset: 0x0005D298
		public static void WriteLine(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.stdout.WriteLine(format);
				return;
			}
			Console.stdout.WriteLine(format, arg);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0005F0B5 File Offset: 0x0005D2B5
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.stdout.WriteLine(buffer, index, count);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.stdout.WriteLine(format, arg0, arg1);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0005F0D3 File Offset: 0x0005D2D3
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.WriteLine(format, arg0, arg1, arg2);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		[CLSCompliant(false)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.WriteLine(string.Format(format, array));
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0005F14E File Offset: 0x0005D34E
		public static int Read()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.Read();
			}
			return Console.stdin.Read();
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0005F173 File Offset: 0x0005D373
		public static string ReadLine()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.ReadLine();
			}
			return Console.stdin.ReadLine();
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0005F198 File Offset: 0x0005D398
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0005F19F File Offset: 0x0005D39F
		public static Encoding InputEncoding
		{
			get
			{
				return Console.inputEncoding;
			}
			set
			{
				Console.inputEncoding = value;
				Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0005F1B6 File Offset: 0x0005D3B6
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0005F1BD File Offset: 0x0005D3BD
		public static Encoding OutputEncoding
		{
			get
			{
				return Console.outputEncoding;
			}
			set
			{
				Console.outputEncoding = value;
				Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005F1D4 File Offset: 0x0005D3D4
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0005F1DB File Offset: 0x0005D3DB
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.BackgroundColor;
			}
			set
			{
				ConsoleDriver.BackgroundColor = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0005F1E3 File Offset: 0x0005D3E3
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0005F1EA File Offset: 0x0005D3EA
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.BufferHeight;
			}
			[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
			set
			{
				ConsoleDriver.BufferHeight = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0005F1F2 File Offset: 0x0005D3F2
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0005F1F9 File Offset: 0x0005D3F9
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.BufferWidth;
			}
			[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
			set
			{
				ConsoleDriver.BufferWidth = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005F201 File Offset: 0x0005D401
		[MonoLimitation("Implemented only on Windows")]
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.CapsLock;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0005F208 File Offset: 0x0005D408
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0005F20F File Offset: 0x0005D40F
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.CursorLeft;
			}
			set
			{
				ConsoleDriver.CursorLeft = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0005F217 File Offset: 0x0005D417
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0005F21E File Offset: 0x0005D41E
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.CursorTop;
			}
			set
			{
				ConsoleDriver.CursorTop = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005F226 File Offset: 0x0005D426
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0005F22D File Offset: 0x0005D42D
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.CursorSize;
			}
			set
			{
				ConsoleDriver.CursorSize = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0005F235 File Offset: 0x0005D435
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0005F23C File Offset: 0x0005D43C
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.CursorVisible;
			}
			set
			{
				ConsoleDriver.CursorVisible = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0005F244 File Offset: 0x0005D444
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0005F24B File Offset: 0x0005D44B
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.ForegroundColor;
			}
			set
			{
				ConsoleDriver.ForegroundColor = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0005F253 File Offset: 0x0005D453
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.KeyAvailable;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005F25A File Offset: 0x0005D45A
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.LargestWindowHeight;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0005F261 File Offset: 0x0005D461
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.LargestWindowWidth;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0005F268 File Offset: 0x0005D468
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.NumberLock;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0005F26F File Offset: 0x0005D46F
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0005F276 File Offset: 0x0005D476
		public static string Title
		{
			get
			{
				return ConsoleDriver.Title;
			}
			set
			{
				ConsoleDriver.Title = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005F27E File Offset: 0x0005D47E
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0005F285 File Offset: 0x0005D485
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0005F28D File Offset: 0x0005D48D
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0005F294 File Offset: 0x0005D494
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.WindowHeight;
			}
			[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
			set
			{
				ConsoleDriver.WindowHeight = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0005F29C File Offset: 0x0005D49C
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0005F2A3 File Offset: 0x0005D4A3
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.WindowLeft;
			}
			[MonoLimitation("Works only on Windows")]
			set
			{
				ConsoleDriver.WindowLeft = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0005F2AB File Offset: 0x0005D4AB
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0005F2B2 File Offset: 0x0005D4B2
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.WindowTop;
			}
			[MonoLimitation("Works only on Windows")]
			set
			{
				ConsoleDriver.WindowTop = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0005F2BA File Offset: 0x0005D4BA
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0005F2C1 File Offset: 0x0005D4C1
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.WindowWidth;
			}
			[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
			set
			{
				ConsoleDriver.WindowWidth = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0005F2C9 File Offset: 0x0005D4C9
		public static bool IsErrorRedirected
		{
			get
			{
				return ConsoleDriver.IsErrorRedirected;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0005F2D0 File Offset: 0x0005D4D0
		public static bool IsOutputRedirected
		{
			get
			{
				return ConsoleDriver.IsOutputRedirected;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0005F2D7 File Offset: 0x0005D4D7
		public static bool IsInputRedirected
		{
			get
			{
				return ConsoleDriver.IsInputRedirected;
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0005F2DE File Offset: 0x0005D4DE
		public static void Beep()
		{
			Console.Beep(1000, 500);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0005F2EF File Offset: 0x0005D4EF
		public static void Beep(int frequency, int duration)
		{
			if (frequency < 37 || frequency > 32767)
			{
				throw new ArgumentOutOfRangeException("frequency");
			}
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException("duration");
			}
			ConsoleDriver.Beep(frequency, duration);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0005F31F File Offset: 0x0005D51F
		public static void Clear()
		{
			ConsoleDriver.Clear();
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0005F326 File Offset: 0x0005D526
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0005F338 File Offset: 0x0005D538
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0005F358 File Offset: 0x0005D558
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x0005F360 File Offset: 0x0005D560
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.ReadKey(intercept);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0005F368 File Offset: 0x0005D568
		public static void ResetColor()
		{
			ConsoleDriver.ResetColor();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0005F36F File Offset: 0x0005D56F
		[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.SetBufferSize(width, height);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005F378 File Offset: 0x0005D578
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.SetCursorPosition(left, top);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0005F381 File Offset: 0x0005D581
		[MonoLimitation("Works only on Windows")]
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.SetWindowPosition(left, top);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x0005F38A File Offset: 0x0005D58A
		[MonoLimitation("Works only on Windows, or with some Xterm-based terminals")]
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.SetWindowSize(width, height);
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600191E RID: 6430 RVA: 0x0005F393 File Offset: 0x0005D593
		// (remove) Token: 0x0600191F RID: 6431 RVA: 0x0005F3C9 File Offset: 0x0005D5C9
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			add
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Combine(Console.cancel_event, value);
				if (Environment.IsRunningOnWindows && !Console.WindowsConsole.ctrlHandlerAdded)
				{
					Console.WindowsConsole.AddCtrlHandler();
				}
			}
			remove
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Remove(Console.cancel_event, value);
				if (Console.cancel_event == null && Environment.IsRunningOnWindows && Console.WindowsConsole.ctrlHandlerAdded)
				{
					Console.WindowsConsole.RemoveCtrlHandler();
				}
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0005F406 File Offset: 0x0005D606
		private static void DoConsoleCancelEventInBackground()
		{
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
			{
				Console.DoConsoleCancelEvent();
			}, null);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x0005F430 File Offset: 0x0005D630
		private static void DoConsoleCancelEvent()
		{
			bool flag = true;
			if (Console.cancel_event != null)
			{
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(ConsoleSpecialKey.ControlC);
				foreach (ConsoleCancelEventHandler consoleCancelEventHandler in Console.cancel_event.GetInvocationList())
				{
					try
					{
						consoleCancelEventHandler(null, consoleCancelEventArgs);
					}
					catch
					{
					}
				}
				flag = !consoleCancelEventArgs.Cancel;
			}
			if (flag)
			{
				Environment.Exit(58);
			}
		}

		// Token: 0x040015C3 RID: 5571
		internal static TextWriter stdout;

		// Token: 0x040015C4 RID: 5572
		private static TextWriter stderr;

		// Token: 0x040015C5 RID: 5573
		private static TextReader stdin;

		// Token: 0x040015C6 RID: 5574
		private static Encoding inputEncoding;

		// Token: 0x040015C7 RID: 5575
		private static Encoding outputEncoding;

		// Token: 0x040015C8 RID: 5576
		private static ConsoleCancelEventHandler cancel_event;

		// Token: 0x02000201 RID: 513
		private class WindowsConsole
		{
			// Token: 0x06001922 RID: 6434
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleCP();

			// Token: 0x06001923 RID: 6435
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleOutputCP();

			// Token: 0x06001924 RID: 6436
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern bool SetConsoleCtrlHandler(Console.WindowsConsole.WindowsCancelHandler handler, bool addHandler);

			// Token: 0x06001925 RID: 6437 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
			private static bool DoWindowsConsoleCancelEvent(int keyCode)
			{
				if (keyCode == 0)
				{
					Console.DoConsoleCancelEvent();
				}
				return keyCode == 0;
			}

			// Token: 0x06001926 RID: 6438 RVA: 0x0005F4B2 File Offset: 0x0005D6B2
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetInputCodePage()
			{
				return Console.WindowsConsole.GetConsoleCP();
			}

			// Token: 0x06001927 RID: 6439 RVA: 0x0005F4B9 File Offset: 0x0005D6B9
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetOutputCodePage()
			{
				return Console.WindowsConsole.GetConsoleOutputCP();
			}

			// Token: 0x06001928 RID: 6440 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
			public static void AddCtrlHandler()
			{
				Console.WindowsConsole.SetConsoleCtrlHandler(Console.WindowsConsole.cancelHandler, true);
				Console.WindowsConsole.ctrlHandlerAdded = true;
			}

			// Token: 0x06001929 RID: 6441 RVA: 0x0005F4D4 File Offset: 0x0005D6D4
			public static void RemoveCtrlHandler()
			{
				Console.WindowsConsole.SetConsoleCtrlHandler(Console.WindowsConsole.cancelHandler, false);
				Console.WindowsConsole.ctrlHandlerAdded = false;
			}

			// Token: 0x0600192A RID: 6442 RVA: 0x000025BE File Offset: 0x000007BE
			public WindowsConsole()
			{
			}

			// Token: 0x0600192B RID: 6443 RVA: 0x0005F4E8 File Offset: 0x0005D6E8
			// Note: this type is marked as 'beforefieldinit'.
			static WindowsConsole()
			{
			}

			// Token: 0x040015C9 RID: 5577
			public static bool ctrlHandlerAdded = false;

			// Token: 0x040015CA RID: 5578
			private static Console.WindowsConsole.WindowsCancelHandler cancelHandler = new Console.WindowsConsole.WindowsCancelHandler(Console.WindowsConsole.DoWindowsConsoleCancelEvent);

			// Token: 0x02000202 RID: 514
			// (Invoke) Token: 0x0600192D RID: 6445
			private delegate bool WindowsCancelHandler(int keyCode);
		}

		// Token: 0x02000203 RID: 515
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001930 RID: 6448 RVA: 0x0005F501 File Offset: 0x0005D701
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001931 RID: 6449 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06001932 RID: 6450 RVA: 0x0005F50D File Offset: 0x0005D70D
			internal void <DoConsoleCancelEventInBackground>b__143_0(object _)
			{
				Console.DoConsoleCancelEvent();
			}

			// Token: 0x040015CB RID: 5579
			public static readonly Console.<>c <>9 = new Console.<>c();

			// Token: 0x040015CC RID: 5580
			public static WaitCallback <>9__143_0;
		}
	}
}
