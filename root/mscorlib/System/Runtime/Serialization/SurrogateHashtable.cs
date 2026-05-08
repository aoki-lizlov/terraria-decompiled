using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x02000648 RID: 1608
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x06003D5A RID: 15706 RVA: 0x000D5360 File Offset: 0x000D3560
		internal SurrogateHashtable(int size)
			: base(size)
		{
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000D536C File Offset: 0x000D356C
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
