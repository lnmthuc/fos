using FOS.Repositories.Mapping;
using FOS.Services.FeedbackServices;
using FOS.Services.Providers;
using FOS.Services.SPListService;
using FOS.Services.SPUserService;
using FOS.Services.SummaryService;
using System;

using Unity;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Lifetime;
using Unity.log4net;

namespace FOS.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer()
              .AddNewExtension<Log4NetExtension>()
              .AddNewExtension<Interception>();

              RegisterTypes(container);

              return container;
          });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            // TODO: Register your type's mappings here.
            container.RegisterType<Repositories.FosContext, Repositories.FosContext>(
                new PerResolveLifetimeManager());
            container.RegisterType<Repositories.Mapping.IOrderMapper, Repositories.Mapping.OrderMapper>();
            container.RegisterType<Repositories.Mapping.IRecurrenceEventMapper, Repositories.Mapping.RecurrenceEventMapper>();
            container.RegisterType< Repositories.Mapping.IEventPromotionMapper, Repositories.Mapping.EventPromotionMapper > (
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<IFeedbackMapper, FeedbackMapper>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Repositories.Mapping.IGraphUserMapper, Repositories.Mapping.GraphUserMapper>();
            container.RegisterType<Repositories.Mapping.ICustomGroupMapper, Repositories.Mapping.CustomGroupMapper>();

            container.RegisterType<Repositories.Repositories.ICustomGroupRepository, Repositories.Repositories.CustomGroupRepository>();
            container.RegisterType<Repositories.Repositories.IRecurrenceEventRepository, Repositories.Repositories.RecurrenceEventRepository>();
            container.RegisterType<Repositories.Repositories.IOrderRepository, Repositories.Repositories.OrderRepository>();
            container.RegisterType<Repositories.Repositories.IDelegateHostRepository, Repositories.Repositories.DelegateHostRepository>();
            container.RegisterType<Repositories.Infrastructor.IDbFactory, Repositories.Infrastructor.DbFactory>();
            container.RegisterType<Repositories.Repositories.IFOSFoodServiceAPIsRepository, Repositories.Repositories.FOSFoodServiceAPIsRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());

            container.RegisterType<Repositories.Repositories.IReportFileRepository, Repositories.Repositories.ReportFileRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Repositories.Repositories.IEventPromotionRepository, Repositories.Repositories.EventPromotionRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Repositories.Repositories.IFOSFavoriteRestaurantRepository, Repositories.Repositories.FOSFavoriteRestaurantRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Repositories.Repositories.IFeedbackRepository, Repositories.Repositories.FeedbackRepository>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType<Repositories.Repositories.IFOSHostLinkRepository, Repositories.Repositories.FOSHostLinkRepository>();

            container.RegisterType<Services.ExternalServices.IExternalServiceFactory, Services.ExternalServices.ExternalServiceFactory>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.IFOSFoodServiceAPIsService, Services.FOSFoodServiceAPIsService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.DeliveryServices.IDeliveryService, Services.DeliveryServices.DeliveryService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.ProvinceServices.IProvinceService, Services.ProvinceServices.ProvinceService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.RestaurantServices.IRestaurantService, Services.RestaurantServices.RestaurantService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.FavoriteService.IFavoriteService, Services.FavoriteService.FavoriteService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.EventServices.IEventService, Services.EventServices.EventService>();
            container.RegisterType<Services.EventPromotionServices.IEventPromotionService, Services.EventPromotionServices.EventPromotionService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.CustomGroupService.ICustomGroupService, Services.CustomGroupService.CustomGroupService>();
            container.RegisterType<Services.ExcelService.IExcelService, Services.ExcelService.ExcelService>();
            container.RegisterType<Services.DelegateHostService.IDelegateHostService, Services.DelegateHostService.DelegateHostService>();

            container.RegisterType<Services.FoodServices.IFoodService, Services.FoodServices.FoodService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.SendEmailServices.ISendEmailService, Services.SendEmailServices.SendEmailService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Services.RecurrenceEventServices.IRecurrenceEventService, Services.RecurrenceEventServices.RecurrenceEventService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            //container.RegisterType<Services.ICrawlLinksService, Services.CrawlLinksService>();
            //container.RegisterType<Repositories.APIExternalServiceEntities, Repositories.APIExternalServiceEntities>(new PerResolveLifetimeManager());
            //container.RegisterType<Services.RequestMethods.IRequestMethod, Services.RequestMethods.GetMethod> ("GetMethod");
            //container.RegisterType<Services.RequestMethods.IRequestMethod, Services.RequestMethods.PostMethod>("PostMethod");
            container.RegisterType<Services.OrderServices.IOrderService, Services.OrderServices.OrderService>();
            container.RegisterType<Services.IOAuthService, Services.OAuthService>(
                new HierarchicalLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<Model.Mapping.IOrderDtoMapper, Model.Mapping.OrderDtoMapper>();
            container.RegisterType<ICustomAuthentication, CustomAuthentication>();
            container.RegisterType<Model.Mapping.IAPIsDtoMapper, Model.Mapping.APIsDtoMapper>();
            container.RegisterType<Model.Mapping.ICategoryDtoMapper, Model.Mapping.CategoryDtoMapper>();
            container.RegisterType<Model.Mapping.ICategoryGroupDtoMapper, Model.Mapping.CategoryGroupDtoMapper>();
            container.RegisterType<Model.Mapping.IDeliveryInfosDtoMapper, Model.Mapping.DeliveryInfosDtoMapper>();
            container.RegisterType<Model.Mapping.IFavoriteRestaurantDtoMapper, Model.Mapping.FavoriteRestaurantDtoMapper>();
            container.RegisterType<Model.Mapping.IFoodCategoryDtoMapper, Model.Mapping.FoodCategoryDtoMapper>();
            container.RegisterType<Model.Mapping.IFoodDtoMapper, Model.Mapping.FoodDtoMapper>();
            container.RegisterType<Model.Mapping.IProvinceDtoMapper, Model.Mapping.ProvinceDtoMapper>();
            container.RegisterType<Model.Mapping.IRestaurantDetailDtoMapper, Model.Mapping.RestaurantDetailDtoMapper>();
            container.RegisterType<Model.Mapping.IRestaurantDtoMapper, Model.Mapping.RestaurantDtoMapper>();
            container.RegisterType<Model.Mapping.IEventDtoMapper, Model.Mapping.EventDtoMapper>();
            container.RegisterType<Model.Mapping.IUserDtoMapper, Model.Mapping.UserDtoMapper>();
            container.RegisterType<Model.Mapping.IGraphUserDtoMapper, Model.Mapping.GraphUserDtoMapper>();
            container.RegisterType<Model.Mapping.IRestaurantSummaryDtoMapper, Model.Mapping.RestaurantSummaryDtoMapper>();
            container.RegisterType<Model.Mapping.IRecurrenceEventDtoMapper, Model.Mapping.RecurrenceEventDtoMapper>();
            container.RegisterType<Model.Mapping.IDishesSummaryDtoMapper, Model.Mapping.DishesSummaryDtoMapper>();
            container.RegisterType<Model.Mapping.IUserNotOrderDtoMapper, Model.Mapping.UserNotOrderDtoMapper>();
            container.RegisterType<Model.Mapping.INewGraphUserDtoMapper, Model.Mapping.NewGraphUserDtoMapper>();
            container.RegisterType<Model.Mapping.ICustomGroupDtoMapper, Model.Mapping.CustomGroupDtoMapper>();
            container.RegisterType<Model.Mapping.IUserReorderDtoMapper, Model.Mapping.UserReorderDtoMapper>();
            container.RegisterType<Model.Mapping.IEventPromotionDtoMapper, Model.Mapping.EventPromotionDtoMapper>();
            container.RegisterType<Model.Mapping.IPromotionDtoMapper, Model.Mapping.PromotionDtoMapper>();
            container.RegisterType<Model.Mapping.IEventUserDtoMapper, Model.Mapping.EventUserDtoMapper>();
            container.RegisterType<Model.Mapping.IUserOrderDtoMapper, Model.Mapping.UserOrderDtoMapper>();
            container.RegisterType<Model.Mapping.IExcelModelDtoMapper, Model.Mapping.ExcelModelDtoMapper>();
            container.RegisterType<Model.Mapping.IDelegateHostDtoMapper, Model.Mapping.DelegateHostDtoMapper>();

            container.RegisterType<Model.Mapping.IUserNotOrderEmailDtoMapper, Model.Mapping.UserNotOrderEmailDtoMapper>();
            container.RegisterType<Model.Mapping.IGroupDtoMapper, Model.Mapping.GroupDtoMapper>();
            container.RegisterType<Model.Mapping.IFeedbackDtoMapper, Model.Mapping.FeedbackDtoMapper>();
            container.RegisterType<IGraphApiProvider, GraphApiProvider>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<ISharepointContextProvider, SharepointContextProvider>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<ITokenProvider, TokenProvider>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<ISPListService, SPListService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<ISPUserService, SPUserService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<ISummaryService, SummaryService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.RegisterType<IFeedbackService, FeedbackService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());

            container.RegisterType<FOS.Services.FosCoreService.IFosCoreService, FOS.Services.FosCoreService.FosCoreService>(
                new TransientLifetimeManager(),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<LoggingInterceptor>());
            container.AddExtension(new Diagnostic());
        }
    }
}