using System;

namespace System.Reflection
{
	// Token: 0x02000894 RID: 2196
	internal sealed class SignatureGenericMethodParameterType : SignatureGenericParameterType
	{
		// Token: 0x060049A5 RID: 18853 RVA: 0x000EF2B4 File Offset: 0x000ED4B4
		internal SignatureGenericMethodParameterType(int position)
			: base(position)
		{
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060049A6 RID: 18854 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060049A7 RID: 18855 RVA: 0x00003FB7 File Offset: 0x000021B7
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060049A8 RID: 18856 RVA: 0x000EF2C0 File Offset: 0x000ED4C0
		public sealed override string Name
		{
			get
			{
				return "!!" + this.GenericParameterPosition.ToString();
			}
		}
	}
}
