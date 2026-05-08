using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000718 RID: 1816
	public static class Marshal
	{
		// Token: 0x060040E3 RID: 16611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddRefInternal(IntPtr pUnk);

		// Token: 0x060040E4 RID: 16612 RVA: 0x000E1A22 File Offset: 0x000DFC22
		public static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.AddRefInternal(pUnk);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool AreComObjectsAvailableForCleanup()
		{
			return false;
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000E1A42 File Offset: 0x000DFC42
		public static void CleanupUnusedObjectsInCurrentContext()
		{
			if (Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060040E7 RID: 16615
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocCoTaskMem(int cb);

		// Token: 0x060040E8 RID: 16616
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr AllocCoTaskMemSize(UIntPtr sizet);

		// Token: 0x060040E9 RID: 16617
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocHGlobal(IntPtr cb);

		// Token: 0x060040EA RID: 16618 RVA: 0x000E1A51 File Offset: 0x000DFC51
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000174FB File Offset: 0x000156FB
		public static object BindToMoniker(string monikerName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000174FB File Offset: 0x000156FB
		public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000E1A5E File Offset: 0x000DFC5E
		internal static void copy_to_unmanaged(Array source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
		}

		// Token: 0x060040EE RID: 16622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void copy_to_unmanaged_fixed(Array source, int startIndex, IntPtr destination, int length, void* fixed_source_element);

		// Token: 0x060040EF RID: 16623 RVA: 0x000E1A6B File Offset: 0x000DFC6B
		private static bool skip_fixed(Array array, int startIndex)
		{
			return startIndex < 0 || startIndex >= array.Length;
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000E1A80 File Offset: 0x000DFC80
		internal unsafe static void copy_to_unmanaged(byte[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
				return;
			}
			fixed (byte* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000E1ABC File Offset: 0x000DFCBC
		internal unsafe static void copy_to_unmanaged(char[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
				return;
			}
			fixed (char* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000E1AF8 File Offset: 0x000DFCF8
		public unsafe static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (byte* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000E1B30 File Offset: 0x000DFD30
		public unsafe static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (char* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000E1B68 File Offset: 0x000DFD68
		public unsafe static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (short* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000E1BA0 File Offset: 0x000DFDA0
		public unsafe static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (int* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000E1BD8 File Offset: 0x000DFDD8
		public unsafe static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (long* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000E1C10 File Offset: 0x000DFE10
		public unsafe static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (float* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000E1C48 File Offset: 0x000DFE48
		public unsafe static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (double* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000E1C80 File Offset: 0x000DFE80
		public unsafe static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (IntPtr* ptr = &source[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000E1CB8 File Offset: 0x000DFEB8
		internal static void copy_from_unmanaged(IntPtr source, int startIndex, Array destination, int length)
		{
			Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, null);
		}

		// Token: 0x060040FB RID: 16635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void copy_from_unmanaged_fixed(IntPtr source, int startIndex, Array destination, int length, void* fixed_destination_element);

		// Token: 0x060040FC RID: 16636 RVA: 0x000E1CC8 File Offset: 0x000DFEC8
		public unsafe static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (byte* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000E1D00 File Offset: 0x000DFF00
		public unsafe static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (char* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000E1D38 File Offset: 0x000DFF38
		public unsafe static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (short* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000E1D70 File Offset: 0x000DFF70
		public unsafe static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (int* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x000E1DA8 File Offset: 0x000DFFA8
		public unsafe static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (long* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000E1DE0 File Offset: 0x000DFFE0
		public unsafe static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (float* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000E1E18 File Offset: 0x000E0018
		public unsafe static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (double* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000E1E50 File Offset: 0x000E0050
		public unsafe static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (IntPtr* ptr = &destination[startIndex])
			{
				void* ptr2 = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, ptr2);
			}
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x000E1E88 File Offset: 0x000E0088
		public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
		{
			return Marshal.CreateAggregatedObject(pOuter, o);
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000E1E98 File Offset: 0x000E0098
		public static object CreateWrapperOfType(object o, Type t)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException("o must derive from __ComObject", "o");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			foreach (Type type in o.GetType().GetInterfaces())
			{
				if (type.IsImport && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
			}
			return ComInteropProxy.GetProxy(_ComObject.IUnknown, t).GetTransparentProxy();
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000E1F23 File Offset: 0x000E0123
		public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
		{
			return (TWrapper)((object)Marshal.CreateWrapperOfType(o, typeof(TWrapper)));
		}

		// Token: 0x06004108 RID: 16648
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		// Token: 0x06004109 RID: 16649 RVA: 0x000E1F3F File Offset: 0x000E013F
		public static void DestroyStructure<T>(IntPtr ptr)
		{
			Marshal.DestroyStructure(ptr, typeof(T));
		}

		// Token: 0x0600410A RID: 16650
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeBSTR(IntPtr ptr);

		// Token: 0x0600410B RID: 16651
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeCoTaskMem(IntPtr ptr);

		// Token: 0x0600410C RID: 16652
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeHGlobal(IntPtr hglobal);

		// Token: 0x0600410D RID: 16653 RVA: 0x000E1F54 File Offset: 0x000E0154
		private static void ClearBSTR(IntPtr ptr)
		{
			int num = Marshal.ReadInt32(ptr, -4);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteByte(ptr, i, 0);
			}
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000E1F7E File Offset: 0x000E017E
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Marshal.ClearBSTR(s);
			Marshal.FreeBSTR(s);
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x000E1F8C File Offset: 0x000E018C
		private static void ClearAnsi(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				Marshal.WriteByte(ptr, num, 0);
				num++;
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x000E1FB4 File Offset: 0x000E01B4
		private static void ClearUnicode(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadInt16(ptr, num) != 0)
			{
				Marshal.WriteInt16(ptr, num, 0);
				num += 2;
			}
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x000E1FDA File Offset: 0x000E01DA
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x000E1FE8 File Offset: 0x000E01E8
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x000E1FDA File Offset: 0x000E01DA
		public static void ZeroFreeCoTaskMemUTF8(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x000E1FF6 File Offset: 0x000E01F6
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x000E2004 File Offset: 0x000E0204
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeHGlobal(s);
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x000E2012 File Offset: 0x000E0212
		public static Guid GenerateGuidForType(Type type)
		{
			return type.GUID;
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x000E201C File Offset: 0x000E021C
		public static string GenerateProgIdForType(Type type)
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(type))
			{
				if (customAttributeData.Constructor.DeclaringType.Name == "ProgIdAttribute")
				{
					IList<CustomAttributeTypedArgument> constructorArguments = customAttributeData.ConstructorArguments;
					string text = customAttributeData.ConstructorArguments[0].Value as string;
					if (text == null)
					{
						text = string.Empty;
					}
					return text;
				}
			}
			return type.FullName;
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x000174FB File Offset: 0x000156FB
		public static object GetActiveObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004119 RID: 16665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCCW(object o, Type T);

		// Token: 0x0600411A RID: 16666 RVA: 0x000E20B8 File Offset: 0x000E02B8
		private static IntPtr GetComInterfaceForObjectInternal(object o, Type T)
		{
			if (Marshal.IsComObject(o))
			{
				return ((__ComObject)o).GetInterface(T);
			}
			return Marshal.GetCCW(o, T);
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000E20D6 File Offset: 0x000E02D6
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			IntPtr comInterfaceForObjectInternal = Marshal.GetComInterfaceForObjectInternal(o, T);
			Marshal.AddRef(comInterfaceForObjectInternal);
			return comInterfaceForObjectInternal;
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000E20E6 File Offset: 0x000E02E6
		public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
		{
			return Marshal.GetComInterfaceForObject(o, typeof(T));
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000E20FD File Offset: 0x000E02FD
		public static object GetComObjectData(object obj, object key)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x06004120 RID: 16672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComSlotForMethodInfoInternal(MemberInfo m);

		// Token: 0x06004121 RID: 16673 RVA: 0x000E210C File Offset: 0x000E030C
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is MethodInfo))
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			return Marshal.GetComSlotForMethodInfoInternal(m);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x000174FB File Offset: 0x000156FB
		public static int GetEndComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x000174FB File Offset: 0x000156FB
		[ComVisible(true)]
		public static IntPtr GetExceptionPointers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x000E2168 File Offset: 0x000E0368
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeModule runtimeModule = m as RuntimeModule;
			if (runtimeModule != null)
			{
				return RuntimeModule.GetHINSTANCE(runtimeModule.MonoModule);
			}
			return (IntPtr)(-1);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static int GetExceptionCode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x000E21A8 File Offset: 0x000E03A8
		public static int GetHRForException(Exception e)
		{
			if (e == null)
			{
				return 0;
			}
			ManagedErrorInfo managedErrorInfo = new ManagedErrorInfo(e);
			Marshal.SetErrorInfo(0, managedErrorInfo);
			return e._HResult;
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x000174FB File Offset: 0x000156FB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004128 RID: 16680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectInternal(object o);

		// Token: 0x06004129 RID: 16681 RVA: 0x000E21CF File Offset: 0x000E03CF
		public static IntPtr GetIDispatchForObject(object o)
		{
			IntPtr idispatchForObjectInternal = Marshal.GetIDispatchForObjectInternal(o);
			Marshal.AddRef(idispatchForObjectInternal);
			return idispatchForObjectInternal;
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr GetITypeInfoForType(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600412C RID: 16684 RVA: 0x000174FB File Offset: 0x000156FB
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600412E RID: 16686 RVA: 0x000174FB File Offset: 0x000156FB
		public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600412F RID: 16687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectInternal(object o);

		// Token: 0x06004130 RID: 16688 RVA: 0x000E21DE File Offset: 0x000E03DE
		public static IntPtr GetIUnknownForObject(object o)
		{
			IntPtr iunknownForObjectInternal = Marshal.GetIUnknownForObjectInternal(o);
			Marshal.AddRef(iunknownForObjectInternal);
			return iunknownForObjectInternal;
		}

		// Token: 0x06004131 RID: 16689 RVA: 0x000E21F0 File Offset: 0x000E03F0
		public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
		{
			Variant variant = default(Variant);
			variant.SetValue(obj);
			Marshal.StructureToPtr<Variant>(variant, pDstNativeVariant, false);
		}

		// Token: 0x06004132 RID: 16690 RVA: 0x000E2215 File Offset: 0x000E0415
		public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
		{
			Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
		}

		// Token: 0x06004133 RID: 16691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetObjectForCCW(IntPtr pUnk);

		// Token: 0x06004134 RID: 16692 RVA: 0x000E2224 File Offset: 0x000E0424
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			object obj = Marshal.GetObjectForCCW(pUnk);
			if (obj == null)
			{
				obj = ComInteropProxy.GetProxy(pUnk, typeof(__ComObject)).GetTransparentProxy();
			}
			return obj;
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x000E2254 File Offset: 0x000E0454
		public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
		{
			return ((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue();
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000E2280 File Offset: 0x000E0480
		public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
		{
			return (T)((object)((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue());
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x000E22B0 File Offset: 0x000E04B0
		public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			object[] array = new object[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x000E230C File Offset: 0x000E050C
		public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			T[] array = new T[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant<T>((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x000174FB File Offset: 0x000156FB
		public static int GetStartComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete("This method has been deprecated")]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x000E236C File Offset: 0x000E056C
		public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
		{
			__ComObject _ComObject = (__ComObject)new ComInteropProxy(pUnk, t).GetTransparentProxy();
			foreach (Type type in t.GetInterfaces())
			{
				if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.Import && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					return null;
				}
			}
			return _ComObject;
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x000174FB File Offset: 0x000156FB
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x000174FB File Offset: 0x000156FB
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x000174FB File Offset: 0x000156FB
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x000174FB File Offset: 0x000156FB
		public static int GetTypeLibLcid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x000174FB File Offset: 0x000156FB
		public static string GetTypeLibName(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x000174FB File Offset: 0x000156FB
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x000174FB File Offset: 0x000156FB
		public static bool IsTypeVisibleFromCom(Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x000174FB File Offset: 0x000156FB
		public static int NumParamBytes(MethodInfo m)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x0003CB93 File Offset: 0x0003AD93
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600414C RID: 16716
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		// Token: 0x0600414D RID: 16717
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		// Token: 0x0600414E RID: 16718
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OffsetOf(Type t, string fieldName);

		// Token: 0x0600414F RID: 16719 RVA: 0x000E23CD File Offset: 0x000E05CD
		public static IntPtr OffsetOf<T>(string fieldName)
		{
			return Marshal.OffsetOf(typeof(T), fieldName);
		}

		// Token: 0x06004150 RID: 16720
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Prelink(MethodInfo m);

		// Token: 0x06004151 RID: 16721
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrelinkAll(Type c);

		// Token: 0x06004152 RID: 16722
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr);

		// Token: 0x06004153 RID: 16723
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr, int len);

		// Token: 0x06004154 RID: 16724 RVA: 0x000E23DF File Offset: 0x000E05DF
		public static string PtrToStringUTF8(IntPtr ptr)
		{
			return Marshal.PtrToStringAnsi(ptr);
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000E23E7 File Offset: 0x000E05E7
		public static string PtrToStringUTF8(IntPtr ptr, int byteLen)
		{
			return Marshal.PtrToStringAnsi(ptr, byteLen);
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x000E23F0 File Offset: 0x000E05F0
		public static string PtrToStringAuto(IntPtr ptr)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr);
			}
			return Marshal.PtrToStringUni(ptr);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x000E2407 File Offset: 0x000E0607
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr, len);
			}
			return Marshal.PtrToStringUni(ptr, len);
		}

		// Token: 0x06004158 RID: 16728
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr);

		// Token: 0x06004159 RID: 16729
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr, int len);

		// Token: 0x0600415A RID: 16730
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringBSTR(IntPtr ptr);

		// Token: 0x0600415B RID: 16731
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PtrToStructure(IntPtr ptr, object structure);

		// Token: 0x0600415C RID: 16732
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object PtrToStructure(IntPtr ptr, Type structureType);

		// Token: 0x0600415D RID: 16733 RVA: 0x000E2420 File Offset: 0x000E0620
		public static void PtrToStructure<T>(IntPtr ptr, T structure)
		{
			Marshal.PtrToStructure(ptr, structure);
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x000E242E File Offset: 0x000E062E
		public static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x0600415F RID: 16735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryInterfaceInternal(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		// Token: 0x06004160 RID: 16736 RVA: 0x000E2445 File Offset: 0x000E0645
		public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.QueryInterfaceInternal(pUnk, ref iid, out ppv);
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x000E2467 File Offset: 0x000E0667
		public unsafe static byte ReadByte(IntPtr ptr)
		{
			return *(byte*)(void*)ptr;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x000E2470 File Offset: 0x000E0670
		public unsafe static byte ReadByte(IntPtr ptr, int ofs)
		{
			return ((byte*)(void*)ptr)[ofs];
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x000E247C File Offset: 0x000E067C
		public unsafe static short ReadInt16(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short num;
			Buffer.Memcpy((byte*)(&num), (byte*)(void*)ptr, 2);
			return num;
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000E24AC File Offset: 0x000E06AC
		public unsafe static short ReadInt16(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short num;
			Buffer.Memcpy((byte*)(&num), ptr2, 2);
			return num;
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x000E24D8 File Offset: 0x000E06D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 3U) == 0U)
			{
				return *(int*)ptr2;
			}
			int num;
			Buffer.Memcpy((byte*)(&num), ptr2, 4);
			return num;
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000E2500 File Offset: 0x000E0700
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 3) == 0)
			{
				return *(int*)ptr2;
			}
			int num;
			Buffer.Memcpy((byte*)(&num), ptr2, 4);
			return num;
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000174FB File Offset: 0x000156FB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		public static int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x000E252C File Offset: 0x000E072C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static long ReadInt64(IntPtr ptr)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)(void*)ptr;
			}
			long num;
			Buffer.Memcpy((byte*)(&num), ptr2, 8);
			return num;
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x000E255C File Offset: 0x000E075C
		public unsafe static long ReadInt64(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)ptr2;
			}
			long num;
			Buffer.Memcpy((byte*)(&num), ptr2, 8);
			return num;
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x000174FB File Offset: 0x000156FB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000E2586 File Offset: 0x000E0786
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr);
			}
			return (IntPtr)Marshal.ReadInt64(ptr);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x000E25A7 File Offset: 0x000E07A7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr, ofs);
			}
			return (IntPtr)Marshal.ReadInt64(ptr, ofs);
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000174FB File Offset: 0x000156FB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004170 RID: 16752
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocCoTaskMem(IntPtr pv, int cb);

		// Token: 0x06004171 RID: 16753
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb);

		// Token: 0x06004172 RID: 16754
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseInternal(IntPtr pUnk);

		// Token: 0x06004173 RID: 16755 RVA: 0x000E25CA File Offset: 0x000E07CA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.ReleaseInternal(pUnk);
		}

		// Token: 0x06004174 RID: 16756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseComObjectInternal(object co);

		// Token: 0x06004175 RID: 16757 RVA: 0x000E25EA File Offset: 0x000E07EA
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentException("Value cannot be null.", "o");
			}
			if (!Marshal.IsComObject(o))
			{
				throw new ArgumentException("Value must be a Com object.", "o");
			}
			return Marshal.ReleaseComObjectInternal(o);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x000174FB File Offset: 0x000156FB
		[Obsolete]
		public static void ReleaseThreadCache()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x000E20FD File Offset: 0x000E02FD
		public static bool SetComObjectData(object obj, object key, object data)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x000E261D File Offset: 0x000E081D
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		// Token: 0x06004179 RID: 16761
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type t);

		// Token: 0x0600417A RID: 16762 RVA: 0x000E262A File Offset: 0x000E082A
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000E263B File Offset: 0x000E083B
		public static int SizeOf<T>(T structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x000E264F File Offset: 0x000E084F
		internal static uint SizeOfType(Type type)
		{
			return (uint)Marshal.SizeOf(type);
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x000E2658 File Offset: 0x000E0858
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = Marshal.SizeOfType(typeof(T));
			if (num == 1U || num == 2U)
			{
				return num;
			}
			if (IntPtr.Size == 8 && num == 4U)
			{
				return num;
			}
			return (num + 3U) & 4294967292U;
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000E2694 File Offset: 0x000E0894
		public unsafe static IntPtr StringToBSTR(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.BufferToBSTR(ptr, s.Length);
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x000E26C6 File Offset: 0x000E08C6
		public static IntPtr StringToCoTaskMemAnsi(string s)
		{
			return Marshal.StringToAllocatedMemoryUTF8(s);
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x000E26CE File Offset: 0x000E08CE
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToCoTaskMemAnsi(s);
			}
			return Marshal.StringToCoTaskMemUni(s);
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x000E26E8 File Offset: 0x000E08E8
		public static IntPtr StringToCoTaskMemUni(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * 2);
			char[] array = new char[num];
			s.CopyTo(0, array, 0, s.Length);
			array[s.Length] = '\0';
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		// Token: 0x06004182 RID: 16770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr StringToHGlobalAnsi(char* s, int length);

		// Token: 0x06004183 RID: 16771 RVA: 0x000E2730 File Offset: 0x000E0930
		public unsafe static IntPtr StringToHGlobalAnsi(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.StringToHGlobalAnsi(ptr, (s != null) ? s.Length : 0);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x000E2760 File Offset: 0x000E0960
		public unsafe static IntPtr StringToAllocatedMemoryUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 3;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocCoTaskMemSize(new UIntPtr((uint)(num + 1)));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			byte* ptr = (byte*)(void*)intPtr;
			fixed (string text = s)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				int bytes = Encoding.UTF8.GetBytes(ptr2, s.Length, ptr, num);
				ptr[bytes] = 0;
			}
			return intPtr;
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x000E27E9 File Offset: 0x000E09E9
		public static IntPtr StringToHGlobalAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToHGlobalAnsi(s);
			}
			return Marshal.StringToHGlobalUni(s);
		}

		// Token: 0x06004186 RID: 16774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr StringToHGlobalUni(char* s, int length);

		// Token: 0x06004187 RID: 16775 RVA: 0x000E2800 File Offset: 0x000E0A00
		public unsafe static IntPtr StringToHGlobalUni(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.StringToHGlobalUni(ptr, (s != null) ? s.Length : 0);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x000E2830 File Offset: 0x000E0A30
		public unsafe static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			byte[] buffer = s.GetBuffer();
			int length = s.Length;
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < buffer.Length; i += 2)
				{
					byte b = buffer[i];
					buffer[i] = buffer[i + 1];
					buffer[i + 1] = b;
				}
			}
			byte[] array;
			byte* ptr;
			if ((array = buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Marshal.BufferToBSTR((char*)ptr, length);
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x000E28A5 File Offset: 0x000E0AA5
		internal static IntPtr SecureStringCoTaskMemAllocator(int len)
		{
			return Marshal.AllocCoTaskMem(len);
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x000E28AD File Offset: 0x000E0AAD
		internal static IntPtr SecureStringGlobalAllocator(int len)
		{
			return Marshal.AllocHGlobal(len);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x000E28B8 File Offset: 0x000E0AB8
		internal static IntPtr SecureStringToAnsi(SecureString s, Marshal.SecureStringAllocator allocator)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = allocator(length + 1);
			byte[] array = new byte[length + 1];
			try
			{
				byte[] buffer = s.GetBuffer();
				int i = 0;
				int num = 0;
				while (i < length)
				{
					array[i] = buffer[num + 1];
					buffer[num] = 0;
					buffer[num + 1] = 0;
					i++;
					num += 2;
				}
				array[i] = 0;
				Marshal.copy_to_unmanaged(array, 0, intPtr, length + 1);
			}
			finally
			{
				int j = length;
				while (j > 0)
				{
					j--;
					array[j] = 0;
				}
			}
			return intPtr;
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x000E295C File Offset: 0x000E0B5C
		internal static IntPtr SecureStringToUnicode(SecureString s, Marshal.SecureStringAllocator allocator)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = allocator(length * 2 + 2);
			byte[] array = null;
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, i * 2, (short)(((int)array[i * 2] << 8) | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return intPtr;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000E29F0 File Offset: 0x000E0BF0
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToAnsi(s, new Marshal.SecureStringAllocator(Marshal.SecureStringCoTaskMemAllocator));
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000E2A04 File Offset: 0x000E0C04
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToUnicode(s, new Marshal.SecureStringAllocator(Marshal.SecureStringCoTaskMemAllocator));
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000E2A18 File Offset: 0x000E0C18
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToAnsi(s, new Marshal.SecureStringAllocator(Marshal.SecureStringGlobalAllocator));
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000E2A3A File Offset: 0x000E0C3A
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToUnicode(s, new Marshal.SecureStringAllocator(Marshal.SecureStringGlobalAllocator));
		}

		// Token: 0x06004191 RID: 16785
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		// Token: 0x06004192 RID: 16786 RVA: 0x000E2A5C File Offset: 0x000E0C5C
		public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
		{
			Marshal.StructureToPtr(structure, ptr, fDeleteOld);
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000E2A6C File Offset: 0x000E0C6C
		public static void ThrowExceptionForHR(int errorCode)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000E2A88 File Offset: 0x000E0C88
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode, errorInfo);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x06004195 RID: 16789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr BufferToBSTR(char* ptr, int slen);

		// Token: 0x06004196 RID: 16790
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		// Token: 0x06004197 RID: 16791 RVA: 0x000E2AA2 File Offset: 0x000E0CA2
		public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000E2AAB File Offset: 0x000E0CAB
		public unsafe static void WriteByte(IntPtr ptr, byte val)
		{
			*(byte*)(void*)ptr = val;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000E2AB5 File Offset: 0x000E0CB5
		public unsafe static void WriteByte(IntPtr ptr, int ofs, byte val)
		{
			*(byte*)(void*)IntPtr.Add(ptr, ofs) = val;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000E2AC8 File Offset: 0x000E0CC8
		public unsafe static void WriteInt16(IntPtr ptr, short val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000E2AF0 File Offset: 0x000E0CF0
		public unsafe static void WriteInt16(IntPtr ptr, int ofs, short val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2);
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000E2B1A File Offset: 0x000E0D1A
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, (short)val);
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000E2B25 File Offset: 0x000E0D25
		public static void WriteInt16(IntPtr ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000174FB File Offset: 0x000156FB
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000E2B30 File Offset: 0x000E0D30
		public unsafe static void WriteInt32(IntPtr ptr, int val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000E2B58 File Offset: 0x000E0D58
		public unsafe static void WriteInt32(IntPtr ptr, int ofs, int val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x000E2B84 File Offset: 0x000E0D84
		public unsafe static void WriteInt64(IntPtr ptr, long val)
		{
			byte* ptr2 = (byte*)(void*)ptr;
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x000E2BAC File Offset: 0x000E0DAC
		public unsafe static void WriteInt64(IntPtr ptr, int ofs, long val)
		{
			byte* ptr2 = (byte*)(void*)ptr + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8);
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x000174FB File Offset: 0x000156FB
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000E2BD6 File Offset: 0x000E0DD6
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, (long)val);
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E2BF9 File Offset: 0x000E0DF9
		public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, ofs, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, ofs, (long)val);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000174FB File Offset: 0x000156FB
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E2C20 File Offset: 0x000E0E20
		private static Exception ConvertHrToException(int errorCode)
		{
			if (errorCode > -2147024362)
			{
				if (errorCode <= -2146232828)
				{
					if (errorCode <= -2146893792)
					{
						if (errorCode != -2147023895)
						{
							if (errorCode != -2146893792)
							{
								goto IL_03A9;
							}
							return new CryptographicException();
						}
					}
					else
					{
						if (errorCode == -2146234348)
						{
							return new AppDomainUnloadedException();
						}
						switch (errorCode)
						{
						case -2146233088:
							return new Exception();
						case -2146233087:
							return new SystemException();
						case -2146233086:
							return new ArgumentOutOfRangeException();
						case -2146233085:
							return new ArrayTypeMismatchException();
						case -2146233084:
							return new ContextMarshalException();
						case -2146233083:
						case -2146233074:
						case -2146233073:
						case -2146233061:
						case -2146233060:
						case -2146233059:
						case -2146233058:
						case -2146233057:
						case -2146233055:
						case -2146233053:
						case -2146233052:
						case -2146233051:
						case -2146233050:
						case -2146233046:
						case -2146233045:
						case -2146233044:
						case -2146233043:
						case -2146233042:
						case -2146233041:
						case -2146233040:
						case -2146233035:
						case -2146233034:
							goto IL_03A9;
						case -2146233082:
							return new ExecutionEngineException();
						case -2146233081:
							return new FieldAccessException();
						case -2146233080:
							return new IndexOutOfRangeException();
						case -2146233079:
							return new InvalidOperationException();
						case -2146233078:
							return new SecurityException();
						case -2146233077:
							return new RemotingException();
						case -2146233076:
							return new SerializationException();
						case -2146233075:
							return new VerificationException();
						case -2146233072:
							return new MethodAccessException();
						case -2146233071:
							return new MissingFieldException();
						case -2146233070:
							return new MissingMemberException();
						case -2146233069:
							return new MissingMethodException();
						case -2146233068:
							return new MulticastNotSupportedException();
						case -2146233067:
							return new NotSupportedException();
						case -2146233066:
							return new OverflowException();
						case -2146233065:
							return new RankException();
						case -2146233064:
							return new SynchronizationLockException();
						case -2146233063:
							return new ThreadInterruptedException();
						case -2146233062:
							return new MemberAccessException();
						case -2146233056:
							return new ThreadStateException();
						case -2146233054:
							return new TypeLoadException();
						case -2146233049:
							return new InvalidComObjectException();
						case -2146233048:
							return new NotFiniteNumberException();
						case -2146233047:
							return new DuplicateWaitObjectException();
						case -2146233039:
							return new InvalidOleVariantTypeException();
						case -2146233038:
							return new MissingManifestResourceException();
						case -2146233037:
							return new SafeArrayTypeMismatchException();
						case -2146233036:
							return new TypeInitializationException("", null);
						case -2146233033:
							return new FormatException();
						default:
							switch (errorCode)
							{
							case -2146232832:
								return new ApplicationException();
							case -2146232831:
								return new InvalidFilterCriteriaException();
							case -2146232830:
								return new ReflectionTypeLoadException(new Type[0], new Exception[0]);
							case -2146232829:
								return new TargetException();
							case -2146232828:
								return new TargetInvocationException(null);
							default:
								goto IL_03A9;
							}
							break;
						}
					}
				}
				else if (errorCode <= 3)
				{
					if (errorCode == -2146232800)
					{
						return new IOException();
					}
					if (errorCode == 2)
					{
						goto IL_02A6;
					}
					if (errorCode != 3)
					{
						goto IL_03A9;
					}
					goto IL_027C;
				}
				else
				{
					if (errorCode == 11)
					{
						goto IL_026A;
					}
					if (errorCode == 206)
					{
						goto IL_032A;
					}
					if (errorCode != 1001)
					{
						goto IL_03A9;
					}
				}
				return new StackOverflowException();
			}
			if (errorCode <= -2147024893)
			{
				if (errorCode <= -2147352562)
				{
					switch (errorCode)
					{
					case -2147467263:
						return new NotImplementedException();
					case -2147467262:
						return new InvalidCastException();
					case -2147467261:
						return new NullReferenceException();
					default:
						if (errorCode != -2147352562)
						{
							goto IL_03A9;
						}
						return new TargetParameterCountException();
					}
				}
				else
				{
					if (errorCode == -2147352558)
					{
						return new DivideByZeroException();
					}
					if (errorCode == -2147024894)
					{
						goto IL_02A6;
					}
					if (errorCode != -2147024893)
					{
						goto IL_03A9;
					}
					goto IL_027C;
				}
			}
			else if (errorCode <= -2147024858)
			{
				if (errorCode != -2147024885)
				{
					if (errorCode == -2147024882)
					{
						return new OutOfMemoryException();
					}
					if (errorCode != -2147024858)
					{
						goto IL_03A9;
					}
					return new EndOfStreamException();
				}
			}
			else
			{
				if (errorCode == -2147024809)
				{
					return new ArgumentException();
				}
				if (errorCode == -2147024690)
				{
					goto IL_032A;
				}
				if (errorCode != -2147024362)
				{
					goto IL_03A9;
				}
				return new ArithmeticException();
			}
			IL_026A:
			return new BadImageFormatException();
			IL_027C:
			return new DirectoryNotFoundException();
			IL_02A6:
			return new FileNotFoundException();
			IL_032A:
			return new PathTooLongException();
			IL_03A9:
			if (errorCode < 0)
			{
				return new COMException("", errorCode);
			}
			return null;
		}

		// Token: 0x060041AB RID: 16811
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetErrorInfo")]
		private static extern int _SetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] IErrorInfo pIErrorInfo);

		// Token: 0x060041AC RID: 16812
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetErrorInfo")]
		private static extern int _GetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppIErrorInfo);

		// Token: 0x060041AD RID: 16813 RVA: 0x000E2FE8 File Offset: 0x000E11E8
		internal static int SetErrorInfo(int dwReserved, IErrorInfo errorInfo)
		{
			int num = 0;
			errorInfo = null;
			if (Marshal.SetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				num = Marshal._SetErrorInfo(dwReserved, errorInfo);
			}
			catch (Exception)
			{
				Marshal.SetErrorInfoNotAvailable = true;
			}
			return num;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000E3028 File Offset: 0x000E1228
		internal static int GetErrorInfo(int dwReserved, out IErrorInfo errorInfo)
		{
			int num = 0;
			errorInfo = null;
			if (Marshal.GetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				num = Marshal._GetErrorInfo(dwReserved, out errorInfo);
			}
			catch (Exception)
			{
				Marshal.GetErrorInfoNotAvailable = true;
			}
			return num;
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000E3068 File Offset: 0x000E1268
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000E3078 File Offset: 0x000E1278
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			IErrorInfo errorInfo2 = null;
			if (errorInfo != (IntPtr)(-1))
			{
				if (errorInfo == IntPtr.Zero)
				{
					if (Marshal.GetErrorInfo(0, out errorInfo2) != 0)
					{
						errorInfo2 = null;
					}
				}
				else
				{
					errorInfo2 = Marshal.GetObjectForIUnknown(errorInfo) as IErrorInfo;
				}
			}
			if (errorInfo2 is ManagedErrorInfo && ((ManagedErrorInfo)errorInfo2).Exception._HResult == errorCode)
			{
				return ((ManagedErrorInfo)errorInfo2).Exception;
			}
			Exception ex = Marshal.ConvertHrToException(errorCode);
			if (errorInfo2 != null && ex != null)
			{
				uint num;
				errorInfo2.GetHelpContext(out num);
				string text;
				errorInfo2.GetSource(out text);
				ex.Source = text;
				errorInfo2.GetDescription(out text);
				ex.SetMessage(text);
				errorInfo2.GetHelpFile(out text);
				if (num == 0U)
				{
					ex.HelpLink = text;
				}
				else
				{
					ex.HelpLink = string.Format("{0}#{1}", text, num);
				}
			}
			return ex;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000E3146 File Offset: 0x000E1346
		public static int FinalReleaseComObject(object o)
		{
			while (Marshal.ReleaseComObject(o) != 0)
			{
			}
			return 0;
		}

		// Token: 0x060041B2 RID: 16818
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		// Token: 0x060041B3 RID: 16819 RVA: 0x000E3154 File Offset: 0x000E1354
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsSubclassOf(typeof(MulticastDelegate)) || t == typeof(MulticastDelegate))
			{
				throw new ArgumentException("Type is not a delegate", "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException("The specified Type must not be a generic type definition.");
			}
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000E31DB File Offset: 0x000E13DB
		public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
		{
			return (TDelegate)((object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate)));
		}

		// Token: 0x060041B5 RID: 16821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		// Token: 0x060041B6 RID: 16822 RVA: 0x000E31F2 File Offset: 0x000E13F2
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000E3208 File Offset: 0x000E1408
		public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal((Delegate)((object)d));
		}

		// Token: 0x060041B8 RID: 16824
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastWin32Error(int error);

		// Token: 0x060041B9 RID: 16825
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

		// Token: 0x060041BA RID: 16826
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetHRForException_WinRT(Exception e);

		// Token: 0x060041BB RID: 16827
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetNativeActivationFactory(Type type);

		// Token: 0x060041BC RID: 16828 RVA: 0x000E3230 File Offset: 0x000E1430
		internal static ICustomMarshaler GetCustomMarshalerInstance(Type type, string cookie)
		{
			ValueTuple<Type, string> valueTuple = new ValueTuple<Type, string>(type, cookie);
			LazyInitializer.EnsureInitialized<Dictionary<ValueTuple<Type, string>, ICustomMarshaler>>(ref Marshal.MarshalerInstanceCache, () => new Dictionary<ValueTuple<Type, string>, ICustomMarshaler>(new Marshal.MarshalerInstanceKeyComparer()));
			object obj = Marshal.MarshalerInstanceCacheLock;
			ICustomMarshaler customMarshaler;
			bool flag2;
			lock (obj)
			{
				flag2 = Marshal.MarshalerInstanceCache.TryGetValue(valueTuple, out customMarshaler);
			}
			if (!flag2)
			{
				RuntimeMethodInfo runtimeMethodInfo;
				try
				{
					runtimeMethodInfo = (RuntimeMethodInfo)type.GetMethod("GetInstance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[] { typeof(string) }, null);
				}
				catch (AmbiguousMatchException)
				{
					throw new ApplicationException("Custom marshaler '" + type.FullName + "' implements multiple static GetInstance methods that take a single string parameter.");
				}
				if (runtimeMethodInfo == null || runtimeMethodInfo.ReturnType != typeof(ICustomMarshaler))
				{
					throw new ApplicationException("Custom marshaler '" + type.FullName + "' does not implement a static GetInstance method that takes a single string parameter and returns an ICustomMarshaler.");
				}
				Exception ex;
				try
				{
					customMarshaler = (ICustomMarshaler)runtimeMethodInfo.InternalInvoke(null, new object[] { cookie }, out ex);
				}
				catch (Exception ex)
				{
					customMarshaler = null;
				}
				if (ex != null)
				{
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (customMarshaler == null)
				{
					throw new ApplicationException("A call to GetInstance() for custom marshaler '" + type.FullName + "' returned null, which is not allowed.");
				}
				obj = Marshal.MarshalerInstanceCacheLock;
				lock (obj)
				{
					Marshal.MarshalerInstanceCache[valueTuple] = customMarshaler;
				}
			}
			return customMarshaler;
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000E33DC File Offset: 0x000E15DC
		public unsafe static IntPtr StringToCoTaskMemUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int maxByteCount = Encoding.UTF8.GetMaxByteCount(s.Length);
			IntPtr intPtr = Marshal.AllocCoTaskMem(maxByteCount + 1);
			byte* ptr = (byte*)(void*)intPtr;
			int bytes;
			fixed (string text = s)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				bytes = Encoding.UTF8.GetBytes(ptr2, s.Length, ptr, maxByteCount);
			}
			ptr[bytes] = 0;
			return intPtr;
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000E3440 File Offset: 0x000E1640
		// Note: this type is marked as 'beforefieldinit'.
		static Marshal()
		{
		}

		// Token: 0x04002B4E RID: 11086
		public static readonly int SystemMaxDBCSCharSize = 2;

		// Token: 0x04002B4F RID: 11087
		public static readonly int SystemDefaultCharSize = (Environment.IsRunningOnWindows ? 2 : 1);

		// Token: 0x04002B50 RID: 11088
		private static bool SetErrorInfoNotAvailable;

		// Token: 0x04002B51 RID: 11089
		private static bool GetErrorInfoNotAvailable;

		// Token: 0x04002B52 RID: 11090
		internal static Dictionary<ValueTuple<Type, string>, ICustomMarshaler> MarshalerInstanceCache;

		// Token: 0x04002B53 RID: 11091
		internal static readonly object MarshalerInstanceCacheLock = new object();

		// Token: 0x02000719 RID: 1817
		// (Invoke) Token: 0x060041C0 RID: 16832
		internal delegate IntPtr SecureStringAllocator(int len);

		// Token: 0x0200071A RID: 1818
		internal class MarshalerInstanceKeyComparer : IEqualityComparer<ValueTuple<Type, string>>
		{
			// Token: 0x060041C3 RID: 16835 RVA: 0x000E3462 File Offset: 0x000E1662
			public bool Equals(ValueTuple<Type, string> lhs, ValueTuple<Type, string> rhs)
			{
				return lhs.CompareTo(rhs) == 0;
			}

			// Token: 0x060041C4 RID: 16836 RVA: 0x000E346F File Offset: 0x000E166F
			public int GetHashCode(ValueTuple<Type, string> key)
			{
				return key.GetHashCode();
			}

			// Token: 0x060041C5 RID: 16837 RVA: 0x000025BE File Offset: 0x000007BE
			public MarshalerInstanceKeyComparer()
			{
			}
		}

		// Token: 0x0200071B RID: 1819
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060041C6 RID: 16838 RVA: 0x000E347E File Offset: 0x000E167E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060041C7 RID: 16839 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060041C8 RID: 16840 RVA: 0x000E348A File Offset: 0x000E168A
			internal Dictionary<ValueTuple<Type, string>, ICustomMarshaler> <GetCustomMarshalerInstance>b__225_0()
			{
				return new Dictionary<ValueTuple<Type, string>, ICustomMarshaler>(new Marshal.MarshalerInstanceKeyComparer());
			}

			// Token: 0x04002B54 RID: 11092
			public static readonly Marshal.<>c <>9 = new Marshal.<>c();

			// Token: 0x04002B55 RID: 11093
			public static Func<Dictionary<ValueTuple<Type, string>, ICustomMarshaler>> <>9__225_0;
		}
	}
}
