using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x020005B4 RID: 1460
	[ComVisible(true)]
	public sealed class SoapDateTime
	{
		// Token: 0x060038D9 RID: 14553 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapDateTime()
		{
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000CAE25 File Offset: 0x000C9025
		public static string XsdType
		{
			get
			{
				return "dateTime";
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000CAE2C File Offset: 0x000C902C
		public static DateTime Parse(string value)
		{
			return DateTime.ParseExact(value, SoapDateTime._datetimeFormats, null, DateTimeStyles.None);
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000CAE3B File Offset: 0x000C903B
		public static string ToString(DateTime value)
		{
			return value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000CAE50 File Offset: 0x000C9050
		// Note: this type is marked as 'beforefieldinit'.
		static SoapDateTime()
		{
		}

		// Token: 0x040025A8 RID: 9640
		private static readonly string[] _datetimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff", "yyyy-MM-ddTHH:mm:sszzz", "yyyy-MM-ddTHH:mm:ss.fzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzz", "yyyy-MM-ddTHH:mm:ss.fffzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzz", "yyyy-MM-ddTHH:mm:ss.fffffffzzz", "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fZ", "yyyy-MM-ddTHH:mm:ss.ffZ", "yyyy-MM-ddTHH:mm:ss.fffZ",
			"yyyy-MM-ddTHH:mm:ss.ffffZ", "yyyy-MM-ddTHH:mm:ss.fffffZ", "yyyy-MM-ddTHH:mm:ss.ffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffZ"
		};
	}
}
