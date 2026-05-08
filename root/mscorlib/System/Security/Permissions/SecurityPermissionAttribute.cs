using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000428 RID: 1064
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002CC5 RID: 11461 RVA: 0x000A1EBF File Offset: 0x000A00BF
		public SecurityPermissionAttribute(SecurityAction action)
			: base(action)
		{
			this.m_Flags = SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000A1ECF File Offset: 0x000A00CF
		// (set) Token: 0x06002CC7 RID: 11463 RVA: 0x000A1EDC File Offset: 0x000A00DC
		public bool Assertion
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Assertion) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Assertion;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Assertion;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000A1EFF File Offset: 0x000A00FF
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x000A1F10 File Offset: 0x000A0110
		public bool BindingRedirects
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.BindingRedirects) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.BindingRedirects;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.BindingRedirects;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000A1F3A File Offset: 0x000A013A
		// (set) Token: 0x06002CCB RID: 11467 RVA: 0x000A1F4B File Offset: 0x000A014B
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlAppDomain) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlAppDomain;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlAppDomain;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000A1F75 File Offset: 0x000A0175
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000A1F86 File Offset: 0x000A0186
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlDomainPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlDomainPolicy;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlDomainPolicy;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000A1FB0 File Offset: 0x000A01B0
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x000A1FBE File Offset: 0x000A01BE
		public bool ControlEvidence
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlEvidence) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlEvidence;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlEvidence;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000A1FE2 File Offset: 0x000A01E2
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x000A1FF0 File Offset: 0x000A01F0
		public bool ControlPolicy
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPolicy;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlPolicy;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000A2014 File Offset: 0x000A0214
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x000A2025 File Offset: 0x000A0225
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlPrincipal) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlPrincipal;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlPrincipal;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000A204F File Offset: 0x000A024F
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x000A205D File Offset: 0x000A025D
		public bool ControlThread
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.ControlThread) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.ControlThread;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.ControlThread;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000A2081 File Offset: 0x000A0281
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x000A208E File Offset: 0x000A028E
		public bool Execution
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Execution) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Execution;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Execution;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000A20B1 File Offset: 0x000A02B1
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000A20C2 File Offset: 0x000A02C2
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.Infrastructure) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.Infrastructure;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.Infrastructure;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000A20EC File Offset: 0x000A02EC
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000A20FD File Offset: 0x000A02FD
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.RemotingConfiguration) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.RemotingConfiguration;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.RemotingConfiguration;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000A2127 File Offset: 0x000A0327
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000A2138 File Offset: 0x000A0338
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SerializationFormatter) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SerializationFormatter;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.SerializationFormatter;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000A2162 File Offset: 0x000A0362
		// (set) Token: 0x06002CDF RID: 11487 RVA: 0x000A216F File Offset: 0x000A036F
		public bool SkipVerification
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.SkipVerification) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.SkipVerification;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.SkipVerification;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000A2192 File Offset: 0x000A0392
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000A219F File Offset: 0x000A039F
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_Flags & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= SecurityPermissionFlag.UnmanagedCode;
					return;
				}
				this.m_Flags &= ~SecurityPermissionFlag.UnmanagedCode;
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000A21C4 File Offset: 0x000A03C4
		public override IPermission CreatePermission()
		{
			SecurityPermission securityPermission;
			if (base.Unrestricted)
			{
				securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			}
			else
			{
				securityPermission = new SecurityPermission(this.m_Flags);
			}
			return securityPermission;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000A21F1 File Offset: 0x000A03F1
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000A21F9 File Offset: 0x000A03F9
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x04001F62 RID: 8034
		private SecurityPermissionFlag m_Flags;
	}
}
