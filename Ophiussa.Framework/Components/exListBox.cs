using System.Drawing;
using System.Windows.Forms;

namespace OphiussaFramework.Components {
    public class ExListBoxItem {
        public ExListBoxItem(string id, string title, string details, object pObject, Image image = null) {
            Id               = id;
            Title            = title;
            Details          = details;
            AssociatedObject = pObject;
            ItemImage        = image;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public object AssociatedObject { get; set; }

        public Image ItemImage { get; set; }

        public void DrawItem(DrawItemEventArgs e,
                             Padding           margin,
                             Font              titleFont,
                             Font              detailsFont,
                             StringFormat      aligment,
                             Size?             imageSize = null) {
            // if selected, mark the background differently
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            else
                e.Graphics.FillRectangle(Brushes.Beige, e.Bounds);

            // draw some item separator
            e.Graphics.DrawLine(Pens.DarkGray, e.Bounds.X, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y);

            // draw item image
            int imgWidth  = 0;
            int imgHeight = 0;
            if (ItemImage != null) {
                imgWidth  = imageSize.Value.Width;
                imgHeight = imageSize.Value.Height;
                e.Graphics.DrawImage(ItemImage, e.Bounds.X + margin.Left, e.Bounds.Y + margin.Top, imgWidth, imgHeight);
            }

            // calculate bounds for title text drawing
            var titleBounds = new Rectangle(e.Bounds.X                 + margin.Horizontal + imgWidth,
                                            e.Bounds.Y                 + margin.Top,
                                            e.Bounds.Width             - margin.Right - imgWidth - margin.Horizontal,
                                            (int)titleFont.GetHeight() + 2);

            // calculate bounds for details text drawing
            var detailBounds = new Rectangle(e.Bounds.X      + margin.Horizontal          + imgWidth,
                                             e.Bounds.Y      + (int)titleFont.GetHeight() + 2                          + margin.Vertical + margin.Top,
                                             e.Bounds.Width  - margin.Right               - imgWidth                   - margin.Horizontal,
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
        private readonly Size         _imageSize;
        private readonly Font         _titleFont;

        public ExListBox(Font            titleFont,
                         Font            detailsFont,
                         Size            imageSize,
                         StringAlignment aligment,
                         StringAlignment lineAligment) {
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
            _imageSize         = imageSize;
            ItemHeight         = _imageSize.Height + Margin.Vertical;
            _fmt               = new StringFormat();
            _fmt.Alignment     = aligment;
            _fmt.LineAlignment = lineAligment;
            _titleFont         = titleFont;
            _detailsFont       = detailsFont;
        }

        public ExListBox() {
            InitializeComponent();
            _imageSize         = new Size(80, 60);
            ItemHeight         = _imageSize.Height + Margin.Vertical;
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
                item.DrawItem(e, Margin, _titleFont, _detailsFont, _fmt, _imageSize);
            }
        }


        protected override void OnPaint(PaintEventArgs pe) {
            base.OnPaint(pe);
        }
    }
}