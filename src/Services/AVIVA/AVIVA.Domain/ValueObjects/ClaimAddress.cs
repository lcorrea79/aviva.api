namespace AVIVA.Domain.ValueObjects
{
    /// <summary>
    /// Claim address value object
    /// </summary>
    /// <param name="Address1"></param>
    /// <param name="City"></param>
    /// <param name="State"></param>
    /// <param name="ZipCode"></param>
    /// <param name="PhoneNumber"></param>
    public record ClaimAddress(string Address1, string City, string State, string ZipCode, string PhoneNumber);
}