using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000006 RID: 6
public class UTF8Marshaler : ICustomMarshaler
{
	// Token: 0x06000011 RID: 17 RVA: 0x00002050 File Offset: 0x00000250
	public IntPtr MarshalManagedToNative(object obj)
	{
		if (obj == null)
		{
			return IntPtr.Zero;
		}
		if (!(obj is string))
		{
			throw new MarshalDirectiveException("Invalid obj in UTF8Marshaler.");
		}
		byte[] bytes = Encoding.UTF8.GetBytes((string)obj);
		IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
		Marshal.Copy(bytes, 0, intPtr, bytes.Length);
		Marshal.WriteByte(intPtr + bytes.Length, 0);
		return intPtr;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000020B0 File Offset: 0x000002B0
	public object MarshalNativeToManaged(IntPtr data)
	{
		int num = 0;
		while (Marshal.ReadByte(data, num) != 0)
		{
			num++;
		}
		if (num == 0)
		{
			return string.Empty;
		}
		byte[] array = new byte[num];
		Marshal.Copy(data, array, 0, num);
		return Encoding.UTF8.GetString(array);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000020F2 File Offset: 0x000002F2
	public void CleanUpNativeData(IntPtr data)
	{
		Marshal.FreeHGlobal(data);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000020FA File Offset: 0x000002FA
	public void CleanUpManagedData(object obj)
	{
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000020FC File Offset: 0x000002FC
	public int GetNativeDataSize()
	{
		return -1;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000020FF File Offset: 0x000002FF
	public static ICustomMarshaler GetInstance(string cookie)
	{
		if (UTF8Marshaler.instance_ == null)
		{
			return UTF8Marshaler.instance_ = new UTF8Marshaler();
		}
		return UTF8Marshaler.instance_;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002119 File Offset: 0x00000319
	public UTF8Marshaler()
	{
	}

	// Token: 0x04000001 RID: 1
	private static UTF8Marshaler instance_;
}
