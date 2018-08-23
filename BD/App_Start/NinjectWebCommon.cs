using BD.Services;
using BD.Services.Interfaces;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Mongo;
using DAL.Repositories.Mongo.Interfaces;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BD.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BD.App_Start.NinjectWebCommon), "Stop")]

namespace BD.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IEshopDbContext>().To<EshopDbContext>().InRequestScope();
            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>));
            kernel.Bind<IVisitService>().To<VisitService>();
            kernel.Bind<IHeadPhoneService>().To<HeadPhoneService>();
            kernel.Bind<IMediaPlayerService>().To<MediaPlayerService>();
            kernel.Bind<ILoudspeakerService>().To<LoudspeakerService>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IMarkRepository>().To<MarkRepository>();
            kernel.Bind<IEshopMongoDbContext>().To<EshopMongoDbContext>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<ICommentsRepository>().To<CommentsRepository>();
        }        
    }
}
