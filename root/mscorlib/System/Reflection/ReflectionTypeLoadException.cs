using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200088E RID: 2190
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		// Token: 0x06004974 RID: 18804 RVA: 0x000EEF7D File Offset: 0x000ED17D
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
			: base(null)
		{
			this.Types = classes;
			this.LoaderExceptions = exceptions;
			base.HResult = -2146232830;
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x000EEF9F File Offset: 0x000ED19F
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
			: base(message)
		{
			this.Types = classes;
			this.LoaderExceptions = exceptions;
			base.HResult = -2146232830;
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x000EEFC1 File Offset: 0x000ED1C1
		private ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.LoaderExceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x000EEFEB File Offset: 0x000ED1EB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Types", null, typeof(Type[]));
			info.AddValue("Exceptions", this.LoaderExceptions, typeof(Exception[]));
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004978 RID: 18808 RVA: 0x000EF026 File Offset: 0x000ED226
		public Type[] Types
		{
			[CompilerGenerated]
			get
			{
				return this.<Types>k__BackingField;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004979 RID: 18809 RVA: 0x000EF02E File Offset: 0x000ED22E
		public Exception[] LoaderExceptions
		{
			[CompilerGenerated]
			get
			{
				return this.<LoaderExceptions>k__BackingField;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x000EF036 File Offset: 0x000ED236
		public override string Message
		{
			get
			{
				return this.CreateString(true);
			}
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x000EF03F File Offset: 0x000ED23F
		public override string ToString()
		{
			return this.CreateString(false);
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x000EF048 File Offset: 0x000ED248
		private string CreateString(bool isMessage)
		{
			string text = (isMessage ? base.Message : base.ToString());
			Exception[] loaderExceptions = this.LoaderExceptions;
			if (loaderExceptions == null || loaderExceptions.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			foreach (Exception ex in loaderExceptions)
			{
				if (ex != null)
				{
					stringBuilder.AppendLine();
					stringBuilder.Append(isMessage ? ex.Message : ex.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002EA5 RID: 11941
		[CompilerGenerated]
		private readonly Type[] <Types>k__BackingField;

		// Token: 0x04002EA6 RID: 11942
		[CompilerGenerated]
		private readonly Exception[] <LoaderExceptions>k__BackingField;
	}
}
