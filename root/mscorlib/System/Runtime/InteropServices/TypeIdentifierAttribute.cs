using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006B3 RID: 1715
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(false)]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		// Token: 0x06003FF2 RID: 16370 RVA: 0x00002050 File Offset: 0x00000250
		public TypeIdentifierAttribute()
		{
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000E0959 File Offset: 0x000DEB59
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope_ = scope;
			this.Identifier_ = identifier;
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003FF4 RID: 16372 RVA: 0x000E096F File Offset: 0x000DEB6F
		public string Scope
		{
			get
			{
				return this.Scope_;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06003FF5 RID: 16373 RVA: 0x000E0977 File Offset: 0x000DEB77
		public string Identifier
		{
			get
			{
				return this.Identifier_;
			}
		}

		// Token: 0x040029AD RID: 10669
		internal string Scope_;

		// Token: 0x040029AE RID: 10670
		internal string Identifier_;
	}
}
