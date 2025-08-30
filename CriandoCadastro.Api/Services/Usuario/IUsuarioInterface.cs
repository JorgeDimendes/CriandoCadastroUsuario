using CriandoCadastro.Api.Model;

namespace CriandoCadastro.Api.Services.Usuario;

public interface IUsuarioInterface
{
    Task<ResponseModel<IEnumerable<UsuarioModel>>> GetAll();
    Task<ResponseModel<UsuarioModel>> GetId(int id);
    Task<ResponseModel<UsuarioModel>> Post(UsuarioModel usuario);
    Task<ResponseModel<UsuarioModel>> Put(UsuarioModel usuario);
    Task<ResponseModel<UsuarioModel>> Delete(int id);
}