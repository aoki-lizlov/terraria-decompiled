using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000827 RID: 2087
	[Serializable]
	public sealed class SwitchExpressionException : InvalidOperationException
	{
		// Token: 0x060046BD RID: 18109 RVA: 0x000E79AD File Offset: 0x000E5BAD
		public SwitchExpressionException()
			: base("Non-exhaustive switch expression failed to match its input.")
		{
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x000E79BA File Offset: 0x000E5BBA
		public SwitchExpressionException(Exception innerException)
			: base("Non-exhaustive switch expression failed to match its input.", innerException)
		{
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000E79C8 File Offset: 0x000E5BC8
		public SwitchExpressionException(object unmatchedValue)
			: this()
		{
			this.UnmatchedValue = unmatchedValue;
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000E79D7 File Offset: 0x000E5BD7
		private SwitchExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.UnmatchedValue = info.GetValue("UnmatchedValue", typeof(object));
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x000E79FC File Offset: 0x000E5BFC
		public SwitchExpressionException(string message)
			: base(message)
		{
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x000E7A05 File Offset: 0x000E5C05
		public SwitchExpressionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x000E7A0F File Offset: 0x000E5C0F
		public object UnmatchedValue
		{
			[CompilerGenerated]
			get
			{
				return this.<UnmatchedValue>k__BackingField;
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x000E7A17 File Offset: 0x000E5C17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("UnmatchedValue", this.UnmatchedValue, typeof(object));
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060046C5 RID: 18117 RVA: 0x000E7A3C File Offset: 0x000E5C3C
		public override string Message
		{
			get
			{
				if (this.UnmatchedValue == null)
				{
					return base.Message;
				}
				string text = SR.Format("Unmatched value was {0}.", this.UnmatchedValue.ToString());
				return base.Message + Environment.NewLine + text;
			}
		}

		// Token: 0x04002D20 RID: 11552
		[CompilerGenerated]
		private readonly object <UnmatchedValue>k__BackingField;
	}
}
