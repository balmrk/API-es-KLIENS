using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{ 
    public partial class MainWindow : Window
    {
        public List<Product> products;
        public Product product;
        private static readonly HttpClient client = new HttpClient(); //httpkliens post-hoz
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public void ParseJSON(String url,int a)
        {
            try
            {
                string json = new WebClient().DownloadString(url);
                switch (a)
                {
                    case 1:
                        products = JsonConvert.DeserializeObject<List<Product>>(json);
                        break;
                    case 2:
                        product = JsonConvert.DeserializeObject<Product>(json);
                        break;
                }
            }
            catch
            {
                Trace.WriteLine("ERROR 404");
            }
            
        }

        //GET ALL
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            String url = "https://localhost:7208/api/Products";
            ParseJSON(url,1);
            allProductsListBox.Items.Clear();
            foreach (Product termek in products) {
                allProductsListBox.Items.Add(termek.productId+ " "+termek.productName+" "+termek.unitPrice+"$");
            }
            GetAllButton.Background = Brushes.Beige;
            GetAllButton.Content = "GOT'EM";
        }

        //GET BY ID
        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {
            String uri = "https://localhost:7208/api/Products";
            uri+="/"+idBox.Text;
            ParseJSON(uri, 2);
            //HIBAKEZELÉS GAGYI
            try
            {
                prodnam.Content = "("+ product.productId.ToString()+") "+product.productName;
                prodpric.Content = "$"+product.unitPrice.ToString();
            }
            catch
            {
                prodnam.Content = "404 Error.";
            }
            GetButton.Background = Brushes.Beige;
            GetButton.Content = "GOTIT";
        }

        //POST OBJECT
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String uri = "https://localhost:7208/api/Products";
            Product prod = new Product
            {
                productId = Int32.Parse(postPID.Text),
                productName = postPN.Text,
                unitPrice = float.Parse(postUP.Text)
            };

            JObject o = (JObject)JToken.FromObject(prod);
            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json");
                var resp = await client.SendAsync(request);


                var szeroValasz = await resp.Content.ReadAsStringAsync();
                Trace.WriteLine(szeroValasz);
            }

            PostButton.Background = Brushes.Beige;
            PostButton.Content = "POSTED";
        }

        //PUT BY ID
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

            String uri = "https://localhost:7208/api/Products";
            uri += "/"+putPID.Text;

            PutProduct prod = new PutProduct
            {
                productName = putPN.Text,
                unitPrice = float.Parse(putUP.Text)
            };

            JObject o = (JObject)JToken.FromObject(prod);
            using (var request = new HttpRequestMessage(HttpMethod.Put, uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(prod), Encoding.UTF8, "application/json");
                var resp = await client.SendAsync(request);


                var szeroValasz = await resp.Content.ReadAsStringAsync();
                Trace.WriteLine(szeroValasz);
            }
            PutButton.Background = Brushes.Beige;
            PutButton.Content = "UPDATED";
        }

        //DELETE BY ID
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String uri = "https://localhost:7208/api/Products";
            uri += "/" + deletePID.Text;

            using (var request = new HttpRequestMessage(HttpMethod.Delete, uri))
            {
                var resp = await client.SendAsync(request);

                var szeroValasz = await resp.Content.ReadAsStringAsync();
                Trace.WriteLine(szeroValasz);
            }
            DeleteButton.Background = Brushes.Beige;
            DeleteButton.Content = "REMOVED";

        }
    }
}
