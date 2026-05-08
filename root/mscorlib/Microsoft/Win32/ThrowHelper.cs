using System;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x02000087 RID: 135
	internal static class ThrowHelper
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x000153A0 File Offset: 0x000135A0
		internal static void ThrowArgumentException(string msg)
		{
			throw new ArgumentException(msg);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000153A8 File Offset: 0x000135A8
		internal static void ThrowArgumentException(string msg, string argument)
		{
			throw new ArgumentException(msg, argument);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000153B1 File Offset: 0x000135B1
		internal static void ThrowArgumentNullException(string argument)
		{
			throw new ArgumentNullException(argument);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000153B9 File Offset: 0x000135B9
		internal static void ThrowInvalidOperationException(string msg)
		{
			throw new InvalidOperationException(msg);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000153C1 File Offset: 0x000135C1
		internal static void ThrowSecurityException(string msg)
		{
			throw new SecurityException(msg);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000153C9 File Offset: 0x000135C9
		internal static void ThrowUnauthorizedAccessException(string msg)
		{
			throw new UnauthorizedAccessException(msg);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000153D1 File Offset: 0x000135D1
		internal static void ThrowObjectDisposedException(string objectName, string msg)
		{
			throw new ObjectDisposedException(objectName, msg);
		}
	}
}
