using System;

namespace System.Reflection
{
	// Token: 0x020008AF RID: 2223
	public sealed class MissingMetadataException : TypeAccessException
	{
		// Token: 0x06004B0E RID: 19214 RVA: 0x000F0AF8 File Offset: 0x000EECF8
		public MissingMetadataException()
		{
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x000F0B00 File Offset: 0x000EED00
		public MissingMetadataException(string message)
			: base(message)
		{
		}
	}
}
