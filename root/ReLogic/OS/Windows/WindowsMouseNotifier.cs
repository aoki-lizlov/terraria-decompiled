using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ReLogic.OS.Windows
{
	// Token: 0x0200006C RID: 108
	internal class WindowsMouseNotifier : IMessageFilter, IMouseNotifier
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000259 RID: 601 RVA: 0x0000A174 File Offset: 0x00008374
		// (remove) Token: 0x0600025A RID: 602 RVA: 0x0000A1AC File Offset: 0x000083AC
		internal event Action<bool> MouseStateChanged
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.MouseStateChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.MouseStateChanged, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.MouseStateChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.MouseStateChanged, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A1E1 File Offset: 0x000083E1
		internal WindowsMouseNotifier(WindowsMessageHook wndProc)
		{
			wndProc.AddMessageFilter(this);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A1F7 File Offset: 0x000083F7
		public void AddMouseHandler(Action<bool> action)
		{
			this.MouseStateChanged += action;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A200 File Offset: 0x00008400
		public void RemoveMouseHandler(Action<bool> action)
		{
			this.MouseStateChanged -= action;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A20C File Offset: 0x0000840C
		public void ForceCursorHidden()
		{
			for (int i = NativeMethods.ShowCursor(false); i > 0; i = NativeMethods.ShowCursor(false))
			{
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A230 File Offset: 0x00008430
		private bool HasMouseAttached()
		{
			uint num = 0U;
			NativeMethods.GetRawInputDeviceList(null, ref num, (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICELIST)));
			NativeMethods.RAWINPUTDEVICELIST[] array = new NativeMethods.RAWINPUTDEVICELIST[num];
			NativeMethods.GetRawInputDeviceList(array, ref num, (uint)Marshal.SizeOf(typeof(NativeMethods.RAWINPUTDEVICELIST)));
			bool flag = false;
			NativeMethods.RAWINPUTDEVICELIST[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i].dwType == 0U)
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A29C File Offset: 0x0000849C
		public bool PreFilterMessage(ref Message message)
		{
			if (message.Msg == 537)
			{
				int num = (int)message.WParam;
				if (num == 7)
				{
					bool flag = this.HasMouseAttached();
					if (flag != this.mouseAttached)
					{
						this.mouseAttached = flag;
						if (this.MouseStateChanged != null)
						{
							this.MouseStateChanged.Invoke(this.mouseAttached);
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04000324 RID: 804
		private const int WM_DEVICECHANGE = 537;

		// Token: 0x04000325 RID: 805
		private const int DBT_DEVNODES_CHANGED = 7;

		// Token: 0x04000326 RID: 806
		private bool mouseAttached = true;

		// Token: 0x04000327 RID: 807
		[CompilerGenerated]
		private Action<bool> MouseStateChanged;
	}
}
