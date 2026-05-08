using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000C7 RID: 199
	internal abstract class QueryExpression
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0002A456 File Offset: 0x00028656
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0002A45E File Offset: 0x0002865E
		public QueryOperator Operator
		{
			[CompilerGenerated]
			get
			{
				return this.<Operator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Operator>k__BackingField = value;
			}
		}

		// Token: 0x06000A7D RID: 2685
		public abstract bool IsMatch(JToken root, JToken t);

		// Token: 0x06000A7E RID: 2686 RVA: 0x00008020 File Offset: 0x00006220
		protected QueryExpression()
		{
		}

		// Token: 0x0400038C RID: 908
		[CompilerGenerated]
		private QueryOperator <Operator>k__BackingField;
	}
}
