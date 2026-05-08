using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020005AA RID: 1450
	[AttributeUsage(AttributeTargets.Field)]
	[ComVisible(true)]
	public sealed class SoapFieldAttribute : SoapAttribute
	{
		// Token: 0x06003894 RID: 14484 RVA: 0x000CA9F2 File Offset: 0x000C8BF2
		public SoapFieldAttribute()
		{
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x000CA9FA File Offset: 0x000C8BFA
		// (set) Token: 0x06003896 RID: 14486 RVA: 0x000CAA02 File Offset: 0x000C8C02
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06003897 RID: 14487 RVA: 0x000CAA0B File Offset: 0x000C8C0B
		// (set) Token: 0x06003898 RID: 14488 RVA: 0x000CAA13 File Offset: 0x000C8C13
		public string XmlElementName
		{
			get
			{
				return this._elementName;
			}
			set
			{
				this._isElement = value != null;
				this._elementName = value;
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000CAA26 File Offset: 0x000C8C26
		public bool IsInteropXmlElement()
		{
			return this._isElement;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000CAA30 File Offset: 0x000C8C30
		internal override void SetReflectionObject(object reflectionObject)
		{
			FieldInfo fieldInfo = (FieldInfo)reflectionObject;
			if (this._elementName == null)
			{
				this._elementName = fieldInfo.Name;
			}
		}

		// Token: 0x04002586 RID: 9606
		private int _order;

		// Token: 0x04002587 RID: 9607
		private string _elementName;

		// Token: 0x04002588 RID: 9608
		private bool _isElement;
	}
}
