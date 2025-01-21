//using Microsoft.Azure.Functions.Worker.Extensions.DependencyInjection;
//using Ych.Api;

//[assembly: FunctionsStartup(typeof(YchApiFunctions.Startup))]

//namespace YchApiFunctions
//{
//    public class Startup : FunctionsStartup
//    {
//        /// <summary>
//        /// Called once by Azure Functions framework when a new instance is starting. Not called per endpoint invocation.
//        /// </summary>
//        public override void Configure(IFunctionsHostBuilder builder)
//        {
//            ServiceManager.Initialize();
//            ServiceManager.RegisterServices(builder.Services);
//        }
//    }
//}
