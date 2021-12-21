using AdminPanel.Data.Model;
using AdminPanel.Data.Operations;
using AdminPanel.Services.Infrastructure;
using AdminPanel.Services.Mail;
using AdminPanelWeb.Models;
using AutoMapper;
using Cogito.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminPanelWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserOperation _userOperation;
        private readonly IPasswordService _passwordService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public UserController(IUserOperation userOperation, IPasswordService passwordService, IEmailSender emailSender, IMapper mapper)
        {
            _mapper = mapper;
            _passwordService = passwordService;
            _userOperation = userOperation;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("CurrentUser") == null)
            {
                return Redirect("~/Home/Login");
            }
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                var list = _userOperation.GetAllUser();
                var model = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(list);
                return View(model);
            }
            return Redirect("~/Home/Login");
        }
        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("CurrentUser") != null)
            {
                UserDTO model = new UserDTO();
                return View(model);
            }

            return Redirect("~/Home/Login");

        }
        [HttpPost]
        public IActionResult Create(UserDTO modelDTO)
        {
            //HttpContext.Session.SetString("Id", model.Id.ToString());
            var model = _mapper.Map<User>(modelDTO);

            if (ModelState.IsValid)
            {
                var password = _passwordService.GeneratePassword();
                model.Password = _passwordService.EncryptString(password);

                if (HttpContext.Session.GetString("CurrentUser") != null)
                {
                    var userList = _userOperation.GetAllUser();
                    if (!userList.Any(s => s.UserName.ToLower().Contains(model.UserName.ToLower())) && !userList.Any(s => s.Email.ToLower().Contains(model.Email.ToLower())))
                        model = _userOperation.CreateUser(model);
                    else if (userList.Any(s => s.UserName.ToLower().Contains(model.UserName.ToLower())) && !userList.Any(s => s.Email.ToLower().Contains(model.Email.ToLower())))
                    {
                        ModelState.AddModelError("UserName", "Bu kullanıcı zaten mevcut");
                        return View(modelDTO);
                    }

                    else if (!userList.Any(s => s.UserName.ToLower().Contains(model.UserName.ToLower())) && userList.Any(s => s.Email.ToLower().Contains(model.Email.ToLower())))
                    {
                        ModelState.AddModelError("Email", "Bu email zaten mevcut");
                        return View(modelDTO);
                    }

                    else
                    {
                        ModelState.AddModelError("UserName", "Bu kullanıcı zaten mevcut");
                        ModelState.AddModelError("Email", "Bu email zaten mevcut");

                        return View(modelDTO);

                    }

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

                        return RedirectToAction("List");
                    }
                }
            }

            return View(modelDTO);

        }

    }
}
