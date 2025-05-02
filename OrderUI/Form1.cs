using OrderCommonModels.Models;
using System.Globalization;
using System.Net.Http.Json;

namespace OrderUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (dgvOrdens.Columns.Contains("DataCriacao"))
            {
                dgvOrdens.Columns["DataCriacao"].Visible = false;
            }

            CarregarOrdensProcessadas();
            CarregarNegociosRealizados();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void CarregarOrdensProcessadas()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7083/api/Ordem/ordens-processadas");

            if (response.IsSuccessStatusCode)
            {
                var ordensProcessadas = await response.Content.ReadFromJsonAsync<List<Ordem>>();

                dgvOrdens.AutoGenerateColumns = true;
                dgvOrdens.DataSource = ordensProcessadas;
            }
            else
            {
                MessageBox.Show("Erro ao carregar ordens processadas.");
            }
        }


        private async void CarregarNegociosRealizados()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7083/api/Ordem/negocios");

            if (response.IsSuccessStatusCode)
            {
                var negocios = await response.Content.ReadFromJsonAsync<List<Negocio>>();
                dgvNegocios.DataSource = negocios;
            }
            else
            {
                MessageBox.Show("Erro ao carregar negócios realizados.");
            }
        }


        private async void btnUploadCsv_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog1.Title = "Selecione o arquivo de ordens";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var caminhoArquivo = openFileDialog1.FileName;

                try
                {
                    var linhas = File.ReadAllLines(caminhoArquivo);
                    var ordens = new List<Ordem>();

                    foreach (var linha in linhas)
                    {
                        if (string.IsNullOrWhiteSpace(linha)) continue;

                        var partes = linha.Split(';');

                        if (partes.Length < 4) continue;

                        var ordem = new Ordem
                        {
                            TipoOrdem = partes[0],
                            NomeAtivo = partes[1],
                            Preco = decimal.Parse(partes[2].Replace(",", "."), CultureInfo.InvariantCulture),
                            Quantidade = int.Parse(partes[3])
                        };

                        ordens.Add(ordem);
                    }


                    var client = new HttpClient();
                    var response = await client.PostAsJsonAsync("https://localhost:7083/api/Ordem/upload", ordens);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Ordens enviadas com sucesso!");
                        CarregarOrdensProcessadas();
                        CarregarNegociosRealizados();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao enviar as ordens.");
                    }

                    dgvOrdens.DataSource = ordens;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao processar o CSV: " + ex.Message);
                }
            }
        }
    }
}
