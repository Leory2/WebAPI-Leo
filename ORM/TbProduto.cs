using System;
using System.Collections.Generic;

namespace RDLSuperMarket.ORM;

public partial class TbProduto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public byte[]? Notaff { get; set; }

    public virtual ICollection<TbVendum> TbVenda { get; set; } = new List<TbVendum>();
}
