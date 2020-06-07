using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.ComponentModel.Design;

namespace LinqTapPucMinas
{
    class Program
    {
        #region Dados geração caros
        private static readonly string[] modelos = new string[]
        {
            "Pálio", "Uno", "Mobi", "Gol", "Up!", "Ka", "Fiesta", "Focus", "Celta", "Onix", "Colbat"
        };

        private static readonly int[] anos = new int[] { 2010, 2012, 2014, 2016, 2018, 2020 };

        private static readonly string[] cores = new string[] { "Preto", "Branco", "Vermelhor", "Verde", "Azul" };
        #endregion

        private class Carro
        {
            public string Placa { get; set; }
            public string Modelo { get; set; }
            public string Cor { get; set; }
            public int Ano { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("\t\tExercícios LINQ");
            Console.WriteLine("------------------------------------------------");

            List<Carro> carros = RetornarCarros(quantidade: 100);

            QuestaoA(carros);
            QuestaoB(carros);
            QuestaoC(carros);
            QuestaoD(carros);
            QuestaoE(carros);


            Console.ReadLine();
        }

        private static void QuestaoA(List<Carro> carros)
        {
            Console.WriteLine("\r\n\r\n(a) Exiba todas as informações da lista de carros");

            IEnumerable<string> informacoesCarro = from c in carros select $"{c.Placa} / {c.Modelo} / {c.Cor} / {c.Ano}";

            foreach (var informacao in informacoesCarro)
                Console.WriteLine(informacao);
        }

        private static void QuestaoB(List<Carro> carros)
        {
            Console.WriteLine("\r\n\r\n(b) Exiba a quantidade de carros de um determinado modelo informado pelo usuário");
            var quantidadesPorModelo = from c in carros
                                       group c by new { c.Modelo } into carrosAgrupados
                                       select new { carrosAgrupados.Key.Modelo, Quantidade = carrosAgrupados.Count() };

            foreach (var quantidadeModelo in quantidadesPorModelo)
                Console.WriteLine($"{quantidadeModelo.Modelo} => {quantidadeModelo.Quantidade}");
        }

        private static void QuestaoC(List<Carro> carros)
        {
            Console.WriteLine("\r\n\r\n(c) Exiba todas as informações dos carros que terminam a placa com o número 1 e possuem a cor preto.");
            var informacoesCarro = from c in carros
                                   where c.Placa.EndsWith("1") && c.Cor.ToUpper() == "PRETO"
                                   select $"{c.Placa} / {c.Modelo} / {c.Cor} / {c.Ano}";

            foreach (var informacao in informacoesCarro)
                Console.WriteLine(informacao);
        }

        private static void QuestaoD(List<Carro> carros)
        {
            Console.WriteLine("\r\n\r\n(d) Exiba a quantidade de cada modelo, ordenando por modelo");

            var quantidadesPorModelo = from c in carros
                                       group c by new { c.Modelo } into carrosAgrupados
                                       orderby carrosAgrupados.Key.Modelo
                                       select new { carrosAgrupados.Key.Modelo, Quantidade = carrosAgrupados.Count() };

            foreach (var quantidadeModelo in quantidadesPorModelo)
                Console.WriteLine($"{quantidadeModelo.Modelo} => {quantidadeModelo.Quantidade}");
        }

        private static void QuestaoE(List<Carro> carros)
        {
            Console.WriteLine("\r\n\r\n(e) Exiba todas as informações dos carros que são do modelo que mais aparece na lista de carros");

            var informacoesCarro = from carro in carros
                                   where carro.Modelo == (
                                                            from carroaux in carros
                                                            group carroaux by new { carroaux.Modelo } into carrosAgrupados
                                                            orderby carrosAgrupados.Count()
                                                            select carrosAgrupados.Key.Modelo
                                                         ).Last()
                                   select $"{carro.Placa} / {carro.Modelo} / {carro.Cor} / {carro.Ano}";

            foreach (var informacao in informacoesCarro)
                Console.WriteLine(informacao);
        }

        private static List<Carro> RetornarCarros(int quantidade)
        {
            var random = new Random();
            var carros = new List<Carro>();

            for (int i = 1; i <= quantidade; i++)
            {
                carros.Add(new Carro
                {
                    Placa = "ABCD" + i.ToString("0000"),
                    Modelo = modelos[random.Next(anos.Length)],
                    Cor = cores[random.Next(cores.Length)],
                    Ano = anos[random.Next(anos.Length)]
                });
            }

            return carros;
        }
    }
}
