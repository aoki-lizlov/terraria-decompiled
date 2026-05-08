using System;

namespace System
{
	// Token: 0x02000134 RID: 308
	internal readonly struct ParamsArray
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00032956 File Offset: 0x00030B56
		public ParamsArray(object arg0)
		{
			this._arg0 = arg0;
			this._arg1 = null;
			this._arg2 = null;
			this._args = ParamsArray.s_oneArgArray;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00032978 File Offset: 0x00030B78
		public ParamsArray(object arg0, object arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = null;
			this._args = ParamsArray.s_twoArgArray;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0003299A File Offset: 0x00030B9A
		public ParamsArray(object arg0, object arg1, object arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._args = ParamsArray.s_threeArgArray;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000329BC File Offset: 0x00030BBC
		public ParamsArray(object[] args)
		{
			int num = args.Length;
			this._arg0 = ((num > 0) ? args[0] : null);
			this._arg1 = ((num > 1) ? args[1] : null);
			this._arg2 = ((num > 2) ? args[2] : null);
			this._args = args;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00032A04 File Offset: 0x00030C04
		public int Length
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x170000EE RID: 238
		public object this[int index]
		{
			get
			{
				if (index != 0)
				{
					return this.GetAtSlow(index);
				}
				return this._arg0;
			}
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00032A21 File Offset: 0x00030C21
		private object GetAtSlow(int index)
		{
			if (index == 1)
			{
				return this._arg1;
			}
			if (index == 2)
			{
				return this._arg2;
			}
			return this._args[index];
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00032A41 File Offset: 0x00030C41
		// Note: this type is marked as 'beforefieldinit'.
		static ParamsArray()
		{
		}

		// Token: 0x0400112C RID: 4396
		private static readonly object[] s_oneArgArray = new object[1];

		// Token: 0x0400112D RID: 4397
		private static readonly object[] s_twoArgArray = new object[2];

		// Token: 0x0400112E RID: 4398
		private static readonly object[] s_threeArgArray = new object[3];

		// Token: 0x0400112F RID: 4399
		private readonly object _arg0;

		// Token: 0x04001130 RID: 4400
		private readonly object _arg1;

		// Token: 0x04001131 RID: 4401
		private readonly object _arg2;

		// Token: 0x04001132 RID: 4402
		private readonly object[] _args;
	}
}
