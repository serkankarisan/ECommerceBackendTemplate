using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        /// <summary>
        /// İş kurallarını kontrol eder ve başarısız olan ilk kuralın sonucunu döner.
        /// </summary>
        /// <param name="logics">Kontrol edilecek iş kuralları.</param>
        /// <returns>Başarısız olan ilk kuralın sonucunu döner, aksi takdirde null döner.</returns>
        public static IResult? Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic; // Başarısız olan ilk kuralı döndür
                }
            }
            return new SuccessResult(); // Tüm kurallar başarılıysa
        }

        /// <summary>
        /// Asenkron iş kurallarını kontrol eder ve başarısız olan ilk kuralın sonucunu döner.
        /// </summary>
        /// <param name="logics">Kontrol edilecek iş kuralları (asenkron).</param>
        /// <returns>Başarısız olan ilk kuralın sonucunu döner, aksi takdirde null döner.</returns>
        public static async Task<IResult?> RunAsync(params Func<Task<IResult>>[] logics)
        {
            foreach (var logic in logics)
            {
                var result = await logic();
                if (!result.Success)
                {
                    return result; // Başarısız olan ilk kuralı döndür
                }
            }
            return new SuccessResult(); // Tüm kurallar başarılıysa
        }
    }
}
