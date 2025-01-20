using AVIVA.Domain.Common;

namespace AVIVA.Domain.ValueObjects
{
    public class FederalTaxIdType(int id, string name) : Enumeration(id, name)
    {
        public static readonly FederalTaxIdType SSN = new(1, nameof(SSN));
        public static readonly FederalTaxIdType EIN = new(2, nameof(EIN));
    }
}