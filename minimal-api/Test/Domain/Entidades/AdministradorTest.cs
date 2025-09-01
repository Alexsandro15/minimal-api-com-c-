using minimalapi.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class AdministradorTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        //Arrange = preparar partes do teste
        var adm = new Administrador();

        //Act(testar em si)
        adm.Id = 1;
        adm.Email = "teste@gmail.com";
        adm.Senha = "12345";
        adm.Perfil = "Adm";

        //Assert(comprarar se o resultado é o esperado)
        Assert.AreEqual(1, adm.Id);
        Assert.AreEqual("teste@gmail.com", adm.Email);
        Assert.AreEqual("12345", adm.Senha);
        Assert.AreEqual("Adm", adm.Perfil);
    }
}