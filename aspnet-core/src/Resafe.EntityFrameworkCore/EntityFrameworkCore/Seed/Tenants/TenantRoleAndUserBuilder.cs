using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Resafe.Authorization;
using Resafe.Authorization.Roles;
using Resafe.Authorization.Users;
using System.Collections.Generic;

namespace Resafe.EntityFrameworkCore.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly ResafeDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(ResafeDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {

            CreateChildren();
            // Admin role

            var adminRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();

                // Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new ResafeAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            //Parent Role

            var parentRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Parent);
            if (parentRole == null)
            {
                parentRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Parent, StaticRoleNames.Tenants.Parent) { IsStatic = true }).Entity;
                _context.SaveChanges();

                // Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new ResafeAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) && p.Name == PermissionNames.Pages_Parent)
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }


            //School Role

            var schoolRole = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.School);
            if (schoolRole == null)
            {
                schoolRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.School, StaticRoleNames.Tenants.School) { IsStatic = true }).Entity;
                _context.SaveChanges();

                // Grant all permissions to parent role
                var permissions = PermissionFinder
                    .GetAllPermissions(new ResafeAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant) && p.Name == PermissionNames.Pages_School)
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            // Admin user

            var adminUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == AbpUserBase.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com");
                adminUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(adminUser, "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                // Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();

                // User account of admin user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = adminUser.Id,
                        UserName = AbpUserBase.AdminUserName,
                        EmailAddress = adminUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }

            //Parent user

            var parentUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == "Parent");
            if (parentUser == null)
            {
                parentUser = User.CreateTenantUser(_tenantId, "parent@defaulttenant.com", "Parent");
                parentUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(parentUser, "parent");
                parentUser.IsEmailConfirmed = true;
                parentUser.IsActive = true;

                _context.Users.Add(parentUser);
                _context.SaveChanges();

                // Assign Parent role to parent user
                _context.UserRoles.Add(new UserRole(_tenantId, parentUser.Id, parentRole.Id));
                _context.SaveChanges();

                // User account of parent user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = parentUser.Id,
                        UserName = "Parent",
                        EmailAddress = parentUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }

            //School user

            var schoolUser = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == "School");
            if (schoolUser == null)
            {
                schoolUser = User.CreateTenantUser(_tenantId, "school@defaulttenant.com", "School");
                schoolUser.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(parentUser, "school");
                schoolUser.IsEmailConfirmed = true;
                schoolUser.IsActive = true;

                _context.Users.Add(schoolUser);
                _context.SaveChanges();

                // Assign School role to school user
                _context.UserRoles.Add(new UserRole(_tenantId, schoolUser.Id, schoolRole.Id));
                _context.SaveChanges();

                // User account of school user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = schoolUser.Id,
                        UserName = "School",
                        EmailAddress = schoolUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }

        }

        private void CreateChildren()
        {
            var children = new List<Child.Child>()
            {
                new Child.Child { Name = "Kaeden Patrick", Latitude = -29.844172, Longitude = 30.998959},
                new Child.Child { Name = "Kolten Patrick", Latitude = -29.844370, Longitude = 30.998266},
                new Child.Child { Name = "Anton Henson", Latitude = -29.821353, Longitude = 30.956933},
                new Child.Child { Name = "Jarrett Harrington", Latitude = -29.844658, Longitude = 30.998424},
                new Child.Child { Name = "Leah David", Latitude = -29.844304, Longitude = 30.998520},
                new Child.Child { Name = "Titus Maynard", Latitude = -29.844417, Longitude = 30.998780},
                new Child.Child { Name = "MichaelHuffman", Latitude = -29.844972, Longitude = 30.998944},
                new Child.Child { Name = "Kathryn Cunningham", Latitude = -29.844316, Longitude = 30.998754},
                new Child.Child { Name = "Tori Stewart", Latitude = -29.844638, Longitude = 30.998127},
                new Child.Child { Name = "Lawrence Schmidt", Latitude = -29.844104, Longitude = 30.998148},
                new Child.Child { Name = "Darien Powell", Latitude = -29.844870, Longitude = 30.998989},
                new Child.Child { Name = "Clare Blankenship", Latitude = -29.844119, Longitude = 30.998871},
                new Child.Child { Name = "Demarcus Goodwin", Latitude = -29.844836, Longitude = 30.998033},
                new Child.Child { Name = "Adrienne Lara", Latitude = -29.844070, Longitude = 30.998090},
                new Child.Child { Name = "Akira Barton", Latitude = -29.844695, Longitude = 30.998650},
                new Child.Child { Name = "Wendy Baird", Latitude = -29.844718, Longitude = 30.998643},
                new Child.Child { Name = "Fiona Mcmillan", Latitude = -29.844398, Longitude = 30.998929},
                new Child.Child { Name = "Caroline Andrews", Latitude = -29.844837, Longitude = 30.998969},
                new Child.Child { Name = "Marilyn Haley", Latitude = -29.844213, Longitude = 30.998763},
                new Child.Child { Name = "Amari Carpenter", Latitude = -29.844601, Longitude = 30.998272},
                new Child.Child { Name = "Jazmyn Marks", Latitude = -29.844053, Longitude = 30.998136},
                new Child.Child { Name = "Leroy Horton", Latitude = -29.844696, Longitude = 30.998258},
                new Child.Child { Name = "Keagan Estrada", Latitude = -29.844035, Longitude = 30.998312},
                new Child.Child { Name = "Miya Wyatt", Latitude = -29.844897, Longitude = 30.998484},
                new Child.Child { Name = "Lydia Barron", Latitude = -29.844288, Longitude = 30.998002},
                new Child.Child { Name = "Chaim Hooper", Latitude = -29.844799, Longitude = 30.998664},
                new Child.Child { Name = "Kate Orr", Latitude = -29.844367, Longitude = 30.998870},
                new Child.Child { Name = "Gabriella Bernard", Latitude = -29.844019, Longitude = 30.998918},
                new Child.Child { Name = "Jason Alexander", Latitude = -29.844703, Longitude = 30.998673},
                new Child.Child { Name = "Karli Patel", Latitude = -29.844828, Longitude = 30.998014},
                new Child.Child { Name = "Nehemiah Spears", Latitude = -29.844743, Longitude = 30.998488},
                new Child.Child { Name = "Lawson Osborne", Latitude = -29.844627, Longitude = 30.998584},
                new Child.Child { Name = "Jett Buchanan", Latitude = -29.844979, Longitude = 30.998662},
                new Child.Child { Name = "Reyna Hamilton", Latitude = -29.844720, Longitude = 30.998489},
                new Child.Child { Name = "Rory Lynch", Latitude = -29.844838, Longitude = 30.998699},
                new Child.Child { Name = "Darnell Perez", Latitude = -29.844416, Longitude = 30.998261},
                new Child.Child { Name = "Cecelia Paul", Latitude = -29.844095, Longitude = 30.998474},
                new Child.Child { Name = "Matilda Parsons", Latitude = -29.844063, Longitude = 30.998013},
                new Child.Child { Name = "Shaniya Bradshaw", Latitude = -29.844622, Longitude = 30.998147},
                new Child.Child { Name = "Zaniyah Obrien", Latitude = -29.844634, Longitude = 30.998516},
                new Child.Child { Name = "Fisher Hardin", Latitude = -29.844798, Longitude = 30.998802},
                new Child.Child { Name = "Efrain Fletcher", Latitude = -29.844775, Longitude = 30.998968},
                new Child.Child { Name = "Paul Whitney", Latitude = -29.844368, Longitude = 30.998093},
                new Child.Child { Name = "Talon Conrad", Latitude = -29.844747, Longitude = 30.998561},
                new Child.Child { Name = "Dillan Mullen", Latitude = -29.844429, Longitude = 30.998083},
                new Child.Child { Name = "Amare Shepard", Latitude = -29.844464, Longitude = 30.998612},
                new Child.Child { Name = "Blaine Fry", Latitude = -29.844607, Longitude = 30.998800},
                new Child.Child { Name = "Eric Duran", Latitude = -29.844560, Longitude = 30.998578},
                new Child.Child { Name = "Abigail Rangel", Latitude = -29.844047, Longitude = 30.998952},
                new Child.Child { Name = "Darion Jenkins", Latitude = -29.844504, Longitude = 30.998068},
                new Child.Child { Name = "Evelyn Dawson", Latitude = -29.844341, Longitude = 30.998720},
                new Child.Child { Name = "Seamus Combs", Latitude = -29.844778, Longitude = 30.998272},
                new Child.Child { Name = "Jaelyn Hurley", Latitude = -29.844237, Longitude = 30.998411},
                new Child.Child { Name = "Maximillian Dennis", Latitude = -29.844926, Longitude = 30.998056},
                new Child.Child { Name = "Teagan Mayer", Latitude = -29.844186, Longitude = 30.998400},
                new Child.Child { Name = "Hamza Graves", Latitude = -29.844515, Longitude = 30.998646},
                new Child.Child { Name = "Joseph Prince", Latitude = -29.844049, Longitude = 30.998616},
                new Child.Child { Name = "Conner Green", Latitude = -29.844221, Longitude = 30.998161},
                new Child.Child { Name = "Finley Suarez", Latitude = -29.844167, Longitude = 30.998556},
                new Child.Child { Name = "Leonardo Ryan", Latitude = -29.844684, Longitude = 30.998050},  
            };
            if (_context.Children.Count() == 0)
            {
                _context.Children.AddRange(children);
            }
            _context.SaveChanges();
        }
    }
}
