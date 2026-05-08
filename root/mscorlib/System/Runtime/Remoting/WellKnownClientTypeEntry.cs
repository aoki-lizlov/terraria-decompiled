using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000549 RID: 1353
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		// Token: 0x06003691 RID: 13969 RVA: 0x000C6289 File Offset: 0x000C4489
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			base.AssemblyName = type.Assembly.FullName;
			base.TypeName = type.FullName;
			this.obj_type = type;
			this.obj_url = objectUrl;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000C62BC File Offset: 0x000C44BC
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			this.obj_url = objectUrl;
			base.AssemblyName = assemblyName;
			base.TypeName = typeName;
			Assembly assembly = Assembly.Load(assemblyName);
			this.obj_type = assembly.GetType(typeName);
			if (this.obj_type == null)
			{
				throw new RemotingException("Type not found: " + typeName + ", " + assemblyName);
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000C631D File Offset: 0x000C451D
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x000C6325 File Offset: 0x000C4525
		public string ApplicationUrl
		{
			get
			{
				return this.app_url;
			}
			set
			{
				this.app_url = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x000C632E File Offset: 0x000C452E
		public Type ObjectType
		{
			get
			{
				return this.obj_type;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06003696 RID: 13974 RVA: 0x000C6336 File Offset: 0x000C4536
		public string ObjectUrl
		{
			get
			{
				return this.obj_url;
			}
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000C633E File Offset: 0x000C453E
		public override string ToString()
		{
			if (this.ApplicationUrl != null)
			{
				return base.TypeName + base.AssemblyName + this.ObjectUrl + this.ApplicationUrl;
			}
			return base.TypeName + base.AssemblyName + this.ObjectUrl;
		}

		// Token: 0x040024F2 RID: 9458
		private Type obj_type;

		// Token: 0x040024F3 RID: 9459
		private string obj_url;

		// Token: 0x040024F4 RID: 9460
		private string app_url;
	}
}
