using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace System.Security.AccessControl
{
	// Token: 0x020004FA RID: 1274
	public abstract class GenericAce
	{
		// Token: 0x060033DA RID: 13274 RVA: 0x000BE4A1 File Offset: 0x000BC6A1
		internal GenericAce(AceType type, AceFlags flags)
		{
			if (type > AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.ace_type = type;
			this.ace_flags = flags;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
		internal GenericAce(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 2)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			this.ace_type = (AceType)binaryForm[offset];
			this.ace_flags = (AceFlags)binaryForm[offset + 1];
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x000BE51F File Offset: 0x000BC71F
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x000BE527 File Offset: 0x000BC727
		public AceFlags AceFlags
		{
			get
			{
				return this.ace_flags;
			}
			set
			{
				this.ace_flags = value;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x000BE530 File Offset: 0x000BC730
		public AceType AceType
		{
			get
			{
				return this.ace_type;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000BE538 File Offset: 0x000BC738
		public AuditFlags AuditFlags
		{
			get
			{
				AuditFlags auditFlags = AuditFlags.None;
				if ((this.ace_flags & AceFlags.SuccessfulAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Success;
				}
				if ((this.ace_flags & AceFlags.FailedAccess) != AceFlags.None)
				{
					auditFlags |= AuditFlags.Failure;
				}
				return auditFlags;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060033E0 RID: 13280
		public abstract int BinaryLength { get; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x000BE56C File Offset: 0x000BC76C
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				InheritanceFlags inheritanceFlags = InheritanceFlags.None;
				if ((this.ace_flags & AceFlags.ObjectInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ObjectInherit;
				}
				if ((this.ace_flags & AceFlags.ContainerInherit) != AceFlags.None)
				{
					inheritanceFlags |= InheritanceFlags.ContainerInherit;
				}
				return inheritanceFlags;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000BE598 File Offset: 0x000BC798
		public bool IsInherited
		{
			get
			{
				return (this.ace_flags & AceFlags.Inherited) > AceFlags.None;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x000BE5A8 File Offset: 0x000BC7A8
		public PropagationFlags PropagationFlags
		{
			get
			{
				PropagationFlags propagationFlags = PropagationFlags.None;
				if ((this.ace_flags & AceFlags.InheritOnly) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.InheritOnly;
				}
				if ((this.ace_flags & AceFlags.NoPropagateInherit) != AceFlags.None)
				{
					propagationFlags |= PropagationFlags.NoPropagateInherit;
				}
				return propagationFlags;
			}
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000BE5D4 File Offset: 0x000BC7D4
		public GenericAce Copy()
		{
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			return GenericAce.CreateFromBinaryForm(array, 0);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000BE5FC File Offset: 0x000BC7FC
		public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 1)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (GenericAce.IsObjectType((AceType)binaryForm[offset]))
			{
				return new ObjectAce(binaryForm, offset);
			}
			return new CommonAce(binaryForm, offset);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000BE652 File Offset: 0x000BC852
		public sealed override bool Equals(object o)
		{
			return this == o as GenericAce;
		}

		// Token: 0x060033E7 RID: 13287
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		// Token: 0x060033E8 RID: 13288 RVA: 0x000BE660 File Offset: 0x000BC860
		public sealed override int GetHashCode()
		{
			byte[] array = new byte[this.BinaryLength];
			this.GetBinaryForm(array, 0);
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num = (num << 3) | ((num >> 29) & 7);
				num ^= (int)(array[i] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000BE6AC File Offset: 0x000BC8AC
		public static bool operator ==(GenericAce left, GenericAce right)
		{
			if (left == null)
			{
				return right == null;
			}
			if (right == null)
			{
				return false;
			}
			int binaryLength = left.BinaryLength;
			int binaryLength2 = right.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return false;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			left.GetBinaryForm(array, 0);
			right.GetBinaryForm(array2, 0);
			for (int i = 0; i < binaryLength; i++)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000BE718 File Offset: 0x000BC918
		public static bool operator !=(GenericAce left, GenericAce right)
		{
			if (left == null)
			{
				return right != null;
			}
			if (right == null)
			{
				return true;
			}
			int binaryLength = left.BinaryLength;
			int binaryLength2 = right.BinaryLength;
			if (binaryLength != binaryLength2)
			{
				return true;
			}
			byte[] array = new byte[binaryLength];
			byte[] array2 = new byte[binaryLength2];
			left.GetBinaryForm(array, 0);
			right.GetBinaryForm(array2, 0);
			for (int i = 0; i < binaryLength; i++)
			{
				if (array[i] != array2[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033EB RID: 13291
		internal abstract string GetSddlForm();

		// Token: 0x060033EC RID: 13292 RVA: 0x000BE784 File Offset: 0x000BC984
		internal static GenericAce CreateFromSddlForm(string sddlForm, ref int pos)
		{
			if (sddlForm[pos] != '(')
			{
				throw new ArgumentException("Invalid SDDL string.", "sddlForm");
			}
			int num = sddlForm.IndexOf(')', pos);
			if (num < 0)
			{
				throw new ArgumentException("Invalid SDDL string.", "sddlForm");
			}
			int num2 = num - (pos + 1);
			string[] array = sddlForm.Substring(pos + 1, num2).ToUpperInvariant().Split(';', StringSplitOptions.None);
			if (array.Length != 6)
			{
				throw new ArgumentException("Invalid SDDL string.", "sddlForm");
			}
			ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
			AceType aceType = GenericAce.ParseSddlAceType(array[0]);
			AceFlags aceFlags = GenericAce.ParseSddlAceFlags(array[1]);
			int num3 = GenericAce.ParseSddlAccessRights(array[2]);
			Guid empty = Guid.Empty;
			if (!string.IsNullOrEmpty(array[3]))
			{
				empty = new Guid(array[3]);
				objectAceFlags |= ObjectAceFlags.ObjectAceTypePresent;
			}
			Guid empty2 = Guid.Empty;
			if (!string.IsNullOrEmpty(array[4]))
			{
				empty2 = new Guid(array[4]);
				objectAceFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(array[5]);
			if (aceType == AceType.AccessAllowedCallback || aceType == AceType.AccessDeniedCallback)
			{
				throw new NotImplementedException("Conditional ACEs not supported");
			}
			pos = num + 1;
			if (GenericAce.IsObjectType(aceType))
			{
				return new ObjectAce(aceType, aceFlags, num3, securityIdentifier, objectAceFlags, empty, empty2, null);
			}
			if (objectAceFlags != ObjectAceFlags.None)
			{
				throw new ArgumentException("Invalid SDDL string.", "sddlForm");
			}
			return new CommonAce(aceType, aceFlags, num3, securityIdentifier, null);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000BE8C4 File Offset: 0x000BCAC4
		private static bool IsObjectType(AceType type)
		{
			return type == AceType.AccessAllowedCallbackObject || type == AceType.AccessAllowedObject || type == AceType.AccessDeniedCallbackObject || type == AceType.AccessDeniedObject || type == AceType.SystemAlarmCallbackObject || type == AceType.SystemAlarmObject || type == AceType.SystemAuditCallbackObject || type == AceType.SystemAuditObject;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000BE8EC File Offset: 0x000BCAEC
		internal static string GetSddlAceType(AceType type)
		{
			switch (type)
			{
			case AceType.AccessAllowed:
				return "A";
			case AceType.AccessDenied:
				return "D";
			case AceType.SystemAudit:
				return "AU";
			case AceType.SystemAlarm:
				return "AL";
			case AceType.AccessAllowedObject:
				return "OA";
			case AceType.AccessDeniedObject:
				return "OD";
			case AceType.SystemAuditObject:
				return "OU";
			case AceType.SystemAlarmObject:
				return "OL";
			case AceType.AccessAllowedCallback:
				return "XA";
			case AceType.AccessDeniedCallback:
				return "XD";
			}
			throw new ArgumentException("Unable to convert to SDDL ACE type: " + type.ToString(), "type");
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000BE98C File Offset: 0x000BCB8C
		private static AceType ParseSddlAceType(string type)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(type);
			if (num <= 2078582897U)
			{
				if (num <= 936719067U)
				{
					if (num != 517278592U)
					{
						if (num == 936719067U)
						{
							if (type == "AU")
							{
								return AceType.SystemAudit;
							}
						}
					}
					else if (type == "AL")
					{
						return AceType.SystemAlarm;
					}
				}
				else if (num != 1561581017U)
				{
					if (num != 1611913874U)
					{
						if (num == 2078582897U)
						{
							if (type == "OU")
							{
								return AceType.SystemAuditObject;
							}
						}
					}
					else if (type == "XA")
					{
						return AceType.AccessAllowedCallback;
					}
				}
				else if (type == "XD")
				{
					return AceType.AccessDeniedCallback;
				}
			}
			else if (num <= 2330247182U)
			{
				if (num != 2196026230U)
				{
					if (num == 2330247182U)
					{
						if (type == "OD")
						{
							return AceType.AccessDeniedObject;
						}
					}
				}
				else if (type == "OL")
				{
					return AceType.SystemAlarmObject;
				}
			}
			else if (num != 2414135277U)
			{
				if (num != 3238785555U)
				{
					if (num == 3289118412U)
					{
						if (type == "A")
						{
							return AceType.AccessAllowed;
						}
					}
				}
				else if (type == "D")
				{
					return AceType.AccessDenied;
				}
			}
			else if (type == "OA")
			{
				return AceType.AccessAllowedObject;
			}
			throw new ArgumentException("Unable to convert SDDL to ACE type: " + type, "type");
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000BEAF8 File Offset: 0x000BCCF8
		internal static string GetSddlAceFlags(AceFlags flags)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((flags & AceFlags.ObjectInherit) != AceFlags.None)
			{
				stringBuilder.Append("OI");
			}
			if ((flags & AceFlags.ContainerInherit) != AceFlags.None)
			{
				stringBuilder.Append("CI");
			}
			if ((flags & AceFlags.NoPropagateInherit) != AceFlags.None)
			{
				stringBuilder.Append("NP");
			}
			if ((flags & AceFlags.InheritOnly) != AceFlags.None)
			{
				stringBuilder.Append("IO");
			}
			if ((flags & AceFlags.Inherited) != AceFlags.None)
			{
				stringBuilder.Append("ID");
			}
			if ((flags & AceFlags.SuccessfulAccess) != AceFlags.None)
			{
				stringBuilder.Append("SA");
			}
			if ((flags & AceFlags.FailedAccess) != AceFlags.None)
			{
				stringBuilder.Append("FA");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000BEB90 File Offset: 0x000BCD90
		private static AceFlags ParseSddlAceFlags(string flags)
		{
			AceFlags aceFlags = AceFlags.None;
			int i = 0;
			while (i < flags.Length - 1)
			{
				string text = flags.Substring(i, 2);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1476560089U)
				{
					if (num != 619077139U)
					{
						if (num != 1458105184U)
						{
							if (num != 1476560089U)
							{
								goto IL_0112;
							}
							if (!(text == "SA"))
							{
								goto IL_0112;
							}
							aceFlags |= AceFlags.SuccessfulAccess;
						}
						else
						{
							if (!(text == "ID"))
							{
								goto IL_0112;
							}
							aceFlags |= AceFlags.Inherited;
						}
					}
					else
					{
						if (!(text == "NP"))
						{
							goto IL_0112;
						}
						aceFlags |= AceFlags.NoPropagateInherit;
					}
				}
				else if (num <= 2145001825U)
				{
					if (num != 1642658993U)
					{
						if (num != 2145001825U)
						{
							goto IL_0112;
						}
						if (!(text == "CI"))
						{
							goto IL_0112;
						}
						aceFlags |= AceFlags.ContainerInherit;
					}
					else
					{
						if (!(text == "IO"))
						{
							goto IL_0112;
						}
						aceFlags |= AceFlags.InheritOnly;
					}
				}
				else if (num != 2211671016U)
				{
					if (num != 2279914325U)
					{
						goto IL_0112;
					}
					if (!(text == "OI"))
					{
						goto IL_0112;
					}
					aceFlags |= AceFlags.ObjectInherit;
				}
				else
				{
					if (!(text == "FA"))
					{
						goto IL_0112;
					}
					aceFlags |= AceFlags.FailedAccess;
				}
				i += 2;
				continue;
				IL_0112:
				throw new ArgumentException("Invalid SDDL string.", "flags");
			}
			if (i != flags.Length)
			{
				throw new ArgumentException("Invalid SDDL string.", "flags");
			}
			return aceFlags;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000BECEC File Offset: 0x000BCEEC
		private static int ParseSddlAccessRights(string accessMask)
		{
			if (accessMask.StartsWith("0X"))
			{
				return int.Parse(accessMask.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			}
			if (char.IsDigit(accessMask, 0))
			{
				return int.Parse(accessMask, NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			return GenericAce.ParseSddlAliasRights(accessMask);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000BED3C File Offset: 0x000BCF3C
		private static int ParseSddlAliasRights(string accessMask)
		{
			int num = 0;
			int i;
			for (i = 0; i < accessMask.Length - 1; i += 2)
			{
				SddlAccessRight sddlAccessRight = SddlAccessRight.LookupByName(accessMask.Substring(i, 2));
				if (sddlAccessRight == null)
				{
					throw new ArgumentException("Invalid SDDL string.", "accessMask");
				}
				num |= sddlAccessRight.Value;
			}
			if (i != accessMask.Length)
			{
				throw new ArgumentException("Invalid SDDL string.", "accessMask");
			}
			return num;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000BEDA2 File Offset: 0x000BCFA2
		internal static ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000BEDB0 File Offset: 0x000BCFB0
		internal static int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | ((int)buffer[offset + 1] << 8) | ((int)buffer[offset + 2] << 16) | ((int)buffer[offset + 3] << 24);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000BEDCF File Offset: 0x000BCFCF
		internal static void WriteInt(int val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000BEDF3 File Offset: 0x000BCFF3
		internal static void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x0400242A RID: 9258
		private AceFlags ace_flags;

		// Token: 0x0400242B RID: 9259
		private AceType ace_type;
	}
}
