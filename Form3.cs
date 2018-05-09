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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public void LstView()
        {
            if (!Form2.d1.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d1);

                items.SubItems.Add(Form2.q1);
                items.SubItems.Add(Form2.r1);
                items.SubItems.Add(Form2.p1);
                listView1.Items.Add(items);
            }
            if (!Form2.d2.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d2);

                items.SubItems.Add(Form2.q2);
                items.SubItems.Add(Form2.r2);
                items.SubItems.Add(Form2.p2);
                listView1.Items.Add(items);
            }
            if (!Form2.d3.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d3);

                items.SubItems.Add(Form2.q3);
                items.SubItems.Add(Form2.r3);
                items.SubItems.Add(Form2.p3);
                listView1.Items.Add(items);
            }
            if (!Form2.d4.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d4);

                items.SubItems.Add(Form2.q4);
                items.SubItems.Add(Form2.r4);
                items.SubItems.Add(Form2.p4);
                listView1.Items.Add(items);
            }
            if (!Form2.d5.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d5);

                items.SubItems.Add(Form2.q5);
                items.SubItems.Add(Form2.r5);
                items.SubItems.Add(Form2.p5);
                listView1.Items.Add(items);
            }
            if (!Form2.d6.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d6);

                items.SubItems.Add(Form2.q6);
                items.SubItems.Add(Form2.r6);
                items.SubItems.Add(Form2.p6);
                listView1.Items.Add(items);
            }
            if (!Form2.d7.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d7);

                items.SubItems.Add(Form2.q7);
                items.SubItems.Add(Form2.r7);
                items.SubItems.Add(Form2.p7);
                listView1.Items.Add(items);
            }
            if (!Form2.d8.Equals(""))
            {
                ListViewItem items = new ListViewItem(Form2.d8);

                items.SubItems.Add(Form2.q8);
                items.SubItems.Add(Form2.r8);
                items.SubItems.Add(Form2.p8);
                listView1.Items.Add(items);
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font F = listView1.Font;
            Font FL = lblCompany.Font;
            Font FC = lblCName.Font;
            Font FF = lblFont.Font;
            Brush B = new SolidBrush(listView1.ForeColor);
            e.Graphics.DrawString(lblCompany.Text, FL, B, 240, 40);
            e.Graphics.DrawString("Receipt No." + " : " + Form2.lv12, F, B, 150, 20);
            e.Graphics.DrawString(Form2.lv1, F, B, 550, 20);
            e.Graphics.DrawString("Customer Name" + " : " + Form2.lv22, FC, B, 150, 110);
            e.Graphics.DrawString("Paid By" + " : " + Form2.lv11, FC, B, 217, 130);
            e.Graphics.DrawString("Detail", FF, B, 150, 160);
            e.Graphics.DrawString("Kg", FF, B, 370, 160);
            e.Graphics.DrawString("Rate", FF, B, 490, 160);
            e.Graphics.DrawString("Price", FF, B, 600, 160);

            int[] X = { 150, 370, 490, 600 };
            int Y = 190;

            for (int I = 0; I < listView1.Items.Count; I++)
            {
                for (int J = 0; J < listView1.Items[I].SubItems.Count; J++)
                    e.Graphics.DrawString(listView1.Items[I].SubItems[J].Text, F, B, X[J], Y);
                Y += F.Height;
                Y += F.Height;
            }
            e.Graphics.DrawString("Total Amount" + " = " + Form2.lv52, FF, B, 490, 400);
            e.Graphics.DrawString("Paid Amount" + " = " + Form2.lv62, FF, B, 490, 420);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowInTaskbar = true;
            printPreviewDialog1.MinimizeBox = false;
            printPreviewDialog1.WindowState = FormWindowState.Normal;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Height = 535;
            printPreviewDialog1.Width = 900;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.Visible = false; 
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LstView();
            lblCName.Text = Form2.lv22;
        }
    }
}
