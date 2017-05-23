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
using System.Windows.Shapes;

namespace WordTestApp
{
    /// <summary>
    /// ThreeQ.xaml の相互作用ロジック
    /// </summary>
    public partial class ThreeQ : Window
    {
        List<string[]> wordsList = new List<string[]>();
        int[] shuffle;
        int qNumber;
        bool power = false;
        public ThreeQ(List<string[]> wList)
        {
            InitializeComponent();
            wordsList = wList;
            qArea.Text = "";
            one.Content = "";
            two.Content = "";
            three.Content = "";
            startButton.IsEnabled = true;
            one.IsEnabled = false;
            two.IsEnabled = false;
            three.IsEnabled = false;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if(wordsList.Count > 2)
            {
                int wc = wordsList.Count;
                int[] shuf = new int[wc];
                for (int i = 0; i < wc; i++)
                {
                    shuf[i] = i;
                }
                //シャッフルする
                shuffle = shuf.OrderBy(i => Guid.NewGuid()).ToArray();
                qNumber = 0;
                startButton.IsEnabled = false;
                one.IsEnabled = true;
                two.IsEnabled = true;
                three.IsEnabled = true;
                power = true;
                SelectAnswers(shuffle[0]);
            }
        }

        void SelectAnswers(int qn)
        {
            int[] sel = new int[3];
            int[] outp = new int[3];
            int wc = wordsList.Count;
            int[] s = new int[wc];
            int[] sh = new int[wc];

            sel[0] = qn;
            for (int i = 0; i < wc; i++)
            {
                s[i] = i;
            }
            //シャッフルする
            sh = s.OrderBy(i => Guid.NewGuid()).ToArray();

            if(sh[0]==qn)
            {
                sel[1] = sh[1];
                sel[2] = sh[2];
            }
            else if(sh[1]==qn)
            {
                sel[1] = sh[0];
                sel[2] = sh[2];
            }
            else
            {
                sel[1] = sh[0];
                sel[2] = sh[1];
            }

            qArea.Text = wordsList[qn][0];
            
            outp = sel.OrderBy(i => Guid.NewGuid()).ToArray();

            one.Content = wordsList[outp[0]][1];
            two.Content = wordsList[outp[1]][1];
            three.Content = wordsList[outp[2]][1];
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
           
            Button b = (Button)sender;
            Answer_Keydown(b);
        }

        void Answer_Keydown(Button sender)
        {
            if (power)
            {
                if ((string)sender.Content != wordsList[shuffle[qNumber]][1])
                {
                    MessageBox.Show("「" + wordsList[shuffle[qNumber]][0] + "」は" + wordsList[shuffle[qNumber]][1] + "です！！");
                }
                if (qNumber == wordsList.Count - 1)
                {
                    qArea.Text = "All End!";
                    one.Content = "";
                    two.Content = "";
                    three.Content = "";
                    one.IsEnabled = false;
                    two.IsEnabled = false;
                    three.IsEnabled = false;
                    power = false;
                    startButton.IsEnabled = true;
                }
                else
                {
                    qNumber++;
                    SelectAnswers(shuffle[qNumber]);
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            switch (e.Key.ToString())
            {
                case "D1":
                    Answer_Keydown(one);
                    break;
                case "D2":
                    Answer_Keydown(two);
                    break;
                case "D3":
                    Answer_Keydown(three);
                    break;

            }
        }
    }
}
