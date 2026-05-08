using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace System.Security.AccessControl
{
	// Token: 0x020004FC RID: 1276
	public abstract class GenericSecurityDescriptor
	{
		// Token: 0x06003407 RID: 13319 RVA: 0x000025BE File Offset: 0x000007BE
		protected GenericSecurityDescriptor()
		{
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000BEE94 File Offset: 0x000BD094
		public int BinaryLength
		{
			get
			{
				int num = 20;
				if (this.Owner != null)
				{
					num += this.Owner.BinaryLength;
				}
				if (this.Group != null)
				{
					num += this.Group.BinaryLength;
				}
				if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
				{
					num += this.InternalDacl.BinaryLength;
				}
				if (this.SaclPresent)
				{
					num += this.InternalSacl.BinaryLength;
				}
				return num;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06003409 RID: 13321
		public abstract ControlFlags ControlFlags { get; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600340A RID: 13322
		// (set) Token: 0x0600340B RID: 13323
		public abstract SecurityIdentifier Group { get; set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600340C RID: 13324
		// (set) Token: 0x0600340D RID: 13325
		public abstract SecurityIdentifier Owner { get; set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static byte Revision
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal virtual GenericAcl InternalDacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal virtual GenericAcl InternalSacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x0000408A File Offset: 0x0000228A
		internal virtual byte InternalReservedField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000BEF14 File Offset: 0x000BD114
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			int binaryLength = this.BinaryLength;
			if (offset < 0 || offset > binaryForm.Length - binaryLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			ControlFlags controlFlags = this.ControlFlags;
			if (this.DaclIsUnmodifiedAefa)
			{
				controlFlags &= ~ControlFlags.DiscretionaryAclPresent;
			}
			binaryForm[offset] = GenericSecurityDescriptor.Revision;
			binaryForm[offset + 1] = this.InternalReservedField;
			this.WriteUShort((ushort)controlFlags, binaryForm, offset + 2);
			int num = 20;
			if (this.Owner != null)
			{
				this.WriteInt(num, binaryForm, offset + 4);
				this.Owner.GetBinaryForm(binaryForm, offset + num);
				num += this.Owner.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 4);
			}
			if (this.Group != null)
			{
				this.WriteInt(num, binaryForm, offset + 8);
				this.Group.GetBinaryForm(binaryForm, offset + num);
				num += this.Group.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 8);
			}
			GenericAcl internalSacl = this.InternalSacl;
			if (this.SaclPresent)
			{
				this.WriteInt(num, binaryForm, offset + 12);
				internalSacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalSacl.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 12);
			}
			GenericAcl internalDacl = this.InternalDacl;
			if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				this.WriteInt(num, binaryForm, offset + 16);
				internalDacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalDacl.BinaryLength;
				return;
			}
			this.WriteInt(0, binaryForm, offset + 16);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000BF094 File Offset: 0x000BD294
		public string GetSddlForm(AccessControlSections includeSections)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this.Owner != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "O:{0}", this.Owner.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this.Group != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "G:{0}", this.Group.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "D:{0}", this.InternalDacl.GetSddlForm(this.ControlFlags, true));
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.SaclPresent)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "S:{0}", this.InternalSacl.GetSddlForm(this.ControlFlags, false));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x00003FB7 File Offset: 0x000021B7
		public static bool IsSddlConversionSupported()
		{
			return true;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x0000408A File Offset: 0x0000228A
		internal virtual bool DaclIsUnmodifiedAefa
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000BF173 File Offset: 0x000BD373
		private bool DaclPresent
		{
			get
			{
				return this.InternalDacl != null && (this.ControlFlags & ControlFlags.DiscretionaryAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06003417 RID: 13335 RVA: 0x000BF18A File Offset: 0x000BD38A
		private bool SaclPresent
		{
			get
			{
				return this.InternalSacl != null && (this.ControlFlags & ControlFlags.SystemAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000BF1A2 File Offset: 0x000BD3A2
		private void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000BF1B2 File Offset: 0x000BD3B2
		private void WriteInt(int val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}
	}
}
