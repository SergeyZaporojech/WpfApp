using Aspose.Imaging.ImageOptions;
using Aspose.Imaging;
using Microsoft.Win32;
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
using WpfApp1.Models;
using System.IO;
using LibData.Helpers;
using System.Drawing.Imaging;
using System.Drawing;
using WpfApp1.Helper;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SaveImageWindowaml.xaml
    /// </summary>
    public partial class SaveImageWindowaml : Window
    {
        public SaveImageWindowaml()
        {
            InitializeComponent();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog()== true)
            {

                string imputFile = dlg.FileName;
                imputImage.Source = UserVM.toBitmap(File.ReadAllBytes(imputFile));

                try
                {
                    using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(imputFile)))
                    {

                        var img = System.Drawing.Image.FromStream(stream);
                        Bitmap bitmap = new Bitmap(img);
                        string imageName = System.IO.Path.GetRandomFileName() + ".jpg";
                        string[] sizes = { "32", "100", "300", "600", "1200" };
                        foreach (var size in sizes)
                        {
                            int width = int.Parse(size);
                            var saveBmp = ImageWorker.CompressImage(bitmap, width, width, false);
                            saveBmp.Save($"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{size}_{imageName}",
                                ImageFormat.Jpeg);

                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
              
                   


               




                //using (var image = Image.Load(imputFile))
                //{
                //    string outputFile = @"C:\Users\Sergiy\Desktop\WebP\template.jpg";

                //    ImageOptionsBase exportOptions = new JpegOptions();

                //    image.Save(outputFile, exportOptions);

                //}




                //MessageBox.Show("select file" + dlg.FileName);

            }
        }
    }
}
