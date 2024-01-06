using System;

namespace MeuPrimeiroProjeto
{
    class BankUser
    {   
        SytleFuntions func = new SytleFuntions();
        private string nome;
        private string senha;
        private decimal saldo;

        public BankUser(string nome, string senha, decimal saldo)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                func.ColorFunction("Nenhum campo deve ser nulo.", ConsoleColor.Red);
                return;
            }
            if (saldo < 0)
            {
                func.ColorFunction("Saldo não pode ser negativo.", ConsoleColor.Red);
                return;
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
                func.ColorFunction("Valor maior que o saldo.", ConsoleColor.Red);
                return;
            }
            this.saldo -= valor;
            func.ColorFunction($"Saque de {valor} realizado com sucesso. Saldo restante: {saldo}", ConsoleColor.Green);
        }

        public void Depositar(decimal valor)
        {
            if (valor < 0)
            {   
                func.ColorFunction("Valor não pode ser negativo.", ConsoleColor.Red);
                return;
            }
            this.saldo += valor;
            func.ColorFunction($"Depósito de {valor} realizado com sucesso. Saldo atual: {saldo}", ConsoleColor.Green);
        }

        public void ConsultarSaldo(string senhaConsole)
        {
            if (senha != senhaConsole)
            {
                func.ColorFunction("Senha incorreta.", ConsoleColor.Red);
                return;
            }
            func.ColorFunction($"Saldo disponível: {saldo}", ConsoleColor.Green);
        }

        public void Transferir(decimal valor, BankUser destino)
        {
            if (valor < 0)
            {
                func.ColorFunction("Valor não pode ser negativo.", ConsoleColor.Red);
                return;
            }
            if (valor > saldo)
            {   
                func.ColorFunction("Valor maior que o saldo.", ConsoleColor.Red);
                return;
            }

            this.saldo -= valor;
            destino.Receber(valor);
            
            func.ColorFunction($"Transferência de {valor} realizada com sucesso para {destino.nome}. Saldo restante: {saldo}", ConsoleColor.Green);
        }

        public void Receber(decimal valor)
        {
            if (valor < 0)
            {
                return;
            }
            this.saldo += valor;
        }
    }
}
