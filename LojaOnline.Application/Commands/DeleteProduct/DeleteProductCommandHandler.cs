using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaOnline.Domain.Repositories;
using MediatR;

namespace LojaOnline.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _productRepository.DeleteAsync(request.Id);

            if (!deleted)
                throw new KeyNotFoundException($"Produto com ID {request.Id} não encontrado");

            return Unit.Value;
        }
    }
}
