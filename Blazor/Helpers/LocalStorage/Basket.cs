using Blazored.LocalStorage;
using Blazored.Toast.Services;
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
        private readonly IToastService _toastService;

        public Basket(ILocalStorageService localStorageService, HttpClient httpClient, IToastService toastService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _toastService = toastService;
        }

        #region Add
        public async Task AddAsync(int id)
        {
            try
            {
                uint stock = await _httpClient.GetFromJsonAsync<uint>($"product/stock/{id}");
                string name = await _httpClient.GetStringAsync($"product/name/{id}");

                if (await _localStorageService.ContainKeyAsync(BASKET))
                {
                    SessionBasketDto basket = await GetBasketAsync();

                    if (basket.Products.ContainsKey(id))
                    {
                        if (basket.Products[id] < stock)
                        {
                            basket.Products[id]++; // Add to existing basket
                            SetBasketAsync(basket);
                            ToastAddAnotherProduct(name);
                            return;
                        }
                    }
                    else if (stock >= 1)
                    {
                        basket.Products.Add(id, 1); // Add new product to basket
                        SetBasketAsync(basket);
                        ToastAddNewProduct(name);
                        return;
                    }
                }
                else
                {
                    // Add new basket with new product
                    SessionBasketDto basket = new();
                    basket.Products.Add(id, 1);
                    SetBasketAsync(basket);
                    ToastAddNewProduct(name);
                    return;
                }
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message, Globals.SYSTEM_ERROR);
                return;
            }

            ToastNotEnoughStock();
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
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"product/basket", await GetBasketAsync());
                string content = await response.Content.ReadAsStringAsync();

                return content.Deserialize<List<BasketProductDto>>(true);
            }
            catch (Exception e)
            {
                _toastService.ShowError(e.Message, Globals.SYSTEM_ERROR);
                return new List<BasketProductDto>();
            }
        }

        private async Task<SessionBasketDto> GetBasketAsync()
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
                            ToastNotEnoughStock();
                        }

                        SetBasketAsync(basket);
                    }
                }
                catch (Exception e)
                {
                    _toastService.ShowError(e.Message, Globals.SYSTEM_ERROR);
                }
            }
        }
        #endregion

        #region Toast
        private void ToastAddNewProduct(string name)
        {
            _toastService.ShowSuccess($"Added {name} to basket.");
        }

        private void ToastAddAnotherProduct(string name)
        {
            _toastService.ShowSuccess($"Added another {name} to basket.");
        }
        
        private void ToastNotEnoughStock()
        {
            _toastService.ShowError(Globals.NOT_ENOUGH_STOCK, Globals.SORRY);
        }
        #endregion
    }
}
