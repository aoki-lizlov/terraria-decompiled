using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C8 RID: 200
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002A467 File Offset: 0x00028667
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0002A46F File Offset: 0x0002866F
		public List<QueryExpression> Expressions
		{
			[CompilerGenerated]
			get
			{
				return this.<Expressions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expressions>k__BackingField = value;
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002A478 File Offset: 0x00028678
		public CompositeExpression()
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002A48C File Offset: 0x0002868C
		public override bool IsMatch(JToken root, JToken t)
		{
			QueryOperator @operator = base.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400038D RID: 909
		[CompilerGenerated]
		private List<QueryExpression> <Expressions>k__BackingField;
	}
}
