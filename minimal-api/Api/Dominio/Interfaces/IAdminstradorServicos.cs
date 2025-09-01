using minimalapi.DTOs;
using minimalapi.Dominio.Entidades;

namespace minimalapi.Dominio.Interfaces;

public interface IAdministradorServico
{
    Administrador? Login(LoginDTO loginDTO);

    Administrador Incluir(Administrador administrador);
    List<Administrador> Todos(int? pagina);
    Administrador? BuscaPorId(int id);

}