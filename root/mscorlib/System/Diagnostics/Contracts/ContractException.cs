using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000A2A RID: 2602
	[Serializable]
	internal sealed class ContractException : Exception
	{
		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x0014D197 File Offset: 0x0014B397
		public ContractFailureKind Kind
		{
			get
			{
				return this._Kind;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x0014D19F File Offset: 0x0014B39F
		public string Failure
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x0014D1A7 File Offset: 0x0014B3A7
		public string UserMessage
		{
			get
			{
				return this._UserMessage;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600603F RID: 24639 RVA: 0x0014D1AF File Offset: 0x0014B3AF
		public string Condition
		{
			get
			{
				return this._Condition;
			}
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x0014D1B7 File Offset: 0x0014B3B7
		private ContractException()
		{
			base.HResult = -2146233022;
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x0014D1CA File Offset: 0x0014B3CA
		public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
			: base(failure, innerException)
		{
			base.HResult = -2146233022;
			this._Kind = kind;
			this._UserMessage = userMessage;
			this._Condition = condition;
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x0014D1F6 File Offset: 0x0014B3F6
		private ContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._Kind = (ContractFailureKind)info.GetInt32("Kind");
			this._UserMessage = info.GetString("UserMessage");
			this._Condition = info.GetString("Condition");
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x0014D234 File Offset: 0x0014B434
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Kind", this._Kind);
			info.AddValue("UserMessage", this._UserMessage);
			info.AddValue("Condition", this._Condition);
		}

		// Token: 0x040039E7 RID: 14823
		private readonly ContractFailureKind _Kind;

		// Token: 0x040039E8 RID: 14824
		private readonly string _UserMessage;

		// Token: 0x040039E9 RID: 14825
		private readonly string _Condition;
	}
}
