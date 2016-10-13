using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tourism_Management_System
{
        public partial class AdobeReaderControl : UserControl
        {
            public AdobeReaderControl(string fileName)
            {
                InitializeComponent();

                this.axAcroPDF1.LoadFile(fileName);
            }
        }
    }
