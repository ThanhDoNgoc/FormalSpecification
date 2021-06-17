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
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using ScintillaNET;
using ScintillaNET.WPF;
using System.Text.RegularExpressions;
using System.Drawing;


namespace FormalSpecification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbOutput.WrapMode = tbInput.WrapMode = WrapMode.None;
            tbOutput.IndentationGuides = tbInput.IndentationGuides = IndentView.LookBoth;


            InitColors(tbOutput);
            InitColors(tbInput);
            InitSyntaxColoring(tbOutput);
            InitSyntaxColoring(tbInput);
            InitNumberMargin(tbOutput);
            InitNumberMargin(tbInput);
            tbInput.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            tbInput.SetKeywords(1, "R Z N B def void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path System File Windows Forms ScintillaNET");
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            tbInput.Text = "";
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT|*.txt";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var str = File.ReadAllText(filePath);
                tbInput.Text = str;
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt | All Files (*.*)|*.*";


            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(File.Create(saveFileDialog.FileName));
                sw.Write(tbInput.Text);
                sw.Dispose();
            }           
        }
        public static System.Drawing.Color IntToColor(int rgb)
        {
            return System.Drawing.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
        private void InitNumberMargin(ScintillaNET.WPF.ScintillaWPF TextArea)
        {
            //TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = IntToColor(0x212121);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = System.Drawing.Color.DarkBlue;
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = IntToColor(0xB7B7B7);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = IntToColor(0x2A211C);

            var nums = TextArea.Margins[0];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;


        }
        private void InitSyntaxColoring(ScintillaNET.WPF.ScintillaWPF TextArea)
        {

            // Configure the default style
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 10;

            TextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            TextArea.Styles[ScintillaNET.Style.Cpp.Identifier].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = System.Drawing.Color.DarkGreen;
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDoc].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.String].ForeColor = System.Drawing.Color.DarkRed;
            TextArea.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            TextArea.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.Regex].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = System.Drawing.Color.Blue;
            TextArea.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = System.Drawing.Color.Blue;
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeyword].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.CommentDocKeywordError].ForeColor = System.Drawing.Color.Black;
            TextArea.Styles[ScintillaNET.Style.Cpp.GlobalClass].ForeColor = System.Drawing.Color.Black;

            TextArea.Lexer = Lexer.Cpp;

            TextArea.SetKeywords(0, "pre post class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            TextArea.SetKeywords(1, "def void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path System File Windows Forms ScintillaNET");

        }

        private void InitColors(ScintillaNET.WPF.ScintillaWPF TextArea)
        {
            TextArea.CaretForeColor = Colors.White;
            TextArea.SetSelectionBackColor(true, IntToMediaColor(0x114D9C));

        }
        public static System.Windows.Media.Color IntToMediaColor(int rgb)
        {
            return System.Windows.Media.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void btnCplusplus_Click(object sender, RoutedEventArgs e)
        {
            string str = tbInput.Text;
            str = Regex.Replace(str, @"\s+",string.Empty);
            Match pre = Regex.Match(str, "pre");
            Match post = Regex.Match(str, "post");

            if (pre.Success && post.Success )
            {
                Provider provider = new Provider(tbClass.Text, tbInput.Text);
                CPlusPlusTranslate cpp = new CPlusPlusTranslate();
                cpp.CPP_transtale(provider);
                tbOutput.Text = cpp.generateCPPCode();
            }
            else
            {
                System.Windows.MessageBox.Show("Inccorrect formal");
            }
        }

        private void btnCsharp_Click(object sender, RoutedEventArgs e)
        {
            string str = tbInput.Text;
            str = Regex.Replace(str, @"\s+", string.Empty);
            Match pre = Regex.Match(str, "pre");
            Match post = Regex.Match(str, "post");

            if (pre.Success && post.Success)
            {
                Provider provider = new Provider(tbClass.Text, tbInput.Text);
                CSharpTranslate cshap = new CSharpTranslate();
                cshap.CSharp_transtale(provider);
                tbOutput.Text = cshap.generateCSharpCode();
            }
            else
            {
                System.Windows.MessageBox.Show("Incorrect formal");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
