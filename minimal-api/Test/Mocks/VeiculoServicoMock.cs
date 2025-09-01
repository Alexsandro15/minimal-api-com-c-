using minimalapi.Dominio.Entidades;
using minimalapi.Dominio.Interfaces;
using minimalapi.DTOs;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        },
        new Veiculo{
            Id = 2,
            Nome = "Corolla",
            Marca = "Toyota",
            Ano = 2021
        }
    };

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(a => a.Id == id);
    }

    public Veiculo Incluir(Veiculo veiculo)
    {
        veiculo.Id = veiculos.Count() + 1;
        veiculos.Add(veiculo);

        return veiculo;
    }

    public Veiculo Apagar(Veiculo veiculo)
    {
        veiculos.Remove(veiculo);

        return veiculo;
    }

    public Veiculo Atualizar(Veiculo veiculo)
    {
        var existente = veiculos.FirstOrDefault(v => v.Id == veiculo.Id);
        
        if (existente != null)
            {
                existente.Nome = veiculo.Nome;
                existente.Marca = veiculo.Marca;
                existente.Ano = veiculo.Ano;
            }


        return veiculo;
    }

    public List<Veiculo> Todos(int? pagina, string? nome = null, string? marca = null, int? ano = null)
    {
        return veiculos;
    }
}