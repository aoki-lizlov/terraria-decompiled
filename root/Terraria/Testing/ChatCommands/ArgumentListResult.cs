using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Testing.ChatCommands
{
	// Token: 0x0200011B RID: 283
	public class ArgumentListResult : IEnumerable<string>, IEnumerable
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x004FAB07 File Offset: 0x004F8D07
		public int Count
		{
			get
			{
				return this._results.Count;
			}
		}

		// Token: 0x170002E0 RID: 736
		public string this[int index]
		{
			get
			{
				return this._results[index];
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x004FAB22 File Offset: 0x004F8D22
		public ArgumentListResult(IEnumerable<string> results)
		{
			this._results = results.ToList<string>();
			this.IsValid = true;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x004FAB3D File Offset: 0x004F8D3D
		private ArgumentListResult(bool isValid)
		{
			this._results = new List<string>();
			this.IsValid = isValid;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x004FAB57 File Offset: 0x004F8D57
		public IEnumerator<string> GetEnumerator()
		{
			return this._results.GetEnumerator();
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x004FAB69 File Offset: 0x004F8D69
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x004FAB71 File Offset: 0x004F8D71
		// Note: this type is marked as 'beforefieldinit'.
		static ArgumentListResult()
		{
		}

		// Token: 0x0400155F RID: 5471
		public static readonly ArgumentListResult Empty = new ArgumentListResult(true);

		// Token: 0x04001560 RID: 5472
		public static readonly ArgumentListResult Invalid = new ArgumentListResult(false);

		// Token: 0x04001561 RID: 5473
		public readonly bool IsValid;

		// Token: 0x04001562 RID: 5474
		private readonly List<string> _results;
	}
}
