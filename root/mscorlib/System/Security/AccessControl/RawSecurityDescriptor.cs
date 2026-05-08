using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000514 RID: 1300
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x060034F3 RID: 13555 RVA: 0x000C0EC9 File Offset: 0x000BF0C9
		public RawSecurityDescriptor(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this.ParseSddl(sddlForm.Replace(" ", ""));
			this.control_flags |= ControlFlags.SelfRelative;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000C0F08 File Offset: 0x000BF108
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 20)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (binaryForm[offset] != 1)
			{
				throw new ArgumentException("Unrecognized Security Descriptor revision.", "binaryForm");
			}
			this.resourcemgr_control = binaryForm[offset + 1];
			this.control_flags = (ControlFlags)this.ReadUShort(binaryForm, offset + 2);
			int num = this.ReadInt(binaryForm, offset + 4);
			int num2 = this.ReadInt(binaryForm, offset + 8);
			int num3 = this.ReadInt(binaryForm, offset + 12);
			int num4 = this.ReadInt(binaryForm, offset + 16);
			if (num != 0)
			{
				this.owner_sid = new SecurityIdentifier(binaryForm, num);
			}
			if (num2 != 0)
			{
				this.group_sid = new SecurityIdentifier(binaryForm, num2);
			}
			if (num3 != 0)
			{
				this.system_acl = new RawAcl(binaryForm, num3);
			}
			if (num4 != 0)
			{
				this.discretionary_acl = new RawAcl(binaryForm, num4);
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000C0FEB File Offset: 0x000BF1EB
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.control_flags = flags;
			this.owner_sid = owner;
			this.group_sid = group;
			this.system_acl = systemAcl;
			this.discretionary_acl = discretionaryAcl;
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000C1018 File Offset: 0x000BF218
		public override ControlFlags ControlFlags
		{
			get
			{
				return this.control_flags;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000C1020 File Offset: 0x000BF220
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000C1028 File Offset: 0x000BF228
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				this.discretionary_acl = value;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000C1031 File Offset: 0x000BF231
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000C1039 File Offset: 0x000BF239
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group_sid;
			}
			set
			{
				this.group_sid = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000C1042 File Offset: 0x000BF242
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000C104A File Offset: 0x000BF24A
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner_sid;
			}
			set
			{
				this.owner_sid = value;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000C1053 File Offset: 0x000BF253
		// (set) Token: 0x060034FE RID: 13566 RVA: 0x000C105B File Offset: 0x000BF25B
		public byte ResourceManagerControl
		{
			get
			{
				return this.resourcemgr_control;
			}
			set
			{
				this.resourcemgr_control = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x000C1064 File Offset: 0x000BF264
		// (set) Token: 0x06003500 RID: 13568 RVA: 0x000C106C File Offset: 0x000BF26C
		public RawAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				this.system_acl = value;
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000C1075 File Offset: 0x000BF275
		public void SetFlags(ControlFlags flags)
		{
			this.control_flags = flags | ControlFlags.SelfRelative;
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x000C1084 File Offset: 0x000BF284
		internal override GenericAcl InternalDacl
		{
			get
			{
				return this.DiscretionaryAcl;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x000C108C File Offset: 0x000BF28C
		internal override GenericAcl InternalSacl
		{
			get
			{
				return this.SystemAcl;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06003504 RID: 13572 RVA: 0x000C1094 File Offset: 0x000BF294
		internal override byte InternalReservedField
		{
			get
			{
				return this.ResourceManagerControl;
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000C109C File Offset: 0x000BF29C
		private void ParseSddl(string sddlForm)
		{
			ControlFlags controlFlags = ControlFlags.None;
			int i = 0;
			while (i < sddlForm.Length - 2)
			{
				string text = sddlForm.Substring(i, 2);
				if (!(text == "O:"))
				{
					if (!(text == "G:"))
					{
						if (!(text == "D:"))
						{
							if (!(text == "S:"))
							{
								throw new ArgumentException("Invalid SDDL.", "sddlForm");
							}
							i += 2;
							this.SystemAcl = RawAcl.ParseSddlForm(sddlForm, false, ref controlFlags, ref i);
							controlFlags |= ControlFlags.SystemAclPresent;
						}
						else
						{
							i += 2;
							this.DiscretionaryAcl = RawAcl.ParseSddlForm(sddlForm, true, ref controlFlags, ref i);
							controlFlags |= ControlFlags.DiscretionaryAclPresent;
						}
					}
					else
					{
						i += 2;
						this.Group = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
					}
				}
				else
				{
					i += 2;
					this.Owner = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
				}
			}
			if (i != sddlForm.Length)
			{
				throw new ArgumentException("Invalid SDDL.", "sddlForm");
			}
			this.SetFlags(controlFlags);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000C0EBB File Offset: 0x000BF0BB
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000C118E File Offset: 0x000BF38E
		private int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24);
		}

		// Token: 0x0400245C RID: 9308
		private ControlFlags control_flags;

		// Token: 0x0400245D RID: 9309
		private SecurityIdentifier owner_sid;

		// Token: 0x0400245E RID: 9310
		private SecurityIdentifier group_sid;

		// Token: 0x0400245F RID: 9311
		private RawAcl system_acl;

		// Token: 0x04002460 RID: 9312
		private RawAcl discretionary_acl;

		// Token: 0x04002461 RID: 9313
		private byte resourcemgr_control;
	}
}
