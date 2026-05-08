using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000100 RID: 256
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : JsonWriter
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x000302EF File Offset: 0x0002E4EF
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x000302FC File Offset: 0x0002E4FC
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0003030A File Offset: 0x0002E50A
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0003032E File Offset: 0x0002E52E
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0003034D File Offset: 0x0002E54D
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0003035A File Offset: 0x0002E55A
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00030382 File Offset: 0x0002E582
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00030390 File Offset: 0x0002E590
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003039E File Offset: 0x0002E59E
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003039E File Offset: 0x0002E59E
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000303AC File Offset: 0x0002E5AC
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000303BF File Offset: 0x0002E5BF
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000303D2 File Offset: 0x0002E5D2
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000303E2 File Offset: 0x0002E5E2
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00030402 File Offset: 0x0002E602
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00030412 File Offset: 0x0002E612
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00030425 File Offset: 0x0002E625
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00030434 File Offset: 0x0002E634
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000304C4 File Offset: 0x0002E6C4
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.SetWriteState(JsonToken.Integer, null);
				this.AddToken(new BsonBinary(((BigInteger)value).ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00030503 File Offset: 0x0002E703
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00030516 File Offset: 0x0002E716
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00030529 File Offset: 0x0002E729
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00030549 File Offset: 0x0002E749
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00030560 File Offset: 0x0002E760
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003058C File Offset: 0x0002E78C
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000305A3 File Offset: 0x0002E7A3
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000305D3 File Offset: 0x0002E7D3
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000305E9 File Offset: 0x0002E7E9
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000305FF File Offset: 0x0002E7FF
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0003061D File Offset: 0x0002E81D
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00030634 File Offset: 0x0002E834
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0003064C File Offset: 0x0002E84C
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string text = value.ToString(CultureInfo.InvariantCulture);
			this.AddToken(new BsonString(text, true));
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003067C File Offset: 0x0002E87C
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00030693 File Offset: 0x0002E893
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000306AA File Offset: 0x0002E8AA
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000306C0 File Offset: 0x0002E8C0
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000306E5 File Offset: 0x0002E8E5
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000306FC File Offset: 0x0002E8FC
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0003071C File Offset: 0x0002E91C
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00030738 File Offset: 0x0002E938
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003075A File Offset: 0x0002E95A
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00030785 File Offset: 0x0002E985
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000307B7 File Offset: 0x0002E9B7
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04000404 RID: 1028
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04000405 RID: 1029
		private BsonToken _root;

		// Token: 0x04000406 RID: 1030
		private BsonToken _parent;

		// Token: 0x04000407 RID: 1031
		private string _propertyName;
	}
}
