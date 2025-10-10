using CustomerAuth.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuth.Pages
{
    public class UserListModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UserListModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<Useraccount> Users { get; set; } = new();

        [BindProperty]
        public int EditUserId { get; set; }

        [BindProperty]
        public string First_name { get; set; }

        [BindProperty]
        public string Last_name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _context.User_Accounts.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostToggleBlockAsync(int id)
        {
            var user = await _context.User_Accounts.FindAsync(id);
            if (user != null)
            {
                user.IsBlocked = !user.IsBlocked;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _context.User_Accounts.FindAsync(id);
            if (user != null)
            {
                _context.User_Accounts.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var user = await _context.User_Accounts.FindAsync(EditUserId);
            if (user == null) return RedirectToPage();

            user.First_name = First_name;
            user.Last_name = Last_name;
            user.Email = Email;
            user.username = username;

            if (ProfileImage != null)
            {
                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ProfileImage.FileName)}";
                var filePath = Path.Combine(uploadsDir, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ProfileImage.CopyToAsync(stream);

                user.ProfileImagePath = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
