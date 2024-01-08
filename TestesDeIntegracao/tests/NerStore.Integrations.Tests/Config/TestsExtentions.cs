using System.Net.Http.Headers;

namespace NerStore.Integrations.Tests.Config
{
    public static class TestsExtentions
    {
        public static decimal OnlyNumebrs(this string value)
        {
            return Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();   
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void AtribuirToken(this HttpClient client, string token)
        {
            client.AtribuirJsonMediaType();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
