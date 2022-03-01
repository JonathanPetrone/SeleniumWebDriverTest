
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;


[TestFixture]
public class INCEPTIVESeleniumTest
{
    private IWebDriver driver;
    public IDictionary<string, object> vars { get; private set; }
    private IJavaScriptExecutor js;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }
    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }
    public string waitForWindow(int timeout)
    {
        try
        {
            Thread.Sleep(timeout);
        }
        catch (Exception e)
        {
            Console.WriteLine("{0} Exception caught.", e);
        }
        var whNow = ((IReadOnlyCollection<object>)driver.WindowHandles).ToList();
        var whThen = ((IReadOnlyCollection<object>)vars["WindowHandles"]).ToList();
        if (whNow.Count > whThen.Count)
        {
            return whNow.Except(whThen).First().ToString();
        }
        else
        {
            return whNow.First().ToString();
        }
    }

    [Test]
    public void checkDreamTeam()
    {
        driver.Navigate().GoToUrl("https://www.inceptive.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        var elements = driver.FindElements(By.XPath("//img[@alt=\'INCEPTIVE - WE EMPOWER TEAMS WITH HIGHLY SKILLED QA PROFESSIONALS\']"));
        Assert.True(elements.Count > 0);
        driver.FindElement(By.LinkText("MEET THE DREAM TEAM")).Click();
        Assert.That(driver.FindElement(By.XPath("//section[@id=\'dev-section\']/div[5]/div[2]/div/span")).Text, Is.EqualTo("MARTIN Jansson"));
        Assert.That(driver.FindElement(By.CssSelector(".person-div:nth-child(15) strong")).Text, Is.EqualTo("JONATHAN Petrone"));
        var jonathanbild = driver.FindElements(By.XPath("//img[contains(@src,\'https://uploads-ssl.webflow.com/608fa9bc5a9b7b5a40b07c92/61b886b8ec1cf1623463a015_Jonathan%20Petrone.png\')]"));
        Assert.True(jonathanbild.Count > 0);
        var martinbild = driver.FindElements(By.XPath("//img[contains(@src,\'https://uploads-ssl.webflow.com/608fa9bc5a9b7b5a40b07c92/611f8dd02d36effd872c8357_Martin%20Jansson_l.png\')]"));
        Assert.True(martinbild.Count > 0);
    }

    [Test]
    public void checkJC()
    {
        driver.Navigate().GoToUrl("https://www.inceptive.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        vars["WindowHandles"] = driver.WindowHandles;
        driver.FindElement(By.CssSelector(".menu-link:nth-child(3) > .menu-txt")).Click();
        vars["win2681"] = waitForWindow(2000);
        driver.SwitchTo().Window(vars["win2681"].ToString());
        driver.FindElement(By.CssSelector(".px-6")).Click();
        var elementsStart = driver.FindElements(By.LinkText("Start"));
        Assert.True(elementsStart.Count > 0);
        var elementsDep = driver.FindElements(By.LinkText("Departments"));
        Assert.True(elementsDep.Count > 0);
        var elementsPlatser = driver.FindElements(By.LinkText("Platser"));
        Assert.True(elementsPlatser.Count > 0);
        var elementsJobb = driver.FindElements(By.LinkText("Jobb"));
        Assert.True(elementsJobb.Count > 0);
        driver.FindElement(By.CssSelector(".px-6")).Click();
        var elementsConnect = driver.FindElements(By.LinkText("Connect"));
        Assert.True(elementsConnect.Count > 0);
        var elementsEmail = driver.FindElements(By.Id("full_email"));
        Assert.True(elementsEmail.Count > 0);
        driver.FindElement(By.CssSelector(".fa-circle-chevron-right")).Click();
        var elements = driver.FindElements(By.CssSelector(".pt-12"));
        Assert.True(elements.Count > 0);
    }

    [Test]
    public void checkSkills()
    {
        driver.Navigate().GoToUrl("https://www.inceptive.se/");
        driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
        Assert.That(driver.FindElement(By.CssSelector(".menu-link:nth-child(2) > .menu-txt")).Text, Is.EqualTo("SKILLS"));
        driver.FindElement(By.CssSelector(".menu-link:nth-child(2) > .menu-txt")).Click();
        driver.FindElement(By.CssSelector(".div-block-37 .btn-dark")).Click();
        IWebElement popUpAdress = driver.FindElement(By.CssSelector("body > div.popup > div > div.popup-top > div.pop-up-adress > div:nth-child(1) > a.link-block-20.w-inline-block > div"));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30000);
        Assert.That(popUpAdress.Text, Is.EqualTo("HELLO@INCEPTIVE.SE"));
    }
}
