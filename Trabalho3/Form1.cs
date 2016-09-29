using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho3
{
    public partial class Form1 : Form
    {
        private double conta; //ACUMULA O RESULTADO DAS OPERACOES

        //ATRIBUTOS UTILIZADOS APOS SER ACIONADO O '='
        private double ultimoValor;
        private char ultimaOperacao = 'd';

        private double memoria = 0; //UTILIZADO NOS METODOS DE MEMORIA

        private bool alteracao = false; //ATRIBUTO DE CONTROLE PARA SABER SE A LABEL lb_recebeNumero FOI ALTERADA

        public Form1()
        {
            InitializeComponent();
        }

        /****FUNCOES PARA CONTROLE DO TECLADO****/
        #region funcoes_controle_teclado

        //FOCO NO IGUAL AO INICIAR O FORM
        private void Form1_Activated(object sender, EventArgs e)
        {
            btn_igual.Focus();
        }

        //PEGA O KEYPRESS E MANDA PARA O TEXTBOX
        private void btn_igual_KeyPress(object sender, KeyPressEventArgs e)
        {          
            var a = new EventArgs();

            /*****OPERACOES*****/
            #region botoes_operacoes

            //IGUALDADE OU ENTER
            if (e.KeyChar == 61 || e.KeyChar == 13)
            {
                btn_igual_Click(sender, a);
            }
            //ADICAO
            else if (e.KeyChar == 43)
            {
                btn_adicao_Click(sender, a);
            }
            //SUBTRACAO
            else if (e.KeyChar == 45)
            {
                btn_subtracao_Click(sender, a);
            }
            //MULTIPLICACAO
            else if (e.KeyChar == 42)
            {
                btn_multiplicacao_Click(sender, a);
            }
            //DIVISAO
            else if (e.KeyChar == 47)
            {
                btn_divisao_Click(sender, a);
            }
            #endregion

            /*****CARACTERES E CONTROLE*****/
            #region botes_caracteres_e_controle

            //VIRUGLA
            else if (e.KeyChar == 44)
            {
                btn_virgula_Click(sender, a);
            }
            //BACKSPACE
            else if (e.KeyChar == 8)
            {
                btn_backspace_Click(sender, a);
            }
            //ESC
            else if (e.KeyChar == 27)
            {
                btn_c_Click(sender, a);
            }
            #endregion

            //VERIFICACAO PARA ACEITAR APENAS DIGITOS
            if (char.IsDigit(e.KeyChar)) 
            {
                if (!alteracao || lb_recebeNumero.Text == "0") 
                    lb_recebeNumero.Text = string.Empty;

                if (lb_recebeNumero.Text.Length < 12)
                    lb_recebeNumero.Text += e.KeyChar.ToString();
            }
        }

        //FUNCAO ESPECIAL APENAS PARA ALGUNS ATALHOS E TECLAS ESPECIAIS
        private void btn_igual_KeyDown(object sender, KeyEventArgs e)
        {
            var a = new EventArgs();
            //DELETE => CE
            if (e.KeyCode == Keys.Delete)
            {
                btn_ce_Click(sender, a);
            }
            //SHIFT + 2 => RAIZ QUADRADA
            else if ((Control.ModifierKeys == Keys.Shift) && e.KeyValue == 50)
            {
                button2_Click(sender, a);
            }
            //SHIFT + 5 => PORCENTAGEM
            else if ((Control.ModifierKeys == Keys.Shift) && e.KeyValue == 53)
            {
                btn_porcentagem_Click(sender, a);
            }
            //CTRL + C
            else if ((Control.ModifierKeys == Keys.Control) && (e.KeyValue == 67))
            {
                tsmi_copiar_Click(sender, a);
            }
            //CTRL + V
            else if ((Control.ModifierKeys == Keys.Control) && (e.KeyValue == 86))
            {
                tsmi_colar_Click(sender, a);
            }
        }

        //VERIFICA SE FOI ALTERADO A lb_recebeNumero
        private void lb_recebeNumero_TextChanged(object sender, EventArgs e)
        {
            alteracao = true;
        }
        #endregion

        /****BOTOES NUMERICOS****/
        #region botoes_numericos

        //ZERO
        private void btn_zero_Click(object sender, EventArgs e)
        {
            escreveNumero("0");
        }

        //UM
        private void btn_um_Click(object sender, EventArgs e)
        {
            escreveNumero("1");
        }

        //DOIS
        private void btn_dois_Click(object sender, EventArgs e)
        {
            escreveNumero("2");
        }

        //TRES
        private void btn_tres_Click(object sender, EventArgs e)
        {
            escreveNumero("3");
        }

        //QUATRO
        private void btn_quatro_Click(object sender, EventArgs e)
        {
            escreveNumero("4");
        }

        //CINCO
        private void btn_cinco_Click(object sender, EventArgs e)
        {
            escreveNumero("5");
        }

        //SEIS
        private void btn_seis_Click(object sender, EventArgs e)
        {
            escreveNumero("6");
        }

        //SETE
        private void btn_sete_Click(object sender, EventArgs e)
        {
            escreveNumero("7");
        }

        //OITO
        private void btn_oito_Click(object sender, EventArgs e)
        {
            escreveNumero("8");
        }

        //NOVE
        private void btn_nove_Click(object sender, EventArgs e)
        {
            escreveNumero("9");
        }

        //VIRGULA - CASO ESPECIAL AONDE NAO VAO PARA A FUNCAO escreveNumero
        private void btn_virgula_Click(object sender, EventArgs e)
        {
            lb_recebeNumero.Text += ",";
            btn_igual.Focus();
        }
        #endregion

        /****BOTOES DE OPERACOES****/
        #region botoes_operacoes

        //IGUALDADE
        private void btn_igual_Click(object sender, EventArgs e)
        {
            if (getUltimaOperacao() != 'q')
            {
                ultimaOperacao = getUltimaOperacao();
                ultimoValor = double.Parse(lb_recebeNumero.Text);
            }
            else
            {
                if (ultimaOperacao == 'd')
                    return; //MATA A FUNCAO

                lb_recebeNumero.Text = ultimoValor.ToString();
            }                                

            executaOperacao('=');
        }

        //ADICAO
        private void btn_adicao_Click(object sender, EventArgs e)
        {
            escreveOperacoesBasicas("+");
        }

        //SUBTRACAO
        private void btn_subtracao_Click(object sender, EventArgs e)
        {
            escreveOperacoesBasicas("-");
        }

        //MULTIPLICACAO
        private void btn_multiplicacao_Click(object sender, EventArgs e)
        {
            escreveOperacoesBasicas("*");
        }

        //DIVISAO
        private void btn_divisao_Click(object sender, EventArgs e)
        {
            escreveOperacoesBasicas("/");
        }

        //INVERTE SINAL
        private void btn_inverte_sinal_Click(object sender, EventArgs e)
        {
            lb_recebeNumero.Text = (double.Parse(lb_recebeNumero.Text) * -1).ToString();
            btn_igual.Focus();
        }

        //RAIZ QUADRADA
        private void button2_Click(object sender, EventArgs e)
        {   
            if (getUltimoCaracter() == ')')
            {
                string antes, depois;
                int index = getIndexUltimoParenteses();

                //CASO A ULTIMA OPERACAO FOR UM SOBRE X
                if (lb_mostraConta.Text[index] == '/')
                {
                    antes = lb_mostraConta.Text.Substring(0, index - 1);
                    depois = lb_mostraConta.Text.Substring(index - 1);
                }
                //CASO A ULTIMA OPERACAO FOR A PROPRIA RAIZ QUADRADA
                else
                {
                    antes = lb_mostraConta.Text.Substring(0, index - 3);
                    depois = lb_mostraConta.Text.Substring(index - 3);
                }
                lb_mostraConta.Text = antes + "sqrt(" + depois + ")";
            }
            else
            {
                //CASO A ULTIMA OPERACAO TENHA SIDO A PORCENTAGEM
                if (char.IsNumber(getUltimoCaracter()))
                {
                    int index = lb_mostraConta.Text.LastIndexOf(getUltimaOperacao()) + 2;

                    if (getUltimaOperacao() == 'q')
                        index -= 1;

                    lb_mostraConta.Text = lb_mostraConta.Text.Substring(0, index);
                    lb_mostraConta.Text += "sqrt(" + double.Parse(lb_recebeNumero.Text) + ")";                    
                }
                //CASO SEJA FAZER A PRIMEIRA RAIZ
                else
                {
                    lb_mostraConta.Text += "sqrt(" + double.Parse(lb_recebeNumero.Text) + ")";
                }
            }

            double raiz = (double)Math.Sqrt(double.Parse(lb_recebeNumero.Text));
            lb_recebeNumero.Text = raiz.ToString();
            btn_igual.Focus();
        }

        //PORCENTAGEM
        private void btn_porcentagem_Click(object sender, EventArgs e)
        {
            double valor = (conta * double.Parse(lb_recebeNumero.Text)) / 100;
            lb_recebeNumero.Text = valor.ToString();

            if (char.IsNumber(getUltimoCaracter()) || getUltimoCaracter() == ')')
            {
                if (getUltimaOperacao() != 'q')
                {
                    lb_mostraConta.Text = lb_mostraConta.Text.Substring(0, lb_mostraConta.Text.LastIndexOf(getUltimaOperacao()) + 2);
                }
                else
                {
                    lb_mostraConta.Text = string.Empty;

                }
            }

            lb_mostraConta.Text += valor.ToString();
            btn_igual.Focus();
        }

        //UM SOBRE X
        private void btn_um_sobre_x_Click(object sender, EventArgs e)
        {
            if (getUltimoCaracter() == ')')
            {
                string antes, depois;
                int index = getIndexUltimoParenteses();

                //CASO A ULTIMA OPERACAO FOR UM SOBRE X
                if (lb_mostraConta.Text[index] == '/')
                {                    
                    antes = lb_mostraConta.Text.Substring(0, index - 2);
                    depois = lb_mostraConta.Text.Substring(index - 2);
                }
                //CASO A ULTIMA OPERACAO FOR A PROPRIA RAIZ QUADRADA
                else
                {
                    antes = lb_mostraConta.Text.Substring(0, index - 3);
                    depois = lb_mostraConta.Text.Substring(index - 3);
                }

                lb_mostraConta.Text = antes + "1/(" + depois + ")";
            }
            else if (char.IsNumber(getUltimoCaracter()))
            {
                string antes, depois;

                int espaco = lb_mostraConta.Text.LastIndexOf(" ");

                antes = lb_mostraConta.Text.Substring(0, espaco);
                depois = lb_mostraConta.Text.Substring(espaco);

                lb_mostraConta.Text = antes + " 1/(" + depois + ")";
            }
            else
            {
                lb_mostraConta.Text += " 1/(" + double.Parse(lb_recebeNumero.Text) + ")";
            }

            double valor = 1 / double.Parse(lb_recebeNumero.Text);
            lb_recebeNumero.Text = valor.ToString();
            btn_igual.Focus();
        }
        #endregion

        /****FUNCOES AUXILIARES PARA OPERACOES****/
        #region funcoes_operacoes

        //EXECUTA OPERACAO, PASSADA POR PARAMETRO OU A ULTIMA OPERACAO DA LABEL lb_mostraConta
        private void executaOperacao(char operacao = 'd')
        {
            if (lb_mostraConta.Text.Length > 0 && operacao == 'd')
            {
                operacao = getUltimaOperacao();
            }

            switch (operacao)
            {
                //ADICAO
                case '+':
                    conta += double.Parse(lb_recebeNumero.Text);
                    break;

                //SUBTRACAO
                case '-':
                    conta -= double.Parse(lb_recebeNumero.Text);
                    break;

                //MULTIPLICACAO
                case '*':
                    conta *= double.Parse(lb_recebeNumero.Text);
                    break;

                //DIVISAO
                case '/':
                    conta /= double.Parse(lb_recebeNumero.Text);
                    break;

                //IGUAL
                case '=':
                    executaOperacao(ultimaOperacao);
                    lb_mostraConta.Text = string.Empty;
                    break;

                //PRIMEIRO NUMERO
                default:
                    conta = double.Parse(lb_recebeNumero.Text);
                    break;
            }

            if (mostrarConta(operacao))
            {
                lb_mostraConta.Text += double.Parse(lb_recebeNumero.Text).ToString();
                ajustaTamanhoMostraConta();
            }
                

            lb_recebeNumero.Text = conta.ToString();

            alteracao = false;
        }

        //VERICA QUAL FOI A ULTIMA OPERACOA SIMPLES SETADA NA LABEL lb_mostraConta
        private char getUltimaOperacao()
        {
            for (int i = 1; i < lb_mostraConta.Text.Length; i++)
            {
                switch (lb_mostraConta.Text[lb_mostraConta.Text.Length - i])
                {
                    case '+': return '+';
                    case '-': return '-';
                    case '*': return '*';
                    case '/':

                        //VERIFICACAO ESPECIAL POIS O / E USADO NO 1/X TAMBEM
                        if ((lb_mostraConta.Text[lb_mostraConta.Text.Length - i - 1] == '1'))
                            continue;

                        return '/';
                    default: continue;
                }
            }
            return 'q';
        }

        //VERIFICA O ULTIMO CARACATER
        private char getUltimoCaracter()
        {
            try
            {
                return lb_mostraConta.Text[lb_mostraConta.Text.Length - 1];
            }
            catch
            {
                return 'q';
            }
        }

        private int getIndexUltimoParenteses()
        {
            int indexUP;

            if (getUltimaOperacao() != 'q')
            {
                indexUP = lb_mostraConta.Text.IndexOf(getUltimaOperacao());
            }
            else
            {
                indexUP = 0;
            }

            return indexUP + lb_mostraConta.Text.Substring(indexUP).IndexOf('(') - 1;
        }

        //VERIFICA QUANDO DEVE-SE MOSTRAR O NUMERO NA LABEL lb_mostraConta
        private bool mostrarConta(char operacao)
        {
            if (lb_mostraConta.Text.Length > 0)
            {
                if (getUltimoCaracter() != ')' && !char.IsNumber(getUltimoCaracter()))
                    return true;

                return false;
            }

            if (operacao == '=')
                return false;

            return true;
        }
        #endregion

        /****FUNCOES DE ESCRITA****/
        #region funcoes_escrita

        private void escreveNumero(string num)
        {
            if (alteracao)
            {
                if (lb_recebeNumero.Text.Length < 12)
                    lb_recebeNumero.Text += num;
            }
            else
            {
                lb_recebeNumero.Text = num;
            }
            btn_igual.Focus();
        }

        private void escreveOperacoesBasicas(string operacao)
        {
            if (alteracao || lb_mostraConta.Text.Length == 0)
            {
                executaOperacao();
            }
            else
            {
                if (getUltimoCaracter() != ')')
                    lb_mostraConta.Text = lb_mostraConta.Text.Substring(0, lb_mostraConta.Text.Length - 3);
            }
            lb_mostraConta.Text += " " + operacao + " ";
            ajustaTamanhoMostraConta();
            btn_igual.Focus();
        }

        private void ajustaTamanhoMostraConta()
        {
            if (lb_mostraConta.Text.Length > 28)
            {
                for (int i = 0; i < lb_mostraConta.Text.Length - 28; i++)
                {
                    lb_mostraConta.Text = lb_mostraConta.Text.Substring(1);
                }
            }
        }
        #endregion

        /****BOTOES DE CONTROLE****/
        #region botoes_controle

        //CE
        private void btn_ce_Click(object sender, EventArgs e)
        {
            lb_recebeNumero.Text = "0";
            btn_igual.Focus();
        }

        //C
        private void btn_c_Click(object sender, EventArgs e)
        {
            conta = 0;
            alteracao = false;
            ultimaOperacao = 'd';
            lb_mostraConta.Text = string.Empty;
            lb_recebeNumero.Text = "0";
            btn_igual.Focus();
        }

        //← BACKSPACE
        private void btn_backspace_Click(object sender, EventArgs e)
        {
            if (lb_recebeNumero.Text.Length > 0)
                lb_recebeNumero.Text = lb_recebeNumero.Text.Remove(lb_recebeNumero.Text.Length - 1);

            if (lb_recebeNumero.Text.Length == 0 || lb_recebeNumero.Text == "-")
                lb_recebeNumero.Text = "0";

            btn_igual.Focus();
        }
        #endregion

        /****BOTES DE MEMORIA*****/
        #region botoes_memoria

        private void btn_ms_Click(object sender, EventArgs e)
        {
            memoria = double.Parse(lb_recebeNumero.Text);
            btn_mc.Enabled = true;
            btn_mr.Enabled = true;
            btn_igual.Focus();
        }

        private void btn_m_mais_Click(object sender, EventArgs e)
        {
            memoria += double.Parse(lb_recebeNumero.Text);
            btn_mc.Enabled = true;
            btn_mr.Enabled = true;
            btn_igual.Focus();
        }

        private void btn_m_menos_Click(object sender, EventArgs e)
        {
            memoria -= double.Parse(lb_recebeNumero.Text);
            btn_mc.Enabled = true;
            btn_mr.Enabled = true;
            btn_igual.Focus();
        }

        private void btn_mr_Click(object sender, EventArgs e)
        {
            lb_recebeNumero.Text = memoria.ToString();
            btn_igual.Focus();
        }

        private void btn_mc_Click(object sender, EventArgs e)
        {
            memoria = 0;
            btn_mc.Enabled = false;
            btn_mr.Enabled = false;
            btn_igual.Focus();
        }

        #endregion
        
        /****BOTES DO MENU*****/
        #region botoes_menu
        
        //COPIAR
        private void tsmi_copiar_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lb_recebeNumero.Text);
        }

        //COLAR
        private void tsmi_colar_Click(object sender, EventArgs e)
        {
            string cv = Clipboard.GetText();
            lb_recebeNumero.Text = String.Empty;

            for (int i = 0; i < cv.Length; i++)
            {
                if (char.IsNumber(cv[i]) && lb_recebeNumero.Text.Length < 12)
                    lb_recebeNumero.Text += cv[i];
            }
        }

        //EXIBIR AJUDA
        private void tsmi_exibir_ajuda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Suave na nave truta, tenta de novo!");
        }

        //SOBRE
        private void tsmi_sobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gambiarras Calc 2.0\nDesenvolvida por Lorenzo Freitas e Tiago Berg\nDisponivel em: Link Github");
        }
        #endregion
    }
}
