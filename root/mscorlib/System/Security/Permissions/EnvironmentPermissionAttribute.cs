using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000405 RID: 1029
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002B50 RID: 11088 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public EnvironmentPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x0009DE15 File Offset: 0x0009C015
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x0009DE21 File Offset: 0x0009C021
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.read = value;
				this.write = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x0009DE31 File Offset: 0x0009C031
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x0009DE39 File Offset: 0x0009C039
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x0009DE42 File Offset: 0x0009C042
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x0009DE4A File Offset: 0x0009C04A
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0009DE54 File Offset: 0x0009C054
		public override IPermission CreatePermission()
		{
			EnvironmentPermission environmentPermission;
			if (base.Unrestricted)
			{
				environmentPermission = new EnvironmentPermission(PermissionState.Unrestricted);
			}
			else
			{
				environmentPermission = new EnvironmentPermission(PermissionState.None);
				if (this.read != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					environmentPermission.AddPathList(EnvironmentPermissionAccess.Write, this.write);
				}
			}
			return environmentPermission;
		}

		// Token: 0x04001ED8 RID: 7896
		private string read;

		// Token: 0x04001ED9 RID: 7897
		private string write;
	}
}
