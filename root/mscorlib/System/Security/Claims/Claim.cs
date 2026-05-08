using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System.Security.Claims
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	public class Claim
	{
		// Token: 0x06003227 RID: 12839 RVA: 0x000B9674 File Offset: 0x000B7874
		public Claim(BinaryReader reader)
			: this(reader, null)
		{
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000B967E File Offset: 0x000B787E
		public Claim(BinaryReader reader, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader, subject);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000B96A7 File Offset: 0x000B78A7
		public Claim(string type, string value)
			: this(type, value, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000B96C1 File Offset: 0x000B78C1
		public Claim(string type, string value, string valueType)
			: this(type, value, valueType, "LOCAL AUTHORITY", "LOCAL AUTHORITY", null)
		{
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000B96D7 File Offset: 0x000B78D7
		public Claim(string type, string value, string valueType, string issuer)
			: this(type, value, valueType, issuer, issuer, null)
		{
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000B96E7 File Offset: 0x000B78E7
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer)
			: this(type, value, valueType, issuer, originalIssuer, null)
		{
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000B96F8 File Offset: 0x000B78F8
		public Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject)
			: this(type, value, valueType, issuer, originalIssuer, subject, null, null)
		{
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000B9718 File Offset: 0x000B7918
		internal Claim(string type, string value, string valueType, string issuer, string originalIssuer, ClaimsIdentity subject, string propertyKey, string propertyValue)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_type = type;
			this.m_value = value;
			if (string.IsNullOrEmpty(valueType))
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			else
			{
				this.m_valueType = valueType;
			}
			if (string.IsNullOrEmpty(issuer))
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			else
			{
				this.m_issuer = issuer;
			}
			if (string.IsNullOrEmpty(originalIssuer))
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else
			{
				this.m_originalIssuer = originalIssuer;
			}
			this.m_subject = subject;
			if (propertyKey != null)
			{
				this.Properties.Add(propertyKey, propertyValue);
			}
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000B97D4 File Offset: 0x000B79D4
		protected Claim(Claim other)
			: this(other, (other == null) ? null : other.m_subject)
		{
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000B97EC File Offset: 0x000B79EC
		protected Claim(Claim other, ClaimsIdentity subject)
		{
			this.m_propertyLock = new object();
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.m_issuer = other.m_issuer;
			this.m_originalIssuer = other.m_originalIssuer;
			this.m_subject = subject;
			this.m_type = other.m_type;
			this.m_value = other.m_value;
			this.m_valueType = other.m_valueType;
			if (other.m_properties != null)
			{
				this.m_properties = new Dictionary<string, string>();
				foreach (string text in other.m_properties.Keys)
				{
					this.m_properties.Add(text, other.m_properties[text]);
				}
			}
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000B98E8 File Offset: 0x000B7AE8
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000B98F0 File Offset: 0x000B7AF0
		public string Issuer
		{
			get
			{
				return this.m_issuer;
			}
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x000B98F8 File Offset: 0x000B7AF8
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			this.m_propertyLock = new object();
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000B9905 File Offset: 0x000B7B05
		public string OriginalIssuer
		{
			get
			{
				return this.m_originalIssuer;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000B9910 File Offset: 0x000B7B10
		public IDictionary<string, string> Properties
		{
			get
			{
				if (this.m_properties == null)
				{
					object propertyLock = this.m_propertyLock;
					lock (propertyLock)
					{
						if (this.m_properties == null)
						{
							this.m_properties = new Dictionary<string, string>();
						}
					}
				}
				return this.m_properties;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000B996C File Offset: 0x000B7B6C
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x000B9974 File Offset: 0x000B7B74
		public ClaimsIdentity Subject
		{
			get
			{
				return this.m_subject;
			}
			internal set
			{
				this.m_subject = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x000B997D File Offset: 0x000B7B7D
		public string Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x000B9985 File Offset: 0x000B7B85
		public string Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000B998D File Offset: 0x000B7B8D
		public string ValueType
		{
			get
			{
				return this.m_valueType;
			}
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000B9995 File Offset: 0x000B7B95
		public virtual Claim Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000B999E File Offset: 0x000B7B9E
		public virtual Claim Clone(ClaimsIdentity identity)
		{
			return new Claim(this, identity);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000B99A8 File Offset: 0x000B7BA8
		private void Initialize(BinaryReader reader, ClaimsIdentity subject)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.m_subject = subject;
			Claim.SerializationMask serializationMask = (Claim.SerializationMask)reader.ReadInt32();
			int num = 1;
			int num2 = reader.ReadInt32();
			this.m_value = reader.ReadString();
			if ((serializationMask & Claim.SerializationMask.NameClaimType) == Claim.SerializationMask.NameClaimType)
			{
				this.m_type = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			else if ((serializationMask & Claim.SerializationMask.RoleClaimType) == Claim.SerializationMask.RoleClaimType)
			{
				this.m_type = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			else
			{
				this.m_type = reader.ReadString();
				num++;
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				this.m_valueType = reader.ReadString();
				num++;
			}
			else
			{
				this.m_valueType = "http://www.w3.org/2001/XMLSchema#string";
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				this.m_issuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_issuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuerEqualsIssuer) == Claim.SerializationMask.OriginalIssuerEqualsIssuer)
			{
				this.m_originalIssuer = this.m_issuer;
			}
			else if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				this.m_originalIssuer = reader.ReadString();
				num++;
			}
			else
			{
				this.m_originalIssuer = "LOCAL AUTHORITY";
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.Properties.Add(reader.ReadString(), reader.ReadString());
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num++;
			}
			for (int j = num; j < num2; j++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000B9B12 File Offset: 0x000B7D12
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000B9B1C File Offset: 0x000B7D1C
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 1;
			Claim.SerializationMask serializationMask = Claim.SerializationMask.None;
			if (string.Equals(this.m_type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
			{
				serializationMask |= Claim.SerializationMask.NameClaimType;
			}
			else if (string.Equals(this.m_type, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"))
			{
				serializationMask |= Claim.SerializationMask.RoleClaimType;
			}
			else
			{
				num++;
			}
			if (!string.Equals(this.m_valueType, "http://www.w3.org/2001/XMLSchema#string", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.StringType;
			}
			if (!string.Equals(this.m_issuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.Issuer;
			}
			if (string.Equals(this.m_originalIssuer, this.m_issuer, StringComparison.Ordinal))
			{
				serializationMask |= Claim.SerializationMask.OriginalIssuerEqualsIssuer;
			}
			else if (!string.Equals(this.m_originalIssuer, "LOCAL AUTHORITY", StringComparison.Ordinal))
			{
				num++;
				serializationMask |= Claim.SerializationMask.OriginalIssuer;
			}
			if (this.Properties.Count > 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.HasProperties;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= Claim.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			writer.Write(this.m_value);
			if ((serializationMask & Claim.SerializationMask.NameClaimType) != Claim.SerializationMask.NameClaimType && (serializationMask & Claim.SerializationMask.RoleClaimType) != Claim.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_type);
			}
			if ((serializationMask & Claim.SerializationMask.StringType) == Claim.SerializationMask.StringType)
			{
				writer.Write(this.m_valueType);
			}
			if ((serializationMask & Claim.SerializationMask.Issuer) == Claim.SerializationMask.Issuer)
			{
				writer.Write(this.m_issuer);
			}
			if ((serializationMask & Claim.SerializationMask.OriginalIssuer) == Claim.SerializationMask.OriginalIssuer)
			{
				writer.Write(this.m_originalIssuer);
			}
			if ((serializationMask & Claim.SerializationMask.HasProperties) == Claim.SerializationMask.HasProperties)
			{
				writer.Write(this.Properties.Count);
				foreach (string text in this.Properties.Keys)
				{
					writer.Write(text);
					writer.Write(this.Properties[text]);
				}
			}
			if ((serializationMask & Claim.SerializationMask.UserData) == Claim.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000B9D04 File Offset: 0x000B7F04
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.m_type, this.m_value);
		}

		// Token: 0x04002306 RID: 8966
		private string m_issuer;

		// Token: 0x04002307 RID: 8967
		private string m_originalIssuer;

		// Token: 0x04002308 RID: 8968
		private string m_type;

		// Token: 0x04002309 RID: 8969
		private string m_value;

		// Token: 0x0400230A RID: 8970
		private string m_valueType;

		// Token: 0x0400230B RID: 8971
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x0400230C RID: 8972
		private Dictionary<string, string> m_properties;

		// Token: 0x0400230D RID: 8973
		[NonSerialized]
		private object m_propertyLock;

		// Token: 0x0400230E RID: 8974
		[NonSerialized]
		private ClaimsIdentity m_subject;

		// Token: 0x020004C2 RID: 1218
		private enum SerializationMask
		{
			// Token: 0x04002310 RID: 8976
			None,
			// Token: 0x04002311 RID: 8977
			NameClaimType,
			// Token: 0x04002312 RID: 8978
			RoleClaimType,
			// Token: 0x04002313 RID: 8979
			StringType = 4,
			// Token: 0x04002314 RID: 8980
			Issuer = 8,
			// Token: 0x04002315 RID: 8981
			OriginalIssuerEqualsIssuer = 16,
			// Token: 0x04002316 RID: 8982
			OriginalIssuer = 32,
			// Token: 0x04002317 RID: 8983
			HasProperties = 64,
			// Token: 0x04002318 RID: 8984
			UserData = 128
		}
	}
}
