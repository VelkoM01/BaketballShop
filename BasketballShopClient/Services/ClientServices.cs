using BasketballShopClient.Authentication;
using BasketballShopSharedLibrary.Contracts;
using BasketballShopSharedLibrary.DTOs;
using BasketballShopSharedLibrary.Models;
using BasketballShopSharedLibrary.Response;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;

namespace BasketballShopClient.Services
{
    public class ClientServices(HttpClient httpClient, ILocalStorageService localStorage) : IProduct, IUserAccountService
    {
        private const string BaseUrl = "api/product";
        private const string AuthenticationBaseUrl = "api/account";

        public async Task<ServiceResponse> AddProduct(Product model)
        {
        var response = await httpClient.PostAsync(BaseUrl, General.GenerateStringContent(General.SerializeObj(model)));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse(false, "Error occured. Try again later...");
            }
        var apiResponse = await response.Content.ReadAsStringAsync();
            return General.DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts()
        {
        var response = await httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode)
            {
                return null!;
            }
        var result = await response.Content.ReadAsStringAsync();
            return [.. General.DeserializeJsonStringList<Product>(result)];
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Product>($"{BaseUrl}/{id}");
                if (response == null)
                {
                    throw new Exception("Product not found");
                }
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching product details: {ex.Message}");
                return null!;
            }
        }


        private static async Task<string> ReadContent(HttpResponseMessage response) => await response.Content.ReadAsStringAsync();
        private static ServiceResponse CheckResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse(false, "Error occured. Try again later...");
            else          
            return new ServiceResponse(true, null!);
        }
        public async Task<ServiceResponse> Register(UserDTO model)
        {
            var response = await httpClient.PostAsync($"{AuthenticationBaseUrl}/register",
            General.GenerateStringContent(General.SerializeObj(model)));

            var result = CheckResponse(response);
            if (!result.Flag)
                return result;

            var apiResponse = await ReadContent(response);
            return General.DeserializeJsonString<ServiceResponse>(apiResponse);        
        }

        public async Task<LoginResponse> Login(LoginDTO model)
        {
            var response = await httpClient.PostAsync($"{AuthenticationBaseUrl}/login",
            General.GenerateStringContent(General.SerializeObj(model)));

            if (!response.IsSuccessStatusCode)
                return new LoginResponse(false, "Error occured", null!, null!);

            var apiResponse = await ReadContent(response);
            return General.DeserializeJsonString<LoginResponse>(apiResponse);
        }

        public async Task<ServiceResponse> Logout()
        {
            var response = await httpClient.PostAsync("api/account/logout", null);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse(false, "Error during logout. Please try again.");
            }

            await localStorage.RemoveItemAsync("accessToken");
            await localStorage.RemoveItemAsync("refreshToken");

            return new ServiceResponse(true, "Logout successful.");
        }

        public async Task UpdateProductQuantitiesAsync(List<Order> cartItems)
        {
            var response = await httpClient.PostAsync($"{BaseUrl}/update-quantities",
                General.GenerateStringContent(General.SerializeObj(cartItems)));

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error occurred while updating quantities.");
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
        }

    }
}
