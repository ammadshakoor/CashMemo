using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashMemo
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (!txtChequeNo.Text.Equals("") && !cbBankName.Text.Equals("")
                && !txtBranch.Text.Equals("") && !txtAccountNo.Text.Equals(""))
            {
                AllDataContext dc = new AllDataContext();
                var c = new Cheque()
                {
                    ChequeNo = Convert.ToInt32(txtChequeNo.Text),
                    BankName = cbBankName.Text,
                    Branch = txtBranch.Text,
                    AccountNo = txtAccountNo.Text,
                    Status = cbStatus.Text
                };
                dc.Cheques.InsertOnSubmit(c);
                dc.SubmitChanges();
                txtChequeNo.Text = txtBranch.Text = txtAccountNo.Text = "";
                this.Hide();
            }
            else if (txtChequeNo.Text.Equals("") && cbBankName.Text.Equals("")
                && txtBranch.Text.Equals("") && txtAccountNo.Text.Equals(""))
                MessageBox.Show("Please Fill Cheque Details");

            else
            {
                if (txtChequeNo.Text.Equals(""))
                    errorProvider1.SetError(txtChequeNo, "Cheque No. Required");

                else if (cbBankName.Text.Equals(""))
                    errorProvider1.SetError(cbBankName, "Select Bank Required");

                else if (txtBranch.Text.Equals(""))
                    errorProvider1.SetError(txtBranch, "Branch Address Required");

                else if (txtAccountNo.Text.Equals(""))
                    errorProvider1.SetError(txtAccountNo, "A/C Required");
            }
        }
    }
}
