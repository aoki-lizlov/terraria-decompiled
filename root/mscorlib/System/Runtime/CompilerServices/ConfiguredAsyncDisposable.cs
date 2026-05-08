using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007B4 RID: 1972
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		// Token: 0x0600456D RID: 17773 RVA: 0x000E4D92 File Offset: 0x000E2F92
		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			this._source = source;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000E4DA4 File Offset: 0x000E2FA4
		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return this._source.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
		}

		// Token: 0x04002CAC RID: 11436
		private readonly IAsyncDisposable _source;

		// Token: 0x04002CAD RID: 11437
		private readonly bool _continueOnCapturedContext;
	}
}
