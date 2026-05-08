using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000664 RID: 1636
	internal interface IStreamable
	{
		// Token: 0x06003DA6 RID: 15782
		[SecurityCritical]
		void Read(__BinaryParser input);

		// Token: 0x06003DA7 RID: 15783
		void Write(__BinaryWriter sout);
	}
}
