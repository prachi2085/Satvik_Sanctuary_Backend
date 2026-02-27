using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;

namespace HealthWellness.Services
{
    public class ArticleService : IArticleService
    {
        private readonly WellnessDbContext _db;

        public ArticleService(WellnessDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Article> GetAll()
            => _db.Articles.OrderByDescending(a => a.CreatedAt).ToList();

        public Article? GetById(int id)
            => _db.Articles.Find(id);

        public Article Create(ArticleDto dto)
        {
            var article = new Article
            {
                Title = dto.Title,
                Description = dto.Description,
                Content = dto.Content,
                MediumUrl = dto.MediumUrl,
                CreatedAt = DateTime.UtcNow
            };

            _db.Articles.Add(article);
            _db.SaveChanges();

            return article;
        }

        public Article? Update(int id, ArticleDto dto)
        {
            var article = _db.Articles.Find(id);
            if (article == null) return null;

            article.Title = dto.Title;
            article.Description = dto.Description;
            article.Content = dto.Content;
            article.MediumUrl = dto.MediumUrl;

            _db.SaveChanges();
            return article;
        }

        public bool Delete(int id)
        {
            var article = _db.Articles.Find(id);
            if (article == null) return false;

            _db.Articles.Remove(article);
            _db.SaveChanges();
            return true;
        }
    }
}
