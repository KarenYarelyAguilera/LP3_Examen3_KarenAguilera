using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using Dapper;

namespace Datos.Repositorios;

public class UsuarioRepositorio:IUsuarioRepositorio
{
    private string CadenaConexion;

    public UsuarioRepositorio(string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }

    private MySqlConnection Conexion()
    {
        return new MySqlConnection(CadenaConexion);
    }



    public async Task<bool> Actualizar(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "UPDATE usuario SET CodigoUsuario = @CodigoUsuario, NombreUsuario = @NombreUsuario, Rol = @Rol, Contraseña = @Contraseña WHERE CodigoUsuario = @CodigoUsuario ;";
            resultado = await conexion.ExecuteAsync(sql, new
            {
                usuario.CodigoUsuario,
                usuario.NombreUsuario,
                usuario.Rol,
                usuario.Contraseña,
                
            });
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public async Task<bool> Eliminar(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "DELETE FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
            resultado = await conexion.ExecuteAsync(sql, new { usuario.CodigoUsuario });
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<Usuario>> GetLista()
    {
        IEnumerable<Usuario> lista = new List<Usuario>();

        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT * FROM usuario;";
            lista = await conexion.QueryAsync<Usuario>(sql);
        }
        catch (Exception ex)
        {
        }
        return lista;
    }

    public async Task<Usuario> GetPorCodigo(string codigoUsuario)
    {
        Usuario user = new Usuario();
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT * FROM usuario WHERE CodigoUsuario = @CodigoUsuario;";
            user = await conexion.QueryFirstAsync<Usuario>(sql, new { codigoUsuario });
        }
        catch (Exception)
        {
        }
        return user;
    }

    public async Task<bool> Nuevo(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "INSERT INTO usuario (CodigoUsuario, NombreUsuario, Rol, Contraseña) VALUES (@CodigoUsuario, @NombreUsuario, @Rol, @Contraseña)";
            resultado = await conexion.ExecuteAsync(sql, usuario);
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ValidaUsuario(Login login)
    {
        bool valido = false;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Contraseña = @Contraseña;";
            valido = await conexion.ExecuteScalarAsync<bool>(sql, new { login.CodigoUsuario, login.Contraseña });
        }
        catch (Exception ex)
        {
        }
        return valido;
    }
}
