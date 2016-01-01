using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESApi.Models.ViewModel;

namespace ESApi.Models.Code
{
    public class OrderCode
    {
        public ESDBEntities db = new ESDBEntities();
        public string CreateId()
        {
            string idcreated = "";
            //lấy mã số đơn hàng cuối cùng trong bảng
            DONHANG lastitem = db.DONHANGs.ToList().Last();
            if (lastitem != null)
            {
                //tien hanh tach chuoi de lay 3 so cuoi cung
                string lastitemid = lastitem.MA.ToString();
                string idfigure = lastitemid.Substring(1);
                int figure = int.Parse(idfigure);
                figure++;
                if (figure <= 99) //  co 2 chu so
                {
                    idcreated = "A0" + figure;
                }
                else
                {
                    idcreated = "A" + figure;
                }
            }
            else
            {
                idcreated = "A001";
            }

            return idcreated;
        }

        public void AddOrder(OrderDetail receiver)
        {
            //khoi tao id
            string id = CreateId();
            if (id != "")
            {
                //thêm đơn hàng
                DONHANG order = new DONHANG();
                order.MA = id;
                order.TONGTIEN = receiver.TotalMoney;
                order.NGAYDATHANG = DateTime.Now;
                order.NGAYNHANHANG = DateTime.Now.AddDays(3);
                order.TENNGUOINHAN = receiver.receive.name;
                order.DIACHINHAN = receiver.receive.address;
                order.DIENTHOAINGUOINHAN = receiver.receive.phone;
                order.TRANGTHAI = 1;
                order.DAXOA = false;

                // lưu đơn hàng xuống
                db.DONHANGs.Add(order);
                db.SaveChanges();

                //lưu bảng chi tiết đơn hàng
                for (int i = 0; i < receiver.listCartSession.Count; i++)
                {
                    CHITIETDONHANG orderdetail = new CHITIETDONHANG();
                    orderdetail.DONHANG = id;
                    orderdetail.SANPHAM = receiver.listCartSession[i].sp.MA;
                    orderdetail.SOLUONG = receiver.listCartSession[i].soluong;
                    orderdetail.THANHTIEN = receiver.listCartSession[i].sp.DONGIABAN*receiver.listCartSession[i].soluong;
                    orderdetail.DAXOA = false;

                    db.CHITIETDONHANGs.Add(orderdetail);

                    //giam so luong san pham xuong
                    var listsp = db.SANPHAMs.ToList();
                    foreach (var item in listsp)
                    {
                        if (item.MA == receiver.listCartSession[i].sp.MA)
                        {
                            item.SOLUONG = item.SOLUONG - receiver.listCartSession[i].soluong;
                            break;
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

    }
}