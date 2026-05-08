using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200085E RID: 2142
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	public sealed class AssemblySignatureKeyAttribute : Attribute
	{
		// Token: 0x06004804 RID: 18436 RVA: 0x000EDB83 File Offset: 0x000EBD83
		public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
		{
			this.PublicKey = publicKey;
			this.Countersignature = countersignature;
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x000EDB99 File Offset: 0x000EBD99
		public string PublicKey
		{
			[CompilerGenerated]
			get
			{
				return this.<PublicKey>k__BackingField;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x000EDBA1 File Offset: 0x000EBDA1
		public string Countersignature
		{
			[CompilerGenerated]
			get
			{
				return this.<Countersignature>k__BackingField;
			}
		}

		// Token: 0x04002DE2 RID: 11746
		[CompilerGenerated]
		private readonly string <PublicKey>k__BackingField;

		// Token: 0x04002DE3 RID: 11747
		[CompilerGenerated]
		private readonly string <Countersignature>k__BackingField;
	}
}
