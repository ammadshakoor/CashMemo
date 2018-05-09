using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashMemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CGrid();
            POC();
            ORGrid();
            OPRefreshGrid();
            PRefreshGrid();
            CRefreshGrid();
            LGrid();
            LRefreshGrid();

            AllDataContext dc = new AllDataContext();
            Ordr o = new Ordr();

            int? p = dc.Ordrs.Max(cu => (int?)cu.Id);
            if (p == null)
                p = 0;

            var u = p + 1;
            lblOId.Text = u.ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        //tabPage Customer Start
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Addpanel.Visible = true;
            Updatepanel.Visible = false;
            Deletepanel.Visible = false;
            listBox1.Visible = false;
        }

        private void AddCustomer()                                   //Add Customer Method
        {
            AllDataContext dc = new AllDataContext();
            Customer c = new Customer()
            {
                Name = txtName.Text,
                MobileNumber = txtMobile.Text,
                Address = txtAddress.Text,
                CNIC = txtCNIC.Text
            };
            dc.Customers.InsertOnSubmit(c);
            dc.SubmitChanges();
        }

        private void btnAdd_Click(object sender, EventArgs e)           //Add Customer
        {
            errorProvider1.Clear();
            if (!txtName.Text.Equals("") && !txtMobile.Text.Equals("")
                && !txtCNIC.Text.Equals("") && !txtAddress.Text.Equals(""))
            {
                AddCustomer();
                txtName.Text = txtMobile.Text = txtCNIC.Text = txtAddress.Text = "";
                MessageBox.Show("New Customer Added Successfully!");
                RefreshGrid();
                POC();
                LGrid();
            }
            else if (txtName.Text.Equals("") && txtMobile.Text.Equals("")
                && txtCNIC.Text.Equals("") && txtAddress.Text.Equals(""))
                MessageBox.Show("Please Fill the Complete Form");

            else {
                if (txtName.Text.Equals(""))
                    errorProvider1.SetError(txtName, "Name is missing");

                else if (txtMobile.Text.Equals(""))
                    errorProvider1.SetError(txtMobile, "Mobile Number is missing");

                else if (txtAddress.Text.Equals(""))
                    errorProvider1.SetError(txtAddress, "Address is missing");

                else if (txtCNIC.Text.Equals(""))
                    errorProvider1.SetError(txtCNIC, "CNIC is missing");
            }
        }

        private void RefreshGrid()                     //Show Customer In Grid After Add
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.GetTable<Customer>()
                    select cu;
            dataGridView1.DataSource = q;

            var CustomerName =
                    from cu in dc.Customers
                    select new { cu.Name, cu.Id };
            listBox1.DataSource = CustomerName;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
        }

        private void CGrid()                            //Customer's DataGridView
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.GetTable<Customer>()
                    select cu;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].Name = "name";
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[0].DataPropertyName = "Name";

            dataGridView1.Columns[1].Name = "mobilenumber";
            dataGridView1.Columns[1].HeaderText = "Mobile Number";
            dataGridView1.Columns[1].DataPropertyName = "MobileNumber";

            dataGridView1.Columns[2].Name = "address";
            dataGridView1.Columns[2].HeaderText = "Address";
            dataGridView1.Columns[2].DataPropertyName = "Address";

            dataGridView1.Columns[3].Name = "cnic";
            dataGridView1.Columns[3].HeaderText = "CNIC";
            dataGridView1.Columns[3].DataPropertyName = "CNIC";

            dataGridView1.DataSource = q;

            var CustomerName =
                    from cu in dc.Customers
                    select new { cu.Name, cu.Id };
            listBox1.DataSource = CustomerName;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            Updatepanel.Visible = true;
            listBox1.Visible = true;
            Addpanel.Visible = false;
            Deletepanel.Visible = false;
        }

        private void listBox1_Click(object sender, EventArgs e)     //Customer's Listbox
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox1.SelectedValue);
            Customer cs = dc.Customers.Single(c => c.Id == selectedId);

            txtUpdateName.Text = cs.Name;
            txtUpdateMobile.Text = cs.MobileNumber;
            txtUpdateAddress.Text = cs.Address;
            txtUpdateCNIC.Text = cs.CNIC;
        }

        private void updateGrid()                                   //Customer's Update Method
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox1.SelectedValue);
            Customer cs = dc.Customers.Single(c => c.Id == selectedId);

            cs.Name = txtUpdateName.Text;
            cs.MobileNumber = txtUpdateMobile.Text;
            cs.Address = txtUpdateAddress.Text;
            cs.CNIC = txtUpdateCNIC.Text;

            dc.SubmitChanges();
        }

        private void btnUpdate_Click(object sender, EventArgs e)    //Update Customer
        {
            errorProvider1.Clear();
            if (!txtUpdateName.Text.Equals("") && !txtUpdateMobile.Text.Equals("")
                && !txtUpdateCNIC.Text.Equals("") && !txtUpdateAddress.Text.Equals(""))
            {
                updateGrid();
                txtUpdateName.Text = txtUpdateMobile.Text = txtUpdateAddress.Text = txtUpdateCNIC.Text = "";
                MessageBox.Show("Customer Updated Successfully!");
                RefreshGrid();
                POC();
                LGrid();
            }
            else if (txtUpdateName.Text.Equals("") && txtUpdateMobile.Text.Equals("")
                && txtUpdateCNIC.Text.Equals("") && txtUpdateAddress.Text.Equals(""))
                MessageBox.Show("Please Fill the Complete Form");

            else
            {
                if (txtUpdateName.Text.Equals(""))
                    errorProvider1.SetError(txtUpdateName, "Name is missing");

                else if (txtUpdateMobile.Text.Equals(""))
                    errorProvider1.SetError(txtUpdateMobile, "Mobile Number is missing");

                else if (txtUpdateAddress.Text.Equals(""))
                    errorProvider1.SetError(txtUpdateAddress, "Address is missing");

                else if (txtUpdateCNIC.Text.Equals(""))
                    errorProvider1.SetError(txtUpdateCNIC, "CNIC is missing");
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            Deletepanel.Visible = true;
            Addpanel.Visible = false;
            Updatepanel.Visible = false;
            listBox1.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)    //Search Customer
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.Customers
                    where cu.Name.Contains(txtDelete.Text)
                    select cu;
            dataGridView1.DataSource = q;
        }

        private void btnDelete_Click(object sender, EventArgs e)    //Delete Customer
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox1.SelectedValue);
            Customer cs = dc.Customers.Single(c => c.Id == selectedId);
            dc.Customers.DeleteOnSubmit(cs);
            dc.SubmitChanges();
            MessageBox.Show("Customer Deleted Successfully!");
            RefreshGrid();
            POC();
            LGrid();
        }
        //tabPage Customer End


        //tabPage Product Start
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddPpanel.Visible = true;
            UpdatePPanel.Visible = false;
        }

        private void PRefreshGrid()                     //Product DataGridView & ListBox
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.FastFoods
                    select cu;
            dataGridView2.DataSource = q;

            var p = from cu in dc.FastFoods
                    select new { cu.Name, cu.Id };
            listBox2.DataSource = p;
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "Id";
        }

        private void AddProduct()                       //Add Product Method
        {
            AllDataContext dc = new AllDataContext();
            FastFood ff = new FastFood()
            {
                Name = txtPName.Text,
                Rate = txtPRate.Text
            };
            dc.FastFoods.InsertOnSubmit(ff);
            dc.SubmitChanges();
        }

        private void btnAddP_Click(object sender, EventArgs e)         //Add Product
        {
            errorProvider1.Clear();
            if (!txtPName.Text.Equals("") && !txtPRate.Text.Equals(""))
            {
                AddProduct();
                txtPName.Text = txtPRate.Text = "";
                MessageBox.Show("New Product Added Successfully!");
                PRefreshGrid();
                OPRefreshGrid();
            }
            else if (txtPName.Text.Equals("") && txtPRate.Text.Equals(""))
                MessageBox.Show("Please Fill the Complete Form");

            else
            {
                if (txtPName.Text.Equals(""))
                    errorProvider1.SetError(txtPName, "Product Name is missing");

                else if (txtPRate.Text.Equals(""))
                    errorProvider1.SetError(txtPRate, "Rate is missing");
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            AddPpanel.Visible = false;
            UpdatePPanel.Visible = true;
        }

        private void listBox2_Click(object sender, EventArgs e)     //Product ListBox
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox2.SelectedValue);
            FastFood cs = dc.FastFoods.Single(c => c.Id == selectedId);

            txtUpdatePName.Text = cs.Name;
            txtUpdatePRate.Text = cs.Rate;
        }

        private void PUpdateGrid()                              //Update Product Method
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox2.SelectedValue);
            FastFood ff = dc.FastFoods.Single(c => c.Id == selectedId);

            ff.Name = txtUpdatePName.Text;
            ff.Rate = txtUpdatePRate.Text;

            dc.SubmitChanges();
        }

        private void btnPUpdate_Click(object sender, EventArgs e)   //Update Product
        {
            errorProvider1.Clear();
            if (!txtUpdatePName.Text.Equals("") && !txtUpdatePRate.Text.Equals(""))
            {
                PUpdateGrid();
                txtUpdatePName.Text = txtUpdatePRate.Text = "";
                MessageBox.Show("Product Updated Successfully!");
                PRefreshGrid();
                OPRefreshGrid();
            }
            else if (txtUpdatePName.Text.Equals("") && txtUpdatePRate.Text.Equals(""))
                MessageBox.Show("Please Fill the Complete Form");

            else
            {
                if (txtUpdatePName.Text.Equals(""))
                    errorProvider1.SetError(txtUpdatePName, "Product Name is missing");

                else if (txtUpdatePRate.Text.Equals(""))
                    errorProvider1.SetError(txtUpdatePRate, "Rate is missing");
            }
        }

        private void btnPDelete_Click(object sender, EventArgs e)   //Delete Product
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox2.SelectedValue);
            FastFood ff = dc.FastFoods.Single(c => c.Id == selectedId);
            dc.FastFoods.DeleteOnSubmit(ff);
            dc.SubmitChanges();
            txtUpdatePName.Text = txtUpdatePRate.Text = "";
            MessageBox.Show("Product Deleted Successfully!");
            PRefreshGrid();
            OPRefreshGrid();
        }
        //tabPage Product End


        //tabPage CashMemo Start
        /*private void txtd1_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd1.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr1.Text = q;
        //}
        //private void txtd2_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd2.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr2.Text = q;
        //}
        //private void txtd3_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd3.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr3.Text = q;
        //}
        //private void txtd4_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd4.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr4.Text = q;
        //}
        //private void txtd5_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd5.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr5.Text = q;
        //}
        //private void txtd6_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd6.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr6.Text = q;
        //}
        //private void txtd7_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd7.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr7.Text = q;
        //}
        //private void txtd8_TextChanged(object sender, EventArgs e)
        //{
        //    AllDataContext dc = new AllDataContext();
        //    var q = (from cu in dc.GetTable<FastFood>()
        //             where cu.Name == txtd8.Text
        //             select cu.Rate).SingleOrDefault();
        //    txtr8.Text = q;
        }*/

        private void txtq1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq1.Text) && !string.IsNullOrEmpty(txtr1.Text))
                txtp1.Text = (Convert.ToDouble(txtq1.Text) * Convert.ToDouble(txtr1.Text)).ToString();
        }
        private void txtq2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq2.Text) && !string.IsNullOrEmpty(txtr2.Text))
                txtp2.Text = (Convert.ToDouble(txtq2.Text) * Convert.ToDouble(txtr2.Text)).ToString();
        }
        private void txtq3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq3.Text) && !string.IsNullOrEmpty(txtr3.Text))
                txtp3.Text = (Convert.ToDouble(txtq3.Text) * Convert.ToDouble(txtr3.Text)).ToString();
        }
        private void txtq4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq4.Text) && !string.IsNullOrEmpty(txtr4.Text))
                txtp4.Text = (Convert.ToDouble(txtq4.Text) * Convert.ToDouble(txtr4.Text)).ToString();
        }
        private void txtq5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq5.Text) && !string.IsNullOrEmpty(txtr5.Text))
                txtp5.Text = (Convert.ToDouble(txtq5.Text) * Convert.ToDouble(txtr5.Text)).ToString();
        }
        private void txtq6_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq6.Text) && !string.IsNullOrEmpty(txtr6.Text))
                txtp6.Text = (Convert.ToDouble(txtq6.Text) * Convert.ToDouble(txtr6.Text)).ToString();
        }
        private void txtq7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq7.Text) && !string.IsNullOrEmpty(txtr7.Text))
                txtp7.Text = (Convert.ToDouble(txtq7.Text) * Convert.ToDouble(txtr7.Text)).ToString();
        }
        private void txtq8_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtq8.Text) && !string.IsNullOrEmpty(txtr8.Text))
                txtp8.Text = (Convert.ToDouble(txtq8.Text) * Convert.ToDouble(txtr8.Text)).ToString();
        }

        private void TotalPrice()                   // Calculate Total Price
        {
            if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text) && !string.IsNullOrEmpty(txtp4.Text) &&
                !string.IsNullOrEmpty(txtp5.Text) && !string.IsNullOrEmpty(txtp6.Text) &&
                !string.IsNullOrEmpty(txtp7.Text) && !string.IsNullOrEmpty(txtp8.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text) +
                                   Convert.ToDouble(txtp4.Text) +
                                   Convert.ToDouble(txtp5.Text) +
                                   Convert.ToDouble(txtp6.Text) +
                                   Convert.ToDouble(txtp7.Text) +
                                   Convert.ToDouble(txtp8.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text) && !string.IsNullOrEmpty(txtp4.Text) &&
                !string.IsNullOrEmpty(txtp5.Text) && !string.IsNullOrEmpty(txtp6.Text) &&
                !string.IsNullOrEmpty(txtp7.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text) +
                                   Convert.ToDouble(txtp4.Text) +
                                   Convert.ToDouble(txtp5.Text) +
                                   Convert.ToDouble(txtp6.Text) +
                                   Convert.ToDouble(txtp7.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text) && !string.IsNullOrEmpty(txtp4.Text) &&
                !string.IsNullOrEmpty(txtp5.Text) && !string.IsNullOrEmpty(txtp6.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text) +
                                   Convert.ToDouble(txtp4.Text) +
                                   Convert.ToDouble(txtp5.Text) +
                                   Convert.ToDouble(txtp6.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text) && !string.IsNullOrEmpty(txtp4.Text) &&
                !string.IsNullOrEmpty(txtp5.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text) +
                                   Convert.ToDouble(txtp4.Text) +
                                   Convert.ToDouble(txtp5.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text) && !string.IsNullOrEmpty(txtp4.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text) +
                                   Convert.ToDouble(txtp4.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text) &&
                !string.IsNullOrEmpty(txtp3.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text) +
                                   Convert.ToDouble(txtp3.Text)).ToString();
            }
            else if (!string.IsNullOrEmpty(txtp1.Text) && !string.IsNullOrEmpty(txtp2.Text))
            {
                lblTAmount.Text = (Convert.ToDouble(txtp1.Text) +
                                   Convert.ToDouble(txtp2.Text)).ToString();
            }
            else
                lblTAmount.Text = txtp1.Text;
        }

        private void txtp1_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp2_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp3_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp4_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp5_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp6_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp7_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        private void txtp8_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
        //CashMemo Table End

        public static int q;
        public static int t;
        private void addOrder()                             //Add Order Method
        {
            AllDataContext dc = new AllDataContext();
            Ordr o = new Ordr();

            o.Id = Convert.ToInt32(lblOId.Text);
            o.C_Id = q;
            o.Amount = lblTAmount.Text;
            o.OrderDate = label1.Text;
            if (radioButton1.Checked)
                o.Payment = radioButton1.Text;

            else if (radioButton2.Checked)
                o.Payment = radioButton2.Text;

            dc.Ordrs.InsertOnSubmit(o);
            dc.SubmitChanges();
            t = o.Id;
        }

        private void addLedger()                            //Add Ledger Method
        {
            AllDataContext dc = new AllDataContext();
            var z = (from w in dc.GetTable<Customer>()
                     where w.Name == txtCusName.Text
                     select w.Id).SingleOrDefault();

            int? i = dc.Ledgers.Where(x => x.C_Id == z)
                         .Max(x => (int?)x.Id);

            var r = (from u in dc.GetTable<Ledger>()
                     where u.Id == i
                     select u.Balance).SingleOrDefault();

            //Ledger Start
            Ledger l = new Ledger();
            l.C_Id = q;
            l.O_Id = Convert.ToInt32(lblOId.Text);

            if (Convert.ToDouble(txtPAmount.Text) == Convert.ToDouble(lblTAmount.Text))
                l.Balance = r;

            else if (Convert.ToDouble(txtPAmount.Text) == 0)
            {
                l.Credit = lblTAmount.Text;
                l.Balance = (Convert.ToDouble(r) + Convert.ToDouble(l.Credit)).ToString();
            }
            else if (Convert.ToDouble(txtPAmount.Text) < Convert.ToDouble(lblTAmount.Text))
            {
                double j = Convert.ToDouble(lblTAmount.Text) - Convert.ToDouble(txtPAmount.Text);
                l.Credit = Convert.ToString(j);
                l.Balance = (Convert.ToDouble(r) + Convert.ToDouble(l.Credit)).ToString();
            }
            else
            {
                double j = Convert.ToDouble(txtPAmount.Text) - Convert.ToDouble(lblTAmount.Text);
                l.Debit = Convert.ToString(j);
                l.Balance = (Convert.ToDouble(r) - Convert.ToDouble(l.Debit)).ToString();
            }
            dc.Ledgers.InsertOnSubmit(l);
            dc.SubmitChanges();
            //Ledger End
        }

        private void opDetails()                            //Add Order Product Details
        {
            if (txtd1.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d1 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd1.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d1;
                pd.Kg = txtq1.Text;
                pd.Price = txtp1.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd2.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d2 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd2.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d2;
                pd.Kg = txtq2.Text;
                pd.Price = txtp2.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd3.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d3 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd3.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d3;
                pd.Kg = txtq3.Text;
                pd.Price = txtp3.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd4.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d4 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd4.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d4;
                pd.Kg = txtq4.Text;
                pd.Price = txtp4.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd5.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d5 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd5.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d5;
                pd.Kg = txtq5.Text;
                pd.Price = txtp5.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd6.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d6 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd6.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d6;
                pd.Kg = txtq6.Text;
                pd.Price = txtp6.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd7.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d7 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd7.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d7;
                pd.Kg = txtq7.Text;
                pd.Price = txtp7.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
            if (txtd8.Text != "")
            {
                AllDataContext dc = new AllDataContext();
                PDetail pd = new PDetail();
                pd.O_Id = Convert.ToInt32(lblOId.Text);
                int d8 = (from cu in dc.GetTable<FastFood>()
                          where cu.Name == txtd8.Text
                          select cu.Id).SingleOrDefault();
                pd.P_Id = d8;
                pd.Kg = txtq8.Text;
                pd.Price = txtp8.Text;

                dc.PDetails.InsertOnSubmit(pd);
                dc.SubmitChanges();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e) //Add Receipt
        {
            if (radioButton1.Checked || radioButton2.Checked
                && !txtPAmount.Text.Equals("") && !txtCusName.Text.Equals(""))
            {
                AllDataContext dc = new AllDataContext();
                q = (from cu in dc.GetTable<Customer>()
                     where cu.Name == txtCusName.Text
                     select cu.Id).SingleOrDefault();

                addOrder();
                opDetails();
                addLedger();
                ORefreshGrid();
                CRefreshGrid();
                LRefreshGrid();
                LGrid();

                txtd1.Text = txtd2.Text = txtd3.Text = txtd4.Text =
                txtd5.Text = txtd6.Text = txtd7.Text = txtd8.Text =
                txtq1.Text = txtq2.Text = txtq3.Text = txtq4.Text =
                txtq5.Text = txtq6.Text = txtq7.Text = txtq8.Text =
                txtr1.Text = txtr2.Text = txtr3.Text = txtr4.Text =
                txtr5.Text = txtr6.Text = txtr7.Text = txtr8.Text =
                txtp1.Text = txtp2.Text = txtp3.Text = txtp4.Text =
                txtp5.Text = txtp6.Text = txtp7.Text = txtp8.Text =
                txtCusName.Text = txtPAmount.Text = "";
                MessageBox.Show("New Receipt Added Successfully");
                lblOId.Text = (Convert.ToInt32(lblOId.Text) + 1).ToString();
            }
            else
                MessageBox.Show("Please Fill receipt Information!");
        }

        private void txtCusName_Click(object sender, EventArgs e)
        {
            if (txtCusName.Text != "")
                txtCusName.SelectAll();
        }

        private void OPRefreshGrid()                        //Show Product in Listbox for Order after Add
        {
            AllDataContext dc = new AllDataContext();

            var p = from cu in dc.FastFoods
                    select new { cu.Name, cu.Id };
            listBox3.DataSource = p;
            listBox3.DisplayMember = "Name";
            listBox3.ValueMember = "Id";
        }

        private void listBox3_Click(object sender, EventArgs e) //Product's Listbox for Order
        {
            AllDataContext dc = new AllDataContext();
            int selectedId = Convert.ToInt32(listBox3.SelectedValue);
            FastFood cs = dc.FastFoods.Single(c => c.Id == selectedId);

            if (txtd1.Text.Equals(""))
            {
                txtd1.Text = cs.Name;
                txtr1.Text = cs.Rate;
            }

            else if (!txtd1.Text.Equals("") && txtd2.Text.Equals(""))
            {
                txtd2.Text = cs.Name;
                txtr2.Text = cs.Rate;
            }

            else if (!txtd2.Text.Equals("") && txtd3.Text.Equals(""))
            {
                txtd3.Text = cs.Name;
                txtr3.Text = cs.Rate;
            }

            else if (!txtd3.Text.Equals("") && txtd4.Text.Equals(""))
            {
                txtd4.Text = cs.Name;
                txtr4.Text = cs.Rate;
            }
            else if (!txtd4.Text.Equals("") && txtd5.Text.Equals(""))
            {
                txtd5.Text = cs.Name;
                txtr5.Text = cs.Rate;
            }
            else if (!txtd5.Text.Equals("") && txtd6.Text.Equals(""))
            {
                txtd6.Text = cs.Name;
                txtr6.Text = cs.Rate;
            }

            else if (!txtd6.Text.Equals("") && txtd7.Text.Equals(""))
            {
                txtd7.Text = cs.Name;
                txtr7.Text = cs.Rate;
            }

            else if (!txtd7.Text.Equals("") && txtd8.Text.Equals(""))
            {
                txtd8.Text = cs.Name;
                txtr8.Text = cs.Rate;
            }
            else if (!txtd1.Text.Equals("") && !txtd2.Text.Equals("")
                  && !txtd3.Text.Equals("") && !txtd4.Text.Equals("")
                  && !txtd5.Text.Equals("") && !txtd6.Text.Equals("")
                  && !txtd7.Text.Equals("") && !txtd8.Text.Equals(""))
                MessageBox.Show("Receipt List are full");
        }

        private void POC()                                  //Customer Name get
        {
            using (var conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\OMS.mdf; Integrated Security=True; MultipleActiveResultSets=True;"))
            {
                SqlCommand comm = new SqlCommand("Select Name FROM Customer", conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader["Name"].ToString());
                }
                txtCusName.AutoCompleteCustomSource = MyCollection;
            }
        }

        public static string lv1 = "";
        public static string lv11 = "";
        public static string lv12 = "";
        public static string lv22 = "";

        public static string lv52 = "";
        public static string lv62 = "";

        public static string d1 = "";
        public static string q1 = "";
        public static string r1 = "";
        public static string p1 = "";

        public static string d2 = "";
        public static string q2 = "";
        public static string r2 = "";
        public static string p2 = "";

        public static string d3 = "";
        public static string q3 = "";
        public static string r3 = "";
        public static string p3 = "";

        public static string d4 = "";
        public static string q4 = "";
        public static string r4 = "";
        public static string p4 = "";

        public static string d5 = "";
        public static string q5 = "";
        public static string r5 = "";
        public static string p5 = "";

        public static string d6 = "";
        public static string q6 = "";
        public static string r6 = "";
        public static string p6 = "";

        public static string d7 = "";
        public static string q7 = "";
        public static string r7 = "";
        public static string p7 = "";

        public static string d8 = "";
        public static string q8 = "";
        public static string r8 = "";
        public static string p8 = "";

        public int columnIndex { get; private set; }
        public int rowIndex { get; private set; }

        private void btnView_Click(object sender, EventArgs e)  //Order's ListView
        {
            
            if (radioButton1.Checked)
                lv11 = radioButton1.Text;

            else if (radioButton2.Checked)
                lv11 = radioButton2.Text;

            lv1 = label1.Text;
            lv12 = lblOId.Text;
            lv22 = txtCusName.Text;

            lv52 = lblTAmount.Text;
            lv62 = txtPAmount.Text;

            d1 = txtd1.Text;
            q1 = txtq1.Text;
            r1 = txtr1.Text;
            p1 = txtp1.Text;

            d2 = txtd2.Text;
            q2 = txtq2.Text;
            r2 = txtr2.Text;
            p2 = txtp2.Text;

            d3 = txtd3.Text;
            q3 = txtq3.Text;
            r3 = txtr3.Text;
            p3 = txtp3.Text;

            d4 = txtd4.Text;
            q4 = txtq4.Text;
            r4 = txtr4.Text;
            p4 = txtp4.Text;

            d5 = txtd5.Text;
            q5 = txtq5.Text;
            r5 = txtr5.Text;
            p5 = txtp5.Text;

            d6 = txtd6.Text;
            q6 = txtq6.Text;
            r6 = txtr6.Text;
            p6 = txtp6.Text;

            d7 = txtd7.Text;
            q7 = txtq7.Text;
            r7 = txtr7.Text;
            p7 = txtp7.Text;

            d8 = txtd8.Text;
            q8 = txtq8.Text;
            r8 = txtr8.Text;
            p8 = txtp8.Text;

            Form3 f3 = new Form3();
            f3.Visible = true;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }
        //tabPage CashMemo End


        //tabPage DailyState Start
        private void ORefreshGrid()                 //Show Product In Grid After Add, Update & Delete
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.OrViews
                    select cu;

            dataGridView3.DataSource = q;
        }
        private void ORGrid()                       //Product's DataGridView
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.GetTable<OrView>()
                    select cu;

            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.ColumnCount = 5;

            dataGridView3.Columns[0].Name = "id";
            dataGridView3.Columns[0].HeaderText = "Order No.";
            dataGridView3.Columns[0].DataPropertyName = "Id";

            dataGridView3.Columns[1].Name = "cid";
            dataGridView3.Columns[1].HeaderText = "Customer Name";
            dataGridView3.Columns[1].DataPropertyName = "Name";

            dataGridView3.Columns[2].Name = "amount";
            dataGridView3.Columns[2].HeaderText = "Amount";
            dataGridView3.Columns[2].DataPropertyName = "Amount";

            dataGridView3.Columns[3].Name = "OrderDate";
            dataGridView3.Columns[3].HeaderText = "Order Date";
            dataGridView3.Columns[3].DataPropertyName = "orderDate";

            dataGridView3.Columns[4].Name = "Payment";
            dataGridView3.Columns[4].HeaderText = "Payment";
            dataGridView3.Columns[4].DataPropertyName = "payment";

            dataGridView3.DataSource = q;
        }
        //tabPage DailyState End


        //tabPage Cheque Start
        private void CRefreshGrid()                 //Show Cheque In Grid After Add
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.Cheques
                    select cu;

            dataGridView4.DataSource = q;
        }
        //tabPage Cheque End


        //tabPage Ledger Start
        private void LGrid() {
            AllDataContext dc = new AllDataContext();
            var CustomerName = from l in dc.Customers
                               select new { l.Id, l.Name};
            listBox4.DataSource = CustomerName;
            listBox4.DisplayMember = "Name";
            listBox4.ValueMember = "Id";
        }
        private void LRefreshGrid()                 //Show Ledger In Grid After Adding Order 
        {
            AllDataContext dc = new AllDataContext();
            var q = from cu in dc.LegCusOrds
                    select cu;

            dataGridView5.DataSource = q;

            var CustomerName = from l in dc.Ledgers
                               select new { l.Customer.Id, l.Customer.Name };
            listBox4.DataSource = CustomerName;
            listBox4.DisplayMember = "Name";
            listBox4.ValueMember = "Id";
        }

        private void listBox4_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox4.SelectedIndex != -1)
                {
                    String itemSelected = listBox4.GetItemText(listBox4.SelectedItem);
                    AllDataContext dc = new AllDataContext();
                    var p = from l in dc.LegCusOrds
                            where l.Name == itemSelected
                            select l;
                    dataGridView5.DataSource = p;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Record Found!", ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
        //tabPage Ledger End   
    }
}
