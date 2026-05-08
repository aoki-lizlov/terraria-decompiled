using System;
using Terraria.Localization;

namespace Terraria.UI
{
	// Token: 0x020000ED RID: 237
	public class ItemTooltip
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x004E7723 File Offset: 0x004E5923
		public int Lines
		{
			get
			{
				this.ValidateTooltip();
				if (this._tooltipLines == null)
				{
					return 0;
				}
				return this._tooltipLines.Length;
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0000357B File Offset: 0x0000177B
		private ItemTooltip()
		{
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x004E773D File Offset: 0x004E593D
		private ItemTooltip(string key)
		{
			this._text = Language.GetText(key);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x004E7751 File Offset: 0x004E5951
		public static ItemTooltip FromLanguageKey(string key)
		{
			return new ItemTooltip(key);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x004E7759 File Offset: 0x004E5959
		public string GetLine(int line)
		{
			this.ValidateTooltip();
			return this._tooltipLines[line];
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x004E7769 File Offset: 0x004E5969
		private ItemTooltip(string[] hardcodedLines)
		{
			this._validatorKey = ItemTooltip._neverUpdateHack;
			this._tooltipLines = hardcodedLines;
			this._processedText = string.Join("\n", hardcodedLines);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x004E7794 File Offset: 0x004E5994
		public static ItemTooltip FromHardcodedText(params string[] text)
		{
			return new ItemTooltip(text);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x004E779C File Offset: 0x004E599C
		private void ValidateTooltip()
		{
			if (this._validatorKey == ItemTooltip._neverUpdateHack)
			{
				return;
			}
			if (this._validatorKey != ItemTooltip._globalValidatorKey)
			{
				this._validatorKey = ItemTooltip._globalValidatorKey;
				if (this._text == null || !this._text.HasValue)
				{
					this._tooltipLines = null;
					this._processedText = string.Empty;
					return;
				}
				string value = this._text.Value;
				this._tooltipLines = value.Split(new char[] { '\n' });
				this._processedText = value;
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x004E7821 File Offset: 0x004E5A21
		public static void InvalidateTooltips()
		{
			ItemTooltip._globalValidatorKey += 1UL;
			if (ItemTooltip._globalValidatorKey == 18446744073709551615UL)
			{
				ItemTooltip._globalValidatorKey = 0UL;
			}
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x004E7840 File Offset: 0x004E5A40
		// Note: this type is marked as 'beforefieldinit'.
		static ItemTooltip()
		{
		}

		// Token: 0x0400132B RID: 4907
		public static readonly ItemTooltip None = new ItemTooltip();

		// Token: 0x0400132C RID: 4908
		private static ulong _globalValidatorKey = 1UL;

		// Token: 0x0400132D RID: 4909
		private static readonly ulong _neverUpdateHack = ulong.MaxValue;

		// Token: 0x0400132E RID: 4910
		private string[] _tooltipLines;

		// Token: 0x0400132F RID: 4911
		private ulong _validatorKey;

		// Token: 0x04001330 RID: 4912
		private readonly LocalizedText _text;

		// Token: 0x04001331 RID: 4913
		private string _processedText;
	}
}
