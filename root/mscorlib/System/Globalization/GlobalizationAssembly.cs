using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009E0 RID: 2528
	internal sealed class GlobalizationAssembly
	{
		// Token: 0x06005C92 RID: 23698 RVA: 0x0013D5A4 File Offset: 0x0013B7A4
		[SecurityCritical]
		internal unsafe static byte* GetGlobalizationResourceBytePtr(Assembly assembly, string tableName)
		{
			UnmanagedMemoryStream unmanagedMemoryStream = assembly.GetManifestResourceStream(tableName) as UnmanagedMemoryStream;
			if (unmanagedMemoryStream != null)
			{
				byte* positionPointer = unmanagedMemoryStream.PositionPointer;
				if (positionPointer != null)
				{
					return positionPointer;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x000025BE File Offset: 0x000007BE
		public GlobalizationAssembly()
		{
		}
	}
}
