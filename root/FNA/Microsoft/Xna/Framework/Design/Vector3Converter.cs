using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x02000101 RID: 257
	public class Vector3Converter : MathTypeConverter
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x00036FD4 File Offset: 0x000351D4
		public Vector3Converter()
		{
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000376A4 File Offset: 0x000358A4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Vector3(float.Parse(array[0], culture), float.Parse(array[1], culture), float.Parse(array[2], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00037704 File Offset: 0x00035904
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Vector3 vector = (Vector3)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					vector.X.ToString(culture),
					vector.Y.ToString(culture),
					vector.Z.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00037787 File Offset: 0x00035987
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Vector3((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"]);
		}
	}
}
