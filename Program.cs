using System;
using System.Collections.Generic; // Adicione esta linha para usar List

namespace MeuPrimeiroProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            SytleFuntions func = new SytleFuntions();

            func.ColorFunction("Nome:", ConsoleColor.Blue);
            string nome = Console.ReadLine();
            
            func.ColorFunction("Senha:", ConsoleColor.Blue);
            string senha = Console.ReadLine();

            func.ColorFunction("Saldo:", ConsoleColor.Blue);
            decimal saldo = decimal.Parse(Console.ReadLine());

            BankUser user1 = new BankUser(nome, senha, saldo);
            
            //User Teste
            BankUser user2 = new BankUser("Pedro", "456", 1000);

            List<BankUser> usuarios = new List<BankUser>
            {
                user1,
                user2
            };

            bool continuar = true;

            while (continuar)
            {
                Console.Write("\n1) Sacar\n2) Depositar\n3) Transferir\n4) Consultar Saldo\n5) Sair\n\nEscolha: ");

                try
                {
                    if (int.TryParse(Console.ReadLine(), out int escolha))
                    {
                        switch (escolha)
                        {
                            case 1:
                                func.ColorFunction("Digite o valor a ser sacado:", ConsoleColor.Yellow);
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorSacar))
                                    user1.Sacar(valorSacar);
                                else
                                    func.ColorFunction("Valor inválido.", ConsoleColor.Red);
                                break;

                            case 2:
                                func.ColorFunction("Digite o valor a ser depositado:", ConsoleColor.Yellow);
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorDepositar))
                                    user1.Depositar(valorDepositar);
                                else
                                    func.ColorFunction("Valor inválido.", ConsoleColor.Red);
                                break;

                            case 3:
                                func.ColorFunction("Digite o valor a ser transferido:", ConsoleColor.Yellow);
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorTransferir))
                                {
                                    func.ColorFunction("Digite o nome do destinatário:", ConsoleColor.Yellow);
                                    string nomeDestinatario = Console.ReadLine();
                                    user1.Transferir(valorTransferir, EncontrarUsuarioPorNome(nomeDestinatario, usuarios));
                                }
                                else
                                    func.ColorFunction("Valor inválido.", ConsoleColor.Red);
                                break;

                            case 4:
                                func.ColorFunction("Digite sua senha: ", ConsoleColor.Yellow);
                                string senhaConsole = Console.ReadLine();
                                user1.ConsultarSaldo(senhaConsole);
                                break;

                            case 5:
                                func.ColorFunction("Saindo do programa.", ConsoleColor.Blue);
                                continuar = false;
                                break;

                            default:
                                func.ColorFunction("Opção inválida.", ConsoleColor.Red);
                                break;
                        }
                    }
                    else
                    {
                        func.ColorFunction("Escolha inválida.", ConsoleColor.Red);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        }

        static BankUser EncontrarUsuarioPorNome(string nome, List<BankUser> usuarios)
        {
            return usuarios.Find(u => u.Nome == nome);
        }
    }
}
