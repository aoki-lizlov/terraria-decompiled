using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002C5 RID: 709
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x060020CE RID: 8398 RVA: 0x00077A60 File Offset: 0x00075C60
		internal RegisteredWaitHandle(WaitHandle waitObject, WaitOrTimerCallback callback, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			this._waitObject = waitObject;
			this._callback = callback;
			this._state = state;
			this._timeout = timeout;
			this._executeOnlyOnce = executeOnlyOnce;
			this._finalEvent = null;
			this._cancelEvent = new ManualResetEvent(false);
			this._callsInProcess = 0;
			this._unregistered = false;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00077ABC File Offset: 0x00075CBC
		internal void Wait(object state)
		{
			bool flag = false;
			try
			{
				this._waitObject.SafeWaitHandle.DangerousAddRef(ref flag);
				RegisteredWaitHandle registeredWaitHandle;
				try
				{
					WaitHandle[] array = new WaitHandle[] { this._waitObject, this._cancelEvent };
					do
					{
						int num = WaitHandle.WaitAny(array, this._timeout, false);
						if (!this._unregistered)
						{
							registeredWaitHandle = this;
							lock (registeredWaitHandle)
							{
								this._callsInProcess++;
							}
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoCallBack), num == 258);
						}
					}
					while (!this._unregistered && !this._executeOnlyOnce);
				}
				catch
				{
				}
				registeredWaitHandle = this;
				lock (registeredWaitHandle)
				{
					this._unregistered = true;
					if (this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
						this._finalEvent = null;
					}
				}
			}
			catch (ObjectDisposedException)
			{
				if (flag)
				{
					throw;
				}
			}
			finally
			{
				if (flag)
				{
					this._waitObject.SafeWaitHandle.DangerousRelease();
				}
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00077C10 File Offset: 0x00075E10
		private void DoCallBack(object timedOut)
		{
			try
			{
				if (this._callback != null)
				{
					this._callback(this._state, (bool)timedOut);
				}
			}
			finally
			{
				lock (this)
				{
					this._callsInProcess--;
					if (this._unregistered && this._callsInProcess == 0 && this._finalEvent != null)
					{
						NativeEventCalls.SetEvent(this._finalEvent.SafeWaitHandle);
						this._finalEvent = null;
					}
				}
			}
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00077CB4 File Offset: 0x00075EB4
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			bool flag2;
			lock (this)
			{
				if (this._unregistered)
				{
					flag2 = false;
				}
				else
				{
					this._finalEvent = waitObject;
					this._unregistered = true;
					this._cancelEvent.Set();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x04001A48 RID: 6728
		private WaitHandle _waitObject;

		// Token: 0x04001A49 RID: 6729
		private WaitOrTimerCallback _callback;

		// Token: 0x04001A4A RID: 6730
		private object _state;

		// Token: 0x04001A4B RID: 6731
		private WaitHandle _finalEvent;

		// Token: 0x04001A4C RID: 6732
		private ManualResetEvent _cancelEvent;

		// Token: 0x04001A4D RID: 6733
		private TimeSpan _timeout;

		// Token: 0x04001A4E RID: 6734
		private int _callsInProcess;

		// Token: 0x04001A4F RID: 6735
		private bool _executeOnlyOnce;

		// Token: 0x04001A50 RID: 6736
		private bool _unregistered;
	}
}
