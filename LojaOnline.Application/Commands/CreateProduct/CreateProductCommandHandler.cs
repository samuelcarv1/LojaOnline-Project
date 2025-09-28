using System.Data;
using LojaOnline.Domain.Entities;
using LojaOnline.Domain.Repositories;
using MediatR;

namespace LojaOnline.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            var id = await _productRepository.AddAsync(product);

            return id;
        }
    }
}
