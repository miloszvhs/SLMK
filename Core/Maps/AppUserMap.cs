using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.AspNet.Identity;
using NHibernate.AspNetCore.Identity;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using IdentityUserClaim = NHibernate.AspNetCore.Identity.IdentityUserClaim;
using NHIdentityUser = NHibernate.AspNetCore.Identity.IdentityUser;

namespace Core.Maps;

// public class AppUserMapping : IdentityUserMap
// {
//     public AppUserMapping() {
//     }
// }
//
// public class AppRoleMapping : IdentityUserRole
// {
//     public AppRoleMapping() {
//     }
// }
//
// public class AppUserTokenMapping : IdentityUserToken
// {
//     public AppUserTokenMapping() {
//     }
// }
//
// public class AppUserClaimMapping : IdentityUserClaim
// {
//     public AppUserClaimMapping() {
//     }
// }
//
// public class AppUserTokenMapping : IdentityRoleClaim
// {
//     public AppUserTokenMapping() {
//     }
// }