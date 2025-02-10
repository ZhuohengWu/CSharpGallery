

namespace eCommerceClean.Application.Features.ProductDto.Get
{
    public record class GetProduct : ProductResponseBase
    {
        public int Id { get; set; }
    }
}
