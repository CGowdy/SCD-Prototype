using Controller_Config_Items;
using Controller_Input_Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using VDF_Parser;

namespace SCD_Prototype
{
    public partial class MainForm : Form
    {
        private const string STEAM_DESKTOP_CONFIG_DESC_TEXT = "#SettingsControllerCfg_ConfigDesktop_Desc";
        private string desktopVDFFileLoc;
        private ControllerMapping controllerMapping;
        private A_Button a;
        private B_Button b;
        private X_Button x;
        private Y_Button y;
        public MainForm()
        {
            InitializeComponent();
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                VDFParser parser = new VDFParser();
                ParentKey parent = parser.Parse(new StreamReader(File.Open(desktopVDFFileLoc, FileMode.Open)));
                controllerMapping = new ControllerMapping();
                controllerMapping.ParseParentKey(parent);

                a = new A_Button(controllerMapping);
                b = new B_Button(controllerMapping);
                x = new X_Button(controllerMapping);
                y = new Y_Button(controllerMapping);
                lbl_A_Button.Text = a.action;
                lbl_B_Button.Text = b.action;
                lbl_X_Button.Text = x.action;
                lbl_Y_Button.Text = y.action;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read file");
                Console.WriteLine(ex.Message);
            }


        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //SaveFileDialog sfd = new SaveFileDialog();
            //Stream s;
            //if(sfd.ShowDialog() == DialogResult.OK)
            //{
            //    if((s = sfd.OpenFile()) != null)
            //    {
            //        using(StreamWriter sw = new StreamWriter(s))
            //        {
            //            sw.WriteLine(controllerMapping.ExportToVDF());
            //        }
            //    }
            //}
            File.Delete(desktopVDFFileLoc + ".bak");
            File.Copy(desktopVDFFileLoc, desktopVDFFileLoc + ".bak");
            File.WriteAllText(desktopVDFFileLoc, controllerMapping.ExportToVDF());

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            InputForm inForm = new InputForm();
            inForm.ShowDialog();
            lbl_A_Button.Text = "key_press " + inForm.pressedKey.ToUpper();
            a.action = lbl_A_Button.Text;
            a.ChangeAction(a.action);


        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog();
            string steamFolderPath = "";
            ofd.Description = "Select Steam Folder...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                steamFolderPath = ofd.SelectedPath.ToString();
            }
            //TODO: Need to have some catch here if dialog doesn't work

            foreach (string f in Directory.GetFiles(steamFolderPath, "*autosave.vdf", SearchOption.AllDirectories))
            {
                using (StreamReader sr = new StreamReader(File.Open(f, FileMode.Open)))
                {
                    while (!sr.EndOfStream)
                    {
                        if (sr.ReadLine().Contains(STEAM_DESKTOP_CONFIG_DESC_TEXT) && f.Contains("autosave"))
                        {
                            desktopVDFFileLoc = f;
                            tbxFolderPath.Text = desktopVDFFileLoc;
                            btnImport.Enabled = true;
                            btnExport.Enabled = true;
                            break;
                        }

                    }

                }
            }
        }
    }
}
