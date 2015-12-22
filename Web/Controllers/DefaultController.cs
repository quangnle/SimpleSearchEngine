using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.SearchEngine;

namespace Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public ActionResult SearchResult(string query)
        {
            ViewBag.Query = query;

            var suggestions = new SearchSuggestor { Metric = new LevenshteinMetric() }.Suggest(query, 2);
            ViewBag.Suggestions = suggestions;

            var result = SearchCore.Search(query);
            ViewBag.Result = result;

            return View();
        }

        public ActionResult UpdateSearchIndex()
        {
            SEArticleRepository repo = new SEArticleRepository();
            var list = repo.GetAllArticles();

            SearchCore.ClearLuceneIndex();
            SearchCore.AddUpdateLuceneIndex(list);
            SearchCore.Optimize();
            return View();
        }
    }   
}