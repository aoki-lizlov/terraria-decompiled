using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public class MissingFieldException : MissingMemberException, ISerializable
	{
		// Token: 0x060013B2 RID: 5042 RVA: 0x0004F4AE File Offset: 0x0004D6AE
		public MissingFieldException()
			: base("Attempted to access a non-existing field.")
		{
			base.HResult = -2146233071;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004F4C6 File Offset: 0x0004D6C6
		public MissingFieldException(string message)
			: base(message)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004F4DA File Offset: 0x0004D6DA
		public MissingFieldException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233071;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0002D74F File Offset: 0x0002B94F
		public MissingFieldException(string className, string fieldName)
		{
			this.ClassName = className;
			this.MemberName = fieldName;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0002D765 File Offset: 0x0002B965
		protected MissingFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0004F4F0 File Offset: 0x0004D6F0
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Field '{0}' not found.", ((this.Signature != null) ? (MissingMemberException.FormatSignature(this.Signature) + " ") : "") + this.ClassName + "." + this.MemberName);
			}
		}
	}
}
