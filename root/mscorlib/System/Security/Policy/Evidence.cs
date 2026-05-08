using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003DB RID: 987
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : ICollection, IEnumerable
	{
		// Token: 0x06002A00 RID: 10752 RVA: 0x000025BE File Offset: 0x000007BE
		public Evidence()
		{
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000997F2 File Offset: 0x000979F2
		public Evidence(Evidence evidence)
		{
			if (evidence != null)
			{
				this.Merge(evidence);
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00099804 File Offset: 0x00097A04
		public Evidence(EvidenceBase[] hostEvidence, EvidenceBase[] assemblyEvidence)
		{
			if (hostEvidence != null)
			{
				this.HostEvidenceList.AddRange(hostEvidence);
			}
			if (assemblyEvidence != null)
			{
				this.AssemblyEvidenceList.AddRange(assemblyEvidence);
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00099804 File Offset: 0x00097A04
		[Obsolete]
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			if (hostEvidence != null)
			{
				this.HostEvidenceList.AddRange(hostEvidence);
			}
			if (assemblyEvidence != null)
			{
				this.AssemblyEvidenceList.AddRange(assemblyEvidence);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0009982C File Offset: 0x00097A2C
		[Obsolete]
		public int Count
		{
			get
			{
				int num = 0;
				if (this.hostEvidenceList != null)
				{
					num += this.hostEvidenceList.Count;
				}
				if (this.assemblyEvidenceList != null)
				{
					num += this.assemblyEvidenceList.Count;
				}
				return num;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002A06 RID: 10758 RVA: 0x0000408A File Offset: 0x0000228A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002A07 RID: 10759 RVA: 0x00099868 File Offset: 0x00097A68
		// (set) Token: 0x06002A08 RID: 10760 RVA: 0x00099870 File Offset: 0x00097A70
		public bool Locked
		{
			get
			{
				return this._locked;
			}
			[SecurityPermission(SecurityAction.Demand, ControlEvidence = true)]
			set
			{
				this._locked = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000025CE File Offset: 0x000007CE
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x00099879 File Offset: 0x00097A79
		internal ArrayList HostEvidenceList
		{
			get
			{
				if (this.hostEvidenceList == null)
				{
					this.hostEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.hostEvidenceList;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x00099899 File Offset: 0x00097A99
		internal ArrayList AssemblyEvidenceList
		{
			get
			{
				if (this.assemblyEvidenceList == null)
				{
					this.assemblyEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.assemblyEvidenceList;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000998B9 File Offset: 0x00097AB9
		[Obsolete]
		public void AddAssembly(object id)
		{
			this.AssemblyEvidenceList.Add(id);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000998C8 File Offset: 0x00097AC8
		[Obsolete]
		public void AddHost(object id)
		{
			if (this._locked && SecurityManager.SecurityEnabled)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.HostEvidenceList.Add(id);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000998F2 File Offset: 0x00097AF2
		[ComVisible(false)]
		public void Clear()
		{
			if (this.hostEvidenceList != null)
			{
				this.hostEvidenceList.Clear();
			}
			if (this.assemblyEvidenceList != null)
			{
				this.assemblyEvidenceList.Clear();
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0009991A File Offset: 0x00097B1A
		[ComVisible(false)]
		public Evidence Clone()
		{
			return new Evidence(this);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00099924 File Offset: 0x00097B24
		[Obsolete]
		public void CopyTo(Array array, int index)
		{
			int num = 0;
			if (this.hostEvidenceList != null)
			{
				num = this.hostEvidenceList.Count;
				if (num > 0)
				{
					this.hostEvidenceList.CopyTo(array, index);
				}
			}
			if (this.assemblyEvidenceList != null && this.assemblyEvidenceList.Count > 0)
			{
				this.assemblyEvidenceList.CopyTo(array, index + num);
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00099980 File Offset: 0x00097B80
		[Obsolete]
		public IEnumerator GetEnumerator()
		{
			IEnumerator enumerator = null;
			if (this.hostEvidenceList != null)
			{
				enumerator = this.hostEvidenceList.GetEnumerator();
			}
			IEnumerator enumerator2 = null;
			if (this.assemblyEvidenceList != null)
			{
				enumerator2 = this.assemblyEvidenceList.GetEnumerator();
			}
			return new Evidence.EvidenceEnumerator(enumerator, enumerator2);
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x000999C0 File Offset: 0x00097BC0
		public IEnumerator GetAssemblyEnumerator()
		{
			return this.AssemblyEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x000999CD File Offset: 0x00097BCD
		public IEnumerator GetHostEnumerator()
		{
			return this.HostEvidenceList.GetEnumerator();
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000999DC File Offset: 0x00097BDC
		public void Merge(Evidence evidence)
		{
			if (evidence != null && evidence.Count > 0)
			{
				if (evidence.hostEvidenceList != null)
				{
					foreach (object obj in evidence.hostEvidenceList)
					{
						this.AddHost(obj);
					}
				}
				if (evidence.assemblyEvidenceList != null)
				{
					foreach (object obj2 in evidence.assemblyEvidenceList)
					{
						this.AddAssembly(obj2);
					}
				}
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00099A94 File Offset: 0x00097C94
		[ComVisible(false)]
		public void RemoveType(Type t)
		{
			for (int i = this.hostEvidenceList.Count; i >= 0; i--)
			{
				if (this.hostEvidenceList.GetType() == t)
				{
					this.hostEvidenceList.RemoveAt(i);
				}
			}
			for (int j = this.assemblyEvidenceList.Count; j >= 0; j--)
			{
				if (this.assemblyEvidenceList.GetType() == t)
				{
					this.assemblyEvidenceList.RemoveAt(j);
				}
			}
		}

		// Token: 0x06002A16 RID: 10774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAuthenticodePresent(Assembly a);

		// Token: 0x06002A17 RID: 10775 RVA: 0x00099B0C File Offset: 0x00097D0C
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static Evidence GetDefaultHostEvidence(Assembly a)
		{
			Evidence evidence = new Evidence();
			string escapedCodeBase = a.EscapedCodeBase;
			evidence.AddHost(Zone.CreateFromUrl(escapedCodeBase));
			evidence.AddHost(new Url(escapedCodeBase));
			evidence.AddHost(new Hash(a));
			if (string.Compare("FILE://", 0, escapedCodeBase, 0, 7, true, CultureInfo.InvariantCulture) != 0)
			{
				evidence.AddHost(Site.CreateFromUrl(escapedCodeBase));
			}
			AssemblyName name = a.GetName();
			byte[] publicKey = name.GetPublicKey();
			if (publicKey != null && publicKey.Length != 0)
			{
				StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(publicKey);
				evidence.AddHost(new StrongName(strongNamePublicKeyBlob, name.Name, name.Version));
			}
			if (Evidence.IsAuthenticodePresent(a))
			{
				try
				{
					X509Certificate x509Certificate = X509Certificate.CreateFromSignedFile(a.Location);
					evidence.AddHost(new Publisher(x509Certificate));
				}
				catch (CryptographicException)
				{
				}
			}
			if (a.GlobalAssemblyCache)
			{
				evidence.AddHost(new GacInstalled());
			}
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			if (domainManager != null && (domainManager.HostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				evidence = domainManager.HostSecurityManager.ProvideAssemblyEvidence(a, evidence);
			}
			return evidence;
		}

		// Token: 0x04001E5B RID: 7771
		private bool _locked;

		// Token: 0x04001E5C RID: 7772
		private ArrayList hostEvidenceList;

		// Token: 0x04001E5D RID: 7773
		private ArrayList assemblyEvidenceList;

		// Token: 0x020003DC RID: 988
		private class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06002A18 RID: 10776 RVA: 0x00099C20 File Offset: 0x00097E20
			public EvidenceEnumerator(IEnumerator hostenum, IEnumerator assemblyenum)
			{
				this.hostEnum = hostenum;
				this.assemblyEnum = assemblyenum;
				this.currentEnum = this.hostEnum;
			}

			// Token: 0x06002A19 RID: 10777 RVA: 0x00099C44 File Offset: 0x00097E44
			public bool MoveNext()
			{
				if (this.currentEnum == null)
				{
					return false;
				}
				bool flag = this.currentEnum.MoveNext();
				if (!flag && this.hostEnum == this.currentEnum && this.assemblyEnum != null)
				{
					this.currentEnum = this.assemblyEnum;
					flag = this.assemblyEnum.MoveNext();
				}
				return flag;
			}

			// Token: 0x06002A1A RID: 10778 RVA: 0x00099C9C File Offset: 0x00097E9C
			public void Reset()
			{
				if (this.hostEnum != null)
				{
					this.hostEnum.Reset();
					this.currentEnum = this.hostEnum;
				}
				else
				{
					this.currentEnum = this.assemblyEnum;
				}
				if (this.assemblyEnum != null)
				{
					this.assemblyEnum.Reset();
				}
			}

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00099CE9 File Offset: 0x00097EE9
			public object Current
			{
				get
				{
					return this.currentEnum.Current;
				}
			}

			// Token: 0x04001E5E RID: 7774
			private IEnumerator currentEnum;

			// Token: 0x04001E5F RID: 7775
			private IEnumerator hostEnum;

			// Token: 0x04001E60 RID: 7776
			private IEnumerator assemblyEnum;
		}
	}
}
