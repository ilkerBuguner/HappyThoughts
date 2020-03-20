namespace HappyThoughts.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Models.Enumerations;
    using HappyThoughts.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public partial class IndexModel : PageModel
    {
        private const int UserFullNameMaxLength = 50;
        private const int UserBiographyMaxLength = 300;
        private const int UserLocationMaxLength = 100;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICloudinaryService cloudinaryService,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.cloudinaryService = cloudinaryService;
            this.userRepository = userRepository;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [MaxLength(50)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Birthday")]
            public DateTime Birthday { get; set; }

            [Display(Name = "Profile Picture")]
            public IFormFile ProfilePicture { get; set; }

            public string ProfilePictureUrl { get; set; }

            [MaxLength(300)]
            [Display(Name = "Biography")]
            public string Biography { get; set; }

            [Display(Name = "Gender")]
            public string Gender { get; set; }

            [MaxLength(100)]
            [Display(Name = "Location")]
            public string Location { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);

            this.Username = userName;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FullName = user.FullName,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Biography = user.Biography,
                Birthday = user.Birthday,
                Gender = user.Gender.ToString(),
                Location = user.Location,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await this._userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            var currentUser = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.Input.ProfilePicture != null)
            {
                var pictureUrl = await this.cloudinaryService.UploadPhotoAsync(
                this.Input.ProfilePicture,
                $"{user.UserName}-{Guid.NewGuid().ToString()}");

                this.Input.ProfilePictureUrl = pictureUrl;

                currentUser.ProfilePictureUrl = this.Input.ProfilePictureUrl;
            }

            if (this.Input.FullName != null)
            {
                if (this.Input.FullName.Length > UserFullNameMaxLength)
                {
                    var userId = await this._userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Full Name length must be smaller than or equal to {UserFullNameMaxLength} characters!");
                }

                currentUser.FullName = this.Input.FullName;
            }
            else
            {
                currentUser.FullName = string.Empty;
            }

            if (this.Input.Biography != null)
            {
                if (this.Input.Biography.Length > UserBiographyMaxLength)
                {
                    var userId = await this._userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Biography length must be smaller than or equal to {UserBiographyMaxLength} characters!");
                }

                currentUser.Biography = this.Input.Biography;
            }
            else
            {
                currentUser.Biography = string.Empty;
            }

            if (this.Input.Location != null)
            {
                if (this.Input.Location.Length > UserLocationMaxLength)
                {
                    var userId = await this._userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Location length must be smaller than or equal to {UserLocationMaxLength} characters!");
                }

                currentUser.Location = this.Input.Location;
            }
            else
            {
                currentUser.Location = string.Empty;
            }

            if (this.Input.Gender != "Not Selected")
            {
                var gender = Enum.Parse<Gender>(this.Input.Gender);

                currentUser.Gender = gender;
            }

            if (!this.Input.Birthday.ToString().Contains("1.1.0001"))
            {
                var userFromDb = this.userRepository.GetByIdWithDeletedAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var birthdayFromDb = userFromDb.Result.Birthday;

                if (birthdayFromDb != this.Input.Birthday)
                {
                    var birthdayParsed = DateTime.Parse(this.Input.Birthday.ToString());

                    currentUser.Birthday = birthdayParsed;
                }
            }

            this.userRepository.Update(currentUser);
            await this.userRepository.SaveChangesAsync();

            await this._signInManager.RefreshSignInAsync(user);

            // StatusMessage = "Your profile has been updated";
            return this.Redirect($"/Users/Profile/{currentUser.Id}");
        }
    }
}
