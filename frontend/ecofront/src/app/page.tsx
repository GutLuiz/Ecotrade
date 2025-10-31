"use client";

import { useState } from "react";
import { useRouter } from "next/navigation"; // ✅ Importa o router do Next.js
import { Usuario } from "@/types/usuario";
import axios from "axios";

export default function App() {
  const router = useRouter(); // ✅ Instancia o router
  const [tipo, setTipo] = useState("produtor");
  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");
  const [empresaNome, setEmpresaNome] = useState("");
  const [cpfCnpj, setCpfCnpj] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const payload: Usuario = {
      tipo,
      nome: tipo === "empresa" ? empresaNome : nome,
      email,
      senha,
      cpf: cpfCnpj,
    };

    console.log("Enviando:", payload);

    try {
      const response = await axios.post<Usuario>(
        "https://localhost:7001/api/Usuarios/registrar",
        payload
      );

      console.log("Usuário cadastrado com sucesso:", response.data);

      // ✅ Redireciona para a página do produtor
      if (tipo === "produtor") {
        router.push("/produtor");
      } else {
        alert("Cadastro de empresa concluído!");
      }
    } catch (error) {
      console.error("Erro ao cadastrar usuário:", error);
      alert("Erro ao cadastrar. Tente novamente.");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <form
        onSubmit={handleSubmit}
        className="bg-white shadow-md rounded-2xl p-8 w-full max-w-md"
      >
        <h1 className="text-2xl font-bold text-center mb-6 text-gray-800">
          Cadastro EcoTrade
        </h1>

        <div className="mb-4">
          <label className="block text-gray-700 mb-2">Tipo de Usuário</label>
          <select
            value={tipo}
            onChange={(e) => setTipo(e.target.value)}
            className="w-full border border-gray-300 rounded-lg p-2"
          >
            <option value="produtor">Produtor</option>
            <option value="empresa">Empresa</option>
          </select>
        </div>

        {tipo === "empresa" && (
          <div className="mb-4">
            <label className="block text-gray-700 mb-2">Nome da Empresa</label>
            <input
              type="text"
              value={empresaNome}
              onChange={(e) => setEmpresaNome(e.target.value)}
              placeholder="Digite o nome da empresa"
              className="w-full border border-gray-300 rounded-lg p-2"
            />
          </div>
        )}

        {tipo === "produtor" && (
          <div className="mb-4">
            <label className="block text-gray-700 mb-2">Nome</label>
            <input
              type="text"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
              placeholder="Digite seu nome"
              className="w-full border border-gray-300 rounded-lg p-2"
            />
          </div>
        )}

        <div className="mb-4">
          <label className="block text-gray-700 mb-2">E-mail</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="Digite seu e-mail"
            className="w-full border border-gray-300 rounded-lg p-2"
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 mb-2">Senha</label>
          <input
            type="password"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            placeholder="Digite sua senha"
            className="w-full border border-gray-300 rounded-lg p-2"
          />
        </div>

        <div className="mb-6">
          <label className="block text-gray-700 mb-2">
            {tipo === "empresa" ? "CNPJ" : "CPF"}
          </label>
          <input
            type="text"
            value={cpfCnpj}
            onChange={(e) => setCpfCnpj(e.target.value)}
            placeholder={tipo === "empresa" ? "Digite o CNPJ" : "Digite o CPF"}
            className="w-full border border-gray-300 rounded-lg p-2"
          />
        </div>

        <button
          type="submit"
          className="w-full bg-green-600 text-white font-semibold py-2 rounded-lg hover:bg-green-700 transition"
        >
          Cadastrar
        </button>
      </form>
    </div>
  );
}
