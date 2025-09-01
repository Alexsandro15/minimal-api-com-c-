using minimalapi.Dominio.Interfaces;
using minimalapi.Dominio.Entidades;
using minimalapi.DTOs;
using minimalapi.Infraestrutura.Db;
using Microsoft.EntityFrameworkCore;

namespace minimalapi.Dominio.Servicos;

public class VeiculoServico : IVeiculoServico
{
    private readonly DbContexto _contexto;

    public VeiculoServico(DbContexto contexto)
    {
        _contexto = contexto;
    }

    public Veiculo Apagar(Veiculo veiculo)
    {
        _contexto.Veiculos.Remove(veiculo);
        _contexto.SaveChanges();

        return veiculo;
    }

    public Veiculo Atualizar(Veiculo veiculo)
    {
        _contexto.Veiculos.Update(veiculo);
        _contexto.SaveChanges();

        return veiculo;
    }

    public Veiculo? BuscaPorId(int id)
    {
        return _contexto.Veiculos.Where(v => v.Id == id).FirstOrDefault();
    }

    public Veiculo Incluir(Veiculo veiculo)
    {
        _contexto.Veiculos.Add(veiculo);
        _contexto.SaveChanges();

        return veiculo;
    }

    public List<Veiculo> Todos(int? pagina = 1, string? nome = null, string? marca = null, int? ano = null)
    {
        var query = _contexto.Veiculos.AsQueryable();
        if (!string.IsNullOrEmpty(nome))
        {
            query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
        }

        int itensPorPagina = 10;

        if (pagina != null)
        {
            query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }
        

        return query.ToList();
    }
}