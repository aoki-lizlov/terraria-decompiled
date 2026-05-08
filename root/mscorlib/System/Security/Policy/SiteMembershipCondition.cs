using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003EF RID: 1007
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002ACB RID: 10955 RVA: 0x0009C59B File Offset: 0x0009A79B
		internal SiteMembershipCondition()
		{
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x0009C5AA File Offset: 0x0009A7AA
		public SiteMembershipCondition(string site)
		{
			this.Site = site;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x0009C5C0 File Offset: 0x0009A7C0
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		public string Site
		{
			get
			{
				return this._site;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("site");
				}
				if (!global::System.Security.Policy.Site.IsValid(value))
				{
					throw new ArgumentException("invalid site");
				}
				this._site = value;
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0009C5F4 File Offset: 0x0009A7F4
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Site)
				{
					string[] array = this._site.Split('.', StringSplitOptions.None);
					string[] array2 = (hostEnumerator.Current as Site).origin_site.Split('.', StringSplitOptions.None);
					int i = array.Length - 1;
					int num = array2.Length - 1;
					while (i >= 0)
					{
						if (i == 0)
						{
							return string.Compare(array[0], "*", true, CultureInfo.InvariantCulture) == 0;
						}
						if (string.Compare(array[i], array2[num], true, CultureInfo.InvariantCulture) != 0)
						{
							return false;
						}
						i--;
						num--;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009C6A2 File Offset: 0x0009A8A2
		public IMembershipCondition Copy()
		{
			return new SiteMembershipCondition(this._site);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0009C6AF File Offset: 0x0009A8AF
		public override bool Equals(object o)
		{
			return o != null && o is SiteMembershipCondition && new Site((o as SiteMembershipCondition)._site).Equals(new Site(this._site));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0009C6E0 File Offset: 0x0009A8E0
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0009C6EA File Offset: 0x0009A8EA
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			this._site = e.Attribute("Site");
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0009C715 File Offset: 0x0009A915
		public override int GetHashCode()
		{
			return this._site.GetHashCode();
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0009C722 File Offset: 0x0009A922
		public override string ToString()
		{
			return "Site - " + this._site;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0009C734 File Offset: 0x0009A934
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0009C73D File Offset: 0x0009A93D
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(SiteMembershipCondition), this.version);
			securityElement.AddAttribute("Site", this._site);
			return securityElement;
		}

		// Token: 0x04001E7E RID: 7806
		private readonly int version = 1;

		// Token: 0x04001E7F RID: 7807
		private string _site;
	}
}
