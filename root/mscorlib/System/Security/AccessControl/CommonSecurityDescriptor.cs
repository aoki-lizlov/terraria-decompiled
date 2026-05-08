using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004E5 RID: 1253
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x06003333 RID: 13107 RVA: 0x000BD5BC File Offset: 0x000BB7BC
		public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			this.Init(isContainer, isDS, rawSecurityDescriptor);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000BD5CD File Offset: 0x000BB7CD
		public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
		{
			this.Init(isContainer, isDS, new RawSecurityDescriptor(sddlForm));
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000BD5E3 File Offset: 0x000BB7E3
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
		{
			this.Init(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset));
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000BD5FB File Offset: 0x000BB7FB
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.Init(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000BD614 File Offset: 0x000BB814
		private void Init(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			SystemAcl systemAcl = null;
			if (rawSecurityDescriptor.SystemAcl != null)
			{
				systemAcl = new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl);
			}
			DiscretionaryAcl discretionaryAcl = null;
			if (rawSecurityDescriptor.DiscretionaryAcl != null)
			{
				discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl);
			}
			this.Init(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000BD67B File Offset: 0x000BB87B
		private void Init(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.flags = flags & ~ControlFlags.SystemAclPresent;
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.Owner = owner;
			this.Group = group;
			this.SystemAcl = systemAcl;
			this.DiscretionaryAcl = discretionaryAcl;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000BD6B8 File Offset: 0x000BB8B8
		public override ControlFlags ControlFlags
		{
			get
			{
				ControlFlags controlFlags = this.flags;
				controlFlags |= ControlFlags.DiscretionaryAclPresent;
				controlFlags |= ControlFlags.SelfRelative;
				if (this.SystemAcl != null)
				{
					controlFlags |= ControlFlags.SystemAclPresent;
				}
				return controlFlags;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000BD6E6 File Offset: 0x000BB8E6
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x000BD6F0 File Offset: 0x000BB8F0
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				if (value == null)
				{
					value = new DiscretionaryAcl(this.IsContainer, this.IsDS, 1);
					value.AddAccess(AccessControlType.Allow, new SecurityIdentifier("WD"), -1, this.IsContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
					value.IsAefa = true;
				}
				this.CheckAclConsistency(value);
				this.discretionary_acl = value;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000BD748 File Offset: 0x000BB948
		internal override GenericAcl InternalDacl
		{
			get
			{
				return this.DiscretionaryAcl;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000BD750 File Offset: 0x000BB950
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000BD758 File Offset: 0x000BB958
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000BD761 File Offset: 0x000BB961
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000BD769 File Offset: 0x000BB969
		public bool IsDiscretionaryAclCanonical
		{
			get
			{
				return this.DiscretionaryAcl.IsCanonical;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000BD776 File Offset: 0x000BB976
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000BD77E File Offset: 0x000BB97E
		public bool IsSystemAclCanonical
		{
			get
			{
				return this.SystemAcl == null || this.SystemAcl.IsCanonical;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000BD795 File Offset: 0x000BB995
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000BD79D File Offset: 0x000BB99D
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000BD7A6 File Offset: 0x000BB9A6
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000BD7AE File Offset: 0x000BB9AE
		public SystemAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				if (value != null)
				{
					this.CheckAclConsistency(value);
				}
				this.system_acl = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000BD7C1 File Offset: 0x000BB9C1
		internal override GenericAcl InternalSacl
		{
			get
			{
				return this.SystemAcl;
			}
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000BD7C9 File Offset: 0x000BB9C9
		public void PurgeAccessControl(SecurityIdentifier sid)
		{
			this.DiscretionaryAcl.Purge(sid);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000BD7D7 File Offset: 0x000BB9D7
		public void PurgeAudit(SecurityIdentifier sid)
		{
			if (this.SystemAcl != null)
			{
				this.SystemAcl.Purge(sid);
			}
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000BD7F0 File Offset: 0x000BB9F0
		public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
		{
			this.DiscretionaryAcl.IsAefa = false;
			if (!isProtected)
			{
				this.flags &= ~ControlFlags.DiscretionaryAclProtected;
				return;
			}
			this.flags |= ControlFlags.DiscretionaryAclProtected;
			if (!preserveInheritance)
			{
				this.DiscretionaryAcl.RemoveInheritedAces();
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000BD83F File Offset: 0x000BBA3F
		public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.flags &= ~ControlFlags.SystemAclProtected;
				return;
			}
			this.flags |= ControlFlags.SystemAclProtected;
			if (!preserveInheritance && this.SystemAcl != null)
			{
				this.SystemAcl.RemoveInheritedAces();
			}
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000BD87F File Offset: 0x000BBA7F
		public void AddDiscretionaryAcl(byte revision, int trusted)
		{
			this.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.flags |= ControlFlags.DiscretionaryAclPresent;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000BD8A8 File Offset: 0x000BBAA8
		public void AddSystemAcl(byte revision, int trusted)
		{
			this.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.flags |= ControlFlags.SystemAclPresent;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000BD8D2 File Offset: 0x000BBAD2
		private void CheckAclConsistency(CommonAcl acl)
		{
			if (this.IsContainer != acl.IsContainer)
			{
				throw new ArgumentException("IsContainer must match between descriptor and ACL.");
			}
			if (this.IsDS != acl.IsDS)
			{
				throw new ArgumentException("IsDS must match between descriptor and ACL.");
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000BD906 File Offset: 0x000BBB06
		internal override bool DaclIsUnmodifiedAefa
		{
			get
			{
				return this.DiscretionaryAcl.IsAefa;
			}
		}

		// Token: 0x040023DB RID: 9179
		private bool is_container;

		// Token: 0x040023DC RID: 9180
		private bool is_ds;

		// Token: 0x040023DD RID: 9181
		private ControlFlags flags;

		// Token: 0x040023DE RID: 9182
		private SecurityIdentifier owner;

		// Token: 0x040023DF RID: 9183
		private SecurityIdentifier group;

		// Token: 0x040023E0 RID: 9184
		private SystemAcl system_acl;

		// Token: 0x040023E1 RID: 9185
		private DiscretionaryAcl discretionary_acl;
	}
}
