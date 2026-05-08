using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000774 RID: 1908
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerator
	{
		// Token: 0x060044A3 RID: 17571
		bool MoveNext();

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060044A4 RID: 17572
		object Current { get; }

		// Token: 0x060044A5 RID: 17573
		void Reset();
	}
}
