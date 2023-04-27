using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contas.LibClasses
{
    public class Carteira
    {
        public double Saldo
        {
            get;
            private set;
        }
        public string Dono { get; set; }
        public int ID { get; set; }
        public double CPF { get; set; }
        private static int _IDAtual = 0;
        public double limite { get; set; }  

        public Carteira()
        {
            _IDAtual++;
            this.ID = _IDAtual; 
        }

        public bool Sacar(double Valor)
        {
          
            if (Valor <= this.Saldo)
            {
                
                this.Saldo -= Valor;
                //this.Saldo = Saldo - Valor;
                return true;

            }
            else if (Valor <= this.Saldo + this.limite)
            {
                
                double restante = this.Saldo + this.limite - Valor;
                this.limite = restante;
                this.Saldo = this.Saldo - Valor;
                return true;
            }
            else { return false; }


        }
        

        public bool Depositar(double Valor)
        {
            this.Saldo += Valor;
            return true;
        }

        public bool Transferir
            (Carteira destino, double valor)
        {  
            //se nao tiver saldo cancela transferencia retornando false
            if (this.Saldo < valor)
                return false;

            //Executa transferencia tirando da conta origram e deposinto na conta destino
            this.Sacar(valor);
            bool tOK = destino.Depositar(valor);
            if (tOK)// se transferencia ocorreu com sucesso retorna true
                return true;
            else// caso ocorrer erro faz o rollback voltando dinheiro para conta de origem
            {
                this.Depositar(valor);
                return false;
            }
        }
        public bool CobrarTarifa()
        {
            double Tarifa = 19.90;
            this.Saldo -= Tarifa;
            return true;
        }
    }
}
