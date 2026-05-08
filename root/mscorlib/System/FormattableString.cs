using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000E3 RID: 227
	public abstract class FormattableString : IFormattable
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000918 RID: 2328
		public abstract string Format { get; }

		// Token: 0x06000919 RID: 2329
		public abstract object[] GetArguments();

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600091A RID: 2330
		public abstract int ArgumentCount { get; }

		// Token: 0x0600091B RID: 2331
		public abstract object GetArgument(int index);

		// Token: 0x0600091C RID: 2332
		public abstract string ToString(IFormatProvider formatProvider);

		// Token: 0x0600091D RID: 2333 RVA: 0x0002027A File Offset: 0x0001E47A
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00020283 File Offset: 0x0001E483
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002029E File Offset: 0x0001E49E
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000025BE File Offset: 0x000007BE
		protected FormattableString()
		{
		}
	}
}
