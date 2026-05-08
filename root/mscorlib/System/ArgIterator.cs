using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001FF RID: 511
	[StructLayout(LayoutKind.Auto)]
	public struct ArgIterator
	{
		// Token: 0x060018A7 RID: 6311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Setup(IntPtr argsp, IntPtr start);

		// Token: 0x060018A8 RID: 6312 RVA: 0x0005EA84 File Offset: 0x0005CC84
		public ArgIterator(RuntimeArgumentHandle arglist)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			if (arglist.args == IntPtr.Zero)
			{
				throw new PlatformNotSupportedException();
			}
			this.Setup(arglist.args, IntPtr.Zero);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0005EAE4 File Offset: 0x0005CCE4
		[CLSCompliant(false)]
		public unsafe ArgIterator(RuntimeArgumentHandle arglist, void* ptr)
		{
			this.sig = IntPtr.Zero;
			this.args = IntPtr.Zero;
			this.next_arg = (this.num_args = 0);
			if (arglist.args == IntPtr.Zero)
			{
				throw new PlatformNotSupportedException();
			}
			this.Setup(arglist.args, (IntPtr)ptr);
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0005EB43 File Offset: 0x0005CD43
		public void End()
		{
			this.next_arg = this.num_args;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0005EB51 File Offset: 0x0005CD51
		public override bool Equals(object o)
		{
			throw new NotSupportedException("ArgIterator does not support Equals.");
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0005EB5D File Offset: 0x0005CD5D
		public override int GetHashCode()
		{
			return this.sig.GetHashCode();
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			TypedReference typedReference = default(TypedReference);
			this.IntGetNextArg((void*)(&typedReference));
			return typedReference;
		}

		// Token: 0x060018AE RID: 6318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void IntGetNextArg(void* res);

		// Token: 0x060018AF RID: 6319 RVA: 0x0005EBA4 File Offset: 0x0005CDA4
		[CLSCompliant(false)]
		public unsafe TypedReference GetNextArg(RuntimeTypeHandle rth)
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			TypedReference typedReference = default(TypedReference);
			this.IntGetNextArgWithType((void*)(&typedReference), rth.Value);
			return typedReference;
		}

		// Token: 0x060018B0 RID: 6320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void IntGetNextArgWithType(void* res, IntPtr rth);

		// Token: 0x060018B1 RID: 6321 RVA: 0x0005EBE3 File Offset: 0x0005CDE3
		public RuntimeTypeHandle GetNextArgType()
		{
			if (this.num_args == this.next_arg)
			{
				throw new InvalidOperationException("Invalid iterator position.");
			}
			return new RuntimeTypeHandle(this.IntGetNextArgType());
		}

		// Token: 0x060018B2 RID: 6322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr IntGetNextArgType();

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005EC09 File Offset: 0x0005CE09
		public int GetRemainingCount()
		{
			return this.num_args - this.next_arg;
		}

		// Token: 0x040015BF RID: 5567
		private IntPtr sig;

		// Token: 0x040015C0 RID: 5568
		private IntPtr args;

		// Token: 0x040015C1 RID: 5569
		private int next_arg;

		// Token: 0x040015C2 RID: 5570
		private int num_args;
	}
}
