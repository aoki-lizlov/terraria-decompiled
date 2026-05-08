using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F1 RID: 241
	public class XmlNodeConverter : JsonConverter
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002D3BB File Offset: 0x0002B5BB
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0002D3C3 File Offset: 0x0002B5C3
		public string DeserializeRootElementName
		{
			[CompilerGenerated]
			get
			{
				return this.<DeserializeRootElementName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DeserializeRootElementName>k__BackingField = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002D3CC File Offset: 0x0002B5CC
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public bool WriteArrayAttribute
		{
			[CompilerGenerated]
			get
			{
				return this.<WriteArrayAttribute>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WriteArrayAttribute>k__BackingField = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0002D3DD File Offset: 0x0002B5DD
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0002D3E5 File Offset: 0x0002B5E5
		public bool OmitRootObject
		{
			[CompilerGenerated]
			get
			{
				return this.<OmitRootObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OmitRootObject>k__BackingField = value;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002D3F0 File Offset: 0x0002B5F0
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IXmlNode xmlNode = this.WrapXml(value);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			this.PushParentNamespaces(xmlNode, xmlNamespaceManager);
			if (!this.OmitRootObject)
			{
				writer.WriteStartObject();
			}
			this.SerializeNode(writer, xmlNode, xmlNamespaceManager, !this.OmitRootObject);
			if (!this.OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002D448 File Offset: 0x0002B648
		private IXmlNode WrapXml(object value)
		{
			XObject xobject = value as XObject;
			if (xobject != null)
			{
				return XContainerWrapper.WrapNode(xobject);
			}
			XmlNode xmlNode = value as XmlNode;
			if (xmlNode != null)
			{
				return XmlNodeWrapper.WrapNode(xmlNode);
			}
			throw new ArgumentException("Value must be an XML object.", "value");
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002D488 File Offset: 0x0002B688
		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == 1)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list != null)
			{
				list.Reverse();
				foreach (IXmlNode xmlNode2 in list)
				{
					manager.PushScope();
					foreach (IXmlNode xmlNode3 in xmlNode2.Attributes)
					{
						if (xmlNode3.NamespaceUri == "http://www.w3.org/2000/xmlns/" && xmlNode3.LocalName != "xmlns")
						{
							manager.AddNamespace(xmlNode3.LocalName, xmlNode3.Value);
						}
					}
				}
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002D580 File Offset: 0x0002B780
		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = ((node.NamespaceUri == null || (node.LocalName == "xmlns" && node.NamespaceUri == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceUri));
			if (!string.IsNullOrEmpty(text))
			{
				return text + ":" + XmlConvert.DecodeName(node.LocalName);
			}
			return XmlConvert.DecodeName(node.LocalName);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case 1:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return this.ResolveFullName(node, manager);
			case 2:
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + this.ResolveFullName(node, manager);
			case 3:
				return "#text";
			case 4:
				return "#cdata-section";
			case 7:
				return "?" + this.ResolveFullName(node, manager);
			case 8:
				return "#comment";
			case 10:
				return "!" + this.ResolveFullName(node, manager);
			case 13:
				return "#whitespace";
			case 14:
				return "#significant-whitespace";
			case 17:
				return "?xml";
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002D720 File Offset: 0x0002B920
		private bool IsArray(IXmlNode node)
		{
			foreach (IXmlNode xmlNode in node.Attributes)
			{
				if (xmlNode.LocalName == "Array" && xmlNode.NamespaceUri == "http://james.newtonking.com/projects/json")
				{
					return XmlConvert.ToBoolean(xmlNode.Value);
				}
			}
			return false;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002D7A4 File Offset: 0x0002B9A4
		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			int count = node.ChildNodes.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					string propertyName = this.GetPropertyName(node.ChildNodes[0], manager);
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, propertyName);
					return;
				}
				Dictionary<string, object> dictionary = null;
				string text = null;
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					IXmlNode xmlNode = node.ChildNodes[i];
					string propertyName2 = this.GetPropertyName(xmlNode, manager);
					object obj;
					if (dictionary == null)
					{
						if (text == null)
						{
							text = propertyName2;
						}
						else if (!(propertyName2 == text))
						{
							dictionary = new Dictionary<string, object>();
							if (i > 1)
							{
								List<IXmlNode> list = new List<IXmlNode>(i);
								for (int j = 0; j < i; j++)
								{
									list.Add(node.ChildNodes[j]);
								}
								dictionary.Add(text, list);
							}
							else
							{
								dictionary.Add(text, node.ChildNodes[0]);
							}
							dictionary.Add(propertyName2, xmlNode);
						}
					}
					else if (!dictionary.TryGetValue(propertyName2, ref obj))
					{
						dictionary.Add(propertyName2, xmlNode);
					}
					else
					{
						List<IXmlNode> list2 = obj as List<IXmlNode>;
						if (list2 == null)
						{
							List<IXmlNode> list3 = new List<IXmlNode>();
							list3.Add((IXmlNode)obj);
							list2 = list3;
							dictionary[propertyName2] = list2;
						}
						list2.Add(xmlNode);
					}
				}
				if (dictionary == null)
				{
					this.WriteGroupedNodes(writer, manager, writePropertyName, node.ChildNodes, text);
					return;
				}
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					List<IXmlNode> list4 = keyValuePair.Value as List<IXmlNode>;
					if (list4 != null)
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, list4, keyValuePair.Key);
					}
					else
					{
						this.WriteGroupedNodes(writer, manager, writePropertyName, (IXmlNode)keyValuePair.Value, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002D98C File Offset: 0x0002BB8C
		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, List<IXmlNode> groupedNodes, string elementNames)
		{
			if (groupedNodes.Count == 1 && !this.IsArray(groupedNodes[0]))
			{
				this.SerializeNode(writer, groupedNodes[0], manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			for (int i = 0; i < groupedNodes.Count; i++)
			{
				this.SerializeNode(writer, groupedNodes[i], manager, false);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002DA05 File Offset: 0x0002BC05
		private void WriteGroupedNodes(JsonWriter writer, XmlNamespaceManager manager, bool writePropertyName, IXmlNode node, string elementNames)
		{
			if (!this.IsArray(node))
			{
				this.SerializeNode(writer, node, manager, writePropertyName);
				return;
			}
			if (writePropertyName)
			{
				writer.WritePropertyName(elementNames);
			}
			writer.WriteStartArray();
			this.SerializeNode(writer, node, manager, false);
			writer.WriteEndArray();
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002DA40 File Offset: 0x0002BC40
		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case 1:
				if (this.IsArray(node) && XmlNodeConverter.AllSameName(node) && node.ChildNodes.Count > 0)
				{
					this.SerializeGroupedNodes(writer, node, manager, false);
					return;
				}
				manager.PushScope();
				foreach (IXmlNode xmlNode in node.Attributes)
				{
					if (xmlNode.NamespaceUri == "http://www.w3.org/2000/xmlns/")
					{
						string text = ((xmlNode.LocalName != "xmlns") ? XmlConvert.DecodeName(xmlNode.LocalName) : string.Empty);
						string value = xmlNode.Value;
						manager.AddNamespace(text, value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				if (!this.ValueAttributes(node.Attributes) && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == 3)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
				}
				else if (node.ChildNodes.Count == 0 && node.Attributes.Count == 0)
				{
					if (((IXmlElement)node).IsEmpty)
					{
						writer.WriteNull();
					}
					else
					{
						writer.WriteValue(string.Empty);
					}
				}
				else
				{
					writer.WriteStartObject();
					for (int i = 0; i < node.Attributes.Count; i++)
					{
						this.SerializeNode(writer, node.Attributes[i], manager, true);
					}
					this.SerializeGroupedNodes(writer, node, manager, true);
					writer.WriteEndObject();
				}
				manager.PopScope();
				return;
			case 2:
			case 3:
			case 4:
			case 7:
			case 13:
			case 14:
				if (node.NamespaceUri == "http://www.w3.org/2000/xmlns/" && node.Value == "http://james.newtonking.com/projects/json")
				{
					return;
				}
				if (node.NamespaceUri == "http://james.newtonking.com/projects/json" && node.LocalName == "Array")
				{
					return;
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				writer.WriteValue(node.Value);
				return;
			case 8:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
					return;
				}
				return;
			case 9:
			case 11:
				this.SerializeGroupedNodes(writer, node, manager, writePropertyName);
				return;
			case 10:
			{
				IXmlDocumentType xmlDocumentType = (IXmlDocumentType)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDocumentType.Name))
				{
					writer.WritePropertyName("@name");
					writer.WriteValue(xmlDocumentType.Name);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.Public))
				{
					writer.WritePropertyName("@public");
					writer.WriteValue(xmlDocumentType.Public);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.System))
				{
					writer.WritePropertyName("@system");
					writer.WriteValue(xmlDocumentType.System);
				}
				if (!string.IsNullOrEmpty(xmlDocumentType.InternalSubset))
				{
					writer.WritePropertyName("@internalSubset");
					writer.WriteValue(xmlDocumentType.InternalSubset);
				}
				writer.WriteEndObject();
				return;
			}
			case 17:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002DE30 File Offset: 0x0002C030
		private static bool AllSameName(IXmlNode node)
		{
			using (List<IXmlNode>.Enumerator enumerator = node.ChildNodes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.LocalName != node.LocalName)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002DE94 File Offset: 0x0002C094
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType != JsonToken.StartObject)
			{
				if (tokenType == JsonToken.Null)
				{
					return null;
				}
				throw JsonSerializationException.Create(reader, "XmlNodeConverter can only convert JSON that begins with an object.");
			}
			else
			{
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
				IXmlDocument xmlDocument = null;
				IXmlNode xmlNode = null;
				if (typeof(XObject).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(XDocument) && objectType != typeof(XElement))
					{
						throw JsonSerializationException.Create(reader, "XmlNodeConverter only supports deserializing XDocument or XElement.");
					}
					xmlDocument = new XDocumentWrapper(new XDocument());
					xmlNode = xmlDocument;
				}
				if (typeof(XmlNode).IsAssignableFrom(objectType))
				{
					if (objectType != typeof(XmlDocument))
					{
						throw JsonSerializationException.Create(reader, "XmlNodeConverter only supports deserializing XmlDocuments");
					}
					xmlDocument = new XmlDocumentWrapper(new XmlDocument
					{
						XmlResolver = null
					});
					xmlNode = xmlDocument;
				}
				if (xmlDocument == null || xmlNode == null)
				{
					throw JsonSerializationException.Create(reader, "Unexpected type when converting XML: " + objectType);
				}
				if (!string.IsNullOrEmpty(this.DeserializeRootElementName))
				{
					this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, xmlNamespaceManager);
				}
				else
				{
					reader.Read();
					this.DeserializeNode(reader, xmlDocument, xmlNamespaceManager, xmlNode);
				}
				if (objectType == typeof(XElement))
				{
					XElement xelement = (XElement)xmlDocument.DocumentElement.WrappedNode;
					xelement.Remove();
					return xelement;
				}
				return xmlDocument.WrappedNode;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002DFD8 File Offset: 0x0002C1D8
		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (propertyName == "#text")
			{
				currentNode.AppendChild(document.CreateTextNode(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (propertyName == "#cdata-section")
			{
				currentNode.AppendChild(document.CreateCDataSection(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (propertyName == "#whitespace")
			{
				currentNode.AppendChild(document.CreateWhitespace(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (propertyName == "#significant-whitespace")
			{
				currentNode.AppendChild(document.CreateSignificantWhitespace(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (!string.IsNullOrEmpty(propertyName) && propertyName.get_Chars(0) == '?')
			{
				this.CreateInstruction(reader, document, currentNode, propertyName);
				return;
			}
			if (string.Equals(propertyName, "!DOCTYPE", 5))
			{
				this.CreateDocumentType(reader, document, currentNode);
				return;
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
				return;
			}
			this.ReadElement(reader, document, currentNode, propertyName, manager);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw JsonSerializationException.Create(reader, "XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			Dictionary<string, string> dictionary = this.ReadAttributeElements(reader, manager);
			string text = MiscellaneousUtils.GetPrefix(propertyName);
			if (propertyName.StartsWith('@'))
			{
				string text2 = propertyName.Substring(1);
				string prefix = MiscellaneousUtils.GetPrefix(text2);
				XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, text2, manager, prefix);
				return;
			}
			if (propertyName.StartsWith('$'))
			{
				if (propertyName == "$values")
				{
					propertyName = propertyName.Substring(1);
					text = manager.LookupPrefix("http://james.newtonking.com/projects/json");
					this.CreateElement(reader, document, currentNode, propertyName, manager, text, dictionary);
					return;
				}
				if (propertyName == "$id" || propertyName == "$ref" || propertyName == "$type" || propertyName == "$value")
				{
					string text3 = propertyName.Substring(1);
					string text4 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
					XmlNodeConverter.AddAttribute(reader, document, currentNode, propertyName, text3, manager, text4);
					return;
				}
			}
			this.CreateElement(reader, document, currentNode, propertyName, manager, text, dictionary);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		private void CreateElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string elementName, XmlNamespaceManager manager, string elementPrefix, Dictionary<string, string> attributeNameValues)
		{
			IXmlElement xmlElement = this.CreateElement(elementName, document, elementPrefix, manager);
			currentNode.AppendChild(xmlElement);
			if (attributeNameValues != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in attributeNameValues)
				{
					string text = XmlConvert.EncodeName(keyValuePair.Key);
					string prefix = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
					IXmlNode xmlNode = ((!string.IsNullOrEmpty(prefix)) ? document.CreateAttribute(text, manager.LookupNamespace(prefix) ?? string.Empty, keyValuePair.Value) : document.CreateAttribute(text, keyValuePair.Value));
					xmlElement.SetAttributeNode(xmlNode);
				}
			}
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Date:
			{
				string text2 = this.ConvertTokenToXmlValue(reader);
				if (text2 != null)
				{
					xmlElement.AppendChild(document.CreateTextNode(text2));
					return;
				}
				return;
			}
			case JsonToken.Null:
				return;
			case JsonToken.EndObject:
				manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
				return;
			}
			manager.PushScope();
			this.DeserializeNode(reader, document, manager, xmlElement);
			manager.PopScope();
			manager.RemoveNamespace(string.Empty, manager.DefaultNamespace);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E348 File Offset: 0x0002C548
		private static void AddAttribute(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, string attributeName, XmlNamespaceManager manager, string attributePrefix)
		{
			if (currentNode.NodeType == 9)
			{
				throw JsonSerializationException.Create(reader, "JSON root object has property '{0}' that will be converted to an attribute. A root object cannot have any attribute properties. Consider specifying a DeserializeRootElementName.".FormatWith(CultureInfo.InvariantCulture, propertyName));
			}
			string text = XmlConvert.EncodeName(attributeName);
			string text2 = reader.Value.ToString();
			IXmlNode xmlNode = ((!string.IsNullOrEmpty(attributePrefix)) ? document.CreateAttribute(text, manager.LookupNamespace(attributePrefix), text2) : document.CreateAttribute(text, text2));
			((IXmlElement)currentNode).SetAttributeNode(xmlNode);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		private string ConvertTokenToXmlValue(JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case JsonToken.Integer:
				if (reader.Value is BigInteger)
				{
					return ((BigInteger)reader.Value).ToString(CultureInfo.InvariantCulture);
				}
				return XmlConvert.ToString(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Float:
				if (reader.Value is decimal)
				{
					return XmlConvert.ToString((decimal)reader.Value);
				}
				if (reader.Value is float)
				{
					return XmlConvert.ToString((float)reader.Value);
				}
				return XmlConvert.ToString(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.String:
			{
				object value = reader.Value;
				if (value == null)
				{
					return null;
				}
				return value.ToString();
			}
			case JsonToken.Boolean:
				return XmlConvert.ToString(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Null:
				return null;
			case JsonToken.Date:
			{
				if (reader.Value is DateTimeOffset)
				{
					return XmlConvert.ToString((DateTimeOffset)reader.Value);
				}
				DateTime dateTime = Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture);
				return XmlConvert.ToString(dateTime, DateTimeUtils.ToSerializationMode(dateTime.Kind));
			}
			}
			throw JsonSerializationException.Create(reader, "Cannot get an XML string value from token type '{0}'.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002E520 File Offset: 0x0002C720
		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read() && reader.TokenType != JsonToken.EndArray)
			{
				this.DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (this.WriteArrayAttribute)
			{
				this.AddJsonArrayAttribute(xmlElement, document);
			}
			if (num == 1 && this.WriteArrayAttribute)
			{
				foreach (IXmlNode xmlNode in xmlElement.ChildNodes)
				{
					IXmlElement xmlElement2 = xmlNode as IXmlElement;
					if (xmlElement2 != null && xmlElement2.LocalName == propertyName)
					{
						this.AddJsonArrayAttribute(xmlElement2, document);
						break;
					}
				}
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
			if (element is XElementWrapper && element.GetPrefixOfNamespace("http://james.newtonking.com/projects/json") == null)
			{
				element.SetAttributeNode(document.CreateAttribute("xmlns:json", "http://www.w3.org/2000/xmlns/", "http://james.newtonking.com/projects/json"));
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002E648 File Offset: 0x0002C848
		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			JsonToken jsonToken = reader.TokenType;
			switch (jsonToken)
			{
			case JsonToken.StartConstructor:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
				break;
			case JsonToken.PropertyName:
			case JsonToken.Comment:
			case JsonToken.Raw:
				goto IL_003A;
			default:
				if (jsonToken != JsonToken.Date)
				{
					goto IL_003A;
				}
				break;
			}
			return null;
			IL_003A:
			Dictionary<string, string> dictionary = null;
			bool flag = false;
			while (!flag && reader.Read())
			{
				jsonToken = reader.TokenType;
				if (jsonToken != JsonToken.PropertyName)
				{
					if (jsonToken != JsonToken.Comment && jsonToken != JsonToken.EndObject)
					{
						throw JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType);
					}
					flag = true;
				}
				else
				{
					string text = reader.Value.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						char c = text.get_Chars(0);
						if (c != '$')
						{
							if (c == '@')
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								text = text.Substring(1);
								reader.Read();
								string text2 = this.ConvertTokenToXmlValue(reader);
								dictionary.Add(text, text2);
								string text3;
								if (this.IsNamespaceAttribute(text, out text3))
								{
									manager.AddNamespace(text3, text2);
								}
							}
							else
							{
								flag = true;
							}
						}
						else if (text == "$values" || text == "$id" || text == "$ref" || text == "$type" || text == "$value")
						{
							string text4 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
							if (text4 == null)
							{
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								int? num = default(int?);
								while (manager.LookupNamespace("json" + num) != null)
								{
									num = new int?(num.GetValueOrDefault() + 1);
								}
								text4 = "json" + num;
								dictionary.Add("xmlns:" + text4, "http://james.newtonking.com/projects/json");
								manager.AddNamespace(text4, "http://james.newtonking.com/projects/json");
							}
							if (text == "$values")
							{
								flag = true;
							}
							else
							{
								text = text.Substring(1);
								reader.Read();
								if (!JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
								{
									throw JsonSerializationException.Create(reader, "Unexpected JsonToken: " + reader.TokenType);
								}
								if (dictionary == null)
								{
									dictionary = new Dictionary<string, string>();
								}
								object value = reader.Value;
								string text2 = ((value != null) ? value.ToString() : null);
								dictionary.Add(text4 + ":" + text, text2);
							}
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002E8B0 File Offset: 0x0002CAB0
		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				while (reader.Read() && reader.TokenType != JsonToken.EndObject)
				{
					string text4 = reader.Value.ToString();
					if (!(text4 == "@version"))
					{
						if (!(text4 == "@encoding"))
						{
							if (!(text4 == "@standalone"))
							{
								throw JsonSerializationException.Create(reader, "Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
							}
							reader.Read();
							text3 = this.ConvertTokenToXmlValue(reader);
						}
						else
						{
							reader.Read();
							text2 = this.ConvertTokenToXmlValue(reader);
						}
					}
					else
					{
						reader.Read();
						text = this.ConvertTokenToXmlValue(reader);
					}
				}
				IXmlNode xmlNode = document.CreateXmlDeclaration(text, text2, text3);
				currentNode.AppendChild(xmlNode);
				return;
			}
			IXmlNode xmlNode2 = document.CreateProcessingInstruction(propertyName.Substring(1), this.ConvertTokenToXmlValue(reader));
			currentNode.AppendChild(xmlNode2);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		private void CreateDocumentType(JsonReader reader, IXmlDocument document, IXmlNode currentNode)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			while (reader.Read() && reader.TokenType != JsonToken.EndObject)
			{
				string text5 = reader.Value.ToString();
				if (!(text5 == "@name"))
				{
					if (!(text5 == "@public"))
					{
						if (!(text5 == "@system"))
						{
							if (!(text5 == "@internalSubset"))
							{
								throw JsonSerializationException.Create(reader, "Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
							}
							reader.Read();
							text4 = this.ConvertTokenToXmlValue(reader);
						}
						else
						{
							reader.Read();
							text3 = this.ConvertTokenToXmlValue(reader);
						}
					}
					else
					{
						reader.Read();
						text2 = this.ConvertTokenToXmlValue(reader);
					}
				}
				else
				{
					reader.Read();
					text = this.ConvertTokenToXmlValue(reader);
				}
			}
			IXmlNode xmlNode = document.CreateXmlDocumentType(text, text2, text3, text4);
			currentNode.AppendChild(xmlNode);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002EA8C File Offset: 0x0002CC8C
		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			string text = XmlConvert.EncodeName(elementName);
			string text2 = (string.IsNullOrEmpty(elementPrefix) ? manager.DefaultNamespace : manager.LookupNamespace(elementPrefix));
			if (string.IsNullOrEmpty(text2))
			{
				return document.CreateElement(text);
			}
			return document.CreateElement(text, text2);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.StartConstructor:
				{
					string text = reader.Value.ToString();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndConstructor)
						{
							break;
						}
						this.DeserializeValue(reader, document, manager, text, currentNode);
					}
					goto IL_0175;
				}
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == 9 && document.DocumentElement != null)
					{
						goto Block_4;
					}
					string text2 = reader.Value.ToString();
					reader.Read();
					if (reader.TokenType == JsonToken.StartArray)
					{
						int num = 0;
						while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						{
							this.DeserializeValue(reader, document, manager, text2, currentNode);
							num++;
						}
						if (num != 1 || !this.WriteArrayAttribute)
						{
							goto IL_0175;
						}
						using (List<IXmlNode>.Enumerator enumerator = currentNode.ChildNodes.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IXmlNode xmlNode = enumerator.Current;
								IXmlElement xmlElement = xmlNode as IXmlElement;
								if (xmlElement != null && xmlElement.LocalName == text2)
								{
									this.AddJsonArrayAttribute(xmlElement, document);
									break;
								}
							}
							goto IL_0175;
						}
					}
					this.DeserializeValue(reader, document, manager, text2, currentNode);
					goto IL_0175;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_0175;
				}
				break;
				IL_0175:
				if (!reader.Read())
				{
					return;
				}
			}
			if (tokenType != JsonToken.EndObject && tokenType != JsonToken.EndArray)
			{
				throw JsonSerializationException.Create(reader, "Unexpected JsonToken when deserializing node: " + reader.TokenType);
			}
			return;
			Block_4:
			throw JsonSerializationException.Create(reader, "JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifying a DeserializeRootElementName.");
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002EC74 File Offset: 0x0002CE74
		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith("xmlns", 4))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName.get_Chars(5) == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
		private bool ValueAttributes(List<IXmlNode> c)
		{
			foreach (IXmlNode xmlNode in c)
			{
				if (!(xmlNode.NamespaceUri == "http://james.newtonking.com/projects/json") && (!(xmlNode.NamespaceUri == "http://www.w3.org/2000/xmlns/") || !(xmlNode.Value == "http://james.newtonking.com/projects/json")))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002ED48 File Offset: 0x0002CF48
		public override bool CanConvert(Type valueType)
		{
			if (valueType.AssignableToTypeName("System.Xml.Linq.XObject", false))
			{
				return this.IsXObject(valueType);
			}
			return valueType.AssignableToTypeName("System.Xml.XmlNode", false) && this.IsXmlNode(valueType);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0002ED77 File Offset: 0x0002CF77
		[MethodImpl(8)]
		private bool IsXObject(Type valueType)
		{
			return typeof(XObject).IsAssignableFrom(valueType);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002ED89 File Offset: 0x0002CF89
		[MethodImpl(8)]
		private bool IsXmlNode(Type valueType)
		{
			return typeof(XmlNode).IsAssignableFrom(valueType);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002AB63 File Offset: 0x00028D63
		public XmlNodeConverter()
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002ED9B File Offset: 0x0002CF9B
		// Note: this type is marked as 'beforefieldinit'.
		static XmlNodeConverter()
		{
		}

		// Token: 0x040003B7 RID: 951
		internal static readonly List<IXmlNode> EmptyChildNodes = new List<IXmlNode>();

		// Token: 0x040003B8 RID: 952
		private const string TextName = "#text";

		// Token: 0x040003B9 RID: 953
		private const string CommentName = "#comment";

		// Token: 0x040003BA RID: 954
		private const string CDataName = "#cdata-section";

		// Token: 0x040003BB RID: 955
		private const string WhitespaceName = "#whitespace";

		// Token: 0x040003BC RID: 956
		private const string SignificantWhitespaceName = "#significant-whitespace";

		// Token: 0x040003BD RID: 957
		private const string DeclarationName = "?xml";

		// Token: 0x040003BE RID: 958
		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";

		// Token: 0x040003BF RID: 959
		[CompilerGenerated]
		private string <DeserializeRootElementName>k__BackingField;

		// Token: 0x040003C0 RID: 960
		[CompilerGenerated]
		private bool <WriteArrayAttribute>k__BackingField;

		// Token: 0x040003C1 RID: 961
		[CompilerGenerated]
		private bool <OmitRootObject>k__BackingField;
	}
}
