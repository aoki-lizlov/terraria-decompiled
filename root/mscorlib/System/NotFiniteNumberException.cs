using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	public class NotFiniteNumberException : ArithmeticException
	{
		// Token: 0x06000C05 RID: 3077 RVA: 0x0002D811 File Offset: 0x0002BA11
		public NotFiniteNumberException()
			: base("Arg_NotFiniteNumberException = Number encountered was not a finite quantity.")
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002D838 File Offset: 0x0002BA38
		public NotFiniteNumberException(double offendingNumber)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002D852 File Offset: 0x0002BA52
		public NotFiniteNumberException(string message)
			: base(message)
		{
			this._offendingNumber = 0.0;
			base.HResult = -2146233048;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002D875 File Offset: 0x0002BA75
		public NotFiniteNumberException(string message, double offendingNumber)
			: base(message)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0002D890 File Offset: 0x0002BA90
		public NotFiniteNumberException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233048;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002D8A5 File Offset: 0x0002BAA5
		public NotFiniteNumberException(string message, double offendingNumber, Exception innerException)
			: base(message, innerException)
		{
			this._offendingNumber = offendingNumber;
			base.HResult = -2146233048;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002D8C1 File Offset: 0x0002BAC1
		protected NotFiniteNumberException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._offendingNumber = (double)info.GetInt32("OffendingNumber");
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002D8DD File Offset: 0x0002BADD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OffendingNumber", this._offendingNumber, typeof(int));
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002D907 File Offset: 0x0002BB07
		public double OffendingNumber
		{
			get
			{
				return this._offendingNumber;
			}
		}

		// Token: 0x04001107 RID: 4359
		private double _offendingNumber;
	}
}
