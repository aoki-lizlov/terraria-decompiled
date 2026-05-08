using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020009B0 RID: 2480
	[Serializable]
	public class CultureNotFoundException : ArgumentException
	{
		// Token: 0x06005AB0 RID: 23216 RVA: 0x00133B82 File Offset: 0x00131D82
		public CultureNotFoundException()
			: base(CultureNotFoundException.DefaultMessage)
		{
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x00133B8F File Offset: 0x00131D8F
		public CultureNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x00133B98 File Offset: 0x00131D98
		public CultureNotFoundException(string paramName, string message)
			: base(message, paramName)
		{
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x00133BA2 File Offset: 0x00131DA2
		public CultureNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x00133BAC File Offset: 0x00131DAC
		public CultureNotFoundException(string paramName, string invalidCultureName, string message)
			: base(message, paramName)
		{
			this._invalidCultureName = invalidCultureName;
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x00133BBD File Offset: 0x00131DBD
		public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
			: base(message, innerException)
		{
			this._invalidCultureName = invalidCultureName;
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x00133BCE File Offset: 0x00131DCE
		public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
			: base(message, innerException)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x00133BE4 File Offset: 0x00131DE4
		public CultureNotFoundException(string paramName, int invalidCultureId, string message)
			: base(message, paramName)
		{
			this._invalidCultureId = new int?(invalidCultureId);
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x00133BFC File Offset: 0x00131DFC
		protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._invalidCultureId = (int?)info.GetValue("InvalidCultureId", typeof(int?));
			this._invalidCultureName = (string)info.GetValue("InvalidCultureName", typeof(string));
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x00133C54 File Offset: 0x00131E54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InvalidCultureId", this._invalidCultureId, typeof(int?));
			info.AddValue("InvalidCultureName", this._invalidCultureName, typeof(string));
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06005ABA RID: 23226 RVA: 0x00133CA4 File Offset: 0x00131EA4
		public virtual int? InvalidCultureId
		{
			get
			{
				return this._invalidCultureId;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06005ABB RID: 23227 RVA: 0x00133CAC File Offset: 0x00131EAC
		public virtual string InvalidCultureName
		{
			get
			{
				return this._invalidCultureName;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06005ABC RID: 23228 RVA: 0x00133CB4 File Offset: 0x00131EB4
		private static string DefaultMessage
		{
			get
			{
				return "Culture is not supported.";
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005ABD RID: 23229 RVA: 0x00133CBC File Offset: 0x00131EBC
		private string FormatedInvalidCultureId
		{
			get
			{
				if (this.InvalidCultureId == null)
				{
					return this.InvalidCultureName;
				}
				return string.Format(CultureInfo.InvariantCulture, "{0} (0x{0:x4})", this.InvalidCultureId.Value);
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06005ABE RID: 23230 RVA: 0x00133D04 File Offset: 0x00131F04
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (this._invalidCultureId == null && this._invalidCultureName == null)
				{
					return message;
				}
				string text = SR.Format("{0} is an invalid culture identifier.", this.FormatedInvalidCultureId);
				if (message == null)
				{
					return text;
				}
				return message + Environment.NewLine + text;
			}
		}

		// Token: 0x04003600 RID: 13824
		private string _invalidCultureName;

		// Token: 0x04003601 RID: 13825
		private int? _invalidCultureId;
	}
}
