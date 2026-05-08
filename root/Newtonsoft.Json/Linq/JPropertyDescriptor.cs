using System;
using System.ComponentModel;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B0 RID: 176
	public class JPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00022ED8 File Offset: 0x000210D8
		public JPropertyDescriptor(string name)
			: base(name, null)
		{
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00022EE2 File Offset: 0x000210E2
		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00022EEA File Offset: 0x000210EA
		public override object GetValue(object component)
		{
			return JPropertyDescriptor.CastInstance(component)[this.Name];
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00002C0D File Offset: 0x00000E0D
		public override void ResetValue(object component)
		{
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022F00 File Offset: 0x00021100
		public override void SetValue(object component, object value)
		{
			JToken jtoken = (value as JToken) ?? new JValue(value);
			JPropertyDescriptor.CastInstance(component)[this.Name] = jtoken;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00022F30 File Offset: 0x00021130
		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00022F3C File Offset: 0x0002113C
		public override Type PropertyType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00022F48 File Offset: 0x00021148
		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}
	}
}
