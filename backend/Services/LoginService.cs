using backend.Models;
using Backend.Conexao;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    public class LoginService
    {

        public String Registrar(Usuario usuario) 
        {

            using var comando = ConexaoServico.ConexaoPostgres.CreateCommand();

            comando.CommandText = @"
                 INSERT INTO usuarios (tipo, cnpjcpf, email, senha) VALUES (@tipo, @cnpjcpf, @email, @senha)
            ";
            comando.Parameters.AddWithValue("@tipo", usuario.Tipo);
            comando.Parameters.AddWithValue("@cnpjcpf", usuario.CnpjCpf);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);

            comando.ExecuteNonQuery();

            return "Sucesso";
        }
    }
}
