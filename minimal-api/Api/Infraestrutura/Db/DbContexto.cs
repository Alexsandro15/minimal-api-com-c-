using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using minimalapi.Dominio.Entidades;

namespace minimalapi.Infraestrutura.Db;

//esse dbContext vem do EntityFrameworkCore e Ele serve como uma ponte entre o seu código C# e o banco de dados
//permiti = Conectar, consultar, inserir, atualizar, deletar e configurar mapeamentos entre classes C# e tabelas de bancos
public class DbContexto : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings;
    public DbContexto(IConfiguration configuracaoAppSettings)
    {
        _configuracaoAppSettings = configuracaoAppSettings;
    }
    // Define as "tabelas" do banco de dados
    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Veiculo> Veiculos { get; set; } = default!;

    // Configuração do banco de dados

    /*
    Esse método é chamado quando o EF Core está criando o modelo do banco de dados (a representação das entidades no banco).

    Aqui você pode configurar entidades, chaves, relacionamentos e até inserir dados iniciais.

    Ele substitui a configuração padrão baseada apenas nos atributos ([Key], [Required], etc.).
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Administrador>()
        //Diz para o EF Core que vamos configurar a entidade Administrador (uma classe que você criou no seu projeto, provavelmente algo como public class Administrador { ... }).

        //.HasData(...)
        /*
        Método usado para definir dados iniciais (seeding) que devem ser inseridos no banco quando você rodar a migration.

        Diferente de um INSERT manual, isso vira parte do histórico de migrations do EF Core.
        */
        modelBuilder.Entity<Administrador>().HasData(
            //O EF Core vai transformar isso em um INSERT INTO Administradores (...) VALUES (...) durante a criação/migração do banco.
            //é um objeto com valore iniciais
            new Administrador
            {
                Id = 1,
                Email = "adminstrador@gmail.com",
                Senha = "123456",
                Perfil = "Adm"

            }
        );
    }
    //Serve para configurar como o DbContext vai se conectar ao banco de dados, caso não tenha sido configurado via AddDbContext na injeção de dependência.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Garante que você não sobrescreva uma configuração existente.

        //Útil quando você registra o DbContext com builder.Services.AddDbContext<DbContexto>(...) no Program.cs.
        if (!optionsBuilder.IsConfigured)
        {
            //Pega a string de conexão do arquivo appsettings.json usando uma instância de IConfiguration (_configuracaoAppSettings).
            var stringConexao = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();

            //Só configura o DbContext se a string de conexão existir e não estiver vazia.
            if (!string.IsNullOrEmpty(stringConexao))
            {
                //Configura o EF Core para usar MySQL.

                //ServerVersion.AutoDetect(stringConexao) detecta automaticamente a versão do MySQL para garantir compatibilidade.
                optionsBuilder.UseMySql(
                stringConexao,
                ServerVersion.AutoDetect(stringConexao));
            }
        }

    }
}