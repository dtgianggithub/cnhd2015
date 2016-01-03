using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserAPI.Models;

namespace UserAPI.Controllers
{
    public class AllAccountController : Controller
    {
        GHDBEntities db = new GHDBEntities();

        // GET: AllAccount
        public ActionResult UserLogin()
        {
            string username = Session["username"] as string;
            if (username != null)
            {
                return RedirectToAction("UserDetail");
            }
            
            ViewBag.Message = "";
            return View();
        }



        [HttpPost]
        public ActionResult UserLogin(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            //kiểm tra thông tin đăng nhập tại đây
            if (username != "" && password != "")
            {
                //kiểm tra với database
                
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username  &&  nd.DAXOA.Value.Equals(false)).SingleOrDefault();
                if (nguoidung != null) // đúng username và mật khẩu
                {
                    password = encryptSHA(password);
                    if (password == nguoidung.MATKHAU)
                    {
                        //lưu session
                        Session["username"] = username;
                        Session["password"] = password;
                        return RedirectToAction("UserDetail");
                    }
                   
                }


            }
            ViewBag.Message = "Wrong username or password";
            return View();
        }


        public ActionResult UserLogoff()
        {
            Session["username"] = null;
            Session["password"] = null;
            return RedirectToAction("Index","Home");
        }

