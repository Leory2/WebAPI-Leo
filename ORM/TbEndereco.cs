using System;
using System.Collections.Generic;

namespace RDLSuperMarket.ORM;

public partial class TbEndereco
{
    public int Id { get; set; }

    public string Logradouro { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public int Cep { get; set; }

    public string? PontoReferencia { get; set; }

    public int Numero { get; set; }

    public int Fkcliente { get; set; }

    public virtual TbCliente FkclienteNavigation { get; set; } = null!;
}
