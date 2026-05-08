using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003F2 RID: 1010
	[ComVisible(true)]
	public class TrustManagerContext
	{
		// Token: 0x06002AF6 RID: 10998 RVA: 0x0009CC66 File Offset: 0x0009AE66
		public TrustManagerContext()
			: this(TrustManagerUIContext.Run)
		{
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0009CC6F File Offset: 0x0009AE6F
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this._ignorePersistedDecision = false;
			this._noPrompt = false;
			this._keepAlive = false;
			this._persist = false;
			this._ui = uiContext;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x0009CC9A File Offset: 0x0009AE9A
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x0009CCA2 File Offset: 0x0009AEA2
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this._ignorePersistedDecision;
			}
			set
			{
				this._ignorePersistedDecision = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0009CCAB File Offset: 0x0009AEAB
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x0009CCB3 File Offset: 0x0009AEB3
		public virtual bool KeepAlive
		{
			get
			{
				return this._keepAlive;
			}
			set
			{
				this._keepAlive = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0009CCBC File Offset: 0x0009AEBC
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0009CCC4 File Offset: 0x0009AEC4
		public virtual bool NoPrompt
		{
			get
			{
				return this._noPrompt;
			}
			set
			{
				this._noPrompt = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0009CCCD File Offset: 0x0009AECD
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x0009CCD5 File Offset: 0x0009AED5
		public virtual bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0009CCDE File Offset: 0x0009AEDE
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x0009CCE6 File Offset: 0x0009AEE6
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this._previousId;
			}
			set
			{
				this._previousId = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x0009CCEF File Offset: 0x0009AEEF
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0009CCF7 File Offset: 0x0009AEF7
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this._ui;
			}
			set
			{
				this._ui = value;
			}
		}

		// Token: 0x04001E87 RID: 7815
		private bool _ignorePersistedDecision;

		// Token: 0x04001E88 RID: 7816
		private bool _noPrompt;

		// Token: 0x04001E89 RID: 7817
		private bool _keepAlive;

		// Token: 0x04001E8A RID: 7818
		private bool _persist;

		// Token: 0x04001E8B RID: 7819
		private ApplicationIdentity _previousId;

		// Token: 0x04001E8C RID: 7820
		private TrustManagerUIContext _ui;
	}
}
