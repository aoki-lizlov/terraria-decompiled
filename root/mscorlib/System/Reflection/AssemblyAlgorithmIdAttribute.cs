using System;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200084D RID: 2125
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		// Token: 0x060047E1 RID: 18401 RVA: 0x000EDA0D File Offset: 0x000EBC0D
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.AlgorithmId = (uint)algorithmId;
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x000EDA0D File Offset: 0x000EBC0D
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060047E3 RID: 18403 RVA: 0x000EDA1C File Offset: 0x000EBC1C
		[CLSCompliant(false)]
		public uint AlgorithmId
		{
			[CompilerGenerated]
			get
			{
				return this.<AlgorithmId>k__BackingField;
			}
		}

		// Token: 0x04002DC9 RID: 11721
		[CompilerGenerated]
		private readonly uint <AlgorithmId>k__BackingField;
	}
}
