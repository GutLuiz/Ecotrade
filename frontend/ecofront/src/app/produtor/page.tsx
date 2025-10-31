"use client";
import { useState } from "react";
import axios from "axios";

type CreditoCarbono = {
  produtorId: number;
  quantidade: number;
  origem: string;
  dataGeracao: string; // formato ISO (input type="date")
  status?: string;
};

export default function RegistrarCreditoCarbono() {
  const [formData, setFormData] = useState<CreditoCarbono>({
    produtorId: 0,
    quantidade: 0,
    origem: "",
    dataGeracao: "",
    status: "pendente",
  });

  const [mensagem, setMensagem] = useState("");

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post(
        "https://localhost:7001/api/Creditos/registrar",
        formData
      );
      setMensagem("Crédito de carbono registrado com sucesso!");
      setFormData({
        produtorId: 0,
        quantidade: 0,
        origem: "",
        dataGeracao: "",
        status: "pendente",
      });
    } catch (error) {
      console.error(error);
      setMensagem("Erro ao registrar crédito de carbono.");
    }
  };

  return (
    <div className="flex justify-center items-center min-h-screen bg-gray-100">
      <form
        onSubmit={handleSubmit}
        className="bg-white shadow-lg rounded-xl p-8 w-full max-w-md"
      >
        <h1 className="text-2xl font-semibold text-center mb-6 text-green-700">
          Registrar Crédito de Carbono
        </h1>

        <div className="mb-4">
          <label className="block text-gray-700 mb-1">Código do Produtor</label>
          <input
            type="number"
            name="produtorId"
            value={formData.produtorId}
            onChange={handleChange}
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none focus:ring-2 focus:ring-green-500"
            required
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 mb-1">Quantidade (tCO₂)</label>
          <input
            type="number"
            step="0.01"
            name="quantidade"
            value={formData.quantidade}
            onChange={handleChange}
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none focus:ring-2 focus:ring-green-500"
            required
          />
        </div>

        <div className="mb-4">
          <label className="block text-gray-700 mb-1">Origem</label>
          <input
            type="text"
            name="origem"
            value={formData.origem}
            onChange={handleChange}
            placeholder="Ex: Reflorestamento, energia solar..."
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none focus:ring-2 focus:ring-green-500"
            required
          />
        </div>

        <div className="mb-6">
          <label className="block text-gray-700 mb-1">Data de Geração</label>
          <input
            type="date"
            name="dataGeracao"
            value={formData.dataGeracao}
            onChange={handleChange}
            className="w-full border border-gray-300 rounded-lg p-2 focus:outline-none focus:ring-2 focus:ring-green-500"
            required
          />
        </div>

        <button
          type="submit"
          className="w-full bg-green-600 text-white font-semibold py-2 rounded-lg hover:bg-green-700 transition"
        >
          Registrar
        </button>

        {mensagem && (
          <p className="text-center mt-4 text-sm text-gray-600">{mensagem}</p>
        )}
      </form>
    </div>
  );
}
