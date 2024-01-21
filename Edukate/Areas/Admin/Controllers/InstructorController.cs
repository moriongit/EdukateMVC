using Edukate.Context;
using Edukate.Helpers;
using Edukate.Models;
using Edukate.ViewModels.InstructorVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edukate.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly EdukateDbContext _edukateDbContext;
        public InstructorController(EdukateDbContext edukateDbContext)
        {
            _edukateDbContext = edukateDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _edukateDbContext.Instructors.Select(x=> new InstructorListVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgPath = x.ImgPath,
                Course = x.Course,

            }).ToListAsync();

            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(InstructorCreateVM vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            Instructor instructor = new Instructor()
            {
                Course = vm.Course,
                Name = vm.Name,
                ImgPath = await vm.ImgPath.SaveImageAsync(PathConstants.InstructorsImages)

            };
            await _edukateDbContext.Instructors.AddAsync(instructor);
            await _edukateDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult>Delete(int id) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
           
            var data = await _edukateDbContext.Instructors.FindAsync(id);
            if (id ==null)
            {
                return BadRequest();
            }
             _edukateDbContext.Remove(data);
            await _edukateDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(id ==null)
            {
                return BadRequest();
            }
            var data= await _edukateDbContext.Instructors.FindAsync(id);
            if (data==null)
            {
                return BadRequest();
            }
            return View(new InstructorUpdateVM
            {
                Course=data.Course,
                Name=data.Name,
                
            });
        }

        [HttpPost]

        public async Task<IActionResult>Update(InstructorUpdateVM vm,int id)
        {
            var data = await _edukateDbContext.Instructors.FindAsync(id);
            data.Course= vm.Course;
            data.Name= vm.Name;
            if (vm.ImgPath != null)
            {
                string filePath = Path.Combine(PathConstants.RootPath, data.ImgPath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                data.ImgPath = await vm.ImgPath.SaveImageAsync(PathConstants.InstructorsImages);

            }
            await _edukateDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
