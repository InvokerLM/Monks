using System.Windows.Forms;

namespace Monks
{
    public partial class Pio : UserControl
    {
        private void UpStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        public Pio()
        {
            InitializeComponent();

            UpStyle();
        }

        public int LocX { get; set; }
        public int LocY { get; set; }

        private int _pioType = 0;
        public int PioType
        {
            get
            {
                return _pioType;
            }
            set
            {
                if (value > 2)
                    return;
                switch (value)
                {
                    case 1:
                        this.BackgroundImage = Properties.Resources.pio1;
                        break;
                    case 2:
                        this.BackgroundImage = Properties.Resources.pio2;
                        break;
                    default:
                        this.BackgroundImage = null;
                        break;
                }
                _pioType = value;
            }
        }
        public void setNullPic()
        {
            this.BackgroundImage = null;
        }
    }
}
