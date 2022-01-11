using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.Models;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface IFormHelpersService
    {
        Task<List<SelectListItem>> BuildCompanyList();
        public Task<List<CheckboxHelper>> BuildCheckboxList(bool isActor, Movie editMovie);
        public Task<List<CheckboxHelper>> BuildCheckboxList(bool isActor);
    }
}