using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TennisScoreApp
{
    public class NewGameForm : Form
    {
        public string FirstPlayerName { get; private set; }
        public int FirstPlayerPoints { get; private set; }
        public string FirstPlayerCountry { get; private set; }
        public string SecondPlayerName { get; private set; }
        public int SecondPlayerPoints { get; private set; }
        public string SecondPlayerCountry { get; private set; }

        private TextBox textBoxFirstPlayer;
        private TextBox textBoxSecondPlayer;
        private NumericUpDown numericUpDownFirst;
        private NumericUpDown numericUpDownSecond;
        private ComboBox comboBoxFirstCountry;
        private ComboBox comboBoxSecondCountry;
        private Button buttonSave;

        private Dictionary<string, Player> existingPlayers;

        public NewGameForm(Dictionary<string, Player> existingPlayers)
        {
            this.existingPlayers = existingPlayers;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "New Game";
            this.Size = new Size(340, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MinimumSize = new Size(340, 320);
            this.MaximumSize = new Size(340, 320);
            this.MaximizeBox = false;

            Label labelTitle = new Label
            {
                Text = "New Game",
                Font = new Font("Segoe UI", 14),
                Location = new Point(100, 10),
                AutoSize = true
            };

            // First Player Name
            Label labelFirstPlayer = new Label
            {
                Text = "First Player:",
                Location = new Point(20, 50)
            };
            textBoxFirstPlayer = new TextBox
            {
                Location = new Point(120, 50),
                Width = 130
            };
            textBoxFirstPlayer.TextChanged += TextBoxFirstPlayer_TextChanged;

            // First Player Points
            Label labelFirstPoints = new Label
            {
                Text = "Points:",
                Location = new Point(20, 80)
            };
            numericUpDownFirst = new NumericUpDown
            {
                Location = new Point(120, 80),
                Width = 50,
                Minimum = 0,
                Maximum = 100
            };

            // First Player Country
            Label labelFirstCountry = new Label
            {
                Text = "Country:",
                Location = new Point(20, 110)
            };
            comboBoxFirstCountry = new ComboBox
            {
                Location = new Point(120, 110),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed
            };
            comboBoxFirstCountry.DrawItem += ComboBox_DrawItem;
            comboBoxFirstCountry.DataSource = CountryData.GetCountries();
            comboBoxFirstCountry.DisplayMember = "Code";

            // Second Player Name
            Label labelSecondPlayer = new Label
            {
                Text = "Second Player:",
                Location = new Point(20, 150)
            };
            textBoxSecondPlayer = new TextBox
            {
                Location = new Point(120, 150),
                Width = 130
            };
            textBoxSecondPlayer.TextChanged += TextBoxSecondPlayer_TextChanged;

            // Second Player Points
            Label labelSecondPoints = new Label
            {
                Text = "Points:",
                Location = new Point(20, 180)
            };
            numericUpDownSecond = new NumericUpDown
            {
                Location = new Point(120, 180),
                Width = 50,
                Minimum = 0,
                Maximum = 100
            };

            // Second Player Country
            Label labelSecondCountry = new Label
            {
                Text = "Country:",
                Location = new Point(20, 210)
            };
            comboBoxSecondCountry = new ComboBox
            {
                Location = new Point(120, 210),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DrawMode = DrawMode.OwnerDrawFixed
            };
            comboBoxSecondCountry.DrawItem += ComboBox_DrawItem;
            comboBoxSecondCountry.DataSource = CountryData.GetCountries().ToList();
            comboBoxSecondCountry.DisplayMember = "Code";

            buttonSave = new Button
            {
                Text = "Save",
                Location = new Point(100, 250)
            };
            buttonSave.Click += ButtonSave_Click;

            this.Controls.Add(labelTitle);
            this.Controls.Add(labelFirstPlayer);
            this.Controls.Add(textBoxFirstPlayer);
            this.Controls.Add(labelSecondPlayer);
            this.Controls.Add(textBoxSecondPlayer);
            this.Controls.Add(labelFirstPoints);
            this.Controls.Add(numericUpDownFirst);
            this.Controls.Add(labelSecondPoints);
            this.Controls.Add(numericUpDownSecond);
            this.Controls.Add(labelFirstCountry);
            this.Controls.Add(comboBoxFirstCountry);
            this.Controls.Add(labelSecondCountry);
            this.Controls.Add(comboBoxSecondCountry);
            this.Controls.Add(buttonSave);
        }

        private void TextBoxFirstPlayer_TextChanged(object sender, EventArgs e)
        {
            string name = textBoxFirstPlayer.Text.Trim();
            comboBoxFirstCountry.Visible = !existingPlayers.ContainsKey(name);
        }

        private void TextBoxSecondPlayer_TextChanged(object sender, EventArgs e)
        {
            string name = textBoxSecondPlayer.Text.Trim();
            comboBoxSecondCountry.Visible = !existingPlayers.ContainsKey(name);
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox cb = sender as ComboBox;
            CountryInfo country = (CountryInfo)cb.Items[e.Index];

            e.DrawBackground();

            if (country.Flag != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(country.Flag, e.Bounds.Left + 2, e.Bounds.Top + 2, 22 - 5, 16 - 5);
            }

            e.Graphics.DrawString($"{country.Code}, {country.Name}", e.Font, SystemBrushes.ControlText, e.Bounds.Left + 20, e.Bounds.Top + 2);
            e.DrawFocusRectangle();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (CheckIfInputsAreValid())
            {
                FirstPlayerName = textBoxFirstPlayer.Text.Trim();
                SecondPlayerName = textBoxSecondPlayer.Text.Trim();
                FirstPlayerPoints = (int)numericUpDownFirst.Value;
                SecondPlayerPoints = (int)numericUpDownSecond.Value;
                FirstPlayerCountry = comboBoxFirstCountry.Visible ? ((CountryInfo)comboBoxFirstCountry.SelectedItem).Code : string.Empty;
                SecondPlayerCountry = comboBoxSecondCountry.Visible ? ((CountryInfo)comboBoxSecondCountry.SelectedItem).Code : string.Empty;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool CheckIfInputsAreValid()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstPlayer.Text) ||
                string.IsNullOrWhiteSpace(textBoxSecondPlayer.Text))
            {
                MessageBox.Show("Player names cannot be empty.", "Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (textBoxFirstPlayer.Text.Trim().Equals(textBoxSecondPlayer.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Player names must be different.", "Error!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
