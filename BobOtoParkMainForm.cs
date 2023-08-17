using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Bob.Otopark.Entities;
namespace BoBOtoparking
{
    public partial class BobOtoParkMainForm : Form
    {
        public BobOtoParkMainForm()
        {
            InitializeComponent();
        }
        public static int BariyeriAc;
        public static int HataVar = 0;
        //-- Giriþ yapan araç deðiþkenleri
        public static string LocalPlakaNo = "";
        public static Guid LocalGlobalId;
        public static int LocalIlkKullaniciKod=1;
        public static int LocalSonKullaniciKod=1;
        public static int LocalHizmetYerID=11;
        public static int LocalAlanId;
        public static string LocalAlanAdi="";
        public static int LocalKapasite;
        public static int LocalAracSayisi;
        public static int LocalIstasyonId;
        public static string LocalIstasyonAdi="";
        public static int LocalKameraId = 1;
        public static int LocalBariyerId= 1;
        public static int LocalAracId;
        public static int LocalGrupId;
        public static string LocalGrupAdi = "";
        public static int LocalAracGrupId;
        public static int LocalTarifeId;
        public static int LocalVarsayilanTarifeId;
        public static int LocalTarifeAracId;
        public static int LocalKullanimId;
        public static int LocalKullanimDetayId;
        public static DateTime LocalBaslangicTarihi;
        public static DateTime LocalBitisTarihi;
        //-- Çýkýþ yapan araç deðiþkenleri
        public static string OutLocalPlakaNo = "";
        public static int OutLocalIlkKullaniciKod = 1;
        public static int OutLocalSonKullaniciKod = 1;
        public static int OutLocalIstasyonId;
        public static int OutLocalKameraId;
        public static int OUTLocalBariyerId;
        public static int OutLocalAracId;
        public static int OutLocalGrupId;
        public static int OutLocalAracGrupDetayId;
        public static int OutLocalTarifeId;
        public static int OutLocalKullanimId;
        public static int OutLocalAvantajId = 0;
        public static Guid OutLocalGlobalId;
        public static decimal OutLocalTutar;
        public static decimal OutLocalIskontoTutar = 0;
        public static decimal OutLocalToplamTutar;
        public static DateTimeOffset OutLocalGirisTarihi;
        public static DateTimeOffset OutLocalCikisTarihi;
        public static TimeSpan OutLocalKalisSuresi;
        private void BobOtoParkMainForm_Load(object sender, EventArgs e)
        {            
            LocalAlanId = 0;
            LocalKapasite = 0;
            LocalAracSayisi = 0;
            LocalIstasyonId = 0;
            LocalKameraId = 0;
            LocalBariyerId = 0;
            AlanCBDoldur();
        }
        void AlanCBDoldur()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                string strSQL;
                SqlCommand cmd;
                SqlDataAdapter da = new SqlDataAdapter();
                // Fill DataTable
                DataTable dt = new DataTable();
                strSQL = "Select * From TblOtoparkAlan Order by Id ASC";
                cmd = new SqlCommand(strSQL, cnn);
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cnn.Close();
                //Bind ComboBox
                OtoparkAlanCB.Items.Clear();
                OtoparkAlanCB.Items.Add("");
                OtoparkAlanCB.DataSource = dt;
                OtoparkAlanCB.DisplayMember = "Adi";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            IstasyonCBDoldur();
        }
        void IstasyonCBDoldur()
        {
            //try
            //{
            BobOtoParkMainForm.SQLServerConnectionControl();
            string strSQL;
            SqlCommand cmd;
            SqlDataAdapter da = new SqlDataAdapter();
            // Fill DataTable
            DataTable dt = new DataTable();
            strSQL = "Select A.Adi From TblOtoparkIstasyon A, TblOtoparkAlan B WHERE A.AlanId = B.Id AND B.Adi ='" + OtoparkAlanCB.Text + "'";
            cmd = new SqlCommand(strSQL, cnn);
            cmd.CommandType = CommandType.Text;
            da.SelectCommand = cmd;
            da.Fill(dt);
            cnn.Close();
            //Bind ComboBox
            OtoparkIstasyonCB.DataSource = dt;
            OtoparkIstasyonCB.DisplayMember = "Adi";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
        //--SQLServer baðlantý ve sorgu cümlesi tanýmý
        public static string connectionString;
        public static SqlConnection cnn;
        DataSet BobOtoparkDS = new DataSet();
        public static void SQLServerConnection()
        {

            connectionString = "Data Source=178.18.206.40;Initial Catalog=BobOtopark;TrustServerCertificate=true;User ID=BobOtoparkUser;Password=vcA66749RKu4";
            //connectionString = "Data Source =DESKTOP-SGMHQOC\\MYSQLSERVER; Initial Catalog = BobOtopark; Integrated Security = True; ";
            cnn = new SqlConnection(connectionString);
        }
        public static void SQLServerConnectionControl()
        {
            BobOtoParkMainForm.SQLServerConnection();
            try
            {
                if (BobOtoParkMainForm.cnn.State != ConnectionState.Open)
                {
                    BobOtoParkMainForm.cnn.Open();
                    //MessageBox.Show("Connection Open  !");
                }
            }
            catch
            {
                MessageBox.Show("SQLServer Baðlantý Hatasý !");
            }
        }

        private void GirenAracBtn_Click(object sender, EventArgs e)
        {
            //-- Giren aracýn plakasý okunduðu zaman iþletilecek
            string okunanplaka = GirenAracPlakaNo.Text.ToUpper();
            LocalPlakaNo = null;
            for (int i = 0; i < okunanplaka.Length; i++)
            {
                if (okunanplaka[i] != ' ')
                {
                    LocalPlakaNo += okunanplaka[i];
                }
            }
            //-- Plaka No içerideki araçlarýn plakasý ile karþýlaþtýrýlacak
            //-- Otopark kapasitesi kontrol edilecek+
            HataVar = 0;
            if (LocalAracSayisi >= LocalKapasite)
            {
                MessageBox.Show("OTOPARKIN KAPASÝTESÝ DOLU");
                HataVar = 1;
            }
            //-- Bu Otopark için varsayýlan tarifeyi belirle+
            if (HataVar == 0)
                OtoparktakVarsayilanTarifeyiBul();
            //-- Araç Grubunu ve tarifesini belirle (Varsayýlan tarife dýþýnda tarifeleri olan otoparklar için iþletilecek)
            if (HataVar == 0)
                OtoparktakiAracGrubuveTarifesiniBul();
            //OtoparkAracTarifeBelirle();
            //-- Araç Grup Id'sine göre tarifeyi belirle
            //--    Tarife Alan Tablosundan Alan ve Araç Grubu uyum kontrolü yap - Bariyeri aç/açma
            //-- Araç doðru alana giriþ yapýyorsa senaryoyu belirle
            //--    Senaryo deðeri var/yok kontrol ile limit aþým kontrolüne göre belirlenecek
            //-- TblOtoparkArac Tablosuna Kayýt Yap. (Id, Plaka)
            if (HataVar == 0)
                OtoparkAracTablosuKayit();
            //-- TblOtoparkAracGrupDetay Tablosuna Kayýt Yap. (GrupId, AracId)
            if (HataVar == 0)
                OtoparkAracGrupDetayTablosuKayit();
            //-- TblOtoparkTarifeArac Tablosuna Kayýt yap. (TarifeId, AracId, AracGrupId)
            if (HataVar == 0)
                OtoparkTarifeAracTablosuKayit();
            //-- TblOtoparkKullanim Tablosuna Giriþ Kaydý Yap. (HizmetYerId,IstasyonId,AlanId,AracId,GirisTarihi)
            if (HataVar == 0)
                OtoparkKullanimTablosuKayit();
        }
        private void CikanAracBtn_Click(object sender, EventArgs e)
        {
            //-- Çýkan aracýn plakasý okundu
            string okunanplaka = CikanAracPlakaNo.Text.ToUpper();
            OutLocalPlakaNo = null;
            for (int i = 0; i < okunanplaka.Length; i++)
            {
                if (okunanplaka[i] != ' ')
                {
                    OutLocalPlakaNo += okunanplaka[i];
                }
            }
            CikanAracPlakaToAracIdBelirle();
            CikanAracGrupIdBelirle();
            CikanAracTarifeIdBelirle();
            //-- Otoparkta kalýþ süresini hesapla
            CikanAracGirisZamaniBelirle();
            //-- Tarife Id'ye göre fitalandýr
            BariyeriAc = 0;
            CikanAracTutarBelirle();
            //-- Popup Tahsilat Ekraný
            Form BobOtoparkTahsilatForm = new BobOtoparkTahsilatForm();
            BobOtoparkTahsilatForm.Owner = this;
            BobOtoparkTahsilatForm.ShowDialog();
            //-- TblOtoparkKullanim Tablosunu Güncelle (CikisTarihi)
            OtoparkKullanimTablosuGuncelle();
            //-- TblOtoparkTarifeArac Tablosunda Kullanildi kolonunu True yap.
            OtoparkTarifeAracTablosuGuncelle();
            //-- TblOtoparkKullanimDetay tablosuna Kayýt yap. (KullanýmId,TarifeId,AvantajId)
            OtoparkKullanýmDetayTablosuKayit();
        }
        void OtoParkAlanKapasiteBul()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable OtoParkAlanKapasite = new DataTable();
                string AlanKApasiteBilgileriStr = "SELECT TOP 1 Id,Kapasite FROM TblOtoparkAlan WHERE Adi='" + BobOtoParkMainForm.LocalAlanAdi+"'";
                SqlDataAdapter AlanKapasiteda = new SqlDataAdapter(AlanKApasiteBilgileriStr.ToString(), BobOtoParkMainForm.connectionString);
                AlanKapasiteda.Fill(OtoParkAlanKapasite);
                BobOtoParkMainForm.cnn.Close();
                LocalKapasite = Convert.ToInt32(OtoParkAlanKapasite.Rows[0]["Kapasite"].ToString());
            }
            catch
            {
                MessageBox.Show("Alan ile ilgili Kapasite Bilgisi Bulunamadý.");
            }
            label1.Text = LocalKapasite.ToString();
        }
        void OtoparktakiAracSayisiBul()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable AracSayisi = new DataTable();
                string AlandakiAracSayisiStr = "SELECT Count(A.AracId) AS OtoparkdakiAracSayisi FROM TblOtoparkKullanim A, TblOtoparkAlan B WHERE A.CikisTarihi IS NULL AND A.AlanId = B.Id AND B.Adi='" + BobOtoParkMainForm.LocalAlanAdi+"'";
                SqlDataAdapter AracSayisida = new SqlDataAdapter(AlandakiAracSayisiStr.ToString(), BobOtoParkMainForm.connectionString);
                AracSayisida.Fill(AracSayisi);
                BobOtoParkMainForm.cnn.Close();
                LocalAracSayisi = Convert.ToInt32(AracSayisi.Rows[0]["OtoparkdakiAracSayisi"].ToString());
            }
            catch
            {
                MessageBox.Show("Alan ile ilgili Ýçerdeki Araç Sayýsý Bulunamadý.");
            }
            label2.Text = LocalAracSayisi.ToString();
        }
        void OtoparktakiAracGrubuveTarifesiniBul()
        {
            //-- Okunan plakadan aracýn hangi gruba dahil edileceði belinenecek
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable AracGrupdt = new DataTable();
                //string AracGrupStr = "SELECT A.Id AS AracId, A.Plaka, B.Id AS GrupId, B.Adi, C.Id AS AracGrupId, BaslangicTarihi,BitisTarihi FROM TblOtoparkArac A, TblOtoparkAracGrup B, TblOtoparkAracGrupDetay C WHERE C.AracId = A.Id AND C.GrupId = B.Id AND A.Plaka='" + BobOtoParkMainForm.LocalPlakaNo + "'";
                string AracGrupStr = "SELECT A.Id AS AracId, A.Plaka, B.Id AS GrupId, B.Adi, C.Id AS AracGrupId, BaslangicTarihi,BitisTarihi FROM TblOtoparkArac A INNER JOIN TblOtoparkAracGrupDetay C ON A.Id = C.AracId INNER JOIN TblOtoparkAracGrup B ON B.Id = C.GrupId WHERE A.Plaka='" + BobOtoParkMainForm.LocalPlakaNo + "'";
                SqlDataAdapter AracGrupda = new SqlDataAdapter(AracGrupStr.ToString(), BobOtoParkMainForm.connectionString);
                AracGrupda.Fill(AracGrupdt);
                BobOtoParkMainForm.cnn.Close();
                //-- Araç plakasý daha önceki kayýtlar ile eþleþti mi?
                if (AracGrupdt.Rows.Count > 0 ) 
                {
                    LocalGrupId = Convert.ToInt32(AracGrupdt.Rows[0]["GrupId"].ToString());
                    LocalGrupAdi = AracGrupdt.Rows[0]["Adi"].ToString();
                    LocalAracId = Convert.ToInt32(AracGrupdt.Rows[0]["AracId"].ToString());
                    LocalBaslangicTarihi = DateTime.Parse(AracGrupdt.Rows[0]["BaslangicTarihi"].ToString());
                    LocalBitisTarihi = DateTime.Parse(AracGrupdt.Rows[0]["BitisTarihi"].ToString());
                    LocalAracGrupId = Convert.ToInt32(AracGrupdt.Rows[0]["AracGrupId"].ToString());
                //-- Araç Tarifesini belirle
                    OtoparkAracTarifeBelirle();
                    if (HataVar == 1)
                        MessageBox.Show("Araç Grubununa Uygun Tarife Bulunamadý");
                    //-- Gruba özel denetlemeler yapýlacak
                    //-- 1.Girmek istediði alan için uygun mu?
                    if (HataVar == 0)
                        OtoparkAracGirmeyeYetkiliAlanlariBul();
                    if (HataVar == 1)
                        MessageBox.Show("Araç Grubununa Göre Yapýlan Kontrolde Bu Alan Girmeye Uygun Deðil");
                    //-- 1.Baþlangýç bitiþ tarihleri uygun mu?
                    //-- 2.Senaryo deðeri var mý? Varsa bu senaryoydan kaç araç var ve senaryo deðeri aþýlmýþ mý?
            }
                else
                {
                    LocalTarifeId = LocalVarsayilanTarifeId;
                }
            }
            catch
            {
                MessageBox.Show("Araç Grubu Belirlenken Hata Oluþtu.");
            }
            label2.Text = LocalAracSayisi.ToString();
        }
        void OtoparktakVarsayilanTarifeyiBul()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Tarifestr = "SELECT Id AS TarifeId FROM TblOtoparkTarife WHERE Varsayilan = 'True'";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Tarifestr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                    LocalVarsayilanTarifeId = Convert.ToInt32(Otoparkdt.Rows[0]["TarifeId"].ToString());
                else
                {
                    HataVar = 1;
                }
            }
            catch
            {
                //MessageBox.Show("Hata");
            }
        }
        void OtoparkAracTarifeBelirle()
        {
            HataVar = 0;
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Tarifestr = "SELECT Id AS TarifeId FROM TblOtoparkTarife WHERE Adi='" + BobOtoParkMainForm.LocalGrupAdi + "'";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Tarifestr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                    LocalTarifeId = Convert.ToInt32(Otoparkdt.Rows[0]["TarifeId"].ToString());
                else
                {
                    LocalTarifeId = LocalVarsayilanTarifeId;
                }
            }
            catch
            {
                //MessageBox.Show("Hata");
            }
        }
        void OtoparkAracGirmeyeYetkiliAlanlariBul()
        {
            BobOtoParkMainForm.SQLServerConnectionControl();
            DataTable Otoparkdt = new DataTable();
            string AlanStr = "SELECT AlanId FROM TblOtoparkTarifeAlan WHERE TarifeId=" + LocalTarifeId;
            SqlDataAdapter Otoparkda = new SqlDataAdapter(AlanStr.ToString(), BobOtoParkMainForm.connectionString);
            Otoparkda.Fill(Otoparkdt);
            BobOtoParkMainForm.cnn.Close();
            HataVar = 1;
            for (int j = 0; j <= Otoparkdt.Rows.Count - 1; j++)
            {
                if (LocalAlanId == Convert.ToInt32(Otoparkdt.Rows[0]["TarifeId"].ToString()))
                    HataVar = 0;                    
            }
        }
        void OtoparkAracTablosuKayit()
        {
            System.Guid guid = System.Guid.NewGuid();
            LocalGlobalId = guid;
            int AracId = 1;
            LocalAracId = AracId;
            string Kayitekle;
            SqlCommand ekle;
            //-- Okunanplakalý araç içerde mi?
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Aracsorgudt = new DataTable();
                string AracSor = "SELECT A.Plaka FROM TblOtoparkArac A, TblOtoparkKullanim B WHERE A.Plaka = '" + LocalPlakaNo + "' AND A.Id = B.AracId AND B.CikisTarihi IS NULL";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(AracSor.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Aracsorgudt);
                BobOtoParkMainForm.cnn.Close();
                if (Aracsorgudt.Rows.Count > 0)
                {
                    MessageBox.Show(LocalPlakaNo + " Plakalý Araç Ýçeride Görünüyor. Lütfen Kontrol Ediniz.");
                    HataVar = 1;
                }
                else
                    HataVar = 0;
            }
            catch
            {
                MessageBox.Show("Hata.");
                HataVar = 1;
            }
            //-- Araç giriþ kaydýný tamamla
            if (HataVar == 0)
            {
                //-- Son kayýt bulunup Id bir artýrýlýyor
                try
                {
                    BobOtoParkMainForm.SQLServerConnectionControl();
                    DataTable sonkayitdt = new DataTable();
                    string AracStr = "SELECT TOP 1 Id AS SonKayitIdNo FROM TblOtoparkArac ORDER BY Id desc";
                    SqlDataAdapter Otoparkda = new SqlDataAdapter(AracStr.ToString(), BobOtoParkMainForm.connectionString);
                    Otoparkda.Fill(sonkayitdt);
                    BobOtoParkMainForm.cnn.Close();
                    if (sonkayitdt.Rows.Count > 0)
                    {
                        AracId = Convert.ToInt32(sonkayitdt.Rows[0]["SonKayitIdNo"].ToString());
                        AracId += 1;
                        LocalAracId = AracId;
                        HataVar = 0;
                    }
                }
                catch
                {
                    MessageBox.Show("Hata.");
                    HataVar = 1;
                }
            }
            if (HataVar == 0)
            {

                //-- Datetime2(7) formatýnda bilgi elde ediliyor
                var SimdikiZamaniAl = DateTimeOffset.Now;
                try
                {
                    BobOtoParkMainForm.SQLServerConnectionControl();
                    Kayitekle = "INSERT INTO TblOtoparkArac (Id,Plaka,GlobalId,IlkKullaniciKod,IlkIslemTarihi,Silindi) values (@p1,@p2,@p3,@p4,@p5,@p6)";
                    ekle = new SqlCommand(Kayitekle, cnn);
                    ekle.Parameters.AddWithValue("@p1", AracId);
                    ekle.Parameters.AddWithValue("@p2", LocalPlakaNo);
                    ekle.Parameters.AddWithValue("@p3", LocalGlobalId);
                    ekle.Parameters.AddWithValue("@p4", 1);
                    ekle.Parameters.AddWithValue("@p5", SimdikiZamaniAl);
                    ekle.Parameters.AddWithValue("@p6", false);
                    ekle.ExecuteNonQuery();
                    BobOtoParkMainForm.cnn.Close();
                    HataVar = 0;
                }
                catch
                {
                    MessageBox.Show("Hata.");
                    HataVar = 1;
                }
            }
        }
        void OtoparkAracGrupDetayTablosuKayit()
        {
            int AracGrupDetayId = 1;
            LocalAracGrupId = AracGrupDetayId;
            string Kayitekle;
            SqlCommand ekle;
            //-- Son kayýt bulunup Id bir artýrýlýyor
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable sonkayitdt = new DataTable();
                string AracGrupDetayStr = "SELECT TOP 1 Id AS SonKayitIdNo FROM TblOtoparkAracGrupDetay ORDER BY Id desc";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(AracGrupDetayStr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(sonkayitdt);
                BobOtoParkMainForm.cnn.Close();
                if (sonkayitdt.Rows.Count > 0)
                {
                    AracGrupDetayId = Convert.ToInt32(sonkayitdt.Rows[0]["SonKayitIdNo"].ToString());
                    AracGrupDetayId += 1;
                    LocalAracGrupId = AracGrupDetayId;
                }
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
            //-- Datetime2(7) formatýnda bilgi elde ediliyor
            var SimdikiZamaniAl = DateTimeOffset.Now;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                LocalGlobalId = guid;
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitekle = "INSERT INTO TblOtoparkAracGrupDetay (Id,GrupId,AracId,BaslangicTarihi,BitisTarihi,GlobalId,EntegrasyonId,EsitlenmeTarihi,IlkKullaniciKod,SonKullaniciKod,IlkIslemTarihi,SonIslemTarihi,Silindi,HizmetYerId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)";
                ekle = new SqlCommand(Kayitekle, cnn);
                ekle.Parameters.AddWithValue("@p1", LocalAracGrupId);
                ekle.Parameters.AddWithValue("@p2", LocalGrupId);
                ekle.Parameters.AddWithValue("@p3", LocalAracId);
                ekle.Parameters.AddWithValue("@p4", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p5", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p6", LocalGlobalId);
                ekle.Parameters.AddWithValue("@p7", 1);
                ekle.Parameters.AddWithValue("@p8", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p9", LocalIlkKullaniciKod);
                ekle.Parameters.AddWithValue("@p10", LocalSonKullaniciKod);
                ekle.Parameters.AddWithValue("@p11", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p12", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p13", false);
                ekle.Parameters.AddWithValue("@p14", LocalHizmetYerID);
                ekle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }
        void OtoparkTarifeAracTablosuKayit()
        {
            int TarifeAracId = 1;
            LocalTarifeAracId = TarifeAracId;
            string Kayitekle;
            SqlCommand ekle;
            //-- Son kayýt bulunup Id bir artýrýlýyor
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable sonkayitdt = new DataTable();
                string AracGrupDetayStr = "SELECT TOP 1 Id AS SonKayitIdNo FROM TblOtoparkTarifeArac ORDER BY Id desc";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(AracGrupDetayStr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(sonkayitdt);
                BobOtoParkMainForm.cnn.Close();
                if (sonkayitdt.Rows.Count > 0)
                {
                    TarifeAracId = Convert.ToInt32(sonkayitdt.Rows[0]["SonKayitIdNo"].ToString());
                    TarifeAracId += 1;
                    LocalTarifeAracId = TarifeAracId;
                }
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
            //-- Datetime2(7) formatýnda bilgi elde ediliyor
            var SimdikiZamaniAl = DateTimeOffset.Now;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                LocalGlobalId = guid;
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitekle = "INSERT INTO TblOtoparkTarifeArac (Id,TarifeId,AracId,AracGrupId,Kullanildi,GlobalId,EntegrasyonId,EsitlenmeTarihi,IlkKullaniciKod,SonKullaniciKod,IlkIslemTarihi,SonIslemTarihi,Silindi,HizmetYerId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)";
                ekle = new SqlCommand(Kayitekle, cnn);
                ekle.Parameters.AddWithValue("@p1", LocalTarifeAracId);
                ekle.Parameters.AddWithValue("@p2", LocalTarifeId);
                ekle.Parameters.AddWithValue("@p3", LocalAracId);
                ekle.Parameters.AddWithValue("@p4", LocalAracGrupId);
                ekle.Parameters.AddWithValue("@p5", false);
                ekle.Parameters.AddWithValue("@p6", LocalGlobalId);
                ekle.Parameters.AddWithValue("@p7", 1);
                ekle.Parameters.AddWithValue("@p8", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p9", LocalIlkKullaniciKod);
                ekle.Parameters.AddWithValue("@p10", LocalSonKullaniciKod);
                ekle.Parameters.AddWithValue("@p11", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p12", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p13", false);
                ekle.Parameters.AddWithValue("@p14", LocalHizmetYerID);
                ekle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }
        void OtoparkKullanimTablosuKayit()
        {
            int KullanimId = 1;
            LocalKullanimId = KullanimId;
            string Kayitekle;
            SqlCommand ekle;
            //-- Son kayýt bulunup Id bir artýrýlýyor
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable sonkayitdt = new DataTable();
                string AracGrupDetayStr = "SELECT TOP 1 Id AS SonKayitIdNo FROM TblOtoparkKullanim ORDER BY Id desc";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(AracGrupDetayStr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(sonkayitdt);
                BobOtoParkMainForm.cnn.Close();
                if (sonkayitdt.Rows.Count > 0)
                {
                    KullanimId = Convert.ToInt32(sonkayitdt.Rows[0]["SonKayitIdNo"].ToString());
                    KullanimId += 1;
                    LocalKullanimId = KullanimId;
                }
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
            //-- Datetime2(7) formatýnda bilgi elde ediliyor
            var SimdikiZamaniAl = DateTimeOffset.Now;
            try
            {
                System.Guid guid = System.Guid.NewGuid();
                LocalGlobalId = guid;
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitekle = "INSERT INTO TblOtoparkKullanim (Id,IstasyonId,AlanId,AracId,GirisTarihi,GlobalId,EntegrasyonId,EsitlenmeTarihi,IlkKullaniciKod,SonKullaniciKod,IlkIslemTarihi,SonIslemTarihi,Silindi,HizmetYerId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)";
                ekle = new SqlCommand(Kayitekle, cnn);
                ekle.Parameters.AddWithValue("@p1", LocalKullanimId);
                ekle.Parameters.AddWithValue("@p2", LocalIstasyonId);
                ekle.Parameters.AddWithValue("@p3", LocalAlanId);
                ekle.Parameters.AddWithValue("@p4", LocalAracId);
                ekle.Parameters.AddWithValue("@p5", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p6", LocalGlobalId);
                ekle.Parameters.AddWithValue("@p7", 1);
                ekle.Parameters.AddWithValue("@p8", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p9", LocalIlkKullaniciKod);
                ekle.Parameters.AddWithValue("@p10", LocalSonKullaniciKod);
                ekle.Parameters.AddWithValue("@p11", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p12", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p13", false);
                ekle.Parameters.AddWithValue("@p14", LocalHizmetYerID);
                ekle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }
        void CikanAracPlakaToAracIdBelirle()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                
                string Plakastr = "SELECT Id AS AracId FROM TblOtoparkArac WHERE Plaka='" + BobOtoParkMainForm.OutLocalPlakaNo + "'";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Plakastr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                    OutLocalAracId = Convert.ToInt32(Otoparkdt.Rows[0]["AracId"].ToString());
                else
                    MessageBox.Show("Araç Bulunamadý");
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }
        void CikanAracGrupIdBelirle()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Grupstr = "SELECT GrupId FROM TblOtoparkAracGrupDetay WHERE AracId=" + BobOtoParkMainForm.OutLocalAracId;
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Grupstr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                {
                    OutLocalGrupId = Convert.ToInt32(Otoparkdt.Rows[0]["GrupId"].ToString());
                }
                else
                    MessageBox.Show("Araç Bulunamadý");
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }
        void CikanAracTarifeIdBelirle()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Tarifestr = "SELECT TarifeId,AracGrupId FROM TblOtoparkTarifeArac WHERE AracId=" + BobOtoParkMainForm.OutLocalAracId;
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Tarifestr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                {
                    OutLocalTarifeId = Convert.ToInt32(Otoparkdt.Rows[0]["TarifeId"].ToString());
                    OutLocalAracGrupDetayId = Convert.ToInt32(Otoparkdt.Rows[0]["AracGrupId"].ToString());
                }
                else
                    MessageBox.Show("Araç Bulunamadý");
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }
        void CikanAracGirisZamaniBelirle()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Tarihstr = "SELECT Id,GirisTarihi,GLobalId FROM TblOtoparkKullanim WHERE AracId=" + BobOtoParkMainForm.OutLocalAracId;
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Tarihstr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                if (Otoparkdt.Rows.Count > 0)
                {
                    OutLocalGirisTarihi = DateTimeOffset.Parse(Otoparkdt.Rows[0]["GirisTarihi"].ToString());
                    var SimdikiZamaniAl = DateTimeOffset.Now;
                    OutLocalCikisTarihi = SimdikiZamaniAl;
                    OutLocalKalisSuresi = OutLocalCikisTarihi - OutLocalGirisTarihi;
                    OutLocalKullanimId = Convert.ToInt32(Otoparkdt.Rows[0]["Id"].ToString());
                    OutLocalGlobalId = Guid.Parse(Otoparkdt.Rows[0]["GLobalId"].ToString());
                }
                else
                    MessageBox.Show("Araç Bulunamadý");
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }
        void CikanAracTutarBelirle()
        {
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable Otoparkdt = new DataTable();
                string Tutarstr = "SELECT BaslangicSaat,BitisSaat,Ucret FROM TblOtoparkTarifeDetay WHERE TarifeId=" + BobOtoParkMainForm.OutLocalTarifeId;
                SqlDataAdapter Otoparkda = new SqlDataAdapter(Tutarstr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(Otoparkdt);
                BobOtoParkMainForm.cnn.Close();
                for (int j = 0; j <= Otoparkdt.Rows.Count - 1; j++)
                {
                    TimeSpan Baslangic = TimeSpan.Parse(Otoparkdt.Rows[j]["BaslangicSaat"].ToString());
                    TimeSpan Bitis = TimeSpan.Parse(Otoparkdt.Rows[j]["BitisSaat"].ToString());
                    TimeSpan Gunluk = TimeSpan.Parse("24:00:00");
                    int Ucret = Convert.ToInt32(Otoparkdt.Rows[j]["Ucret"].ToString());
                    int GunlukUcret;
                    if (Bitis == TimeSpan.Parse("23:59:59"))
                        GunlukUcret = Ucret;
                    if (OutLocalKalisSuresi >= Baslangic && OutLocalKalisSuresi <= Bitis && OutLocalKalisSuresi.Days < 1)
                    {
                        OutLocalTutar = Ucret;
                        break;
                    }
                    else if (OutLocalKalisSuresi.Days >= 1)
                    {
                        double GTutar = Ucret * OutLocalKalisSuresi.Days;
                        OutLocalTutar = (Decimal)GTutar;
                    }
                }
                if (OutLocalTutar == 0)
                    BariyeriAc = 1;
            }
            catch
            {
                MessageBox.Show("Hata");
            }
        }
        void OtoparkKullanimTablosuGuncelle()
        {
            string Kayitgunle;
            SqlCommand gunle;
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitgunle = "UPDATE TblOtoparkKullanim SET CikisTarihi = @p1,SonIslemTarihi = @p2 WHERE AracId=" + BobOtoParkMainForm.OutLocalAracId;
                gunle = new SqlCommand(Kayitgunle, cnn);
                gunle.Parameters.AddWithValue("@p1", OutLocalCikisTarihi);
                gunle.Parameters.AddWithValue("@p2", OutLocalCikisTarihi);
                gunle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }
        void OtoparkTarifeAracTablosuGuncelle()
        {
            string Kayitgunle;
            SqlCommand gunle;
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitgunle = "UPDATE TblOtoparkTarifeArac SET Kullanildi = @p1,SonIslemTarihi = @p2 WHERE AracId=" + BobOtoParkMainForm.OutLocalAracId;
                gunle = new SqlCommand(Kayitgunle, cnn);
                gunle.Parameters.AddWithValue("@p1", true);
                gunle.Parameters.AddWithValue("@p2", OutLocalCikisTarihi);
                gunle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }
        void OtoparkKullanýmDetayTablosuKayit()
        {
            int KullanimDetayId = 1;
            LocalKullanimDetayId = KullanimDetayId;
            string Kayitekle;
            SqlCommand ekle;
            //-- Son kayýt bulunup Id bir artýrýlýyor
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                DataTable sonkayitdt = new DataTable();
                string KullanimDetayStr = "SELECT TOP 1 Id AS SonKayitIdNo FROM TblOtoparkKullanimDetay ORDER BY Id desc";
                SqlDataAdapter Otoparkda = new SqlDataAdapter(KullanimDetayStr.ToString(), BobOtoParkMainForm.connectionString);
                Otoparkda.Fill(sonkayitdt);
                BobOtoParkMainForm.cnn.Close();
                if (sonkayitdt.Rows.Count > 0)
                {
                    KullanimDetayId = Convert.ToInt32(sonkayitdt.Rows[0]["SonKayitIdNo"].ToString());
                    KullanimDetayId += 1;
                    LocalKullanimDetayId = KullanimDetayId;
                }
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
            //-- Datetime2(7) formatýnda bilgi elde ediliyor
            var SimdikiZamaniAl = DateTimeOffset.Now;
            try
            {
                BobOtoParkMainForm.SQLServerConnectionControl();
                Kayitekle = "INSERT INTO TblOtoparkKullanimDetay (Id,KullanimId,TarifeId,AvantajId,Tutar,IskontoTutar,ToplamTutar,GlobalId,EntegrasyonId,EsitlenmeTarihi,IlkKullaniciKod,SonKullaniciKod,IlkIslemTarihi,SonIslemTarihi,Silindi,HizmetYerId) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)";
                ekle = new SqlCommand(Kayitekle, cnn);
                ekle.Parameters.AddWithValue("@p1", LocalKullanimDetayId);
                ekle.Parameters.AddWithValue("@p2", OutLocalKullanimId);
                ekle.Parameters.AddWithValue("@p3", OutLocalTarifeId);
                ekle.Parameters.AddWithValue("@p4", OutLocalAvantajId);
                ekle.Parameters.AddWithValue("@p5", OutLocalTutar);
                ekle.Parameters.AddWithValue("@p6", OutLocalIskontoTutar);
                ekle.Parameters.AddWithValue("@p7", OutLocalTutar - OutLocalIskontoTutar);
                ekle.Parameters.AddWithValue("@p8", OutLocalGlobalId);
                ekle.Parameters.AddWithValue("@p9", 1);
                ekle.Parameters.AddWithValue("@p10", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p11", OutLocalIlkKullaniciKod);
                ekle.Parameters.AddWithValue("@p12", OutLocalSonKullaniciKod);
                ekle.Parameters.AddWithValue("@p13", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p14", SimdikiZamaniAl);
                ekle.Parameters.AddWithValue("@p15", false);
                ekle.Parameters.AddWithValue("@p16", LocalHizmetYerID);
                ekle.ExecuteNonQuery();
                cnn.Close();
            }
            catch
            {
                MessageBox.Show("Hata.");
            }
        }

        private void OtoparkAlanCB_TextChanged(object sender, EventArgs e)
        {
            BobOtoParkMainForm.LocalAlanAdi = OtoparkAlanCB.Text;
            IstasyonCBDoldur();
        }

        private void OtoparkIstasyonCB_TextChanged(object sender, EventArgs e)
        {
            BobOtoParkMainForm.LocalIstasyonAdi = OtoparkIstasyonCB.Text;
            //-- Otopark Kapasite Bul
            OtoParkAlanKapasiteBul();
            //-- Ýçerdeki Araç Sayýsýný bul
            OtoparktakiAracSayisiBul();
            //-- Kamera Bilgilerini al
            //-- Bariyer Bilgilerini al
            //-- Araç grubuna göre Tarife belirlenecek
        }
    }
}