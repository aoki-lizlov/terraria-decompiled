using System;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A2C RID: 2604
	public readonly struct SymbolToken
	{
		// Token: 0x06006046 RID: 24646 RVA: 0x0014D299 File Offset: 0x0014B499
		public SymbolToken(int val)
		{
			this._token = val;
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x0014D2A2 File Offset: 0x0014B4A2
		public int GetToken()
		{
			return this._token;
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x0014D2A2 File Offset: 0x0014B4A2
		public override int GetHashCode()
		{
			return this._token;
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x0014D2AA File Offset: 0x0014B4AA
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && this.Equals((SymbolToken)obj);
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x0014D2C2 File Offset: 0x0014B4C2
		public bool Equals(SymbolToken obj)
		{
			return obj._token == this._token;
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x0014D2D2 File Offset: 0x0014B4D2
		public static bool operator ==(SymbolToken a, SymbolToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x0014D2DC File Offset: 0x0014B4DC
		public static bool operator !=(SymbolToken a, SymbolToken b)
		{
			return !(a == b);
		}

		// Token: 0x040039EA RID: 14826
		private readonly int _token;
	}
}
