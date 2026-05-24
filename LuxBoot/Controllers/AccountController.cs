using LuxBoot.DOS_Servers;
using LuxBoot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace LuxBoot.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<UserApp> _dbContext;
        private readonly AppDbContext db;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ServerState serverState;
        public Servers _server = new Servers(new Attack_Methods());
        public AccountController(UserManager<UserApp> context, AppDbContext _ddbb, IServiceScopeFactory serviceProvider, ServerState state)
        {
            db = _ddbb;
            _dbContext = context;
            _scopeFactory = serviceProvider;
            serverState = state;
        }

        public IActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var getdata = _dbContext.Users.Include(x => x.AccountInfo).FirstOrDefault(x => x.Id == userId);

                if (getdata == null)
                {
                    return View();
                }
                else
                {

                    getdata.AccountInfo.CurrentPlan = getdata.AccountInfo.CurrentPlan + "_" + serverState.ServerCount.ToString();


                    return View(getdata.AccountInfo);
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }

        }
        public IActionResult Panel()
        {
            if (User.Identity.IsAuthenticated)
            {


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var getdata = db.Users.Include(x => x.AccountInfo).FirstOrDefault(x => x.Id == userId);

                if (getdata == null)
                {
                    return View();
                }
                else
                {

                    return View(getdata.AccountInfo);
                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }

        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Panel(string ipaddress, int port, int time, string type)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var getdata = db.Users.Include(x => x.AccountInfo).FirstOrDefault(x => x.Id == userId);

            var get_data_1 = db.Users.Include(x => x.AccountInfo).ThenInclude(x => x.CurrentAttacksList).FirstOrDefault(x => x.Id == userId);


            int set_time_user = time;
            int limit_time = 0;
            int limit_concurrents = 0
                ;
            int active_sub = 0;

            switch (getdata.AccountInfo.CurrentPlan.ToLower())
            {
                case "basic":
                    limit_time = 60;
                    limit_concurrents = 1;
                    active_sub = 7;
                    break;
                case "standard":
                    limit_time = 120;
                    limit_concurrents = 2;
                    active_sub = 30;
                    break;
                case "premium":
                    limit_time = 360;
                    limit_concurrents = 10;
                    active_sub = 35;
                    break;
                    
            }


            
            if (ipaddress != null && port != null && time != null && type != null)
            {
                var compare = DateTime.Parse(get_data_1.AccountInfo.MemberSince).AddDays(active_sub);

                if (getdata == null || getdata.AccountInfo.AttacksLeft == 0) {
                    TempData["Message"] = "You currently have 0 Attacks left, You will get 10 new attacks Tomorrow.";
                    return View(getdata.AccountInfo);
                }
                else if(set_time_user > limit_time)
                {
                    TempData["Message"] = $"You are only allowed {limit_time}s for each attack.";
                    return View(getdata.AccountInfo);
                }
                else if(get_data_1.AccountInfo.CurrentAttacksList.Count >= limit_concurrents)
                {
                    TempData["Message"] = $"You are only allowed {limit_concurrents} Concurrent's";
                    return View(getdata.AccountInfo);
                }
                else if (compare < DateTime.UtcNow)
                {
                    TempData["Message"] = $"Your Active subscription has expired, please reorder a new subscription.";
                    return View(getdata.AccountInfo);
                }
                else
                {
                    Console.WriteLine(limit_concurrents);
                    Console.WriteLine(getdata.AccountInfo.CurrentAttacksList.Count);
                    getdata.AccountInfo.AttacksLeft--;
                    getdata.AccountInfo.LastAttack = $"{DateTime.UtcNow}";
                    getdata.AccountInfo.TotalAttacks++;

                    AttackItem newItem = new AttackItem()
                    {
                        userId = getdata,
                        IpAddress = ipaddress
                        ,
                        Port = $"{port}",
                        AttackType = type,
                        Time = $"{time}",
                        TimeLeft = DateTime.UtcNow.AddSeconds(time).ToString("o")
                    };

                    switch (DateTime.Today.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            getdata.AccountInfo.Attacks[0] = $"{(int.Parse(getdata.AccountInfo.Attacks[0]) + 1).ToString()}";
                            break;
                        case DayOfWeek.Tuesday:
                            getdata.AccountInfo.Attacks[1] = $"{(int.Parse(getdata.AccountInfo.Attacks[1]) + 1).ToString()}";
                            break;
                        case DayOfWeek.Wednesday:
                            getdata.AccountInfo.Attacks[2] = $"{(int.Parse(getdata.AccountInfo.Attacks[2]) + 1).ToString()}";
                            break;
                        case DayOfWeek.Thursday:
                            getdata.AccountInfo.Attacks[3] = $"{(int.Parse(getdata.AccountInfo.Attacks[3]) + 1).ToString()}";

                            break;
                        case DayOfWeek.Friday:
                            getdata.AccountInfo.Attacks[4] = $"{(int.Parse(getdata.AccountInfo.Attacks[4]) + 1).ToString()}";
                            break;
                        case DayOfWeek.Saturday:
                            getdata.AccountInfo.Attacks[5] = $"{(int.Parse(getdata.AccountInfo.Attacks[5]) + 1).ToString()}";
                            break;
                        case DayOfWeek.Sunday:
                            getdata.AccountInfo.Attacks[6] = $"{(int.Parse(getdata.AccountInfo.Attacks[6]) + 1).ToString()}";
                            break;
                    }

                    getdata.AccountInfo.CurrentAttacksList.Add(newItem);

                    await db.SaveChangesAsync();

                    newItem.TimeLeft = DateTime.UtcNow.AddSeconds(time).ToString();

                    _ = Task.Run(() => {_server.Start(newItem.AttackType, newItem).GetAwaiter().GetResult(); });

                    _ = Task.Run(() =>
                    {
                        try
                        {
                            RemoveAfterTime(time, getdata.Id).GetAwaiter().GetResult();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    });

                    serverState.ServerCount -= 1;

                    var current_attackslist = db.Users.Include(x => x.AccountInfo).FirstOrDefault(x => x.Id == userId);
                    if (current_attackslist != null)
                    {
                        if (current_attackslist.AccountInfo.AttacksLeft == 0)
                        {
                            _ = Task.Run(() =>
                            {
                                AddAttacks(10, getdata.Id).GetAwaiter().GetResult();
                            });
                        }
                    }


                    TempData["Message"] = "Attack Has Started.";
                    return View(getdata.AccountInfo);
                }

            }
            TempData["Message"] = "You have not inputted everything correctly";
            return View(getdata.AccountInfo);
        }
        public async Task AddAttacks(int count, string userId)
        {
            await Task.Delay(TimeSpan.FromHours(24));
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var find = await db.Users.Include(x => x.AccountInfo).FirstOrDefaultAsync(x => x.Id == userId);

            if(find != null)
            {
                find.AccountInfo.AttacksLeft = count;
                await db.SaveChangesAsync();
            }
            
        }

        public async Task RemoveAfterTime(int time, string data)
        {
            

            await Task.Delay(TimeSpan.FromSeconds(time));

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var user = await db.Users
                .Include(x => x.AccountInfo)
                .ThenInclude(x => x.CurrentAttacksList)
                .FirstOrDefaultAsync(x => x.Id == data);

            if (user == null) return;

            user.AccountInfo.CurrentAttacksList.Clear();
            
            await db.SaveChangesAsync();

            serverState.ServerCount += 1;

        }

        [Authorize]
        public IActionResult Prices()
        {
            return View();
        }

        
        public IActionResult Overview()
        {
            if (User.Identity.IsAuthenticated)
            {


                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var getdata = _dbContext.Users.Include(x => x.AccountInfo.CurrentAttacksList).FirstOrDefault(x => x.Id == userId);

                if (getdata == null)
                {
                    return RedirectToAction("Login", "Auth");
                }
                else
                {

                    return View(getdata.AccountInfo);
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> Overview(string type,string address, string port, string time)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var getdata = await db.Users.Include(x => x.AccountInfo).ThenInclude(x => x.CurrentAttacksList).FirstOrDefaultAsync(x => x.Id == userid);

                var findit = getdata.AccountInfo.CurrentAttacksList.FirstOrDefault(x => x.IpAddress == address);

                getdata.AccountInfo.CurrentAttacksList.Remove(findit);

                await db.SaveChangesAsync();
                TempData["Message"] = "Attack Stopped!";
                return View(getdata.AccountInfo);

            }
            return View();
        }
    }
}
