using Ex01.Data.Repositories;
using Ex01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            var list = _jobRepository.GetAll();
            return View(list);
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
            job.Status = 0;
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