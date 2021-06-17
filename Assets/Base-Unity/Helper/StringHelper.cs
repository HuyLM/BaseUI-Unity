using UnityEngine;


namespace AtoLib.Helper
{
    public static class StringHelper 
    {
        public static string GetCommaCurrencyFormat(int number) // 1,000,000
        {
            return System.String.Format( "{0:N0}", number );
        }
    }
}
