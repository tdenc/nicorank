// Copyright (c) 2008 - 2009 rankingloid
//
// under GNU General Public License Version 2.
//
using System;
using System.Drawing;
using System.Windows.Forms;

namespace videocut
{
    public partial class VideoSlideControl : UserControl
    {
        public delegate void MouseDraggingEventHandler(object sender, MouseDraggingEventArgs e);

        private int length_ = 1;
        private int position_ = 0;
        private int start_position_ = 0;
        private int end_position_ = 1;
        private int[] key_frame_list_ = null;
        private bool[] frame_exists_list_ = null;

        private const int margin_width_ = 10;
        private int mode_ = 0;
        private int old_x_ = 0;
        private int old_frame_ = 0;

        public new MouseEventHandler MouseDown;
        public MouseDraggingEventHandler Dragging;
        public new MouseEventHandler MouseUp;

        public VideoSlideControl()
        {
            InitializeComponent();
        }

        public int Position
        {
            get { return position_; }
            set
            {
                int old_position = position_;
                position_ = AdjustPosition(value);
                if (old_position != position_)
                {
                    pictureBoxSeek.Invalidate();
                }
            }
        }
        
        public int StartPosition
        {
            get { return start_position_; }
            set
            {
                int old_position = start_position_;
                start_position_ = AdjustPosition(value);
                if (old_position != start_position_)
                {
                    pictureBoxSeek.Invalidate();
                }
            }
        }

        public int EndPosition
        {
            get { return end_position_; }
            set
            {
                int old_position = end_position_;
                end_position_ = AdjustPosition(value);
                if (old_position != end_position_)
                {
                    pictureBoxSeek.Invalidate();
                }
            }
        }

        public int Length
        {
            get { return length_; }
            set { length_ = value; }
        }

        public int[] KeyFrameList
        {
            get { return key_frame_list_; }
            set { key_frame_list_ = value; }
        }

        public bool[] FrameExistsList
        {
            get { return frame_exists_list_; }
            set { frame_exists_list_ = value; }
        }

