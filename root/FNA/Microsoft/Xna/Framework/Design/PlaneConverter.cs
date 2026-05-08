using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FB RID: 251
	public class PlaneConverter : MathTypeConverter
	{
		// Token: 0x060016C3 RID: 5827 RVA: 0x00036F55 File Offset: 0x00035155
		public PlaneConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000372B7 File Offset: 0x000354B7
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Plane((Vector3)propertyValues["Normal"], (float)propertyValues["D"]);
		}
	}
}
