using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000123 RID: 291
	[Serializable]
	public class MissingMethodException : MissingMemberException
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0002D70E File Offset: 0x0002B90E
		public MissingMethodException()
			: base("Attempted to access a missing method.")
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002D726 File Offset: 0x0002B926
		public MissingMethodException(string message)
			: base(message)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002D73A File Offset: 0x0002B93A
		public MissingMethodException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233069;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002D74F File Offset: 0x0002B94F
		public MissingMethodException(string className, string methodName)
		{
			this.ClassName = className;
			this.MemberName = methodName;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002D765 File Offset: 0x0002B965
		protected MissingMethodException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002D770 File Offset: 0x0002B970
		public override string Message
		{
			get
			{
				if (this.ClassName != null)
				{
					return SR.Format("Method '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
				}
				return base.Message;
			}
		}
	}
}
