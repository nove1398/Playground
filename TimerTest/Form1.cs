using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TimerTest
{
    public partial class Form1 : Form
    {
        private List<string> StatusList = new List<string>() { "Waiting" };
        private readonly ILogger<Form1> _logger;

        public Form1(ILogger<Form1> logger)
        {
            _logger = logger;

            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void StartServiceButton_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("Pressed");
            statusListBox.DataSource = StatusList;
        }
    }
}