using Blazored.LocalStorage;
using ServiceLayer.ProductService;
using ServiceLayer.ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.Helpers.LocalStorage
{
    public class Basket
    {
        private static readonly string BASKET = "basket";

        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public Basket(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        #region Add
        public async Task AddAsync(int id)
        {
            if (await _localStorageService.ContainKeyAsync(BASKET))
            {
                SessionBasketDto basket = await GetBasketAsync();

                try
                {
                    uint stock = await _httpClient.GetFromJsonAsync<uint>($"product/stock/{id}");

                    if (basket.Products.ContainsKey(id) && basket.Products[id] < stock)
                    {
                        basket.Products[id]++; // Add to existing basket
                        SetBasketAsync(basket);
                    }
                    else if (stock >= 1)
                    {
                        basket.Products.Add(id, 1); // Add new product to basket
                        SetBasketAsync(basket);
                    }
                }
                catch (Exception) { }
            }
            else
            {
                // Add new basket with new product
                SessionBasketDto basket = new();
                basket.Products.Add(id, 1);
                SetBasketAsync(basket);
            }
        }
        #endregion

        #region Remove
        public async Task RemoveAsync(int id)
        {
            if (await _localStorageService.ContainKeyAsync(BASKET))
            {
                SessionBasketDto basket = await GetBasketAsync();
                if (basket.Products.ContainsKey(id))
                {
                    if (basket.Products[id] > 1)
                    {
                        basket.Products[id]--; // Decrement
                    }
                    else
                    {
                        basket.Products.Remove(id); // Remove
                        return;
                    }
                    
                    SetBasketAsync(basket);
                }
            }
        }

        public async Task RemoveAllAsync(int id)
        {
            if (await _localStorageService.ContainKeyAsync(BASKET))
            {
                SessionBasketDto basket = await GetBasketAsync();
                if (basket.Products.Remove(id))
                {
                    SetBasketAsync(basket);
                }
            }
        }
        #endregion

        #region Get
        public async Task<int> GetCountAsync()
        {
            int count = 0;

            if (await _localStorageService.ContainKeyAsync(BASKET))
            {
                SessionBasketDto basket = await GetBasketAsync();
                
                foreach (var item in basket.Products)
                {
                    count += item.Value;
                }
            }

            return count;
        }

        public async Task<List<BasketProductDto>> GetBasketProductsAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/product/basket", await GetBasketAsync());
                string content = await response.Content.ReadAsStringAsync();

                return content.Deserialize<List<BasketProductDto>>(true);
            }
            catch (Exception)
            {
                return new List<BasketProductDto>();
            }
        }

        public async Task<SessionBasketDto> GetBasketAsync()
        {
            return await _localStorageService.GetItemAsync<SessionBasketDto>(BASKET);
        }

        private async void SetBasketAsync(SessionBasketDto basket)
        {
            await _localStorageService.SetItemAsync(BASKET, basket);
        }
        #endregion

        #region Set
        public async Task SetCountAsync(int id, int count)
        {
            if (await _localStorageService.ContainKeyAsync(BASKET))
            {
                SessionBasketDto basket = await GetBasketAsync();

                try
                {
                    uint stock = await _httpClient.GetFromJsonAsync<uint>($"product/stock/{id}");

                    if (basket.Products.ContainsKey(id))
                    {
                        if (count <= 0)
                        {
                            await RemoveAllAsync(id);
                            return;
                        }
                        else if (count <= stock)
                        {
                            basket.Products[id] = count;
                        }
                        else if (count > stock)
                        {
                            basket.Products[id] = (int)stock;
                        }

                        SetBasketAsync(basket);
                    }
                }
                catch (Exception) { }
            }
        }
        #endregion
    }
}
