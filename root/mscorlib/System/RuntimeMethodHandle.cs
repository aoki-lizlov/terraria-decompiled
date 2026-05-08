using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x02000221 RID: 545
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001AD5 RID: 6869 RVA: 0x000650D2 File Offset: 0x000632D2
		internal RuntimeMethodHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000650DC File Offset: 0x000632DC
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)info.GetValue("MethodObj", typeof(RuntimeMethodInfo));
			this.value = runtimeMethodInfo.MethodHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00065143 File Offset: 0x00063343
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006514C File Offset: 0x0006334C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("MethodObj", (RuntimeMethodInfo)MethodBase.GetMethodFromHandle(this), typeof(RuntimeMethodInfo));
		}

		// Token: 0x06001AD9 RID: 6873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointer(IntPtr m);

		// Token: 0x06001ADA RID: 6874 RVA: 0x000651A9 File Offset: 0x000633A9
		public IntPtr GetFunctionPointer()
		{
			return RuntimeMethodHandle.GetFunctionPointer(this.value);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000651B8 File Offset: 0x000633B8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeMethodHandle)obj).Value;
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00065200 File Offset: 0x00063400
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00065214 File Offset: 0x00063414
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00065221 File Offset: 0x00063421
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006522B File Offset: 0x0006342B
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00065238 File Offset: 0x00063438
		internal static string ConstructInstantiation(RuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type[] genericArguments = method.GetGenericArguments();
			stringBuilder.Append("[");
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(genericArguments[i].Name);
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0006529D File Offset: 0x0006349D
		internal bool IsNullHandle()
		{
			return this.value == IntPtr.Zero;
		}

		// Token: 0x0400165E RID: 5726
		private IntPtr value;
	}
}
