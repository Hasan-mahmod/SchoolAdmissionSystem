using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionSystemAPI.ExtraCs
{
    public class OtherHelper
    {
        //file Save 
        public string SaveFil(IFormFile file, string folderpath, string oldphoto)
        {
            if (file.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                    }

                    if (oldphoto != null || oldphoto != string.Empty)
                    {
                        if (System.IO.File.Exists(folderpath + oldphoto))
                        {
                            System.IO.File.Delete(folderpath + oldphoto);
                        }
                    }
                    var filePath = Path.Combine(folderpath, file.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                    return file.FileName;

                }
                catch (Exception ex)
                {
                    var e = ex.InnerException;
                }
            }
            return "";
        }

        //send Email 
        public string Sendmail(string to,string subject, string message)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.Credentials = new NetworkCredential("schooladmissionbd@gmail.com", "Test@123");
            var msg = new MailMessage();
            msg.From = new MailAddress("schooladmissionbd@gmail.com");
            msg.To.Add(new MailAddress(to));
            msg.IsBodyHtml = true;
            msg.Body = message;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.Priority = MailPriority.Normal;
            msg.Subject = subject;
            try
            {
                client.Send(msg);
                return "Success";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }

        }

        // Auto Password Genaraton 
        public string GeneratePassword(int length)
        {

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}
