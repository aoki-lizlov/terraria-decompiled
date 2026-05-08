using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	// Token: 0x02000536 RID: 1334
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000C2500 File Offset: 0x000C0700
		public static string ApplicationId
		{
			get
			{
				RemotingConfiguration.applicationID = RemotingConfiguration.ApplicationName;
				return RemotingConfiguration.applicationID;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060035BC RID: 13756 RVA: 0x000C2511 File Offset: 0x000C0711
		// (set) Token: 0x060035BD RID: 13757 RVA: 0x000C2518 File Offset: 0x000C0718
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000C2520 File Offset: 0x000C0720
		// (set) Token: 0x060035BF RID: 13759 RVA: 0x000C2527 File Offset: 0x000C0727
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfiguration._errorMode;
			}
			set
			{
				RemotingConfiguration._errorMode = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000C252F File Offset: 0x000C072F
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000C2548 File Offset: 0x000C0748
		[MonoTODO("ensureSecurity support has not been implemented")]
		public static void Configure(string filename, bool ensureSecurity)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (!RemotingConfiguration.defaultConfigRead)
				{
					string bundledMachineConfig = Environment.GetBundledMachineConfig();
					if (bundledMachineConfig != null)
					{
						RemotingConfiguration.ReadConfigString(bundledMachineConfig);
					}
					if (File.Exists(Environment.GetMachineConfigPath()))
					{
						RemotingConfiguration.ReadConfigFile(Environment.GetMachineConfigPath());
					}
					RemotingConfiguration.defaultConfigRead = true;
				}
				if (filename != null)
				{
					RemotingConfiguration.ReadConfigFile(filename);
				}
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000C25BC File Offset: 0x000C07BC
		[Obsolete("Use Configure(String,Boolean)")]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000C25C8 File Offset: 0x000C07C8
		private static void ReadConfigString(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StringReader(filename))
				{
					ConfigHandler configHandler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, configHandler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration string could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000C2634 File Offset: 0x000C0834
		private static void ReadConfigFile(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StreamReader(filename))
				{
					ConfigHandler configHandler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, configHandler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration file '" + filename + "' could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000C26A8 File Offset: 0x000C08A8
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler configHandler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, configHandler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000C2734 File Offset: 0x000C0934
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry[] array2;
			lock (hashtable)
			{
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[RemotingConfiguration.activatedClientEntries.Count];
				RemotingConfiguration.activatedClientEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000C2794 File Offset: 0x000C0994
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedServiceTypeEntry[] array2;
			lock (hashtable)
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[RemotingConfiguration.activatedServiceEntries.Count];
				RemotingConfiguration.activatedServiceEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000C27F4 File Offset: 0x000C09F4
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry[] array2;
			lock (hashtable)
			{
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[RemotingConfiguration.wellKnownClientEntries.Count];
				RemotingConfiguration.wellKnownClientEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000C2854 File Offset: 0x000C0A54
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownServiceTypeEntry[] array2;
			lock (hashtable)
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[RemotingConfiguration.wellKnownServiceEntries.Count];
				RemotingConfiguration.wellKnownServiceEntries.Values.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000C28B4 File Offset: 0x000C0AB4
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			bool flag2;
			lock (hashtable)
			{
				flag2 = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return flag2;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000C28FC File Offset: 0x000C0AFC
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry activatedClientTypeEntry;
			lock (hashtable)
			{
				activatedClientTypeEntry = RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry;
			}
			return activatedClientTypeEntry;
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000C2948 File Offset: 0x000C0B48
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsRemotelyActivatedClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000C295C File Offset: 0x000C0B5C
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry wellKnownClientTypeEntry;
			lock (hashtable)
			{
				wellKnownClientTypeEntry = RemotingConfiguration.wellKnownClientEntries[svrType] as WellKnownClientTypeEntry;
			}
			return wellKnownClientTypeEntry;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000C29A8 File Offset: 0x000C0BA8
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsWellKnownClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000C29BC File Offset: 0x000C0BBC
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000C2A58 File Offset: 0x000C0C58
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000C2A88 File Offset: 0x000C0C88
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000C2AD4 File Offset: 0x000C0CD4
		public static void RegisterActivatedServiceType(Type type)
		{
			RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000C2AE1 File Offset: 0x000C0CE1
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000C2B14 File Offset: 0x000C0D14
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000C2BB0 File Offset: 0x000C0DB0
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000C2BC0 File Offset: 0x000C0DC0
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000C2C24 File Offset: 0x000C0E24
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000C2C37 File Offset: 0x000C0E37
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000C2C4A File Offset: 0x000C0E4A
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000C2C60 File Offset: 0x000C0E60
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if ((!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true")) && (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true")))
				{
					if (channelData.Ref != null)
					{
						ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
						if (channelData2 == null)
						{
							throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
						}
						channelData.CopyFrom(channelData2);
					}
					foreach (object obj2 in channelData.ServerProviders)
					{
						ProviderData providerData = (ProviderData)obj2;
						if (providerData.Ref != null)
						{
							ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
							if (providerData2 == null)
							{
								throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
							}
							providerData.CopyFrom(providerData2);
						}
					}
					foreach (object obj3 in channelData.ClientProviders)
					{
						ProviderData providerData3 = (ProviderData)obj3;
						if (providerData3.Ref != null)
						{
							ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
							if (providerData4 == null)
							{
								throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
							}
							providerData3.CopyFrom(providerData4);
						}
					}
					ChannelServices.RegisterChannelConfig(channelData);
				}
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000C2E78 File Offset: 0x000C1078
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000C2F18 File Offset: 0x000C1118
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return RemotingConfiguration._errorMode != CustomErrorsModes.Off && (RemotingConfiguration._errorMode == CustomErrorsModes.On || !isLocalRequest);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000C2F34 File Offset: 0x000C1134
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = (CustomErrorsModes)Enum.Parse(typeof(CustomErrorsModes), text, true);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000C2FAC File Offset: 0x000C11AC
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingConfiguration()
		{
		}

		// Token: 0x040024B2 RID: 9394
		private static string applicationID = null;

		// Token: 0x040024B3 RID: 9395
		private static string applicationName = null;

		// Token: 0x040024B4 RID: 9396
		private static string processGuid = null;

		// Token: 0x040024B5 RID: 9397
		private static bool defaultConfigRead = false;

		// Token: 0x040024B6 RID: 9398
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x040024B7 RID: 9399
		private static CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;

		// Token: 0x040024B8 RID: 9400
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x040024B9 RID: 9401
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x040024BA RID: 9402
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x040024BB RID: 9403
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x040024BC RID: 9404
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x040024BD RID: 9405
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x040024BE RID: 9406
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
