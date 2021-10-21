using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using ServiceLayer.ProductService.DTO;

namespace WebApp.Helpers
{
    public static class Session
    {
        public static readonly string USER_ID = "user_id";
        public static readonly string USERNAME = "username";
        public static readonly string IS_ADMIN = "is_admin";

        public static readonly string BASKET = "basket";

        /// <summary>
        /// Serialize object to string.
        /// </summary>
        /// <typeparam name="T">Type of object to serialize.</typeparam>
        /// <param name="obj"><typeparamref name="T"/> to serialize.</param>
        /// <returns>Serialized object.</returns>
        public static string Serialize<T>(this T obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception e)
            {
                // TODO: Log exception.
            }

            return "";
        }

        /// <summary>
        /// Deserialize string to object.
        /// </summary>
        /// <typeparam name="T">Newable object.</typeparam>
        /// <param name="str"><see cref="string"/> to deserialize.</param>
        /// <returns>Deserialized <typeparamref name="T"/>; otherwise new <typeparamref name="T"/>.</returns>
        public static T Deserialize<T>(this string str) where T : new()
        {
            try
            {
                return JsonSerializer.Deserialize<T>(str);
            }
            catch (Exception e)
            {
                // TODO: Log exception.
            }

            return new T();
        }

        #region User
        /// <summary>
        /// Check whether current user has admin rights.
        /// </summary>
        /// <param name="httpContext">Current <see cref="HttpContext"/>.</param>
        /// <returns>True if user is admin; otherwise false.</returns>
        public static bool IsAdmin(this HttpContext httpContext) =>
            (httpContext.Session.GetInt32(IS_ADMIN) ?? 0) > 0;
        #endregion

        #region Basket
        /// <summary>
        /// Get basket of the session.
        /// </summary>
        /// <param name="httpContext">Current <see cref="HttpContext"/>.</param>
        /// <returns><see cref="SessionBasketDto"/> with added products.</returns>
        public static SessionBasketDto GetBasket(this HttpContext httpContext) =>
            httpContext.Session.GetString(BASKET).Deserialize<SessionBasketDto>();

        /// <summary>
        /// Set basket of the session.
        /// </summary>
        /// <param name="httpContext">Current <see cref="HttpContext"/>.</param>
        /// <param name="basket">Basket to set.</param>
        public static void SetBasket(this HttpContext httpContext, SessionBasketDto basket)
        {
            httpContext.Session.SetString(BASKET, basket.Serialize());
        }

        /// <summary>
        /// Add product to current session basket.
        /// </summary>
        /// <param name="httpContext">Current <see cref="HttpContext"/>.</param>
        /// <param name="productId">Product ID.</param>
        public static void AddProductToBasket(this HttpContext httpContext, int productId)
        {
            SessionBasketDto basket = httpContext.GetBasket();

            if (basket.Products.ContainsKey(productId))
            {
                basket.Products[productId]++;
            }
            else
            {
                basket.Products.Add(productId, 1);
            }

            httpContext.SetBasket(basket);
        }
        #endregion
    }
}
