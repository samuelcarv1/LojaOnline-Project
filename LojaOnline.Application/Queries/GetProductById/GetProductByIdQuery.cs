
using LojaOnline.Application.ViewModels;
using MediatR;

namespace LojaOnline.Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductViewModel>
    {
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }

    }
}
