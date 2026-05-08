using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020003D4 RID: 980
	[ComVisible(true)]
	public sealed class ApplicationTrustCollection : ICollection, IEnumerable
	{
		// Token: 0x060029AE RID: 10670 RVA: 0x00098715 File Offset: 0x00096915
		internal ApplicationTrustCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x00098728 File Offset: 0x00096928
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700052C RID: 1324
		public ApplicationTrust this[int index]
		{
			get
			{
				return (ApplicationTrust)this._list[index];
			}
		}

		// Token: 0x1700052D RID: 1325
		public ApplicationTrust this[string appFullName]
		{
			get
			{
				for (int i = 0; i < this._list.Count; i++)
				{
					ApplicationTrust applicationTrust = this._list[i] as ApplicationTrust;
					if (applicationTrust.ApplicationIdentity.FullName == appFullName)
					{
						return applicationTrust;
					}
				}
				return null;
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00098793 File Offset: 0x00096993
		public int Add(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			return this._list.Add(trust);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000987CC File Offset: 0x000969CC
		public void AddRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x00098828 File Offset: 0x00096A28
		public void AddRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				if (applicationTrust.ApplicationIdentity == null)
				{
					throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
				}
				this._list.Add(applicationTrust);
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00098885 File Offset: 0x00096A85
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00098892 File Offset: 0x00096A92
		public void CopyTo(ApplicationTrust[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00098892 File Offset: 0x00096A92
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000988A4 File Offset: 0x00096AA4
		public ApplicationTrustCollection Find(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			string text = applicationIdentity.FullName;
			if (versionMatch != ApplicationVersionMatch.MatchExactVersion)
			{
				if (versionMatch != ApplicationVersionMatch.MatchAllVersions)
				{
					throw new ArgumentException("versionMatch");
				}
				int num = text.IndexOf(", Version=");
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
			}
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection();
			foreach (object obj in this._list)
			{
				ApplicationTrust applicationTrust = (ApplicationTrust)obj;
				if (applicationTrust.ApplicationIdentity.FullName.StartsWith(text))
				{
					applicationTrustCollection.Add(applicationTrust);
				}
			}
			return applicationTrustCollection;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x00098960 File Offset: 0x00096B60
		public ApplicationTrustEnumerator GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x00098960 File Offset: 0x00096B60
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ApplicationTrustEnumerator(this);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00098968 File Offset: 0x00096B68
		public void Remove(ApplicationTrust trust)
		{
			if (trust == null)
			{
				throw new ArgumentNullException("trust");
			}
			if (trust.ApplicationIdentity == null)
			{
				throw new ArgumentException(Locale.GetText("ApplicationTrust.ApplicationIdentity can't be null."), "trust");
			}
			this.RemoveAllInstances(trust);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x0009899C File Offset: 0x00096B9C
		public void Remove(ApplicationIdentity applicationIdentity, ApplicationVersionMatch versionMatch)
		{
			foreach (ApplicationTrust applicationTrust in this.Find(applicationIdentity, versionMatch))
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000989D0 File Offset: 0x00096BD0
		public void RemoveRange(ApplicationTrust[] trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00098A08 File Offset: 0x00096C08
		public void RemoveRange(ApplicationTrustCollection trusts)
		{
			if (trusts == null)
			{
				throw new ArgumentNullException("trusts");
			}
			foreach (ApplicationTrust applicationTrust in trusts)
			{
				this.RemoveAllInstances(applicationTrust);
			}
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00098A44 File Offset: 0x00096C44
		internal void RemoveAllInstances(ApplicationTrust trust)
		{
			for (int i = this._list.Count - 1; i >= 0; i--)
			{
				if (trust.Equals(this._list[i]))
				{
					this._list.RemoveAt(i);
				}
			}
		}

		// Token: 0x04001E2C RID: 7724
		private ArrayList _list;
	}
}
