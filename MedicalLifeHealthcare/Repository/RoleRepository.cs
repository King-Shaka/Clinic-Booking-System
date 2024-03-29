﻿using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace MedicalLifeHealthcare.Repository
{
    public class RoleRepository :IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }

    }
}
