using System;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020003D8 RID: 984
	internal static class DefaultPolicies
	{
		// Token: 0x060029ED RID: 10733 RVA: 0x000991AC File Offset: 0x000973AC
		public static PermissionSet GetSpecialPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2314740779U)
			{
				if (num != 734303062U)
				{
					if (num != 753551658U)
					{
						if (num == 2314740779U)
						{
							if (name == "LocalIntranet")
							{
								return DefaultPolicies.LocalIntranet;
							}
						}
					}
					else if (name == "Nothing")
					{
						return DefaultPolicies.Nothing;
					}
				}
				else if (name == "FullTrust")
				{
					return DefaultPolicies.FullTrust;
				}
			}
			else if (num <= 3132872517U)
			{
				if (num != 2939433820U)
				{
					if (num == 3132872517U)
					{
						if (name == "SkipVerification")
						{
							return DefaultPolicies.SkipVerification;
						}
					}
				}
				else if (name == "Internet")
				{
					return DefaultPolicies.Internet;
				}
			}
			else if (num != 3650199797U)
			{
				if (num == 4030759744U)
				{
					if (name == "Everything")
					{
						return DefaultPolicies.Everything;
					}
				}
			}
			else if (name == "Execution")
			{
				return DefaultPolicies.Execution;
			}
			return null;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x000992BF File Offset: 0x000974BF
		public static PermissionSet FullTrust
		{
			get
			{
				if (DefaultPolicies._fullTrust == null)
				{
					DefaultPolicies._fullTrust = DefaultPolicies.BuildFullTrust();
				}
				return DefaultPolicies._fullTrust;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x000992D7 File Offset: 0x000974D7
		public static PermissionSet LocalIntranet
		{
			get
			{
				if (DefaultPolicies._localIntranet == null)
				{
					DefaultPolicies._localIntranet = DefaultPolicies.BuildLocalIntranet();
				}
				return DefaultPolicies._localIntranet;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000992EF File Offset: 0x000974EF
		public static PermissionSet Internet
		{
			get
			{
				if (DefaultPolicies._internet == null)
				{
					DefaultPolicies._internet = DefaultPolicies.BuildInternet();
				}
				return DefaultPolicies._internet;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x00099307 File Offset: 0x00097507
		public static PermissionSet SkipVerification
		{
			get
			{
				if (DefaultPolicies._skipVerification == null)
				{
					DefaultPolicies._skipVerification = DefaultPolicies.BuildSkipVerification();
				}
				return DefaultPolicies._skipVerification;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060029F2 RID: 10738 RVA: 0x0009931F File Offset: 0x0009751F
		public static PermissionSet Execution
		{
			get
			{
				if (DefaultPolicies._execution == null)
				{
					DefaultPolicies._execution = DefaultPolicies.BuildExecution();
				}
				return DefaultPolicies._execution;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x00099337 File Offset: 0x00097537
		public static PermissionSet Nothing
		{
			get
			{
				if (DefaultPolicies._nothing == null)
				{
					DefaultPolicies._nothing = DefaultPolicies.BuildNothing();
				}
				return DefaultPolicies._nothing;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060029F4 RID: 10740 RVA: 0x0009934F File Offset: 0x0009754F
		public static PermissionSet Everything
		{
			get
			{
				if (DefaultPolicies._everything == null)
				{
					DefaultPolicies._everything = DefaultPolicies.BuildEverything();
				}
				return DefaultPolicies._everything;
			}
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00099368 File Offset: 0x00097568
		public static StrongNameMembershipCondition FullTrustMembership(string name, DefaultPolicies.Key key)
		{
			StrongNamePublicKeyBlob strongNamePublicKeyBlob = null;
			if (key != DefaultPolicies.Key.Ecma)
			{
				if (key == DefaultPolicies.Key.MsFinal)
				{
					if (DefaultPolicies._msFinal == null)
					{
						DefaultPolicies._msFinal = new StrongNamePublicKeyBlob(DefaultPolicies._msFinalKey);
					}
					strongNamePublicKeyBlob = DefaultPolicies._msFinal;
				}
			}
			else
			{
				if (DefaultPolicies._ecma == null)
				{
					DefaultPolicies._ecma = new StrongNamePublicKeyBlob(DefaultPolicies._ecmaKey);
				}
				strongNamePublicKeyBlob = DefaultPolicies._ecma;
			}
			if (DefaultPolicies._fxVersion == null)
			{
				DefaultPolicies._fxVersion = new Version("4.0.0.0");
			}
			return new StrongNameMembershipCondition(strongNamePublicKeyBlob, name, DefaultPolicies._fxVersion);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000993E2 File Offset: 0x000975E2
		private static NamedPermissionSet BuildFullTrust()
		{
			return new NamedPermissionSet("FullTrust", PermissionState.Unrestricted);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000993F0 File Offset: 0x000975F0
		private static NamedPermissionSet BuildLocalIntranet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("LocalIntranet", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(EnvironmentPermissionAccess.Read, "USERNAME;USER"));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.AssemblyIsolationByUser,
				UserQuota = long.MaxValue
			});
			namedPermissionSet.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.ReflectionEmit));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.Assertion | SecurityPermissionFlag.Execution;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000994A0 File Offset: 0x000976A0
		private static NamedPermissionSet BuildInternet()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Internet", PermissionState.None);
			namedPermissionSet.AddPermission(new FileDialogPermission(FileDialogPermissionAccess.Open));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.None)
			{
				UsageAllowed = IsolatedStorageContainment.DomainIsolationByUser,
				UserQuota = 512000L
			});
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			namedPermissionSet.AddPermission(new UIPermission(UIPermissionWindow.SafeTopLevelWindows, UIPermissionClipboard.OwnClipboard));
			namedPermissionSet.AddPermission(PermissionBuilder.Create(DefaultPolicies.PrintingPermission("SafePrinting")));
			return namedPermissionSet;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00099519 File Offset: 0x00097719
		private static NamedPermissionSet BuildSkipVerification()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("SkipVerification", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.SkipVerification));
			return namedPermissionSet;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00099533 File Offset: 0x00097733
		private static NamedPermissionSet BuildExecution()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Execution", PermissionState.None);
			namedPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			return namedPermissionSet;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0009954D File Offset: 0x0009774D
		private static NamedPermissionSet BuildNothing()
		{
			return new NamedPermissionSet("Nothing", PermissionState.None);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009955C File Offset: 0x0009775C
		private static NamedPermissionSet BuildEverything()
		{
			NamedPermissionSet namedPermissionSet = new NamedPermissionSet("Everything", PermissionState.None);
			namedPermissionSet.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileDialogPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new IsolatedStorageFilePermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new RegistryPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(new KeyContainerPermission(PermissionState.Unrestricted));
			SecurityPermissionFlag securityPermissionFlag = SecurityPermissionFlag.AllFlags;
			securityPermissionFlag &= ~SecurityPermissionFlag.SkipVerification;
			namedPermissionSet.AddPermission(new SecurityPermission(securityPermissionFlag));
			namedPermissionSet.AddPermission(new UIPermission(PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			namedPermissionSet.AddPermission(PermissionBuilder.Create("System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", PermissionState.Unrestricted));
			return namedPermissionSet;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x000996BA File Offset: 0x000978BA
		private static SecurityElement PrintingPermission(string level)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Level", level);
			return securityElement;
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x000996F2 File Offset: 0x000978F2
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultPolicies()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			DefaultPolicies._ecmaKey = array;
			DefaultPolicies._msFinalKey = new byte[]
			{
				0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
				0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
				82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
				1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
				163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
				253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
				76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
				220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
				11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
				16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
				146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
				232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
				40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
				61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
				15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
				24, 32, 109, 192, 147, 52, 77, 90, 210, 147
			};
		}

		// Token: 0x04001E3A RID: 7738
		private const string DnsPermissionClass = "System.Net.DnsPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E3B RID: 7739
		private const string EventLogPermissionClass = "System.Diagnostics.EventLogPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E3C RID: 7740
		private const string PrintingPermissionClass = "System.Drawing.Printing.PrintingPermission, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001E3D RID: 7741
		private const string SocketPermissionClass = "System.Net.SocketPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E3E RID: 7742
		private const string WebPermissionClass = "System.Net.WebPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E3F RID: 7743
		private const string PerformanceCounterPermissionClass = "System.Diagnostics.PerformanceCounterPermission, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E40 RID: 7744
		private const string DirectoryServicesPermissionClass = "System.DirectoryServices.DirectoryServicesPermission, System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001E41 RID: 7745
		private const string MessageQueuePermissionClass = "System.Messaging.MessageQueuePermission, System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001E42 RID: 7746
		private const string ServiceControllerPermissionClass = "System.ServiceProcess.ServiceControllerPermission, System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04001E43 RID: 7747
		private const string OleDbPermissionClass = "System.Data.OleDb.OleDbPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E44 RID: 7748
		private const string SqlClientPermissionClass = "System.Data.SqlClient.SqlClientPermission, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

		// Token: 0x04001E45 RID: 7749
		private static Version _fxVersion;

		// Token: 0x04001E46 RID: 7750
		private static byte[] _ecmaKey;

		// Token: 0x04001E47 RID: 7751
		private static StrongNamePublicKeyBlob _ecma;

		// Token: 0x04001E48 RID: 7752
		private static byte[] _msFinalKey;

		// Token: 0x04001E49 RID: 7753
		private static StrongNamePublicKeyBlob _msFinal;

		// Token: 0x04001E4A RID: 7754
		private static NamedPermissionSet _fullTrust;

		// Token: 0x04001E4B RID: 7755
		private static NamedPermissionSet _localIntranet;

		// Token: 0x04001E4C RID: 7756
		private static NamedPermissionSet _internet;

		// Token: 0x04001E4D RID: 7757
		private static NamedPermissionSet _skipVerification;

		// Token: 0x04001E4E RID: 7758
		private static NamedPermissionSet _execution;

		// Token: 0x04001E4F RID: 7759
		private static NamedPermissionSet _nothing;

		// Token: 0x04001E50 RID: 7760
		private static NamedPermissionSet _everything;

		// Token: 0x020003D9 RID: 985
		public static class ReservedNames
		{
			// Token: 0x060029FF RID: 10751 RVA: 0x00099720 File Offset: 0x00097920
			public static bool IsReserved(string name)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 2314740779U)
				{
					if (num != 734303062U)
					{
						if (num != 753551658U)
						{
							if (num != 2314740779U)
							{
								return false;
							}
							if (!(name == "LocalIntranet"))
							{
								return false;
							}
						}
						else if (!(name == "Nothing"))
						{
							return false;
						}
					}
					else if (!(name == "FullTrust"))
					{
						return false;
					}
				}
				else if (num <= 3132872517U)
				{
					if (num != 2939433820U)
					{
						if (num != 3132872517U)
						{
							return false;
						}
						if (!(name == "SkipVerification"))
						{
							return false;
						}
					}
					else if (!(name == "Internet"))
					{
						return false;
					}
				}
				else if (num != 3650199797U)
				{
					if (num != 4030759744U)
					{
						return false;
					}
					if (!(name == "Everything"))
					{
						return false;
					}
				}
				else if (!(name == "Execution"))
				{
					return false;
				}
				return true;
			}

			// Token: 0x04001E51 RID: 7761
			public const string FullTrust = "FullTrust";

			// Token: 0x04001E52 RID: 7762
			public const string LocalIntranet = "LocalIntranet";

			// Token: 0x04001E53 RID: 7763
			public const string Internet = "Internet";

			// Token: 0x04001E54 RID: 7764
			public const string SkipVerification = "SkipVerification";

			// Token: 0x04001E55 RID: 7765
			public const string Execution = "Execution";

			// Token: 0x04001E56 RID: 7766
			public const string Nothing = "Nothing";

			// Token: 0x04001E57 RID: 7767
			public const string Everything = "Everything";
		}

		// Token: 0x020003DA RID: 986
		public enum Key
		{
			// Token: 0x04001E59 RID: 7769
			Ecma,
			// Token: 0x04001E5A RID: 7770
			MsFinal
		}
	}
}
