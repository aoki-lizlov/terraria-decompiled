using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000212 RID: 530
	[ComVisible(true)]
	public struct ModuleHandle
	{
		// Token: 0x060019F9 RID: 6649 RVA: 0x00060958 File Offset: 0x0005EB58
		internal ModuleHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00060961 File Offset: 0x0005EB61
		internal IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00060969 File Offset: 0x0005EB69
		public int MDStreamVersion
		{
			get
			{
				if (this.value == IntPtr.Zero)
				{
					throw new ArgumentNullException(string.Empty, "Invalid handle");
				}
				return RuntimeModule.GetMDStreamVersion(this.value);
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00060998 File Offset: 0x0005EB98
		internal void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			RuntimeModule.GetPEKind(this.value, out peKind, out machine);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000609C9 File Offset: 0x0005EBC9
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken, null, null);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000609D4 File Offset: 0x0005EBD4
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken, null, null);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000609DF File Offset: 0x0005EBDF
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken, null, null);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000609EC File Offset: 0x0005EBEC
		private IntPtr[] ptrs_from_handles(RuntimeTypeHandle[] handles)
		{
			if (handles == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[handles.Length];
			for (int i = 0; i < handles.Length; i++)
			{
				array[i] = handles[i].Value;
			}
			return array;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00060A28 File Offset: 0x0005EC28
		public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveTypeToken(this.value, typeToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new TypeLoadException(string.Format("Could not load type '0x{0:x}' from assembly '0x{1:x}'", typeToken, this.value.ToInt64()));
			}
			return new RuntimeTypeHandle(intPtr);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00060AB0 File Offset: 0x0005ECB0
		public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveMethodToken(this.value, methodToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load method '0x{0:x}' from assembly '0x{1:x}'", methodToken, this.value.ToInt64()));
			}
			return new RuntimeMethodHandle(intPtr);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00060B38 File Offset: 0x0005ED38
		public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
		{
			if (this.value == IntPtr.Zero)
			{
				throw new ArgumentNullException(string.Empty, "Invalid handle");
			}
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = RuntimeModule.ResolveFieldToken(this.value, fieldToken, this.ptrs_from_handles(typeInstantiationContext), this.ptrs_from_handles(methodInstantiationContext), out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception(string.Format("Could not load field '0x{0:x}' from assembly '0x{1:x}'", fieldToken, this.value.ToInt64()));
			}
			return new RuntimeFieldHandle(intPtr);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00060BBD File Offset: 0x0005EDBD
		public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
		{
			return this.ResolveFieldHandle(fieldToken);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00060BC6 File Offset: 0x0005EDC6
		public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
		{
			return this.ResolveMethodHandle(methodToken);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00060BCF File Offset: 0x0005EDCF
		public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
		{
			return this.ResolveTypeHandle(typeToken);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00060BD8 File Offset: 0x0005EDD8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((ModuleHandle)obj).Value;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00060C20 File Offset: 0x0005EE20
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(ModuleHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00060C34 File Offset: 0x0005EE34
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00060C41 File Offset: 0x0005EE41
		public static bool operator ==(ModuleHandle left, ModuleHandle right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00060C54 File Offset: 0x0005EE54
		public static bool operator !=(ModuleHandle left, ModuleHandle right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00060C6A File Offset: 0x0005EE6A
		// Note: this type is marked as 'beforefieldinit'.
		static ModuleHandle()
		{
		}

		// Token: 0x04001604 RID: 5636
		private IntPtr value;

		// Token: 0x04001605 RID: 5637
		public static readonly ModuleHandle EmptyHandle = new ModuleHandle(IntPtr.Zero);
	}
}
