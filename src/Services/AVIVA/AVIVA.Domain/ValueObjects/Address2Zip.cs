namespace AVIVA.Domain.ValueObjects
{
    public record Address2Zip(string Address1, string? Address2, string City, string State, string ZipCode, string? ZipCodePlus4);
}