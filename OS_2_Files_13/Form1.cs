using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace OS_2_Files_13
{
    public partial class Form_Main : Form
    {
        string dir1, dir2;
        public Form_Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tb_result.Clear();
            //Если оба диалога завершили корректно
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK &&
                folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                //Получаем пути и выводим их
                dir1 = folderBrowserDialog1.SelectedPath;
                dir2 = folderBrowserDialog2.SelectedPath;
                tb_result.Text += "1-я директория: "+dir1 + "\r\n" +"2-я директория: "+ dir2 + "\r\n";
                if (dir1 == dir2)
                {
                    tb_result.Text += "Указанные директории совпадают\r\n";
                    return;
                }
                //Вызываем методы сравнений
                //Создаем потоки, в котором выводим в текстбокс сообщения, возвращаемые анализаторами/
                Thread countFilesThread = new Thread(new ThreadStart(new Action(() => 
                    tb_result.Invoke(new Action(()=>
                        tb_result.AppendText(CountAnalyser.Analysis(dir1, dir2)))))));
                Thread datFilesThread = new Thread(new ThreadStart(new Action(() => 
                    tb_result.Invoke(new Action(()=>
                        tb_result.AppendText(TimeAnalyser.Analysis(dir1, dir2)))))));

                countFilesThread.Start();
                datFilesThread.Start();
                countFilesThread.Join(1000);
                datFilesThread.Join(1000);
            }
        }

        void CountRun(string dir1, string dir2)
        {
            string msg = CountAnalyser.Analysis(dir1, dir2);
            tb_result.Invoke(new Action(() => tb_result.AppendText(msg)));
        }
        private void button_condition_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Заданы два каталога. Для каждого из них найти, вывести и сравнить общее количество файлов, диапазон дат создания", "Условие", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void Form_Main_Load(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"D:\test";
            folderBrowserDialog2.SelectedPath = @"D:\test";
        }
    }
}
