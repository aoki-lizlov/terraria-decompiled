using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008EC RID: 2284
	[ComVisible(true)]
	public class DynamicILInfo
	{
		// Token: 0x06004EC9 RID: 20169 RVA: 0x000025BE File Offset: 0x000007BE
		internal DynamicILInfo()
		{
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x000F9492 File Offset: 0x000F7692
		internal DynamicILInfo(DynamicMethod method)
		{
			this.method = method;
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x000F94A1 File Offset: 0x000F76A1
		public DynamicMethod DynamicMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public int GetTokenFor(byte[] signature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x000F94A9 File Offset: 0x000F76A9
		public int GetTokenFor(DynamicMethod method)
		{
			return this.method.GetILGenerator().TokenGenerator.GetToken(method, false);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x000F94C2 File Offset: 0x000F76C2
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.method.GetILGenerator().TokenGenerator.GetToken(FieldInfo.GetFieldFromHandle(field), false);
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x000F94E0 File Offset: 0x000F76E0
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			MethodBase methodFromHandle = MethodBase.GetMethodFromHandle(method);
			return this.method.GetILGenerator().TokenGenerator.GetToken(methodFromHandle, false);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x000F950C File Offset: 0x000F770C
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			Type typeFromHandle = Type.GetTypeFromHandle(type);
			return this.method.GetILGenerator().TokenGenerator.GetToken(typeFromHandle, false);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x000F9537 File Offset: 0x000F7737
		public int GetTokenFor(string literal)
		{
			return this.method.GetILGenerator().TokenGenerator.GetToken(literal);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x000F954F File Offset: 0x000F774F
		public void SetCode(byte[] code, int maxStackSize)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			this.method.GetILGenerator().SetCode(code, maxStackSize);
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x000F9571 File Offset: 0x000F7771
		[CLSCompliant(false)]
		public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			this.method.GetILGenerator().SetCode(code, codeSize, maxStackSize);
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void SetExceptions(byte[] exceptions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		[CLSCompliant(false)]
		public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO]
		public void SetLocalSignature(byte[] localSignature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x000F9598 File Offset: 0x000F7798
		[CLSCompliant(false)]
		public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
		{
			byte[] array = new byte[signatureSize];
			for (int i = 0; i < signatureSize; i++)
			{
				array[i] = localSignature[i];
			}
		}

		// Token: 0x040030B4 RID: 12468
		private DynamicMethod method;
	}
}
