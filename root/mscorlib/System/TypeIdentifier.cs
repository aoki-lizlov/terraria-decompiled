using System;

namespace System
{
	// Token: 0x0200022D RID: 557
	internal interface TypeIdentifier : TypeName, IEquatable<TypeName>
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001B89 RID: 7049
		string InternalName { get; }
	}
}
