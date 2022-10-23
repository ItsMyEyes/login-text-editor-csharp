using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private bool mBold = false;
        private bool mItalic = false;
        private bool mUnderline = false;

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
            toolStripLabel1.Text = String.Format("Hello {0} {1}", this.UserDetail[2], this.UserDetail[3]);
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
            AboutTextEditor aboutBox1 = new AboutTextEditor();
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
            AboutMe aboutBox1 = new AboutMe(this.UserDetail);
            aboutBox1.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
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

        private void toolStripButton2_Click(object sender, EventArgs e)
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mIsSaved = true;

            if (mCurrentFile == "")
                saveAsToolStripMenuItem_Click(sender, e);
            else
                richTextBox1.SaveFile(mCurrentFile, RichTextBoxStreamType.PlainText);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
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

        private void BoldSelectedText(RichTextBox control)
        {
            // Remember selection
            int selstart = control.SelectionStart;
            int sellength = control.SelectionLength;

            // Set font of selected text
            // You can use FontStyle.Bold | FontStyle.Italic to apply more than one style
            control.SelectionFont = new Font(control.Font, FontStyle.Bold);

            // Set cursor after selected text
            control.SelectionStart = control.SelectionStart + control.SelectionLength;
            control.SelectionLength = 0;
            // Set font immediately after selection
            control.SelectionFont = control.Font;

            // Reselect previous text
            control.Select(selstart, sellength);
        }

        private void ItalicSelectedText(RichTextBox control)
        {
            // Remember selection
            int selstart = control.SelectionStart;
            int sellength = control.SelectionLength;

            // Set font of selected text
            // You can use FontStyle.Bold | FontStyle.Italic to apply more than one style
            control.SelectionFont = new Font(control.Font, FontStyle.Italic);

            // Set cursor after selected text
            control.SelectionStart = control.SelectionStart + control.SelectionLength;
            control.SelectionLength = 0;
            // Set font immediately after selection
            control.SelectionFont = control.Font;

            // Reselect previous text
            control.Select(selstart, sellength);
        }

        private void UnderlineSelectedText(RichTextBox control)
        {
            // Remember selection
            int selstart = control.SelectionStart;
            int sellength = control.SelectionLength;

            // Set font of selected text
            // You can use FontStyle.Bold | FontStyle.Italic to apply more than one style
            control.SelectionFont = new Font(control.Font, FontStyle.Underline);

            // Set cursor after selected text
            control.SelectionStart = control.SelectionStart + control.SelectionLength;
            control.SelectionLength = 0;
            // Set font immediately after selection
            control.SelectionFont = control.Font;

            // Reselect previous text
            control.Select(selstart, sellength);
        }

        private void NormalSelectedText(RichTextBox control)
        {
            // Remember selection
            int selstart = control.SelectionStart;
            int sellength = control.SelectionLength;

            // Set font of selected text
            // You can use FontStyle.Bold | FontStyle.Italic to apply more than one style
            control.SelectionFont = new Font(control.Font, FontStyle.Underline);

            // Set cursor after selected text
            control.SelectionStart = control.SelectionStart + control.SelectionLength;
            control.SelectionLength = 0;
            // Set font immediately after selection
            control.SelectionFont = control.Font;

            // Reselect previous text
            control.Select(selstart, sellength);
        }

        private void ChangeSizeSelectedText(RichTextBox control, int size)
        {
            // Remember selection
            //int selstart = control.SelectionStart;
            //int sellength = control.SelectionLength;

            // Set font of selected text
            // You can use FontStyle.Bold | FontStyle.Italic to apply more than one style
            control.Font = new Font(control.Font.ToString(), size, FontStyle.Regular);

            // Set cursor after selected text
            //control.SelectionStart = control.SelectionStart + control.SelectionLength;
            //control.SelectionLength = 0;
            // Set font immediately after selection
            control.SelectionFont = control.Font;

            // Reselect previous text
            //control.Select(selstart, sellength);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!mBold)
            {
                BoldSelectedText(richTextBox1);
                toolStripButton5.BackColor = Color.LightSteelBlue;
                mBold = true;
            } else
            {
                NormalSelectedText(richTextBox1);
                toolStripButton5.BackColor = Color.GhostWhite;
                mBold = false;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (!mItalic)
            {
                toolStripButton6.BackColor = Color.LightSteelBlue;
                ItalicSelectedText(richTextBox1);
                mItalic = true;
            }
            else
            {
                NormalSelectedText(richTextBox1);
                toolStripButton6.BackColor = Color.GhostWhite;
                mItalic = false;
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!mUnderline)
            {
                toolStripButton7.BackColor = Color.LightSteelBlue;
                UnderlineSelectedText(richTextBox1);
                mUnderline = true;
            }
            else
            {
                NormalSelectedText(richTextBox1);
                toolStripButton7.BackColor = Color.GhostWhite;
                mUnderline = false;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int size = int.Parse(toolStripComboBox1.Text);
            ChangeSizeSelectedText(richTextBox1, size);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            toolStripLabel1.Text = String.Format("Hello {0} {1}", this.UserDetail[2], this.UserDetail[3]);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            AboutTextEditor aboutBox1 = new AboutTextEditor();
            aboutBox1.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            string selected = richTextBox1.SelectedText;
            if (selected != String.Empty)
            {
                Clipboard.SetText(selected);
                richTextBox1.SelectedText = String.Empty;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            string selected = richTextBox1.SelectedText;
            if (selected != String.Empty)
            {
                Clipboard.SetText(selected);
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            string xx = Clipboard.GetText();
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, xx);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.S))
            {
                saveToolStripMenuItem_Click_1(sender, e);
            }

            if (e.KeyData == (Keys.Control | Keys.O))
            {
                openToolStripMenuItem_Click_1(sender, e);
            }

            if (e.KeyData == (Keys.Control | Keys.N))
            {
                newToolStripMenuItem_Click(sender, e);
            }
        }
    }
}
