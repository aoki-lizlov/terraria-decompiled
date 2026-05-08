using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008B RID: 139
	internal struct ResolverContractKey : IEquatable<ResolverContractKey>
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x00019658 File Offset: 0x00017858
		public ResolverContractKey(Type resolverType, Type contractType)
		{
			this._resolverType = resolverType;
			this._contractType = contractType;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00019668 File Offset: 0x00017868
		public override int GetHashCode()
		{
			return this._resolverType.GetHashCode() ^ this._contractType.GetHashCode();
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00019681 File Offset: 0x00017881
		public override bool Equals(object obj)
		{
			return obj is ResolverContractKey && this.Equals((ResolverContractKey)obj);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00019699 File Offset: 0x00017899
		public bool Equals(ResolverContractKey other)
		{
			return this._resolverType == other._resolverType && this._contractType == other._contractType;
		}

		// Token: 0x04000290 RID: 656
		private readonly Type _resolverType;

		// Token: 0x04000291 RID: 657
		private readonly Type _contractType;
	}
}
