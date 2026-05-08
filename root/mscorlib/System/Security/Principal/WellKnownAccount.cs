using System;
using System.Runtime.CompilerServices;

namespace System.Security.Principal
{
	// Token: 0x020004B9 RID: 1209
	internal class WellKnownAccount
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000B8255 File Offset: 0x000B6455
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x000B825D File Offset: 0x000B645D
		public WellKnownSidType WellKnownValue
		{
			[CompilerGenerated]
			get
			{
				return this.<WellKnownValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WellKnownValue>k__BackingField = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000B8266 File Offset: 0x000B6466
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x000B826E File Offset: 0x000B646E
		public bool IsAbsolute
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAbsolute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsAbsolute>k__BackingField = value;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x000B8277 File Offset: 0x000B6477
		// (set) Token: 0x060031E1 RID: 12769 RVA: 0x000B827F File Offset: 0x000B647F
		public string Sid
		{
			[CompilerGenerated]
			get
			{
				return this.<Sid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Sid>k__BackingField = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000B8288 File Offset: 0x000B6488
		// (set) Token: 0x060031E3 RID: 12771 RVA: 0x000B8290 File Offset: 0x000B6490
		public string Rid
		{
			[CompilerGenerated]
			get
			{
				return this.<Rid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Rid>k__BackingField = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000B8299 File Offset: 0x000B6499
		// (set) Token: 0x060031E5 RID: 12773 RVA: 0x000B82A1 File Offset: 0x000B64A1
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x000B82AA File Offset: 0x000B64AA
		// (set) Token: 0x060031E7 RID: 12775 RVA: 0x000B82B2 File Offset: 0x000B64B2
		public string SddlForm
		{
			[CompilerGenerated]
			get
			{
				return this.<SddlForm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SddlForm>k__BackingField = value;
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000B82BC File Offset: 0x000B64BC
		public static WellKnownAccount LookupByType(WellKnownSidType sidType)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.WellKnownValue == sidType)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000B82F0 File Offset: 0x000B64F0
		public static WellKnownAccount LookupBySid(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.Sid == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000B8328 File Offset: 0x000B6528
		public static WellKnownAccount LookupByName(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.Name == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000B8360 File Offset: 0x000B6560
		public static WellKnownAccount LookupBySddlForm(string s)
		{
			foreach (WellKnownAccount wellKnownAccount in WellKnownAccount.accounts)
			{
				if (wellKnownAccount.SddlForm == s)
				{
					return wellKnownAccount;
				}
			}
			return null;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000025BE File Offset: 0x000007BE
		public WellKnownAccount()
		{
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000B8398 File Offset: 0x000B6598
		// Note: this type is marked as 'beforefieldinit'.
		static WellKnownAccount()
		{
		}

		// Token: 0x0400224A RID: 8778
		[CompilerGenerated]
		private WellKnownSidType <WellKnownValue>k__BackingField;

		// Token: 0x0400224B RID: 8779
		[CompilerGenerated]
		private bool <IsAbsolute>k__BackingField;

		// Token: 0x0400224C RID: 8780
		[CompilerGenerated]
		private string <Sid>k__BackingField;

		// Token: 0x0400224D RID: 8781
		[CompilerGenerated]
		private string <Rid>k__BackingField;

		// Token: 0x0400224E RID: 8782
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400224F RID: 8783
		[CompilerGenerated]
		private string <SddlForm>k__BackingField;

		// Token: 0x04002250 RID: 8784
		private static readonly WellKnownAccount[] accounts = new WellKnownAccount[]
		{
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NullSid,
				IsAbsolute = true,
				Sid = "S-1-0-0",
				Name = "NULL SID"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WorldSid,
				IsAbsolute = true,
				Sid = "S-1-1-0",
				Name = "Everyone",
				SddlForm = "WD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalSid,
				IsAbsolute = true,
				Sid = "S-1-2-0",
				Name = "LOCAL"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorOwnerSid,
				IsAbsolute = true,
				Sid = "S-1-3-0",
				Name = "CREATOR OWNER",
				SddlForm = "CO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorGroupSid,
				IsAbsolute = true,
				Sid = "S-1-3-1",
				Name = "CREATOR GROUP",
				SddlForm = "CG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorOwnerServerSid,
				IsAbsolute = true,
				Sid = "S-1-3-2",
				Name = "CREATOR OWNER SERVER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.CreatorGroupServerSid,
				IsAbsolute = true,
				Sid = "S-1-3-3",
				Name = "CREATOR GROUP SERVER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NTAuthoritySid,
				IsAbsolute = true,
				Sid = "S-1-5",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.DialupSid,
				IsAbsolute = true,
				Sid = "S-1-5-1",
				Name = "NT AUTHORITY\\DIALUP"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NetworkSid,
				IsAbsolute = true,
				Sid = "S-1-5-2",
				Name = "NT AUTHORITY\\NETWORK",
				SddlForm = "NU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BatchSid,
				IsAbsolute = true,
				Sid = "S-1-5-3",
				Name = "NT AUTHORITY\\BATCH"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.InteractiveSid,
				IsAbsolute = true,
				Sid = "S-1-5-4",
				Name = "NT AUTHORITY\\INTERACTIVE",
				SddlForm = "IU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-6",
				Name = "NT AUTHORITY\\SERVICE",
				SddlForm = "SU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AnonymousSid,
				IsAbsolute = true,
				Sid = "S-1-5-7",
				Name = "NT AUTHORITY\\ANONYMOUS LOGON",
				SddlForm = "AN"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ProxySid,
				IsAbsolute = true,
				Sid = "S-1-5-8",
				Name = "NT AUTHORITY\\PROXY"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.EnterpriseControllersSid,
				IsAbsolute = true,
				Sid = "S-1-5-9",
				Name = "NT AUTHORITY\\ENTERPRISE DOMAIN CONTROLLERS",
				SddlForm = "ED"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.SelfSid,
				IsAbsolute = true,
				Sid = "S-1-5-10",
				Name = "NT AUTHORITY\\SELF",
				SddlForm = "PS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AuthenticatedUserSid,
				IsAbsolute = true,
				Sid = "S-1-5-11",
				Name = "NT AUTHORITY\\Authenticated Users",
				SddlForm = "AU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.RestrictedCodeSid,
				IsAbsolute = true,
				Sid = "S-1-5-12",
				Name = "NT AUTHORITY\\RESTRICTED",
				SddlForm = "RC"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.TerminalServerSid,
				IsAbsolute = true,
				Sid = "S-1-5-13",
				Name = "NT AUTHORITY\\TERMINAL SERVER USER"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.RemoteLogonIdSid,
				IsAbsolute = true,
				Sid = "S-1-5-14",
				Name = "NT AUTHORITY\\REMOTE INTERACTIVE LOGON"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalSystemSid,
				IsAbsolute = true,
				Sid = "S-1-5-18",
				Name = "NT AUTHORITY\\SYSTEM",
				SddlForm = "SY"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.LocalServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-19",
				Name = "NT AUTHORITY\\LOCAL SERVICE",
				SddlForm = "LS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NetworkServiceSid,
				IsAbsolute = true,
				Sid = "S-1-5-20",
				Name = "NT AUTHORITY\\NETWORK SERVICE",
				SddlForm = "NS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinDomainSid,
				IsAbsolute = true,
				Sid = "S-1-5-32",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAdministratorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-544",
				Name = "BUILTIN\\Administrators",
				SddlForm = "BA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-545",
				Name = "BUILTIN\\Users",
				SddlForm = "BU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinGuestsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-546",
				Name = "BUILTIN\\Guests",
				SddlForm = "BG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPowerUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-547",
				Name = null,
				SddlForm = "PU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAccountOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-548",
				Name = null,
				SddlForm = "AO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinSystemOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-549",
				Name = null,
				SddlForm = "SO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPrintOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-550",
				Name = null,
				SddlForm = "PO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinBackupOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-551",
				Name = null,
				SddlForm = "BO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinReplicatorSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-552",
				Name = null,
				SddlForm = "RE"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPreWindows2000CompatibleAccessSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-554",
				Name = null,
				SddlForm = "RU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinRemoteDesktopUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-555",
				Name = null,
				SddlForm = "RD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinNetworkConfigurationOperatorsSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-556",
				Name = null,
				SddlForm = "NO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountAdministratorSid,
				IsAbsolute = false,
				Rid = "500",
				SddlForm = "LA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountGuestSid,
				IsAbsolute = false,
				Rid = "501",
				SddlForm = "LG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountKrbtgtSid,
				IsAbsolute = false,
				Rid = "502"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainAdminsSid,
				IsAbsolute = false,
				Rid = "512",
				SddlForm = "DA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainUsersSid,
				IsAbsolute = false,
				Rid = "513",
				SddlForm = "DU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountDomainGuestsSid,
				IsAbsolute = false,
				Rid = "514",
				SddlForm = "DG"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountComputersSid,
				IsAbsolute = false,
				Rid = "515",
				SddlForm = "DC"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountControllersSid,
				IsAbsolute = false,
				Rid = "516",
				SddlForm = "DD"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountCertAdminsSid,
				IsAbsolute = false,
				Rid = "517",
				SddlForm = "CA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountSchemaAdminsSid,
				IsAbsolute = false,
				Rid = "518",
				SddlForm = "SA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountEnterpriseAdminsSid,
				IsAbsolute = false,
				Rid = "519",
				SddlForm = "EA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountPolicyAdminsSid,
				IsAbsolute = false,
				Rid = "520",
				SddlForm = "PA"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.AccountRasAndIasServersSid,
				IsAbsolute = false,
				Rid = "553",
				SddlForm = "RS"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.NtlmAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-10",
				Name = "NT AUTHORITY\\NTLM Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.DigestAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-21",
				Name = "NT AUTHORITY\\Digest Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.SChannelAuthenticationSid,
				IsAbsolute = true,
				Sid = "S-1-5-64-14",
				Name = "NT AUTHORITY\\SChannel Authentication"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.ThisOrganizationSid,
				IsAbsolute = true,
				Sid = "S-1-5-15",
				Name = "NT AUTHORITY\\This Organization"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.OtherOrganizationSid,
				IsAbsolute = true,
				Sid = "S-1-5-1000",
				Name = "NT AUTHORITY\\Other Organization"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinIncomingForestTrustBuildersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-557",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPerformanceMonitoringUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-558",
				Name = "BUILTIN\\Performance Monitor Users",
				SddlForm = "MU"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinPerformanceLoggingUsersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-559",
				Name = "BUILTIN\\Performance Log Users"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.BuiltinAuthorizationAccessSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-560",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinBuiltinTerminalServerLicenseServersSid,
				IsAbsolute = true,
				Sid = "S-1-5-32-561",
				Name = null
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinLowLabelSid,
				IsAbsolute = false,
				Rid = "4096",
				SddlForm = "LW"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinMediumLabelSid,
				IsAbsolute = false,
				Rid = "8192",
				SddlForm = "ME"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinHighLabelSid,
				IsAbsolute = false,
				Rid = "12288",
				SddlForm = "HI"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinSystemLabelSid,
				IsAbsolute = false,
				Rid = "16384",
				SddlForm = "SI"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinEnterpriseReadonlyControllersSid,
				IsAbsolute = false,
				Rid = "521",
				SddlForm = "RO"
			},
			new WellKnownAccount
			{
				WellKnownValue = WellKnownSidType.WinBuiltinCertSvcDComAccessGroup,
				IsAbsolute = false,
				Rid = "574",
				SddlForm = "CD"
			}
		};
	}
}
