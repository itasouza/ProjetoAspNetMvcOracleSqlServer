using Generico.Dominio;
using Generico.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generico.Aplicacao
{
    public class UsuarioAplicacao
    {
        private ContextoSqlServer contexto;


        public TB_USUARIO ListarPorLoginSenha(string login, string senha)
        {
            var strQuery = "";
            strQuery += " select         ";
            strQuery += "  IdUsuario,    ";
            strQuery += "  NomeUsuario,  ";
            strQuery += "  EmailUsuario, ";
            strQuery += "  LoginUsuario, ";
            strQuery += "  SenhaUsuario  ";
            strQuery += "  from TB_USUARIO ";
            strQuery += string.Format("  where LoginUsuario = '{0}' and SenhaUsuario = '{1}' ", login, senha);
            strQuery += "  and UsuarioAtivo = 'S' ";
            using (contexto = new ContextoSqlServer())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaObjetos(retornoDataReader).FirstOrDefault();
            }

        }


        public TB_USUARIO ListarPorEmailLoginSenha(string email)
        {
            var strQuery = "";
            strQuery += " select  ";
            strQuery += "  IdUsuario,  ";
            strQuery += "  NomeUsuario,  ";
            strQuery += "  EmailUsuario,  ";
            strQuery += "  LoginUsuario,  ";
            strQuery += "  SenhaUsuario  ";
            strQuery += "  from TB_USUARIO ";
            strQuery += string.Format("  where EmailUsuario = '{0}' ", email);

            using (contexto = new ContextoSqlServer())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaObjetos(retornoDataReader).FirstOrDefault();
            }

        }



        private List<TB_USUARIO> TransformaReaderEmListaObjetos(SqlDataReader reader)
        {
            var retornando = new List<TB_USUARIO>();
            while (reader.Read())
            {

                TB_USUARIO tabela = new TB_USUARIO()
                {
                    IdUsuario = reader["IdUsuario"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdUsuario"]),
                    NomeUsuario = reader["NomeUsuario"] == DBNull.Value ? string.Empty : reader["NomeUsuario"].ToString(),
                    EmailUsuario = reader["EmailUsuario"] == DBNull.Value ? string.Empty : reader["EmailUsuario"].ToString(),
                    LoginUsuario = reader["LoginUsuario"] == DBNull.Value ? string.Empty : reader["LoginUsuario"].ToString(),
                    SenhaUsuario = reader["SenhaUsuario"] == DBNull.Value ? string.Empty : reader["SenhaUsuario"].ToString()
                };

                retornando.Add(tabela);
            }
            reader.Close();
            return retornando;
        }



    }
}
