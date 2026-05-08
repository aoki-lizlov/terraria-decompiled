using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200090A RID: 2314
	[ComVisible(true)]
	public readonly struct OpCode : IEquatable<OpCode>
	{
		// Token: 0x06005149 RID: 20809 RVA: 0x000FFF20 File Offset: 0x000FE120
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)((p >> 8) & 255);
			this.push = (byte)((p >> 16) & 255);
			this.pop = (byte)((p >> 24) & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)((q >> 8) & 255);
			this.args = (byte)((q >> 16) & 255);
			this.flow = (byte)((q >> 24) & 255);
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x000FFFAD File Offset: 0x000FE1AD
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x000FFFBC File Offset: 0x000FE1BC
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x000FFFFB File Offset: 0x000FE1FB
		public bool Equals(OpCode obj)
		{
			return obj.op1 == this.op1 && obj.op2 == this.op2;
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x0010001B File Offset: 0x000FE21B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x00100023 File Offset: 0x000FE223
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x00100051 File Offset: 0x000FE251
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x00100059 File Offset: 0x000FE259
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.type;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x00100061 File Offset: 0x000FE261
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x00100069 File Offset: 0x000FE269
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x00100071 File Offset: 0x000FE271
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x00100079 File Offset: 0x000FE279
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x00100081 File Offset: 0x000FE281
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)(((int)this.op1 << 8) | (int)this.op2);
			}
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x001000A3 File Offset: 0x000FE2A3
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x001000C3 File Offset: 0x000FE2C3
		public static bool operator !=(OpCode a, OpCode b)
		{
			return a.op1 != b.op1 || a.op2 != b.op2;
		}

		// Token: 0x0400317E RID: 12670
		internal readonly byte op1;

		// Token: 0x0400317F RID: 12671
		internal readonly byte op2;

		// Token: 0x04003180 RID: 12672
		private readonly byte push;

		// Token: 0x04003181 RID: 12673
		private readonly byte pop;

		// Token: 0x04003182 RID: 12674
		private readonly byte size;

		// Token: 0x04003183 RID: 12675
		private readonly byte type;

		// Token: 0x04003184 RID: 12676
		private readonly byte args;

		// Token: 0x04003185 RID: 12677
		private readonly byte flow;
	}
}
