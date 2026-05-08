using System;
using System.Runtime.CompilerServices;

namespace System.IO
{
	// Token: 0x0200094E RID: 2382
	public class EnumerationOptions
	{
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x060055E2 RID: 21986 RVA: 0x00121996 File Offset: 0x0011FB96
		internal static EnumerationOptions Compatible
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<Compatible>k__BackingField;
			}
		} = new EnumerationOptions
		{
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x0012199D File Offset: 0x0011FB9D
		private static EnumerationOptions CompatibleRecursive
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<CompatibleRecursive>k__BackingField;
			}
		} = new EnumerationOptions
		{
			RecurseSubdirectories = true,
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x001219A4 File Offset: 0x0011FBA4
		internal static EnumerationOptions Default
		{
			[CompilerGenerated]
			get
			{
				return EnumerationOptions.<Default>k__BackingField;
			}
		} = new EnumerationOptions();

		// Token: 0x060055E5 RID: 21989 RVA: 0x001219AB File Offset: 0x0011FBAB
		public EnumerationOptions()
		{
			this.IgnoreInaccessible = true;
			this.AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x001219C1 File Offset: 0x0011FBC1
		internal static EnumerationOptions FromSearchOption(SearchOption searchOption)
		{
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Enum value was out of legal range.");
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				return EnumerationOptions.Compatible;
			}
			return EnumerationOptions.CompatibleRecursive;
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x060055E7 RID: 21991 RVA: 0x001219E9 File Offset: 0x0011FBE9
		// (set) Token: 0x060055E8 RID: 21992 RVA: 0x001219F1 File Offset: 0x0011FBF1
		public bool RecurseSubdirectories
		{
			[CompilerGenerated]
			get
			{
				return this.<RecurseSubdirectories>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RecurseSubdirectories>k__BackingField = value;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x001219FA File Offset: 0x0011FBFA
		// (set) Token: 0x060055EA RID: 21994 RVA: 0x00121A02 File Offset: 0x0011FC02
		public bool IgnoreInaccessible
		{
			[CompilerGenerated]
			get
			{
				return this.<IgnoreInaccessible>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IgnoreInaccessible>k__BackingField = value;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x00121A0B File Offset: 0x0011FC0B
		// (set) Token: 0x060055EC RID: 21996 RVA: 0x00121A13 File Offset: 0x0011FC13
		public int BufferSize
		{
			[CompilerGenerated]
			get
			{
				return this.<BufferSize>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BufferSize>k__BackingField = value;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x060055ED RID: 21997 RVA: 0x00121A1C File Offset: 0x0011FC1C
		// (set) Token: 0x060055EE RID: 21998 RVA: 0x00121A24 File Offset: 0x0011FC24
		public FileAttributes AttributesToSkip
		{
			[CompilerGenerated]
			get
			{
				return this.<AttributesToSkip>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AttributesToSkip>k__BackingField = value;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00121A2D File Offset: 0x0011FC2D
		// (set) Token: 0x060055F0 RID: 22000 RVA: 0x00121A35 File Offset: 0x0011FC35
		public MatchType MatchType
		{
			[CompilerGenerated]
			get
			{
				return this.<MatchType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MatchType>k__BackingField = value;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060055F1 RID: 22001 RVA: 0x00121A3E File Offset: 0x0011FC3E
		// (set) Token: 0x060055F2 RID: 22002 RVA: 0x00121A46 File Offset: 0x0011FC46
		public MatchCasing MatchCasing
		{
			[CompilerGenerated]
			get
			{
				return this.<MatchCasing>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MatchCasing>k__BackingField = value;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x00121A4F File Offset: 0x0011FC4F
		// (set) Token: 0x060055F4 RID: 22004 RVA: 0x00121A57 File Offset: 0x0011FC57
		public bool ReturnSpecialDirectories
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnSpecialDirectories>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ReturnSpecialDirectories>k__BackingField = value;
			}
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00121A60 File Offset: 0x0011FC60
		// Note: this type is marked as 'beforefieldinit'.
		static EnumerationOptions()
		{
		}

		// Token: 0x04003401 RID: 13313
		[CompilerGenerated]
		private static readonly EnumerationOptions <Compatible>k__BackingField;

		// Token: 0x04003402 RID: 13314
		[CompilerGenerated]
		private static readonly EnumerationOptions <CompatibleRecursive>k__BackingField;

		// Token: 0x04003403 RID: 13315
		[CompilerGenerated]
		private static readonly EnumerationOptions <Default>k__BackingField;

		// Token: 0x04003404 RID: 13316
		[CompilerGenerated]
		private bool <RecurseSubdirectories>k__BackingField;

		// Token: 0x04003405 RID: 13317
		[CompilerGenerated]
		private bool <IgnoreInaccessible>k__BackingField;

		// Token: 0x04003406 RID: 13318
		[CompilerGenerated]
		private int <BufferSize>k__BackingField;

		// Token: 0x04003407 RID: 13319
		[CompilerGenerated]
		private FileAttributes <AttributesToSkip>k__BackingField;

		// Token: 0x04003408 RID: 13320
		[CompilerGenerated]
		private MatchType <MatchType>k__BackingField;

		// Token: 0x04003409 RID: 13321
		[CompilerGenerated]
		private MatchCasing <MatchCasing>k__BackingField;

		// Token: 0x0400340A RID: 13322
		[CompilerGenerated]
		private bool <ReturnSpecialDirectories>k__BackingField;
	}
}
