using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Mono.Security.Cryptography;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000994 RID: 2452
	[ComVisible(true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	public sealed class IsolatedStorageFile : IsolatedStorage, IDisposable
	{
		// Token: 0x0600595C RID: 22876 RVA: 0x0012F17E File Offset: 0x0012D37E
		public static IEnumerator GetEnumerator(IsolatedStorageScope scope)
		{
			IsolatedStorageFile.Demand(scope);
			if (scope != IsolatedStorageScope.User && scope != (IsolatedStorageScope.User | IsolatedStorageScope.Roaming) && scope != IsolatedStorageScope.Machine)
			{
				throw new ArgumentException(Locale.GetText("Invalid scope, only User, User|Roaming and Machine are valid"));
			}
			return new IsolatedStorageFileEnumerator(scope, IsolatedStorageFile.GetIsolatedStorageRoot(scope));
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x0012F1B0 File Offset: 0x0012D3B0
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Evidence domainEvidence, Type domainEvidenceType, Evidence assemblyEvidence, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			bool flag = (scope & IsolatedStorageScope.Domain) > IsolatedStorageScope.None;
			if (flag && domainEvidence == null)
			{
				throw new ArgumentNullException("domainEvidence");
			}
			bool flag2 = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag2 && assemblyEvidence == null)
			{
				throw new ArgumentNullException("assemblyEvidence");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				if (domainEvidenceType == null)
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(domainEvidence);
				}
				else
				{
					isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(domainEvidence, domainEvidenceType);
				}
				if (isolatedStorageFile._domainIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find domain identity."));
				}
			}
			if (flag2)
			{
				if (assemblyEvidenceType == null)
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(assemblyEvidence);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(assemblyEvidence, assemblyEvidenceType);
				}
				if (isolatedStorageFile._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("Couldn't find assembly identity."));
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x0012F280 File Offset: 0x0012D480
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object domainIdentity, object assemblyIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None && domainIdentity == null)
			{
				throw new ArgumentNullException("domainIdentity");
			}
			bool flag = (scope & IsolatedStorageScope.Assembly) > IsolatedStorageScope.None;
			if (flag && assemblyIdentity == null)
			{
				throw new ArgumentNullException("assemblyIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if (flag)
			{
				isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			}
			isolatedStorageFile._domainIdentity = domainIdentity;
			isolatedStorageFile._assemblyIdentity = assemblyIdentity;
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x0012F2EC File Offset: 0x0012D4EC
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type domainEvidenceType, Type assemblyEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
			{
				if (domainEvidenceType == null)
				{
					domainEvidenceType = typeof(Url);
				}
				isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetTypeFromEvidence(AppDomain.CurrentDomain.Evidence, domainEvidenceType);
			}
			if ((scope & IsolatedStorageScope.Assembly) != IsolatedStorageScope.None)
			{
				Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
				isolatedStorageFile._fullEvidences = evidence;
				if ((scope & IsolatedStorageScope.Domain) != IsolatedStorageScope.None)
				{
					if (assemblyEvidenceType == null)
					{
						assemblyEvidenceType = typeof(Url);
					}
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetTypeFromEvidence(evidence, assemblyEvidenceType);
				}
				else
				{
					isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
				}
			}
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x0012F389 File Offset: 0x0012D589
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, object applicationIdentity)
		{
			IsolatedStorageFile.Demand(scope);
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile._applicationIdentity = applicationIdentity;
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x0012F3C2 File Offset: 0x0012D5C2
		public static IsolatedStorageFile GetStore(IsolatedStorageScope scope, Type applicationEvidenceType)
		{
			IsolatedStorageFile.Demand(scope);
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(scope);
			isolatedStorageFile.InitStore(scope, applicationEvidenceType);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0012F3F0 File Offset: 0x0012D5F0
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForApplication()
		{
			IsolatedStorageScope isolatedStorageScope = IsolatedStorageScope.Machine | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(isolatedStorageScope);
			isolatedStorageFile.InitStore(isolatedStorageScope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0012F424 File Offset: 0x0012D624
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x0012F45C File Offset: 0x0012D65C
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByMachine)]
		public static IsolatedStorageFile GetMachineStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x0012F4AC File Offset: 0x0012D6AC
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.ApplicationIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForApplication()
		{
			IsolatedStorageScope isolatedStorageScope = IsolatedStorageScope.User | IsolatedStorageScope.Application;
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(isolatedStorageScope);
			isolatedStorageFile.InitStore(isolatedStorageScope, null);
			isolatedStorageFile._fullEvidences = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0012F4E0 File Offset: 0x0012D6E0
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForAssembly()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Assembly);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x0012F518 File Offset: 0x0012D718
		[IsolatedStorageFilePermission(SecurityAction.Demand, UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser)]
		public static IsolatedStorageFile GetUserStoreForDomain()
		{
			IsolatedStorageFile isolatedStorageFile = new IsolatedStorageFile(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly);
			isolatedStorageFile._domainIdentity = IsolatedStorageFile.GetDomainIdentityFromEvidence(AppDomain.CurrentDomain.Evidence);
			Evidence evidence = Assembly.GetCallingAssembly().UnprotectedGetEvidence();
			isolatedStorageFile._fullEvidences = evidence;
			isolatedStorageFile._assemblyIdentity = IsolatedStorageFile.GetAssemblyIdentityFromEvidence(evidence);
			isolatedStorageFile.PostInit();
			return isolatedStorageFile;
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00047E00 File Offset: 0x00046000
		[ComVisible(false)]
		public static IsolatedStorageFile GetUserStoreForSite()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x0012F564 File Offset: 0x0012D764
		public static void Remove(IsolatedStorageScope scope)
		{
			string isolatedStorageRoot = IsolatedStorageFile.GetIsolatedStorageRoot(scope);
			if (!Directory.Exists(isolatedStorageRoot))
			{
				return;
			}
			try
			{
				Directory.Delete(isolatedStorageRoot, true);
			}
			catch (IOException)
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x0012F5A8 File Offset: 0x0012D7A8
		internal static string GetIsolatedStorageRoot(IsolatedStorageScope scope)
		{
			string text = null;
			if ((scope & IsolatedStorageScope.User) != IsolatedStorageScope.None)
			{
				if ((scope & IsolatedStorageScope.Roaming) != IsolatedStorageScope.None)
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
				}
				else
				{
					text = Environment.UnixGetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
				}
			}
			else if ((scope & IsolatedStorageScope.Machine) != IsolatedStorageScope.None)
			{
				text = Environment.UnixGetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create);
			}
			if (text == null)
			{
				throw new IsolatedStorageException(string.Format(Locale.GetText("Couldn't access storage location for '{0}'."), scope));
			}
			return Path.Combine(text, ".isolated-storage");
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0012F61B File Offset: 0x0012D81B
		private static void Demand(IsolatedStorageScope scope)
		{
			if (SecurityManager.SecurityEnabled)
			{
				new IsolatedStorageFilePermission(PermissionState.None)
				{
					UsageAllowed = IsolatedStorageFile.ScopeToContainment(scope)
				}.Demand();
			}
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0012F63C File Offset: 0x0012D83C
		private static IsolatedStorageContainment ScopeToContainment(IsolatedStorageScope scope)
		{
			if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
			{
				if (scope <= (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.AssemblyIsolationByUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly))
					{
						return IsolatedStorageContainment.DomainIsolationByUser;
					}
				}
				else
				{
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.AssemblyIsolationByRoamingUser;
					}
					if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Roaming))
					{
						return IsolatedStorageContainment.DomainIsolationByRoamingUser;
					}
				}
			}
			else if (scope <= (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
			{
				if (scope == (IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.AssemblyIsolationByMachine;
				}
				if (scope == (IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly | IsolatedStorageScope.Machine))
				{
					return IsolatedStorageContainment.DomainIsolationByMachine;
				}
			}
			else
			{
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByUser;
				}
				if (scope == (IsolatedStorageScope.User | IsolatedStorageScope.Roaming | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByRoamingUser;
				}
				if (scope == (IsolatedStorageScope.Machine | IsolatedStorageScope.Application))
				{
					return IsolatedStorageContainment.ApplicationIsolationByMachine;
				}
			}
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage;
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0012F6AC File Offset: 0x0012D8AC
		internal static ulong GetDirectorySize(DirectoryInfo di)
		{
			ulong num = 0UL;
			foreach (FileInfo fileInfo in di.GetFiles())
			{
				num += (ulong)fileInfo.Length;
			}
			foreach (DirectoryInfo directoryInfo in di.GetDirectories())
			{
				num += IsolatedStorageFile.GetDirectorySize(directoryInfo);
			}
			return num;
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x0012F706 File Offset: 0x0012D906
		private IsolatedStorageFile(IsolatedStorageScope scope)
		{
			this.storage_scope = scope;
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x0012F715 File Offset: 0x0012D915
		internal IsolatedStorageFile(IsolatedStorageScope scope, string location)
		{
			this.storage_scope = scope;
			this.directory = new DirectoryInfo(location);
			if (!this.directory.Exists)
			{
				throw new IsolatedStorageException(Locale.GetText("Invalid storage."));
			}
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x0012F750 File Offset: 0x0012D950
		~IsolatedStorageFile()
		{
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x0012F778 File Offset: 0x0012D978
		private void PostInit()
		{
			string text = IsolatedStorageFile.GetIsolatedStorageRoot(base.Scope);
			string text2;
			if (this._applicationIdentity != null)
			{
				text2 = string.Format("a{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._applicationIdentity));
			}
			else if (this._domainIdentity != null)
			{
				text2 = string.Format("d{0}{1}{0}{2}", this.SeparatorInternal, this.GetNameFromIdentity(this._domainIdentity), this.GetNameFromIdentity(this._assemblyIdentity));
			}
			else
			{
				if (this._assemblyIdentity == null)
				{
					throw new IsolatedStorageException(Locale.GetText("No code identity available."));
				}
				text2 = string.Format("d{0}none{0}{1}", this.SeparatorInternal, this.GetNameFromIdentity(this._assemblyIdentity));
			}
			text = Path.Combine(text, text2);
			this.directory = new DirectoryInfo(text);
			if (!this.directory.Exists)
			{
				try
				{
					this.directory.Create();
					this.SaveIdentities(text);
				}
				catch (IOException)
				{
				}
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06005972 RID: 22898 RVA: 0x0012F87C File Offset: 0x0012DA7C
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong CurrentSize
		{
			get
			{
				return IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x0012F88C File Offset: 0x0012DA8C
		[CLSCompliant(false)]
		[Obsolete]
		public override ulong MaximumSize
		{
			get
			{
				if (!SecurityManager.SecurityEnabled)
				{
					return 9223372036854775807UL;
				}
				if (this._resolved)
				{
					return this._maxSize;
				}
				Evidence evidence;
				if (this._fullEvidences != null)
				{
					evidence = this._fullEvidences;
				}
				else
				{
					evidence = new Evidence();
					if (this._assemblyIdentity != null)
					{
						evidence.AddHost(this._assemblyIdentity);
					}
				}
				if (evidence.Count < 1)
				{
					throw new InvalidOperationException(Locale.GetText("Couldn't get the quota from the available evidences."));
				}
				PermissionSet permissionSet = null;
				PermissionSet permissionSet2 = SecurityManager.ResolvePolicy(evidence, null, null, null, out permissionSet);
				IsolatedStoragePermission permission = this.GetPermission(permissionSet2);
				if (permission == null)
				{
					if (!permissionSet2.IsUnrestricted())
					{
						throw new InvalidOperationException(Locale.GetText("No quota from the available evidences."));
					}
					this._maxSize = 9223372036854775807UL;
				}
				else
				{
					this._maxSize = (ulong)permission.UserQuota;
				}
				this._resolved = true;
				return this._maxSize;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06005974 RID: 22900 RVA: 0x0012F95A File Offset: 0x0012DB5A
		internal string Root
		{
			get
			{
				return this.directory.FullName;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x0012F967 File Offset: 0x0012DB67
		[ComVisible(false)]
		public override long AvailableFreeSpace
		{
			get
			{
				this.CheckOpen();
				return long.MaxValue;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005976 RID: 22902 RVA: 0x0012F978 File Offset: 0x0012DB78
		[ComVisible(false)]
		public override long Quota
		{
			get
			{
				this.CheckOpen();
				return (long)this.MaximumSize;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x0012F986 File Offset: 0x0012DB86
		[ComVisible(false)]
		public override long UsedSize
		{
			get
			{
				this.CheckOpen();
				return (long)IsolatedStorageFile.GetDirectorySize(this.directory);
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(false)]
		public static bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005979 RID: 22905 RVA: 0x0012F999 File Offset: 0x0012DB99
		internal bool IsClosed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x0012F9A1 File Offset: 0x0012DBA1
		internal bool IsDisposed
		{
			get
			{
				return this.disposed;
			}
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x0012F9A9 File Offset: 0x0012DBA9
		public void Close()
		{
			this.closed = true;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x0012F9B4 File Offset: 0x0012DBB4
		public void CreateDirectory(string dir)
		{
			if (dir == null)
			{
				throw new ArgumentNullException("dir");
			}
			if (dir.IndexOfAny(Path.PathSeparatorChars) >= 0)
			{
				string[] array = dir.Split(Path.PathSeparatorChars, StringSplitOptions.RemoveEmptyEntries);
				DirectoryInfo directoryInfo = this.directory;
				for (int i = 0; i < array.Length; i++)
				{
					if (directoryInfo.GetFiles(array[i]).Length != 0)
					{
						throw new IsolatedStorageException("Unable to create directory.");
					}
					directoryInfo = directoryInfo.CreateSubdirectory(array[i]);
				}
				return;
			}
			if (this.directory.GetFiles(dir).Length != 0)
			{
				throw new IsolatedStorageException("Unable to create directory.");
			}
			this.directory.CreateSubdirectory(dir);
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x0012FA48 File Offset: 0x0012DC48
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName)
		{
			this.CopyFile(sourceFileName, destinationFileName, false);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x0012FA54 File Offset: 0x0012DC54
		[ComVisible(false)]
		public void CopyFile(string sourceFileName, string destinationFileName, bool overwrite)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("destinationFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (File.Exists(text2) && !overwrite)
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Copy(text, text2, overwrite);
			}
			catch (IOException ex)
			{
				throw new IsolatedStorageException("Operation not allowed.", ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				throw new IsolatedStorageException("Operation not allowed.", ex2);
			}
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x0012FB98 File Offset: 0x0012DD98
		[ComVisible(false)]
		public IsolatedStorageFileStream CreateFile(string path)
		{
			return new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None, this);
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0012FBA4 File Offset: 0x0012DDA4
		public void DeleteDirectory(string dir)
		{
			try
			{
				if (Path.IsPathRooted(dir))
				{
					dir = dir.Substring(1);
				}
				this.directory.CreateSubdirectory(dir).Delete();
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete directory '{0}'", new object[] { dir }));
			}
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x0012FC00 File Offset: 0x0012DE00
		public void DeleteFile(string file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			if (!File.Exists(Path.Combine(this.directory.FullName, file)))
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[] { file }));
			}
			try
			{
				File.Delete(Path.Combine(this.directory.FullName, file));
			}
			catch
			{
				throw new IsolatedStorageException(Locale.GetText("Could not delete file '{0}'", new object[] { file }));
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x0012FC94 File Offset: 0x0012DE94
		public void Dispose()
		{
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x0012FCA4 File Offset: 0x0012DEA4
		[ComVisible(false)]
		public bool DirectoryExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(text) && Directory.Exists(text);
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x0012FCE8 File Offset: 0x0012DEE8
		[ComVisible(false)]
		public bool FileExists(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			return this.IsPathInStorage(text) && File.Exists(text);
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x0012FD2C File Offset: 0x0012DF2C
		[ComVisible(false)]
		public DateTimeOffset GetCreationTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetCreationTime(text);
			}
			return Directory.GetCreationTime(text);
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x0012FD98 File Offset: 0x0012DF98
		[ComVisible(false)]
		public DateTimeOffset GetLastAccessTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetLastAccessTime(text);
			}
			return Directory.GetLastAccessTime(text);
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x0012FE04 File Offset: 0x0012E004
		[ComVisible(false)]
		public DateTimeOffset GetLastWriteTime(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("An empty path is not valid.");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, path);
			if (File.Exists(text))
			{
				return File.GetLastWriteTime(text);
			}
			return Directory.GetLastWriteTime(text);
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x0012FE70 File Offset: 0x0012E070
		public string[] GetDirectoryNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			DirectoryInfo[] array = null;
			if (directoryName == null || directoryName.Length == 0)
			{
				array = this.directory.GetDirectories(searchPattern);
			}
			else
			{
				DirectoryInfo directoryInfo = this.directory.GetDirectories(directoryName)[0];
				if (directoryInfo.FullName.IndexOf(this.directory.FullName) >= 0)
				{
					array = directoryInfo.GetDirectories(fileName);
					string[] array2 = directoryName.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
					for (int i = array2.Length - 1; i >= 0; i--)
					{
						if (directoryInfo.Name != array2[i])
						{
							array = null;
							break;
						}
						directoryInfo = directoryInfo.Parent;
					}
				}
			}
			if (array == null)
			{
				throw new SecurityException();
			}
			FileSystemInfo[] array3 = array;
			return this.GetNames(array3);
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x0012FF5F File Offset: 0x0012E15F
		[ComVisible(false)]
		public string[] GetDirectoryNames()
		{
			return this.GetDirectoryNames("*");
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x0012FF6C File Offset: 0x0012E16C
		private string[] GetNames(FileSystemInfo[] afsi)
		{
			string[] array = new string[afsi.Length];
			for (int num = 0; num != afsi.Length; num++)
			{
				array[num] = afsi[num].Name;
			}
			return array;
		}

		// Token: 0x0600598B RID: 22923 RVA: 0x0012FF9C File Offset: 0x0012E19C
		public string[] GetFileNames(string searchPattern)
		{
			if (searchPattern == null)
			{
				throw new ArgumentNullException("searchPattern");
			}
			if (searchPattern.Contains(".."))
			{
				throw new ArgumentException("Search pattern cannot contain '..' to move up directories.", "searchPattern");
			}
			string directoryName = Path.GetDirectoryName(searchPattern);
			string fileName = Path.GetFileName(searchPattern);
			FileInfo[] array;
			if (directoryName == null || directoryName.Length == 0)
			{
				array = this.directory.GetFiles(searchPattern);
			}
			else
			{
				DirectoryInfo[] directories = this.directory.GetDirectories(directoryName);
				if (directories.Length != 1)
				{
					throw new SecurityException();
				}
				if (!directories[0].FullName.StartsWith(this.directory.FullName))
				{
					throw new SecurityException();
				}
				if (directories[0].FullName.Substring(this.directory.FullName.Length + 1) != directoryName)
				{
					throw new SecurityException();
				}
				array = directories[0].GetFiles(fileName);
			}
			FileSystemInfo[] array2 = array;
			return this.GetNames(array2);
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00130075 File Offset: 0x0012E275
		[ComVisible(false)]
		public string[] GetFileNames()
		{
			return this.GetFileNames("*");
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00130082 File Offset: 0x0012E282
		[ComVisible(false)]
		public override bool IncreaseQuotaTo(long newQuotaSize)
		{
			if (newQuotaSize < this.Quota)
			{
				throw new ArgumentException();
			}
			this.CheckOpen();
			return false;
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x0013009C File Offset: 0x0012E29C
		[ComVisible(false)]
		public void MoveDirectory(string sourceDirectoryName, string destinationDirectoryName)
		{
			if (sourceDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (destinationDirectoryName == null)
			{
				throw new ArgumentNullException("sourceDirectoryName");
			}
			if (sourceDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "sourceDirectoryName");
			}
			if (destinationDirectoryName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty directory name is not valid.", "destinationDirectoryName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceDirectoryName);
			string text2 = Path.Combine(this.directory.FullName, destinationDirectoryName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!Directory.Exists(text))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + sourceDirectoryName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new DirectoryNotFoundException("Could not find a part of path '" + destinationDirectoryName + "'.");
			}
			try
			{
				Directory.Move(text, text2);
			}
			catch (IOException ex)
			{
				throw new IsolatedStorageException("Operation not allowed.", ex);
			}
			catch (UnauthorizedAccessException ex2)
			{
				throw new IsolatedStorageException("Operation not allowed.", ex2);
			}
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x001301C8 File Offset: 0x0012E3C8
		[ComVisible(false)]
		public void MoveFile(string sourceFileName, string destinationFileName)
		{
			if (sourceFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (destinationFileName == null)
			{
				throw new ArgumentNullException("sourceFileName");
			}
			if (sourceFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "sourceFileName");
			}
			if (destinationFileName.Trim().Length == 0)
			{
				throw new ArgumentException("An empty file name is not valid.", "destinationFileName");
			}
			this.CheckOpen();
			string text = Path.Combine(this.directory.FullName, sourceFileName);
			string text2 = Path.Combine(this.directory.FullName, destinationFileName);
			if (!this.IsPathInStorage(text) || !this.IsPathInStorage(text2))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			if (!File.Exists(text))
			{
				throw new FileNotFoundException("Could not find a part of path '" + sourceFileName + "'.");
			}
			if (!Directory.Exists(Path.GetDirectoryName(text2)))
			{
				throw new IsolatedStorageException("Operation not allowed.");
			}
			try
			{
				File.Move(text, text2);
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new IsolatedStorageException("Operation not allowed.", ex);
			}
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x001302D0 File Offset: 0x0012E4D0
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode)
		{
			return new IsolatedStorageFileStream(path, mode, this);
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x001302DA File Offset: 0x0012E4DA
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access)
		{
			return new IsolatedStorageFileStream(path, mode, access, this);
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x001302E5 File Offset: 0x0012E4E5
		[ComVisible(false)]
		public IsolatedStorageFileStream OpenFile(string path, FileMode mode, FileAccess access, FileShare share)
		{
			return new IsolatedStorageFileStream(path, mode, access, share, this);
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x001302F4 File Offset: 0x0012E4F4
		public override void Remove()
		{
			this.CheckOpen(false);
			try
			{
				this.directory.Delete(true);
			}
			catch
			{
				throw new IsolatedStorageException("Could not remove storage.");
			}
			this.Close();
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x00130338 File Offset: 0x0012E538
		protected override IsolatedStoragePermission GetPermission(PermissionSet ps)
		{
			if (ps == null)
			{
				return null;
			}
			return (IsolatedStoragePermission)ps.GetPermission(typeof(IsolatedStorageFilePermission));
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x00130354 File Offset: 0x0012E554
		private void CheckOpen()
		{
			this.CheckOpen(true);
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x00130360 File Offset: 0x0012E560
		private void CheckOpen(bool checkDirExists)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("IsolatedStorageFile");
			}
			if (this.closed)
			{
				throw new InvalidOperationException("Storage needs to be open for this operation.");
			}
			if (checkDirExists && !Directory.Exists(this.directory.FullName))
			{
				throw new IsolatedStorageException("Isolated storage has been removed or disabled.");
			}
		}

		// Token: 0x06005997 RID: 22935 RVA: 0x001303B3 File Offset: 0x0012E5B3
		private bool IsPathInStorage(string path)
		{
			return Path.GetFullPath(path).StartsWith(this.directory.FullName);
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x001303CC File Offset: 0x0012E5CC
		private string GetNameFromIdentity(object identity)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(identity.ToString());
			Array array = SHA1.Create().ComputeHash(bytes, 0, bytes.Length);
			byte[] array2 = new byte[10];
			Buffer.BlockCopy(array, 0, array2, 0, array2.Length);
			return CryptoConvert.ToHex(array2);
		}

		// Token: 0x06005999 RID: 22937 RVA: 0x00130414 File Offset: 0x0012E614
		private static object GetTypeFromEvidence(Evidence e, Type t)
		{
			foreach (object obj in e)
			{
				if (obj.GetType() == t)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x00130474 File Offset: 0x0012E674
		internal static object GetAssemblyIdentityFromEvidence(Evidence e)
		{
			object obj = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Publisher));
			if (obj != null)
			{
				return obj;
			}
			obj = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(StrongName));
			if (obj != null)
			{
				return obj;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x001304C0 File Offset: 0x0012E6C0
		internal static object GetDomainIdentityFromEvidence(Evidence e)
		{
			object typeFromEvidence = IsolatedStorageFile.GetTypeFromEvidence(e, typeof(ApplicationDirectory));
			if (typeFromEvidence != null)
			{
				return typeFromEvidence;
			}
			return IsolatedStorageFile.GetTypeFromEvidence(e, typeof(Url));
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x001304F4 File Offset: 0x0012E6F4
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void SaveIdentities(string root)
		{
			IsolatedStorageFile.Identities identities = new IsolatedStorageFile.Identities(this._applicationIdentity, this._assemblyIdentity, this._domainIdentity);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			IsolatedStorageFile.mutex.WaitOne();
			try
			{
				using (FileStream fileStream = File.Create(root + ".storage"))
				{
					binaryFormatter.Serialize(fileStream, identities);
				}
			}
			finally
			{
				IsolatedStorageFile.mutex.ReleaseMutex();
			}
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x0013057C File Offset: 0x0012E77C
		// Note: this type is marked as 'beforefieldinit'.
		static IsolatedStorageFile()
		{
		}

		// Token: 0x0400357C RID: 13692
		private bool _resolved;

		// Token: 0x0400357D RID: 13693
		private ulong _maxSize;

		// Token: 0x0400357E RID: 13694
		private Evidence _fullEvidences;

		// Token: 0x0400357F RID: 13695
		private static readonly Mutex mutex = new Mutex();

		// Token: 0x04003580 RID: 13696
		private bool closed;

		// Token: 0x04003581 RID: 13697
		private bool disposed;

		// Token: 0x04003582 RID: 13698
		private DirectoryInfo directory;

		// Token: 0x02000995 RID: 2453
		[Serializable]
		private struct Identities
		{
			// Token: 0x0600599E RID: 22942 RVA: 0x00130588 File Offset: 0x0012E788
			public Identities(object application, object assembly, object domain)
			{
				this.Application = application;
				this.Assembly = assembly;
				this.Domain = domain;
			}

			// Token: 0x04003583 RID: 13699
			public object Application;

			// Token: 0x04003584 RID: 13700
			public object Assembly;

			// Token: 0x04003585 RID: 13701
			public object Domain;
		}
	}
}
