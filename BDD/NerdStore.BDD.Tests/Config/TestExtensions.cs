﻿namespace NerdStore.BDD.Tests.Config
{
    public static class TestExtensions
    {
        public static int ApenasNumeros(this string value)
        {
            return Convert.ToInt32(value.Where(char.IsDigit).ToArray());
        }
    }
}