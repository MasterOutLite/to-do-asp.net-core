using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IMvcBuilder AddPresentation(this IMvcBuilder service)
        {
            service.AddApplicationPart(Assembly.GetExecutingAssembly());
            return service;
        }
    }
}