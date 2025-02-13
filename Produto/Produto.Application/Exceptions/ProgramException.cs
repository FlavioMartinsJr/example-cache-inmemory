using Produto.Domain.Constants;
using Produto.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Produto.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    public static class ProgramException
    {
        public static UserMessageException AppsettingNotSetException()
            => new(ErrorCode.Internal, ErrorMessageConstants.AppConfigurationMessage, ErrorMessageConstants.InternalError);
    }
}
