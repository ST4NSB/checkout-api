namespace BusinessLogic.Helpers
{
    public static class HelperFunctions
    {
        public static decimal CalculateTotalGrossAmount(decimal netAmount)
        {
            return netAmount * (1 + Constants.VAT);
        }
    }
}
