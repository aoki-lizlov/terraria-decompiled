using System;

namespace System.Threading
{
	// Token: 0x02000258 RID: 600
	internal interface IAsyncLocal
	{
		// Token: 0x06001D3B RID: 7483
		void OnValueChanged(object previousValue, object currentValue, bool contextChanged);
	}
}
