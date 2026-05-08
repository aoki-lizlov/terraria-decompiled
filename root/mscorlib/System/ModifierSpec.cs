using System;
using System.Text;

namespace System
{
	// Token: 0x02000235 RID: 565
	internal interface ModifierSpec
	{
		// Token: 0x06001BA9 RID: 7081
		Type Resolve(Type type);

		// Token: 0x06001BAA RID: 7082
		StringBuilder Append(StringBuilder sb);
	}
}
