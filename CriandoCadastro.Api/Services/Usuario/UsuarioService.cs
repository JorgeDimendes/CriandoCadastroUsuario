using CriandoCadastro.Api.Data;
using CriandoCadastro.Api.DTO.Usuario;
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

    public async Task<ResponseModel<UsuarioCriacaoDto>> Post(UsuarioCriacaoDto usuarioCriacao)
    {
        var response = new ResponseModel<UsuarioCriacaoDto>();

        try
        {
            if (!verificaSeExisteEmailUsuarioRepetido(usuarioCriacao))
            {
                response.Mensagem = "Email/Usuario já cadastrado";
                return response;
            }
            
            
            
            var usuarioInserido = await _Context.AddAsync(usuarioCriacao);
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

    public async Task<ResponseModel<UsuarioModel>> Delete(int id)
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
            _Context.Remove(usuario);
            await _Context.SaveChangesAsync();
            
            response.Mensagem = $"Usuario {usuario.Nome} removido com sucesso";
            return response;
        }
        catch (Exception ex)
        {
            response.Mensagem = ex.Message;
            response.Status = false;
            return response;
        }
    }
    
    //Verificacao
    private bool verificaSeExisteEmailUsuarioRepetido(UsuarioCriacaoDto usuarioCriacao)
    {
        var usuario = _Context.Usuarios.FirstOrDefault(item => item.Email == usuarioCriacao.Email ||
                                                                          item.Usuario == usuarioCriacao.Usuario);
        if (usuario != null)
        {
            return false;
        }
        return true;
    }
}