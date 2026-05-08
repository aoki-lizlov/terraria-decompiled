using System;

namespace Terraria.Cinematics
{
	// Token: 0x020005B0 RID: 1456
	public struct FrameEventData
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x00653A99 File Offset: 0x00651C99
		public int AbsoluteFrame
		{
			get
			{
				return this._absoluteFrame;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x00653AA1 File Offset: 0x00651CA1
		public int Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x00653AA9 File Offset: 0x00651CA9
		public int Duration
		{
			get
			{
				return this._duration;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x00653AB1 File Offset: 0x00651CB1
		public int Frame
		{
			get
			{
				return this._absoluteFrame - this._start;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x00653AC0 File Offset: 0x00651CC0
		public bool IsFirstFrame
		{
			get
			{
				return this._start == this._absoluteFrame;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060039AB RID: 14763 RVA: 0x00653AD0 File Offset: 0x00651CD0
		public bool IsLastFrame
		{
			get
			{
				return this.Remaining == 0;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x00653ADB File Offset: 0x00651CDB
		public int Remaining
		{
			get
			{
				return this._start + this._duration - this._absoluteFrame - 1;
			}
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x00653AF3 File Offset: 0x00651CF3
		public FrameEventData(int absoluteFrame, int start, int duration)
		{
			this._absoluteFrame = absoluteFrame;
			this._start = start;
			this._duration = duration;
		}

		// Token: 0x04005DAC RID: 23980
		private int _absoluteFrame;

		// Token: 0x04005DAD RID: 23981
		private int _start;

		// Token: 0x04005DAE RID: 23982
		private int _duration;
	}
}
