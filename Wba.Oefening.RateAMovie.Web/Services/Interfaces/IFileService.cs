using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> StoreFile(IFormFile file,string subPath);
        Task<string> StoreFile(IFormFile file,string subPath,string existingFileName);
    }
}
