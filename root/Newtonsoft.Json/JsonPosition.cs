using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200000F RID: 15
	internal struct JsonPosition
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000020D8 File Offset: 0x000002D8
		public JsonPosition(JsonContainerType type)
		{
			this.Type = type;
			this.HasIndex = JsonPosition.TypeHasIndex(type);
			this.Position = -1;
			this.PropertyName = null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020FC File Offset: 0x000002FC
		internal int CalculateLength()
		{
			switch (this.Type)
			{
			case JsonContainerType.Object:
				return this.PropertyName.Length + 5;
			case JsonContainerType.Array:
			case JsonContainerType.Constructor:
				return MathUtils.IntLength((ulong)((long)this.Position)) + 2;
			default:
				throw new ArgumentOutOfRangeException("Type");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002150 File Offset: 0x00000350
		internal void WriteTo(StringBuilder sb)
		{
			switch (this.Type)
			{
			case JsonContainerType.Object:
			{
				string propertyName = this.PropertyName;
				if (propertyName.IndexOfAny(JsonPosition.SpecialCharacters) != -1)
				{
					sb.Append("['");
					sb.Append(propertyName);
					sb.Append("']");
					return;
				}
				if (sb.Length > 0)
				{
					sb.Append('.');
				}
				sb.Append(propertyName);
				return;
			}
			case JsonContainerType.Array:
			case JsonContainerType.Constructor:
				sb.Append('[');
				sb.Append(this.Position);
				sb.Append(']');
				return;
			default:
				return;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E9 File Offset: 0x000003E9
		internal static bool TypeHasIndex(JsonContainerType type)
		{
			return type == JsonContainerType.Array || type == JsonContainerType.Constructor;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021F8 File Offset: 0x000003F8
		internal static string BuildPath(List<JsonPosition> positions, JsonPosition? currentPosition)
		{
			int num = 0;
			if (positions != null)
			{
				for (int i = 0; i < positions.Count; i++)
				{
					num += positions[i].CalculateLength();
				}
			}
			if (currentPosition != null)
			{
				num += currentPosition.GetValueOrDefault().CalculateLength();
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (positions != null)
			{
				foreach (JsonPosition jsonPosition in positions)
				{
					jsonPosition.WriteTo(stringBuilder);
				}
			}
			if (currentPosition != null)
			{
				currentPosition.GetValueOrDefault().WriteTo(stringBuilder);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B8 File Offset: 0x000004B8
		internal static string FormatMessage(IJsonLineInfo lineInfo, string path, string message)
		{
			if (!message.EndsWith(Environment.NewLine, 4))
			{
				message = message.Trim();
				if (!message.EndsWith('.'))
				{
					message += ".";
				}
				message += " ";
			}
			message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber, lineInfo.LinePosition);
			}
			message += ".";
			return message;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000235D File Offset: 0x0000055D
		// Note: this type is marked as 'beforefieldinit'.
		static JsonPosition()
		{
		}

		// Token: 0x04000021 RID: 33
		private static readonly char[] SpecialCharacters = new char[] { '.', ' ', '[', ']', '(', ')' };

		// Token: 0x04000022 RID: 34
		internal JsonContainerType Type;

		// Token: 0x04000023 RID: 35
		internal int Position;

		// Token: 0x04000024 RID: 36
		internal string PropertyName;

		// Token: 0x04000025 RID: 37
		internal bool HasIndex;
	}
}
