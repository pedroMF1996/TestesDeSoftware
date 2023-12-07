using Nerdstore.Vendas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerdstore.Vendas.Domain.Repositories
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
    }
}
