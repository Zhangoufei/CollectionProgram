using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfCollectionDemo1.mvvm的使用.ViewModel;

namespace WpfCollectionDemo1
{
    /// <summary>
    /// DropWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DropWindow : Window
    {

        DropWindowVm dropWindowVm;
        public DropWindow()
        {
            InitializeComponent();

            DataContext = dropWindowVm = new DropWindowVm();

            this.listBox.AddHandler(UIElement.MouseDownEvent,
             new MouseButtonEventHandler(ListBoxFile_MouseDown), true);
        }

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
        }


        Point pos = new Point();



        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {

                //显示图片
                FileNames fileNames = (listBox.SelectedItem as FileNames);
                string[] files = new string[1];
                files[0] = fileNames.TitleName;

                dropWindowVm.PicImageDrop = fileNames.PicImage;


                DragDrop.DoDragDrop(textBox3, new DataObject(DataFormats.Text, files[0]), DragDropEffects.Copy | DragDropEffects.Move   /*DragDropEffects.Link*/);
            }
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            ((TextBlock)sender).Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void TextBox2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragDrop.DoDragDrop(textBox2, textBox2.Text, DragDropEffects.Copy);
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            textBox3.Text = e.Data.GetData(DataFormats.Text).ToString();
        }
        private void ImageDrop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pos = e.GetPosition(imageDrop);
            imageDrop.CaptureMouse();
        }

        private void ImageDrop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point point = e.GetPosition(canvastest);
                double dx = point.X - pos.X;
                double dy = point.Y - pos.Y;
                imageDrop.SetValue(Canvas.LeftProperty, dx - 20);
                imageDrop.SetValue(Canvas.TopProperty, dy - 20);

                FileNames fileNames = (listBox.SelectedItem as FileNames);
                imageDrop.Tag = fileNames;
            }
        }

        private void ImageDrop_MouseUp(object sender, MouseButtonEventArgs e)
        {
            pos = new Point(0,0);
            imageDrop.ReleaseMouseCapture();
            //imageDrop.Visibility = Visibility.Collapsed;
        }



        Point point;
        private void ListBoxFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imageDrop.Visibility = Visibility.Visible;
            imageDrop.CaptureMouse();

            FileNames fileNames = (listBox.SelectedItem as FileNames);

            dropWindowVm.PicImageDrop = fileNames.PicImage;

        }

        private void ListBox_TouchDown(object sender, TouchEventArgs e)
        {
            imageDrop.Visibility = Visibility.Visible;
            imageDrop.CaptureMouse();

            FileNames fileNames = (listBox.SelectedItem as FileNames);

            dropWindowVm.PicImageDrop = fileNames.PicImage;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //imageDrop.Visibility = Visibility.Visible;
            //imageDrop.CaptureMouse();


            //Image image = sender as Image;
            //string temp = image.Tag.ToString();

            //dropWindowVm.PicImageDrop = temp;
        }

        private void ImageDrop2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        int num = 0;
        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                //显示图片
                FileNames fileNames = (listBox.SelectedItem as FileNames);

                dropWindowVm.PicImageDrop = fileNames.PicImage;

                DragDrop.DoDragDrop(textBox3, new DataObject(DataFormats.Text, fileNames.TitleName), DragDropEffects.Copy | DragDropEffects.Move   /*DragDropEffects.Link*/);
            }
        }

        private void ListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void TextBox_MouseMove(object sender, MouseEventArgs e)
        {
            ContentControl image = sender as ContentControl;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DoImageMove(image, e.GetPosition(textBox));
            }

        }

        private void DoImageMove(ContentControl image, Point position)
        {

            TransformGroup group = grid.FindResource("ImageCompareResources") as TransformGroup;

            TranslateTransform transform = group.Children[1] as TranslateTransform;

            transform.X += position.X - this.previousMousePoint.X;

            transform.Y += position.Y - this.previousMousePoint.Y;

            this.previousMousePoint = position;



        }
        Point previousMousePoint;
        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            previousMousePoint = e.GetPosition(textBox);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            FileNames fileNames = imageDrop.Tag as FileNames;

            if (fileNames == null) return;
            textBox3.Text = fileNames.TitleName;
        }

        private void ListBox_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

     
    }
}
