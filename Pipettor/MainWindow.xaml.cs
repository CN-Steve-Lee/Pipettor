using System.Threading;
using System.Windows;

namespace Pipettor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MotorController.Instance.OpenSerialPort();
        }

        private void btnSetDegree_Click(object sender, RoutedEventArgs e)
        {
            //double arm1Angle = double.Parse(txtArm1Angle.Text);
            //double arm2Angle = double.Parse(txtArm2Angle.Text);

            //MotorController.Instance.SetSpeed(speed);

            //MotorController.Instance.RotateAtSpeed(1, arm1Angle);
            //Thread.Sleep(100);
            //MotorController.Instance.RotateAtSpeed(2, arm2Angle);
            int tubeID = int.Parse(txtTubeID.Text);
            double arm1Angle;
            double arm2Angle;


            myCanvas.Move2Tube(tubeID);

            //arm1Angle = ;
            //arm2Angle = ;

            //MotorController.Instance.RotateAtSpeed(1, arm1Angle);
            //Thread.Sleep(100);
            //MotorController.Instance.RotateAtSpeed(2, arm2Angle);











            //--------------------------------------------------------
            //Thread.Sleep(100);
            //MotorController.Instance.RotateAtSpeed(1, arm1Angle);
            //for(int i = 0; i< 20; i++)
            //{
            //    MotorController.Instance.RotateAtSpeed(1, 170);
            //    Thread.Sleep(500);
            //    MotorController.Instance.RotateAtSpeed(1, 100.6);
            //    Thread.Sleep(2000);
            //}
        }
    }
}
