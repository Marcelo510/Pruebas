using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using WebAppCore.Datos;

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebAppCore.Interfaces;


namespace WebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            


            await _emailSender
                .SendEmailAsync("malonso510@gmail.com", "Asunto", "Mensaje")
                .ConfigureAwait(false);

            return View();
        }


        //public IActionResult Index()
        //{

        //    var InvEmail = new Emails(_configuration);

        //    InvEmail.EnvioEmails();




        //    //var conn = _configuration.GetConnectionString("DefaultConnection");
        //    //using (SqlConnection con = new SqlConnection(conn))
        //    //{
        //    //    con.Open();
        //    //    using (SqlCommand command = new SqlCommand("ObtenerUsuarios", con))
        //    //    using (SqlDataReader reader = command.ExecuteReader())
        //    //    {
        //    //        List<Usuaios> Usu = new List<Usuaios>();
        //    //        while (reader.Read())
        //    //        {
        //    //            var regis = new Usuaios();
        //    //            regis.IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
        //    //            regis.Nombres = reader.GetString(reader.GetOrdinal("Nombres"));

        //    //            Usu.Add(regis);
        //    //        }

        //    //        var wre = from Usu1 in Usu
        //    //                  where Usu1.IdUsuario == 2
        //    //                  select Usu1;

        //    //        //var wre2 = Usu.Where(a => a.IdUsuario > 2).OrderBy(;
        //    //    }
        //    //}

        //    return View();  
        //}

        

        public IActionResult JavaScript()
        {
            return View();
        }

        public IActionResult JavaScript2()
        {
            return View();
        }


        public async Task<IActionResult> Clima()
        {
            //string[] datosArrayFinal = new string[100];
            var datosArrayFinal = await getHtmlClima();
            //Console.ReadLine();
            return View(datosArrayFinal);
        }

        public async Task<IActionResult> PrivacyAsync()
        {
            //string[] datosArrayFinal = new string[100];
            var datosArrayFinal = await getHtml();
            //Console.ReadLine();
            return View(datosArrayFinal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<Array> getHtml()
        {
            var url1 = "https://news.google.com/topstories?hl=es-419&pli=1&gl=AR&ceid=AR:es-419";

            var httpclient = new HttpClient();
            string[] datosArray = new string[20];



            var html = await httpclient.GetStringAsync(url1);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.ChildNodes;

            //var p = htmlDocument.QuerySelector("");

            //var nodos = nodes.ToArray();

            //string datos1;
            //datos1 = nodos[1].InnerText;
            //var datosHTML = nodos[1].InnerHtml;

            //var sigue = true;
            //string str1 = "";
            //int fin = 5;
            //string str3 = "";
            //int last = 0;
            //int i = 0;
            //do
            //{
            //    last = datos1.IndexOf("bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up") + "bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up".Length;

            //    str1 = datos1.Substring(last, datos1.Length - last);
            //    fin = str1.IndexOf("ampvideo");

            //    if (fin < 1 || i == 20)
            //    {
            //        return datosArray;
            //    }

            //    str3 = str1.Substring(0, fin - 1);
            //    //str3 = str3.Replace("|", "--");
            //    datosArray[i] = str3;
            //    i++;
            //    datos1 = str1;
            //} while (sigue);
            return datosArray;
        }

        private async Task<Array> getHtml2()
        {
            var url1 = "https://news.google.com/topstories?hl=es-419&pli=1&gl=AR&ceid=AR:es-419";

            var httpclient = new HttpClient();
            string[] datosArray = new string[20];



            var html = await httpclient.GetStringAsync(url1);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.ChildNodes;

            var nodos = nodes.ToArray();

            string datos1;
            datos1 = nodos[1].InnerText;
            var datosHTML = nodos[1].InnerHtml;

            var sigue = true;
            string str1 = "";
            int fin = 5;
            string str3 = "";
            int last = 0;
            int i = 0;
            do
            {
                last = datos1.IndexOf("bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up") + "bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up".Length;

                str1 = datos1.Substring(last, datos1.Length - last);
                fin = str1.IndexOf("ampvideo");

                if (fin < 1 || i == 20)
                {
                    return datosArray;
                }

                str3 = str1.Substring(0, fin - 1);
                //str3 = str3.Replace("|", "--");
                datosArray[i] = str3;
                i++;
                datos1 = str1;
            } while (sigue);
            return datosArray;
        }


        private async Task<Array> getHtmlClima()
        {
            var url1 = "https://weather.com/es-AR/tiempo/hoy/l/ARBA0009:1:AR";

            var httpclient = new HttpClient();
            string[] datosArray = new string[20];



            var html = await httpclient.GetStringAsync(url1);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.ChildNodes;

            var nodos = nodes.ToArray();

            string datos1;
            datos1 = nodos[1].InnerText;
            var datosHTML = nodos[1].InnerHtml;

            var sigue = true;
            string str1 = "";
            int fin = 5;
            string str3 = "";
            int last = 0;
            int i = 0;
            do
            {
                last = datos1.IndexOf("bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up") + "bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up".Length;

                str1 = datos1.Substring(last, datos1.Length - last);
                fin = str1.IndexOf("ampvideo");

                if (fin < 1 || i == 20)
                {
                    return datosArray;
                }

                str3 = str1.Substring(0, fin - 1);
                //str3 = str3.Replace("|", "--");
                datosArray[i] = str3;
                i++;
                datos1 = str1;
            } while (sigue);
            return datosArray;
        }
    }
}
