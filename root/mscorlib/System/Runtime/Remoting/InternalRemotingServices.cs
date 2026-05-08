using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Remoting
{
	// Token: 0x02000533 RID: 1331
	[ComVisible(true)]
	public class InternalRemotingServices
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000025BE File Offset: 0x000007BE
		public InternalRemotingServices()
		{
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x00047E00 File Offset: 0x00046000
		[Conditional("_LOGGING")]
		public static void DebugOutChnl(string s)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000C1F50 File Offset: 0x000C0150
		public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
		{
			object syncRoot = InternalRemotingServices._soapAttributes.SyncRoot;
			SoapAttribute soapAttribute2;
			lock (syncRoot)
			{
				SoapAttribute soapAttribute = InternalRemotingServices._soapAttributes[reflectionObject] as SoapAttribute;
				if (soapAttribute != null)
				{
					soapAttribute2 = soapAttribute;
				}
				else
				{
					object[] customAttributes = ((ICustomAttributeProvider)reflectionObject).GetCustomAttributes(typeof(SoapAttribute), true);
					if (customAttributes.Length != 0)
					{
						soapAttribute = (SoapAttribute)customAttributes[0];
					}
					else if (reflectionObject is Type)
					{
						soapAttribute = new SoapTypeAttribute();
					}
					else if (reflectionObject is FieldInfo)
					{
						soapAttribute = new SoapFieldAttribute();
					}
					else if (reflectionObject is MethodBase)
					{
						soapAttribute = new SoapMethodAttribute();
					}
					else if (reflectionObject is ParameterInfo)
					{
						soapAttribute = new SoapParameterAttribute();
					}
					soapAttribute.SetReflectionObject(reflectionObject);
					InternalRemotingServices._soapAttributes[reflectionObject] = soapAttribute;
					soapAttribute2 = soapAttribute;
				}
			}
			return soapAttribute2;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x00047E00 File Offset: 0x00046000
		[Conditional("_DEBUG")]
		public static void RemotingAssert(bool condition, string message)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x00047E00 File Offset: 0x00046000
		[Conditional("_LOGGING")]
		public static void RemotingTrace(params object[] messages)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000C2028 File Offset: 0x000C0228
		[CLSCompliant(false)]
		public static void SetServerIdentity(MethodCall m, object srvID)
		{
			Identity identity = srvID as Identity;
			if (identity == null)
			{
				throw new ArgumentException("srvID");
			}
			RemotingServices.SetMessageTargetIdentity(m, identity);
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000C2051 File Offset: 0x000C0251
		// Note: this type is marked as 'beforefieldinit'.
		static InternalRemotingServices()
		{
		}

		// Token: 0x040024A8 RID: 9384
		private static Hashtable _soapAttributes = new Hashtable();
	}
}
