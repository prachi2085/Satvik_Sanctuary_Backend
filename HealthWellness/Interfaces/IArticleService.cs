using HealthWellness.DTOs;
using HealthWellness.Models;
using HealthWellness.Interfaces;

namespace HealthWellness.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAll();
        Article? GetById(int id);
        Article Create(ArticleDto dto);
        Article? Update(int id, ArticleDto dto);
        bool Delete(int id);
    }
}
