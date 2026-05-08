using System;
using System.Dynamic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004A RID: 74
	internal class NoThrowGetBinderMember : GetMemberBinder
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0001055A File Offset: 0x0000E75A
		public NoThrowGetBinderMember(GetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010578 File Offset: 0x0000E778
		public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, CollectionUtils.ArrayEmpty<DynamicMetaObject>());
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x040001E6 RID: 486
		private readonly GetMemberBinder _innerBinder;
	}
}
