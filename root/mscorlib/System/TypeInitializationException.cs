using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	public sealed class TypeInitializationException : SystemException
	{
		// Token: 0x060011D6 RID: 4566 RVA: 0x000487B0 File Offset: 0x000469B0
		private TypeInitializationException()
			: base("Type constructor threw an exception.")
		{
			base.HResult = -2146233036;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000487C8 File Offset: 0x000469C8
		public TypeInitializationException(string fullTypeName, Exception innerException)
			: this(fullTypeName, SR.Format("The type initializer for '{0}' threw an exception.", fullTypeName), innerException)
		{
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000487DD File Offset: 0x000469DD
		internal TypeInitializationException(string message)
			: base(message)
		{
			base.HResult = -2146233036;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000487F1 File Offset: 0x000469F1
		internal TypeInitializationException(string fullTypeName, string message, Exception innerException)
			: base(message, innerException)
		{
			this._typeName = fullTypeName;
			base.HResult = -2146233036;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0004880D File Offset: 0x00046A0D
		internal TypeInitializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._typeName = info.GetString("TypeName");
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00048828 File Offset: 0x00046A28
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("TypeName", this.TypeName, typeof(string));
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004884D File Offset: 0x00046A4D
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		// Token: 0x04001242 RID: 4674
		private string _typeName;
	}
}
