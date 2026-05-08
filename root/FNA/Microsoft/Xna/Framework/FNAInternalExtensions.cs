using System;
using System.IO;
using System.Reflection;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000037 RID: 55
	internal static class FNAInternalExtensions
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x0001AC5C File Offset: 0x00018E5C
		internal static bool TryGetBuffer(this MemoryStream stream, out byte[] buffer)
		{
			if (!(FNAInternalExtensions.f_MemoryStream_Public != null))
			{
				bool flag;
				try
				{
					buffer = stream.GetBuffer();
					flag = true;
				}
				catch (UnauthorizedAccessException)
				{
					buffer = null;
					flag = false;
				}
				return flag;
			}
			if ((bool)FNAInternalExtensions.f_MemoryStream_Public.GetValue(stream))
			{
				buffer = stream.GetBuffer();
				return true;
			}
			buffer = null;
			return false;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001ACC0 File Offset: 0x00018EC0
		// Note: this type is marked as 'beforefieldinit'.
		static FNAInternalExtensions()
		{
		}

		// Token: 0x040005BC RID: 1468
		private static readonly FieldInfo f_MemoryStream_Public = typeof(MemoryStream).GetField("_exposable", BindingFlags.Instance | BindingFlags.NonPublic) ?? typeof(MemoryStream).GetField("allowGetBuffer", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
