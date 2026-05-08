using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000643 RID: 1603
	[ComVisible(true)]
	public sealed class SerializationInfo
	{
		// Token: 0x06003D13 RID: 15635 RVA: 0x000D464A File Offset: 0x000D284A
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter)
			: this(type, converter, false)
		{
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000D4658 File Offset: 0x000D2858
		[CLSCompliant(false)]
		public SerializationInfo(Type type, IFormatterConverter converter, bool requireSameTokenInPartialTrust)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			this.objectType = type;
			this.m_fullTypeName = type.FullName;
			this.m_assemName = type.Module.Assembly.FullName;
			this.m_members = new string[4];
			this.m_data = new object[4];
			this.m_types = new Type[4];
			this.m_nameToIndex = new Dictionary<string, int>();
			this.m_converter = converter;
			this.requireSameTokenInPartialTrust = requireSameTokenInPartialTrust;
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000D46ED File Offset: 0x000D28ED
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x000D46F5 File Offset: 0x000D28F5
		public string FullTypeName
		{
			get
			{
				return this.m_fullTypeName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_fullTypeName = value;
				this.isFullTypeNameSetExplicit = true;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000D4713 File Offset: 0x000D2913
		// (set) Token: 0x06003D18 RID: 15640 RVA: 0x000D471B File Offset: 0x000D291B
		public string AssemblyName
		{
			get
			{
				return this.m_assemName;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.requireSameTokenInPartialTrust)
				{
					SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.m_assemName, value);
				}
				this.m_assemName = value;
				this.isAssemblyNameSetExplicit = true;
			}
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x000D4750 File Offset: 0x000D2950
		[SecuritySafeCritical]
		public void SetType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (this.requireSameTokenInPartialTrust)
			{
				SerializationInfo.DemandForUnsafeAssemblyNameAssignments(this.ObjectType.Assembly.FullName, type.Assembly.FullName);
			}
			if (this.objectType != type)
			{
				this.objectType = type;
				this.m_fullTypeName = type.FullName;
				this.m_assemName = type.Module.Assembly.FullName;
				this.isFullTypeNameSetExplicit = false;
				this.isAssemblyNameSetExplicit = false;
			}
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000D47D4 File Offset: 0x000D29D4
		private static bool Compare(byte[] a, byte[] b)
		{
			if (a == null || b == null || a.Length == 0 || b.Length == 0 || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000D4812 File Offset: 0x000D2A12
		[SecuritySafeCritical]
		internal static void DemandForUnsafeAssemblyNameAssignments(string originalAssemblyName, string newAssemblyName)
		{
			SerializationInfo.IsAssemblyNameAssignmentSafe(originalAssemblyName, newAssemblyName);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000D481C File Offset: 0x000D2A1C
		internal static bool IsAssemblyNameAssignmentSafe(string originalAssemblyName, string newAssemblyName)
		{
			if (originalAssemblyName == newAssemblyName)
			{
				return true;
			}
			AssemblyName assemblyName = new AssemblyName(originalAssemblyName);
			AssemblyName assemblyName2 = new AssemblyName(newAssemblyName);
			return !string.Equals(assemblyName2.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) && !string.Equals(assemblyName2.Name, "mscorlib.dll", StringComparison.OrdinalIgnoreCase) && SerializationInfo.Compare(assemblyName.GetPublicKeyToken(), assemblyName2.GetPublicKeyToken());
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000D487B File Offset: 0x000D2A7B
		public int MemberCount
		{
			get
			{
				return this.m_currMember;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000D4883 File Offset: 0x000D2A83
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x000D488B File Offset: 0x000D2A8B
		public bool IsFullTypeNameSetExplicit
		{
			get
			{
				return this.isFullTypeNameSetExplicit;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x000D4893 File Offset: 0x000D2A93
		public bool IsAssemblyNameSetExplicit
		{
			get
			{
				return this.isAssemblyNameSetExplicit;
			}
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000D489B File Offset: 0x000D2A9B
		public SerializationInfoEnumerator GetEnumerator()
		{
			return new SerializationInfoEnumerator(this.m_members, this.m_data, this.m_types, this.m_currMember);
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000D48BC File Offset: 0x000D2ABC
		private void ExpandArrays()
		{
			int num = this.m_currMember * 2;
			if (num < this.m_currMember && 2147483647 > this.m_currMember)
			{
				num = int.MaxValue;
			}
			string[] array = new string[num];
			object[] array2 = new object[num];
			Type[] array3 = new Type[num];
			Array.Copy(this.m_members, array, this.m_currMember);
			Array.Copy(this.m_data, array2, this.m_currMember);
			Array.Copy(this.m_types, array3, this.m_currMember);
			this.m_members = array;
			this.m_data = array2;
			this.m_types = array3;
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000D494E File Offset: 0x000D2B4E
		public void AddValue(string name, object value, Type type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.AddValueInternal(name, value, type);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000D4975 File Offset: 0x000D2B75
		public void AddValue(string name, object value)
		{
			if (value == null)
			{
				this.AddValue(name, value, typeof(object));
				return;
			}
			this.AddValue(name, value, value.GetType());
		}

		// Token: 0x06003D25 RID: 15653 RVA: 0x000D499B File Offset: 0x000D2B9B
		public void AddValue(string name, bool value)
		{
			this.AddValue(name, value, typeof(bool));
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000D49B4 File Offset: 0x000D2BB4
		public void AddValue(string name, char value)
		{
			this.AddValue(name, value, typeof(char));
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000D49CD File Offset: 0x000D2BCD
		[CLSCompliant(false)]
		public void AddValue(string name, sbyte value)
		{
			this.AddValue(name, value, typeof(sbyte));
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000D49E6 File Offset: 0x000D2BE6
		public void AddValue(string name, byte value)
		{
			this.AddValue(name, value, typeof(byte));
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x000D49FF File Offset: 0x000D2BFF
		public void AddValue(string name, short value)
		{
			this.AddValue(name, value, typeof(short));
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x000D4A18 File Offset: 0x000D2C18
		[CLSCompliant(false)]
		public void AddValue(string name, ushort value)
		{
			this.AddValue(name, value, typeof(ushort));
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000D4A31 File Offset: 0x000D2C31
		public void AddValue(string name, int value)
		{
			this.AddValue(name, value, typeof(int));
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000D4A4A File Offset: 0x000D2C4A
		[CLSCompliant(false)]
		public void AddValue(string name, uint value)
		{
			this.AddValue(name, value, typeof(uint));
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000D4A63 File Offset: 0x000D2C63
		public void AddValue(string name, long value)
		{
			this.AddValue(name, value, typeof(long));
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000D4A7C File Offset: 0x000D2C7C
		[CLSCompliant(false)]
		public void AddValue(string name, ulong value)
		{
			this.AddValue(name, value, typeof(ulong));
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000D4A95 File Offset: 0x000D2C95
		public void AddValue(string name, float value)
		{
			this.AddValue(name, value, typeof(float));
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000D4AAE File Offset: 0x000D2CAE
		public void AddValue(string name, double value)
		{
			this.AddValue(name, value, typeof(double));
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000D4AC7 File Offset: 0x000D2CC7
		public void AddValue(string name, decimal value)
		{
			this.AddValue(name, value, typeof(decimal));
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000D4AE0 File Offset: 0x000D2CE0
		public void AddValue(string name, DateTime value)
		{
			this.AddValue(name, value, typeof(DateTime));
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000D4AFC File Offset: 0x000D2CFC
		internal void AddValueInternal(string name, object value, Type type)
		{
			if (this.m_nameToIndex.ContainsKey(name))
			{
				throw new SerializationException(Environment.GetResourceString("Cannot add the same member twice to a SerializationInfo object."));
			}
			this.m_nameToIndex.Add(name, this.m_currMember);
			if (this.m_currMember >= this.m_members.Length)
			{
				this.ExpandArrays();
			}
			this.m_members[this.m_currMember] = name;
			this.m_data[this.m_currMember] = value;
			this.m_types[this.m_currMember] = type;
			this.m_currMember++;
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000D4B88 File Offset: 0x000D2D88
		internal void UpdateValue(string name, object value, Type type)
		{
			int num = this.FindElement(name);
			if (num < 0)
			{
				this.AddValueInternal(name, value, type);
				return;
			}
			this.m_data[num] = value;
			this.m_types[num] = type;
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000D4BC0 File Offset: 0x000D2DC0
		private int FindElement(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num;
			if (this.m_nameToIndex.TryGetValue(name, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000D4BF0 File Offset: 0x000D2DF0
		private object GetElement(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				throw new SerializationException(Environment.GetResourceString("Member '{0}' was not found.", new object[] { name }));
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000D4C38 File Offset: 0x000D2E38
		[ComVisible(true)]
		private object GetElementNoThrow(string name, out Type foundType)
		{
			int num = this.FindElement(name);
			if (num == -1)
			{
				foundType = null;
				return null;
			}
			foundType = this.m_types[num];
			return this.m_data[num];
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000D4C68 File Offset: 0x000D2E68
		[SecuritySafeCritical]
		public object GetValue(string name, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
			}
			Type type2;
			object element = this.GetElement(name, out type2);
			if (RemotingServices.IsTransparentProxy(element))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(element), runtimeType))
				{
					return element;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || element == null)
			{
				return element;
			}
			return this.m_converter.Convert(element, type);
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000D4CE4 File Offset: 0x000D2EE4
		[SecuritySafeCritical]
		[ComVisible(true)]
		internal object GetValueNoThrow(string name, Type type)
		{
			Type type2;
			object elementNoThrow = this.GetElementNoThrow(name, out type2);
			if (elementNoThrow == null)
			{
				return null;
			}
			if (RemotingServices.IsTransparentProxy(elementNoThrow))
			{
				if (RemotingServices.ProxyCheckCast(RemotingServices.GetRealProxy(elementNoThrow), (RuntimeType)type))
				{
					return elementNoThrow;
				}
			}
			else if (type2 == type || type.IsAssignableFrom(type2) || elementNoThrow == null)
			{
				return elementNoThrow;
			}
			return this.m_converter.Convert(elementNoThrow, type);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000D4D3C File Offset: 0x000D2F3C
		public bool GetBoolean(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(bool))
			{
				return (bool)element;
			}
			return this.m_converter.ToBoolean(element);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000D4D74 File Offset: 0x000D2F74
		public char GetChar(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(char))
			{
				return (char)element;
			}
			return this.m_converter.ToChar(element);
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000D4DAC File Offset: 0x000D2FAC
		[CLSCompliant(false)]
		public sbyte GetSByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(sbyte))
			{
				return (sbyte)element;
			}
			return this.m_converter.ToSByte(element);
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000D4DE4 File Offset: 0x000D2FE4
		public byte GetByte(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(byte))
			{
				return (byte)element;
			}
			return this.m_converter.ToByte(element);
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000D4E1C File Offset: 0x000D301C
		public short GetInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(short))
			{
				return (short)element;
			}
			return this.m_converter.ToInt16(element);
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000D4E54 File Offset: 0x000D3054
		[CLSCompliant(false)]
		public ushort GetUInt16(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ushort))
			{
				return (ushort)element;
			}
			return this.m_converter.ToUInt16(element);
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000D4E8C File Offset: 0x000D308C
		public int GetInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(int))
			{
				return (int)element;
			}
			return this.m_converter.ToInt32(element);
		}

		// Token: 0x06003D41 RID: 15681 RVA: 0x000D4EC4 File Offset: 0x000D30C4
		[CLSCompliant(false)]
		public uint GetUInt32(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(uint))
			{
				return (uint)element;
			}
			return this.m_converter.ToUInt32(element);
		}

		// Token: 0x06003D42 RID: 15682 RVA: 0x000D4EFC File Offset: 0x000D30FC
		public long GetInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(long))
			{
				return (long)element;
			}
			return this.m_converter.ToInt64(element);
		}

		// Token: 0x06003D43 RID: 15683 RVA: 0x000D4F34 File Offset: 0x000D3134
		[CLSCompliant(false)]
		public ulong GetUInt64(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(ulong))
			{
				return (ulong)element;
			}
			return this.m_converter.ToUInt64(element);
		}

		// Token: 0x06003D44 RID: 15684 RVA: 0x000D4F6C File Offset: 0x000D316C
		public float GetSingle(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(float))
			{
				return (float)element;
			}
			return this.m_converter.ToSingle(element);
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000D4FA4 File Offset: 0x000D31A4
		public double GetDouble(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(double))
			{
				return (double)element;
			}
			return this.m_converter.ToDouble(element);
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000D4FDC File Offset: 0x000D31DC
		public decimal GetDecimal(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(decimal))
			{
				return (decimal)element;
			}
			return this.m_converter.ToDecimal(element);
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000D5014 File Offset: 0x000D3214
		public DateTime GetDateTime(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(DateTime))
			{
				return (DateTime)element;
			}
			return this.m_converter.ToDateTime(element);
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000D504C File Offset: 0x000D324C
		public string GetString(string name)
		{
			Type type;
			object element = this.GetElement(name, out type);
			if (type == typeof(string) || element == null)
			{
				return (string)element;
			}
			return this.m_converter.ToString(element);
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000D5086 File Offset: 0x000D3286
		internal string[] MemberNames
		{
			get
			{
				return this.m_members;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x000D508E File Offset: 0x000D328E
		internal object[] MemberValues
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x0400270A RID: 9994
		private const int defaultSize = 4;

		// Token: 0x0400270B RID: 9995
		private const string s_mscorlibAssemblySimpleName = "mscorlib";

		// Token: 0x0400270C RID: 9996
		private const string s_mscorlibFileName = "mscorlib.dll";

		// Token: 0x0400270D RID: 9997
		internal string[] m_members;

		// Token: 0x0400270E RID: 9998
		internal object[] m_data;

		// Token: 0x0400270F RID: 9999
		internal Type[] m_types;

		// Token: 0x04002710 RID: 10000
		private Dictionary<string, int> m_nameToIndex;

		// Token: 0x04002711 RID: 10001
		internal int m_currMember;

		// Token: 0x04002712 RID: 10002
		internal IFormatterConverter m_converter;

		// Token: 0x04002713 RID: 10003
		private string m_fullTypeName;

		// Token: 0x04002714 RID: 10004
		private string m_assemName;

		// Token: 0x04002715 RID: 10005
		private Type objectType;

		// Token: 0x04002716 RID: 10006
		private bool isFullTypeNameSetExplicit;

		// Token: 0x04002717 RID: 10007
		private bool isAssemblyNameSetExplicit;

		// Token: 0x04002718 RID: 10008
		private bool requireSameTokenInPartialTrust;
	}
}
