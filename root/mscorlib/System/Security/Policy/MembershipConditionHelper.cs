using System;

namespace System.Security.Policy
{
	// Token: 0x020003E7 RID: 999
	internal sealed class MembershipConditionHelper
	{
		// Token: 0x06002A68 RID: 10856 RVA: 0x0009A7FC File Offset: 0x000989FC
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != MembershipConditionHelper.XmlTag)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid tag {0}, expected {1}."), se.Tag, MembershipConditionHelper.XmlTag), parameterName);
			}
			int num = minimumVersion;
			string text = se.Attribute("version");
			if (text != null)
			{
				try
				{
					num = int.Parse(text);
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Couldn't parse version from '{0}'."), text), parameterName, ex);
				}
			}
			return num;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x0009A88C File Offset: 0x00098A8C
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement(MembershipConditionHelper.XmlTag);
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000025BE File Offset: 0x000007BE
		public MembershipConditionHelper()
		{
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x0009A8E4 File Offset: 0x00098AE4
		// Note: this type is marked as 'beforefieldinit'.
		static MembershipConditionHelper()
		{
		}

		// Token: 0x04001E6A RID: 7786
		private static readonly string XmlTag = "IMembershipCondition";
	}
}
