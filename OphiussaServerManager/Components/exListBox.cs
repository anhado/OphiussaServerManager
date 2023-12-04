using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OphiussaServerManager
{
    public class exListBoxItem
    {
        private string _title;
        private string _details;
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public exListBoxItem(string id, string title, string details)
        {
            _id = id;
            _title = title;
            _details = details;
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }


        public void drawItem(DrawItemEventArgs e, Padding margin, 
                             Font titleFont, Font detailsFont, StringFormat aligment)
        {            

            // if selected, mark the background differently
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Beige, e.Bounds);
            }

            // draw some item separator
            e.Graphics.DrawLine(Pens.DarkGray, e.Bounds.X, e.Bounds.Y, e.Bounds.X + e.Bounds.Width, e.Bounds.Y);

            // calculate bounds for title text drawing
            Rectangle titleBounds = new Rectangle(e.Bounds.X + margin.Horizontal,
                                                  e.Bounds.Y + margin.Top,
                                                  e.Bounds.Width - margin.Right - margin.Horizontal,
                                                  (int)titleFont.GetHeight() + 2);
            
            // calculate bounds for details text drawing
            Rectangle detailBounds = new Rectangle(e.Bounds.X + margin.Horizontal ,
                                                   e.Bounds.Y + (int)titleFont.GetHeight() + 2 + margin.Vertical + margin.Top,
                                                   e.Bounds.Width - margin.Right  - margin.Horizontal,
                                                   e.Bounds.Height - margin.Bottom - (int)titleFont.GetHeight() - 2 - margin.Vertical - margin.Top);

            // draw the text within the bounds
            e.Graphics.DrawString(this.Title, titleFont, Brushes.Black, titleBounds, aligment);
            e.Graphics.DrawString(this.Details, detailsFont, Brushes.DarkGray, detailBounds, aligment);            
            
            // put some focus rectangle
            e.DrawFocusRectangle();
        
        }

    }

    public partial class exListBox : ListBox
    {
         
        private StringFormat _fmt;
        private Font _titleFont;
        private Font _detailsFont;

        public exListBox(Font titleFont, Font detailsFont, 
                         StringAlignment aligment, StringAlignment lineAligment)
        {
            _titleFont = titleFont;
            _detailsFont = detailsFont;
            _fmt = new StringFormat();
            _fmt.Alignment = aligment;
            _fmt.LineAlignment = lineAligment;
            _titleFont = titleFont;
            _detailsFont = detailsFont;
        }

        public exListBox()
        {
            InitializeComponent();
            _fmt = new StringFormat();
            _fmt.Alignment = StringAlignment.Near;
            _fmt.LineAlignment = StringAlignment.Near;
            _titleFont = new Font(this.Font, FontStyle.Bold);
            _detailsFont = new Font(this.Font, FontStyle.Regular);
            
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // prevent from error Visual Designer
            if (this.Items.Count > 0 && e.Index>=0)            
            {                
                exListBoxItem item = (exListBoxItem)this.Items[e.Index];                
                item.drawItem(e, this.Margin, _titleFont, _detailsFont, _fmt);
            }                            
        }
       

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
