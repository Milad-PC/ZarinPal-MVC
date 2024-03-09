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
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendReq(long Amount)
        {
            ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();

            String MerchantID = "71c705f8-bd37-11e6-aa0c-000c295eb8fc";
            String CallbackURL = "https://localhost:44324/Home/Verfication";
            String Description = "This is Test Payment";

            ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);

            /*
             * Save Order To DataBase
             * 
             */


            zarinpal.EnableSandboxMode();
            var res = zarinpal.InvokePaymentRequest(pr);
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

            if (Status != "OK")
            {
                Response.Write("<script>alert('Purchase unsuccessfully')</script>");
                return View(Json(Status));
            }

            var zarinpal = ZarinPal.ZarinPal.Get();

            String Authority = collection["Authority"];
            String MerchantID = "71c705f8-bd37-11e6-aa0c-000c295eb8fc";

            /*
             * Read Order To DataBase
             * 
             */

            long Amount = 250000;//Amount Az Order DataBase

            var pv = new ZarinPal.PaymentVerification(MerchantID, Amount, Authority);
            var verificationResponse = zarinpal.InvokePaymentVerification(pv); // For use WithExtra method InvokePaymentVerificationWithExtra()
            if (verificationResponse.Status == 100)
            {
                ViewBag.Message = "تراکنش موفق";
                ViewBag.Status = verificationResponse.Status;
                ViewBag.RefId = verificationResponse.RefID;
                return View(Json(verificationResponse));
            }
            else
            {
                ViewBag.Message = "تراکنش ناموفق";
                ViewBag.Status = verificationResponse.Status;
                ViewBag.RefId = verificationResponse.RefID;
                return View(Json(verificationResponse));
            }
        }
    }
}