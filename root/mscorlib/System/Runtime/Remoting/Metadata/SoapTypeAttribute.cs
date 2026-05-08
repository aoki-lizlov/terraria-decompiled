using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020005AE RID: 1454
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	[ComVisible(true)]
	public sealed class SoapTypeAttribute : SoapAttribute
	{
		// Token: 0x060038AA RID: 14506 RVA: 0x000CA9F2 File Offset: 0x000C8BF2
		public SoapTypeAttribute()
		{
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x000CAB51 File Offset: 0x000C8D51
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x000CAB59 File Offset: 0x000C8D59
		public SoapOption SoapOptions
		{
			get
			{
				return this._soapOption;
			}
			set
			{
				this._soapOption = value;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x000CAB62 File Offset: 0x000C8D62
		// (set) Token: 0x060038AE RID: 14510 RVA: 0x000CAB6A File Offset: 0x000C8D6A
		public override bool UseAttribute
		{
			get
			{
				return this._useAttribute;
			}
			set
			{
				this._useAttribute = value;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x000CAB73 File Offset: 0x000C8D73
		// (set) Token: 0x060038B0 RID: 14512 RVA: 0x000CAB7B File Offset: 0x000C8D7B
		public string XmlElementName
		{
			get
			{
				return this._xmlElementName;
			}
			set
			{
				this._isElement = value != null;
				this._xmlElementName = value;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x000CAB8E File Offset: 0x000C8D8E
		// (set) Token: 0x060038B2 RID: 14514 RVA: 0x000CAB96 File Offset: 0x000C8D96
		public XmlFieldOrderOption XmlFieldOrder
		{
			get
			{
				return this._xmlFieldOrder;
			}
			set
			{
				this._xmlFieldOrder = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000CAB9F File Offset: 0x000C8D9F
		// (set) Token: 0x060038B4 RID: 14516 RVA: 0x000CABA7 File Offset: 0x000C8DA7
		public override string XmlNamespace
		{
			get
			{
				return this._xmlNamespace;
			}
			set
			{
				this._isElement = value != null;
				this._xmlNamespace = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060038B5 RID: 14517 RVA: 0x000CABBA File Offset: 0x000C8DBA
		// (set) Token: 0x060038B6 RID: 14518 RVA: 0x000CABC2 File Offset: 0x000C8DC2
		public string XmlTypeName
		{
			get
			{
				return this._xmlTypeName;
			}
			set
			{
				this._isType = value != null;
				this._xmlTypeName = value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060038B7 RID: 14519 RVA: 0x000CABD5 File Offset: 0x000C8DD5
		// (set) Token: 0x060038B8 RID: 14520 RVA: 0x000CABDD File Offset: 0x000C8DDD
		public string XmlTypeNamespace
		{
			get
			{
				return this._xmlTypeNamespace;
			}
			set
			{
				this._isType = value != null;
				this._xmlTypeNamespace = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x000CABF0 File Offset: 0x000C8DF0
		internal bool IsInteropXmlElement
		{
			get
			{
				return this._isElement;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000CABF8 File Offset: 0x000C8DF8
		internal bool IsInteropXmlType
		{
			get
			{
				return this._isType;
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000CAC00 File Offset: 0x000C8E00
		internal override void SetReflectionObject(object reflectionObject)
		{
			Type type = (Type)reflectionObject;
			if (this._xmlElementName == null)
			{
				this._xmlElementName = type.Name;
			}
			if (this._xmlTypeName == null)
			{
				this._xmlTypeName = type.Name;
			}
			if (this._xmlTypeNamespace == null)
			{
				string text;
				if (type.Assembly == typeof(object).Assembly)
				{
					text = string.Empty;
				}
				else
				{
					text = type.Assembly.GetName().Name;
				}
				this._xmlTypeNamespace = SoapServices.CodeXmlNamespaceForClrTypeNamespace(type.Namespace, text);
			}
			if (this._xmlNamespace == null)
			{
				this._xmlNamespace = this._xmlTypeNamespace;
			}
		}

		// Token: 0x04002596 RID: 9622
		private SoapOption _soapOption;

		// Token: 0x04002597 RID: 9623
		private bool _useAttribute;

		// Token: 0x04002598 RID: 9624
		private string _xmlElementName;

		// Token: 0x04002599 RID: 9625
		private XmlFieldOrderOption _xmlFieldOrder;

		// Token: 0x0400259A RID: 9626
		private string _xmlNamespace;

		// Token: 0x0400259B RID: 9627
		private string _xmlTypeName;

		// Token: 0x0400259C RID: 9628
		private string _xmlTypeNamespace;

		// Token: 0x0400259D RID: 9629
		private bool _isType;

		// Token: 0x0400259E RID: 9630
		private bool _isElement;
	}
}
