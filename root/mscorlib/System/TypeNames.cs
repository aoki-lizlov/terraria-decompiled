using System;

namespace System
{
	// Token: 0x0200022E RID: 558
	internal class TypeNames
	{
		// Token: 0x06001B8A RID: 7050 RVA: 0x000685A2 File Offset: 0x000667A2
		internal static TypeName FromDisplay(string displayName)
		{
			return new TypeNames.Display(displayName);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000025BE File Offset: 0x000007BE
		public TypeNames()
		{
		}

		// Token: 0x0200022F RID: 559
		internal abstract class ATypeName : TypeName, IEquatable<TypeName>
		{
			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06001B8C RID: 7052
			public abstract string DisplayName { get; }

			// Token: 0x06001B8D RID: 7053
			public abstract TypeName NestedName(TypeIdentifier innerName);

			// Token: 0x06001B8E RID: 7054 RVA: 0x000685AA File Offset: 0x000667AA
			public bool Equals(TypeName other)
			{
				return other != null && this.DisplayName == other.DisplayName;
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x000685C2 File Offset: 0x000667C2
			public override int GetHashCode()
			{
				return this.DisplayName.GetHashCode();
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x000685CF File Offset: 0x000667CF
			public override bool Equals(object other)
			{
				return this.Equals(other as TypeName);
			}

			// Token: 0x06001B91 RID: 7057 RVA: 0x000025BE File Offset: 0x000007BE
			protected ATypeName()
			{
			}
		}

		// Token: 0x02000230 RID: 560
		private class Display : TypeNames.ATypeName
		{
			// Token: 0x06001B92 RID: 7058 RVA: 0x000685DD File Offset: 0x000667DD
			internal Display(string displayName)
			{
				this.displayName = displayName;
			}

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000685EC File Offset: 0x000667EC
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x06001B94 RID: 7060 RVA: 0x000685F4 File Offset: 0x000667F4
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return new TypeNames.Display(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001872 RID: 6258
			private string displayName;
		}
	}
}
