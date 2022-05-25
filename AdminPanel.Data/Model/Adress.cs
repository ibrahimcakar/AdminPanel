using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Model
{
    public class Adress
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  
    }
}
