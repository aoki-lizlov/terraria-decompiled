using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	// Token: 0x02000526 RID: 1318
	[ComVisible(true)]
	[Serializable]
	public sealed class ActivationArguments : EvidenceBase
	{
		// Token: 0x0600355A RID: 13658 RVA: 0x000C1A72 File Offset: 0x000BFC72
		public ActivationArguments(ActivationContext activationData)
		{
			if (activationData == null)
			{
				throw new ArgumentNullException("activationData");
			}
			this._context = activationData;
			this._identity = activationData.Identity;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000C1A9B File Offset: 0x000BFC9B
		public ActivationArguments(ApplicationIdentity applicationIdentity)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000C1AB8 File Offset: 0x000BFCB8
		public ActivationArguments(ActivationContext activationContext, string[] activationData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			this._context = activationContext;
			this._identity = activationContext.Identity;
			this._data = activationData;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000C1AE8 File Offset: 0x000BFCE8
		public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._identity = applicationIdentity;
			this._data = activationData;
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000C1B0C File Offset: 0x000BFD0C
		public ActivationContext ActivationContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000C1B14 File Offset: 0x000BFD14
		public string[] ActivationData
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000C1B1C File Offset: 0x000BFD1C
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x04002494 RID: 9364
		private ActivationContext _context;

		// Token: 0x04002495 RID: 9365
		private ApplicationIdentity _identity;

		// Token: 0x04002496 RID: 9366
		private string[] _data;
	}
}
