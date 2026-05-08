using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000801 RID: 2049
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
	[ComVisible(true)]
	[Serializable]
	public class CompilationRelaxationsAttribute : Attribute
	{
		// Token: 0x06004644 RID: 17988 RVA: 0x000E6B3B File Offset: 0x000E4D3B
		public CompilationRelaxationsAttribute(int relaxations)
		{
			this.m_relaxations = relaxations;
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000E6B3B File Offset: 0x000E4D3B
		public CompilationRelaxationsAttribute(CompilationRelaxations relaxations)
		{
			this.m_relaxations = (int)relaxations;
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x000E6B4A File Offset: 0x000E4D4A
		public int CompilationRelaxations
		{
			get
			{
				return this.m_relaxations;
			}
		}

		// Token: 0x04002CFC RID: 11516
		private int m_relaxations;
	}
}
