using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000FC RID: 252
	public class PointConverter : MathTypeConverter
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x00036FD4 File Offset: 0x000351D4
		public PointConverter()
		{
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000372E4 File Offset: 0x000354E4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Point(int.Parse(array[0], culture), int.Parse(array[1], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00037338 File Offset: 0x00035538
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Point point = (Point)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					point.X.ToString(culture),
					point.Y.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x000373AB File Offset: 0x000355AB
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Point((int)propertyValues["X"], (int)propertyValues["Y"]);
		}
	}
}
