using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab4Git
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private readonly TextBox nameInput;
        public MainForm()
        {
            Text = "Лабораторная работа №4";
            ClientSize = new Size(460, 240);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 11);
            var label = new Label { Text = "Введите ваше имя:", Location = new Point(30, 30), AutoSize = true };
            nameInput = new TextBox { Location = new Point(30, 65), Width = 390 };
            var open = new Button { Text = "Открыть вторую форму", Location = new Point(30, 115), Size = new Size(260, 40) };
            open.Click += OpenSecondForm;
            Controls.AddRange(new Control[] { label, nameInput, open });
            AcceptButton = open;
        }

        private void OpenSecondForm(object sender, EventArgs e)
        {
            string name = nameInput.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Сначала введите имя.", "Проверка ввода", MessageBoxButtons.OK, MessageBoxIcon.Information);
                nameInput.Focus();
                return;
            }
            using (var second = new GreetingForm(name))
            {
                Hide();
                try { second.ShowDialog(); }
                finally { Show(); }
            }
        }
    }

    public class GreetingForm : Form
    {
        public GreetingForm(string name)
        {
            Text = "Вторая форма";
            ClientSize = new Size(460, 240);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 11);
            var greeting = new Label { Text = "Здравствуйте, " + name + "!", Location = new Point(30, 30), Size = new Size(390, 70), AutoEllipsis = true };
            var back = new Button { Text = "Назад", Location = new Point(30, 150), Size = new Size(130, 40) };
            back.Click += (sender, e) => Close();
            Controls.AddRange(new Control[] { greeting, back });
            var color = new Button
            {
                Text = "Сменить цвет",
                Location = new Point(190, 150),
                Size = new Size(200, 40),
            };
            color.Click += (sender, e) =>
            {
                BackColor = BackColor == Color.LightBlue ? SystemColors.Control : Color.LightBlue;
            };
            Controls.Add(color);
        }
    }
}
