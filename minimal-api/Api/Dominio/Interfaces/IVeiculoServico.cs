using minimalapi.DTOs;
using minimalapi.Dominio.Entidades;

namespace minimalapi.Dominio.Interfaces;

public interface IVeiculoServico
{
    List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null, int? ano = null);
    Veiculo? BuscaPorId(int id);
    Veiculo Incluir(Veiculo veiculo);

    Veiculo Atualizar(Veiculo veiculo);

    Veiculo Apagar(Veiculo veiculo);
}