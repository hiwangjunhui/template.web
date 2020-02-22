using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;

namespace JHW.TypeContainers
{
    public class Container
    {
        public static void SetDependencyResolver()
        {
            var container = GetContainer();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
        }

        //Get container
        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            var allAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var select = new Func<Assembly, Type, IEnumerable<Type>>((assembly, destType) => assembly.GetTypes().Where(t => null != t.GetInterface(destType.FullName)));

            //注入ICache及其实现类
            var cacheTypes = allAssemblies.SelectMany(a => select(a, typeof(ICache.ICache))).ToArray();
            if (!cacheTypes.Any())
            {
                throw new RegisterTypeNotFoundException($"未能找到或加载类型{typeof(ICache.ICache).FullName}的实现类型");
            }

            builder.RegisterTypes(cacheTypes).As<ICache.ICache>().SingleInstance();

            //注入IDAL及其实现类
            var dalTypes = allAssemblies.SelectMany(a => select(a, typeof(IDAL.IDAL<>))).ToArray();
            Array.ForEach(dalTypes, type => builder.RegisterGeneric(type).As(typeof(IDAL.IDAL<>))); //泛型注入

            //注入IDALAsync及其实现类（数据的异步操作）
            var dalAsyncTypes = allAssemblies.SelectMany(a => select(a, typeof(IDAL.IDALAsync<>))).ToArray();
            Array.ForEach(dalAsyncTypes, type => builder.RegisterGeneric(type).As(typeof(IDAL.IDALAsync<>))); //泛型注入

            //注入IService及其实现类
            var serviceTypes = allAssemblies.SelectMany(a => select(a, typeof(IService.IService<>))).ToArray();
            Array.ForEach(serviceTypes, type => builder.RegisterGeneric(type).As(typeof(IService.IService<>))); //泛型注入

            //注入ILog及其实现类
            var logTypes = allAssemblies.SelectMany(a => select(a, typeof(ILog.ILog))).ToArray();
            builder.RegisterTypes(logTypes).As<ILog.ILog>();

            //注入控制器
            var assemblys = allAssemblies.Where(a => select(a, typeof(IController))?.Any() ?? false).ToArray();
            builder.RegisterControllers(assemblys);

            return builder.Build();
        }
    }
}
