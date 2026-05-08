using System;

namespace ReLogic.OS
{
	// Token: 0x02000062 RID: 98
	public interface IClipboard
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600021D RID: 541
		// (set) Token: 0x0600021E RID: 542
		string Value { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600021F RID: 543
		string MultiLineValue { get; }
	}
}
