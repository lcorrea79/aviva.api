namespace AVIVA.Domain.ValueObjects
{
    public record PayerAddress(string Address1, string? Address2, string City, string State, string ZipCodePlus4);
}