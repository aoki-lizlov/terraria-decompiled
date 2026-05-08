using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000683 RID: 1667
	internal sealed class TypeInformation
	{
		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000DA2FE File Offset: 0x000D84FE
		internal string FullTypeName
		{
			get
			{
				return this.fullTypeName;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x000DA306 File Offset: 0x000D8506
		internal string AssemblyString
		{
			get
			{
				return this.assemblyString;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x000DA30E File Offset: 0x000D850E
		internal bool HasTypeForwardedFrom
		{
			get
			{
				return this.hasTypeForwardedFrom;
			}
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000DA316 File Offset: 0x000D8516
		internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = fullTypeName;
			this.assemblyString = assemblyString;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x040028AE RID: 10414
		private string fullTypeName;

		// Token: 0x040028AF RID: 10415
		private string assemblyString;

		// Token: 0x040028B0 RID: 10416
		private bool hasTypeForwardedFrom;
	}
}
