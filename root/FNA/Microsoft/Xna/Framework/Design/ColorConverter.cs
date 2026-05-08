using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000F8 RID: 248
	public class ColorConverter : MathTypeConverter
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x00036FD4 File Offset: 0x000351D4
		public ColorConverter()
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00036FDC File Offset: 0x000351DC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Color(int.Parse(array[0], culture), int.Parse(array[1], culture), int.Parse(array[2], culture), int.Parse(array[3], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00037044 File Offset: 0x00035244
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Color color = (Color)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					color.R.ToString(culture),
					color.G.ToString(culture),
					color.B.ToString(culture),
					color.A.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000370E4 File Offset: 0x000352E4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Color((int)propertyValues["R"], (int)propertyValues["G"], (int)propertyValues["B"], (int)propertyValues["A"]);
		}
	}
}
