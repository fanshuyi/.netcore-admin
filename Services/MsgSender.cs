using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using IServices;
using Microsoft.Extensions.Options;
using Models;

namespace Services
{
    // This class is used by the application to send email for account confirmation and password
    // reset. For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig _ec;

        public EmailSender(IOptions<EmailConfig> emailConfig)
        {
            this._ec = emailConfig.Value;
        }

        public System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
            {
                return Task.CompletedTask;
            }

            var client = new SmtpClient(_ec.MailServerAddress, _ec.MailServerPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_ec.UserName, _ec.Password),
                EnableSsl = _ec.EnableSsl
            };

            var mailMessage = new MailMessage { From = new MailAddress(_ec.FromAddress, _ec.FromName) };
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            return client.SendMailAsync(mailMessage);

            //return Task.CompletedTask;
        }
    }

    //public class SmsSender : ISmsSender
    //{
    //    /// <summary>
    //    /// </summary>
    //    /// <param name="PhoneNumber">
    //    /// 下发手机号码，采用
    //    /// e.164标准，格式为+[国家或地区码][手机号]，单次请求最多支持200个手机号且要求全为境内手机号或全为境外手机号。例如：+8613711112222，其中前面有一个+号 ，86为国家码，13711112222为手机号。
    //    /// </param>
    //    /// <param name="code">
    //    /// [\"09879\",\"30\"]
    //    /// </param>
    //    /// <returns>
    //    /// </returns>
    //    public async Task<SendSmsResponse> UserLogin(string PhoneNumber, string TemplateParam)
    //    {
    //        var Endpoint = "sms.tencentcloudapi.com";
    //        var TemplateID = "555784";
    //        var SmsSdkAppid = "1400334691";//app key fef277d4f47ddfc18e2036aa10bd96d2
    //        var Sign = "易顺科技";

    //        Credential cred = new Credential
    //        {
    //            SecretId = "AKIDHWdFgvxEJF0Y5SOifTWRF0oeUIzZ90Xy",
    //            SecretKey = "ch0BF9Vl9PX9ctPn2EKG240xjMo4MVTK"
    //        };

    //        ClientProfile clientProfile = new ClientProfile();
    //        HttpProfile httpProfile = new HttpProfile
    //        {
    //            Endpoint = Endpoint
    //        };
    //        clientProfile.HttpProfile = httpProfile;

    //        string strParams = "{\"PhoneNumberSet\":[\"" + PhoneNumber + "\"],\"TemplateID\":\"" + TemplateID + "\",\"Sign\":\"" + Sign + "\",\"TemplateParamSet\":" + TemplateParam + ",\"SmsSdkAppid\":\"" + SmsSdkAppid + "\"}";

    //        SmsClient client = new SmsClient(cred, "", clientProfile);
    //        SendSmsRequest req = AbstractModel.FromJsonString<SendSmsRequest>(strParams);

    //        return await client.SendSms(req);
    //    }

    //    public async Task<SendSmsResponse> ContractAgree(string PhoneNumber)
    //    {
    //        var Endpoint = "sms.tencentcloudapi.com";
    //        var TemplateID = "693284";
    //        var SmsSdkAppid = "1400334691";//app key fef277d4f47ddfc18e2036aa10bd96d2
    //        var Sign = "易顺科技";

    //        Credential cred = new Credential
    //        {
    //            SecretId = "AKIDHWdFgvxEJF0Y5SOifTWRF0oeUIzZ90Xy",
    //            SecretKey = "ch0BF9Vl9PX9ctPn2EKG240xjMo4MVTK"
    //        };

    //        ClientProfile clientProfile = new ClientProfile();
    //        HttpProfile httpProfile = new HttpProfile
    //        {
    //            Endpoint = Endpoint
    //        };
    //        clientProfile.HttpProfile = httpProfile;

    //        string strParams = "{\"PhoneNumberSet\":[\"" + PhoneNumber + "\"],\"TemplateID\":\"" + TemplateID + "\",\"Sign\":\"" + Sign + "\",\"TemplateParamSet\":[],\"SmsSdkAppid\":\"" + SmsSdkAppid + "\"}";

    //        SmsClient client = new SmsClient(cred, "", clientProfile);
    //        SendSmsRequest req = AbstractModel.FromJsonString<SendSmsRequest>(strParams);

    //        return await client.SendSms(req);
    //    }
    //}
}