using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraDeBinário
{
    public partial class Form1 : Form
    {
        public string NumberToOperator;
        public int binaryNum;
        public int decimalNum;
        public string HexaNum;

        public Form1()
        { InitializeComponent(); }

        public static string InverterString(string str)
        {

            int tamanho = str.Length; char[] caracteres = new char[tamanho];
            for (int i = 0; i < tamanho; i++)
            { caracteres[i] = str[tamanho - 1 - i]; } return new string(caracteres);
        }

        public static string BinarySub(string a, string b)
        {

            string n1 = stringToNormalize(a, b)[0]; string n2 = stringToNormalize(a, b)[1];
            n2 = BinarySum(BinaryNumberBubstitution(n2), "1"); string result = BinarySum(n1, n2);
            result = result.Remove(0, 1); return result;
        }

        public static string[] BinaryDiv(string a, string b)
        {

            string In = "";

            for (int i = 1; i < int.MaxValue; i++)
            {
                In = BinaryMult(b, decimalToBinary(i));
                if (BinaryIsBetter(In, a)) { return new string[2] { decimalToBinary(i - 1), BinarySub(a, BinarySub(In, b)) }; }
                else if (In == a) { return new string[2] { decimalToBinary(i), "0" }; }
            } return new string[2];
        }

        public static string decimalToBinary(double b)
        {

            double rest = b % 2; double result = (b - rest) / 2;
            if (b == 1) { return rest.ToString(); } else { return decimalToBinary(result) + rest.ToString(); }
        }

        public static int BinaryToDecimal(string valorBinario)
        {

            int exp = 0;
            int n;
            int sum = 0;
            string numeroInvertido = InverterString(valorBinario);

            for (int i = 0; i < numeroInvertido.Length; i++)
            { n = Convert.ToInt32(numeroInvertido.Substring(i, 1)); sum += n * (int)Math.Pow(2, exp); exp++; } return sum;
        }

        public static int BinaryToDecimal2(string valorBinario)
        { return Convert.ToInt32(valorBinario, 2); }

        public static bool BinaryIsBetter(string e1, string e2)
        {

            if (BinaryToDecimal(e1) > BinaryToDecimal(e2)) return true;
            else if (BinaryToDecimal(e2) > BinaryToDecimal(e1)) return false;
            else { return false; }

        }

        static string Stringreverse(string Word)
        {

            char[] arrChar = Word.ToCharArray();
            Array.Reverse(arrChar);
            string reverse = new String(arrChar);

            return reverse;
        }

        public static string[] stringToNormalize(string a, string b)
        {

            int numberWrong1 = a.Length; int numberWrong2 = b.Length;

            if (a.Length > b.Length)
                for (int i = 0; i < (numberWrong1 - numberWrong2); i++)
                { b = "0" + b; }

            else if (a.Length < b.Length)
                for (int i = 0; i < (numberWrong2 - numberWrong1); i++)
                { a = "0" + a; }

            string[] r = new string[] { a, b }; return r;
        }

        public static string BinarySum(string a, string b)
        {

            char[] h = (stringToNormalize(a, b))[0].ToCharArray();
            char[] s = (stringToNormalize(a, b))[1].ToCharArray();
            string overflow = "";
            string result = "";

            for (int i = h.Length - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(overflow))
                {
                    if (h[i].Equals('0') && s[i].Equals('0')) { result = '0' + result; }

                    else if (h[i].Equals('0') && s[i].Equals('1')) { result = '1' + result; }
                    else if (h[i].Equals('1') && s[i].Equals('0')) { result = '1' + result; }
                    else if (h[i].Equals('1') && s[i].Equals('1')) { overflow = "1"; result = "0" + result; }
                }

                else if ((h[i].Equals('1') && s[i].Equals('0')) || h[i].Equals('0') && s[i].Equals('1'))
                { overflow = "1"; result = "0" + result; }

                else if ((h[i].Equals('0') && s[i].Equals('0'))) { overflow = ""; result = "1" + result; }

                else if (h[i].Equals('1') && s[i].Equals('1')) { overflow = "1"; result = "1" + result; }
            }

            return overflow + result;
        }

        public static string BinaryMult(string a, string b)
        {

            string result = b;
            for (int i = 0; i < BinaryToDecimal(a) - 1; i++) { result = BinarySum(b, result); } return result;
        }

        public static string BinaryNumberBubstitution(string str)
        { string result = str; result = result.Replace('1', 'b'); result = result.Replace('0', '1'); result = result.Replace('b', '0'); return result; }

        private void Clear() { this.textBoxBinary = null; }

        private void bt1_Click(object sender, EventArgs e) { this.textBoxBinary.Text += bt1.Text; }

        private void bt0_Click(object sender, EventArgs e) { this.textBoxBinary.Text += bt0.Text; }

        private void btSum_Click(object sender, EventArgs e)
        {
        }

        private void btSub_Click(object sender, EventArgs e)
        { this.textBoxOperation.Text = "  -"; if (!textBoxBinary.Text.Equals("")) { NumberToOperator = textBoxDecimal.Text; } }

        private void btMult_Click(object sender, EventArgs e)
        { this.textBoxOperation.Text = "  x"; if (!textBoxBinary.Text.Equals("")) { NumberToOperator = textBoxDecimal.Text; } }

        private void btDiv_Click(object sender, EventArgs e)
        { this.textBoxOperation.Text = "  ÷"; if (!textBoxBinary.Text.Equals("")) { NumberToOperator = textBoxDecimal.Text; } }

        private static string HexaNumbers(string n)
        {

            switch (n)
            {

                case "10": return "A"; break;
                case "11": return "B"; break;
                case "12": return "C"; break;
                case "13": return "D"; break;
                case "14": return "E"; break;
                case "15": return "F"; break;
                default: return n;
            }
        }

        public static string DecimalToHexadecimal(double n)
        {
            double rest = n % 16; double result = (n - rest) / 16; if (n.Equals(0)) { return ""; }
            else { return DecimalToHexadecimal(result) + HexaNumbers(rest.ToString()); }
        }

        private void btConv_Click(object sender, EventArgs e)
        { textBoxDecimal.Text = BinaryToDecimal(textBoxBinary.Text).ToString(); textBoxHexadecimal.Text = DecimalToHexadecimal(Convert.ToDouble(textBoxDecimal.Text)); }

        private void btResult_Click(object sender, EventArgs e)
        {
            if (this.textBoxOperation.Text.Equals("  +")) { textBoxResult.Text = BinarySum(textBoxBinary.Text, textBoxBinary2.Text); }
            else if (this.textBoxOperation.Text.Equals("  x")) { textBoxResult.Text = BinaryMult(textBoxBinary.Text, textBoxBinary2.Text); }
            else if (this.textBoxOperation.Text.Equals("  ÷")) { string[] n = BinaryDiv(textBoxBinary.Text, textBoxBinary2.Text); textBoxResult.Text = n[0] + " Re:" + n[1]; }
            else if (this.textBoxOperation.Text.Equals("  -")) { textBoxResult.Text = BinarySub(textBoxBinary.Text, textBoxBinary2.Text); }
        }



        private void btClear_Click(object sender, EventArgs e)
        {
            textBoxBinary2.Text = null; textBoxDecimal.Text = null; textBoxBinary.Text = null;
            textBoxHexadecimal.Text = null; textBoxOperation.Text = null; textBoxResult.Text = null;
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Space)) { e.Handled = true; }
            string caracteresPermitidos = "01";
            if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper()))) { e.Handled = true; }
        }

        private void NoPressed(object sender, KeyPressEventArgs e)
        { string caracteresPermitidos = ""; if (!(caracteresPermitidos.Contains(e.KeyChar.ToString().ToUpper()))) { e.Handled = true; } }



    }
}


