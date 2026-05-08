using System;
using System.ComponentModel;

namespace Microsoft.Xna.Framework.Design
{
	// Token: 0x020000F9 RID: 249
	public class MathTypeConverter : ExpandableObjectConverter
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0003713B File Offset: 0x0003533B
		public MathTypeConverter()
		{
			this.supportStringConvert = true;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0003714A File Offset: 0x0003534A
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return (this.supportStringConvert && sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00037170 File Offset: 0x00035370
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (this.supportStringConvert && destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00037196 File Offset: 0x00035396
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return this.propertyDescriptions;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000A99 RID: 2713
		protected PropertyDescriptorCollection propertyDescriptions;

		// Token: 0x04000A9A RID: 2714
		protected bool supportStringConvert;
	}
}
