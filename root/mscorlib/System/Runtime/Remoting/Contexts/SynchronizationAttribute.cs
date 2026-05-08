using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200056F RID: 1391
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x000C8744 File Offset: 0x000C6944
		public SynchronizationAttribute()
			: this(8, false)
		{
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000C874E File Offset: 0x000C694E
		public SynchronizationAttribute(bool reEntrant)
			: this(8, reEntrant)
		{
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000C8758 File Offset: 0x000C6958
		public SynchronizationAttribute(int flag)
			: this(flag, false)
		{
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000C8764 File Offset: 0x000C6964
		public SynchronizationAttribute(int flag, bool reEntrant)
			: base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000C87B1 File Offset: 0x000C69B1
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000C87B9 File Offset: 0x000C69B9
		// (set) Token: 0x0600377F RID: 14207 RVA: 0x000C87C4 File Offset: 0x000C69C4
		public virtual bool Locked
		{
			get
			{
				return this._lockCount > 0;
			}
			set
			{
				SynchronizationAttribute synchronizationAttribute;
				if (value)
				{
					this.AcquireLock();
					synchronizationAttribute = this;
					lock (synchronizationAttribute)
					{
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						return;
					}
				}
				synchronizationAttribute = this;
				lock (synchronizationAttribute)
				{
					while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
					{
						this.ReleaseLock();
					}
				}
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000C8854 File Offset: 0x000C6A54
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000C88B0 File Offset: 0x000C6AB0
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					if (this._lockCount == 0)
					{
						this._ownerThread = null;
					}
				}
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000C8924 File Offset: 0x000C6B24
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000C893C File Offset: 0x000C6B3C
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000C8945 File Offset: 0x000C6B45
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x000C8950 File Offset: 0x000C6B50
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			int flavor = this._flavor;
			switch (flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 3:
				break;
			case 4:
				return synchronizationAttribute != null;
			default:
				if (flavor == 8)
				{
					return false;
				}
				break;
			}
			return false;
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000C89A4 File Offset: 0x000C6BA4
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000C89E0 File Offset: 0x000C6BE0
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		// Token: 0x0400253C RID: 9532
		public const int NOT_SUPPORTED = 1;

		// Token: 0x0400253D RID: 9533
		public const int SUPPORTED = 2;

		// Token: 0x0400253E RID: 9534
		public const int REQUIRED = 4;

		// Token: 0x0400253F RID: 9535
		public const int REQUIRES_NEW = 8;

		// Token: 0x04002540 RID: 9536
		private bool _bReEntrant;

		// Token: 0x04002541 RID: 9537
		private int _flavor;

		// Token: 0x04002542 RID: 9538
		[NonSerialized]
		private int _lockCount;

		// Token: 0x04002543 RID: 9539
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x04002544 RID: 9540
		[NonSerialized]
		private Thread _ownerThread;
	}
}
