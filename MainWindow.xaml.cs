using System;
using System.Collections.Generic;
using System.Linq;
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

namespace _23CV_Diseño_Calculadora_WPF_14_JUN_23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {

            try
            {
                Button button = (Button)sender;

                string value = (string)button.Content; // Valor

                if (IsNumber(value))
                {
                    HandleNumbers(value);
                }
                else if (IsOperator(value))
                {
                    HandleOperator(value);
                }
                else if (value == "CE")
                {
                    Screen.Clear();
                }
                else if (value == "=")
                {
                    HandleEquals(Screen.Text);
                }
                else if (value == "C")
                {
                    Clear();
                }
                else if (value == ".")
                {
                    Screen.Text = Screen.Text + ".";
                }

                
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error. " + ex.Message);
            }

        }

        //Metodos auxiliares.
        private bool IsNumber(string num) // Funcion para validar si es un numero o no
        {
            return double.TryParse(num, out _);

            // if (double.TryParse(num, out _))
            // {
            // return true;
            // }
            // return false;
        }

        private void HandleNumbers(string value) // Funcion de cabecera de numeros.
        {
            if (String.IsNullOrEmpty(Screen.Text) && !ContainsOtherOperators(Screen.Text))
            {
                Screen.Text = value;
            }
            else
            {
                Screen.Text += value;
            }


        }

        private bool IsOperator(string possibleOperator) //Funcion para validar un operador
        {

            // if(possibleOperator == "+" || possibleOperator == "-" || possibleOperator == "*" || possibleOperator == "/")
            // {
            // return true;
            // }
            // return false;

            return possibleOperator == "+" || possibleOperator == "-" || possibleOperator == "*" || possibleOperator == "/";
        }

        private void HandleOperator(string value)
        {
            if (!String.IsNullOrEmpty(Screen.Text))
            {
                Screen.Text += value;
            }
        }


        // Validacion de No agregar mas de dos veces el operador.
        
        private bool ContainsOtherOperators(string screenContent)
        {
            return screenContent.Contains("+") || screenContent.Contains("-") || screenContent.Contains("*") || screenContent.Contains("/");
        }

        private void HandleEquals(string screenContent)
        {
            string op = FindOperator(screenContent);
            if(!string.IsNullOrEmpty(op))
            {
                switch (op)
                {
                    case "+":
                        Screen.Text = Sum();
                        break;

                    case "-":
                        Screen.Text = Sub();
                        break;

                    case "*":
                        Screen.Text = Multi();
                        break;

                    case "/":
                        Screen.Text = Divide();
                        break;

                    default:
                        break;
                }

            }


        }
     
        private string FindOperator(string screenContent)
        {
            foreach(var c in screenContent)
            {
                if (IsOperator(c.ToString()))
                {
                    return c.ToString();
                }
                
            }
            return " ";
        }

        //Funciones de operador (Suma, resta...)

        private string Sum() //SUMA
        {
            string[] number = Screen.Text.Split('+');
            double.TryParse(number[0], out double n1); 
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 + n2, 12).ToString();
        }

        private string Sub() //RESTA (Subtraction)
        {
            string[] number = Screen.Text.Split('-');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 - n2, 12).ToString();
        }

        private string Multi() // Multiplicacion 
        {
            string[] number = Screen.Text.Split('*');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 * n2, 12).ToString();
        }

        private string Divide() // Division
        {
            string[] number = Screen.Text.Split('/');
            double.TryParse(number[0], out double n1);
            double.TryParse(number[1], out double n2);

            return Math.Round(n1 / n2, 12).ToString();
        }


        //Funcion para el boton de borrar uno por uno / el boton C

        private void Clear()
        {
            if (Screen.Text.Length == 1)
                Screen.Text = "";
            else
                Screen.Text = Screen.Text.Substring(0, Screen.Text.Length - 1);
        }

        

    }
}
