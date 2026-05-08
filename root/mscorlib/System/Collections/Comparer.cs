using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Collections
{
	// Token: 0x02000A67 RID: 2663
	[Serializable]
	public sealed class Comparer : IComparer, ISerializable
	{
		// Token: 0x06006177 RID: 24951 RVA: 0x0014DA96 File Offset: 0x0014BC96
		public Comparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0014DAB8 File Offset: 0x0014BCB8
		private Comparer(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._compareInfo = (CompareInfo)info.GetValue("CompareInfo", typeof(CompareInfo));
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0014DAEE File Offset: 0x0014BCEE
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("CompareInfo", this._compareInfo);
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0014DB10 File Offset: 0x0014BD10
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			if (a == null)
			{
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this._compareInfo.Compare(text, text2);
			}
			IComparable comparable = a as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(b);
			}
			IComparable comparable2 = b as IComparable;
			if (comparable2 != null)
			{
				return -comparable2.CompareTo(a);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0014DB7E File Offset: 0x0014BD7E
		// Note: this type is marked as 'beforefieldinit'.
		static Comparer()
		{
		}

		// Token: 0x04003A91 RID: 14993
		private CompareInfo _compareInfo;

		// Token: 0x04003A92 RID: 14994
		public static readonly Comparer Default = new Comparer(CultureInfo.CurrentCulture);

		// Token: 0x04003A93 RID: 14995
		public static readonly Comparer DefaultInvariant = new Comparer(CultureInfo.InvariantCulture);
	}
}
