using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009E RID: 158
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001CA58 File Offset: 0x0001AC58
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001CA7E File Offset: 0x0001AC7E
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'.");
				}
				return this._mappings;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001CAAD File Offset: 0x0001ACAD
		private ErrorContext GetErrorContext(object currentObject, object member, string path, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, path, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return this._currentErrorContext;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001CAE7 File Offset: 0x0001ACE7
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001CB04 File Offset: 0x0001AD04
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, string path, Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= 1 && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = ((base.GetType() == typeof(JsonSerializerInternalWriter)) ? "Error serializing" : "Error deserializing");
				if (contract != null)
				{
					text = text + " " + contract.UnderlyingType;
				}
				text = text + ". " + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(1, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x0400030F RID: 783
		private ErrorContext _currentErrorContext;

		// Token: 0x04000310 RID: 784
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x04000311 RID: 785
		internal readonly JsonSerializer Serializer;

		// Token: 0x04000312 RID: 786
		internal readonly ITraceWriter TraceWriter;

		// Token: 0x04000313 RID: 787
		protected JsonSerializerProxy InternalSerializer;

		// Token: 0x0200014A RID: 330
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x06000D03 RID: 3331 RVA: 0x00031632 File Offset: 0x0002F832
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06000D04 RID: 3332 RVA: 0x00031638 File Offset: 0x0002F838
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x06000D05 RID: 3333 RVA: 0x00008020 File Offset: 0x00006220
			public ReferenceEqualsEqualityComparer()
			{
			}
		}
	}
}
