using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B1 RID: 177
	public interface IJEnumerable<out T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x170001B9 RID: 441
		IJEnumerable<JToken> this[object key] { get; }
	}
}
