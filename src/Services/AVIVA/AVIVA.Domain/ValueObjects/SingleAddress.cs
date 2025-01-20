#nullable enable
namespace AVIVA.Domain.ValueObjects
{
    /// <summary>
    /// Single address
    /// </summary>
    /// <param name="Address1"></param>
    /// <param name="Address2"></param>
    /// <param name="City"></param>
    /// <param name="State"></param>
    /// <param name="ZipCode"></param>
    public record SingleAddress(string? Address1, string? Address2, string? City, string? State, string? ZipCode);
}