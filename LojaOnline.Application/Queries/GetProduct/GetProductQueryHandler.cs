using System.Xml.Linq;
using LojaOnline.Application.ViewModels;
using LojaOnline.Domain.Entities;
using LojaOnline.Domain.Repositories;
using MediatR;

namespace LojaOnline.Application.Queries.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductViewModel>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductViewModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            var projectViewModel = products.Select(p => new ProductViewModel(p.Id, p.Name, p.Price)).ToList();

            return projectViewModel;
        }
    }
}
