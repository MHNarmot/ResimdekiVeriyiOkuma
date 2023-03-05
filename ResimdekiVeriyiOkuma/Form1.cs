using IronOcr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronOcr.Exceptions;
using IronOcr.Events;
using System.Text.RegularExpressions;

namespace ResimdekiVeriyiOkuma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Resmi yükleyin
                string filePath = dialog.FileName;

                // OCR motorunu yapılandırın
                var Ocr = new IronTesseract();
                Ocr.Language = IronOcr.OcrLanguage.TurkishBest;
                Ocr.Configuration.ReadBarCodes = false;
                Ocr.Configuration.RenderSearchablePdfsAndHocr = false;
                Ocr.Configuration.WhiteListCharacters = "0123456789.,";
                Ocr.Configuration.PageSegmentationMode = IronOcr.TesseractPageSegmentationMode.SingleBlock;

                // OCR motorunu kullanarak resimdeki metni okuyun
                var Result = Ocr.Read(filePath);

                // Tüm metni alın ve rakamları Regex ile ayıklayın
                string allText = Result.Text;
                string pattern = @"\d{1,3}(\.\d{3})*,\d{2}";
                MatchCollection matches = Regex.Matches(allText, pattern);

                // Rakamları ListBox'a ekle
                foreach (Match match in matches)
                {
                    listBox1.Items.Add(match.Value);
                }
            }
        }
    }
}
