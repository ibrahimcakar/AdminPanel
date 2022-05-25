using System.ComponentModel;

namespace AdminPanelWeb.Models
{
    public class UserDTO 
    {
        public int Id { get; set; }
        [DisplayName("Ad Soyad")]
        public string FullName { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Şifre")]
        public string Password { get; set; }
        [DisplayName("E-Posta")]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        //public bool IsOkey { get; set; }
    }
}
