using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.AccessControl
{
	// Token: 0x02000513 RID: 1299
	public sealed class RawAcl : GenericAcl
	{
		// Token: 0x060034E3 RID: 13539 RVA: 0x000C09D9 File Offset: 0x000BEBD9
		public RawAcl(byte revision, int capacity)
		{
			this.revision = revision;
			this.list = new List<GenericAce>(capacity);
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000C09F4 File Offset: 0x000BEBF4
		public RawAcl(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 8)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			this.revision = binaryForm[offset];
			if (this.revision != GenericAcl.AclRevision && this.revision != GenericAcl.AclRevisionDS)
			{
				throw new ArgumentException("Invalid ACL - unknown revision", "binaryForm");
			}
			int num = (int)this.ReadUShort(binaryForm, offset + 2);
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACL - truncated", "binaryForm");
			}
			int num2 = offset + 8;
			int num3 = (int)this.ReadUShort(binaryForm, offset + 4);
			this.list = new List<GenericAce>(num3);
			for (int i = 0; i < num3; i++)
			{
				GenericAce genericAce = GenericAce.CreateFromBinaryForm(binaryForm, num2);
				this.list.Add(genericAce);
				num2 += genericAce.BinaryLength;
			}
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000C0AD4 File Offset: 0x000BECD4
		internal RawAcl(byte revision, List<GenericAce> aces)
		{
			this.revision = revision;
			this.list = aces;
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000C0AEC File Offset: 0x000BECEC
		public override int BinaryLength
		{
			get
			{
				int num = 8;
				foreach (GenericAce genericAce in this.list)
				{
					num += genericAce.BinaryLength;
				}
				return num;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000C0B44 File Offset: 0x000BED44
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000756 RID: 1878
		public override GenericAce this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060034EA RID: 13546 RVA: 0x000C0B6E File Offset: 0x000BED6E
		public override byte Revision
		{
			get
			{
				return this.revision;
			}
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000C0B78 File Offset: 0x000BED78
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - this.BinaryLength)
			{
				throw new ArgumentException("Offset out of range", "offset");
			}
			binaryForm[offset] = this.Revision;
			binaryForm[offset + 1] = 0;
			this.WriteUShort((ushort)this.BinaryLength, binaryForm, offset + 2);
			this.WriteUShort((ushort)this.list.Count, binaryForm, offset + 4);
			this.WriteUShort(0, binaryForm, offset + 6);
			int num = offset + 8;
			foreach (GenericAce genericAce in this.list)
			{
				genericAce.GetBinaryForm(binaryForm, num);
				num += genericAce.BinaryLength;
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000C0C4C File Offset: 0x000BEE4C
		public void InsertAce(int index, GenericAce ace)
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			this.list.Insert(index, ace);
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000C0C6F File Offset: 0x000BEE6F
		public void RemoveAce(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000C0C80 File Offset: 0x000BEE80
		internal override string GetSddlForm(ControlFlags sdFlags, bool isDacl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (isDacl)
			{
				if ((sdFlags & ControlFlags.DiscretionaryAclProtected) != ControlFlags.None)
				{
					stringBuilder.Append("P");
				}
				if ((sdFlags & ControlFlags.DiscretionaryAclAutoInheritRequired) != ControlFlags.None)
				{
					stringBuilder.Append("AR");
				}
				if ((sdFlags & ControlFlags.DiscretionaryAclAutoInherited) != ControlFlags.None)
				{
					stringBuilder.Append("AI");
				}
			}
			else
			{
				if ((sdFlags & ControlFlags.SystemAclProtected) != ControlFlags.None)
				{
					stringBuilder.Append("P");
				}
				if ((sdFlags & ControlFlags.SystemAclAutoInheritRequired) != ControlFlags.None)
				{
					stringBuilder.Append("AR");
				}
				if ((sdFlags & ControlFlags.SystemAclAutoInherited) != ControlFlags.None)
				{
					stringBuilder.Append("AI");
				}
			}
			foreach (GenericAce genericAce in this.list)
			{
				stringBuilder.Append(genericAce.GetSddlForm());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000C0D68 File Offset: 0x000BEF68
		internal static RawAcl ParseSddlForm(string sddlForm, bool isDacl, ref ControlFlags sdFlags, ref int pos)
		{
			RawAcl.ParseFlags(sddlForm, isDacl, ref sdFlags, ref pos);
			byte b = GenericAcl.AclRevision;
			List<GenericAce> list = new List<GenericAce>();
			while (pos < sddlForm.Length && sddlForm[pos] == '(')
			{
				GenericAce genericAce = GenericAce.CreateFromSddlForm(sddlForm, ref pos);
				if (genericAce as ObjectAce != null)
				{
					b = GenericAcl.AclRevisionDS;
				}
				list.Add(genericAce);
			}
			return new RawAcl(b, list);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000C0DCC File Offset: 0x000BEFCC
		private static void ParseFlags(string sddlForm, bool isDacl, ref ControlFlags sdFlags, ref int pos)
		{
			char c = char.ToUpperInvariant(sddlForm[pos]);
			while (c == 'P' || c == 'A')
			{
				if (c == 'P')
				{
					if (isDacl)
					{
						sdFlags |= ControlFlags.DiscretionaryAclProtected;
					}
					else
					{
						sdFlags |= ControlFlags.SystemAclProtected;
					}
					pos++;
				}
				else
				{
					if (sddlForm.Length <= pos + 1)
					{
						throw new ArgumentException("Invalid SDDL string.", "sddlForm");
					}
					c = char.ToUpperInvariant(sddlForm[pos + 1]);
					if (c == 'R')
					{
						if (isDacl)
						{
							sdFlags |= ControlFlags.DiscretionaryAclAutoInheritRequired;
						}
						else
						{
							sdFlags |= ControlFlags.SystemAclAutoInheritRequired;
						}
						pos += 2;
					}
					else
					{
						if (c != 'I')
						{
							throw new ArgumentException("Invalid SDDL string.", "sddlForm");
						}
						if (isDacl)
						{
							sdFlags |= ControlFlags.DiscretionaryAclAutoInherited;
						}
						else
						{
							sdFlags |= ControlFlags.SystemAclAutoInherited;
						}
						pos += 2;
					}
				}
				c = char.ToUpperInvariant(sddlForm[pos]);
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000BF1A2 File Offset: 0x000BD3A2
		private void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000C0EBB File Offset: 0x000BF0BB
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | ((int)buffer[offset + 1] << 8));
		}

		// Token: 0x0400245A RID: 9306
		private byte revision;

		// Token: 0x0400245B RID: 9307
		private List<GenericAce> list;
	}
}
