using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007B5 RID: 1973
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredCancelableAsyncEnumerable<T>
	{
		// Token: 0x0600456F RID: 17775 RVA: 0x000E4DCA File Offset: 0x000E2FCA
		internal ConfiguredCancelableAsyncEnumerable(IAsyncEnumerable<T> enumerable, bool continueOnCapturedContext, CancellationToken cancellationToken)
		{
			this._enumerable = enumerable;
			this._continueOnCapturedContext = continueOnCapturedContext;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x000E4DE1 File Offset: 0x000E2FE1
		public ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, continueOnCapturedContext, this._cancellationToken);
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x000E4DF5 File Offset: 0x000E2FF5
		public ConfiguredCancelableAsyncEnumerable<T> WithCancellation(CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, this._continueOnCapturedContext, cancellationToken);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x000E4E09 File Offset: 0x000E3009
		public ConfiguredCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator()
		{
			return new ConfiguredCancelableAsyncEnumerable<T>.Enumerator(this._enumerable.GetAsyncEnumerator(this._cancellationToken), this._continueOnCapturedContext);
		}

		// Token: 0x04002CAE RID: 11438
		private readonly IAsyncEnumerable<T> _enumerable;

		// Token: 0x04002CAF RID: 11439
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04002CB0 RID: 11440
		private readonly bool _continueOnCapturedContext;

		// Token: 0x020007B6 RID: 1974
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			// Token: 0x06004573 RID: 17779 RVA: 0x000E4E27 File Offset: 0x000E3027
			internal Enumerator(IAsyncEnumerator<T> enumerator, bool continueOnCapturedContext)
			{
				this._enumerator = enumerator;
				this._continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x06004574 RID: 17780 RVA: 0x000E4E38 File Offset: 0x000E3038
			public ConfiguredValueTaskAwaitable<bool> MoveNextAsync()
			{
				return this._enumerator.MoveNextAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x17000AB1 RID: 2737
			// (get) Token: 0x06004575 RID: 17781 RVA: 0x000E4E5E File Offset: 0x000E305E
			public T Current
			{
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x06004576 RID: 17782 RVA: 0x000E4E6C File Offset: 0x000E306C
			public ConfiguredValueTaskAwaitable DisposeAsync()
			{
				return this._enumerator.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x04002CB1 RID: 11441
			private readonly IAsyncEnumerator<T> _enumerator;

			// Token: 0x04002CB2 RID: 11442
			private readonly bool _continueOnCapturedContext;
		}
	}
}
