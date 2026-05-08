using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003F8 RID: 1016
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x0009D50D File Offset: 0x0009B70D
		internal ZoneMembershipCondition()
		{
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x0009D51C File Offset: 0x0009B71C
		public ZoneMembershipCondition(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0009D532 File Offset: 0x0009B732
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x0009D53C File Offset: 0x0009B73C
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(Locale.GetText("invalid zone"));
				}
				if (value == SecurityZone.NoZone)
				{
					throw new ArgumentException(Locale.GetText("NoZone isn't valid for membership condition"));
				}
				this.zone = value;
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0009D58C File Offset: 0x0009B78C
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Zone zone = obj as Zone;
				if (zone != null && zone.SecurityZone == this.zone)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0009D5CF File Offset: 0x0009B7CF
		public IMembershipCondition Copy()
		{
			return new ZoneMembershipCondition(this.zone);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0009D5DC File Offset: 0x0009B7DC
		public override bool Equals(object o)
		{
			ZoneMembershipCondition zoneMembershipCondition = o as ZoneMembershipCondition;
			return zoneMembershipCondition != null && zoneMembershipCondition.SecurityZone == this.zone;
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0009D603 File Offset: 0x0009B803
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0009D610 File Offset: 0x0009B810
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Zone");
			if (text != null)
			{
				this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
			}
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0009D65F File Offset: 0x0009B85F
		public override int GetHashCode()
		{
			return this.zone.GetHashCode();
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0009D672 File Offset: 0x0009B872
		public override string ToString()
		{
			return "Zone - " + this.zone.ToString();
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0009D68F File Offset: 0x0009B88F
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0009D698 File Offset: 0x0009B898
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(ZoneMembershipCondition), this.version);
			securityElement.AddAttribute("Zone", this.zone.ToString());
			return securityElement;
		}

		// Token: 0x04001E96 RID: 7830
		private readonly int version = 1;

		// Token: 0x04001E97 RID: 7831
		private SecurityZone zone;
	}
}
