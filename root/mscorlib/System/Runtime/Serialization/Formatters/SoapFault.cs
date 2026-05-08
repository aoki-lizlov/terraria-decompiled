using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000651 RID: 1617
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		// Token: 0x06003D78 RID: 15736 RVA: 0x000025BE File Offset: 0x000007BE
		public SoapFault()
		{
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000D54CE File Offset: 0x000D36CE
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000D54F4 File Offset: 0x000D36F4
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000D55D4 File Offset: 0x000D37D4
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000D5641 File Offset: 0x000D3841
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x000D5649 File Offset: 0x000D3849
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x000D5652 File Offset: 0x000D3852
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x000D565A File Offset: 0x000D385A
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000D5663 File Offset: 0x000D3863
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x000D566B File Offset: 0x000D386B
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000D5674 File Offset: 0x000D3874
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x000D567C File Offset: 0x000D387C
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x04002733 RID: 10035
		private string faultCode;

		// Token: 0x04002734 RID: 10036
		private string faultString;

		// Token: 0x04002735 RID: 10037
		private string faultActor;

		// Token: 0x04002736 RID: 10038
		[SoapField(Embedded = true)]
		private object detail;
	}
}
