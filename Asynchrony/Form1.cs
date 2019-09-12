using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asynchrony
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // One thread Synchronization Context
            int before = Thread.CurrentThread.ManagedThreadId;
            var context = SynchronizationContext.Current;

            var delay = Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            await delay;
            
            int after = Thread.CurrentThread.ManagedThreadId;
            label1.Text = "Some Text";

        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            await DelayAsync();
        }

        private async Task DelayAsync()
        {
            await Task.Delay(2000);
            //var configuredTaskAwaitable = Task.Delay(2000).ConfigureAwait(false);
            //await configuredTaskAwaitable;
            int after = Thread.CurrentThread.ManagedThreadId;
            label1.Text = "Some Text";
        }
    }
}
