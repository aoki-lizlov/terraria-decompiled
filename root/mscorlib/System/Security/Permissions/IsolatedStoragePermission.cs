using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000413 RID: 1043
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x0009F4EC File Offset: 0x0009D6EC
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x0009F509 File Offset: 0x0009D709
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x0009F511 File Offset: 0x0009D711
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x0009F51A File Offset: 0x0009D71A
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x0009F524 File Offset: 0x0009D724
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				if (!Enum.IsDefined(typeof(IsolatedStorageContainment), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "IsolatedStorageContainment");
				}
				this.m_allowed = value;
				if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
				{
					this.m_userQuota = long.MaxValue;
					this.m_machineQuota = long.MaxValue;
					this.m_expirationDays = long.MaxValue;
					this.m_permanentData = true;
				}
			}
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x0009F5B0 File Offset: 0x0009D7B0
		public bool IsUnrestricted()
		{
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage == this.m_allowed;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0009F5C0 File Offset: 0x0009D7C0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Allowed", this.m_allowed.ToString());
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString());
				}
			}
			return securityElement;
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0009F634 File Offset: 0x0009D834
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = 0L;
			this.m_permanentData = false;
			this.m_allowed = IsolatedStorageContainment.None;
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			string text = esd.Attribute("Allowed");
			if (text != null)
			{
				this.UsageAllowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
			}
			text = esd.Attribute("UserQuota");
			if (text != null)
			{
				this.m_userQuota = long.Parse(text, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0009F6D3 File Offset: 0x0009D8D3
		internal bool IsEmpty()
		{
			return this.m_userQuota == 0L && this.m_allowed == IsolatedStorageContainment.None;
		}

		// Token: 0x04001F0D RID: 7949
		private const int version = 1;

		// Token: 0x04001F0E RID: 7950
		internal long m_userQuota;

		// Token: 0x04001F0F RID: 7951
		internal long m_machineQuota;

		// Token: 0x04001F10 RID: 7952
		internal long m_expirationDays;

		// Token: 0x04001F11 RID: 7953
		internal bool m_permanentData;

		// Token: 0x04001F12 RID: 7954
		internal IsolatedStorageContainment m_allowed;
	}
}
