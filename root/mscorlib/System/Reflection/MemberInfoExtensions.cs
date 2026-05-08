using System;

namespace System.Reflection
{
	// Token: 0x020008A7 RID: 2215
	public static class MemberInfoExtensions
	{
		// Token: 0x06004ADE RID: 19166 RVA: 0x000F01F0 File Offset: 0x000EE3F0
		public static bool HasMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			bool flag;
			try
			{
				flag = MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member) != 0;
			}
			catch (InvalidOperationException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x000F022C File Offset: 0x000EE42C
		public static int GetMetadataToken(this MemberInfo member)
		{
			Requires.NotNull(member, "member");
			int metadataTokenOrZeroOrThrow = MemberInfoExtensions.GetMetadataTokenOrZeroOrThrow(member);
			if (metadataTokenOrZeroOrThrow == 0)
			{
				throw new InvalidOperationException("There is no metadata token available for the given member.");
			}
			return metadataTokenOrZeroOrThrow;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x000F0250 File Offset: 0x000EE450
		private static int GetMetadataTokenOrZeroOrThrow(MemberInfo member)
		{
			int metadataToken = member.MetadataToken;
			if ((metadataToken & 16777215) == 0)
			{
				return 0;
			}
			return metadataToken;
		}
	}
}
