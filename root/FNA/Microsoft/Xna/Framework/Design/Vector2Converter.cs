using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x02000100 RID: 256
	public class Vector2Converter : MathTypeConverter
	{
		// Token: 0x060016D5 RID: 5845 RVA: 0x00036FD4 File Offset: 0x000351D4
		public Vector2Converter()
		{
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000375B0 File Offset: 0x000357B0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
				return new Vector2(float.Parse(array[0], culture), float.Parse(array[1], culture));
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00037604 File Offset: 0x00035804
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				Vector2 vector = (Vector2)value;
				return string.Join(culture.TextInfo.ListSeparator + " ", new string[]
				{
					vector.X.ToString(culture),
					vector.Y.ToString(culture)
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00037677 File Offset: 0x00035877
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new Vector2((float)propertyValues["X"], (float)propertyValues["Y"]);
		}
	}
}
