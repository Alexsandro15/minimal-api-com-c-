using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimalapi.Dominio.Entidades;
using minimalapi.Dominio.Servicos;
using minimalapi.Infraestrutura.Db;
using minimalapi.DTOs;

namespace Test.Domain.Servicos;

[TestClass]
public class AdministradorServicoTest
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
    public void TestandoSalvarAdministrador()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);

        // Assert
        Assert.AreEqual(1, administradorServico.Todos(1).Count());
    }

    [TestMethod]
    public void TestandoMetodoTodos()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador { Email = "teste@teste.com", Senha = "teste", Perfil = "Adm" };
        var adm2 = new Administrador { Email = "teste2@teste.com", Senha = "teste2", Perfil = "Editor" };
        var adm3 = new Administrador { Email = "teste3@teste.com", Senha = "teste3", Perfil = "Editor" };
        var adm4 = new Administrador { Email = "teste4@teste.com", Senha = "teste4", Perfil = "Editor" };
        var adm5 = new Administrador { Email = "teste5@teste.com", Senha = "teste5", Perfil = "Editor" };
        var adm6 = new Administrador { Email = "teste6@teste.com", Senha = "teste6", Perfil = "Editor" };
        var adm7 = new Administrador { Email = "teste7@teste.com", Senha = "teste7", Perfil = "Editor" };
        var adm8 = new Administrador { Email = "teste8@teste.com", Senha = "teste8", Perfil = "Editor" };
        var adm9 = new Administrador { Email = "teste9@teste.com", Senha = "teste9", Perfil = "Editor" };
        var adm10 = new Administrador { Email = "teste10@teste.com", Senha = "teste10", Perfil = "Editor" };
        var adm11 = new Administrador { Email = "teste11@teste.com", Senha = "teste11", Perfil = "Editor" };

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        administradorServico.Incluir(adm2);
        administradorServico.Incluir(adm3);
        administradorServico.Incluir(adm4);
        administradorServico.Incluir(adm5);
        administradorServico.Incluir(adm6);
        administradorServico.Incluir(adm7);
        administradorServico.Incluir(adm8);
        administradorServico.Incluir(adm9);
        administradorServico.Incluir(adm10);
        administradorServico.Incluir(adm11);

        // Assert
        Assert.AreEqual(10, administradorServico.Todos(1).Count());
        Assert.AreEqual(1, administradorServico.Todos(2).Count());
    }
    [TestMethod]
    public void TestandoLogin()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var loginDTOTest = new LoginDTO
        {
            Email = "teste@teste.com",
            Senha = "teste"
        };
        
        var loginDTOTest2 = new LoginDTO
        {
            Email = "teste2@teste.com",
            Senha = "teste2"
        };

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var LogadoTrue = administradorServico.Login(loginDTOTest);
        var LogadoFalse = administradorServico.Login(loginDTOTest2);

        // Assert
        Assert.AreEqual(loginDTOTest.Email, LogadoTrue.Email);
        Assert.AreEqual(loginDTOTest.Senha, LogadoTrue.Senha);

        Assert.AreEqual(null, LogadoFalse);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "teste@teste.com";
        adm.Senha = "teste";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);

        // Act
        administradorServico.Incluir(adm);
        var admDoBanco = administradorServico.BuscaPorId(adm.Id);

        // Assert
        Assert.AreEqual(1, admDoBanco?.Id);
    }
}