using CriandoCadastro.Api.Model;
using CriandoCadastro.Api.Services.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace CriandoCadastro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuario)
        {
            _usuarioInterface = usuario;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var usuarios =  await _usuarioInterface.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetId(int id)
        {
            var usuario = await _usuarioInterface.GetId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioModel usuario)
        {
            var usuarioInserido = await _usuarioInterface.Post(usuario);
            return Ok(usuarioInserido);
        }
    }
}
