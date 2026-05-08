using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004DC RID: 1244
	public sealed class CommonAce : QualifiedAce
	{
		// Token: 0x060032E0 RID: 13024 RVA: 0x000BC358 File Offset: 0x000BA558
		public CommonAce(AceFlags flags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, bool isCallback, byte[] opaque)
			: base(CommonAce.ConvertType(qualifier, isCallback), flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000BC37A File Offset: 0x000BA57A
		internal CommonAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque)
			: base(type, flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000BC398 File Offset: 0x000BA598
		internal CommonAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
			int num = (int)GenericAce.ReadUShort(binaryForm, offset + 2);
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACE - truncated", "binaryForm");
			}
			if (num < 8 + SecurityIdentifier.MinBinaryLength)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			base.AccessMask = GenericAce.ReadInt(binaryForm, offset + 4);
			base.SecurityIdentifier = new SecurityIdentifier(binaryForm, offset + 8);
			int num2 = num - (8 + base.SecurityIdentifier.BinaryLength);
			if (num2 > 0)
			{
				byte[] array = new byte[num2];
				Array.Copy(binaryForm, offset + 8 + base.SecurityIdentifier.BinaryLength, array, 0, num2);
				base.SetOpaque(array);
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060032E3 RID: 13027 RVA: 0x000BC441 File Offset: 0x000BA641
		public override int BinaryLength
		{
			get
			{
				return 8 + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
			}
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000BC458 File Offset: 0x000BA658
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			ushort binaryLength = (ushort)this.BinaryLength;
			binaryForm[offset] = (byte)base.AceType;
			binaryForm[offset + 1] = (byte)base.AceFlags;
			GenericAce.WriteUShort(binaryLength, binaryForm, offset + 2);
			GenericAce.WriteInt(base.AccessMask, binaryForm, offset + 4);
			base.SecurityIdentifier.GetBinaryForm(binaryForm, offset + 8);
			byte[] opaque = base.GetOpaque();
			if (opaque != null)
			{
				Array.Copy(opaque, 0, binaryForm, offset + 8 + base.SecurityIdentifier.BinaryLength, opaque.Length);
			}
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000BC4CB File Offset: 0x000BA6CB
		public static int MaxOpaqueLength(bool isCallback)
		{
			return 65459;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000BC4D4 File Offset: 0x000BA6D4
		internal override string GetSddlForm()
		{
			if (base.OpaqueLength != 0)
			{
				throw new NotImplementedException("Unable to convert conditional ACEs to SDDL");
			}
			return string.Format(CultureInfo.InvariantCulture, "({0};{1};{2};;;{3})", new object[]
			{
				GenericAce.GetSddlAceType(base.AceType),
				GenericAce.GetSddlAceFlags(base.AceFlags),
				KnownAce.GetSddlAccessRights(base.AccessMask),
				base.SecurityIdentifier.GetSddlForm()
			});
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000BC544 File Offset: 0x000BA744
		private static AceType ConvertType(AceQualifier qualifier, bool isCallback)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (isCallback)
				{
					return AceType.AccessAllowedCallback;
				}
				return AceType.AccessAllowed;
			case AceQualifier.AccessDenied:
				if (isCallback)
				{
					return AceType.AccessDeniedCallback;
				}
				return AceType.AccessDenied;
			case AceQualifier.SystemAudit:
				if (isCallback)
				{
					return AceType.SystemAuditCallback;
				}
				return AceType.SystemAudit;
			case AceQualifier.SystemAlarm:
				if (isCallback)
				{
					return AceType.SystemAlarmCallback;
				}
				return AceType.SystemAlarm;
			default:
				throw new ArgumentException("Unrecognized ACE qualifier: " + qualifier.ToString(), "qualifier");
			}
		}
	}
}
