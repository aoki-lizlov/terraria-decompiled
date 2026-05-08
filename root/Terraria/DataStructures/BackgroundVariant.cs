using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200053B RID: 1339
	public class BackgroundVariant
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x0062ED13 File Offset: 0x0062CF13
		public int[] Backgrounds
		{
			get
			{
				return this._backgrounds;
			}
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x0062ED1B File Offset: 0x0062CF1B
		public void Set(int far, int middle, int near)
		{
			this._backgrounds[0] = far;
			this._backgrounds[1] = middle;
			this._backgrounds[2] = near;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x0062ED38 File Offset: 0x0062CF38
		public void Clear()
		{
			this.Set(-1, -1, -1);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x0062ED43 File Offset: 0x0062CF43
		public bool HasAny
		{
			get
			{
				return this._backgrounds[0] != -1 || this._backgrounds[1] != -1 || this._backgrounds[2] != -1;
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x0062ED6B File Offset: 0x0062CF6B
		public BackgroundVariant()
		{
		}

		// Token: 0x04005B92 RID: 23442
		private readonly int[] _backgrounds = new int[] { -1, -1, -1 };
	}
}
