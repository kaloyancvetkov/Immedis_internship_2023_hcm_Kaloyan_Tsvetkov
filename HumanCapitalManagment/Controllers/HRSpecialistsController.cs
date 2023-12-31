﻿namespace HumanCapitalManagment.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using HumanCapitalManagment.Data;
    using HumanCapitalManagment.Data.Models;
    using HumanCapitalManagment.Infrastructure.Extensions;
    using HumanCapitalManagment.Models.HRSpecialists;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HRSpecialistsController : Controller
    {
        private readonly HCMDbContext data;
        private readonly IConfigurationProvider mapper;

        public HRSpecialistsController(HCMDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeHRSpecialistFormModel hrSpecialist)
        {
            var userId = this.User.Id();

            var userIsAlreadyHR = this.data
                .HRSpecialists
                .Any(h => h.UserId == userId);

            if (userIsAlreadyHR)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(hrSpecialist);
            }

            var hrSpecialistData = new HRSpecialist
            {
                Name = hrSpecialist.Name,
                EmailAddress = hrSpecialist.EmailAddress,
                PhoneNumber = hrSpecialist.PhoneNumber,
                ImageUrl = hrSpecialist.ImageUrl,
                UserId = userId,
            };

            this.data.HRSpecialists.Add(hrSpecialistData);
            this.data.SaveChanges();

            return RedirectToAction("All", "HRSpecialists");
        }

        public IActionResult All()
        {
            var hrSpecialists = this.data
                .HRSpecialists
                .ProjectTo<HRSpecialistViewModel>(this.mapper)
                .ToList();

            return View(hrSpecialists);
        }
    }
}
