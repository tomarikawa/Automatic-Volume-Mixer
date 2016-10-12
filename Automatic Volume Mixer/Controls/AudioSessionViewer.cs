﻿using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Avm.Daemon;

namespace Avm.Controls
{
    public partial class AudioSessionViewer : UserControl
    {
        public AudioSessionViewer()
        {
            InitializeComponent();
        }

        public void RefreshSessions(StateUpdateEventArgs stateUpdateEventArgs)
        {
            var query = from session in stateUpdateEventArgs.Sessions
                orderby session.IsSystemSoundSession descending, session.DisplayName ascending
                select new ListViewItem(new[]
                {
                    session.DisplayName,
                    session.PeakValue.ToString(CultureInfo.CurrentCulture),
                    session.MasterVolume.ToString(CultureInfo.CurrentCulture),
                    session.IsMuted.ToString()
                })
                {Tag = session};

            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.Items.AddRange(query.ToArray());
            listView1.EndUpdate();
        }
    }
}