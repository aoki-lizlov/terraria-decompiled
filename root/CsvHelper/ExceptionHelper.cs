using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CsvHelper
{
	// Token: 0x02000010 RID: 16
	internal static class ExceptionHelper
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005634 File Offset: 0x00003834
		public static void AddExceptionDataMessage(Exception exception, ICsvParser parser, Type type, Dictionary<string, List<int>> namedIndexes, int? currentIndex, string[] currentRecord)
		{
			try
			{
				exception.Data["CsvHelper"] = ExceptionHelper.GetErrorMessage(parser, type, namedIndexes, currentIndex, currentRecord);
			}
			catch (Exception ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("An error occurred while creating exception details.");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(ex.ToString());
				exception.Data["CsvHelper"] = stringBuilder.ToString();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000056B0 File Offset: 0x000038B0
		public static string GetErrorMessage(ICsvParser parser, Type type, Dictionary<string, List<int>> namedIndexes, int? currentIndex, string[] currentRecord)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (parser != null)
			{
				stringBuilder.AppendFormat("Row: '{0}' (1 based)", parser.Row).AppendLine();
			}
			if (type != null)
			{
				stringBuilder.AppendFormat("Type: '{0}'", type.FullName).AppendLine();
			}
			if (currentIndex != null)
			{
				stringBuilder.AppendFormat("Field Index: '{0}' (0 based)", currentIndex).AppendLine();
			}
			if (namedIndexes != null)
			{
				string text = Enumerable.SingleOrDefault<string>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany(namedIndexes, (KeyValuePair<string, List<int>> pair) => pair.Value, (KeyValuePair<string, List<int>> pair, int index) => new { pair, index }), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.index == currentIndex), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.pair.Key));
				if (text != null)
				{
					stringBuilder.AppendFormat("Field Name: '{0}'", text).AppendLine();
				}
			}
			if (currentRecord != null && currentIndex > -1 && currentIndex < currentRecord.Length && currentIndex.Value < currentRecord.Length)
			{
				string text2 = currentRecord[currentIndex.Value];
				stringBuilder.AppendFormat("Field Value: '{0}'", text2).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600025A RID: 602 RVA: 0x00002253 File Offset: 0x00000453
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600025B RID: 603 RVA: 0x0000809C File Offset: 0x0000629C
			internal bool <GetErrorMessage>b__2(<>f__AnonymousType0<KeyValuePair<string, List<int>>, int> <>h__TransparentIdentifier0)
			{
				return <>h__TransparentIdentifier0.index == this.currentIndex;
			}

			// Token: 0x04000089 RID: 137
			public int? currentIndex;
		}

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600025C RID: 604 RVA: 0x000080C8 File Offset: 0x000062C8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600025D RID: 605 RVA: 0x00002253 File Offset: 0x00000453
			public <>c()
			{
			}

			// Token: 0x0600025E RID: 606 RVA: 0x000080D4 File Offset: 0x000062D4
			internal IEnumerable<int> <GetErrorMessage>b__1_0(KeyValuePair<string, List<int>> pair)
			{
				return pair.Value;
			}

			// Token: 0x0600025F RID: 607 RVA: 0x000080DD File Offset: 0x000062DD
			internal <>f__AnonymousType0<KeyValuePair<string, List<int>>, int> <GetErrorMessage>b__1_1(KeyValuePair<string, List<int>> pair, int index)
			{
				return new { pair, index };
			}

			// Token: 0x06000260 RID: 608 RVA: 0x000080E8 File Offset: 0x000062E8
			internal string <GetErrorMessage>b__1_3(<>f__AnonymousType0<KeyValuePair<string, List<int>>, int> <>h__TransparentIdentifier0)
			{
				return <>h__TransparentIdentifier0.pair.Key;
			}

			// Token: 0x0400008A RID: 138
			public static readonly ExceptionHelper.<>c <>9 = new ExceptionHelper.<>c();

			// Token: 0x0400008B RID: 139
			public static Func<KeyValuePair<string, List<int>>, IEnumerable<int>> <>9__1_0;

			// Token: 0x0400008C RID: 140
			public static Func<KeyValuePair<string, List<int>>, int, <>f__AnonymousType0<KeyValuePair<string, List<int>>, int>> <>9__1_1;

			// Token: 0x0400008D RID: 141
			public static Func<<>f__AnonymousType0<KeyValuePair<string, List<int>>, int>, string> <>9__1_3;
		}
	}
}