        public ActionResult UserDetail()
        {
            string username = Session["username"] as string;
            if (username == null)
            {
                NGUOIDUNG nguoidungnull = new NGUOIDUNG();
                return View(nguoidungnull);
            }
            NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username && nd.DAXOA.Value.Equals(false)).SingleOrDefault();
            return View(nguoidung);
        }


        public ActionResult UserRegister()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(FormCollection form)
        {
            //nhận thông tin đầu vào tại đây

            //kiểm tra xem tên đăng nhập có rỗng hay có tồn tại trong hệ thống hay không
            string username = form["username"];
            if (username == "")
            {
                ViewBag.Message = "Empty input";
                return View();
            }

            //kiểm tra xem tên đăng nhập đã tồn tại hay chưa ?
            if(db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username && nd.DAXOA.Value.Equals(false)).SingleOrDefault() != null) //đã tồn tại
            {
                ViewBag.Message = "Existed username";
                return View();
            }

            //kiểm tra mật khẩu
           string password = form["password"];
            if(password == "" || password.Count() < 6)
            {
                ViewBag.Message = "Wrong password";
                return View();
            }

            string repeatpassword = form["repeatpassword"];
            //kiểm tra giống password hay không
            if (repeatpassword != password)
            {
                ViewBag.Message = "Password and repeat not same";
                return View();
            }

            //kiểm tra tên
            string name = form["name"];
            if(name == "")
            { ViewBag.Message = "Empty name"; return View(); }

            //lấy giới tính
            string gender = form["gender"];

            //lấy ngày sinh
            DateTime date = DateTime.Parse(form["date"]);
            if(date > DateTime.Now){
                return View();
            }

            //lấy địa chỉ
            string address = form["address"];

            //lấy số điện thoại
            string phone = form["phone"];
            if (phone.Count() < 10 && phone.Count() > 11)
            {
                ViewBag.Message = "Phone number wrong";
                return View();
            }


            //kiểm tra tên đăng nhập có phải là email đúng hay không
            if (!CheckEmail(username))  // Check email valid
            {
                ViewBag.Message = "Wrong email";
                return View();
            }


            // nếu không có gì xảy ra thì tại đây đăng ký sẽ thành công
            NGUOIDUNG nguoidung = new NGUOIDUNG();
            nguoidung.TENDANGNHAP = username;
            nguoidung.MATKHAU = encryptSHA(password);
            nguoidung.HOTEN = name;
            nguoidung.NGAYSINH = date;
            nguoidung.GIOITINH = gender;
            nguoidung.DIACHI = address;
            nguoidung.SDT = phone;
            nguoidung.MAKICHHOAT = RandomPass(15);
            nguoidung.DAXOA = true;

            db.NGUOIDUNGs.Add(nguoidung);
            db.SaveChanges();


            //gửi email kích hoạt
            SendCodeActiveUser(nguoidung.TENDANGNHAP, nguoidung.MAKICHHOAT, nguoidung.TENDANGNHAP);

            ViewBag.Message = "Register Successfully !";
            return View();
        }

        public ActionResult ApplicationLogin()
        {
            string applicationname = Session["applicationname"] as string;
            if (applicationname != null)
            {
                return RedirectToAction("ApplicationDetail");
            }

            ViewBag.Message = "";
            return View();
            
        }

        [HttpPost]
        public ActionResult ApplicationLogin(FormCollection form)
        {
            string applicationname = form["applicationname"];
            string password = form["password"];
            //kiểm tra thông tin đăng nhập tại đây
            if (applicationname != "" && password != "")
            {
                //kiểm tra với database

                UNGDUNG ungdung = db.UNGDUNGs.Where(ud => ud.TENDANGNHAP == applicationname && ud.DAXOA.Value.Equals(false)).SingleOrDefault();
                if (ungdung != null) // đúng username và mật khẩu
                {
                    password = encryptSHA(password);
                    if (password == ungdung.MATKHAU)
                    {
                        //lưu session
                        Session["applicationname"] = applicationname;
                        Session["applicationpassword"] = password;
                        return RedirectToAction("ApplicationDetail");
                    }

                }


            }
            ViewBag.Message = "Wrong applicationname or password";
            return View();
        }


        public ActionResult ApplicationRegister()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult ApplicationRegister(FormCollection form)
        {
            //nhận thông tin đầu vào tại đây

            //kiểm tra xem tên đăng nhập có rỗng hay có tồn tại trong hệ thống hay không
            string applicationname = form["applicationname"];
            if (applicationname == "")
            {
                ViewBag.Message = "Empty input";
                return View();
            }

            //kiểm tra xem tên đăng nhập đã tồn tại hay chưa ?
            if (db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == applicationname && nd.DAXOA.Value.Equals(false)).SingleOrDefault() != null) //đã tồn tại
            {
                ViewBag.Message = "Existed applicationname";
                return View();
            }

            //kiểm tra mật khẩu
            string applicationpassword = form["applicationpassword"];
            if (applicationpassword == "" || applicationpassword.Count() < 6)
            {
                ViewBag.Message = "Wrong applicationpassword";
                return View();
            }

            string repeatpassword = form["repeatpassword"];
            //kiểm tra giống password hay không
            if (repeatpassword != applicationpassword)
            {
                ViewBag.Message = "Password and repeat not same";
                return View();
            }

            //kiểm tra tên
            string name = form["name"];
            if (name == "")
            { ViewBag.Message = "Empty name"; return View(); }

           

            //lấy địa chỉ
            string domain = form["domain"];
            if (domain == "")
            {
                ViewBag.Message = "Empty domain"; return View();
            }

        


            //kiểm tra tên đăng nhập có phải là email đúng hay không
            if (!CheckEmail(applicationname))  // Check email valid
            {
                ViewBag.Message = "Wrong applicationname";
                return View();
            }


            // nếu không có gì xảy ra thì tại đây đăng ký sẽ thành công
            UNGDUNG ungdung = new UNGDUNG();
            ungdung.TENDANGNHAP = applicationname;
            ungdung.MATKHAU = encryptSHA(applicationpassword);
            ungdung.TEN = name;
            ungdung.DOMAINURL = domain;
            ungdung.MAKICHHOAT = RandomPass(15);
            ungdung.DAXOA = true;

            db.UNGDUNGs.Add(ungdung);
            db.SaveChanges();


            //gửi email kích hoạt
            SendCodeActiveApplication(ungdung.TENDANGNHAP, ungdung.MAKICHHOAT, ungdung.TENDANGNHAP);

            ViewBag.Message = "Register Successfully !";
            return View();
        }


        public ActionResult ApplicationDetail()
        {
            string applicationname = Session["applicationname"] as string;
            if (applicationname == null)
            {
                UNGDUNG ungdungnull = new UNGDUNG();
                return View(ungdungnull);
            }
            UNGDUNG ungdung = db.UNGDUNGs.Where(ud => ud.TENDANGNHAP == applicationname && ud.DAXOA.Value.Equals(false)).SingleOrDefault();
            return View(ungdung);
        }

        public ActionResult ApplicationLogoff()
        {
            Session["applicationname"] = null;
            Session["applicationpassword"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ActiveApplication(string username, string code)
        {
            UNGDUNG ungdung = db.UNGDUNGs.Where(ud => ud.TENDANGNHAP == username).SingleOrDefault();
            ViewBag.info = "";
            if (ungdung != null)
            {
                if (!ungdung.DAXOA.Value)
                {
                    ViewBag.info = "Already active this account";
                }
                else
                {
                    if (ungdung.MAKICHHOAT == code)
                    {
                        ViewBag.info = "Active successfully";
                        ungdung.DAXOA = false;
                        db.SaveChanges();
                    }

                    else
                        ViewBag.info = "Wrong link active";
                }
            }
            return View();
        }
        public ActionResult ActiveUser(string username, string code)
        {
            NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username).SingleOrDefault();
            ViewBag.info = "";
            if (nguoidung != null)
            {
                if (!nguoidung.DAXOA.Value)
                {
                    ViewBag.info = "Already active this account";
                }
                else
                {
                    if (nguoidung.MAKICHHOAT == code)
                    {
                        ViewBag.info = "Active successfully";
                        nguoidung.DAXOA = false;
                        db.SaveChanges();
                    }

                    else
                        ViewBag.info = "Wrong link active";
                }
            }
            return View();
        }


        public bool SendCodeActiveUser(string ToEmail, string Code, String username)  // Get password from Email
        {
            try
            {
                // MailMessage class is present is System.Net.Mail namespace
                MailMessage mailMessage = new MailMessage("namy.web.shop@gmail.com", ToEmail);


                // StringBuilder class is present in System.Text namespace
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Hi,<br/><br/>");
                sbEmailBody.Append("I am congratulation you register successfully on our system");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("To active your accout click below link, please<br/><br/>");
                sbEmailBody.Append("http://ghapi.somee.com/AllAccount/ActiveUser?username=" + username + "&code=" + Code);
                sbEmailBody.Append("<br/><br/>");
                sbEmailBody.Append("<b> Best regards</b>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Reset Your Password";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "namy.web.shop@gmail.com",
                    Password = "Khtn1212111"
                };

                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool SendCodeActiveApplication(string ToEmail, string Code, String username)  // Get password from Email
        {
            try
            {
                // MailMessage class is present is System.Net.Mail namespace
                MailMessage mailMessage = new MailMessage("namy.web.shop@gmail.com", ToEmail);


                // StringBuilder class is present in System.Text namespace
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Hi,<br/><br/>");
                sbEmailBody.Append("I am congratulation you register successfully on our system");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("To active your accout click below link, please<br/><br/>");
                sbEmailBody.Append("http://ghapi.somee.com/AllAccount/ActiveApplication?username=" + username + "&code=" + Code);
                sbEmailBody.Append("<br/><br/>");
                sbEmailBody.Append("<b> Best regards</b>");

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Reset Your Password";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "namy.web.shop@gmail.com",
                    Password = "Khtn1212111"
                };

                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public string RandomPass(int maxSize)  // Random string
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

        public string encryptSHA(string data)   // Encode password
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = sha.ComputeHash(encoder.GetBytes(data));
            return System.Text.Encoding.UTF8.GetString(hashedBytes);
        }

        public bool CheckEmail(string Email)  //Check Email exist or not
        {
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<YourGmailIDHere@gmail.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + Email.Trim() + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (GetResponseCode(ResponseString) == 550)
            {
                return false;
            }
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();
            return true;
        }

        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);  // Convert string to byte
        }

        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }



    }
}