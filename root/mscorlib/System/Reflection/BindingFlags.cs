using System;

namespace System.Reflection
{
	// Token: 0x02000863 RID: 2147
	[Flags]
	public enum BindingFlags
	{
		// Token: 0x04002DE8 RID: 11752
		Default = 0,
		// Token: 0x04002DE9 RID: 11753
		IgnoreCase = 1,
		// Token: 0x04002DEA RID: 11754
		DeclaredOnly = 2,
		// Token: 0x04002DEB RID: 11755
		Instance = 4,
		// Token: 0x04002DEC RID: 11756
		Static = 8,
		// Token: 0x04002DED RID: 11757
		Public = 16,
		// Token: 0x04002DEE RID: 11758
		NonPublic = 32,
		// Token: 0x04002DEF RID: 11759
		FlattenHierarchy = 64,
		// Token: 0x04002DF0 RID: 11760
		InvokeMethod = 256,
		// Token: 0x04002DF1 RID: 11761
		CreateInstance = 512,
		// Token: 0x04002DF2 RID: 11762
		GetField = 1024,
		// Token: 0x04002DF3 RID: 11763
		SetField = 2048,
		// Token: 0x04002DF4 RID: 11764
		GetProperty = 4096,
		// Token: 0x04002DF5 RID: 11765
		SetProperty = 8192,
		// Token: 0x04002DF6 RID: 11766
		PutDispProperty = 16384,
		// Token: 0x04002DF7 RID: 11767
		PutRefDispProperty = 32768,
		// Token: 0x04002DF8 RID: 11768
		ExactBinding = 65536,
		// Token: 0x04002DF9 RID: 11769
		SuppressChangeType = 131072,
		// Token: 0x04002DFA RID: 11770
		OptionalParamBinding = 262144,
		// Token: 0x04002DFB RID: 11771
		IgnoreReturn = 16777216,
		// Token: 0x04002DFC RID: 11772
		DoNotWrapExceptions = 33554432
	}
}
