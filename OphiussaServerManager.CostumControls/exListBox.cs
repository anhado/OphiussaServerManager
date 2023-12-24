using System.Drawing;
using System.Windows.Forms;

namespace testexListBox {
    public class exListBoxItem {
        public exListBoxItem(int id, string title, string details) {
            Id      = id;
            Title   = title;
            Details = details;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }


        public void drawItem(DrawItemEventArgs e,         Padding margin,
                             Font              titleFont, Font    detailsFont, StringFormat aligment) {
            // if selected, mark the background differently
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            else
                e.Graphics.FillRectangle(Brushes.Beige, e.Bounds);

            // draw some item separator
            e.Graphics.DrawLine(Pens.DarkGray, e.Bounds.X, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y);

            // calculate bounds for title text drawing
            var titleBounds = new Rectangle(e.Bounds.X                 + margin.Horizontal,
                                            e.Bounds.Y                 + margin.Top,
                                            e.Bounds.Width             - margin.Right - margin.Horizontal,
                                            (int)titleFont.GetHeight() + 2);

            // calculate bounds for details text drawing
            var detailBounds = new Rectangle(e.Bounds.X      + margin.Horizontal,
                                             e.Bounds.Y      + (int)titleFont.GetHeight() + 2 + margin.Vertical + margin.Top,
                                             e.Bounds.Width  - margin.Right               - margin.Horizontal,
                                             e.Bounds.Height - margin.Bottom              - (int)titleFont.GetHeight() - 2 - margin.Vertical - margin.Top);

            // draw the text within the bounds
            e.Graphics.DrawString(Title,   titleFont,   Brushes.Black,    titleBounds,  aligment);
            e.Graphics.DrawString(Details, detailsFont, Brushes.DarkGray, detailBounds, aligment);

            // put some focus rectangle
            e.DrawFocusRectangle();
        }
    }

    public partial class exListBox : ListBox {
        private readonly Font _detailsFont;

        private readonly StringFormat _fmt;
        private readonly Font         _titleFont;

        public exListBox(Font            titleFont, Font            detailsFont,
                         StringAlignment aligment,  StringAlignment lineAligment) {
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
            _fmt               = new StringFormat();
            _fmt.Alignment     = aligment;
            _fmt.LineAlignment = lineAligment;
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
        }

        public exListBox() {
            InitializeComponent();
            _fmt               = new StringFormat();
            _fmt.Alignment     = StringAlignment.Near;
            _fmt.LineAlignment = StringAlignment.Near;
            _titleFont         = new Font(Font, FontStyle.Bold);
            _detailsFont       = new Font(Font, FontStyle.Regular);
        }


        protected override void OnDrawItem(DrawItemEventArgs e) {
            // prevent from error Visual Designer
            if (Items.Count > 0) {
                var item = (exListBoxItem)Items[e.Index];
                item.drawItem(e, Margin, _titleFont, _detailsFont, _fmt);
            }
        }


        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}