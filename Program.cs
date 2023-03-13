
using System.Text;

public class Program
{

    public class ObjectReturn
    {
        public double BHXH;
        public double thue;
        public double luong;
    }

    public class ExTest
    {

       


        /// <summary>
        /// Thuế thu nhập cá nhân
        ///  https://www.topcv.vn/tinh-luong-gross-net
        /// </summary>
        /// <param name="thuNhap">Thu nhập Gross</param>
        /// <param name="vung">Nhóm vùng đang làm việc</param>SSS
        /// <param name="nguoiPhuThuoc">số lượng người phụ thuộc</param>
        /// <returns>đối tượng được định nghĩa bao gồm thuế,lương Net, tiền khấu trừ BHXH</returns>
        public ObjectReturn TinhLuongNet(double thuNhap, int vung=1, int nguoiPhuThuoc=0,double mucluongdongBH=0)
        {

            double  BH=0, tnTruocThue, tnChiuThue, thueTN = 0, luong = 0;

            switch (vung)
            {
                case 1:
                    if (thuNhap< 4680000 )
                    {
                        Console.WriteLine("Mức lương không đủ điều kiện để đóng bảo hiểm xã hội");                    
                    }
                    else
                    {
                        BH = BHXH(thuNhap, mucluongdongBH);
                    }
                    break;

                case 2:
                    if (thuNhap < 4160000)
                    {
                        Console.WriteLine("Mức lương không đủ điều kiện để đóng bảo hiểm xã hội");                      
                    }
                    else
                    {
                        BH = BHXH(thuNhap, mucluongdongBH);
                    }
                    break;

                case 3:
                    if (thuNhap < 3640000)
                    {
                        Console.WriteLine("Mức lương không đủ điều kiện để đóng bảo hiểm xã hội");                     
                    }
                    else
                    {
                        BH = BHXH(thuNhap, mucluongdongBH);
                    }
                    break;

                case 4:
                    if (thuNhap < 3250000)
                    {
                        Console.WriteLine("Mức lương không đủ điều kiện để đóng bảo hiểm xã hội");
                       
                    }
                    else
                    {
                        BH = BHXH(thuNhap, mucluongdongBH);
                    }
                    break;

                default:
                    break;
            }

            //thu nhập trước thuế
            tnTruocThue = thuNhap - BH;

            //check thu nhập chịu thuế theo người phụ thuộc
            if (nguoiPhuThuoc == 0)
            {
                tnChiuThue = tnTruocThue - 11000000;

            }
            else
            {
                tnChiuThue = tnTruocThue - 11000000 - (4400000 * nguoiPhuThuoc);

            }

            //tính thuế
            if(tnChiuThue > 0)
            {
                thueTN = TinhThue(tnChiuThue);

            }
            else
            {
                thueTN = 0;

            }

            luong = tnTruocThue - thueTN;

            return new ObjectReturn
            {
                BHXH = BH,
                thue = thueTN,
                luong = luong
            };
        }
       
        /// <summary>
        /// Tính thu nhập Gross
        /// </summary>
        /// <param name="thuNhapNet">thu nhập NET (thực nhận)</param>
        /// <param name="vung">Vùng làm việc</param>
        /// <param name="nguoiPhuThuoc">Người phụ thuộc</param>
        /// <param name="mucluongdongBH">Mức lương đóng bảo hiểm</param>
        /// <returns>Lương Gross</returns>
        public ObjectReturn TinhLuongGross(double thuNhapNet, int nguoiPhuThuoc = 0)
        
        {

            double tnChiuThue, thueTN = 0, thuNhapTruocThue = 0;

             tnChiuThue = ThuNhapTinhThue(thuNhapNet, nguoiPhuThuoc);

            //tính thuế           
            if (tnChiuThue > 0)
            {
                thueTN = TinhThue(tnChiuThue);

            }
            else
            {
                thueTN = 0;
            }

            thuNhapTruocThue = thuNhapNet + thueTN;
          
            return new ObjectReturn
            {
               
                luong = thuNhapTruocThue,
                thue = thueTN
            };
        }
        
