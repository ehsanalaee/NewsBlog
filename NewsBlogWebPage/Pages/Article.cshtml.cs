using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace NewsBlogWebPage.Pages
{
    public class ArticleModel : PageModel
    {
        IArticleRepository _articleRepo;

        public ArticleModel(ArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        public Article?  article{ get; set; }
        public List<Article>? relatedArticles { get; set; }
        public List<Article>? popularArticles { get; set; }



        public async Task OnGet(int id)
        {
            article = await _articleRepo.GetById(id);

            // Add null check before using article
            if (article != null)
            {
                _articleRepo.RaisedView(article);
                relatedArticles = await _articleRepo.GetRelated(article);
            }

            popularArticles = await _articleRepo.GetMostView();

            // Ensure lists are never null
            relatedArticles ??= new List<Article>();
            popularArticles ??= new List<Article>();
        }
    }
    }

