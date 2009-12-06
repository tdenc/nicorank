using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace videocut
{
    public class VideoCutConfig
    {
        private string video_file_name = "";
        private string video_dir = "";
        private string rank_file_name = "";
        private bool is_control_form_detail_open = false;
        private string cut_list_file_name = "";
        private bool is_fix_length = false;
        private string video_length = "";
        private bool is_video_size_fixed = false;
        private int video_size_fixed_width = -1;
        private int video_size_fixed_height = -1;
        private bool is_memory_size_manual = false;
        private int memory_size = -1;

        private Point main_form_location = new Point(int.MinValue, int.MinValue);
        private Size main_form_size = new Size(int.MinValue, int.MinValue);

        private bool is_show_control_form = true;
        private Point control_form_location = new Point(int.MinValue, int.MinValue);
        private Size control_form_size = new Size(int.MinValue, int.MinValue);

        private bool is_show_cut_list_form = true;
        private Point cut_list_form_location = new Point(int.MinValue, int.MinValue);
        private Size cut_list_form_size = new Size(int.MinValue, int.MinValue);

        private List<string> cut_list_history_filename_list = new List<string>();
        private bool is_adding_save = false;

        public string VideoFileName
        {
            get { return video_file_name; }
            set { video_file_name = value; }
        }

        public string VideoDir
        {
            get { return video_dir; }
            set { video_dir = value; }
        }

        public string RankFileName
        {
            get { return rank_file_name; }
            set { rank_file_name = value; }
        }

        public bool IsControlFormDetailOpen
        {
            get { return is_control_form_detail_open; }
            set { is_control_form_detail_open = value; }
        }

        public string CutListFileName
        {
            get { return cut_list_file_name; }
            set { cut_list_file_name = value; }
        }

        public bool IsFixLength
        {
            get { return is_fix_length; }
            set { is_fix_length = value; }
        }

        public string VideoLength
        {
            get { return video_length; }
            set { video_length = value; }
        }

        public Point MainFormLocation
        {
            get { return main_form_location; }
            set { main_form_location = value; }
        }

        public Size MainFormSize
        {
            get { return main_form_size; }
            set { main_form_size = value; }
        }

        public bool IsShowControlForm
        {
            get { return is_show_control_form; }
            set { is_show_control_form = value; }
        }

        public Point ControlFormLocation
        {
            get { return control_form_location; }
            set { control_form_location = value; }
        }

        public Size ControlFormSize
        {
            get { return control_form_size; }
            set { control_form_size = value; }
        }

        public bool IsShowCutListForm
        {
            get { return is_show_cut_list_form; }
            set { is_show_cut_list_form = value; }
        }

        public Point CutListFormLocation
        {
            get { return cut_list_form_location; }
            set { cut_list_form_location = value; }
        }

        public Size CutListFormSize
        {
            get { return cut_list_form_size; }
            set { cut_list_form_size = value; }
        }

        public bool IsVideoSizeFixed
        {
            get { return is_video_size_fixed; }
            set { is_video_size_fixed = value; }
        }

        public int VideoSizeFixedWidth
        {
            get { return video_size_fixed_width; }
            set { video_size_fixed_width = value; }
        }

        public int VideoSizeFixedHeight
        {
            get { return video_size_fixed_height; }
            set { video_size_fixed_height = value; }
        }

        public bool IsMemorySizeManual
        {
            get { return is_memory_size_manual; }
            set { is_memory_size_manual = value; }
        }

        public int MemorySize
        {
            get { return memory_size; }
            set { memory_size = value; }
        }

        public List<string> CutListHistoryFileNameList
        {
            get { return cut_list_history_filename_list; }
            set { cut_list_history_filename_list = value; }
        }

        public bool IsAddingSave
        {
            get { return is_adding_save; }
            set { is_adding_save = value; }
        }
    }
}
