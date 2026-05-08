using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting
{
	// Token: 0x0200054B RID: 1355
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x000C637D File Offset: 0x000C457D
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_uri = objectUri;
			this.obj_mode = mode;
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000C63B8 File Offset: 0x000C45B8
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			this.obj_uri = objectUri;
			this.obj_mode = mode;
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		// (set) Token: 0x0600369B RID: 13979 RVA: 0x00004088 File Offset: 0x00002288
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

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000C6421 File Offset: 0x000C4621
		public WellKnownObjectMode Mode
		{
			get
			{
				return this.obj_mode;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000C6429 File Offset: 0x000C4629
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000C6431 File Offset: 0x000C4631
		public string ObjectUri
		{
			get
			{
				return this.obj_uri;
			}
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000C6439 File Offset: 0x000C4639
		public override string ToString()
		{
			return string.Concat(new string[] { base.TypeName, ", ", base.AssemblyName, " ", this.ObjectUri });
		}

		// Token: 0x040024F8 RID: 9464
		private Type obj_type;

		// Token: 0x040024F9 RID: 9465
		private string obj_uri;

		// Token: 0x040024FA RID: 9466
		private WellKnownObjectMode obj_mode;
	}
}