        /// <summary>
        /// Tính thuế thu nhập cá nhân từ thu nhập chịu thuế(sau khi đã trừ đi bảo hiểm
        /// </summary>
        /// <param name="tnChiuThue"> Thu nhập chịu thuế</param>
        /// <returns>Thuế thu nhập cá nhân</returns>
        public double TinhThue(double tnChiuThue)
        {
            double thue = 0;
            if (tnChiuThue <= 5000000)
            {
                thue = tnChiuThue * 5 / 100;
                return thue;
            }
            else
            {
                int i = 0;
                double tmp = 0, tongthue =0;
                bool flag=false;
                do
                { //tính thuế theo các mức lương khác nhau
                    
                   switch (i)
                    {
                        case 0: //<5tr
                            thue = 5000000 * 5 / 100;
                            tnChiuThue = tnChiuThue - 5000000;
                            i++;
                            break;
                        //5-10tr
                        case 1:
                            tmp = tnChiuThue;
                            tnChiuThue = tnChiuThue - 5000000;
                            if (tnChiuThue < 0)
                            {
                                thue = tmp * 10 / 100;
                                tnChiuThue = tmp;
                                flag = true;
                            }
                            else
                            {
                                thue = 5000000 * 10 / 100;
                            }
                            i++;
                            break;
                         //10-18tr
                        case 2:
                            tmp = tnChiuThue;
                            tnChiuThue = tnChiuThue - 8000000;
                            if (tnChiuThue < 0)
                            {
                                thue = tmp * 15 / 100;
                                tnChiuThue = tmp;
                                flag = true;
                            }
                            else
                            {
                                thue = 8000000 * 15 / 100;
                            }
                            i++;
                            break;
                        //18-32tr

                        case 3:
                            tmp = tnChiuThue;
                            tnChiuThue = tnChiuThue - 14000000;
                            if (tnChiuThue < 0)
                            {
                                thue = tmp * 20 / 100;
                                tnChiuThue = tmp;
                                flag = true;
                            }
                            else
                            {
                                thue = 14000000 * 20 / 100;
                            }
                            i++;
                            break;
                        //32-52tr
                        case 4:

                            tmp = tnChiuThue;
                            tnChiuThue = tnChiuThue - 20000000;
                            if (tnChiuThue < 0)
                            {
                                thue = tmp * 25 / 100;
                                tnChiuThue = tmp;
                                flag = true;
                            }
                            else
                            {
                                thue = 20000000 * 25 / 100;
                            }
                            i++;
                            break;
                        //52-80tr
                        case 5:
                            tmp = tnChiuThue;
                            tnChiuThue = tnChiuThue - 28000000;
                            if (tnChiuThue < 0)
                            {
                                thue = tmp * 30 / 100;
                                tnChiuThue = tmp;
                                flag = true;
                            }
                            else
                            {
                                thue = 28000000 * 30 / 100;
                            }
                            i++;
                            break;
                        //trên 80tr
                        case 6:
                            thue = tnChiuThue * 35 / 100;
                            flag = true;
                            i++;
                            break;
                        default:
                            break;
                    }
                    tongthue += thue;
                   
                }
                while (tnChiuThue > 0 && flag==false);

                return tongthue;
            }

        }

        /// <summary>
        /// tính thu nhập trước thuế từ thu nhập NET
        /// </summary>
        /// <param name="thuNhapNET">thu nhập NET</param>
        /// <param name="nguoiPhuThuoc">số lượng người phụ thuộc</param>
        /// <returns>thu nhập trước thuế</returns>
        public double ThuNhapTinhThue(double thuNhapNET,int nguoiPhuThuoc)
        {
            double thuNhapTinhThue = 0;           

            //thu nhập quy đổi
            double TNQD = thuNhapNET - (11000000 + nguoiPhuThuoc * 4400000);


            //cách tính theo Phụ lục 02/PL-TNCN Ban hành kèm theo Thông tư số 111/2013/TT-BTC
            //ngày 15/8/2013 của Bộ Tài chính
            if (TNQD <= 4500000)
            {
                thuNhapTinhThue = TNQD / 0.95;
            }
            else if (TNQD>4750000 && TNQD<9250000)
            {
                thuNhapTinhThue = (TNQD-250000) / 0.9;
            }
            else if (TNQD > 9250000 && TNQD < 16050000)
            {
                thuNhapTinhThue = (TNQD - 750000) / 0.85;
            }
            else if (TNQD > 16050000 && TNQD < 27250000)
            {
                thuNhapTinhThue = (TNQD - 1650000) / 0.8;
            }
            else if (TNQD > 27250000 && TNQD < 42250000)
            {
                thuNhapTinhThue = (TNQD -3250000) / 0.75;
            }
            else if (TNQD > 42250000 && TNQD < 61850000)
            {
                thuNhapTinhThue = (TNQD - 5850000) / 0.7;
            }
            else if (TNQD > 61850000 )
            {
                thuNhapTinhThue = (TNQD - 9850000) / 0.65;
            }

            return thuNhapTinhThue;
        }

