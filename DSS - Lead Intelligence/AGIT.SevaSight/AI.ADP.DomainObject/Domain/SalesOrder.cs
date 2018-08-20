using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agit.Sevasight.Models
{

    public class SalesOrder
    {
        public int ID { get; set; }
        public int DealerID { get; set; }
        public int CustomerID { get; set; }
        public string KodeDealer { get; set; }
        public string Nama { get; set; }
        public string NoKTP { get; set; }
        public DateTime TanggalCetak { get; set; }
        public DateTime TanggalMohon { get; set; }
        public string NoFaktur { get; set; }
        public string NoRangka { get; set; }
        public string KodeMesin { get; set; }
        public string NoMesin { get; set; }
        public string TipeMotor { get; set; }
        public string Warna { get; set; }
        public string JenisKelamin { get; set; }
        public DateTime TanggalLahir { get; set; }
        public string Alamat { get; set; }
        public string Kelurahan { get; set; }
        public string Kecamatan { get; set; }
        public string Kota { get; set; }
        public string KodePos { get; set; }
        public string Provinsi { get; set; }
        public string Email { get; set; }
        public string CashCredit { get; set; }
        public string FinanceCompany { get; set; }
        public string UangMuka { get; set; }
        public string UangMukaAktual { get; set; }
        public string Cicilan { get; set; }
        public string TenorBulan { get; set; }
        public string TenorTahun { get; set; }
        public string Agama { get; set; }
        public string Pekerjaan { get; set; }
        public string Pengeluaran { get; set; }
        public string Pendidikan { get; set; }
        public string StatusHP { get; set; }
        public string NoTelp { get; set; }
        public string NoHP { get; set; }
        public string Umur { get; set; }
        public string RangeUmur { get; set; }
        public string KebersediaanDihubungi { get; set; }
        public string Jenis3 { get; set; }
        public string Jenis6 { get; set; }
        public string MerkSebelumnya { get; set; }
        public string JenisSebelumnya { get; set; }
        public string Fungsi { get; set; }
        public string Pemakai { get; set; }
        public string SalesPerson { get; set; }
        public DateTime TanggalVerifikasi { get; set; }
        public string StatusVerifikasi { get; set; }
        public string Hobi { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string PIC { get; set; }
        public string StatusRumah { get; set; }
        public string TipeATM { get; set; }
        public string TipeVarPlus { get; set; }
        public string NoCustomer { get; set; }
        public string ROType { get; set; }
        public string ROData { get; set; }
        public string NoAnggota { get; set; }
        public string Region { get; set; }
        public string Remark { get; set; }
        public string JenisSales { get; set; }
        public int ROIndex { get; set; }
        public int ROCount { get; set; }
        public DateTime UpdateRoIndexOn { get; set; }
        public DateTime TanggalBeliSebelumnya { get; set; }
        public string TipeSebelumnya { get; set; }
        public string Jenis3Sebelumnya { get; set; }
        public string Jenis6Sebelumnya { get; set; }
        public int BuyingCycleMonth { get; set; }
        public int BuyingCycleYear { get; set; }
        public DateTime UpdateSebelumnyaOn { get; set; }
        public int MLJenisKe { get; set; }
        public DateTime MLJenisUpdateOn { get; set; }
        public string Jenis6Selanjutnya { get; set; }
        public int MLPeriodeKe { get; set; }
        public DateTime MLPeriodeUpdateOn { get; set; }
        public DateTime TanggalBeliSelanjutnya { get; set; }
        public int LogID { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TanggalBeliSelanjutnyaActual { get; set; }
        public string TipeSelanjutnyaActual { get; set; }
        public string Jenis3SelanjutnyaActual { get; set; }
        public string Jenis6SelanjutnyaActual { get; set; }
        public int BuyingCycleMonthSelanjutnyaActual { get; set; }
        public int BuyingCycleYearSelanjutnyaActual { get; set; }
        public DateTime UpdateSelanjutnyaActualOn { get; set; }
        public string SeriesSelanjutnyaActual { get; set; }
        public string SeriesSebelumnya { get; set; }
        public string SeriesLead { get; set; }
        public int statuslead { get; set; }
        public string AppointmentSchedule { get; set; }
        public string Note { get; set; }

    }



}