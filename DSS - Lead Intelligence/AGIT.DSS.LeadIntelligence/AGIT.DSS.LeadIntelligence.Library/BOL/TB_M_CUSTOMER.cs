using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class TB_M_CUSTOMER : ApplicationObject, IApplicationObject
    {
        public Int64 ID { get; set; }
        public String NoFaktur { get; set; }
        public Int32 DealerID { get; set; }
        public String DealerCode { get; set; }
        public DateTime SalesDate { get; set; }
        public DateTime TglMohon { get; set; }
        public String Nama { get; set; }
        public String NoKTP { get; set; }
        public String KodeCustomer { get; set; }
        public String CustID { get; set; }
        public String NoRangka { get; set; }
        //public String KodeMesin { get; set; }
        public String NoMesin { get; set; }
        public String TipeMotor { get; set; }
        //public String Warna { get; set; }
        public String JenisKelamin { get; set; }
        public DateTime TanggalLahir { get; set; }
        public String Alamat { get; set; }
        public String Kelurahan { get; set; }
        public String Kecamatan { get; set; }
        public String Kota { get; set; }
        public String KodePos { get; set; }
        public String Provinsi { get; set; }
        public String Email { get; set; }
        //public String JenisPenjualanSTNK { get; set; }
        //public String JenisPenjualanSSU { get; set; }
        public String CashCredit { get; set; }
        public String FinanceCompany { get; set; }
        //public String BesarDP { get; set; }
        //public String BesarCicilan { get; set; }
        //public String LamaCicilan { get; set; }
        public String Agama { get; set; }
        public String Pekerjaan { get; set; }
        public String Pengeluaran { get; set; }
        public String Pendidikan { get; set; }
        public String StatusNoHP { get; set; }
        public String NoTelp { get; set; }
        public String NoHP { get; set; }
        public String Umur { get; set; }
        public String RangeUmur { get; set; }
        public String KebersediaanDihubungi { get; set; }
        //public String Jenis3 { get; set; }
        //public String Jenis6 { get; set; }
        //public String MerkMotorSebelumnya { get; set; }
        //public String DigunakanUntuk { get; set; }
        public Int16 Pemakai { get; set; }
        public String KodeSalesPerson { get; set; }
        //public String SalesPerson { get; set; }
        //public String TanggalMasukData { get; set; }
        //public String StatusValidasi { get; set; }
        //public DateTime TanggalBeliSebelumnya { get; set; }
        //public Int32 BuyingCycle { get; set; }
        //public String TipeSebelumnya { get; set; }
        //public String Jenis3Sebelumnya { get; set; }
        //public String Jenis6Sebelumnya { get; set; }
        //public DateTime TanggalBeliSelanjutnya { get; set; }
        //public String Jenis6Selanjutnya { get; set; }
        //public String Facebook { get; set; }
        //public String Twitter { get; set; }
        //public String Instagram { get; set; }
        //public String Youtube { get; set; }
        public Boolean RowStatus { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual bool Load(IDataReader dr)
        {
            this.IsValid = false;

            this.ID = DBUtil.GetLongField(dr, "ID");
            //this.CustID = DBUtil.GetCharField(dr, "UserID");
            //this.NoKTP = DBUtil.GetCharField(dr, "Process");
            //this.TanggalLahir = DBUtil.GetDateTimeField(dr, "Filename");
            //this.JenisKelamin = DBUtil.GetCharField(dr, "Start");
            //this.Alamat = DBUtil.GetCharField(dr, "Finish");
            //this.Kelurahan = DBUtil.GetCharField(dr, "Status");
            //this.Kecamatan = DBUtil.GetCharField(dr, "Note");
            //this.Kota = DBUtil.GetCharField(dr, "Kota");
            //this.Provinsi = DBUtil.GetCharField(dr, "Provinsi");
            //this.CreatedBy = DBUtil.GetCharField(dr, "CreatedBy");
            //this.CreatedDate = DBUtil.GetDateTimeField(dr, "CreatedDate");
            //this.LastModifiedBy = DBUtil.GetCharField(dr, "LastModifiedBy");
            //this.LastModifiedDate = DBUtil.GetDateTimeField(dr, "LastModifiedDate");
            //this.RowStatus = DBUtil.GetBoolField(dr, "RowStatus");
            //this.Agama = DBUtil.GetCharField(dr, "Agama");
            //this.DealerID = DBUtil.GetIntField(dr, "DealerID");
            //this.KodeSalesPerson = DBUtil.GetCharField(dr, "SalesPerson");
            //this.NoFaktur = DBUtil.GetCharField(dr, "NoFaktur");
            //this.NoHP = DBUtil.GetCharField(dr, "NoHP");
            //this.StatusNoHP = DBUtil.GetCharField(dr, "StatusHP");
            //this.Pendidikan = DBUtil.GetCharField(dr, "Pendidikan");
            //this.Pekerjaan = DBUtil.GetCharField(dr, "Pekerjaan");
            //this.Pengeluaran = DBUtil.GetCharField(dr, "Pengeluaran");
            //this.Email = DBUtil.GetCharField(dr, "Email");


            this.IsValid = true;
            return this.IsValid;
        }
    }
}
