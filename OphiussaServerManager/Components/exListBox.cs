using System.Drawing;
using System.Windows.Forms;

namespace OphiussaServerManager {
    public class ExListBoxItem {
        public ExListBoxItem(string id, string title, string details, object pObject) {
            Id               = id;
            Title            = title;
            Details          = details;
            AssociatedObject = pObject;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public object AssociatedObject { get; set; }


        public void DrawItem(DrawItemEventArgs e,         Padding margin,
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

    public partial class ExListBox : ListBox {
        private readonly Font _detailsFont;

        private readonly StringFormat _fmt;
        private readonly Font         _titleFont;

        public ExListBox(Font            titleFont, Font            detailsFont,
                         StringAlignment aligment,  StringAlignment lineAligment) {
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
            _fmt               = new StringFormat();
            _fmt.Alignment     = aligment;
            _fmt.LineAlignment = lineAligment;
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
        }

        public ExListBox() {
            InitializeComponent();
            _fmt               = new StringFormat();
            _fmt.Alignment     = StringAlignment.Near;
            _fmt.LineAlignment = StringAlignment.Near;
            _titleFont         = new Font(Font, FontStyle.Bold);
            _detailsFont       = new Font(Font, FontStyle.Regular);
        }


        protected override void OnDrawItem(DrawItemEventArgs e) {
            // prevent from error Visual Designer
            if (Items.Count > 0 && e.Index >= 0) {
                var item = (ExListBoxItem)Items[e.Index];
                item.DrawItem(e, Margin, _titleFont, _detailsFont, _fmt);
            }
        }


        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}