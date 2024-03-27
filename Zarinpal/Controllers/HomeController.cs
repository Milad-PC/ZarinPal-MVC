using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ZarinPal;

namespace Zarinpal.Controllers
{
    public class HomeController : Controller
    {
        MyContext db = new MyContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendReq(long Amount)
        {
            //Amount Bayad Be TOMAN Bashe
            ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();

            String MerchantID = "71c705f8-bd37-11e6-aa0c-000c295eb8fc";//Code Dargah ZarinPal
            String CallbackURL = "https://localhost:44324/Home/Verfication";
            String Description = "لایت کمپانی";

            ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);

            zarinpal.EnableSandboxMode();// Use .DisableSandboxMode()
            var res = zarinpal.InvokePaymentRequest(pr);

            //Save Order To DataBase
            db.Orders.Add(new Order()
            {
                Amount = Amount,
                Authority = res.Authority
            });
            db.SaveChanges();
            //END - Save Order To DataBase

            if (res.Status == 100)
            {
                return Redirect(res.PaymentURL);
            }
            else return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Verfication()
        {
            var collection = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            String Status = collection["Status"];
            /*
            if (Status != "OK")
            {
                Response.Write("<script>alert('تراکنش ناموفق')</script>");
                return View(Json(Status));
            }
            */
            var zarinpal = ZarinPal.ZarinPal.Get();

            String Authority = collection["Authority"];
            String MerchantID = "71c705f8-bd37-11e6-aa0c-000c295eb8fc";

            //Read Order From DataBase
            Order ord = db.Orders.FirstOrDefault(o => o.Authority == Authority);
            if(ord == null)
            {
                ord.Status = -11;
                ViewBag.Message = "سفارش یافت نشد";
                ViewBag.Status = ord.Status;
                ViewBag.RefId = "سفارش یافت نشد";
                return View();
            }
            long Amount = ord.Amount;
            //END - Read Order From DataBase

            var pv = new ZarinPal.PaymentVerification(MerchantID, Amount, Authority);
            var verificationResponse = zarinpal.InvokePaymentVerification(pv); // For use WithExtra method InvokePaymentVerificationWithExtra()

            //Save Order To DataBase
            ord.Status = verificationResponse.Status;
            ord.RefrenceId = verificationResponse.RefID;
            db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //END - Save Order To DataBase


            if (verificationResponse.Status == 100)
            {
                ViewBag.Message = "تراکنش موفق";
                ViewBag.Status = verificationResponse.Status;
                ViewBag.RefId = verificationResponse.RefID;
                return View();
            }
            else
            {
                ViewBag.Message = "تراکنش ناموفق";
                ViewBag.Status = verificationResponse.Status;
                ViewBag.RefId = verificationResponse.RefID;
                return View();
            }
        }
    }
}