using CustomerAuth.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CustomerAuth.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Useraccount NewUser { get; set; } = new Useraccount();

        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public SignUpModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the form errors.";
                return Page();
            }

            // Check if email or username already exists
            bool emailExists = await _context.User_Accounts.AnyAsync(u => u.Email == NewUser.Email);
            bool usernameExists = await _context.User_Accounts.AnyAsync(u => u.username == NewUser.username);

            if (emailExists)
            {
                ErrorMessage = "This email is already registered.";
                return Page();
            }

            if (usernameExists)
            {
                ErrorMessage = "This username is already taken.";
                return Page();
            }

            _context.User_Accounts.Add(NewUser);
            await _context.SaveChangesAsync();

            // Set session and redirect to profile
            HttpContext.Session.SetString("UserEmail", NewUser.Email);

            SuccessMessage = "Account created successfully!";
            return RedirectToPage("/Profile");
        }
    }
}
