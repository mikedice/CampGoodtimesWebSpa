using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using CampGoodtimesSpa.Services;
using System.Web.Http.Dependencies;

namespace CampGoodtimesSpa
{
    class UnityDependencyResolver : IDependencyResolver
    {
        IUnityContainer container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            object result = null;
            try
            {
                result = this.container.Resolve(serviceType);
                if (result != null)
                {
                    return result;
                }
            }
            catch { }
            return result;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> result = null;

            try
            {
                result = this.container.ResolveAll(serviceType);
                if (result != null)
                {
                    return result;
                }
            }
            catch { }

            return result;
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityDependencyResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}