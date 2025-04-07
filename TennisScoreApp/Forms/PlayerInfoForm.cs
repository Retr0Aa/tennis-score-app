using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TennisScoreApp.Properties;

namespace TennisScoreApp
{
    public class PlayerInfoForm : Form
    {
        private string playerName;
        private List<Game> playerGames;
        private ListView listViewVictories;
        private ListView listViewLosses;
        private ListView listViewDraws;

        public PlayerInfoForm(string playerName, List<Game> games)
        {
            this.playerName = playerName;
            this.playerGames = games;
            InitializeComponent();
            FillMatchLists();
        }

        private void InitializeComponent()
        {
            this.Text = "Player Info - " + playerName;
            this.Size = new Size(620, 450);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Icon = Resources.tennis_icon;

            this.MinimumSize = new Size(620, 450);
            this.MaximumSize = new Size(620, 450);
            this.MaximizeBox = false;

            Label labelTitle = new Label
            {
                Text = playerName,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(70, 10),
                AutoSize = true
            };

            PictureBox pictureBoxFlag = new PictureBox
            {
                Location = new Point(20, 15),
                Size = new Size(32, 20),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            var player = ScoreForm.GetPlayerByName(playerName);
            if (player != null && !string.IsNullOrEmpty(player.Country))
            {
                var country = CountryData.GetCountries().FirstOrDefault(c => c.Code.Equals(player.Country, StringComparison.OrdinalIgnoreCase));
                if (country != null)
                    pictureBoxFlag.Image = country.Flag;
            }

            Label labelVictories = new Label { Text = "Victories", Location = new Point(20, 50) };
            listViewVictories = new ListView
            {
                View = View.Details,
                Location = new Point(20, 80),
                Size = new Size(170, 300)
            };
            listViewVictories.Columns.Add("Opponent", 100);
            listViewVictories.Columns.Add("Score", 50);

            Label labelLosses = new Label { Text = "Losses", Location = new Point(220, 50) };
            listViewLosses = new ListView
            {
                View = View.Details,
                Location = new Point(220, 80),
                Size = new Size(170, 300)
            };
            listViewLosses.Columns.Add("Opponent", 100);
            listViewLosses.Columns.Add("Score", 50);

            Label labelDraws = new Label { Text = "Draws", Location = new Point(420, 50) };
            listViewDraws = new ListView
            {
                View = View.Details,
                Location = new Point(420, 80),
                Size = new Size(170, 300)
            };
            listViewDraws.Columns.Add("Opponent", 100);
            listViewDraws.Columns.Add("Score", 50);

            this.Controls.Add(pictureBoxFlag);
            this.Controls.Add(labelTitle);
            this.Controls.Add(labelVictories);
            this.Controls.Add(listViewVictories);
            this.Controls.Add(labelLosses);
            this.Controls.Add(listViewLosses);
            this.Controls.Add(labelDraws);
            this.Controls.Add(listViewDraws);
        }

        private void FillMatchLists()
        {
            listViewVictories.Items.Clear();
            listViewLosses.Items.Clear();
            listViewDraws.Items.Clear();

            foreach (var game in playerGames)
            {
                string opponent;
                string score;
                int playerScore, opponentScore;

                if (game.FirstPlayer.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                {
                    opponent = game.SecondPlayer.Name;
                    playerScore = game.FirstPlayerScore;
                    opponentScore = game.SecondPlayerScore;
                }
                else
                {
                    opponent = game.FirstPlayer.Name;
                    playerScore = game.SecondPlayerScore;
                    opponentScore = game.FirstPlayerScore;
                }
                score = $"{playerScore} - {opponentScore}";

                ListViewItem item = new ListViewItem(opponent);
                item.SubItems.Add(score);

                if (playerScore > opponentScore)
                    listViewVictories.Items.Add(item);
                else if (playerScore < opponentScore)
                    listViewLosses.Items.Add(item);
                else
                    listViewDraws.Items.Add(item);
            }
        }
    }
}
