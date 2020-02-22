using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace JHW.Web
{
    public class ActionConfig
    {
        public static void ConfigActions()
        {
            //数据服务
            var service = DependencyResolver.Current.GetService<IService.IService<JHW.Models.EntityModels.Actions>>();

            var assembly = Assembly.GetExecutingAssembly();
            var assemblyHash = assembly.GetAssemblyHash();//获取程序集的Hash值
            var single = service.QuerySingle(a => a.AssemblyHash == assemblyHash);

            //程序集没有发生改变时，不再进行反射获取控制器及Action
            if (null != single)
            {
                return;
            }

            var actions = new List<JHW.Models.EntityModels.Actions>();
            var controllerTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)));
            
            foreach (Route route in RouteTable.Routes.Where(r => r.GetType() == typeof(Route)))
            {
                var namespaces = route?.DataTokens["Namespaces"] as string[] ?? new[] { "JHW.Web.Controllers" }; //命名空间
                var areaName = route.DataTokens["area"] as string;
                foreach (var type in controllerTypes.Where(t => namespaces.Any(ns => t.Namespace == ns))) //按命名空间来获取类型
                {
                    var controllerIndex = type.Name.IndexOf(nameof(Controller));
                    var controllerName = controllerIndex < 0 ? type.Name : type.Name.Substring(0, controllerIndex);

                    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                    foreach (var method in methods.GroupBy(m => m.Name).Select(g => g.First()))
                    {
                        if (method.ReturnType != typeof(ActionResult) || null != method.GetCustomAttribute<NonActionAttribute>() || null != method.GetCustomAttribute<ChildActionOnlyAttribute>())
                        {
                            continue;
                        }

                        var actionName = method.GetCustomAttribute<ActionNameAttribute>()?.Name ?? method.Name;
                        var desc = GetActionDescription(methods, method, actionName);

                        actions.Add(new JHW.Models.EntityModels.Actions
                        {
                            Id = Guid.NewGuid(),
                            Area = areaName,
                            Controller = controllerName,
                            Action = actionName,
                            Type = 0,//0默认是公开的
                            Remark = desc,
                            AssemblyHash = assemblyHash
                        });
                    }
                }
            }

            DependencyResolver.Current.GetService<IService.IService<JHW.Models.EntityModels.RoleActions>>()?.ClearTable();
            service?.ClearTable();
            service?.Insert(actions.OrderBy(a => a.Area));
        }

        private static string GetActionDescription(MethodInfo[] methods, MethodInfo method, string defaultName)
        {
            foreach (var m in methods.Where(t => t.Name == method.Name))
            {
                var actionAttr = m.GetCustomAttribute<Attributes.ActionDescriptionAttribute>();
                if (null != actionAttr)
                {
                    return actionAttr.Name;
                }
            }

            return defaultName;
        }
    }
}