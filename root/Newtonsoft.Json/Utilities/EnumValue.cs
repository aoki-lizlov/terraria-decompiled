using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000067 RID: 103
	internal class EnumValue<T> where T : struct
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001595B File Offset: 0x00013B5B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00015963 File Offset: 0x00013B63
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001596B File Offset: 0x00013B6B
		public EnumValue(string name, T value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x04000258 RID: 600
		private readonly string _name;

		// Token: 0x04000259 RID: 601
		private readonly T _value;
	}
}
