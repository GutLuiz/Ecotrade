import axios from "axios";
import { Usuario } from "@/types/usuario";

export const cadastrarUsuario = async (
  usuario: Usuario
): Promise<Usuario | null> => {
  try {
    const response = await axios.post<Usuario>(
      "https://localhost:7001/api/Usuarios/registrar",
      usuario // envia os dados do usuário
    );

    return response.data; // retorna o usuário cadastrado
  } catch (error) {
    console.error("Erro ao cadastrar usuário:", error);
    return null;
  }
};
