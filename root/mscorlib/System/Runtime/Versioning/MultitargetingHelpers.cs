using System;
using System.Security;

namespace System.Runtime.Versioning
{
	// Token: 0x0200060D RID: 1549
	internal static class MultitargetingHelpers
	{
		// Token: 0x06003BBE RID: 15294 RVA: 0x000D0B04 File Offset: 0x000CED04
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = type.AssemblyQualifiedName;
				}
			}
			return text;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000D0B50 File Offset: 0x000CED50
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000D0B7D File Offset: 0x000CED7D
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}
	}
}
