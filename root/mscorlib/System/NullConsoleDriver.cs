using System;

namespace System
{
	// Token: 0x0200021A RID: 538
	internal class NullConsoleDriver : IConsoleDriver
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00004088 File Offset: 0x00002288
		public ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x00004088 File Offset: 0x00002288
		public int BufferHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x00004088 File Offset: 0x00002288
		public int BufferWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0000408A File Offset: 0x0000228A
		public bool CapsLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00004088 File Offset: 0x00002288
		public int CursorLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00004088 File Offset: 0x00002288
		public int CursorSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A43 RID: 6723 RVA: 0x00004088 File Offset: 0x00002288
		public int CursorTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x00004088 File Offset: 0x00002288
		public bool CursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x00004088 File Offset: 0x00002288
		public ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0000408A File Offset: 0x0000228A
		public bool KeyAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool Initialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x0000408A File Offset: 0x0000228A
		public int LargestWindowHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x0000408A File Offset: 0x0000228A
		public int LargestWindowWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x0000408A File Offset: 0x0000228A
		public bool NumberLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00061F15 File Offset: 0x00060115
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x00004088 File Offset: 0x00002288
		public string Title
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x00004088 File Offset: 0x00002288
		public bool TreatControlCAsInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A52 RID: 6738 RVA: 0x00004088 File Offset: 0x00002288
		public int WindowHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x00004088 File Offset: 0x00002288
		public int WindowLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A56 RID: 6742 RVA: 0x00004088 File Offset: 0x00002288
		public int WindowTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0000408A File Offset: 0x0000228A
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x00004088 File Offset: 0x00002288
		public int WindowWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00004088 File Offset: 0x00002288
		public void Beep(int frequency, int duration)
		{
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00004088 File Offset: 0x00002288
		public void Clear()
		{
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00004088 File Offset: 0x00002288
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00004088 File Offset: 0x00002288
		public void Init()
		{
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public string ReadLine()
		{
			return null;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00061F1C File Offset: 0x0006011C
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return NullConsoleDriver.EmptyConsoleKeyInfo;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00004088 File Offset: 0x00002288
		public void ResetColor()
		{
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00004088 File Offset: 0x00002288
		public void SetBufferSize(int width, int height)
		{
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00004088 File Offset: 0x00002288
		public void SetCursorPosition(int left, int top)
		{
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00004088 File Offset: 0x00002288
		public void SetWindowPosition(int left, int top)
		{
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00004088 File Offset: 0x00002288
		public void SetWindowSize(int width, int height)
		{
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000025BE File Offset: 0x000007BE
		public NullConsoleDriver()
		{
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00061F23 File Offset: 0x00060123
		// Note: this type is marked as 'beforefieldinit'.
		static NullConsoleDriver()
		{
		}

		// Token: 0x04001618 RID: 5656
		private static readonly ConsoleKeyInfo EmptyConsoleKeyInfo = new ConsoleKeyInfo('\0', (ConsoleKey)0, false, false, false);
	}
}
