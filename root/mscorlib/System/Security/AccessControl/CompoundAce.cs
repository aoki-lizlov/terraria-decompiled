using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x020004E6 RID: 1254
	public sealed class CompoundAce : KnownAce
	{
		// Token: 0x06003350 RID: 13136 RVA: 0x000BD913 File Offset: 0x000BBB13
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid)
			: base(AceType.AccessAllowedCompound, flags)
		{
			this.compound_ace_type = compoundAceType;
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000BD933 File Offset: 0x000BBB33
		// (set) Token: 0x06003353 RID: 13139 RVA: 0x000BD93B File Offset: 0x000BBB3B
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this.compound_ace_type;
			}
			set
			{
				this.compound_ace_type = value;
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000174FB File Offset: 0x000156FB
		internal override string GetSddlForm()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040023E2 RID: 9186
		private CompoundAceType compound_ace_type;
	}
}
