namespace Examen_Tercer_Parcial.Data
{
    public class MySQLConfiguration
    {
        public string CadenaConexion { get; }

        public MySQLConfiguration(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }
    }
}
