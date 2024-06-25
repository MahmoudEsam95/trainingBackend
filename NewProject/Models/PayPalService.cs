
//using PayPal.Api;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;

//public class PayPalService
//{
//    private readonly IConfiguration _configuration;

//    public PayPalService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    private APIContext GetAPIContext()
//    {
//        var config = new Dictionary<string, string>
//        {
//            { "mode", _configuration["PayPal:Mode"] }
//        };

//        var accessToken = new OAuthTokenCredential(
//            _configuration["PayPal:ClientId"],
//            _configuration["PayPal:ClientSecret"],
//            config
//        ).GetAccessToken();

//        return new APIContext(accessToken);
//    }

//    public Payment CreatePayment(string redirectUrl, decimal paymentAmount)
//    {
//        var apiContext = GetAPIContext();

//        var payer = new Payer() { payment_method = "paypal" };

//        var redirUrls = new RedirectUrls()
//        {
//            cancel_url = "http://localhost:4200/cancel",
//            return_url = "http://localhost:4200/success"

//        };

//        var itemList = new ItemList() { items = new List<Item>() };

//        itemList.items.Add(new Item()
//        {
//            name = "Service Fee",
//            currency = "USD",
//            price = paymentAmount.ToString("F2"),
//            quantity = "1",
//            sku = "sku"
//        });

//        var details = new Details()
//        {
//            tax = "0",
//            subtotal = paymentAmount.ToString("F2")
//        };

//        var amount = new Amount()
//        {
//            currency = "USD",
//            total = paymentAmount.ToString("F2"),
//            details = details
//        };

//        var transactionList = new List<Transaction>();

//        transactionList.Add(new Transaction()
//        {
//            description = "Service Payment",
//            invoice_number = Guid.NewGuid().ToString(),
//            amount = amount,
//            item_list = itemList
//        });

//        var payment = new Payment()
//        {
//            intent = "sale",
//            payer = payer,
//            transactions = transactionList,
//            redirect_urls = redirUrls
//        };

//        return payment.Create(apiContext);
//    }
//}


