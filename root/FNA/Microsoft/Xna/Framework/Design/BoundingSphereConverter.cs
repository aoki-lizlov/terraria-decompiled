using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000F7 RID: 247
	public class BoundingSphereConverter : MathTypeConverter
	{
		// Token: 0x060016B2 RID: 5810 RVA: 0x00036F55 File Offset: 0x00035155
		public BoundingSphereConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00036F64 File Offset: 0x00035164
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00036FA8 File Offset: 0x000351A8
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new BoundingSphere((Vector3)propertyValues["Center"], (float)propertyValues["Radius"]);
		}
	}
}
