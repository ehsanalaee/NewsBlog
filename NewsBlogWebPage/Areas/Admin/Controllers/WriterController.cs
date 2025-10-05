using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace NewsBlogWebPage.Areas.Admin.Controllers;
[Area("Admin")]
public class WriterController : Controller
{
    private readonly IBaseRepository<Writer> _repo;
    public WriterController(IBaseRepository<Writer> repo) => _repo = repo;

    public IActionResult Index() => View(_repo.GetAll().ToList());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Writer model)
    {
        if (!ModelState.IsValid) return View(model);
        _repo.Create(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Writer model)
    {
        if (!ModelState.IsValid) return View(model);
        _repo.Edit(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _repo.GetById(id);
        if (item == null) return NotFound();
        _repo.Delete(item);
        return RedirectToAction(nameof(Index));
    }

}