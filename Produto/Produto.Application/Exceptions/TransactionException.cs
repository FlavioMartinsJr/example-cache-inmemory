using System.Diagnostics.CodeAnalysis;
using Produto.Domain.Constants;
using Produto.Domain.Enums;

namespace Produto.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    public static class TransactionException
    {
        public static UserMessageException TransactionNotCommitException()
            => throw new UserMessageException(ErrorCode.Internal, ErrorMessageConstants.TransactionNotCommit, ErrorMessageConstants.TransactionNotCommit);

        public static UserMessageException TransactionNotExecuteException(Exception ex)
            => throw new UserMessageException(ErrorCode.Internal, ErrorMessageConstants.TransactionNotExecute, ErrorMessageConstants.TransactionNotExecute, ex);
    }
}