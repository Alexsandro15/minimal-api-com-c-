using minimalapi.Dominio.Enums;

namespace minimalapi.DTOs;
//namespace para acessar em outros arquivos
public class AdministradorDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
    public Perfil? Perfil { get; set; } = default!;
}
