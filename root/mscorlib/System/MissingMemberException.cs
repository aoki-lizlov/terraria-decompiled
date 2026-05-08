using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public class MissingMemberException : MemberAccessException
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x0004F550 File Offset: 0x0004D750
		public MissingMemberException()
			: base("Attempted to access a missing member.")
		{
			base.HResult = -2146233070;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0004F568 File Offset: 0x0004D768
		public MissingMemberException(string message)
			: base(message)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004F57C File Offset: 0x0004D77C
		public MissingMemberException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004F591 File Offset: 0x0004D791
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004F5A8 File Offset: 0x0004D7A8
		protected MissingMemberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004F600 File Offset: 0x0004D800
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName, typeof(string));
			info.AddValue("MMMemberName", this.MemberName, typeof(string));
			info.AddValue("MMSignature", this.Signature, typeof(byte[]));
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004F668 File Offset: 0x0004D868
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				return SR.Format("Member '{0}' not found.", this.ClassName + "." + this.MemberName + ((this.Signature != null) ? (" " + MissingMemberException.FormatSignature(this.Signature)) : string.Empty));
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00004091 File Offset: 0x00002291
		internal static string FormatSignature(byte[] signature)
		{
			return string.Empty;
		}

		// Token: 0x04001341 RID: 4929
		protected string ClassName;

		// Token: 0x04001342 RID: 4930
		protected string MemberName;

		// Token: 0x04001343 RID: 4931
		protected byte[] Signature;
	}
}
