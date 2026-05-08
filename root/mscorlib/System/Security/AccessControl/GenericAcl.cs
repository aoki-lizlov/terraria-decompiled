using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x020004FB RID: 1275
	public abstract class GenericAcl : ICollection, IEnumerable
	{
		// Token: 0x060033F8 RID: 13304 RVA: 0x000BEE03 File Offset: 0x000BD003
		static GenericAcl()
		{
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000025BE File Offset: 0x000007BE
		protected GenericAcl()
		{
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060033FA RID: 13306
		public abstract int BinaryLength { get; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060033FB RID: 13307
		public abstract int Count { get; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700071E RID: 1822
		public abstract GenericAce this[int index] { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060033FF RID: 13311
		public abstract byte Revision { get; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000025CE File Offset: 0x000007CE
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000BEE1C File Offset: 0x000BD01C
		public void CopyTo(GenericAce[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0 || array.Length - index < this.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be non-negative integer and must not exceed array length - count");
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000BEE75 File Offset: 0x000BD075
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((GenericAce[])array, index);
		}

		// Token: 0x06003403 RID: 13315
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		// Token: 0x06003404 RID: 13316 RVA: 0x000BEE84 File Offset: 0x000BD084
		public AceEnumerator GetEnumerator()
		{
			return new AceEnumerator(this);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000BEE8C File Offset: 0x000BD08C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003406 RID: 13318
		internal abstract string GetSddlForm(ControlFlags sdFlags, bool isDacl);

		// Token: 0x0400242C RID: 9260
		public static readonly byte AclRevision = 2;

		// Token: 0x0400242D RID: 9261
		public static readonly byte AclRevisionDS = 4;

		// Token: 0x0400242E RID: 9262
		public static readonly int MaxBinaryLength = 65536;
	}
}
