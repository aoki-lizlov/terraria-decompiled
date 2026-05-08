using System;

namespace System.Reflection
{
	// Token: 0x02000857 RID: 2135
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		// Token: 0x060047F4 RID: 18420 RVA: 0x000EDAEA File Offset: 0x000EBCEA
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public AssemblyFlagsAttribute(uint flags)
		{
			this._flags = (AssemblyNameFlags)flags;
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060047F5 RID: 18421 RVA: 0x000EDAF9 File Offset: 0x000EBCF9
		[Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public uint Flags
		{
			get
			{
				return (uint)this._flags;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x000EDAF9 File Offset: 0x000EBCF9
		public int AssemblyFlags
		{
			get
			{
				return (int)this._flags;
			}
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x000EDAEA File Offset: 0x000EBCEA
		[Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this._flags = (AssemblyNameFlags)assemblyFlags;
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x000EDAEA File Offset: 0x000EBCEA
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this._flags = assemblyFlags;
		}

		// Token: 0x04002DD5 RID: 11733
		private AssemblyNameFlags _flags;
	}
}
