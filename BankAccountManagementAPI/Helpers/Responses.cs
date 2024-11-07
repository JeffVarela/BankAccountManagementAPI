namespace BankAccountManagementAPI.Helpers
{
    public static class Responses
    {
        public static class UserAccount
        {
            public const string NotFound = "Cuenta de usuario no encontrada";
        }

        public static class AccountTransaction
        {
            public const string NotFound = "Movimiento no encontrado";

            public const string NoBalance = "Saldo insuficiente";
        }
    }
}