        /// <summary>
        /// Tính bảo hiểm xã hội
        /// </summary>
        /// <param name="thuNhap">Thu nhập gross</param>
        /// <param name="mucluongdongBH">Mức lương muốn đóng bảo hiểm</param>
        /// <returns>Giá trị tiền bảo hiểm cần chi trả</returns>
        public double BHXH(double thuNhap, double mucluongdongBH=0)
        {

            double result, BHXHTN, BHYT, BHTN;
            // Tính tiền bỏ ra để đóng BHXH
            if (mucluongdongBH > 0)
            {
                //Bảo hiểm xã hội khi người dùng muốn thay đổi gói đóng
                BHTN = (mucluongdongBH * 1) / 100;
                BHXHTN = (mucluongdongBH * 8) / 100;
                BHYT = (mucluongdongBH * 1.5) / 100;
            }
            else
            {
                //Bảo hiểm xã hội
                BHTN = (thuNhap * 1) / 100;

                //BHXH và BHYT có gói đóng lớn nhất là 29tr800
                if (thuNhap > 29800000)
                {
                    BHXHTN = (29800000 * 8) / 100;
                    BHYT = (29800000 * 1.5) / 100;
                }
                else
                {
                    BHXHTN = (thuNhap * 8) / 100;
                    BHYT = (thuNhap * 1.5) / 100;
                }
            }

            result = BHTN + BHXHTN + BHYT;
            return result;

        }
    }
    static void Main(string[] args)
    {
           
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("Bạn muốn tính lương theo cách nào ?");
            Console.WriteLine("1. Tính lương từ Gross sang NET");
            Console.WriteLine("2. Tính lương từ NET sang Gross");
            Console.WriteLine("0. Thoát chương trình");
            int chossed;
            int.TryParse(Console.ReadLine(),out chossed);

            if (chossed == 0)
            {
                break;
            }
            ExTest exTest = new ExTest();
            switch (chossed)
            {
                //Tính thu nhập gross sang net
                case 1:
                  
                    Console.WriteLine("Nhập thu nhập gross của bạn: ");
                    double thuNhap;
                    double.TryParse(Console.ReadLine(),out thuNhap);
                    while(thuNhap == 0 || thuNhap<0)
                    {
                        Console.WriteLine("Bạn chưa nhập thu nhập gross hoặc mức lương gross của bạn chưa hợp lệ. Vui lòng nhập thu nhập lương gross của bạn: ");
                        double.TryParse(Console.ReadLine(), out thuNhap);
                    }
                    Console.WriteLine("Nhập mức lương muốn đóng BH của bạn(bỏ qua nếu muốn đóng theo lương chính thức): ");
                    double luongdongBH;
                    double.TryParse(Console.ReadLine(),out luongdongBH);
                    while ( luongdongBH < 0 )
                    {
                        Console.WriteLine("Mức lương đóng bảo hiểm không hợp lệ. Vui lòng nhập lại. ");
                        double.TryParse(Console.ReadLine(), out luongdongBH);
                    }

                    Console.WriteLine("Nhập vùng làm việc của bạn( từ vùng 1-4) ");
                    int vung;
                    int.TryParse(Console.ReadLine(),out vung);

                    while (vung!= 1 && vung != 2 && vung != 3 && vung != 4)
                    {
                        Console.WriteLine("Bạn chưa nhập vùng hoặc vùng bạn nhập không đúng. Vui lòng nhập lại ");
                        int.TryParse(Console.ReadLine(), out vung);
                    }

                    Console.WriteLine("Nhập số lượng người phụ thuộc: ");
                    int nguoiPhuThuoc;
                    int.TryParse(Console.ReadLine(),out nguoiPhuThuoc);

                    while (nguoiPhuThuoc<0)
                    {
                        Console.WriteLine("Bạn chưa nhập người phụ thuộc hoặc người phụ thuộc của bạn không hợp lệ. Vui lòng nhập lại ");
                        int.TryParse(Console.ReadLine(), out vung);
                    }


                    
                    var b = exTest.TinhLuongNet(thuNhap, vung, nguoiPhuThuoc, luongdongBH);
                    Console.WriteLine("Thuế thu nhập cá nhân: "+b.thue.ToString() + '\n' +"Mức lương Net: " +b.luong.ToString() + '\n' +"Tiền đóng bảo hiểm xã hội : "+ b.BHXH.ToString());
                    break;

                    //tính thu nhập từ net=> gross
                case 2:

                    Console.WriteLine("Nhập thu nhập NET của bạn: ");
                    double thuNhapNET;
                    double.TryParse(Console.ReadLine(), out thuNhapNET);
                    while (thuNhapNET == 0 || thuNhapNET < 0)
                    {
                        Console.WriteLine("Bạn chưa nhập thu nhập NET hoặc mức lương NET của bạn chưa hợp lệ. Vui lòng nhập thu nhập lương NET của bạn: ");
                        double.TryParse(Console.ReadLine(), out thuNhapNET);
                    }                                    

                    Console.WriteLine("Nhập số lượng người phụ thuộc: ");
                    int nguoiPhuThuocNET;
                    int.TryParse(Console.ReadLine(), out nguoiPhuThuocNET);

                    while (nguoiPhuThuocNET < 0)
                    {
                        Console.WriteLine("Bạn chưa nhập người phụ thuộc hoặc người phụ thuộc của bạn không hợp lệ. Vui lòng nhập lại ");
                        int.TryParse(Console.ReadLine(), out vung);
                    }
                   
                    var case2 = exTest.TinhLuongGross(thuNhapNET, nguoiPhuThuocNET);
                    Console.WriteLine("Thuế thu nhập cá nhân: " + case2.thue.ToString() + '\n' + "Mức lương trước thuế: " + case2.luong.ToString() + '\n' );
                 
                    break;
              
                default:
                    Console.WriteLine("Lựa chọn không đúng, vui lòng nhập lại.");
                    break;
            }
           
        }


    }
}
