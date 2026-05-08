using System;
using System.Linq.Expressions;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004C RID: 76
	internal class NoThrowExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x0001060F File Offset: 0x0000E80F
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == 60)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010643 File Offset: 0x0000E843
		public NoThrowExpressionVisitor()
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001064B File Offset: 0x0000E84B
		// Note: this type is marked as 'beforefieldinit'.
		static NoThrowExpressionVisitor()
		{
		}

		// Token: 0x040001E8 RID: 488
		internal static readonly object ErrorResult = new object();
	}
}
