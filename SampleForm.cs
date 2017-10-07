using System;
using System.Windows.Forms;
using NeonScripting;

namespace GeneralPluginSample
{
    public partial class SampleForm : Form
    {
        private int _currentId;
        public INeonScriptHost ScriptHost { get; set; }
        public SampleForm()
        {
            InitializeComponent();
        }

        public void Populate()
        {
            listBoxPq.Items.Clear();
            foreach (var item in ScriptHost.RemoteCalls.TracksInPlayQueue())
            {
                listBoxPq.Items.Add($"{item.Artist} - {item.Title}");
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            ScriptHost.RemoteCalls.Previous();
            Populate();
        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            ScriptHost.RemoteCalls.Pause();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            ScriptHost.RemoteCalls.Pause();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            ScriptHost.RemoteCalls.Stop();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            ScriptHost.RemoteCalls.Next();
            Populate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ScriptHost.RemoteCalls.PlayerState != NeonPlayerState.Stopped)
            {
                var track = ScriptHost.RemoteCalls.ActiveTrack;
                if (track != null)
                {
                    var id = track.DetailId;
                    if (id != _currentId)
                    {
                        _currentId = id;
                        NowPlayingLabel.Text = $"{track.Artist} - {track.Title}";
                    }
                    DurationLabel.Text = $"{ScriptHost.RemoteCalls.CurrentPosition}/{ScriptHost.RemoteCalls.Duration}";
                }
            }
        }

        private void SampleForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void listBoxPq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ScriptHost.RemoteCalls.Play(listBoxPq.SelectedIndex);
            Populate();
        }
    }
}
