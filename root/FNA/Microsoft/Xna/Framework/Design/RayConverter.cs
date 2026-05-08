using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FE RID: 254
	public class RayConverter : MathTypeConverter
	{
		// Token: 0x060016CE RID: 5838 RVA: 0x00036F55 File Offset: 0x00035155
		public RayConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00036F64 File Offset: 0x00035164
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x0003752B File Offset: 0x0003572B
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Ray((Vector3)propertyValues["Position"], (Vector3)propertyValues["Direction"]);
		}
	}
}
