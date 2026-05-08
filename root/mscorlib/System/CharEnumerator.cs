using System;
using System.Collections;
using System.Collections.Generic;

namespace System
{
	// Token: 0x020000CD RID: 205
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, IEnumerator<char>, IDisposable, ICloneable
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x0001AB47 File Offset: 0x00018D47
		internal CharEnumerator(string str)
		{
			this._str = str;
			this._index = -1;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001AB5D File Offset: 0x00018D5D
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001AB68 File Offset: 0x00018D68
		public bool MoveNext()
		{
			if (this._index < this._str.Length - 1)
			{
				this._index++;
				this._currentElement = this._str[this._index];
				return true;
			}
			this._index = this._str.Length;
			return false;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001ABC3 File Offset: 0x00018DC3
		public void Dispose()
		{
			if (this._str != null)
			{
				this._index = this._str.Length;
			}
			this._str = null;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001ABF2 File Offset: 0x00018DF2
		public char Current
		{
			get
			{
				if (this._index == -1)
				{
					throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
				}
				if (this._index >= this._str.Length)
				{
					throw new InvalidOperationException("Enumeration already finished.");
				}
				return this._currentElement;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001AC2C File Offset: 0x00018E2C
		public void Reset()
		{
			this._currentElement = '\0';
			this._index = -1;
		}

		// Token: 0x04000F04 RID: 3844
		private string _str;

		// Token: 0x04000F05 RID: 3845
		private int _index;

		// Token: 0x04000F06 RID: 3846
		private char _currentElement;
	}
}
