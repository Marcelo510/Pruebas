using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using HtmlAgilityPack;
using Nancy.Json;
using Newtonsoft.Json;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
//using System.Web.Helpers;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            //getHtml();
            //Console.ReadLine();
            getHtml("Jujuy");
            Console.ReadLine();
            getHtml("Salta");
            Console.ReadLine();
            getHtml("Formosa");
            Console.ReadLine();
            getHtml("Catamarca");
            Console.ReadLine();
            getHtml("Tucum%C3%A1n");
            Console.ReadLine();
            getHtml("Santiago%20del%20Estero");
            Console.ReadLine();
            getHtml("Chaco");
            Console.ReadLine();
            getHtml("Corrientes");
            Console.ReadLine();
            getHtml("Misiones"); 
            Console.ReadLine();
            getHtml("Santa%20Fe");
            Console.ReadLine();
            getHtml("San%20Juan");
            Console.ReadLine();
            getHtml("La%20Rioja"); 
            Console.ReadLine();
            getHtml("C%C3%B3rdoba");
            Console.ReadLine();
            getHtml("Entre%20R%C3%ADos");
            Console.ReadLine();
            getHtml("Buenos%20Aires");
            Console.ReadLine();
            getHtml("CABA");
            Console.ReadLine();
            getHtml("San%20Luis");
            Console.ReadLine();
            getHtml("Mendoza"); 
            Console.ReadLine();
            getHtml("La%20Pampa");
            Console.ReadLine();
            getHtml("Chubut");
            Console.ReadLine();
            getHtml("R%C3%ADo%20Negro");
            Console.ReadLine();
            getHtml("Santa%20Cruz");
            Console.ReadLine();
            getHtml("Tierra%20del%20Fuego");
            Console.ReadLine();

            Console.WriteLine("Listo");
        }


        private static async void getHtml(string provincia)
        {
            //var url1 = "https://preciosmaximos.argentina.gob.ar/api/products?pag=1&Provincia=CABA&regs=15000";
            var url1 = "https://preciosmaximos.argentina.gob.ar/api/products?pag=1&Provincia=";
            var url2 = url1 + provincia + "&regs=15000";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url2);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.ChildNodes;

            var nodos = nodes.ToArray();

            string datos1;
            datos1 = nodos[0].InnerText;

            
            
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //StreamReader sr = new StreamReader(datos);
            //string jsonString = sr.ReadToEnd();
            datos1 = datos1.Replace(@"Precio sugerido", "Precio_sugerido");
            var items = JsonConvert.DeserializeObject<dynamic>(datos1);
            
            var item7 = items.result; 
            var largo = items.result.Count;
            //var item9 = item7[1].Precio_sugerido;

            listaPer Produc = new listaPer();
            List<Person> laLista = new List<Person>();

            for (int i = 0; i < largo; i++)
            {
                Person indiv = new Person();

                indiv.Precio_sugerido = item7[i].Precio_sugerido;
                indiv.Producto = item7[i].Producto;
                indiv.Producto_s_tilde = item7[i].Producto_s_tilde;
                indiv.Provincia = item7[i].Provincia;
                indiv.Region = item7[i].Region;
                indiv.categoria = item7[i].categoria;
                indiv.id_producto = item7[i].id_producto;
                indiv.marca = item7[i].marca;
                indiv.subcategoria = item7[i].subcategoria;

                laLista.Add(indiv);
            }


            var workbook = new XLWorkbook();
            workbook.AddWorksheet("sheetName");
            var ws = workbook.Worksheet("sheetName");
            //Recorrer el objecto
            int row = 1;

            ws.Cell("A" + row.ToString()).Value = "Precio_sugerido";
            ws.Cell("B" + row.ToString()).Value = "Producto";
            ws.Cell("C" + row.ToString()).Value = "Producto_s_tilde";
            ws.Cell("D" + row.ToString()).Value = "Provincia";
            ws.Cell("E" + row.ToString()).Value = "Region";
            ws.Cell("F" + row.ToString()).Value = "categoria";
            ws.Cell("G" + row.ToString()).Value = "id_producto";
            ws.Cell("H" + row.ToString()).Value = "marca";
            ws.Cell("I" + row.ToString()).Value = "subcategoria";
            row++;

            foreach (var c in laLista)
            {
                //Escribrie en Excel en cada celda
                ws.Cell("A" + row.ToString()).Value = c.Precio_sugerido;
                ws.Cell("B" + row.ToString()).Value = c.Producto;
                ws.Cell("C" + row.ToString()).Value = c.Producto_s_tilde;
                ws.Cell("D" + row.ToString()).Value = c.Provincia;
                ws.Cell("E" + row.ToString()).Value = c.Region;
                ws.Cell("F" + row.ToString()).Value = c.categoria;
                ws.Cell("G" + row.ToString()).Value = c.id_producto;
                ws.Cell("H" + row.ToString()).Value = c.marca;
                ws.Cell("I" + row.ToString()).Value = c.subcategoria;
                row++;

            }
        //Guardar Excel 
        //Ruta = Nombre_Proyecto\bin\Debug
        //C:\\Users\\Marcelo\\source\\repos\\Core\\Pruebas\\Pruebas\\Excels
            workbook.SaveAs("C:\\Users\\Marcelo\\source\\repos\\Core\\Pruebas\\Pruebas\\Excels\\Productos_" + provincia + ".xlsx");
            Console.WriteLine("Listo provincia de " + provincia);
        }



        //https://preciosmaximos.argentina.gob.ar/#/provincia/Jujuy
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Salta
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Formosa
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Catamarca
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Tucum%C3%A1n
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Santiago%20del%20Estero
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Chaco
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Corrientes
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Misiones
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Santa%20Fe
        //https://preciosmaximos.argentina.gob.ar/#/provincia/San%20Juan
        //https://preciosmaximos.argentina.gob.ar/#/provincia/La%20Rioja
        //https://preciosmaximos.argentina.gob.ar/#/provincia/C%C3%B3rdoba
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Entre%20R%C3%ADos
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Buenos%20Aires
        //https://preciosmaximos.argentina.gob.ar/#/provincia/CABA
        //https://preciosmaximos.argentina.gob.ar/#/provincia/San%20Luis
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Mendoza
        //https://preciosmaximos.argentina.gob.ar/#/provincia/La%20Pampa
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Chubut
        //https://preciosmaximos.argentina.gob.ar/#/provincia/R%C3%ADo%20Negro
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Santa%20Cruz
        //https://preciosmaximos.argentina.gob.ar/#/provincia/Tierra%20del%20Fuego


    }
}
