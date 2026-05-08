using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B4 RID: 180
	public class JConstructor : JContainer
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00023267 File Offset: 0x00021467
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002326F File Offset: 0x0002146F
		internal override int IndexOfItem(JToken item)
		{
			return this._values.IndexOfReference(item);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00023280 File Offset: 0x00021480
		internal override void MergeItem(object content, JsonMergeSettings settings)
		{
			JConstructor jconstructor = content as JConstructor;
			if (jconstructor == null)
			{
				return;
			}
			if (jconstructor.Name != null)
			{
				this.Name = jconstructor.Name;
			}
			JContainer.MergeEnumerableContent(this, jconstructor, settings);
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000232B4 File Offset: 0x000214B4
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x000232BC File Offset: 0x000214BC
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000232C5 File Offset: 0x000214C5
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000232C8 File Offset: 0x000214C8
		public JConstructor()
		{
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000232DB File Offset: 0x000214DB
		public JConstructor(JConstructor other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000232FB File Offset: 0x000214FB
		public JConstructor(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00023305 File Offset: 0x00021505
		public JConstructor(string name, object content)
			: this(name)
		{
			this.Add(content);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00023315 File Offset: 0x00021515
		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Constructor name cannot be empty.", "name");
			}
			this._name = name;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00023358 File Offset: 0x00021558
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002338B File Offset: 0x0002158B
		internal override JToken CloneToken()
		{
			return new JConstructor(this);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00023394 File Offset: 0x00021594
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			int count = this._values.Count;
			for (int i = 0; i < count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		// Token: 0x170001BD RID: 445
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00023457 File Offset: 0x00021657
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ base.ContentsHashCode();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002346B File Offset: 0x0002166B
		public new static JConstructor Load(JsonReader reader)
		{
			return JConstructor.Load(reader, null);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00023474 File Offset: 0x00021674
		public new static JConstructor Load(JsonReader reader, JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo, settings);
			jconstructor.ReadTokenFrom(reader, settings);
			return jconstructor;
		}

		// Token: 0x0400033C RID: 828
		private string _name;

		// Token: 0x0400033D RID: 829
		private readonly List<JToken> _values = new List<JToken>();
	}
}
