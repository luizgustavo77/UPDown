using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using UPDown.Common.PessoaAPI;

namespace UPDown.Client.Components
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISyncLocalStorageService _localStorage;
        public UserType? userType { get; set; }

        public TokenAuthenticationStateProvider(ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;
            userType = GetUserType();
            Console.WriteLine("Construtor do token");
        }

        public void SetToken(LoginResultDTO login)
        {
            if (login == null || login.Token == null)
            {
                _localStorage.SetItem("authToken", "");
                _localStorage.SetItem("authUserType", "");
                _localStorage.SetItem("authTokenExpiry", "");
                Console.WriteLine($"APAGO O USER: {userType}");
            }
            else
            {
                _localStorage.SetItem("authToken", login.Token);
                _localStorage.SetItem("authUserType", login.User);
                _localStorage.SetItem("authTokenExpiry", login.Expiry);
                userType = login.User;
                Console.WriteLine($"CHEGOU O USER: {userType}");
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public string GetToken()
        {
            string? expiry = _localStorage.GetItem<string>("authTokenExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return _localStorage.GetItem<string>("authToken");
                }
                else
                {
                    SetToken(null);
                }
            }

            return null;
        }

        public UserType? GetUserType()
        {
            string? expiry = _localStorage.GetItem<string>("authTokenExpiry");

            if (expiry != null && !string.IsNullOrWhiteSpace(expiry))
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return _localStorage.GetItem<UserType>("authUserType");
                }
                else
                {
                    SetToken(null);
                }
            }

            return null;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine($"CHAMO O METODO DEFAULT: {userType}");
            string? token = "";
            ClaimsIdentity? identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            AuthenticationState? auth = new(new ClaimsPrincipal(identity));

            return auth;
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            string? payload = jwt.Split('.')[1];
            byte[]? jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
