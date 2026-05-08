using System;

namespace System
{
	// Token: 0x0200022C RID: 556
	internal interface TypeName : IEquatable<TypeName>
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001B87 RID: 7047
		string DisplayName { get; }

		// Token: 0x06001B88 RID: 7048
		TypeName NestedName(TypeIdentifier innerName);
	}
}
