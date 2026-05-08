using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x02000102 RID: 258
	public class Vector4Converter : MathTypeConverter
	{
		// Token: 0x060016DD RID: 5853 RVA: 0x00036FD4 File Offset: 0x000351D4
		public Vector4Converter()
		{
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000377C4 File Offset: 0x000359C4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Vector4(float.Parse(array[0], culture), float.Parse(array[1], culture), float.Parse(array[2], culture), float.Parse(array[3], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0003782C File Offset: 0x00035A2C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Vector4 vector = (Vector4)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					vector.X.ToString(culture),
					vector.Y.ToString(culture),
					vector.Z.ToString(culture),
					vector.W.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000378C0 File Offset: 0x00035AC0
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Vector4((float)propertyValues["X"], (float)propertyValues["Y"], (float)propertyValues["Z"], (float)propertyValues["W"]);
		}
	}
}
