using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000906 RID: 2310
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodRental))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class MethodRental : _MethodRental
	{
		// Token: 0x060050C9 RID: 20681 RVA: 0x000025BE File Offset: 0x000007BE
		private MethodRental()
		{
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x000FE32C File Offset: 0x000FC52C
		[MonoTODO]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
		{
			if (methodSize <= 0 || methodSize >= 4128768)
			{
				throw new ArgumentException("Data size must be > 0 and < 0x3f0000", "methodSize");
			}
			if (cls == null)
			{
				throw new ArgumentNullException("cls");
			}
			if (cls is TypeBuilder && !((TypeBuilder)cls).is_created)
			{
				throw new NotSupportedException("Type '" + ((cls != null) ? cls.ToString() : null) + "' is not yet created.");
			}
			throw new NotImplementedException();
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003155 RID: 12629
		public const int JitImmediate = 1;

		// Token: 0x04003156 RID: 12630
		public const int JitOnDemand = 0;
	}
}
