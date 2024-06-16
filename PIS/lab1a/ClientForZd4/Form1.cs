using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForZd4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            result.Text = "";
            if (int.TryParse(TextX.Text, out int x) && int.TryParse(TextY.Text, out int y))
            {
                try
                {
                    int sum = await GetSum(x, y);
                    result.Text = $"X + Y = {sum}";
                }
                catch (Exception ex)
                {
                    result.Text = $"Ошибка: {ex.Message}";
                }
            }
            else
            {
                result.Text = "введите корректные числа.";
            }
        }

        private async Task<int> GetSum(int x, int y)
        {
            int sum;

            string url = $"https://localhost:7278/add?ParmX={x}&ParmY={y}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, null);
                response.EnsureSuccessStatusCode();

                string responseText = await response.Content.ReadAsStringAsync();
                sum = int.Parse(responseText);
            }

            return sum;
        }
    }
}
