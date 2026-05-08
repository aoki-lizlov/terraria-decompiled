using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReLogic.OS.Windows
{
	// Token: 0x0200006B RID: 107
	internal class WindowsMessageHook : IDisposable, IMessageFilter
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00009FA0 File Offset: 0x000081A0
		public WindowsMessageHook(IntPtr windowHandle)
		{
			this._windowHandle = windowHandle;
			Application.AddMessageFilter(this);
			this._wndProc = new WindowsMessageHook.WndProcCallback(this.WndProc);
			this._previousWndProc = (IntPtr)NativeMethods.SetWindowLong(this._windowHandle, -4, (int)Marshal.GetFunctionPointerForDelegate(this._wndProc));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A01B File Offset: 0x0000821B
		public void AddMessageFilter(IMessageFilter filter)
		{
			this._filters.Add(filter);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A029 File Offset: 0x00008229
		public void RemoveMessageFilter(IMessageFilter filter)
		{
			this._filters.Remove(filter);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A038 File Offset: 0x00008238
		private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
		{
			Message message = Message.Create(hWnd, msg, wParam, lParam);
			if (this.InternalWndProc(ref message))
			{
				return message.Result;
			}
			return NativeMethods.CallWindowProc(this._previousWndProc, message.HWnd, message.Msg, message.WParam, message.LParam);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A089 File Offset: 0x00008289
		public bool PreFilterMessage(ref Message message)
		{
			return message.Msg != 258 && this.InternalWndProc(ref message);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A0A4 File Offset: 0x000082A4
		private bool InternalWndProc(ref Message message)
		{
			using (List<IMessageFilter>.Enumerator enumerator = this._filters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.PreFilterMessage(ref message))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A100 File Offset: 0x00008300
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				Application.RemoveMessageFilter(this);
				NativeMethods.SetWindowLong(this._windowHandle, -4, (int)this._previousWndProc);
				this.disposedValue = true;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A134 File Offset: 0x00008334
		~WindowsMessageHook()
		{
			this.Dispose(false);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A164 File Offset: 0x00008364
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400031E RID: 798
		private const int GWL_WNDPROC = -4;

		// Token: 0x0400031F RID: 799
		private IntPtr _windowHandle = IntPtr.Zero;

		// Token: 0x04000320 RID: 800
		private IntPtr _previousWndProc = IntPtr.Zero;

		// Token: 0x04000321 RID: 801
		private WindowsMessageHook.WndProcCallback _wndProc;

		// Token: 0x04000322 RID: 802
		private List<IMessageFilter> _filters = new List<IMessageFilter>();

		// Token: 0x04000323 RID: 803
		private bool disposedValue;

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x06000461 RID: 1121
		private delegate IntPtr WndProcCallback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
	}
}
