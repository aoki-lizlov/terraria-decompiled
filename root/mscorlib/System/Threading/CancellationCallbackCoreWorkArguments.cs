using System;

namespace System.Threading
{
	// Token: 0x02000281 RID: 641
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06001E15 RID: 7701 RVA: 0x00070BDC File Offset: 0x0006EDDC
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this._currArrayFragment = currArrayFragment;
			this._currArrayIndex = currArrayIndex;
		}

		// Token: 0x04001972 RID: 6514
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> _currArrayFragment;

		// Token: 0x04001973 RID: 6515
		internal int _currArrayIndex;
	}
}
