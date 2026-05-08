using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020005AB RID: 1451
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class SoapMethodAttribute : SoapAttribute
	{
		// Token: 0x0600389B RID: 14491 RVA: 0x000CA9F2 File Offset: 0x000C8BF2
		public SoapMethodAttribute()
		{
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000CAA58 File Offset: 0x000C8C58
		// (set) Token: 0x0600389D RID: 14493 RVA: 0x000CAA60 File Offset: 0x000C8C60
		public string ResponseXmlElementName
		{
			get
			{
				return this._responseElement;
			}
			set
			{
				this._responseElement = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x000CAA69 File Offset: 0x000C8C69
		// (set) Token: 0x0600389F RID: 14495 RVA: 0x000CAA71 File Offset: 0x000C8C71
		public string ResponseXmlNamespace
		{
			get
			{
				return this._responseNamespace;
			}
			set
			{
				this._responseNamespace = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000CAA7A File Offset: 0x000C8C7A
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000CAA82 File Offset: 0x000C8C82
		public string ReturnXmlElementName
		{
			get
			{
				return this._returnElement;
			}
			set
			{
				this._returnElement = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x000CAA8B File Offset: 0x000C8C8B
		// (set) Token: 0x060038A3 RID: 14499 RVA: 0x000CAA93 File Offset: 0x000C8C93
		public string SoapAction
		{
			get
			{
				return this._soapAction;
			}
			set
			{
				this._soapAction = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x000CAA9C File Offset: 0x000C8C9C
		// (set) Token: 0x060038A5 RID: 14501 RVA: 0x000CAAA4 File Offset: 0x000C8CA4
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

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060038A6 RID: 14502 RVA: 0x000CAAAD File Offset: 0x000C8CAD
		// (set) Token: 0x060038A7 RID: 14503 RVA: 0x000CAAB5 File Offset: 0x000C8CB5
		public override string XmlNamespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x000CAAC0 File Offset: 0x000C8CC0
		internal override void SetReflectionObject(object reflectionObject)
		{
			MethodBase methodBase = (MethodBase)reflectionObject;
			if (this._responseElement == null)
			{
				this._responseElement = methodBase.Name + "Response";
			}
			if (this._responseNamespace == null)
			{
				this._responseNamespace = SoapServices.GetXmlNamespaceForMethodResponse(methodBase);
			}
			if (this._returnElement == null)
			{
				this._returnElement = "return";
			}
			if (this._soapAction == null)
			{
				this._soapAction = SoapServices.GetXmlNamespaceForMethodCall(methodBase) + "#" + methodBase.Name;
			}
			if (this._namespace == null)
			{
				this._namespace = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
			}
		}

		// Token: 0x04002589 RID: 9609
		private string _responseElement;

		// Token: 0x0400258A RID: 9610
		private string _responseNamespace;

		// Token: 0x0400258B RID: 9611
		private string _returnElement;

		// Token: 0x0400258C RID: 9612
		private string _soapAction;

		// Token: 0x0400258D RID: 9613
		private bool _useAttribute;

		// Token: 0x0400258E RID: 9614
		private string _namespace;
	}
}
