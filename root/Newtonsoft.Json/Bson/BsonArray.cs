using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x020000F7 RID: 247
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0003017A File Offset: 0x0002E37A
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0002818B File Offset: 0x0002638B
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0003018F File Offset: 0x0002E38F
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000301A1 File Offset: 0x0002E3A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000301A9 File Offset: 0x0002E3A9
		public BsonArray()
		{
		}

		// Token: 0x040003E0 RID: 992
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
