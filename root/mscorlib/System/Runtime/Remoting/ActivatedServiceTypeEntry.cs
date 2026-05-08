using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	// Token: 0x0200052A RID: 1322
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		// Token: 0x06003570 RID: 13680 RVA: 0x000C1D06 File Offset: 0x000BFF06
		public ActivatedServiceTypeEntry(Type type)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000C1D34 File Offset: 0x000BFF34
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x06003573 RID: 13683 RVA: 0x00004088 File Offset: 0x00002288
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000C1D8E File Offset: 0x000BFF8E
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000C1D96 File Offset: 0x000BFF96
		public override string ToString()
		{
			return base.AssemblyName + base.TypeName;
		}

		// Token: 0x0400249A RID: 9370
		private Type obj_type;
	}
}
