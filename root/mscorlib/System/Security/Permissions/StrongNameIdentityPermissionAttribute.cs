using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x0200042E RID: 1070
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002D17 RID: 11543 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public StrongNameIdentityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000A30DC File Offset: 0x000A12DC
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x000A30E4 File Offset: 0x000A12E4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000A30ED File Offset: 0x000A12ED
		// (set) Token: 0x06002D1B RID: 11547 RVA: 0x000A30F5 File Offset: 0x000A12F5
		public string PublicKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000A30FE File Offset: 0x000A12FE
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000A3106 File Offset: 0x000A1306
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000A3110 File Offset: 0x000A1310
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.name == null && this.key == null && this.version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.key == null)
			{
				throw new ArgumentException(Locale.GetText("PublicKey is required"));
			}
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = StrongNamePublicKeyBlob.FromString(this.key);
			Version version = null;
			if (this.version != null)
			{
				version = new Version(this.version);
			}
			return new StrongNameIdentityPermission(strongNamePublicKeyBlob, this.name, version);
		}

		// Token: 0x04001F7F RID: 8063
		private string name;

		// Token: 0x04001F80 RID: 8064
		private string key;

		// Token: 0x04001F81 RID: 8065
		private string version;
	}
}
