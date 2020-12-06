﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ModChanger
{

    
    public partial class Form2 : Form
    {
        string modPath;
        string[] GetFiles;
        public string gamePath;

        public Form2(string txt)
        {
            InitializeComponent();
            gamePath = txt;
            radioButton2.Checked = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;

                if (System.IO.Directory.Exists(path + @"\Bin32") || System.IO.Directory.Exists(path + @"\BIO4"))
                {
                    textBox1.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    MessageBox.Show("No mod has been identified. Make sure that the mod folder contains the BIO4 and Bin32 folders within its directory.");
                }

            }
            
        }

        public void btnConfirm_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Please, choose the mod directory.", "Error", MessageBoxButtons.OK);
            }
            else if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Please, give the mod a name.", "Error", MessageBoxButtons.OK);
            }
            else
            {
                modPath = textBox1.Text;

                GetFiles = Directory.GetFiles(modPath, "*.*", SearchOption.AllDirectories);


                string[] lines = {
                    "[SETTINGS]",
                    "path="+textBox1.Text,
                    "name="+textBox2.Text,
                    "diff="+comboBox1.SelectedItem,
                    ""
                };


                int modLength = textBox1.Text.Length + 1;

                System.IO.File.WriteAllLines(modPath + @"\config.cfg", lines);

                TextWriter tw1 = new StreamWriter(modPath + @"\config.cfg", true);
                List<string> filenames = new List<string>();
                tw1.WriteLine("[FILES]");
                foreach (string filename in GetFiles)
                {
                    tw1.WriteLine("file=" + filename.Remove(0,modLength));
                }
                tw1.Close();


                File.AppendAllText(@"settings.cfg", "mod=" + textBox2.Text + "|" + textBox1.Text + "\n");

                MessageBox.Show("The mod has been installed!", "Success", MessageBoxButtons.OK);
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                comboBox1.Enabled = false;
                comboBox1.SelectedItem = null;
            }
            else
            {
                comboBox1.Enabled = true;
            }
        }
    }
}