using CustomerAuth.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuth.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public Useraccount? User { get; set; }
        public string ProfileImage { get; set; } = "/images/default-user.png.jpg";
        public string Message { get; set; } = "";

        public ProfileModel(AppDbContext context)
        {
            _context = context;
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

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}