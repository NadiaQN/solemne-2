using System;
using System.IO;

namespace Ejercicio2
{
    class Program
    {
        static void Main(string[] args)
        {
            string nombreRegion, nombreComuna, strCasosActivos, agregarRegion, agregarComuna, linea;
            int casosActivos = 0, totalCasosActivos = 0; 

            Console.Write("¿Desea agregar una región?: (si/no) ");
            agregarRegion = Console.ReadLine();

            try
            {
                // Crear archivo
                FileStream f = new FileStream("Totales.txt", FileMode.Create, FileAccess.Write);
                StreamWriter fw = new StreamWriter(f);

                while (agregarRegion == "si")
                {

                    Console.Write("Nombre de la Región: ");
                    nombreRegion = Console.ReadLine();

                    Console.Write("Nombre de la Comuna: ");
                    nombreComuna = Console.ReadLine();

                    Console.Write("Cantidad de casos activos: ");
                    strCasosActivos = Console.ReadLine();
                    int.TryParse(strCasosActivos, out casosActivos);

                    totalCasosActivos = totalCasosActivos + casosActivos;

                    Console.Write("¿Desea agregar otra comuna?: (si/no) ");
                    agregarComuna = Console.ReadLine();

                    while (agregarComuna == "si")
                    {
                        
                        Console.Write("Nombre de la Comuna: ");
                        nombreComuna = Console.ReadLine();

                        Console.Write("Cantidad de casos activos: ");
                        strCasosActivos = Console.ReadLine();
                        int.TryParse(strCasosActivos, out casosActivos);

                        totalCasosActivos = totalCasosActivos + casosActivos;

                        Console.Write("¿Desea agregar otra comuna?: (si/no) ");
                        agregarComuna = Console.ReadLine();
                    }

                    linea = nombreRegion + "," + totalCasosActivos;
                    fw.WriteLine(linea);

                    if(agregarComuna != "si")
                    {
                        Console.Write("¿Desea agregar una región?: (si/no) ");
                        agregarRegion = Console.ReadLine();
                        totalCasosActivos = 0;
                    }   
                }

                fw.Close();
                f.Close();
            }
            catch(IOException ex)
            {
                Console.WriteLine("Error al abrir el archivo: " + ex.Message);
            }
            
            if(agregarRegion != "si")
            {
                try
                {
                    // Mostrar resumen
                    FileStream f = new FileStream("Totales.txt", FileMode.Open, FileAccess.Read);
                    StreamReader fr = new StreamReader(f);

                    while (!fr.EndOfStream)
                    {
                        linea = fr.ReadLine();
                        string[] campos = linea.Split(',');

                        Console.WriteLine("La región " + campos[0] + " tiene " + campos[1] + " casos activos");
                    }

                    fr.Close();
                    f.Close();
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error al abrir el archivo: " + ex.Message);
                }
       
            }
        }
    }

}
