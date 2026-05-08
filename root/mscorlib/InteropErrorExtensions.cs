using System;

// Token: 0x0200001A RID: 26
internal static class InteropErrorExtensions
{
	// Token: 0x06000041 RID: 65 RVA: 0x000025C6 File Offset: 0x000007C6
	public static Interop.ErrorInfo Info(this Interop.Error error)
	{
		return new Interop.ErrorInfo(error);
	}
}
