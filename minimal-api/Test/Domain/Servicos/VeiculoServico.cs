using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimalapi.Dominio.Entidades;
using minimalapi.Dominio.Servicos;
using minimalapi.Infraestrutura.Db;
using minimalapi.DTOs;

namespace Test.Domain.Servicos;

[TestClass]
public class VeiculoServicoTest
{
    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }


    [TestMethod]
    public void TestandoSalvarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo1 = new Veiculo();
        veiculo1.Nome = "Civic";
        veiculo1.Marca = "Honda";
        veiculo1.Ano = 2022;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);

        // Assert
        Assert.AreEqual(1, veiculoServico.Todos(1).Count());
    }
    [TestMethod]
    public void TestandoApagarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo1 = new Veiculo();
        veiculo1.Nome = "Civic";
        veiculo1.Marca = "Honda";
        veiculo1.Ano = 2022;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        veiculoServico.Apagar(veiculo1);

        // Assert
        Assert.AreEqual(0, veiculoServico.Todos(1).Count());
    }
    [TestMethod]
    public void TestandoAtualizarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo1 = new Veiculo();
        veiculo1.Nome = "Civic";
        veiculo1.Marca = "Honda";
        veiculo1.Ano = 2022;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo1.Id);

        veiculoDoBanco.Nome = "HB20";
        veiculoDoBanco.Marca = "Hyundai";
        veiculoDoBanco.Ano = 2020;

        veiculoServico.Atualizar(veiculoDoBanco);
        var veiculoAtualizado = veiculoServico.BuscaPorId(veiculo1.Id);

        // Assert
        Assert.AreEqual("HB20", veiculoAtualizado?.Nome);
    }
    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo1 = new Veiculo();
        veiculo1.Nome = "Civic";
        veiculo1.Marca = "Honda";
        veiculo1.Ano = 2022;

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        var veiculoDoBanco = veiculoServico.BuscaPorId(veiculo1.Id);

        // Assert
        Assert.AreEqual(1, veiculoDoBanco?.Id);
    }

    [TestMethod]
    public void TestandoMetodoTodos()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE veiculos");

        var veiculo1 = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 };
        var veiculo2 = new Veiculo { Nome = "Corolla", Marca = "Toyota", Ano = 2021 };
        var veiculo3 = new Veiculo { Nome = "Onix", Marca = "Chevrolet", Ano = 2023 };
        var veiculo4 = new Veiculo { Nome = "Golf", Marca = "Volkswagen", Ano = 2020 };
        var veiculo5 = new Veiculo { Nome = "Fiesta", Marca = "Ford", Ano = 2019 };
        var veiculo6 = new Veiculo { Nome = "HB20", Marca = "Hyundai", Ano = 2022 };
        var veiculo7 = new Veiculo { Nome = "Sandero", Marca = "Renault", Ano = 2021 };
        var veiculo8 = new Veiculo { Nome = "Peugeot 208", Marca = "Peugeot", Ano = 2023 };
        var veiculo9 = new Veiculo { Nome = "Kicks", Marca = "Nissan", Ano = 2022 };
        var veiculo10 = new Veiculo { Nome = "T-Cross", Marca = "Volkswagen", Ano = 2021 };
        var veiculo11 = new Veiculo { Nome = "Creta", Marca = "Hyundai", Ano = 2023 };

        var veiculoServico = new VeiculoServico(context);

        // Act
        veiculoServico.Incluir(veiculo1);
        veiculoServico.Incluir(veiculo2);
        veiculoServico.Incluir(veiculo3);
        veiculoServico.Incluir(veiculo4);
        veiculoServico.Incluir(veiculo5);
        veiculoServico.Incluir(veiculo6);
        veiculoServico.Incluir(veiculo7);
        veiculoServico.Incluir(veiculo8);
        veiculoServico.Incluir(veiculo9);
        veiculoServico.Incluir(veiculo10);
        veiculoServico.Incluir(veiculo11);

        // Assert
        Assert.AreEqual(10, veiculoServico.Todos(1).Count());
        Assert.AreEqual(1, veiculoServico.Todos(2).Count());
    }

}