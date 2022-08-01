using System;
using System.Collections.ObjectModel;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
    public void AddProductToBasket()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");

      // Act
      // Add single product to basket.
      IWebElement addToBasketButton = _webDriver.FindElement(By.LinkText("Add to basket"));
      addToBasketButton.Click();

      // Check basket quantity.
      IWebElement basketButton = _webDriver.FindElement(By.LinkText("Basket (1)"));

      // Assert
      Assert.NotNull(basketButton);
    }

    [Fact]
    public void SearchForProdukt()
    {
      // Arrange
      _webDriver.Navigate().GoToUrl(_URL_HOME_STR + "/Locomotives");
      
      string searchStr = "Re 460";

      // Act
      IWebElement searchBox = _webDriver.FindElement(By.Id("QueryOptions_SearchString"));
      searchBox.SendKeys(searchStr);

      IWebElement searchButton = _webDriver.FindElement(By.Id("search-button"));
      searchButton.Click();

      var results = _webDriver.FindElements(By.ClassName("card-title"));

      // Assert
      foreach (var result in results)
      {
        Assert.Contains(searchStr, result.Text);
      }
    }
  }
}
