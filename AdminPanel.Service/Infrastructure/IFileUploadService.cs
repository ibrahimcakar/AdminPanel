using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Services.Infrastructure
{
  public interface IFileUploadService 
    {
        public string UploadImage(IFormFile file);
    }
}
