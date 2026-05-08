using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System
{
	// Token: 0x02000117 RID: 279
	internal class LazyHelper
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0002A249 File Offset: 0x00028449
		internal LazyState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002A251 File Offset: 0x00028451
		internal LazyHelper(LazyState state)
		{
			this.State = state;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002A260 File Offset: 0x00028460
		internal LazyHelper(LazyThreadSafetyMode mode, Exception exception)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				this.State = LazyState.NoneException;
				break;
			case LazyThreadSafetyMode.PublicationOnly:
				this.State = LazyState.PublicationOnlyException;
				break;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				this.State = LazyState.ExecutionAndPublicationException;
				break;
			}
			this._exceptionDispatch = ExceptionDispatchInfo.Capture(exception);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002A2AD File Offset: 0x000284AD
		internal void ThrowException()
		{
			this._exceptionDispatch.Throw();
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002A2BC File Offset: 0x000284BC
		private LazyThreadSafetyMode GetMode()
		{
			switch (this.State)
			{
			case LazyState.NoneViaConstructor:
			case LazyState.NoneViaFactory:
			case LazyState.NoneException:
				return LazyThreadSafetyMode.None;
			case LazyState.PublicationOnlyViaConstructor:
			case LazyState.PublicationOnlyViaFactory:
			case LazyState.PublicationOnlyWait:
			case LazyState.PublicationOnlyException:
				return LazyThreadSafetyMode.PublicationOnly;
			case LazyState.ExecutionAndPublicationViaConstructor:
			case LazyState.ExecutionAndPublicationViaFactory:
			case LazyState.ExecutionAndPublicationException:
				return LazyThreadSafetyMode.ExecutionAndPublication;
			default:
				return LazyThreadSafetyMode.None;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002A308 File Offset: 0x00028508
		internal static LazyThreadSafetyMode? GetMode(LazyHelper state)
		{
			if (state == null)
			{
				return null;
			}
			return new LazyThreadSafetyMode?(state.GetMode());
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002A32D File Offset: 0x0002852D
		internal static bool GetIsValueFaulted(LazyHelper state)
		{
			return ((state != null) ? state._exceptionDispatch : null) != null;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002A340 File Offset: 0x00028540
		internal static LazyHelper Create(LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			switch (mode)
			{
			case LazyThreadSafetyMode.None:
				if (!useDefaultConstructor)
				{
					return LazyHelper.NoneViaFactory;
				}
				return LazyHelper.NoneViaConstructor;
			case LazyThreadSafetyMode.PublicationOnly:
				if (!useDefaultConstructor)
				{
					return LazyHelper.PublicationOnlyViaFactory;
				}
				return LazyHelper.PublicationOnlyViaConstructor;
			case LazyThreadSafetyMode.ExecutionAndPublication:
				return new LazyHelper(useDefaultConstructor ? LazyState.ExecutionAndPublicationViaConstructor : LazyState.ExecutionAndPublicationViaFactory);
			default:
				throw new ArgumentOutOfRangeException("mode", "The mode argument specifies an invalid value.");
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002A39C File Offset: 0x0002859C
		internal static object CreateViaDefaultConstructor(Type type)
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(type);
			}
			catch (MissingMethodException)
			{
				throw new MissingMemberException("The lazily-initialized type does not have a public, parameterless constructor.");
			}
			return obj;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002A3D0 File Offset: 0x000285D0
		internal static LazyThreadSafetyMode GetModeFromIsThreadSafe(bool isThreadSafe)
		{
			if (!isThreadSafe)
			{
				return LazyThreadSafetyMode.None;
			}
			return LazyThreadSafetyMode.ExecutionAndPublication;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002A3D8 File Offset: 0x000285D8
		// Note: this type is marked as 'beforefieldinit'.
		static LazyHelper()
		{
		}

		// Token: 0x040010E9 RID: 4329
		internal static readonly LazyHelper NoneViaConstructor = new LazyHelper(LazyState.NoneViaConstructor);

		// Token: 0x040010EA RID: 4330
		internal static readonly LazyHelper NoneViaFactory = new LazyHelper(LazyState.NoneViaFactory);

		// Token: 0x040010EB RID: 4331
		internal static readonly LazyHelper PublicationOnlyViaConstructor = new LazyHelper(LazyState.PublicationOnlyViaConstructor);

		// Token: 0x040010EC RID: 4332
		internal static readonly LazyHelper PublicationOnlyViaFactory = new LazyHelper(LazyState.PublicationOnlyViaFactory);

		// Token: 0x040010ED RID: 4333
		internal static readonly LazyHelper PublicationOnlyWaitForOtherThreadToPublish = new LazyHelper(LazyState.PublicationOnlyWait);

		// Token: 0x040010EE RID: 4334
		[CompilerGenerated]
		private readonly LazyState <State>k__BackingField;

		// Token: 0x040010EF RID: 4335
		private readonly ExceptionDispatchInfo _exceptionDispatch;
	}
}
