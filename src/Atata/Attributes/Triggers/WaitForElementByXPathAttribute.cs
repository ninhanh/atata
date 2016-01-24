﻿using OpenQA.Selenium;

namespace Atata
{
    public class WaitForElementByXPathAttribute : WaitForElementAttribute
    {
        public WaitForElementByXPathAttribute(string value, TriggerEvent on, TriggerPriority priority = TriggerPriority.Medium, TriggerScope applyTo = TriggerScope.Self)
            : base(By.XPath(value), on, priority, applyTo)
        {
        }
    }
}