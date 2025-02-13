
namespace Produto.Domain.Constants
{
    public static class ErrorMessageConstants
    {
        public const string InternalError = "Ocorreu algum problema interno, porfavor tente novamente mais tarde.";
        public const string NotFoundMessage = "A solicitação feita não foi encontrada.";
        public const string AppConfigurationMessage = "Não é possível recuperar as configurações do aplicativo.";
        public const string TransactionNotCommit = "A transação não foi salva.";
        public const string TransactionNotExecute = "A transação não foi executada.";
    }
}
