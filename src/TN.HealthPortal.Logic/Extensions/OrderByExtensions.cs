using TN.HealthPortal.Logic.Entities;

namespace TN.HealthPortal.Logic.Extensions
{
    public static class OrderByExtensions
    {
        public static IOrderedEnumerable<VaccinationScheme> Order(this IEnumerable<VaccinationScheme> schemes)
            => schemes
                .OrderBy(_ => _.ProductionPhase)
                .ThenBy(_ => _.PigCategory)
                .ThenBy(_ => _.Pathogen.Name)
                .ThenBy(_ => _.Timing);

        public static IOrderedEnumerable<DewormingScheme> Order(this IEnumerable<DewormingScheme> schemes)
            => schemes
                .OrderBy(_ => _.ProductionPhase)
                .ThenBy(_ => _.PigCategory)
                .ThenBy(_ => _.Timing);
    }
}
