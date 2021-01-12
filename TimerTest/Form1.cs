using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerTest
{
    public partial class Form1 : Form
    {
        private List<string> StatusList = new List<string>() { "Waiting" };
        private RadioButton[] ServiceStates = new RadioButton[3];

        private readonly ILogger<Form1> _logger;
        private readonly IMediator _mediator;

        public Form1(ILogger<Form1> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

            InitializeComponent();
            ServiceStates[0] = serviceStoppedRadio;
            ServiceStates[1] = serviceStartingRadio;
            ServiceStates[2] = serviceRunningRadio;
        }

        private void ClearServiceStatus()
        {
            foreach (var item in ServiceStates)
            {
                item.Checked = false;
            }
        }

        private void SetActiveState(int index)
        {
            ServiceStates[index].Checked = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void StartServiceButton_Click(object sender, EventArgs e)
        {
            _logger.LogInformation("Pressed");
            _mediator.Publish(new ServiceController
            {
                Message = "I am a ping",
                ActionToBeTaken = ServiceController.Action.Start
            });
            statusListBox.DataSource = StatusList;
        }

        private void StopServiceButton_Click(object sender, EventArgs e)
        {
            _mediator.Publish(new ServiceController
            {
                Message = "Pong",
                ActionToBeTaken = ServiceController.Action.Stop
            });
        }

        private void AddNewLog(string logMessage)
        {
            StatusList.Add(logMessage);
        }

        private void statusButton_Click(object sender, EventArgs e)
        {
            _mediator.Publish(new ServiceController
            {
                Message = "Pong",
                ActionToBeTaken = ServiceController.Action.Status
            });
        }
    }
}