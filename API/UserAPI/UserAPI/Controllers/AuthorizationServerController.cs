using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using UserAPI.Models;
using UserAPI.Models.ViewModels;

namespace UserAPI.Controllers
{
    public class AuthorizationServerController : ApiController
    {

        //ở đây sẽ viết hàm trả về accesstoken
        [HttpGet]
        [Route("api/oauth/{clientid}/{AUTHORIZATION_CODE}/accesstoken")]
        public IHttpActionResult RequestAccesstoken(int clientid, string AUTHORIZATION_CODE)
        {
            using(GHDBEntities db = new GHDBEntities()){
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.AUTHORIZATIONCODE == AUTHORIZATION_CODE && nd.DAXOA.Value.Equals(false)).SingleOrDefault();
                
                //tạo accesstoken
                string accesstoken = createAccessToken(15);
                //lưu accesstoken xuống database
                nguoidung.ACCESSTOKEN = accesstoken;
                db.SaveChanges();

                //đưa vào model
                AccessTokenViewModel accesstokenreturn = new AccessTokenViewModel();
                accesstokenreturn.ACCESSTOKEN = accesstoken;

                /*
                
                AutoMapper.Mapper.CreateMap<NGUOIDUNG,UserViewModel>();
                UserViewModel user = AutoMapper.Mapper.Map<NGUOIDUNG,UserViewModel>(nguoidung);
                 */
                //trả accesstoken về
                return Ok(accesstokenreturn);
            }
        }


        public string createAccessToken(int maxSize)
        {

            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
