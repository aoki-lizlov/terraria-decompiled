using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000AF RID: 175
	public class JRaw : JValue
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00022E52 File Offset: 0x00021052
		public JRaw(JRaw other)
			: base(other)
		{
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00022E5B File Offset: 0x0002105B
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00022E68 File Offset: 0x00021068
		public static JRaw Create(JsonReader reader)
		{
			JRaw jraw;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					jraw = new JRaw(stringWriter.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00022ED0 File Offset: 0x000210D0
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
