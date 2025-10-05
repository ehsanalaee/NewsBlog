using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsBlogWebPage.Areas.Admin.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlogWebPage.Areas.Admin.Controllers;
[Area("Admin")]
public class ArticleController : Controller
{
    private readonly IBaseRepository<Article> _articleRepo;
    private readonly IBaseRepository<Category> _categoryRepo;
    private readonly IBaseRepository<Writer> _writerRepo;
    private readonly IWebHostEnvironment _env;

    public ArticleController(
        IBaseRepository<Article> articleRepo,
        IBaseRepository<Category> categoryRepo,
        IBaseRepository<Writer> writerRepo,
        IWebHostEnvironment env)
    {
        _articleRepo = articleRepo;
        _categoryRepo = categoryRepo;
        _writerRepo = writerRepo;
        _env = env;
    }

    public IActionResult Index()
    {
        var list = _articleRepo.GetAll()
            .OrderByDescending(a => a.DateCreated)
            .ToList();
        return View(list);
    }

    public async Task<IActionResult> Details(int id)
    {
        var model = await _articleRepo.GetById(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public IActionResult Create()
    {
        PopulateSelectLists();
        return View(new ArticleCreateEditVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ArticleCreateEditVM vm)
    {
        if (!ModelState.IsValid)
        {
            PopulateSelectLists();
            return View(vm);
        }

        var article = new Article
        {
            Title = vm.Title,
            Text = vm.Text,
            CategoryId = vm.CategoryId,
            WriterId = vm.WriterId,
            DateCreated = DateTime.UtcNow,
            ViewCount = 0
        };

        if (vm.Image != null)
        {
            var fileName = SaveImage(vm.Image);
            article.ImageName = fileName;
        }

        _articleRepo.Create(article);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var article = await _articleRepo.GetById(id);
        if (article == null) return NotFound();
        var vm = new ArticleCreateEditVM
        {
            Id = article.Id,
            Title = article.Title,
            Text = article.Text,
            CategoryId = article.CategoryId,
            WriterId = article.WriterId,
            ExistingImageName = article.ImageName
        };
        PopulateSelectLists();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ArticleCreateEditVM vm)
    {
        if (id != vm.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            PopulateSelectLists();
            return View(vm);
        }
        var article = await _articleRepo.GetById(id);
        if (article == null) return NotFound();

        article.Title = vm.Title;
        article.Text = vm.Text;
        article.CategoryId = vm.CategoryId;
        article.WriterId = vm.WriterId;

        if (vm.Image != null)
        {
            var newName = SaveImage(vm.Image);
            DeleteImage(article.ImageName);
            article.ImageName = newName;
        }

        _articleRepo.Edit(article);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var article = await _articleRepo.GetById(id);
        if (article == null) return NotFound();
        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var article = await _articleRepo.GetById(id);
        if (article == null) return NotFound();
        DeleteImage(article.ImageName);
        await _articleRepo.Delete(article);
        return RedirectToAction(nameof(Index));
    }

    private void PopulateSelectLists()
    {
        ViewBag.Categories = _categoryRepo.GetAll().Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
        ViewBag.Writers = _writerRepo.GetAll().Select(w => new SelectListItem { Value = w.Id.ToString(), Text = w.Name }).ToList();
    }

    private string? SaveImage(IFormFile? file)
    {
        if (file == null || file.Length == 0) return null;
        var uploads = Path.Combine(_env.WebRootPath, "img");
        Directory.CreateDirectory(uploads);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploads, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);
        return fileName;
    }

    private void DeleteImage(string? imageName)
    {
        if (string.IsNullOrEmpty(imageName)) return;
        var uploads = Path.Combine(_env.WebRootPath, "img");
        var path = Path.Combine(uploads, imageName);
        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }
}