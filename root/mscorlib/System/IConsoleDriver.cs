using System;

namespace System
{
	// Token: 0x0200020E RID: 526
	internal interface IConsoleDriver
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060019A6 RID: 6566
		// (set) Token: 0x060019A7 RID: 6567
		ConsoleColor BackgroundColor { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060019A8 RID: 6568
		// (set) Token: 0x060019A9 RID: 6569
		int BufferHeight { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060019AA RID: 6570
		// (set) Token: 0x060019AB RID: 6571
		int BufferWidth { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060019AC RID: 6572
		bool CapsLock { get; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060019AD RID: 6573
		// (set) Token: 0x060019AE RID: 6574
		int CursorLeft { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060019AF RID: 6575
		// (set) Token: 0x060019B0 RID: 6576
		int CursorSize { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060019B1 RID: 6577
		// (set) Token: 0x060019B2 RID: 6578
		int CursorTop { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060019B3 RID: 6579
		// (set) Token: 0x060019B4 RID: 6580
		bool CursorVisible { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060019B5 RID: 6581
		// (set) Token: 0x060019B6 RID: 6582
		ConsoleColor ForegroundColor { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060019B7 RID: 6583
		bool KeyAvailable { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060019B8 RID: 6584
		bool Initialized { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060019B9 RID: 6585
		int LargestWindowHeight { get; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060019BA RID: 6586
		int LargestWindowWidth { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060019BB RID: 6587
		bool NumberLock { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060019BC RID: 6588
		// (set) Token: 0x060019BD RID: 6589
		string Title { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060019BE RID: 6590
		// (set) Token: 0x060019BF RID: 6591
		bool TreatControlCAsInput { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060019C0 RID: 6592
		// (set) Token: 0x060019C1 RID: 6593
		int WindowHeight { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060019C2 RID: 6594
		// (set) Token: 0x060019C3 RID: 6595
		int WindowLeft { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060019C4 RID: 6596
		// (set) Token: 0x060019C5 RID: 6597
		int WindowTop { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060019C6 RID: 6598
		// (set) Token: 0x060019C7 RID: 6599
		int WindowWidth { get; set; }

		// Token: 0x060019C8 RID: 6600
		void Init();

		// Token: 0x060019C9 RID: 6601
		void Beep(int frequency, int duration);

		// Token: 0x060019CA RID: 6602
		void Clear();

		// Token: 0x060019CB RID: 6603
		void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);

		// Token: 0x060019CC RID: 6604
		ConsoleKeyInfo ReadKey(bool intercept);

		// Token: 0x060019CD RID: 6605
		void ResetColor();

		// Token: 0x060019CE RID: 6606
		void SetBufferSize(int width, int height);

		// Token: 0x060019CF RID: 6607
		void SetCursorPosition(int left, int top);

		// Token: 0x060019D0 RID: 6608
		void SetWindowPosition(int left, int top);

		// Token: 0x060019D1 RID: 6609
		void SetWindowSize(int width, int height);

		// Token: 0x060019D2 RID: 6610
		string ReadLine();
	}
}
