using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tranning.DataDBContext;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class TranneeController : Controller
    {
        private readonly TranningDBContext _dbContext;

        public TranneeController(TranningDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public IActionResult Index(string SearchString)
        {

            UserModel userModel = new UserModel();
            userModel.UserDetailLists = new List<UserDetail>();

            var data = from m in _dbContext.Users
                       select m;

            data = data.Where(m => m.deleted_at == null && m.role_id == 4);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.full_name.Contains(SearchString) || m.phone.Contains(SearchString));
            }

            data.ToList();

            foreach (var item in data)
            {
                userModel.UserDetailLists.Add(new UserDetail
                {
                    id = item.id,
                    role_id = item.role_id,
                    extra_code = item.extra_code,
                    username = item.username,
                    password = item.password,
                    email = item.email,
                    phone = item.phone,
                    gender = item.gender,
                    status = item.status,
                    full_name = item.full_name,
                    created_at = item.created_at,
                    updated_at = item.updated_at
                });
            }
            ViewData["CurrentFilter"] = SearchString;
            return View(userModel);
        }

        [HttpGet]
        public IActionResult Add()
        {

            UserDetail user = new UserDetail();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserDetail user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userData = new User()
                    {
                        username = user.username,
                        role_id = user.role_id,
                        password = user.password,
                        extra_code = user.extra_code,
                        full_name = user.full_name,
                        email = user.email,
                        phone = user.phone,
                        status = user.status,
                        gender = user.gender,
                        created_at = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };
                    _dbContext.Users.Add(userData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {

                    TempData["saveStatus"] = false;

                }
                return RedirectToAction(nameof(TranneeController.Index), "Trannee");
            }
            return View(user);
        }

    }
}
