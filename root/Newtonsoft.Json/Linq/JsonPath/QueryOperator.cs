using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C6 RID: 198
	internal enum QueryOperator
	{
		// Token: 0x04000382 RID: 898
		None,
		// Token: 0x04000383 RID: 899
		Equals,
		// Token: 0x04000384 RID: 900
		NotEquals,
		// Token: 0x04000385 RID: 901
		Exists,
		// Token: 0x04000386 RID: 902
		LessThan,
		// Token: 0x04000387 RID: 903
		LessThanOrEquals,
		// Token: 0x04000388 RID: 904
		GreaterThan,
		// Token: 0x04000389 RID: 905
		GreaterThanOrEquals,
		// Token: 0x0400038A RID: 906
		And,
		// Token: 0x0400038B RID: 907
		Or
	}
}
