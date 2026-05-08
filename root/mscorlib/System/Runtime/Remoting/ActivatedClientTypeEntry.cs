using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	// Token: 0x02000529 RID: 1321
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		// Token: 0x06003569 RID: 13673 RVA: 0x000C1C49 File Offset: 0x000BFE49
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.applicationUrl = appUrl;
			this.obj_type = type;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000C1C7C File Offset: 0x000BFE7C
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			this.applicationUrl = appUrl;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000C1CDD File Offset: 0x000BFEDD
		public string ApplicationUrl
		{
			get
			{
				return this.applicationUrl;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x0600356D RID: 13677 RVA: 0x00004088 File Offset: 0x00002288
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

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000C1CE5 File Offset: 0x000BFEE5
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000C1CED File Offset: 0x000BFEED
		public override string ToString()
		{
			return base.TypeName + base.AssemblyName + this.ApplicationUrl;
		}

		// Token: 0x04002498 RID: 9368
		private string applicationUrl;

		// Token: 0x04002499 RID: 9369
		private Type obj_type;
	}
}
