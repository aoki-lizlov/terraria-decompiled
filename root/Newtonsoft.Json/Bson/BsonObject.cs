using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F6 RID: 246
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00030120 File Offset: 0x0002E320
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x000232C5 File Offset: 0x000214C5
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003014D File Offset: 0x0002E34D
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003015F File Offset: 0x0002E35F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00030167 File Offset: 0x0002E367
		public BsonObject()
		{
		}

		// Token: 0x040003DF RID: 991
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
