using AutoMapper;
using fooddotcomapi.Authorization;
using fooddotcomapi.Controllers;
using fooddotcomapi.Dto;
using fooddotcomapi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.IdentityModel.JsonWebTokens;
using AuthenticationPlugin;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace fooddotcomapi.Repositories
{
    public class AccountRepository : IAccountService
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private UrlEncoder _urlEncoder;
        private IMapper _mapper;
        private IMailSender _mailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        IUrlHelperFactory _urlHelperFactory;
        IActionContextAccessor _actionContextAccessor;
        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, 
            UrlEncoder urlEncoder, IMapper mapper, IMailSender mailSender, 
            IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor,
            IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _urlEncoder = urlEncoder;
            _mapper = mapper;
            _mailSender = mailSender;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets the list of all users
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IEnumerable<ApplicationUser> result, string ErrorMessage)> Get()
        {
            var result = await _userManager.Users.ToListAsync();
            if (result == null)
                return (false, null, "Roles have not been created");
            if (result.Count == 0)
                return (false, null, "No roles found to return");

            var users = result;// _mapper.Map<List<UserDTO>>(result);
            return (true, users, "");
        }

        public async Task<(bool IsSuccess, ApplicationUser result, string ErrorMessage)> Get(string email)
        {
            var result = await _userManager.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (result == null)
                return (false, null, "Roles have not been created");

            var user = result;// _mapper.Map<List<UserDTO>>(result);
            return (true, user, "");
        }

        /// <summary>
        /// Register New Users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> Register(RegisterDto model)
        {
            if (model != null)
            {
                if (await Exist(model.Email)) return (false, $"User with email: {model.Email} already exist");
                var userName = await _userManager.FindByNameAsync(model.UserName);
                if (userName!=null) return (false, $"Name: {model.UserName} is already taken");
                var userPhone = await _userManager.Users.Where(x => x.PhoneNumber == model.PhoneNumber).FirstOrDefaultAsync();
                if (userPhone != null) return (false, $"Phone number: {model.PhoneNumber} is already in use");
                var user = _mapper.Map<ApplicationUser>(model);
                user.DateCreated = DateTime.Now.Date;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return (true, "Account created successfully");
                    //var _urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //string _scheme = _urlHelper.ActionContext.HttpContext.Request.Scheme;

                    ////var callbackurl = _urlHelper.Action(action: nameof(AccountController.ResetPassword), controller: "Account", values: new { email = user.Email, code = code }, protocol: _scheme);
                    ////var callbackurl = _urlHelper.Action(action: nameof(AccountController.ConfirmEmail), controller: "Account", values: new { email = user.Email, code = code }, protocol: _scheme);
                    //var callbackurl = _urlHelper.Action("confirmemail", "Account", new { email = user.Email, code = code }, _scheme);

                    //var mailresult = _mailSender.SendEmail("fooddotcom@zayun.biz", model.Email, "Verify your Email", "Please confirm your account by clicking here: <a href=\"" + callbackurl + "\">Confirm your email</a>");
                    ////var mailresult = _mailSender.SendEmail(model.Email, "Verify your Email", "Please confirm your account by clicking here: <a href=\"" + callbackurl + "\">Confirm your email</a>");
                    //if (mailresult.IsSuccess)
                    //{
                    //    // return (true, "Account created successfully, Please visit you email to confirm your email");
                    //    return (true, "Account created successfully");
                    //}
                    //else
                    //{
                    //    return (true, $"Account created successfully,  {mailresult.ErrorMessage}");
                    //}
                }
                return (false, "An error ccured");
            }

            return (false, "Please provide the user data");
        }

        private async Task<bool> Exist(string email)
            => await _userManager.FindByEmailAsync(email) != null;

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdateName(UpdateNameDto model)
        {
            if (model != null)
            {
                if (!await Exist(model.Email)) return (false, $"The user with email: {model.Email} does not exist");

                var user = _mapper.Map<ApplicationUser>(model);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return (true, "record updated successfully");
                }
                return (false, result.Errors.ToString());
            }

            return (false, "Please provide the user data");
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> UpdatePhone(UpdatePhoneDto model)
        {
            if (model != null)
            {
                if (!await Exist(model.Email)) return (false, $"The user with email: {model.Email} does not exist");

                var user = _mapper.Map<ApplicationUser>(model);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return (true, "record updated successfully");
                }
                return (false, result.Errors.ToString());
            }

            return (false, "Please provide the user data");
        }

        /// <summary>
        /// Deletes Users whose email is provided
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, $"The User with the Email {email} does not exist");

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return (true, "The user has been deleted successfully");
            else
                return (false, "Unable to delete this user at this time");
        }

        /// <summary>
        /// Confirm user registered email
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, $"The Email {email} does not exist");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return (true, "Your email has been successfully confirmed");
            else
                return (false, "Unable to confirm the your email as at this time");
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "The email suppplied does not exist");
            }
            var _urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var _scheme = _urlHelper.ActionContext.HttpContext.Request.Scheme;
            var callbackurl = _urlHelper.Action(action: nameof(AccountController.ResetPassword), controller: "Account", values: new { email = user.Email, code = code }, protocol: _scheme);

            //var mailresult = _mailSender.SendEmail(user.Email,"Reset your Password", "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");
            var mailresult = _mailSender.SendEmail(user.Email, "Reset your Password", "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");
            if (mailresult.IsSuccess)
            {
                return (true, "Password will reset, Please visit you email to reset your password");
            }
            else
            {
                return (true, $"Password will reset,  {mailresult.ErrorMessage}");
            }
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, string ErrorMessage)> ResetPassword(string email, string code, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return (false, "Email does not exist");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, code, password);
            if (!resetPassResult.Succeeded)
                return (false, "Password reset is not successful");
            else
                return (true, "Your password has been reset successfully");
        }

        /// <summary>
        /// Login functionality
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TokenResponseDto result, string ErrorMessage)> Login(LoginDto model)
        {
            try
            {
                if (model == null)
                    return (false, null, "Invalid model");

                var usr = await _userManager.FindByEmailAsync(model.Email);
                    //:await _userManager.FindByEmailAsync(model.Email);
                if (usr == null)
                    return (false, null, "User does not exist");

                var signedUser = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(signedUser.UserName, model.Password, true, false);
                if (result.Succeeded)
                {


                    var roles = await _userManager.GetRolesAsync(usr);
                    usr.Role = roles.FirstOrDefault();
                    /*var claims = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.Name, usr.Id.ToString()),
                            new Claim(ClaimTypes.Role,usr.Role),
                            new Claim(ClaimTypes.Email, usr.Email)
                        });*/

                    /*var claims = new[]{
                         new Claim() 
                            new Claim(ClaimTypes.Name, usr.Id.ToString()),
                            new Claim(ClaimTypes.Role,usr.Role),
                            new Claim(ClaimTypes.Email, usr.Email)
                        };*/
                    var claims = new List<Claim>
                    {

                            new Claim(JwtRegisteredClaimNames.Email, usr.Email),
                            new Claim(ClaimTypes.Email, usr.Email),
                            new Claim(ClaimTypes.NameIdentifier, usr.Id),
                            //new Claim(ClaimTypes.Name, $"{usr.LastName} {usr.FirstName}"),
                        
                    };
                    claims.Add(new Claim("displayname", $"{usr.LastName} {usr.FirstName}"));
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var token = _auth.GenerateAccessToken(claims);
                    var _pictureUrl = usr?.PictureUrl;

                    var obj = new TokenResponseDto
                    {
                        Token = token.AccessToken,
                        Expires = token.ExpiresIn,
                        LastName = usr.LastName,
                        FirstName = usr.FirstName,
                        Email = usr.Email,
                        PhoneNumber = usr.PhoneNumber,
                        DateCreated = usr.DateCreated,
                        Roles = roles,
                        PictureUrl = _pictureUrl
                    };
                    return (true, obj, "");
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
            return (false, null, "Invalid Email or Password");
        }
    }
}
