using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.BDD.Tests.Config
{
    public abstract class PageObjectModel
    {
        protected readonly SeleniumHelper Helper;

        protected PageObjectModel(SeleniumHelper helper)
        {
            Helper = helper;
        }
        public string ObterUrl()
            => Helper.ObterUrl();

        public void IrParaUrl(string url)
        {
            Helper.IrParaUrl(url);
        }
    }
}
