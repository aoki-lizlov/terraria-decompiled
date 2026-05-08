using System;
using System.IO;

namespace Terraria.Localization
{
	// Token: 0x0200018B RID: 395
	public class NetworkText
	{
		// Token: 0x06001EEA RID: 7914 RVA: 0x00511A43 File Offset: 0x0050FC43
		private NetworkText(string text, NetworkText.Mode mode)
		{
			this._text = text;
			this._mode = mode;
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00511A5C File Offset: 0x0050FC5C
		private static NetworkText[] ConvertSubstitutionsToNetworkText(object[] substitutions)
		{
			NetworkText[] array = new NetworkText[substitutions.Length];
			for (int i = 0; i < substitutions.Length; i++)
			{
				NetworkText networkText = substitutions[i] as NetworkText;
				if (networkText == null)
				{
					networkText = NetworkText.FromLiteral(substitutions[i].ToString());
				}
				array[i] = networkText;
			}
			return array;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x00511A9F File Offset: 0x0050FC9F
		public static NetworkText FromFormattable(string text, params object[] substitutions)
		{
			return new NetworkText(text, NetworkText.Mode.Formattable)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x00511AB4 File Offset: 0x0050FCB4
		public static NetworkText FromLiteral(string text)
		{
			return new NetworkText(text, NetworkText.Mode.Literal);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00511ABD File Offset: 0x0050FCBD
		public static NetworkText FromKey(string key, params object[] substitutions)
		{
			return new NetworkText(key, NetworkText.Mode.LocalizationKey)
			{
				_substitutions = NetworkText.ConvertSubstitutionsToNetworkText(substitutions)
			};
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00511AD2 File Offset: 0x0050FCD2
		public void Serialize(BinaryWriter writer)
		{
			writer.Write((byte)this._mode);
			writer.Write(this._text);
			this.SerializeSubstitutionList(writer);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00511AF4 File Offset: 0x0050FCF4
		private void SerializeSubstitutionList(BinaryWriter writer)
		{
			if (this._mode == NetworkText.Mode.Literal)
			{
				return;
			}
			writer.Write((byte)this._substitutions.Length);
			for (int i = 0; i < (this._substitutions.Length & 255); i++)
			{
				this._substitutions[i].Serialize(writer);
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00511B40 File Offset: 0x0050FD40
		public static NetworkText Deserialize(BinaryReader reader)
		{
			NetworkText.Mode mode = (NetworkText.Mode)reader.ReadByte();
			NetworkText networkText = new NetworkText(reader.ReadString(), mode);
			networkText.DeserializeSubstitutionList(reader);
			return networkText;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00511B68 File Offset: 0x0050FD68
		public static NetworkText DeserializeLiteral(BinaryReader reader)
		{
			NetworkText.Mode mode = (NetworkText.Mode)reader.ReadByte();
			NetworkText networkText = new NetworkText(reader.ReadString(), mode);
			networkText.DeserializeSubstitutionList(reader);
			if (mode != NetworkText.Mode.Literal)
			{
				networkText.SetToEmptyLiteral();
			}
			return networkText;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00511B9C File Offset: 0x0050FD9C
		private void DeserializeSubstitutionList(BinaryReader reader)
		{
			if (this._mode == NetworkText.Mode.Literal)
			{
				return;
			}
			this._substitutions = new NetworkText[(int)reader.ReadByte()];
			for (int i = 0; i < this._substitutions.Length; i++)
			{
				this._substitutions[i] = NetworkText.Deserialize(reader);
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00511BE4 File Offset: 0x0050FDE4
		private void SetToEmptyLiteral()
		{
			this._mode = NetworkText.Mode.Literal;
			this._text = string.Empty;
			this._substitutions = null;
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00511C00 File Offset: 0x0050FE00
		public override string ToString()
		{
			try
			{
				switch (this._mode)
				{
				case NetworkText.Mode.Literal:
					return this._text;
				case NetworkText.Mode.Formattable:
				{
					string text = this._text;
					object[] array = this._substitutions;
					return string.Format(text, array);
				}
				case NetworkText.Mode.LocalizationKey:
				{
					string text2 = this._text;
					object[] array = this._substitutions;
					return Language.GetTextValue(text2, array);
				}
				default:
					return this._text;
				}
			}
			catch (Exception ex)
			{
				"NetworkText.ToString() threw an exception.\n" + this.ToDebugInfoString("") + "\n" + "Exception: " + ex;
				this.SetToEmptyLiteral();
			}
			return this._text;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00511CB4 File Offset: 0x0050FEB4
		private string ToDebugInfoString(string linePrefix = "")
		{
			string text = string.Format("{0}Mode: {1}\n{0}Text: {2}\n", linePrefix, this._mode, this._text);
			if (this._mode == NetworkText.Mode.LocalizationKey)
			{
				text += string.Format("{0}Localized Text: {1}\n", linePrefix, Language.GetTextValue(this._text));
			}
			if (this._mode != NetworkText.Mode.Literal)
			{
				for (int i = 0; i < this._substitutions.Length; i++)
				{
					text += string.Format("{0}Substitution {1}:\n", linePrefix, i);
					text += this._substitutions[i].ToDebugInfoString(linePrefix + "\t");
				}
			}
			return text;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00511D57 File Offset: 0x0050FF57
		// Note: this type is marked as 'beforefieldinit'.
		static NetworkText()
		{
		}

		// Token: 0x040016FA RID: 5882
		public static readonly NetworkText Empty = NetworkText.FromLiteral("");

		// Token: 0x040016FB RID: 5883
		private NetworkText[] _substitutions;

		// Token: 0x040016FC RID: 5884
		private string _text;

		// Token: 0x040016FD RID: 5885
		private NetworkText.Mode _mode;

		// Token: 0x0200075A RID: 1882
		private enum Mode : byte
		{
			// Token: 0x040069FC RID: 27132
			Literal,
			// Token: 0x040069FD RID: 27133
			Formattable,
			// Token: 0x040069FE RID: 27134
			LocalizationKey
		}
	}
}
