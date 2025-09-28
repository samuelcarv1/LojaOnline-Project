using LojaOnline.Application.ViewModels;
using LojaOnline.Domain.Entities;
using MediatR;

namespace LojaOnline.Application.Queries.GetProduct
{
    public class GetProductQuery : IRequest<List<ProductViewModel>>
    {
    }
}
