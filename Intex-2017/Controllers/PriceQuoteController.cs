using Intex_2017.Models;
using Intex_2017.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intex_2017.DAL;

namespace Intex_2017.Controllers
{
    public class PriceQuoteController : Controller
    {
        private IntexContext db = new IntexContext();

        // GET: PriceQuote
        public ActionResult Index(List<int> assayIDList, int compoundLTNumber)
        {
            PriceQuoteViewModel priceQuoteVM = new PriceQuoteViewModel();
            List<SampleTest> tempSampleTestList = new List<SampleTest>();

            tempSampleTestList = db.SampleTests.ToList();

            for (int i = 0; i < tempSampleTestList.Count(); i++)
            {
                if (assayIDList.Contains(tempSampleTestList[i].AssayID) && tempSampleTestList[i].LTNumber == compoundLTNumber)
                {
                    priceQuoteVM.AssayCompoundList.Add(tempSampleTestList[i]);
                }
            }

            return View(priceQuoteVM);
        }
    }
}