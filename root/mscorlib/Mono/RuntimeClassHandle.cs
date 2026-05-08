using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000029 RID: 41
	internal struct RuntimeClassHandle
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000040A4 File Offset: 0x000022A4
		internal unsafe RuntimeClassHandle(RuntimeStructs.MonoClass* value)
		{
			this.value = value;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000040AD File Offset: 0x000022AD
		internal unsafe RuntimeClassHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.MonoClass*)(void*)ptr;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000040BB File Offset: 0x000022BB
		internal unsafe RuntimeStructs.MonoClass* Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040C4 File Offset: 0x000022C4
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeClassHandle)obj).Value;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000410C File Offset: 0x0000230C
		public unsafe override int GetHashCode()
		{
			return ((IntPtr)((void*)this.value)).GetHashCode();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000412C File Offset: 0x0000232C
		public bool Equals(RuntimeClassHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004140 File Offset: 0x00002340
		public static bool operator ==(RuntimeClassHandle left, object right)
		{
			if (right != null && right is RuntimeClassHandle)
			{
				RuntimeClassHandle runtimeClassHandle = (RuntimeClassHandle)right;
				return left.Equals(runtimeClassHandle);
			}
			return false;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004169 File Offset: 0x00002369
		public static bool operator !=(RuntimeClassHandle left, object right)
		{
			return !(left == right);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004178 File Offset: 0x00002378
		public static bool operator ==(object left, RuntimeClassHandle right)
		{
			return left != null && left is RuntimeClassHandle && ((RuntimeClassHandle)left).Equals(right);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041A1 File Offset: 0x000023A1
		public static bool operator !=(object left, RuntimeClassHandle right)
		{
			return !(left == right);
		}

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr GetTypeFromClass(RuntimeStructs.MonoClass* klass);

		// Token: 0x060000C4 RID: 196 RVA: 0x000041AD File Offset: 0x000023AD
		internal RuntimeTypeHandle GetTypeHandle()
		{
			return new RuntimeTypeHandle(RuntimeClassHandle.GetTypeFromClass(this.value));
		}

		// Token: 0x04000CDE RID: 3294
		private unsafe RuntimeStructs.MonoClass* value;
	}
}
