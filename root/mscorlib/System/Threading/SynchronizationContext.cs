using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200029F RID: 671
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)]
	public class SynchronizationContext
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000025BE File Offset: 0x000007BE
		public SynchronizationContext()
		{
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00073B5C File Offset: 0x00071D5C
		[SecuritySafeCritical]
		protected void SetWaitNotificationRequired()
		{
			Type type = base.GetType();
			if (SynchronizationContext.s_cachedPreparedType1 != type && SynchronizationContext.s_cachedPreparedType2 != type && SynchronizationContext.s_cachedPreparedType3 != type && SynchronizationContext.s_cachedPreparedType4 != type && SynchronizationContext.s_cachedPreparedType5 != type)
			{
				RuntimeHelpers.PrepareDelegate(new SynchronizationContext.WaitDelegate(this.Wait));
				if (SynchronizationContext.s_cachedPreparedType1 == null)
				{
					SynchronizationContext.s_cachedPreparedType1 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType2 == null)
				{
					SynchronizationContext.s_cachedPreparedType2 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType3 == null)
				{
					SynchronizationContext.s_cachedPreparedType3 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType4 == null)
				{
					SynchronizationContext.s_cachedPreparedType4 = type;
				}
				else if (SynchronizationContext.s_cachedPreparedType5 == null)
				{
					SynchronizationContext.s_cachedPreparedType5 = type;
				}
			}
			this._props |= SynchronizationContextProperties.RequireWaitNotification;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x00073C44 File Offset: 0x00071E44
		public bool IsWaitNotificationRequired()
		{
			return (this._props & SynchronizationContextProperties.RequireWaitNotification) > SynchronizationContextProperties.None;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x00073C51 File Offset: 0x00071E51
		public virtual void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00073C5A File Offset: 0x00071E5A
		public virtual void Post(SendOrPostCallback d, object state)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(d.Invoke), state);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void OperationStarted()
		{
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void OperationCompleted()
		{
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00073C6F File Offset: 0x00071E6F
		[SecurityCritical]
		[CLSCompliant(false)]
		[PrePrepareMethod]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00073C88 File Offset: 0x00071E88
		[SecurityCritical]
		[CLSCompliant(false)]
		[PrePrepareMethod]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected unsafe static int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			IntPtr* ptr;
			if (waitHandles == null || waitHandles.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &waitHandles[0];
			}
			return WaitHandle.Wait_internal(ptr, waitHandles.Length, waitAll, millisecondsTimeout);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00073CB8 File Offset: 0x00071EB8
		[SecurityCritical]
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.SynchronizationContext = syncContext;
			mutableExecutionContext.SynchronizationContextNoFlow = syncContext;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00073CD4 File Offset: 0x00071ED4
		public static SynchronizationContext Current
		{
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContext ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00073CFC File Offset: 0x00071EFC
		internal static SynchronizationContext CurrentNoFlow
		{
			[FriendAccessAllowed]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().SynchronizationContextNoFlow ?? SynchronizationContext.GetThreadLocalContext();
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		private static SynchronizationContext GetThreadLocalContext()
		{
			return null;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00073D24 File Offset: 0x00071F24
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext();
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00073D2B File Offset: 0x00071F2B
		[SecurityCritical]
		private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x00073D36 File Offset: 0x00071F36
		internal static SynchronizationContext CurrentExplicit
		{
			get
			{
				return SynchronizationContext.Current;
			}
		}

		// Token: 0x040019D6 RID: 6614
		private SynchronizationContextProperties _props;

		// Token: 0x040019D7 RID: 6615
		private static Type s_cachedPreparedType1;

		// Token: 0x040019D8 RID: 6616
		private static Type s_cachedPreparedType2;

		// Token: 0x040019D9 RID: 6617
		private static Type s_cachedPreparedType3;

		// Token: 0x040019DA RID: 6618
		private static Type s_cachedPreparedType4;

		// Token: 0x040019DB RID: 6619
		private static Type s_cachedPreparedType5;

		// Token: 0x020002A0 RID: 672
		// (Invoke) Token: 0x06001F07 RID: 7943
		private delegate int WaitDelegate(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout);
	}
}
