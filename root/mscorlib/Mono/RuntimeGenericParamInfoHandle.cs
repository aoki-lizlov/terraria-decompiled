using System;
using System.Reflection;

namespace Mono
{
	// Token: 0x0200002B RID: 43
	internal struct RuntimeGenericParamInfoHandle
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x000041DA File Offset: 0x000023DA
		internal unsafe RuntimeGenericParamInfoHandle(RuntimeStructs.GenericParamInfo* value)
		{
			this.value = value;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000041E3 File Offset: 0x000023E3
		internal unsafe RuntimeGenericParamInfoHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GenericParamInfo*)(void*)ptr;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000041F1 File Offset: 0x000023F1
		internal Type[] Constraints
		{
			get
			{
				return this.GetConstraints();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000041F9 File Offset: 0x000023F9
		internal unsafe GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.value->flags;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004208 File Offset: 0x00002408
		private unsafe Type[] GetConstraints()
		{
			int constraintsCount = this.GetConstraintsCount();
			Type[] array = new Type[constraintsCount];
			for (int i = 0; i < constraintsCount; i++)
			{
				RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(*(IntPtr*)(this.value->constraints + (IntPtr)i * (IntPtr)sizeof(RuntimeStructs.MonoClass*) / (IntPtr)sizeof(RuntimeStructs.MonoClass*)));
				array[i] = Type.GetTypeFromHandle(runtimeClassHandle.GetTypeHandle());
			}
			return array;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000425C File Offset: 0x0000245C
		private unsafe int GetConstraintsCount()
		{
			int num = 0;
			RuntimeStructs.MonoClass** ptr = this.value->constraints;
			while (ptr != null && *(IntPtr*)ptr != (IntPtr)((UIntPtr)0))
			{
				ptr += sizeof(RuntimeStructs.MonoClass*) / sizeof(RuntimeStructs.MonoClass*);
				num++;
			}
			return num;
		}

		// Token: 0x04000CE0 RID: 3296
		private unsafe RuntimeStructs.GenericParamInfo* value;
	}
}
