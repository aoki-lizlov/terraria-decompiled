using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace System.Security.Principal
{
	// Token: 0x020004AD RID: 1197
	[Serializable]
	public class GenericIdentity : ClaimsIdentity
	{
		// Token: 0x06003184 RID: 12676 RVA: 0x000B74FC File Offset: 0x000B56FC
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
			this.AddNameClaim();
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000B752A File Offset: 0x000B572A
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
			this.AddNameClaim();
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000B7562 File Offset: 0x000B5762
		private GenericIdentity()
		{
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000B756A File Offset: 0x000B576A
		protected GenericIdentity(GenericIdentity identity)
			: base(identity)
		{
			this.m_name = identity.m_name;
			this.m_type = identity.m_type;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000B758B File Offset: 0x000B578B
		public override ClaimsIdentity Clone()
		{
			return new GenericIdentity(this);
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000B7593 File Offset: 0x000B5793
		public override IEnumerable<Claim> Claims
		{
			get
			{
				return base.Claims;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000B759B File Offset: 0x000B579B
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000B75A3 File Offset: 0x000B57A3
		public override string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000B75AB File Offset: 0x000B57AB
		public override bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000B75C0 File Offset: 0x000B57C0
		private void AddNameClaim()
		{
			if (this.m_name != null)
			{
				base.AddClaim(new Claim(base.NameClaimType, this.m_name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
		}

		// Token: 0x04002228 RID: 8744
		private readonly string m_name;

		// Token: 0x04002229 RID: 8745
		private readonly string m_type;
	}
}
