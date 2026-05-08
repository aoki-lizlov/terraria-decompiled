using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000888 RID: 2184
	[CLSCompliant(false)]
	public sealed class Pointer : ISerializable
	{
		// Token: 0x0600494C RID: 18764 RVA: 0x000EEE39 File Offset: 0x000ED039
		private unsafe Pointer(void* ptr, Type ptrType)
		{
			this._ptr = ptr;
			this._ptrType = ptrType;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000EEE50 File Offset: 0x000ED050
		public unsafe static object Box(void* ptr, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsPointer)
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			if (!type.IsRuntimeImplemented())
			{
				throw new ArgumentException("Type must be a type provided by the runtime.", "ptr");
			}
			return new Pointer(ptr, type);
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x000EEEA8 File Offset: 0x000ED0A8
		public unsafe static void* Unbox(object ptr)
		{
			if (!(ptr is Pointer))
			{
				throw new ArgumentException("Type must be a Pointer.", "ptr");
			}
			return ((Pointer)ptr)._ptr;
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0003CB93 File Offset: 0x0003AD93
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x000EEECD File Offset: 0x000ED0CD
		internal Type GetPointerType()
		{
			return this._ptrType;
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x000EEED5 File Offset: 0x000ED0D5
		internal IntPtr GetPointerValue()
		{
			return (IntPtr)this._ptr;
		}

		// Token: 0x04002E8C RID: 11916
		private unsafe readonly void* _ptr;

		// Token: 0x04002E8D RID: 11917
		private readonly Type _ptrType;
	}
}
