using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000754 RID: 1876
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
	public sealed class InterfaceImplementedInVersionAttribute : Attribute
	{
		// Token: 0x06004413 RID: 17427 RVA: 0x000E34F9 File Offset: 0x000E16F9
		public InterfaceImplementedInVersionAttribute(Type interfaceType, byte majorVersion, byte minorVersion, byte buildVersion, byte revisionVersion)
		{
			this.m_interfaceType = interfaceType;
			this.m_majorVersion = majorVersion;
			this.m_minorVersion = minorVersion;
			this.m_buildVersion = buildVersion;
			this.m_revisionVersion = revisionVersion;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06004414 RID: 17428 RVA: 0x000E3526 File Offset: 0x000E1726
		public Type InterfaceType
		{
			get
			{
				return this.m_interfaceType;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x000E352E File Offset: 0x000E172E
		public byte MajorVersion
		{
			get
			{
				return this.m_majorVersion;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x000E3536 File Offset: 0x000E1736
		public byte MinorVersion
		{
			get
			{
				return this.m_minorVersion;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000E353E File Offset: 0x000E173E
		public byte BuildVersion
		{
			get
			{
				return this.m_buildVersion;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x000E3546 File Offset: 0x000E1746
		public byte RevisionVersion
		{
			get
			{
				return this.m_revisionVersion;
			}
		}

		// Token: 0x04002B9F RID: 11167
		private Type m_interfaceType;

		// Token: 0x04002BA0 RID: 11168
		private byte m_majorVersion;

		// Token: 0x04002BA1 RID: 11169
		private byte m_minorVersion;

		// Token: 0x04002BA2 RID: 11170
		private byte m_buildVersion;

		// Token: 0x04002BA3 RID: 11171
		private byte m_revisionVersion;
	}
}
