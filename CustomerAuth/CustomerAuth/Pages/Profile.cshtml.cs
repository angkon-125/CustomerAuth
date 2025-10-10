using CustomerAuth.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuth.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public Useraccount? User { get; set; }
        public string ProfileImage { get; set; } = "/images/default-user.png";
        public string Message { get; set; } = "";

        public ProfileModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/SignIn");

            User = await _context.User_Accounts.FirstOrDefaultAsync(u => u.Email == email);
            if (User != null && !string.IsNullOrEmpty(User.ProfileImagePath))
                ProfileImage = "/uploads/" + User.ProfileImagePath;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile ProfileImage)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToPage("/SignIn");

            var user = await _context.User_Accounts.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return RedirectToPage("/SignIn");

            if (ProfileImage != null)
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ProfileImage.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImagePath = fileName;
                await _context.SaveChangesAsync();

                Message = "Profile image updated successfully!";
            }

            return RedirectToPage("/Profile");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
