﻿using OpenQA.Selenium;

namespace Atata
{
    public class WaitForElementByClassAttribute : WaitForElementAttribute
    {
        public WaitForElementByClassAttribute(string value, TriggerEvent on, TriggerPriority priority = TriggerPriority.Medium, TriggerScope applyTo = TriggerScope.Self)
            : base(By.ClassName(value), on, priority, applyTo)
        {
        }
    }
}