
using Examen_Tercer_Parcial.Data;
using Examen_Tercer_Parcial.Interfaces;
using Modelos;
using Datos.Interfaces;
using Datos.Repositorios;

namespace Examen_Tercer_Parcial.Servicios;

public class UsuarioServicio: IUsuarioServicio
{
    private readonly MySQLConfiguration _configuration;
    private IUsuarioRepositorio usuarioRepositorio;

    public UsuarioServicio(MySQLConfiguration configuration)
    {
        _configuration = configuration;
        usuarioRepositorio = new UsuarioRepositorio(configuration.CadenaConexion);
    }

    public async Task<bool> Actualizar(Usuario usuario)
    {
        return await usuarioRepositorio.Actualizar(usuario);
    }

    public async Task<bool> Eliminar(Usuario usuario)
    {
        return await usuarioRepositorio.Eliminar(usuario);
    }

    public async Task<IEnumerable<Usuario>> GetLista()
    {
        return await usuarioRepositorio.GetLista();
    }

    public async Task<Usuario> GetPorCodigo(string codigoUsuario)
    {
        return await usuarioRepositorio.GetPorCodigo(codigoUsuario);
    }

    public async Task<bool> Nuevo(Usuario usuario)
    {
        return await usuarioRepositorio.Nuevo(usuario);
    }
}
