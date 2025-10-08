using Microsoft.AspNetCore.Mvc;

namespace NewsBlogWebPage
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [Route("/Upload/Article")]
        public async Task<IActionResult> Article(IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
                return BadRequest(new { error = "No file uploaded" });

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(upload.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            var imageUrl = Url.Content($"~/uploads/{fileName}");
            return new JsonResult(new { uploaded = true, url = imageUrl }); // ✅ This is the fix
        }
    }
}
