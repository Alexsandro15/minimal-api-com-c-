using System.Net;
using System.Text;
using System.Text.Json;
using minimalapi.Dominio.ModelViews;
using minimalapi.DTOs;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class VeiculoRequestTest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }
    
    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        //Login Arrange
        var loginDTO = new LoginDTO{
            Email = "teste@teste.com",
            Senha = "teste"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8,  "Application/json");

        //Login Act 
        var response = await Setup.client.PostAsync("/administradores/login", content);

        //Login Assert 
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var loginResult = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(loginResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        Setup.client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", admLogado!.Token);
            
        // Arrange
        var veiculoDTO = new VeiculoDTO
        {
            Nome = "Civic",
            Marca = "Honda",
            Ano = 2022
        };

        var contentVeiculo = new StringContent(JsonSerializer.Serialize(veiculoDTO), Encoding.UTF8,  "Application/json");

        // Act
        var responseVeiculo = await Setup.client.PostAsync("/veiculos", contentVeiculo);

        // Assert
        var resultVeiculo = await responseVeiculo.Content.ReadAsStringAsync();
        var veiculoLogado = JsonSerializer.Deserialize<VeiculoDTO>(resultVeiculo, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(veiculoLogado?.Nome ?? "");
        Assert.IsNotNull(veiculoLogado?.Marca ?? "");
        Assert.IsNotNull(veiculoLogado?.Ano);  

    }
}