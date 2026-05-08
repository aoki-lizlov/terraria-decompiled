using System;
using System.Dynamic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004B RID: 75
	internal class NoThrowSetBinderMember : SetMemberBinder
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x000105B2 File Offset: 0x0000E7B2
		public NoThrowSetBinderMember(SetMemberBinder innerBinder)
			: base(innerBinder.Name, innerBinder.IgnoreCase)
		{
			this._innerBinder = innerBinder;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000105D0 File Offset: 0x0000E7D0
		public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
		{
			DynamicMetaObject dynamicMetaObject = this._innerBinder.Bind(target, new DynamicMetaObject[] { value });
			return new DynamicMetaObject(new NoThrowExpressionVisitor().Visit(dynamicMetaObject.Expression), dynamicMetaObject.Restrictions);
		}

		// Token: 0x040001E7 RID: 487
		private readonly SetMemberBinder _innerBinder;
	}
}
