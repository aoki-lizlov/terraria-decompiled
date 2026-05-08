using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	// Token: 0x02000545 RID: 1349
	[ComVisible(true)]
	public class SoapServices
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x000025BE File Offset: 0x000007BE
		private SoapServices()
		{
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x000C57D8 File Offset: 0x000C39D8
		public static string XmlNsForClrType
		{
			get
			{
				return "http://schemas.microsoft.com/clr/";
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000C57DF File Offset: 0x000C39DF
		public static string XmlNsForClrTypeWithAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/assem/";
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x000C57E6 File Offset: 0x000C39E6
		public static string XmlNsForClrTypeWithNs
		{
			get
			{
				return "http://schemas.microsoft.com/clr/ns/";
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000C57ED File Offset: 0x000C39ED
		public static string XmlNsForClrTypeWithNsAndAssembly
		{
			get
			{
				return "http://schemas.microsoft.com/clr/nsassem/";
			}
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000C57F4 File Offset: 0x000C39F4
		public static string CodeXmlNamespaceForClrTypeNamespace(string typeNamespace, string assemblyName)
		{
			if (assemblyName == string.Empty)
			{
				return SoapServices.XmlNsForClrTypeWithNs + typeNamespace;
			}
			if (typeNamespace == string.Empty)
			{
				return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithAssembly + assemblyName);
			}
			return SoapServices.EncodeNs(SoapServices.XmlNsForClrTypeWithNsAndAssembly + typeNamespace + "/" + assemblyName);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000C5850 File Offset: 0x000C3A50
		public static bool DecodeXmlNamespaceForClrTypeNamespace(string inNamespace, out string typeNamespace, out string assemblyName)
		{
			if (inNamespace == null)
			{
				throw new ArgumentNullException("inNamespace");
			}
			inNamespace = SoapServices.DecodeNs(inNamespace);
			typeNamespace = null;
			assemblyName = null;
			if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNsAndAssembly))
			{
				int length = SoapServices.XmlNsForClrTypeWithNsAndAssembly.Length;
				if (length >= inNamespace.Length)
				{
					return false;
				}
				int num = inNamespace.IndexOf('/', length + 1);
				if (num == -1)
				{
					return false;
				}
				typeNamespace = inNamespace.Substring(length, num - length);
				assemblyName = inNamespace.Substring(num + 1);
				return true;
			}
			else
			{
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithNs))
				{
					int length2 = SoapServices.XmlNsForClrTypeWithNs.Length;
					typeNamespace = inNamespace.Substring(length2);
					return true;
				}
				if (inNamespace.StartsWith(SoapServices.XmlNsForClrTypeWithAssembly))
				{
					int length3 = SoapServices.XmlNsForClrTypeWithAssembly.Length;
					assemblyName = inNamespace.Substring(length3);
					return true;
				}
				return false;
			}
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000C5910 File Offset: 0x000C3B10
		public static void GetInteropFieldTypeAndNameFromXmlAttribute(Type containingType, string xmlAttribute, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			SoapServices.GetInteropFieldInfo((typeInfo != null) ? typeInfo.Attributes : null, xmlAttribute, xmlNamespace, out type, out name);
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000C5944 File Offset: 0x000C3B44
		public static void GetInteropFieldTypeAndNameFromXmlElement(Type containingType, string xmlElement, string xmlNamespace, out Type type, out string name)
		{
			SoapServices.TypeInfo typeInfo = (SoapServices.TypeInfo)SoapServices._typeInfos[containingType];
			SoapServices.GetInteropFieldInfo((typeInfo != null) ? typeInfo.Elements : null, xmlElement, xmlNamespace, out type, out name);
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000C5978 File Offset: 0x000C3B78
		private static void GetInteropFieldInfo(Hashtable fields, string xmlName, string xmlNamespace, out Type type, out string name)
		{
			if (fields != null)
			{
				FieldInfo fieldInfo = (FieldInfo)fields[SoapServices.GetNameKey(xmlName, xmlNamespace)];
				if (fieldInfo != null)
				{
					type = fieldInfo.FieldType;
					name = fieldInfo.Name;
					return;
				}
			}
			type = null;
			name = null;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000C59BD File Offset: 0x000C3BBD
		private static string GetNameKey(string name, string namspace)
		{
			if (namspace == null)
			{
				return name;
			}
			return name + " " + namspace;
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000C59D0 File Offset: 0x000C3BD0
		public static Type GetInteropTypeFromXmlElement(string xmlElement, string xmlNamespace)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			Type type;
			lock (syncRoot)
			{
				type = (Type)SoapServices._xmlElements[xmlElement + " " + xmlNamespace];
			}
			return type;
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000C5A2C File Offset: 0x000C3C2C
		public static Type GetInteropTypeFromXmlType(string xmlType, string xmlTypeNamespace)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			Type type;
			lock (syncRoot)
			{
				type = (Type)SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace];
			}
			return type;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000C5A88 File Offset: 0x000C3C88
		private static string GetAssemblyName(MethodBase mb)
		{
			if (mb.DeclaringType.Assembly == typeof(object).Assembly)
			{
				return string.Empty;
			}
			return mb.DeclaringType.Assembly.GetName().Name;
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000C5AC6 File Offset: 0x000C3CC6
		public static string GetSoapActionFromMethodBase(MethodBase mb)
		{
			return SoapServices.InternalGetSoapAction(mb);
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000C5AD0 File Offset: 0x000C3CD0
		public static bool GetTypeAndMethodNameFromSoapAction(string soapAction, out string typeName, out string methodName)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				MethodBase methodBase = (MethodBase)SoapServices._soapActionsMethods[soapAction];
				if (methodBase != null)
				{
					typeName = methodBase.DeclaringType.AssemblyQualifiedName;
					methodName = methodBase.Name;
					return true;
				}
			}
			typeName = null;
			methodName = null;
			int num = soapAction.LastIndexOf('#');
			if (num == -1)
			{
				return false;
			}
			methodName = soapAction.Substring(num + 1);
			string text;
			string text2;
			if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(soapAction.Substring(0, num), out text, out text2))
			{
				return false;
			}
			if (text2 == null)
			{
				typeName = text + ", " + typeof(object).Assembly.GetName().Name;
			}
			else
			{
				typeName = text + ", " + text2;
			}
			return true;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000C5BBC File Offset: 0x000C3DBC
		public static bool GetXmlElementForInteropType(Type type, out string xmlElement, out string xmlNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlElement)
			{
				xmlElement = null;
				xmlNamespace = null;
				return false;
			}
			xmlElement = soapTypeAttribute.XmlElementName;
			xmlNamespace = soapTypeAttribute.XmlNamespace;
			return true;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000C5BF6 File Offset: 0x000C3DF6
		public static string GetXmlNamespaceForMethodCall(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000C5BF6 File Offset: 0x000C3DF6
		public static string GetXmlNamespaceForMethodResponse(MethodBase mb)
		{
			return SoapServices.CodeXmlNamespaceForClrTypeNamespace(mb.DeclaringType.FullName, SoapServices.GetAssemblyName(mb));
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000C5C10 File Offset: 0x000C3E10
		public static bool GetXmlTypeForInteropType(Type type, out string xmlType, out string xmlTypeNamespace)
		{
			SoapTypeAttribute soapTypeAttribute = (SoapTypeAttribute)InternalRemotingServices.GetCachedSoapAttribute(type);
			if (!soapTypeAttribute.IsInteropXmlType)
			{
				xmlType = null;
				xmlTypeNamespace = null;
				return false;
			}
			xmlType = soapTypeAttribute.XmlTypeName;
			xmlTypeNamespace = soapTypeAttribute.XmlTypeNamespace;
			return true;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000C5C4A File Offset: 0x000C3E4A
		public static bool IsClrTypeNamespace(string namespaceString)
		{
			return namespaceString.StartsWith(SoapServices.XmlNsForClrType);
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000C5C58 File Offset: 0x000C3E58
		public static bool IsSoapActionValidForMethodBase(string soapAction, MethodBase mb)
		{
			string text;
			string text2;
			SoapServices.GetTypeAndMethodNameFromSoapAction(soapAction, out text, out text2);
			if (text2 != mb.Name)
			{
				return false;
			}
			string assemblyQualifiedName = mb.DeclaringType.AssemblyQualifiedName;
			return text == assemblyQualifiedName;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000C5C94 File Offset: 0x000C3E94
		public static void PreLoad(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				SoapServices.PreLoad(types[i]);
			}
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000C5CC0 File Offset: 0x000C3EC0
		public static void PreLoad(Type type)
		{
			SoapServices.TypeInfo typeInfo = SoapServices._typeInfos[type] as SoapServices.TypeInfo;
			if (typeInfo != null)
			{
				return;
			}
			string text;
			string text2;
			if (SoapServices.GetXmlTypeForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlType(text, text2, type);
			}
			if (SoapServices.GetXmlElementForInteropType(type, out text, out text2))
			{
				SoapServices.RegisterInteropXmlElement(text, text2, type);
			}
			object syncRoot = SoapServices._typeInfos.SyncRoot;
			lock (syncRoot)
			{
				typeInfo = new SoapServices.TypeInfo();
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
					if (soapFieldAttribute.IsInteropXmlElement())
					{
						string nameKey = SoapServices.GetNameKey(soapFieldAttribute.XmlElementName, soapFieldAttribute.XmlNamespace);
						if (soapFieldAttribute.UseAttribute)
						{
							if (typeInfo.Attributes == null)
							{
								typeInfo.Attributes = new Hashtable();
							}
							typeInfo.Attributes[nameKey] = fieldInfo;
						}
						else
						{
							if (typeInfo.Elements == null)
							{
								typeInfo.Elements = new Hashtable();
							}
							typeInfo.Elements[nameKey] = fieldInfo;
						}
					}
				}
				SoapServices._typeInfos[type] = typeInfo;
			}
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000C5DF4 File Offset: 0x000C3FF4
		public static void RegisterInteropXmlElement(string xmlElement, string xmlNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlElements.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlElements[xmlElement + " " + xmlNamespace] = type;
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000C5E4C File Offset: 0x000C404C
		public static void RegisterInteropXmlType(string xmlType, string xmlTypeNamespace, Type type)
		{
			object syncRoot = SoapServices._xmlTypes.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._xmlTypes[xmlType + " " + xmlTypeNamespace] = type;
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000C5EA4 File Offset: 0x000C40A4
		public static void RegisterSoapActionForMethodBase(MethodBase mb)
		{
			SoapServices.InternalGetSoapAction(mb);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000C5EB0 File Offset: 0x000C40B0
		private static string InternalGetSoapAction(MethodBase mb)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			string text2;
			lock (syncRoot)
			{
				string text = (string)SoapServices._soapActions[mb];
				if (text == null)
				{
					text = ((SoapMethodAttribute)InternalRemotingServices.GetCachedSoapAttribute(mb)).SoapAction;
					SoapServices._soapActions[mb] = text;
					SoapServices._soapActionsMethods[text] = mb;
				}
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000C5F30 File Offset: 0x000C4130
		public static void RegisterSoapActionForMethodBase(MethodBase mb, string soapAction)
		{
			object syncRoot = SoapServices._soapActions.SyncRoot;
			lock (syncRoot)
			{
				SoapServices._soapActions[mb] = soapAction;
				SoapServices._soapActionsMethods[soapAction] = mb;
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000C5F88 File Offset: 0x000C4188
		private static string EncodeNs(string ns)
		{
			ns = ns.Replace(",", "%2C");
			ns = ns.Replace(" ", "%20");
			return ns.Replace("=", "%3D");
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000C5FBE File Offset: 0x000C41BE
		private static string DecodeNs(string ns)
		{
			ns = ns.Replace("%2C", ",");
			ns = ns.Replace("%20", " ");
			return ns.Replace("%3D", "=");
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000C5FF4 File Offset: 0x000C41F4
		// Note: this type is marked as 'beforefieldinit'.
		static SoapServices()
		{
		}

		// Token: 0x040024E6 RID: 9446
		private static Hashtable _xmlTypes = new Hashtable();

		// Token: 0x040024E7 RID: 9447
		private static Hashtable _xmlElements = new Hashtable();

		// Token: 0x040024E8 RID: 9448
		private static Hashtable _soapActions = new Hashtable();

		// Token: 0x040024E9 RID: 9449
		private static Hashtable _soapActionsMethods = new Hashtable();

		// Token: 0x040024EA RID: 9450
		private static Hashtable _typeInfos = new Hashtable();

		// Token: 0x02000546 RID: 1350
		private class TypeInfo
		{
			// Token: 0x06003687 RID: 13959 RVA: 0x000025BE File Offset: 0x000007BE
			public TypeInfo()
			{
			}

			// Token: 0x040024EB RID: 9451
			public Hashtable Attributes;

			// Token: 0x040024EC RID: 9452
			public Hashtable Elements;
		}
	}
}
