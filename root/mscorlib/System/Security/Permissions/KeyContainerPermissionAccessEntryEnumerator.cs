using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000418 RID: 1048
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x0009FCE1 File Offset: 0x0009DEE1
		internal KeyContainerPermissionAccessEntryEnumerator(ArrayList list)
		{
			this.e = list.GetEnumerator();
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x0009FCF5 File Offset: 0x0009DEF5
		public KeyContainerPermissionAccessEntry Current
		{
			get
			{
				return (KeyContainerPermissionAccessEntry)this.e.Current;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0009FD07 File Offset: 0x0009DF07
		object IEnumerator.Current
		{
			get
			{
				return this.e.Current;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x0009FD14 File Offset: 0x0009DF14
		public bool MoveNext()
		{
			return this.e.MoveNext();
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0009FD21 File Offset: 0x0009DF21
		public void Reset()
		{
			this.e.Reset();
		}

		// Token: 0x04001F1F RID: 7967
		private IEnumerator e;
	}
}
