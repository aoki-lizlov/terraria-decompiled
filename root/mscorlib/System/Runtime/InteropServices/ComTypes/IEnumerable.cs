using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000773 RID: 1907
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x060044A2 RID: 17570
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
