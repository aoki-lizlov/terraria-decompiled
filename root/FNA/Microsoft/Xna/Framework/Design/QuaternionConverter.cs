using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FD RID: 253
	public class QuaternionConverter : MathTypeConverter
	{
		// Token: 0x060016CA RID: 5834 RVA: 0x00036FD4 File Offset: 0x000351D4
		public QuaternionConverter()
		{
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x000373D8 File Offset: 0x000355D8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Quaternion(float.Parse(array[0], culture), float.Parse(array[1], culture), float.Parse(array[2], culture), float.Parse(array[3], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00037440 File Offset: 0x00035640
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Quaternion quaternion = (Quaternion)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					quaternion.X.ToString(culture),
					quaternion.Y.ToString(culture),
					quaternion.Z.ToString(culture),
					quaternion.W.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000374D4 File Offset: 0x000356D4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Quaternion((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"], (float)propertyValues["W"]);
		}
	}
}
