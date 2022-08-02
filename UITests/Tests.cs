using System;
using System.Collections.ObjectModel;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using System.Collections.Generic;

namespace UITests
{
  public class UITests : IDisposable
  {
    const string _URL_BASE_STR = "https://localhost:";
    const string _PORT_STR = "44373";
    const string _URL_HOME_STR = _URL_BASE_STR + _PORT_STR;
    
    private IWebDriver _webDriver;

    public UITests()
    {
      new DriverManager().SetUpDriver(new EdgeConfig());
      _webDriver = new EdgeDriver();
    }

    public void Dispose()
    {
      _webDriver.Quit();
    }

    [Fact]
    public void LoadStartPage()
    {
      // Arrange

      // Act
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR);

      // Assert
      Assert.Equal("Home page - Choo Choo", _webDriver.Title);
      Assert.Equal(_URL_HOME_STR + "/", _webDriver.Url); 
    }

    [Fact]
    public void AddAnySingleProductToBasket()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.LinkText("Add to basket")).Click(); // Add single product to basket.

      // Assert
      Assert.NotNull(_webDriver.FindElement(By.LinkText("Basket (1)"))); // Assert basket quantity.
    }

    [Fact]
    public void EmptyBasket()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.LinkText("Add to basket")).Click(); // Add any product to basket.

      // Assert
      Assert.NotNull(_webDriver.FindElement(By.LinkText("Basket (1)"))); // Assert basket quantity.

      // Act
      _webDriver.FindElement(By.XPath("//i[@class='fas fa-times']")).Click(); // Remove product from basket.

      // Assert
      Assert.NotNull(_webDriver.FindElement(By.XPath("//i[normalize-space()='The basket is empty. . .']"))); // Assert basket is empty.
    }

    [Fact]
    public void AddSpecificSingleProductToBasketFromProductPage()
    {
      // Arrange
      const string PRODUCT_NAME_STR = "BR 52";

      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.Id("QueryOptions_SearchString")).SendKeys(PRODUCT_NAME_STR); // Enter search string.
      _webDriver.FindElement(By.Id("search-button")).Click(); // Initiate search.
      _webDriver.FindElement(By.XPath("//h5[@class='card-title']")).Click(); // Navigate to product page.

      _webDriver.FindElement(By.LinkText("Add to basket")).Click(); // Add single product to basket.

      IWebElement basketButton = _webDriver.FindElement(By.LinkText("Basket (1)")); // Check basket quantity.
      string addedProductNameStr = _webDriver.FindElement(By.XPath("//div[@class='col col-4']")).Text; // Get name of added product.

      // Assert
      Assert.NotNull(basketButton);
      Assert.Equal(PRODUCT_NAME_STR, addedProductNameStr);
    }

    [Fact]
    public void AddMultipleSpecificProductsToBasket()
    {
      // Arrange
      const string BASKET_LINK_STR = "Basket (2)";

      var productNames = new List<string>{
        { "BR 023 040-9" },
        { "Re 460 068-0" }
      };

      // Act
      foreach (var productName in productNames)
      {
        _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

        _webDriver.FindElement(By.Id("QueryOptions_SearchString")).SendKeys(productName); // Enter search string.
        _webDriver.FindElement(By.Id("search-button")).Click(); // Initiate search.
        _webDriver.FindElement(By.LinkText("Add to basket")).Click(); // Add product to basket.
      }

      // Assert
      Assert.NotNull(_webDriver.FindElement(By.LinkText(BASKET_LINK_STR))); // Assert basket quantity.
      foreach (var productName in productNames)
      {
        Assert.NotNull(_webDriver.FindElement(By.XPath("//div[normalize-space()='" + productName + "']"))); // Assert if product is found in the basket.
      }
    }

    [Fact]
    public void SearchForProdukt()
    {
      // Arrange
      const string SEARCH_STR = "Re 460";
      
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.Id("QueryOptions_SearchString")).SendKeys(SEARCH_STR); // Enter search string.
      _webDriver.FindElement(By.Id("search-button")).Click(); // Initiate search.

      var foundProductNames = _webDriver.FindElements(By.ClassName("card-title"));

      // Assert
      foreach (var product in foundProductNames)
      {
        Assert.Contains(SEARCH_STR, product.Text);
      }
    }

    [Fact]
    public void FilterForProductScaleTTEmpty()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[3]/div[2]/input[1]")).Click(); // Check scale 'TT'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/button[1]")).Click(); // Apply filters.

      var foundProducts = _webDriver.FindElements(By.XPath("//div[@class='card-body']"));

      // Assert
      Assert.Empty(foundProducts);
    }

    [Fact]
    public void FilterForProductWithScaleControlTypeEpoch()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[3]/div[1]/input[1]")).Click(); // Check scale 'HO'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[4]/div[3]/input[1]")).Click(); // Check control 'DC'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[5]/div[1]/input[1]")).Click(); // Check type 'Steam'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[6]/div[4]/input[1]")).Click(); // Check epoch 'IV'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/button[1]")).Click(); // Apply filters.

      int productCount = _webDriver.FindElements(By.XPath("//h5[@class='card-title']")).Count;
      IWebElement firstProductName = _webDriver.FindElement(By.XPath("//h5[@class='card-title']"));

      // Assert
      Assert.Equal(1, productCount);
      Assert.Contains("BR 023 040-9", firstProductName.Text);
    }

    [Fact]
    public void FilterAndSearchForProductWithScaleControlType()
    {
      // Arrange
      const string SEARCH_STR = "52 DB";

      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[3]/div[1]/input[1]")).Click(); // Check scale 'HO'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[4]/div[3]/input[1]")).Click(); // Check control 'DC'.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/div[5]/div[1]/input[1]")).Click(); // Check type 'Steam'.
      _webDriver.FindElement(By.Id("QueryOptions_SearchString")).SendKeys(SEARCH_STR); // Enter search string.
      _webDriver.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/div[1]/form[1]/div[1]/div[1]/button[1]")).Click(); // Apply filters.

      int productCount = _webDriver.FindElements(By.XPath("//h5[@class='card-title']")).Count;
      IWebElement firstProductName = _webDriver.FindElement(By.XPath("//h5[@class='card-title']"));

      // Assert
      Assert.Equal(1, productCount);
      Assert.Contains("BR 52, DB", firstProductName.Text);
    }

    [Fact]
    public void NavigateToAndReadDescriptionOnProduct()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      const string _SEARCH_STR = "Re 460 068-0";
      const string _EXPECTED_DESCRIPTION = "In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as \"Lok 2000\". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.";

      // Act
      _webDriver.FindElement(By.Id("QueryOptions_SearchString")).SendKeys(_SEARCH_STR); // Enter search string.
      _webDriver.FindElement(By.Id("search-button")).Click(); // Initiate search.
      _webDriver.FindElement(By.XPath("//h5[@class='card-title']")).Click(); // Navigate to product page.

      string readDescription = _webDriver.FindElement(By.XPath("//p[contains(text(),'In 1992, the first locomotive Re 460 of the Swiss ')]")).Text; // Get description.

      // Assert
      Assert.Equal(_EXPECTED_DESCRIPTION, readDescription);
    }
  }
}
