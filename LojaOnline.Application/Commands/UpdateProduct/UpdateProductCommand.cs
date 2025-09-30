using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaOnline.Application.ViewModels;
using MediatR;

namespace LojaOnline.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductViewModel>
    {
        public UpdateProductCommand(int id ,string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
