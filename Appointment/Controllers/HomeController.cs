using Appointment.Models;
using BusinesLayer.Concrete;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace Appointment.Controllers
{
    public class HomeController : Controller
    {
        AppointmentManager apm = new AppointmentManager(new EfAppointmentRepositorycs());
        UserManager usm = new UserManager(new EfUserRepository());

        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int userId =Convert.ToInt32( User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userInfo=usm.GetUserById(userId);
            ViewData["UserName"] =userInfo.Name +" "+ userInfo.LastName;
            ViewData["UserTypeId"] = userInfo.UserTypeId.ToString();
            if (userInfo.UserTypeId==4) 
            {
                var values = apm.GetAllAppointmentsByUserId(userId);
                return View(values);
            }
            var gvalues = apm.GetAllAppointments();
            return View(gvalues);
            
        }
        [HttpPost]
        public IActionResult AddAppointment(EntityLayer.Concrete.Appointment appointment)
        {
			int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            appointment.Id= userId;
            appointment.IsSentEmail= false; 
            apm.AddAppointment(appointment);  
            var jsonaddppointment=JsonConvert.SerializeObject(appointment);
            return Json(jsonaddppointment);
        }
        public IActionResult DeleteAppointment(int id)
        {
            var appointment = apm.GetAppointmentById(id);
            apm.DeleteAppointment(appointment);
            return Json(appointment);
        }

        public IActionResult Privacy(int id)
        {
            var value = apm.GetAppointmentById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateAppointment(EntityLayer.Concrete.Appointment appointment, int id)
        {
            if (ModelState.IsValid)
            {
                var existingAppo = apm.GetAppointmentById(id);

                if (existingAppo != null)
                {
                    existingAppo.Description = appointment.Description;
                    existingAppo.AppointmentDate = appointment.AppointmentDate;
                    existingAppo.AppointmentTime = appointment.AppointmentTime;

                    apm.UpdateAppointment(existingAppo);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle the case where the appointment with the given id is not found.
                    // You might want to show an error message or handle it as per your application's requirements.
                }
            }

            // If the ModelState is not valid, return to the same view to display validation errors.
            return View(Privacy);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Users()
        {
            var users = usm.GetAllUsers();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("UserModalPartial");
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            usm.AddUser(user);
            return PartialView("UserModalPartial");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user=usm.GetUserById(id);   
            return PartialView("EditUserModalPartial",user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            usm.UpdateUser(user);
            return PartialView("EditUserModalPartial", user);
        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var user = usm.GetUserById(id);
            return PartialView("DeletePartialView", user);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {

            var appofusers = apm.GetAllAppointmentsByUserId(user.Id);
            foreach (var appofuser in appofusers)
            {
                apm.UpdateAppointment(appofuser);
            }
            
            usm.DeleteUser(user);
            return PartialView("DeletePartialView", user);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}