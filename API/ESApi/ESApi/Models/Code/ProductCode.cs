using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.Code
{
    public class ProductCode
    {
        public ESDBEntities db = new ESDBEntities();
        public ProductList GetProductList()
        {
            ProductList productlist = new ProductList();

            //New list
            var newList = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false) && sp.SANPHAMMOI.Value.Equals(true)).ToList();
            
            for (int i = 0; i < newList.Count; i++)
            {
                int makhuyenmai = newList[i].MAKHUYENMAI.Value;
                if (makhuyenmai != 0)
                {
                    var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                    double dongiagiam = (double)(newList[i].DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;
                    productlist.newListPromotion.Add(dongiagiam);
                }
                else
                {
                    productlist.newListPromotion.Add(0);
                }
            }

            //Special List
            var specialList = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false) && sp.SANPHAMBANCHAY.Value.Equals(true)).ToList();
            for (int i = 0; i < specialList.Count; i++)
            {
                int makhuyenmai = specialList[i].MAKHUYENMAI.Value;
                if (makhuyenmai != 0)
                {
                    var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                    double dongiagiam = (double)(specialList[i].DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;
                    productlist.specialListPromotion.Add(dongiagiam);
                }
                else
                {
                    productlist.specialListPromotion.Add(0);
                }
            }

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            productlist.newList = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(newList);
            productlist.specialList = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(specialList);
            return productlist;
        }

        public SpecialProduct GetSpecialProduct()
        {
            SpecialProduct specialProductViewModel = new SpecialProduct();

            var specialproduct = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false) && sp.SANPHAMBANCHAY.Value.Equals(true)).ToList().First();
            int makhuyenmai = specialproduct.MAKHUYENMAI.Value;
            if (makhuyenmai != 0)
            {
                var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                specialProductViewModel.promotion = (double)(specialproduct.DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;

            }
            else
            {
                specialProductViewModel.promotion = 0;
            }

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            specialProductViewModel.specialproduct = Mapper.Map<SANPHAM, SANPHAMModel>(specialproduct);

            return specialProductViewModel;
        }


        public NewProduct GetNewProduct()
        {
            NewProduct newProductViewModel = new NewProduct();

            var newproduct = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false)  && sp.SANPHAMMOI.Value.Equals(true)).ToList().First();
            int makhuyenmai = newproduct.MAKHUYENMAI.Value;
            if (makhuyenmai != 0)
            {
                var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                newProductViewModel.promotion = (double)(newproduct.DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;

            }
            else
            {
                newProductViewModel.promotion = 0;
            }

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            newProductViewModel.newproduct = Mapper.Map<SANPHAM, SANPHAMModel>(newproduct);
            return newProductViewModel;
        }

        public DetailProduct GetDetailProduct(string id)
        {
            DetailProduct detail = new DetailProduct();

            //xu ly cho 1 san pham
            var detailProduct = db.SANPHAMs.Where(sp => sp.MA.Equals(id) && sp.DAXOA.Value.Equals(false) ).SingleOrDefault();
            int makhuyenmai = detailProduct.MAKHUYENMAI.Value;
            if (makhuyenmai != 0)
            {
                var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                double dongiagiam = (double)(detailProduct.DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;
                detail.detailProductPromotion = dongiagiam;
            }
            else
            {
                detail.detailProductPromotion = 0;
            }

            int manhasanxuat = detailProduct.NHASANXUAT.Value;
            var nhasanxuat = db.NHASANXUATs.Where(nsx => nsx.DAXOA.Value.Equals(false) && nsx.MA.Equals(manhasanxuat)).SingleOrDefault();
            detail.manufactoryName = nhasanxuat.TEN;

            int maloaisanpham = detailProduct.LOAISANPHAM.Value;
            var loaisanpham = db.LOAISANPHAMs.Where(lsp => lsp.DAXOA.Value.Equals(false) && lsp.MA.Equals(maloaisanpham)).SingleOrDefault();
            detail.typeProduct = loaisanpham.TEN;



            //tach chuoi lay mo ta

            string mota = detailProduct.MOTA;
            string[] arrListStr = mota.Split('-');
            foreach (var item in arrListStr)
            {
                if (!item.Equals(""))
                {
                    string[] eachdetail = item.Split(':');
                    detail.subjectDescription.Add(eachdetail[0]);
                    detail.contentDescription.Add(eachdetail[1]);
                }
            }




            //xu ly cho san pham lien quan
            //xu ly cho 1 san pham
            int _loaisanpham = detailProduct.LOAISANPHAM.Value;
            string _madetail = detailProduct.MA;
            var relativeList = db.SANPHAMs.Where(sp => sp.LOAISANPHAM.Value == _loaisanpham && !sp.MA.Equals(_madetail) && sp.DAXOA.Value.Equals(false)).Take(3).ToList();

            for (int i = 0; i < relativeList.Count; i++)
            {
                int mkm = relativeList[i].MAKHUYENMAI.Value;
                if (mkm != 0)
                {
                    var _mkm_tt = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                    double dgg = (double)(relativeList[i].DONGIABAN * (100 - _mkm_tt.NOIDUNG.Value)) / 100;
                    detail.relativeListPromotion.Add(dgg);
                }
                else
                {
                    detail.relativeListPromotion.Add(0);
                }
            }

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            detail.detailProduct = Mapper.Map<SANPHAM, SANPHAMModel>(detailProduct);
            detail.relativeList = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(relativeList);
            return detail;
        }

        public ProductList GetProductsByType(string name, int id)
        {
            ProductList productList = new ProductList();
            List<SANPHAM> list = new List<SANPHAM>();

            if (name.Equals("Default")) //hiển thị tất cả sản phẩm
            {
                list = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false)).ToList();
                productList.path = "Sản phẩm";
            }

            if (name.Equals("Category")) //lấy theo category
            {
                var category = db.LOAISANPHAMs.Where(lsp => lsp.MA == id && lsp.DAXOA.Value.Equals(false)).SingleOrDefault();
                productList.path = category.TEN;
                list = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false) && sp.LOAISANPHAM == id).ToList();
            }

            if (name.Equals("Manufactory")) //lấy theo nhà sản xuất
            {
                var manufactory = db.NHASANXUATs.Where(nsx => nsx.MA == id && nsx.DAXOA.Value.Equals(false)).SingleOrDefault();
                productList.path = manufactory.TEN;
                list = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false) && sp.NHASANXUAT == id).ToList();
            }

            if(id == -2)
            {
                productList.path = "Tìm kiếm";
                string search_string = name;
                list = db.SANPHAMs.Where(sp => sp.DAXOA.Value.Equals(false)  && (sp.NHASANXUAT1.TEN.Contains(search_string) || sp.TEN.Contains(search_string) || sp.LOAISANPHAM1.TEN.Equals(search_string))).ToList();
            }

            for (int i = 0; i < list.Count; i++)
            {
                int makhuyenmai =list[i].MAKHUYENMAI.Value;
                if (makhuyenmai != 0)
                {
                    var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                    double dongiagiam = (double)(list[i].DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;
                    productList.newListPromotion.Add(dongiagiam);
                }
                else
                {
                    productList.newListPromotion.Add(0);
                }
            }

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            productList.newList = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(list);

            return productList;
        }

        public ProductList GetProductsByMultiType(SearchModel seach)  //Advance search
        {
            ProductList productList = new ProductList();
            List<SANPHAM> list = new List<SANPHAM>();

            if(seach.KhuyenMai)
                list = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.TEN.Contains(seach.Ten) && sp.NHASANXUAT1.TEN.Contains(seach.NhaSanSuat) && sp.LOAISANPHAM1.TEN.Contains(seach.LoaiSanPham) && sp.DONGIABAN >= seach.GiaToiThieu && sp.DONGIABAN < seach.GiaToiDa && sp.SANPHAMBANCHAY == seach.SPBanChay && sp.MAKHUYENMAI != 0).ToList();
            else
            {

                list = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.TEN.Contains(seach.Ten) && sp.NHASANXUAT1.TEN.Contains(seach.NhaSanSuat) && sp.LOAISANPHAM1.TEN.Contains(seach.LoaiSanPham) && sp.DONGIABAN >= seach.GiaToiThieu && sp.DONGIABAN < seach.GiaToiDa && sp.SANPHAMBANCHAY == seach.SPBanChay && sp.MAKHUYENMAI == 0).ToList();
            }

            if(list.Count > 0)
                productList.path = "Tìm kiếm nâng cao: có tất cả " + list.Count + " kết quả";
            else
            {
                productList.path = "Tìm kiếm nâng cao: không tìm thấy kết quả nào";
            }

            for (int i = 0; i < list.Count; i++)
            {
                int makhuyenmai = list[i].MAKHUYENMAI.Value;
                if (makhuyenmai != 0)
                {
                    var _mkm = db.KHUYENMAIs.Where(km => km.DAXOA.Value.Equals(false) && km.MA.Equals(makhuyenmai)).SingleOrDefault();
                    double dongiagiam = (double)(list[i].DONGIABAN * (100 - _mkm.NOIDUNG.Value)) / 100;
                    productList.newListPromotion.Add(dongiagiam);
                }
                else
                {
                    productList.newListPromotion.Add(0);
                }
            }
            
            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            productList.newList = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(list);

            return productList;
        }

        public SANPHAMModel GetProduct(string id)
        {
            var product = db.SANPHAMs.Where(sp => sp.MA == id && sp.DAXOA == false).SingleOrDefault();
            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            return Mapper.Map<SANPHAM, SANPHAMModel>(product);
        }
    }
}