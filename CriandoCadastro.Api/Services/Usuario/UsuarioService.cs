using CriandoCadastro.Api.Data;
using CriandoCadastro.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace CriandoCadastro.Api.Services.Usuario;

public class UsuarioService : IUsuarioInterface
{
    private readonly AppDbContext _Context;
    public UsuarioService(AppDbContext context)
    {
        _Context = context;
    }

    public async Task<ResponseModel<IEnumerable<UsuarioModel>>> GetAll()
    {
        ResponseModel<IEnumerable<UsuarioModel>> response = new ResponseModel<IEnumerable<UsuarioModel>>();

        try
        {
            var usuarios =  await _Context.Usuarios.ToListAsync();
            
            if (usuarios.Count == 0)
            {
                response.Mensagem = "Nennhum usuario cadastrado";
                return response;
            }
            response.Dados =  usuarios;
            response.Mensagem = "Usuarios localizados com sucesso";
            
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
        return response;
    }

    public async Task<ResponseModel<UsuarioModel>> GetId(int id)
    {
        var response = new ResponseModel<UsuarioModel>();
        
        try
        {
            var usuario = await _Context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                response.Mensagem = "Nennhum usuario localizado";
                return response;
            }
            response.Dados =  usuario;
            response.Mensagem = "Usuarios localizados com sucesso";
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UsuarioModel>> Post(UsuarioModel usuario)
    {
        var response = new ResponseModel<UsuarioModel>();

        try
        {
            var usuarioInserido = await _Context.Usuarios.AddAsync(usuario);
            if (usuarioInserido == null)
            {
                response.Mensagem = "Erro ao cadastrar usuario";
                response.Status = false;
                return response;
            }
            await _Context.SaveChangesAsync();
            response.Mensagem = "Usuarios cadastrado com sucesso";
            response.Dados =  usuarioInserido.Entity;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
        return response;
    }

    public Task<ResponseModel<UsuarioModel>> Put(UsuarioModel usuario)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<UsuarioModel>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}