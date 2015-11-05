using System.Windows.Forms;

namespace SCD_Prototype
{
    public partial class InputForm : Form
    {
        public string pressedKey;
        public InputForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Focus();
        }

        private void InputForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            tbxBinding.Text = e.KeyChar.ToString();
            btnAssign.Enabled = true;
        }

        private void btnAssign_Click(object sender, System.EventArgs e)
        {
            pressedKey = tbxBinding.Text;
            this.Close();
        }
    }
}
