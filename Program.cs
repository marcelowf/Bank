using System;
using System.Collections.Generic; // Adicione esta linha para usar List

namespace MeuPrimeiroProjeto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            BankUser user1 = new BankUser(nome, senha, 0);
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
                                Console.Write("Digite o valor a ser sacado: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorSacar))
                                    user1.Sacar(valorSacar);
                                else
                                    Console.WriteLine("Valor inválido.");
                                break;
                            case 2:
                                Console.Write("Digite o valor a ser depositado: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorDepositar))
                                    user1.Depositar(valorDepositar);
                                else
                                    Console.WriteLine("Valor inválido.");
                                break;
                            case 3:
                                Console.Write("Digite o valor a ser transferido: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal valorTransferir))
                                {
                                    Console.Write("Digite o nome do destinatário: ");
                                    string nomeDestinatario = Console.ReadLine();
                                    user1.Transferir(valorTransferir, EncontrarUsuarioPorNome(nomeDestinatario, usuarios));
                                }
                                else
                                {
                                    Console.WriteLine("Valor inválido.");
                                }
                                break;
                            case 4:
                                Console.Write("Digite sua senha: ");
                                string senhaConsole = Console.ReadLine();
                                user1.ConsultarSaldo(senhaConsole);
                                break;
                            case 5:
                                Console.WriteLine("Saindo do programa.");
                                continuar = false;
                                break;
                            default:
                                Console.WriteLine("Opção inválida.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Escolha inválida.");
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

    class BankUser
    {
        private string nome;
        private string senha;
        private decimal saldo;

        public BankUser(string nome, string senha, decimal saldo)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                throw new ArgumentException("Nenhum campo deve ser nulo.");
            }
            if (saldo < 0)
            {
                throw new Exception("Saldo não pode ser negativo.");
            }
            this.nome = nome;
            this.senha = senha;
            this.saldo = saldo;
        }

        public string Nome
        {
            get { return nome; }
        }

        public void Sacar(decimal valor)
        {
            if (valor > saldo)
            {
                throw new Exception("Valor maior que o saldo.");
            }
            this.saldo -= valor;
            Console.WriteLine($"Saque de {valor} realizado com sucesso. Saldo restante: {saldo}");
        }

        public void Depositar(decimal valor)
        {
            if (valor < 0)
            {
                throw new Exception("Valor não pode ser negativo.");
            }
            this.saldo += valor;
            Console.WriteLine($"Depósito de {valor} realizado com sucesso. Saldo atual: {saldo}");
        }

        public void ConsultarSaldo(string senhaConsole)
        {
            if (senha != senhaConsole)
            {
                throw new Exception("Senha incorreta.");
            }
            Console.WriteLine($"Saldo disponível: {saldo}");
        }

        public void Transferir(decimal valor, BankUser destino)
        {
            if (valor < 0)
            {
                throw new Exception("Valor não pode ser negativo.");
            }
            if (valor > saldo)
            {
                throw new Exception("Valor maior que o saldo.");
            }

            this.saldo -= valor;
            destino.Depositar(valor);
            Console.WriteLine($"Transferência de {valor} realizada com sucesso para {destino.nome}. Saldo restante: {saldo}");
        }
    }
}
