using System;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200064C RID: 1612
	public interface IFieldInfo
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003D5C RID: 15708
		// (set) Token: 0x06003D5D RID: 15709
		string[] FieldNames { get; set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06003D5E RID: 15710
		// (set) Token: 0x06003D5F RID: 15711
		Type[] FieldTypes { get; set; }
	}
}
