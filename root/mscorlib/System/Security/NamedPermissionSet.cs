using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	// Token: 0x020003B1 RID: 945
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x06002868 RID: 10344 RVA: 0x0009354E File Offset: 0x0009174E
		internal NamedPermissionSet()
		{
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x00093556 File Offset: 0x00091756
		public NamedPermissionSet(string name, PermissionSet permSet)
			: base(permSet)
		{
			this.Name = name;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x00093566 File Offset: 0x00091766
		public NamedPermissionSet(string name, PermissionState state)
			: base(state)
		{
			this.Name = name;
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x00093576 File Offset: 0x00091776
		public NamedPermissionSet(NamedPermissionSet permSet)
			: base(permSet)
		{
			this.name = permSet.name;
			this.description = permSet.description;
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x00093597 File Offset: 0x00091797
		public NamedPermissionSet(string name)
			: this(name, PermissionState.Unrestricted)
		{
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x000935A1 File Offset: 0x000917A1
		// (set) Token: 0x0600286E RID: 10350 RVA: 0x000935A9 File Offset: 0x000917A9
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600286F RID: 10351 RVA: 0x000935B2 File Offset: 0x000917B2
		// (set) Token: 0x06002870 RID: 10352 RVA: 0x000935BA File Offset: 0x000917BA
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new ArgumentException(Locale.GetText("invalid name"));
				}
				this.name = value;
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x000935E3 File Offset: 0x000917E3
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000935EB File Offset: 0x000917EB
		public NamedPermissionSet Copy(string name)
		{
			return new NamedPermissionSet(this)
			{
				Name = name
			};
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000935FA File Offset: 0x000917FA
		public override void FromXml(SecurityElement et)
		{
			base.FromXml(et);
			this.name = et.Attribute("Name");
			this.description = et.Attribute("Description");
			if (this.description == null)
			{
				this.description = string.Empty;
			}
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x00093638 File Offset: 0x00091838
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml();
			if (this.name != null)
			{
				securityElement.AddAttribute("Name", this.name);
			}
			if (this.description != null)
			{
				securityElement.AddAttribute("Description", this.description);
			}
			return securityElement;
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00093680 File Offset: 0x00091880
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			NamedPermissionSet namedPermissionSet = obj as NamedPermissionSet;
			return namedPermissionSet != null && this.name == namedPermissionSet.Name && base.Equals(obj);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x000936BC File Offset: 0x000918BC
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			if (this.name != null)
			{
				num ^= this.name.GetHashCode();
			}
			return num;
		}

		// Token: 0x04001D8A RID: 7562
		private string name;

		// Token: 0x04001D8B RID: 7563
		private string description;
	}
}
