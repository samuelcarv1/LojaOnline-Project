using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaOnline.Domain.Entities;
using MediatR;

namespace LojaOnline.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public CreateProductCommand(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public decimal Price { get; set; } 
    }
}
