﻿using System;
using System.Linq;

namespace Atata
{
    public static class Go
    {
        public static T To<T>(T pageObject = null, string url = null, bool navigate = true, bool temporarily = false)
            where T : PageObject<T>
        {
            return To(pageObject, new GoOptions { Url = url, Navigate = string.IsNullOrWhiteSpace(url) && navigate, Temporarily = temporarily });
        }

        public static T ToWindow<T>(T pageObject, string windowName, bool temporarily = false)
            where T : PageObject<T>
        {
            return To(pageObject, new GoOptions { Navigate = false, WindowName = windowName, Temporarily = temporarily });
        }

        public static T ToWindow<T>(string windowName, bool temporarily = false)
            where T : PageObject<T>
        {
            return To<T>(null, new GoOptions { Navigate = false, WindowName = windowName, Temporarily = temporarily });
        }

        public static T ToNextWindow<T>(T pageObject = null, bool temporarily = false)
            where T : PageObject<T>
        {
            string windowHandle = AtataContext.Current.Driver.WindowHandles.
                SkipWhile(x => x != AtataContext.Current.Driver.CurrentWindowHandle).
                ElementAt(1);

            return To(pageObject, new GoOptions { Navigate = false, WindowName = windowHandle, Temporarily = temporarily });
        }

        public static T ToPreviousWindow<T>(T pageObject = null, bool temporarily = false)
            where T : PageObject<T>
        {
            string windowHandle = AtataContext.Current.Driver.WindowHandles.
                Reverse().
                SkipWhile(x => x != AtataContext.Current.Driver.CurrentWindowHandle).
                ElementAt(1);

            return To(pageObject, new GoOptions { Navigate = false, WindowName = windowHandle, Temporarily = temporarily });
        }

        private static T To<T>(T pageObject, GoOptions options)
            where T : PageObject<T>
        {
            if (AtataContext.Current.PageObject == null)
            {
                pageObject = pageObject ?? Activator.CreateInstance<T>();
                AtataContext.Current.PageObject = pageObject;

                if (!string.IsNullOrWhiteSpace(options.Url))
                {
                    AtataContext.Current.Log.Info("Go to URL '{0}'", options.Url);
                    AtataContext.Current.Driver.Navigate().GoToUrl(options.Url);
                }

                pageObject.Init(new PageObjectContext(AtataContext.Current.Driver, AtataContext.Current.Log));
                return pageObject;
            }
            else
            {
                IPageObject currentPageObject = (IPageObject)AtataContext.Current.PageObject;
                T newPageObject = currentPageObject.GoTo(pageObject, options);
                AtataContext.Current.PageObject = newPageObject;
                return newPageObject;
            }
        }
    }
}