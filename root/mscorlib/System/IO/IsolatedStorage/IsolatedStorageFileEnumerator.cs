using System;
using System.Collections;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000996 RID: 2454
	internal class IsolatedStorageFileEnumerator : IEnumerator
	{
		// Token: 0x0600599F RID: 22943 RVA: 0x0013059F File Offset: 0x0012E79F
		public IsolatedStorageFileEnumerator(IsolatedStorageScope scope, string root)
		{
			this._scope = scope;
			if (Directory.Exists(root))
			{
				this._storages = Directory.GetDirectories(root, "d.*");
			}
			this._pos = -1;
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060059A0 RID: 22944 RVA: 0x001305CE File Offset: 0x0012E7CE
		public object Current
		{
			get
			{
				if (this._pos < 0 || this._storages == null || this._pos >= this._storages.Length)
				{
					return null;
				}
				return new IsolatedStorageFile(this._scope, this._storages[this._pos]);
			}
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x0013060C File Offset: 0x0012E80C
		public bool MoveNext()
		{
			if (this._storages == null)
			{
				return false;
			}
			int num = this._pos + 1;
			this._pos = num;
			return num < this._storages.Length;
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x0013063E File Offset: 0x0012E83E
		public void Reset()
		{
			this._pos = -1;
		}

		// Token: 0x04003586 RID: 13702
		private IsolatedStorageScope _scope;

		// Token: 0x04003587 RID: 13703
		private string[] _storages;

		// Token: 0x04003588 RID: 13704
		private int _pos;
	}
}
