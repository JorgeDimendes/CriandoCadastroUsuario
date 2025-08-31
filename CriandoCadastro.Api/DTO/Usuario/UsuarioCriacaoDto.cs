using System.ComponentModel.DataAnnotations;

namespace CriandoCadastro.Api.DTO.Usuario;

public class UsuarioCriacaoDto
{
    [Required(ErrorMessage = "Digite o Usuario")]
    public string Usuario { get; set; }
    
    [Required(ErrorMessage = "Digite o Nome")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "Digite o Sobrenome")]
    public string Sobrenome { get; set; }
    
    [Required(ErrorMessage = "Digite o Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Digite a senha")]
    public string Senha { get; set; }
    
    [Required(ErrorMessage = "Confirme a senha"), Compare("Senha", ErrorMessage = "A senha deve ser igual")]
    public string ConfirmaSenha { get; set; }
    
    public string Token { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
}