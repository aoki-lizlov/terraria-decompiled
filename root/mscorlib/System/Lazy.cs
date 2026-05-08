using System;
using System.Diagnostics;
using System.Threading;

namespace System
{
	// Token: 0x02000118 RID: 280
	[DebuggerTypeProxy(typeof(LazyDebugView<>))]
	[DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
	[Serializable]
	public class Lazy<T>
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x0002A411 File Offset: 0x00028611
		private static T CreateViaDefaultConstructor()
		{
			return (T)((object)LazyHelper.CreateViaDefaultConstructor(typeof(T)));
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0002A427 File Offset: 0x00028627
		public Lazy()
			: this(null, LazyThreadSafetyMode.ExecutionAndPublication, true)
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002A432 File Offset: 0x00028632
		public Lazy(T value)
		{
			this._value = value;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002A441 File Offset: 0x00028641
		public Lazy(Func<T> valueFactory)
			: this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication, false)
		{
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002A44C File Offset: 0x0002864C
		public Lazy(bool isThreadSafe)
			: this(null, LazyHelper.GetModeFromIsThreadSafe(isThreadSafe), true)
		{
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002A45C File Offset: 0x0002865C
		public Lazy(LazyThreadSafetyMode mode)
			: this(null, mode, true)
		{
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002A467 File Offset: 0x00028667
		public Lazy(Func<T> valueFactory, bool isThreadSafe)
			: this(valueFactory, LazyHelper.GetModeFromIsThreadSafe(isThreadSafe), false)
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002A477 File Offset: 0x00028677
		public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
			: this(valueFactory, mode, false)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002A482 File Offset: 0x00028682
		private Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode, bool useDefaultConstructor)
		{
			if (valueFactory == null && !useDefaultConstructor)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this._factory = valueFactory;
			this._state = LazyHelper.Create(mode, useDefaultConstructor);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002A4B1 File Offset: 0x000286B1
		private void ViaConstructor()
		{
			this._value = Lazy<T>.CreateViaDefaultConstructor();
			this._state = null;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002A4C8 File Offset: 0x000286C8
		private void ViaFactory(LazyThreadSafetyMode mode)
		{
			try
			{
				Func<T> factory = this._factory;
				if (factory == null)
				{
					throw new InvalidOperationException("ValueFactory attempted to access the Value property of this instance.");
				}
				this._factory = null;
				this._value = factory();
				this._state = null;
			}
			catch (Exception ex)
			{
				this._state = new LazyHelper(mode, ex);
				throw;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002A52C File Offset: 0x0002872C
		private void ExecutionAndPublication(LazyHelper executionAndPublication, bool useDefaultConstructor)
		{
			lock (executionAndPublication)
			{
				if (this._state == executionAndPublication)
				{
					if (useDefaultConstructor)
					{
						this.ViaConstructor();
					}
					else
					{
						this.ViaFactory(LazyThreadSafetyMode.ExecutionAndPublication);
					}
				}
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002A580 File Offset: 0x00028780
		private void PublicationOnly(LazyHelper publicationOnly, T possibleValue)
		{
			if (Interlocked.CompareExchange<LazyHelper>(ref this._state, LazyHelper.PublicationOnlyWaitForOtherThreadToPublish, publicationOnly) == publicationOnly)
			{
				this._factory = null;
				this._value = possibleValue;
				this._state = null;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002A5AD File Offset: 0x000287AD
		private void PublicationOnlyViaConstructor(LazyHelper initializer)
		{
			this.PublicationOnly(initializer, Lazy<T>.CreateViaDefaultConstructor());
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002A5BC File Offset: 0x000287BC
		private void PublicationOnlyViaFactory(LazyHelper initializer)
		{
			Func<T> factory = this._factory;
			if (factory == null)
			{
				this.PublicationOnlyWaitForOtherThreadToPublish();
				return;
			}
			this.PublicationOnly(initializer, factory());
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002A5E8 File Offset: 0x000287E8
		private void PublicationOnlyWaitForOtherThreadToPublish()
		{
			SpinWait spinWait = default(SpinWait);
			while (this._state != null)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002A610 File Offset: 0x00028810
		private T CreateValue()
		{
			LazyHelper state = this._state;
			if (state != null)
			{
				switch (state.State)
				{
				case LazyState.NoneViaConstructor:
					this.ViaConstructor();
					goto IL_0084;
				case LazyState.NoneViaFactory:
					this.ViaFactory(LazyThreadSafetyMode.None);
					goto IL_0084;
				case LazyState.PublicationOnlyViaConstructor:
					this.PublicationOnlyViaConstructor(state);
					goto IL_0084;
				case LazyState.PublicationOnlyViaFactory:
					this.PublicationOnlyViaFactory(state);
					goto IL_0084;
				case LazyState.PublicationOnlyWait:
					this.PublicationOnlyWaitForOtherThreadToPublish();
					goto IL_0084;
				case LazyState.ExecutionAndPublicationViaConstructor:
					this.ExecutionAndPublication(state, true);
					goto IL_0084;
				case LazyState.ExecutionAndPublicationViaFactory:
					this.ExecutionAndPublication(state, false);
					goto IL_0084;
				}
				state.ThrowException();
			}
			IL_0084:
			return this.Value;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002A6A8 File Offset: 0x000288A8
		public override string ToString()
		{
			if (!this.IsValueCreated)
			{
				return "Value is not created.";
			}
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002A6D8 File Offset: 0x000288D8
		internal T ValueForDebugDisplay
		{
			get
			{
				if (!this.IsValueCreated)
				{
					return default(T);
				}
				return this._value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002A6FD File Offset: 0x000288FD
		internal LazyThreadSafetyMode? Mode
		{
			get
			{
				return LazyHelper.GetMode(this._state);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002A70C File Offset: 0x0002890C
		internal bool IsValueFaulted
		{
			get
			{
				return LazyHelper.GetIsValueFaulted(this._state);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002A71B File Offset: 0x0002891B
		public bool IsValueCreated
		{
			get
			{
				return this._state == null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002A728 File Offset: 0x00028928
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public T Value
		{
			get
			{
				if (this._state != null)
				{
					return this.CreateValue();
				}
				return this._value;
			}
		}

		// Token: 0x040010F0 RID: 4336
		private volatile LazyHelper _state;

		// Token: 0x040010F1 RID: 4337
		private Func<T> _factory;

		// Token: 0x040010F2 RID: 4338
		private T _value;
	}
}
