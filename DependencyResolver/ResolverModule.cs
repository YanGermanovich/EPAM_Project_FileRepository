using System.Data.Entity;
using BLL.Services_Interface;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace CustomDependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<LumeDBEntities>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<LumeDBEntities>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRatingRepository>().To<RatingRepository>();
            kernel.Bind<IRatingService>().To<RatingService>();

            kernel.Bind<IResourceRepository>().To<ResourceRepository>();
            kernel.Bind<IResourceService>().To<ResourceService>();

            kernel.Bind<IRolesRepository>().To<RoleRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();

        }
    }
}

