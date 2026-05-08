using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x0200064F RID: 1615
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalST
	{
		// Token: 0x06003D6F RID: 15727 RVA: 0x000025BE File Offset: 0x000007BE
		private InternalST()
		{
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000D53E4 File Offset: 0x000D35E4
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000D53F0 File Offset: 0x000D35F0
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x00004088 File Offset: 0x00002288
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000D543E File Offset: 0x000D363E
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000D5478 File Offset: 0x000D3678
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