        private void pictureBoxSeek_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Brushes.Black))
            {
                pen.Color = Color.Pink;

                if (frame_exists_list_ != null)
                {
                    for (int i = 0; i < frame_exists_list_.Length; ++i)
                    {
                        if (frame_exists_list_[i])
                        {
                            int kx = PositionToAxisX(i);
                            e.Graphics.DrawLine(pen, kx, 20, kx, 40);
                        }
                    }
                }

                pen.Color = Color.Red;

                if (key_frame_list_ != null)
                {
                    for (int i = 0; i < key_frame_list_.Length; ++i)
                    {
                        int kx = PositionToAxisX(key_frame_list_[i]);
                        e.Graphics.DrawLine(pen, kx, 20, kx, 40);
                    }
                }

                pen.Color = Color.Blue;

                int sx = PositionToAxisX(start_position_);
                e.Graphics.DrawLine(pen, sx, 20, sx, 50);
                Point[] point_list = new Point[]{
                    new Point(sx, 20), new Point(sx + 6, 25), new Point(sx, 30), new Point(sx, 20)
                };
                e.Graphics.FillPolygon(Brushes.Blue, point_list, System.Drawing.Drawing2D.FillMode.Alternate);

                int ex = PositionToAxisX(end_position_);
                e.Graphics.DrawLine(pen, ex, 20, ex, 50);
                point_list = new Point[]{
                    new Point(ex, 20), new Point(ex - 5, 25), new Point(ex, 30), new Point(ex, 20)
                };
                e.Graphics.FillPolygon(Brushes.Blue, point_list, System.Drawing.Drawing2D.FillMode.Alternate);

                pen.Color = Color.Black;

                int x = PositionToAxisX(position_);
                e.Graphics.DrawLine(pen, margin_width_, 30, pictureBoxSeek.Width - margin_width_, 30);
                e.Graphics.DrawLine(pen, x, 12, x, 45);
                point_list = new Point[]{
                    new Point(x, 20), new Point(x + 1, 20), new Point(x + 9, 12), new Point(x - 8, 12), new Point(x, 20)
                };
                e.Graphics.FillPolygon(Brushes.Black, point_list, System.Drawing.Drawing2D.FillMode.Alternate);
                //point_list = new Point[]{
                //    new Point(x, 40), new Point(x + 10, 50), new Point(x - 10, 50), new Point(x, 40)
                //};
                //e.Graphics.FillPolygon(Brushes.Black, point_list, System.Drawing.Drawing2D.FillMode.Alternate);
            }
        }

        private void pictureBoxSeek_MouseDown(object sender, MouseEventArgs e)
        {
            int x = PositionToAxisX(position_);
            int sx = PositionToAxisX(start_position_);
            int ex = PositionToAxisX(end_position_);

            if (x - 8 <= e.X && e.X <= x + 8 && 12 <= e.Y && e.Y <= 20)
            {
                mode_ = 2;
                old_frame_ = position_;
                old_x_ = e.X;
                MouseDown(this, e);
            }
            //else if (x - 10 <= e.X && e.X <= x + 10 && 40 <= e.Y && e.Y <= 50)
            //{
            //    mode_ = 1;
            //    old_frame_ = position_;
            //    old_x_ = e.X;
            //}
            else if (sx <= e.X && e.X <= sx + 6 && 20 <= e.Y && e.Y <= 30)
            {
                mode_ = 3;
                old_frame_ = start_position_;
                old_x_ = e.X;
            }
            else if (ex - 5 <= e.X && e.X <= ex && 20 <= e.Y && e.Y <= 30)
            {
                mode_ = 4;
                old_frame_ = end_position_;
                old_x_ = e.X;
            }
            else
            {
                mode_ = 1;
                Position = AxisXToPosition(e.X - margin_width_);
                old_frame_ = position_;
                old_x_ = e.X;
                MouseDown(this, e);
            }
        }

        private void pictureBoxSeek_MouseMove(object sender, MouseEventArgs e)
        {
            if (mode_ == 1)
            {
                Position = old_frame_ + AxisXToPosition(e.X - old_x_);
                Dragging(this, new MouseDraggingEventArgs(MouseDraggingEventArgs.EventKind.SeekingPosition, position_));
            }
            else if (mode_ == 2)
            {
                if (key_frame_list_ != null)
                {
                    int frame = old_frame_ + AxisXToPosition(e.X - old_x_);

                    int i;
                    for (i = 1; i < key_frame_list_.Length; ++i)
                    {
                        if (frame < key_frame_list_[i])
                        {
                            break;
                        }
                    }
                    if (i >= key_frame_list_.Length)
                    {
                        position_ = length_;
                    }
                    else if (key_frame_list_[i] - frame < frame - key_frame_list_[i - 1])
                    {
                        position_ = key_frame_list_[i];
                    }
                    else
                    {
                        position_ = key_frame_list_[i - 1];
                    }
                    pictureBoxSeek.Invalidate();
                    Dragging(this, new MouseDraggingEventArgs(MouseDraggingEventArgs.EventKind.SeekingPosition, position_));
                }
            }
            else if (mode_ == 3)
            {
                StartPosition = old_frame_ + AxisXToPosition(e.X - old_x_);
                pictureBoxSeek.Invalidate();
                Dragging(this, new MouseDraggingEventArgs(MouseDraggingEventArgs.EventKind.SeekingStartPosition, start_position_));
            }
            else if (mode_ == 4)
            {
                EndPosition = old_frame_ + AxisXToPosition(e.X - old_x_);
                pictureBoxSeek.Invalidate();
                Dragging(this, new MouseDraggingEventArgs(MouseDraggingEventArgs.EventKind.SeekingEndPosition, end_position_));
            }
        }

        private void pictureBoxSeek_MouseUp(object sender, MouseEventArgs e)
        {
            if (mode_ == 3 || mode_ == 4)
            {
                mode_ = 0;
            }
            else
            {
                mode_ = 0;
                MouseUp(this, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBoxSeek.Invalidate();
        }

        private int AdjustPosition(int pos)
        {
            if (pos < 0)
            {
                return 0;
            }
            else if (pos > length_)
            {
                return length_;
            }
            else
            {
                return pos;
            }
        }

        private int PositionToAxisX(int pos)
        {
            return (int)(((long)pictureBoxSeek.Width - 2 * margin_width_) * pos / length_) + margin_width_;
        }

        private int AxisXToPosition(int x)
        {
            return length_ * x / (pictureBoxSeek.Width - 2 * margin_width_);
        }
    }

    public class MouseDraggingEventArgs : EventArgs
    {
        public enum EventKind { SeekingPosition, SeekingStartPosition, SeekingEndPosition }

        private EventKind event_kind_;
        private int position_;

        public MouseDraggingEventArgs(EventKind kind, int position)
        {
            event_kind_ = kind;
            position_ = position;
        }

        public EventKind Kind
        {
            get { return event_kind_; }
        }

        public int Position
        {
            get { return position_; }
        }
    }
}
