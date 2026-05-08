using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	internal class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x0005B5B1 File Offset: 0x000597B1
		internal static void GetUnitySerializationInfo(SerializationInfo info, Missing missing)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("UnityType", 3);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005B5D0 File Offset: 0x000597D0
		internal static RuntimeType AddElementTypes(SerializationInfo info, RuntimeType type)
		{
			List<int> list = new List<int>();
			while (type.HasElementType)
			{
				if (type.IsSzArray)
				{
					list.Add(3);
				}
				else if (type.IsArray)
				{
					list.Add(type.GetArrayRank());
					list.Add(2);
				}
				else if (type.IsPointer)
				{
					list.Add(1);
				}
				else if (type.IsByRef)
				{
					list.Add(4);
				}
				type = (RuntimeType)type.GetElementType();
			}
			info.AddValue("ElementTypes", list.ToArray(), typeof(int[]));
			return type;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0005B664 File Offset: 0x00059864
		internal Type MakeElementTypes(Type type)
		{
			for (int i = this.m_elementTypes.Length - 1; i >= 0; i--)
			{
				if (this.m_elementTypes[i] == 3)
				{
					type = type.MakeArrayType();
				}
				else if (this.m_elementTypes[i] == 2)
				{
					type = type.MakeArrayType(this.m_elementTypes[--i]);
				}
				else if (this.m_elementTypes[i] == 1)
				{
					type = type.MakePointerType();
				}
				else if (this.m_elementTypes[i] == 4)
				{
					type = type.MakeByRefType();
				}
			}
			return type;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0005B6E8 File Offset: 0x000598E8
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", null, typeof(string));
			info.AddValue("UnityType", unityType);
			info.AddValue("AssemblyName", string.Empty);
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0005B738 File Offset: 0x00059938
		internal static void GetUnitySerializationInfo(SerializationInfo info, RuntimeType type)
		{
			if (type.GetRootElementType().IsGenericParameter)
			{
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.SetType(typeof(UnitySerializationHolder));
				info.AddValue("UnityType", 7);
				info.AddValue("GenericParameterPosition", type.GenericParameterPosition);
				info.AddValue("DeclaringMethod", type.DeclaringMethod, typeof(MethodBase));
				info.AddValue("DeclaringType", type.DeclaringType, typeof(Type));
				return;
			}
			int num = 4;
			if (!type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				num = 8;
				type = UnitySerializationHolder.AddElementTypes(info, type);
				info.AddValue("GenericArguments", type.GetGenericArguments(), typeof(Type[]));
				type = (RuntimeType)type.GetGenericTypeDefinition();
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, num, type.FullName, type.GetRuntimeAssembly());
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0005B818 File Offset: 0x00059A18
		internal static void GetUnitySerializationInfo(SerializationInfo info, int unityType, string data, RuntimeAssembly assembly)
		{
			info.SetType(typeof(UnitySerializationHolder));
			info.AddValue("Data", data, typeof(string));
			info.AddValue("UnityType", unityType);
			string text;
			if (assembly == null)
			{
				text = string.Empty;
			}
			else
			{
				text = assembly.FullName;
			}
			info.AddValue("AssemblyName", text);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005B87C File Offset: 0x00059A7C
		internal UnitySerializationHolder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_unityType = info.GetInt32("UnityType");
			if (this.m_unityType == 3)
			{
				return;
			}
			if (this.m_unityType == 7)
			{
				this.m_declaringMethod = info.GetValue("DeclaringMethod", typeof(MethodBase)) as MethodBase;
				this.m_declaringType = info.GetValue("DeclaringType", typeof(Type)) as Type;
				this.m_genericParameterPosition = info.GetInt32("GenericParameterPosition");
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
				return;
			}
			if (this.m_unityType == 8)
			{
				this.m_instantiation = info.GetValue("GenericArguments", typeof(Type[])) as Type[];
				this.m_elementTypes = info.GetValue("ElementTypes", typeof(int[])) as int[];
			}
			this.m_data = info.GetString("Data");
			this.m_assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005B99E File Offset: 0x00059B9E
		private void ThrowInsufficientInformation(string field)
		{
			throw new SerializationException(Environment.GetResourceString("Insufficient state to deserialize the object. Missing field '{0}'. More information is needed.", new object[] { field }));
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005B9B9 File Offset: 0x00059BB9
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself."));
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0005B9CC File Offset: 0x00059BCC
		[SecurityCritical]
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this.m_unityType)
			{
			case 1:
				return Empty.Value;
			case 2:
				return DBNull.Value;
			case 3:
				return Missing.Value;
			case 4:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				if (this.m_assemblyName.Length == 0)
				{
					return Type.GetType(this.m_data, true, false);
				}
				return Assembly.Load(this.m_assemblyName).GetType(this.m_data, true, false);
			case 5:
			{
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				Module module = Assembly.Load(this.m_assemblyName).GetModule(this.m_data);
				if (module == null)
				{
					throw new SerializationException(Environment.GetResourceString("The given module {0} cannot be found within the assembly {1}.", new object[] { this.m_data, this.m_assemblyName }));
				}
				return module;
			}
			case 6:
				if (this.m_data == null || this.m_data.Length == 0)
				{
					this.ThrowInsufficientInformation("Data");
				}
				if (this.m_assemblyName == null)
				{
					this.ThrowInsufficientInformation("AssemblyName");
				}
				return Assembly.Load(this.m_assemblyName);
			case 7:
				if (this.m_declaringMethod == null && this.m_declaringType == null)
				{
					this.ThrowInsufficientInformation("DeclaringMember");
				}
				if (this.m_declaringMethod != null)
				{
					return this.m_declaringMethod.GetGenericArguments()[this.m_genericParameterPosition];
				}
				return this.MakeElementTypes(this.m_declaringType.GetGenericArguments()[this.m_genericParameterPosition]);
			case 8:
			{
				this.m_unityType = 4;
				Type type = this.GetRealObject(context) as Type;
				this.m_unityType = 8;
				if (this.m_instantiation[0] == null)
				{
					return null;
				}
				return this.MakeElementTypes(type.MakeGenericType(this.m_instantiation));
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Invalid Unity type."));
			}
		}

		// Token: 0x0400151B RID: 5403
		internal const int EmptyUnity = 1;

		// Token: 0x0400151C RID: 5404
		internal const int NullUnity = 2;

		// Token: 0x0400151D RID: 5405
		internal const int MissingUnity = 3;

		// Token: 0x0400151E RID: 5406
		internal const int RuntimeTypeUnity = 4;

		// Token: 0x0400151F RID: 5407
		internal const int ModuleUnity = 5;

		// Token: 0x04001520 RID: 5408
		internal const int AssemblyUnity = 6;

		// Token: 0x04001521 RID: 5409
		internal const int GenericParameterTypeUnity = 7;

		// Token: 0x04001522 RID: 5410
		internal const int PartialInstantiationTypeUnity = 8;

		// Token: 0x04001523 RID: 5411
		internal const int Pointer = 1;

		// Token: 0x04001524 RID: 5412
		internal const int Array = 2;

		// Token: 0x04001525 RID: 5413
		internal const int SzArray = 3;

		// Token: 0x04001526 RID: 5414
		internal const int ByRef = 4;

		// Token: 0x04001527 RID: 5415
		private Type[] m_instantiation;

		// Token: 0x04001528 RID: 5416
		private int[] m_elementTypes;

		// Token: 0x04001529 RID: 5417
		private int m_genericParameterPosition;

		// Token: 0x0400152A RID: 5418
		private Type m_declaringType;

		// Token: 0x0400152B RID: 5419
		private MethodBase m_declaringMethod;

		// Token: 0x0400152C RID: 5420
		private string m_data;

		// Token: 0x0400152D RID: 5421
		private string m_assemblyName;

		// Token: 0x0400152E RID: 5422
		private int m_unityType;
	}
}
