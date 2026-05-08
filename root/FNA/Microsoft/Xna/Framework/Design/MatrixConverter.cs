using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FA RID: 250
	public class MatrixConverter : MathTypeConverter
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x00036F55 File Offset: 0x00035155
		public MatrixConverter()
		{
			this.supportStringConvert = false;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00036F6F File Offset: 0x0003516F
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000371A0 File Offset: 0x000353A0
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Matrix((float)propertyValues["M11"], (float)propertyValues["M12"], (float)propertyValues["M13"], (float)propertyValues["M14"], (float)propertyValues["M21"], (float)propertyValues["M22"], (float)propertyValues["M23"], (float)propertyValues["M24"], (float)propertyValues["M31"], (float)propertyValues["M32"], (float)propertyValues["M33"], (float)propertyValues["M34"], (float)propertyValues["M41"], (float)propertyValues["M42"], (float)propertyValues["M43"], (float)propertyValues["M44"]);
		}
	}
}
