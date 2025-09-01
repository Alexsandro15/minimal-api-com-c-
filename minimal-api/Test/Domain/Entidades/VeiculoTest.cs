using minimalapi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange = preparar partes do teste
        var veiculo = new Veiculo();

        //Act(testar em si)
        veiculo.Id = 1;
        veiculo.Nome = "Civic";
        veiculo.Marca = "Honda";
        veiculo.Ano = 2022;

        //Assert(comparar se o resultado é o esperado)
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Civic", veiculo.Nome);
        Assert.AreEqual("Honda", veiculo.Marca);
        Assert.AreEqual(2022, veiculo.Ano);
    }
}