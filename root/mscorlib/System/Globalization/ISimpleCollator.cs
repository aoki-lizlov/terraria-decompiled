using System;

namespace System.Globalization
{
	// Token: 0x020009F7 RID: 2551
	internal interface ISimpleCollator
	{
		// Token: 0x06005EB7 RID: 24247
		SortKey GetSortKey(string source, CompareOptions options);

		// Token: 0x06005EB8 RID: 24248
		int Compare(string s1, string s2);

		// Token: 0x06005EB9 RID: 24249
		int Compare(string s1, int idx1, int len1, string s2, int idx2, int len2, CompareOptions options);

		// Token: 0x06005EBA RID: 24250
		bool IsPrefix(string src, string target, CompareOptions opt);

		// Token: 0x06005EBB RID: 24251
		bool IsSuffix(string src, string target, CompareOptions opt);

		// Token: 0x06005EBC RID: 24252
		int IndexOf(string s, string target, int start, int length, CompareOptions opt);

		// Token: 0x06005EBD RID: 24253
		int IndexOf(string s, char target, int start, int length, CompareOptions opt);

		// Token: 0x06005EBE RID: 24254
		int LastIndexOf(string s, string target, CompareOptions opt);

		// Token: 0x06005EBF RID: 24255
		int LastIndexOf(string s, string target, int start, int length, CompareOptions opt);

		// Token: 0x06005EC0 RID: 24256
		int LastIndexOf(string s, char target, CompareOptions opt);

		// Token: 0x06005EC1 RID: 24257
		int LastIndexOf(string s, char target, int start, int length, CompareOptions opt);
	}
}
