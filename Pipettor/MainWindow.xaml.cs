using System.Configuration;
using System.IO.Ports;
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
            Thread.Sleep(500);
   

            MotorController.Instance.OnEncoderValue += Instance_onEncoderValue;
            
            

            //MotorController.Instance.Rotate2ABSAngle(1, 111);
            //MotorController.Instance.Rotate2ABSAngle(2, 111);
            //double arm1Position = Arm1StartPosition;/* double.Parse(ConfigurationManager.AppSettings["Arm1StartPosition"]);*/
            //double arm2Position = Arm2StartPosition;/* double.Parse(ConfigurationManager.AppSettings["Arm2StartPosition"]);*/
            ////MotorController.Instance.Rotate2ABSAngle(1, arm1Position);
            //Thread.Sleep(100);
            //MotorController.Instance.Rotate2ABSAngle(2, arm2Position);
        }

        private void Instance_onEncoderValue(object sender, System.Collections.Generic.Dictionary<int, float> id_encoderValue)
        {
            if (id_encoderValue.ContainsKey(1))
            {
                MessageBox.Show(id_encoderValue[1].ToString());//test
                MotorController.Instance.Rotate2ABSAngle(1, id_encoderValue[1]);
            }

            if (id_encoderValue.ContainsKey(2))
            {
                MessageBox.Show(id_encoderValue[2].ToString());//test
                MotorController.Instance.Rotate2ABSAngle(2, id_encoderValue[2]);
            }
        }




        private void btnSetDegree_Click(object sender, RoutedEventArgs e)
        {


            int tubeID = int.Parse(txtTubeID.Text);

            myCanvas.Move2Tube(tubeID);


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

        //private void BtnSet180_Click(object sender, RoutedEventArgs e)
        //{
        //    MotorController.Instance.Rotate2ABSAngleAtSpeed(1, 180);
        //}

        //private void BtnSet10_Click(object sender, RoutedEventArgs e)
        //{
        //    MotorController.Instance.Rotate2ABSAngleAtSpeed(1, 10);
        //}

        //private void BtnSetMinus10_Click(object sender, RoutedEventArgs e)
        //{
        //    MotorController.Instance.Rotate2ABSAngleAtSpeed(1, -10);
        //}

        //private void BtnSetMinus180_Click(object sender, RoutedEventArgs e)
        //{
        //    MotorController.Instance.Rotate2ABSAngleAtSpeed(1, -180);
        //}
    }
}
