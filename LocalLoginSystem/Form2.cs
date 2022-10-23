using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LocalLoginSystem
{
    public partial class TextEditor : Form
    {

        /// <summary>
        /// Is file saved
        /// </summary>
        private bool mIsSaved = true;
        private string role = "anonymous";
        private readonly List<string> UserDetail = new List<string>();

        /// <summary>
        /// The name of the opened file
        /// </summary>
        private string mCurrentFile = string.Empty;

        public TextEditor(List<string> userDetail)
        {
            InitializeComponent();
            this.role = userDetail[1];
            this.UserDetail = userDetail;
            this.Text = "Untitled - Text Editor (Viewed)";
            richTextBox1.ReadOnly = true;
            if (this.role == "Editors")
            {
                richTextBox1.ReadOnly = false;
                this.Text = "Untitled - Text Editor (Editor)";
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the rich textbox has text...
            if (richTextBox1.Text != string.Empty)
            {
                // If the user agrees to losing content...
                if (((DialogResult.OK == MessageBox.Show("The content will be lost.", "Continue?", MessageBoxButtons.OKCancel))))
                {
                    // set current file to none
                    mCurrentFile = string.Empty;

                    // clear the text of rich textbox
                    richTextBox1.Text = string.Empty;
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            mIsSaved = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mIsSaved == false)
            {
                if (MessageBox.Show("Changes were not saved. Exit?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Success logout..");
            this.Hide();
            LoginPage form1 = new LoginPage();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.Show();
        }

        private void iamHereToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                // If the user selected a file...
                if (DialogResult.OK == openFileDialog.ShowDialog())
                {
                    // get the name of the selected file
                    mCurrentFile = openFileDialog.FileName;

                    // If the file is of rich text format...
                    //if (Path.GetExtension(mCurrentFile) == ".rtf")
                    // load the file's content into the rich textbox as rich text format
                    //    richTextBox1.LoadFile(mCurrentFile);
                    // If the file of plain text type...
                    //else
                    // load the file's content into the rich textbox as plain text
                    richTextBox1.LoadFile(mCurrentFile, RichTextBoxStreamType.PlainText);

                    // Add the file name to the window title
                    this.Text = Path.GetFileName(mCurrentFile) + " - Text Editor";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // If the rich textbox has text...
            if (richTextBox1.Text != string.Empty)
            {
                // If the user agrees to losing content...
                if (((DialogResult.OK == MessageBox.Show("The content will be lost.", "Continue?", MessageBoxButtons.OKCancel))))
                {
                    // set current file to none
                    mCurrentFile = string.Empty;

                    // clear the text of rich textbox
                    richTextBox1.Text = string.Empty;
                    this.Text = "Untitled Document";
                }
            }
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            mIsSaved = true;

            if (mCurrentFile == "")
                saveAsToolStripMenuItem_Click(sender, e);
            else
                richTextBox1.SaveFile(mCurrentFile, RichTextBoxStreamType.PlainText);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mCurrentFile == "")
                saveFileDialog.FileName = "Untitled";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt" || Path.GetExtension(saveFileDialog.FileName) == ".cs")
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                else
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);

                mCurrentFile = saveFileDialog.FileName;

                this.Text = Path.GetFileName(mCurrentFile) + " - Text Editor";
            }
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (mCurrentFile == "")
                saveFileDialog.FileName = "Untitled";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                if (Path.GetExtension(saveFileDialog.FileName) == ".txt" || Path.GetExtension(saveFileDialog.FileName) == ".cs")
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                else
                    richTextBox1.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);

                mCurrentFile = saveFileDialog.FileName;

                this.Text = Path.GetFileName(mCurrentFile) + " - Text Editor";
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = richTextBox1.SelectedText;
            if (selected != String.Empty)
            {
                Clipboard.SetText(selected);
                richTextBox1.SelectedText = String.Empty;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selected = richTextBox1.SelectedText;
            if (selected != String.Empty)
            {
                Clipboard.SetText(selected);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string xx = Clipboard.GetText();
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, xx);
        }

        private void detailUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox2 aboutBox1 = new AboutBox2(this.UserDetail);
            aboutBox1.Show();
        }
    }
}
