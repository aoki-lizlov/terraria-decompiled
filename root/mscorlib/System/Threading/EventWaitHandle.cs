using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class EventWaitHandle : WaitHandle
	{
		// Token: 0x06001E81 RID: 7809 RVA: 0x00072992 File Offset: 0x00070B92
		[SecuritySafeCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode)
			: this(initialState, mode, null)
		{
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000729A0 File Offset: 0x00070BA0
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[] { name }));
			}
			int num;
			SafeWaitHandle safeWaitHandle;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid.", new object[] { name }));
				}
				safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(true, initialState, name, out num), true);
			}
			else
			{
				safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(false, initialState, name, out num), true);
			}
			if (safeWaitHandle.IsInvalid)
			{
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[] { name }));
				}
				__Error.WinIOError(num, name);
			}
			base.SetHandleInternal(safeWaitHandle);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00072A6B File Offset: 0x00070C6B
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew)
			: this(initialState, mode, name, out createdNew, null)
		{
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00072A7C File Offset: 0x00070C7C
		[SecurityCritical]
		public EventWaitHandle(bool initialState, EventResetMode mode, string name, out bool createdNew, EventWaitHandleSecurity eventSecurity)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[] { name }));
			}
			bool flag;
			if (mode != EventResetMode.AutoReset)
			{
				if (mode != EventResetMode.ManualReset)
				{
					throw new ArgumentException(Environment.GetResourceString("Value of flags is invalid.", new object[] { name }));
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(NativeEventCalls.CreateEvent_internal(flag, initialState, name, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				safeWaitHandle.SetHandleAsInvalid();
				if (name != null && name.Length != 0 && 6 == num)
				{
					throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[] { name }));
				}
				__Error.WinIOError(num, name);
			}
			createdNew = num != 183;
			base.SetHandleInternal(safeWaitHandle);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00072B48 File Offset: 0x00070D48
		[SecurityCritical]
		private EventWaitHandle(SafeWaitHandle handle)
		{
			base.SetHandleInternal(handle);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00072B57 File Offset: 0x00070D57
		[SecurityCritical]
		public static EventWaitHandle OpenExisting(string name)
		{
			return EventWaitHandle.OpenExisting(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00072B64 File Offset: 0x00070D64
		[SecurityCritical]
		public static EventWaitHandle OpenExisting(string name, EventWaitHandleRights rights)
		{
			EventWaitHandle eventWaitHandle;
			switch (EventWaitHandle.OpenExistingWorker(name, rights, out eventWaitHandle))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				__Error.WinIOError(3, "");
				return eventWaitHandle;
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.", new object[] { name }));
			default:
				return eventWaitHandle;
			}
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00072BBF File Offset: 0x00070DBF
		[SecurityCritical]
		public static bool TryOpenExisting(string name, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, EventWaitHandleRights.Modify | EventWaitHandleRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00072BD0 File Offset: 0x00070DD0
		[SecurityCritical]
		public static bool TryOpenExisting(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			return EventWaitHandle.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00072BE0 File Offset: 0x00070DE0
		[SecurityCritical]
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, EventWaitHandleRights rights, out EventWaitHandle result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("Parameter '{0}' cannot be null."));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Empty name is not legal."), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The name can be no more than 260 characters in length.", new object[] { name }));
			}
			result = null;
			int num;
			SafeWaitHandle safeWaitHandle = new SafeWaitHandle(NativeEventCalls.OpenEvent_internal(name, rights, out num), true);
			if (safeWaitHandle.IsInvalid)
			{
				if (2 == num || 123 == num)
				{
					return WaitHandle.OpenExistingResult.NameNotFound;
				}
				if (3 == num)
				{
					return WaitHandle.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == num)
				{
					return WaitHandle.OpenExistingResult.NameInvalid;
				}
				__Error.WinIOError(num, "");
			}
			result = new EventWaitHandle(safeWaitHandle);
			return WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00072C9D File Offset: 0x00070E9D
		[SecuritySafeCritical]
		public bool Reset()
		{
			bool flag = NativeEventCalls.ResetEvent(this.safeWaitHandle);
			if (!flag)
			{
				throw new IOException();
			}
			return flag;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00072CB5 File Offset: 0x00070EB5
		[SecuritySafeCritical]
		public bool Set()
		{
			bool flag = NativeEventCalls.SetEvent(this.safeWaitHandle);
			if (!flag)
			{
				throw new IOException();
			}
			return flag;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00072CCD File Offset: 0x00070ECD
		[SecuritySafeCritical]
		public EventWaitHandleSecurity GetAccessControl()
		{
			return new EventWaitHandleSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00072CDE File Offset: 0x00070EDE
		[SecuritySafeCritical]
		public void SetAccessControl(EventWaitHandleSecurity eventSecurity)
		{
			if (eventSecurity == null)
			{
				throw new ArgumentNullException("eventSecurity");
			}
			eventSecurity.Persist(this.safeWaitHandle);
		}
	}
}
