using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpresorLCD
{
    class Program
    {
        //lineaEscritura: linea que guarda los caracteres de cada writeline
        private string lineaEscritura = string.Empty;

        //Variables constantes con las que se imprime los numeros tipo lcd        
        static string CARACTER_HORIZONTAL = "-";
        static string CARACTER_VERTICAL = "|";

        //Variables lógicas:
        //    espaciado   : guarda el espacio correspondiente a cada numero;
        //    numero      : numero que se desea imprimir
        //    size        : tamaño de impresión
        //    captura     : variable que recibe los datos
        //    separacion  : variable que separa en digitos el numero capturado         
        private string espaciado = string.Empty;
        private string numero;
        private int size;
        private string captura;
        private string[] separacion;
        private bool continuar = true;
        private bool verificar = true;


        static void Main(string[] args)
        {
            Program imp = new Program();

            //Ciclo que controla el registro de datos y la salida
            while (imp.continuar == true)
            {
                Console.WriteLine(Environment.NewLine + "Ingrese escala de impresión de 1 a 10 y número a imprimir separados por ',':");
                imp.captura = Console.ReadLine();

                imp.verificar = true;
                //Ciclo que controla la vericidad de los datos
                while (imp.verificar == true)
                {
                    try
                    {
                        imp.separacion = imp.captura.Split(',');
                        imp.size = Convert.ToInt32(imp.separacion[0]);
                        imp.numero = imp.separacion[1];
                        int comprobar = Convert.ToInt32(imp.numero);
                        if ((imp.size <= 0 || imp.size > 10) && comprobar > 0)
                        {

                            Console.WriteLine(Environment.NewLine + "Verifique los datos ingresados e ingreselos nuevamente:");
                            imp.captura = Console.ReadLine();
                            imp.verificar = true;
                        }
                        
                        else if (imp.size == 0 && comprobar == 0)
                        {
                            imp.continuar = false;
                            imp.verificar = false;
                        }
                        else
                        {
                            imp.verificar = false;
                        }
                    }
                    catch (Exception)
                    {

                        Console.WriteLine(Environment.NewLine + "Verifique los datos ingresados e ingreselos nuevamente:");
                        imp.captura = Console.ReadLine();
                        imp.verificar = true;
                    }
                }

                imp.Imprimir(imp.numero, imp.size);
                imp.numero = string.Empty;
            }
            Environment.Exit(0);



        }


        //Procedimiento que transforma los datos y los imprime dando el aspecto LCD
        private void Imprimir(string numero, int size)
        {
            for (int i = 0; i < (size * 2) + 3; i++)
            {
                foreach (var num in numero)
                {
                    if (i == 0)
                    {
                        switch (num)
                        {
                            case '1':
                            case '4':
                                lineaEscritura = lineaEscritura + HorizontalEspacios(size);
                                break;

                            default:
                                lineaEscritura = lineaEscritura + HorizontalGuion(size);
                                break;
                        }
                    }
                    else if ((size == 1 && i == 1)
                                || (i > 0 && i < (size + 1)))
                    {
                        switch (num)
                        {
                            case '0':
                            case '4':
                            case '8':
                            case '9':
                                lineaEscritura = lineaEscritura + VerticalDual(size);
                                break;

                            case '5':
                            case '6':
                                lineaEscritura = lineaEscritura + VerticalIzquierdo(size);
                                break;

                            default:
                                lineaEscritura = lineaEscritura + VerticalDerecho(size);
                                break;
                        }
                    }
                    else if ((size == 1 && i == 2)
                                || (i == (size + 1)))
                    {
                        switch (num)
                        {
                            case '0':
                            case '1':
                            case '7':
                                lineaEscritura = lineaEscritura + HorizontalEspacios(size);
                                break;

                            default:
                                lineaEscritura = lineaEscritura + HorizontalGuion(size);
                                break;
                        }
                    }

                    else if ((size == 1 && i == 3)
                                || (i > (size + 1)
                                   && i < (size * 2 + 2)))
                    {
                        switch (num)
                        {
                            case '0':
                            case '6':
                            case '8':
                                lineaEscritura = lineaEscritura + VerticalDual(size);
                                break;

                            case '2':
                                lineaEscritura = lineaEscritura + VerticalIzquierdo(size);
                                break;

                            default:
                                lineaEscritura = lineaEscritura + VerticalDerecho(size);
                                break;
                        }
                    }
                    else
                    {
                        switch (num)
                        {
                            case '1':
                            case '4':
                            case '7':
                            case '9':
                                lineaEscritura = lineaEscritura + HorizontalEspacios(size);
                                break;
                            default:
                                lineaEscritura = lineaEscritura + HorizontalGuion(size);
                                break;
                        }
                    }

                    lineaEscritura = lineaEscritura + " ";


                }

                Console.WriteLine(lineaEscritura);
                lineaEscritura = string.Empty;

            }
        }

        //Funcion que especifica los espacios que se le asignan a una linea 
        private string HorizontalEspacios(int tamaño)
        {
            return " " + Espaciado(tamaño) + " ";
        }

        //Funcion que especifica los caracteres horizontales que se le asignan a una linea
        private string HorizontalGuion(int tamaño)
        {
            string guion = string.Empty;
            for (int i = 0; i < tamaño; i++)
            {
                guion = guion + CARACTER_HORIZONTAL;
            }
            return " " + guion + " ";
        }

        //Funcion que ubica los caracteres verticales cuando se necesitan dos
        private string VerticalDual(int tamaño)
        {
            return CARACTER_VERTICAL + Espaciado(tamaño) + CARACTER_VERTICAL;
        }

        //Funcion que ubica el caracter vertical cuando este se necesita en la derecha del numero
        private string VerticalDerecho(int tamaño)
        {
            return Espaciado(tamaño + 1) + CARACTER_VERTICAL;
        }

        //Funcion que ubica el caracter vertical cuando este se necesita en la izquierda del numero
        private string VerticalIzquierdo(int tamaño)
        {
            return CARACTER_VERTICAL + Espaciado(tamaño + 1);
        }

        //Funcion que devuelve los espacios necesarios para ubicar los caracteres en la posicion correcta
        private string Espaciado(int tamaño)
        {
            espaciado = string.Empty;

            for (int i = 0; i < tamaño; i++)
            {
                espaciado = espaciado + " ";
            }
            return espaciado;
        }


    }
}
