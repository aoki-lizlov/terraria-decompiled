using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000AB RID: 171
	internal struct StateHash : IEquatable<StateHash>
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x0002E260 File Offset: 0x0002C460
		public StateHash(ulong a, ulong b)
		{
			this.a = a;
			this.b = b;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0002E270 File Offset: 0x0002C470
		public override string ToString()
		{
			return Convert.ToString((long)this.a, 2).PadLeft(64, '0') + "|" + Convert.ToString((long)this.b, 2).PadLeft(64, '0');
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0002E2A6 File Offset: 0x0002C4A6
		public bool Equals(StateHash hash)
		{
			return this.a == hash.a && this.b == hash.b;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0002E2C8 File Offset: 0x0002C4C8
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			StateHash stateHash = (StateHash)obj;
			return this.a == stateHash.a && this.b == stateHash.b;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0002E31C File Offset: 0x0002C51C
		public override int GetHashCode()
		{
			int num = (int)(this.a ^ (this.a >> 32));
			int num2 = (int)(this.b ^ (this.b >> 32));
			return num + num2;
		}

		// Token: 0x04000918 RID: 2328
		private readonly ulong a;

		// Token: 0x04000919 RID: 2329
		private readonly ulong b;
	}
}
