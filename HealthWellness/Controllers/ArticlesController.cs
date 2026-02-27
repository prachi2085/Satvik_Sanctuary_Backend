using HealthWellness.DTOs;
using HealthWellness.Helpers;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using HealthWellness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthWellness.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly MediumService _mediumService;
        private readonly IArticleService _service;

        public ArticlesController(MediumService mediumService, IArticleService service)
        {
            _mediumService = mediumService;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _mediumService.GetArticles();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var article = _service.GetById(id);
            if (article == null)
                return NotFound(ApiResponse<object>.Fail("Article not found"));

            return Ok(ApiResponse<Article>.Ok(article));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ArticleDto dto)
        {
            var article = _service.Create(dto);
            return Ok(ApiResponse<Article>.Ok(article, "Article created"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, ArticleDto dto)
        {
            var article = _service.Update(id, dto);
            if (article == null)
                return NotFound(ApiResponse<object>.Fail("Article not found"));

            return Ok(ApiResponse<Article>.Ok(article, "Article updated"));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.Delete(id);
            if (!deleted)
                return NotFound(ApiResponse<object>.Fail("Article not found"));

            return Ok(ApiResponse<object>.Ok(null, "Article deleted"));
        }
    }
}
