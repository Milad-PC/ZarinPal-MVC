using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zarinpal
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "مبلغ")]
        public long Amount { get; set; } //Toman
        [Required]
        [Display(Name = "شناسه پرداخت")]
        public string Authority { get; set; }
        [Display(Name = "رهگیری پرداخت")]
        public string RefrenceId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        /* Status Code
         * -1 : اطلاعات ارسال شده ناقص است
         * -2 : آی پی ویا مرچنت کد پذیره صحیح نیست
         * -3 : با توجه به محدودیت های شاپرک امکان پرداخت با رقم درخواستی میسر نمی باشد
         * -4 : پذیرنده سطح نقره ای نیست
         * -11 : درخواست یافت نشد
         * -12 : امکان ویرایش درخواست میسر نیست
         * -21 : هیچ نوع عملیات مالی یافت نشد
         * -22 : تراکنش ناموفق بود
         * -33 : رقم تراکنش با رقم پرداخت مطابقت ندارد
         * -34 : سقف تقسیم تراکنش از لحاظ تعداد یا رقم 
         * -40 : اجازه دسترسی به متد وجود ندارد
         * -41 : اطلاعات additionalData غیر معتبر است
         * -42 : مدت زمان معتبر عمر شناسه پرداخت 30 دقیقه است
         * -54 : درخواست موردنظر آرشیو شده است
         * 100 : عملیات با موفقیت انجام گردید
         * 101 : عملیات پرداخت موفق بوده و قبلا تایید تراکنش انجام شده
         * 0 : وارد درگاه نشد
         */
        public int Status { get; set; } = 0;
        //UserId ,ProductId
    }
}