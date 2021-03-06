﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace QRTech.Models
{
    public static class AdminDatabase
    {
        public static List<Driver> _drivers = new List<Driver>();
        public static List<Vehicle> _vehicles= new List<Vehicle>();
        public static List<Line> _lines = new List<Line>();
        public static List<Station> _station = new List<Station>();
        public static Admin _admin;
        private static sql sql = new sql();

        public static Admin GetAdmin
        {
            get { return _admin; }
        }
        private static void DriverList()
        {
            _drivers.Clear();
            SqlCommand driverGet = new SqlCommand("exec soforList @p1", sql.baglanti());
            driverGet.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtDriver = driverGet.ExecuteReader();
            while (dtDriver.Read())
            {
                Driver driver = new Driver();
                driver.soforID = Convert.ToInt32(dtDriver[0]);
                driver.AD = dtDriver[1].ToString();
                driver.Soyad = dtDriver[2].ToString();
                driver.TC = dtDriver[3].ToString();
                driver.ehliyetSeriNo = Convert.ToInt32(dtDriver[4]);
                driver.Maas = Convert.ToInt32(dtDriver[5]);
                driver.Telefon =dtDriver[6].ToString();
                driver.Mail = dtDriver[7].ToString();
                driver.Adres = dtDriver[8].ToString();
                driver.AracPlaka = dtDriver[9].ToString();
                _drivers.Add(driver);
            }

            SqlCommand driverGet2 = new SqlCommand("exec soforBosList @p1", sql.baglanti());
            driverGet2.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtDriver2 = driverGet2.ExecuteReader();
            while (dtDriver2.Read())
            {
                Driver driver = new Driver();
                driver.soforID = Convert.ToInt32(dtDriver2[0]);
                driver.AD = dtDriver2[1].ToString();
                driver.Soyad = dtDriver2[2].ToString();
                driver.TC = dtDriver2[3].ToString();
                driver.ehliyetSeriNo = Convert.ToInt32(dtDriver2[4]);
                driver.Maas = Convert.ToInt32(dtDriver2[5]);
                driver.Telefon = dtDriver2[6].ToString();
                driver.Mail = dtDriver2[7].ToString();
                driver.Adres = dtDriver2[8].ToString();
                driver.AracPlaka = "0";
                _drivers.Add(driver);
            }
        }

        public static List<Driver>DriverListe
        {
            get { DriverList(); return _drivers; }
        }

        public static Driver DriverFind(int DriverID,int aracID)
        {
            _drivers.Clear();
            SqlCommand driverGet = new SqlCommand("exec soforBul @p1,@p2", sql.baglanti());
            driverGet.Parameters.AddWithValue("@p1", DriverID);
            driverGet.Parameters.AddWithValue("@p2", aracID);
            SqlDataReader dtDriver = driverGet.ExecuteReader();
            if (dtDriver.Read())
            {
                Driver driver = new Driver();
                driver.soforID = Convert.ToInt32(dtDriver[0]);
                driver.AD = dtDriver[1].ToString();
                driver.Soyad = dtDriver[2].ToString();
                driver.TC = dtDriver[3].ToString();
                driver.ehliyetSeriNo = Convert.ToInt32(dtDriver[4]);
                driver.Maas = Convert.ToInt32(dtDriver[5]);
                driver.Telefon = dtDriver[6].ToString();
                driver.Mail = dtDriver[7].ToString();
                driver.Adres = dtDriver[8].ToString();
                driver.AracPlaka = dtDriver[9].ToString();
                return driver;
            }
            else
                return null;
        }
        public static int DriverNullFind(int DriverID)
        {
            SqlCommand driverFind = new SqlCommand("select aracID from TBL_Sofor where soforID =@p1", sql.baglanti());
            driverFind.Parameters.AddWithValue("@p1", DriverID);
            SqlDataReader dtDriverFind = driverFind.ExecuteReader();
            if (dtDriverFind.Read())
            {
                return Convert.ToInt32(dtDriverFind[0]);
            }
            else
                return 0;
        }

        private static void VehicleList()
        {
            _vehicles.Clear();
            SqlCommand vehicleGet = new SqlCommand("exec aracList @p1", sql.baglanti());
            vehicleGet.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtVehicle = vehicleGet.ExecuteReader();
            while (dtVehicle.Read())
            {
                Vehicle vehicle = new Vehicle();
                vehicle.aracID=Convert.ToInt32(dtVehicle[0]);
                vehicle.Plaka = dtVehicle[1].ToString();
                vehicle.engelliDestegi = Convert.ToBoolean(dtVehicle[2]);
                vehicle.saseNumarasi = dtVehicle[3].ToString();
                vehicle.AracMarka = dtVehicle[4].ToString();
                vehicle.Model = dtVehicle[5].ToString();
                vehicle.Renk = dtVehicle[6].ToString();
                vehicle.HatNo = Convert.ToInt32(dtVehicle[7]);
                _vehicles.Add(vehicle);
            }

            SqlCommand vehicleGet2 = new SqlCommand("exec aracBosList @p1", sql.baglanti());
            vehicleGet2.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtVehicle2 = vehicleGet2.ExecuteReader();
            while (dtVehicle2.Read())
            {
                Vehicle vehicle = new Vehicle();
                vehicle.aracID = Convert.ToInt32(dtVehicle2[0]);
                vehicle.Plaka = dtVehicle2[1].ToString();
                vehicle.engelliDestegi = Convert.ToBoolean(dtVehicle2[2]);
                vehicle.saseNumarasi = dtVehicle2[3].ToString();
                vehicle.AracMarka = dtVehicle2[4].ToString();
                vehicle.Model = dtVehicle2[5].ToString();
                vehicle.Renk = dtVehicle2[6].ToString();
                vehicle.HatNo = 0;
                _vehicles.Add(vehicle);
            }

        }
        public static List<Vehicle> VehicleListe
        {
            get { VehicleList(); return _vehicles; }
        }
        public static int VehicleLineID(int aracID)
        {
            int hatID = 0;
            SqlCommand lineIDGet = new SqlCommand("Select hatID from TBL_Arac where aracID= @p1", sql.baglanti());
            lineIDGet.Parameters.AddWithValue("@p1", aracID);
            SqlDataReader dtLineID = lineIDGet.ExecuteReader();
            if (dtLineID.Read())
            {
                if(DBNull.Value.Equals(dtLineID[0]))
                {
                    hatID = 0;
                }
                else
                {
                    hatID = Convert.ToInt32(dtLineID[0]);
                }
                return hatID;
            }
            else
                return hatID;
        }

        public static Vehicle VehicleFind(int aracID,int hatID)
        {
            SqlCommand vehicleFind = new SqlCommand("exec aracBul @p1,@p2", sql.baglanti());
            vehicleFind.Parameters.AddWithValue("@p1", aracID);
            vehicleFind.Parameters.AddWithValue("@p2", hatID);
            SqlDataReader dtVehicle = vehicleFind.ExecuteReader();
            if (dtVehicle.Read())
            {
                Vehicle vehicle = new Vehicle();
                vehicle.aracID = Convert.ToInt32(dtVehicle[0]);
                vehicle.Plaka = dtVehicle[1].ToString();
                vehicle.engelliDestegi = Convert.ToBoolean(dtVehicle[2]);
                vehicle.saseNumarasi = dtVehicle[3].ToString();
                vehicle.AracMarka = dtVehicle[4].ToString();
                vehicle.Model = dtVehicle[5].ToString();
                vehicle.Renk = dtVehicle[6].ToString();
                if(DBNull.Value.Equals((dtVehicle[7])) )
                {
                    vehicle.HatNo = 0;
                }
                else
                {
                    vehicle.HatNo = Convert.ToInt32(dtVehicle[7]);
                }

                return vehicle;
            }
            else
                return null;
        }
        private static void LineList()
        {
            _lines.Clear();
            SqlCommand lineGet = new SqlCommand("exec hatList @p1", sql.baglanti());
            lineGet.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtLine = lineGet.ExecuteReader();
            while (dtLine.Read())
            {
                Line line = new Line();
                line.HatID = Convert.ToInt32(dtLine[0]);
                line.HatNo = Convert.ToInt32(dtLine[1]);
                line.BaslangicDurak = dtLine[2].ToString();
                line.BitisDurak = dtLine[3].ToString();
                line.ilAD = dtLine[4].ToString();
                line.ogrenciFiyat = Convert.ToInt32(dtLine[5]);
                line.tamFiyat= Convert.ToInt32(dtLine[6]);
                _lines.Add(line);
            }
            
        }
        public static void NullLineDelete(int aracID)
        {
            SqlCommand NullDelete = new SqlCommand("Delete from TBL_Boshat where aracID=@p1 ", sql.baglanti());
            NullDelete.Parameters.AddWithValue("@p1", aracID);
            NullDelete.ExecuteNonQuery();
            sql.baglanti().Close();
        }


        public static List<Line> LineListe
        {
            get { LineList(); return _lines; }
        }
        public static Line LineFind(int hatID)
        {
            SqlCommand lineFind = new SqlCommand("exec hatBul @p1", sql.baglanti());
            lineFind.Parameters.AddWithValue("@p1", hatID);
            SqlDataReader dtLine = lineFind.ExecuteReader();
            if (dtLine.Read())
            {
                Line line = new Line();
                line.HatID = Convert.ToInt32(dtLine[0]);
                line.HatNo = Convert.ToInt32(dtLine[1]);
                line.BaslangicDurak = dtLine[2].ToString();
                line.BitisDurak = dtLine[3].ToString();
                line.ilAD = dtLine[4].ToString();
                line.ogrenciFiyat = Convert.ToInt32(dtLine[5]);
                line.tamFiyat = Convert.ToInt32(dtLine[6]);
                return line;
            }
            else
                return null;
        }

        private static void ProvinceTownStationList()
        {
            _station.Clear();
            Province province = new Province();
            SqlCommand ProvinceGet = new SqlCommand("select D.ilceID from TBL_Durak D inner join TBL_Ilce I on D.ilceID = I.ilceID  where I.ilID = @p1 group by D.ilceID", sql.baglanti());
            ProvinceGet.Parameters.AddWithValue("@p1", _admin.ilID);
            SqlDataReader dtTown = ProvinceGet.ExecuteReader();
            while (dtTown.Read())
            {
                Town town = new Town();
                town.ilceID = Convert.ToInt32(dtTown[0]);
                province.Town.Add(town);    

            }
            foreach (Town town in province.Town)
            {
                SqlCommand StationGet = new SqlCommand("select D.durakID,D.durakAdi,I.ilceAdi,D.enlem,D.boylam from TBL_Durak D inner join TBL_Ilce I on I.ilceID = D.ilceID where D.ilceID =@p1", sql.baglanti());
                StationGet.Parameters.AddWithValue("@p1", town.ilceID);
                SqlDataReader dtStation = StationGet.ExecuteReader();
                while (dtStation.Read())
                {
                    Station station = new Station();
                    station.durakID = Convert.ToInt32(dtStation[0]);
                    station.durakAD = dtStation[1].ToString();
                    station.ilceAdi = dtStation[2].ToString();
                    station.enlem = Convert.ToInt32(dtStation[3]);
                    station.boylam = Convert.ToInt32(dtStation[4]);
                    _station.Add(station);

                }
            }
        }

        public static List<Station> StationListe
        {
            get { ProvinceTownStationList(); return _station; }
        }

        public static Station StationFind(int stationID)
        {
            SqlCommand StationFind = new SqlCommand("select D.durakID,D.durakAdi,I.ilceAdi,D.enlem,D.boylam from TBL_Durak D inner join TBL_Ilce I on I.ilceID = D.ilceID where D.durakID =@p1", sql.baglanti());
            StationFind.Parameters.AddWithValue("@p1", stationID);
            SqlDataReader dtStation = StationFind.ExecuteReader();
            if (dtStation.Read())
            {
                Station station = new Station();
                station.durakID = Convert.ToInt32(dtStation[0]);
                station.durakAD = dtStation[1].ToString();
                station.ilceAdi = dtStation[2].ToString();
                station.enlem = Convert.ToInt32(dtStation[3]);
                station.boylam = Convert.ToInt32(dtStation[4]);
                _station.Add(station);
                return station;
            }
            else
                return null;
        }
        public static void LineAdd(int HatNo, string BaslangicDurak, string BitisDurak, float ogrenciFiyat, float tamFiyat)
        {
            SqlCommand lineAdd = new SqlCommand("insert into TBL_Fiyatlar (ogrenciFiyat,ogrenciOran,tamFiyat,tamOran,engelliFiyat,engelliOran,yasliFiyat,yasliOran,ilID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", sql.baglanti());
            lineAdd.Parameters.AddWithValue("@p1", ogrenciFiyat);
            lineAdd.Parameters.AddWithValue("@p2", 1);
            lineAdd.Parameters.AddWithValue("@p3", tamFiyat);
            lineAdd.Parameters.AddWithValue("@p4", 1);
            lineAdd.Parameters.AddWithValue("@p5", 0);
            lineAdd.Parameters.AddWithValue("@p6", 1);
            lineAdd.Parameters.AddWithValue("@p7", 0);
            lineAdd.Parameters.AddWithValue("@p8", 1);
            lineAdd.Parameters.AddWithValue("@p9", 1);
            lineAdd.ExecuteNonQuery();
            sql.baglanti().Close();

            int fiyatID =0;
            SqlCommand lineFiyat = new SqlCommand("select fiyatID from TBL_Fiyatlar order by fiyatID desc", sql.baglanti());
            SqlDataReader dtLineFiyat = lineFiyat.ExecuteReader();
            if (dtLineFiyat.Read())
            {
                fiyatID = Convert.ToInt32(dtLineFiyat[0]);
            }

            SqlCommand lineDurak = new SqlCommand("(SELECT durakID from TBL_Durak where durakAdi =@p1)", sql.baglanti());
            lineDurak.Parameters.AddWithValue("@p1", BaslangicDurak);
            SqlDataReader dtLineDurak = lineDurak.ExecuteReader();
            while (dtLineDurak.Read())
            {
               BaslangicDurak = dtLineDurak[0].ToString();
            }

            SqlCommand lineDurak1 = new SqlCommand("(SELECT durakID from TBL_Durak where durakAdi =@p1)", sql.baglanti());
            lineDurak1.Parameters.AddWithValue("@p1", BitisDurak);
            SqlDataReader dtLineDurak1 = lineDurak1.ExecuteReader();
            while (dtLineDurak1.Read())
            {
                BitisDurak = dtLineDurak1[0].ToString();
            }

            SqlCommand lineAdd1 = new SqlCommand("insert into TBL_Hat (hatAdi,kazanilanTutar,fiyatID,baslangicDurak,bitisDurak,ilID) values (@p1,@p2,@p3,@p4,@p5,@p6)", sql.baglanti());
            lineAdd1.Parameters.AddWithValue("@p1", HatNo);
            lineAdd1.Parameters.AddWithValue("@p2", 0);
            lineAdd1.Parameters.AddWithValue("@p3", fiyatID);
            lineAdd1.Parameters.AddWithValue("@p4", BaslangicDurak);
            lineAdd1.Parameters.AddWithValue("@p5", BitisDurak);
            lineAdd1.Parameters.AddWithValue("@p6", 1);
            lineAdd1.ExecuteNonQuery();
            sql.baglanti().Close();
        }
        public static void LineUpdate(int HatID, int HatNo, string BaslangicDurak, string BitisDurak, float ogrenciFiyat, float tamFiyat)
        {
            SqlCommand lineUpdate = new SqlCommand("update TBL_Fiyatlar set ogrenciFiyat= @p2 , tamFiyat =@p3 where fiyatID=(select fiyatID from TBL_Hat where hatID=@p1)", sql.baglanti());
            lineUpdate.Parameters.AddWithValue("@p1", HatID);
            lineUpdate.Parameters.AddWithValue("@p2", ogrenciFiyat);
            lineUpdate.Parameters.AddWithValue("@p3", tamFiyat);
            lineUpdate.ExecuteNonQuery();
            sql.baglanti().Close();

            SqlCommand lineUpdate1 = new SqlCommand("update TBL_Hat set hatAdi = @p2 ,baslangicDurak =(SELECT durakID from TBL_Durak where durakAdi =@p3),bitisDurak =(SELECT durakID from TBL_Durak where durakAdi =@p4) where hatID = @p1", sql.baglanti());
            lineUpdate1.Parameters.AddWithValue("@p1", HatID);
            lineUpdate1.Parameters.AddWithValue("@p2", HatNo);
            lineUpdate1.Parameters.AddWithValue("@p3", BaslangicDurak);
            lineUpdate1.Parameters.AddWithValue("@p4", BitisDurak);
            lineUpdate1.ExecuteNonQuery();
            sql.baglanti().Close();
        }
        public static void LineDelete(Line Entity)
        {
            SqlCommand nullLine = new SqlCommand("select aracID from TBL_Arac where hatID = @p1 ", sql.baglanti());
            nullLine.Parameters.AddWithValue("@p1", Entity.HatID);
            SqlDataReader dtnullLine = nullLine.ExecuteReader();
            while (dtnullLine.Read())
            {
                SqlCommand boshatAdd = new SqlCommand("insert into TBL_Boshat (aracID,ilID) values (@p1,@p2)", sql.baglanti());
                boshatAdd.Parameters.AddWithValue("@p1", Convert.ToInt32(dtnullLine[0]));
                boshatAdd.Parameters.AddWithValue("@p2", _admin.ilID);
                boshatAdd.ExecuteNonQuery();
                sql.baglanti().Close();
            }

            SqlCommand lineDelete2 = new SqlCommand("delete from TBL_Hat where hatID =@p1 ", sql.baglanti());
            lineDelete2.Parameters.AddWithValue("@p1", Entity.HatID);
            lineDelete2.ExecuteNonQuery();
            sql.baglanti().Close();
        }


        

        public static void StationAdd(Station Entity)
        {
            int ilceID =0;
            SqlCommand Station = new SqlCommand("select ilceID from TBL_Ilce where ilceAdi = @p1 ", sql.baglanti());
            Station.Parameters.AddWithValue("@p1", Entity.ilceAdi);
            SqlDataReader dtStation = Station.ExecuteReader();
            while (dtStation.Read())
            {
                ilceID = Convert.ToInt32(dtStation[0]);
            }

            SqlCommand StationAdd = new SqlCommand("insert into TBL_Durak (durakAdi,ilceID,enlem,boylam) values (@p1,@p2,@p3,@p4)", sql.baglanti());
            StationAdd.Parameters.AddWithValue("@p1", Entity.durakAD);
            StationAdd.Parameters.AddWithValue("@p2", ilceID);
            StationAdd.Parameters.AddWithValue("@p3", Entity.enlem);
            StationAdd.Parameters.AddWithValue("@p4", Entity.boylam);
            StationAdd.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void StationUpdate(Station Entity)
        {
            SqlCommand StationUpdate = new SqlCommand("update TBL_Durak set durakAdi=@p2,ilceID=(select ilceID from TBL_Ilce where ilceAdi = @p3),enlem=@p4,boylam = @p5 where durakID = @p1 ", sql.baglanti());
            StationUpdate.Parameters.AddWithValue("@p1", Entity.durakID);
            StationUpdate.Parameters.AddWithValue("@p2", Entity.durakAD);
            StationUpdate.Parameters.AddWithValue("@p3", Entity.ilceAdi);
            StationUpdate.Parameters.AddWithValue("@p4", Entity.enlem);
            StationUpdate.Parameters.AddWithValue("@p5", Entity.boylam);
            StationUpdate.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void LineDiscount(Price Entity)
        {
            SqlCommand lineAdd = new SqlCommand("update TBL_Fiyatlar set tamOran =@p2,ogrenciOran =@p3 where ilID=@p1", sql.baglanti());
            lineAdd.Parameters.AddWithValue("@p1", 1);
            lineAdd.Parameters.AddWithValue("@p2", Entity.tamOran);
            lineAdd.Parameters.AddWithValue("@p3", Entity.ogrenciOran);
            lineAdd.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void VehicleAdd(Vehicle Entity)
        {
            SqlCommand Vehicle = new SqlCommand("select Mo.modelID from TBL_AracModel Mo inner join TBL_AracMarka Ma  ON Ma.markaID = Mo.markaID where  markaAdi =@p1 and modelAdi =@p2 ", sql.baglanti());
            Vehicle.Parameters.AddWithValue("@p1", Entity.AracMarka);
            Vehicle.Parameters.AddWithValue("@p2", Entity.Model);
            SqlDataReader dtVehicle = Vehicle.ExecuteReader();
            while (dtVehicle.Read())
            {
                Entity.Model = dtVehicle[0].ToString();
            }

            SqlCommand Vehicle1 = new SqlCommand("select renkID from TBL_Renk where renk = @p1", sql.baglanti());
            Vehicle1.Parameters.AddWithValue("@p1", Entity.Renk);
            SqlDataReader dtVehicle1 = Vehicle1.ExecuteReader();
            while (dtVehicle1.Read())
            {
                Entity.Renk = dtVehicle1[0].ToString();
            }

            int hatID = 0; 
            SqlCommand Vehicle2 = new SqlCommand("select hatID from TBL_Hat where hatAdi = @p1 ", sql.baglanti());
            Vehicle2.Parameters.AddWithValue("@p1", Entity.HatNo);
            SqlDataReader dtVehicle2 = Vehicle2.ExecuteReader();
            while (dtVehicle2.Read())
            {
                hatID = Convert.ToInt32(dtVehicle2[0]);
            }

            SqlCommand VehicleAdd = new SqlCommand("insert into TBL_Arac (plaka,saseNumarasi,engelliDestegi,modelID,renkID,hatID) values (@p1,@p2,@p3,@p4,@p5,@p6) ", sql.baglanti());
            VehicleAdd.Parameters.AddWithValue("@p1", Entity.Plaka);
            VehicleAdd.Parameters.AddWithValue("@p2", Entity.saseNumarasi);
            VehicleAdd.Parameters.AddWithValue("@p3", Entity.engelliDestegi);
            VehicleAdd.Parameters.AddWithValue("@p4", Entity.Model);
            VehicleAdd.Parameters.AddWithValue("@p5", Entity.Renk);
            VehicleAdd.Parameters.AddWithValue("@p6", hatID);
            VehicleAdd.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void VehicleUpdate(Vehicle Entity)
        {
            SqlCommand VehicleUpdate = new SqlCommand("exec aracUpdate @p2,@p3,@p4,@p5,@p6,@p7,@p8,@p1", sql.baglanti());
            VehicleUpdate.Parameters.AddWithValue("@p1", Entity.aracID);
            VehicleUpdate.Parameters.AddWithValue("@p2", Entity.Plaka);
            VehicleUpdate.Parameters.AddWithValue("@p3", Entity.saseNumarasi);
            VehicleUpdate.Parameters.AddWithValue("@p4", Entity.engelliDestegi);
            VehicleUpdate.Parameters.AddWithValue("@p5", Entity.AracMarka);
            VehicleUpdate.Parameters.AddWithValue("@p6", Entity.Model);
            VehicleUpdate.Parameters.AddWithValue("@p7", Entity.Renk);
            VehicleUpdate.Parameters.AddWithValue("@p8", Entity.HatNo);
            VehicleUpdate.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void VehicleDelete(Vehicle Entity)
        {
            SqlCommand VehicleDelete = new SqlCommand("delete from TBL_Arac where AracID=@p1 ", sql.baglanti());
            VehicleDelete.Parameters.AddWithValue("@p1", Entity.aracID);
            VehicleDelete.ExecuteNonQuery();
            sql.baglanti().Close();

            SqlCommand VehicleDelete2 = new SqlCommand("select soforID from TBL_Sofor where aracID=@p1 ", sql.baglanti());
            VehicleDelete2.Parameters.AddWithValue("@p1", Entity.aracID);
            SqlDataReader dtVehicleDelete2 = VehicleDelete2.ExecuteReader();
            while (dtVehicleDelete2.Read())
            {
                SqlCommand VehicleDelete3 = new SqlCommand("update TBL_Sofor set aracID = 0 where soforID=@p2", sql.baglanti());
                VehicleDelete3.Parameters.AddWithValue("@p2", Convert.ToInt32(dtVehicleDelete2[0]));
                VehicleDelete3.ExecuteNonQuery();
                sql.baglanti().Close();
            }
        }

        public static void DriverAdd(Driver Entity)
        {
            int iletisimID = 0;
            SqlCommand DriverAdd = new SqlCommand("insert into TBL_IletisimBilgi (telefonNo,mail,adres) values(@p1,@p2,@p3) ", sql.baglanti());
            DriverAdd.Parameters.AddWithValue("@p1", Entity.Telefon);
            DriverAdd.Parameters.AddWithValue("@p2", Entity.Mail);
            DriverAdd.Parameters.AddWithValue("@p3", Entity.Adres);
            DriverAdd.ExecuteNonQuery();
            sql.baglanti().Close();

            SqlCommand Driver = new SqlCommand("select iletisimID from TBL_IletisimBilgi order by iletisimID desc ", sql.baglanti());
            SqlDataReader dtDriver = Driver.ExecuteReader();
            if (dtDriver.Read())
            {
                iletisimID = Convert.ToInt32(dtDriver[0]);
            }

            int aracID = 0;
            SqlCommand Driver2 = new SqlCommand("select aracID from TBL_Arac where plaka =@p1 ", sql.baglanti());
            Driver2.Parameters.AddWithValue("@p1", Entity.AracPlaka);
            SqlDataReader dtDriver2 = Driver2.ExecuteReader();
            while (dtDriver2.Read())
            {
                aracID = Convert.ToInt32(dtDriver2[0]);
            }

            SqlCommand DriverAdd2 = new SqlCommand("insert into TBL_Sofor (isim,soyisim,tc,ehliyetSeriNo,Maas,iletisimID,aracID,ilID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", sql.baglanti());
            DriverAdd2.Parameters.AddWithValue("@p1", Entity.AD);
            DriverAdd2.Parameters.AddWithValue("@p2", Entity.Soyad);
            DriverAdd2.Parameters.AddWithValue("@p3", Entity.TC);
            DriverAdd2.Parameters.AddWithValue("@p4", Entity.ehliyetSeriNo);
            DriverAdd2.Parameters.AddWithValue("@p5", Entity.Maas);
            DriverAdd2.Parameters.AddWithValue("@p6", iletisimID);
            DriverAdd2.Parameters.AddWithValue("@p7", aracID);
            DriverAdd2.Parameters.AddWithValue("@p8", _admin.ilID);
            DriverAdd2.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void DriverUpdate(Driver Entity)
        {
            SqlCommand DriverUpdate = new SqlCommand("update TBL_Sofor set Maas=@p2 , aracID = (select aracID from TBL_Arac where plaka=@p3) where soforID=@p1 ", sql.baglanti());
            DriverUpdate.Parameters.AddWithValue("@p1", Entity.soforID);
            DriverUpdate.Parameters.AddWithValue("@p2", Entity.Maas);
            DriverUpdate.Parameters.AddWithValue("@p3", Entity.AracPlaka);

            DriverUpdate.ExecuteNonQuery();
            sql.baglanti().Close();

            SqlCommand DriverUpdate2 = new SqlCommand("update TBL_IletisimBilgi set telefonNo=@p2,mail=@p3,adres=@p4 where iletisimID = (select iletisimID from TBL_Sofor where soforID=@p1) ", sql.baglanti());
            DriverUpdate2.Parameters.AddWithValue("@p1", Entity.soforID);
            DriverUpdate2.Parameters.AddWithValue("@p2", Entity.Telefon);
            DriverUpdate2.Parameters.AddWithValue("@p3", Entity.Mail);
            DriverUpdate2.Parameters.AddWithValue("@p4", Entity.Adres);
            DriverUpdate2.ExecuteNonQuery();
            sql.baglanti().Close();
        }

        public static void DriverDelete(Driver Entity)
        {
            SqlCommand DriverDelete = new SqlCommand("delete from Tbl_Sofor where SoforID=@p1 ", sql.baglanti());
            DriverDelete.Parameters.AddWithValue("@p1", Entity.soforID);
            DriverDelete.ExecuteNonQuery();
            sql.baglanti().Close();
        }
        public static bool AdminControl(Admin Entity)
        {
            bool varYok = false;
            SqlCommand AdminControl = new SqlCommand("select * from TBL_Yonetici where  kullaniciAdi = @p1 and sifre=@p2", sql.baglanti());
            AdminControl.Parameters.AddWithValue("@p1", Entity.kullaniciAdi);
            AdminControl.Parameters.AddWithValue("@p2", Entity.Sifre);
            SqlDataReader dtAdmin = AdminControl.ExecuteReader();
            if (dtAdmin.Read())
            {
                Admin admin = new Admin();
                admin.kullaniciAdi = dtAdmin[1].ToString();
                admin.Sifre = dtAdmin[2].ToString();
                admin.ilID = Convert.ToInt32(dtAdmin[3].ToString());
                _admin = admin;
                return varYok = true;
            }
            else
            {
                return varYok = false;
            }
        }

    }
}