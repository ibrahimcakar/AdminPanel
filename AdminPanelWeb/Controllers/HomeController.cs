using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using AdminPanel.Services.Infrastructure;
using AdminPanel.Services.Mail;
using AdminPanelWeb.Models;
using AutoMapper;
using Cogito.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace AdminPanel.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserOperation _userOperation;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;


        public HomeController( IUserOperation userOperation, ILogger<HomeController> logger, IEmailSender emailSender,  IMapper mapper,
            IPasswordService passwordService)
        {
            _passwordService = passwordService;
            _mapper = mapper;
            _userOperation = userOperation;
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index(User model)
        {
            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return Redirect("~/Home/Login");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            HttpContext.Session.Clear();
          model=  _userOperation.GetUserByUsernamePassword(model);
            if (model != null)
            {
                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(model));
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı veya şifre yanlış";
                return View(model);
            }
        }
        public IActionResult Logout()
        {

            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return Redirect("~/Home/Login");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(UserDTO modelDTO)
        {
            var model = _mapper.Map<User>(modelDTO);

            if (ModelState.IsValid)
            {

                model = _userOperation.GetMail(model);
                var password = _passwordService.GeneratePassword();

                //model2.Password = password;

                model.Password = _passwordService.EncryptString(password);
                model = _userOperation.UpdateUser(model);

                if (model != null && model.Id > 0)
                {
                    EmailAccount emailAccount = new EmailAccount();
                    //prop
                    emailAccount.Email = "ckrentdestek@gmail.com";
                    emailAccount.EnableSsl = true;
                    emailAccount.DisplayName = "CkRent";
                    emailAccount.Host = "smtp.gmail.com";
                    emailAccount.Password = "ckrent123";
                    emailAccount.Port = 465;
                    emailAccount.UseDefaultCredentials = false;
                    emailAccount.Username = "ckrentdestek@gmail.com";

                    EmailProperties emailProperties = new EmailProperties();
                    emailProperties.body = "Parolanız ve kullanıcınız oluşturulmuştur. Şifreniz: " + password;
                    emailProperties.fromName = "Ck Rent ";
                    emailProperties.fromAddress = "ckrentdestek@gmail.com";
                    emailProperties.subject = "Ck Rent admin panel şifresi";
                    emailProperties.toAddress = model.Email;
                    emailProperties.toName = "Ck Rent";
                    SendResult result = new SendResult();

                    _emailSender.SendEmail(emailAccount, emailProperties);
                    result.Status = SendResultStatus.Successfull;
                    result.Message = "Mesajınız başarıyla gönderilmiştir.";

                    ViewData["MailSenderResult"] = result;
                    return Redirect("~/Home/Login");
                    //deneme
                }
            }
            return View(model);

        }

    }
}
