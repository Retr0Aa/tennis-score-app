using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TennisScoreApp
{
    public class ScoreForm : Form
    {
        private Label rankingLabel;
        private Label latestGamesLabel;
        private ListView listViewRanking;
        private ListView listViewLatestGames;
        private Button buttonAddNewGame;
        private Label labelInfo;

        private static Dictionary<string, Player> players = new Dictionary<string, Player>();
        private List<Game> games = new List<Game>();

        public ScoreForm()
        {
            InitializeComponent();
        }

        public static Player GetPlayerByName(string name)
        {
            if (players.ContainsKey(name))
                return players[name];
            return null;
        }

        private void InitializeComponent()
        {
            this.Text = "Tennis Score";
            this.Size = new Size(800, 600);

            this.MinimumSize = new Size(800, 600);
            this.MaximumSize = new Size(800, 600);
            this.MaximizeBox = false;

            Label rankingLabel = new Label
            {
                Text = "Ranking",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 10),
                AutoSize = true
            };

            Label latestGamesLabel = new Label
            {
                Text = "Latest Games",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(350, 10),
                AutoSize = true
            };

            listViewRanking = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                Location = new Point(20, 50),
                Size = new Size(300, 400)
            };
            listViewRanking.Columns.Add("Player", 150);
            listViewRanking.Columns.Add("Points", 100);
            listViewRanking.DoubleClick += ListViewRanking_DoubleClick;

            listViewLatestGames = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                Location = new Point(350, 50),
                Size = new Size(400, 400)
            };
            listViewLatestGames.Columns.Add("First Player", 120);
            listViewLatestGames.Columns.Add("Second Player", 120);
            listViewLatestGames.Columns.Add("Winner", 120);
            listViewLatestGames.Columns.Add("Score", 80);

            buttonAddNewGame = new Button
            {
                Text = "Add New Game",
                Location = new Point(20, 470)
            };
            buttonAddNewGame.Click += ButtonAddNewGame_Click;

            labelInfo = new Label
            {
                Text = "*Click on a player's name to view profile",
                Location = new Point(20, 520),
                AutoSize = true
            };

            this.Controls.Add(rankingLabel);
            this.Controls.Add(latestGamesLabel);
            this.Controls.Add(listViewRanking);
            this.Controls.Add(listViewLatestGames);
            this.Controls.Add(buttonAddNewGame);
            this.Controls.Add(labelInfo);
        }

        private void RefreshViews()
        {
            FillRankingListView();
            FillLatestGamesListView();
        }

        private void FillRankingListView()
        {
            listViewRanking.Items.Clear();
            foreach (var player in players.Values.OrderByDescending(p => p.TotalPoints))
            {
                ListViewItem item = new ListViewItem(player.Name);
                item.SubItems.Add(player.TotalPoints.ToString());
                listViewRanking.Items.Add(item);
            }
        }

        private void FillLatestGamesListView()
        {
            listViewLatestGames.Items.Clear();
            foreach (var game in games.AsEnumerable().Reverse())
            {
                string winner = GetWinner(game);
                string score = $"{game.FirstPlayerScore} - {game.SecondPlayerScore}";
                ListViewItem item = new ListViewItem(game.FirstPlayer.Name);
                item.SubItems.Add(game.SecondPlayer.Name);
                item.SubItems.Add(winner);
                item.SubItems.Add(score);
                listViewLatestGames.Items.Add(item);
            }
        }

        private string GetWinner(Game game)
        {
            if (game.FirstPlayerScore > game.SecondPlayerScore)
                return game.FirstPlayer.Name;
            else if (game.SecondPlayerScore > game.FirstPlayerScore)
                return game.SecondPlayer.Name;
            else
                return "Draw";
        }

        private void ButtonAddNewGame_Click(object sender, EventArgs e)
        {
            using (NewGameForm newGameForm = new NewGameForm(players))
            {
                if (newGameForm.ShowDialog() == DialogResult.OK)
                {

                    Player firstPlayer = GetOrCreatePlayer(newGameForm.FirstPlayerName, newGameForm.FirstPlayerCountry);
                    Player secondPlayer = GetOrCreatePlayer(newGameForm.SecondPlayerName, newGameForm.SecondPlayerCountry);

                    Game game = new Game
                    {
                        FirstPlayer = firstPlayer,
                        FirstPlayerScore = newGameForm.FirstPlayerPoints,
                        SecondPlayer = secondPlayer,
                        SecondPlayerScore = newGameForm.SecondPlayerPoints
                    };
                    games.Add(game);

                    firstPlayer.TotalPoints += newGameForm.FirstPlayerPoints;
                    secondPlayer.TotalPoints += newGameForm.SecondPlayerPoints;

                    RefreshViews();
                }
            }
        }

        private Player GetOrCreatePlayer(string name, string country)
        {
            if (players.ContainsKey(name))
                return players[name];
            else
            {
                Player player = new Player { Name = name, TotalPoints = 0, Country = country };
                players[name] = player;
                return player;
            }
        }

        private void ListViewRanking_DoubleClick(object sender, EventArgs e)
        {
            if (listViewRanking.SelectedItems.Count > 0)
            {
                string playerName = listViewRanking.SelectedItems[0].Text;
                List<Game> playerGames = GetPlayerGames(playerName);
                using (PlayerInfoForm infoForm = new PlayerInfoForm(playerName, playerGames))
                {
                    infoForm.ShowDialog();
                }
            }
        }

        private List<Game> GetPlayerGames(string playerName)
        {
            return games.Where(g => g.FirstPlayer.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase)
                                  || g.SecondPlayer.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
