using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B6 RID: 182
	public struct JEnumerable<T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable, IEquatable<JEnumerable<T>> where T : JToken
	{
		// Token: 0x060008BE RID: 2238 RVA: 0x00024474 File Offset: 0x00022674
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00024488 File Offset: 0x00022688
		public IEnumerator<T> GetEnumerator()
		{
			return (this._enumerable ?? JEnumerable<T>.Empty).GetEnumerator();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000244A3 File Offset: 0x000226A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170001D3 RID: 467
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				if (this._enumerable == null)
				{
					return JEnumerable<JToken>.Empty;
				}
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000244D6 File Offset: 0x000226D6
		public bool Equals(JEnumerable<T> other)
		{
			return object.Equals(this._enumerable, other._enumerable);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000244E9 File Offset: 0x000226E9
		public override bool Equals(object obj)
		{
			return obj is JEnumerable<T> && this.Equals((JEnumerable<T>)obj);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00024501 File Offset: 0x00022701
		public override int GetHashCode()
		{
			if (this._enumerable == null)
			{
				return 0;
			}
			return this._enumerable.GetHashCode();
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00024518 File Offset: 0x00022718
		// Note: this type is marked as 'beforefieldinit'.
		static JEnumerable()
		{
		}

		// Token: 0x04000343 RID: 835
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x04000344 RID: 836
		private readonly IEnumerable<T> _enumerable;
	}
}
