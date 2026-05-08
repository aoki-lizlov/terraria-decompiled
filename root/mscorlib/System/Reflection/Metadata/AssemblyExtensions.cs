using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020008D6 RID: 2262
	public static class AssemblyExtensions
	{
		// Token: 0x06004DB6 RID: 19894 RVA: 0x000174FB File Offset: 0x000156FB
		[CLSCompliant(false)]
		public unsafe static bool TryGetRawMetadata(this Assembly assembly, out byte* blob, out int length)
		{
			throw new NotImplementedException();
		}
	}
}
