namespace minimalapi.DTOs;
//namespace para acessar em outros arquivos
public class LoginDTO
{
    public string Email { get; set; } = default!;
    public string Senha { get; set; } = default!;
}
