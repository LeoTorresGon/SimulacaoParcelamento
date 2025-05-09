/* Autores do Projeto: Luan Groppo Viana
*                      Leonardo Torres
*
* TextBox1 = Entrada do preço
* TextBox2 = Entrada da quantidade de parcelas
* TextBox3 = Saída multilinha dos resultados
* Button1 = Enviar o preço e a quantidade de parcelas, efetuar o calculo das parcelas e escrever o resultado na TextBox3
* Button2 = Efetuar o pagamento da proxima parcela
* Label1 = Mostrar (caso exista) a parcela em atraso com juros
*******************************************************************/
namespace projetoPVB_3bim
{
    public partial class Form1 : Form
    {
        DateTime data = DateTime.Parse("01/01/1793");
        DateTime data2 = DateTime.Parse("01/01/1772");
        float preço = 0;
        int qntdParcelas = 0;
        double preçoParcela = 0;
        double[] valorParcelas = new double[2];
        DateTime[] dataParcelas = new DateTime[2];
        public Form1()
        {
            InitializeComponent();
            textBox3.Text = "";
            label1.Text = "";
        }

        public void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            label1.Text = "";
            for (int i = 0; i < valorParcelas.Length - 1; i++)
            {
                valorParcelas[i] = valorParcelas[i + 1];
                dataParcelas[i] = dataParcelas[i + 1];
            }
            qntdParcelas -= 1;
            for (int i = 0; i < qntdParcelas; i++)
            {
                string linha = String.Format("{0}ªParcela: {1:C2} Vencimento: {2}\r\n", i + 1, valorParcelas[i], dataParcelas[i].ToString("dd/MM/yyyy"));
                textBox3.Text += linha;
            }
            for (int c = 0; c < qntdParcelas; c++)
            {
                if (valorParcelas[c] > preçoParcela)
                {
                    label1.Text = String.Format("{0}ªParcela atrasada. Novo valor: {1:C2}", 1, (valorParcelas[0]));
                }
                else
                {
                    label1.Text = "Nenhuma parcela atrasada.";
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            label1.Text = "Nenhuma parcela atrasada.";
            button3.Visible = true;
            string linha = "";
            data = DateTime.Parse(textBox4.Text);
            preço = float.Parse(textBox1.Text);
            qntdParcelas = int.Parse(textBox2.Text);
            dataParcelas = new DateTime[qntdParcelas];
            for (int i = 0; i < qntdParcelas; i++)
            {
                if (data.DayOfWeek == DayOfWeek.Saturday)
                {
                    dataParcelas[i] = data.AddDays(+2);
                }
                else if (data.DayOfWeek == DayOfWeek.Sunday)
                {
                    dataParcelas[i] = data.AddDays(+1);
                }
                else
                {
                    dataParcelas[i] = data;
                }
                data = data.AddMonths(1);
            }
            preçoParcela = preço / qntdParcelas;
            valorParcelas = new double[qntdParcelas];
            valorParcelas[0] = preçoParcela;
            for(int i = 0; i < qntdParcelas; i++)
            {
                valorParcelas[i] = valorParcelas[0];
            }
            for(int i = 0; i < qntdParcelas; i++)
            {
                linha = String.Format("{0}ªParcela: {1:C2} Vencimento: {2}\r\n", i+1, valorParcelas[i], dataParcelas[i].ToString("dd/MM/yyyy"));
                textBox3.Text += linha;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            label1.Text = "";
            DateTime data2 = DateTime.Parse(textBox4.Text);
 
            for (int i = 0; i < qntdParcelas; i++)
            {
                int limite = data2.Month - dataParcelas[i].Month;
                if (data2.Year > dataParcelas[i].Year)
                {
                    for (int c = 1; c <= data2.Year-dataParcelas[i].Year; c++)
                    {
                        limite += 12;
                    }
                }
                else if((data2.Month == dataParcelas[i].Month) && (data2.Day > dataParcelas[i].Day))
                {
                    limite += 1;
                }
                if (data2 > dataParcelas[i])
                {
                    for (int c = 1; c <= limite; c++)
                    {
                        valorParcelas[i] *= 1.03;
                        dataParcelas[i] = dataParcelas[i].AddMonths(1);
                    }
                    if (dataParcelas[i].DayOfWeek == DayOfWeek.Saturday)
                    {
                        dataParcelas[i] = dataParcelas[i].AddDays(+2);
                    }
                    if (dataParcelas[i].DayOfWeek == DayOfWeek.Sunday)
                    {
                        dataParcelas[i] = dataParcelas[i].AddDays(+1);
                    }

                    label1.Text = String.Format("{0}ªParcela atrasada. Novo valor: {1:C2}", 1, (valorParcelas[0]));
                } else if (i == 0)
                {
                    MessageBox.Show("Nenhuma parcela alterada");
                    label1.Text = "Nenhuma parcela atrasada.";
                }
               
                string linha = String.Format("{0}ªParcela: {1:C2} Vencimento: {2}\r\n", i + 1, valorParcelas[i], dataParcelas[i].ToString("dd/MM/yyyy"));
                textBox3.Text += linha;
            }
        }
    }
}