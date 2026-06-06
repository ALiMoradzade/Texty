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
    public partial class SolarHijriToGregorianForm : Form
    {
        public SolarHijriToGregorianForm()
        {
            InitializeComponent();
        }

        private bool IsAnyEmpty()
        {
            bool isYearEmpty = string.IsNullOrEmpty(textBoxSolarHijriYear.Text);
            bool isMonthEmpty = string.IsNullOrEmpty(textBoxSolarHijriMonth.Text);
            bool isDayEmpty = string.IsNullOrEmpty(textBoxSolarHijriDay.Text);
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
            PersianDate persianDate = DateConverter.GetPersianDate(now);

            textBoxSolarHijriYear.Text = persianDate.Year.ToString("0000");
            textBoxSolarHijriMonth.Text = persianDate.Month.ToString("00");
            textBoxSolarHijriDay.Text = persianDate.Day.ToString("00");
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (IsAnyEmpty())
            {
                MessageBoxCannotBeEmpty();
                return;
            }

            int year = int.Parse(textBoxSolarHijriYear.Text);
            int month = int.Parse(textBoxSolarHijriMonth.Text);
            int day = int.Parse(textBoxSolarHijriDay.Text);

            PersianDate persianDate = new PersianDate(year, month, day, 0, 0, 0, 0);
            DateTime gregorianDate = DateConverter.GetGregorianDate(persianDate);

            textBoxGregorianYear.Text = gregorianDate.Year.ToString("0000");
            textBoxGregorianMonth.Text = gregorianDate.Month.ToString("00");
            textBoxGregorianDay.Text = gregorianDate.Day.ToString("00");
        }

        private void copyDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string date = $"{textBoxGregorianYear.Text}/{textBoxGregorianMonth.Text}/{textBoxGregorianDay.Text}";
            ClipboardManager.CopyToClipboard(date);
        }

        private void textBoxSolarHijriDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') return;
            else if (!Regex.IsMatch(e.KeyChar.ToString(), "[0-9]{1}"))
            {
                e.Handled = true;
            }
        }
    }
}
