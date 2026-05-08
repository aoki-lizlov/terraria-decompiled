using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000426 RID: 1062
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		// Token: 0x06002CB1 RID: 11441 RVA: 0x000A1C1F File Offset: 0x0009FE1F
		protected SecurityAttribute(SecurityAction action)
		{
			this.Action = action;
		}

		// Token: 0x06002CB2 RID: 11442
		public abstract IPermission CreatePermission();

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000A1C2E File Offset: 0x0009FE2E
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000A1C36 File Offset: 0x0009FE36
		public bool Unrestricted
		{
			get
			{
				return this.m_Unrestricted;
			}
			set
			{
				this.m_Unrestricted = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000A1C3F File Offset: 0x0009FE3F
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000A1C47 File Offset: 0x0009FE47
		public SecurityAction Action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x04001F5E RID: 8030
		private SecurityAction m_Action;

		// Token: 0x04001F5F RID: 8031
		private bool m_Unrestricted;
	}
}
