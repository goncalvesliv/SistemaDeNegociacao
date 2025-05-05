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

                dgvOrdens.AutoGenerateColumns = false;

                dgvOrdens.Columns.Clear();

                AdicionarColunasAoDataGridView();

                dgvOrdens.DataSource = ordensProcessadas;
            }
            else
            {
                MessageBox.Show("Erro ao carregar ordens processadas.");
            }
        }

        private void AdicionarColunasAoDataGridView()
        {
            var colunas = new (string Header, string Property)[]
            {
            ("Tipo Ordem", "TipoOrdem"),
            ("Nome Ativo", "NomeAtivo"),
            ("Preço", "Preco"),
            ("Quantidade", "Quantidade"),
            ("Status", "Status")
            };

            foreach (var coluna in colunas)
            {
                dgvOrdens.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = coluna.Header,
                    DataPropertyName = coluna.Property
                });
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
                MessageBox.Show("Erro ao carregar neg?cios realizados.");
            }
        }


        private async void btnUploadCsv_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv";
            openFileDialog1.Title = "Selecione o arquivo de ordens";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;

                using var client = new HttpClient();
                using var content = new MultipartFormDataContent();
                using var fileStream = File.OpenRead(filePath);
                using var fileContent = new StreamContent(fileStream);

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
                content.Add(fileContent, "file", Path.GetFileName(filePath));

                var response = await client.PostAsync("https://localhost:7083/api/Ordem/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Ordens enviadas com sucesso!");
                    CarregarOrdensProcessadas();
                    CarregarNegociosRealizados();
                }
                else
                {
                    var erroDetalhado = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao enviar as ordens:\n{erroDetalhado}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
