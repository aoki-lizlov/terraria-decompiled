using System;
using System.Security;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000999 RID: 2457
	public class IsolatedStorageSecurityState : SecurityState
	{
		// Token: 0x060059C2 RID: 22978 RVA: 0x001308FF File Offset: 0x0012EAFF
		internal IsolatedStorageSecurityState()
		{
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x0001A197 File Offset: 0x00018397
		public IsolatedStorageSecurityOptions Options
		{
			get
			{
				return IsolatedStorageSecurityOptions.IncreaseQuotaForApplication;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x000174FB File Offset: 0x000156FB
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x00004088 File Offset: 0x00002288
		public long Quota
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x000174FB File Offset: 0x000156FB
		public long UsedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x000174FB File Offset: 0x000156FB
		public override void EnsureState()
		{
			throw new NotImplementedException();
		}
	}
}
