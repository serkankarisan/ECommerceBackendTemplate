using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        // Method çağrılmadan önce yapılacak işlemler
        protected virtual void OnBefore(IInvocation invocation) { }

        // Method çağrıldıktan sonra yapılacak işlemler
        protected virtual void OnAfter(IInvocation invocation) { }

        // Hata durumu ile karşılaşıldığında yapılacak işlemler
        protected virtual void OnException(IInvocation invocation, Exception exception) { }

        // Method başarıyla tamamlandığında yapılacak işlemler
        protected virtual void OnSuccess(IInvocation invocation) { }

        // Metod çalışırken yapılacak işlemler
        public override void Intercept(IInvocation invocation)
        {
            OnBefore(invocation); // Method öncesi işlem
            try
            {
                invocation.Proceed(); // Methodu çalıştır
                OnSuccess(invocation); // Başarıyla tamamlanmışsa işlem
            }
            catch (Exception ex)
            {
                OnException(invocation, ex); // Hata durumunda işlem
                throw; // Hata dışarıya fırlatılır
            }
            finally
            {
                OnAfter(invocation); // Her durumda yapılacak işlem
            }
        }
    }
}
