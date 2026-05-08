using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020005A9 RID: 1449
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x0600388C RID: 14476 RVA: 0x00002050 File Offset: 0x00000250
		public SoapAttribute()
		{
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x000CA9B6 File Offset: 0x000C8BB6
		// (set) Token: 0x0600388E RID: 14478 RVA: 0x000CA9BE File Offset: 0x000C8BBE
		public virtual bool Embedded
		{
			get
			{
				return this._nested;
			}
			set
			{
				this._nested = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000CA9C7 File Offset: 0x000C8BC7
		// (set) Token: 0x06003890 RID: 14480 RVA: 0x000CA9CF File Offset: 0x000C8BCF
		public virtual bool UseAttribute
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

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003891 RID: 14481 RVA: 0x000CA9D8 File Offset: 0x000C8BD8
		// (set) Token: 0x06003892 RID: 14482 RVA: 0x000CA9E0 File Offset: 0x000C8BE0
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000CA9E9 File Offset: 0x000C8BE9
		internal virtual void SetReflectionObject(object reflectionObject)
		{
			this.ReflectInfo = reflectionObject;
		}

		// Token: 0x04002582 RID: 9602
		private bool _nested;

		// Token: 0x04002583 RID: 9603
		private bool _useAttribute;

		// Token: 0x04002584 RID: 9604
		protected string ProtXmlNamespace;

		// Token: 0x04002585 RID: 9605
		protected object ReflectInfo;
	}
}
