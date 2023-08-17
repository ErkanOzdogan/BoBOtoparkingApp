using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoBOtoparking
{
    public partial class BobOtoparkTahsilatForm : Form
    {
        public BobOtoparkTahsilatForm()
        {
            InitializeComponent();
        }

        private void BobOtoparkTahsilatForm_Load(object sender, EventArgs e)
        {
            PlakaNoTxt.Text = BobOtoParkMainForm.OutLocalPlakaNo;
            AracIdTxt.Text = BobOtoParkMainForm.OutLocalAracId.ToString();
            AracGrupIdTxt.Text = BobOtoParkMainForm.OutLocalAracGrupDetayId.ToString();
            GrupIdTxt.Text = BobOtoParkMainForm.OutLocalGrupId.ToString();
            TarifeTxt.Text = BobOtoParkMainForm.OutLocalTarifeId.ToString();
            GirisTarihiTXT.Text = BobOtoParkMainForm.OutLocalGirisTarihi.ToString();
            CikisTArihiTXT.Text = BobOtoParkMainForm.OutLocalCikisTarihi.ToString();
            SureTXT.Text = BobOtoParkMainForm.OutLocalKalisSuresi.ToString();
            TutarTxt.Text = BobOtoParkMainForm.OutLocalTutar.ToString();
        }

        private void TahsilatBtn_Click(object sender, EventArgs e)
        {
            BobOtoParkMainForm.BariyeriAc = 1;
            this.Close();
        }
    }
}
