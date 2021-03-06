﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccessClassLibrary;
using System.IO;

namespace Stinger_Bug_Tracker
{
    public partial class AddbugForm : Form
    {
        BugClass bc = new BugClass();
        ProjectClass pc = new ProjectClass();
        public AddbugForm()
        {
            InitializeComponent();
            combProject.DataSource = pc.getallproject();
            combProject.DisplayMember = "projectname";
            combProject.ValueMember = "projectid";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MemoryStream ms1 =new MemoryStream();
            MemoryStream ms2 =new MemoryStream();
            pictureBoxErrorSnapshot.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
            byte[] ImageData = new byte[ms1.Length];
            ms1.Read(ImageData, 0, ImageData.Length);

            bool res = bc.ManageBugs(dtpDate.Text, combProject.Text, txtBug.Text, txtClass.Text, txtRichCode.Text, txtMethod.Text, comboIdentify.Text, Int32.Parse(txtLinenumber.Text), txtClassLibrary.Text, ImageData, 1);
            if (res == true)
            {
                MessageBox.Show("Successfully Added the bug!!", "New bug", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add new bug, please enter the correct values.", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image files | *.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //txtStudentImage.Text = openFileDialog1.FileName;
                pictureBoxErrorSnapshot.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
