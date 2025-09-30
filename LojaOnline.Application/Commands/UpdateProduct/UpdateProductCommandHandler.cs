using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaOnline.Application.ViewModels;
using LojaOnline.Domain.Entities;
using LojaOnline.Domain.Repositories;
using MediatR;

namespace LojaOnline.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductViewModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price
            };

            var updatedProduct = await _productRepository.UpdateAsync(product);

            if (updatedProduct == null)
                throw new KeyNotFoundException("Produto não encontrado");

            return new ProductViewModel(updatedProduct.Id,
                                        updatedProduct.Name,
                                        updatedProduct.Price);

        }
    }
}
