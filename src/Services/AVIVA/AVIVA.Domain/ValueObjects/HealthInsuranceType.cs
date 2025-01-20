using AVIVA.Domain.Common;

namespace AVIVA.Domain.ValueObjects
{
    /// <summary>
    /// Health insurance type
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public class HealthInsuranceType(int id, string name) : Enumeration(id, name)
    {
        /// <summary>
        /// Medicare
        /// </summary>
        public static readonly HealthInsuranceType Medicare = new(1, nameof(Medicare));
        /// <summary>
        /// Medicaid
        /// </summary>
        public static readonly HealthInsuranceType Medicaid = new(2, nameof(Medicaid));
        /// <summary>
        /// Tricare
        /// </summary>
        public static readonly HealthInsuranceType Tricare = new(3, nameof(Tricare));
        /// <summary>
        /// Champva
        /// </summary>
        public static readonly HealthInsuranceType Champva = new(4, nameof(Champva));
        /// <summary>
        /// Group health plan
        /// </summary>
        public static readonly HealthInsuranceType GroupHealthPlan = new(5, nameof(GroupHealthPlan));
        /// <summary>
        /// FECA
        /// </summary>
        public static readonly HealthInsuranceType FECA = new(6, nameof(FECA));
        /// <summary>
        /// Other
        /// </summary>
        public static readonly HealthInsuranceType Other = new(7, nameof(Other));
    }
}