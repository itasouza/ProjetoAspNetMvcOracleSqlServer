using System;
using System.Configuration;
using System.Data;
using Oracle.DataAccess.Client;

namespace Generico.Repositorio
{
    public class ContextoOracle : IDisposable
    {
        private readonly OracleConnection minhaConexao;

        public ContextoOracle()
        {
            minhaConexao = new OracleConnection(ConfigurationManager.AppSettings["Conexao"]);
            minhaConexao.Open();
        }

        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new OracleCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };

            cmdComando.ExecuteNonQuery();
        }

        public OracleDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new OracleCommand(strQuery, minhaConexao);
            return cmdComando.ExecuteReader();
        }

        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
                minhaConexao.Close();
        }
    }
}