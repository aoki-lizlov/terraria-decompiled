using System;

// Token: 0x0200001D RID: 29
internal sealed class Locale
{
	// Token: 0x06000042 RID: 66 RVA: 0x000025BE File Offset: 0x000007BE
	private Locale()
	{
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000025CE File Offset: 0x000007CE
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000025D1 File Offset: 0x000007D1
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
