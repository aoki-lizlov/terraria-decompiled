using System;

namespace System
{
	// Token: 0x02000231 RID: 561
	internal class TypeIdentifiers
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x00068611 File Offset: 0x00066811
		internal static TypeIdentifier FromDisplay(string displayName)
		{
			return new TypeIdentifiers.Display(displayName);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00068619 File Offset: 0x00066819
		internal static TypeIdentifier FromInternal(string internalName)
		{
			return new TypeIdentifiers.Internal(internalName);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00068621 File Offset: 0x00066821
		internal static TypeIdentifier FromInternal(string internalNameSpace, TypeIdentifier typeName)
		{
			return new TypeIdentifiers.Internal(internalNameSpace, typeName);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0006862A File Offset: 0x0006682A
		internal static TypeIdentifier WithoutEscape(string simpleName)
		{
			return new TypeIdentifiers.NoEscape(simpleName);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000025BE File Offset: 0x000007BE
		public TypeIdentifiers()
		{
		}

		// Token: 0x02000232 RID: 562
		private class Display : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B9A RID: 7066 RVA: 0x00068632 File Offset: 0x00066832
			internal Display(string displayName)
			{
				this.displayName = displayName;
				this.internal_name = null;
			}

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06001B9B RID: 7067 RVA: 0x00068648 File Offset: 0x00066848
			public override string DisplayName
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x1700032E RID: 814
			// (get) Token: 0x06001B9C RID: 7068 RVA: 0x00068650 File Offset: 0x00066850
			public string InternalName
			{
				get
				{
					if (this.internal_name == null)
					{
						this.internal_name = this.GetInternalName();
					}
					return this.internal_name;
				}
			}

			// Token: 0x06001B9D RID: 7069 RVA: 0x0006866C File Offset: 0x0006686C
			private string GetInternalName()
			{
				return TypeSpec.UnescapeInternalName(this.displayName);
			}

			// Token: 0x06001B9E RID: 7070 RVA: 0x00068679 File Offset: 0x00066879
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001873 RID: 6259
			private string displayName;

			// Token: 0x04001874 RID: 6260
			private string internal_name;
		}

		// Token: 0x02000233 RID: 563
		private class Internal : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001B9F RID: 7071 RVA: 0x00068696 File Offset: 0x00066896
			internal Internal(string internalName)
			{
				this.internalName = internalName;
				this.display_name = null;
			}

			// Token: 0x06001BA0 RID: 7072 RVA: 0x000686AC File Offset: 0x000668AC
			internal Internal(string nameSpaceInternal, TypeIdentifier typeName)
			{
				this.internalName = nameSpaceInternal + "." + typeName.InternalName;
				this.display_name = null;
			}

			// Token: 0x1700032F RID: 815
			// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000686D2 File Offset: 0x000668D2
			public override string DisplayName
			{
				get
				{
					if (this.display_name == null)
					{
						this.display_name = this.GetDisplayName();
					}
					return this.display_name;
				}
			}

			// Token: 0x17000330 RID: 816
			// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000686EE File Offset: 0x000668EE
			public string InternalName
			{
				get
				{
					return this.internalName;
				}
			}

			// Token: 0x06001BA3 RID: 7075 RVA: 0x000686F6 File Offset: 0x000668F6
			private string GetDisplayName()
			{
				return TypeSpec.EscapeDisplayName(this.internalName);
			}

			// Token: 0x06001BA4 RID: 7076 RVA: 0x00068679 File Offset: 0x00066879
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001875 RID: 6261
			private string internalName;

			// Token: 0x04001876 RID: 6262
			private string display_name;
		}

		// Token: 0x02000234 RID: 564
		private class NoEscape : TypeNames.ATypeName, TypeIdentifier, TypeName, IEquatable<TypeName>
		{
			// Token: 0x06001BA5 RID: 7077 RVA: 0x00068703 File Offset: 0x00066903
			internal NoEscape(string simpleName)
			{
				this.simpleName = simpleName;
			}

			// Token: 0x17000331 RID: 817
			// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x00068712 File Offset: 0x00066912
			public override string DisplayName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x17000332 RID: 818
			// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x00068712 File Offset: 0x00066912
			public string InternalName
			{
				get
				{
					return this.simpleName;
				}
			}

			// Token: 0x06001BA8 RID: 7080 RVA: 0x00068679 File Offset: 0x00066879
			public override TypeName NestedName(TypeIdentifier innerName)
			{
				return TypeNames.FromDisplay(this.DisplayName + "+" + innerName.DisplayName);
			}

			// Token: 0x04001877 RID: 6263
			private string simpleName;
		}
	}
}
