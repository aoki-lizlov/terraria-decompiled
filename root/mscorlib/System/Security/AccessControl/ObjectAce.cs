using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200050C RID: 1292
	public sealed class ObjectAce : QualifiedAce
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x000BFA14 File Offset: 0x000BDC14
		public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque)
			: base(ObjectAce.ConvertType(qualifier, isCallback), aceFlags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
			this.ObjectAceFlags = flags;
			this.ObjectAceType = type;
			this.InheritedObjectAceType = inheritedType;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000BFA4E File Offset: 0x000BDC4E
		internal ObjectAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, ObjectAceFlags objFlags, Guid objType, Guid inheritedType, byte[] opaque)
			: base(type, flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
			this.ObjectAceFlags = objFlags;
			this.ObjectAceType = objType;
			this.InheritedObjectAceType = inheritedType;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000BFA84 File Offset: 0x000BDC84
		internal ObjectAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
			int num = (int)GenericAce.ReadUShort(binaryForm, offset + 2);
			int num2 = 12 + SecurityIdentifier.MinBinaryLength;
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACE - truncated", "binaryForm");
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			base.AccessMask = GenericAce.ReadInt(binaryForm, offset + 4);
			this.ObjectAceFlags = (ObjectAceFlags)GenericAce.ReadInt(binaryForm, offset + 8);
			if (this.ObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			int num3 = 12;
			if (this.ObjectAceTypePresent)
			{
				this.ObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				this.InheritedObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			base.SecurityIdentifier = new SecurityIdentifier(binaryForm, offset + num3);
			num3 += base.SecurityIdentifier.BinaryLength;
			int num4 = num - num3;
			if (num4 > 0)
			{
				byte[] array = new byte[num4];
				Array.Copy(binaryForm, offset + num3, array, 0, num4);
				base.SetOpaque(array);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x000BFBA4 File Offset: 0x000BDDA4
		public override int BinaryLength
		{
			get
			{
				int num = 12 + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
				if (this.ObjectAceTypePresent)
				{
					num += 16;
				}
				if (this.InheritedObjectAceTypePresent)
				{
					num += 16;
				}
				return num;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000BFBE2 File Offset: 0x000BDDE2
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x000BFBEA File Offset: 0x000BDDEA
		public Guid InheritedObjectAceType
		{
			get
			{
				return this.inherited_object_type;
			}
			set
			{
				this.inherited_object_type = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000BFBF3 File Offset: 0x000BDDF3
		private bool InheritedObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000BFC00 File Offset: 0x000BDE00
		// (set) Token: 0x0600347A RID: 13434 RVA: 0x000BFC08 File Offset: 0x000BDE08
		public ObjectAceFlags ObjectAceFlags
		{
			get
			{
				return this.object_ace_flags;
			}
			set
			{
				this.object_ace_flags = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000BFC11 File Offset: 0x000BDE11
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000BFC19 File Offset: 0x000BDE19
		public Guid ObjectAceType
		{
			get
			{
				return this.object_ace_type;
			}
			set
			{
				this.object_ace_type = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000BFC22 File Offset: 0x000BDE22
		private bool ObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000BFC30 File Offset: 0x000BDE30
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			ushort binaryLength = (ushort)this.BinaryLength;
			binaryForm[offset++] = (byte)base.AceType;
			binaryForm[offset++] = (byte)base.AceFlags;
			GenericAce.WriteUShort(binaryLength, binaryForm, offset);
			offset += 2;
			GenericAce.WriteInt(base.AccessMask, binaryForm, offset);
			offset += 4;
			GenericAce.WriteInt((int)this.ObjectAceFlags, binaryForm, offset);
			offset += 4;
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.ObjectAceType, binaryForm, offset);
				offset += 16;
			}
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.InheritedObjectAceType, binaryForm, offset);
				offset += 16;
			}
			base.SecurityIdentifier.GetBinaryForm(binaryForm, offset);
			offset += base.SecurityIdentifier.BinaryLength;
			byte[] opaque = base.GetOpaque();
			if (opaque != null)
			{
				Array.Copy(opaque, 0, binaryForm, offset, opaque.Length);
				offset += opaque.Length;
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000BFD05 File Offset: 0x000BDF05
		public static int MaxOpaqueLength(bool isCallback)
		{
			return 65423;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000BFD0C File Offset: 0x000BDF0C
		internal override string GetSddlForm()
		{
			if (base.OpaqueLength != 0)
			{
				throw new NotImplementedException("Unable to convert conditional ACEs to SDDL");
			}
			string text = "";
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				text = this.object_ace_type.ToString("D");
			}
			string text2 = "";
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				text2 = this.inherited_object_type.ToString("D");
			}
			return string.Format(CultureInfo.InvariantCulture, "({0};{1};{2};{3};{4};{5})", new object[]
			{
				GenericAce.GetSddlAceType(base.AceType),
				GenericAce.GetSddlAceFlags(base.AceFlags),
				KnownAce.GetSddlAccessRights(base.AccessMask),
				text,
				text2,
				base.SecurityIdentifier.GetSddlForm()
			});
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000BFDC4 File Offset: 0x000BDFC4
		private static AceType ConvertType(AceQualifier qualifier, bool isCallback)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (isCallback)
				{
					return AceType.AccessAllowedCallbackObject;
				}
				return AceType.AccessAllowedObject;
			case AceQualifier.AccessDenied:
				if (isCallback)
				{
					return AceType.AccessDeniedCallbackObject;
				}
				return AceType.AccessDeniedObject;
			case AceQualifier.SystemAudit:
				if (isCallback)
				{
					return AceType.SystemAuditCallbackObject;
				}
				return AceType.SystemAuditObject;
			case AceQualifier.SystemAlarm:
				if (isCallback)
				{
					return AceType.SystemAlarmCallbackObject;
				}
				return AceType.SystemAlarmObject;
			default:
				throw new ArgumentException("Unrecognized ACE qualifier: " + qualifier.ToString(), "qualifier");
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000BFE2A File Offset: 0x000BE02A
		private void WriteGuid(Guid val, byte[] buffer, int offset)
		{
			Array.Copy(val.ToByteArray(), 0, buffer, offset, 16);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000BFE40 File Offset: 0x000BE040
		private Guid ReadGuid(byte[] buffer, int offset)
		{
			byte[] array = new byte[16];
			Array.Copy(buffer, offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x04002449 RID: 9289
		private Guid object_ace_type;

		// Token: 0x0400244A RID: 9290
		private Guid inherited_object_type;

		// Token: 0x0400244B RID: 9291
		private ObjectAceFlags object_ace_flags;
	}
}
