using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x020004DB RID: 1243
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060032DB RID: 13019 RVA: 0x000BC30B File Offset: 0x000BA50B
		public AuthorizationRuleCollection()
		{
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000BC313 File Offset: 0x000BA513
		internal AuthorizationRuleCollection(AuthorizationRule[] rules)
		{
			base.InnerList.AddRange(rules);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000BC327 File Offset: 0x000BA527
		public void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		// Token: 0x170006EB RID: 1771
		public AuthorizationRule this[int index]
		{
			get
			{
				return (AuthorizationRule)base.InnerList[index];
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000BC349 File Offset: 0x000BA549
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			base.InnerList.CopyTo(rules, index);
		}
	}
}
