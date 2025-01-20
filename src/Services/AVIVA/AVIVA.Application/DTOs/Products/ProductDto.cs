namespace AVIVA.Application.DTOs.Products
{
    public record ProductDto(
         int Id,
         string Name,
         string Details,
         decimal UnitPrice,
         bool Status
    );


}