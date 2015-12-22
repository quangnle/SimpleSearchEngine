using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Models.SEModels;

namespace Web.Models
{
    public class SEArticleRepository
    {

        public SEArticleRepository()
        {}

        public List<SEArticle> GetAllArticles()
        {
            using (var context = new Model())
            {
                return context.Articles.Select(a => new SEArticle { Id = a.Id, Title = a.Title, Body = a.Body }).ToList();
            }
        }

        public List<string> GetAllKeywords()
        {
            using (var context = new Model())
            {
                HashSet<string> keywords = new HashSet<string>();
                
                var articles = context.Articles.ToList();
                foreach (var article in articles)
                {
                    var words = article.Keywords.Split(',');
                    foreach (var word in words)
                    {
                        if (!keywords.Contains(word.ToLower()))
                            keywords.Add(word.ToLower());
                    }
                }

                return keywords.ToList();
            }
        }

        public List<string> GetAllKeywordsSingle()
        {
            using (var context = new Model())
            {
                HashSet<string> keywords = new HashSet<string>();

                var articles = context.Articles.ToList();
                foreach (var article in articles)
                {
                    var words = article.Keywords.Split(',');
                    foreach (var word in words)
                    {
                        var singleWords = word.Split(' ');
                        foreach (var sWord in singleWords)
                        {
                            if (!keywords.Contains(sWord.ToLower()))
                                keywords.Add(sWord.ToLower());
                        }
                    }
                }

                return keywords.ToList();
            }
        }

        public SEArticle GetArticleById(int id)
        {
            using (var context = new Model())
            {
                return context.Articles.Where(a=> a.Id == id).Select(a => new SEArticle { Id = a.Id, Title = a.Title, Body = a.Body }).FirstOrDefault();
            }
        }
    }
}
