using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard_Manager;

namespace Texty.Date_Converter
{
    public partial class GregorianToSolarHijriForm : Form
    {
        public GregorianToSolarHijriForm()
        {
            InitializeComponent();
        }
        private bool IsAnyEmpty()
        {
            bool isYearEmpty = string.IsNullOrEmpty(textBoxGregorianYear.Text);
            bool isMonthEmpty = string.IsNullOrEmpty(textBoxGregorianMonth.Text);
            bool isDayEmpty = string.IsNullOrEmpty(textBoxGregorianDay.Text);
            return isYearEmpty || isMonthEmpty || isDayEmpty;
        }

        private DialogResult MessageBoxCannotBeEmpty()
        {
            var r = MessageBox.Show("Please enter Year, Month and Day",
                                    "Invalid Date",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            return r;
        }

        private void buttonNowDateTime_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
           
            textBoxGregorianYear.Text = now.Year.ToString("0000");
            textBoxGregorianMonth.Text = now.Month.ToString("00");
            textBoxGregorianDay.Text = now.Day.ToString("00");
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (IsAnyEmpty())
            {
                MessageBoxCannotBeEmpty();
                return;
            }

            int year = int.Parse(textBoxGregorianYear.Text);
            int month = int.Parse(textBoxGregorianMonth.Text);
            int day = int.Parse(textBoxGregorianDay.Text);

            DateTime dateTime = new DateTime(year, month, day);
            PersianDate persianDate = DateConverter.GetPersianDate(dateTime);

            textBoxSolarHijriYear.Text = persianDate.Year.ToString("0000");
            textBoxSolarHijriMonth.Text = persianDate.Month.ToString("00");
            textBoxSolarHijriDay.Text = persianDate.Day.ToString("00");
        }

        private void textBoxGregorianYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') return;
            else if (!Regex.IsMatch(e.KeyChar.ToString(), "[0-9]{1}"))
            {
                e.Handled = true;
            }
        }

        private void copyDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string date = $"{textBoxSolarHijriYear.Text}/{textBoxSolarHijriMonth.Text}/{textBoxSolarHijriDay.Text}";
            ClipboardManager.CopyToClipboard(date);
        }
    }
}
