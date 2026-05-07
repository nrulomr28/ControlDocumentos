using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.UI
{
    public static class SigelicXPTheme
    {
        public static Color Primary = Color.FromArgb(10, 36, 106); // azul XP
        public static Color Light = Color.FromArgb(236, 233, 216); // fondo XP
        public static Color Border = Color.FromArgb(172, 168, 153);
        public static Color Button = Color.FromArgb(240, 240, 240);
        public static Color ButtonHover = Color.FromArgb(255, 255, 220);

        public static void ApplyForm(Form form)
        {
            form.BackColor = Light;
            form.Font = new Font("Tahoma", 8.25F);

            ApplyControls(form.Controls);
        }

        private static void ApplyControls(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                switch (ctrl)
                {
                    case Panel panel:
                        panel.BackColor = Light;
                        panel.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case Button btn:
                        ApplyButton(btn);
                        break;

                    case DataGridView grid:
                        ApplyGrid(grid);
                        break;

                    case TextBox txt:
                        txt.BorderStyle = BorderStyle.FixedSingle;
                        break;

                    case ComboBox cmb:
                        cmb.FlatStyle = FlatStyle.Standard;
                        break;

                    case Label lbl:
                        lbl.ForeColor = Color.Black;
                        break;
                }

                if (ctrl.HasChildren)
                    ApplyControls(ctrl.Controls);
            }
        }

        private static void ApplyButton(Button btn)
        {
            btn.BackColor = Button;
            btn.FlatStyle = FlatStyle.Standard;
            btn.Font = new Font("Tahoma", 8.25F);

            btn.MouseEnter += (s, e) => btn.BackColor = ButtonHover;
            btn.MouseLeave += (s, e) => btn.BackColor = Button;
        }

        private static void ApplyGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.GridColor = Border;

            grid.EnableHeadersVisualStyles = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 8.25F, FontStyle.Bold);

            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(49, 106, 197);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        public static void ApplyStartButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Tahoma", 8.75F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            btn.Paint += (s, e) =>
            {
                var g = e.Graphics;
                var rect = btn.ClientRectangle;

                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    rect,
                    Color.FromArgb(60, 180, 75),   // verde claro XP
                    Color.FromArgb(0, 120, 40),    // verde oscuro XP
                    90f))
                {
                    g.FillRectangle(brush, rect);
                }

                using (var pen = new Pen(Color.FromArgb(0, 80, 30)))
                {
                    g.DrawRectangle(pen, 0, 0, rect.Width - 1, rect.Height - 1);
                }

                TextRenderer.DrawText(
                    g,
                    btn.Text,
                    btn.Font,
                    rect,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            // Hover
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = Color.FromArgb(80, 200, 90);
                btn.Invalidate();
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Color.Transparent;
                btn.Invalidate();
            };
        }
    }
}
