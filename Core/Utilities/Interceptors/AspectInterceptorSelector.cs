using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            // Sınıf seviyesindeki attribute'ları alıyoruz
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            // Metot seviyesindeki attribute'ları alıyoruz
            var methodAttributes = method.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            // Attribute'ları birleştiriyoruz
            classAttributes.AddRange(methodAttributes);

            // Varsayılan olarak bir ExceptionLogAspect ekliyoruz
            classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            // Öncelik sırasına göre sıralayıp diziye dönüştürüyoruz
            return classAttributes
                .OrderBy(static attribute => attribute.Priority)
                .ToArray();
        }
    }
}
