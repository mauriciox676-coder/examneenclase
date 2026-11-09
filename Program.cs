using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
class Program
{

    // Validar precio y cantidad
    static bool ValidarDatos(double precio, int cantidad)
    {
        if (precio > 0 && cantidad > 0)
        {
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: El precio y la cantidad deben ser mayores a cero.");
            return false;
        }
    }
    // Imprimir matriz de datos
    static void ImprimirDatos(double[,] matriz, string[] codigos)
    {
        Console.WriteLine("\n========== MATRIZ DE VENTAS ==========");
        Console.WriteLine("CODIGO\t\tPRECIO\t\tCANTIDAD");
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            Console.WriteLine(
                codigos[i] + "\t$" +
                matriz[i, 0].ToString("N2") + "\t" +
                matriz[i, 1]
            );
        }
    }
    // Calcular montos netos
    static void CalcularMontos(double[,] datos, double[] montosNetos,
                               ref double totalAcumulado,
                               ref int contadorPremium)
    {
        totalAcumulado = 0;
        contadorPremium = 0;
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            double precio = datos[i, 0];
            int cantidad = (int)datos[i, 1];
            if (ValidarDatos(precio, cantidad))
            {
                montosNetos[i] = precio * cantidad;
                totalAcumulado += montosNetos[i];
                if (montosNetos[i] > 50000)
                {
                    contadorPremium++;
                }
            }
            else
            {
                montosNetos[i] = 0;
            }
        }
    }
    // Mostrar reporte de ventas
    static void MostrarReporte(string[] codigos, string[] modelos,
                               double[] montosNetos)
    {
        Console.WriteLine("\n========== REPORTE DE VENTAS ==========");
        Console.WriteLine("CODIGO\t\tMODELO\t\t\tMONTO NETO");
        for (int i = 0; i < codigos.Length; i++)
        {
            Console.WriteLine(
                codigos[i] + "\t" +
                modelos[i] + "\t\t$" +
                montosNetos[i].ToString("N2")
            );
        }
    }
    // Ordenamiento burbuja de mayor a menor
    static void OrdenarBurbuja(double[,] datos, string[] codigos,
                               string[] modelos, double[] montosNetos)
    {
        int n = montosNetos.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (montosNetos[j] < montosNetos[j + 1])
                {
                    // Intercambiar montos netos
                    double auxiliarMonto = montosNetos[j];
                    montosNetos[j] = montosNetos[j + 1];
                    montosNetos[j + 1] = auxiliarMonto;
                    // Intercambiar códigos
                    string auxiliarCodigo = codigos[j];
                    codigos[j] = codigos[j + 1];
                    codigos[j + 1] = auxiliarCodigo;
                    // Intercambiar modelos
                    string auxiliarModelo = modelos[j];
                    modelos[j] = modelos[j + 1];
                    modelos[j + 1] = auxiliarModelo;
                    // Intercambiar filas de la matriz
                    for (int k = 0; k < datos.GetLength(1); k++)
                    {
                        double auxiliar = datos[j, k];
                        datos[j, k] = datos[j + 1, k];
                        datos[j + 1, k] = auxiliar;
                    }
                }
            }
        }
    }
    // Búsqueda lineal por código
    static void BuscarCodigo(string[] codigos, string[] modelos,
                             double[] montos, string codigoBuscado)
    {
        bool encontrado = false;
        if (string.IsNullOrWhiteSpace(codigoBuscado))
        {
            Console.WriteLine("ERROR: El código ingresado está vacío.");
            return;
        }
        for (int i = 0; i < codigos.Length; i++)
        {
            if (codigos[i].Equals(codigoBuscado.Trim(),
                                  StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\n========== VEHICULO ENCONTRADO ==========");
                Console.WriteLine("Código: " + codigos[i]);
                Console.WriteLine("Modelo: " + modelos[i]);
                Console.WriteLine("Monto Neto: $" + montos[i].ToString("N2"));
                encontrado = true;
                break;
            }
        }
        if (!encontrado)
        {
            Console.WriteLine("ERROR: El código no se encuentra registrado.");
        }
    }
    static void Main(string[] args)
    {
        string[] codigos = {

            "AUTO-201",

            "AUTO-202",

            "AUTO-203",

            "AUTO-204",

            "AUTO-205",

            "AUTO-206"
        };
        string[] modelos = {
            "SUV Interceptor",

            "Sedan Executive",

            "PickUp Titan",

            "City Hatchback",

            "Electric EV",

            "Van Carga"
        };
        // Matriz financiera: precio y cantidad
        double[,] datos = {
            { 28000.0, 2 },
            { 18500.0, 4 },
            { 35000.0, 1 },
            { 14000.0, 5 },
            { 42000.0, 1 },
            { 22500.0, 3 }
        };
        // Arreglo de montos netos
        double[] montosNetos = new double[6];
        double totalAcumulado = 0;
        int contadorPremium = 0;
        // Validar datos iniciales
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            if (!ValidarDatos(datos[i, 0], (int)datos[i, 1]))
            {
                return;
            }
        }
        // Calcular ventas
        CalcularMontos(datos, montosNetos,
        ref totalAcumulado, ref contadorPremium);
        // Mostrar matriz original
        ImprimirDatos(datos, codigos);
        // Mostrar reporte original
        MostrarReporte(codigos, modelos, montosNetos);
        // Mostrar indicadores financieros
        Console.WriteLine("\n========== RESUMEN FINANCIERO ==========");
        Console.WriteLine("Gran Total Acumulado: $" + totalAcumulado.ToString("N2"));
        Console.WriteLine("Ventas Premium (> $50,000): " + contadorPremium + " transacciones");
        // Ordenar ventas
        OrdenarBurbuja(datos, codigos, modelos, montosNetos);
        Console.WriteLine("\n========== VENTAS ORDENADAS ==========");
        MostrarReporte(codigos, modelos, montosNetos);
        // Buscar vehículo
        Console.WriteLine("\n========== BUSQUEDA DE VEHICULO ==========");
        Console.Write("Ingrese el código del vehículo: ");
        string codigoBuscado = Console.ReadLine();
        BuscarCodigo(codigos, modelos, montosNetos, codigoBuscado);
        Console.WriteLine("\nPresione una tecla para finalizar...");
        Console.ReadKey();
    }
}
