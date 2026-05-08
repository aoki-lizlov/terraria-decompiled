using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C3 RID: 1987
	public static class FormattableStringFactory
	{
		// Token: 0x06004593 RID: 17811 RVA: 0x000E51F3 File Offset: 0x000E33F3
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x020007C4 RID: 1988
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06004594 RID: 17812 RVA: 0x000E5218 File Offset: 0x000E3418
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x06004595 RID: 17813 RVA: 0x000E522E File Offset: 0x000E342E
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06004596 RID: 17814 RVA: 0x000E5236 File Offset: 0x000E3436
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06004597 RID: 17815 RVA: 0x000E523E File Offset: 0x000E343E
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x06004598 RID: 17816 RVA: 0x000E5248 File Offset: 0x000E3448
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x06004599 RID: 17817 RVA: 0x000E5252 File Offset: 0x000E3452
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x04002CBB RID: 11451
			private readonly string _format;

			// Token: 0x04002CBC RID: 11452
			private readonly object[] _arguments;
		}
	}
}
