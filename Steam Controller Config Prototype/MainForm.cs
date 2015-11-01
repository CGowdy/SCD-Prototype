using Controller_Config_Items;
using Controller_Input_Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VDF_Parser;

namespace Steam_Controller_Config_Prototype
{
    public partial class MainForm : Form
    {
        private string desktopVDFFileLoc;
        private ControllerMapping controllerMapping;
        public MainForm()
        {
            InitializeComponent();

#if DEBUG
            desktopVDFFileLoc = "C:\\Program Files (x86)\\Steam\\controller_base\\steamdesktop.vdf";
#endif
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ControllerComponentObject> controllerComponents = new List<ControllerComponentObject>();
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                desktopVDFFileLoc = ofd.FileName;
            }
            VDFParser parser = new VDFParser();
            ParentKey parent = parser.Parse(new StreamReader(File.Open(desktopVDFFileLoc, FileMode.Open)));


            controllerMapping = new ControllerMapping();
            controllerMapping.ParseParentKey(parent);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                VDFParser parser = new VDFParser();
                ParentKey parent = parser.Parse(new StreamReader(File.Open(desktopVDFFileLoc, FileMode.Open)));
                controllerMapping = new ControllerMapping();
                controllerMapping.ParseParentKey(parent);

                A_Button a = new A_Button(controllerMapping);
                lbl_A_Button.Text = a.action;
                B_Button b = new B_Button(controllerMapping);
                lbl_B_Button.Text = b.action;
                X_Button x = new X_Button(controllerMapping);
                lbl_X_Button.Text = x.action;
                Y_Button y = new Y_Button(controllerMapping);
                lbl_Y_Button.Text = y.action;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read file");
                Console.WriteLine(ex.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            Stream s;
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                if((s = sfd.OpenFile()) != null)
                {
                    using(StreamWriter sw = new StreamWriter(s))
                    {
                        sw.WriteLine(controllerMapping.ExportToVDF());
                    }
                }
            }
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
