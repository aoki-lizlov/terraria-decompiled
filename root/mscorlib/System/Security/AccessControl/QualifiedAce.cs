using System;

namespace System.Security.AccessControl
{
	// Token: 0x02000512 RID: 1298
	public abstract class QualifiedAce : KnownAce
	{
		// Token: 0x060034DC RID: 13532 RVA: 0x000C0884 File Offset: 0x000BEA84
		internal QualifiedAce(AceType type, AceFlags flags, byte[] opaque)
			: base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000C0895 File Offset: 0x000BEA95
		internal QualifiedAce(byte[] binaryForm, int offset)
			: base(binaryForm, offset)
		{
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000C08A0 File Offset: 0x000BEAA0
		public AceQualifier AceQualifier
		{
			get
			{
				switch (base.AceType)
				{
				case AceType.AccessAllowed:
				case AceType.AccessAllowedCompound:
				case AceType.AccessAllowedObject:
				case AceType.AccessAllowedCallback:
				case AceType.AccessAllowedCallbackObject:
					return AceQualifier.AccessAllowed;
				case AceType.AccessDenied:
				case AceType.AccessDeniedObject:
				case AceType.AccessDeniedCallback:
				case AceType.AccessDeniedCallbackObject:
					return AceQualifier.AccessDenied;
				case AceType.SystemAudit:
				case AceType.SystemAuditObject:
				case AceType.SystemAuditCallback:
				case AceType.SystemAuditCallbackObject:
					return AceQualifier.SystemAudit;
				case AceType.SystemAlarm:
				case AceType.SystemAlarmObject:
				case AceType.SystemAlarmCallback:
				case AceType.SystemAlarmCallbackObject:
					return AceQualifier.SystemAlarm;
				default:
					throw new ArgumentException("Unrecognised ACE type: " + base.AceType.ToString());
				}
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000C092C File Offset: 0x000BEB2C
		public bool IsCallback
		{
			get
			{
				return base.AceType == AceType.AccessAllowedCallback || base.AceType == AceType.AccessAllowedCallbackObject || base.AceType == AceType.AccessDeniedCallback || base.AceType == AceType.AccessDeniedCallbackObject || base.AceType == AceType.SystemAlarmCallback || base.AceType == AceType.SystemAlarmCallbackObject || base.AceType == AceType.SystemAuditCallback || base.AceType == AceType.SystemAuditCallbackObject;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000C098B File Offset: 0x000BEB8B
		public int OpaqueLength
		{
			get
			{
				if (this.opaque == null)
				{
					return 0;
				}
				return this.opaque.Length;
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000C099F File Offset: 0x000BEB9F
		public byte[] GetOpaque()
		{
			if (this.opaque == null)
			{
				return null;
			}
			return (byte[])this.opaque.Clone();
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000C09BB File Offset: 0x000BEBBB
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x04002459 RID: 9305
		private byte[] opaque;
	}
}
