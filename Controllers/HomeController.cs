using Ex01.Data.Repositories;
using Ex01.Helper;
using Ex01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ex01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobRepository _jobRepository;

        public HomeController(ILogger<HomeController> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNum = 1, int pageSize = 10)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CreatedAtParm"] = String.IsNullOrEmpty(sortOrder) ? "create_asc" : "";
            ViewData["ProcessingTimeParm"] = sortOrder == "ProcessingTime" ? "processingTime_desc" : "ProcessingTime";
            
            if(searchString != null)
            {
                pageNum = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Job, bool>> filter = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                filter = f => f.Name.Contains(searchString) 
                           || f.ProcessingTime.ToString().Contains(searchString)
                           || f.Status.Contains(searchString);
            }

            Func<IQueryable<Job>, IOrderedQueryable<Job>> orderBy = null;

            switch (sortOrder)
            {
                case "create_asc": orderBy = o => o.OrderBy(x=>x.CreatedAt); break;
                case "ProcessingTime": orderBy = o => o.OrderBy(x=>x.ProcessingTime); break;
                case "processingTime_desc": orderBy = o => o.OrderByDescending(x=>x.ProcessingTime); break;
                default: orderBy = o => o.OrderByDescending(x => x.CreatedAt);break;
            }

            return View(await _jobRepository.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageNum ?? 1, pageSize: pageSize));
        }

        public IActionResult AddJob()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddJob(Job job)
        {
            if(!ModelState.IsValid)
            {
                return View(job);
            }
            job.CreatedAt= DateTime.Now;
            _jobRepository.Add(job);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateJob(int? id)
        {
            var job = _jobRepository.JobGetById((int)id);
            return View(job);
        }

        [HttpPost]
        public IActionResult UpdateJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return View(job);
            }
            _jobRepository.Update(job);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteJob(int id)
        {
            var job = _jobRepository.JobGetById(id);
            _jobRepository.Delete(job);
            return RedirectToAction("Index");
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
    }
}