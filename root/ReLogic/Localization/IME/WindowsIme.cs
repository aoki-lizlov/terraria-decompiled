using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ReLogic.Localization.IME.Windows;
using ReLogic.OS.Windows;

namespace ReLogic.Localization.IME
{
	// Token: 0x0200007D RID: 125
	internal class WindowsIme : PlatformIme, IMessageFilter
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000AC03 File Offset: 0x00008E03
		public override string CompositionString
		{
			get
			{
				return Marshal.PtrToStringUni(ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetCompositionString()).ToString();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000AC14 File Offset: 0x00008E14
		public override bool IsCandidateListVisible
		{
			get
			{
				return ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_IsCandidateListVisible();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public override uint? SelectedCandidate
		{
			get
			{
				if (!this._visibleUnfocusedCandidateWindow)
				{
					return new uint?(ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetCandidateSelection());
				}
				return default(uint?);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000AC45 File Offset: 0x00008E45
		public override uint CandidateCount
		{
			get
			{
				return ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetCandidatePageSize();
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public WindowsIme(WindowsMessageHook wndProcHook, IntPtr windowHandle)
		{
			this._wndProcHook = wndProcHook;
			this._windowHandle = windowHandle;
			this._isFocused = ReLogic.OS.Windows.NativeMethods.GetForegroundWindow() == this._windowHandle;
			this._wndProcHook.AddMessageFilter(this);
			ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Initialize(this._windowHandle, false);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000AC9C File Offset: 0x00008E9C
		protected override void OnEnable()
		{
			if (this._isFocused)
			{
				ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Enable(true);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000ACAC File Offset: 0x00008EAC
		protected override void OnDisable()
		{
			ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_FinalizeString(false);
			ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Enable(false);
			this._visibleUnfocusedCandidateWindow = false;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		public override string GetCandidate(uint index)
		{
			return Marshal.PtrToStringUni(ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetCandidate(index));
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public bool PreFilterMessage(ref Message message)
		{
			if (message.Msg == 8)
			{
				ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Enable(false);
				this._isFocused = false;
				this._visibleUnfocusedCandidateWindow = false;
				return true;
			}
			if (message.Msg == 7)
			{
				if (base.IsEnabled)
				{
					ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Enable(true);
				}
				this._isFocused = true;
				return true;
			}
			if (!base.IsEnabled)
			{
				return false;
			}
			bool flag = false;
			IntPtr lparam = message.LParam;
			ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_ProcessMessage(message.HWnd, message.Msg, message.WParam, ref lparam, ref flag);
			message.LParam = lparam;
			if (flag)
			{
				return true;
			}
			int msg = message.Msg;
			if (msg <= 258)
			{
				if (msg == 81)
				{
					return true;
				}
				if (msg != 256)
				{
					if (msg == 258)
					{
						base.OnKeyPress((char)message.WParam.ToInt32());
					}
				}
				else
				{
					bool flag2 = false;
					uint? num = default(uint?);
					if ((int)message.WParam == 229 && ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetPrimaryLanguage() == 17)
					{
						if (string.IsNullOrEmpty(this.CompositionString))
						{
							this._visibleUnfocusedCandidateWindow = false;
						}
						else if (!this._visibleUnfocusedCandidateWindow)
						{
							uint num2 = ReLogic.Localization.IME.Windows.NativeMethods.ImmGetVirtualKey(message.HWnd);
							if (num2 == 8U || num2 == 27U)
							{
								this._visibleUnfocusedCandidateWindow = true;
							}
							if (num2 == 9U)
							{
								num = this.SelectedCandidate;
							}
						}
						else
						{
							uint num3 = ReLogic.Localization.IME.Windows.NativeMethods.ImmGetVirtualKey(message.HWnd);
							if (num3 == 40U || num3 == 9U)
							{
								this._visibleUnfocusedCandidateWindow = false;
								uint? num4 = this.SelectedCandidate;
								uint num5 = 0U;
								flag2 = (num4.GetValueOrDefault() == num5) & (num4 != null);
							}
						}
					}
					if (!ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_ShouldIgnoreHotKey(ref message))
					{
						ReLogic.OS.Windows.NativeMethods.TranslateMessage(ref message);
					}
					if (flag2)
					{
						uint? num4 = this.SelectedCandidate;
						uint num5 = 1U;
						if ((num4.GetValueOrDefault() == num5) & (num4 != null))
						{
							ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_SetCandidateSelection(0U);
						}
					}
					if (num != null)
					{
						uint? num4 = this.SelectedCandidate;
						uint? num6 = num;
						if ((num4.GetValueOrDefault() == num6.GetValueOrDefault()) & (num4 != null == (num6 != null)))
						{
							ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_SetCandidateSelection(0U);
						}
					}
				}
			}
			else
			{
				if (msg == 269)
				{
					return true;
				}
				if (msg != 641)
				{
					if (msg == 642)
					{
						int num7 = message.WParam.ToInt32();
						if (num7 == 5 && ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_GetPrimaryLanguage() == 17)
						{
							this._visibleUnfocusedCandidateWindow = true;
						}
						else if (num7 == 4)
						{
							this._visibleUnfocusedCandidateWindow = false;
						}
						return true;
					}
				}
				else
				{
					message.LParam = IntPtr.Zero;
				}
			}
			return false;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000AF33 File Offset: 0x00009133
		protected override void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (base.IsEnabled)
				{
					base.Disable();
				}
				this._wndProcHook.RemoveMessageFilter(this);
				ReLogic.Localization.IME.Windows.NativeMethods.ImeUi_Uninitialize();
				this._disposedValue = true;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AF68 File Offset: 0x00009168
		~WindowsIme()
		{
			this.Dispose(false);
		}

		// Token: 0x0400032F RID: 815
		private IntPtr _windowHandle;

		// Token: 0x04000330 RID: 816
		private bool _isFocused;

		// Token: 0x04000331 RID: 817
		private bool _visibleUnfocusedCandidateWindow;

		// Token: 0x04000332 RID: 818
		private WindowsMessageHook _wndProcHook;

		// Token: 0x04000333 RID: 819
		private bool _disposedValue;
	}
}
