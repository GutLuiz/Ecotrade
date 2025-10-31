import axios from "axios";
import { Produtor } from "@/types/produtor";

export const registrarCredito = async (
  produtor: Produtor
): Promise<Produtor | null> => {
  try {
    const response = await axios.post<Produtor>(
      "https://localhost:7001/creditos/registrar", // rota certa do backend
      produtor // envia os dados do produtor
    );

    return response.data; // retorna o dado cadastrado
  } catch (error) {
    console.error("Erro ao registrar crédito:", error);
    return null;
  }
};
