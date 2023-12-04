﻿using AutoMapper;
using ExamonimyWeb.Attributes;
using ExamonimyWeb.DTOs.UserDTO;
using ExamonimyWeb.Entities;
using ExamonimyWeb.Managers.UserManager;
using ExamonimyWeb.Repositories.GenericRepository;
using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers
{
    public class ExamController : GenericController<Exam>
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public ExamController(IUserManager userManager, IMapper mapper, IGenericRepository<Exam> examRepository) : base(mapper, examRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [CustomAuthorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.User.Identity!.Name;
            var user = await _userManager.FindByUsernameAsync(username!);
            var role = _userManager.GetRole(user!);
            var userGetDto = _mapper.Map<UserGetDto>(user);
            return View(role, userGetDto);
        }
    }
}
