using System;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace GetAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            getHtml();
            Console.ReadLine();
        }

        private static async void getHtml()
        {
            var url1 = "https://news.google.com/topstories?hl=es-419&pli=1&gl=AR&ceid=AR:es-419";
            //var url1 = "https://weather.com/es-AR/tiempo/hoy/l/ARBA0009:1:AR";

            var httpclient = new HttpClient();

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

            do
            {
                last = datos1.IndexOf("bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up") + "bookmark_bordersharemore_vertVer cobertura completakeyboard_arrow_up".Length;
                

                str1 = datos1.Substring(last, datos1.Length - last);
                fin = str1.IndexOf("ampvideo");
                
                if (fin < 1)
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Llegó al final");
                    sigue = false;
                    return;
                }

                str3 = str1.Substring(0, fin - 1);

                Console.WriteLine("--------------------------------");
                Console.WriteLine("");
                Console.WriteLine("--------------------------------");
                Console.WriteLine(str3);
                datos1 = str1;
            } while (sigue);
        }
    }
}
