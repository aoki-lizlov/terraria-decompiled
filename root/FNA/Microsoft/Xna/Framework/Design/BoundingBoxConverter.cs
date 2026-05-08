using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000F6 RID: 246
	public class BoundingBoxConverter : MathTypeConverter
	{
		// Token: 0x060016AE RID: 5806 RVA: 0x00036F55 File Offset: 0x00035155
		public BoundingBoxConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00036F64 File Offset: 0x00035164
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00036F7C File Offset: 0x0003517C
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new BoundingBox((Vector3)propertyValues["Min"], (Vector3)propertyValues["Max"]);
		}
	}
}
