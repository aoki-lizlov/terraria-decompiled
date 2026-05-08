using System;

namespace System
{
	// Token: 0x020000C5 RID: 197
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x000190BB File Offset: 0x000172BB
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this._attributeTarget = validOn;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000190DC File Offset: 0x000172DC
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this._attributeTarget = validOn;
			this._allowMultiple = allowMultiple;
			this._inherited = inherited;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001910B File Offset: 0x0001730B
		public AttributeTargets ValidOn
		{
			get
			{
				return this._attributeTarget;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00019113 File Offset: 0x00017313
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0001911B File Offset: 0x0001731B
		public bool AllowMultiple
		{
			get
			{
				return this._allowMultiple;
			}
			set
			{
				this._allowMultiple = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00019124 File Offset: 0x00017324
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0001912C File Offset: 0x0001732C
		public bool Inherited
		{
			get
			{
				return this._inherited;
			}
			set
			{
				this._inherited = value;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00019135 File Offset: 0x00017335
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeUsageAttribute()
		{
		}

		// Token: 0x04000EE7 RID: 3815
		private AttributeTargets _attributeTarget = AttributeTargets.All;

		// Token: 0x04000EE8 RID: 3816
		private bool _allowMultiple;

		// Token: 0x04000EE9 RID: 3817
		private bool _inherited = true;

		// Token: 0x04000EEA RID: 3818
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
