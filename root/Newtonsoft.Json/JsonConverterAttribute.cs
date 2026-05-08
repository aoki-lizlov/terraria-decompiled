using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001A RID: 26
	[AttributeUsage(3484, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000250B File Offset: 0x0000070B
		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002513 File Offset: 0x00000713
		public object[] ConverterParameters
		{
			[CompilerGenerated]
			get
			{
				return this.<ConverterParameters>k__BackingField;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000251B File Offset: 0x0000071B
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000253E File Offset: 0x0000073E
		public JsonConverterAttribute(Type converterType, params object[] converterParameters)
			: this(converterType)
		{
			this.ConverterParameters = converterParameters;
		}

		// Token: 0x0400004D RID: 77
		private readonly Type _converterType;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private readonly object[] <ConverterParameters>k__BackingField;
	}
}
