using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FF RID: 255
	public class RectangleConverter : MathTypeConverter
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x00036F55 File Offset: 0x00035155
		public RectangleConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00037558 File Offset: 0x00035758
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Rectangle((int)propertyValues["X"], (int)propertyValues["Y"], (int)propertyValues["Width"], (int)propertyValues["Height"]);
		}
	}
}
