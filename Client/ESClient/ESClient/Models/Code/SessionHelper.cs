using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.Code
{
    public class SessionHelper
    {
        public static string GetUserSession()
        {
            var session = HttpContext.Current.Session["username"];

            if (session == null)
                return null;
            else
            {
                return session as string;
            }
        }

        public static void SetUserSession(string user)
        {
            HttpContext.Current.Session["username"] = user;
        }


        public static void SetTypeLoginSession(string type)
        {
            HttpContext.Current.Session["type"] = type;
        }

        public static string GetTypeLoginSession()
        {
            var session = HttpContext.Current.Session["type"];

            if (session == null)
                return null;
            else
            {
                return session as string;
            }
        }


        public static void SetAccessTokenSession(string accesstoken)
        {
            HttpContext.Current.Session["access"] = accesstoken;

        }

        public static string GetAccessTokenSession()
        {
            var session = HttpContext.Current.Session["access"];

            if (session == null)
                return null;
            else
            {
                return session as string;
            }
        }


        public static List<CartSession> GetCartSession(string username)
        {

            if (username == "")
            {
                var session = HttpContext.Current.Session["cart"];
                if (session == null)
                    return null;
                else
                {
                    return session as List<CartSession>;
                }
            }
            else
            {
                var session = HttpContext.Current.Session[username];
                if (session == null)
                    return null;
                else
                {
                    return session as List<CartSession>;
                }
            }



        }

        public static void SetCartSession(string username, List<CartSession> listCartSession)
        {
            if (username == "")
            {
                HttpContext.Current.Session["cart"] = listCartSession;
            }
            HttpContext.Current.Session[username] = listCartSession;
        }


        public static void SetReceiverSession(ReceiverViewModel receiver)
        {
            HttpContext.Current.Session["receiver"] = receiver;
        }


        public static ReceiverViewModel GetReceiverSession()
        {
            var session = HttpContext.Current.Session["receiver"];
            return session as ReceiverViewModel;
        }

    }
}