using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.IO.Enumeration
{
	// Token: 0x0200099C RID: 2460
	public class FileSystemEnumerable<TResult> : IEnumerable<TResult>, IEnumerable
	{
		// Token: 0x060059DB RID: 23003 RVA: 0x00130C2C File Offset: 0x0012EE2C
		public FileSystemEnumerable(string directory, FileSystemEnumerable<TResult>.FindTransform transform, EnumerationOptions options = null)
		{
			if (directory == null)
			{
				throw new ArgumentNullException("directory");
			}
			this._directory = directory;
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			this._transform = transform;
			this._options = options ?? EnumerationOptions.Default;
			this._enumerator = new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060059DC RID: 23004 RVA: 0x00130C87 File Offset: 0x0012EE87
		// (set) Token: 0x060059DD RID: 23005 RVA: 0x00130C8F File Offset: 0x0012EE8F
		public FileSystemEnumerable<TResult>.FindPredicate ShouldIncludePredicate
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldIncludePredicate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShouldIncludePredicate>k__BackingField = value;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x00130C98 File Offset: 0x0012EE98
		// (set) Token: 0x060059DF RID: 23007 RVA: 0x00130CA0 File Offset: 0x0012EEA0
		public FileSystemEnumerable<TResult>.FindPredicate ShouldRecursePredicate
		{
			[CompilerGenerated]
			get
			{
				return this.<ShouldRecursePredicate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShouldRecursePredicate>k__BackingField = value;
			}
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00130CA9 File Offset: 0x0012EEA9
		public IEnumerator<TResult> GetEnumerator()
		{
			return Interlocked.Exchange<FileSystemEnumerable<TResult>.DelegateEnumerator>(ref this._enumerator, null) ?? new FileSystemEnumerable<TResult>.DelegateEnumerator(this);
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00130CC1 File Offset: 0x0012EEC1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003596 RID: 13718
		private FileSystemEnumerable<TResult>.DelegateEnumerator _enumerator;

		// Token: 0x04003597 RID: 13719
		private readonly FileSystemEnumerable<TResult>.FindTransform _transform;

		// Token: 0x04003598 RID: 13720
		private readonly EnumerationOptions _options;

		// Token: 0x04003599 RID: 13721
		private readonly string _directory;

		// Token: 0x0400359A RID: 13722
		[CompilerGenerated]
		private FileSystemEnumerable<TResult>.FindPredicate <ShouldIncludePredicate>k__BackingField;

		// Token: 0x0400359B RID: 13723
		[CompilerGenerated]
		private FileSystemEnumerable<TResult>.FindPredicate <ShouldRecursePredicate>k__BackingField;

		// Token: 0x0200099D RID: 2461
		// (Invoke) Token: 0x060059E3 RID: 23011
		public delegate bool FindPredicate(ref FileSystemEntry entry);

		// Token: 0x0200099E RID: 2462
		// (Invoke) Token: 0x060059E7 RID: 23015
		public delegate TResult FindTransform(ref FileSystemEntry entry);

		// Token: 0x0200099F RID: 2463
		private sealed class DelegateEnumerator : FileSystemEnumerator<TResult>
		{
			// Token: 0x060059EA RID: 23018 RVA: 0x00130CC9 File Offset: 0x0012EEC9
			public DelegateEnumerator(FileSystemEnumerable<TResult> enumerable)
				: base(enumerable._directory, enumerable._options)
			{
				this._enumerable = enumerable;
			}

			// Token: 0x060059EB RID: 23019 RVA: 0x00130CE4 File Offset: 0x0012EEE4
			protected override TResult TransformEntry(ref FileSystemEntry entry)
			{
				return this._enumerable._transform(ref entry);
			}

			// Token: 0x060059EC RID: 23020 RVA: 0x00130CF7 File Offset: 0x0012EEF7
			protected override bool ShouldRecurseIntoEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldRecursePredicate = this._enumerable.ShouldRecursePredicate;
				return shouldRecursePredicate == null || shouldRecursePredicate(ref entry);
			}

			// Token: 0x060059ED RID: 23021 RVA: 0x00130D10 File Offset: 0x0012EF10
			protected override bool ShouldIncludeEntry(ref FileSystemEntry entry)
			{
				FileSystemEnumerable<TResult>.FindPredicate shouldIncludePredicate = this._enumerable.ShouldIncludePredicate;
				return shouldIncludePredicate == null || shouldIncludePredicate(ref entry);
			}

			// Token: 0x0400359C RID: 13724
			private readonly FileSystemEnumerable<TResult> _enumerable;
		}
	}
}
