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
    public partial class FixbugForm : Form
    {
        FixbugClass fbc = new FixbugClass();
        ProjectClass pc = new ProjectClass();
        BugClass bc = new BugClass();
        public FixbugForm()
        {
            InitializeComponent();
            combProject.DataSource = pc.getallproject();
            combProject.DisplayMember = "projectname";
            combProject.ValueMember = "projectid";

            comboBox1.DataSource = bc.getallbug();
            comboBox1.DisplayMember = "bug";
            comboBox1.ValueMember = "bugid";
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {   
            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            pictureBoxErrorSnapshot.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ImageData = new byte[ms1.Length];
            ms1.Read(ImageData, 0, ImageData.Length);

            bool res = fbc.ManageFixbug(1, dtpDate.Text, combProject.Text, comboBox1.Text, txtClass.Text, txtRichCode.Text, txtMethod.Text, comboSolved.Text, Int32.Parse(txtLinenumber.Text), txtClassLibrary.Text, ImageData, 2);
            if (res == true)
            {
                MessageBox.Show("Successfully updated the solutions!!", "New bug", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
