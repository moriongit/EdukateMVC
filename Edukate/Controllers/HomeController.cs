using Edukate.Context;
using Edukate.Models;
using Edukate.ViewModels.HomeVm;
using Edukate.ViewModels.InstructorVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Edukate.Controllers
{
    public class HomeController : Controller
    {
        

       

        public readonly EdukateDbContext _edukateDbContext;

        public HomeController(EdukateDbContext edukateDbContext)
        {
            _edukateDbContext = edukateDbContext;
        }


        public async Task<IActionResult> Index()
        {
            HomeVM vm = new HomeVM
            {
                Instructors = await _edukateDbContext.Instructors.Select(x => new InstructorListVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                    Course = x.Course,
                }).ToListAsync()
            };
            return View(vm);
        }

       
    }
}