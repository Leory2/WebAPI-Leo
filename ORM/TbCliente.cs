using System;
using System.Collections.Generic;

namespace RDLSuperMarket.ORM;

public partial class TbCliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Telefone { get; set; }

    public byte[]? Documentoid { get; set; }

    public virtual ICollection<TbEndereco> TbEnderecos { get; set; } = new List<TbEndereco>();

    public virtual ICollection<TbVendum> TbVenda { get; set; } = new List<TbVendum>();
}
